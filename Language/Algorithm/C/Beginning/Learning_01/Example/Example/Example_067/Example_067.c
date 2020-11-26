//
//  Example_067.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_067.h"

//! 값을 교환한다
void E067_Swap(int *a_pnLhs, int *a_pnRhs);

//! Example 67
void Example_067(const int argc, const char **args) {
	int nLhs = 0;
	int nRhs = 0;
	
	printf("정수 (2 개) 입력 : ");
	scanf("%d %d", &nLhs, &nRhs);
	
	printf("\n===== 교환 전 출력 =====\n");
	printf("%d, %d\n", nLhs, nRhs);
	
	E067_Swap(&nLhs, &nRhs);
	
	printf("\n===== 교환 후 출력 =====\n");
	printf("%d, %d\n", nLhs, nRhs);
}

//! 값을 교환한다
void E067_Swap(int *a_pnLhs, int *a_pnRhs) {
	int nTemp = *a_pnLhs;
	*a_pnLhs = *a_pnRhs;
	*a_pnRhs = nTemp;
}
