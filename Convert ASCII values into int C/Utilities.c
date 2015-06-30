/**********************************************
* Filename:"Utilities.c"                      
* Programer:Georgian Costea                   
* Date:Dec/22/2011                            
**********************************************/



#include<stdio.h>
#include<windows.h>
#include<stdlib.h>
#include"ConvertASCII_proto.h"
#include"constants.h"



/********************************************
* Function Name: getSmallFileLength         
* Description : it takes in a char pointer through FindFirstFile and returns the size of the file.                       
* Parameter: char *pFile                    
* return value : sizeOfFile                 
*********************************************/
int getSmallFileLength( char * pFile)
{									
	WIN32_FIND_DATA findFileData ={0};
									
	HANDLE hFile = 0;
	int sizeOfFile = 0;
								/
	hFile = FindFirstFile(pFile,&findFileData);
															//if h file is 0 then error
		if(hFile == kFalse)
		{													/
			printf("ERROR there is a problem with FindFirstFile ");
			return -1;
		}										
		else if((FindClose(hFile)) == kFalse)
		{
			printf("Error closing FindFirstFile");
		}
												//file has to be smaller then 2,147,483,647 bytes 
		sizeOfFile = findFileData.nFileSizeLow;
												
		return sizeOfFile ;
		
}