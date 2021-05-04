import os
import sys

# Example 10
def Example_010(args):
	oValList = [1, 3, 5, 7, 9]
	
	print(BinarySearch(oValList, 3))
	print(BinarySearch(oValList, -1))
	
# 값을 탐색한다
def BinarySearch(a_oValList, a_nTarget):
	nLeft = 0;
	nRight = len(a_oValList) - 1
	
	while nLeft <= nRight:
		nMiddle = int((nLeft + nRight) / 2)
		
		# 값이 존재 할 경우
		if a_nTarget == a_oValList[nMiddle]:
			return nMiddle
		else:
			# 목표 값이 작을 경우
			if a_nTarget < a_oValList[nMiddle]:
				nRight = nMiddle - 1
			else:
				nLeft = nMiddle + 1
				
	return None
