import os
import sys

# Example 34
def Example_034(args:[str]):
	oValList = [
		10, 50, 100, 500
	]
	
	print(GetNumCombinations(oValList, 1000, 15))
	
# 조합 개수를 반환한다
def GetNumCombinations(a_oValList, a_nVal, a_nMaxNumVals):
	# 조합이 불가능 할 경우
	if len(a_oValList) <= 1:
		nDivide = a_nVal // a_oValList[0]
		return 1 if a_nVal - (nDivide * a_oValList[0]) == 0 and nDivide <= a_nMaxNumVals else 0
	
	nMinVal = 0
	nNumCombinations = 0
	
	for i in range(nMinVal, (a_nVal // a_oValList[0]) + 1):
		nVal = a_nVal - (i * a_oValList[0])
		nNumCombinations += GetNumCombinations(a_oValList[1:], nVal, a_nMaxNumVals - i)
		
	return nNumCombinations
