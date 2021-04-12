//
//  LinkedList.hpp
//  E02
//
//  Created by 이상동 on 2021/04/13.
//

#ifndef LinkedList_hpp
#define LinkedList_hpp

#include "../../Global/Define/KGDefine.hpp"

namespace E038 {
	//! 노드
	struct STNode {
		int m_nValue;
		STNode *m_pstNextNode;
	};
	
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue);
	
	//! 노드를 제거한다
	void DestroyNode(STNode *a_pstNode);
	
	//! 노드를 추가한다
	void AddNode(STNode **a_pstHeadNode, STNode *a_pstNode);
	
	//! 노드를 삽입한다
	void InsertNode(STNode *a_pstCurNode, STNode *a_pstNode);
	
	//! 노드를 삽입한다
	void InsertHeadNode(STNode **a_pstHeadNode, STNode *a_pstNode);
	
	//! 노드를 제거한다
	void RemoveNode(STNode **a_pstHeadNode, STNode *a_pstNode);
	
	//! 노드를 반환한다
	STNode * GetNode(STNode *a_pstHeadNode, int a_nIdx);
	
	int GetNumNodes(STNode *a_pstHeadNode);
}

#endif /* LinkedList_hpp */
