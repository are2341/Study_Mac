using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

#if NOTI_MODULE_ENABLE
using Unity.Notifications.iOS;
#endif			// #if NOTI_MODULE_ENABLE

//! 에디터 기본 상수
public static partial class KCEditorDefine {
	#region 기본
	// 단위
	public const int B_UNIT_CUSTOM_TAG_START_ID = 10;

	// 시간
	public const float B_DELTA_T_HIERARCHY_UPDATE = 1.0f;
	public const float B_DELTA_T_SCENE_M_SCRIPT_UPDATE = 1.0f;
	public const float B_DELTA_T_EDITOR_SM_SCENE_UPDATE = 1.0f;

	// 토큰
	public const string B_TOKEN_DEFINE_SYMBOL = ";";

	// 형식
	public const string B_SORTING_OI_FMT = "[{0}:{1}]";

	// 버전
	public const string B_VER_UNITY_MODULE = "1.6.1";

	// 커맨드 라인
	public const string B_CMD_LINE_PARAMS_FMT_SHELL = "-c \"{0}\"";
	public const string B_CMD_LINE_PARAMS_FMT_CMD_PROMPT = "/c \"{0}\"";

	// 이름 {
	public const string B_MODULE_N_INPUT_SYSTEM = "com.unity.input.settings";

	public const string B_OBJ_N_GAME_OBJ = "GameObj";
	public const string B_OBJ_N_OBJ_N_EDITOR_POPUP = "ObjNameEditorPopup";

	public const string B_OBJ_N_TEXT = "Text";
	public const string B_OBJ_N_LOCALIZE_TEXT = "LocalizeText";

	public const string B_OBJ_N_IMG = "Img";
	public const string B_OBJ_N_RAW_IMG = "RawImg";

	public const string B_OBJ_N_TEXT_BTN = "TextBtn";
	public const string B_OBJ_N_TEXT_SCALE_BTN = "TextScaleBtn";

	public const string B_OBJ_N_LOCALIZE_TEXT_BTN = "LocalizeTextBtn";
	public const string B_OBJ_N_LOCALIZE_TEXT_SCALE_BTN = "LocalizeTextScaleBtn";

	public const string B_OBJ_N_IMG_BTN = "ImgBtn";
	public const string B_OBJ_N_IMG_SCALE_BTN = "ImgScaleBtn";

	public const string B_OBJ_N_IMG_TEXT_BTN = "ImgTextBtn";
	public const string B_OBJ_N_IMG_TEXT_SCALE_BTN = "ImgTextScaleBtn";

	public const string B_OBJ_N_IMG_LOCALIZE_TEXT_BTN = "ImgLocalizeTextBtn";
	public const string B_OBJ_N_IMG_LOCALIZE_TEXT_SCALE_BTN = "ImgLocalizeTextScaleBtn";

	public const string B_OBJ_N_PAGE_VIEW = "PageView";
	public const string B_OBJ_N_SCROLL_VIEW = "ScrollView";
	public const string B_OBJ_N_RECYCLE_VIEW = "RecycleView";

	public const string B_OBJ_N_PARTICLE_FX = "ParticleFX";
	public const string B_OBJ_N_SPRITE = "Sprite";

	public const string B_OBJ_N_TOUCH_RESPONDER = "TouchResponder";
	public const string B_OBJ_N_DRAG_RESPONDER = "DragResponder";

	public const string B_OBJ_N_SCENE_EDITOR_CAMERA = "SceneCamera";
	public const string B_OBJ_N_SCENE_EDITOR_LIGHT = "SceneLight";
	
	public const string B_PROPERTY_N_CATEGORY = "applicationCategoryType";
	public const string B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS = "requiresARKitSupport";
	public const string B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION = "appleEnableProMotion";
	public const string B_PROPERTY_N_AUTO_ADD_CAPABILITIES = "automaticallyDetectAndAddCapabilities";
	public const string B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE = "validateAppBundleSize";
	public const string B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE = "appBundleSizeToValidate";
	public const string B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE = "supportedAspectRatioMode";

	public const string B_PROPERTY_N_SORTING_LAYER = "sortingLayerName";
	public const string B_PROPERTY_N_SORTING_ORDER = "sortingOrder";

	public const string B_PROPERTY_N_TAG_M_TAG = "tags";
	public const string B_PROPERTY_N_TAG_M_NAME = "name";
	public const string B_PROPERTY_N_TAG_M_UNIQUE_ID = "uniqueID";
	public const string B_PROPERTY_N_TAG_M_SORTING_LAYER = "m_SortingLayers";

	public const string B_PROPERTY_N_SND_M_GLOBAL_VOLUME = "m_Volume";
	public const string B_PROPERTY_N_SND_M_ROLLOFF_SCALE = "Rolloff Scale";
	public const string B_PROPERTY_N_SND_M_DOPPLER_FACTOR = "Doppler Factor";
	public const string B_PROPERTY_N_SND_M_DISABLE_AUDIO = "m_DisableAudio";
	public const string B_PROPERTY_N_SND_M_VIRTUALIZE_EFFECT = "m_VirtualizeEffects";

	public const string B_SCENE_N_PATTERN = "t:Example t:Scene";

	public const string B_FUNC_N_SET_COMPRESSION_TYPE = "SetCompressionType";
	public const string B_FUNC_N_SET_LIGHTMAP_ENCODING_QUALITY = "SetLightmapEncodingQualityForPlatformGroup";
	public const string B_FUNC_N_SET_LIGHTMAP_STREAMING_ENABLE = "SetLightmapStreamingEnabledForPlatformGroup";
	public const string B_FUNC_N_SET_LIGHTMAP_STREAMING_PRIORITY = "SetLightmapStreamingPriorityForPlatformGroup";
	// 이름 }

	// 경로 {
	public const string B_TOOL_P_SHELL = "/bin/zsh";
	public const string B_TOOL_P_CMD_PROMPT = "cmd.exe";

	public const string B_DIR_P_ASSETS = "Assets/";
	public const string B_DIR_P_PACKAGES = "Packages/";
	public const string B_DIR_P_AUTO_CREATE = "00.AutoCreate/";
	public const string B_DIR_P_SUB_AUTO_CREATE = "01.SubAutoCreate/";
	public const string B_DIR_P_UNITY_PROJ = "02.UnityProject/";
	public const string B_DIR_P_UNITY_PROJ_EDITOR = "03.UnityProjectEditor/";
	public const string B_DIR_P_PROJ_SETTINGS = "ProjectSettings/";
	public const string B_DIR_P_EDITOR_DEF_RESES = "Editor Default Resources/";

	public const string B_DIR_P_EXPORT_IMG_BASE = "Export/Images/";
	public const string B_ASSET_P_FMT_LIGHTING_SETTINGS = "{0}/{1}Settings.lighting";
	// 경로 }

	// 맥
	public const string B_BUILD_P_MAC = "Builds/Standalone/Mac/MacBuildOutput.app";

	// 윈도우즈
	public const string B_BUILD_P_WNDS = "Builds/Standalone/Wnds/WndsBuildOutput.exe";

	// iOS {
	public const string B_TEXT_IOS_YES = "YES";
	public const string B_TEXT_IOS_NO = "NO";

	public const string B_IPA_EXPORT_METHOD_IOS_DEV = "development";
	public const string B_IPA_EXPORT_METHOD_IOS_ADHOC = "ad-hoc";
	public const string B_IPA_EXPORT_METHOD_IOS_STORE = "app-store";

	public const string B_BUILD_P_IOS = "Builds/iOS";
	public const string B_PLIST_P_FMT_IOS = "{0}/Info.plist";

	public const string B_KEY_IOS_ENCRYPTION_ENABLE = "ITSAppUsesNonExemptEncryption";
	public const string B_KEY_IOS_USER_TRACKING_USAGE_DESC = "NSUserTrackingUsageDescription";

	public const string B_KEY_IOS_ADS_NETWORK_ITEMS = "SKAdNetworkItems";
	public const string B_KEY_IOS_ADS_NETWORK_ID = "SKAdNetworkIdentifier";

	public const string B_PROPERTY_N_IOS_ENABLE_BITCODE = "ENABLE_BITCODE";
	public const string B_PROPERTY_N_IOS_PREPROCESSOR_DEFINITIONS = "GCC_PREPROCESSOR_DEFINITIONS";
	// iOS }

	// 안드로이드 {
	public const string B_BUILD_FILE_EXTENSION_ANDROID_APK = "apk";
	public const string B_BUILD_FILE_EXTENSION_ANDROID_AAB = "aab";

	public const string B_BUILD_P_FMT_ANDROID = "Builds/Android/{0}/{1}.{2}";
	public const string B_BUILD_FILE_N_FMT_ANDROID = "{0}BuildOutput";
	// 안드로이드 }

	// 젠킨스 {
	public const string B_KEY_JENKINS_ENGINE_VER = "EngineVer";
	public const string B_KEY_JENKINS_BRANCH = "Branch";
	public const string B_KEY_JENKINS_SRC = "Src";
	public const string B_KEY_JENKINS_PROJ_NAME = "ProjName";
	public const string B_KEY_JENKINS_PROJ_PATH = "ProjPath";
	public const string B_KEY_JENKINS_BUNDLE_ID = "BundleID";
	public const string B_KEY_JENKINS_PROFILE_ID = "ProfileID";
	public const string B_KEY_JENKINS_PLATFORM = "Platform";
	public const string B_KEY_JENKINS_ANALYTICS = "Analytics";
	public const string B_KEY_JENKINS_BUILD_MODE = "BuildMode";
	public const string B_KEY_JENKINS_BUILD_VER = "BuildVer";
	public const string B_KEY_JENKINS_BUILD_FUNC = "BuildFunc";
	public const string B_KEY_JENKINS_PIPELINE_NAME = "PipelineName";
	public const string B_KEY_JENKINS_IPA_EXPORT_METHOD = "IPAExportMethod";
	public const string B_KEY_JENKINS_BUILD_FILE_EXTENSION = "BuildFileExtension";

	public const string B_BUILD_PARAMS_TOKEN_JENKINS = " ";
	public const string B_PIPELINE_GROUP_NAME_FMT_JENKINS = "job/000001.Common/job/{0}/job/01.Pipelines/job";

	public const string B_BUILD_DATA_FMT_JENKINS = "--data {0}={1}";
	public const string B_BUILD_CMD_FMT_JENKINS = "curl -X POST {0} --user {1}:{2} --data token={3}";

	public const string B_BRANCH_FMT_JENKINS = "origin/{0}";
	public const string B_SRC_FMT_JENKINS = "{0}/{1}";
	public const string B_PROJ_P_FMT_JENKINS = "{0}/{1}/{2}";
	public const string B_ANALYTICS_FMT_JENKINS = "{0}/00.Analytics";

	public const string B_STANDALONE_BUILD_PROJ_N_JENKINS = "41.Standalone";
	public const string B_IOS_BUILD_PROJ_N_JENKINS = "01.iOS";
	public const string B_ANDROID_BUILD_PROJ_N_JENKINS = "11.Android";

	public const string B_DEBUG_BUILD_FUNC_JENKINS = "Debug";
	public const string B_RELEASE_BUILD_FUNC_JENKINS = "Release";
	public const string B_ADHOC_BUILD_FUNC_JENKINS = "Adhoc";
	public const string B_ADHOC_UPLOAD_BUILD_FUNC_JENKINS = "AdhocUpload";
	public const string B_STORE_BUILD_FUNC_JENKINS = "Store";
	public const string B_STORE_TEST_BUILD_FUNC_JENKINS = "StoreTest";
	public const string B_STORE_UPLOAD_BUILD_FUNC_JENKINS = "StoreUpload";

	public const string B_STANDALONE_DEBUG_PIPELINE_N_JENKINS = "41.StandaloneDebug";
	public const string B_STANDALONE_RELEASE_PIPELINE_N_JENKINS = "42.StandaloneRelease";

	public const string B_IOS_DEBUG_PIPELINE_N_JENKINS = "01.iOSDebug";
	public const string B_IOS_RELEASE_PIPELINE_N_JENKINS = "02.iOSRelease";
	public const string B_IOS_ADHOC_PIPELINE_N_JENKINS = "03.iOSAdhoc";
	public const string B_IOS_STORE_PIPELINE_N_JENKINS = "04.iOSStore";

	public const string B_ANDROID_DEBUG_PIPELINE_N_JENKINS = "11.AndroidDebug";
	public const string B_ANDROID_RELEASE_PIPELINE_N_JENKINS = "12.AndroidRelease";
	public const string B_ANDROID_ADHOC_PIPELINE_N_JENKINS = "13.AndroidAdhoc";
	public const string B_ANDROID_STORE_PIPELINE_N_JENKINS = "14.AndroidStore";
	// 젠킨스 }

	// 계층 뷰
	public const float B_OFFSET_HIERARCHY_TEXT = 15.0f;
	public const float B_OFFSET_HIERARCHY_OUTLINE = 1.0f;

	// 에디터 옵션 {
	public const int B_EDITOR_OPTS_GAME_OBJ_NAMING_DIGITS = 2;

	public const string B_EDITOR_OPTS_IOS_REMOTE_DEVICE = "Any iOS Device";
	public const string B_EDITOR_OPTS_ANDROID_REMOTE_DEVICE = "Any Android Device";
	public const string B_EDITOR_OPTS_DISABLE_REMOTE_DEVICE = "None";

	public const string B_EDITOR_OPTS_REMOTE_COMPRESSION = "JPEG";
	public const string B_EDITOR_OPTS_REMOTE_RESOLUTION = "Downsize";
	public const string B_EDITOR_OPTS_VER_CONTROL = "Visible Meta Files";
	public const string B_EDITOR_OPTS_JOYSTIC_SRC = "Remote";

#if GPU_LIGHTMAPPER_ENABLE
	public const LightingSettings.Lightmapper B_EDITOR_OPTS_LIGHTMAPPER = LightingSettings.Lightmapper.ProgressiveGPU;
#else
	public const LightingSettings.Lightmapper B_EDITOR_OPTS_LIGHTMAPPER = LightingSettings.Lightmapper.ProgressiveCPU;
#endif			// #if GPU_LIGHTMAPPER_ENABLE

#if LIGHTMAP_SHADOW_BAKE_ENABLE
#if LIGHTMAP_SHADOW_BAKE_MASK_MODE_ENABLE
	public const MixedLightingMode B_EDITOR_OPTS_LIGHTMAP_BAKE_MODE = MixedLightingMode.Shadowmask;
#else
	public const MixedLightingMode B_EDITOR_OPTS_LIGHTMAP_BAKE_MODE = MixedLightingMode.Subtractive;
#endif			// #if LIGHTMAP_SHADOW_BAKE_MASK_MODE_ENABLE
#else
	public const MixedLightingMode B_EDITOR_OPTS_LIGHTMAP_BAKE_MODE = MixedLightingMode.IndirectOnly;
#endif			// #if LIGHTMAP_SHADOW_BAKE_ENABLE

	public static readonly string[] B_EDITOR_OPTS_EXTENSIONS = new string[] {
		"txt", "xml", "fnt", "cd", "asmdef", "rsp", "asmref"
	};
	// 에디터 옵션 }

	// 경고 팝업 {
	public const string B_TEXT_ALERT_P_TITLE = "알림";
	public const string B_TEXT_ALERT_P_OK_BTN = "확인";
	public const string B_TEXT_ALERT_P_CANCEL_BTN = "취소";

	public const string B_MSG_ALERT_P_EXPORT_IMG_SUCCESS = "이미지를 추출했습니다.";

	public const string B_MSG_ALERT_P_EXPORT_TEXTURE_FAIL = "텍스처를 선택해주세요.";
	public const string B_MSG_ALERT_P_EXPORT_SPRITE_FAIL = "스프라이트를 선택해주세요.";
	// 경고 팝업 }

	// 객체 이름 에디터 윈도우 {
	public const float B_WIDTH_OBJ_NEW_APPLY_BTN = 100.0f;
	public const float B_WIDTH_OBJ_NEW_NAME_INPUT = 300.0f;

	public const string B_TEXT_OBJ_NEW_TARGET = "검색";
	public const string B_TEXT_OBJ_NEW_REPLACE = "변경";

	public const string B_TEXT_OBJ_NEW_APPLY_BTN = "적용";
	// 객체 이름 에디터 윈도우 }

	// FPS 카운터
	public const int B_FONT_SIZE_FPS_C_STATIC_TEXT = 24;
	public const int B_FONT_SIZE_FPS_C_DYNAMIC_TEXT = 24;

	// 디버그 콘솔
	public const int B_FONT_SIZE_DEBUG_C_TEXT = 30;
	public const string B_PROPERTY_N_DEBUG_C_LOG_ITEM_PREFAB = "logItemPrefab";
	#endregion			// 기본

	#region 런타임 상수
	// 색상
	public static readonly Color B_COLOR_UNITY_LOGO_BG = Color.black;

	// 크기
	public static readonly Vector3 B_MIN_SIZE_EDITOR_WND = new Vector3(350.0f, 350.0f, 0.0f);

	// 경로 {
	public static readonly string B_SCENE_P_INIT_SCENE = $"{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INIT}.unity";

	public static readonly string B_DIR_P_AUTO_CREATE_RESES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/";
	public static readonly string B_DIR_P_SUB_AUTO_CREATE_RESES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/";
	public static readonly string B_DIR_P_UNITY_PROJ_RESES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/";

	public static readonly string B_DIR_P_AUTO_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes";
	public static readonly string B_DIR_P_PROJ_AUTO_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes";

	public static readonly string B_DIR_P_PROJ_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes";
	public static readonly string B_DIR_P_PROJ_EDITOR_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scenes";

	public static readonly string B_ABS_DIR_P_ASSETS = $"{Application.dataPath}/";
	public static readonly string B_ABS_DIR_P_PACKAGES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Packages/";
	public static readonly string B_ABS_DIR_P_EXTERNAL_DATAS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../ExternalDatas/";
	public static readonly string B_ABS_DIR_P_TABLES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Tables/";
	public static readonly string B_ABS_DIR_P_UNITY_ENGINE = $"{EditorApplication.applicationPath}/";

#if SAMPLE_PROJ
	public static readonly string B_ABS_DIR_P_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon/Templates/";
	public static readonly string B_ABS_DIR_P_UNITY_PACKAGES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon/UnityPackages/";

	public static readonly string B_ABS_DIR_P_SAMPLE_SCENE = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon/Scenes/{KCDefine.B_SCENE_N_SAMPLE}.unity";
	public static readonly string B_ABS_DIR_P_STUDY_SAMPLE_SCENE = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy/Scenes/{KCDefine.B_SCENE_N_STUDY_SAMPLE}.unity";

	public static readonly string B_ABS_DIR_P_STUDY_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy/Templates/";
#else
	public static readonly string B_ABS_DIR_P_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon@{KCEditorDefine.B_VER_UNITY_MODULE}/Templates/";
	public static readonly string B_ABS_DIR_P_UNITY_PACKAGES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon@{KCEditorDefine.B_VER_UNITY_MODULE}/UnityPackages/";

	public static readonly string B_ABS_DIR_P_SAMPLE_SCENE = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon@{KCEditorDefine.B_VER_UNITY_MODULE}/Scenes/{KCDefine.B_SCENE_N_SAMPLE}.unity";
	public static readonly string B_ABS_DIR_P_STUDY_SAMPLE_SCENE = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy@{KCEditorDefine.B_VER_UNITY_MODULE}/Scenes/{KCDefine.B_SCENE_N_STUDY_SAMPLE}.unity";

	public static readonly string B_ABS_DIR_P_STUDY_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy@{KCEditorDefine.B_VER_UNITY_MODULE}/Templates/";
#endif			// #if SAMPLE_PROJ

	public static readonly string B_ABS_DIR_P_PLUGINS = $"{Application.dataPath}/Plugins/";
	public static readonly string B_ABS_DIR_P_IOS_PLUGINS = $"{KCEditorDefine.B_ABS_DIR_P_PLUGINS}iOS/";
	public static readonly string B_ABS_DIR_P_ANDROID_PLUGINS = $"{KCEditorDefine.B_ABS_DIR_P_PLUGINS}Android/";

	public static readonly string B_ABS_DIR_P_ICON_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Icons/";
	public static readonly string B_ABS_DIR_P_FONT_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Fonts/";
	public static readonly string B_ABS_DIR_P_DATA_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Datas/";
	public static readonly string B_ABS_DIR_P_PREFAB_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Prefabs/";
	public static readonly string B_ABS_DIR_P_SCRIPT_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Scripts/";
	public static readonly string B_ABS_DIR_P_SCRIPTABLE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Scriptables/";
	public static readonly string B_ABS_DIR_P_SPRITE_ATLAS_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}SpriteAtlases/";
	public static readonly string B_ABS_DIR_P_PIPELINE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Pipelines/";
	public static readonly string B_ABS_DIR_P_TABLE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Tables/";
	public static readonly string B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}AssemblyDefines/";

	public static readonly string B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_STUDY_TEMPLATES}Scripts/";

	public static readonly string B_ABS_DIR_P_SRC_PYTHON_SCRIPTS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python";
	public static readonly string B_ABS_DIR_P_DEST_PYTHON_SCRIPTS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Scripts/Python";
	
	public static readonly string B_ABS_PKGS_P_2D_TOOLKIT = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/2DToolkit-v2.5.8.unitypackage";
	public static readonly string B_ABS_PKGS_P_BITMAP_FONT_IMPORTER = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/BitmapFontImporter-v2.4.1.unitypackage";
	public static readonly string B_ABS_PKGS_P_BUILD_REPORT_TOOL = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/BuildReportTool-v3.8.2.unitypackage";
	public static readonly string B_ABS_PKGS_P_NGUI = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/NGUI-v2020.2.2.unitypackage";
	public static readonly string B_ABS_PKGS_P_ODIN_INSPECTOR = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/OdinInspector-v3.0.5.unitypackage";
	public static readonly string B_ABS_PKGS_P_ZENJECT = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/Zenject-v9.2.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_SPINE = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/Spine-v3.8.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_SPINE_URP_SHADERS = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/SpineURPShaders-v3.8.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_REST_CLIENT = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/RestClient-v2.6.1.unitypackage";
	public static readonly string B_ABS_PKGS_P_SPRITE_TRAIL = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/SpriteTrail-v1.4.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_ULTIMATE_STATUS_BAR = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/UltimateStatusBar-v2.6.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_LEAN_GUI = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/LeanGUI-v1.2.0.unitypackage";
	public static readonly string B_ABS_PKGS_P_LEAN_TOUCH = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PACKAGES}/LeanTouch-v2.2.1.unitypackage";

	public static readonly string B_ASSET_P_TAG_MANAGER = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}TagManager.asset";
	public static readonly string B_ASSET_P_SND_MANAGER = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}AudioManager.asset";

	public static readonly string B_ASSET_P_BUILD_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_BUILD_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_BUILD_OPTS_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_BUILD_OPTS_TABLE}.asset";
	public static readonly string B_ASSET_P_PROJ_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_PROJ_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_DEFINE_SYMBOL_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_DEFINE_SYMBOL_TABLE}.asset";
	public static readonly string B_ASSET_P_SALE_ITEM_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_SALE_ITEM_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_SALE_PRODUCT_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_SALE_PRODUCT_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_MISSION_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_MISSION_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_REWARD_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_REWARD_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_EPISODE_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESES}{KCDefine.U_ASSET_P_G_EPISODE_INFO_TABLE}.asset";

	public static readonly string B_ASSET_P_FMT_SCRIPTABLE_OBJ = $"{KCEditorDefine.B_DIR_P_ASSETS}{"{0}.asset"}";
	public static readonly string B_ASSET_P_FMT_DEFINE_S_OUTPUT = $"{KCDefine.B_DIR_P_WRITABLE}/BuildOutput/{"{0}DefineSymbol.txt"}";

	public static readonly string B_IMG_P_FMT_TEXTURE_TO_IMG = $"{KCEditorDefine.B_DIR_P_EXPORT_IMG_BASE}{"Textures/{0}.png"}";
	public static readonly string B_IMG_P_FMT_SPRITE_TO_IMG = $"{KCEditorDefine.B_DIR_P_EXPORT_IMG_BASE}{"Sprites/{0}.png"}";

	public static readonly string B_DATA_P_UNITY_PKGS = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}manifest.json";
	public static readonly string B_ICON_P_STANDALONE_APP = $"{KCDefine.B_DIR_P_ICONS}Standalone/App/Icon";

	public static readonly string B_ICON_P_IOS_APP_76x76 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon76x76";
	public static readonly string B_ICON_P_IOS_APP_120x120 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon120x120";
	public static readonly string B_ICON_P_IOS_APP_152x152 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon152x152";
	public static readonly string B_ICON_P_IOS_APP_167x167 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon167x167";
	public static readonly string B_ICON_P_IOS_APP_180x180 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon180x180";
	public static readonly string B_ICON_P_IOS_APP_1024x1024 = $"{KCDefine.B_DIR_P_ICONS}iOS/App/Icon1024x1024";

	public static readonly string B_ICON_P_IOS_NOTI_20x20 = $"{KCDefine.B_DIR_P_ICONS}iOS/Notification/Icon20x20";
	public static readonly string B_ICON_P_IOS_NOTI_40x40 = $"{KCDefine.B_DIR_P_ICONS}iOS/Notification/Icon40x40";
	public static readonly string B_ICON_P_IOS_NOTI_60x60 = $"{KCDefine.B_DIR_P_ICONS}iOS/Notification/Icon60x60";

	public static readonly string B_ICON_P_ANDROID_APP_36x36 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon36x36";
	public static readonly string B_ICON_P_ANDROID_APP_48x48 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon48x48";
	public static readonly string B_ICON_P_ANDROID_APP_72x72 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon72x72";
	public static readonly string B_ICON_P_ANDROID_APP_96x96 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon96x96";
	public static readonly string B_ICON_P_ANDROID_APP_144x144 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon144x144";
	public static readonly string B_ICON_P_ANDROID_APP_192x192 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon192x192";
	public static readonly string B_ICON_P_ANDROID_APP_512x512 = $"{KCDefine.B_DIR_P_ICONS}Android/App/Icon512x512";

	public static readonly string B_ICON_P_ANDROID_NOTI_24x24 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon24x24";
	public static readonly string B_ICON_P_ANDROID_NOTI_36x36 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon36x36";
	public static readonly string B_ICON_P_ANDROID_NOTI_48x48 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon48x48";
	public static readonly string B_ICON_P_ANDROID_NOTI_72x72 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon72x72";
	public static readonly string B_ICON_P_ANDROID_NOTI_96x96 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon96x96";
	public static readonly string B_ICON_P_ANDROID_NOTI_256x256 = $"{KCDefine.B_DIR_P_ICONS}Android/Notification/Icon256x256";

	public static readonly string[] B_SEARCH_P_SCENES = new string[] {
		KCEditorDefine.B_DIR_P_AUTO_SCENES,
		KCEditorDefine.B_DIR_P_PROJ_AUTO_SCENES,
		KCEditorDefine.B_DIR_P_PROJ_SCENES,

#if EDITOR_ENABLE && (UNITY_EDITOR || UNITY_STANDALONE)
		KCEditorDefine.B_DIR_P_PROJ_EDITOR_SCENES
#endif			// #if EDITOR_ENABLE && (UNITY_EDITOR || UNITY_STANDALONE)
	};

	public static readonly KeyValuePair<string, string>[] B_DATA_P_INFOS = new KeyValuePair<string, string>[] {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_Doxyfile", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Doxyfile"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}README.md"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}README.md"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}README.md"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}README.md"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESES}{KCEditorDefine.B_DIR_P_UNITY_PROJ}README.md"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESES}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}README.md"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_Services_{SystemLanguage.Korean}.txt", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.AS_DATA_P_SERVICES}.txt"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_Privacy_{SystemLanguage.Korean}.txt", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.AS_DATA_P_PRIVACY}.txt"),

#if FIREBASE_MODULE_ENABLE		
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_GameConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_DATA_P_G_GAME_CONFIG}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_BuildVerConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json")
#endif			// #if FIREBASE_MODULE_ENABLE
	};
	
	public static readonly KeyValuePair<string, string>[] B_SCRIPT_P_INFOS = new KeyValuePair<string, string>[] {
		// 00.AutoCreate {
		// 에디터 상수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Define/T_KEditorDefine+Abs.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Define/KEditorDefine+Abs.cs"),

		// 에디터 팩토리
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Factory/T_EditorFactory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Factory/EditorFactory.cs"),

		// 에디터 유틸리티
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Build/T_CBuildProcessor.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Utility/Build/CBuildProcessor.cs"),

		// 에디터 씬 관리자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Scene/T_CEditorSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/EditorScene/CEditorSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Scene/T_CEditorSceneManager+Setup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/EditorScene/CEditorSceneManager+Setup.cs"),

		// 상수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Abs.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+Abs.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+InitScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+InitScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SetupScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+SetupScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+AgreeScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+AgreeScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LateSetupScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+LateSetupScene.cs"),

		// 접근자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_Access.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Access/Access.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_AccessExtension.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Access/AccessExtension.cs"),

		// 팩토리
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Factory/Factory.cs"),

		// 확장 클래스
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Extension/T_Extension.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Extension/Extension.cs"),

		// 함수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Function/Func.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_LogFunc.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Function/LogFunc.cs"),

		// 유틸리티
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/External/T_CMsgPackRegister.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Utility/External/CMsgPackRegister.cs"),

		// 씬 관리자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubInitSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubInitScene/CSubInitSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubSetupSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubSetupScene/CSubSetupSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubAgreeSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubAgreeScene/CSubAgreeSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLateSetupSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubLateSetupScene/CSubLateSetupSceneManager.cs"),
		// 00.AutoCreate }

		// 01.SubAutoCreate {
		// 상수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+StartScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+StartScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SplashScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+SplashScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+PermissionScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+PermissionScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+IntroScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+IntroScene.cs"),

		// 효과
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/FX/T_CLevelModeCorrector.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Utility/UI/FX/CLevelModeCorrector.cs"),

		// 씬 관리자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubStartSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubStartScene/CSubStartSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubSplashSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubSplashScene/CSubSplashSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubPermissionSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubPermissionScene/CSubPermissionSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubIntroSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubIntroScene/CSubIntroSceneManager.cs"),

#if STUDY_ENABLE || STUDY_MODULE_ENABLE
		// 상수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+MenuScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+MenuScene.cs"),

		// 씬 관리자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubMenuSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubMenuScene/CSubMenuSceneManager.cs"),
#endif			// #if STUDY_ENABLE || STUDY_MODULE_ENABLE
		// 01.SubAutoCreate }

		// 02.UnityProject {
		// 에디터 팩토리
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Factory/T_EditorFactory+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Editor/Global/Factory/EditorFactory+Global.cs"),

		// 에셋 추가자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Importer/T_CSubAssetImporter.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Editor/Global/Importer/CSubAssetImporter.cs"),

		// 엔진 {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/CEngine.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Setup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/CEngine+Setup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Access.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/CEngine+Access.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Factory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/CEngine+Factory.cs"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+Engine.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+EngineType.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+EngineType.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+EngineEnum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+EngineEnum.cs"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Access/T_Access+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Access/Access+Engine.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Access/T_AccessExtension+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Access/AccessExtension+Engine.cs"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Factory/T_Factory+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Factory/Factory+Engine.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Extension/T_Extension+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Extension/Extension+Engine.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Function/T_Func+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Engine/Global/Function/Func+Engine.cs"),
		// 엔진 }

		// 상수 {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Type.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Type.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Enum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Enum.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Global.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Log.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Log.cs"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+ValTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+ValTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+StrTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+StrTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+GameCenter.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+GameCenter.cs"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+TitleScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+TitleScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+GameScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+GameScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+OverlayScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+OverlayScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LoadingScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+LoadingScene.cs"),
		// 상수 }

		// 접근자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_Access+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Access/Access+Global.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_AccessExtension+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Access/AccessExtension+Global.cs"),

		// 팩토리
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Factory/Factory+Global.cs"),

		// 확장 클래스
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Extension/T_Extension+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Extension/Extension+Global.cs"),

		// 함수
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Function/Func+Global.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_LogFunc+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Function/LogFunc+Global.cs"),

		// 기본
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CSubPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CMissionPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CMissionPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubAlertPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CSubAlertPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubToastPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CSubToastPopup.cs"),

		// 저장소
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CBaseInfo.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CBaseInfo.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CAppInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CAppInfoStorage.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CUserInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CUserInfoStorage.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CGameInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CGameInfoStorage.cs"),

		// 테이블
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CLevelInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CLevelInfoTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CSaleItemInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CSaleItemInfoTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CSaleProductInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CSaleProductInfoTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CMissionInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CMissionInfoTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CRewardInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CRewardInfoTable.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CEpisodeInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CEpisodeInfoTable.cs"),

		// 팝업
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CStorePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CStorePopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CSettingsPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CSettingsPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CDailyMissionPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CDailyMissionPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CRewardPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CRewardPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CFreeRewardPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CFreeRewardPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CDailyRewardPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CDailyRewardPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CFocusPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CFocusPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CTutorialPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CTutorialPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CChangesPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CChangesPopup.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CChangesAcquirePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Popup/CChangesAcquirePopup.cs"),

		// 씬 관리자
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubTitleSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubTitleScene/CSubTitleSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubGameSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubGameScene/CSubGameSceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubOverlaySceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubOverlayScene/CSubOverlaySceneManager.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLoadingSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubLoadingScene/CSubLoadingSceneManager.cs"),
		// 02.UnityProject }

		// 03.UnityProjectEditor
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LevelEditorScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Define/KDefine+LevelEditorScene.cs"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLevelEditorSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/SubLevelEditorScene/CSubLevelEditorSceneManager.cs")
	};
	
	public static readonly KeyValuePair<string, string>[] B_PREFAB_P_INFOS = new KeyValuePair<string, string>[] {
		// 00.AutoCreate {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_Text.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_TEXT}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_TextBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_TEXT_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_Img.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_RawImg.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_RAW_IMG}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_TextScaleBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_TEXT_SCALE_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_LocalizeText.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_LOCALIZE_TEXT}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_LocalizeTextBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_LOCALIZE_TEXT_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_LocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgScaleBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_SCALE_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgTextBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_TEXT_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgTextScaleBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_TEXT_SCALE_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgLocalizeTextBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgLocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_ScrollView.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_SCROLL_VIEW}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_RecycleView.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_RECYCLE_VIEW}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_PageView.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_PAGE_VIEW}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_FX_ROOT}T_ParticleFX.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_PARTICLE_FX}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_2D_ROOT}T_Sprite.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_SPRITE}.prefab"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_LandscapeAgreePopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.AS_OBJ_P_LANDSCAPE_AGREE_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_PortraitAgreePopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.AS_OBJ_P_PORTRAIT_AGREE_POPUP}.prefab"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_DescPopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.LSS_OBJ_P_DESC_POPUP}.prefab"),

#if SAMPLE_PROJ
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals/Externals/OmniSARTechnologies/LiteFPSCounter/Prefabs/LiteFPSCounter.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_FPS_COUNTER}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals/Externals/SmartTimersManager/TimerManager/TimersManager.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_TIMER_MANAGER}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals/Plugins/IngameDebugConsole/IngameDebugConsole.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_DEBUG_CONSOLE}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals/Plugins/IngameDebugConsole/Prefabs/DebugLogItem.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_DEBUG_LOG_ITEM}.prefab"),
#else
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals@{KCEditorDefine.B_VER_UNITY_MODULE}/Externals/OmniSARTechnologies/LiteFPSCounter/Prefabs/LiteFPSCounter.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_FPS_COUNTER}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals@{KCEditorDefine.B_VER_UNITY_MODULE}/Externals/SmartTimersManager/TimerManager/TimersManager.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_TIMER_MANAGER}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals@{KCEditorDefine.B_VER_UNITY_MODULE}/Plugins/IngameDebugConsole/IngameDebugConsole.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_DEBUG_CONSOLE}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommonexternals@{KCEditorDefine.B_VER_UNITY_MODULE}/Plugins/IngameDebugConsole/Prefabs/DebugLogItem.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_DEBUG_LOG_ITEM}.prefab"),
#endif			// #if SAMPLE_PROJ
		// 00.AutoCreate }

		// 01.SubAutoCreate {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SOUND_ROOT}T_BGSnd.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_G_BG_SND}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SOUND_ROOT}T_FXSnd.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_G_FX_SND}.prefab"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_DragResponder.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_G_DRAG_RESPONDER}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_TouchResponder.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.U_OBJ_P_G_TOUCH_RESPONDER}.prefab"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_START_SCENE}T_LoadingText.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.SS_OBJ_P_LOADING_TEXT}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_START_SCENE}T_LoadingGauge.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.SS_OBJ_P_LOADING_GAUGE}.prefab"),
		// 01.SubAutoCreate }

		// 02.UnityProject {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_AlertPopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_ALERT_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_ToastPopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_TOAST_POPUP}.prefab"),
		
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_STORE_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_SETTINGS_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_DAILY_MISSION_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_REWARD_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_FREE_REWARD_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_DAILY_REWARD_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_CHANGES_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_CHANGES_ACQUIRE_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_FocusPopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_FOCUS_POPUP}.prefab"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_FocusPopup.prefab", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_OBJ_P_G_TUTORIAL_POPUP}.prefab")
		// 02.UnityProject }
	};

	public static readonly KeyValuePair<string, string>[] B_TABLE_P_INFOS = new KeyValuePair<string, string>[] {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}LevelInfo/T_LevelInfoTable.txt", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.txt"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes"),
		
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleItemInfo/T_SaleItemInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}SaleItemInfoTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleItemInfo/T_SaleItemInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleItemInfo/T_SaleItemInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleProductInfo/T_SaleProductInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}SaleProductInfoTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleProductInfo/T_SaleProductInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}SaleProductInfo/T_SaleProductInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}MissionInfo/T_MissionInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}MissionInfoTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}MissionInfo/T_MissionInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}MissionInfo/T_MissionInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_MISSION_INFO}.json"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}RewardInfo/T_RewardInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}RewardInfoTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}RewardInfo/T_RewardInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}RewardInfo/T_RewardInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_REWARD_INFO}.json"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}EpisodeInfo/T_EpisodeInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}EpisodeInfoTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}EpisodeInfo/T_EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}EpisodeInfo/T_EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}T_ValTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}ValTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}T_ValTable_Common.csv", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_COMMON_VAL}.csv"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_STRING_INFO_ROOT}T_StrTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}StrTable.xlsx"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_STRING_INFO_ROOT}T_StrTable_Common.csv", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_COMMON_STR}.csv"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_STRING_INFO_ROOT}T_StrTable_Common_{SystemLanguage.Korean}.csv", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_KOREAN_COMMON_STR}.csv"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_TABLE_TEMPLATES}{KCDefine.B_DIR_P_STRING_INFO_ROOT}T_StrTable_Common_{SystemLanguage.English}.csv", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_TABLE_P_G_ENGLISH_COMMON_STR}.csv")
	};

	public static readonly KeyValuePair<string, string>[] B_ASSET_P_INFOS = new KeyValuePair<string, string>[] {
		// 01.SubAutoCreate
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_FONT_TEMPLATES}T_ThaiFont.ttf", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCDefine.U_FONT_P_G_THAI}.ttf"),

		// 02.UnityProject {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas_01.spriteatlas.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_01}.spriteatlas"),

		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_BuildInfoTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_BUILD_INFO_TABLE}.asset"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_BuildOptsTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_BUILD_OPTS_TABLE}.asset"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_DefineSymbolTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_DEFINE_SYMBOL_TABLE}.asset"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_DeviceInfoTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_DEVICE_INFO_TABLE}.asset"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_ProjInfoTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_PROJ_INFO_TABLE}.asset"),

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_PluginInfoTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_PLUGIN_INFO_TABLE}.asset"),
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE			
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_SCRIPTABLE_TEMPLATES}T_ProductInfoTable.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_ASSET_P_G_PRODUCT_INFO_TABLE}.asset")
#endif			// #if PURCHASE_MODULE_ENABLE
		// 02.UnityProject }
	};

	public static readonly KeyValuePair<string, string>[] B_PIPELINE_P_INFOS = new KeyValuePair<string, string>[] {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_PIPELINE_TEMPLATES}T_UniversalRPAsset.asset", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCDefine.U_PIPELINE_P_G_UNIVERSAL_RP_ASSET}.asset")
	};

	public static readonly KeyValuePair<string, string>[] B_SCENE_P_INFOS = new KeyValuePair<string, string>[] {
		// 00.AutoCreate
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INIT}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_SETUP}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_AGREE}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_LATE_SETUP}.unity"),

		// 01.SubAutoCreate {
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_START}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_SPLASH}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_PERMISSION}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INTRO}.unity"),

#if STUDY_ENABLE || STUDY_MODULE_ENABLE
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_STUDY_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_MENU}.unity"),
#endif			// #if STUDY_ENABLE || STUDY_MODULE_ENABLE
		// 01.SubAutoCreate }

		// 02.UnityProject
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_TITLE}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_GAME}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_OVERLAY}.unity"),
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_LOADING}.unity"),

		// 03.UnityProjectEditor
		new KeyValuePair<string, string>(KCEditorDefine.B_ABS_DIR_P_SAMPLE_SCENE, $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scenes/{KCDefine.B_SCENE_N_LEVEL_EDITOR}.unity")
	};

	public static readonly KeyValuePair<string, string>[] B_ASSEMBLY_DEFINE_P_INFOS = new KeyValuePair<string, string>[] {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityIronSrc.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}IronSource/Module.UnityIronSrc.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityIronSrc.Editor.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}IronSource/Editor/Module.UnityIronSrc.Editor.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityFlurry.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}Plugins/FlurrySDK/Module.UnityFlurry.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityGameAnalytics.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}GameAnalytics/Module.UnityGameAnalytics.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityGameAnalytics.Editor.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}GameAnalytics/Editor/Module.UnityGameAnalytics.Editor.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnitySingular.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}Code/Module.UnitySingular.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityGooglePlayGames.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}GooglePlayGames/Module.UnityGooglePlayGames.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityGooglePlayGames.Editor.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}GooglePlayGames/Editor/Module.UnityGooglePlayGames.Editor.asmdef"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityUnityPurchasing.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}Scripts/UnityPurchasing/generated/Module.UnityUnityPurchasing.asmdef")
	};

	public static readonly KeyValuePair<string, string>[] B_ICON_P_INFOS = new KeyValuePair<string, string>[] {
		// 독립 플랫폼
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Standalone/App/T_Icon.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_STANDALONE_APP}.png"),

		// iOS
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon76x76.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_76x76}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon120x120.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_120x120}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon152x152.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_152x152}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon167x167.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_167x167}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon180x180.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_180x180}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon1024x1024.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_APP_1024x1024}.png"),

		// 안드로이드
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon36x36.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_36x36}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon48x48.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_48x48}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon72x72.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_72x72}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon96x96.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_96x96}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon144x144.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_144x144}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon192x192.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_192x192}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/App/T_Icon512x512.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_APP_512x512}.png"),

#if NOTI_MODULE_ENABLE
		// iOS
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon20x20.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_NOTI_20x20}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon40x40.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_NOTI_40x40}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon60x60.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_IOS_NOTI_60x60}.png"),

		// 안드로이드
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon24x24.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_24x24}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon36x36.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_36x36}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon48x48.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_48x48}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon72x72.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_72x72}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon96x96.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_96x96}.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon256x256.png", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_256x256}.png")
#endif			// #if NOTI_MODULE_ENABLE
	};
	// 경로 }

	// 옵션
	public static readonly string[] B_COMPILER_OPTS_ADDITIONALS = new string[] {
		"-FIRDebugEnabled"
	};

	// 계층 뷰
	public static readonly Color B_COLOR_HIERARCHY_TEXT = new Color(1.0f, 0.27f, 0.0f, 1.0f);
	public static readonly Color B_COLOR_HIERARCHY_OUTLINE = Color.black;

	// FPS 카운터 {
	public static readonly Vector3 B_POS_FPS_C_STATIC_TEXT = new Vector3(-10.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_POS_FPS_C_DYNAMIC_TEXT = new Vector3(-10.0f, 140.0f, 0.0f);

	public static readonly string B_DIR_P_FILTER_FPS_COUNTER = $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESES}{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}External";
	// FPS 카운터 }

	// 디버그 콘솔 {
	public static readonly Vector3 B_SIZE_DEBUG_C_LOG_ITEM = new Vector3(0.0f, 80.0f, 0.0f);

	public static readonly string B_DIR_P_FILTER_DEBUG_CONSOLE = $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESES}{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}External";
	public static readonly string B_DIR_P_FILTER_DEBUG_LOG_ITEM = $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESES}{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}External";
	// 디버그 콘솔 }

	// 독립 플랫폼
	public static readonly string B_ABS_BUILD_P_FMT_STANDALONE = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/Standalone/{"{0}"}";
	public static readonly string B_DIR_P_FMT_EXTERNAL_DATAS_STANDALONE = $"{"{0}"}/{KCDefine.B_DIR_N_EXTERNAL_DATAS}/";

	// 맥
	public static readonly GraphicsDeviceType[] B_GRAPHICS_DEVICE_TYPES_MAC = new GraphicsDeviceType[] {
		GraphicsDeviceType.Metal, GraphicsDeviceType.OpenGLCore
	};

	// 윈도우즈
	public static readonly GraphicsDeviceType[] B_GRAPHICS_DEVICE_TYPES_WNDS = new GraphicsDeviceType[] {
		GraphicsDeviceType.Direct3D12, GraphicsDeviceType.Direct3D11
	};

	// iOS {
	public static readonly string B_ABS_BUILD_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/iOS";
	public static readonly string B_PLUGIN_PROJ_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/iOS";

	public static readonly string B_SRC_PLUGIN_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/iOS/Classes/Plugin/";
	public static readonly string B_DEST_PLUGIN_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_IOS_PLUGINS}CustomiOSPlugin/";

	public static readonly string B_ENTITLEMENTS_P_IOS_CAPABILITY = $"{Application.productName}.entitlements";

	public static readonly GraphicsDeviceType[] B_DEVICE_GRAPHICS_DEVICE_TYPES_IOS = new GraphicsDeviceType[] {
		GraphicsDeviceType.Metal
	};
	// iOS }

	// 안드로이드 {
	public static readonly string B_SRC_LIBRARY_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Temp/StagingArea/libs";
	public static readonly string B_ABS_BUILD_P_FMT_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/Android/{"{0}"}";

	public static readonly string B_PLUGIN_PROJ_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android";
	public static readonly string B_PLUGIN_BUILD_CMD_ANDROID = $"cd {KCEditorDefine.B_PLUGIN_PROJ_P_ANDROID};./gradlew exportPlugin";

	public static readonly string B_SRC_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android/app/build/outputs/aar/app-release.aar";
	public static readonly string B_DEST_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}CustomAndroidPlugin.aar";

	public static readonly string B_SRC_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/AndroidManifest.xml";
	public static readonly string B_ORIGIN_SRC_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}../UnityPackages/Templates/Options/Android/AndroidManifest.xml";
	public static readonly string B_DEST_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}AndroidManifest.xml";

	public static readonly string B_SRC_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/mainTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}../UnityPackages/Templates/Options/Android/mainTemplate.gradle";
	public static readonly string B_DEST_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}mainTemplate.gradle";

	public static readonly string B_SRC_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/launcherTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}../UnityPackages/Templates/Options/Android/launcherTemplate.gradle";
	public static readonly string B_DEST_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}launcherTemplate.gradle";

	public static readonly string B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/baseProjectTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}../UnityPackages/Templates/Options/Android/baseProjectTemplate.gradle";
	public static readonly string B_DEST_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}baseProjectTemplate.gradle";
	
	public static readonly string B_SRC_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/gradleTemplate.properties";
	public static readonly string B_ORIGIN_SRC_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}../UnityPackages/Templates/Options/Android/gradleTemplate.properties";
	public static readonly string B_DEST_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}gradleTemplate.properties";

	public static readonly string B_SRC_UNITY_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_ENGINE}../PlaybackEngines/AndroidPlayer/Variations/il2cpp/Release/Classes/classes.jar";
	public static readonly string B_DEST_UNITY_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android/app/libs/classes.jar";

	public static readonly GraphicsDeviceType[] B_GRAPHICS_DEVICE_TYPES_ANDROID = new GraphicsDeviceType[] {
		GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2
	};
	// 안드로이드 }

	// 젠킨스
	public static readonly string B_JENKINS_STANDALONE_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/41.Standalone", Application.unityVersion);
	public static readonly string B_JENKINS_IOS_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/01.iOS", Application.unityVersion);
	public static readonly string B_JENKINS_ANDROID_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/11.Android", Application.unityVersion);
	#endregion			// 런타임 상수

	#region 조건부 상수
#if NOTI_MODULE_ENABLE
	// 옵션
	public const PresentationOption B_PRESENT_OPTS_NOTI = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound;
	public const PresentationOption B_PRESENT_OPTS_REMOTE_NOTI = KCEditorDefine.B_PRESENT_OPTS_NOTI;
	
	// 이름
	public const string B_ACTIVITY_N_NOTI = "com.unity3d.player.UnityPlayerActivity";
#endif			// #if NOTI_MODULE_ENABLE
	#endregion			// 조건부 상수

	#region 조건부 런타임 상수
#if INPUT_SYSTEM_MODULE_ENABLE
	// 경로
	public static readonly string B_ASSET_P_INPUT_SETTINGS = $"{KCEditorDefine.B_DIR_P_ASSETS}InputSystem.inputsettings.asset";
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
	// 경로
	public static readonly KeyValuePair<string, string>[] B_NOTI_ICON_P_INFOS = new KeyValuePair<string, string>[] {
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_24x24}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-mdpi/icon24.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_36x36}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-hdpi/icon36.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_48x48}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xhdpi/icon48.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_72x72}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xxhdpi/icon72.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_96x96}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xxxhdpi/icon96.png"),
		new KeyValuePair<string, string>($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/{KCEditorDefine.B_ICON_P_ANDROID_NOTI_256x256}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable/icon256.png")
	};
#endif			// #if NOTI_MODULE_ENABLE
	#endregion			// 조건부 런타임 상수
}
#endif			// #if UNITY_EDITOR
