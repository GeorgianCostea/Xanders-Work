/********************************************
* Filename:"xToThePower.c"                          
* Programer:Georgian Costea                                   
* Discription:  Uses a function to calculate
* the result of raising one number to the   
* power of another.                         
********************************************/
#include<stdio.h>

  //prototypes
int headingFunction(void);              
int powerFunction(int oldBase,int oldExponent);
int rangeCheckingFunction(int min,int max,int value);
int getNum(void);




int main(void)
{
	 //declaring variables
	int keyNumber = 0;   
	int baseNum = 1;
	int exponentNum =1;
	int oldBase = 1;
	int oldExponent= 1;
	
	//infinite loop until the user press key 4
	
	while(keyNumber !=4)  
		{
			//cals the headingFunction
		headingFunction();
		// making keynumber= getnum()
		keyNumber=getNum();          
		
	
		//if statement if the key pressed is 1 do this
			
		if( keyNumber == 1 )                       
			{	//ask for an base
				printf("Change Base <Between 1 and 25>\t");
				//get the number
				baseNum=getNum();
			// check if the values are in the range
				if(rangeCheckingFunction(1,25,baseNum))
					{	
						//if the input its out of range then the old value will take
						//the place of the new value
						oldBase = baseNum;
					}
			
				
			}
					//else if keynumber is 2 do this			
		else if( keyNumber == 2 )                  
			{			//ask for an exponenet
				printf("Change Exponent < Between 1 and 5>\t");  
					exponentNum = getNum();                
				//check if the values are in the range
				if(rangeCheckingFunction(1,5,exponentNum))
					{	
						//if the input its out of range then the old value will take
						//the place of the new value
						oldExponent= exponentNum;
					}
			}
		  //when user will press key 3 they are calling the power funtion
			else if( keyNumber == 3 )                          
			{
				
				powerFunction( oldBase , oldExponent );         
			}
	
	}	
			//returns 0
		return 0;
}


int headingFunction(void)
{
	//Headingfunction it shows the
	// menu bar(Power Menu)
	printf("\n Power Menu\n\n"                           
			"1. Change Base\n"
			"2. Change Exponent\n"
			"3. Display base raised to the exponent\n"
			"4. Exit program\n\n"
			"Option?\n\n");
	
	return 0;
}


int powerFunction(int oldBase, int oldExponent)          
{	
	  //declaring variables
	int counter = 0, answer = 1;                          
	
	
	 //loop
	for(counter = 0; counter < oldExponent; counter++)  
	{
		// the answer is answer times baseNum
		answer *=oldBase;                                     
		
	}
	 //print the base raised to exponent and the answer
		printf("Display base raised to the exponent:%d ^ %d = %d\n\n",oldBase,oldExponent,answer);   

		return 0;
}


int rangeCheckingFunction(int min ,int max,int value)              
{ 
		//making a statement if the value is in parameters.
	if((value < min) || (value > max))                             
	{		
			//display the error
		 printf("Your value is not between %d and %d\n",min,max);        
		 //return 0 if its out of range
		 return 0; 
	}																	
	else
	{		//return 1 if its in the range 
		return 1;
	}
	

}  


int getNum(void)
   {

	/* the array is 121 bytes in size; we'll see in a later lecture how we can improve this code */
   char record[121] = {0};                      /*  record stores the string */
   int number = 0;

		fgets(record,  121, stdin);

   	if(  sscanf_s(record, "%d", &number) != 1 )
   	{
   		/*  if the user did not enter a number recognizable by 
		     * the system, set number to -1 */
			number  = -1;
   	}

       return  number;
   }