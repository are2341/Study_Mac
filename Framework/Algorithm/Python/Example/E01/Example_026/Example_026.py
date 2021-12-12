import os
import sys

# Example 26
def Example_026(args:[str]):
	oFlagList = [False] * 100
	
	for i in range(1, len(oFlagList)):
		for j in range(i, len(oFlagList), i + 1):
			oFlagList[j] = not oFlagList[j]
			
	for i in range(0, len(oFlagList)):
		# 출력이 가능 할 경우
		if not oFlagList[i]:
			print(i + 1)
			