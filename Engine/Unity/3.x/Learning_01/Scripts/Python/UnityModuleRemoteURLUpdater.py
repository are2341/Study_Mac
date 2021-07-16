import os
import sys

oProjName = sys.argv[1]

oSubmoduleInfos = [
	{
		"Name": ".Module.UnityStudy",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.module_unitystudy_client.git"
	},

	{
		"Name": ".Module.UnityStudyDefine",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.module_unitystudydefine_client.git"
	},

	{
		"Name": ".Module.UnityStudyUtility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.module_unitystudyutility_client.git"
	},

	{
		"Name": ".Module.UnityStudyImporter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/dante.distribution.individual/000001.module_unitystudyimporter_client.git"
	},

	{
		"Name": ".Module.UnityCommon",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommon_client.git"
	},

	{
		"Name": ".Module.UnityCommonDefine",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommondefine_client.git"
	},

	{
		"Name": ".Module.UnityCommonAccess",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonaccess_client.git"
	},

	{
		"Name": ".Module.UnityCommonFactory",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonfactory_client.git"
	},

	{
		"Name": ".Module.UnityCommonExtension",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonextension_client.git"
	},

	{
		"Name": ".Module.UnityCommonFunc",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonfunc_client.git"
	},

	{
		"Name": ".Module.UnityCommonUtility",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonutility_client.git"
	},
	
	{
		"Name": ".Module.UnityCommonExternals",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonexternals_client.git"
	},

	{
		"Name": ".Module.UnityCommonAds",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonads_client.git"
	},

	{
		"Name": ".Module.UnityCommonFlurry",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonflurry_client.git"
	},
	
	{
		"Name": ".Module.UnityCommonFacebook",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonfacebook_client.git"
	},

	{
		"Name": ".Module.UnityCommonFirebase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonfirebase_client.git"
	},

	{
		"Name": ".Module.UnityCommonGameAnalytics",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommongameanalytics_client.git"
	},
	
	{
		"Name": ".Module.UnityCommonSingular",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonsingular_client.git"
	},

	{
		"Name": ".Module.UnityCommonGameCenter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommongamecenter_client.git"
	},

	{
		"Name": ".Module.UnityCommonPurchase",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonpurchase_client.git"
	},

	{
		"Name": ".Module.UnityCommonNoti",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonnoti_client.git"
	},

	{
		"Name": ".Module.UnityCommonImporter",
		"Path": f"{oProjName}/Packages",
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitycommonimporter_client.git"
	},

	{
		"Name": "NativePlugins",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unityplugin_client.git"
	},

	{
		"Name": "UnityPackages",
		"Path": oProjName,
		"URL": "https://gitlab.com/9tapmodule.repository/000001.module_unitypackage_client.git"
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