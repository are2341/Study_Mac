import os
import sys

# Example 50
def Example_050(args:[str]):
	nMinVal = 2
	nNumVals = 0
	
	oValListA = [
		0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26
	]
	
	oValListB = [
		0, 28, 9, 26, 30, 11, 7, 20, 32, 17, 5, 22, 34, 15, 3, 24, 36, 13, 1, 0, 27, 10, 25, 29, 12, 8, 19, 31, 18, 6, 21, 33, 16, 4, 23, 35, 14, 2
	]
	
	for i in range(nMinVal, min(len(oValListA), len(oValListB))):
		nNumVals += 1 if GetMaxSumVal(oValListA, i) < GetMaxSumVal(oValListB, i) else 0

	print(nNumVals)
	
# 최대 합계를 반환한다
def GetMaxSumVal(a_oValList, a_nNumVals):
	nMinVal = 0
	nSumVal = sum(a_oValList[0:a_nNumVals])
	nCompareVal = nSumVal
	
	for i in range(nMinVal, len(a_oValList)):
		nCompareVal += a_oValList[(a_nNumVals + i) % len(a_oValList)]
		nCompareVal -= a_oValList[i]
		
		nSumVal = max(nSumVal, nCompareVal)
		
	return nSumVal
