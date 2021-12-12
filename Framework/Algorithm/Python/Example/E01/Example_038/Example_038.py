import os
import sys

# Example 38
def Example_038(args:[str]):
	nNumVals = 0
	
	for i in range(2, 10000 + 1, 2):
		if IsLoopVal(i):
			nNumVals += 1
			
	print(f"{nNumVals}")
	
# 루프 값 여부를 검사한다
def IsLoopVal(a_nVal:int):
	nCompareVal = (a_nVal * 3) + 1
	
	while nCompareVal != 1 and nCompareVal != a_nVal:
		nCompareVal = nCompareVal // 2 if nCompareVal % 2 == 0 else (nCompareVal * 3) + 1
		
	return nCompareVal != 1
