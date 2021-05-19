//
//  Example_102.cpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#include "Example_102.hpp"

namespace E102 {
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
	
	//! 값을 탐색한다
	int FindVal(int *a_pnVals, int a_nSize, int a_nTarget) {
		int nLeft = 0;
		int nRight = a_nSize - 1;
		
		while(nLeft <= nRight) {
			int nMiddle = (nLeft + nRight) / 2;
			
			// 값이 존재 할 경우
			if(a_pnVals[nMiddle] == a_nTarget) {
				return nMiddle;
			} else {
				// 탐색 값이 작을 경우
				if(a_pnVals[nMiddle] > a_nTarget) {
					nRight = nMiddle - 1;
				} else {
					nLeft = nMiddle + 1;
				}
			}
		}
		
		return -1;
	}
	
	//! Example 102
	void Example_102(const int argc, const char **args) {
		int anVals[5] = { 0 };
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		
		InitVals(anVals, nSize);
		
		printf("===== 배열 요소 =====\n");
		PrintVals(anVals, nSize);
		
		int nVal = 0;
		
		printf("\n정수 입력 : ");
		scanf("%d", &nVal);
		
		printf("탐색 결과 : %d\n", FindVal(anVals, nSize, nVal));
	}
}
