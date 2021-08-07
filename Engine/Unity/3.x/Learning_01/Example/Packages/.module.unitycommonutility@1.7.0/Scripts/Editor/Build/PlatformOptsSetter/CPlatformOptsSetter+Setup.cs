using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

#if UNITY_IOS
using UnityEditor.iOS;
#elif UNITY_ANDROID
using UnityEditor.Android;
#endif			// #if UNITY_IOS

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
using Unity.Notifications;
#endif			// #if NOTI_MODULE_ENABLE

//! 플랫폼 옵션 설정자 - 설정
public static partial class CPlatformOptsSetter {
	#region 클래스 함수
	//! 플레이어 옵션을 설정한다
	[MenuItem("Tools/Utility/Setup/PlayerOpts")]
	public static void SetupPlayerOpts() {
		CPlatformOptsSetter.SetupOpts();
		CPlatformOptsSetter.SetupBuildOpts();
	}

	//! 에디터 옵션을 설정한다
	[MenuItem("Tools/Utility/Setup/EditorOpts")]
	public static void SetupEditorOpts() {
		CPlatformOptsSetter.EditorInitialize();
		VersionControlSettings.mode = KCEditorDefine.B_EDITOR_OPTS_VER_CONTROL;

		// 에디터 옵션을 설정한다 {
		EditorSettings.assetNamingUsesSpace = true;
		EditorSettings.enterPlayModeOptionsEnabled = false;
		EditorSettings.gameObjectNamingDigits = KCEditorDefine.B_EDITOR_OPTS_GAME_OBJ_NAMING_DIGITS;
		
		EditorSettings.spritePackerMode = SpritePackerMode.AlwaysOnAtlas;
		EditorSettings.serializationMode = SerializationMode.ForceText;
		EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.None;
		EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore;

		EditorSettings.unityRemoteResolution = KCEditorDefine.B_EDITOR_OPTS_REMOTE_RESOLUTION;
		EditorSettings.unityRemoteCompression = KCEditorDefine.B_EDITOR_OPTS_REMOTE_COMPRESSION;
		EditorSettings.unityRemoteJoystickSource = KCEditorDefine.B_EDITOR_OPTS_JOYSTIC_SRC;
		EditorSettings.projectGenerationRootNamespace = string.Empty;
		EditorSettings.projectGenerationUserExtensions = KCEditorDefine.B_EDITOR_OPTS_EXTENSIONS;

		EditorSettings.prefabUIEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_SCENES);
		EditorSettings.prefabRegularEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_SCENES);

#if MODE_2D_ENABLE
		EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
#else
		EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode3D;
#endif			// #if MODE_2D_ENABLE

#if UNITY_IOS
		EditorSettings.unityRemoteDevice = KCEditorDefine.B_EDITOR_OPTS_IOS_REMOTE_DEVICE;
#elif UNITY_ANDROID
		EditorSettings.unityRemoteDevice = KCEditorDefine.B_EDITOR_OPTS_ANDROID_REMOTE_DEVICE;
#else
		EditorSettings.unityRemoteDevice = KCEditorDefine.B_EDITOR_OPTS_DISABLE_REMOTE_DEVICE;
#endif			// #if UNITY_IOS

#if BURST_COMPILER_ENABLE
		// TODO: 버스트 컴파일러 사용 여부 설정 로직 구현 필요
#endif			// #if BURST_COMPILER_ENABLE

#if NOTI_MODULE_ENABLE
		// iOS 알림 옵션을 설정한다 {
		NotificationSettings.iOSSettings.UseAPSReleaseEnvironment = false;
		NotificationSettings.iOSSettings.UseLocationNotificationTrigger = false;
		NotificationSettings.iOSSettings.AddRemoteNotificationCapability = false;
		NotificationSettings.iOSSettings.RequestAuthorizationOnAppLaunch = true;
		NotificationSettings.iOSSettings.NotificationRequestAuthorizationForRemoteNotificationsOnAppLaunch = false;

		NotificationSettings.iOSSettings.DefaultAuthorizationOptions = KCEditorDefine.B_PRESENT_OPTS_NOTI;
		NotificationSettings.iOSSettings.RemoteNotificationForegroundPresentationOptions = KCEditorDefine.B_PRESENT_OPTS_REMOTE_NOTI;
		// iOS 알림 옵션을 설정한다 }

		// 안드로이드 알림 옵션을 설정한다 {
		NotificationSettings.AndroidSettings.UseCustomActivity = false;
		NotificationSettings.AndroidSettings.RescheduleOnDeviceRestart = true;
		
		NotificationSettings.AndroidSettings.CustomActivityString = KCEditorDefine.B_ACTIVITY_N_NOTI;
		// 안드로이드 알림 옵션을 설정한다 }
#endif			// #if NOTI_MODULE_ENABLE

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildOptsTable != null) {
			EditorSettings.asyncShaderCompilation = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_bIsAsyncShaderCompile;
			EditorSettings.cachingShaderPreprocessor = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_bIsCacheShaderPreprocessor;
			EditorSettings.useLegacyProbeSampleCount = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_bIsUseLegacyProbeSampleCount;
			EditorSettings.enableTextureStreamingInPlayMode = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_bIsEnableTextureStreamingInPlayMode;
			EditorSettings.enableTextureStreamingInEditMode = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_bIsEnableTextureStreamingInEditMode;

			EditorSettings.cacheServerMode = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_eCacheServerMode;	
			EditorSettings.lineEndingsForNewScripts = CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_eLineEndingMode;
			EditorSettings.etcTextureCompressorBehavior = (int)CPlatformOptsSetter.BuildOptsTable.EditorOpts.m_eTextureCompressionType;
		}
		// 에디터 옵션을 설정한다 }

		// 사운드 옵션을 설정한다 {
		var oSndManager = CEditorFunc.LoadAsset(KCEditorDefine.B_ASSET_P_SND_MANAGER);

		// 사운드 관리자가 존재 할 경우
		if(oSndManager != null && CPlatformOptsSetter.BuildOptsTable != null) {
			var oConfiguration = AudioSettings.GetConfiguration();
			oConfiguration.sampleRate = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_nSampleRate;
			oConfiguration.numRealVoices = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_nNumRealVoices;
			oConfiguration.numVirtualVoices = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_nNumVirtualVoices;
			oConfiguration.speakerMode = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_eSpeakerMode;
			oConfiguration.dspBufferSize = (int)CPlatformOptsSetter.BuildOptsTable.SndOpts.m_eDSPBufferSize;

			AudioSettings.Reset(oConfiguration);
			var oSerializeObj = new SerializedObject(oSndManager);

			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DISABLE_AUDIO, (a_oProperty) => a_oProperty.boolValue = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_bIsDisable);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_VIRTUALIZE_EFFECT, (a_oProperty) => a_oProperty.boolValue = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_bIsVirtualizeEffect);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_GLOBAL_VOLUME, (a_oProperty) => a_oProperty.floatValue = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_fGlobalVolume);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_ROLLOFF_SCALE, (a_oProperty) => a_oProperty.floatValue = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_fRolloffScale);
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DOPPLER_FACTOR, (a_oProperty) => a_oProperty.floatValue = CPlatformOptsSetter.BuildOptsTable.SndOpts.m_fDopplerFactor);
		}
		// 사운드 옵션을 설정한다 }

		// 태그 옵션을 설정한다 {
#if !CUSTOM_TAG_ENABLE || !CUSTOM_SORTING_L_ENABLE
		var oTagManager = CEditorFunc.LoadAsset(KCEditorDefine.B_ASSET_P_TAG_MANAGER);

		// 태그 관리자가 존재 할 경우
		if(oTagManager != null) {
			var oSerializeObj = new SerializedObject(oTagManager);

#if !CUSTOM_TAG_ENABLE
			// 태그를 설정한다
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_TAG_M_TAG, (a_oProperty) => {
#if !EXTRA_TAG_ENABLE
				a_oProperty.ClearArray();
#endif			// #if !EXTRA_TAG_ENABLE

				// 기존 배열과 길이가 다를 경우
				if(a_oProperty.arraySize < KCDefine.U_TAGS.Length) {
					for(int i = a_oProperty.arraySize; i < KCDefine.U_TAGS.Length; ++i) {
						a_oProperty.InsertArrayElementAtIndex(i);
					}
				}

				for(int i = 0; i < a_oProperty.arraySize; ++i) {
					var oProperty = a_oProperty.GetArrayElementAtIndex(i);
					oProperty.stringValue = KCDefine.U_TAGS[i];
				}
			});
#endif			// #if !CUSTOM_TAG_ENABLE

#if !CUSTOM_SORTING_L_ENABLE
			// 정렬 레이어를 설정한다
			oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_TAG_M_SORTING_LAYER, (a_oProperty) => {
#if !EXTRA_SORTING_L_ENABLE
				a_oProperty.ClearArray();
#endif			// #if !EXTRA_SORTING_L_ENABLE

				// 기존 배열과 길이가 다를 경우
				if(a_oProperty.arraySize < KCDefine.U_SORTING_LAYERS.Length) {
					for(int i = a_oProperty.arraySize; i < KCDefine.U_SORTING_LAYERS.Length; ++i) {
						a_oProperty.InsertArrayElementAtIndex(i);
					}
				}

				for(int i = 0; i < a_oProperty.arraySize; ++i) {
					var oEnumerator = a_oProperty.GetArrayElementAtIndex(i).GetEnumerator();

					while(oEnumerator.MoveNext()) {
						var oProperty = oEnumerator.Current as SerializedProperty;
						string oSortingLayer = KCDefine.U_SORTING_LAYERS[i];

						// 태그 이름과 동일 할 경우
						if(oProperty.name.ExIsEquals(KCEditorDefine.B_PROPERTY_N_TAG_M_NAME)) {
							oProperty.stringValue = oSortingLayer;
						}
						// 고유 식별자 이름과 동일 할 경우
						else if(oProperty.name.ExIsEquals(KCEditorDefine.B_PROPERTY_N_TAG_M_UNIQUE_ID)) {
							oProperty.intValue = oSortingLayer.ExIsEquals(KCDefine.U_SORTING_L_DEF) ? KCDefine.B_VAL_0_INT : i + KCEditorDefine.B_UNIT_CUSTOM_TAG_START_ID;
						}
					}
				}
			});
#endif			// #if !CUSTOM_SORTING_L_ENABLE
#endif			// #if !CUSTOM_TAG_ENABLE || !CUSTOM_SORTING_L_ENABLE
			// 태그 옵션을 설정한다 }
		}
	}

	//! 프로젝트 옵션을 설정한다
	[MenuItem("Tools/Utility/Setup/ProjOpts")]
	public static void SetupProjOpts() {
		string oSearch = KCEditorDefine.DS_DEFINE_S_NEVER_USE_THIS;
		CFunc.CopyDir(KCEditorDefine.B_ABS_DIR_P_SRC_PYTHON_SCRIPTS, KCEditorDefine.B_ABS_DIR_P_DEST_PYTHON_SCRIPTS, false);

		for(int i = 0; i < KCEditorDefine.B_SCRIPT_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_SCRIPT_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, oSearch, false);
		}

		for(int i = 0; i < KCEditorDefine.B_DATA_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_DATA_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}

		for(int i = 0; i < KCEditorDefine.B_PREFAB_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_PREFAB_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}

		for(int i = 0; i < KCEditorDefine.B_TABLE_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_TABLE_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}

		for(int i = 0; i < KCEditorDefine.B_ASSET_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_ASSET_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}

		for(int i = 0; i < KCEditorDefine.B_PIPELINE_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_PIPELINE_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}
		
		for(int i = 0; i < KCEditorDefine.B_SCENE_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_SCENE_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}
		
		for(int i = 0; i < KCEditorDefine.B_ASSEMBLY_DEFINE_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_ASSEMBLY_DEFINE_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}
		
		for(int i = 0; i < KCEditorDefine.B_ICON_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_ICON_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value, false);
		}

#if NOTI_MODULE_ENABLE
		for(int i = 0; i < KCEditorDefine.B_NOTI_ICON_P_INFOS.Length; ++i) {
			var stPathInfo = KCEditorDefine.B_NOTI_ICON_P_INFOS[i];
			CFunc.CopyFile(stPathInfo.Key, stPathInfo.Value);
		}
#endif			// #if NOTI_MODULE_ENABLE

		CEditorFunc.UpdateAssetDBState();
	}
	
	//! 플러그인 프로젝트를 설정한다
	[MenuItem("Tools/Utility/Setup/PluginProjs")]
	public static void SetupPluginProjs() {
		// iOS 플러그인을 복사한다
		CFunc.CopyDir(KCEditorDefine.B_SRC_PLUGIN_P_IOS, KCEditorDefine.B_DEST_PLUGIN_P_IOS, false);

		// 안드로이드 플러그인을 복사한다 {
		CFunc.CopyFile(KCEditorDefine.B_SRC_PLUGIN_P_ANDROID, KCEditorDefine.B_DEST_PLUGIN_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_UNITY_PLUGIN_P_ANDROID, KCEditorDefine.B_DEST_UNITY_PLUGIN_P_ANDROID);

		CFunc.CopyFile(KCEditorDefine.B_ORIGIN_SRC_MANIFEST_P_ANDROID, KCEditorDefine.B_SRC_MANIFEST_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_MANIFEST_P_ANDROID, KCEditorDefine.B_DEST_MANIFEST_P_ANDROID, false);

		CFunc.CopyFile(KCEditorDefine.B_ORIGIN_SRC_MAIN_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_MAIN_TEMPLATE_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_MAIN_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_MAIN_TEMPLATE_P_ANDROID, false);

		CFunc.CopyFile(KCEditorDefine.B_ORIGIN_SRC_LAUNCHER_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_LAUNCHER_TEMPLATE_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_LAUNCHER_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_LAUNCHER_TEMPLATE_P_ANDROID, false);

		CFunc.CopyFile(KCEditorDefine.B_ORIGIN_SRC_BASE_PROJ_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_BASE_PROJ_TEMPLATE_P_ANDROID, false);

		CFunc.CopyFile(KCEditorDefine.B_ORIGIN_SRC_GRADLE_TEMPLATE_P_ANDROID, KCEditorDefine.B_SRC_GRADLE_TEMPLATE_P_ANDROID, false);
		CFunc.CopyFile(KCEditorDefine.B_SRC_GRADLE_TEMPLATE_P_ANDROID, KCEditorDefine.B_DEST_GRADLE_TEMPLATE_P_ANDROID, false);
		// 안드로이드 플러그인을 복사한다 }
		
		CEditorFunc.UpdateAssetDBState();
	}

	//! 전처리기 심볼을 설정한다
	[MenuItem("Tools/Utility/Setup/DefineSymbols")]
	public static void SetupDefineSymbols() {
		// 테이블을 제거한다
		Resources.UnloadAsset(CPlatformOptsSetter.BuildInfoTable);
		Resources.UnloadAsset(CPlatformOptsSetter.BuildOptsTable);
		Resources.UnloadAsset(CPlatformOptsSetter.ProjInfoTable);
		Resources.UnloadAsset(CPlatformOptsSetter.DefineSymbolTable);

		// 전처리기 심볼을 설정한다 {
		CPlatformOptsSetter.EditorInitialize();

		// 전처리기 심볼 테이블이 존재 할 경우
		if(CPlatformOptsSetter.DefineSymbolDictContainer.ExIsValid()) {
			foreach(var stKeyVal in CPlatformOptsSetter.DefineSymbolDictContainer) {
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_FPS_ENABLE);
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_RECEIPT_CHECK_ENABLE);
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_ADHOC_BUILD);
				CPlatformOptsSetter.RemoveDefineSymbol(stKeyVal.Key, KCEditorDefine.DS_DEFINE_S_STORE_BUILD);
			}

			CEditorFunc.SetupDefineSymbols(CPlatformOptsSetter.DefineSymbolDictContainer);
		}
		// 전처리기 심볼을 설정한다 }
	}

	//! 그래픽 API 를 설정한다
	[MenuItem("Tools/Utility/Setup/GraphicAPIs")]
	public static void SetupGraphicAPIs() {
		// 독립 플랫폼 그래픽 API 를 설정한다
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneOSX, KCEditorDefine.B_GRAPHICS_DEVICE_TYPES_MAC);
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneWindows, KCEditorDefine.B_GRAPHICS_DEVICE_TYPES_WNDS);
		CEditorAccess.SetGraphicAPI(BuildTarget.StandaloneWindows64, KCEditorDefine.B_GRAPHICS_DEVICE_TYPES_WNDS);

		// iOS 그래픽 API 를 설정한다
		CEditorAccess.SetGraphicAPI(BuildTarget.iOS, KCEditorDefine.B_DEVICE_GRAPHICS_DEVICE_TYPES_IOS);

		// 안드로이드 그래픽 API 를 설정한다
		CEditorAccess.SetGraphicAPI(BuildTarget.Android, KCEditorDefine.B_GRAPHICS_DEVICE_TYPES_ANDROID);
	}

	//! 옵션을 설정한다
	private static void SetupOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupStandaloneOpts();
		CPlatformOptsSetter.SetupiOSOpts();
		CPlatformOptsSetter.SetupAndroidOpts();

		// 퀄리티를 설정한다
		CFunc.SetupQuality(KCDefine.U_QUALITY_LEVEL, true);

		// 앱 식별자를 설정한다
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			PlayerSettings.macOS.buildNumber = CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_stBuildVer.m_nNum.ToString();
			PlayerSettings.iOS.buildNumber = CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_stBuildVer.m_nNum.ToString();

			PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, CPlatformOptsSetter.ProjInfoTable.iOSProjInfo.m_oAppID);

			bool bIsValidNum = false;
			int nBuildNum = KCDefine.B_VAL_0_INT;

			// 배치 모드 일 경우
			if(Application.isBatchMode) {
				// 윈도우즈 일 경우
				if(CPlatformBuilder.StandaloneType == EStandaloneType.WNDS) {
					PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.WndsProjInfo.m_oAppID);
				} else {
					PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_oAppID);
				}

				// 원 스토어 일 경우
				if(CPlatformBuilder.AndroidType == EAndroidType.ONE_STORE) {
					bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
				}
				// 갤럭시 스토어 일 경우
				else if(CPlatformBuilder.AndroidType == EAndroidType.GALAXY_STORE) {
					bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
				} else {
					bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
				}
			} else {
#if WNDS
				PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.WndsProjInfo.m_oAppID);
#else
				PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.MacProjInfo.m_oAppID);
#endif			// #if WNDS

#if ONE_STORE
				bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
#elif GALAXY_STORE
				bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
#else
				bIsValidNum = int.TryParse(CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_stBuildVer.m_nNum.ToString(), out nBuildNum);
#endif			// #if ONE_STORE
			}
			
			CAccess.Assert(bIsValidNum && nBuildNum >= KCDefine.B_MIN_BUILD_NUM);
			PlayerSettings.Android.bundleVersionCode = nBuildNum;

			// 배치 모드 일 경우
			if(Application.isBatchMode) {
				// 원 스토어 일 경우
				if(CPlatformBuilder.AndroidType == EAndroidType.ONE_STORE) {
					PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_oAppID);
				}
				// 갤럭시 스토어 일 경우
				else if(CPlatformBuilder.AndroidType == EAndroidType.GALAXY_STORE) {
					PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_oAppID);
				} else {
					PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_oAppID);
				}
			} else {
#if ONE_STORE
				PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.OneStoreProjInfo.m_oAppID);
#elif GALAXY_STORE
				PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GalaxyStoreProjInfo.m_oAppID);
#else
				PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GoogleProjInfo.m_oAppID);
#endif			// #if ONE_STORE
			}
		}

		// 스크립트 API 를 설정한다 {
		PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
		PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Standalone, Il2CppCompilerConfiguration.Release);

		PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);

		PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
		PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
		PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Release);
		// 스크립트 API 를 설정한다 }

#if FIREBASE_MODULE_ENABLE && FIREBASE_ANALYTICS_ENABLE
		// 컴파일러 추가 옵션을 설정한다
		// PlayerSettings.SetAdditionalCompilerArgumentsForGroup(BuildTargetGroup.Standalone, KCEditorDefine.B_COMPILER_OPTS_ADDITIONALS);
		// PlayerSettings.SetAdditionalCompilerArgumentsForGroup(BuildTargetGroup.iOS, KCEditorDefine.B_COMPILER_OPTS_ADDITIONALS);
		// PlayerSettings.SetAdditionalCompilerArgumentsForGroup(BuildTargetGroup.Android, KCEditorDefine.B_COMPILER_OPTS_ADDITIONALS);
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_ANALYTICS_ENABLE
	}

	//! 독립 플랫폼 옵션을 설정한다
	private static void SetupStandaloneOpts() {
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			var stBuildInfo = CPlatformOptsSetter.BuildInfoTable.StandaloneBuildInfo;
			CAccessExtension.ExSetRuntimePropertyVal<PlayerSettings.macOS>(null, KCEditorDefine.B_PROPERTY_N_CATEGORY, stBuildInfo.m_oCategory);
		}
	}

	//! iOS 옵션을 설정한다
	private static void SetupiOSOpts() {
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.iOS.SetiPadLaunchScreenType(CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_eiPadLaunchScreenType);
			PlayerSettings.iOS.SetiPhoneLaunchScreenType(CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_eiPhoneLaunchScreenType);
		}
	}

	//! 안드로이드 옵션을 설정한다
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

	//! 빌드 옵션을 설정한다
	private static void SetupBuildOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupStandaloneBuildOpts();
		CPlatformOptsSetter.SetupiOSBuildOpts();
		CPlatformOptsSetter.SetupAndroidBuildOpts();
		
		// 기본 옵션을 설정한다 {
		PlayerSettings.usePlayerLog = true;
		PlayerSettings.graphicsJobs = false;
		PlayerSettings.gcIncremental = true;
		PlayerSettings.statusBarHidden = true;
		PlayerSettings.stripEngineCode = true;
		PlayerSettings.allowUnsafeCode = false;
		PlayerSettings.enableCrashReportAPI = true;
		PlayerSettings.enableMetalAPIValidation = true;
		PlayerSettings.stripUnusedMeshComponents = true;
		PlayerSettings.defaultIsNativeResolution = true;
		PlayerSettings.logObjCUncaughtExceptions = true;
		PlayerSettings.legacyClampBlendShapeWeights = false;
		PlayerSettings.actionOnDotNetUnhandledException = ActionOnDotNetUnhandledException.Crash;

		PlayerSettings.SetVirtualTexturingSupportEnabled(false);
		PlayerSettings.SetShaderPrecisionModel(ShaderPrecisionModel.PlatformDefault);

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			PlayerSettings.companyName = CPlatformOptsSetter.ProjInfoTable.CompanyName;
			PlayerSettings.productName = CPlatformOptsSetter.ProjInfoTable.ProductName;
		}

#if LINEAR_PIPELINE_ENABLE
		PlayerSettings.colorSpace = ColorSpace.Linear;
#else
		PlayerSettings.colorSpace = ColorSpace.Gamma;
#endif			// #if LINEAR_PIPELINE_ENABLE
		// 기본 옵션을 설정한다 }

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
		PlayerSettings.SplashScreen.backgroundColor = KCEditorDefine.B_COLOR_UNITY_LOGO_BG;
		// 스플래시 옵션을 설정한다 }

		// 로그 타입을 설정한다
		PlayerSettings.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly);
		PlayerSettings.SetStackTraceLogType(LogType.Warning, StackTraceLogType.ScriptOnly);
		PlayerSettings.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);
		PlayerSettings.SetStackTraceLogType(LogType.Assert, StackTraceLogType.ScriptOnly);
		PlayerSettings.SetStackTraceLogType(LogType.Exception, StackTraceLogType.ScriptOnly);

		// 스크립트 관리 수준을 설정한다
		PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Standalone, ManagedStrippingLevel.Low);
		PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.iOS, ManagedStrippingLevel.Low);
		PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, ManagedStrippingLevel.Low);

		// 리소스 압축 방식을 설정한다 {
#if LZ4_COMPRESS_ENABLE
#if DEBUG || DEVELOPMENT_BUILD
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Standalone, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.iOS, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Android, (int)CompressionType.Lz4
		});
#else
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Standalone, (int)CompressionType.Lz4HC
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.iOS, (int)CompressionType.Lz4HC
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Android, (int)CompressionType.Lz4HC
		});
#endif			// #if DEBUG || DEVELOPMENT_BUILD
#else
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Standalone, (int)CompressionType.None
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.iOS, (int)CompressionType.None
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
			BuildTargetGroup.Android, (int)CompressionType.None
		});
#endif			// #if LZ4_COMPRESS_ENABLE
		// 리소스 압축 방식을 설정한다 }

		// 빌드 옵션 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildOptsTable != null) {
			PlayerSettings.gpuSkinning = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsGPUSkinning;
			PlayerSettings.MTRendering = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsMTRendering;
			PlayerSettings.runInBackground = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsRunInBackground;
			PlayerSettings.bakeCollisionMeshes = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsPreBakeCollisionMesh;
			PlayerSettings.use32BitDisplayBuffer = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsUse32BitDisplayBuffer;
			PlayerSettings.muteOtherAudioSources = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsMuteOtherAudioSource;
			PlayerSettings.enableFrameTimingStats = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableFrameTimingStats;
			PlayerSettings.enableInternalProfiler = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableInternalProfiler;
			PlayerSettings.preserveFramebufferAlpha = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsPreserveFrameBufferAlpha;
			PlayerSettings.vulkanEnableSetSRGBWrite = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableVulkanSRGBWrite;

			PlayerSettings.aotOptions = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_oAOTCompileOpts;
			PlayerSettings.accelerometerFrequency = (int)CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_eAccelerometerFrequency;
			PlayerSettings.vulkanNumSwapchainBuffers = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_nNumVulkanSwapChainBuffers;

			// 기타 옵션을 설정한다
			if(CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_oPreloadAssetList != null) {
				var oPreloadAssets = CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_oPreloadAssetList.ToArray();
				PlayerSettings.SetPreloadedAssets(oPreloadAssets);
			}

			// 멀티 쓰레드 렌더링을 설정한다
			PlayerSettings.SetMobileMTRendering(BuildTargetGroup.iOS, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsMTRendering);
			PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsMTRendering);

			// 광원 맵 엔코딩 퀄리티를 설정한다 {
			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Standalone, (int)CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_eLightmapEncodingQuality
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.iOS, (int)CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_eLightmapEncodingQuality
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Android, (int)CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_eLightmapEncodingQuality
			});
			// 광원 맵 엔코딩 퀄리티를 설정한다 }

			// 광원 맵 스트리밍 여부를 설정한다 {
			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Standalone, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableLightmapStreaming
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.iOS, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableLightmapStreaming
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Android, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_bIsEnableLightmapStreaming
			});
			// 광원 맵 스트리밍 여부를 설정한다 }

			// 광원 맵 스트리밍 우선 순위를 설정한다 {
			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Standalone, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_nLightmapStreamingPriority
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.iOS, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_nLightmapStreamingPriority
			});

			CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new object[] {
				BuildTargetGroup.Android, CPlatformOptsSetter.BuildOptsTable.CommonBuildOpts.m_nLightmapStreamingPriority
			});
			// 광원 맵 스트리밍 우선 순위를 설정한다 }
		}
	}

	//! 독립 플랫폼 빌드 옵션을 설정한다
	private static void SetupStandaloneBuildOpts() {
		PlayerSettings.resizableWindow = false;
		PlayerSettings.macRetinaSupport = true;
		PlayerSettings.visibleInBackground = false;
		PlayerSettings.useFlipModelSwapchain = true;
		PlayerSettings.allowFullscreenSwitch = false;
		PlayerSettings.useMacAppStoreValidation = false;
		
		PlayerSettings.defaultScreenWidth = KCDefine.B_DESKTOP_SCREEN_WIDTH;
		PlayerSettings.defaultScreenHeight = KCDefine.B_DESKTOP_SCREEN_HEIGHT;

		PlayerSettings.SetAspectRatio(AspectRatio.Aspect4by3, true);
		PlayerSettings.SetAspectRatio(AspectRatio.Aspect5by4, true);
		PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by9, true);
		PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by10, true);
		PlayerSettings.SetAspectRatio(AspectRatio.AspectOthers, true);
		
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new Texture2D[] {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_STANDALONE_APP)
		}, IconKind.Application);

		// 빌드 옵션 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildOptsTable != null) {
			PlayerSettings.fullScreenMode = CPlatformOptsSetter.BuildOptsTable.StandaloneBuildOpts.m_eFullscreenMode;
			PlayerSettings.forceSingleInstance = CPlatformOptsSetter.BuildOptsTable.StandaloneBuildOpts.m_bIsForceSingleInstance;
			PlayerSettings.captureSingleScreen = CPlatformOptsSetter.BuildOptsTable.StandaloneBuildOpts.m_bIsCaptureSingleScreen;
		}
	}

	//! iOS 빌드 옵션을 설정한다
	private static void SetupiOSBuildOpts() {
		PlayerSettings.iOS.hideHomeButton = false;
		PlayerSettings.iOS.allowHTTPDownload = true;
		PlayerSettings.iOS.requiresFullScreen = true;
		PlayerSettings.iOS.useOnDemandResources = false;
		PlayerSettings.iOS.forceHardShadowsOnMetal = false;
		PlayerSettings.iOS.appleEnableAutomaticSigning = false;
		PlayerSettings.iOS.disableDepthAndStencilBuffers = false;

		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
		PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.None;
		PlayerSettings.iOS.scriptCallOptimization = ScriptCallOptimizationLevel.SlowAndSafe;
		PlayerSettings.iOS.showActivityIndicatorOnLoading = iOSShowActivityIndicatorOnLoading.DontShow;

		EditorUserBuildSettings.symlinkLibraries = false;
		PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, (int)AppleMobileArchitecture.ARM64);

#if UNITY_IOS
		var oAppIcons = new Texture2D[] {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_180x180),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_167x167),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_152x152),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_120x120),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_76x76)
		};

		var oPlatformAppIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Application);

		for(int i = 0; i < oAppIcons.Length && i < oPlatformAppIcons.Length; ++i) {
			oPlatformAppIcons[i].SetTexture(oAppIcons[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Application, oPlatformAppIcons);

		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iOS, new Texture2D[] {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_APP_1024x1024)
		}, IconKind.Store);

#if NOTI_MODULE_ENABLE
		var oNotiIcons = new Texture2D[] {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_60x60),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_40x40),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_IOS_NOTI_20x20)
		};

		var oPlatformNotiIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification);

		for(int i = 0; i < oNotiIcons.Length && i < oPlatformNotiIcons.Length; ++i) {
			oPlatformNotiIcons[i].SetTexture(oNotiIcons[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification, oPlatformNotiIcons);
#endif			// #if NOTI_MODULE_ENABLE
#endif			// #if UNITY_IOS

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.iOS.appleDeveloperTeamID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTeamID;
			PlayerSettings.iOS.targetOSVersionString = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTargetOSVer;

			// URL 스키마가 유효 할 경우
			if(CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oURLSchemeList != null) {
				var oURLSchemes = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oURLSchemeList.ToArray();
				PlayerSettings.iOS.iOSUrlSchemes = oURLSchemes;
			}
		}

		// 빌드 옵션 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildOptsTable != null) {
			var stBuildOpts = CPlatformOptsSetter.BuildOptsTable.iOSBuildOpts;

			PlayerSettings.iOS.targetDevice = stBuildOpts.m_eTargetDevice;
			PlayerSettings.iOS.statusBarStyle = stBuildOpts.m_eStatusBarStyle;
			PlayerSettings.iOS.requiresPersistentWiFi = stBuildOpts.m_bIsRequirePersistentWIFI;
			PlayerSettings.iOS.appInBackgroundBehavior = stBuildOpts.m_eBackgroundBehavior;
			
			PlayerSettings.iOS.cameraUsageDescription = stBuildOpts.m_oCameraDescription;
			PlayerSettings.iOS.locationUsageDescription = stBuildOpts.m_oLocationDescription;
			PlayerSettings.iOS.microphoneUsageDescription = stBuildOpts.m_oMicrophoneDescription;

			CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, stBuildOpts.m_bIsEnableProMotion);
			CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, stBuildOpts.m_bIsRequreARKitSupports);
			CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_AUTO_ADD_CAPABILITIES, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, stBuildOpts.m_bIsAutoAddCapabilities);
		}
	}

	//! 안드로이드 빌드 옵션을 설정한다
	private static void SetupAndroidBuildOpts() {
		PlayerSettings.Android.minifyDebug = false;
		PlayerSettings.Android.minifyRelease = false;
		PlayerSettings.Android.minifyWithR8 = false;
		PlayerSettings.Android.androidIsGame = true;
		PlayerSettings.Android.startInFullscreen = true;
		PlayerSettings.Android.useAPKExpansionFiles = false;
		PlayerSettings.Android.forceSDCardPermission = false;
		PlayerSettings.Android.forceInternetPermission = true;
		PlayerSettings.Android.buildApkPerCpuArchitecture = false;
		PlayerSettings.Android.disableDepthAndStencilBuffers = false;
		
		PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFit;
		PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
		PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.DontShow;

		EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
		EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
		EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Public;

		CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, true);

#if UNITY_ANDROID
		var oIcons = new Texture2D[] {
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_192x192),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_144x144),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_96x96),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_72x72),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_48x48),
			Resources.Load<Texture2D>(KCEditorDefine.B_ICON_P_ANDROID_APP_36x36)
		};
		
		var oPlatformIcons = PlayerSettings.GetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy);

		for(int i = 0; i < oIcons.Length && i < oPlatformIcons.Length; ++i) {
			oPlatformIcons[i].SetTexture(oIcons[i]);
		}

		PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy, oPlatformIcons);
#endif			// #if UNITY_ANDROID

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			PlayerSettings.Android.minSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eMinSDKVer;
			PlayerSettings.Android.targetSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eTargetSDKVer;
		}

		// 빌드 옵션 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildOptsTable != null) {
			var stBuildOpts = CPlatformOptsSetter.BuildOptsTable.AndroidBuildOpts;

			PlayerSettings.Android.blitType = stBuildOpts.m_eBlitType;
			PlayerSettings.Android.renderOutsideSafeArea = stBuildOpts.m_bIsRenderOutside;
			PlayerSettings.Android.androidTVCompatibility = stBuildOpts.m_bIsTVCompatibility;
			PlayerSettings.Android.preferredInstallLocation = stBuildOpts.m_ePreferredInstallLocation;

			CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, stBuildOpts.m_nAppBundleSize);
			CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, (int)stBuildOpts.m_eAspectRatioMode);
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
