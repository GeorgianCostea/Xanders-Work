/*********************************************
* Filename:"ConvertASCII.c"                           *
* Programer:Georgian Costea                  *
* Date:Dec/22/2011                           *
* Discription: This program will take in main*
* a file as a parameter and it will output   *
* into "context.txt" what ever is in the file*
* as integers values  3  characters in size  *
**********************************************/



#include <stdio.h>
#include <stdlib.h>
#include "ConvertASC_proto.h"
#include "constants.h"
#include <windows.h>



//it desables the warning of  fopen(4996)
#pragma warning(disable:4996)



int main(int argc, char *argv[])
{
	//file pointers
FILE *pFile1 = NULL;
FILE *pFile2 = NULL;
//variables
unsigned char * buffer = NULL;
int howManyRead = 0;
int counter = 0;
long lSize = 0;

					//take in argmuments not more then 2
					// arv[0] and arv[1]
	if( argc != 2 )
	{				
		printf("ERROR No Input File has been found\n");
		return 0;
	}
	else
	{				//opens the file received from argc onto argv
		pFile1 = fopen(argv[1], "rb");
					//if  the file does not exists
		if( pFile1 == NULL )
		{			//printf error
			printf("Can't open  file\n");
		}				// if the size of the file is less then kFalse quit the program
						//file has to be smaller then 2,147,483,647 bytes 
		else if((lSize = getSmallFileLength(argv[1])) <= kFalse)
		{				//quiting the program
			return 0;
		}
		else
		{
						// assign a block of memory for the file
			buffer = (char*) malloc((sizeof(char)*lSize)+1);
					
			pFile2 = fopen("context.txt", "w");
			
			if( pFile2 == NULL )
			{			//error print
				printf("Can't open output file\n");
						
				if( fclose(pFile1) != kFalse )
				{		
					printf("Error closing input file\n");
				}
			}
			else
			{			
				howManyRead = fread(buffer, sizeof (char), lSize, pFile1);
						
				if(howManyRead != lSize)
				{		
					printf("cant read the file");
				}
						
					for(counter=1; counter < lSize ; counter++)
					{
						
						fprintf(pFile2,"%.3u%c",buffer[counter],space);
					
						if((counter % 10) == kFalse )
						{		
							fprintf(pFile2,"%c%c",carriageReturn,newLine);
						}	
						if(!feof(pFile2))
						{	//put a new line
							fprintf(pFile2,"%c",newLine);
						}
							
					}					
							
			}				
						if( fclose (pFile1) != kFalse )
						{	
							printf("Error closing input file\n");
						}//ckeck if the file closed properly
						if( fclose (pFile2) != kFalse )
						{
							printf("Error closing output file\n");
						}
						///free the memory 
						free(buffer);
							
							
		}
	}
	

	return 0;
}