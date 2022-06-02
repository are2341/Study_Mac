import os
import sys

# Example 46
def Example_046(args:[str]):
	nMinVal = 0
	nNumRows = 10
	nNumCols = 20
	
	oValList = []
	
	for i in range(nMinVal, nNumRows + 1):
		oValList.append([0] * (nNumCols + 1))

	oValList[0][0] = 1

	for i in range(nMinVal, len(oValList)):
		for j in range(nMinVal, len(oValList[i])):
			# 조건을 만족 할 경우
			if i != j and nNumRows - i != nNumCols - j:
				oValList[i][j] += oValList[i - 1][j] if i > 0 else 0
				oValList[i][j] += oValList[i][j - 1] if j > 0 else 0

	print(oValList[nNumRows - 1][nNumCols] + oValList[nNumRows][nNumCols - 1])
