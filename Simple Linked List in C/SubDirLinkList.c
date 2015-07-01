/************************************************************
* Programmer: Georgian Costea								
* Date: 05/05/2013										
* Description: This source file has the ability to open a   
* file for read,find all the subdirectories in the current  
* directory also it will store the names of the             
* subDirectories into a sorted link list and print the      
* list on the screen.										
*************************************************************/
#include"prototypes.h"





/*******************************************************

* Description : using findfirstfile and findnextfile   
* the following function will search and find all      
* the subdirectories in the current directory and open 
* the file "nicknames.txt" for reading.                
********************************************************/
void doDirAndOpenFile(void) 
{
	
	const char *where = ".\\*";
	char buffer[kBufferLengh] = {0};

	WIN32_FIND_DATA     filedata = {0}; 
	HANDLE				h = FindFirstFile(where, &filedata);
	
	FILE* pFile = NULL;
	DIR_NAMES *head = NULL;
	NICK_NAMES* newHead = NULL;
	


	//open the file and check if its null 
	pFile = fopen("nicknames.txt","r");

	if(pFile == NULL)
	{
		printf("Error!!! Can`t open file");
	}

		do
		{
			// prompt user for a first name
		
			fgets(buffer,kBufferLengh, pFile);

			// if it's empty, quit inputting
		
			
			// get rid of the '\n' that fgets() put in if it's there
			eliminateEndOfLine(buffer);

			// put the first name into the list
			newHead = insert(buffer,newHead);

		} while(!feof(pFile));
		//call the fuction showList with
		//newhead as a parameter to print
		//the data on the screen
		showList(newHead);



		/* This will search in the current directory ussing FindfirstFile
		and FindNextFile for all the subdirectories  expect if its
		.,.. or _allFiles the code will skip those*/
		if( h != INVALID_HANDLE_VALUE )   
		{
			do
			{
	   			 if((filedata.dwFileAttributes&FILE_ATTRIBUTE_DIRECTORY) != 0)	
				 {		
					
					 if ((!strcmp(filedata.cFileName,"."))||(!strcmp(filedata.cFileName,".."))
						 ||!strcmp(filedata.cFileName,"_allFiles"))
					 {
						 continue;
					 }
					/*passing  the name of the file and the pointer head to the
						enter_new_info and the return value is store pointer head*/
					 head = enter_new_info(filedata.cFileName,head);
				
					
				 }

		     
			} while( FindNextFile(h, &filedata));
			//display the linked list and the head
			//is passes by reference
			show_List(head);
			//it will find all the files in the subdirectories
			// and we are passing DIR_NAMES *head and NICK_NAMES* newHead
			FindFiles(head,newHead);
			//close handle file 
			FindClose(h);
		}
}





/*********************************************************************
 * This function will allocate a new entry in the                    
 * linked list, allow data entry, and append this                    
 * entry to the end of the chain.                                                                     *
 *********************************************************************/
DIR_NAMES *enter_new_info (char* subDirName ,DIR_NAMES *newHead)
{
	DIR_NAMES *newBlock = NULL;
	DIR_NAMES*ptr = NULL;
	DIR_NAMES *prev = NULL;

	char buffer[kBufferLengh] =" ";
	int counter = 0;
	int lengh = 0;


	// allocate a block of memory for new record
	newBlock = (DIR_NAMES *)malloc (sizeof (DIR_NAMES));
	if (newBlock == NULL) 
	{
		printf ("Error! No more memory!\n");
		return newHead;
	}	/* endif */

	/* the purpose of this loop is to split the name
	from the date and im ussing isdigit to find the first
	digit and break and im putting a '\0' where the space was */
		strcpy(buffer,subDirName);

		lengh = strlen(buffer-kTrue);

		for(counter = 0; counter < lengh;counter++)
		{
			if(isdigit(buffer[counter]))
			{
				buffer[counter-kTrue] ='\0';
				break;
			
			}	
		}
		// obtain information for new record
		strcpy(newBlock->realNames,buffer);
		strcpy(newBlock->dNames, subDirName);
	
		newBlock->next = NULL;

		// now link this record to the list in the appropriate place
		if (newHead == NULL) 
		{
			// empty list, so set head
			newHead = newBlock;
		} 
		else if(strcmp(newHead->dNames, newBlock->dNames) >= 0) // special case!
		{
			// we're inserting at the front of the list
		
			// set the next pointer for the newBlock record to be the
			// location that used to be at the front of the list
			newBlock->next = newHead;
			// set first_number to point to the new start of the list
			newHead = newBlock;
		}
		else
		{
			/*
			 * non-empty list where we're not inserting at the front
			 * of the list, so use ptr to follow links until we reach the 
			 * right place, according to the sorting order
			 */

			prev = newHead;		// first item in list
			ptr = newHead->next;	// second item in list 
		
			while (ptr != NULL) 
			{
				if (strcmp(ptr->dNames, newBlock->dNames) >= 0)
				{
					// we've found a name in the list that is either equal to or greater 
					// than the one we're entering now
					break;
				}
				prev = ptr;
				ptr = ptr->next;
			}	/* end while */

			// add the new node here, between prev and ptr
			newBlock->next = ptr;
			prev->next = newBlock;

		}	/* endif */
	
		return newHead;

}	/* end enter_new_info */





/*****************************************************************                                                        *
 * This function will print all information in the list          
 * to the screen.                                                
 *                                                               
 */
void show_List(DIR_NAMES *head)
{
	DIR_NAMES*item = NULL;

	item = head;

	printf("\n\nThe SubDir Link list contains:\n");
	/* the purpose of this loop is to print on the screen
	data that is stored into the linked list and is stopes
	when *item is null meaning when item->next gets to the 
	end of the list.*/
	while( item != NULL )
	{
		printf("%s", item->dNames);

		printf("%50s\n", item->realNames);

		item = item->next;
	}
}