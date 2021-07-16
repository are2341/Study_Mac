using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더 - 독립 플랫폼
public static partial class CPlatformBuilder {
	#region 클래스 함수
	//! 맥을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Mac/Debug", false, 11)]
	public static void BuildMacDebug() {
		CPlatformBuilder.BuildStandaloneDebug(EStandaloneType.MAC);
	}

	//! 윈도우즈를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Windows/Debug", false, 11)]
	public static void BuildWndsDebug() {
		CPlatformBuilder.BuildStandaloneDebug(EStandaloneType.WNDS);
	}

	//! 맥을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Mac/Debug with AutoPlay", false, 11)]
	public static void BuildMacWithAutoPlayDebug() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayDebug(EStandaloneType.MAC);
	}

	//! 윈도우즈를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Windows/Debug with AutoPlay", false, 11)]
	public static void BuildWndsWithAutoPlayDebug() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayDebug(EStandaloneType.WNDS);
	}

	//! 맥을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Mac/Release")]
	public static void BuildMacRelease() {
		CPlatformBuilder.BuildStandaloneRelease(EStandaloneType.MAC);
	}

	//! 윈도우즈를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Windows/Release")]
	public static void BuildWndsRelease() {
		CPlatformBuilder.BuildStandaloneRelease(EStandaloneType.WNDS);
	}

	//! 맥을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Mac/Release with AutoPlay")]
	public static void BuildMacWithAutoPlayRelease() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayRelease(EStandaloneType.MAC);
	}

	//! 윈도우즈를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Windows/Release with AutoPlay")]
	public static void BuildWndsWithAutoPlayRelease() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayRelease(EStandaloneType.WNDS);
	}

	//! 맥을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Mac/Release with AutoPlay (Disable FPS)")]
	public static void BuildMacWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayDisableFPSRelease(EStandaloneType.MAC);
	}

	//! 윈도우즈를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Standalone/Windows/Release with AutoPlay (Disable FPS)")]
	public static void BuildWndsWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.BuildStandaloneWithAutoPlayDisableFPSRelease(EStandaloneType.WNDS);
	}

	//! 맥을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Standalone/Mac/Debug", false, 11)]
	public static void RemoteBuildMacDebug() {
		CPlatformBuilder.RemoteBuildStandaloneDebug(EStandaloneType.MAC);
	}

	//! 윈도우즈를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Standalone/Windows/Debug", false, 11)]
	public static void RemoteBuildWndsDebug() {
		CPlatformBuilder.RemoteBuildStandaloneDebug(EStandaloneType.WNDS);
	}

	//! 맥을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Standalone/Mac/Release")]
	public static void RemoteBuildMacRelease() {
		CPlatformBuilder.RemoteBuildStandaloneRelease(EStandaloneType.MAC);
	}

	//! 윈도우즈를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Standalone/Windows/Release")]
	public static void RemoteBuildWndsRelease() {
		CPlatformBuilder.RemoteBuildStandaloneRelease(EStandaloneType.WNDS);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandaloneDebug(EStandaloneType a_eType) {
		CPlatformBuilder.BuildType = EBuildType.DEBUG;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Standalone, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Standalone, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);

		// 빌드 옵션을 설정한다
		var oPlayerOpts = new BuildPlayerOptions();
		oPlayerOpts.options = BuildOptions.Development;
		
		CPlatformBuilder.BuildStandalone(oPlayerOpts, a_eType);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandaloneWithAutoPlayDebug(EStandaloneType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildStandaloneDebug(a_eType);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandaloneRelease(EStandaloneType a_eType) {
		CPlatformBuilder.BuildType = EBuildType.RELEASE;

		// 전처리기 심볼을 추가한다 {
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Standalone, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);

		// 자동 실행 모드 일 경우
		if(CPlatformBuilder.IsAutoPlay) {
			CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Standalone, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);
		}
		// 전처리기 심볼을 추가한다 }

		CPlatformBuilder.BuildStandalone(new BuildPlayerOptions(), a_eType);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandaloneWithAutoPlayRelease(EStandaloneType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Standalone, KCEditorDefine.DS_DEFINE_S_FPS_ENABLE);

		CPlatformBuilder.BuildStandaloneRelease(a_eType);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandaloneWithAutoPlayDisableFPSRelease(EStandaloneType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildStandaloneRelease(a_eType);
	}

	//! 독립 플랫폼을 빌드한다
	private static void BuildStandalone(BuildPlayerOptions a_oPlayerOpts, EStandaloneType a_eType) {
		CPlatformBuilder.StandaloneType = a_eType;
		CPlatformBuilder.IsEnableEditorScene = true;

		// 빌드 옵션을 설정한다 {
		string oPlatform = CEditorAccess.GetStandaloneName(a_eType);
		a_oPlayerOpts.targetGroup = BuildTargetGroup.Standalone;

		// 윈도우즈 일 경우
		if(a_eType == EStandaloneType.WNDS) {
			a_oPlayerOpts.target = BuildTarget.StandaloneWindows;
			a_oPlayerOpts.locationPathName = KCEditorDefine.B_BUILD_P_WNDS;

			CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_WNDS);
		} else {
			a_oPlayerOpts.target = BuildTarget.StandaloneOSX;
			a_oPlayerOpts.locationPathName = KCEditorDefine.B_BUILD_P_MAC;
			
			CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_MAC);
		}

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			var stWndsProjInfo = CPlatformOptsSetter.ProjInfoTable.WndsProjInfo;
			var stProjInfo = (a_eType == EStandaloneType.WNDS) ? stWndsProjInfo : CPlatformOptsSetter.ProjInfoTable.MacProjInfo;

			PlayerSettings.bundleVersion = stProjInfo.m_stBuildVer.m_oVer;
		}
		// 빌드 옵션을 설정한다 }

		// 빌드 디렉토리를 생성한다
		string oBuildPath = string.Format(KCEditorDefine.B_ABS_BUILD_P_FMT_STANDALONE, oPlatform);
		CFactory.CreateDir(oBuildPath);

		// 플랫폼을 빌드한다
		CPlatformBuilder.BuildPlatform(a_oPlayerOpts);
	}

	//! 독립 플랫폼을 원격 빌드한다
	private static void RemoteBuildStandaloneDebug(EStandaloneType a_eType) {
		CPlatformBuilder.ExecuteStandaloneJenkinsBuild(EBuildType.DEBUG, a_eType, KCDefine.B_BUILD_MODE_DEBUG, KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS, KCEditorDefine.B_STANDALONE_DEBUG_PIPELINE_N_JENKINS);
	}

	//! 독립 플랫폼을 원격 빌드한다
	private static void RemoteBuildStandaloneRelease(EStandaloneType a_eType) {
		CPlatformBuilder.ExecuteStandaloneJenkinsBuild(EBuildType.RELEASE, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS, KCEditorDefine.B_STANDALONE_RELEASE_PIPELINE_N_JENKINS);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
