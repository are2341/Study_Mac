//
//  Example_070.hpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#ifndef Example_070_hpp
#define Example_070_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E070 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 값을 교환한다
	int GetSumVal(int *a_pnVals, int a_nSize);
	
	//! Example 70
	void Example_070(const int argc, const char **args);
}

#endif /* Example_070_hpp */
