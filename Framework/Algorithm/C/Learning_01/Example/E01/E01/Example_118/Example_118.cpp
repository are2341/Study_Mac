//
//  Example_118.cpp
//  E01
//
//  Created by 이상동 on 2021/05/19.
//

#include "Example_118.hpp"

namespace E118 {
	//! 값을 초기화한다
	void InitVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			a_pnVals[i] = (rand() % 10) + 1;
		}
	}
	
	//! 값을 출력한다
	void PrintVals(int *a_pnVals, int a_nSize) {
		for(int i = 0; i < a_nSize; ++i) {
			printf("%d, ", a_pnVals[i]);
		}
		
		printf("\n");
	}
	
	//! 해시 테이블을 생성한다
	void MakeHashTable(int *a_pnVals, int a_nSize, int **a_pnOutHashTable, int *a_pnOutSize) {
		int nSize = (int)(a_nSize * 1.5f);
		int *pnVals = (int *)malloc(sizeof(int) * nSize);
		
		for(int i = 0; i < nSize; ++i) {
			pnVals[i] = INT_MAX;
		}
		
		for(int i = 0; i < a_nSize; ++i) {
			int j = a_pnVals[i] % nSize;
			
			while(pnVals[j] != INT_MAX) {
				j = (j + 1) % nSize;
			}
			
			pnVals[j] = a_pnVals[i];
		}
		
		*a_pnOutSize = nSize;
		*a_pnOutHashTable = pnVals;
	}
	
	//! 값을 탐색한다
	int FindVal(int *a_pnVals, int a_nSize, int a_nTarget) {
		int i = a_nTarget % a_nSize;
		
		while(a_pnVals[i] != INT_MAX) {
			// 값이 존재 할 경우
			if(a_pnVals[i] == a_nTarget) {
				return i;
			}
			
			i = (i + 1) % a_nSize;
		}
		
		return -1;
	}
	
	//! Example 118
	void Example_118(const int argc, const char **args) {
		int anVals[5] = { 0 };
		const int nSize = sizeof(anVals) / sizeof(anVals[0]);
		
		int nHashSize = 0;
		int *pnHashTable = nullptr;
		
		InitVals(anVals, nSize);
		MakeHashTable(anVals, nSize, &pnHashTable, &nHashSize);
		
		printf("===== 배열 요소 =====\n");
		PrintVals(anVals, nSize);
		
		printf("\n===== 해시 테이블 요소 =====\n");
		PrintVals(pnHashTable, nHashSize);
		
		int nVal = 0;
		
		printf("\n정수 입력 : ");
		scanf("%d", &nVal);
		
		printf("탐색 결과 : %d\n", FindVal(pnHashTable, nHashSize, nVal));
		SAFE_FREE(pnHashTable);
	}
}
