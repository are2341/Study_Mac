//
//  Example_038.cpp
//  E02
//
//  Created by 이상동 on 2021/04/12.
//

#include "Example_038.hpp"
#include "Utility/LinkedList.hpp"

namespace E038 {
	//! Example 38
	void Example_038(const int argc, const char **args) {
		STNode *pstList = nullptr;
		
		for(int i = 0; i < 5; ++i) {
			auto pstNode = CreateNode(i);
			AddNode(&pstList, pstNode);
		}
		
		InsertHeadNode(&pstList, CreateNode(-1));
		InsertHeadNode(&pstList, CreateNode(-2));
		
		for(int i = 0; i < GetNumNodes(pstList); ++i) {
			auto pstNode = GetNode(pstList, i);
			printf("List[%d] : %d\n", i, pstNode->m_nValue);
		}
		
		printf("\nInserting 3000 After [2]...\n\n");
		
		auto pstPrevNode = GetNode(pstList, 2);
		InsertNode(pstPrevNode, CreateNode(3000));
		
		for(int i = 0; i < GetNumNodes(pstList); ++i) {
			auto pstNode = GetNode(pstList, i);
			printf("List[%d] : %d\n", i, pstNode->m_nValue);
		}
		
		printf("\nDestroying List...\n");
		
		for(int i = 0; i < GetNumNodes(pstList); ++i) {
			auto pstNode = GetNode(pstList, 0);
			
			if(pstNode != nullptr) {
				RemoveNode(&pstList, pstNode);
				DestroyNode(pstNode);
			}
		}
	}
}
