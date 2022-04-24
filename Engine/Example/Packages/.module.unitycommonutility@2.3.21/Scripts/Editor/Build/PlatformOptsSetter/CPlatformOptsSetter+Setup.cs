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

		EditorSettings.prefabUIEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_PREFAB_SCENE_LIST);
		EditorSettings.prefabRegularEnvironment = CEditorFunc.FindAsset<SceneAsset>(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_PREFAB_SCENE_LIST);
		EditorSettings.projectGenerationUserExtensions = KCEditorDefine.B_EDITOR_OPTS_EXTENSION_LIST.ToArray();

		var oIsSetupOptsList = new List<bool>() {
			EditorSettings.assetNamingUsesSpace,
			EditorSettings.prefabModeAllowAutoSave,
			EditorSettings.enableTextureStreamingInPlayMode,
			EditorSettings.enableTextureStreamingInEditMode,
			EditorSettings.serializeInlineMappingsOnOneLine,

			EditorSettings.asyncShaderCompilation == false,
			EditorSettings.useLegacyProbeSampleCount == false,
			EditorSettings.cachingShaderPreprocessor == false,
			EditorSettings.enableCookiesInLightmapper == false,
			EditorSettings.enterPlayModeOptionsEnabled == false,

			EditorSettings.gameObjectNamingDigits == KCDefine.B_VAL_2_INT,
			EditorSettings.gameObjectNamingScheme == EditorSettings.NamingScheme.Underscore,

			EditorSettings.cacheServerMode == CacheServerMode.AsPreferences,
			EditorSettings.spritePackerMode == SpritePackerMode.AlwaysOnAtlas,
			EditorSettings.serializationMode == SerializationMode.ForceText,
			EditorSettings.etcTextureCompressorBehavior == (int)ETextureCompression.DEFAULT,

			EditorSettings.enterPlayModeOptions == EnterPlayModeOptions.None,
			EditorSettings.lineEndingsForNewScripts == LineEndingsMode.Windows,

			EditorSettings.unityRemoteDevice.Equals(KCEditorDefine.B_EDITOR_OPTS_REMOTE_DEVICE),
			EditorSettings.unityRemoteResolution.Equals(KCEditorDefine.B_EDITOR_OPTS_REMOTE_RESOLUTION),
			EditorSettings.unityRemoteCompression.Equals(KCEditorDefine.B_EDITOR_OPTS_REMOTE_COMPRESSION),
			EditorSettings.unityRemoteJoystickSource.Equals(KCEditorDefine.B_EDITOR_OPTS_JOYSTIC_SRC),
			EditorSettings.projectGenerationRootNamespace.Equals(string.Empty),

#if MODE_2D_ENABLE
			EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode2D,
#else
			EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode3D,
#endif			// #if MODE_2D_ENABLE

			VersionControlSettings.mode.Equals(KCEditorDefine.B_EDITOR_OPTS_VER_CONTROL)
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			EditorSettings.assetNamingUsesSpace = true;
			EditorSettings.prefabModeAllowAutoSave = true;
			EditorSettings.enableTextureStreamingInPlayMode = true;
			EditorSettings.enableTextureStreamingInEditMode = true;
			EditorSettings.serializeInlineMappingsOnOneLine = true;

			EditorSettings.asyncShaderCompilation = false;
			EditorSettings.useLegacyProbeSampleCount = false;
			EditorSettings.cachingShaderPreprocessor = false;
			EditorSettings.enableCookiesInLightmapper = false;
			EditorSettings.enterPlayModeOptionsEnabled = false;

			EditorSettings.gameObjectNamingDigits = KCDefine.B_VAL_2_INT;
			EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore;
			
			EditorSettings.cacheServerMode = CacheServerMode.AsPreferences;
			EditorSettings.spritePackerMode = SpritePackerMode.AlwaysOnAtlas;
			EditorSettings.serializationMode = SerializationMode.ForceText;
			EditorSettings.etcTextureCompressorBehavior = (int)ETextureCompression.DEFAULT;

			EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.None;
			EditorSettings.lineEndingsForNewScripts = LineEndingsMode.Windows;

			EditorSettings.unityRemoteDevice = KCEditorDefine.B_EDITOR_OPTS_REMOTE_DEVICE;
			EditorSettings.unityRemoteResolution = KCEditorDefine.B_EDITOR_OPTS_REMOTE_RESOLUTION;
			EditorSettings.unityRemoteCompression = KCEditorDefine.B_EDITOR_OPTS_REMOTE_COMPRESSION;
			EditorSettings.unityRemoteJoystickSource = KCEditorDefine.B_EDITOR_OPTS_JOYSTIC_SRC;
			EditorSettings.projectGenerationRootNamespace = string.Empty;

#if MODE_2D_ENABLE
			EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
#else
			EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode3D;
#endif			// #if MODE_2D_ENABLE

			VersionControlSettings.mode = KCEditorDefine.B_EDITOR_OPTS_VER_CONTROL;
		}

#if NOTI_MODULE_ENABLE
		var oIsSetupNotiOptsList = new List<bool>() {
			// iOS {
			NotificationSettings.iOSSettings.RequestAuthorizationOnAppLaunch,

			NotificationSettings.iOSSettings.UseAPSReleaseEnvironment == false,
			NotificationSettings.iOSSettings.UseLocationNotificationTrigger == false,
			NotificationSettings.iOSSettings.AddRemoteNotificationCapability == false,
			NotificationSettings.iOSSettings.NotificationRequestAuthorizationForRemoteNotificationsOnAppLaunch == false,

			NotificationSettings.iOSSettings.DefaultAuthorizationOptions == KCEditorDefine.B_PRESENT_OPTS_AUTHORIZATION_NOTI,
			NotificationSettings.iOSSettings.RemoteNotificationForegroundPresentationOptions == KCEditorDefine.B_PRESENT_OPTS_REMOTE_NOTI,
			// iOS }

			// 안드로이드 {
			NotificationSettings.AndroidSettings.RescheduleOnDeviceRestart,
			NotificationSettings.AndroidSettings.UseCustomActivity == false,

			NotificationSettings.AndroidSettings.CustomActivityString.Equals(KCEditorDefine.B_ACTIVITY_N_NOTI)
			// 안드로이드 }
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupNotiOptsList.Contains(false)) {
			// iOS {
			NotificationSettings.iOSSettings.RequestAuthorizationOnAppLaunch = true;

			NotificationSettings.iOSSettings.UseAPSReleaseEnvironment = false;
			NotificationSettings.iOSSettings.UseLocationNotificationTrigger = false;
			NotificationSettings.iOSSettings.AddRemoteNotificationCapability = false;
			NotificationSettings.iOSSettings.NotificationRequestAuthorizationForRemoteNotificationsOnAppLaunch = false;

			NotificationSettings.iOSSettings.DefaultAuthorizationOptions = KCEditorDefine.B_PRESENT_OPTS_AUTHORIZATION_NOTI;
			NotificationSettings.iOSSettings.RemoteNotificationForegroundPresentationOptions = KCEditorDefine.B_PRESENT_OPTS_REMOTE_NOTI;
			// iOS }

			// 안드로이드 {
			NotificationSettings.AndroidSettings.RescheduleOnDeviceRestart = true;
			NotificationSettings.AndroidSettings.UseCustomActivity = false;
			
			NotificationSettings.AndroidSettings.CustomActivityString = KCEditorDefine.B_ACTIVITY_N_NOTI;
			// 안드로이드 }
		}
#endif			// #if NOTI_MODULE_ENABLE

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			// Do Something
		}

		// 사운드 관리자가 존재 할 경우
		if(CEditorAccess.IsExistsAsset(KCEditorDefine.B_ASSET_P_SND_MANAGER)) {
			var oConfig = AudioSettings.GetConfiguration();
			var oSerializeObj = CEditorFactory.CreateSerializeObj(KCEditorDefine.B_ASSET_P_SND_MANAGER);

			bool bIsSetupSndOpts = true;

			// 옵션 정보 테이블이 존재 할 경우
			if(CPlatformOptsSetter.OptsInfoTable != null) {
				var oIsSetupSndOptsList = new List<bool>() {
					oConfig.numRealVoices == CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumRealVoices,
					oConfig.numVirtualVoices == CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumVirtualVoices
				};

				// 설정 갱신이 필요 할 경우
				if(oIsSetupSndOptsList.Contains(false)) {
					bIsSetupSndOpts = false;
					oConfig.numRealVoices = CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumRealVoices;
					oConfig.numVirtualVoices = CPlatformOptsSetter.OptsInfoTable.SndOptsInfo.m_nNumVirtualVoices;
				}
			}

			var oIsSetupSndManagerOptsList = new List<bool>() {
				bIsSetupSndOpts,
				oConfig.sampleRate == KCDefine.B_VAL_0_INT,
				oConfig.speakerMode == AudioSpeakerMode.Mono,
				oConfig.dspBufferSize == (int)EDSPBufferSize.BEST_PERFORMANCE,

				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_VIRTUALIZE_EFFECT).boolValue,
				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_ENABLE_OUTPUT_SUSPENSION).boolValue,

				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_DISABLE_AUDIO).boolValue == false,

				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_GLOBAL_VOLUME).floatValue.ExIsEquals(KCDefine.B_VAL_1_FLT),
				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_ROLLOFF_SCALE).floatValue.ExIsEquals(KCDefine.B_VAL_1_FLT),
				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_DOPPLER_FACTOR).floatValue.ExIsEquals(KCDefine.B_VAL_1_FLT),

				oSerializeObj.FindProperty(KCEditorDefine.B_PROPERTY_N_SND_M_AMBISONIC_DECODER_PLUGIN).stringValue.Equals(string.Empty),
				AudioSettings.GetSpatializerPluginName().Equals(string.Empty)
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupSndManagerOptsList.Contains(false)) {
				oConfig.sampleRate = KCDefine.B_VAL_0_INT;
				oConfig.speakerMode = AudioSpeakerMode.Mono;
				oConfig.dspBufferSize = (int)EDSPBufferSize.BEST_PERFORMANCE;

				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_VIRTUALIZE_EFFECT, (a_oProperty) => a_oProperty.boolValue = true);
				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_ENABLE_OUTPUT_SUSPENSION, (a_oProperty) => a_oProperty.boolValue = true);

				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DISABLE_AUDIO, (a_oProperty) => a_oProperty.boolValue = false);

				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_GLOBAL_VOLUME, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);
				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_ROLLOFF_SCALE, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);
				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_DOPPLER_FACTOR, (a_oProperty) => a_oProperty.floatValue = KCDefine.B_VAL_1_FLT);

				oSerializeObj.ExSetPropertyVal(KCEditorDefine.B_PROPERTY_N_SND_M_AMBISONIC_DECODER_PLUGIN, (a_oProperty) => a_oProperty.stringValue = string.Empty);
				AudioSettings.SetSpatializerPluginName(string.Empty);

				AudioSettings.Reset(oConfig);
			}
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
		var oCopyFileInfoListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_DATA_P_INFO_LIST, KCEditorDefine.B_TABLE_P_INFO_LIST, KCEditorDefine.B_ASSEMBLY_DEFINE_P_INFO_LIST,

#if NOTI_MODULE_ENABLE
			KCEditorDefine.B_NOTI_ICON_P_INFO_LIST
#endif			// #if NOTI_MODULE_ENABLE
		};

		var oCopyAssetInfoListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_ICON_P_INFO_LIST, KCEditorDefine.B_PREFAB_P_INFO_LIST, KCEditorDefine.B_ASSET_P_INFO_LIST, KCEditorDefine.B_SCENE_P_INFO_LIST, KCEditorDefine.B_PIPELINE_P_INFO_LIST
		};

		for(int i = 0; i < oCopyAssetInfoListContainer.Count; ++i) {
			for(int j = 0; j < oCopyAssetInfoListContainer[i].Count; ++j) {
				CEditorFunc.CopyAsset(oCopyAssetInfoListContainer[i][j].Item1, oCopyAssetInfoListContainer[i][j].Item2, false);
			}
		}

		for(int i = 0; i < oCopyFileInfoListContainer.Count; ++i) {
			for(int j = 0; j < oCopyFileInfoListContainer[i].Count; ++j) {
				CFunc.CopyFile(oCopyFileInfoListContainer[i][j].Item1, oCopyFileInfoListContainer[i][j].Item2, false);
			}
		}

		for(int i = 0; i < KCEditorDefine.B_SCRIPT_P_INFO_LIST.Count; ++i) {
			CFunc.CopyFile(KCEditorDefine.B_SCRIPT_P_INFO_LIST[i].Item1, KCEditorDefine.B_SCRIPT_P_INFO_LIST[i].Item2, KCEditorDefine.DS_DEFINE_S_SCRIPT_TEMPLATE_ONLY, System.Text.Encoding.UTF8, false);
		}
		
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

	/** 테이블 정보를 설정한다 <summary>외부 디렉토리 (+ Ex. ExternalDatas) 에 존재하는 테이블을 유니티 프로젝트에 복사합니다.</summary> */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_SETUP_BASE + "TableInfos", false, KCEditorDefine.B_SORTING_O_SETUP_MENU + 1)]
	public static void SetupTableInfos() {
		var oCopyDirListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_DIR_P_TABLE_INFO_LIST
		};

		var oCopyFileInfoListContainer = new List<List<(string, string)>>() {
			KCEditorDefine.B_FILE_P_TABLE_INFO_LIST
		};

		for(int i = 0; i < oCopyDirListContainer.Count; ++i) {
			for(int j = 0; j < oCopyDirListContainer[i].Count; ++j) {
				CFunc.CopyDir(oCopyDirListContainer[i][j].Item1, oCopyDirListContainer[i][j].Item2);
			}
		}

		for(int i = 0; i < oCopyFileInfoListContainer.Count; ++i) {
			for(int j = 0; j < oCopyFileInfoListContainer[i].Count; ++j) {
				CFunc.CopyFile(oCopyFileInfoListContainer[i][j].Item1, oCopyFileInfoListContainer[i][j].Item2);
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
			var oIsSetupOptsList = new List<bool>() {
				GraphicsSettings.useScriptableRenderPipelineBatching == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsUseSRPBatching
			};

			for(int i = 0; i < oQualityLevelList.Count; ++i) {
				// 퀄리티 수준이 다를 경우
				if(oQualityLevelList[i] != CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eQualityLevel) {
					CPlatformOptsSetter.DoSetupQuality(oQualityLevelList[i]);
				}
			}

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				GraphicsSettings.useScriptableRenderPipelineBatching = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsUseSRPBatching;
			}

			CPlatformOptsSetter.DoSetupQuality(CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eQualityLevel);
		}
	}

	/** 옵션을 설정한다 */
	private static void SetupOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupiOSOpts();
		CPlatformOptsSetter.SetupAndroidOpts();
		CPlatformOptsSetter.SetupStandaloneOpts();

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			// 배치 모드 일 경우
			if(Application.isBatchMode) {
				switch(CPlatformBuilder.iOSType) {
					default: PlayerSettings.iOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo)); break;
				}

				switch(CPlatformBuilder.AndroidType) {
					case EAndroidType.AMAZON: PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo)); break;
					default: PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo)); break;
				}

				switch(CPlatformBuilder.StandaloneType) {
					case EStandaloneType.WNDS_STEAM: PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo)); break;
					default: PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo)); break;
				}
			} else {
				PlayerSettings.iOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.iOSAppleProjInfo));

#if ANDROID_AMAZON_PLATFORM
				PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.AndroidAmazonProjInfo));
#else
				PlayerSettings.Android.bundleVersionCode = CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo.m_stBuildVerInfo.m_nNum; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.AndroidGoogleProjInfo));
#endif			// #if ANDROID_AMAZON_PLATFORM

#if STANDALONE_WNDS_STEAM_PLATFORM
				PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.StandaloneWndsSteamProjInfo));
#else
				PlayerSettings.macOS.buildNumber = $"{CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo.m_stBuildVerInfo.m_nNum}"; PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, CPlatformOptsSetter.ProjInfoTable.GetAppID(CPlatformOptsSetter.ProjInfoTable.StandaloneMacSteamProjInfo));
#endif			// #if STANDALONE_WNDS_STEAM_PLATFORM
			}
		}

		var oIsSetupOptsList = new List<bool>() {
			PlayerSettings.GetScriptingBackend(BuildTargetGroup.iOS) == ScriptingImplementation.IL2CPP,
			PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.iOS) == ApiCompatibilityLevel.NET_Unity_4_8,
			PlayerSettings.GetIl2CppCompilerConfiguration(BuildTargetGroup.iOS) == Il2CppCompilerConfiguration.Release,

			PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android) == ScriptingImplementation.IL2CPP,
			PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Android) == ApiCompatibilityLevel.NET_Unity_4_8,
			PlayerSettings.GetIl2CppCompilerConfiguration(BuildTargetGroup.Android) == Il2CppCompilerConfiguration.Release,

			PlayerSettings.GetScriptingBackend(BuildTargetGroup.Standalone) == ScriptingImplementation.Mono2x,
			PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Standalone) == ApiCompatibilityLevel.NET_Unity_4_8,
			PlayerSettings.GetIl2CppCompilerConfiguration(BuildTargetGroup.Standalone) == Il2CppCompilerConfiguration.Release
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
			PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_Unity_4_8);
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS, Il2CppCompilerConfiguration.Release);

			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
			PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_Unity_4_8);
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Release);

			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
			PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_Unity_4_8);
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Standalone, Il2CppCompilerConfiguration.Release);
		}
	}

	/** 빌드 옵션을 설정한다 */
	private static void SetupBuildOpts() {
		CPlatformOptsSetter.EditorInitialize();

		CPlatformOptsSetter.SetupiOSBuildOpts();
		CPlatformOptsSetter.SetupAndroidBuildOpts();
		CPlatformOptsSetter.SetupStandaloneBuildOpts();

		var oIsSetupDeviceOptsList = new List<bool>() {
#if MODE_PORTRAIT_ENABLE
			PlayerSettings.allowedAutorotateToPortrait,

			PlayerSettings.allowedAutorotateToLandscapeLeft == false,
			PlayerSettings.allowedAutorotateToLandscapeRight == false,
#else
			PlayerSettings.allowedAutorotateToLandscapeLeft,
			PlayerSettings.allowedAutorotateToLandscapeRight,

			PlayerSettings.allowedAutorotateToPortrait == false,
#endif			// #if MODE_PORTRAIT_ENABLE

			PlayerSettings.useAnimatedAutorotation,
			PlayerSettings.allowedAutorotateToPortraitUpsideDown == false,
			PlayerSettings.defaultInterfaceOrientation == UIOrientation.AutoRotation
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupDeviceOptsList.Contains(false)) {
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
		}

		var oIsSetupSplashOptsList = new List<bool>() {
			PlayerSettings.SplashScreen.show == false,
			PlayerSettings.SplashScreen.showUnityLogo == false,
			PlayerSettings.SplashScreen.blurBackgroundImage == false,

			PlayerSettings.SplashScreen.drawMode == PlayerSettings.SplashScreen.DrawMode.UnityLogoBelow,
			PlayerSettings.SplashScreen.animationMode == PlayerSettings.SplashScreen.AnimationMode.Static,
			PlayerSettings.SplashScreen.unityLogoStyle == PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark,
			PlayerSettings.SplashScreen.backgroundColor == Color.black
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupSplashOptsList.Contains(false)) {
			PlayerSettings.SplashScreen.show = false;
			PlayerSettings.SplashScreen.showUnityLogo = false;
			PlayerSettings.SplashScreen.blurBackgroundImage = false;

			PlayerSettings.SplashScreen.drawMode = PlayerSettings.SplashScreen.DrawMode.UnityLogoBelow;
			PlayerSettings.SplashScreen.animationMode = PlayerSettings.SplashScreen.AnimationMode.Static;
			PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark;
			PlayerSettings.SplashScreen.backgroundColor = Color.black;
		}

		var oIsSetupVulkanOptsList = new List<bool>() {
			PlayerSettings.vulkanEnablePreTransform == false,
			PlayerSettings.vulkanEnableSetSRGBWrite == false,
			PlayerSettings.vulkanEnableLateAcquireNextImage == false,

			PlayerSettings.vulkanNumSwapchainBuffers == KCDefine.B_VAL_3_INT
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupVulkanOptsList.Contains(false)) {
			PlayerSettings.vulkanEnablePreTransform = false;
			PlayerSettings.vulkanEnableSetSRGBWrite = false;
			PlayerSettings.vulkanEnableLateAcquireNextImage = false;
			
			PlayerSettings.vulkanNumSwapchainBuffers = KCDefine.B_VAL_3_INT;
		}

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
			PlayerSettings.SetManagedStrippingLevel(oBuildTargetGroupList[i], ManagedStrippingLevel.Medium);
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

		// 리소스 압축 방식을 설정한다 {
		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.iOS, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Android, (int)CompressionType.Lz4
		});

		CExtension.ExCallFunc<EditorUserBuildSettings>(null, KCEditorDefine.B_FUNC_N_SET_COMPRESSION_TYPE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTargetGroup.Standalone, (int)CompressionType.Lz4
		});
		// 리소스 압축 방식을 설정한다 }

		// 배칭을 설정한다 {
		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_BATCHING_FOR_PLATFORM, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTarget.iOS, KCDefine.B_VAL_1_INT, KCDefine.B_VAL_1_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_BATCHING_FOR_PLATFORM, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTarget.Android, KCDefine.B_VAL_1_INT, KCDefine.B_VAL_1_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_BATCHING_FOR_PLATFORM, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTarget.StandaloneOSX, KCDefine.B_VAL_1_INT, KCDefine.B_VAL_1_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_BATCHING_FOR_PLATFORM, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTarget.StandaloneWindows, KCDefine.B_VAL_1_INT, KCDefine.B_VAL_1_INT
		});

		CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_BATCHING_FOR_PLATFORM, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
			BuildTarget.StandaloneWindows64, KCDefine.B_VAL_1_INT, KCDefine.B_VAL_1_INT
		});
		// 배칭을 설정한다 }

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			var oIsSetupOptsList = new List<bool>() {
				(int)CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_GET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() { BuildTargetGroup.iOS }) == (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality,
				(int)CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_GET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() { BuildTargetGroup.Android }) == (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality,
				(int)CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_GET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() { BuildTargetGroup.Standalone }) == (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
					BuildTargetGroup.iOS, (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality
				});

				CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
					BuildTargetGroup.Android, (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality
				});

				CExtension.ExCallFunc<PlayerSettings>(null, KCEditorDefine.B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, new List<object>() {
					BuildTargetGroup.Standalone, (int)CPlatformOptsSetter.OptsInfoTable.QualityOptsInfo.m_eLightmapEncodingQuality
				});
			}
		}
	}

	/** 퀄리티를 설정한다 */
	private static void DoSetupQuality(EQualityLevel a_eQualityLevel) {
		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			CSceneManager.SetupQuality(a_eQualityLevel, true);
		}

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
					var oIsSetupPipelineOptsList = new List<bool>() {
						oRenderingPipelineRendererDataList[i].useNativeRenderPass,
						oRenderingPipelineRendererDataList[i].accurateGbufferNormals,
						oRenderingPipelineRendererDataList[i].shadowTransparentReceive,

						oRenderingPipelineRendererDataList[i].defaultStencilState.overrideStencilState == false,

						oRenderingPipelineRendererDataList[i].renderingMode == RenderingMode.Forward,
						oRenderingPipelineRendererDataList[i].depthPrimingMode == DepthPrimingMode.Disabled,
						oRenderingPipelineRendererDataList[i].intermediateTextureMode == IntermediateTextureMode.Always,

						oRenderingPipelineRendererDataList[i].opaqueLayerMask.value == int.MaxValue,
						oRenderingPipelineRendererDataList[i].transparentLayerMask.value == int.MaxValue
					};

					// 설정 갱신이 필요 할 경우
					if(oIsSetupPipelineOptsList.Contains(false)) {
						oRenderingPipelineRendererDataList[i].useNativeRenderPass = true;
						oRenderingPipelineRendererDataList[i].accurateGbufferNormals = true;
						oRenderingPipelineRendererDataList[i].shadowTransparentReceive = true;

						oRenderingPipelineRendererDataList[i].defaultStencilState.overrideStencilState = false;

						oRenderingPipelineRendererDataList[i].renderingMode = RenderingMode.Forward;
						oRenderingPipelineRendererDataList[i].depthPrimingMode = DepthPrimingMode.Disabled;
						oRenderingPipelineRendererDataList[i].intermediateTextureMode = IntermediateTextureMode.Always;

						oRenderingPipelineRendererDataList[i].opaqueLayerMask = oRenderingPipelineRendererDataList[i].opaqueLayerMask.ExGetLayerVal(int.MaxValue);
						oRenderingPipelineRendererDataList[i].transparentLayerMask = oRenderingPipelineRendererDataList[i].transparentLayerMask.ExGetLayerVal(int.MaxValue);
					}
				}
			}

			for(int i = 0; i < oRenderingPipeline2DRendererDataList.Count; ++i) {
				// 렌더링 파이프라인 2D 렌더러 데이터가 존재 할 경우
				if(oRenderingPipeline2DRendererDataList[i] != null) {
					var oIsSetupPipelineOptsList = new List<bool>() {
						oRenderingPipeline2DRendererDataList[i].useNativeRenderPass
					};

					// 설정 갱신이 필요 할 경우
					if(oIsSetupPipelineOptsList.Contains(false)) {
						oRenderingPipeline2DRendererDataList[i].useNativeRenderPass = true;
					}
				}
			}

			var oIsSetupOptsList = new List<bool>() {
				a_oRenderingPipeline.supportsHDR,
				a_oRenderingPipeline.supportsDynamicBatching,
				a_oRenderingPipeline.supportsCameraDepthTexture,
				a_oRenderingPipeline.supportsCameraOpaqueTexture,

				a_oRenderingPipeline.useAdaptivePerformance == false,

				a_oRenderingPipeline.colorGradingMode == ColorGradingMode.HighDynamicRange,
				a_oRenderingPipeline.shadowCascadeCount == (int)EShadowCascadesOpts.FOUR_CASCADES,
				a_oRenderingPipeline.colorGradingLutSize == sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE,
				a_oRenderingPipeline.shaderVariantLogLevel == ShaderVariantLogLevel.AllShaders,

				a_oRenderingPipeline.renderScale.ExIsEquals(KCDefine.B_VAL_1_FLT),
				a_oRenderingPipeline.shadowDistance.ExIsEquals(KCDefine.U_DISTANCE_CAMERA_FAR_PLANE / KCDefine.B_VAL_2_FLT),
				a_oRenderingPipeline.shadowDepthBias.ExIsEquals(KCDefine.B_VAL_1_FLT),
				a_oRenderingPipeline.shadowNormalBias.ExIsEquals(KCDefine.B_VAL_1_FLT),

				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_SOFT_SHADOW),
				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_TERRAIN_HOLES),
				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SUPPORTS_SHADOW),
				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SUPPORTS_SHADOW),

				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BLENDING) == false,
				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BOX_PROJECTION) == false,
				(bool)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_USE_FAST_SRGB_LINEAR_CONVERSION) == false,

				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MSAA_QUALITY) == (int)MsaaQuality.Disabled,
				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_OPAQUE_DOWN_SAMPLING) == (int)Downsampling._4xBilinear,
				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_RENDERING_MODE) == (int)LightRenderingMode.PerPixel,

				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_FMT) == (int)LightCookieFormat.ColorHDR,
				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ_LIMIT) == KCDefine.B_VAL_8_INT,
				(int)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_RENDERING_MODE) == (int)LightRenderingMode.PerVertex,

				((float)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_BORDER)).ExIsEquals(KCDefine.B_VAL_2_FLT / KCDefine.B_UNIT_DIGITS_PER_TEN),
				((float)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_2_SPLIT)).ExIsEquals(KCEditorDefine.U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT),
				((Vector2)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_3_SPLIT)).ExIsEquals(KCEditorDefine.B_EDITOR_OPTS_CASCADE_3_SPLIT_PERCENT),
				((Vector3)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_4_SPLIT)).ExIsEquals(KCEditorDefine.B_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT)
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				a_oRenderingPipeline.supportsHDR = true;
				a_oRenderingPipeline.supportsDynamicBatching = true;
				a_oRenderingPipeline.supportsCameraDepthTexture = true;
				a_oRenderingPipeline.supportsCameraOpaqueTexture = true;

				a_oRenderingPipeline.useAdaptivePerformance = false;

				a_oRenderingPipeline.colorGradingMode = ColorGradingMode.HighDynamicRange;
				a_oRenderingPipeline.shadowCascadeCount = (int)EShadowCascadesOpts.FOUR_CASCADES;
				a_oRenderingPipeline.colorGradingLutSize = sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE;
				a_oRenderingPipeline.shaderVariantLogLevel = ShaderVariantLogLevel.AllShaders;

				a_oRenderingPipeline.renderScale = KCDefine.B_VAL_1_FLT;
				a_oRenderingPipeline.shadowDistance = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE / KCDefine.B_VAL_2_FLT;
				a_oRenderingPipeline.shadowDepthBias = KCDefine.B_VAL_1_FLT;
				a_oRenderingPipeline.shadowNormalBias = KCDefine.B_VAL_1_FLT;

				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_SOFT_SHADOW, true);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_SUPPORTS_TERRAIN_HOLES, true);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SUPPORTS_SHADOW, true);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SUPPORTS_SHADOW, true);

				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BLENDING, false);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BOX_PROJECTION, false);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_USE_FAST_SRGB_LINEAR_CONVERSION, false);

				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MSAA_QUALITY, MsaaQuality.Disabled);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_OPAQUE_DOWN_SAMPLING, Downsampling._4xBilinear);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_RENDERING_MODE, LightRenderingMode.PerPixel);

				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_FMT, LightCookieFormat.ColorHDR);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ_LIMIT, KCDefine.B_VAL_8_INT);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_RENDERING_MODE, LightRenderingMode.PerVertex);

				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_BORDER, KCDefine.B_VAL_2_FLT / KCDefine.B_UNIT_DIGITS_PER_TEN);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_2_SPLIT, KCEditorDefine.U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_3_SPLIT, KCEditorDefine.B_EDITOR_OPTS_CASCADE_3_SPLIT_PERCENT);
				a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_CASCADE_4_SPLIT, KCEditorDefine.B_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT);
			}

			// 옵션 정보 테이블이 존재 할 경우
			if(CPlatformOptsSetter.OptsInfoTable != null) {
				var stRenderingOptsInfo = CPlatformOptsSetter.OptsInfoTable.GetRenderingOptsInfo(a_eQualityLevel);

				var oIsSetupRenderingOptsList = new List<bool>() {
					a_oRenderingPipeline.useSRPBatcher == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsUseSRPBatching,

					(LightCookieResolution)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_RESOLUTION) == (LightCookieResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eLightCookieResolution,
					(UnityEngine.Rendering.Universal.ShadowResolution)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SHADOW_MAP_RESOLUTION) == (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eMainLightShadowResolution,
					(UnityEngine.Rendering.Universal.ShadowResolution)a_oRenderingPipeline.ExGetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SHADOW_MAP_RESOLUTION) == (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eAdditionalShadowResolution
				};

				// 설정 갱신이 필요 할 경우
				if(oIsSetupRenderingOptsList.Contains(false)) {
					a_oRenderingPipeline.useSRPBatcher = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsUseSRPBatching;

					a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_RESOLUTION, (LightCookieResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eLightCookieResolution);
					a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SHADOW_MAP_RESOLUTION, (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eMainLightShadowResolution);
					a_oRenderingPipeline.ExSetRuntimeFieldVal<UniversalRenderPipelineAsset>(KCEditorDefine.U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SHADOW_MAP_RESOLUTION, (UnityEngine.Rendering.Universal.ShadowResolution)stRenderingOptsInfo.m_stUniversalRPOptsInfo.m_eAdditionalShadowResolution);
				}
			}
		}
	}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}

/** 플랫폼 옵션 설정자 - iOS 설정 */
public static partial class CPlatformOptsSetter {
	#region 클래스 함수
	/** iOS 옵션을 설정한다 */
	private static void SetupiOSOpts() {
		PlayerSettings.iOS.SetiPadLaunchScreenType(iOSLaunchScreenType.Default);
		PlayerSettings.iOS.SetiPhoneLaunchScreenType(iOSLaunchScreenType.Default);

		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			var oIsSetupOptsList = new List<bool>() {
				PlayerSettings.iOS.appleEnableAutomaticSigning == false
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				PlayerSettings.iOS.appleEnableAutomaticSigning = false;
			}
		}
	}

	/** iOS 빌드 옵션을 설정한다 */
	private static void SetupiOSBuildOpts() {
		var oIsSetupOptsList = new List<bool>() {
			PlayerSettings.iOS.allowHTTPDownload,
			PlayerSettings.iOS.requiresFullScreen,

			PlayerSettings.iOS.hideHomeButton == false,
			PlayerSettings.iOS.useOnDemandResources == false,
			PlayerSettings.iOS.requiresPersistentWiFi == false,
			PlayerSettings.iOS.forceHardShadowsOnMetal == false,
			PlayerSettings.iOS.disableDepthAndStencilBuffers == false,

			PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK,
			PlayerSettings.iOS.targetDevice == iOSTargetDevice.iPhoneAndiPad,
			PlayerSettings.iOS.statusBarStyle == iOSStatusBarStyle.Default,

			PlayerSettings.iOS.scriptCallOptimization == ScriptCallOptimizationLevel.SlowAndSafe,
			PlayerSettings.iOS.appInBackgroundBehavior == iOSAppInBackgroundBehavior.Suspend,
			PlayerSettings.iOS.showActivityIndicatorOnLoading == iOSShowActivityIndicatorOnLoading.WhiteLarge,

			EditorUserBuildSettings.symlinkSources == false,

			PlayerSettings.GetArchitecture(BuildTargetGroup.iOS) == (int)AppleMobileArchitecture.ARM64,
			(bool)CAccessExtension.ExGetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_AUTO_ADD_CAPABILITIES, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC),
			(bool)CAccessExtension.ExGetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC) == false,

#if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE
			PlayerSettings.iOS.backgroundModes == (iOSBackgroundMode.Fetch | iOSBackgroundMode.RemoteNotification)
#else
			PlayerSettings.iOS.backgroundModes == iOSBackgroundMode.None
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			PlayerSettings.iOS.allowHTTPDownload = true;
			PlayerSettings.iOS.requiresFullScreen = true;

			PlayerSettings.iOS.hideHomeButton = false;
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
			CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, false);

#if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE
			PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.Fetch | iOSBackgroundMode.RemoteNotification;
#else
			PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.None;
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_CLOUD_MSG_ENABLE
		}

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
			var oIsSetupBuildOptsList = new List<bool>() {
				// iOS
				PlayerSettings.iOS.appleDeveloperTeamID.Equals(CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTeamID),
				PlayerSettings.iOS.targetOSVersionString.Equals(CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTargetOSVer),

				// 안드로이드
				PlayerSettings.Android.minSdkVersion == CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eMinSDKVer,
				PlayerSettings.Android.targetSdkVersion == CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eTargetSDKVer
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupBuildOptsList.Contains(false)) {
				// iOS
				PlayerSettings.iOS.appleDeveloperTeamID = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTeamID;
				PlayerSettings.iOS.targetOSVersionString = CPlatformOptsSetter.BuildInfoTable.iOSBuildInfo.m_oTargetOSVer;

				// 안드로이드
				PlayerSettings.Android.minSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eMinSDKVer;
				PlayerSettings.Android.targetSdkVersion = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_eTargetSDKVer;
			}
		}

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			var oIsSetupBuildOptsList = new List<bool>() {
				// iOS {
				(bool)CAccessExtension.ExGetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC) == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_bIsEnableProMotion,

				PlayerSettings.iOS.cameraUsageDescription.Equals(CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oCameraDesc),
				PlayerSettings.iOS.locationUsageDescription.Equals(CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oLocationDesc),
				PlayerSettings.iOS.microphoneUsageDescription.Equals(CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oMicrophoneDesc),
				// iOS }

				// 안드로이드
				PlayerSettings.Android.useAPKExpansionFiles == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stAndroidBuildOptsInfo.m_bIsUseAPKExpansionFiles
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupBuildOptsList.Contains(false)) {
				// iOS {
				CAccessExtension.ExSetPropertyVal<PlayerSettings.iOS>(null, KCEditorDefine.B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_bIsEnableProMotion);

				PlayerSettings.iOS.cameraUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oCameraDesc;
				PlayerSettings.iOS.locationUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oLocationDesc;
				PlayerSettings.iOS.microphoneUsageDescription = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stiOSBuildOptsInfo.m_oMicrophoneDesc;
				// iOS }

				// 안드로이드
				PlayerSettings.Android.useAPKExpansionFiles = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_stAndroidBuildOptsInfo.m_bIsUseAPKExpansionFiles;
			}
		}
	}
	#endregion			// 클래스 함수
}

/** 플랫폼 옵션 설정자 - 안드로이드 설정 */
public static partial class CPlatformOptsSetter {
	#region 클래스 함수
	/** 안드로이드 옵션을 설정한다 */
	private static void SetupAndroidOpts() {
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			var oIsSetupOptsList = new List<bool>() {
				PlayerSettings.Android.useCustomKeystore,

				PlayerSettings.Android.keystoreName.Equals(CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePath),
				PlayerSettings.Android.keyaliasName.Equals(CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasName),

				PlayerSettings.Android.keystorePass.Equals(CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePassword),
				PlayerSettings.Android.keyaliasPass.Equals(CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasPassword)
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				PlayerSettings.Android.useCustomKeystore = true;

				PlayerSettings.Android.keystoreName = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePath;
				PlayerSettings.Android.keyaliasName = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasName;

				PlayerSettings.Android.keystorePass = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeystorePassword;
				PlayerSettings.Android.keyaliasPass = CPlatformOptsSetter.BuildInfoTable.AndroidBuildInfo.m_oKeyaliasPassword;
			}
		}
	}

	/** 안드로이드 빌드 옵션을 설정한다 */
	private static void SetupAndroidBuildOpts() {
		var oIsSetupOptsList = new List<bool>() {
			PlayerSettings.Android.androidIsGame,
			PlayerSettings.Android.startInFullscreen,
			PlayerSettings.Android.optimizedFramePacing,
			PlayerSettings.Android.renderOutsideSafeArea,
			PlayerSettings.Android.forceInternetPermission,

			PlayerSettings.Android.minifyDebug == false,
			PlayerSettings.Android.minifyWithR8 == false,
			PlayerSettings.Android.minifyRelease == false,

			PlayerSettings.Android.forceSDCardPermission == false,
			PlayerSettings.Android.androidTVCompatibility == false,
			PlayerSettings.Android.buildApkPerCpuArchitecture == false,
			PlayerSettings.Android.disableDepthAndStencilBuffers == false,

			PlayerSettings.Android.blitType == AndroidBlitType.Always,
			PlayerSettings.Android.fullscreenMode == FullScreenMode.FullScreenWindow,
			PlayerSettings.Android.splashScreenScale == AndroidSplashScreenScale.ScaleToFit,
			PlayerSettings.Android.androidTargetDevices == AndroidTargetDevices.PhonesTabletsAndTVDevicesOnly,
			PlayerSettings.Android.preferredInstallLocation == AndroidPreferredInstallLocation.PreferExternal,
			PlayerSettings.Android.showActivityIndicatorOnLoading == AndroidShowActivityIndicatorOnLoading.InversedLarge,
			PlayerSettings.Android.targetArchitectures == (AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64),

			EditorUserBuildSettings.exportAsGoogleAndroidProject == false,

			EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle,
			EditorUserBuildSettings.androidETC2Fallback == AndroidETC2Fallback.Quality32Bit,
			EditorUserBuildSettings.androidCreateSymbols == AndroidCreateSymbols.Public,

			(bool)CAccessExtension.ExGetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC),

			(int)CAccessExtension.ExGetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC) == (int)EAspectRatioMode.NATIVE_ASPECT_RATIO,
			(int)CAccessExtension.ExGetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC) == KCEditorDefine.B_UNIT_VALIDATE_APP_BUNDLE_SIZE
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			PlayerSettings.Android.androidIsGame = true;
			PlayerSettings.Android.startInFullscreen = true;
			PlayerSettings.Android.optimizedFramePacing = true;
			PlayerSettings.Android.renderOutsideSafeArea = true;
			PlayerSettings.Android.forceInternetPermission = true;

			PlayerSettings.Android.minifyDebug = false;
			PlayerSettings.Android.minifyWithR8 = false;
			PlayerSettings.Android.minifyRelease = false;

			PlayerSettings.Android.forceSDCardPermission = false;
			PlayerSettings.Android.androidTVCompatibility = false;
			PlayerSettings.Android.buildApkPerCpuArchitecture = false;
			PlayerSettings.Android.disableDepthAndStencilBuffers = false;

			PlayerSettings.Android.blitType = AndroidBlitType.Always;
			PlayerSettings.Android.fullscreenMode = FullScreenMode.FullScreenWindow;
			PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFit;
			PlayerSettings.Android.androidTargetDevices = AndroidTargetDevices.PhonesTabletsAndTVDevicesOnly;
			PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
			PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.InversedLarge;
			PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;

			EditorUserBuildSettings.exportAsGoogleAndroidProject = false;

			EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
			EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
			EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Public;

			CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, true);

			CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, (int)EAspectRatioMode.NATIVE_ASPECT_RATIO);
			CAccessExtension.ExSetPropertyVal<PlayerSettings.Android>(null, KCEditorDefine.B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE, KCDefine.B_BINDING_F_NON_PUBLIC_STATIC, KCEditorDefine.B_UNIT_VALIDATE_APP_BUNDLE_SIZE);
		}

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
	}
	#endregion			// 클래스 함수
}

/** 플랫폼 옵션 설정자 - 독립 플래폼 설정 */
public static partial class CPlatformOptsSetter {
	#region 클래스 함수
	/** 독립 플랫폼 옵션을 설정한다 */
	private static void SetupStandaloneOpts() {
		// 빌드 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.BuildInfoTable != null) {
			var oIsSetupOptsList = new List<bool>() {
				((string)CAccessExtension.ExGetRuntimePropertyVal<PlayerSettings.macOS>(null, KCEditorDefine.B_PROPERTY_N_CATEGORY)).Equals(CPlatformOptsSetter.BuildInfoTable.StandaloneBuildInfo.m_oCategory)
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupOptsList.Contains(false)) {
				CAccessExtension.ExSetRuntimePropertyVal<PlayerSettings.macOS>(null, KCEditorDefine.B_PROPERTY_N_CATEGORY, CPlatformOptsSetter.BuildInfoTable.StandaloneBuildInfo.m_oCategory);
			}
		}
	}

	/** 독립 플랫폼 빌드 옵션을 설정한다 */
	private static void SetupStandaloneBuildOpts() {
		var oIsSetupOptsList = new List<bool>() {
			PlayerSettings.MTRendering,
			PlayerSettings.gpuSkinning,
			PlayerSettings.macRetinaSupport,
			PlayerSettings.assemblyVersionValidation,
			
			PlayerSettings.usePlayerLog,
			PlayerSettings.mipStripping,
			PlayerSettings.gcIncremental,
			PlayerSettings.statusBarHidden,

			PlayerSettings.stripEngineCode,
			PlayerSettings.runInBackground,
			PlayerSettings.forceSingleInstance,
			PlayerSettings.visibleInBackground,
			PlayerSettings.useFlipModelSwapchain,
			PlayerSettings.enable360StereoCapture,

			PlayerSettings.enableCrashReportAPI,
			PlayerSettings.use32BitDisplayBuffer,
			PlayerSettings.enableFrameTimingStats,
			PlayerSettings.enableMetalAPIValidation,

			PlayerSettings.stripUnusedMeshComponents,
			PlayerSettings.defaultIsNativeResolution,
			PlayerSettings.logObjCUncaughtExceptions,

			PlayerSettings.graphicsJobs == false,
			PlayerSettings.resizableWindow == false,
			PlayerSettings.allowUnsafeCode == false,
			PlayerSettings.captureSingleScreen == false,
			
			PlayerSettings.allowFullscreenSwitch == false,
			PlayerSettings.muteOtherAudioSources == false,
			PlayerSettings.enableInternalProfiler == false,
			PlayerSettings.useMacAppStoreValidation == false,
			PlayerSettings.legacyClampBlendShapeWeights == false,

			PlayerSettings.defaultScreenWidth == KCDefine.B_LANDSCAPE_SCREEN_WIDTH,
			PlayerSettings.defaultScreenHeight == KCDefine.B_LANDSCAPE_SCREEN_HEIGHT,

			PlayerSettings.accelerometerFrequency == (int)EAccelerometerFrequency.FREQUENCY_60_HZ,
			PlayerSettings.actionOnDotNetUnhandledException == ActionOnDotNetUnhandledException.Crash,

			PlayerSettings.GetWsaHolographicRemotingEnabled() == false,
			PlayerSettings.GetVirtualTexturingSupportEnabled() == false,

			PlayerSettings.GetShaderPrecisionModel() == ShaderPrecisionModel.PlatformDefault,
			EditorUserBuildSettings.overrideMaxTextureSize == KCDefine.B_VAL_0_INT,

			EditorUserBuildSettings.il2CppCodeGeneration == Il2CppCodeGeneration.OptimizeSpeed,
			EditorUserBuildSettings.overrideTextureCompression == OverrideTextureCompression.NoOverride,

#if DEBUG || DEVELOPMENT_BUILD
			PlayerSettings.fullScreenMode == FullScreenMode.Windowed
#else
			PlayerSettings.fullScreenMode == FullScreenMode.FullScreenWindow
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			PlayerSettings.MTRendering = true;
			PlayerSettings.gpuSkinning = true;
			PlayerSettings.macRetinaSupport = true;
			PlayerSettings.assemblyVersionValidation = true;
			
			PlayerSettings.usePlayerLog = true;
			PlayerSettings.mipStripping = true;
			PlayerSettings.gcIncremental = true;
			PlayerSettings.statusBarHidden = true;

			PlayerSettings.stripEngineCode = true;
			PlayerSettings.runInBackground = true;
			PlayerSettings.forceSingleInstance = true;
			PlayerSettings.visibleInBackground = true;
			PlayerSettings.useFlipModelSwapchain = true;
			PlayerSettings.enable360StereoCapture = true;

			PlayerSettings.enableCrashReportAPI = true;
			PlayerSettings.use32BitDisplayBuffer = true;
			PlayerSettings.enableFrameTimingStats = true;
			PlayerSettings.enableMetalAPIValidation = true;

			PlayerSettings.stripUnusedMeshComponents = true;
			PlayerSettings.defaultIsNativeResolution = true;
			PlayerSettings.logObjCUncaughtExceptions = true;

			PlayerSettings.graphicsJobs = false;
			PlayerSettings.resizableWindow = false;
			PlayerSettings.allowUnsafeCode = false;
			PlayerSettings.captureSingleScreen = false;
			
			PlayerSettings.allowFullscreenSwitch = false;
			PlayerSettings.muteOtherAudioSources = false;
			PlayerSettings.enableInternalProfiler = false;
			PlayerSettings.useMacAppStoreValidation = false;
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
			PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
#else
			PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow;
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		}

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

		// 옵션 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.OptsInfoTable != null) {
			var oIsSetupBuildOptsList = new List<bool>() {
				PlayerSettings.bakeCollisionMeshes == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreBakeCollisionMesh,
				PlayerSettings.preserveFramebufferAlpha == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreserveFrameBufferAlpha,

				PlayerSettings.colorSpace == ColorSpace.Linear,

				PlayerSettings.GetNormalMapEncoding(BuildTargetGroup.iOS) == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding,
				PlayerSettings.GetNormalMapEncoding(BuildTargetGroup.Android) == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding,
				PlayerSettings.GetNormalMapEncoding(BuildTargetGroup.Standalone) == CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupBuildOptsList.Contains(false)) {
				PlayerSettings.bakeCollisionMeshes = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreBakeCollisionMesh;
				PlayerSettings.preserveFramebufferAlpha = CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_bIsPreserveFrameBufferAlpha;

				PlayerSettings.colorSpace = ColorSpace.Linear;

				PlayerSettings.SetNormalMapEncoding(BuildTargetGroup.iOS, CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding);
				PlayerSettings.SetNormalMapEncoding(BuildTargetGroup.Android, CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding);
				PlayerSettings.SetNormalMapEncoding(BuildTargetGroup.Standalone, CPlatformOptsSetter.OptsInfoTable.BuildOptsInfo.m_eNormapMapEncoding);
			}
		}

		// 프로젝트 정보 테이블이 존재 할 경우
		if(CPlatformOptsSetter.ProjInfoTable != null) {
			var oIsSetupBuildOptsList = new List<bool>() {
				PlayerSettings.companyName.Equals(CPlatformOptsSetter.ProjInfoTable.CompanyInfo.m_oCompany),
				PlayerSettings.productName.Equals(CPlatformOptsSetter.ProjInfoTable.CommonProjInfo.m_oProductName)
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupBuildOptsList.Contains(false)) {
				PlayerSettings.companyName = CPlatformOptsSetter.ProjInfoTable.CompanyInfo.m_oCompany;
				PlayerSettings.productName = CPlatformOptsSetter.ProjInfoTable.CommonProjInfo.m_oProductName;
			}
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
