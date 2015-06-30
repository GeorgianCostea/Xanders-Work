/*
 * signals-R-US.c
 *
 * This is a sample signal handling program
 */

#include <stdio.h>
#include <stdlib.h>
#include <signal.h>
#include <string.h>
#include <unistd.h>


void allPowerfulSignalHandler (int signal_number)
{
  static int counter = 0;
  int x;
  char *p;
  FILE *fp;
  char buffer[128];

  switch (signal_number) 
  {
    case SIGUSR1:
    {
      // determine how many users logged in
      // by piping the output of the who command
      // and parsing its output

      x = 0;
      fp = popen ("who -q", "r");
      if (fp != NULL) 
      {
	while (fgets (buffer, 100, fp) != NULL) 
	{
	  p = strstr (buffer, "=");
	  if (p != NULL) break;
	}
	if (p != NULL)  x = atoi (p + 1);
	fclose (fp);
      }
      printf ("%d people online right now!\n", x);
      break;
    }
    case SIGINT:
    {
      // disable CTRL-C support! but only 3 times max!
      counter++;
      printf ("\nCTRL-C Pressed ... steeeeeeeeerike %d!\n", counter);
      if (counter == 3) 
      {
	printf(" ... Try that again, and you're OUTTA HERE !!\n\n");
	signal (SIGINT, SIG_DFL);
	return;
      }
      else
      {
	printf("\n");
      }
      break;
    }
  }

  // reactivate our custom signal handler for next time ...
  signal (signal_number, allPowerfulSignalHandler);
}


void alarmHandler(int signal_number)
{
  // this is where you could do period work!
  // like saving a file, calculating statistics, etc.
  printf ("\n5 seconds have elapsed!\n\n");

  alarm (5);	// reset alarm

  // reactivate signal handler for next time ...

  signal (signal_number, alarmHandler);
}

int main (void)
{
  int x, y=0;

  // original registration of SIGNAL HANDLERS when application starts
  signal (SIGINT, allPowerfulSignalHandler);
  signal (SIGUSR1, allPowerfulSignalHandler);
  signal (SIGALRM, alarmHandler);

  alarm (5);

  printf ("Before Loop\n");
  while (1) 
  {
    // simulate some "heavy duty work"
    for (x = 0; x < 10000000; x++) 
    {
      x = x + 5;
      x -= 5;
    }
    y++;
    if((y%10) == 0) printf ("    You've executed 10 of your \"busy\" loops ...\n");
  }

  printf ("Done Loop\n");

  return 0;
}
