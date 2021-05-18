//
//  Example_067.cpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#include "Example_067.hpp"

namespace E067 {
	//! 값을 교환한다
	void Swap(int *a_pnLhs, int *a_pnRhs) {
		int nTemp = *a_pnLhs;
		*a_pnLhs = *a_pnRhs;
		*a_pnRhs = nTemp;
	}
	
	//! Example 67
	void Example_067(const int argc, const char **args) {
		int nValA = 0;
		int nValB = 0;
		
		printf("정수 (2 개) 입력 : ");
		scanf("%d %d", &nValA, &nValB);
		
		printf("===== 교환 전 =====\n");
		printf("%d, %d\n", nValA, nValB);
		
		Swap(&nValA, &nValB);
		
		printf("\n===== 교환 후 =====\n");
		printf("%d, %d\n", nValA, nValB);
	}
}
