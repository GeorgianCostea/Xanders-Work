/*
*Project    : MAD Assignment 4
*Date       : March 17 2013
*Programmer :Georgian Costea
*Description: This Program is capable of playing pure avi files and mp3 files 
*
*/


#include "stdafx.h"
#include "MAD Assign 4.h"
#include "MAD Assign 4Dlg.h"
#include "afxdialogex.h"
#include "Vfw.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#endif



class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support


protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()



CMADAssign4Dlg::CMADAssign4Dlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMADAssign4Dlg::IDD, pParent)
{
	m_Path = _T("");
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMADAssign4Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_PLAY, m_Play);
	DDX_Control(pDX, IDC_PAUSE, m_Pause);
	DDX_Text(pDX, IDC_PATH, m_Path);
}

BEGIN_MESSAGE_MAP(CMADAssign4Dlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_PLAY, &CMADAssign4Dlg::OnBnClickedPlay)
	ON_BN_CLICKED(IDC_STOP, &CMADAssign4Dlg::OnBnClickedStop)
	ON_BN_CLICKED(IDC_PAUSE, &CMADAssign4Dlg::OnBnClickedPause)
	ON_BN_CLICKED(IDC_BROWSE, &CMADAssign4Dlg::OnBnClickedBrowse)
END_MESSAGE_MAP()


// CMADAssign4Dlg message handlers

BOOL CMADAssign4Dlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	m_Video = NULL;
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMADAssign4Dlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMADAssign4Dlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMADAssign4Dlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CMADAssign4Dlg::OnBnClickedBrowse()
{

	m_Video = NULL;
	if(m_Video == NULL)
	{
		CFileDialog avi(TRUE,NULL,NULL,OFN_HIDEREADONLY,"AVI Files(*.avi)|*.avi|");
		if(avi.DoModal() == IDOK)
		{
			m_Path = avi.GetPathName();
			
			UpdateData(FALSE);
			
		}
	}

}


void CMADAssign4Dlg::OnBnClickedPlay()
{
	

	m_Video = NULL;
	if(m_Video == NULL)
	{
		m_Video = MCIWndCreate(this->GetSafeHwnd(),
		  AfxGetInstanceHandle(),
		  WS_CHILD | WS_VISIBLE|MCIWNDF_NOMENU,m_Path);
			
	}
	else
	{
		MCIWndHome(m_Video);
	}
	MCIWndPlay(m_Video);
	Pause = FALSE;
	m_Play.EnableWindow(FALSE);
}



void CMADAssign4Dlg::OnBnClickedStop()
{
	
	MCIWndStop(m_Video);
	if(m_Video !=NULL)
	{
		MCIWndDestroy(m_Video);
	
	}
	m_Play.EnableWindow(TRUE);

}



void CMADAssign4Dlg::OnBnClickedPause()
{
	
	if(Pause)
	{
		m_Pause.SetWindowText("Pause");
		MCIWndResume(m_Video);
		Pause = FALSE;
	}
	else
	{
		m_Pause.SetWindowText("UnPause");
		MCIWndPause(m_Video);
		Pause = TRUE;
	}
}


