import os
import sys

# Example 36
def Example_036(args:[str]):
	nIntValA:int = 123
	nIntValB:int = -123
	nIntValC:int = 0
	
	print("=====> 정수형 <=====")
	print(f"{nIntValA}, {nIntValB}, {nIntValC}")
	
	fFltValA:float = 1.2
	fFltValB:float = -3.45
	fFltValC:float = 4.24E10
	fFltValD:float = 4.24e-10
	
	print("\n=====> 실수형 <=====")
	print(f"{fFltValA}, {fFltValB}, {fFltValC}, {fFltValD}")
	
	nOctVal:int = 0o177
	
	print("\n=====> 8 진수 <=====")
	print(f"{nOctVal}")
	
	nHexValA:int = 0x8ff
	nHexValB:int = 0xABC
	
	print("\n=====> 16 진수 <=====")
	print(f"{nHexValA}, {nHexValB}")