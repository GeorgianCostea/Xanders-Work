/************************************************************
* Programmer: Georgian Costea								*
* Date: 07/03/2013			
* Description: This source file has the ability to create a *
* hash table and display if the item was found or not and   *
* also dealocate the alocated memory also we have a			*
* hashfunction that will come up with a unique offset		*
*************************************************************/
#include"prototypes.h"






/*********************************************************************
*
 * This function will allocate a new entry in the                    
 * hash table , allow data entry, and append this                    
 * entry to the end of the chain.                                    *					 
 *********************************************************************/
int addHashItem(LLIST *hashtable[], char *w)
{
	unsigned long offset = 0;
	size_t x = 0;
	int val = 0;
	LLIST *p = NULL, *q = NULL;


	char buffer[kBufferLengh] = " ";
	char nickNames[kBufferLengh] = " ";
	char * pRealNames = NULL;

	// create a linked list entry
	 
	p = (LLIST *) malloc (sizeof (LLIST));
	if( p == NULL ) 
	{
		printf ("Out of memory!\n");
		return 0;
	}	// endif


	strcpy(buffer,w);

	if((sscanf(w,"%s",nickNames))!=kTrue)
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


	strcpy(p->word, pRealNames+ kTrue);
	strcpy(p->nickName, nickNames);
	p->next = NULL;

	// generate unique offset
	offset = hashFunction(pRealNames+kTrue);
	

	// add to hash table
	if (hashtable[offset] == NULL) 
	{
		// first entry on list
		hashtable[offset] = p;
	} 
	else 
	{
		// add to existing list at this entry
		// note: this is different from the lecture
		//			so each list will be backwards
		p->next = hashtable[offset];
		hashtable[offset] = p;
	}	// endif 

	
	return 1;
}	// end addHashItem()



/*********************************************************************

 * This function will generate an unique offset for the hash table   *
 
 *********************************************************************/
unsigned long hashFunction(char *str)
{

	unsigned long hash = 5381; 
	int c = 0; 

	while ((c = *str++) != '\0') 
	{
			hash = ((hash << 5) + hash) + c; 
	}

	hash =hash % MaxHashEntries;

	return hash; 
}




/*****************************************************************

* This function will print the if the in the item was found or   *
* not to the screen.                                             *
							 *
******************************************************************/
int findHashItem(LLIST *hashtable[], char *w,int* hashCount)
{
	int count = 0, offset = 0, val = 0;
	size_t x = 0;
	LLIST *p = NULL;

	// generate hopefully unique offset to existing data
	offset = hashFunction(w);
	for( p = hashtable[offset]; p != NULL; p = p->next ) 
	{
		/* the purpose of hashCount and count is to keep track of how many time
		did the hash table was compared until the item was found or not*/
		count++;
		*hashCount+= 1;
		if( strcmp(p->word, w) == 0 )  
		{
			
			printf("%s was found in the hash table in %d comparisons\n",w, count);
			return 1;
		}	// endif
	}
	// endif 
	
		printf("%s was not found in the hash table in after %d comparisons\n",w, count);
	
	return 0;
}	// end findHashItem()




/*****************************************************************
												 *
* This function will free() all allocated memory				 *
* in the hash table.											 *
******************************************************************/
void emptyHashTable(LLIST *hashtable[])
{
int counter = 0;
LLIST *p = NULL, *q = NULL;

	for( counter = 0; counter < MaxHashEntries; counter++ ) 
	{
		for( p = hashtable[counter]; p != NULL; p = q ) 
		{
			q = p->next;
			free (p);
		}	/* end for */

		hashtable[counter] = NULL;
	}	/* end for */
}	/* end emptyHashTable() */