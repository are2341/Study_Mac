import os
import sys

oProjName = sys.argv[1]
oBranchName = sys.argv[2]

oSubmoduleInfos = [
	{
		"Name": ".Module.UnityCommon",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommon_client.git"
	},

	{
		"Name": ".Module.UnityCommonDefine",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommondefine_client.git"
	},

	{
		"Name": ".Module.UnityCommonAccess",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonaccess_client.git"
	},

	{
		"Name": ".Module.UnityCommonFactory",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonfactory_client.git"
	},

	{
		"Name": ".Module.UnityCommonExtension",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonextension_client.git"
	},

	{
		"Name": ".Module.UnityCommonFunc",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonfunc_client.git"
	},

	{
		"Name": ".Module.UnityCommonUtility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonutility_client.git"
	},

	{
		"Name": ".Module.UnityCommonExternals",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonexternals_client.git"
	},

	{
		"Name": ".Module.UnityCommonAds",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonads_client.git"
	},

	{
		"Name": ".Module.UnityCommonFlurry",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonflurry_client.git"
	},
	
	{
		"Name": ".Module.UnityCommonFacebook",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonfacebook_client.git"
	},

	{
		"Name": ".Module.UnityCommonFirebase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonfirebase_client.git"
	},

	{
		"Name": ".Module.UnityCommonAppsFlyer",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonappsflyer_client.git"
	},
	
	{
		"Name": ".Module.UnityCommonGameCenter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommongamecenter_client.git"
	},

	{
		"Name": ".Module.UnityCommonPurchase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonpurchase_client.git"
	},

	{
		"Name": ".Module.UnityCommonNoti",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/0300000001.module_unitycommonnoti_client.git"
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

os.system(f"python3 UnityModuleRemoteURLUpdater.py {oProjName}")
