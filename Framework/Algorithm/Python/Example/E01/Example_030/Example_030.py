import os
import sys

# Example 30
def Example_030(args:[str]):
	print(GetCutTimes(3, 1, 20))
	print(GetCutTimes(5, 1, 100))

# 자르기 횟수를 반환한다
def GetCutTimes(a_nNumWorkers:int, a_nNumBars:int, a_nLength:int):
	if a_nNumBars > a_nLength:
		return 0
	
	a_nNumBars = a_nNumBars * 2 if a_nNumBars < a_nNumWorkers else a_nNumBars + a_nNumWorkers
	return 1 + GetCutTimes(a_nNumWorkers, a_nNumBars, a_nLength)
