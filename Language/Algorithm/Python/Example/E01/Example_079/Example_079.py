import os
import sys

from itertools import combinations

# Example 79
def Example_079(args:[str]):
	nMinVal = 1
	nMaxVal = 500 // 4
	
	oValList = []
	
	for i in range(nMinVal, nMaxVal + 1):
		oValMap = map(lambda a_nVal: a_nVal * ((i * 2) - a_nVal), range(nMinVal, i))
		
		for j, k in combinations(oValMap, 2):
			# 조건을 만족 할 경우
			if j + k == i * i:
				oValList.append([k / j, (i * i) / j])
				
	print(len(set(map(tuple, oValList))))
	