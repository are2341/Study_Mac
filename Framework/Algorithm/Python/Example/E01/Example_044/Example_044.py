import os
import sys

KEY_POS_X = "x"
KEY_POS_Y = "y"

# Example 44
def Example_044(args:[str]):
	oPosList = [
		{ KEY_POS_X: 0, KEY_POS_Y: 0 }
	]
	
	print(GetNumPaths(oPosList, 12))
	
# 경로 개수를 반환한다
def GetNumPaths(a_oPosList, a_nMaxNumPaths):
	# 이동이 불가능 할 경우
	if a_nMaxNumPaths <= 0:
		return 1
	
	nNumPaths = 0
	
	oNextPosList = [
		{ KEY_POS_X: a_oPosList[-1][KEY_POS_X], KEY_POS_Y: a_oPosList[-1][KEY_POS_Y] + 1 },
		{ KEY_POS_X: a_oPosList[-1][KEY_POS_X], KEY_POS_Y: a_oPosList[-1][KEY_POS_Y] - 1 },
		{ KEY_POS_X: a_oPosList[-1][KEY_POS_X] - 1, KEY_POS_Y: a_oPosList[-1][KEY_POS_Y] },
		{ KEY_POS_X: a_oPosList[-1][KEY_POS_X] + 1, KEY_POS_Y: a_oPosList[-1][KEY_POS_Y] }
	]
	
	for oNextPos in oNextPosList:
		bIsContains = False
		
		for i in range(0, a_oPosList):
			# 위치가 동일 할 경우
			if a_oPosList[i][KEY_POS_X] == oNextPos[KEY_POS_X] and a_oPosList[i][KEY_POS_Y] == oNextPos[KEY_POS_Y]:
				bIsContains = True
				break
		
		# 다음 위치가 없을 경우
		if not bIsContains:
			nNumPaths += GetNumPaths(a_oPosList + [ oNextPos ], a_nMaxNumPaths - 1)
			
	return nNumPaths
