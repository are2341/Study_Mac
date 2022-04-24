using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

#if NOTI_MODULE_ENABLE
using Unity.Notifications.iOS;
#endif			// #if NOTI_MODULE_ENABLE

/** 에디터 기본 상수 */
public static partial class KCEditorDefine {
	#region 기본
	// 단위
	public const int B_UNIT_CUSTOM_TAG_START_ID = 10;

	// 정렬 순서 {
	public const int B_SORTING_O_BUILD_MENU = 1000;

	public const int B_SORTING_O_SETUP_MENU = 2000;
	public const int B_SORTING_O_RESET_MENU = KCEditorDefine.B_SORTING_O_SETUP_MENU + 1;

	public const int B_SORTING_O_IMPORT_MENU = 3000;
	public const int B_SORTING_O_EXPORT_MENU = KCEditorDefine.B_SORTING_O_IMPORT_MENU + 1;

	public const int B_SORTING_O_CREATE_MENU = 4000;
	public const int B_SORTING_O_SUB_CREATE_MENU = KCEditorDefine.B_SORTING_O_CREATE_MENU + 1;

	public const int B_SORTING_O_EDITOR_WND_MENU = 5000;
	public const int B_SORTING_O_CHANGE_PLATFORM_MENU = KCEditorDefine.B_SORTING_O_EDITOR_WND_MENU + 1;
	// 정렬 순서 }

	// 시간
	public const float B_DELTA_T_SCENE_M_SCRIPT_UPDATE = 1.0f;
	public const float B_DELTA_T_EDITOR_SM_SCENE_UPDATE = 1.0f;

	// 플래그
	public const StaticEditorFlags B_STATIC_E_FLAGS = StaticEditorFlags.ContributeGI | StaticEditorFlags.OccluderStatic | StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic | StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic;

	// 토큰
	public const string B_TOKEN_REPLACE_UNITY_VERSION = "/*** UnityVersion */";

	// 형식
	public const string B_SORTING_OI_FMT = "[{0}:{1}]";

	// 버전
	public const string B_VER_UNITY_MODULE = "2.3.21";

	// 메뉴 {
	public const string B_MENU_TOOLS_BASE = "Tools/Utility/";
	public const string B_MENU_GAME_OBJECT_BASE = "GameObject/Utility/";

	public const string B_MENU_TOOLS_BUILD_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Build/";
	public const string B_MENU_TOOLS_SETUP_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Setup/";
	public const string B_MENU_TOOLS_RESET_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Reset/";
	public const string B_MENU_TOOLS_IMPORT_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Import/";
	public const string B_MENU_TOOLS_EXPORT_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Export/";
	public const string B_MENU_TOOLS_CREATE_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "Create/";
	public const string B_MENU_TOOLS_SUB_CREATE_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "SubCreate/";
	public const string B_MENU_TOOLS_EDITOR_WND_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "EditorWindow/";
	public const string B_MENU_TOOLS_CHANGE_PLATFORM_BASE = KCEditorDefine.B_MENU_TOOLS_BASE + "ChangePlatform/";

	public const string B_MENU_GAME_OBJECT_UI_BASE = KCEditorDefine.B_MENU_GAME_OBJECT_BASE + "UI/";
	public const string B_MENU_GAME_OBJECT_2D_BASE = KCEditorDefine.B_MENU_GAME_OBJECT_BASE + "2D/";
	public const string B_MENU_GAME_OBJECT_FX_BASE = KCEditorDefine.B_MENU_GAME_OBJECT_BASE + "FX/";
	// 메뉴 }

	// 커맨드 라인 {
	public const string B_CMD_LINE_PARAMS_FMT_SHELL = "-c \"{0}\"";
	public const string B_CMD_LINE_PARAMS_FMT_CMD_PROMPT = "/c \"{0}\"";

	public const string B_BUILD_CMD_EXPORT_PATH = "export PATH=\"${PATH}:/usr/local/bin\"";
	// 커맨드 라인 }

	// 이름 {
	public const string B_OBJ_N_SCENE_EDITOR_POPUP = "SceneEditorPopup";
	public const string B_MODULE_N_INPUT_SYSTEM_SETTINGS = "com.unity.input.settings";

	public const string B_PROPERTY_N_CATEGORY = "applicationCategoryType";
	public const string B_PROPERTY_N_REQUIRE_AR_KIT_SUPPORTS = "requiresARKitSupport";
	public const string B_PROPERTY_N_APPLE_ENABLE_PRO_MOTION = "appleEnableProMotion";
	public const string B_PROPERTY_N_AUTO_ADD_CAPABILITIES = "automaticallyDetectAndAddCapabilities";
	public const string B_PROPERTY_N_VALIDATE_APP_BUNDLE_SIZE = "validateAppBundleSize";
	public const string B_PROPERTY_N_APP_BUNDLE_SIZE_TO_VALIDATE = "appBundleSizeToValidate";
	public const string B_PROPERTY_N_SUPPORTED_ASPECT_RATIO_MODE = "supportedAspectRatioMode";

	public const string B_PROPERTY_N_ATLAS_WIDTH = "atlasWidth";
	public const string B_PROPERTY_N_ATLAS_HEIGHT = "atlasHeight";
	public const string B_PROPERTY_N_ATLAS_RENDER_MODE = "atlasRenderMode";
	public const string B_PROPERTY_N_CLEAR_DYNAMIC_DATA_ON_BUILD = "clearDynamicDataOnBuild";

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
	public const string B_PROPERTY_N_SND_M_AMBISONIC_DECODER_PLUGIN = "m_AmbisonicDecoderPlugin";
	public const string B_PROPERTY_N_SND_M_ENABLE_OUTPUT_SUSPENSION = "m_EnableOutputSuspension";

	public const string B_SCENE_N_PATTERN = "t:Example t:Scene";
	public const string B_ASSET_N_LIGHTING_SETTINGS_TEMPLATE = "T_LightingSettings";
	public const string B_ASSET_N_POST_PROCESSING_SETTINGS_TEMPLATE = "T_PostProcessingSettings";

	public const string B_FUNC_N_GET_LIGHTMAP_ENCODING_QUALITY = "GetLightmapEncodingQualityForPlatformGroup";
	public const string B_FUNC_N_GET_LIGHTMAP_STREAMING_ENABLE = "GetLightmapStreamingEnabledForPlatformGroup";
	public const string B_FUNC_N_GET_LIGHTMAP_STREAMING_PRIORITY = "GetLightmapStreamingPriorityForPlatformGroup";

	public const string B_FUNC_N_SET_COMPRESSION_TYPE = "SetCompressionType";
	public const string B_FUNC_N_SET_BATCHING_FOR_PLATFORM = "SetBatchingForPlatform";

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
	public const string B_DIR_P_SUB_UNITY_PROJ = "03.SubUnityProject/";
	public const string B_DIR_P_UNITY_PROJ_EDITOR = "04.UnityProjectEditor/";
	public const string B_DIR_P_SUB_UNITY_PROJ_EDITOR = "05.SubUnityProjectEditor/";
	public const string B_DIR_P_PROJ_SETTINGS = "ProjectSettings/";
	public const string B_DIR_P_EDITOR_DEF_RESOURCES = "Editor Default Resources/";

	public const string B_DIR_P_EXPORT_IMG_BASE = "Export/Images/";
	// 경로 }

	// iOS {
	public const string B_TEXT_IOS_TRUE = "YES";
	public const string B_TEXT_IOS_FALSE = "NO";

	public const string B_TEXT_IOS_METAL = "metal";
	public const string B_TEXT_IOS_ARM_64 = "arm64";

	public const string B_IPA_EXPORT_METHOD_IOS_DEV = "development";
	public const string B_IPA_EXPORT_METHOD_IOS_STORE = "app-store";

	public const string B_SEARCH_P_IOS_PODS = "$(SRCROOT)/**";
	public const string B_BUILD_FILE_EXTENSION_IOS_IPA = "ipa";

	public const string B_BUILD_P_FMT_IOS = "Builds/iOS/{0}";
	public const string B_PLIST_P_FMT_IOS = "{0}/Info.plist";
	public const string B_BUILD_OUTPUT_P_FMT_IOS = "Builds/iOS/{0}/BuildOutput/Export/{0}BuildOutput.{1}";

	public const string B_DATA_P_FMT_COCOA_PODS = "{0}/Podfile";
	public const string B_PROJ_P_FMT_COCOA_PODS = "{0}/Pods/Pods.xcodeproj/project.pbxproj";
	public const string B_BUILD_CMD_FMT_IOS_COCOA_PODS = "pod update --clean-install --project-directory={0}";

	public const string B_KEY_IOS_ENCRYPTION_ENABLE = "ITSAppUsesNonExemptEncryption";
	public const string B_KEY_IOS_DEVICE_CAPABILITIES = "UIRequiredDeviceCapabilities";
	public const string B_KEY_IOS_USER_TRACKING_USAGE_DESC = "NSUserTrackingUsageDescription";

	public const string B_KEY_IOS_ADS_NETWORK_ID = "SKAdNetworkIdentifier";
	public const string B_KEY_IOS_ADS_NETWORK_ITEMS = "SKAdNetworkItems";
	
	public const string B_PROPERTY_N_IOS_ENABLE_BITCODE = "ENABLE_BITCODE";
	public const string B_PROPERTY_N_IOS_USER_HEADER_SEARCH_PATHS = "USER_HEADER_SEARCH_PATHS";
	public const string B_PROPERTY_N_IOS_PREPROCESSOR_DEFINITIONS = "GCC_PREPROCESSOR_DEFINITIONS";
	// iOS }

	// 안드로이드 {
	public const int B_UNIT_VALIDATE_APP_BUNDLE_SIZE = 150;

	public const string B_BUILD_FILE_EXTENSION_ANDROID_APK = "apk";
	public const string B_BUILD_FILE_EXTENSION_ANDROID_AAB = "aab";

	public const string B_BUILD_P_FMT_ANDROID = "Builds/Android/{0}/{1}";
	public const string B_BUILD_FILE_N_FMT_ANDROID = "{0}BuildOutput.{1}";
	public const string B_BUILD_OUTPUT_P_FMT_ANDROID = "Builds/Android/{0}/{0}BuildOutput.{1}";
	// 안드로이드 }

	// 독립 플랫폼 {
	public const string B_BUILD_FILE_EXTENSION_STANDALONE_APP = "app";
	public const string B_BUILD_FILE_EXTENSION_STANDALONE_EXE = "exe";
	public const string B_BUILD_FILE_EXTENSION_STANDALONE_ZIP = "zip";

	public const string B_BUILD_P_FMT_STANDALONE = "Builds/Standalone/{0}/{1}";
	public const string B_BUILD_FILE_N_FMT_STANDALONE = "{0}BuildOutput.{1}";
	public const string B_BUILD_OUTPUT_P_FMT_STANDALONE = "Builds/Standalone/{0}BuildOutput.{1}";
	// 독립 플랫폼 }
	
	// 젠킨스 {
	public const string B_KEY_JENKINS_MODULE_VER = "ModuleVer";
	public const string B_KEY_JENKINS_BRANCH = "Branch";
	public const string B_KEY_JENKINS_SRC = "Src";
	public const string B_KEY_JENKINS_ANALYTICS_SRC = "AnalyticsSrc";
	public const string B_KEY_JENKINS_PROJ_NAME = "ProjName";
	public const string B_KEY_JENKINS_PROJ_PATH = "ProjPath";
	public const string B_KEY_JENKINS_BUILD_OUTPUT_PATH = "BuildOutputPath";
	public const string B_KEY_JENKINS_BUNDLE_ID = "BundleID";
	public const string B_KEY_JENKINS_PROFILE_ID = "ProfileID";
	public const string B_KEY_JENKINS_PLATFORM = "Platform";
	public const string B_KEY_JENKINS_PROJ_PLATFORM = "ProjPlatform";
	public const string B_KEY_JENKINS_BUILD_VER = "BuildVer";
	public const string B_KEY_JENKINS_BUILD_FUNC = "BuildFunc";
	public const string B_KEY_JENKINS_PIPELINE_NAME = "PipelineName";
	public const string B_KEY_JENKINS_IPA_EXPORT_METHOD = "IPAExportMethod";
	public const string B_KEY_JENKINS_BUILD_FILE_EXTENSION = "BuildFileExtension";

	public const string B_PROJ_PLATFORM_N_IOS = "iOS";
	public const string B_PROJ_PLATFORM_N_ANDROID = "Android";
	public const string B_PROJ_PLATFORM_N_STANDALONE_MAC = "OSXUniversal";
	public const string B_PROJ_PLATFORM_N_STANDALONE_WNDS = "Win64";

	public const string B_ASSET_IMPORTER_PLATFORM_N_DEF = "DefaultTexturePlatform";
	public const string B_ASSET_IMPORTER_PLATFORM_N_IOS = "iPhone";
	public const string B_ASSET_IMPORTER_PLATFORM_N_ANDROID = "Android";
	public const string B_ASSET_IMPORTER_PLATFORM_N_STANDALONE = "Standalone";

	public const string B_BUILD_CMD_FMT_JENKINS = "curl -X POST {0} --user {1}:{2} --data token={3}";
	public const string B_BUILD_DATA_FMT_JENKINS = "--data {0}={1}";

	public const string B_BRANCH_FMT_JENKINS = "origin/{0}";
	public const string B_ANALYTICS_FMT_JENKINS = "{0}/00.Analytics";
	public const string B_IOS_APPLE_BUILD_PROJ_N_JENKINS = "01.iOSApple";

	public const string B_ANDROID_GOOGLE_BUILD_PROJ_N_JENKINS = "11.AndroidGoogle";
	public const string B_ANDROID_AMAZON_BUILD_PROJ_N_JENKINS = "12.AndroidAmazon";

	public const string B_STANDALONE_MAC_STEAM_BUILD_PROJ_N_JENKINS = "41.StandaloneMacSteam";
	public const string B_STANDALONE_WNDS_STEAM_BUILD_PROJ_N_JENKINS = "51.StandaloneWndsSteam";

	public const string B_DEBUG_BUILD_FUNC_JENKINS = "Debug";
	public const string B_RELEASE_BUILD_FUNC_JENKINS = "Release";
	public const string B_STORE_A_BUILD_FUNC_JENKINS = "StoreA";
	public const string B_STORE_B_BUILD_FUNC_JENKINS = "StoreB";
	public const string B_STORE_DIST_BUILD_FUNC_JENKINS = "StoreDist";

	public const string B_IOS_DEBUG_PIPELINE_N_JENKINS = "01.iOSDebug";
	public const string B_IOS_RELEASE_PIPELINE_N_JENKINS = "02.iOSRelease";
	public const string B_IOS_STORE_PIPELINE_N_JENKINS = "03.iOSStore";

	public const string B_ANDROID_DEBUG_PIPELINE_N_JENKINS = "11.AndroidDebug";
	public const string B_ANDROID_RELEASE_PIPELINE_N_JENKINS = "12.AndroidRelease";
	public const string B_ANDROID_STORE_PIPELINE_N_JENKINS = "13.AndroidStore";

	public const string B_STANDALONE_DEBUG_PIPELINE_N_JENKINS = "41.StandaloneDebug";
	public const string B_STANDALONE_RELEASE_PIPELINE_N_JENKINS = "42.StandaloneRelease";
	public const string B_STANDALONE_STORE_PIPELINE_N_JENKINS = "43.StandaloneStore";

#if NINETAP_BUILD_PIPELINE_ENABLE
	public const string B_PIPELINE_GROUP_NAME_FMT_JENKINS = "job/000000.Common/job/{0}/job/01.Pipelines/job";
#else
	public const string B_PIPELINE_GROUP_NAME_FMT_JENKINS = "job/0000000000.Common/job/{0}/job/01.Pipelines/job";
#endif			// #if NINETAP_BUILD_PIPELINE_ENABLE
	// 젠킨스 }

	// 계층 뷰
	public const float B_OFFSET_HIERARCHY_TEXT = 15.0f;
	public const float B_OFFSET_HIERARCHY_OUTLINE = 1.0f;

	// 에디터 옵션 {
	public const float U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT = 0.25f;

	public const string B_EDITOR_OPTS_REMOTE_COMPRESSION = "JPEG";
	public const string B_EDITOR_OPTS_REMOTE_RESOLUTION = "Downsize";
	public const string B_EDITOR_OPTS_VER_CONTROL = "Visible Meta Files";
	public const string B_EDITOR_OPTS_JOYSTIC_SRC = "Remote";
	
#if UNITY_IOS
	public const string B_EDITOR_OPTS_REMOTE_DEVICE = "Any iOS Device";
#elif UNITY_ANDROID
	public const string B_EDITOR_OPTS_REMOTE_DEVICE = "Any Android Device";
#else
	public const string B_EDITOR_OPTS_REMOTE_DEVICE = "None";
#endif			// #if UNITY_IOS

	public static readonly List<string> B_EDITOR_OPTS_EXTENSION_LIST = new List<string>() {
		"txt", "xml", "fnt", "cd", "asmdef", "rsp", "asmref"
	};
	// 에디터 옵션 }

	// 경고 팝업 {
	public const string B_TEXT_ALERT_P_TITLE = "알림";
	public const string B_TEXT_ALERT_P_OK_BTN = "확인";
	public const string B_TEXT_ALERT_P_CANCEL_BTN = "취소";

	public const string B_MSG_ALERT_P_RESET = "컴포넌트를 리셋하시겠습니까?";
	public const string B_MSG_ALERT_P_EXPORT_IMG_SUCCESS = "이미지를 추출했습니다.";

	public const string B_MSG_ALERT_P_EXPORT_TEXTURE_FAIL = "텍스처를 선택해주세요.";
	public const string B_MSG_ALERT_P_EXPORT_SPRITE_FAIL = "스프라이트를 선택해주세요.";
	// 경고 팝업 }

	// 객체 이름 에디터 윈도우 {
	public const string B_TEXT_SEARCH = "검색";
	public const string B_TEXT_REPLACE = "변경";
	public const string B_TEXT_APPLY = "적용";

	public const string B_TEXT_FONT_REPLACE = "=====> 폰트 변경 <=====";
	public const string B_TEXT_OBJ_NAME_REPLACE = "=====> 객체 이름 변경 <=====";
	// 객체 이름 에디터 윈도우 }

	// 패키지
	public const string B_NAME_BUILD_REPORT_TOOLS_PKGS = "BuildReportTool-3.9.3";
	public const string B_NAME_SPRITE_TRAIL_PKGS = "SpriteTrail-1.4.0";
	public const string B_NAME_ULTIMATE_STATUS_BAR_PKGS = "UltimateStatusBar-2.6.0";
	public const string B_NAME_APPLE_AUTH_PKGS = "AppleAuth-1.4.2";
	#endregion			// 기본

	#region 런타임 상수
	// 크기
	public static readonly Vector3 B_MIN_SIZE_EDITOR_WND = new Vector3(350.0f, 350.0f, 0.0f);

	// 에디터 옵션
	public static readonly Vector2 B_EDITOR_OPTS_CASCADE_3_SPLIT_PERCENT = new Vector2(0.1f, 0.3f);
	public static readonly Vector3 B_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT = new Vector3(0.075f, 0.2f, 0.45f);

	// 이름 {
	public static readonly List<string> B_OBJ_N_ROOT_OBJ_LIST = new List<string>() {
		KCDefine.U_OBJ_N_SCENE_BASE, KCDefine.U_OBJ_N_SCENE_OBJS_BASE
	};

	public static readonly List<string> B_OBJ_N_STATIC_OBJ_LIST = new List<string>() {
		KCDefine.U_OBJ_N_SCENE_STATIC_OBJS, KCDefine.U_OBJ_N_SCENE_ADDITIONAL_LIGHTS, KCDefine.U_OBJ_N_SCENE_ADDITIONAL_CAMERAS, KCDefine.U_OBJ_N_SCENE_REFLECTION_PROBES, KCDefine.U_OBJ_N_SCENE_LIGHT_PROBE_GROUPS,
	};

	public static readonly List<string> B_OBJ_N_SCENE_EDITOR_LIGHT_LIST = new List<string>() {
		"SceneLight", "PreRenderLight"
	};

	public static readonly List<string> B_OBJ_N_SCENE_EDITOR_CAMERA_LIST = new List<string>() {
		"SceneCamera", "Main Camera", "Preview Camera", "Preview Scene Camera"
	};
	// 이름 }

	// 경로 {
	public static readonly string B_SCENE_P_INIT_SCENE = $"{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INIT}.unity";

	public static readonly string B_DIR_P_AUTO_CREATE_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/";
	public static readonly string B_DIR_P_SUB_AUTO_CREATE_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/";
	public static readonly string B_DIR_P_UNITY_PROJ_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/";
	public static readonly string B_DIR_P_SUB_UNITY_PROJ_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Resources/";
	public static readonly string B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Resources/";
	public static readonly string B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}Resources/";

	public static readonly string B_DIR_P_AUTO_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/";
	public static readonly string B_DIR_P_SUB_AUTO_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/";

	public static readonly string B_DIR_P_PROJ_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/";
	public static readonly string B_DIR_P_SUB_PROJ_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scenes/";

	public static readonly string B_DIR_P_EDITOR_PROJ_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scenes/";
	public static readonly string B_DIR_P_SUB_EDITOR_PROJ_SCENES = $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}Scenes/";

	public static readonly string B_DIR_P_TEMPLATES = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Templates/";
	public static readonly string B_DIR_P_ICON_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Icons/";
	public static readonly string B_DIR_P_FONT_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Fonts/";
	public static readonly string B_DIR_P_SCRIPTABLE_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Scriptables/";
	public static readonly string B_DIR_P_SPRITE_ATLAS_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}SpriteAtlases/";
	public static readonly string B_DIR_P_PIPELINE_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Pipelines/";
	public static readonly string B_DIR_P_SETTINGS_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Settings/";
	public static readonly string B_DIR_P_PREFAB_TEMPLATES = $"{KCEditorDefine.B_DIR_P_TEMPLATES}Prefabs/";

	public static readonly string B_ABS_DIR_P_ASSETS = $"{Application.dataPath}/";
	public static readonly string B_ABS_DIR_P_PACKAGES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Packages/";
	public static readonly string B_ABS_DIR_P_EXTERNAL_DATAS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../ExternalDatas/";
	public static readonly string B_ABS_DIR_P_PROJ_SETTINGS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../ProjectSettings/";
	public static readonly string B_ABS_DIR_P_TABLES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Tables/";
	public static readonly string B_ABS_DIR_P_SCRIPTS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Scripts/";
	public static readonly string B_ABS_DIR_P_UNITY_ENGINE = $"{EditorApplication.applicationPath}/";

	public static readonly string B_ABS_DIR_P_AUTO_CREATE_RESOURCES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Resources/";
	public static readonly string B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Resources/";
	public static readonly string B_ABS_DIR_P_UNITY_PROJ_RESOURCES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Resources/";
	public static readonly string B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Resources/";

#if SAMPLE_PROJ
	public static readonly string B_ABS_DIR_P_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon/Templates/";
	public static readonly string B_ABS_DIR_P_STUDY_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy/Templates/";
#else
	public static readonly string B_ABS_DIR_P_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitycommon@{KCEditorDefine.B_VER_UNITY_MODULE}/Templates/";
	public static readonly string B_ABS_DIR_P_STUDY_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}.module.unitystudy@{KCEditorDefine.B_VER_UNITY_MODULE}/Templates/";
#endif			// #if SAMPLE_PROJ

	public static readonly string B_ABS_DIR_P_PLUGINS = $"{Application.dataPath}/Plugins/";
	public static readonly string B_ABS_DIR_P_IOS_PLUGINS = $"{KCEditorDefine.B_ABS_DIR_P_PLUGINS}iOS/";
	public static readonly string B_ABS_DIR_P_ANDROID_PLUGINS = $"{KCEditorDefine.B_ABS_DIR_P_PLUGINS}Android/";

	public static readonly string B_ABS_DIR_P_DATA_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Datas/";
	public static readonly string B_ABS_DIR_P_SCRIPT_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Scripts/";
	public static readonly string B_ABS_DIR_P_TABLE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}Tables/";
	public static readonly string B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_TEMPLATES}AssemblyDefines/";

	public static readonly string B_ABS_DIR_P_UNITY_PACKAGES = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}UnityPackages/";
	public static readonly string B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES = $"{KCEditorDefine.B_ABS_DIR_P_STUDY_TEMPLATES}Scripts/";
	
	public static readonly string B_ABS_PKGS_P_BUILD_REPORT_TOOLS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Packages/UnityPackages/{KCEditorDefine.B_NAME_BUILD_REPORT_TOOLS_PKGS}.unitypackage";
	public static readonly string B_ABS_PKGS_P_SPRITE_TRAIL = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Packages/UnityPackages/{KCEditorDefine.B_NAME_SPRITE_TRAIL_PKGS}.unitypackage";
	public static readonly string B_ABS_PKGS_P_ULTIMATE_STATUS_BAR = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Packages/UnityPackages/{KCEditorDefine.B_NAME_ULTIMATE_STATUS_BAR_PKGS}.unitypackage";
	public static readonly string B_ABS_PKGS_P_APPLE_AUTH = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Packages/UnityPackages/{KCEditorDefine.B_NAME_APPLE_AUTH_PKGS}.unitypackage";

	public static readonly string B_ASSET_P_SAMPLE_SCENE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Scenes/{KCDefine.B_SCENE_N_SAMPLE}.unity";
	public static readonly string B_ASSET_P_STUDY_SAMPLE_SCENE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityStudy/Scenes/{KCDefine.B_SCENE_N_STUDY_SAMPLE}.unity";
	public static readonly string B_ASSET_P_EDITOR_SAMPLE_SCENE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Scenes/{KCDefine.B_SCENE_N_EDITOR_SAMPLE}.unity";

	public static readonly string B_ASSET_P_SAMPLE_SCENE_TEMPLATE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Scenes/{KCDefine.B_SCENE_N_SAMPLE}.scenetemplate";
	public static readonly string B_ASSET_P_STUDY_SAMPLE_SCENE_TEMPLATE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityStudy/Scenes/{KCDefine.B_SCENE_N_STUDY_SAMPLE}.scenetemplate";
	public static readonly string B_ASSET_P_EDITOR_SAMPLE_SCENE_TEMPLATE = $"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Scenes/{KCDefine.B_SCENE_N_EDITOR_SAMPLE}.scenetemplate";

	public static readonly string B_ASSET_P_TAG_MANAGER = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}TagManager.asset";
	public static readonly string B_ASSET_P_SND_MANAGER = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}AudioManager.asset";

	public static readonly string B_ASSET_P_BURST_AOT_SETTINGS_IOS = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}BurstAotSettings_iOS.json";
	public static readonly string B_ASSET_P_BURST_AOT_SETTINGS_ANDROID = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}BurstAotSettings_Android.json";
	public static readonly string B_ASSET_P_BURST_AOT_SETTINGS_MAC = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}BurstAotSettings_StandaloneOSX.json";
	public static readonly string B_ASSET_P_BURST_AOT_SETTINGS_WNDS = $"{KCEditorDefine.B_DIR_P_PROJ_SETTINGS}BurstAotSettings_StandaloneWindows.json";

	public static readonly string B_ASSET_P_OPTS_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_OPTS_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_BUILD_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_BUILD_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_PROJ_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_PROJ_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_LOCALIZE_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_LOCALIZE_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_DEFINE_SYMBOL_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_DEFINE_SYMBOL_INFO_TABLE}.asset";
	
	public static readonly string B_ASSET_P_ITEM_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_ITEM_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_ITEM_SALE_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_ITEM_SALE_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_PRODUCT_SALE_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_PRODUCT_SALE_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_MISSION_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_MISSION_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_REWARD_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_REWARD_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_EPISODE_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_EPISODE_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_TUTORIAL_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_TUTORIAL_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_FX_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_FX_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_SKILL_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SKILL_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_BLOCK_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_BLOCK_INFO_TABLE}.asset";
	public static readonly string B_ASSET_P_RES_INFO_TABLE = $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_RES_INFO_TABLE}.asset";

	public static readonly string B_ASSET_P_FMT_SCRIPTABLE_OBJ = $"{KCEditorDefine.B_DIR_P_ASSETS}{"{0}.asset"}";
	public static readonly string B_ASSET_P_FMT_DEFINE_S_OUTPUT = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/DefineSymbols/{"{0}DefineSymbols.txt"}";

	public static readonly string B_IMG_P_FMT_TEXTURE_TO_IMG = $"{KCEditorDefine.B_DIR_P_EXPORT_IMG_BASE}{"Textures/{0}.png"}";
	public static readonly string B_IMG_P_FMT_SPRITE_TO_IMG = $"{KCEditorDefine.B_DIR_P_EXPORT_IMG_BASE}{"Sprites/{0}.png"}";
	
	public static readonly string B_DATA_P_BUILD_METHOD = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/BuildMethod.txt";
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

	public static readonly List<string> B_SEARCH_P_SCENE_LIST = new List<string>() {
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_AUTO_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_SUB_AUTO_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_PROJ_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_SUB_PROJ_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_EDITOR_PROJ_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_SUB_EDITOR_PROJ_SCENES)
	};

	public static readonly List<string> B_SEARCH_P_PREFAB_SCENE_LIST = new List<string>() {
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_AUTO_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_SUB_AUTO_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_PROJ_SCENES),
		Path.GetDirectoryName(KCEditorDefine.B_DIR_P_EDITOR_PROJ_SCENES)
	};

	public static readonly List<string> B_SEARCH_P_SPRITE_ATLAS_LIST = new List<string>() {
		$"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESOURCES}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}",
		$"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESOURCES}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}"
	};

	public static readonly List<TextureImporterType> B_ENABLE_SRGB_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		TextureImporterType.Default, TextureImporterType.Sprite
	};	

	public static readonly List<TextureImporterType> B_ENABLE_ALPHA_TRANSPARENCY_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		TextureImporterType.Default, TextureImporterType.Sprite, TextureImporterType.GUI, TextureImporterType.Cursor, TextureImporterType.Cookie
	};

	public static readonly List<TextureImporterType> B_IGNORE_RGBA_32_FMT_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		TextureImporterType.SingleChannel
	};

	public static readonly List<TextureImporterType> B_IGNORE_WRAP_MODE_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		TextureImporterType.NormalMap, TextureImporterType.Lightmap, TextureImporterType.DirectionalLightmap, TextureImporterType.Shadowmask
	};

	public static readonly List<TextureImporterType> B_IGNORE_FILTER_MODE_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		// Do Something
	};

	public static readonly List<TextureImporterType> B_IGNORE_NON_POT_SCALE_TEXTURE_TYPE_LIST = new List<TextureImporterType>() {
		TextureImporterType.GUI, TextureImporterType.Sprite
	};

	public static readonly List<(string, string)> B_DIR_P_TABLE_INFO_LIST = new List<(string, string)>() {
		(Path.GetDirectoryName(KCDefine.U_RUNTIME_TABLE_P_G_LEVEL_INFO), Path.GetDirectoryName($"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO}")),

#if AB_TEST_ENABLE
		(Path.GetDirectoryName(KCDefine.U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_A), Path.GetDirectoryName($"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_A}")),
		(Path.GetDirectoryName(KCDefine.U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_B), Path.GetDirectoryName($"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_B}"))
#endif			// #if AB_TEST_ENABLE
	};

	public static readonly List<(string, string)> B_FILE_P_TABLE_INFO_LIST = new List<(string, string)>() {
		(KCDefine.U_RUNTIME_TABLE_P_G_ITEM_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_ITEM_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_ITEM_SALE_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_ITEM_SALE_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_PRODUCT_SALE_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_PRODUCT_SALE_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_MISSION_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_REWARD_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_EPISODE_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_TUTORIAL_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_FX_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_FX_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_SKILL_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_SKILL_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_BLOCK_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_BLOCK_INFO}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_RES_INFO, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_RES_INFO}.json"),

#if AB_TEST_ENABLE
		(KCDefine.U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_A, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_A}.json"),
		(KCDefine.U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_B, $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_B}.json")
#endif			// #if AB_TEST_ENABLE
	};

	public static readonly List<(string, string)> B_DATA_P_INFO_LIST = new List<(string, string)>() {
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESOURCES}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}README.md"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_README.md", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_EDITOR_DEF_RESOURCES}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}README.md"),

		// 00.AutoCreate
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_Privacy_{SystemLanguage.Korean}.txt", $"{KCEditorDefine.B_ABS_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.AS_DATA_P_PRIVACY}.txt"),
		($"{KCEditorDefine.B_ABS_DIR_P_DATA_TEMPLATES}T_Services_{SystemLanguage.Korean}.txt", $"{KCEditorDefine.B_ABS_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.AS_DATA_P_SERVICES}.txt"),

		// 04.UnityPackages {
#if FIREBASE_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Datas/GameConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_GAME_CONFIG}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Datas/GameConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_DATA_P_G_GAME_CONFIG}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Datas/BuildVerConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Datas/BuildVerConfig.json", $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json"),
#endif			// #if FIREBASE_MODULE_ENABLE

#if SAMPLE_PROJ || DEVELOPMENT_PROJ
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Analytics/Doxyfile", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../../Doxyfile")
#endif			// #if SAMPLE_PROJ || DEVELOPMENT_PROJ
		// 04.UnityPackages }
	};
	
	public static readonly List<(string, string)> B_SCRIPT_P_INFO_LIST = new List<(string, string)>() {
#if SAMPLE_PROJ || DEVELOPMENT_PROJ
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/DSYMUploader.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/DSYMUploader.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleBranchEraser.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleBranchEraser.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleBranchMerger.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleBranchMerger.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleBranchSwitcher.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleBranchSwitcher.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleCmdExecuter.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleCmdExecuter.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleCommonImporter.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleCommonImporter.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleEraser.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleEraser.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleGC.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleGC.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleImporter.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleImporter.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModulePluginImporter.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModulePluginImporter.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleRemoteURLUpdater.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleRemoteURLUpdater.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleStudyImporter.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleStudyImporter.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleTagUpdater.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleTagUpdater.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleUpdater.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleUpdater.py"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Scripts/Python/UnityModuleUploader.py", $"{KCEditorDefine.B_ABS_DIR_P_SCRIPTS}Python/UnityModuleUploader.py"),
#endif			// #if SAMPLE_PROJ || DEVELOPMENT_PROJ

		// 00.AutoCreate {
		// 에디터 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Define/T_KEditorDefine+Abs.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Define/KEditorDefine+Abs.cs"),

		// 에디터 팩토리
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Factory/T_EditorFactory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Factory/EditorFactory.cs"),

		// 에디터 유틸리티
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Build/T_CBuildProcessor.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/Global/Utility/Build/CBuildProcessor.cs"),

		// 에디터 씬 관리자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Scene/T_CEditorSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/EditorScene/CEditorSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Scene/T_CEditorSceneManager+Setup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Editor/EditorScene/CEditorSceneManager+Setup.cs"),

		// 유틸리티
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/External/T_CMsgPackRegister.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Utility/External/CMsgPackRegister.cs"),

#if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		// 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+InitScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+InitScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SetupScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+SetupScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+AgreeScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+AgreeScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LateSetupScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+LateSetupScene.cs"),

		// 씬 관리자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubInitSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubInitScene/CSubInitSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubSetupSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubSetupScene/CSubSetupSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubAgreeSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubAgreeScene/CSubAgreeSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLateSetupSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/SubLateSetupScene/CSubLateSetupSceneManager.cs"),
#endif			// #if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE

#if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Abs.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+Abs.cs"),

		// 접근자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_Access.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Access/Access.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_AccessExtension.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Access/AccessExtension.cs"),

		// 팩토리
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Factory/Factory.cs"),

		// 확장 클래스
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Extension/T_Extension.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Extension/Extension.cs"),

		// 함수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Function/Func.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_LogFunc.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scripts/Runtime/Global/Function/LogFunc.cs"),
#endif			// #if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 00.AutoCreate }

		// 01.SubAutoCreate {
#if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		// 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+StartScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+StartScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SplashScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+SplashScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+IntroScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+IntroScene.cs"),

		// 씬 관리자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubStartSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubStartScene/CSubStartSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubSplashSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubSplashScene/CSubSplashSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubIntroSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubIntroScene/CSubIntroSceneManager.cs"),

#if STUDY_ENABLE || STUDY_MODULE_ENABLE
		// 상수
		($"{KCEditorDefine.B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+MenuScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/Global/Define/KDefine+MenuScene.cs"),

		// 씬 관리자
		($"{KCEditorDefine.B_ABS_DIR_P_STUDY_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubMenuSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scripts/Runtime/SubMenuScene/CSubMenuSceneManager.cs"),
#endif			// #if STUDY_ENABLE || STUDY_MODULE_ENABLE
#endif			// #if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		// 01.SubAutoCreate }

		// 02.UnityProject {
		// 에디터 팩토리
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Factory/T_EditorFactory+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Editor/Global/Factory/EditorFactory+Global.cs"),

#if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 상수 {
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Type.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Type.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Enum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Enum.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Global.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Log.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+Log.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+ValTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+ValTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+GameCenter.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+GameCenter.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+TitleScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+TitleScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+MainScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+MainScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+GameScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+GameScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LoadingScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+LoadingScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+OverlayScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+OverlayScene.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+TestScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+TestScene.cs"),
		// 상수 }

		// 접근자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_Access+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Access/Access+Global.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_AccessExtension+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Access/AccessExtension+Global.cs"),

		// 팩토리
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Factory/Factory+Global.cs"),

		// 확장 클래스
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Extension/T_Extension+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Extension/Extension+Global.cs"),

		// 함수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Function/Func+Global.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_LogFunc+Global.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Function/LogFunc+Global.cs"),

		// 저장소
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CBaseInfo.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CBaseInfo.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CAppInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CAppInfoStorage.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CUserInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CUserInfoStorage.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Storage/T_CGameInfoStorage.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/Global/Utility/Storage/CGameInfoStorage.cs"),

		// 씬 관리자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubTitleSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubTitleScene/CSubTitleSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubMainSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubMainScene/CSubMainSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubGameSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubGameScene/CSubGameSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLoadingSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubLoadingScene/CSubLoadingSceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubOverlaySceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubOverlayScene/CSubOverlaySceneManager.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubTestSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scripts/Runtime/SubTestScene/CSubTestSceneManager.cs"),
#endif			// #if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 02.UnityProject }
		
		// 03.SubUnityProject {
#if EXTRA_SCRIPT || EXTRA_SCRIPT_ENABLE
		// 에디터 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Define/T_KEditorDefine+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Editor/Global/Define/KEditorDefine+SubGlobal.cs"),
		
		// 상수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SubType.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+SubType.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SubEnum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+SubEnum.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Define/KDefine+SubGlobal.cs"),

		// 접근자
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_Access+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Access/Access+SubGlobal.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Access/T_AccessExtension+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Access/AccessExtension+SubGlobal.cs"),

		// 팩토리
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Factory/Factory+SubGlobal.cs"),

		// 확장 클래스
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Extension/T_Extension+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Extension/Extension+SubGlobal.cs"),

		// 함수
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Function/Func+SubGlobal.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_LogFunc+SubGlobal.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Function/LogFunc+SubGlobal.cs"),

		// 에셋 임포터
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Editor/Importer/T_CSubAssetImporter.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Editor/Global/Importer/CSubAssetImporter.cs"),
#endif			// #if EXTRA_SCRIPT || EXTRA_SCRIPT_ENABLE

		// 엔진 {
#if ENGINE_TEMPLATES_ENABLE || ENGINE_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/CEngine.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Setup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/CEngine+Setup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Access.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/CEngine+Access.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/T_CEngine+Factory.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/CEngine+Factory.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/FX/T_CEFX.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/FX/CEFX.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Block/T_CEBlock.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Block/CEBlock.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+Engine.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+EngineType.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+EngineType.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Define/T_KDefine+EngineEnum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Define/KDefine+EngineEnum.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Access/T_Access+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Access/Access+Engine.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Access/T_AccessExtension+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Access/AccessExtension+Engine.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Factory/T_Factory+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Factory/Factory+Engine.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Extension/T_Extension+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Extension/Extension+Engine.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Engine/Function/T_Func+Engine.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Engine/Global/Function/Func+Engine.cs"),
#endif			// #if ENGINE_TEMPLATES_ENABLE || ENGINE_TEMPLATES_MODULE_ENABLE
		// 엔진 }

#if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 기본
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CSubPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CMissionPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CMissionPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubAlertPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Base/CSubAlertPopup.cs"),

		// 효과
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/FX/T_CDifficultyCorrector.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/UI/FX/CDifficultyCorrector.cs"),

		// 테이블
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CLevelInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CLevelInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CItemInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CItemInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CItemSaleInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CItemSaleInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CProductSaleInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CProductSaleInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CMissionInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CMissionInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CRewardInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CRewardInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CEpisodeInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CEpisodeInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CTutorialInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CTutorialInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CFXInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CFXInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CSkillInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CSkillInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CBlockInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CBlockInfoTable.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Table/T_CResInfoTable.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/Table/CResInfoTable.cs"),

		// 팝업
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CStorePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CStorePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CSettingsPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CSettingsPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CSyncPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CSyncPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CDailyMissionPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CDailyMissionPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CFreeRewardPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CFreeRewardPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CDailyRewardPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CDailyRewardPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CSaleCoinsPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CSaleCoinsPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CRewardAcquirePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CRewardAcquirePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CSaleCoinsAcquirePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CSaleCoinsAcquirePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CContinuePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CContinuePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CResultPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CResultPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CResumePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CResumePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CPausePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CPausePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CProductSalePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CProductSalePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CFocusPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CFocusPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Popup/T_CTutorialPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Popup/CTutorialPopup.cs"),

		// 스크롤러 셀 뷰
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/ScrollView/T_CLevelScrollerCellView.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/ScrollView/CLevelScrollerCellView.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/ScrollView/T_CStageScrollerCellView.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/ScrollView/CStageScrollerCellView.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/ScrollView/T_CChapterScrollerCellView.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ}Scripts/Runtime/Global/Utility/ScrollView/CChapterScrollerCellView.cs"),
#endif			// #if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 03.SubUnityProject }

		// 04.UnityProjectEditor {
#if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+Editor.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Define/KDefine+Editor.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+EditorType.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Define/KDefine+EditorType.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+EditorEnum.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Define/KDefine+EditorEnum.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Define/T_KDefine+LevelEditorScene.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Define/KDefine+LevelEditorScene.cs"),

		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Factory/T_Factory+Editor.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Factory/Factory+Editor.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Function/T_Func+Editor.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Function/Func+Editor.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Scene/T_CSubLevelEditorSceneManager.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scripts/Runtime/SubLevelEditorScene/CSubLevelEditorSceneManager.cs"),
#endif			// #if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		// 04.UnityProjectEditor }

		// 05.SubUnityProjectEditor {
#if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubEditorInputPopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Utility/Base/CSubEditorInputPopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubEditorLevelCreatePopup.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Utility/Base/CSubEditorLevelCreatePopup.cs"),
		($"{KCEditorDefine.B_ABS_DIR_P_SCRIPT_TEMPLATES}Runtime/Base/T_CSubEditorScrollerCellView.cs", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR}Scripts/Runtime/Global/Utility/Base/CSubEditorScrollerCellView.cs")
#endif			// #if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		// 05.SubUnityProjectEditor }
	};
	
	public static readonly List<(string, string)> B_PREFAB_P_INFO_LIST = new List<(string, string)>() {
		// 00.AutoCreate {
		($"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommonExternals/Externals/Etc/SmartTimersManager/TimerManager/TimersManager.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TIMER_MANAGER}.prefab"),

#if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_Text.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TEXT}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_TextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_TextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_TMPText.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_TEXT}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_LocalizeText.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_LOCALIZE_TEXT}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_LocalizeTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_LOCALIZE_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_LocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_TEXT_ROOT}T_TMPLocalizeText.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_LOCALIZE_TEXT}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPLocalizeTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_LOCALIZE_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPLocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_LOCALIZE_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_Img.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_RawImg.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_RAW_IMG}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_FocusImg.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_FOCUS_IMG}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_IMAGE_ROOT}T_GaugeImg.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_GAUGE_IMG}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}T_ImgScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_ImgTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_ImgTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPImgTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_IMG_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPImgTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_IMG_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_ImgLocalizeTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/T_ImgLocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPImgLocalizeTextBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_IMG_LOCALIZE_TEXT_BTN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/T_TMPImgLocalizeTextScaleBtn.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_IMG_LOCALIZE_TEXT_SCALE_BTN}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_INPUT_ROOT}T_Dropdown.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_DROPDOWN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_INPUT_ROOT}T_InputField.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_INPUT_FIELD}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_INPUT_ROOT}T_TMPDropdown.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_DROPDOWN}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_INPUT_ROOT}T_TMPInputField.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_TMP_INPUT_FIELD}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_ScrollView.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_SCROLL_VIEW}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_RecycleView.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_RECYCLE_VIEW}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}T_PageView.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_PAGE_VIEW}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_FX_ROOT}T_LineFX.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_LINE_FX}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_FX_ROOT}T_ParticleFX.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_PARTICLE_FX}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_FX_ROOT}T_ReflectionProbe.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_REFLECTION_PROBE}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_FX_ROOT}T_LightProbeGroup.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_LIGHT_PROBE_GROUP}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_2D_ROOT}T_Sprite.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_SPRITE}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_PortraitAgreePopup.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.AS_OBJ_P_PORTRAIT_AGREE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_LandscapeAgreePopup.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.AS_OBJ_P_LANDSCAPE_AGREE_POPUP}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_TrackingDescPopup.prefab", $"{KCEditorDefine.B_DIR_P_AUTO_CREATE_RESOURCES}{KCDefine.LSS_OBJ_P_TRACKING_DESC_POPUP}.prefab"),
#endif			// #if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE
		// 00.AutoCreate }

		// 01.SubAutoCreate {
#if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SOUND_ROOT}T_BGSnd.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_BG_SND}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_SOUND_ROOT}T_FXSnds.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_FX_SNDS}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_DragResponder.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_DRAG_RESPONDER}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_TouchResponder.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_TOUCH_RESPONDER}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_TouchResponder.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_INDICATOR_TOUCH_RESPONDER}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_RESPONDER_ROOT}T_TouchResponder.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.U_OBJ_P_G_SCREEN_FADE_TOUCH_RESPONDER}.prefab"),

		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_START_SCENE}T_LoadingText.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.SS_OBJ_P_LOADING_TEXT}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_START_SCENE}T_LoadingGauge.prefab", $"{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCDefine.SS_OBJ_P_LOADING_GAUGE}.prefab"),
#endif			// #if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE
		// 01.SubAutoCreate }

		// 04.SubUnityProject {
#if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_AlertPopup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_ALERT_POPUP}.prefab"),
#endif			// #if PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE

#if (PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE) && (RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE)
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_STORE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_SETTINGS_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_SYNC_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_DAILY_MISSION_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_FREE_REWARD_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_DAILY_REWARD_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_SALE_COINS_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_REWARD_ACQUIRE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_SALE_COINS_ACQUIRE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_CONTINUE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_RESULT_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_RESUME_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_PAUSE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_Popup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_PRODUCT_SALE_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_FocusPopup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_FOCUS_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_POPUP_ROOT}T_FocusPopup.prefab", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_OBJ_P_G_TUTORIAL_POPUP}.prefab"),
#endif			// #if (PREFAB_TEMPLATES_ENABLE || PREFAB_TEMPLATES_MODULE_ENABLE) && (RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE)
		// 04.SubUnityProject }

		// 04.UnityProjectEditor {
#if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_EDITOR_SCENE}T_EditorInputPopup.prefab", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.E_OBJ_P_EDITOR_INPUT_POPUP}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_EDITOR_SCENE}T_EditorLevelCreatePopup.prefab", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.E_OBJ_P_EDITOR_LEVEL_CREATE_POPUP}.prefab"),
	
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_EDITOR_SCENE}T_EditorScrollerCellView.prefab", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.E_OBJ_P_LEVEL_EDITOR_SCROLLER_CELL_VIEW}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_EDITOR_SCENE}T_EditorScrollerCellView.prefab", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.E_OBJ_P_STAGE_EDITOR_SCROLLER_CELL_VIEW}.prefab"),
		($"{KCEditorDefine.B_DIR_P_PREFAB_TEMPLATES}{KCDefine.B_DIR_P_EDITOR_SCENE}T_EditorScrollerCellView.prefab", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.E_OBJ_P_CHAPTER_EDITOR_SCROLLER_CELL_VIEW}.prefab")
#endif			// #if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		// 04.UnityProjectEditor }
	};

	public static readonly List<(string, string)> B_TABLE_P_INFO_LIST = new List<(string, string)>() {
#if SAMPLE_PROJ || DEVELOPMENT_PROJ
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ValueInfo/ValTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_ValTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/StringInfo/StrTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_StrTable.xlsx"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_EpisodeInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}A/G_EpisodeInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}B/G_EpisodeInfoTable.xlsx"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemInfo/ItemInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_ItemInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemSaleInfo/ItemSaleInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_ItemSaleInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ProductSaleInfo/ProductSaleInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_ProductSaleInfoTable.xlsx"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/MissionInfo/MissionInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_MissionInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/RewardInfo/RewardInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_RewardInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/TutorialInfo/TutorialInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_TutorialInfoTable.xlsx"),
		
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/FXInfo/FXInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_FXInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/SkillInfo/SkillInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_SkillInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/BlockInfo/BlockInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_BlockInfoTable.xlsx"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ResInfo/ResInfoTable.xlsx", $"{KCEditorDefine.B_ABS_DIR_P_TABLES}G_ResInfoTable.xlsx"),
#endif			// #if SAMPLE_PROJ || DEVELOPMENT_PROJ

		// 03.UnityProject
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ValueInfo/ValTable_Common.csv", $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_COMMON_VAL}.csv"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/StringInfo/StrTable_Common.csv", $"{KCEditorDefine.B_ABS_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_COMMON_STR}.csv"),

		// 04.SubUnityProject {
#if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/StringInfo/StrTable_Common_{SystemLanguage.Korean}.csv", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_KOREAN_COMMON_STR}.csv"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/StringInfo/StrTable_Common_{SystemLanguage.English}.csv", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_ENGLISH_COMMON_STR}.csv"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemInfo/ItemInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_ITEM_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemInfo/ItemInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_ITEM_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemSaleInfo/ItemSaleInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_ITEM_SALE_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ItemSaleInfo/ItemSaleInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_ITEM_SALE_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ProductSaleInfo/ProductSaleInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_PRODUCT_SALE_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ProductSaleInfo/ProductSaleInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_PRODUCT_SALE_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/MissionInfo/MissionInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/MissionInfo/MissionInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/RewardInfo/RewardInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/RewardInfo/RewardInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/TutorialInfo/TutorialInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/TutorialInfo/TutorialInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/FXInfo/FXInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_FX_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/FXInfo/FXInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_FX_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/SkillInfo/SkillInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SKILL_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/SkillInfo/SkillInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_SKILL_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/BlockInfo/BlockInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_BLOCK_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/BlockInfo/BlockInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_BLOCK_INFO}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ResInfo/ResInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_RES_INFO}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/ResInfo/ResInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_RES_INFO}.json"),

#if AB_TEST_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_A}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_A}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_B}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_B}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_A}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_A}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_B}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/EpisodeInfo/EpisodeInfoTable.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_B}.json"),
#endif			// #if AB_TEST_ENABLE

#if MSG_PACK_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO, 1)}.bytes"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO, 1)}.bytes"),

#if AB_TEST_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A, 1)}.bytes"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B, 1)}.bytes"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A, 1)}.bytes"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.bytes", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B, 1)}.bytes")
#endif			// #if AB_TEST_ENABLE
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO, 1)}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO, 1)}.json"),

#if AB_TEST_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A, 1)}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B, 1)}.json"),

		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A, 1)}.json"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Tables/LevelInfo/LevelInfo_000000001.json", $"{KCEditorDefine.B_ABS_DIR_P_SUB_UNITY_PROJ_RESOURCES}{string.Format(KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B, 1)}.json")
#endif			// #if AB_TEST_ENABLE
#endif			// #if MSG_PACK_ENABLE
#endif			// #if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 04.SubUnityProject }
	};

	public static readonly List<(string, string)> B_ASSET_P_INFO_LIST = new List<(string, string)>() {
		// 02.UnityProject {
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_OptsInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_OPTS_INFO_TABLE}.asset"),
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_BuildInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_BUILD_INFO_TABLE}.asset"),
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_DefineSymbolInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_DEFINE_SYMBOL_INFO_TABLE}.asset"),
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_ProjInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_PROJ_INFO_TABLE}.asset"),
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_DeviceInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_DEVICE_INFO_TABLE}.asset"),
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_LocalizeInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_LOCALIZE_INFO_TABLE}.asset"),

		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_LightingSettings.lighting", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_NORM_QUALITY_LIGHTING_SETTINGS}.lighting"),
		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_LightingSettings.lighting", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_HIGH_QUALITY_LIGHTING_SETTINGS}.lighting"),
		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_LightingSettings.lighting", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_ULTRA_QUALITY_LIGHTING_SETTINGS}.lighting"),

#if POST_PROCESSING_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_PostProcessingSettings.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_NORM_QUALITY_POST_PROCESSING_SETTINGS}.asset"),
		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_PostProcessingSettings.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_HIGH_QUALITY_POST_PROCESSING_SETTINGS}.asset"),
		($"{KCEditorDefine.B_DIR_P_SETTINGS_TEMPLATES}T_PostProcessingSettings.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_ULTRA_QUALITY_POST_PROCESSING_SETTINGS}.asset"),
#endif			// #if POST_PROCESSING_MODULE_ENABLE

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_PluginInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_PLUGIN_INFO_TABLE}.asset"),
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE			
		($"{KCEditorDefine.B_DIR_P_SCRIPTABLE_TEMPLATES}T_ProductInfoTable.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_PRODUCT_INFO_TABLE}.asset"),
#endif			// #if PURCHASE_MODULE_ENABLE
		// 02.UnityProject }

		// 03.SubUnityProject
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_01}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_02}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_03}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_04}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_05}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_06}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_07}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_08}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_SPRITE_ATLAS_09}.spriteatlas"),

		// 05.SubUnityProjectEditor
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_01}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_02}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_03}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_04}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_05}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_06}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_07}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_08}.spriteatlas"),
		($"{KCEditorDefine.B_DIR_P_SPRITE_ATLAS_TEMPLATES}T_SpriteAtlas.spriteatlas", $"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_RESOURCES}{KCDefine.U_ASSET_P_EG_SPRITE_ATLAS_09}.spriteatlas")
	};

	public static readonly List<(string, string)> B_PIPELINE_P_INFO_LIST = new List<(string, string)>() {
		// 02.UnityProject {
#if UNIVERSAL_RENDERING_PIPELINE_ENABLE || UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
		($"{KCEditorDefine.B_DIR_P_PIPELINE_TEMPLATES}T_UniversalRPAsset.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_NORM_QUALITY_UNIVERSAL_RP}.asset"),
		($"{KCEditorDefine.B_DIR_P_PIPELINE_TEMPLATES}T_UniversalRPAsset.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_HIGH_QUALITY_UNIVERSAL_RP}.asset"),
		($"{KCEditorDefine.B_DIR_P_PIPELINE_TEMPLATES}T_UniversalRPAsset.asset", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCDefine.U_ASSET_P_G_ULTRA_QUALITY_UNIVERSAL_RP}.asset")
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_ENABLE || UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
		// 02.UnityProject }
	};

	public static readonly List<(string, string)> B_SCENE_P_INFO_LIST = new List<(string, string)>() {
		// 00.AutoCreate {
#if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE		
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INIT}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_SETUP}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_AGREE}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_LATE_SETUP}.unity"),
#endif			// #if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		// 00.AutoCreate }

		// 01.SubAutoCreate {
#if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_START}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_SPLASH}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_INTRO}.unity"),

#if STUDY_ENABLE || STUDY_MODULE_ENABLE
		(KCEditorDefine.B_ASSET_P_STUDY_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_SUB_AUTO_CREATE}Scenes/{KCDefine.B_SCENE_N_MENU}.unity"),
#endif			// #if STUDY_ENABLE || STUDY_MODULE_ENABLE
#endif			// #if SCENE_TEMPLATES_ENABLE || SCENE_TEMPLATES_MODULE_ENABLE
		// 01.SubAutoCreate }

		// 02.UnityProject {
#if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_TITLE}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_MAIN}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_GAME}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_LOADING}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_OVERLAY}.unity"),
		(KCEditorDefine.B_ASSET_P_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ}Scenes/{KCDefine.B_SCENE_N_TEST}.unity"),
#endif			// #if RUNTIME_TEMPLATES_ENABLE || RUNTIME_TEMPLATES_MODULE_ENABLE
		// 02.UnityProject }

		// 04.UnityProjectEditor {
#if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		(KCEditorDefine.B_ASSET_P_EDITOR_SAMPLE_SCENE, $"{KCEditorDefine.B_DIR_P_ASSETS}{KCEditorDefine.B_DIR_P_UNITY_PROJ_EDITOR}Scenes/{KCDefine.B_SCENE_N_LEVEL_EDITOR}.unity")
#endif			// #if EDITOR_SCENE_TEMPLATES_ENABLE || EDITOR_SCENE_TEMPLATES_MODULE_ENABLE
		// 04.UnityProjectEditor }
	};

	public static readonly List<(string, string)> B_ASSEMBLY_DEFINE_P_INFO_LIST = new List<(string, string)>() {
#if ADS_ENABLE || ADS_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityIronSrc.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}IronSource/Module.UnityIronSrc.asmdef"),
		($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityIronSrc.Editor.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}IronSource/Editor/Module.UnityIronSrc.Editor.asmdef"),
#endif			// #if ADS_ENABLE || ADS_MODULE_ENABLE

#if FLURRY_ENABLE || FLURRY_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityFlurry.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}Plugins/FlurrySDK/Module.UnityFlurry.asmdef"),
#endif			// #if FLURRY_ENABLE || FLURRY_MODULE_ENABLE

#if PURCHASE_ENABLE || PURCHASE_MODULE_ENABLE
		($"{KCEditorDefine.B_ABS_DIR_P_ASSEMBLY_DEFINE_TEMPLATES}T_Module.UnityUnityPurchasing.asmdef.t", $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}Scripts/UnityPurchasing/generated/Module.UnityUnityPurchasing.asmdef")
#endif			// #if PURCHASE_ENABLE || PURCHASE_MODULE_ENABLE
	};

	public static readonly List<(string, string)> B_ICON_P_INFO_LIST = new List<(string, string)>() {
		// 02.UnityProject {
		// iOS
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon76x76.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_76x76}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon120x120.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_120x120}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon152x152.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_152x152}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon167x167.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_167x167}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon180x180.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_180x180}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/App/T_Icon1024x1024.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_APP_1024x1024}.png"),

		// 안드로이드
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon36x36.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_36x36}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon48x48.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_48x48}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon72x72.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_72x72}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon96x96.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_96x96}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon144x144.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_144x144}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon192x192.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_192x192}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/App/T_Icon512x512.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_APP_512x512}.png"),

		// 독립 플랫폼
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Standalone/App/T_Icon.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_STANDALONE_APP}.png"),

#if NOTI_MODULE_ENABLE
		// iOS
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon20x20.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_NOTI_20x20}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon40x40.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_NOTI_40x40}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}iOS/Notification/T_Icon60x60.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_IOS_NOTI_60x60}.png"),

		// 안드로이드
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon24x24.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_24x24}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon36x36.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_36x36}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon48x48.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_48x48}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon72x72.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_72x72}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon96x96.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_96x96}.png"),
		($"{KCEditorDefine.B_DIR_P_ICON_TEMPLATES}Android/Notification/T_Icon256x256.png", $"{KCEditorDefine.B_DIR_P_UNITY_PROJ_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_256x256}.png")
#endif			// #if NOTI_MODULE_ENABLE
		// 02.UnityProject }
	};
	// 경로 }
	
	// 계층 뷰
	public static readonly Color B_COLOR_HIERARCHY_TEXT = new Color(1.0f, 0.27f, 0.0f, 1.0f);
	public static readonly Color B_COLOR_HIERARCHY_OUTLINE = Color.black;
	
	// iOS {
	public static readonly string B_ABS_BUILD_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/iOS";
	public static readonly string B_PLUGIN_PROJ_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/iOS";

	public static readonly string B_SRC_PLUGIN_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/iOS/Classes/Plugin/";
	public static readonly string B_DEST_PLUGIN_P_IOS = $"{KCEditorDefine.B_ABS_DIR_P_IOS_PLUGINS}CustomiOSPlugin/";

	public static readonly string B_ENTITLEMENTS_P_CAPABILITY_IOS = $"{Application.productName}.entitlements";

	public static readonly List<GraphicsDeviceType> B_DEVICE_GRAPHICS_DEVICE_TYPE_LIST_IOS = new List<GraphicsDeviceType>() {
		GraphicsDeviceType.Metal
	};
	// iOS }

	// 안드로이드 {
	public static readonly string B_ABS_BUILD_P_FMT_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/Android/{"{0}"}";

	public static readonly string B_SRC_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android/app/build/outputs/aar/app-release.aar";
	public static readonly string B_DEST_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}CustomAndroidPlugin.aar";

	public static readonly string B_SRC_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/AndroidManifest.xml";
	public static readonly string B_DEST_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}AndroidManifest.xml";
	public static readonly string B_ORIGIN_SRC_MANIFEST_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/AndroidManifest.xml";

	public static readonly string B_SRC_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/mainTemplate.gradle";
	public static readonly string B_DEST_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}mainTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_MAIN_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/mainTemplate.gradle";

	public static readonly string B_SRC_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/launcherTemplate.gradle";
	public static readonly string B_DEST_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}launcherTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_LAUNCHER_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/launcherTemplate.gradle";

	public static readonly string B_SRC_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/baseProjectTemplate.gradle";
	public static readonly string B_DEST_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}baseProjectTemplate.gradle";
	public static readonly string B_ORIGIN_SRC_BASE_PROJ_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/baseProjectTemplate.gradle";
	
	public static readonly string B_SRC_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_PACKAGES}Options/Android/gradleTemplate.properties";
	public static readonly string B_DEST_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}gradleTemplate.properties";
	public static readonly string B_ORIGIN_SRC_GRADLE_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/gradleTemplate.properties";

	public static readonly string B_SRC_LOCAL_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../UnityPackages/Templates/Options/Android/local.properties";
	public static readonly string B_DEST_LOCAL_TEMPLATE_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android/local.properties";

	public static readonly string B_SRC_UNITY_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_UNITY_ENGINE}../PlaybackEngines/AndroidPlayer/Variations/il2cpp/Release/Classes/classes.jar";
	public static readonly string B_DEST_UNITY_PLUGIN_P_ANDROID = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../NativePlugins/Android/unityLibrary/libs/unity-classes.jar";

	public static readonly List<GraphicsDeviceType> B_GRAPHICS_DEVICE_TYPE_LIST_ANDROID = new List<GraphicsDeviceType>() {
		GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3
	};
	// 안드로이드 }

	// 독립 플랫폼
	public static readonly string B_ABS_BUILD_P_FMT_STANDALONE = $"{KCEditorDefine.B_ABS_DIR_P_ASSETS}../Builds/Standalone/{"{0}"}";
	public static readonly string B_DIR_P_FMT_EXTERNAL_DATAS_STANDALONE = $"{"{0}"}/{KCDefine.B_DIR_N_EXTERNAL_DATAS}/";

	// 맥
	public static readonly List<GraphicsDeviceType> B_GRAPHICS_DEVICE_TYPE_LIST_MAC = new List<GraphicsDeviceType>() {
		GraphicsDeviceType.Metal, GraphicsDeviceType.OpenGLCore
	};

	// 윈도우즈
	public static readonly List<GraphicsDeviceType> B_GRAPHICS_DEVICE_TYPE_LIST_WNDS = new List<GraphicsDeviceType>() {
		GraphicsDeviceType.Direct3D11, GraphicsDeviceType.OpenGLCore
	};

	// 리눅스
	public static readonly List<GraphicsDeviceType> B_GRAPHICS_DEVICE_TYPE_LIST_LINUX = new List<GraphicsDeviceType>() {
		GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLCore
	};
	
	// 젠킨스 {
	public static readonly string B_JENKINS_IOS_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/01.iOS", KCEditorDefine.B_VER_UNITY_MODULE);
	public static readonly string B_JENKINS_ANDROID_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/11.Android", KCEditorDefine.B_VER_UNITY_MODULE);
	public static readonly string B_JENKINS_STANDALONE_PIPELINE = string.Format($"{KCEditorDefine.B_PIPELINE_GROUP_NAME_FMT_JENKINS}/41.Standalone", KCEditorDefine.B_VER_UNITY_MODULE);

	public static readonly Dictionary<EiOSType, Dictionary<string, string>> B_JENKINS_IOS_SOURCES = new Dictionary<EiOSType, Dictionary<string, string>>() {
		[EiOSType.APPLE] = new Dictionary<string, string>() {
			[KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS] = "01.iOSAppleDebug",
			[KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS] = "11.iOSAppleRelease",
			[KCEditorDefine.B_STORE_A_BUILD_FUNC_JENKINS] = "21.iOSAppleStoreA",
			[KCEditorDefine.B_STORE_DIST_BUILD_FUNC_JENKINS] = "31.iOSAppleStoreDist"
		}
	};

	public static readonly Dictionary<EAndroidType, Dictionary<string, string>> B_JENKINS_ANDROID_SOURCES = new Dictionary<EAndroidType, Dictionary<string, string>>() {
		[EAndroidType.GOOGLE] = new Dictionary<string, string>() {
			[KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS] = "01.AndroidGoogleDebug",
			[KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS] = "11.AndroidGoogleRelease",
			[KCEditorDefine.B_STORE_A_BUILD_FUNC_JENKINS] = "21.AndroidGoogleStoreA",
			[KCEditorDefine.B_STORE_B_BUILD_FUNC_JENKINS] = "22.AndroidGoogleStoreB",
			[KCEditorDefine.B_STORE_DIST_BUILD_FUNC_JENKINS] = "31.AndroidGoogleStoreDist"
		},

		[EAndroidType.AMAZON] = new Dictionary<string, string>() {
			[KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS] = "01.AndroidAmazonDebug",
			[KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS] = "11.AndroidAmazonRelease",
			[KCEditorDefine.B_STORE_A_BUILD_FUNC_JENKINS] = "21.AndroidAmazonStoreA",
			[KCEditorDefine.B_STORE_B_BUILD_FUNC_JENKINS] = "22.AndroidAmazonStoreB",
			[KCEditorDefine.B_STORE_DIST_BUILD_FUNC_JENKINS] = "31.AndroidAmazonStoreDist"
		}
	};

	public static readonly Dictionary<EStandaloneType, Dictionary<string, string>> B_JENKINS_STANDALONE_SOURCES = new Dictionary<EStandaloneType, Dictionary<string, string>>() {
		[EStandaloneType.MAC_STEAM] = new Dictionary<string, string>() {
			[KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS] = "01.StandaloneMacSteamDebug",
			[KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS] = "11.StandaloneMacSteamRelease",
			[KCEditorDefine.B_STORE_A_BUILD_FUNC_JENKINS] = "21.StandaloneMacSteamStoreA",
			[KCEditorDefine.B_STORE_DIST_BUILD_FUNC_JENKINS] = "31.StandaloneMacSteamStoreDist"
		},

		[EStandaloneType.WNDS_STEAM] = new Dictionary<string, string>() {
			[KCEditorDefine.B_DEBUG_BUILD_FUNC_JENKINS] = "01.StandaloneWndsSteamDebug",
			[KCEditorDefine.B_RELEASE_BUILD_FUNC_JENKINS] = "11.StandaloneWndsSteamRelease",
			[KCEditorDefine.B_STORE_A_BUILD_FUNC_JENKINS] = "21.StandaloneWndsSteamStoreA",
			[KCEditorDefine.B_STORE_DIST_BUILD_FUNC_JENKINS] = "31.StandaloneWndsSteamStoreDist"
		}
	};
	// 젠킨스 }
	#endregion			// 런타임 상수

	#region 조건부 상수
#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	// 이름 {
	public const string U_FIELD_N_UNIVERSAL_RP_MSAA_QUALITY = "m_MSAA";
	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_BORDER = "m_CascadeBorder";
	public const string U_FIELD_N_UNIVERSAL_RP_OPAQUE_DOWN_SAMPLING = "m_OpaqueDownsampling";

	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_2_SPLIT = "m_Cascade2Split";
	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_3_SPLIT = "m_Cascade3Split";
	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_4_SPLIT = "m_Cascade4Split";

	public const string U_FIELD_N_UNIVERSAL_RP_RENDERER_DATAS = "m_RendererDataList";
	public const string U_FIELD_N_UNIVERSAL_RP_SUPPORTS_SOFT_SHADOW = "m_SoftShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_SUPPORTS_TERRAIN_HOLES = "m_SupportsTerrainHoles";

	public const string U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BLENDING = "m_ReflectionProbeBlending";
	public const string U_FIELD_N_UNIVERSAL_RP_REFLECTION_PROBE_BOX_PROJECTION = "m_ReflectionProbeBoxProjection";
	
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_RENDERING_MODE = "m_MainLightRenderingMode";
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SUPPORTS_SHADOW = "m_MainLightShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_USE_FAST_SRGB_LINEAR_CONVERSION = "m_UseFastSRGBLinearConversion";
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SHADOW_MAP_RESOLUTION = "m_MainLightShadowmapResolution";

	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_FMT = "m_AdditionalLightsCookieFormat";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ_LIMIT = "m_AdditionalLightsPerObjectLimit";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_RENDERING_MODE = "m_AdditionalLightsRenderingMode";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SUPPORTS_SHADOW = "m_AdditionalLightShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_COOKIE_RESOLUTION = "m_AdditionalLightsCookieResolution";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SHADOW_MAP_RESOLUTION = "m_AdditionalLightsShadowmapResolution";

	public const string B_PROPERTY_N_STRIP_DEBUG_VARIANTS = "m_StripDebugVariants";
	public const string B_PROPERTY_N_STRIP_UNUSED_VARIANTS = "m_StripUnusedVariants";
	public const string B_PROPERTY_N_STRIP_UNUSED_POST_PROCESSING_VARIANTS = "m_StripUnusedPostProcessingVariants";
	// 이름 }
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
	// 옵션
	public const PresentationOption B_PRESENT_OPTS_REMOTE_NOTI = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound;
	public const AuthorizationOption B_PRESENT_OPTS_AUTHORIZATION_NOTI = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
	
	// 이름
	public const string B_ACTIVITY_N_NOTI = "com.unity3d.player.UnityPlayerActivity";
#endif			// #if NOTI_MODULE_ENABLE
	#endregion			// 조건부 상수

	#region 조건부 런타임 상수
#if INPUT_SYSTEM_MODULE_ENABLE
	// 경로
	public static readonly string B_ASSET_P_INPUT_SETTINGS = $"{KCEditorDefine.B_DIR_P_ASSETS}InputSystem.inputsettings.asset";
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	// 경로
	public static readonly string B_ASSET_P_UNIVERSAL_RP_SETTINGS = $"{KCEditorDefine.B_DIR_P_ASSETS}UniversalRenderPipelineGlobalSettings.asset";
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
	// 경로
	public static readonly List<(string, string)> B_NOTI_ICON_P_INFO_LIST = new List<(string, string)>() {
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_24x24}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-mdpi/icon24.png"),
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_36x36}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-hdpi/icon36.png"),
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_48x48}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xhdpi/icon48.png"),
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_72x72}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xxhdpi/icon72.png"),
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_96x96}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable-xxxhdpi/icon96.png"),
		($"{KCEditorDefine.B_ABS_DIR_P_SUB_AUTO_CREATE_RESOURCES}{KCEditorDefine.B_ICON_P_ANDROID_NOTI_256x256}.png", $"{KCEditorDefine.B_ABS_DIR_P_ANDROID_PLUGINS}res/drawable/icon256.png")
	};
#endif			// #if NOTI_MODULE_ENABLE

#if BURST_COMPILER_MODULE_ENABLE
	// 경로
	public static readonly string B_DATA_P_IOS_BURST_AOT_SETTINGS = $"{KCEditorDefine.B_ABS_DIR_P_PROJ_SETTINGS}BurstAotSettings_iOS.json";
	public static readonly string B_DATA_P_ANDROID_BURST_AOT_SETTINGS = $"{KCEditorDefine.B_ABS_DIR_P_PROJ_SETTINGS}BurstAotSettings_Android.json";
	public static readonly string B_DATA_P_MAC_BURST_AOT_SETTINGS = $"{KCEditorDefine.B_ABS_DIR_P_PROJ_SETTINGS}BurstAotSettings_StandaloneOSX.json";
	public static readonly string B_DATA_P_WNDS_BURST_AOT_SETTINGS = $"{KCEditorDefine.B_ABS_DIR_P_PROJ_SETTINGS}BurstAotSettings_StandaloneWindows.json";
#endif			// #if BURST_COMPILER_MODULE_ENABLE
	#endregion			// 조건부 런타임 상수
}
#endif			// #if UNITY_EDITOR
