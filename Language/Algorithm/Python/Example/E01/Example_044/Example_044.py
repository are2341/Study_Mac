import os
import sys

# Example 44
def Example_044(args:[str]):
	oPosList = [
		{ "x": 0, "y": 0 }
	]
	
	print(GetNumPaths(oPosList, 12))
	
# 경로 개수를 반환한다
def GetNumPaths(a_oPosList, a_nMaxNumPaths):
	# 조건을 만족 할 경우
	if a_nMaxNumPaths <= 0:
		return 1
	
	nNumPaths = 0
	
	oNextPosList = [
		{ "x": a_oPosList[-1]["x"], "y": a_oPosList[-1]["y"] + 1 },
		{ "x": a_oPosList[-1]["x"], "y": a_oPosList[-1]["y"] - 1 },
		{ "x": a_oPosList[-1]["x"] - 1, "y": a_oPosList[-1]["y"] },
		{ "x": a_oPosList[-1]["x"] + 1, "y": a_oPosList[-1]["y"] }
	]
	
	for oNextPos in oNextPosList:
		bIsContains = False
		
		for oPos in a_oPosList:
			# 조건을 만족 할 경우
			if oPos["x"] == oNextPos["x"] and oPos["y"] == oNextPos["y"]:
				bIsContains = True
				break
		
		# 조건을 만족 할 경우
		if not bIsContains:
			nNumPaths += GetNumPaths(a_oPosList + [ oNextPos ], a_nMaxNumPaths - 1)
			
	return nNumPaths
