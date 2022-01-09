import os
import sys

from math import sqrt

# Example 62
def Example_062(args:[str]):
	nMinVal = 1
	nMaxNumVals = 10
	
	for i in range(nMinVal, sys.maxsize):
		oStr = f"{sqrt(i):0.10f}".replace(".", "")[0:nMaxNumVals]
		
		# 조건을 만족 할 경우
		if len(set(oStr)) == nMaxNumVals:
			print(i)
			break
			
	for i in range(nMinVal, sys.maxsize):
		oStr = f"{sqrt(i):0.10f}".split(".")[1]
		
		# 조건을 만족 할 경우
		if len(set(oStr)) == nMaxNumVals:
			print(i)
			break
			