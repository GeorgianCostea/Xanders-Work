/************************************************************

* Programmer: Georgian Costea								*
* Date: 05/05/2013										*
* Description: This source file has the ability to look for *
* files in any of the subdirectories from the current       *
* directory and also compare if the names of  directories   *
* the same with names that we find in the nick linked list. *
* Also a side note it creates a _allFiles folder where the  *
* files that are in the subdirectories are copied into      *
* with a nick name in front .								*
*************************************************************/
#include"prototypes.h"




/*******************************************************************
*
* Description:looks for files in any of the subdirectories from the   *
* current directory and also compare if the names of  directories  *
* the same with names that we find in the nick linked list, it     *
* creates a _allFiles folder where the files that are in the       *
* subdirectories are copied into  with a nick name in front        *														   *
********************************************************************/
int FindFiles(DIR_NAMES* head,NICK_NAMES*newHead)
{
	WIN32_FIND_DATA     filedata = {0}; 
	HANDLE              h = NULL;
	
	DIR_NAMES* ptr = NULL;
	DIR_NAMES* prev = NULL;
	NICK_NAMES* newPtr =NULL;
	NICK_NAMES* cmpNames = NULL;

	int retVal= 0;
	int retVal1= 0;
	int foundIt = 0;

	char buffer[kBufferLengh] = " ";
	char *fileName =NULL;
	char *path = "..\\.\\_allFiles\\";
	const char *filePath = ".\\*";
	
	ptr = head;
	prev= head;
	newPtr=newHead;
	



	while(ptr !=NULL)
	{
		/*changing the current directory to the name
		that was stored in the linked list(directory)
		and also checks for return value if it changed the
		directory or not.If the directory was not changed 
		an error mesage will pop up.*/

		retVal = chdir(ptr->dNames);

		if(retVal != 0)
		{
			printf("Error Changing the directory!!!\n");
			break;
		}
			/*ussing the findfirstfile and findnextfile to find all the
			files in the subdirectories also the path its a const char called
			filepath */
			h=FindFirstFile(filePath, &filedata);
	
			if( h != INVALID_HANDLE_VALUE )   
			{
				/* assigning newhead to cmpNames and the loop below will
				check if the name of the subdirectory is in the Real names
				linked list and it will do that by looping every time thru the list
				and if it found it then it will break and the variable foundIt will equal to 1*/
				cmpNames=newHead;

				foundIt= 0;

				while(cmpNames !=NULL)
				{			
					if(strcmp(ptr->realNames,cmpNames->kNames)==0)
					{
						foundIt++;
						break;
					}//pointing at the next 
					cmpNames = cmpNames->next;
				}
				/*if the name was found in the list then the code below will
				be executed else then it will print an error*/

				if(foundIt == 1)
				{
					do
					{	
	   					if((filedata.dwFileAttributes
		   				&FILE_ATTRIBUTE_DIRECTORY) == 0)
						{ /* after a file was found the we will copy to a char buffer
						  the path ,filename the nickname and the "-" to the buffer and
						  that will be the new name of the file*/
						
							strcpy(buffer,path);

							fileName = filedata.cFileName;

							strcat(buffer,newPtr->nickNames);

							strcat(buffer,"-");
								
							strcat(buffer,fileName);
							/* making a directory _allFiles and a new path had to be
							specified and it will print a mesage if it was a success*/
							if( _mkdir( "..\\.\\_allFiles" ) == 0 )
							{
								printf( "Directory '_allFiles' was successfully created\n" );
      
							}	/*copy the current file at the new location that we specified in the
								char buffer also we check the return value */
								if((CopyFile(filedata.cFileName,buffer,0))==0)
								{
									printf("Error has occured using CopyFile\n");
								}
								/*we need to clear the buffer before we loop to look and copy another file*/
								ZeroMemory(buffer, kBufferLengh);
						}
						
					} while( FindNextFile(h, &filedata) );
				}
				else 
				{		//printing the error if the directory name was not found in the list
					printf("\n\nError '[%s]' directory name was not found in the list\n",ptr->dNames);
				}//newptr points to whatever is in next
				newPtr=newPtr->next;
				//check if the "h" closed properly 
				if(FindClose(h)==0)
				{
					printf("Error Closing the File!!!");
				}
			}
			ptr=ptr->next;
			/*a side note the directory was changed back to the parent directory 
			which was the current directory so we can access the rest of the subdirectories
			and  again we check for the return value of chdir*/
			retVal1 = chdir("..");

			if(retVal1 !=0)
			{
				printf("Error Changing the directory!!!\n");
			}
	}
		return 0;
}