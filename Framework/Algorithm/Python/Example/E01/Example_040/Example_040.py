import os
import sys

from datetime import datetime, timedelta

# Example 40
def Example_040(args:[str]):
	oMinTime = datetime.strptime("1964-10-10", "%Y-%m-%d")
	oMaxTime = datetime.strptime("2020-07-24", "%Y-%m-%d")
	
	while oMinTime <= oMaxTime:
		oStr = "{0:b}".format(int(oMinTime.strftime("%Y%m%d")))
		
		# 조건을 만족 할 경우
		if oStr == oStr[::-1]:
			print(oMinTime.strftime("%Y-%m-%d"))
		
		oMinTime += timedelta(days = 1)
		