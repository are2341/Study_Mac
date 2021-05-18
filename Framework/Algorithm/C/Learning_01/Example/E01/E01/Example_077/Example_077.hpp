//
//  Example_077.hpp
//  E01
//
//  Created by 이상동 on 2021/05/18.
//

#ifndef Example_077_hpp
#define Example_077_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E077 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 최대 값을 교환한다
	int GetMaxVal(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! Example 77
	void Example_077(const int argc, const char **args);
}

#endif /* Example_077_hpp */
