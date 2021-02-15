import os
import sys

oProjRoot = sys.argv[1]
oProjName = sys.argv[2]
oBranchName = sys.argv[3]

oSubmoduleInfos = [
	{
		"Name": "PluginProjects",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_plugin_client.git"
	},

	{
		"Name": "UnityPackages",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_package_client.git"
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
