/*********************************************
* Filename:"CalcFlyingTime.c"                           *
* Programer:Georgian Costea                  *
* Date:Nov/09/2011                           *
* Discription: This program lets the user to *
* make a choice from a list where they want  *
* to travel  and display how long it will    *
* take  to fly between any two cities or     *
* more then two.                             *
**********************************************/
#include<stdio.h>





//preporcessing directives  or constants
#define kArrLenght  6
#define kCharLenght 81
#define kFalse		0





//prototypes
int mainMenuDisplay(void);
int inputRangeCheck(int min, int max, int value);
int sumOfArrays(int arr[],int arr2[],int departingNum , int landingNum);
int errorDisplayMessage(int departing , int landing );
int getNum(void);





int main(void)
{	
	    //arrays where we store the times for traveling and
		// it is stored  in minutes.
	int flyingTimeArr[kArrLenght]={255,238,235,134,207};
	int layoverTimeArr[kArrLenght]={80,46,689,53};
	
	    //variables 
	int keyPress = 0;
	int departingNum = 0;
	int landingNum = 0;
	int oldLandingNum = 0;
	int oldDepartingNum = 0;
	
		//prints this for the user
		printf("\t     **********************************************\n"
			"\t   ***   Welcome to the Calculation flying time   ***\n"
			"\t********************************************************\n\n\n"
			" Press one of the options below ( #1 to #3 )\n\n");

		//infinite loop
			while(1)
			{

				//call the main function and display the menu.
				mainMenuDisplay();
			
				//get input from the user.
				keyPress= getNum();

				//check if the input is the right one
				inputRangeCheck(1,3,keyPress);

					//if statement if keypress is 1 do this
					if(keyPress == 1)
					{	//print the menu
						printf(" 1.Chose the City you are leaving from\n"
								"(#0) To Exit\n"
								"(#1) Toronto\n"
								"(#2) Atlanta\n"
								"(#3) Austin\n"
								"(#4) Denver\n"
								"(#5) Chicago\n"
								"(#6) Buffalo\n");  
					
					
						//getting the input from the user
						departingNum = getNum();

							//if departingNum is 0 then exit program
							if(departingNum == 0)
							{	
								//break out of the loop
								break;

							}	//checks for the input if is in the range
							else if(inputRangeCheck(1,6,departingNum))
							{	
								//if its in range it prints this to the user
								printf("Now press 2 to chose your destination city\n\n");
							}
							else	
							{	//if the departingNum is not in range
								//reset the value of departingNum back to 0
								departingNum = 0;
							}

					}//if keypress is 2 then do this.		
					else if(keyPress == 2)
					{	
						//printthe menu on the display for the user
						printf("2.Chose your landing City\n"
							"(#1) Toronto\n"
							"(#2) Atlanta\n"
							"(#3) Austin\n"
							"(#4) Denver\n"
							"(#5) Chicago\n"
							"(#6) Buffalo\n");  

					
						//getting the input
						landingNum = getNum();

							//if the landing and departing are equal do this.
							if(landingNum == departingNum)
							{            //error message
								printf(" Sorry your home city and landing city can`t be the same.\n"
									"Please try other options\n\n\n" );

							}//checking if the input is in the range
							else if(inputRangeCheck(1,6,landingNum))
							{
								//calling the function and taking the parameters.
								errorDisplayMessage( landingNum ,departingNum );
							}
							else
							{	//if the landing num is not in range it will set the
								//value back to 0
								landingNum = 0;
							}
					

					}// else if keypress is 3 do this
					else if(keyPress == 3)
					{	 
							sumOfArrays(flyingTimeArr,layoverTimeArr,departingNum,landingNum);	
					}
			
			}
	
	//return 0
	return 0;
}





/**************************************************************
 * Function name: mainMenuDisplay(void)                       *
 * Description:when this function is called in the            *
 * main function it will print on the screen the Option Menu  *
 * Parameters: None                                           * 
 * Return value: returns 0;                                   *
 **************************************************************/
int mainMenuDisplay(void)
{	
	//prints on the screen the  main menu
	printf(" 1.Chose your Departing city\n"
		" 2.Chose your Destination city\n"
		" 3.Calculate flying time and layover time\n\n"
		" To EXIT press option 1 and chose 0\n\n");

	//returns 0
	return 0;
}





/***********************************************************
 * Function Name:inputRangeCheck(int, int, int)            *
 * This function  it checks if the range is good.it takes  *
 * 3 perameters  min max and value and it checks if the    *
 * input is between range or not...if its in range it      *
 * returns 1 ,if its not in range it returns 0 and a       *
 * error message.                                          *
 **********************************************************/
int inputRangeCheck(int min, int max,int value)
{	
	//making a statement if the value is in parameters.
	if(( value < min)||(value >max))
	{		//printing the error
		printf("      You entered an unvalid value    \n"
			"  Your value should be between %d and %d  \n\n\n", min,max);

		//returns 0 if its out of range
		return 0;
	}
	else
	{	//returns 1 if is in range
		return 1;
	}
	

}





/*************************************************************
 * Function Name:sumOfArrays(int arr[], int arr2[], int,int) *
 * This function  is to calculate the elements of 2 arrays   *
 * and to display that in HH:MM format.                      *
 * Parameters: int arr[],int arr2[],                         *
 *			   int departingNum,int landingNum               *
 * returns : it returs 0;                                    *
 *************************************************************/
int  sumOfArrays( int arr[],int arr2[],int departingNum , int landingNum)
{
	//variables
	int counter1 = 0;
	int counter2 = 0;
	int sum1 = 0;
	int sum2 = 0;
	int totalSum=0;
	int totalMinutes = 0;
	int totalHours = 0;
	int newDepartingNum = 0;
	int newLandingNum = 0;

		//the purpose of this variable is to let us 
		//start the loop from element 0 for the array
		newDepartingNum = departingNum - 1;

		//The purpose of this variable is to let us 
		// end the loop and it points at the previous element in the array 
		newLandingNum = landingNum - 1;

		// if any of the statements are 0 then print error
		if((departingNum== kFalse) || (landingNum==kFalse))
		{	//error
			printf("Please go back and check your inputs :(\n\n\n\n");
		}
		else
		{
			//loop 1
			for( counter1 = newDepartingNum ; counter1 < newLandingNum ; counter1++ )
			{	//sum 1 = with sum + elements of the array until the counter is smaller
				//then the landingNum
				sum1 += arr[counter1];
			}	//loop 2  newLandingNUm -1 means that the counter will stop and add all the numbers together
				// until it reaches the input from the user.

				for( counter2 = newDepartingNum ; counter2 < (newLandingNum - 1) ; counter2++ )
				{	//sum 2 = with sum+ elements of the array 2 until the counter is 
					//smaller the newLandingNum -1
					sum2 += arr2[counter2];  
				}

			//totalsum = with the sum 1 form the first loop added with 
			//   the sum of loop 2
			totalSum = sum1 + sum2;

			// devide the total sum by 60 to get the numbers of hours
			totalHours = totalSum / 60;

			// do module 60 to get the minuts
			totalMinutes = totalSum % 60;
				
			//print the time
			printf("\tThank you for using the calculation flying time \n"
					"\tTotal Flight Time and Layover is %d hours and %d minutes \n  "
					" \t TO EXIT press option 1 and chose 0\n\n\n" , totalHours , totalMinutes );
	     
		}
	//return 0 
	 return 0;
}





/************************************************************
 * Function Name:errorDisplayMessage(int, int)              *
 * Description: takes 2 parameters with are the inputs from *
 * the user and compare and if landing< departing the print *
 * error message else gets through.                         *
 * parameters: int landing, int departing                   *
 * return value: returns 0 if the statement is true and it  *
 * returns 1 if its false.                                  *
 ************************************************************/
int errorDisplayMessage(int landing, int departing )
{	
	// if landing its smaller then departing the print error
	if( landing < departing )
	{	//error
		printf("Sorry you can not travel backwards!!!!\n"
		"       Please try other options      \n\n\n");	

		//returns 0 if its false
		return 0;
	}
	else
	{	
		// prints to the user to chose option 3 to calculate flying time
		printf("Now press 3 to calculate your flying time\n\n");

		//returs 1 if its true
		return 1;
	}
}
	




/* Function Name: int getNum()                      *
 *  Description: it take an input from the user     *
 * and it saves it in a variable                    *
 * parameters: no parameters                        *
 * return value it returs number                    *               
 ***************************************************/
int getNum(void)
   {

	/* the array is 81 bytes in size; we'll see in a later lecture how we can improve this code */
   char record[kCharLenght] = {0};                      /*  record stores the string */
   int number = 0;

   	/* NOTE to  student: indent and brace this function consistent with  your others */
		/* use  fgets() to get a string from the keyboard */
		fgets(record,  kCharLenght, stdin);
		

       /* extract  the number from the string; sscanf() returns a number 
   	 * corresponding with the number of items it found in the string */
   	if(  sscanf_s(record, "%d", &number) != 1 )
   	{
   		/*  if the user did not enter a number recognizable by 
		     * the system, set number to -1 */
			number  = -1;
   	}

       return  number;
   }
