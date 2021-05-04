//
//  Example_019.cpp
//  E01
//
//  Created by 이상동 on 2021/04/18.
//

#include "Example_019.hpp"

namespace E019 {
	//! 값을 탐색한다
	int LSearch(int *a_pnVals, int a_nSize, int a_nTarget) {
		for(int i = 0; i < a_nSize; ++i) {
			// 값이 존재 할 경우
			if(a_pnVals[i] == a_nTarget) {
				return i;
			}
		}
		
		return -1;
	}
	
	//! Example 19
	void Example_019(const int argc, const char **args) {
		int anVals[] = {
			3, 5, 2, 4, 9
		};
		
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		int nIdx = LSearch(anVals, nSize, 4);
		
		// 값이 존재 할 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n", nIdx);
		}
		
		nIdx = LSearch(anVals, nSize, 7);
		
		// 값이 없을 경우
		if(nIdx <= -1) {
			printf("탐색 실패\n");
		} else {
			printf("타겟 저장 인덱스 : %d\n", nIdx);
		}
	}
}
