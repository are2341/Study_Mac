//
//  GFunc.cpp
//  E01
//
//  Created by 이상동 on 2021/10/07.
//

#include "GFunc.hpp"

//! 문자열을 반환한다
void GFunc::GetStr(char *a_pszStr, int a_nSize, FILE *a_pstRStream) {
	fgets(a_pszStr, a_nSize, a_pstRStream);
	
	// 개행 문자가 존재 할 경우
	if(a_pszStr[strlen(a_pszStr) - 1] == '\n') {
		a_pszStr[strlen(a_pszStr) - 1] = '\0';
	}
}
