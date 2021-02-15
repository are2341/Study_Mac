using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//! 기본 상수
public static partial class KCDefine {
	#region 기본
	// 유닛 {
	public const int B_UNIT_SEC_TO_MILLISEC = 1000;

	public const int B_UNIT_NORM_VALUE_TO_BYTE = 255;
	public const float B_UNIT_NORM_VALUE_TO_VALUE = 100.0f;

	public const float B_UNIT_BYTE_TO_KILO_BYTE = 1024.0f;
	public const float B_UNIT_BYTE_TO_MEGA_BYTE = 1024.0f * 1024.0f;

	public const float B_UNIT_SCALE = 0.01f;
	public const float B_REF_PIXELS_UNIT = 1.0f;
	// 유닛 }

	// 값 {
	public const int B_VALUE_INT_0 = 0;
	public const int B_VALUE_INT_1 = 1;

	public const long B_VALUE_LONG_0 = 0L;
	public const long B_VALUE_LONG_1 = 1L;
	
	public const float B_VALUE_FLT_0 = 0.0f;
	public const float B_VALUE_FLT_1 = 1.0f;

	public const double B_VALUE_DBL_0 = 0.0;
	public const double B_VALUE_DBL_1 = 1.0;
	// 값 }

	// 빌드 번호
	public const int B_MIN_BUILD_NUMBER = 1;

	// 비교 결과
	public const int B_COMPARE_R_LESS = -1;
	public const int B_COMPARE_R_EQUALS = 0;
	public const int B_COMPARE_R_GREATE = 1;

	// 개수
	public const int B_MAX_NUM_BACKUP_FILES = 50;

	// 횟수 {
	public const int B_TIMES_INT_ONCE = 1;
	public const int B_TIMES_INT_INFINITE = -1;

	public const uint B_TIMES_UINT_ONCE = 1;
	public const uint B_TIMES_UINT_INFINITE = uint.MaxValue;
	// 횟수 }

	// 시간 {
	public const float B_DELTA_T_INFINITE = float.MaxValue;
	public const float B_DELTA_T_INTERMEDIATE = float.Epsilon;
	public const float B_DELTA_T_ASYNC_OPERATION = 0.25f;

	public const double B_DELTA_T_UTC_TO_PST = -8.0;
	// 시간 }

	// 플래그 {
	public const BindingFlags B_BINDING_F_PUBLIC_INSTANCE = BindingFlags.Public | BindingFlags.Instance;

	public const BindingFlags B_BINDING_F_PUBLIC_STATIC = BindingFlags.Public | BindingFlags.Static;
	public const BindingFlags B_BINDING_F_NON_PUBLIC_STATIC = BindingFlags.NonPublic | BindingFlags.Static;
	// 플래그 }

	// 인덱스 {
	public const int B_IDX_INVALID = -1;

	public static readonly Vector2Int B_IDX_INVALID_2D = new Vector2Int(KCDefine.B_IDX_INVALID, KCDefine.B_IDX_INVALID);
	public static readonly Vector3Int B_IDX_INVALID_3D = new Vector3Int(KCDefine.B_IDX_INVALID, KCDefine.B_IDX_INVALID, KCDefine.B_IDX_INVALID);

	public static readonly Vector2Int[] B_IDX_OFFSETS_PREV_2D = new Vector2Int[] {
		Vector2Int.down,
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.left,

		Vector2Int.down,
		Vector2Int.down,
		Vector2Int.up,
		Vector2Int.up,

		Vector2Int.right,
		Vector2Int.right,
		Vector2Int.left,
		Vector2Int.left
	};

	public static readonly Vector2Int[] B_IDX_OFFSETS_NEXT_2D = new Vector2Int[] {
		Vector2Int.up,
		Vector2Int.down,
		Vector2Int.left,
		Vector2Int.right,

		Vector2Int.left,
		Vector2Int.right,
		Vector2Int.left,
		Vector2Int.right,

		Vector2Int.up,
		Vector2Int.down,
		Vector2Int.up,
		Vector2Int.down
	};
	// 인덱스 }

	// 디바이스 {
	public const int B_PORTRAIT_SCREEN_WIDTH = 720;
	public const int B_PORTRAIT_SCREEN_HEIGHT = 1280;

	public const int B_LANDSCAPE_SCREEN_WIDTH = 1280;
	public const int B_LANDSCAPE_SCREEN_HEIGHT = 720;

#if MODE_PORTRAIT_ENABLE
	public const int B_SCREEN_WIDTH = KCDefine.B_PORTRAIT_SCREEN_WIDTH;
	public const int B_SCREEN_HEIGHT = KCDefine.B_PORTRAIT_SCREEN_HEIGHT;
#else
	public const int B_SCREEN_WIDTH = KCDefine.B_LANDSCAPE_SCREEN_WIDTH;
	public const int B_SCREEN_HEIGHT = KCDefine.B_LANDSCAPE_SCREEN_HEIGHT;
#endif			// #if MODE_PORTRAIT_ENABLE

	public const int B_DEF_TARGET_FRAME_RATE = 30;

	public const float B_DEF_DPI = 160.0f;
	public const float B_EDITOR_DPI = 320.0f;
	public const float B_DEF_FIXED_DELTA_TIME = 0.02f;

	public const float B_WORLD_SCREEN_WIDTH = KCDefine.B_SCREEN_WIDTH * KCDefine.B_UNIT_SCALE;
	public const float B_WORLD_SCREEN_HEIGHT = KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE;
	
	public const string B_EMPTY_STRING = "";
	public const string B_UNKNOWN_STRING = "Unknown";
	public const string B_NEW_LINE_STRING = "\n";
	public const string B_VERSION_STRING_FMT = "Ver.{0}";

	public const string B_STRING_INT_0 = "0";
	public const string B_STRING_INT_1 = "1";

	public const string B_STRING_FLOAT_0 = "0.0";
	public const string B_STRING_FLOAT_1 = "1.0";

	public const string B_UNKNOWN_LANGUAGE = KCDefine.B_UNKNOWN_STRING;
	public const string B_UNKNOWN_ERROR_MSG = KCDefine.B_UNKNOWN_STRING;
	public const string B_UNKNOWN_DEVICE_ID = KCDefine.B_UNKNOWN_STRING;

	public static readonly int B_DESKTOP_SCREEN_WIDTH = (int)(Screen.currentResolution.width * 0.95f);
	public static readonly int B_DESKTOP_SCREEN_HEIGHT = (int)(KCDefine.B_DESKTOP_SCREEN_WIDTH * (KCDefine.B_LANDSCAPE_SCREEN_HEIGHT / (float)KCDefine.B_LANDSCAPE_SCREEN_WIDTH));

	public static readonly Vector2 B_SCREEN_SIZE = new Vector2(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT);
	public static readonly Vector2 B_WORLD_SCREEN_SIZE = new Vector2(KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT);

	public static readonly Bounds B_SCREEN_BOUNDS = new Bounds(Vector3.zero, KCDefine.B_SCREEN_SIZE);
	public static readonly Bounds B_WORLD_SCREEN_BOUNDS = new Bounds(Vector3.zero, KCDefine.B_WORLD_SCREEN_SIZE);
	// 디바이스 }

	// 국가 코드
	public const string B_KOREA_COUNTRY_CODE = "KR";
	public const string B_UNKNOWN_COUNTRY_CODE = "Unknown";

	// 이름 {
	public const string B_PLATFORM_N_MAC = "Mac";
	public const string B_PLATFORM_N_WNDS = "Wnds";

	public const string B_PLATFORM_N_IOS = "iOS";
	public const string B_PLATFORM_N_ANDROID = "Android";
	
	public const string B_PLATFORM_N_GOOGLE = "Google";
	public const string B_PLATFORM_N_ONE_STORE = "OneStore";
	public const string B_PLATFORM_N_GALAXY_STORE = "GalaxyStore";
	
	public const string B_DIR_N_BACKUP = "Backup";
	public const string B_DIR_N_EXTERNAL_DATAS = "ExternalDatas";

	public const string B_EDITOR_SCENE_N_PATTERN_A = "EditorMenu";
	public const string B_EDITOR_SCENE_N_PATTERN_B = "EditorScene";
	// 이름 }

	// 씬 이름 {
	public const string B_SCENE_N_SAMPLE = "00.SampleScene";

	public const string B_SCENE_N_INIT = "00.InitScene";
	public const string B_SCENE_N_SETUP = "01.SetupScene";
	public const string B_SCENE_N_AGREE = "01.AgreeScene";
	public const string B_SCENE_N_LATE_SETUP = "01.LateSetupScene";
	
	public const string B_SCENE_N_START = "01.StartScene";
	public const string B_SCENE_N_SPLASH = "01.SplashScene";
	public const string B_SCENE_N_PERMISSION = "01.PermissionScene";
	public const string B_SCENE_N_LOADING = "01.LoadingScene";
	public const string B_SCENE_N_INTRO = "01.IntroScene";
	public const string B_SCENE_N_TITLE = "01.TitleScene";
	
#if STUDY_ENABLE || STUDY_MODULE_ENABLE
	public const string B_SCENE_N_MENU = "01.MenuScene";
#endif			// #if STUDY_ENABLE || STUDY_MODULE_ENABLE
	// 씬 이름 }

	// 식별자
	public const string B_KEY_JSON_ROOT_DATA = "Root";
	public const string B_KEY_JSON_COMMON_DATA = "Common";

	// 토큰
	public const string B_TOKEN_CSV_STRING = ",";
	public const string B_TOKEN_POST_STRING = "&";
	public const string B_TOKEN_VERSION_NUMBER = ".";

	// 빌드 모드
	public const string B_BUILD_MODE_DEBUG = "Debug";
	public const string B_BUILD_MODE_RELEASE = "Release";

	// 명령어 {
	public const string B_CMD_INIT = "Init";

	public const string B_CMD_GET_DEVICE_ID = "GetDeviceID";
	public const string B_CMD_GET_COUNTRY_CODE = "GetCountryCode";
	public const string B_CMD_GET_STORE_VERSION = "GetStoreVersion";

	public const string B_CMD_SET_ADS_TRACKING_ENABLE = "SetAdsTrackingEnable";

	public const string B_CMD_SHOW_ALERT = "ShowAlert";
	public const string B_CMD_SHOW_TOAST = "ShowToast";

	public const string B_CMD_VIBRATE = "Vibrate";
	public const string B_CMD_TRACKING = "Tracking";
	public const string B_CMD_INDICATOR = "Indicator";

	public const string B_CMD_INIT_ADS = "InitAds";
	public const string B_CMD_LOAD_RESUME_ADS = "LoadResumeAds";
	public const string B_CMD_SHOW_RESUME_ADS = "ShowResumeAds";
	// 명령어 }

	// 경로 {
	public const string B_DIR_P_ICONS = "Icons/";
	public const string B_DIR_P_FONTS = "Fonts/";
	public const string B_DIR_P_DATAS = "Datas/";
	public const string B_DIR_P_IMAGES = "Images/";
	public const string B_DIR_P_SOUNDS = "Sounds/";
	public const string B_DIR_P_TABLES = "Tables/";
	public const string B_DIR_P_PREFABS = "Prefabs/";
	public const string B_DIR_P_TEXTURES = "Textures/";
	public const string B_DIR_P_MATERIALS = "Materials/";
	public const string B_DIR_P_PIPELINES = "Pipelines/";
	public const string B_DIR_P_SCRIPTABLES = "Scriptables/";
	public const string B_DIR_P_SPRITE_ATLASES = "SpriteAtlases/";
	public const string B_DIR_P_SETTINGS = "Settings/";

	public const string B_DIR_P_GLOBAL = "Global/";
	public const string B_DIR_P_UTILITY = "Utility/";
	public const string B_DIR_P_TUTORIAL = "Tutorial/";
	public const string B_DIR_P_POPUP = "Popup/";
	public const string B_DIR_P_INIT_SCENE = "InitScene/";
	public const string B_DIR_P_SETUP_SCENE = "SetupScene/";
	public const string B_DIR_P_START_SCENE = "StartScene/";
	public const string B_DIR_P_LOADING_SCENE = "LoadingScene/";
	public const string B_DIR_P_SPLASH_SCENE = "SplashScene/";
	public const string B_DIR_P_AGREE_SCENE = "AgreeScene/";
	public const string B_DIR_P_INTRO_SCENE = "IntroScene/";
	public const string B_DIR_P_AUTO_CREATE = "00.AutoCreate/";

	public const string B_DIR_P_TEXT_ROOT = "UI/Text/";
	public const string B_DIR_P_IMAGE_ROOT = "UI/Image/";
	public const string B_DIR_P_BUTTON_ROOT = "UI/Button/";
	public const string B_DIR_P_SCROLL_VIEW_ROOT = "UI/ScrollView/";
	public const string B_DIR_P_POPUP_ROOT = "UI/Popup/";
	public const string B_DIR_P_RESPONDER_ROOT = "UI/Responder/";
	public const string B_DIR_P_SOUND_ROOT = "Sound/";
	public const string B_DIR_P_FX_ROOT = "FX/";
	public const string B_DIR_P_2D_ROOT = "2D/";
	public const string B_DIR_P_EXTERNAL_ROOT = "External/";
	public const string B_DIR_P_STRING_INFO_ROOT = "StringInfo/";
	public const string B_DIR_P_VALUE_INFO_ROOT = "ValueInfo/";
	// 경로 }

	// 형식 {
	public const string B_TEXT_FMT_PRICE = "{0} {1}";
	public const string B_TEXT_FMT_CURRENCY = "{0:N0}";
	public const string B_TEXT_FMT_HH_MM_SS = "{0:00}:{1:00}:{2:00}";

	public const string B_TEXT_FMT_1_DIGITS = "{0:0}";
	public const string B_TEXT_FMT_2_DIGITS = "{0:00}";
	public const string B_TEXT_FMT_3_DIGITS = "{0:000}";
	public const string B_TEXT_FMT_4_DIGITS = "{0:0000}";
	public const string B_TEXT_FMT_5_DIGITS = "{0:00000}";
	
	public const string B_SIZE_FMT_STRING = "<size={0}>{1}</size>";
	public const string B_COLOR_FMT_STRING = "<color=#{0}>{1}</color>";
	public const string B_DICTIONARY_FMT_STRING = "[{0}]{1}";

	public const string B_NAME_FMT_BACKUP = "yyyy_MM_dd HH_mm_ss";
	public const string B_NAME_FMT_BACKUP_COMBINE = "{0} ({1})";
	public const string B_NAME_FMT_UNDER_SCORE_COMBINE = "{0}_{1}";

	public const string B_PATH_FMT_1_COMBINE = "{0}/";
	public const string B_PATH_FMT_2_COMBINE = "{0}/{1}/";
	public const string B_PATH_FMT_3_COMBINE = "{0}/{1}/{2}/";
	public const string B_PATH_FMT_4_COMBINE = "{0}/{1}/{2}/{3}/";
	public const string B_PATH_FMT_5_COMBINE = "{0}/{1}/{2}/{3}/{4}/";

	public const string B_DATE_T_FMT_HH_MM_SS = "HH:mm:ss";
	public const string B_DATE_T_FMT_YYYY_MM_DD = "yyyyMMdd";
	public const string B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS = "yyyyMMdd HH:mm:ss";
	// 형식 }

	// URL
	public const string B_MAIL_URL_FMT = "mailto:{0}?subject={1}&body={2}";
	public const string B_MAIL_MSG_FMT = "App: {0}\nVersion: v{1}\nPlatform: {2}\nProcessor: {3}\nGraphics: {4} [{5}]\nOS: {6}\nUserID: {7}\n\nPlease enter your inquiry:\n{8}";
	#endregion			// 기본

	#region 런타임 상수
	// 색상 {
	public static readonly Color B_LOG_COLOR_WARNING = new Color(0xff / 255.0f, 0xa5 / 255.0f, 0.0f, 1.0f);
	public static readonly Color B_LOG_COLOR_ERROR = Color.red;

	public static readonly Color B_LOG_COLOR_SETUP = Color.magenta;
	public static readonly Color B_LOG_COLOR_PLUGIN = Color.yellow;
	public static readonly Color B_LOG_COLOR_PLATFORM_INFO = Color.red;
	// 색상 }

	// 앵커 {
	public static readonly Vector2 B_ANCHOR_TOP_LEFT = new Vector2(0.0f, 1.0f);
	public static readonly Vector2 B_ANCHOR_TOP_CENTER = new Vector2(0.5f, 1.0f);
	public static readonly Vector2 B_ANCHOR_TOP_RIGHT = new Vector2(1.0f, 1.0f);

	public static readonly Vector2 B_ANCHOR_MIDDLE_LEFT = new Vector2(0.0f, 0.5f);
	public static readonly Vector2 B_ANCHOR_MIDDLE_CENTER = new Vector2(0.5f, 0.5f);
	public static readonly Vector2 B_ANCHOR_MIDDLE_RIGHT = new Vector2(1.0f, 0.5f);

	public static readonly Vector2 B_ANCHOR_BOTTOM_LEFT = new Vector2(0.0f, 0.0f);
	public static readonly Vector2 B_ANCHOR_BOTTOM_CENTER = new Vector2(0.5f, 0.0f);
	public static readonly Vector2 B_ANCHOR_BOTTOM_RIGHT = new Vector2(1.0f, 0.0f);
	// 앵커 }

	// 비율
	public static readonly Vector3 B_SCALE_NORM = new Vector3(1.0f, 1.0f, 1.0f);

	// 위치 {
	public static readonly Vector3 B_POS_TOP_LEFT = new Vector3(0.0f, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	public static readonly Vector3 B_POS_TOP_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	public static readonly Vector3 B_POS_TOP_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	
	public static readonly Vector3 B_POS_MIDDLE_LEFT = new Vector3(0.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_POS_MIDDLE_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_POS_MIDDLE_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);

	public static readonly Vector3 B_POS_BOTTOM_LEFT = Vector3.zero;
	public static readonly Vector3 B_POS_BOTTOM_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_POS_BOTTOM_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, 0.0f, 0.0f);

#if MODE_CENTER_ENABLE
	public static readonly Vector3 B_POS_POPUP = Vector3.zero;
	public static readonly Vector3 B_POS_TOUCH_RESPONDER = Vector3.zero;
#else
	public static readonly Vector3 B_POS_POPUP = KCDefine.B_POS_MIDDLE_CENTER;
	public static readonly Vector3 B_POS_TOUCH_RESPONDER = KCDefine.B_POS_MIDDLE_CENTER;
#endif			// #if MODE_CENTER_ENABLE
	// 위치 }

	// 월드 위치 {
	public static readonly Vector3 B_WORLD_POS_TOP_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_TOP_CENTER = new Vector3(0.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_TOP_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);

	public static readonly Vector3 B_WORLD_POS_MIDDLE_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_MIDDLE_CENTER = Vector3.zero;
	public static readonly Vector3 B_WORLD_POS_MIDDLE_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, 0.0f, 0.0f);

	public static readonly Vector3 B_WORLD_POS_BOTTOM_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_BOTTOM_CENTER = new Vector3(0.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_BOTTOM_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
	// 월드 위치 }

	// 국가 코드
	public static readonly string[] B_EU_COUNTRY_CODES = new string[] {
		"BE", "BG", "CZ", "DK", "DE", "EE", "IE", 
		"GR", "ES", "FR", "HR", "IT", "CY", "LV", 
		"LT", "LU", "HU", "MT", "NL", "AT", "PL", 
		"PT", "RO", "SI", "SK", "FI", "SE"
	};

	// 경로 {
	public static readonly string B_DIR_P_WRITABLE = $"{Application.persistentDataPath}/{Application.identifier}/";
	public static readonly string B_ABS_DIR_P_EXTERNAL_DATAS = $"{Application.dataPath}/../{KCDefine.B_DIR_N_EXTERNAL_DATAS}/";

	public static readonly string B_DATA_P_LOG = $"{KCDefine.B_DIR_P_WRITABLE}Log.txt";

#if UNITY_STANDALONE_WIN
	public static readonly string B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS = $"{Application.dataPath}/../{KCDefine.B_DIR_N_EXTERNAL_DATAS}/";
#else
	public static readonly string B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS = $"{Application.dataPath}/../../{KCDefine.B_DIR_N_EXTERNAL_DATAS}/";
#endif			// #if UNITY_STANDALONE_WIN
	// 경로 }
	#endregion			// 런타임 상수
}
