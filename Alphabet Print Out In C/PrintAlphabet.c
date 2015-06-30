/********************************************
* Filename:"PrintAlphabet.c"                          *                           *
* Programer:Georgian Costea                 *
* Date:Sept/20/2011                         *
* Discription: Prints out the alphabet in   *
* one column and the running total of all   *
* letters in another column.                *
********************************************/


#include<stdio.h>     //allows for input/output

int main(void)
{
	char letter='a';       //declaring character "letter"
	int runningTotal = 0, counter=0;   //declaring integer "runningTotal" , "counter"

	
	for(letter='a'; letter<='z';++letter) //its a loop and its stops when its equal with 'z'
	{
		runningTotal= runningTotal+letter; //previous runningtotal + letter
		counter++;         // its equal with counter + 1
			if(letter=='g')    //we are making a statement .
			{
				printf("%c%8d\n",letter,runningTotal/counter);//this will print the integer conrespondent to letter 'g'.
			}
			else                               // else stands for if the declaration above its false then do this  (statement).
			{
				printf("%c%8c\n",letter,runningTotal/counter);//This will print the letters a to z and the runningTotal devided by counter.
			}

	}




	return 0;//it returns back to 0

}
