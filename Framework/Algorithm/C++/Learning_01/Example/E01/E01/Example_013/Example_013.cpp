//
//  Example_013.cpp
//  E01
//
//  Created by 이상동 on 2021/10/06.
//

#include "Example_013.hpp"

namespace E013 {
	//! Example 13
	void Example_013(const int argc, const char **args) {
		int nValA = 0;
		int nValB = 0;
		int nValC = 0;
		
		printf("정수 (3 개) 입력 : ");
		scanf("%d %d %d", &nValA, &nValB, &nValC);
		
		int nMaxVal = nValA;
		nMaxVal = (nMaxVal >= nValB) ? nMaxVal : nValB;
		nMaxVal = (nMaxVal >= nValC) ? nMaxVal : nValC;
		
		printf("최대 값 : %d\n", nMaxVal);
	}
}
