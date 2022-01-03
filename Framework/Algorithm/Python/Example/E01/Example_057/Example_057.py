import os
import sys

# Example 57
def Example_057(args:[str]):
	nVal = 2
	nNumVals = 0
	nMaxNumVals = 10
	
	oValDict = {}
	
	while nNumVals < nMaxNumVals + 1:
		nSumVal = 0
		nFibonacci = GetFibonacci(nVal, oValDict)
		
		for chLetter in f"{nFibonacci}":
			nSumVal += int(chLetter)
		
		nVal += 1
		
		# 조건을 만족 할 경우
		if nFibonacci % nSumVal <= 0:
			nNumVals += 1
			print(f"{nFibonacci}, ")
			
# 피보나치 값을 반환한다
def GetFibonacci(a_nVal, oValDict):
	# 값이 존재 할 경우
	if a_nVal in oValDict:
		return oValDict[a_nVal]
	
	# 조건을 만족 할 경우
	if a_nVal == 0 or a_nVal == 1:
		oValDict[a_nVal] = 1
	else:
		oValDict[a_nVal] = GetFibonacci(a_nVal - 1, oValDict) + GetFibonacci(a_nVal - 2, oValDict)

	return oValDict[a_nVal]
