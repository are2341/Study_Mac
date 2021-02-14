import os
import sys

oProjRoot = sys.argv[1]
oProjName = sys.argv[2]

oSubmoduleInfos = [
	{
		"Name": ".UnityModule.Study",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Study.Define",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Study.Utility",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Study.Importer",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Define",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Access",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Factory",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Extension",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Func",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Utility",
		"Path": f"{oProjName}/Packages"
	},
	
	{
		"Name": ".UnityModule.Common.Externals",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Ads",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Flurry",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Tenjin",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Facebook",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Firebase",
		"Path": f"{oProjName}/Packages"
	},
	
	{
		"Name": ".UnityModule.Common.Singular",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.GameCenter",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Purchase",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Noti",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": ".UnityModule.Common.Importer",
		"Path": f"{oProjName}/Packages"
	},

	{
		"Name": "PluginProjects",
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
