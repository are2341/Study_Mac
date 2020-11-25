//
//  main.c
//  Example
//
//  Created by 이상동 on 2020/11/26.
//

#include "Example_091/Example_091.h"
#include "Example_102/Example_102.h"

//! 메인 함수
int main(const int argc, const char **args) {
	srand((unsigned int)time(NULL));
	
	Example_091(argc, args);
//	Example_102(argc, args);
	
	return 0;
}
