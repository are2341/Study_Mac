import os
import sys

import re

# Example 21
def Example_021(args:[str]):
	nMinVal = 1000
	nMaxVal = 9999
	
	oOperatorList = [
		"+", "-", "*", "/", ""
	]
	
	for i in range(nMinVal, nMaxVal + 1):
		oStr = f"{i}"
		PrintCalcCombinations(oStr[::-1], 1, i, len(oStr) - 1, oOperatorList)

# 수식 조합을 출력한다
def PrintCalcCombinations(a_oStr, a_nIdx, a_nVal, a_nNumOperators, a_oOperatorList):
	# 수식 조합이 불가능 할 경우
	if a_nNumOperators <= 0:
		try:
			oCalc = re.sub(r"0(\d+)", r"\1", a_oStr)
			
			# 수식 계산이 가능 할 경우
			if len(oCalc) > 4 and a_nVal == eval(oCalc):
				print(f"{oCalc} = {a_nVal}")
		except:
			return
	else:
		for oOperator in a_oOperatorList:
			PrintCalcCombinations(a_oStr[:a_nIdx] + oOperator + a_oStr[a_nIdx:], a_nIdx + 1 if len(oOperator) <= 0 else a_nIdx + 2, a_nVal, a_nNumOperators - 1, a_oOperatorList)
