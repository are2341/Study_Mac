import os
import sys

# Example 26
def Example_026(args:[str]):
	nMinVal = 1
	nMaxVal = 100
	
	for i in range(nMinVal, nMaxVal + 1):
		bIsOpen = True
		
		for j in range(nMinVal + 1, i + 1):
			bIsOpen = not bIsOpen if i % j == 0 else bIsOpen
		
		# 조건을 만족 할 경우
		if bIsOpen:
			print(i)
			