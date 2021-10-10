//
//  Example_300.cpp
//  E01
//
//  Created by 이상동 on 2021/10/10.
//

#include "Example_300.hpp"
#include "../Global/Function/GFunc.hpp"

namespace E300 {
	//! 포함 여부를 검사한다
	int IsContains(const char *a_pszStr, const char *a_pszSubStr) {
		int *pnIndices = (int *)malloc(sizeof(int) * (strlen(a_pszStr) + 1));
		memset(pnIndices, 0, sizeof(int) * (strlen(a_pszStr) + 1));
		
		SAFE_FREE(pnIndices);
		return -1;
	}
	
	//! Example 300
	void Example_300(const int argc, const char **args) {
		char szStr[100] = "";
		char szSubStr[100] = "";
		
		printf("문자열 입력 : ");
		GFunc::GetStr(szStr, sizeof(szStr), stdin);
		
		printf("서브 문자열 입력 : ");
		GFunc::GetStr(szSubStr, sizeof(szSubStr), stdin);
		
		printf("\n결과 : %d\n", IsContains(szStr, szSubStr));
	}
}
