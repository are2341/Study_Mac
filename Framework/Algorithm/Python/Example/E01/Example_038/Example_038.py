import os
import sys

# Example 38
def Example_038(args:[str]):
	nMinVal = 2
	nMaxVal = 10000
	
	nNumVals = 0
	
	for i in range(nMinVal, nMaxVal + 1, 2):
		nNumVals += 1 if IsCollatzVal((i * 3) + 1, i) else 0
		
	print(nNumVals)
	
# 콜라츠 값 여부를 검사한다
def IsCollatzVal(a_nVal, a_nCompareVal):
	# 조건을 만족 할 경우
	if a_nVal == 1 or a_nVal == a_nCompareVal:
		return a_nVal != 1
	
	return IsCollatzVal(a_nVal // 2 if a_nVal % 2 == 0 else (a_nVal * 3) + 1, a_nCompareVal)
