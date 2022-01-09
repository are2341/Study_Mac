import os
import sys

# Example 30
def Example_030(args:[str]):
	print(GetDivideTimes(3, 1, 20))
	print(GetDivideTimes(5, 1, 100))
	
# 나누기 횟수를 반환한다
def GetDivideTimes(a_nDivide, a_nNumVals, a_nMaxNumVals):
	# 조건을 만족 할 경우
	if a_nNumVals > a_nMaxNumVals:
		return 0
	
	nNumVals = a_nNumVals * 2 if a_nNumVals < a_nDivide else a_nNumVals + a_nDivide
	return 1 + GetDivideTimes(a_nDivide, nNumVals, a_nMaxNumVals)
