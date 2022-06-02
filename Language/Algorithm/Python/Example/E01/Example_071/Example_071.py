import os
import sys

# Example 71
def Example_071(args:[str]):
	oStrList = [
		"Brazil", "Croatia", "Mexico", "Cameroon", "Spain", "Netherlands", "Chile", "Australia", "Colombia", "Greece", "Cote d\'lvoire", "Japan", "Uruguay", "Costa Rica", "England", "Italy", "Switzerland", "Ecuador", "France", "Honduras", "Argentina", "Bosnia and Hoerzegovina", "Iran", "Nigeria", "Germany", "Portugal", "Ghana", "USA", "Belgium", "Algeria", "Russia", "Korea Republic"
	]
	
	oMaxStrList = []
	
	for oStr in oStrList:
		oCurStrList = GetMaxStrs([ oStr ], oStrList)
		oMaxStrList = oMaxStrList if len(oMaxStrList) >= len(oCurStrList) else oCurStrList
		
	print(oMaxStrList)
	print(len(oMaxStrList))
	
# 최대 문자열을 반환한다
def GetMaxStrs(a_oCurStrList, a_oStrList):
	oMaxStrList = a_oCurStrList
	
	for oStr in a_oStrList:
		# 조건을 만족 할 경우
		if oStr not in a_oCurStrList and oStr[0].upper() == a_oCurStrList[-1][-1].upper():
			oStrList = GetMaxStrs(a_oCurStrList + [ oStr ], a_oStrList)
			oMaxStrList = oMaxStrList if len(oMaxStrList) >= len(oStrList) else oStrList
			
	return oMaxStrList
