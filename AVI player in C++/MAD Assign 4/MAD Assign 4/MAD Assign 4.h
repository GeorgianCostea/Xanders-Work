
// MAD Assign 4.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif



#include "resource.h"		// main symbols


// CMADAssign4App:
// See MAD Assign 4.cpp for the implementation of this class
//

class CMADAssign4App : public CWinApp
{
public:
	CMADAssign4App();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CMADAssign4App theApp;