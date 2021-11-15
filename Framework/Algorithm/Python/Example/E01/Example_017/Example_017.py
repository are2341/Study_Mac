import os
import sys

# Example 17
def Example_017(args:[str]):
	for i in range(10, 10000):
		oStrA = str(i)
		oStrB = "{0:o}".format(i)
		oStrC = "{0:b}".format(i)
		
		bIsPalindromeA = oStrA == oStrA[::-1]
		bIsPalindromeB = oStrB == oStrB[::-1]
		bIsPalindromeC = oStrC == oStrC[::-1]
		
		# 대칭 수 일 경우
		if bIsPalindromeA and bIsPalindromeB and bIsPalindromeC:
			print(f"{i}")
			