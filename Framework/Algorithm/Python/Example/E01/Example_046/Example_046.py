import os
import sys

# Example 46
def Example_046(args:[str]):
	nNumRows = 10
	nNumCols = 20
	
	oValList = []
	
	for i in range(0, nNumCols + 1):
		oValList.append([0] * (nNumCols + 1))
		
	oValList[0][0] = 1
	
	for i in range(0, len(oValList)):
		for j in range(0, len(oValList[i])):
			if i != j and nNumRows - i != nNumCols - j:
				oValList[i][j] += oValList[i - 1][j] if i > 0 else 0
				oValList[i][j] += oValList[i][j - 1] if j > 0 else 0
				
	print(f"{oValList[nNumRows - 1][nNumCols] + oValList[nNumRows][nNumCols - 1]}")
	