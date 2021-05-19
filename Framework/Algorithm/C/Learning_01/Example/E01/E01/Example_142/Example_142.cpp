//
//  Example_142.cpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#include "Example_142.hpp"

namespace E142 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			a_pnVals[i] = (rand() % 10) + 1;
		}
	}
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			printf("%d, ", a_pnVals[i]);
		}
		
		printf("\n");
	}
	
	//! 값을 정렬한다
	void SortVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize - 1; ++i) {
			int nCompareIdx = i;
			
			for(int j = i + 1; j < a_nSize; ++j) {
				// 값이 작을 경우
				if(a_pnVals[j] <= a_pnVals[nCompareIdx]) {
					nCompareIdx = j;
				}
			}
			
			Swap(&a_pnVals[i], &a_pnVals[nCompareIdx]);
		}
	}
	
	//! 값을 교환한다
	void Swap(int *a_pnLhs, int *a_pnRhs) {
		int nTemp = *a_pnLhs;
		*a_pnLhs = *a_pnRhs;
		*a_pnRhs = nTemp;
	}
	
	//! Example 142
	void Example_142(const int argc, const char **args) {
		int anVals[5] = { 0 };
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		
		InitVals(anVals, nSize);
		
		printf("===== 정렬 전 배열 요소 =====\n");
		PrintVals(anVals, nSize);
		
		SortVals(anVals, nSize);
		
		printf("\n===== 정렬 후 배열 요소 =====\n");
		PrintVals(anVals, nSize);
	}
}
