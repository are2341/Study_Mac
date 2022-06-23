import os
import sys

import re

# Example 21
def Example_021(args:[str]):
	nMinVal = 1000
	nMaxVal = 9999
	
	oOpList = [
		"+", "-", "*", "/", ""
	]
	
	for i in range(nMinVal, nMaxVal + 1):
		PrintCalcCombinations("", oOpList, i, 0)

# 수식 조합을 출력한다
def PrintCalcCombinations(a_oCalc, a_oOpList, a_nVal, a_nIdx):
	oStr = f"{a_nVal}"[::-1]
	
	# 조건을 만족 할 경우
	if a_nIdx >= len(oStr) - 1:
		try:
			a_oCalc += oStr[a_nIdx]
			
			# 조건을 만족 할 경우
			if len(a_oCalc) > len(oStr) and a_nVal == eval(a_oCalc):
				print(f"{a_oCalc} = {a_nVal}")
		except:
			return
	else:
		for oOp in a_oOpList:
			PrintCalcCombinations(a_oCalc + oStr[a_nIdx] + oOp, a_oOpList, a_nVal, a_nIdx + 1)
