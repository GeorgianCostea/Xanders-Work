/************************************************************
* Programmer: Georgian Costea								*
* Date: 07/03/2013												*
* Description: This source file is used to store all the    *
* number define prototypes,structs, include<>,and warnings  *
* desables                                                  *
*************************************************************/
#include <stdio.h>
#include<Windows.h>
#include<direct.h>
#include<string.h>



//desable the warning 4996 because its 
//a microsoft warning related 
#pragma warning( disable : 4996 )
#pragma warning( disable : 4133 )



/*where the data about the subDirectories
and nicknames are stored (structs)*/

typedef struct nickNames
{
	char kNames[50];
	char nickNames[50];
	struct nickNames* next;
} NICK_NAMES;




typedef struct tagLLIST 
{
	char word[50];
	char nickName[50];
	struct tagLLIST *next;
} LLIST;



//constants
#define kBufferLengh 256
#define kTrue 1
#define MaxHashEntries 127




void eliminateEndOfLine(char * buffer);
NICK_NAMES *insert(char* insertString ,NICK_NAMES *newHead);
void showSearchResultsForLinkList(NICK_NAMES *head,char buffer[],int*linkListCount);
int addHashItem(LLIST *hashtable[], char *w);
int findHashItem(LLIST *hashtable[], char *w,int*hashCount);
unsigned long hashFunction(char *str);
NICK_NAMES *delete_info (NICK_NAMES*head);
void emptyHashTable(LLIST *hashtable[]);





