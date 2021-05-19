//
//  Example_091.hpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#ifndef Example_091_hpp
#define Example_091_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E091 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 값을 탐색한다
	int FindVal(int *a_pnVals, int a_nSize, int a_nTarget);
	
	//! Example 91
	void Example_091(const int argc, const char **args);
}

#endif /* Example_091_hpp */
