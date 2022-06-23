import os
import sys

# Example 17
def Example_017(args:[str]):
	nMinVal = 10
	
	for i in range(nMinVal, sys.maxsize):
		oStrA = f"{i}"
		oStrB = f"{i:o}"
		oStrC = f"{i:b}"
		
		# 조건을 만족 할 경우
		if oStrA == oStrA[::-1] and oStrB == oStrB[::-1] and oStrC == oStrC[::-1]:
			print(i)
			break
			