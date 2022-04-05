//
//  Example_017.cpp
//  E01
//
//  Created by 이상동 on 2022/04/02.
//

#include "Example_017.hpp"

namespace E017 {
	/** Example 17 */
	void Example_017(const int argc, const char **args) {
		int nLhs = 0;
		int nRhs = 0;
		
		std::cout << "정수 1 입력 : "; std::cin >> nLhs;
		std::cout << "정수 2 입력 : "; std::cin >> nRhs;
		
		std::cout << std::endl << "결과 : " << (nLhs + nRhs) << std::endl;
	}
}
