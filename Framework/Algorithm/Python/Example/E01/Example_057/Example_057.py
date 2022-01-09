import os
import sys

oValDict = {}

# Example 57
def Example_057(args:[str]):
	nVal = 2
	nNumVals = 0
	nMaxNumVals = 10

	while nNumVals < nMaxNumVals + 1:
		nSumVal = 0
		nFibonacci = GetFibonacci(nVal)

		for chLetter in f"{nFibonacci}":
			nSumVal += int(chLetter)

		# 조건을 만족 할 경우
		if nFibonacci % nSumVal <= 0:
			nNumVals += 1
			print(nFibonacci)

		nVal += 1
			
# 피보나치 값을 반환한다
def GetFibonacci(a_nVal):
	# 조건을 만족 할 경우
	if a_nVal in oValDict:
		return oValDict[a_nVal]
	
	# 조건을 만족 할 경우
	if a_nVal == 0 or a_nVal == 1:
		oValDict[a_nVal] = 1
	else:
		oValDict[a_nVal] = GetFibonacci(a_nVal - 1) + GetFibonacci(a_nVal - 2)
	
	return oValDict[a_nVal]
