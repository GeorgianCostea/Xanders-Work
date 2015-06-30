/*
filename: ValidateSin.c
by: Georgian Costea
description: get a valid SIN number
                                          */



#include<stdio.h>



int getNum(void);
int getSinDigit(int ,int);

int main(void)

{
		int counter = 0;
		int number = 0;
		int witchDigit = 0;
		int totalNum = 0;
		int checkDigit = 0;
		int digitNum[9]={0};
		int keypress = 1;
	
		
		
		printf("input a valid Sin number\n");
	//get a input
		while(keypress != 0 )
		{
			totalNum = 0;
			number = getNum();
			if(number == 0)
			{
				keypress = 0;
			}
			else
			{
			
			for(counter=0; counter < 9 ;counter++)
			{
			digitNum[counter]=getSinDigit(number,counter + 1);
			
			}

		
		for(counter=1; counter< 9; counter+=2)
		{
			digitNum[counter]*=2;
				//printf("%d\t",digitNum[counter]);

				if(digitNum[counter] > 9)
				{
					digitNum[counter]= digitNum[counter] % 10 + 1;
					printf("%d\n",digitNum[counter]);
					
				}
				
		}
		for(counter = 0; counter < 8 ;counter++)
		{
			totalNum +=digitNum[counter];
			//printf("%d\n",totalNum);
		}
		 checkDigit =10 - (totalNum %10);
		 printf("%d",checkDigit);

		if(checkDigit == digitNum[8])
		{
			printf("this SIn # is OK\n");
		}
		else
		{
			printf("Its not OK\n");
		}
			}
		}
		return 0;

}




 int getSinDigit(int bigNumber, int whichDigit)
 {
	 whichDigit=9 -whichDigit;
	 while(whichDigit > 0)
	 {
		 bigNumber /=10;
			 whichDigit--;
	 }
	 return bigNumber % 10;




 }

 int getNum(void)
{

char record[121] = {0};                  
int number = 0;
    
    fgets(record,  121, stdin);
   
    if(  sscanf_s(record, "%d", &number) != 1 )
    {
        number  = -1;
    }
    return  number;
}