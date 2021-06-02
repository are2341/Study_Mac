//
//  Example_158.hpp
//  E01
//
//  Created by 이상동 on 2021/06/02.
//

#ifndef Example_158_hpp
#define Example_158_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E158 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 값을 정렬한다
	void SortVals(int *a_pnVals, int a_nSize);
	
	//! 값을 교환한다
	void Swap(int *a_pnLhs, int *a_pnRhs);
	
	//! Example 158
	void Example_158(const int argc, const char **args);
}

#endif /* Example_158_hpp */
