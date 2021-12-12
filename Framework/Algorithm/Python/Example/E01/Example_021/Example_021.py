import os
import sys

# Example 21
def Example_021(args:[str]):
	oOperatorList = [
		"", "+", "-", "*", "/"
	]

	for i in range(1000, 10000):
		oStr = str(i)

		for oOperatorA in oOperatorList:
			for oOperatorB in oOperatorList:
				for oOperatorC in oOperatorList:
					oCalc = oStr[0] + oOperatorA + oStr[1] + oOperatorB + oStr[2] + oOperatorC + oStr[3]
					print(f"{oCalc}")
