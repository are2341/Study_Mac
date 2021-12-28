import os
import sys

from datetime import datetime, timedelta

# Example 40
def Example_040(args:[str]):
	oStartTime = datetime.strptime("1964-10-10", "%Y-%m-%d")
	oEndTime = datetime.strptime("2020-07-24", "%Y-%m-%d")
	
	oDeltaTime = timedelta(days = 1)
	
	while oStartTime <= oEndTime:
		oStr = "{0:b}".format(int(oStartTime.strftime("%Y%m%d")))
		
		# 회문 일 경우
		if oStr == oStr[::-1]:
			print(oStartTime.strftime("%Y-%m-%d"))
		
		oStartTime += oDeltaTime
		