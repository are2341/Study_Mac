//
//  Example_118.hpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#ifndef Example_118_hpp
#define Example_118_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E118 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize);
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize);
	
	//! 해시 테이블을 생성한다
	void MakeHashTable(int *a_pnVals, int a_nSize, int **a_pnOutHashTable, int *a_pnOutSize);
	
	//! 값을 탐색한다
	int FindVal(int *a_pnVals, int a_nSize, int a_nTarget);
	
	//! Example 118
	void Example_118(const int argc, const char **args);
}

#endif /* Example_118_hpp */
