import os
import sys

# Example 17
def Example_017(args):
	nNumber:int = 11
	
	while True:
		oStrA:str = "{0}".format(nNumber)
		oStrB:str = "{0:o}".format(nNumber)
		oStrC:str = "{0:b}".format(nNumber)
		
		bIsValidA:bool = oStrA == oStrA[::-1]
		bIsValidB:bool = oStrB == oStrB[::-1]
		bIsValidC:bool = oStrC == oStrC[::-1]
		
		# 회문 일 경우
		if bIsValidA and bIsValidB and bIsValidC:
			print(f"결과 : {nNumber}")
			break
			
		nNumber += 2