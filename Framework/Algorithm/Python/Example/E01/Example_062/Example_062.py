import os
import sys

from math import sqrt

# Example 62
def Example_062(args:[str]):
	nVal = 1
	nMaxNumVals = 10
	
	while True:
		oStr = f"{sqrt(nVal):0.10f}".replace(".", "")[0:10]
		
		# 조건을 만족 할 경우
		if len(set(oStr)) == nMaxNumVals:
			print(f"{nVal}, ")
			break
			
		nVal += 1
		
	nVal = 1
	
	while True:
		oStr = f"{sqrt(nVal):0.10f}".split(".")[1]
		
		# 조건을 만족 할 경우
		if len(set(oStr)) == nMaxNumVals:
			print(f"{nVal}, ")
			break
			
		nVal += 1
		