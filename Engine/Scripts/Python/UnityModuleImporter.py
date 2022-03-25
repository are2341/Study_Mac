import os
import sys

oProjName = sys.argv[1]
oBranchName = sys.argv[2]

oSubmoduleInfos = [
	{
		"Name": ".Module.UnityStudyImporter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/0300000001.module_unitystudyimporter_client.git"
	},

	{
		"Name": ".Module.UnityCommonImporter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonimporter_client.git"
	}
]

for oSubmoduleInfo in oSubmoduleInfos:
	oURL = oSubmoduleInfo["URL"]
	oPath = f"../../{oSubmoduleInfo['Path']}"
	oFullPath = f"../../{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}"

	# 서브 모듈이 없을 경우
	if not os.path.exists(oFullPath):
		# 디렉토리가 없을 경우
		if not os.path.exists(oPath):
			os.makedirs(oPath)

		os.system(f"git submodule add -f {oURL} {oFullPath}")

	oSubmodulePath = f"{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}"
	os.system(f"git submodule set-branch --branch {oBranchName} {oSubmodulePath}")

os.system(f"python3 UnityModuleStudyImporter.py {oProjName} {oBranchName}")
os.system(f"python3 UnityModuleCommonImporter.py {oProjName} {oBranchName}")
os.system(f"python3 UnityModulePluginImporter.py {oProjName} {oBranchName}")
os.system(f"python3 UnityModuleRemoteURLUpdater.py {oProjName}")
