using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더 - 안드로이드
public static partial class CPlatformBuilder {
	#region 클래스 함수
	//! 구글을 빌드한다
	public static void BuildGoogleAdhocUpload() {
		CPlatformBuilder.BuildGoogleAdhoc();
	}

	//! 원 스토어를 빌드한다
	public static void BuildOneStoreAdhocUpload() {
		CPlatformBuilder.BuildOneStoreAdhoc();
	}
	
	//! 갤럭시 스토어를 빌드한다
	public static void BuildGalaxyStoreAdhocUpload() {
		CPlatformBuilder.BuildGalaxyStoreAdhoc();
	}

	//! 구글을 빌드한다
	public static void BuildGoogleStoreUpload() {
		CPlatformBuilder.BuildGoogleStore();
	}

	//! 원 스토어를 빌드한다
	public static void BuildOneStoreStoreUpload() {
		CPlatformBuilder.BuildOneStoreStore();
	}

	//! 갤럭시 스토어를 빌드한다
	public static void BuildGalaxyStoreStoreUpload() {
		CPlatformBuilder.BuildGalaxyStoreStore();
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Debug", false, 11)]
	public static void BuildGoogleDebug() {
		CPlatformBuilder.BuildAndroidDebug(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Debug", false, 11)]
	public static void BuildOneStoreDebug() {
		CPlatformBuilder.BuildAndroidDebug(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Debug", false, 11)]
	public static void BuildGalaxyStoreDebug() {
		CPlatformBuilder.BuildAndroidDebug(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Debug with AutoPlay", false, 11)]
	public static void BuildGoogleWithAutoPlayDebug() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDebug(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Debug with AutoPlay", false, 11)]
	public static void BuildOneStoreWithAutoPlayDebug() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDebug(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Debug with AutoPlay", false, 11)]
	public static void BuildGalaxyStoreWithAutoPlayDebug() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDebug(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Release", false, 22)]
	public static void BuildGoogleRelease() {
		CPlatformBuilder.BuildAndroidRelease(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Release", false, 22)]
	public static void BuildOneStoreRelease() {
		CPlatformBuilder.BuildAndroidRelease(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Release", false, 22)]
	public static void BuildGalaxyStoreRelease() {
		CPlatformBuilder.BuildAndroidRelease(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Release with AutoPlay", false, 22)]
	public static void BuildGoogleWithAutoPlayRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayRelease(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Release with AutoPlay", false, 22)]
	public static void BuildOneStoreWithAutoPlayRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayRelease(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Release with AutoPlay", false, 22)]
	public static void BuildGalaxyStoreWithAutoPlayRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayRelease(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Release with AutoPlay (Disable FPS)", false, 22)]
	public static void BuildGoogleWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDisableFPSRelease(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Release with AutoPlay (Disable FPS)", false, 22)]
	public static void BuildOneStoreWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDisableFPSRelease(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Release with AutoPlay (Disable FPS)", false, 22)]
	public static void BuildGalaxyStoreWithAutoPlayDisableFPSRelease() {
		CPlatformBuilder.BuildAndroidWithAutoPlayDisableFPSRelease(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Distribution (Adhoc)", false, 33)]
	public static void BuildGoogleAdhoc() {
		CPlatformBuilder.BuildAndroidAdhoc(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Distribution (Adhoc)", false, 33)]
	public static void BuildOneStoreAdhoc() {
		CPlatformBuilder.BuildAndroidAdhoc(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Distribution (Adhoc)", false, 33)]
	public static void BuildGalaxyStoreAdhoc() {
		CPlatformBuilder.BuildAndroidAdhoc(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Distribution (Adhoc Robo Test)", false, 33)]
	public static void BuildGoogleWithRoboTestAdhoc() {
		CPlatformBuilder.BuildAndroidWithRoboTestAdhoc(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Distribution (Adhoc Robo Test)", false, 33)]
	public static void BuildOneStoreWithRoboTestAdhoc() {
		CPlatformBuilder.BuildAndroidWithRoboTestAdhoc(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Distribution (Adhoc Robo Test)", false, 33)]
	public static void BuildGalaxyStoreWithRoboTestAdhoc() {
		CPlatformBuilder.BuildAndroidWithRoboTestAdhoc(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Distribution (AAB)")]
	public static void BuildGoogleStore() {
		CPlatformBuilder.BuildAndroidStore(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Distribution (AAB)")]
	public static void BuildOneStoreStore() {
		CPlatformBuilder.BuildAndroidStore(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Distribution (AAB)")]
	public static void BuildGalaxyStoreStore() {
		CPlatformBuilder.BuildAndroidStore(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/Google/Distribution (APK)")]
	public static void BuildGoogleStoreTest() {
		CPlatformBuilder.BuildAndroidStoreTest(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/OneStore/Distribution (APK)")]
	public static void BuildOneStoreStoreTest() {
		CPlatformBuilder.BuildAndroidStoreTest(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 빌드한다
	[MenuItem("Tools/Utility/Build/Local/Android/GalaxyStore/Distribution (APK)")]
	public static void BuildGalaxyStoreStoreTest() {
		CPlatformBuilder.BuildAndroidStoreTest(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Debug", false, 11)]
	public static void RemoteBuildGoogleDebug() {
		CPlatformBuilder.RemoteBuildAndroidDebug(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Debug", false, 11)]
	public static void RemoteBuildOneStoreDebug() {
		CPlatformBuilder.RemoteBuildAndroidDebug(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Debug", false, 11)]
	public static void RemoteBuildGalaxyStoreDebug() {
		CPlatformBuilder.RemoteBuildAndroidDebug(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Release", false, 22)]
	public static void RemoteBuildGoogleRelease() {
		CPlatformBuilder.RemoteBuildAndroidRelease(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Release", false, 22)]
	public static void RemoteBuildOneStoreRelease() {
		CPlatformBuilder.RemoteBuildAndroidRelease(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Release", false, 22)]
	public static void RemoteBuildGalaxyStoreRelease() {
		CPlatformBuilder.RemoteBuildAndroidRelease(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Distribution (Adhoc)", false, 33)]
	public static void RemoteBuildGoogleAdhoc() {
		CPlatformBuilder.RemoteBuildAndroidAdhoc(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Distribution (Adhoc)", false, 33)]
	public static void RemoteBuildOneStoreAdhoc() {
		CPlatformBuilder.RemoteBuildAndroidAdhoc(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Distribution (Adhoc)", false, 33)]
	public static void RemoteBuildGalaxyStoreAdhoc() {
		CPlatformBuilder.RemoteBuildAndroidAdhoc(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Distribution (Adhoc Upload)", false, 33)]
	public static void RemoteBuildGoogleAdhocUpload() {
		CPlatformBuilder.RemoteBuildAndroidAdhocUpload(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Distribution (Adhoc Upload)", false, 33)]
	public static void RemoteBuildOneStoreAdhocUpload() {
		CPlatformBuilder.RemoteBuildAndroidAdhocUpload(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Distribution (Adhoc Upload)", false, 33)]
	public static void RemoteBuildGalaxyStoreAdhocUpload() {
		CPlatformBuilder.RemoteBuildAndroidAdhocUpload(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Distribution (AAB)")]
	public static void RemoteBuildGoogleStore() {
		CPlatformBuilder.RemoteBuildAndroidStore(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Distribution (AAB)")]
	public static void RemoteBuildOneStoreStore() {
		CPlatformBuilder.RemoteBuildAndroidStore(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Distribution (AAB)")]
	public static void RemoteBuildGalaxyStoreStore() {
		CPlatformBuilder.RemoteBuildAndroidStore(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Distribution (APK)")]
	public static void RemoteBuildGoogleStoreTest() {
		CPlatformBuilder.RemoteBuildAndroidStoreTest(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Distribution (APK)")]
	public static void RemoteBuildOneStoreStoreTest() {
		CPlatformBuilder.RemoteBuildAndroidStoreTest(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Distribution (APK)")]
	public static void RemoteBuildGalaxyStoreStoreTest() {
		CPlatformBuilder.RemoteBuildAndroidStoreTest(EAndroidType.GALAXY_STORE);
	}

	//! 구글을 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/Google/Distribution (Store Upload)")]
	public static void RemoteBuildGoogleStoreUpload() {
		CPlatformBuilder.RemoteBuildAndroidStoreUpload(EAndroidType.GOOGLE);
	}

	//! 원 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/OneStore/Distribution (Store Upload)")]
	public static void RemoteBuildOneStoreStoreUpload() {
		CPlatformBuilder.RemoteBuildAndroidStoreUpload(EAndroidType.ONE_STORE);
	}

	//! 갤럭시 스토어를 원격 빌드한다
	[MenuItem("Tools/Utility/Build/Remote (Jenkins)/Android/GalaxyStore/Distribution (Store Upload)")]
	public static void RemoteBuildGalaxyStoreStoreUpload() {
		CPlatformBuilder.RemoteBuildAndroidStoreUpload(EAndroidType.GALAXY_STORE);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidDebug(EAndroidType a_eType) {
		CPlatformBuilder.BuildType = EBuildType.DEBUG;
		EditorUserBuildSettings.buildAppBundle = false;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);

		// 빌드 옵션을 설정한다
		var oPlayerOpts = new BuildPlayerOptions();
		oPlayerOpts.options = BuildOptions.Development;

		CPlatformBuilder.BuildAndroid(oPlayerOpts, a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidWithAutoPlayDebug(EAndroidType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildAndroidDebug(a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidRelease(EAndroidType a_eType) {
		CPlatformBuilder.BuildType = EBuildType.RELEASE;
		EditorUserBuildSettings.buildAppBundle = false;

		// 전처리기 심볼을 추가한다 {
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);

		// 자동 실행 모드 일 경우
		if(CPlatformBuilder.IsAutoPlay) {
			CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);
		}
		// 전처리기 심볼을 추가한다 }

		CPlatformBuilder.BuildAndroid(new BuildPlayerOptions(), a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidWithAutoPlayRelease(EAndroidType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_FPS_ENABLE);

		CPlatformBuilder.BuildAndroidRelease(a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidWithAutoPlayDisableFPSRelease(EAndroidType a_eType) {
		CPlatformBuilder.IsAutoPlay = true;
		CPlatformBuilder.BuildAndroidRelease(a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidAdhoc(EAndroidType a_eType) {
		CPlatformBuilder.BuildType = EBuildType.ADHOC;
		EditorUserBuildSettings.buildAppBundle = false;

		// 전처리기 심볼을 추가한다
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_ADHOC_BUILD);

		CPlatformBuilder.BuildAndroid(new BuildPlayerOptions(), a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidWithRoboTestAdhoc(EAndroidType a_eType) {
		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_ROBO_TEST_ENABLE);
		CPlatformBuilder.BuildAndroidAdhoc(a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidAdhocUpload(EAndroidType a_eType) {
		CPlatformBuilder.BuildAndroidAdhoc(a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidStore(EAndroidType a_eType) {
		CPlatformBuilder.BuildAndroidStore(a_eType, true);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroidStoreTest(EAndroidType a_eType) {
		CPlatformBuilder.BuildAndroidStore(a_eType, false);
	}
	
	//! 안드로이드를 빌드한다
	private static void BuildAndroidStore(EAndroidType a_eType, bool a_bIsBuildAppBundle) {
		CPlatformBuilder.BuildType = EBuildType.STORE;
		EditorUserBuildSettings.buildAppBundle = a_bIsBuildAppBundle;

		CPlatformOptsSetter.AddDefineSymbol(BuildTargetGroup.Android, KCEditorDefine.DS_DEFINE_S_STORE_BUILD);
		CPlatformBuilder.BuildAndroid(new BuildPlayerOptions(), a_eType);
	}

	//! 안드로이드를 빌드한다
	private static void BuildAndroid(BuildPlayerOptions a_oPlayerOpts, EAndroidType a_eType) {
		CPlatformBuilder.AndroidType = a_eType;
		CPlatformBuilder.IsEnableEditorScene = false;

		// 플러그인 파일을 복사한다
		if(!Application.isBatchMode) {
			CFunc.CopyFile(KCEditorDefine.B_SRC_PLUGIN_P_ANDROID, KCEditorDefine.B_DEST_PLUGIN_P_ANDROID, false);
		}

		// 빌드 옵션을 설정한다 {
		string oPlatform = CEditorAccess.GetAndroidName(a_eType);
		string oFileName = string.Format(KCEditorDefine.B_BUILD_FILE_N_FMT_ANDROID, oPlatform);

		string oAABExtension = KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_AAB;
		string oBuildFileExtension = EditorUserBuildSettings.buildAppBundle ? oAABExtension : KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK;

		a_oPlayerOpts.target = BuildTarget.Android;
		a_oPlayerOpts.targetGroup = BuildTargetGroup.Android;
		a_oPlayerOpts.locationPathName = string.Format(KCEditorDefine.B_BUILD_P_FMT_ANDROID, oPlatform, oFileName, oBuildFileExtension);

		// 원 스토어 일 경우
		if(a_eType == EAndroidType.ONE_STORE) {
			CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_ONE_STORE);
		}
		// 갤럭시 스토어 일 경우
		else if(a_eType == EAndroidType.GALAXY_STORE) {
			CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_GALAXY_STORE);
		} else {
			CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_GOOGLE);
		}

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			var stOneStoreProjInfo = CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo;
			var stGalaxyStoreProjInfo = CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo;

			var stProjInfo = CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo;
			stProjInfo = (a_eType == EAndroidType.ONE_STORE) ? stOneStoreProjInfo : stProjInfo;
			stProjInfo = (a_eType == EAndroidType.GALAXY_STORE) ? stGalaxyStoreProjInfo : stProjInfo;

			PlayerSettings.bundleVersion = stProjInfo.m_stBuildVer.m_oVer;
		}
		// 빌드 옵션을 설정한다 }

		// 빌드 디렉토리를 생성한다
		string oBuildPath = string.Format(KCEditorDefine.B_ABS_BUILD_P_FMT_ANDROID, oPlatform);
		CFactory.CreateDir(oBuildPath);

		// 플랫폼을 빌드한다
		CPlatformBuilder.BuildPlatform(a_oPlayerOpts);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidDebug(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.DEBUG, a_eType, KCDefine.B_BUILD_MODE_DEBUG, KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_DEBUG_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidRelease(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.RELEASE, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_RELEASE_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidAdhoc(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.ADHOC, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_ADHOC_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_ADHOC_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidAdhocUpload(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.ADHOC, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_ADHOC_UPLOAD_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_ADHOC_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidStore(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.STORE, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_STORE_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_STORE_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_AAB);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidStoreTest(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.STORE, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_STORE_TEST_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_STORE_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_APK);
	}

	//! 안드로이드를 원격 빌드한다
	private static void RemoteBuildAndroidStoreUpload(EAndroidType a_eType) {
		CPlatformBuilder.ExecuteAndroidJenkinsBuild(EBuildType.STORE, a_eType, KCDefine.B_BUILD_MODE_RELEASE, KCEditorDefine.B_STORE_UPLOAD_BUILD_FUNC_JENKINS, KCEditorDefine.B_ANDROID_STORE_PIPELINE_N_JENKINS, KCEditorDefine.B_BUILD_FILE_EXTENSION_ANDROID_AAB);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
