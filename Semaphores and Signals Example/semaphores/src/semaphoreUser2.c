/*
 * s2.c
 *
 * A simple example of semaphore control.
 * This app is launched by s1.c, and will
 * wait for access to a critical region in
 * order to read file data written by s1.
 */

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/sem.h>

#include "../inc/semaphoreStruct.h"

int
main (int argc, char *argv[])
{
	int semid;
	char data[512];
	FILE *fp;
	int x;

	printf ("     (USER2) This program will use critical regions.\n");
	printf ("     (USER2) It will mark the critical region with a semaphore\n");
	printf ("     (USER2) and only when instructed, will a common file between\n");
	printf ("     (USER2) USER1 and USER2 be accessed.\n");

	if (argc != 2) 
	{
	  printf ("     (USER2) AHHHHH... Insufficient args for this app\n");
	  exit (1);
	}

	/*
	 * since program USER1 forked and exec us into existance, it
	 * provided the semaphore ID as the 2nd argument for this
	 * task. Thus, convert the argument into an integer and
	 * use this integer as the semaphore ID
	 */
	
	semid = atoi (argv[1]);

	printf ("     (USER2) Our semaphore ID is %d\n", semid);

	printf ("     (USER2) Loop forever, until our semaphore goes away.\n");
	printf ("     (USER2) This will happen only if S1 terminates and deallocates the semaphore\n");

	while (1) 
	{
	  /*
	   * attempt to decrement the semaphore
	   */

	  if (semop (semid, &acquire_operation, 1) == -1) 
	  {
	    printf ("     (USER2) AHHHHH... Can't start critical region\n");
	    break;
	  }

	  if ((fp = fopen ("shared.txt", "r")) != NULL) 
	  {
	    fgets (data, sizeof (data), fp);
	    printf ("     (USER2) Received from USER1 ... <%s>\n", data);
	    fclose (fp);
	  }

	  /*
	   * attempt to increment the semaphore
	   */

	  if (semop (semid, &release_operation, 1) == -1) 
	  {
	    printf ("     (USER2) AHHHHH... Can't end critical region\n");
	    break;
	  }
	}

	printf ("     (USER2) We're done!\n");
	
	return 0;
}




