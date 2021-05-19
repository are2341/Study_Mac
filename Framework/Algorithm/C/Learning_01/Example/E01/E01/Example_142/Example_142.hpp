//
//  Example_142.hpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#ifndef Example_142_hpp
#define Example_142_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E142 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 값을 정렬한다
	void SortVals(int *a_pnVals, int a_nSize);
	
	//! 값을 교환한다
	void Swap(int *a_pnLhs, int *a_pnRhs);
	
	//! Example 142
	void Example_142(const int argc, const char **args);
}

#endif /* Example_142_hpp */
