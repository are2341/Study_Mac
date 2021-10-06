//
//  GFunc.hpp
//  E01
//
//  Created by 이상동 on 2021/10/07.
//

#ifndef GFunc_hpp
#define GFunc_hpp

#include "../Define/KGDefine.hpp"

//! 전역 함수
class GFunc {
public:			// public 함수
	
	//! 문자열을 반환한다
	static void GetStr(char *a_pszStr, int a_nSize, FILE *a_pstRStream);
};

#endif /* GFunc_hpp */
