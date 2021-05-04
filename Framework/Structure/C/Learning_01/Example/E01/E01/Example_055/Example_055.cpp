//
//  Example_055.cpp
//  E01
//
//  Created by 이상동 on 2021/05/04.
//

#include "Example_055.hpp"

namespace E055 {
	//! 팩토리얼을 반환한다
	int Factorial(int a_nVal) {
		// 재귀 호출이 불가능 할 경우
		if(a_nVal <= 0) {
			return 1;
		}
		
		return a_nVal * Factorial(a_nVal - 1);
	}
	
	//! Example 55
	void Example_055(const int argc, const char **args) {
		printf("1! = %d\n", Factorial(1));
		printf("2! = %d\n", Factorial(2));
		printf("3! = %d\n", Factorial(3));
		printf("4! = %d\n", Factorial(4));
		printf("5! = %d\n", Factorial(5));
	}
}
