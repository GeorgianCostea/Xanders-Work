/************************************************************
* Programmer: Georgian Costea								
* Date: 05/05/2013									
* Description: This source file is used to store all the    
* number define prototypes,structs, include<>,and warnings  
* desables                                                  
*************************************************************/
#include <stdio.h>
#include<Windows.h>
#include<direct.h>
#include<string.h>



//desable the warning 4996 because its 
//a microsoft warning related 
#pragma warning( disable : 4996 )


/*where the data about the subDirectories
and nicknames are stored (structs)*/

typedef struct nickNames
{
	char kNames[50];
	char nickNames[50];
	struct nickNames* next;
} NICK_NAMES;




typedef struct DirNames
{
	char dNames[100];
	char realNames[100];
	struct DirNames * next;
} DIR_NAMES;



//constants
#define kBufferLengh 256
#define kTrue 1



//prototypes
void doDirAndOpenFile(void);
void eliminateEndOfLine(char *buffer);
NICK_NAMES *insert(char* insertString ,NICK_NAMES *newHead);
void showList(NICK_NAMES *head);
DIR_NAMES *enter_new_info (char* subDirName ,DIR_NAMES *newHead);
void show_List(DIR_NAMES *head);
int FindFiles(DIR_NAMES* head,NICK_NAMES*newHead);




