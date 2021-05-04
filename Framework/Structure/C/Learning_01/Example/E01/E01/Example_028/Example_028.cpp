//
//  Example_028.cpp
//  E01
//
//  Created by 이상동 on 2021/05/04.
//

#include "Example_028.hpp"

namespace E028 {
	//! 값을 탐색한다
	int BSearch(int *a_pnVals, int a_nSize, int a_nTarget) {
		int nLeft = 0;
		int nRight = a_nSize - 1;
		
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
		}
		
		return -1;
	}
	
	//! Example 28
	void Example_028(const int argc, const char **args) {
		int anVals[] = {
			3, 5, 2, 4, 9
		};
		
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		int nIdx = BSearch(anVals, nSize, 4);
		
		// 값이 존재 할 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n", nIdx);
		}
		
		nIdx = BSearch(anVals, nSize, 7);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n", nIdx);
		}
	}
}
