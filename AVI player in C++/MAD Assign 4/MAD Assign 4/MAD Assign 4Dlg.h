
// MAD Assign 4Dlg.h : header file
//

#pragma once
#include "afxwin.h"



class CMADAssign4Dlg : public CDialogEx
{
// Construction
public:
	CMADAssign4Dlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MADASSIGN4_DIALOG };
	CButton m_Play;
	CButton m_Pause;
	CString m_Path;
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBnClickedPlay();
	afx_msg void OnBnClickedStop();
	afx_msg void OnBnClickedPause();
	afx_msg void OnBnClickedBrowse();
	DECLARE_MESSAGE_MAP()
public:
	HWND m_Video;
	BOOL Pause;

};
