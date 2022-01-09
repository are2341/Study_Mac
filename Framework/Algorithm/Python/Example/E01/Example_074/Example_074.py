import os
import sys

oValDict = {}

# Example 74
def Example_074(args:[str]):
	print(GetNumOffsets(0, 10, 4))
	
# 간격 개수를 반환한다
def GetNumOffsets(a_nValA, a_nValB, a_nMaxNumOffsets):
	# 조건을 만족 할 경우
	if a_nValA >= a_nValB:
		return 1 if a_nValA == a_nValB else 0
	
	nMinVal = 1
	nNumOffsets = 0
	
	oValTuple = (a_nValA, a_nValB)
	
	# 조건을 만족 할 경우
	if oValTuple in oValDict:
		return oValDict[oValTuple]
	
	for i in range(nMinVal, a_nMaxNumOffsets + 1):
		for j in range(nMinVal, a_nMaxNumOffsets + 1):
			nNumOffsets += GetNumOffsets(a_nValA + i, a_nValB - j, a_nMaxNumOffsets)
	
	oValDict[oValTuple] = nNumOffsets
	return oValDict[oValTuple]
