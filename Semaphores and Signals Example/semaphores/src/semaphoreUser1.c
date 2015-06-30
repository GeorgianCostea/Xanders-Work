/*
 * semaphore1.c
 *
 * A simple example of semaphore control.
 * It will launch s2 and negotiate control over
 * a file with semaphores.
 */

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/sem.h>

#include "../inc/semaphoreStruct.h"

char *info[10] = {
	"apple", "beef", "cat", "dog", "elephant", 
	"green", "house", "igloo", "juice", "kite"
};

int main (void)
{
	int semid;
	char tmp[20];
	FILE *fp;
	int x;


	printf ("(USER1) This program will use critical regions.\n");
	printf ("(USER1) It will mark the critical region with a semaphore\n");
	printf ("(USER1) and only when instructed, will a common file between\n");
	printf ("(USER1) USER1 and USER2 be accessed.\n\n");

	printf ("(USER1) Obtaining semaphores for use ...\n");
	semid = semget (IPC_PRIVATE, 1, IPC_CREAT | 0666);

	if (semid == -1)
	{
	  printf ("(USER1) GRRRRR ... couldn't get semaphores!\n");
	  exit (1);
	}

	printf ("(USER1) Our semID is %d\n", semid);

	printf ("(USER1) Will initialize the semaphore to a known value ...\n");
	if (semctl (semid, 0, SETALL, init_values) == -1) 
	{
	  printf ("(USER1) GRRRRR... Can't initialize semaphores\n");
	  exit (1);
	}

	printf ("(USER1) Now that we have semaphores, let's alternate control over a shared file with USER2.\n");

	switch (fork()) 
	{
	  case 0:
	  {
	    /*
	     * child program
	     */

	    sprintf (tmp, "%d", semid);
	    execl ("./bin/semaphore2", "semaphore2", tmp, NULL);
	    printf ("(USER1) GRRRRR... Error in execl\n");
	    semctl (semid, 0, IPC_RMID);
	    exit (2);
	    break;
	  }
	  case -1:
	  {
	    printf ("(USER1) GRRRRR... Couldn't fork!\n");
	    semctl (semid, 0, IPC_RMID);
	    exit (3);
	    break;
	  }
	}

	/*
	 * only parent runs this. loop 10 times via semaphore control
	 * to place data into shared file between apps.
	 */

	for (x = 0; x < 10; x++) 
	{
	  /*
	   * do a decrement on our only semaphore
	   */

	  if (semop (semid, &acquire_operation, 1) == -1) 
	  {
	    printf ("(USER1) GRRRRR.... Can't start critical region\n");
	    exit (4);
	  }

	  printf ("(USER1) We're in our critical region\n");
	  if ((fp = fopen ("shared.txt", "w")) == NULL) 
	  {
	    printf ("(USER1) GRRRRR... Can't write to shared file\n");
	    semctl (semid, 0, IPC_RMID);
	    exit (5);
	  }

	  fprintf (fp, "%s", info[x]);
	  fclose (fp);

	  printf ("(USER1) Sleeping for 2 seconds to simulate really important work being done by USER1 in the critical region.\n");
	  sleep (2);

	  printf ("(USER1) Done critical region\n");

	  printf ("(USER1) tell USER2 we're done by incrementing semaphore ...\n");

	  /*
	   * do an increment on our only semaphore
	   */

	  if (semop (semid, &release_operation, 1) == -1) 
	  {
	    printf ("(USER1) GRRRRR.... Can't exit critical region\n");
	    semctl (semid, 0, IPC_RMID);
	    exit (6);
	  }
	}

	remove("shared.txt"); // remove shared file as it's no longer needed

	printf ("(USER1) We're done. wait 5 sec for USER2 to finish with the shared fle.\n");
	sleep (5);
	
	printf ("(USER1) Release the semaphores\n");
	semctl (semid, 0, IPC_RMID, 0);

	return 0;
}




