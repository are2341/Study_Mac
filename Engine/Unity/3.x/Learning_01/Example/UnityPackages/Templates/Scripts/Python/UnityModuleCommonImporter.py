import os
import sys

oProjRoot = sys.argv[1]
oProjName = sys.argv[2]
oBranchName = sys.argv[3]

oSubmoduleInfos = [
	{
		"Name": ".UnityModule.Common",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_client.git"
	},

	{
		"Name": ".UnityModule.Common.Define",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_define_client.git"
	},

	{
		"Name": ".UnityModule.Common.Access",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_access_client.git"
	},

	{
		"Name": ".UnityModule.Common.Factory",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_factory_client.git"
	},

	{
		"Name": ".UnityModule.Common.Extension",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_extension_client.git"
	},

	{
		"Name": ".UnityModule.Common.Func",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_func_client.git"
	},

	{
		"Name": ".UnityModule.Common.Utility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_utility_client.git"
	},

	{
		"Name": ".UnityModule.Common.Externals",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_externals_client.git"
	},

	{
		"Name": ".UnityModule.Common.Ads",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_ads_client.git"
	},

	{
		"Name": ".UnityModule.Common.Flurry",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_flurry_client.git"
	},

	{
		"Name": ".UnityModule.Common.Tenjin",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_tenjin_client.git"
	},

	{
		"Name": ".UnityModule.Common.Facebook",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_facebook_client.git"
	},

	{
		"Name": ".UnityModule.Common.Firebase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_firebase_client.git"
	},
	
	{
		"Name": ".UnityModule.Common.Singular",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_singular_client.git"
	},

	{
		"Name": ".UnityModule.Common.GameCenter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_gamecenter_client.git"
	},

	{
		"Name": ".UnityModule.Common.Purchase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_purchase_client.git"
	},

	{
		"Name": ".UnityModule.Common.Noti",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/unitymodule_common_noti_client.git"
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
