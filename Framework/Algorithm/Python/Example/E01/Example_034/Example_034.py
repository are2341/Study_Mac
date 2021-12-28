import os
import sys

# Example 34
def Example_034(args:[str]):
	oValList = [
		10, 50, 100, 500
	]
	
	print(GetNumCombinations(oValList, 0, 1000, 15))
	
# 조합 개수를 반환한다
def GetNumCombinations(a_oValList:[int], a_nIdx:int, a_nVal:int, a_nMaxNumVals:int):
	# 값을 벗어났을 경우
	if a_nIdx >= len(a_oValList):
		return 1 if a_nVal == 0 and a_nMaxNumVals >= 0 else 0
	
	nNumCombinations = 0
	
	for i in range(0, (a_nVal // a_oValList[a_nIdx]) + 1):
		nVal = a_nVal - (i * a_oValList[a_nIdx])
		nNumCombinations += GetNumCombinations(a_oValList, a_nIdx + 1, nVal, a_nMaxNumVals - i)
		
	return nNumCombinations
