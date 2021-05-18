import os
import sys

from random import *

# Example 95
def Practice_095(args):
	oValList = list()
	
	for i in range(0, 10):
		oValList.append(randint(1, 99))
	
	nSumVal = GetSumVal(oValList)
	
	print(f"리스트 요소: {oValList}")
	print(f"합계: {nSumVal}")
	
# 합계를 반환한다
def GetSumVal(a_oValList):
	# 데이터가 존재하지 않을 경우
	if len(a_oValList) <= 0:
		return 0
	
	return a_oValList[0] + GetSumVal(a_oValList[1:])
