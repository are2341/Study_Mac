//
//  Example_063.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_063.h"

//! Example 63
void Example_063(const int argc, const char **args) {
	int nLhs = 0;
	int nRhs = 0;
	
	printf("정수 (2 개) 입력 : ");
	scanf("%d %d", &nLhs, &nRhs);
	
	printf("큰 수 : %d\n", (nLhs >= nRhs) ? nLhs : nRhs);
}
