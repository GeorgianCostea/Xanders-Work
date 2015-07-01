/*
* Programmer: Georgian Costea
* Date: 15/03/2013
* Description: Creating a virtual display where we display 
* string of characters and depending of the position the
* screen will scroll.
*/


#include<stdio.h>
#include<string.h>

#define MAX_COLS 40
#define MAX_ROWS 24


class VideoSim
{

	/* ====================================== */
	/*              PRIVATE                   */
	/* ====================================== */
private:


	/* -------------- ATTRIBUTES ------------ */
	char  video[MAX_ROWS][MAX_COLS];
	int currentRow;
	int currentColumn;


	/* ====================================== */
	/*              PUBLIC                    */
	/* ====================================== */

public:

	
	/* -------------- METHODS --------------- */
	void ClearScreen(void);
	void scrollScreen(void);
	void DisplayVideoMemory(void);
	void SetCursorPosition(int a ,int b);
	void OutputString(char * string );
	

		

};




/**
* Description:  this function is a mutator it sets the 2 private
* variables to a specific value only if the value is in range.
*/
void VideoSim::SetCursorPosition(int a, int b)
{
	
	if((( a > 0)&&(a < MAX_COLS))&&
		(b > 0)&&(b < MAX_ROWS))
	{
		currentRow = b;
		currentColumn = a;
	}
	else
	{
		printf("cannot support that virtual screen size : 40X24 \n");
	}
} 




/**
* Description:  once the input string is to long to fit into the screen
* it clears the first row and moves everything up by 1 until the string fits
* on the virtual screen.Also there is another function that clears the last row.
*/
void VideoSim::scrollScreen(void)
{
	char *pVideo = &video[0][0];
	int vidOffset = 0;
	int counter= 0;
	
		//copies row by row back into the 2 dimensional array but it starts at index 1
		for( counter = MAX_COLS; counter < (MAX_ROWS * MAX_COLS); counter++ )
		{
			pVideo[vidOffset] = pVideo[counter];
			vidOffset++;
		
			if(vidOffset >=(MAX_COLS * MAX_ROWS))
			{// if offset is grater then it will jump to the next row
				vidOffset =(MAX_ROWS -1) * MAX_COLS;
			}
		
		}
	
	pVideo = &video[MAX_ROWS-1][0];
	//it clears the last row 
	for( counter = 0; counter < MAX_COLS; counter++ )
	{
		pVideo[counter] =' ';
	}
}




/**
* Description: the following function takes the string from
* main and looks thru each character and stores the character
* in the 2 dimensional array.
*/
void VideoSim::OutputString(char *string)
{
	
	int counter =0;
	int vidOffset = (currentRow* MAX_COLS )+ currentColumn;
	char * pVideo = &video[0][0];

	while(string[counter] != '\0')
	{
		pVideo[vidOffset] = string[counter];
		vidOffset++;
		// if offset is grater then it will jump to the next row
		
		if(vidOffset >=(MAX_COLS * MAX_ROWS))
		{

			vidOffset =(MAX_ROWS -1) * MAX_COLS;

			VideoSim::scrollScreen();

		}
		counter++;
	}
}



/*
* Description: loop thru the 2 dimensional array and
* replace everything with a space.
*/
void VideoSim::ClearScreen(void)
{
	currentRow = 0;
	currentColumn = 0;
	
	int i = 0;
	char *pVideo = &video[0][0];

		/* clears the screen  replacing it with space*/
	for( i = 0; i < (MAX_ROWS * MAX_COLS); i++ )
	{
			*(pVideo++) = ' ';
			
	}

}





/*
* Description: his function will allow you to
* output to the Windows console the current 
* contents of your video memory to prove your 
* APIs are working correctly.
*/
void VideoSim::DisplayVideoMemory(void)
{
	int i = 0, j = 0;

	printf ("Video memory holds:\n");

	printf ("   ");

	for (i = 0; i < MAX_COLS; i++)
	{	// constant that needs definition

		if ((i % 10) == 0)
		{
			printf ("%d", i / 10);
		}
		else
		{
			printf (" ");
		}
	}	/* end for */
	printf ("\n");

	printf ("   ");
	for (i = 0; i < MAX_COLS; i++) 
	{
		printf ("%d", i % 10);
	}	/* end for */
	printf ("\n");

	for (i = 0; i < MAX_ROWS; i++) 
	{	// constant that needs definition
		printf ("%02d ", i);

		for (j = 0; j < MAX_COLS; j++)
		{
			printf ("%c", video[i][j]);	// private data member
		}
		printf ("\n");
	}
	printf ("\n\n");
}







int main(void)
{
	VideoSim v;


	v.ClearScreen();
	v.OutputString("top left corner");
	v.DisplayVideoMemory();
	v.SetCursorPosition(10 ,5);
	v.OutputString("here`s some text in the middle of the screen");
	v.DisplayVideoMemory();
	v.SetCursorPosition(35 , 20);
	v.OutputString ( "here`s text that will scroll the screen!");
	v.DisplayVideoMemory();
	v.SetCursorPosition(35 , 23);
	v.OutputString ( "here`s that is going to scroll over the screen!");
	v.DisplayVideoMemory();
	
	return 0;

}