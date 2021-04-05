//
//  Example_025.hpp
//  E02
//
//  Created by 이상동 on 2021/02/14.
//

#ifndef Example_025_hpp
#define Example_025_hpp

#include "../Global/Define/KGDefine.hpp"

namespace EXAMPLE_025 {
	//! 노드
	struct STNode {
		int m_nValue;
		STNode *m_pstNextNode;
	};
	
	//! Example 25
	void Example_025(const int argc, const char **args);
	
	//! 노드를 생성한다
	STNode * CreateNode(int a_nValue);

	//! 노드를 제거한다
	void DestroyNode(STNode *a_pstNode);

	//! 노드를 추가한다
	void AddNode(STNode **a_pstHeadNode, STNode *a_pstNode);
}

#endif /* Example_025_hpp */
