//
//  Example_025.cpp
//  E02
//
//  Created by 이상동 on 2021/02/14.
//

#include "Example_025.hpp"

//! 노드
struct STE025Node {
	int m_nValue;
	STE025Node *m_pstNextNode;
};

//! 노드를 생성한다
STE025Node * E025CreateNode(int a_nValue);

//! 노드를 제거한다
void E025DestroyNode(STE025Node *a_pstNode);

//! 노드를 추가한다
void E025AddNode(STE025Node **a_pstHeadNode, STE025Node *a_pstNode);

//! Example 25
void Example_025(const int argc, const char **args) {
	
}

//! 노드를 생성한다
STE025Node * E025CreateNode(int a_nValue) {
	auto pstNode = (STE025Node *)malloc(sizeof(STE025Node));
	pstNode->m_nValue = a_nValue;
	pstNode->m_pstNextNode = nullptr;
	
	return pstNode;
}

//! 노드를 제거한다
void E025DestroyNode(STE025Node *a_pstNode) {
	free(a_pstNode);
}

//! 노드를 추가한다
void E025AddNode(STE025Node **a_pstHeadNode, STE025Node *a_pstNode) {
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
