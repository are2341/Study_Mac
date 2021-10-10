//
//  Example_292.cpp
//  E01
//
//  Created by 이상동 on 2021/10/07.
//

#include "Example_292.hpp"
#include "../Global/Function/GFunc.hpp"

namespace E292 {
	//! 포함 여부를 검사한다
	int IsContains(const char *a_pszStr, const char *a_pszSubStr) {
		for(int i = 0; i < strlen(a_pszStr); ++i) {
			int j = 0;
			
			for(j = 0; j < strlen(a_pszSubStr); ++j) {
				// 문자가 다를 경우
				if(a_pszStr[i + j] != a_pszSubStr[j]) {
					break;
				}
			}
			
			// 문자열이 존재 할 경우
			if(j >= strlen(a_pszSubStr)) {
				return i;
			}
		}
		
		return -1;
	}
	
	//! Example 292
	void Example_292(const int argc, const char **args) {
		char szStr[100] = "";
		char szSubStr[100] = "";
		
		printf("문자열 입력 : ");
		GFunc::GetStr(szStr, sizeof(szStr), stdin);
		
		printf("서브 문자열 입력 : ");
		GFunc::GetStr(szSubStr, sizeof(szSubStr), stdin);
		
		printf("\n결과 : %d\n", IsContains(szStr, szSubStr));
	}
}
