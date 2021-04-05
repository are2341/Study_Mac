//
//  Example_025.cpp
//  E02
//
//  Created by 이상동 on 2021/02/14.
//

#include "Example_025.hpp"

namespace EXAMPLE_025 {
	//! Example 25
	void Example_025(const int argc, const char **args) {
		
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
		free(a_pstNode);
	}

	//! 노드를 추가한다
	void AddNode(STNode **a_pstHeadNode, STNode *a_pstNode) {
		// 헤드 노드가 없을 경우
		if(*a_pstHeadNode == nullptr) {
			*a_pstHeadNode = a_pstNode;
		} else {
			auto pstTailNode = *a_pstHeadNode;
			
			while(pstTailNode->m_pstNextNode != nullptr) {
				pstTailNode = pstTailNode->m_pstNextNode;
			}
			
			pstTailNode->m_pstNextNode = a_pstNode;
		}
	}
}
