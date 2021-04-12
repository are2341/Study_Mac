//
//  LinkedList.cpp
//  E02
//
//  Created by 이상동 on 2021/04/13.
//

#include "LinkedList.hpp"

namespace E038 {
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
	
	//! 노드를 삽입한다
	void InsertNode(STNode *a_pstPrevNode, STNode *a_pstNode) {
		a_pstNode->m_pstNextNode = a_pstPrevNode->m_pstNextNode;
		a_pstPrevNode->m_pstNextNode = a_pstNode;
	}
	
	//! 노드를 삽입한다
	void InsertHeadNode(STNode **a_pstHeadNode, STNode *a_pstNode) {
		// 헤드 노드가 없을 경우
		if(*a_pstHeadNode == nullptr) {
			*a_pstHeadNode = a_pstNode;
		} else {
			a_pstNode->m_pstNextNode = *a_pstHeadNode;
			*a_pstHeadNode = a_pstNode;
		}
	}
	
	//! 노드를 제거한다
	void RemoveNode(STNode **a_pstHeadNode, STNode *a_pstNode) {
		// 헤드 노드 일 경우
		if(*a_pstHeadNode == a_pstNode) {
			*a_pstHeadNode = a_pstNode->m_pstNextNode;
		} else {
			auto pstPrevNode = *a_pstHeadNode;
			
			while(pstPrevNode != nullptr && pstPrevNode->m_pstNextNode != a_pstNode) {
				pstPrevNode = pstPrevNode->m_pstNextNode;
			}
			
			// 제거 할 노드가 존재 할 경우
			if(pstPrevNode != nullptr) {
				pstPrevNode->m_pstNextNode = a_pstNode->m_pstNextNode;
			}
		}
	}
	
	//! 노드를 반환한다
	STNode * GetNode(STNode *a_pstHeadNode, int a_nIdx) {
		auto pstNode = a_pstHeadNode;
		
		for(int i = 0; i < a_nIdx && pstNode != nullptr; ++i) {
			pstNode = pstNode->m_pstNextNode;
		}
		
		return pstNode;
	}
	
	int GetNumNodes(STNode *a_pstHeadNode) {
		int nNumNodes = 0;
		auto pstNode = a_pstHeadNode;
		
		while(pstNode != nullptr) {
			pstNode = pstNode->m_pstNextNode;
			nNumNodes += 1;
		}
		
		return nNumNodes;
	}
}
