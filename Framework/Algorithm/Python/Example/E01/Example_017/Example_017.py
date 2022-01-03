import os
import sys

# Example 17
def Example_017(args:[str]):
	nVal = 10
	
	while True:
		oStrA = f"{nVal}"
		oStrB = f"{nVal:o}"
		oStrC = f"{nVal:b}"
		
		# 회문 일 경우
		if oStrA == oStrA[::-1] and oStrB == oStrB[::-1] and oStrC == oStrC[::-1]:
			print(f"{nVal}")
			break
		
		nVal += 1
		