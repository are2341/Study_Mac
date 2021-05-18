//
//  Example_063.cpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#include "Example_063.hpp"

namespace E063 {
	//! Example 63
	void Example_063(const int argc, const char **args) {
		int nValA = 0;
		int nValB = 0;
		
		printf("정수 (2 개) 입력 : ");
		scanf("%d %d", &nValA, &nValB);
		
		printf("최대 값 : %d\n", (nValA >= nValB) ? nValA : nValB);
	}
}
