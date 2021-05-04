import os
import sys

# Example 42
def Example_042(args):
	oValList = [5, 3, 6, 2, 10]
	print(SelectionSort(oValList))
	
# 최소 값을 탐색한다
def FindSmallest(a_oValList):
	nSmallestIdx = 0
	
	for i in range(1, len(a_oValList)):
		if a_oValList[i] < a_oValList[nSmallestIdx]:
			nSmallestIdx = i
			
	return nSmallestIdx

# 선택 정렬을 수행한다
def SelectionSort(a_oValList):
	oSortValList = []
	
	for i in range(0, len(a_oValList)):
		nSmallest = FindSmallest(a_oValList)
		oSortValList.append(a_oValList.pop(nSmallest))
		
	return oSortValList
