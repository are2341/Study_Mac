//
//  GFunction.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "GFunction.h"

//! 배열을 초기화한다
void G_InitArray(int *a_pnValues, int a_nSize) {
	for(int i = 0; i < a_nSize; ++i) {
		a_pnValues[i] = rand() % 100;
	}
}

//! 배열을 출력한다
void G_PrintArray(const int *a_pnValues, int a_nSize) {
	for(int i = 0; i < a_nSize; ++i) {
		printf("%d, ", a_pnValues[i]);
	}
	
	printf("\n");
}
