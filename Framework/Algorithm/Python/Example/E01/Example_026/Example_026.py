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
			
		# 오픈 상태 일 경우
		if bIsOpen:
			print(f"{i}, ")
			