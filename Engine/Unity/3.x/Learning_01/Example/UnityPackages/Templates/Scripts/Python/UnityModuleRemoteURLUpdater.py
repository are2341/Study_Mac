import os
import sys

oProjName = sys.argv[1]

oSubmoduleInfos = [
	{
		"Name": ".UnityModule.Study",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.unitymodule_study_client.git"
	},

	{
		"Name": ".UnityModule.Study.Define",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.unitymodule_study_define_client.git"
	},

	{
		"Name": ".UnityModule.Study.Utility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.unitymodule_study_utility_client.git"
	},

	{
		"Name": ".UnityModule.Study.Importer",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.unitymodule_study_importer_client.git"
	},

	{
		"Name": ".UnityModule.Common",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_client.git"
	},

	{
		"Name": ".UnityModule.Common.Define",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_define_client.git"
	},

	{
		"Name": ".UnityModule.Common.Access",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_access_client.git"
	},

	{
		"Name": ".UnityModule.Common.Factory",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_factory_client.git"
	},

	{
		"Name": ".UnityModule.Common.Extension",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_extension_client.git"
	},

	{
		"Name": ".UnityModule.Common.Func",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_func_client.git"
	},

	{
		"Name": ".UnityModule.Common.Utility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_utility_client.git"
	},
	
	{
		"Name": ".UnityModule.Common.Externals",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_externals_client.git"
	},

	{
		"Name": ".UnityModule.Common.Ads",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_ads_client.git"
	},

	{
		"Name": ".UnityModule.Common.Flurry",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_flurry_client.git"
	},

	{
		"Name": ".UnityModule.Common.Tenjin",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_tenjin_client.git"
	},

	{
		"Name": ".UnityModule.Common.Facebook",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_facebook_client.git"
	},

	{
		"Name": ".UnityModule.Common.Firebase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_firebase_client.git"
	},
	
	{
		"Name": ".UnityModule.Common.Singular",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_singular_client.git"
	},

	{
		"Name": ".UnityModule.Common.GameCenter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_gamecenter_client.git"
	},

	{
		"Name": ".UnityModule.Common.Purchase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_purchase_client.git"
	},

	{
		"Name": ".UnityModule.Common.Noti",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_noti_client.git"
	},

	{
		"Name": ".UnityModule.Common.Importer",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_common_importer_client.git"
	},

	{
		"Name": "PluginProjects",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_plugin_client.git"
	},

	{
		"Name": "UnityPackages",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/000001.unitymodule_package_client.git"
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
	oCurPath = os.getcwd()
	
	# 서브 모듈이 존재 할 경우
	if os.path.exists(oPath):
		os.chdir(oPath)

		os.system(f"git remote set-url origin {oSubmoduleInfo['URL']}")
		os.chdir(oCurPath)
