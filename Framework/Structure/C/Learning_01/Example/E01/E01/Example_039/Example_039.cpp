//
//  Example_039.cpp
//  E01
//
//  Created by 이상동 on 2021/04/18.
//

#include "Example_039.hpp"

namespace E039 {
	//! 값을 탐색한다
	int FindValues(int *a_pnValues, int a_nSize, int a_nTarget) {
		int nLeft = 0;
		int nRight = a_nSize - 1;
		
		int nNumOperations = 0;
		
		while(nLeft <= nRight) {
			int nMiddle = (nLeft + nRight) / 2;
			
			// 값이 존재 할 경우
			if(a_pnValues[nMiddle] == a_nTarget) {
				return nMiddle;
			} else {
				// 탐색 값이 작을 경우
				if(a_nTarget < a_pnValues[nMiddle]) {
					nRight = nMiddle - 1;
				} else {
					nLeft = nMiddle + 1;
				}
			}
			
			nNumOperations += 1;
		}
		
		printf("비교 연산 횟수 : %d\n", nNumOperations);
		return -1;
	}
	
	//! Example 39
	void Example_039(const int argc, const char **args) {
		int anValuesA[500] = { 0 };
		int anValuesB[5000] = { 0 };
		int anValuesC[50000] = { 0 };
		
		int nIdx = FindValues(anValuesA, sizeof(anValuesA) / sizeof(anValuesA[0]), 1);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n\n", nIdx);
		}
		
		nIdx = FindValues(anValuesB, sizeof(anValuesB) / sizeof(anValuesB[0]), 1);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n\n", nIdx);
		}
		
		nIdx = FindValues(anValuesC, sizeof(anValuesC) / sizeof(anValuesC[0]), 1);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n\n", nIdx);
		}
	}
}
