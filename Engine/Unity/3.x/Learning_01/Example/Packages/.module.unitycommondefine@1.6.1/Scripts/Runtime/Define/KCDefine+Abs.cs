using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//! 기본 상수
public static partial class KCDefine {
	#region 기본
	// 단위 {
	public const int B_UNIT_KINDS_PER_TYPE = 10000000;
	public const int B_UNIT_KINDS_PER_KINDS_TYPE = 10000;
	public const int B_UNIT_KINDS_PER_SUB_KINDS_TYPE = 100;

	public const int B_UNIT_IDS_PER_CHAPTER = 10000000;
	public const int B_UNIT_IDS_PER_STAGE = 10000;

	public const int B_UNIT_NORM_VAL_TO_BYTE = 255;
	public const int B_UNIT_NORM_VAL_TO_PERCENT = 100;

	public const int B_UNIT_BITS_PER_BYTE = 8;
	public const int B_UNIT_BYTES_PER_KILO_BYTE = 1024;
	public const int B_UNIT_BYTES_PER_MEGA_BYTE = 1024 * 1024;
	
	public const int B_UNIT_MILLI_SECS_PER_SECS = 1000;

	public const float B_UNIT_SCALE = 0.01f;
	public const float B_UNIT_REF_PIXELS = 1.0f;
	// 단위 }

	// 값 {
	public const int B_VAL_0_INT = 0;
	public const int B_VAL_1_INT = 1;
	public const int B_VAL_2_INT = 2;
	public const int B_VAL_3_INT = 3;
	public const int B_VAL_4_INT = 4;
	public const int B_VAL_5_INT = 5;
	public const int B_VAL_6_INT = 6;
	public const int B_VAL_7_INT = 7;
	public const int B_VAL_8_INT = 8;
	public const int B_VAL_9_INT = 9;

	public const long B_VAL_0_LONG = 0L;
	public const long B_VAL_1_LONG = 1L;
	public const long B_VAL_2_LONG = 2L;
	public const long B_VAL_3_LONG = 3L;
	public const long B_VAL_4_LONG = 4L;
	public const long B_VAL_5_LONG = 5L;
	public const long B_VAL_6_LONG = 6L;
	public const long B_VAL_7_LONG = 7L;
	public const long B_VAL_8_LONG = 8L;
	public const long B_VAL_9_LONG = 9L;
	
	public const float B_VAL_0_FLT = 0.0f;
	public const float B_VAL_1_FLT = 1.0f;
	public const float B_VAL_2_FLT = 2.0f;
	public const float B_VAL_3_FLT = 3.0f;
	public const float B_VAL_4_FLT = 4.0f;
	public const float B_VAL_5_FLT = 5.0f;
	public const float B_VAL_6_FLT = 6.0f;
	public const float B_VAL_7_FLT = 7.0f;
	public const float B_VAL_8_FLT = 8.0f;
	public const float B_VAL_9_FLT = 9.0f;

	public const double B_VAL_0_DBL = 0.0;
	public const double B_VAL_1_DBL = 1.0;
	public const double B_VAL_2_DBL = 2.0;
	public const double B_VAL_3_DBL = 3.0;
	public const double B_VAL_4_DBL = 4.0;
	public const double B_VAL_5_DBL = 5.0;
	public const double B_VAL_6_DBL = 6.0;
	public const double B_VAL_7_DBL = 7.0;
	public const double B_VAL_8_DBL = 8.0;
	public const double B_VAL_9_DBL = 9.0;
	// 값 }

	// 회전 {
	public const float B_ANGLE_45_DEG = 45.0f;
	public const float B_ANGLE_90_DEG = 90.0f;
	public const float B_ANGLE_180_DEG = 180.0f;
	public const float B_ANGLE_360_DEG = 360.0f;

	public const float B_ANGLE_45_RAD = KCDefine.B_ANGLE_45_DEG * Mathf.Deg2Rad;
	public const float B_ANGLE_90_RAD = KCDefine.B_ANGLE_90_DEG * Mathf.Deg2Rad;
	public const float B_ANGLE_180_RAD = KCDefine.B_ANGLE_180_DEG * Mathf.Deg2Rad;
	public const float B_ANGLE_360_RAD = KCDefine.B_ANGLE_360_DEG * Mathf.Deg2Rad;
	// 회전 }

	// 빌드 번호
	public const int B_MIN_BUILD_NUM = 1;

	// 비교
	public const int B_COMPARE_LESS = -1;
	public const int B_COMPARE_EQUALS = 0;
	public const int B_COMPARE_GREATE = 1;

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
	public const float B_MAX_DELTA_T_CONSENT_VIEW = 2.5f;

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

	public const int B_TARGET_FRAME_RATE = 60;

	public const float B_DPI = 160.0f;
	public const float B_FIXED_DELTA_TIME = 0.02f;

	public const float B_WORLD_SCREEN_WIDTH = KCDefine.B_SCREEN_WIDTH * KCDefine.B_UNIT_SCALE;
	public const float B_WORLD_SCREEN_HEIGHT = KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE;
	
	public const string B_EMPTY_STR = "";
	public const string B_TRUE_STR = "True";
	public const string B_FALSE_STR = "False";
	public const string B_UNKNOWN_STR = "Unknown";
	public const string B_NEW_LINE_STR = "\n";
	public const string B_VER_STR_FMT = "Ver.{0}";

	public const string B_STR_0_INT = "0";
	public const string B_STR_1_INT = "1";
	public const string B_STR_2_INT = "2";
	public const string B_STR_3_INT = "3";
	public const string B_STR_4_INT = "4";
	public const string B_STR_5_INT = "5";
	public const string B_STR_6_INT = "6";
	public const string B_STR_7_INT = "7";
	public const string B_STR_8_INT = "8";
	public const string B_STR_9_INT = "9";

	public const string B_STR_0_FLT = "0.0";
	public const string B_STR_1_FLT = "1.0";
	public const string B_STR_2_FLT = "2.0";
	public const string B_STR_3_FLT = "3.0";
	public const string B_STR_4_FLT = "4.0";
	public const string B_STR_5_FLT = "5.0";
	public const string B_STR_6_FLT = "6.0";
	public const string B_STR_7_FLT = "7.0";
	public const string B_STR_8_FLT = "8.0";
	public const string B_STR_9_FLT = "9.0";

	public const string B_FILE_EXTENSION_TXT = ".txt";
	public const string B_FILE_EXTENSION_JSON = ".json";
	public const string B_FILE_EXTENSION_BYTES = ".bytes";

	public const string B_UNKNOWN_LANGUAGE = KCDefine.B_UNKNOWN_STR;
	public const string B_UNKNOWN_DEVICE_ID = KCDefine.B_UNKNOWN_STR;
	public const string B_UNKNOWN_ERROR_MSG = KCDefine.B_UNKNOWN_STR;
	public const string B_UNKNOWN_SUPPORT_MSG = KCDefine.B_UNKNOWN_STR;

	public static readonly int B_DESKTOP_SCREEN_WIDTH = (int)(Screen.currentResolution.width * 0.95f);
	public static readonly int B_DESKTOP_SCREEN_HEIGHT = (int)(KCDefine.B_DESKTOP_SCREEN_WIDTH * (KCDefine.B_LANDSCAPE_SCREEN_HEIGHT / (float)KCDefine.B_LANDSCAPE_SCREEN_WIDTH));

	public static readonly Vector3 B_SCREEN_SIZE = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	public static readonly Vector3 B_WORLD_SCREEN_SIZE = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT, 0.0f);

	public static readonly Bounds B_SCREEN_BOUNDS = new Bounds(Vector3.zero, KCDefine.B_SCREEN_SIZE);
	public static readonly Bounds B_WORLD_SCREEN_BOUNDS = new Bounds(Vector3.zero, KCDefine.B_WORLD_SCREEN_SIZE);
	// 디바이스 }

	// 국가 코드
	public const string B_KOREA_COUNTRY_CODE = "KR";
	public const string B_UNKNOWN_COUNTRY_CODE = KCDefine.B_UNKNOWN_STR;

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

	public const string B_ASSET_N_PATTERN_FIX_POINT_FILTER = "_FIX_PF_";
	// 이름 }

	// 씬 이름 {
	public const string B_SCENE_N_SAMPLE = "00.SampleScene";
	public const string B_SCENE_N_LEVEL_EDITOR = "91.LevelEditorScene";

	public const string B_SCENE_N_INIT = "00.InitScene";
	public const string B_SCENE_N_SETUP = "01.SetupScene";
	public const string B_SCENE_N_AGREE = "01.AgreeScene";
	public const string B_SCENE_N_LATE_SETUP = "01.LateSetupScene";

	public const string B_SCENE_N_START = "01.StartScene";
	public const string B_SCENE_N_SPLASH = "01.SplashScene";
	public const string B_SCENE_N_PERMISSION = "01.PermissionScene";
	public const string B_SCENE_N_INTRO = "01.IntroScene";

	public const string B_SCENE_N_TITLE = "02.TitleScene";
	public const string B_SCENE_N_GAME = "03.GameScene";
	public const string B_SCENE_N_OVERLAY = "00.OverlayScene";
	public const string B_SCENE_N_LOADING = "01.LoadingScene";

	public const string B_SCENE_N_MENU = "01.MenuScene";
	public const string B_SCENE_N_STUDY_SAMPLE = "00.StudySampleScene";
	// 씬 이름 }

	// 식별자
	public const string B_KEY_JSON_ROOT_DATA = "Root";
	public const string B_KEY_JSON_COMMON_DATA = "Common";
	public const string B_KEY_JSON_USER_INFO_DATA = "UserInfo";
	public const string B_KEY_JSON_COMMON_USER_INFO_DATA = "CommonUserInfo";

	// 토큰 {
	public const string B_TOKEN_VER = ".";
	public const string B_TOKEN_CSV_STR = ",";
	public const string B_TOKEN_POST_STR = "&";

	public const string B_TOKEN_INCR = "+";
	public const string B_TOKEN_DECR = "-";

	public const string B_TOKEN_USER_A = "A";
	public const string B_TOKEN_USER_B = "B";
	public const string B_TOKEN_USER_UNKNOWN = "U";
	// 토큰 }

	// 빌드 모드
	public const string B_BUILD_MODE_DEBUG = "Debug";
	public const string B_BUILD_MODE_RELEASE = "Release";

	// 이벤트
	public const string B_EVENT_START = "Start";
	public const string B_EVENT_CANCEL = "Cancel";

	// 명령어 {
	public const string B_CMD_GET_DEVICE_ID = "GetDeviceID";
	public const string B_CMD_GET_COUNTRY_CODE = "GetCountryCode";
	public const string B_CMD_GET_STORE_VER = "GetStoreVer";

	public const string B_CMD_SET_ENABLE_ADS_TRACKING = "SetEnableAdsTracking";

	public const string B_CMD_SHOW_ALERT = "ShowAlert";
	public const string B_CMD_SHOW_TOAST = "ShowToast";

	public const string B_CMD_MAIL = "Mail";
	public const string B_CMD_VIBRATE = "Vibrate";
	public const string B_CMD_INDICATOR = "Indicator";
	// 명령어 }

	// 경로 {
	public const string B_DIR_P_BGS = "BGs/";
	public const string B_DIR_P_FXS = "FXs/";
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
	public const string B_DIR_P_ENGINE = "Engine/";
	public const string B_DIR_P_POPUP = "Popup/";
	public const string B_DIR_P_FX = "FX/";

	public const string B_DIR_P_INIT_SCENE = "InitScene/";
	public const string B_DIR_P_SETUP_SCENE = "SetupScene/";
	public const string B_DIR_P_AGREE_SCENE = "AgreeScene/";
	public const string B_DIR_P_LATE_SETUP_SCENE = "LateSetupScene/";

	public const string B_DIR_P_START_SCENE = "StartScene/";
	public const string B_DIR_P_SPLASH_SCENE = "SplashScene/";
	public const string B_DIR_P_PERMISSION_SCENE = "PermissionScene/";
	public const string B_DIR_P_INTRO_SCENE = "IntroScene/";

	public const string B_DIR_P_TITLE_SCENE = "TitleScene/";
	public const string B_DIR_P_GAME_SCENE = "GameScene/";
	public const string B_DIR_P_OVERLAY_SCENE = "OverlayScene/";
	public const string B_DIR_P_LOADING_SCENE = "LoadingScene/";

	public const string B_DIR_P_MENU_SCENE = "MenuScene/";

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
	public const string B_DIR_P_LEVEL_INFO_ROOT = "LevelInfo/";
	public const string B_DIR_P_VALUE_INFO_ROOT = "ValueInfo/";
	public const string B_DIR_P_STRING_INFO_ROOT = "StringInfo/";
	// 경로 }

	// 형식 {
	public const string B_TEXT_FMT_NUM = "x{0}";
	public const string B_TEXT_FMT_BRACKET = "({0})";

	public const string B_TEXT_FMT_INCR = "+{0}";
	public const string B_TEXT_FMT_DECR = "-{0}";

	public const string B_TEXT_FMT_PAGE = "{0}/{1}";
	public const string B_TEXT_FMT_PRICE = "{0} {1}";
	public const string B_TEXT_FMT_VER = "Ver.{0}{1}";
	
	public const string B_TEXT_FMT_CURRENCY = "{0:N0}";
	public const string B_TEXT_FMT_HH_MM_SS = "{0:00}:{1:00}:{2:00}";

	public const string B_TEXT_FMT_1_DIGITS = "{0:0}";
	public const string B_TEXT_FMT_2_DIGITS = "{0:00}";
	public const string B_TEXT_FMT_3_DIGITS = "{0:000}";
	public const string B_TEXT_FMT_4_DIGITS = "{0:0000}";
	public const string B_TEXT_FMT_5_DIGITS = "{0:00000}";
	
	public const string B_DICT_FMT_STR = "[{0}]{1}";
	public const string B_SIZE_FMT_STR = "<size={0}>{1}</size>";
	public const string B_COLOR_FMT_STR = "<color=#{0}>{1}</color>";

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

	// 메일
	public const string B_MAIL_MSG_FMT = "App: {0}\nVer: {1}\nPlatform: {2}\nProcessor: {3}\nGraphics: {4} [{5}]\nOS: {6}\nUserID: {7}\n\nPlease enter your inquiry:\n{8}";
	#endregion			// 기본

	#region 런타임 상수
	// 색상 {
	public static readonly Color B_LOG_COLOR_WARNING = new Color(0xff / (float)KCDefine.B_UNIT_NORM_VAL_TO_BYTE, 0xa5 / (float)KCDefine.B_UNIT_NORM_VAL_TO_BYTE, 0.0f, 1.0f);
	public static readonly Color B_LOG_COLOR_ERROR = Color.red;

	public static readonly Color B_LOG_COLOR_SETUP = Color.magenta;
	public static readonly Color B_LOG_COLOR_PLUGIN = Color.yellow;
	public static readonly Color B_LOG_COLOR_PLATFORM_INFO = Color.red;
	// 색상 }

	// 앵커 {
	public static readonly Vector2 B_ANCHOR_UP_LEFT = new Vector2(0.0f, 1.0f);
	public static readonly Vector2 B_ANCHOR_UP_CENTER = new Vector2(0.5f, 1.0f);
	public static readonly Vector2 B_ANCHOR_UP_RIGHT = new Vector2(1.0f, 1.0f);

	public static readonly Vector2 B_ANCHOR_MID_LEFT = new Vector2(0.0f, 0.5f);
	public static readonly Vector2 B_ANCHOR_MID_CENTER = new Vector2(0.5f, 0.5f);
	public static readonly Vector2 B_ANCHOR_MID_RIGHT = new Vector2(1.0f, 0.5f);

	public static readonly Vector2 B_ANCHOR_DOWN_LEFT = new Vector2(0.0f, 0.0f);
	public static readonly Vector2 B_ANCHOR_DOWN_CENTER = new Vector2(0.5f, 0.0f);
	public static readonly Vector2 B_ANCHOR_DOWN_RIGHT = new Vector2(1.0f, 0.0f);
	// 앵커 }

	// 비율
	public static readonly Vector3 B_SCALE_ZERO = new Vector3(0.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_SCALE_NORM = new Vector3(1.0f, 1.0f, 1.0f);

	// 위치 {
	public static readonly Vector3 B_POS_UP_LEFT = new Vector3(0.0f, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	public static readonly Vector3 B_POS_UP_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	public static readonly Vector3 B_POS_UP_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT, 0.0f);
	
	public static readonly Vector3 B_POS_MID_LEFT = new Vector3(0.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_POS_MID_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_POS_MID_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / 2.0f, 0.0f);

	public static readonly Vector3 B_POS_DOWN_LEFT = Vector3.zero;
	public static readonly Vector3 B_POS_DOWN_CENTER = new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_POS_DOWN_RIGHT = new Vector3(KCDefine.B_SCREEN_WIDTH, 0.0f, 0.0f);
	
	public static readonly Vector3 B_POS_POPUP = Vector3.zero;
	public static readonly Vector3 B_POS_TOUCH_RESPONDER = Vector3.zero;
	// 위치 }

	// 월드 위치 {
	public static readonly Vector3 B_WORLD_POS_UP_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_UP_CENTER = new Vector3(0.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_UP_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f);

	public static readonly Vector3 B_WORLD_POS_MID_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, 0.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_MID_CENTER = Vector3.zero;
	public static readonly Vector3 B_WORLD_POS_MID_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, 0.0f, 0.0f);

	public static readonly Vector3 B_WORLD_POS_DOWN_LEFT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_DOWN_CENTER = new Vector3(0.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
	public static readonly Vector3 B_WORLD_POS_DOWN_RIGHT = new Vector3(KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f);
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
