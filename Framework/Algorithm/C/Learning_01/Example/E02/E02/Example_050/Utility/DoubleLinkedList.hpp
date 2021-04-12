//
//  DoubleLinkedList.hpp
//  E02
//
//  Created by 이상동 on 2021/04/13.
//

#ifndef DoubleLinkedList_hpp
#define DoubleLinkedList_hpp

#include "../../Global/Define/KGDefine.hpp"

namespace E050 {
	//! 노드
	struct STNode {
		int m_nValue;
		
		STNode *m_pstPrevNode;
		STNode *m_pstNextNode;
	};
	
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue);
}

#endif /* DoubleLinkedList_hpp */
