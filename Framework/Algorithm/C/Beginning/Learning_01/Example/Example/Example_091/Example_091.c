//
//  Example_091.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_091.h"
#include "../Global/Function/GFunc.h"

//! 선형 탐색을 시작한다
int E091_LinearSearch(int *a_pnValues, int a_nSize, int a_nTarget);

//! Example 91
void Example_091(const int argc, const char **args) {
	int anValues[10] = { 0 };
	const int nSize = sizeof(anValues) / sizeof(anValues[0]);
	
	G_InitArray(anValues, nSize);
	
	printf("===== 배열 요소 출력 =====\n");
	G_PrintArray(anValues, nSize);
	
	int nValue = 0;
	
	printf("\n정수 입력 : ");
	scanf("%d", &nValue);
	
	int nResult = E091_LinearSearch(anValues, nSize, nValue);
	printf("탐색 결과 : %d\n", nResult);
}

//! 선형 탐색을 시작한다
int E091_LinearSearch(int *a_pnValues, int a_nSize, int a_nTarget) {
	for(int i = 0; i < a_nSize; ++i) {
		if(a_pnValues[i] == a_nTarget) {
			return i;
		}
	}
	
	return -1;
}
