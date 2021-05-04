//
//  Example_052.cpp
//  E01
//
//  Created by 이상동 on 2021/05/04.
//

#include "Example_052.hpp"

namespace E052 {
	//! 재귀 호출을 처리한다
	void Recursive(int a_nVal) {
		// 재귀 호출이 불가능 할 경우
		if(a_nVal <= 0) {
			return;
		}
		
		printf("Recursive Call : %d\n", a_nVal);
		Recursive(a_nVal - 1);
	}
	
	//! Example 52
	void Example_052(const int argc, const char **args) {
		Recursive(3);
	}
}
