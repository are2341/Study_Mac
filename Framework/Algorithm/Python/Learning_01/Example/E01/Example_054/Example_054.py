import os
import sys

# Example 54
def Example_054(args):
	Countdown(10)
	
# 카운트를 출력한다
def Countdown(a_nVal):
	print(a_nVal)
	
	# 재귀 호출이 불가능 할 경우
	if a_nVal <= 1:
		return
	else:
		Countdown(a_nVal - 1)
		