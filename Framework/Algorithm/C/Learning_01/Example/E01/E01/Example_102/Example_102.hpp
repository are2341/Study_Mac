//
//  Example_102.hpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#ifndef Example_102_hpp
#define Example_102_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E102 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 값을 탐색한다
	int FindVal(int *a_pnVals, int a_nSize, int a_nTarget);
	
	//! Example 102
	void Example_102(const int argc, const char **args);
}

#endif /* Example_102_hpp */
