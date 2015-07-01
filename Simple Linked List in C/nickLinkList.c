/************************************************************
* Programmer: Georgian Costea								*
* Date: 05/05/2013									
* Description: This source file has the ability to create a *
* sorted linked list and display the content of that list on*
* the screen also there is a small function that takes out  *
* the newline from the end of a line .						*
*************************************************************/
#include"prototypes.h"





/*********************************************************************
 * This function will allocate a new entry in the                    *
 * linked list, allow data entry, and append this                    *
 * entry to the end of the chain.                                    *
 *                                                                   *
 *********************************************************************/
NICK_NAMES *insert(char*insertString ,NICK_NAMES *newHead)
{
	NICK_NAMES *newBlock = NULL;
	NICK_NAMES *ptr = NULL;
	NICK_NAMES *prev = NULL;

	
	char buffer[kBufferLengh] = " ";
	char nickNames[kBufferLengh] = " ";
	char * pRealNames = NULL;

	// allocate a block of memory for new record
	newBlock = (NICK_NAMES *)malloc (sizeof (NICK_NAMES));
	if (newBlock == NULL) 
	{
		printf ("Error! No more memory!\n");
		return newHead;
	}	/* endif */

	/* copy the content of insertString into the buffer and
	then we scan for a string into insertstring because
	we need to get the nick name and sscanf does not look 
	beyond a space and we are checking sscanf.*/
	strcpy(buffer,insertString);

	if((sscanf(insertString,"%s",nickNames))!=kTrue)
	{
		printf("Error using sscanf");
	}
	/*the following block of code it searches for a space in 
	our case the first space and the break*/
	pRealNames=strchr(buffer,' ');

		while (1)
		{
			pRealNames = strchr(pRealNames,' ');
			break;
		}
		/* copy the content that is into pRealNames+kTrue im adding 1
		because i wanted to get rid of the space from the bigging of the
		string*/

		strcpy(newBlock->kNames,pRealNames+kTrue);
		/*copy what ever we got from sscanf into newblock->nickNames*/
		strcpy(newBlock->nickNames,nickNames);
	
		newBlock->next = NULL;
	

		// now link this record to the list in the appropriate place
		if (newHead == NULL) 
		{
			// empty list, so set head
			newHead = newBlock;
		} 
		else if(strcmp(newHead->kNames, newBlock->kNames) >= 0) // special case!
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
				if (strcmp(ptr->kNames, newBlock->kNames) >= 0)
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
		//printf("Test 3: %s\n",newHead->kNames);
		return newHead;
		

}	




/*****************************************************************
 * This function will print all information in the list          *
 * to the screen.                                                *
 *                                                               *
 *****************************************************************/
void showList(NICK_NAMES *head)
{
	NICK_NAMES *item = NULL;

	item = head;

	printf("List with NickNames & RealNames:\n\n");
	/* the purpose of this loop is to print on the screen
	data that is stored into the linked list and is stopes
	when *item is null meaning when item->next gets to the 
	end of the list.*/
	while( item != NULL )
	{
		printf("%s",item->nickNames);
		printf("\t\t%s\n", item->kNames);
		
		item = item->next;
	}
}





/************************************************
* Description: takes in as a parameter a char * *
* and it checks for a newline and eliminates it *
*************************************************/
void eliminateEndOfLine(char *buffer)
{
	/*checks the buffer for a \n and
	if the char pointer target is not NULL
	it will make it NULL*/
char *target = strchr(buffer, '\n');
	if( target != NULL )
	{
		*target = '\0';
	}
}