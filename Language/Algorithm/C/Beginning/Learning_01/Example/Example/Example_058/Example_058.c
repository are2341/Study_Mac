//
//  Example_058.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_058.h"

//! Example 58
void Example_058(const int argc, const char **args) {
	int nWidth = 0;
	int nHeight = 0;
	
	printf("삼각형 크기 입력 : ");
	scanf("%d %d", &nWidth, &nHeight);
	
	printf("삼각형 면적 : %f\n", (nWidth * nHeight) / 2.0f);
}
