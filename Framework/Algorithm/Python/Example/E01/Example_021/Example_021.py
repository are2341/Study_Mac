import os
import sys

# Example 21
def Example_021(args:[str]):
	oOperatorList = [
		"", "+", "-", "*", "/"
	]
	
	for nVal in range(1000, 10000):
		oValStr = str(nVal)
		
		for oOperatorA in oOperatorList:
			for oOperatorB in oOperatorList:
				for oOperatorC in oOperatorList:
					oCalc = oValStr[0] + oOperatorA + oValStr[1] + oOperatorB + oValStr[2] + oOperatorC + oValStr[3]
					print(f"{oCalc}")
