import os
import sys

oProjRoot = sys.argv[1]
oProjName = sys.argv[2]

oSubmoduleInfos = [
	{
		"Name": ".Module.UnityStudy",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityStudyDefine",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityStudyUtility",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityStudyImporter",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommon",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonDefine",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonAccess",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonFactory",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonExtension",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonFunc",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonUtility",
		"Path": f"{oProjName}/Packages"
	},
	
	{
		"Name": ".Module.UnityCommonExternals",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonAds",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonFlurry",
		"Path": f"{oProjName}/Packages"
	},
	
	{
		"Name": ".Module.UnityCommonFacebook",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonFirebase",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonGameAnalytics",
		"Path": f"{oProjName}/Packages"
	},
	
	{
		"Name": ".Module.UnityCommonSingular",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonGameCenter",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonPurchase",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonNoti",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".Module.UnityCommonImporter",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": "NativePlugins",
		"Path": oProjName
	},

	{
		"Name": "UnityPackages",
		"Path": oProjName
	}
]

# 경로를 탐색한다
def FindPath(a_oBasePath):
	i = 0

	while i < 10 and not os.path.exists(a_oBasePath):
		i += 1
		a_oBasePath = f"../{a_oBasePath}"

	return a_oBasePath

for oSubmoduleInfo in oSubmoduleInfos:
	oPath = FindPath(f"{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}")

	# 프로젝트 루트가 유효 할 경우
	if len(oProjRoot) >= 1:
		oModulePath = FindPath(f".git/modules/{oProjRoot}/{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}")
	else:
		oModulePath = FindPath(f".git/modules/{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}")

	print(oModulePath)
	
	# 서브 모듈이 존재 할 경우
	if os.path.exists(oPath):
		os.system(f"git submodule deinit -f {oPath}")
		os.system(f"git rm -f {oPath}")

	os.system(f"rm -rf {oPath}")
	os.system(f"rm -rf {oModulePath}")
