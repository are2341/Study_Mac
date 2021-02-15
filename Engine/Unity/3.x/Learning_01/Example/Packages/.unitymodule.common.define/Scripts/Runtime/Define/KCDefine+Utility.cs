﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if ADS_MODULE_ENABLE && ADMOB_ENABLE
using GoogleMobileAds.Api;
#endif			// #if ADS_MODULE_ENABLE && ADMOB_ENABLE

#if NOTI_MODULE_ENABLE
#if UNITY_IOS
using Unity.Notifications.iOS;
#elif UNITY_ANDROID
using Unity.Notifications.Android;
#endif			// #if UNITY_IOS
#endif			// #if NOTI_MODULE_ENABLE

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE

//! 유틸리티 상수
public static partial class KCDefine {
	#region 기본
	// 개수
	public const int U_MAX_NUM_LAYERS = 32;
	public const int U_MAX_NUM_DUPLICATE_FX_SNDS = 10;

	// 크기
	public const int U_DEF_SIZE_OBJ_POOL = 10;

	// 길이
	public const int U_MAX_LENGTH_LOG = 100000000;
	public const float U_MAX_PERCENT_ASYNC_OPERATION = 0.9f;

	// 유닛
	public const float U_UNIT_TABLET_INCHES = 7.0f;
	public const float U_UNIT_TABLET_ASPECT = 2.0f;

	// 세기
	public const float U_DEF_INTENSITY_VIBRATE = 1.0f;

	// 깊이
	public const float U_DEPTH_UI_CAMERA = 50.0f;
	public const float U_DEPTH_MAIN_CAMERA = -50.0f;

	// 거리 {
	public const float U_DEF_DISTANCE_PAGE = 25.0f;
	public const float U_DEF_DISTANCE_CAMERA_PLANE = 5.0f;

	public const float U_DISTANCE_CAMERA_FAR_PLANE = 5000.0f;
	public const float U_DISTANCE_CAMERA_NEAR_PLANE = 0.1f;
	// 거리 }

	// 비율 {
	public const float U_MIN_SCALE_POPUP = 0.001f;
	public const float U_DEF_SCALE_POPUP = 1.0f;

	public const float U_DEF_SCALE_TOUCH = 1.05f;
	public const float U_DEF_SCALE_PAGE_SCROLL = 0.35f;
	// 비율 }

	// 시간 {
	public const float U_DEF_TIME_SCALE = 1.0f;
	public const float U_ZERO_TIME_SCALE = 0.0f;
	public const float U_DELTA_T_SCHEDULE_M_CALLBACK = 0.15f;

	public const float U_DEF_DURATION_ANI = 0.25f;
	public const float U_DEF_DURATION_SCROLL_ANI = 0.25f;
	public const float U_DEF_DURATION_SCREEN_FADE_IN_ANI = 0.15f;
	public const float U_DEF_DURATION_SCREEN_FADE_OUT_ANI = 0.15f;
	public const float U_DEF_DURATION_POPUP_SCALE_ANI = 0.35f;
	public const float U_DEF_DURATION_TOAST_POPUP = 2.0f;

	public const float U_DEF_DURATION_LIGHT_VIBRATE = 0.05f;
	public const float U_DEF_DURATION_MEDIUM_VIBRATE = 0.1f;
	public const float U_DEF_DURATION_HEAVY_VIBRATE = 0.15f;

	public const float U_DELAY_INIT = 0.15f;
	public const float U_DELAY_NEXT_SCENE_LOAD = 0.5f;
	public const float U_DEF_DELAY_POPUP_SHOW_ANI = KCDefine.B_DELTA_T_INTERMEDIATE;

	public const float U_DEF_TIMEOUT_ASYNC_TASK = 30.0f;
	public const float U_DEF_TIMEOUT_NETWORK_CONNECTION = 30.0f;

#if MODE_PORTRAIT_ENABLE
	public const float U_DEF_DURATION_POPUP_DROPDOWN_ANI = 0.5f;
	public const float U_DEF_DURATION_POPUP_SLIDE_ANI = 0.45f;
#else
	public const float U_DEF_DURATION_POPUP_DROPDOWN_ANI = 0.45f;
	public const float U_DEF_DURATION_POPUP_SLIDE_ANI = 0.5f;
#endif			// #if MODE_PORTRAIT_ENABLE
	// 시간 }

	// 레이어
	public const int U_LAYER_DEF = 0;
	public const int U_LAYER_TRANSPARENT_FX = 1;
	public const int U_LAYER_IGNORE_RAYCAST = 2;
	public const int U_LAYER_WATER = 4;
	public const int U_LAYER_UI = 5;
	public const int U_LAYER_CUSTOM = 8;

	// 정렬 순서 {
	public const int U_SORTING_O_SCREEN_POPUP_UI = 0;
	public const int U_SORTING_O_SCREEN_TOPMOST_UI = 1;
	public const int U_SORTING_O_SCREEN_ABS_UI = 2;
	public const int U_SORTING_O_SCREEN_BLIND_UI = 3;
	public const int U_SORTING_O_SCREEN_DEBUG_UI = 4;

	public const int U_SORTING_O_FPS_COUNTER = 5;
	public const int U_SORTING_O_FILE_BROWSER = 6;
	public const int U_SORTING_O_DEBUG_CONSOLE = 7;
	// 정렬 순서 }

	// 애니메이션
	public const Ease U_DEF_EASE_ANI = Ease.OutQuad;

	// 광원 {
#if LIGHT_ENABLE && SHADOW_ENABLE
#if SOFT_SHADOW_ENABLE
	public const LightShadows U_DEF_LIGHT_SHADOW_TYPE = LightShadows.Soft;
#else
	public const LightShadows U_DEF_LIGHT_SHADOW_TYPE = LightShadows.Hard;
#endif			// #if SOFT_SHADOW_ENABLE
#else
	public const LightShadows U_DEF_LIGHT_SHADOW_TYPE = LightShadows.None;
#endif			// #if LIGHT_ENABLE && SHADOW_ENABLE
	// 광원 }

	// 회전
	public static readonly Vector3 U_DEF_ANGLE_MAIN_LIGHT = new Vector3(45.0f, 45.0f, 0.0f);

	// 버전
	public const string U_VERSION_COMMON_APP_INFO = "1.0.0";
	public const string U_VERSION_COMMON_USER_INFO = "1.0.0";
	public const string U_VERSION_COMMON_GAME_INFO = "1.0.0";
	
	// 형식
	public const string U_FMT_LOG_MSG = "[{0}]\nLogType: {1}\nCondition: {2}\nStackTrace:\n{3}==============================\n\n";

	// 식별자 {
	public const string U_ADS_ID_TEST_DEVICE = "TestDevice";

	public const string U_KEY_DEVICE_CMD = "Cmd";
	public const string U_KEY_DEVICE_MSG = "Msg";
	// 식별자 }

	// 이름 {
	public const string U_OBJ_N_SCENE_UI_TOP = "UIRoot";
	public const string U_OBJ_N_SCENE_UI_BASE = "Canvas";
	public const string U_OBJ_N_SCENE_UI_ROOT = "UIs";
	public const string U_OBJ_N_SCENE_ANCHOR_UI_ROOT = "AnchorUIs";
	public const string U_OBJ_N_SCENE_EVENT_SYSTEM = "EventSystem";

	public const string U_OBJ_N_SCENE_LEFT_UI_ROOT = "LeftUIs";
	public const string U_OBJ_N_SCENE_RIGHT_UI_ROOT = "RightUIs";
	public const string U_OBJ_N_SCENE_TOP_UI_ROOT = "TopUIs";
	public const string U_OBJ_N_SCENE_BOTTOM_UI_ROOT = "BottomUIs";

	public const string U_OBJ_N_SCENE_POPUP_UI_ROOT = "PopupUIs";
	public const string U_OBJ_N_SCENE_TOPMOST_UI_ROOT = "TopmostUIs";

	public const string U_OBJ_N_SCENE_BASE = "Base";
	public const string U_OBJ_N_SCENE_OBJ_BASE = "ObjRoot";
	public const string U_OBJ_N_SCENE_OBJ_ROOT = "Objs";
	public const string U_OBJ_N_SCENE_ANCHOR_OBJ_ROOT = "AnchorObjs";

	public const string U_OBJ_N_SCENE_LEFT_OBJ_ROOT = "LeftObjs";
	public const string U_OBJ_N_SCENE_RIGHT_OBJ_ROOT = "RightObjs";
	public const string U_OBJ_N_SCENE_TOP_OBJ_ROOT = "TopObjs";
	public const string U_OBJ_N_SCENE_BOTTOM_OBJ_ROOT = "BottomObjs";

	public const string U_OBJ_N_SCENE_OBJ_CANVAS_TOP = "ObjCanvasRoot";
	public const string U_OBJ_N_SCENE_OBJ_CANVAS_BASE = "ObjCanvas";
	public const string U_OBJ_N_SCENE_CANVAS_OBJ_ROOT = "CanvasObjs";

	public const string U_OBJ_N_SCENE_UI_CAMERA = "UI Camera";
	public const string U_OBJ_N_SCENE_MAIN_CAMERA = "Main Camera";
	public const string U_OBJ_N_SCENE_MAIN_LIGHT = "Directional Light";
	public const string U_OBJ_N_SCENE_MANAGER = "SceneManager";

	public const string U_OBJ_N_SCREEN_BLIND_UI_ROOT = "ScreenBlindUIs";
	public const string U_OBJ_N_SCREEN_POPUP_UI_ROOT = "ScreenPopupUIs";
	public const string U_OBJ_N_SCREEN_TOPMOST_UI_ROOT = "ScreenTopmostUIs";
	public const string U_OBJ_N_SCREEN_ABS_UI_ROOT = "ScreenAbsUIs";

	public const string U_OBJ_N_LEFT_BLIND_IMG = "LeftBlindImg";
	public const string U_OBJ_N_RIGHT_BLIND_IMG = "RightBlindImg";
	public const string U_OBJ_N_TOP_BLIND_IMG = "TopBlindImg";
	public const string U_OBJ_N_BOTTOM_BLIND_IMG = "BottomBlindImg";

	public const string U_OBJ_N_POPUP_CONTENTS_ROOT = "Contents";
	public const string U_OBJ_N_POPUP_CLOSE_BTN = "CloseBtn";
	public const string U_OBJ_N_FMT_POPUP_TOUCH_RESPONDER = "PopupTouchResponder_{0}";

	public const string U_OBJ_N_ALERT_POPUP = "AlertPopup";
	public const string U_OBJ_N_TOAST_POPUP = "ToastPopup";

	public const string U_OBJ_N_SCREEN_F_TOUCH_RESPONDER = "ScreenFTouchResponder";
	public const string U_OBJ_N_INDICATOR_TOUCH_RESPONDER = "IndicatorTouchResponder";

	public const string U_OBJ_N_FILE_BROWSER_UI = "SimpleFileBrowserCanvas(Clone)";

	public const string U_IMG_N_DEF_SPRITE = "DefSprite";
	public const string U_IMG_N_SPRITE_CLONE = "(Clone)";

	public const string U_FUNC_N_ON_DRAG = "OnDrag";
	public const string U_FUNC_N_ON_POINTER_UP = "OnPointerUp";
	public const string U_FUNC_N_ON_POINTER_DOWN = "OnPointerDown";
	public const string U_FUNC_N_ON_POINTER_ENTER = "OnPointerEnter";
	public const string U_FUNC_N_ON_POINTER_EXIT = "OnPointerExit";

	public const string U_FUNC_N_RESET_LOCALIZE = "ResetLocalize";

	public const string U_INPUT_N_JUMP = "Jump";
	public const string U_INPUT_N_VERTICAL = "Vertical";
	public const string U_INPUT_N_HORIZONTAL = "Horizontal";
	// 이름 }

	// 태그 {
	public const string U_TAG_PLAYER = "Player";
	public const string U_TAG_FINISH = "Finish";
	public const string U_TAG_RESPAWN = "Respawn";
	public const string U_TAG_EDITOR_ONLY = "EditorOnly";
	public const string U_TAG_MAIN_CAMERA = "MainCamera";
	public const string U_TAG_GAME_CONTROLLER = "GameController";

	public const string U_TAG_ENEMY = "Enemy";
	public const string U_TAG_OBSTACLE = "Obstacle";
	public const string U_TAG_UI_CAMERA = "UICamera";
	public const string U_TAG_MAIN_LIGHT = "MainLight";
	public const string U_TAG_SCENE_MANAGER = "SceneManager";
	// 태그 }

	// 정렬 레이어 {
	public const string U_SORTING_L_UNDERGROUND = "Underground";
	public const string U_SORTING_L_BACKGROUND = "Background";
	public const string U_SORTING_L_DEF = "Default";
	public const string U_SORTING_L_FOREGROUND = "Foreground";
	public const string U_SORTING_L_OVERGROUND = "Overground";
	public const string U_SORTING_L_TOP = "Top";
	public const string U_SORTING_L_TOPMOST = "Topmost";
	public const string U_SORTING_L_ABS = "Abs";

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	public const string U_SORTING_L_UNDERGROUND_UI = "UndergroundUI";
	public const string U_SORTING_L_BACKGROUND_UI = "BackgroundUI";
	public const string U_SORTING_L_DEF_UI = "DefaultUI";
	public const string U_SORTING_L_FOREGROUND_UI = "ForegroundUI";
	public const string U_SORTING_L_OVERGROUND_UI = "OvergroundUI";
	public const string U_SORTING_L_TOP_UI = "TopUI";
	public const string U_SORTING_L_TOPMOST_UI = "TopmostUI";
	public const string U_SORTING_L_ABS_UI = "AbsUI";
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	// 정렬 레이어 }

	// 씬 관리자
	public const string U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER = "SceneMDialogTouchResponder";
	public const string U_KEY_FMT_SCENE_M_TOUCH_RESPONDER = "SceneMTouchResponder_{0}";

	// 토스트 팝업
	public const string U_OBJ_N_TOAST_P_MSG_TEXT = "MsgText";
	public const string U_OBJ_N_TOAST_P_TOAST_POPUP = "ToastPopup";

	// 사운드 관리자
	public const string U_OBJ_N_SND_M_BG_SND = "BGSnd";
	public const string U_OBJ_N_SND_M_FX_SND = "FXSnd";

	// 디버그 콘솔
	public const string U_OBJ_N_DEBUG_C_LOG_WND = "DebugLogWindow";
	public const string U_OBJ_N_DEBUG_C_LOG_POPUP = "DebugLogPopup";

	// 스크롤 뷰
	public const string U_OBJ_N_SCROLL_V_VIEWPORT = "Viewport";
	public const string U_OBJ_N_SCROLL_V_CONTENTS_ROOT = "Contents";

	// 문자열 테이블
	public const string U_KEY_STRING_T_ID = "ID";
	public const string U_KEY_STRING_T_STRING = "String";
	public const string U_KEY_STRING_T_REPLACE = "Replace";

	// 값 테이블 {
	public const int U_IDX_VALUE_T_BOOL_LIST = 0;
	public const int U_IDX_VALUE_T_INT_LIST = 1;
	public const int U_IDX_VALUE_T_FLOAT_LIST = 2;
	public const int U_IDX_VALUE_T_STRING_LIST = 3;	

	public const string U_KEY_VALUE_T_ID = "ID";
	public const string U_KEY_VALUE_T_VALUE = "Value";
	public const string U_KEY_VALUE_T_VALUE_TYPE = "ValueType";
	public const string U_KEY_VALUE_T_REPLACE = "Replace";
	// 값 테이블 }

	// 경고 팝업 {
	public const string U_KEY_ALERT_P_TITLE = "Title";
	public const string U_KEY_ALERT_P_MSG = "Msg";
	public const string U_KEY_ALERT_P_OK_BTN_TEXT = "OKBtnText";
	public const string U_KEY_ALERT_P_CANCEL_BTN_TEXT = "CancelBtnText";

	public const string U_OBJ_N_ALERT_P_TITLE_TEXT = "TitleText";
	public const string U_OBJ_N_ALERT_P_MSG_TEXT = "MsgText";
	public const string U_OBJ_N_ALERT_P_BTN_TEXT = "Text";

	public const string U_OBJ_N_ALERT_P_BG_IMG = "BGImg";

	public const string U_OBJ_N_ALERT_P_OK_BTN = "OKBtn";
	public const string U_OBJ_N_ALERT_P_CANCEL_BTN = "CancelBtn";
	// 경고 팝업 }

	// 유니티 메세지 전송자 {
	public const string U_KEY_UNITY_MS_APP_ID = "AppID";
	public const string U_KEY_UNITY_MS_VERSION = "Version";
	public const string U_KEY_UNITY_MS_TIMEOUT = "Timeout";

	public const string U_KEY_UNITY_MS_BUILD_MODE = "BuildMode";
	public const string U_KEY_UNITY_MS_ORIENTATION = "Orientation";

	public const string U_KEY_UNITY_MS_ALERT_TITLE = KCDefine.U_KEY_ALERT_P_TITLE;
	public const string U_KEY_UNITY_MS_ALERT_MSG = KCDefine.U_KEY_ALERT_P_MSG;
	public const string U_KEY_UNITY_MS_ALERT_OK_BTN_TEXT = KCDefine.U_KEY_ALERT_P_OK_BTN_TEXT;
	public const string U_KEY_UNITY_MS_ALERT_CANCEL_BTN_TEXT = KCDefine.U_KEY_ALERT_P_CANCEL_BTN_TEXT;

	public const string U_KEY_UNITY_MS_VIBRATE_TYPE = "Type";
	public const string U_KEY_UNITY_MS_VIBRATE_STYLE = "Style";
	public const string U_KEY_UNITY_MS_VIBRATE_DURATION = "Duration";
	public const string U_KEY_UNITY_MS_VIBRATE_INTENSITY = "Intensity";

	public const string U_KEY_UNITY_MS_TRACKING_NAME = "Name";
	public const string U_KEY_UNITY_MS_TRACKING_DATAS = "Datas";
	public const string U_KEY_UNITY_MS_TRACKING_IS_START = "IsStart";

	public const string U_KEY_UNITY_MS_ADMOB_IDS = "AdmobIDs";
	public const string U_KEY_UNITY_MS_RESUME_ADS_ID = "ResumeAdsID";

	public const string U_KEY_UNITY_MS_SHARE_MSG_CALLBACK = "UnityMSShareMsgCallback";
	
	public const string U_CLS_N_UNITY_MS_MSG_RECEIVER = "dante.distribution.android.CAndroidPlugin";
	public const string U_FUNC_N_UNITY_MS_MSG_HANDLER = "handleUnityMsg";
	// 유니티 메세지 전송자 }

	// 디바이스 메세지 수신자
	public const string U_KEY_DEVICE_MR_RESULT = "Result";
	public const string U_KEY_DEVICE_MR_VERSION = KCDefine.U_KEY_UNITY_MS_VERSION;
	public const string U_KEY_FMT_DEVICE_MR_HANDLE_MSG_CALLBACK = "DeviceMRHandleMsgCallback_{0}";
	#endregion			// 기본

	#region 런타임 상수
	// 영역
	public static readonly Rect U_RECT_UI_CAMERA = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
	public static readonly Rect U_RECT_MAIN_CAMERA = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

	// 색상 {
	public static readonly Color U_COLOR_TRANSPARENT = new Color(0.0f, 0.0f, 0.0f, 0.0f);

	public static readonly Color U_DEF_COLOR_NORM = Color.white;
	public static readonly Color U_DEF_COLOR_PRESS = new Color(0.75f, 0.75f, 0.75f, 1.0f);
	public static readonly Color U_DEF_COLOR_SELECT = Color.white;
	public static readonly Color U_DEF_COLOR_HIGHLIGHT = Color.white;
	public static readonly Color U_DEF_COLOR_DISABLE = new Color(0.35f, 0.35f, 0.35f, 1.0f);

	public static readonly Color U_DEF_COLOR_BLIND_UI = Color.black;
	public static readonly Color U_DEF_COLOR_SCREEN_FADE_IN = Color.black;
	public static readonly Color U_DEF_COLOR_SCREEN_FADE_OUT = KCDefine.U_COLOR_TRANSPARENT;
	
	public static readonly Color U_DEF_COLOR_POPUP_BG = new Color(0.0f, 0.0f, 0.0f, 0.75f);
	public static readonly Color U_DEF_COLOR_INDICATOR_BG = KCDefine.U_DEF_COLOR_POPUP_BG;

#if UNITY_EDITOR
	public static readonly Color U_DEF_COLOR_CAMERA_BG = new Color(0.15f, 0.15f, 0.15f, 1.0f);
#else
	public static readonly Color U_DEF_COLOR_CAMERA_BG = Color.black;
#endif			// #if UNITY_EDITOR
	// 색상 }

	// 태그
	public static readonly string[] U_TAGS = new string[] {
		KCDefine.U_TAG_ENEMY,
		KCDefine.U_TAG_OBSTACLE,
		KCDefine.U_TAG_UI_CAMERA,
		KCDefine.U_TAG_MAIN_LIGHT,
		KCDefine.U_TAG_SCENE_MANAGER
	};

	// 정렬 레이어
	public static readonly string[] U_SORTING_LAYERS = new string[] {
		KCDefine.U_SORTING_L_UNDERGROUND,
		KCDefine.U_SORTING_L_BACKGROUND,
		KCDefine.U_SORTING_L_DEF,
		KCDefine.U_SORTING_L_FOREGROUND,
		KCDefine.U_SORTING_L_OVERGROUND,
		KCDefine.U_SORTING_L_TOP,
		KCDefine.U_SORTING_L_TOPMOST,
		KCDefine.U_SORTING_L_ABS,

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
		KCDefine.U_SORTING_L_UNDERGROUND_UI,
		KCDefine.U_SORTING_L_BACKGROUND_UI,
		KCDefine.U_SORTING_L_DEF_UI,
		KCDefine.U_SORTING_L_FOREGROUND_UI,
		KCDefine.U_SORTING_L_OVERGROUND_UI,
		KCDefine.U_SORTING_L_TOP_UI,
		KCDefine.U_SORTING_L_TOPMOST_UI,
		KCDefine.U_SORTING_L_ABS_UI
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	};

	// 정렬 순서 {
	public static readonly STSortingOrderInfo U_DEF_SORTING_OI_OBJ_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 0,
		m_oLayer = KCDefine.U_SORTING_L_DEF
	};

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	public static readonly STSortingOrderInfo U_DEF_SORTING_OI_UI_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 0,
		m_oLayer = KCDefine.U_SORTING_L_DEF_UI
	};
#else
	public static readonly STSortingOrderInfo U_DEF_SORTING_OI_UI_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 0,
		m_oLayer = KCDefine.U_SORTING_L_DEF
	};
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	// 정렬 순서 }

	// 레이어 마스크 {
	public static readonly int[] U_DEF_LAYER_MASK_UI_CAMERA = new int[] {
		KCDefine.U_LAYER_UI
	};

	public static readonly int[] U_DEF_LAYER_MASK_MAIN_CAMERA = new int[] {
		KCDefine.U_LAYER_DEF,
		KCDefine.U_LAYER_TRANSPARENT_FX,
		KCDefine.U_LAYER_IGNORE_RAYCAST,
		KCDefine.U_LAYER_WATER,

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
		KCDefine.U_LAYER_UI
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	};
	// 레이어 마스크 }

	// 디버그 콘솔
	public static readonly Vector2 U_POS_DEBUG_C_DEBUG_LOG_POPUP = new Vector2(36.0f, -36.0f);

	// 동기화 객체
	public static readonly object U_LOCK_OBJ_COMMON = new object();
	public static readonly object U_LOCK_OBJ_SCHEDULE_M_UPDATE = new object();
	public static readonly object U_LOCK_OBJ_TASK_M_UPDATE = new object();

	// 경로 {
	public static readonly string U_DATA_P_COMMON_APP_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonAppInfo.bytes";
	public static readonly string U_DATA_P_COMMON_USER_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonUserInfo.bytes";
	public static readonly string U_DATA_P_COMMON_GAME_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonGameInfo.bytes";

	public static readonly string U_OBJ_P_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_Text";
	public static readonly string U_OBJ_P_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_TextBtn";
	public static readonly string U_OBJ_P_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_TextScaleBtn";

	public static readonly string U_OBJ_P_LOCALIZE_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_LocalizeText";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_LocalizeTextBtn";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_LocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_Img";
	public static readonly string U_OBJ_P_RAW_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_RawImg";

	public static readonly string U_OBJ_P_IMG_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgBtn";
	public static readonly string U_OBJ_P_IMG_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgScaleBtn";

	public static readonly string U_OBJ_P_IMG_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgTextBtn";
	public static readonly string U_OBJ_P_IMG_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgTextScaleBtn";

	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgLocalizeTextBtn";
	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgLocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_SCROLL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_ScrollView";
	public static readonly string U_OBJ_P_RECYCLE_SCROLL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_RecycleScrollView";
	public static readonly string U_OBJ_P_PAGE_SCROLL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_PageScrollView";

	public static readonly string U_OBJ_P_PARTICLE_FX = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_ParticleFX";
	public static readonly string U_OBJ_P_SPRITE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_2D_ROOT}U_Sprite";

	public static readonly string U_OBJ_P_FPS_COUNTER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_FPSCounter";
	public static readonly string U_OBJ_P_TIMER_MANAGER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_TimerManager";

	public static readonly string U_OBJ_P_DEBUG_CONSOLE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_DebugConsole";
	public static readonly string U_OBJ_P_DEBUG_LOG_ITEM = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_DebugLogItem";

	public static readonly string U_OBJ_P_G_BG_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_BGSnd";
	public static readonly string U_OBJ_P_G_FX_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_FXSnd";

	public static readonly string U_OBJ_P_G_ALERT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP_ROOT}G_AlertPopup";
	public static readonly string U_OBJ_P_G_TOAST_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP_ROOT}G_ToastPopup";

	public static readonly string U_OBJ_P_G_DRAG_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_DragResponder";
	public static readonly string U_OBJ_P_G_TOUCH_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_TouchResponder";

	public static readonly string U_ASSET_P_G_BUILD_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BuildInfoTable";
	public static readonly string U_ASSET_P_G_BUILD_OPTS_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BuildOptsTable";
	public static readonly string U_ASSET_P_G_DEFINE_SYMBOL_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DefineSymbolTable";
	public static readonly string U_ASSET_P_G_PROJ_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProjInfoTable";
	public static readonly string U_ASSET_P_G_DEVICE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DeviceInfoTable";
	public static readonly string U_ASSET_P_G_ITEM_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ItemInfoTable";
	public static readonly string U_ASSET_P_G_SALE_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleProductInfoTable";
	
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_01 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_01";
	public static readonly string U_ASSET_P_LIGHTING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_UTILITY}U_LightingSettings";

	public static readonly string U_TABLE_P_G_ITEM_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_ItemInfoTable";
	public static readonly string U_TABLE_P_G_COMMON_VALUE = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}G_ValueTable_Common";
	public static readonly string U_TABLE_P_G_COMMON_STRING = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_STRING_INFO_ROOT}G_StringTable_Common";

	public static readonly string U_TABLE_P_FMT_G_COMMON_VALUE = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}{KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE}";
	public static readonly string U_TABLE_P_FMT_G_COMMON_STRING = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_STRING_INFO_ROOT}{KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE}";

	public static readonly string U_TABLE_P_FMT_G_LOCALIZE_COMMON_VALUE = string.Format(KCDefine.U_TABLE_P_FMT_G_COMMON_VALUE, "G_ValueTable_Common", "{0}");
	public static readonly string U_TABLE_P_FMT_G_LOCALIZE_COMMON_STRING = string.Format(KCDefine.U_TABLE_P_FMT_G_COMMON_STRING, "G_StringTable_Common", "{0}");

	public static readonly string U_TABLE_P_G_KOREAN_COMMON_STRING = string.Format(KCDefine.U_TABLE_P_FMT_G_LOCALIZE_COMMON_STRING, SystemLanguage.Korean);
	public static readonly string U_TABLE_P_G_ENGLISH_COMMON_STRING = string.Format(KCDefine.U_TABLE_P_FMT_G_LOCALIZE_COMMON_STRING, SystemLanguage.English);

	public static readonly string U_BASE_TABLE_P_G_LOCALIZE_COMMON_VALUE = KCDefine.U_TABLE_P_G_COMMON_VALUE;
	public static readonly string U_BASE_TABLE_P_G_LOCALIZE_COMMON_STRING = KCDefine.U_TABLE_P_G_COMMON_STRING;

	public static readonly string U_FONT_P_G_THAI = $"{KCDefine.B_DIR_P_FONTS}{KCDefine.B_DIR_P_GLOBAL}G_ThaiFont";

	public static readonly string U_SND_P_G_TOUCH_BEGIN = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchBegin";
	public static readonly string U_SND_P_G_TOUCH_END = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchEnd";
	public static readonly string U_SND_P_G_POPUP_CLOSE = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_PopupClose";

	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_ASSET = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPAsset";
	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_RENDER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPRenderData";
	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_SSAO_RENDER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPSSAORenderData";

	public static readonly string U_IMG_P_G_SPLASH = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_Splash";
	public static readonly string U_IMG_P_G_WHITE = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_UnityWhite";

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	public static readonly string U_IMG_P_SCREENSHOT = $"{Application.identifier}/Screenshot.png";
#else
	public static readonly string U_IMG_P_SCREENSHOT = $"{KCDefine.B_DIR_P_WRITABLE}Screenshot.png";
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	// 경로 }
	#endregion			// 런타임 상수

	#region 조건부 상수
#if UNITY_EDITOR
	// 퀄리티 {
	public const bool U_QUALITY_ASYNC_UPLOAD_PERSISTENT_BUFFER = true;

	public const int U_QUALITY_ANTI_ALIASING = 0;
	public const int U_QUALITY_MAX_LOD_LEVEL = 0;
	public const int U_QUALITY_ASYNC_UPLOAD_TIME_SLICE = 2;
	public const int U_QUALITY_ASYNC_UPLOAD_BUFFER_SIZE = 16;

	public const float U_QUALITY_RESOLUTION_SCALE_FIXED_DPI_FACTOR = 1.0f;
	public const EQualityLevel U_DEF_QUALITY_LEVEL = EQualityLevel.AUTO;
	// 퀄리티 }

	// 스크립트 순서 {
	public const int U_SCRIPT_O_SINGLETON = -10;
	public const int U_SCRIPT_O_BANNER_ADS_CORRECTOR = 5;

	public const int U_SCRIPT_O_INIT_SCENE_MANAGER = -20;
	public const int U_SCRIPT_O_SETUP_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_START_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_LOADING_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_SPLASH_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_AGREE_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_LATE_SETUP_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_PERMISSION_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 2;
	// 스크립트 순서 }

	// 광원 {
#if REALTIME_LIGHTMAP_BAKE_ENABLE
	public const LightmapBakeType U_LIGHTMAP_BAKE_TYPE_DIRECTIONAL = LightmapBakeType.Realtime;
#else
	public const LightmapBakeType U_LIGHTMAP_BAKE_TYPE_DIRECTIONAL = LightmapBakeType.Mixed;
#endif			// #if REALTIME_LIGHTMAP_BAKE_ENABLE
	// 광원 }

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
	// 개수
	public const int U_MAX_NUM_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ = 4;

	// 크기
	public const int U_SIZE_UNIVERSAL_RP_COLOR_GRADING_LUT = 32;

	// 비율
	public const float U_SCALE_UNIVERSAL_RP_RENDERING = 1.0f;

	// 길이
	public const float U_PERCENT_UNIVERSAL_RP_CASCADE_2_SPLIT = 0.25f;

	// 이름 {
	public const string U_FIELD_N_UNIVERSAL_RP_ANTI_ALIASING = "m_MSAA";
	public const string U_FIELD_N_UNIVERSAL_RP_OPAQUE_DOWN_SAMPLING = "m_OpaqueDownsampling";

	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_2_SPLIT = "m_Cascade2Split";
	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_3_SPLIT = "m_Cascade3Split";
	public const string U_FIELD_N_UNIVERSAL_RP_CASCADE_4_SPLIT = "m_Cascade4Split";

	public const string U_FIELD_N_UNIVERSAL_RP_RENDERER_DATAS = "m_RendererDataList";
	public const string U_FIELD_N_UNIVERSAL_RP_SUPPORTS_SOFT_SHADOW = "m_SoftShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_SUPPORTS_TERRAIN_HOLES = "m_SupportsTerrainHoles";
	public const string U_FIELD_N_UNIVERSAL_RP_SUPPORTS_MIXED_LIGHTING = "m_MixedLightingSupported";
	
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_RENDERING_MODE = "m_MainLightRenderingMode";
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SUPPORTS_SHADOW = "m_MainLightShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_MAIN_LIGHT_SHADOW_MAP_RESOLUTION = "m_MainLightShadowmapResolution";

	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_RENDERING_MODE = "m_AdditionalLightsRenderingMode";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SUPPORTS_SHADOW = "m_AdditionalLightShadowsSupported";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_PER_OBJ_LIMIT = "m_AdditionalLightsPerObjectLimit";
	public const string U_FIELD_N_UNIVERSAL_RP_ADDITIONAL_LIGHT_SHADOW_MAP_RESOLUTION = "m_AdditionalLightShadowmapResolution";
	// 이름 }

#if ULTRA_QUALITY_LEVEL_ENABLE
	// 옵션 {
	public const bool U_OPTS_UNIVERSAL_RP_SUPPORTS_HDR = true;

	public const Downsampling U_OPTS_UNIVERSAL_RP_DOWN_SAMPLING = Downsampling._2xBilinear;
	public const ColorGradingMode U_OPTS_UNIVERSAL_RP_COLOR_GRADING_MODE = ColorGradingMode.HighDynamicRange;
	public const EShadowCascadesOpts U_OPTS_UNIVERSAL_RP_SHADOW_CASCADES = EShadowCascadesOpts.FOUR_CASCADES;
	// 옵션 }
#elif HIGH_QUALITY_LEVEL_ENABLE
	// 옵션 {
	public const bool U_OPTS_UNIVERSAL_RP_SUPPORTS_HDR = true;

	public const Downsampling U_OPTS_UNIVERSAL_RP_DOWN_SAMPLING = Downsampling._4xBilinear;
	public const ColorGradingMode U_OPTS_UNIVERSAL_RP_COLOR_GRADING_MODE = ColorGradingMode.HighDynamicRange;
	public const EShadowCascadesOpts U_OPTS_UNIVERSAL_RP_SHADOW_CASCADES = EShadowCascadesOpts.THREE_CASCADES;
	// 옵션 }
#else
	// 옵션 {
	public const bool U_OPTS_UNIVERSAL_RP_SUPPORTS_HDR = false;

	public const Downsampling U_OPTS_UNIVERSAL_RP_DOWN_SAMPLING = Downsampling._2xBilinear;
	public const ColorGradingMode U_OPTS_UNIVERSAL_RP_COLOR_GRADING_MODE = ColorGradingMode.LowDynamicRange;
	public const EShadowCascadesOpts U_OPTS_UNIVERSAL_RP_SHADOW_CASCADES = EShadowCascadesOpts.TWO_CASCADES;
	// 옵션 }
#endif			// #if ULTRA_QUALITY_LEVEL_ENABLE
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
	// 식별자
	public const string U_KEY_UNITY_MS_LOGIN_WITH_APPLE_CALLBACK = "UnityMSLoginWithAppleCallback";
	public const string U_KEY_UNITY_MS_GET_CREDENTIAL_STATE_CALLBACK = "UnityMSGetCredentialStateCallback";

	// 이름 {
	public const string U_OBJ_N_LOGIN_WITH_APPLE = "LoginWithApple";

#if HAPTIC_FEEDBACK_ENABLE
	public const string U_MODEL_N_IPHONE = "iPhone";
	public const string U_MODEL_N_IPAD = "iPad";
#endif			// #if HAPTIC_FEEDBACK_ENABLE
	// 이름 }
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
	// 시간
	public const float U_DELTA_T_PERMISSION_M_REQUEST_CHECK = 0.25f;
	public const float U_MAX_DELTA_T_PERMISSION_M_REQUEST_CHECK = 0.5f;
#endif			// #if UNITY_ANDROID

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 시간
	public const float U_DELTA_T_DYNAMIC_DEBUG = 1.0f;

	// 형식 {
	public const string U_FMT_STATIC_DEBUG_MSG = "{0}\n\n{1}";
	public const string U_FMT_DYNAMIC_DEBUG_MSG = "{0}\n\n{1}";

	public const string U_FMT_STATIC_DEBUG_INFO_A = "Resolution: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_B = "Design Resolution: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_C = "Canvas Size: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_D = "UI Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_E = "Object Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_F = "UI Root Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_G = "Object Root Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_H = "Safe Area: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_FMT_STATIC_DEBUG_INFO_I = "Screen DPI: <color=orange>{0:0.0}</color>, Banner Ads Height: <color=orange>{1:0.0}</color>";

	public const string U_FMT_DYNAMIC_DEBUG_INFO_A = "GC: <color=orange>{0:0.0}</color> MB, Used Heap: <color=orange>{1:0.0}</color> MB\n";
	public const string U_FMT_DYNAMIC_DEBUG_INFO_B = "Mono Heap: <color=orange>{0:0.0}</color> MB, Mono Used: <color=orange>{1:0.0}</color> MB\n";
	public const string U_FMT_DYNAMIC_DEBUG_INFO_C = "Temp Alloc: <color=orange>{0:0.0}</color> MB, Total Alloc: <color=orange>{1:0.0}</color> MB\n";
	public const string U_FMT_DYNAMIC_DEBUG_INFO_D = "Reserved: <color=orange>{0:0.0}</color> MB, Unused Reserved: <color=orange>{1:0.0}</color> MB\n";
	public const string U_FMT_DYNAMIC_DEBUG_INFO_E = "GPU Alloc: <color=orange>{0:0.0}</color> MB\n";
	public const string U_FMT_DYNAMIC_DEBUG_INFO_F = "Time Scale: <color=orange>{0:0.00000}</color>";
	// 형식 }

	// 이름 {
	public const string U_OBJ_N_SCREEN_DEBUG_UI_ROOT = "ScreenDebugUIs";
	public const string U_OBJ_N_SCREEN_DEBUG_TEXT_ROOT = "DebugTexts";

	public const string U_OBJ_N_SCREEN_STATIC_DEBUG_TEXT = "StaticDebugText";
	public const string U_OBJ_N_SCREEN_DYNAMIC_DEBUG_TEXT = "DynamicDebugText";

	public const string U_OBJ_N_SCREEN_FPS_BTN = "FPSBtn";
	public const string U_OBJ_N_SCREEN_DEBUG_BTN = "DebugBtn";

	public const string U_OBJ_N_FPS_C_STATIC_TEXT = "StaticInfoText";
	public const string U_OBJ_N_FPS_C_DYNAMIC_TEXT = "DynamicInfoText";
	// 이름 }
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if TENJIN_MODULE_ENABLE
	// 식별자
	public const string U_KEY_TENJIN_M_RECEIPT = "json";
	public const string U_KEY_TENJIN_M_PAYLOAD = "Payload";
	public const string U_KEY_TENJIN_M_SIGNATURE = "signature";
#endif			// #if TENJIN_MODULE_ENABLE

#if SINGULAR_MODULE_ENABLE
	// 이름
	public const string U_OBJ_N_SINGULAR_SDK = "SingularSDK";
#endif			// #if SINGULAR_MODULE_ENABLE

#if ADS_MODULE_ENABLE
	// 시간
	public const float U_DELTA_T_ADS_M_ADS_LOAD = 5.0f;
	public const float U_DELTA_T_REWARD_ATI_UPDATE = 0.5f;

	// 식별자 {
	public const string U_KEY_ADS_M_BANNER_ADS_ID = "AdsMBannerAdsID";
	public const string U_KEY_ADS_M_REWARD_ADS_ID = "AdsMRewardAdsID";
	public const string U_KEY_ADS_M_FULLSCREEN_ADS_ID = "AdsMFullscreenAdsID";
	public const string U_KEY_ADS_M_RESUME_ADS_ID = "AdsMResumeAdsID";

	public const string U_KEY_FMT_ADS_M_BANNER_ADS_LOADER_INFO = "AdsMBannerAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_REWARD_ADS_LOADER_INFO = "AdsMRewardAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_FULLSCREEN_ADS_LOADER_INFO = "AdsMFullscreenAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_RESUME_ADS_LOADER_INFO = "AdsMResumeAdsLoaderInfo_{0}";
	// 식별자 }

#if ADMOB_ENABLE
	// 식별자 {
	public const string U_ADS_ID_ADMOB_TEST_BANNER_ADS = "ca-app-pub-3940256099942544/6300978111";
	public const string U_ADS_ID_ADMOB_TEST_REWARD_ADS = "ca-app-pub-3940256099942544/5224354917";
	public const string U_ADS_ID_ADMOB_TEST_FULLSCREEN_ADS = "ca-app-pub-3940256099942544/1033173712";
	public const string U_ADS_ID_ADMOB_TEST_RESUME_ADS = "ca-app-pub-3940256099942544/1033173712";

	public const string U_KEY_ADS_M_ADMOB_INIT_CALLBACK = "AdsMAdmobInitCallback";

	public const string U_KEY_ADS_M_ADMOB_BANNER_ADS_LOAD_CALLBACK = "AdsMAdmobBannerAdsLoadCallback";
	public const string U_KEY_ADS_M_ADMOB_BANNER_ADS_LOAD_FAIL_CALLBACK = "AdsMAdmobBannerAdsLoadFailCallback";
	public const string U_KEY_ADS_M_ADMOB_BANNER_ADS_CLOSE_CALLBACK = "AdsMAdmobBannerAdsCloseCallback";

	public const string U_KEY_ADS_M_ADMOB_REWARD_ADS_LOAD_CALLBACK = "AdsMAdmobRewardAdsLoadCallback";
	public const string U_KEY_ADS_M_ADMOB_REWARD_ADS_LOAD_FAIL_CALLBACK = "AdsMAdmobRewardAdsLoadFailCallback";
	public const string U_KEY_ADS_M_ADMOB_REWARD_ADS_CLOSE_CALLBACK = "AdsMAdmobRewardAdsCloseCallback";
	public const string U_KEY_ADS_M_ADMOB_REWARD_ADS_RECEIVE_REWARD_CALLBACK = "AdsMAdmobRewardAdsReceiveRewardCallback";

	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_LOAD_CALLBACK = "AdsMAdmobFullscreenAdsLoadCallback";
	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMAdmobFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMAdmobFullscreenAdsCloseCallback";
	
	public const string U_KEY_ADS_M_ADMOB_RESUME_ADS_LOAD_CALLBACK = "AdsMAdmobResumeAdsLoadCallback";
	public const string U_KEY_ADS_M_ADMOB_RESUME_ADS_LOAD_FAIL_CALLBACK = "AdsMAdmobResumeAdsLoadFailCallback";
	public const string U_KEY_ADS_M_ADMOB_RESUME_ADS_CLOSE_CALLBACK = "AdsMAdmobResumeAdsCloseCallback";
	// 식별자 }
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	// 식별자 {
	public const string U_KEY_ADS_M_IRON_SRC_INIT_CALLBACK = "AdsMIronSrcInitCallback";

	public const string U_KEY_ADS_M_IRON_SRC_BANNER_ADS_LOAD_CALLBACK = "AdsMIronSrcBannerAdsLoadCallback";
	public const string U_KEY_ADS_M_IRON_SRC_BANNER_ADS_LOAD_FAIL_CALLBACK = "AdsMIronSrcBannerAdsLoadFailCallback";

	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_CLOSE_CALLBACK = "AdsMIronSrcRewardAdsCloseCallback";
	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_RECEIVE_REWARD_CALLBACK = "AdsMIronSrcRewardAdsReceiveRewardCallback";
	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_CHANGE_STATE_CALLBACK = "AdsMIronSrcRewardAdsChangeStateCallback";

	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_LOAD_CALLBACK = "AdsMIronSrcFullscreenAdsLoadCallback";
	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMIronSrcFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMIronSrcFullscreenAdsCloseCallback";
	// 식별자 }
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	// 식별자 {
	public const string U_KEY_ADS_M_APP_LOVIN_INIT_CALLBACK = "AdsMAppLovinInitCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_BANNER_ADS_LOAD_CALLBACK = "AdsMAppLovinBannerAdsLoadCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_BANNER_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinBannerAdsLoadFailCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinRewardAdsLoadFailCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_CLOSE_CALLBACK = "AdsMAppLovinRewardAdsCloseCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_RECEIVE_REWARD_CALLBACK = "AdsMAppLovinRewardAdsReceiveRewardCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMAppLovinFullscreenAdsCloseCallback";
	// 식별자 }
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE || TENJIN_MODULE_ENABLE || FACEBOOK_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE
	// 이름
	public const string U_LOG_N_APP_LAUNCH = "AppLaunch";

	// 식별자 {
	public const string U_LOG_KEY_USER_ID = "UserID";
	public const string U_LOG_KEY_COUNTRY_CODE = "CountryCode";

	public const string U_LOG_KEY_DEVICE_ID = "DeviceID";
	public const string U_LOG_KEY_PLATFORM = "Platform";
	public const string U_LOG_KEY_USER_TYPE = "UserType";
	public const string U_LOG_KEY_LOG_TIME = "LogTime";
	public const string U_LOG_KEY_INSTALL_TIME = "InstallTime";

	public const string U_LOG_KEY_PARAMS_A = "ParamsA";
	public const string U_LOG_KEY_PARAMS_B = "ParamsB";
	public const string U_LOG_KEY_PARAMS_C = "ParamsC";
	public const string U_LOG_KEY_PARAMS_D = "ParamsD";
	public const string U_LOG_KEY_PARAMS_E = "ParamsE";
	public const string U_LOG_KEY_PARAMS_F = "ParamsF";
	// 식별자 }

#if FLURRY_MODULE_ENABLE
	// 시간
	public const long U_TIMEOUT_FLURRY_M_NETWORK_CONNECTION = 60 * KCDefine.B_UNIT_SEC_TO_MILLISEC;

	// 식별자
	public const string U_KEY_FLURRY_M_INIT_CALLBACK = "FlurryMInitCallback";
#endif			// #if FLURRY_MODULE_ENABLE

#if TENJIN_MODULE_ENABLE
	// 식별자
	public const string U_KEY_TENJIN_M_INIT_CALLBACK = "TenjinMInitCallback";
#endif			// #if TENJIN_MODULE_ENABLE
	
#if FACEBOOK_MODULE_ENABLE
	// 식별자
	public const string U_KEY_FACEBOOK_M_INIT_CALLBACK = "FacebookMInitCallback";
	public const string U_KEY_FACEBOOK_M_LOGIN_CALLBACK = "FacebookMLoginCallback";
	public const string U_KEY_FACEBOOK_M_VIEW_STATE_SHOW_CALLBACK = "FacebookMViewStateShowCallback";
	public const string U_KEY_FACEBOOK_M_VIEW_STATE_CLOSE_CALLBACK = "FacebookMViewStateCloseCallback";
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	// 이름
	public const string U_TRACKING_N_APP_LAUNCH = KCDefine.U_LOG_N_APP_LAUNCH;
	
	// 식별자 {
	public const string U_KEY_FIREBASE_M_INIT_CALLBACK = "FirebaseMInitCallback";

	public const string U_CONFIG_KEY_FIREBASE_M_GAME = "GameConfig";
	public const string U_CONFIG_KEY_FIREBASE_M_DEVICE = "DeviceConfig";
	public const string U_CONFIG_KEY_FIREBASE_M_BUILD_VERSION = "BuildVersionConfig";

	public const string U_TRACKING_KEY_DEVICE_ID = KCDefine.U_LOG_KEY_DEVICE_ID;
	public const string U_TRACKING_KEY_PLATFORM = KCDefine.U_LOG_KEY_PLATFORM;
	public const string U_TRACKING_KEY_USER_TYPE = KCDefine.U_LOG_KEY_USER_TYPE;

	public const string U_TRACKING_KEY_PARAMS_A = KCDefine.U_LOG_KEY_PARAMS_A;
	public const string U_TRACKING_KEY_PARAMS_B = KCDefine.U_LOG_KEY_PARAMS_B;
	public const string U_TRACKING_KEY_PARAMS_C = KCDefine.U_LOG_KEY_PARAMS_C;
	public const string U_TRACKING_KEY_PARAMS_D = KCDefine.U_LOG_KEY_PARAMS_D;
	public const string U_TRACKING_KEY_PARAMS_E = KCDefine.U_LOG_KEY_PARAMS_E;
	public const string U_TRACKING_KEY_PARAMS_F = KCDefine.U_LOG_KEY_PARAMS_F;
	// 식별자 }

	// 노드
	public const string U_NODE_FIREBASE_POST_ITEM_LIST = "PostItemList";
	public const string U_NODE_FIREBASE_USER_INFO_LIST = "UserInfoList";
	public const string U_NODE_FIREBASE_PURCHASE_INFO_LIST = "PurchaseInfoList";

#if FIREBASE_AUTH_ENABLE
	// 식별자 {
	public const string U_KEY_FIREBASE_M_LOGIN_CALLBACK = "FirebaseMLoginCallback";	

#if UNITY_IOS
	public const string U_KEY_FIREBASE_M_GAME_CENTER_CALLBACK = "FirebaseMGameCenterCallback";
#endif			// #if UNITY_IOS
	// 식별자 }
#endif			// #if FIREBASE_AUTH_ENABLE

#if FIREBASE_DB_ENABLE
	// 식별자
	public const string U_KEY_FIREBASE_M_SAVE_DB_CALLBACK = "FirebaseMSaveDBCallback";
	public const string U_KEY_FIREBASE_M_LOAD_DB_CALLBACK = "FirebaseMLoadDBCallback";
#endif			// #if FIREBASE_DB_ENABLE

#if FIREBASE_REMOTE_CONFIG_ENABLE
	// 식별자
	public const string U_KEY_FIREBASE_M_LOAD_CONFIG_CALLBACK = "FirebaseMLoadConfigCallback";
#endif			// #if FIREBASE_REMOTE_CONFIG_ENABLE

#if FIREBASE_CLOUD_MSG_ENABLE
	// 식별자
	public const string U_KEY_FIREBASE_M_TOKEN_CALLBACK = "FirebaseMTokenCallback";
	public const string U_KEY_FIREBASE_M_MSG_CALLBACK = "FirebaseMMsgCallback";
#endif			// #if FIREBASE_CLOUD_MSG_ENABLE
#endif			// #if FIREBASE_MODULE_ENABLE

#if SINGULAR_MODULE_ENABLE
	// 식별자
	public const string U_KEY_SINGULAR_M_INIT_CALLBACK = "SingularMInitCallback";
#endif			// #if SINGULAR_MODULE_ENABLE
#endif			// #if FLURRY_MODULE_ENABLE || TENJIN_MODULE_ENABLE || FACEBOOK_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE

#if GAME_CENTER_MODULE_ENABLE
	// 식별자
	public const string U_KEY_GAME_CM_INIT_CALLBACK = "GameCMInitCallback";
	public const string U_KEY_GAME_CM_LOGIN_CALLBACK = "GameCMLoginCallback";
	public const string U_KEY_GAME_CM_LOAD_SCORES_CALLBACK = "GameCMLoadScoresCallback";
	public const string U_KEY_GAME_CM_UPDATE_SCORE_CALLBACK = "GameCMUpdateScoreCallback";
	public const string U_KEY_GAME_CM_UPDATE_ACHIEVEMENT_CALLBACK = "GameCMUpdateAchievementCallback";
#endif			// #if GAME_CENTER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	// 식별자 {
	public const string U_KEY_PURCHASE_M_INIT_CALLBACK = "PurchaseMInitCallback";
	public const string U_KEY_PURCHASE_M_INIT_FAIL_CALLBACK = "PurchaseMInitFailCallback";
	public const string U_KEY_PURCHASE_M_PURCHASE_FAIL_CALLBACK = "PurchaseMPurchaseFailCallback";

	public const string U_KEY_PURCHASE_M_CONFIRM_CALLBACK = "PurchaseMConfirmCallback";
	public const string U_KEY_PURCHASE_M_RESTORE_CALLBACK = "PurchaseMRestoreCallback";
	public const string U_KEY_PURCHASE_M_PURCHASE_RESULT_CALLBACK = "PurchaseMPurchaseResultCallback";
	// 식별자 }
#endif			// #if PURCHASE_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
	// 시간
	public const float U_DELTA_T_NOTI_M_REQUEST_CHECK = 0.15f;
	public const float U_MAX_DELTA_T_NOTI_M_REQUEST_CHECK = 0.5f;

	// 식별자
	public const string U_KEY_NOTI_M_INIT_CALLBACK = "NotiMInitCallback";

	// 그룹
	public const string U_DEF_GROUP_ID_NOTI = "DefNotiGroup";

#if UNITY_IOS
	// 옵션
	public const AuthorizationOption U_AUTH_OPTS_NOTI = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
	public const PresentationOption U_PRESENT_OPTS_NOTI = PresentationOption.Alert | PresentationOption.Sound;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
	// 그룹 정보 {
	public const Importance U_IMPORTANCE_NOTI = Importance.Default;
	
	public const string U_DEF_GROUP_N_NOTI = KCDefine.U_DEF_GROUP_ID_NOTI;
	public const string U_DEF_GROUP_DESC_NOTI = KCDefine.U_DEF_GROUP_ID_NOTI;
	// 그룹 정보 }
#endif			// #if UNITY_ANDROID
#endif			// #if NOTI_ENABLE
	#endregion			// 조건부 상수

	#region 조건부 런타임 상수
#if UNITY_IOS
	// 버전
	public static readonly System.Version U_MIN_VERSION_HAPTIC_FEEDBACK = new System.Version(10, 0, 0);
	public static readonly System.Version U_MIN_VERSION_LOGIN_WITH_APPLE = new System.Version(13, 0, 0);
	
#if HAPTIC_FEEDBACK_ENABLE
	// 햅틱 피드백 지원 모델
	public static readonly DeviceGeneration[] U_HAPTIC_FEEDBACK_SUPPORTS_MODELS = new DeviceGeneration[] {
		DeviceGeneration.iPhone7, DeviceGeneration.iPhone7Plus, DeviceGeneration.iPhone8, DeviceGeneration.iPhone8Plus,
		DeviceGeneration.iPhoneX, DeviceGeneration.iPhoneXR, DeviceGeneration.iPhoneXS, DeviceGeneration.iPhoneXSMax,
		DeviceGeneration.iPhone11, DeviceGeneration.iPhone11Pro, DeviceGeneration.iPhone11ProMax, DeviceGeneration.iPhoneUnknown
	};
#endif			// #if HAPTIC_FEEDBACK_ENABLE
#endif			// #if UNITY_IOS

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
	// 길이
	public static readonly Vector2 U_PERCENT_UNIVERSAL_RP_CASCADE_3_SPLIT = new Vector2(0.1f, 0.3f);
	public static readonly Vector3 U_PERCENT_UNIVERSAL_RP_CASCADE_4_SPLIT = new Vector3(0.075f, 0.2f, 0.45f);
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE

#if ADS_MODULE_ENABLE
	// 크기
	public static readonly Vector2 U_SIZE_PHONE_BANNER_ADS = new Vector2(320.0f, 50.0f);
	public static readonly Vector2 U_SIZE_TABLET_BANNER_ADS = new Vector2(728.0f, 90.0f);

#if ADMOB_ENABLE
	// 크기
	public static readonly AdSize U_SIZE_ADMOB_BANNER_ADS = new AdSize(AdSize.FullWidth, 50);
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	// 크기
	public static readonly IronSourceBannerSize U_SIZE_IRON_SRC_BANNER_ADS = new IronSourceBannerSize(320, 50);
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	// 색상
	public static readonly Color U_COLOR_APP_LOVIN_BANNER_BG = Color.black;
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || TENJIN_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE
	// 경로
	public static readonly string U_ASSET_P_G_PLUGIN_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_PluginInfoTable";
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || TENJIN_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || SINGULAR_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	// 권한
	public static readonly string[] U_DEF_PERMISSIONS_FACEBOOK = new string[] {
		"public_profile"
	};
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	// 시간
	public static readonly System.TimeSpan U_TIMEOUT_FIREBASE_SESSION = new System.TimeSpan(0, 0, 60);
	public static readonly System.TimeSpan U_TIMEOUT_FIREBASE_FETCH_CONFIG = new System.TimeSpan(0, 0, 30);

	// 경로
	public static readonly string U_DATA_P_G_GAME_CONFIG = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_GLOBAL}G_GameConfig";
	public static readonly string U_DATA_P_G_BUILD_VERSION_CONFIG = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_GLOBAL}G_BuildVersionConfig";
#endif			// #if FIREBASE_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	// 경로
	public static readonly string U_DATA_P_PURCHASE_M_PRODUCT_ID_LIST = $"{KCDefine.B_DIR_P_WRITABLE}PurchaseProductIDList.bytes";
	public static readonly string U_ASSET_P_G_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProductInfoTable";
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 런타임 상수
}