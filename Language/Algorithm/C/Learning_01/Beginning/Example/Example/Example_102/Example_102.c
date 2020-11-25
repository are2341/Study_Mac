//
//  Example_102.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_102.h"
#include "../Global/Function/GFunction.h"

//! 이진 탐색을 시작한다
int E102_BinarySearch(int *a_pnValues, int a_nLeft, int a_nRight, int a_nTarget);

//! Example 102
void Example_102(const int argc, const char **args) {
	int anValues[10] = { 0 };
	const int nSize = sizeof(anValues) / sizeof(anValues[0]);
	
	G_InitArray(anValues, nSize);
	
	printf("===== 배열 요소 출력 =====\n");
	G_PrintArray(anValues, nSize);
	
	int nValue = 0;
	
	printf("\n정수 입력 : ");
	scanf("%d", &nValue);
	
	int nResult = E102_BinarySearch(anValues, 0, nSize - 1, nValue);
	printf("탐색 결과 : %d\n", nResult);
}

//! 이진 탐색을 시작한다
int E102_BinarySearch(int *a_pnValues, int a_nLeft, int a_nRight, int a_nTarget) {
	while(a_nLeft <= a_nRight) {
		int nMiddle = (a_nLeft + a_nRight) / 2;
		
		if(a_pnValues[nMiddle] == a_nTarget) {
			return nMiddle;
		} else {
			if(a_nTarget < a_pnValues[nMiddle]) {
				a_nRight = nMiddle - 1;
			} else {
				a_nLeft = nMiddle + 1;
			}
		}
	}
	
	return -1;
}
