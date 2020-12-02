//
//  Example_070.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_070.h"
#include "../Global/Function/GFunc.h"

//! 합계를 반환한다
int E070_GetSumValue(const int *a_pnValues, int a_nSize);

//! Example 70
void Example_070(const int argc, const char **args) {
	int anValues[10] = { 0 };
	const int nSize = sizeof(anValues) / sizeof(anValues[0]);
	
	G_InitArray(anValues, nSize);
	
	printf("===== 배열 요소 출력 =====\n");
	G_PrintArray(anValues, nSize);
	
	printf("\n합계 : %d\n", E070_GetSumValue(anValues, nSize));
}

//! 합계를 반환한다
int E070_GetSumValue(const int *a_pnValues, int a_nSize) {
	int nSumValue = 0;
	
	for(int i = 0; i < a_nSize; ++i) {
		nSumValue += a_pnValues[i];
	}
	
	return nSumValue;
}
