//
//  DoubleLinkedList.cpp
//  E02
//
//  Created by 이상동 on 2021/04/13.
//

#include "DoubleLinkedList.hpp"

namespace E050 {
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue) {
		auto pstNode = (STNode *)malloc(sizeof(STNode));
		pstNode->m_nValue = a_nValue;
		pstNode->m_pstPrevNode = nullptr;
		pstNode->m_pstNextNode = nullptr;
		
		return pstNode;
	}
}
