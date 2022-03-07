using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;

#if UNITY_IOS
using UnityEditor.iOS;
#elif UNITY_ANDROID
using UnityEditor.Android;
#endif			// #if UNITY_IOS

#if NOTI_MODULE_ENABLE
using Unity.Notifications;
#endif			// #if NOTI_MODULE_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

/** 플랫폼 옵션 설정자 - 설정 */
public static partial class CPlatformOptsSetter {
	#region 클래스 함수
	/** 플레이어 옵션을 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "PlayerOpts", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupPlayerOpts() {
		CPlatformOptsSetter.SetupOpts();
		CPlatformOptsSetter.SetupBuildOpts();
	}

	/** 에디터 옵션을 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "EditorOpts", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupEditorOpts() {
		CPlatformOptsSetter.EditorInitialize();
		VersionControlSettings.mode = KCEditorDefine.B_EDITOR_OPTS_VER_CONTROL;

		// TODO: 해당 기능 안정화 필요
		// EditorSettings.spritePackerMode = SpritePackerMode.SpriteAtlasV2;

		// 에디터 옵션을 설정한다 {
		EditorSettings.assetNamingUsesSpace = true;
		EditorSettings.asyncShaderCompilation = false;
		EditorSettings.prefabModeAllowAutoSave = true;
		EditorSettings.useLegacyProbeSampleCount = false;
		EditorSettings.cachingShaderPreprocessor = false;

		EditorSettings.enableCookiesInLightmapper = false;
		EditorSettings.enterPlayModeOptionsEnabled = false;
		EditorSettings.enableTextureStreamingInPlayMode = true;
		EditorSettings.enableTextureStreamingInEditMode = true;
		EditorSettings.serializeInlineMappingsOnOneLine = true;

		EditorSettings.gameObjectNamingDigits = KCDefine.B_VAL_2_INT;
		EditorSettings.etcTextureCompressorBehavior = (int)ETextureCompressionType.DEFAULT;

		EditorSettings.cacheServerMode = CacheServerMode.AsPreferences;
		EditorSettings.spritePackerMode = SpritePackerMode.AlwaysOnAtlas;
		EditorSettings.serializationMode = SerializationMode.ForceText;

		EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.None;
		EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore;
		EditorSettings.lineEndingsForNewScripts = LineEndingsMode.Windows;

		EditorSettings.unityRemoteDevice = KCEditorDefine.B_EDITOR_OPTS_REMOTE_DEVICE;
		EditorSettings.unityRemoteResolution = KCEditorDefine.B_EDITOR_OPTS_REMOTE_RESOLUTION;
		EditorSettings.unityRemoteCompression = KCEditorDefine.B_EDITOR_OPTS_REMOTE_COMPRESSION;
		EditorSettings.unityRemoteJoystickSource = KCEditorDefine.B_EDITOR_OPTS_JOYSTIC_SRC;

		EditorSettings.projectGenerationRootNamespace = string.Empty;
		EditorSettings.projectGenerationUserExtensions = KCEditorDefine.B_EDITOR_OPTS_EXTENSION_LIST.ToArray();

		EditorSettings.prefabUIEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_SCENE_LIST);
		EditorSettings.prefabRegularEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_SCENE_LIST);

#if MODE_2D_ENABLE
		EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
#else
		EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode3D;
#endif			// #if MODE_2D_ENABLE

#if BURST_COMPILER_MODULE_ENABLE
		// TODO: 버스트 컴파일러 사용 여부 설정 로직 구현 필요
#endif			// #if BURST_COMPILER_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
		// iOS 알림 옵션을 설정한다 {
		NotificationSettings.iOSSettings.UseAPSReleaseEnvironment = false;
		NotificationSettings.iOSSettings.UseLocationNotificationTrigger = false;
		NotificationSettings.iOSSettings.AddRemoteNotificationCapability = false;
		NotificationSettings.iOSSettings.RequestAuthorizationOnAppLaunch = true;
		NotificationSettings.iOSSettings.NotificationRequestAuthorizationForRemoteNotificationsOnAppLaunch = false;
		
		NotificationSettings.iOSSettings.DefaultAuthorizationOptions = KCEditorDefine.B_PRESENT_OPTS_AUTHORIZATION_NOTI;
		NotificationSettings.iOSSettings.RemoteNotificationForegroundPresentationOptions = KCEditorDefine.B_PRESENT_OPTS_REMOTE_NOTI;
		// iOS 알림 옵션을 설정한다 }

		// 안드로이드 알림 옵션을 설정한다 {
		NotificationSettings.AndroidSettings.UseCustomActivity = false;
		NotificationSettings.AndroidSettings.RescheduleOnDeviceRestart = true;
		
		NotificationSettings.AndroidSettings.CustomActivityString = KCEditorDefine.B_ACTIVITY_N_NOTI;
		// 안드로이드 알림 옵션을 설정한다 }
#endif			// #if NOTI_MODULE_ENABLE

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			// Do Something
		}
		// 에디터 옵션을 설정한다 }

		// 사운드 관리자가 존재 할 경우
		if(CEditorAccess.IsExistsAsset(KCEditorDefine.B_ASSET_P_SND_MANAGER)) {
			var oConfiguration = AudioSettings.GetConfiguration();
			oConfiguration.sampleRate = KCDefine.B_VAL_0_INT;
			oConfiguration.speakerMode = AudioSpeakerMode.Mono;
			oConfiguration.dspBufferSize = (int)EDSPBufferSize.BEST_PERFORMANCE;

			// 옵션 정보 테이블이 존재 할 경우
			if(CPlatformOptsSetter.OptsInfoTable != null) {
				oConfiguration.numRealVoices = CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumRealVoices;
				oConfiguration.numVirtualVoices = CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumVirtualVoices;
			}

			var oSerializeObj = CEditorFactory.CreateSerializeObj(KCEditorDefine.B_ASSET_P_SND_MANAGER);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DISABLE_AUDIO, (a_oProperty) => a_oProperty.boolValue = false);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_VIRTUALIZE_EFFECT, (a_oProperty) => a_oProperty.boolValue = true);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_ENABLE_OUTPUT_SUSPENSION, (a_oProperty) => a_oProperty.boolValue = true);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_GLOBAL_VOLUME, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_ROLLOFF_SCALE, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DOPPLER_FACTOR, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_AMBISONIC_DECODER_PLUGIN, (a_oProperty) => a_oProperty.stringValue = string.Empty);

			AudioSettings.SetSpatializerPluginName(string.Empty);
			AudioSettings.Reset(oConfiguration);
		}

		// 태그 관리자가 존재 할 경우
		if(CEditorAccess.IsExistsAsset(KCEditorDefine.B_ASSET_P_TAG_MANAGER)) {
			var oSerializeObj = CEditorFactory.CreateSerializeObj(KCEditorDefine.B_ASSET_P_TAG_MANAGER);
			
			// 태그를 설정한다
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_TAG_M_TAG, (a_oProperty) => {
				for(int i = a_oProperty.arraySize; i < KCDefine.U_TAG_LIST.Count; ++i) {
					a_oProperty.InsertArrayElementAtIndex(i);
				}

				for(int i = 0; i < KCDefine.U_TAG_LIST.Count; ++i) {
					var oProperty = a_oProperty.GetArrayElementAtIndex(i);
					oProperty.stringValue = KCDefine.U_TAG_LIST[i];
				}
			});

			// 정렬 레이어를 설정한다
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_TAG_M_SORTING_LAYER, (a_oProperty) => {
				for(int i = a_oProperty.arraySize; i < KCDefine.U_SORTING_LAYER_LIST.Count; ++i) {
					a_oProperty.InsertArrayElementAtIndex(i);
				}

				for(int i = 0; i < KCDefine.U_SORTING_LAYER_LIST.Count; ++i) {
					foreach(var oElement in a_oProperty.GetArrayElementAtIndex(i)) {
						var oProperty = oElement as SerializedProperty;

						// 태그 이름과 동일 할 경우
						if(oProperty.name.Equals(KCEditorDefine.B_PROPERTY_N_TAG_M_NAME)) {
							oProperty.stringValue = KCDefine.U_SORTING_LAYER_LIST[i];
						}
						// 고유 식별자 이름과 동일 할 경우
						else if(oProperty.name.Equals(KCEditorDefine.B_PROPERTY_N_TAG_M_UNIQUE_ID)) {
							oProperty.intValue = KCDefine.U_SORTING_LAYER_LIST[i].Equals(KCDefine.U_SORTING_L_DEF) ? KCDefine.B_VAL_0_INT : i + KCEditorDefine.B_UNIT_CUSTOM_TAG_START_ID;
						}
					}
				}
			});
		}
	}

	/** 프로젝트 옵션을 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "ProjOpts", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupProjOpts() {
		var oCopyFileListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_ICON_P_INFO_LIST,
			KCEditorDefine.B_DATA_P_INFO_LIST,
			KCEditorDefine.B_TABLE_P_INFO_LIST,
			KCEditorDefine.B_PREFAB_P_INFO_LIST,
			KCEditorDefine.B_ASSEMBLY_DEFINE_P_INFO_LIST,

#if NOTI_MODULE_ENABLE
			KCEditorDefine.B_NOTI_ICON_P_INFO_LIST
#endif			// #if NOTI_MODULE_ENABLE
		};

		var oCopyAssetListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_ASSET_P_INFO_LIST, KCEditorDefine.B_SCENE_P_INFO_LIST, KCEditorDefine.B_PIPELINE_P_INFO_LIST
		};

		for(int i = 0; i < oCopyAssetListContainer.Count; ++i) {
			for(int j = 0; j < oCopyAssetListContainer[i].Count; ++j) {
				CEditorFunc.CopyAsset(oCopyAssetListContainer[i][j].Item1, oCopyAssetListContainer[i][j].Item2, false);
			}
		}

		for(int i = 0; i < oCopyFileListContainer.Count; ++i) {
			for(int j = 0; j < oCopyFileListContainer[i].Count; ++j) {
				CFunc.CopyFile(oCopyFileListContainer[i][j].Item1, oCopyFileListContainer[i][j].Item2, false);
			}
		}

		for(int i = 0; i < KCEditorDefine.B_SCRIPT_P_INFO_LIST.Count; ++i) {
			CFunc.CopyFile(KCEditorDefine.B_SCRIPT_P_INFO_LIST[i].Item1, KCEditorDefine.B_SCRIPT_P_INFO_LIST[i].Item2, KCEditorDefine.DS_DEFINE_S_SCRIPT_TEMPLATE_ONLY, System.Text.Encoding.UTF8, false);
		}

		CFunc.CopyDir(KCEditorDefine.B_ABS_DIR_P_SRC_PYTHON_SCRIPTS, KCEditorDefine.B_ABS_DIR_P_DEST_PYTHON_SCRIPTS, false);
		CEditorFunc.UpdateAssetDBState();
	}
	
	/** 플러그인 프로젝트를 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "PluginProjs", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupPluginProjs() {
		var oDirPathInfoList = new List<(string, string)>() {
			(KCEditorDefine.B_SRC_PLUGIN_P_IOS, KCEditorDefine.B_DEST_PLUGIN_P_IOS)
		};

		var oFilePathInfoList = new List<(string, string)>() {
			(KCEditorDefine.B_ORIGIN_SRC_MANIFEST_P_ANDROID, KCEditorDefine.B_SRC_MANIFEST_P_ANDROID),
			(KCEditorDefine.B_ORIGIN_SRC_MAIN_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_MAIN_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_ORIGIN_SRC_GRADLE_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_GRADLE_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_ORIGIN_SRC_LAUNCHER_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_LAUNCHER_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_ORIGIN_SRC_BASE_PROJ_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID),

			(KCEditorDefine.B_SRC_PLUGIN_P_ANDROID, KCEditorDefine.B_DEST_PLUGIN_P_ANDROID),
			(KCEditorDefine.B_SRC_MANIFEST_P_ANDROID, KCEditorDefine.B_DEST_MANIFEST_P_ANDROID),
			(KCEditorDefine.B_SRC_MAIN_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_MAIN_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_SRC_GRADLE_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_GRADLE_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_SRC_LAUNCHER_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_LAUNCHER_TEMPLATE_P_ANDROID),
			(KCEditorDefine.B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_BASE_PROJ_TEMPLATE_P_ANDROID),
		};

		for(int i = 0; i < oDirPathInfoList.Count; ++i) {
			CFunc.CopyDir(oDirPathInfoList[i].Item1, oDirPathInfoList[i].Item2, false);
		}

		for(int i = 0; i < oFilePathInfoList.Count; ++i) {
			CFunc.CopyFile(oFilePathInfoList[i].Item1, oFilePathInfoList[i].Item2, false);
		}
		
		CFunc.CopyFile(KCEditorDefine.B_SRC_UNITY_PLUGIN_P_ANDROID, KCEditorDefine.B_DEST_UNITY_PLUGIN_P_ANDROID);
		CFunc.CopyFile(KCEditorDefine.B_SRC_LOCAL_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_LOCAL_TEMPLATE_P_ANDROID, KCEditorDefine.B_TOKEN_REPLACE_UNITY_VERSION, Application.unityVersion, System.Text.Encoding.UTF8);

		CEditorFunc.UpdateAssetDBState();
	}

	/** 전처리기 심볼을 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "DefineSymbols", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupDefineSymbols() {
		// 테이블을 제거한다
		Resources.UnloadAsset(CPlatformOptsSetter.BuildInfoTable);
		Resources.UnloadAsset(CPlatformOptsSetter.OptsInfoTable);
		Resources.UnloadAsset(CPlatformOptsSetter.ProjInfoTable);
		Resources.UnloadAsset(CPlatformOptsSetter.DefineSymbolInfoTable);

		// 전처리기 심볼을 설정한다 {
		CPlatformOptsSetter.EditorInitialize();

		// 전처리기 심볼 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.DefineSymbolDictContainer.ExIsValid()) {
			var oRemoveDefineSymbolList = new List<string>() {
				KCEditorDefine.DS_DEFINE_S_STORE_DIST_BUILD,
				KCEditorDefine.DS_DEFINE_S_EDITOR_DIST_BUILD,
				KCEditorDefine.DS_DEFINE_S_CREATIVE_DIST_BUILD,
				KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE,
				KCEditorDefine.DS_DEFINE_S_ROBO_TEST_ENABLE
			};
			
			foreach(var stKeyVal in CPlatformOptsSetter.DefineSymbolDictContainer) {
				CPlatformOptsSetter.RemoveDefineSymbols(stKeyVal.Key, oRemoveDefineSymbolList);
			}

			CEditorFunc.SetupDefineSymbols(CPlatformOptsSetter.DefineSymbolDictContainer);
		}
		// 전처리기 심볼을 설정한다 }
	}

	/** 그래픽 API 를 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "GraphicAPIs", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupGraphicAPIs() {
		CEditorAccess.SetGraphicAPI(BuildTarget.iOS, KCEditorDefine.B_DEVICE_GRAPHICS_DEVICE_TYPE_LIST_IOS);
		CEditorAccess.SetGraphicAPI(BuildTarget.Android, KCEditorDefine.B_GRAPHICS_DEVICE_TYPE_LIST_ANDROID);
		
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneOSX, KCEditorDefine.B_GRAPHICS_DEVICE_TYPE_LIST_MAC);
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneWindows, KCEditorDefine.B_GRAPHICS_DEVICE_TYPE_LIST_WNDS);
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneWindows64, KCEditorDefine.B_GRAPHICS_DEVICE_TYPE_LIST_WNDS);
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneLinux64, KCEditorDefine.B_GRAPHICS_DEVICE_TYPE_LIST_LINUX);
	}

	/** 레벨 정보 테이블을 설정한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "LevelInfoTable", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupLevelInfoTable() {
		var oCopyDirListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_DIR_P_INFO_LIST,
		};

		for(int i = 0; i < oCopyDirListContainer.Count; ++i) {
			for(int j = 0; j < oCopyDirListContainer[i].Count; ++j) {
				CFunc.ShowLog($"{oCopyDirListContainer[i][j].Item1}, {oCopyDirListContainer[i][j].Item2}");

				CFunc.CopyDir(oCopyDirListContainer[i][j].Item1, oCopyDirListContainer[i][j].Item2, true);
			}
		}

		CEditorFunc.UpdateAssetDBState();
	}

	/** 퀄리티를 설정한다 */
	public static void SetupQuality() {
		var oQualityLevelList = new List<EQualityLevel>() {
			EQualityLevel.NORM, EQualityLevel.HIGH, EQualityLevel.ULTRA
		};

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			for(int i = 0; i < oQualityLevelList.Count; ++i) {
				// 퀄리티 수준이 다를 경우
				if(oQualityLevelList[i] != CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eQualityLevel) {
					CPlatformOptsSetter.DoSetupQuality(oQualityLevelList[i]);
				}
			}

			CPlatformOptsSetter.DoSetupQuality(CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eQualityLevel);
			GraphicsSettings.useScriptableRenderPipelineBatching = CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_bIsUseSRPBatching;
		}
	}

	/** 옵션을 설정한다 */
	private static void SetupOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupiOSOpts();
		CPlatformOptsSetter.SetupAndroidOpts();
		
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			CAccessExtension.ExSetRuntimePropertyVal<PlayerSettings.macOS>(null, KCEditorDefine.B_PROPERTY_N_CATEGORY, CPlatformOptsSetter.BuildInfoTable.StandaloneBuildInfo.m_oCategory);
		}

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			// 배치 모드 일 경우
			if(Application.isBatchMode) {
				switch(CPlatformBuilder.iOSType) {
					default: PlayerSettings.iOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_oAppID); break;
				}

				switch(CPlatformBuilder.AndroidType) {
					case EAndroidType.AMAZON: PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_oAppID); break;
					default: PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_oAppID); break;
				}

				switch(CPlatformBuilder.StandaloneType) {
					case EStandaloneType.WNDS_STEAM: PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_oAppID); break;
					default: PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_oAppID); break;
				}
			} else {
				PlayerSettings.iOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_oAppID);

#if ANDROID_AMAZON_PLATFORM
				PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_oAppID);
#else
				PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_oAppID);
#endif			// #if ANDROID_AMAZON_PLATFORM

#if STANDALONE_WNDS_STEAM_PLATFORM
				PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_oAppID);
#else
				PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_oAppID);
#endif			// #if STANDALONE_WNDS_STEAM_PLATFORM
			}
		}
		
		// 스크립트 API 를 설정한다 {
		PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_Unity_4_8);
		PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS, Il2CppCompilerConfiguration.Release);

		PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_Unity_4_8);
		PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Release);

		PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_Unity_4_8);
		PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Standalone, Il2CppCompilerConfiguration.Release);
		// 스크립트 API 를 설정한다 }
	}

	/** iOS 옵션을 설정한다 */
	private static void SetupiOSOpts() {
		PlayerSettings.iOS.SetiPadLaunchScreenType(iOSLaunchScreenType.Default);
		PlayerSettings.iOS.SetiPhoneLaunchScreenType(iOSLaunchScreenType.Default);

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.iOS.appleEnableAutomaticSigning = false;
		}
	}

	/** 안드로이드 옵션을 설정한다 */
	private static void SetupAndroidOpts() {
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.Android.useCustomKeystore = true;

			PlayerSettings.Android.keystoreName = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePath;
			PlayerSettings.Android.keyaliasName = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasName;

			PlayerSettings.Android.keystorePass = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePassword;
			PlayerSettings.Android.keyaliasPass = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasPassword;
		}
	}

	/** 빌드 옵션을 설정한다 */
	private static void SetupBuildOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupiOSBuildOpts();
		CPlatformOptsSetter.SetupAndroidBuildOpts();
		
		// 기본 옵션을 설정한다 {
		PlayerSettings.MTRendering = true;
		PlayerSettings.gpuSkinning = true;
		PlayerSettings.resizableWindow = false;
		PlayerSettings.macRetinaSupport = true;
		PlayerSettings.allowFullscreenSwitch = false;

		PlayerSettings.usePlayerLog = true;
		PlayerSettings.mipStripping = true;
		PlayerSettings.graphicsJobs = false;
		PlayerSettings.gcIncremental = true;
		PlayerSettings.statusBarHidden = true;

		PlayerSettings.stripEngineCode = true;
		PlayerSettings.allowUnsafeCode = false;
		PlayerSettings.captureSingleScreen = false;
		PlayerSettings.enableInternalProfiler = false;

		PlayerSettings.runInBackground = true;
		PlayerSettings.forceSingleInstance = true;
		PlayerSettings.visibleInBackground = true;
		PlayerSettings.useFlipModelSwapchain = true;
		PlayerSettings.enable360StereoCapture = true;

		PlayerSettings.enableCrashReportAPI = true;
		PlayerSettings.use32BitDisplayBuffer = true;
		PlayerSettings.muteOtherAudioSources = false;
		PlayerSettings.enableFrameTimingStats = true;
		PlayerSettings.enableMetalAPIValidation = true;

		PlayerSettings.stripUnusedMeshComponents = true;
		PlayerSettings.defaultIsNativeResolution = true;
		PlayerSettings.logObjCUncaughtExceptions = true;
		PlayerSettings.legacyClampBlendShapeWeights = false;

		PlayerSettings.defaultScreenWidth = KCDefine.B_LANDSCAPE_SCREEN_WIDTH;
		PlayerSettings.defaultScreenHeight = KCDefine.B_LANDSCAPE_SCREEN_HEIGHT;

		PlayerSettings.accelerometerFrequency = (int)EAccelerometerFrequency.FREQUENCY_60_HZ;
		PlayerSettings.actionOnDotNetUnhandledException = ActionOnDotNetUnhandledException.Crash;

		PlayerSettings.SetWsaHolographicRemotingEnabled(false);
		PlayerSettings.SetVirtualTexturingSupportEnabled(false);
		PlayerSettings.SetShaderPrecisionModel(ShaderPrecisionModel.PlatformDefault);

		EditorUserBuildSettings.overrideMaxTextureSize = KCDefine.B_VAL_0_INT;
		
		EditorUserBuildSettings.il2CppCodeGeneration = Il2CppCodeGeneration.OptimizeSpeed;
		EditorUserBuildSettings.overrideTextureCompression = OverrideTextureCompression.NoOverride;

#if DEBUG || DEVELOPMENT_BUILD
		PlayerSettings.useMacAppStoreValidation = false;
		PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
#else
		PlayerSettings.useMacAppStoreValidation = true;
		PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow;
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		// 기본 옵션을 설정한다 }

		// 종횡비를 설정한다 {
		var oAspectRatioList = new List<AspectRatio>() {
			AspectRatio.Aspect4by3, AspectRatio.Aspect5by4, AspectRatio.Aspect16by9, AspectRatio.Aspect16by10, AspectRatio.AspectOthers
		};

		for(int i = 0; i < oAspectRatioList.Count; ++i) {
			PlayerSettings.SetAspectRatio(oAspectRatioList[i], true);
		}
		// 종횡비를 설정한다 }

		// 아이콘을 설정한다 {
		var oIconTextureList = new List<Texture2D>() {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP)
		};
		
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, oIconTextureList.ToArray(), IconKind.Application);
		// 아이콘을 설정한다 }

		// 디바이스 방향을 설정한다 {
#if MODE_PORTRAIT_ENABLE
		PlayerSettings.allowedAutorotateToPortrait = true;

		PlayerSettings.allowedAutorotateToLandscapeLeft = false;
		PlayerSettings.allowedAutorotateToLandscapeRight = false;
#else
		PlayerSettings.allowedAutorotateToLandscapeLeft = true;
		PlayerSettings.allowedAutorotateToLandscapeRight = true;

		PlayerSettings.allowedAutorotateToPortrait = false;
#endif			// #if MODE_PORTRAIT_ENABLE

		PlayerSettings.useAnimatedAutorotation = true;
		PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
		PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
		// 디바이스 방향을 설정한다 }

		// 스플래시 옵션을 설정한다 {
		PlayerSettings.SplashScreen.show = false;
		PlayerSettings.SplashScreen.showUnityLogo = false;
		PlayerSettings.SplashScreen.blurBackgroundImage = true;

		PlayerSettings.SplashScreen.drawMode = PlayerSettings.SplashScreen.DrawMode.UnityLogoBelow;
		PlayerSettings.SplashScreen.animationMode = PlayerSettings.SplashScreen.AnimationMode.Static;
		PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark;
		PlayerSettings.SplashScreen.backgroundColor = Color.black;
		// 스플래시 옵션을 설정한다 }

		// 벌칸 옵션을 설정한다 {
		PlayerSettings.vulkanEnableSetSRGBWrite = false;
		PlayerSettings.vulkanEnableLateAcquireNextImage = false;
		
		PlayerSettings.vulkanNumSwapchainBuffers = KCDefine.B_VAL_2_INT;
		// 벌칸 옵션을 설정한다 }

		// 멀티 쓰레드 렌더링을 설정한다
		PlayerSettings.SetMobileMTRendering(BuildTargetGroup.iOS, PlayerSettings.MTRendering);
		PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, PlayerSettings.MTRendering);

		// 로그 타입을 설정한다 {
		var oLogTypeList = new List<LogType>() {
			LogType.Log, LogType.Warning, LogType.Error, LogType.Assert, LogType.Exception
		};

		for(int i = 0; i < oLogTypeList.Count; ++i) {
			PlayerSettings.SetStackTraceLogType(oLogTypeList[i], StackTraceLogType.ScriptOnly);
		}
		// 로그 타입을 설정한다 }

		// 스크립트 관리 수준을 설정한다 {
		var oBuildTargetGroupList = new List<BuildTargetGroup>() {
			BuildTargetGroup.iOS, BuildTargetGroup.Android, BuildTargetGroup.Standalone
		};

		for(int i = 0; i < oBuildTargetGroupList.Count; ++i) {
			PlayerSettings.SetManagedStrippingLevel(oBuildTargetGroupList[i], ManagedStrippingLevel.Low);
		}
		// 스크립트 관리 수준을 설정한다 }

		// 광원 맵 스트리밍 여부를 설정한다 {
		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, true
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, true
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, true
		});
		// 광원 맵 스트리밍 여부를 설정한다 }

		// 광원 맵 스트리밍 우선 순위를 설정한다 {
		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, KCDefine.B_VAL_0_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, KCDefine.B_VAL_0_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, KCDefine.B_VAL_0_INT
		});
		// 광원 맵 스트리밍 우선 순위를 설정한다 }

		// 광원 맵 엔코딩 퀄리티를 설정한다 {
		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, (int)ELightmapEncodingQuality.HIGH
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, (int)ELightmapEncodingQuality.HIGH
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, (int)ELightmapEncodingQuality.HIGH
		});
		// 광원 맵 엔코딩 퀄리티를 설정한다 }

		// 리소스 압축 방식을 설정한다 {
#if DEBUG || DEVELOPMENT_BUILD
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, (int)CompressionType.Lz4
		});
#else
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, (int)CompressionType.Lz4HC
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, (int)CompressionType.Lz4HC
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, (int)CompressionType.Lz4HC
		});
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		// 리소스 압축 방식을 설정한다 }

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			PlayerSettings.bakeCollisionMeshes = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreBakeCollisionMesh;
			PlayerSettings.preserveFramebufferAlpha = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreserveFrameBufferAlpha;

			PlayerSettings.colorSpace = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eColorSpace;
		}

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			PlayerSettings.companyName = CPlatformOptsSetter.ProjInfoTable.Company;
			PlayerSettings.productName = CPlatformOptsSetter.ProjInfoTable.ProductName;
		}
	}

	/** iOS 빌드 옵션을 설정한다 */
	private static void SetupiOSBuildOpts() {
		PlayerSettings.iOS.hideHomeButton = false;
		PlayerSettings.iOS.allowHTTPDownload = true;
		PlayerSettings.iOS.requiresFullScreen = true;
		PlayerSettings.iOS.useOnDemandResources = false;

		PlayerSettings.iOS.requiresPersistentWiFi = false;
		PlayerSettings.iOS.forceHardShadowsOnMetal = false;
		PlayerSettings.iOS.disableDepthAndStencilBuffers = false;

		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
		PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
		PlayerSettings.iOS.statusBarStyle = iOSStatusBarStyle.Default;
		
		PlayerSettings.iOS.scriptCallOptimization = ScriptCallOptimizationLevel.SlowAndSafe;
		PlayerSettings.iOS.appInBackgroundBehavior = iOSAppInBackgroundBehavior.Suspend;
		PlayerSettings.iOS.showActivityIndicatorOnLoading = iOSShowActivityIndicatorOnLoading.WhiteLarge;

		EditorUserBuildSettings.symlinkSources = false;
		PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, (int)AppleMobileArchitecture.ARM64);

		CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_AUTO_ADD_CAPABILITIES, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, true);
		CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, true);
		CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, false);

#if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE
		PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.Fetch | iOSBackgroundMode.RemoteNotification;
#else
		PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.None;
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE

#if UNITY_IOS
		var oAppIconList = new List<Texture2D>() {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_180x180),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_167x167),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_152x152),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_120x120),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_76x76)
		};

		var oStoreIconList = new List<Texture2D>() {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_1024x1024)
		};

		var oPlatformAppIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Application);

		for(int i = 0; i < oAppIconList.Count && i < oPlatformAppIcons.Length; ++i) {
			oPlatformAppIcons[i].SetTexture(oAppIconList[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Application, oPlatformAppIcons);
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iOS, oStoreIconList.ToArray(), IconKind.Store);

#if NOTI_MODULE_ENABLE
		var oNotiIconList = new List<Texture2D>() {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_60x60),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_40x40),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_20x20)
		};

		var oPlatformNotiIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification);

		for(int i = 0; i < oNotiIconList.Count && i < oPlatformNotiIcons.Length; ++i) {
			oPlatformNotiIcons[i].SetTexture(oNotiIconList[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification, oPlatformNotiIcons);
#endif			// #if NOTI_MODULE_ENABLE
#endif			// #if UNITY_IOS

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.iOS.appleDeveloperTeamID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTeamID;
			PlayerSettings.iOS.targetOSVersionString = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTargetOSVer;
		}

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			PlayerSettings.iOS.cameraUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oCameraDescription;
			PlayerSettings.iOS.locationUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oLocationDescription;
			PlayerSettings.iOS.microphoneUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oMicrophoneDescription;
		}
	}

	/** 안드로이드 빌드 옵션을 설정한다 */
	private static void SetupAndroidBuildOpts() {
		PlayerSettings.Android.minifyDebug = false;
		PlayerSettings.Android.minifyWithR8 = false;
		PlayerSettings.Android.minifyRelease = false;

		PlayerSettings.Android.androidIsGame = true;
		PlayerSettings.Android.startInFullscreen = true;
		PlayerSettings.Android.optimizedFramePacing = true;

		PlayerSettings.Android.forceSDCardPermission = false;
		PlayerSettings.Android.androidTVCompatibility = false;

		PlayerSettings.Android.renderOutsideSafeArea = true;
		PlayerSettings.Android.forceInternetPermission = true;
		PlayerSettings.Android.buildApkPerCpuArchitecture = false;
		PlayerSettings.Android.disableDepthAndStencilBuffers = false;

		PlayerSettings.Android.blitType = AndroidBlitType.Always;
		PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFit;
		PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
		PlayerSettings.Android.androidTargetDevices = AndroidTargetDevices.PhonesTabletsAndTVDevicesOnly;
		PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
		PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.InversedLarge;

		EditorUserBuildSettings.exportAsGoogleAndroidProject = false;

		EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
		EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
		EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Public;

		CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, true);
		CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, (int)EAspectRatioMode.NATIVE_ASPECT_RATIO);
		CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, KCEditorDefine.B_UNIT_VALIDATE_APP_BUNDLE_SIZE);

#if UNITY_ANDROID
		var oIconList = new List<Texture2D>() {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_192x192),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_144x144),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_96x96),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_72x72),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_48x48),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_36x36)
		};
		
		var oPlatformIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy);

		for(int i = 0; i < oIconList.Count && i < oPlatformIcons.Length; ++i) {
			oPlatformIcons[i].SetTexture(oIconList[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy, oPlatformIcons);
#endif			// #if UNITY_ANDROID

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.Android.minSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eMinSDKVer;
			PlayerSettings.Android.targetSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eTargetSDKVer;
		}

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			PlayerSettings.Android.useAPKExpansionFiles = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stAndroidBuildOptsInfo.m_bIsUseAPKExpansionFiles;
		}
	}

	/** 퀄리티를 설정한다 */
	private static void DoSetupQuality(EQualityLevel a_eQualityLevel) {
		CSceneManager.SetupQuality(a_eQualityLevel);

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
		CPlatformOptsSetter.SetupRenderingPipeline(a_eQualityLevel, Resources.Load<UniversalRenderPipelineAsset>(CAccess.GetRenderingPipelinePath(a_eQualityLevel)), false);
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	/** 렌더링 파이프라인을 설정한다 */
	private static void SetupRenderingPipeline(EQualityLevel a_eQualityLevel, UniversalRenderPipelineAsset a_oRenderingPipeline, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oRenderingPipeline != null);

		// 렌더링 파이프라인이 존재 할 경우
		if(a_oRenderingPipeline != null) {
			var oRenderingPipelineRendererDataList = new List<UniversalRendererData>() {
				Resources.Load<UniversalRendererData>(KCDefine.U_ASSET_P_G_UNIVERSAL_RP_RENDERER_DATA),
				Resources.Load<UniversalRendererData>(KCDefine.U_ASSET_P_G_UNIVERSAL_RP_SSAO_RENDERER_DATA)
			};

			var oRenderingPipeline2DRendererDataList = new List<Renderer2DData>() {
				Resources.Load<Renderer2DData>(KCDefine.U_ASSET_P_G_UNIVERSAL_RP_2D_RENDERER_DATA),
				Resources.Load<Renderer2DData>(KCDefine.U_ASSET_P_G_UNIVERSAL_RP_2D_SSAO_RENDERER_DATA)
			};

			for(int i = 0; i < oRenderingPipelineRendererDataList.Count; ++i) {
				// 렌더링 파이프라인 렌더러 데이터가 존재 할 경우
				if(oRenderingPipelineRendererDataList[i] != null) {
					oRenderingPipelineRendererDataList[i].useNativeRenderPass = true;
					oRenderingPipelineRendererDataList[i].accurateGbufferNormals = true;
					oRenderingPipelineRendererDataList[i].shadowTransparentReceive = true;
					oRenderingPipelineRendererDataList[i].defaultStencilState.overrideStencilState = false;

					oRenderingPipelineRendererDataList[i].renderingMode = RenderingMode.Forward;
					oRenderingPipelineRendererDataList[i].depthPrimingMode = DepthPrimingMode.Disabled;
					oRenderingPipelineRendererDataList[i].intermediateTextureMode = IntermediateTextureMode.Auto;
				}
			}

			for(int i = 0; i < oRenderingPipeline2DRendererDataList.Count; ++i) {
				// 렌더링 파이프라인 2D 렌더러 데이터가 존재 할 경우
				if(oRenderingPipeline2DRendererDataList[i] != null) {
					oRenderingPipeline2DRendererDataList[i].useNativeRenderPass = true;
				}
			}

			a_oRenderingPipeline.supportsHDR = true;
			a_oRenderingPipeline.useAdaptivePerformance = false;
			
			a_oRenderingPipeline.supportsDynamicBatching = true;
			a_oRenderingPipeline.supportsCameraDepthTexture = true;
			a_oRenderingPipeline.supportsCameraOpaqueTexture = true;

			a_oRenderingPipeline.colorGradingMode = ColorGradingMode.HighDynamicRange;
			a_oRenderingPipeline.shaderVariantLogLevel = ShaderVariantLogLevel.AllShaders;

			a_oRenderingPipeline.renderScale = KCDefine.B_VAL_1_FLT;
			a_oRenderingPipeline.colorGradingLutSize = sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE;
			
			a_oRenderingPipeline.shadowDistance = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE / KCDefine.B_VAL_2_FLT;
			a_oRenderingPipeline.shadowDepthBias = KCDefine.B_VAL_1_FLT;
			a_oRenderingPipeline.shadowNormalBias = KCDefine.B_VAL_1_FLT;
			a_oRenderingPipeline.shadowCascadeCount = (int)EShadowCascadesOpts.FOUR_CASCADES;

			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_SOFT_SHADOW, true);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_TERRAIN_HOLES, true);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_MIXED_LIGHTING, true);
			
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SUPPORTS_SHADOW, true);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_USE_FAST_SRGB_LINEAR_CONVERSION, false);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SUPPORTS_SHADOW, true);

			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_BORDER, KCDefine.B_VAL_2_FLT / KCDefine.B_UNIT_DIGITS_PER_TEN);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_OPAQUE_DOWN_SAMPLING, Downsampling._4xBilinear);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ_LIMIT, KCDefine.B_VAL_4_INT);

			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_2_SPLIT, KCEditorDefine.U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_3_SPLIT, KCEditorDefine.U_EDITOR_OPTS_CASCADE_3_SPLIT_PERCENT);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_4_SPLIT, KCEditorDefine.U_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT);

			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_RENDERING_MODE, LightRenderingMode.PerPixel);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_FMT, LightCookieFormat.ColorHDR);
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_RENDERING_MODE, LightRenderingMode.PerPixel);

#if MSAA_ENABLE
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MSAA_QUALITY, MsaaQuality._4x);
#else
			a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MSAA_QUALITY, MsaaQuality.Disabled);
#endif			// #if MSAA_ENABLE

			// 옵션 정보 테이블이 존재 할 경우
			if(CPlatformOptsSetter.OptsInfoTable != null) {
				var stRenderingOptsInfo = CPlatformOptsSetter.OptsInfoTable.GetRenderingOptsInfo(a_eQualityLevel);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_RESOLUTION, (LightCookieResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eLightCookieResolution);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SHADOW_MAP_RESOLUTION, (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eMainLightShadowResolution);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SHADOW_MAP_RESOLUTION, (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eAdditionalShadowResolution);

				a_oRenderingPipeline.useSRPBatcher = CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_bIsUseSRPBatching;
			}
		}
	}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_EDITOR
