import os
import sys

# Example 17
def Example_017(args:[str]):
	nVal = 10
	
	while True:
		oStrA = str(nVal)
		oStrB = "{0:o}".format(nVal)
		oStrC = "{0:b}".format(nVal)
		
		# 회문 일 경우
		if oStrA == oStrA[::-1] and oStrB == oStrB[::-1] and oStrC == oStrC[::-1]:
			break
		
		nVal += 1
		
	print(f"{nVal}")
	