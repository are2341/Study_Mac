//
//  Example_142.c
//  Example
//
//  Created by 이상동 on 2020/11/30.
//

#include "Example_142.h"
#include "../Global/Function/GFunc.h"

//! 선택 정렬을 시작한다
void E142_SelectionSort(int *a_pnValues, int a_nSize) {
	for(int i = 0; i < a_nSize - 1; ++i) {
		int nMin = i;
		
		for(int j = i + 1; j < a_nSize; ++j) {
			if(a_pnValues[j] < a_pnValues[nMin]) {
				nMin = j;
			}
		}
		
		G_Swap(&a_pnValues[i], &a_pnValues[nMin]);
	}
}

//! Example 142
void Example_142(const int argc, const char **args) {
	int anValues[10] = { 0 };
	const int nSize = sizeof(anValues) / sizeof(anValues[0]);
	
	G_InitArray(anValues, nSize);
	
	printf("===== 정렬 전 배열 요소 출력 =====\n");
	G_PrintArray(anValues, nSize);
	
	E142_SelectionSort(anValues, nSize);
	
	printf("\n===== 정렬 후 배열 요소 출력 =====\n");
	G_PrintArray(anValues, nSize);
}
