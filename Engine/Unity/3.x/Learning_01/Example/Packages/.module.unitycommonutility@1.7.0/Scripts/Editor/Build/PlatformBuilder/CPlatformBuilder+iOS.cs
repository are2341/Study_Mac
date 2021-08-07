using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더 - iOS
public static partial class CPlatformBuilder {
	#region 클래스 함수
	//! iOS 를 빌드한다
	public static void BuildiOSAdhocUpload() {
		CPlatformBuilder.BuildiOSAdhoc();
	}

	//! iOS 를 빌드한다
	public static void BuildiOSStoreUpload() {
		CPlatformBuilder.BuildiOSStore();
	}
	
	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Debug", false, 11)]
	public static void BuildiOSDebug() {
		CPlatformBuilder.BuildType = EBuildType.DEBUG;
		EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);

		// 프로비저닝 파일 정보를 설정한다
		PlayerSettings.iOS.iOSManualProvisioningProfileID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oDevProfileID;
		PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Development;

		// 빌드 옵션을 설정한다
		var oPlayerOpts = new BuildPlayerOptions();
		oPlayerOpts.options = BuildOptions.Development;
		
		CPlatformBuilder.BuildiOS(oPlayerOpts);
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Debug with AutoPlay", false, 11)]
	public static void BuildiOSWithAutoPlayDebug() {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildiOSDebug();
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Release", false, 22)]
	public static void BuildiOSRelease() {
		CPlatformBuilder.BuildType = EBuildType.RELEASE;
		EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Release;

		// 전처리기 심볼을 추가한다 {
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);

		// 자동 실행 모드 일 경우
		if(CPlatformBuilder.IsAutoPlay) {
			CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);
		}
		// 전처리기 심볼을 추가한다 }

		// 프로비저닝 파일 정보를 설정한다
		PlayerSettings.iOS.iOSManualProvisioningProfileID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oDevProfileID;
		PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Development;

		CPlatformBuilder.BuildiOS(new BuildPlayerOptions());
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Release with AutoPlay", false, 22)]
	public static void BuildiOSWithAutoPlayRelease() {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_FPS_ENABLE);

		CPlatformBuilder.BuildiOSRelease();
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Release with AutoPlay (Disable FPS)", false, 22)]
	public static void BuildiOSWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildiOSRelease();
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Distribution (Adhoc)", false, 33)]
	public static void BuildiOSAdhoc() {
		CPlatformBuilder.BuildType = EBuildType.ADHOC;
		EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Release;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_ADHOC_BUILD);

		// 프로비저닝 파일 정보를 설정한다
		PlayerSettings.iOS.iOSManualProvisioningProfileID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oAdhocProfileID;
		PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;

		CPlatformBuilder.BuildiOS(new BuildPlayerOptions());
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Distribution (Adhoc Robo Test)", false, 33)]
	public static void BuildiOSWithRoboTestAdhoc() {
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_ROBO_TEST_ENABLE);
		CPlatformBuilder.BuildiOSAdhoc();
	}

	//! iOS 를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/iOS/Distribution (Store)")]
	public static void BuildiOSStore() {
		CPlatformBuilder.BuildType = EBuildType.STORE;
		EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Release;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.iOS, KCEditorDefine.DS_DEFINE_S_STORE_BUILD);

		// 프로비저닝 파일 정보를 설정한다
		PlayerSettings.iOS.iOSManualProvisioningProfileID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oStoreProfileID;
		PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;

		CPlatformBuilder.BuildiOS(new BuildPlayerOptions());
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Debug", false, 11)]
	public static void RemoteBuildiOSDebug() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.DEBUG, KCDefine.B_BUILD_MODE_DEBUG, KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_DEBUG_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oDevProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_DEV);
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Release", false, 22)]
	public static void RemoteBuildiOSRelease() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.RELEASE, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_RELEASE_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oDevProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_DEV);
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Distribution (Adhoc)", false, 33)]
	public static void RemoteBuildiOSAdhoc() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.ADHOC, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_ADHOC_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_ADHOC_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oAdhocProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_ADHOC);
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Distribution (Adhoc Upload)", false, 33)]
	public static void RemoteBuildiOSAdhocUpload() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.ADHOC, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_ADHOC_UPLOAD_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_ADHOC_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oAdhocProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_ADHOC);
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Distribution (Store)")]
	public static void RemoteBuildiOSStore() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.STORE, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_STORE_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_STORE_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oStoreProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_STORE);
	}

	//! iOS 를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/iOS/Distribution (Store Upload)")]
	public static void RemoteBuildiOSStoreUpload() {
		CPlatformBuilder.ExecuteiOSJenkinsBuild(EBuildType.STORE, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_STORE_UPLOAD_BUILD_FUNC_JENKINS, KCEditorDefine.B_IOS_STORE_PIPELINE_N_JENKINS, CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oStoreProfileID, KCEditorDefine.B_IPA_EXPORT_METHOD_IOS_STORE);
	}

	//! iOS 를 빌드한다
	private static void BuildiOS(BuildPlayerOptions a_oPlayerOpts) {
		CPlatformBuilder.IsEnableEditorScene = false;
		
		// 플러그인 파일을 복사한다
		if(!Application.isBatchMode) {
			CFunc.CopyDir(KCEditorDefine.B_SRC_PLUGIN_P_IOS, KCEditorDefine.B_DEST_PLUGIN_P_IOS, false);
		}

		// 빌드 옵션을 설정한다 {
		a_oPlayerOpts.target = BuildTarget.iOS;
		a_oPlayerOpts.targetGroup = BuildTargetGroup.iOS;
		a_oPlayerOpts.locationPathName = KCEditorDefine.B_BUILD_P_IOS;

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			PlayerSettings.bundleVersion = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_stBuildVer.m_oVer;
		}
		// 빌드 옵션을 설정한다 }

		// 빌드 디렉토리를 생성한다
		CFactory.CreateDir(KCEditorDefine.B_ABS_BUILD_P_IOS);

		// 플랫폼을 빌드한다
		CPlatformBuilder.BuildPlatform(a_oPlayerOpts);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
