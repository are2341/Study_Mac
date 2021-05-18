//
//  Example_070.cpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#include "Example_070.hpp"

namespace E070 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			a_pnVals[i] = (rand() % 10) + 1;
		}
	}
	
	//! 값을 교환한다
	int GetSumVal(int *a_pnVals, int a_nSize) {
		int nSumVal = 0;
		
		for(int i = 0; i < a_nSize; ++i) {
			nSumVal += a_pnVals[i];
		}
		
		return nSumVal;
	}
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			printf("%d, ", a_pnVals[i]);
		}
		
		printf("\n");
	}
	
	//! Example 70
	void Example_070(const int argc, const char **args) {
		int anVals[5] = { 0 };
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		
		InitVals(anVals, nSize);
		
		printf("===== 배열 요소 =====\n");
		PrintVals(anVals, nSize);
		
		printf("\n합계 : %d\n", GetSumVal(anVals, nSize));
	}
}
