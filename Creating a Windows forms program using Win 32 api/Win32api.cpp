/*******************************************************
* Programmer: Georgian Costea                          *
* Date : 9/8/2012                                      *
* Description : Windows application using Win32 api to *
* create a Windows form with 2 listboxes and a button .*
* Everytime the button is pressed and an item was      *
* selected in the left list will be deleted  from that *
* list and moved to the list from the right.           *
*******************************************************/

#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>

/*-----DEFINES-----*/
#define IDC_MAIN_BUTTON	101			
#define IDC_LEFT_LIST	102		
#define IDC_RIGHT_LIST  103



#pragma comment(linker,"\"/manifestdependency:type='win32' \
name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \
processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")


/*-----Global variables-----*/
HINSTANCE hInst;
HWND hLeftListBox;
HWND hRightListhBox;
HWND hMoveButton;

/* The main window class name.*/
static TCHAR szWindowClass[] = _T("win32app");


/* The string that appears in the application's title bar.*/
static TCHAR szTitle[] = _T("Georgian Costea: Attempt 1 ");



/*-----Prototypes-----*/
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);




int WINAPI WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance,LPSTR lpCmdLine,int nCmdShow)
{
    WNDCLASSEX wcex;

	ZeroMemory(&wcex,sizeof(WNDCLASSEX));
	wcex.cbClsExtra = NULL;
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = (WNDPROC)WndProc;
    wcex.cbClsExtra     = NULL;
    wcex.cbWndExtra     = NULL;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = NULL;
	wcex.hCursor        = LoadCursor(NULL, IDC_HAND);
    wcex.hbrBackground  = (HBRUSH)COLOR_WINDOW;
    wcex.lpszMenuName   = NULL;
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));


	/*Registers a window class for subsequent use in calls to the CreateWindow
	or CreateWindowEx function.*/ 
    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL,
            _T("Call to RegisterClassEx failed!"),
            _T("Call to RegisterClassEx failed!"),
            NULL);

        return 1;
    }

    hInst = hInstance; /* Store instance handle in our global variable*/

    /* creating the window */
    HWND hWnd = CreateWindow(szWindowClass , szTitle  ,WS_OVERLAPPEDWINDOW ,200,
		        200,600, 500,NULL,NULL,hInstance, NULL);

    if (!hWnd)
    {
        MessageBox(NULL,_T("Call to CreateWindow failed!"), _T("Call to CreateWindow failed!"),NULL);
        return 1;
    }

    /* The parameters to ShowWindow explained:
     hWnd: the value returned from CreateWindow
    nCmdShow: the fourth parameter from WinMain*/
    ShowWindow(hWnd,nCmdShow);
    UpdateWindow(hWnd);

    /* Main message loop:*/
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int) msg.wParam;
}



/*
* Function Name : LRESULT CALLBACK WndProc
* Parameters : HWND hWnd
*              UINT message
*              WPARAM wParam 
*              LPARAM lParam
* Return Value : returns 0 
*                DefWindowProc(hWnd,message,wParam,lParam);
*
* Description : Creates the listboxes and the button and executes
* the commands .
*/
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int counter = 0;
	int NameIndex = 0;
	int NameLength = 0;
	
	LPCSTR Names[] = {"John Smith","Mark Ryan","Jerry Hayes","Anothony Hodgins","Bart Simpson"};
	LPTSTR Buffer = " " ;

	switch(message)

	{
		case WM_CREATE:
		{
			/*Creating the left listbox*/
			hLeftListBox = CreateWindow("LISTBOX","",WS_BORDER|WS_CHILD|WS_VISIBLE|LBS_NOTIFY,
				50,150,150,170,hWnd,(HMENU)IDC_LEFT_LIST,NULL,NULL);

			for(counter = 0 ; counter <5 ;counter++)
			{
				SendMessage(hLeftListBox, LB_ADDSTRING, 0, (LPARAM)Names[counter]);
			}
			
			/*second the right listbox*/
			hRightListhBox=CreateWindow("LISTBOX","",WS_BORDER|WS_CHILD|WS_VISIBLE,
				350,150,150,170,hWnd,(HMENU)IDC_RIGHT_LIST,NULL,NULL);



			/*Create the Move button */
			hMoveButton=CreateWindow("BUTTON","Move",WS_TABSTOP|WS_VISIBLE|WS_CHILD|BS_DEFPUSHBUTTON,
				225,230,100,50,hWnd,(HMENU)IDC_MAIN_BUTTON,NULL,NULL);
			EnableWindow(hMoveButton,false);
		}
		break;

		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
            {
				
				
				case IDC_LEFT_LIST:
				{
					
					//enable the button only when an item was selected 
					if((SendMessage(hLeftListBox, LB_GETCURSEL ,NULL , NULL)) != LB_ERR) 
						
					{ // the button enables ONLY when an items is sellected in the list 
						EnableWindow(hMoveButton,true);
					}
					else
					{
						EnableWindow(hMoveButton,false);
					}
					break;
					
				}
				case IDC_MAIN_BUTTON:
				{
					NameIndex = SendMessage(hLeftListBox, LB_GETCURSEL, NULL, NULL);
					/* getting the current position of the cursor , if its not out of scope it will 
					  execute the code otherwise it will break out of it.*/
					if(NameIndex != LB_ERR)
					{
						
						/* getting the length of the name to create a buffer long enough*/
						NameLength = SendMessage(hLeftListBox, LB_GETTEXTLEN, NameIndex, NULL);

						if(NameLength == LB_ERR)
						{
							break;
						}
						/* allocating memory */
						Buffer = (LPTSTR)malloc(NameLength + 1);

						/* getting the text from the left listbox by using LB_GETTEXT and putting it in the Buffer
						 after it deletes that perticular field from the left listbox and the next SendMessage puts the
						 name in the right listbox*/
						SendMessage(hLeftListBox, LB_GETTEXT, NameIndex, (LPARAM)Buffer);
						SendMessage(hLeftListBox, LB_DELETESTRING, NameIndex, NULL);
						SendMessage(hRightListhBox, LB_ADDSTRING, NULL, (LPARAM)Buffer);

						free(Buffer);
						EnableWindow(hMoveButton,false);
					
						break;
					}
					else
					{
						break;
					}				
				}
			}	
		}
		break;
		case WM_DESTROY:
		{
			PostQuitMessage(0);   
			return 0;
		}
		break;
	
	}
	return DefWindowProc(hWnd,message,wParam,lParam);

}
