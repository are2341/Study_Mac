import os
import sys

oProjRoot = sys.argv[1]
oProjName = sys.argv[2]
oBranchName = sys.argv[3]

oSubmoduleInfos = [
	{
		"Name": ".UnityModule.Study.Importer",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.unitymodule_study_importer_client.git"
	},

	{
		"Name": ".UnityModule.Common.Importer",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_importer_client.git"
	}
]

for oSubmoduleInfo in oSubmoduleInfos:
	oURL = oSubmoduleInfo["URL"]
	oPath = f"../../{oSubmoduleInfo['Path']}"
	oFullpath = f"../../{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}"

	if not os.path.exists(oFullpath):
		if not os.path.exists(oPath):
			os.makedirs(oPath)

		os.system(f"git submodule add -f {oURL} {oFullpath}")

	oSubmodulePath = f"{oSubmoduleInfo['Path']}/{oSubmoduleInfo['Name']}"

	# 프로젝트 루트가 유효 할 경우
	if len(oProjRoot) >= 1:
		os.system(f"git submodule set-branch --branch {oBranchName} {oProjRoot}/{oSubmodulePath}")
	else:
		os.system(f"git submodule set-branch --branch {oBranchName} {oSubmodulePath}")

os.system(f"python3 UnityModuleStudyImporter.py '{oProjRoot}' {oProjName} {oBranchName}")
os.system(f"python3 UnityModuleCommonImporter.py '{oProjRoot}' {oProjName} {oBranchName}")
os.system(f"python3 UnityModulePluginImporter.py '{oProjRoot}' {oProjName} {oBranchName}")
