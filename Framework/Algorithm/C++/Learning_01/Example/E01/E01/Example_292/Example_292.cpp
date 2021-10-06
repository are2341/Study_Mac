//
//  Example_292.cpp
//  E01
//
//  Created by 이상동 on 2021/10/07.
//

#include "Example_292.hpp"
#include "../Global/Function/GFunc.hpp"

namespace E292 {
	//! 서브 문자열 여부를 검사한다
	int IsSubStr(const char *a_pszStr, const char *a_pszSubStr) {
		int i = 0;
		int j = 0;
		
		while(i < strlen(a_pszStr) && j < strlen(a_pszSubStr)) {
			// 문자가 동일 할 경우
			if(a_pszStr[i] == a_pszSubStr[j]) {
				i += 1;
				j += 1;
			} else {
				i = (i - j) + 1;
				j = 0;
			}
		}
		
		return (j >= strlen(a_pszSubStr)) ? i - j : -1;
	}
	
	//! Example 292
	void Example_292(const int argc, const char **args) {
		char szStr[100] = "";
		char szSubStr[100] = "";
		
		printf("문자열 입력 : ");
		GFunc::GetStr(szStr, sizeof(szStr), stdin);
		
		printf("패턴 입력 : ");
		GFunc::GetStr(szSubStr, sizeof(szSubStr), stdin);
		
		printf("\n결과 : %d\n", IsSubStr(szStr, szSubStr));
	}
}
