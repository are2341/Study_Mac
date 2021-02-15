//
//  Example_063.cpp
//  E01
//
//  Created by 이상동 on 2021/02/15.
//

#include "Example_063.hpp"

namespace EXAMPLE_063 {
	//! Example 63
	void Example_063(const int argc, const char **args) {
		int nValueA = 0;
		int nValueB = 0;
		
		printf("정수 (2 개) 입력 : ");
		scanf("%d %d", &nValueA, &nValueB);
		
		printf("최대 값 : %d\n", (nValueA >= nValueB) ? nValueA : nValueB);
	}
}
