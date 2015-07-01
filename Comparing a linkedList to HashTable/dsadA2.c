/************************************************************
* FileName: "dsadA2.c"								        *
* Project: "dsadA2"											*
* Programmer: Georgian Costea								*
* Date: 07/03/2012											*
* Description: Read in the file and pass the input to the   *
* function also we prompt the user for input and we check   *
* how long did the link list and hash table took to execute *
*************************************************************/
#include"prototypes.h"




int main(int argc, char* argv[])
{	
	//this function will search for the directories
	//open the file called nicknames.txt
	const char *where = ".\\*";
	char  input[100] = " ";
	char * pQuit = "QUIT";
	char buffer[kBufferLengh] = {0};
	

	int searchCount = 0;
	int hashCount = 0;
	int linkListCount = 0;

	LARGE_INTEGER startTime = {0};
	LARGE_INTEGER endTime = {0};
	LARGE_INTEGER frequency = {0};

    double hashTotalTime = 0;
    double linkedTotalTime = 0;

	
	FILE* pFile = NULL;
	WIN32_FIND_DATA     filedata = {0}; 
	HANDLE				h = FindFirstFile(where, &filedata);
	
	NICK_NAMES* newHead = NULL;
	LLIST *hashtable[MaxHashEntries] = {NULL};

	
	//open the file and check if its null 
	pFile = fopen("nicknames.txt","r");

	if(pFile == NULL)
	{
		printf("Error!!! Can`t open file");
		return 0;
	}

		do
		{
			// prompt user for a first name
		
			fgets(buffer,kBufferLengh, pFile);

			// if it's empty, quit inputting
		
			
			// get rid of the '\n' that fgets() put in if it's there
			eliminateEndOfLine(buffer);

			/*pass in the "buffer" that contains the nickname and real name*/
			newHead = insert(buffer,newHead);
			addHashItem(hashtable,buffer);
			

		} while(!feof(pFile));
		
		/* prompt the user for input and the look quits when "QUIT" is typed also
		we call the QueryPerformanceCounter and we pass the frequency startTime and endTime
		to measure how long did each search took[linklist and hash table]*/


	printf("Please enter the name you want to search\n\n");
	while(1)
	{
		
		fgets(input,sizeof(input),stdin);
		eliminateEndOfLine(input);

		if(strcmp(input,pQuit)== 0)
		{
			break;

		}
		else
		{
			
			searchCount++;
			QueryPerformanceFrequency(&frequency);
			QueryPerformanceCounter(&startTime);

			showSearchResultsForLinkList(newHead,input,&linkListCount);

			QueryPerformanceCounter(&endTime);
			linkedTotalTime += (double)(endTime.QuadPart - startTime.QuadPart) / frequency.QuadPart;

			
			QueryPerformanceCounter(&startTime);

			findHashItem(hashtable,input,&hashCount);

			QueryPerformanceCounter(&endTime);
			hashTotalTime += (double)(endTime.QuadPart - startTime.QuadPart) / frequency.QuadPart;
			
		}

	}

	printf("=============Results=============\n\n");
	printf("The Total Number of Searches: %d\n",searchCount); 
	printf("the total number of comparisons done by linked list : %i\n",linkListCount);
	printf("the total number of comparisons done by hash Table : %i\n\n",hashCount); 
	printf("=============Timing Results=============\n\n");
	printf("the total time took by link list was : %0.2f miliseconds\n",linkedTotalTime * 1000);
	printf("the total time took by hash Table was : %0.2f miliseconds\n",hashTotalTime * 1000);
	
	//dealocate all the memory
	delete_info(newHead);
	emptyHashTable(hashtable);

	return 0;
}


