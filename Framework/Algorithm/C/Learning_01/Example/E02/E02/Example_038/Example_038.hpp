//
//  Example_038.hpp
//  E02
//
//  Created by 이상동 on 2021/04/12.
//

#ifndef Example_038_hpp
#define Example_038_hpp

#include "../Global/Define/KGDefine.hpp"

namespace E038 {
	//! 노드
	struct STNode {
		int m_nValue;
		STNode *m_pstNextNode;
	};
	
	//! Example 38
	void Example_038(const int argc, const char **args);
	
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue);
	
	//! 노드를 제거한다
	void DestroyNode(STNode *a_pstNode);
}

#endif /* Example_038_hpp */
