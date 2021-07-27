using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더 - 젠킨스
public static partial class CPlatformBuilder {
	#region 클래스 함수
	//! 젠킨스 빌드를 실행한다
	public static void ExecuteJenkinsBuild(EBuildType a_eBuildType, string a_oPipeline, string a_oProjName, string a_oPlatform, string a_oBuildMode, string a_oBuildVer, string a_oBuildFunc, string a_oPipelineName, Dictionary<string, string> a_oDataDict = null) {
		string oURL = string.Format(CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBuildURLFmt, a_oPipeline);
		string oUserID = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oUserID;
		string oAccessToken = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oAccessToken;
		string oBuildToken = CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBuildToken;

		var oStrBuilder = new System.Text.StringBuilder();
		oStrBuilder.AppendFormat(KCEditorDefine.B_BUILD_CMD_FMT_JENKINS, oURL, oUserID, oAccessToken, oBuildToken);
			
		// 매개 변수를 설정한다 {
		string oBranch = string.Format(KCEditorDefine.B_BRANCH_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oBranch);
		string oSrc = string.Format(KCEditorDefine.B_SRC_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oSrc, a_oProjName);
		string oProjPath = string.Format(KCEditorDefine.B_PROJ_P_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oWorkspace, oSrc, CPlatformOptsSetter.ProjInfoTable.ProjName);
		string oAnalytics = string.Format(KCEditorDefine.B_ANALYTICS_FMT_JENKINS, CPlatformOptsSetter.BuildInfoTable.JenkinsInfo.m_oSrc);
		
		var oDataDict = a_oDataDict ?? new Dictionary<string, string>();
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_ENGINE_VER, Application.unityVersion);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_BRANCH, oBranch);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_SRC, oSrc);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_PROJ_NAME, CPlatformOptsSetter.ProjInfoTable.ProjName);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_PROJ_PATH, oProjPath);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_PLATFORM, a_oPlatform);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_ANALYTICS, oAnalytics);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_BUILD_MODE, a_oBuildMode);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_BUILD_VER, a_oBuildVer);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_BUILD_FUNC, a_oBuildFunc);
		oDataDict.ExAddVal(KCEditorDefine.B_KEY_JENKINS_PIPELINE_NAME, a_oPipelineName);
		
		foreach(var stKeyVal in oDataDict) {
			oStrBuilder.Append(KCEditorDefine.B_BUILD_PARAMS_TOKEN_JENKINS);
			oStrBuilder.AppendFormat(KCEditorDefine.B_BUILD_DATA_FMT_JENKINS, stKeyVal.Key, stKeyVal.Value);
		}
		// 매개 변수를 설정한다 }
		
		CEditorFunc.ExecuteCmdLine(oStrBuilder.ToString());
	}

	//! 독립 플랫폼 젠킨스 빌드를 실행한다
	public static void ExecuteStandaloneJenkinsBuild(EBuildType a_eBuildType, EStandaloneType a_eType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName) {
		int nNum = (a_eType == EStandaloneType.WNDS) ? CPlatformOptsSetter.ProjInfoTable.WndsProjInfo.m_stBuildVer.m_nNum : CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_stBuildVer.m_nNum;
		string oVer = (a_eType == EStandaloneType.WNDS) ? CPlatformOptsSetter.ProjInfoTable.WndsProjInfo.m_stBuildVer.m_oVer : CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_stBuildVer.m_oVer;

		string oPlatform = CEditorAccess.GetStandaloneName(a_eType);
		string oBuildVer = string.Format(KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE, oVer, nNum);

		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_STANDALONE_PIPELINE, KCEditorDefine.B_STANDALONE_BUILD_PROJ_N_JENKINS, oPlatform, a_oBuildMode, oBuildVer, a_oBuildFunc, a_oPipelineName, null);
	}

	//! iOS 젠킨스 빌드를 실행한다
	public static void ExecuteiOSJenkinsBuild(EBuildType a_eBuildType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName, string a_oProfileID, string a_oIPAExportMethod) {
		var oDataDict = new Dictionary<string, string>() {
			[KCEditorDefine.B_KEY_JENKINS_BUNDLE_ID] = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_oAppID,
			[KCEditorDefine.B_KEY_JENKINS_PROFILE_ID] = a_oProfileID,
			[KCEditorDefine.B_KEY_JENKINS_IPA_EXPORT_METHOD] = a_oIPAExportMethod
		};

		int nNum = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_stBuildVer.m_nNum;
		string oVer = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_stBuildVer.m_oVer;
		string oBuildVer = string.Format(KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE, oVer, nNum);

		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_IOS_PIPELINE, KCEditorDefine.B_IOS_BUILD_PROJ_N_JENKINS, KCDefine.B_PLATFORM_N_IOS, a_oBuildMode, oBuildVer, a_oBuildFunc, a_oPipelineName, oDataDict);
	}

	//! 안드로이드 젠킨스 빌드를 실행한다
	public static void ExecuteAndroidJenkinsBuild(EBuildType a_eBuildType, EAndroidType a_eType, string a_oBuildMode, string a_oBuildFunc, string a_oPipelineName, string a_oBuildFileExtension) {
		var oDataDict = new Dictionary<string, string>() {
			[KCEditorDefine.B_KEY_JENKINS_BUILD_FILE_EXTENSION] = a_oBuildFileExtension
		};
		
		int nNum = CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_stBuildVer.m_nNum;
		string oVer = CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_stBuildVer.m_oVer;

		// 원 스토어 일 경우
		if(a_eType == EAndroidType.ONE_STORE) {
			nNum = CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_stBuildVer.m_nNum;
			oVer = CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_stBuildVer.m_oVer;
		} 
		// 갤럭시 스토어 일 경우
		else if(a_eType == EAndroidType.GALAXY_STORE) {
			nNum = CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_stBuildVer.m_nNum;
			oVer = CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_stBuildVer.m_oVer;
		}

		string oPlatform = CEditorAccess.GetAndroidName(a_eType);
		string oBuildVer = string.Format(KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE, oVer, nNum);
		
		CPlatformBuilder.ExecuteJenkinsBuild(a_eBuildType, KCEditorDefine.B_JENKINS_ANDROID_PIPELINE, KCEditorDefine.B_ANDROID_BUILD_PROJ_N_JENKINS, oPlatform, a_oBuildMode, oBuildVer, a_oBuildFunc, a_oPipelineName, oDataDict);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
