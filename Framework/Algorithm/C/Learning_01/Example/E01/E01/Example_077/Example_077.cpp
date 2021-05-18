//
//  Example_077.cpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#include "Example_077.hpp"

namespace E077 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			a_pnVals[i] = (rand() % 10) + 1;
		}
	}
	
	//! 최대 값을 교환한다
	int GetMaxVal(int *a_pnVals, int a_nSize) {
		int nMaxVal = a_pnVals[0];
		
		for(int i = 1; i < a_nSize; ++i) {
			nMaxVal = (nMaxVal >= a_pnVals[i]) ? nMaxVal : a_pnVals[i];
		}
		
		return nMaxVal;
	}
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			printf("%d, ", a_pnVals[i]);
		}
		
		printf("\n");
	}
	
	//! Example 77
	void Example_077(const int argc, const char **args) {
		int anVals[5] = { 0 };
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		
		InitVals(anVals, nSize);
		
		printf("===== 배열 요소 =====\n");
		PrintVals(anVals, nSize);
		
		printf("\n최대 값 : %d\n", GetMaxVal(anVals, nSize));
	}
}
