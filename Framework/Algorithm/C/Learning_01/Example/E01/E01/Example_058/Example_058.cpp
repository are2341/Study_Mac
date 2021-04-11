//
//  Example_058.cpp
//  E01
//
//  Created by 이상동 on 2021/04/12.
//

#include "Example_058.hpp"

namespace E058 {
	//! Example 58
	void Example_058(const int argc, const char **args) {
		int nWidth = 0;
		int nHeight = 0;
		
		printf("너비, 높이 입력 : ");
		scanf("%d %d", &nWidth, &nHeight);
		
		printf("삼각형 너비 : %f\n", (nWidth * nHeight) / 2.0f);
	}
}
