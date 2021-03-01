using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더 - 젠킨스
public static partial class CPlatformBuilder {
	#region 클래스 함수
	//! 젠킨스 빌드를 실행한다
	public static void ExecuteJenkinsBuild(EBuildType a_eBuildType, string a_oPipeline, string a_oProjName, string a_oPlatform, string a_oBuildMode, string a_oBuildVersion, string a_oBuildFunc, string a_oPipelineName, Dictionary<string, string> a_oDataList = null) {
		string oURL = string.Format(CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBuildURLFormat, a_oPipeline);
		string oUserID = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oUserID;
		string oAccessToken = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oAccessToken;
		string oBuildToken = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBuildToken;

		var oStringBuilder = new System.Text.StringBuilder();
		oStringBuilder.AppendFormat(KCEditorDefine.B_BUILD_CMD_FMT_JENKINS, oURL, oUserID, oAccessToken, oBuildToken);
			
		// 매개 변수를 설정한다 {
		string oSrc = string.Format(KCEditorDefine.B_SRC_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oSrcRoot, a_oProjName);
		string oProjPath = string.Format(KCEditorDefine.B_PROJ_P_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oWorkspaceRoot, oSrc, CPlatformOptsSetter.ProjInfoTable.ProjName);
		string oAnalytics = string.Format(KCEditorDefine.B_ANALYTICS_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oSrcRoot);
			
		var oDataList = a_oDataList ?? new Dictionary<string, string>();
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_BRANCH, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBranch);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_SRC, oSrc);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_PROJ_NAME, CPlatformOptsSetter.ProjInfoTable.ProjName);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_PROJ_PATH, oProjPath);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_PLATFORM, a_oPlatform);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_ANALYTICS, oAnalytics);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_BUILD_MODE, a_oBuildMode);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_BUILD_VERSION, a_oBuildVersion);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_BUILD_FUNC, a_oBuildFunc);
		oDataList.ExAddValue(KCEditorDefine.B_KEY_JENKINS_PIPELINE_NAME, a_oPipelineName);
		
		foreach(var stKeyValue in oDataList) {
			oStringBuilder.Append(KCEditorDefine.B_BUILD_PARAMS_TOKEN_JENKINS);
			oStringBuilder.AppendFormat(KCEditorDefine.B_BUILD_DATA_FMT_JENKINS, stKeyValue.Key, stKeyValue.Value);
		}
		// 매개 변수를 설정한다 }

		CEditorFunc.ExecuteCmdLine(oStringBuilder.ToString());
	}

	//! 독립 플랫폼 젠킨스 빌드를 실행한다
	public static void ExecuteStandaloneJenkinsBuild(EBuildType a_eBuildType, EStandaloneType a_eType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName) {
		string oPlatform = CEditorAccess.GetStandaloneName(a_eType);
		string oWndsVersion = CPlatformOptsSetter.ProjInfoTable.WndsProjInfo.m_stBuildVersion.m_oVersion;
		string oBuildVersion = (a_eType == EStandaloneType.WNDS) ? oWndsVersion : CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_stBuildVersion.m_oVersion;

		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_STANDALONE_PIPELINE, KCEditorDefine.B_STANDALONE_BUILD_PROJ_N_JENKINS, oPlatform, a_oBuildMode, oBuildVersion, a_oBuildFunc, a_oPipelineName, null);
	}

	//! iOS 젠킨스 빌드를 실행한다
	public static void ExecuteiOSJenkinsBuild(EBuildType a_eBuildType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName, string a_oProfileID, string a_oIPAExportMethod) {
		var oDataList = new Dictionary<string, string>() {
			[KCEditorDefine.B_KEY_JENKINS_BUNDLE_ID] = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_oAppID,
			[KCEditorDefine.B_KEY_JENKINS_PROFILE_ID] = a_oProfileID,
			[KCEditorDefine.B_KEY_JENKINS_IPA_EXPORT_METHOD] = a_oIPAExportMethod
		};
		
		string oBuildVersion = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_stBuildVersion.m_oVersion;
		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_IOS_PIPELINE, KCEditorDefine.B_IOS_BUILD_PROJ_N_JENKINS, KCDefine.B_PLATFORM_N_IOS, a_oBuildMode, oBuildVersion, a_oBuildFunc, a_oPipelineName, oDataList);
	}

	//! 안드로이드 젠킨스 빌드를 실행한다
	public static void ExecuteAndroidJenkinsBuild(EBuildType a_eBuildType, EAndroidType a_eType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName, string a_oBuildFileExtension) {
		var oDataList = new Dictionary<string, string>() {
			[KCEditorDefine.B_KEY_JENKINS_BUILD_FILE_EXTENSION] = a_oBuildFileExtension
		};

		string oPlatform = CEditorAccess.GetAndroidName(a_eType);
		string oBuildVersion = CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_stBuildVersion.m_oVersion;

		// 원 스토어 일 경우
		if(a_eType == EAndroidType.ONE_STORE) {
			oBuildVersion = CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_stBuildVersion.m_oVersion;
		} 
		// 갤럭시 스토어 일 경우
		else if(a_eType == EAndroidType.GALAXY_STORE) {
			oBuildVersion = CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_stBuildVersion.m_oVersion;
		}

		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_ANDROID_PIPELINE, KCEditorDefine.B_ANDROID_BUILD_PROJ_N_JENKINS, oPlatform, a_oBuildMode, oBuildVersion, a_oBuildFunc, a_oPipelineName, oDataList);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
