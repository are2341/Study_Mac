//
//  Example_038.cpp
//  E02
//
//  Created by 이상동 on 2021/04/12.
//

#include "Example_038.hpp"

namespace E038 {
	//! Example 38
	void Example_038(const int argc, const char **args) {
		
	}
	
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue) {
		auto pstNode = (STNode *)malloc(sizeof(STNode));
		pstNode->m_nValue = a_nValue;
		pstNode->m_pstNextNode = nullptr;
		
		return pstNode;
	}
	
	//! 노드를 제거한다
	void DestroyNode(STNode *a_pstNode) {
		SAFE_FREE(a_pstNode);
	}
}
