import os
import sys

from itertools import permutations

# Example 66
def Example_066(args:[str]):
	nMinVal = 0
	nMaxVal = 9
	
	nNumVals = 0
	
	for nR, nE, nA, nD, nW, nI, nT, nL, nK, nS in permutations(range(nMinVal, nMaxVal + 1)):
		# 조건을 만족 할 경우
		if nR == 0 or nW == 0 or nT == 0 or nS == 0:
			continue
		
		nRead = (nR * 1000) + (nE * 100) + (nA * 10) + nD
		nWrite = (nW * 10000) + (nR * 1000) + (nI * 100) + (nT * 10) + nE
		nTalk = (nT * 1000) + (nA * 100) + (nL * 10) + nK
		nSkill = (nS * 10000) + (nK * 1000) + (nI * 100) + (nL * 10) + nL
		
		# 조건을 만족 할 경우
		if nRead + nWrite + nTalk == nSkill:
			nNumVals += 1
			print(f"{nRead} + {nWrite} + {nTalk} = {nSkill}")
	