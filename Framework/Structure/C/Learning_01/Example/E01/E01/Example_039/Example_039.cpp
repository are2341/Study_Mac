//
//  Example_039.cpp
//  E01
//
//  Created by 이상동 on 2021/05/04.
//

#include "Example_039.hpp"

namespace E039 {
	//! 값을 탐색한다
	int BSearch(int *a_pnVals, int a_nSize, int a_nTarget) {
		int nLeft = 0;
		int nRight = a_nSize - 1;
		
		int nOperationTimes = 0;
		
		while(nLeft <= nRight) {
			int nMiddle = (nLeft + nRight) / 2;
			
			// 값이 존재 할 경우
			if(a_pnVals[nMiddle] == a_nTarget) {
				return nMiddle;
			} else {
				// 목표 값이 작을 경우
				if(a_nTarget < a_pnVals[nMiddle]) {
					nRight = nMiddle - 1;
				} else {
					nLeft = nMiddle + 1;
				}
			}
			
			nOperationTimes += 1;
		}
		
		printf("비교 연산 횟수 : %d\n", nOperationTimes);
		return -1;
	}
	
	//! Example 39
	void Example_039(const int argc, const char **args) {
		int anValsA[100] = { 0 };
		int anValsB[1000] = { 0 };
		int anValsC[10000] = { 0 };
		
		const int nSizeA = sizeof(anValsA) / sizeof(anValsA[0]);
		const int nSizeB = sizeof(anValsB) / sizeof(anValsB[0]);
		const int nSizeC = sizeof(anValsC) / sizeof(anValsC[0]);
		
		int nIdx = BSearch(anValsA, nSizeA, 4);
		
		// 값이 존재 할 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n\n", nIdx);
		}
		
		nIdx = BSearch(anValsB, nSizeB, 7);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n\n", nIdx);
		}
		
		nIdx = BSearch(anValsC, nSizeC, 7);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n", nIdx);
		}
	}
}
