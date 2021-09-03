﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	// 개수 {
	public const int U_MAX_NUM_LAYERS = 32;
	public const int U_MAX_NUM_DUPLICATE_FX_SNDS = 10;

	public const int U_MAX_NUM_LEVEL_INFOS = 9999;
	public const int U_MAX_NUM_STAGE_INFOS = 999;
	public const int U_MAX_NUM_CHAPTER_INFOS = 99;
	// 개수 }

	// 크기
	public const int U_SIZE_OBJS_POOL = 10;
	public const int U_DEF_MAX_SIZE_FONT = 10;

	// 길이
	public const int U_MAX_LENGTH_LOG = 100000000;
	public const float U_MAX_PERCENT_ASYNC_OPERATION = 0.9f;

	// 단위
	public const float U_UNIT_TABLET_INCHES = 6.5f;
	public const float U_UNIT_SCROLL_SENSITIVITY = 250.0f;

	// 세기
	public const float U_INTENSITY_VIBRATE = 1.0f;

	// 깊이
	public const float U_DEPTH_UIS_CAMERA = 50.0f;
	public const float U_DEPTH_MAIN_CAMERA = -50.0f;

	// 거리
	public const float U_DISTANCE_CAMERA_PLANE = 5.0f;
	public const float U_DISTANCE_CAMERA_FAR_PLANE = 5000.0f;
	public const float U_DISTANCE_CAMERA_NEAR_PLANE = 0.1f;

	// 비율 {
	public const float U_SCALE_POPUP = 1.0f;
	public const float U_MIN_SCALE_POPUP = 0.001f;

	public const float U_SCALE_TOUCH = 1.05f;
	public const float U_SCALE_PAGE_SCROLL = 0.35f;
	// 비율 }

	// 시간 {
	public const float U_DELAY_INIT = 0.15f;
	public const float U_DELAY_NEXT_SCENE_LOAD = 0.5f;
	public const float U_DELAY_POPUP_SHOW_ANI = KCDefine.B_DELTA_T_INTERMEDIATE;
	
	public const float U_DURATION_ANI = 0.25f;
	public const float U_DURATION_SCROLL_ANI = 0.25f;
	public const float U_DURATION_SCREEN_FADE_IN_ANI = 0.15f;
	public const float U_DURATION_SCREEN_FADE_OUT_ANI = 0.15f;

	public const float U_DURATION_POPUP_SCALE_ANI = 0.25f;
	public const float U_DURATION_POPUP_DROPDOWN_ANI = 0.35f;
	public const float U_DURATION_POPUP_SLIDE_ANI = 0.35f;

	public const float U_DURATION_TOAST_POPUP = 2.0f;

	public const float U_DURATION_LIGHT_VIBRATE = 0.05f;
	public const float U_DURATION_MEDIUM_VIBRATE = 0.1f;
	public const float U_DURATION_HEAVY_VIBRATE = 0.15f;

	public const float U_TIMEOUT_ASYNC_TASK = 30.0f;
	public const float U_TIMEOUT_NETWORK_CONNECTION = 30.0f;

	public const float U_DELTA_T_SCHEDULE_M_CALLBACK = 0.15f;
	// 시간 }

	// 레이어
	public const int U_LAYER_DEF = 0;
	public const int U_LAYER_TRANSPARENT_FX = 1;
	public const int U_LAYER_IGNORE_RAYCAST = 2;
	public const int U_LAYER_WATER = 4;
	public const int U_LAYER_UIS = 5;
	public const int U_LAYER_CUSTOM = 8;

	// 정렬 순서 {
	public const int U_SORTING_O_SCREEN_POPUP_UIS = 0;
	public const int U_SORTING_O_SCREEN_TOPMOST_UIS = 1;
	public const int U_SORTING_O_SCREEN_ABS_UIS = 2;
	public const int U_SORTING_O_SCREEN_BLIND_UIS = 3;
	public const int U_SORTING_O_SCREEN_DEBUG_UIS = 4;

	public const int U_SORTING_O_FPS_COUNTER = 5;
	public const int U_SORTING_O_DEBUG_CONSOLE = 6;
	// 정렬 순서 }

	// 애니메이션
	public const Ease U_EASE_ANI = Ease.OutQuad;

	// 광원 {
#if LIGHT_ENABLE && SHADOW_ENABLE
#if SOFT_SHADOW_ENABLE
	public const LightShadows U_LIGHT_SHADOW_TYPE = LightShadows.Soft;
#else
	public const LightShadows U_LIGHT_SHADOW_TYPE = LightShadows.Hard;
#endif			// #if SOFT_SHADOW_ENABLE
#else
	public const LightShadows U_LIGHT_SHADOW_TYPE = LightShadows.None;
#endif			// #if LIGHT_ENABLE && SHADOW_ENABLE
	// 광원 }

	// 회전
	public static readonly Vector3 U_ANGLE_MAIN_LIGHT = new Vector3(45.0f, 45.0f, 0.0f);
	
	// 형식
	public const string U_FMT_LOG_MSG = "[{0}]\nLogType: {1}\nCondition: {2}\nStackTrace:\n{3}==============================\n\n";

	// 식별자 {
	public const string U_ADS_ID_TEST_DEVICE = "TestDevice";

	public const string U_KEY_DEVICE_CMD = "Cmd";
	public const string U_KEY_DEVICE_MSG = "Msg";

	public const string U_KEY_NAME = "Name";
	public const string U_KEY_DESC = "Desc";
	public const string U_KEY_REPLACE = "Replace";
	public const string U_KEY_REWARD_QUALITY = "RewardQuality";

	public const string U_KEY_PLAY = "Play";
	public const string U_KEY_HELP = "Help";
	public const string U_KEY_FREE = "Free";
	public const string U_KEY_DAILY = "Daily";
	public const string U_KEY_EVENT = "Event";
	public const string U_KEY_CLEAR = "Clear";
	public const string U_KEY_PRICE = "Price";

	public const string U_KEY_LEVEL = "Level";
	public const string U_KEY_STAGE = "Stage";
	public const string U_KEY_CHAPTER = "Chapter";

	public const string U_KEY_ID = "ID";
	public const string U_KEY_STAGE_ID = "StageID";
	public const string U_KEY_CHAPTER_ID = "ChapterID";

	public const string U_KEY_LEVEL_MODE = "LevelMode";
	public const string U_KEY_LEVEL_KINDS = "LevelKinds";	
	public const string U_KEY_STAGE_KINDS = "StageKinds";
	public const string U_KEY_CHAPTER_KINDS = "ChapterKinds";

	public const string U_KEY_PRICE_KINDS = "PriceKinds";
	public const string U_KEY_REWARD_KINDS = "RewardKinds";
	public const string U_KEY_PRODUCT_KINDS = "ProductKinds";
	public const string U_KEY_MISSION_KINDS = "MissionKinds";
	public const string U_KEY_SALE_ITEM_KINDS = "SaleItemKinds";
	public const string U_KEY_SALE_PRODUCT_KINDS = "SaleProductKinds";

	public const string U_KEY_TUTORIAL_KINDS = "TutorialKinds";
	public const string U_KEY_NEXT_TUTORIAL_KINDS = "NextTutorialKinds";

	public const string U_KEY_FMT_NUM_ITEMS = "NumItems_{0:00}";
	public const string U_KEY_FMT_ITEM_KINDS = "ItemKinds_{0:00}";

	public const string U_KEY_FMT_NUM_TARGETS = "NumTargets_{0:00}";
	public const string U_KEY_FMT_TARGET_KINDS = "TargetKinds_{0:00}";

	public const string U_KEY_FMT_UNLOCK_NUM_TARGETS = "UnlockNumTargets_{0:00}";
	public const string U_KEY_FMT_UNLOCK_TARGET_KINDS = "UnlockTargetKinds_{0:00}";

	public const string U_KEY_FMT_STRS = "Str_{0:00}";
	public const string U_KEY_FMT_TUTORIAL_MSG = "TUTORIAL_MSG_{0:00}_{1:00}";
	// 식별자 }

	// 이름 {
	public const string U_OBJ_N_SCENE_UIS_TOP = "UIsRoot";
	public const string U_OBJ_N_SCENE_UIS_BASE = "Canvas";
	public const string U_OBJ_N_SCENE_UIS = "UIs";
	public const string U_OBJ_N_SCENE_TEST_UIS = "TestUIs";
	public const string U_OBJ_N_SCENE_PIVOT_UIS = "PivotUIs";
	public const string U_OBJ_N_SCENE_ANCHOR_UIS = "AnchorUIs";
	public const string U_OBJ_N_SCENE_EVENT_SYSTEM = "EventSystem";

	public const string U_OBJ_N_SCENE_UP_UIS = "UpUIs";
	public const string U_OBJ_N_SCENE_DOWN_UIS = "DownUIs";
	public const string U_OBJ_N_SCENE_LEFT_UIS = "LeftUIs";
	public const string U_OBJ_N_SCENE_RIGHT_UIS = "RightUIs";

	public const string U_OBJ_N_SCENE_POPUP_UIS = "PopupUIs";
	public const string U_OBJ_N_SCENE_TOPMOST_UIS = "TopmostUIs";
	public const string U_OBJ_N_SCENE_ABS_UIS = "AbsUIs";

	public const string U_OBJ_N_SCENE_BASE = "Base";
	public const string U_OBJ_N_SCENE_OBJS_BASE = "ObjsRoot";
	public const string U_OBJ_N_SCENE_OBJS = "Objs";
	public const string U_OBJ_N_SCENE_PIVOT_OBJS = "PivotObjs";
	public const string U_OBJ_N_SCENE_ANCHOR_OBJS = "AnchorObjs";

	public const string U_OBJ_N_SCENE_UP_OBJS = "UpObjs";
	public const string U_OBJ_N_SCENE_DOWN_OBJS = "DownObjs";
	public const string U_OBJ_N_SCENE_LEFT_OBJS = "LeftObjs";
	public const string U_OBJ_N_SCENE_RIGHT_OBJS = "RightObjs";

	public const string U_OBJ_N_SCENE_OBJS_CANVAS_TOP = "ObjsCanvasRoot";
	public const string U_OBJ_N_SCENE_OBJS_CANVAS_BASE = "ObjsCanvas";
	public const string U_OBJ_N_SCENE_CANVAS_OBJS = "CanvasObjs";
	public const string U_OBJ_N_SCENE_CANVAS_PIVOT_OBJS = "CanvasPivotObjs";

	public const string U_OBJ_N_SCENE_UIS_CAMERA = "UIs Camera";
	public const string U_OBJ_N_SCENE_MAIN_CAMERA = "Main Camera";
	public const string U_OBJ_N_SCENE_MAIN_LIGHT = "Directional Light";
	public const string U_OBJ_N_SCENE_MANAGER = "SceneManager";

	public const string U_OBJ_N_SCREEN_BLIND_UIS = "ScreenBlindUIs";
	public const string U_OBJ_N_SCREEN_POPUP_UIS = "ScreenPopupUIs";
	public const string U_OBJ_N_SCREEN_TOPMOST_UIS = "ScreenTopmostUIs";
	public const string U_OBJ_N_SCREEN_ABS_UIS = "ScreenAbsUIs";

	public const string U_OBJ_N_UP_BLIND_IMG = "UpBlindImg";
	public const string U_OBJ_N_DOWN_BLIND_IMG = "DownBlindImg";
	public const string U_OBJ_N_LEFT_BLIND_IMG = "LeftBlindImg";
	public const string U_OBJ_N_RIGHT_BLIND_IMG = "RightBlindImg";

	public const string U_OBJ_N_CONTENTS = "Contents";
	public const string U_OBJ_N_CONTENTS_UIS = "ContentsUIs";

	public const string U_OBJ_N_DESC_UIS = "DescUIs";
	public const string U_OBJ_N_PAGE_UIS = "PageUIs";
	
	public const string U_OBJ_N_VIEWPORT = "Viewport";
	public const string U_OBJ_N_PAGINATION = "Pagination";

	public const string U_OBJ_N_BLIND_UIS = "BlindUIs";
	public const string U_OBJ_N_CLEAR_UIS = "ClearUIs";
	public const string U_OBJ_N_CLEAR_FAIL_UIS = "ClearFailUIs";
	public const string U_OBJ_N_LOCK_UIS = "LockUIs";
	public const string U_OBJ_N_OPEN_UIS = "OpenUIs";
	public const string U_OBJ_N_GAUGE_UIS = "GaugeUIs";

	public const string U_OBJ_N_LOGIN_UIS = "LoginUIs";
	public const string U_OBJ_N_LOGOUT_UIS = "LogoutUIs";

	public const string U_OBJ_N_ADS_PRICE_UIS = "AdsPriceUIs";
	public const string U_OBJ_N_GOODS_PRICE_UIS = "GoodsPriceUIs";
	public const string U_OBJ_N_PURCHASE_PRICE_UIS = "PurchasePriceUIs";

	public const string U_OBJ_N_TOP_MENU_UIS = "TopMenuUIs";
	public const string U_OBJ_N_BOTTOM_MENU_UIS = "BottomMenuUIs";
	
	public const string U_OBJ_N_TITLE_TEXT = "TitleText";
	public const string U_OBJ_N_MSG_TEXT = "MsgText";

	public const string U_OBJ_N_NAME_TEXT = "NameText";
	public const string U_OBJ_N_DESC_TEXT = "DescText";

	public const string U_OBJ_N_LEVEL_TEXT = "LevelText";
	public const string U_OBJ_N_STAGE_TEXT = "StageText";
	public const string U_OBJ_N_CHAPTER_TEXT = "ChapterText";

	public const string U_OBJ_N_NUM_TEXT = "NumText";
	public const string U_OBJ_N_TIME_TEXT = "TimeText";
	public const string U_OBJ_N_PRICE_TEXT = "PriceText";
	public const string U_OBJ_N_COUNTDOWN_TEXT = "CountdownText";

	public const string U_OBJ_N_SCORE_TEXT = "ScoreText";
	public const string U_OBJ_N_LOADING_TEXT = "LoadingText";
	public const string U_OBJ_N_BEST_SCORE_TEXT = "BestScoreText";

	public const string U_OBJ_N_NUM_COINS_TEXT = "NumCoinsText";
	public const string U_OBJ_N_NUM_STARS_TEXT = "NumStarsText";
	public const string U_OBJ_N_NUM_STARS_STATE_TEXT = "NumStarsStateText";

	public const string U_OBJ_N_BG_IMG = "BGImg";
	public const string U_OBJ_N_BLIND_IMG = "BlindImg";
	public const string U_OBJ_N_SPLASH_IMG = "SplashImg";

	public const string U_OBJ_N_COINS_IMG = "CoinsImg";
	public const string U_OBJ_N_STARS_IMG = "StarsImg";

	public const string U_OBJ_N_CHECK_IMG = "CheckImg";
	public const string U_OBJ_N_RIBBON_IMG = "RibbonImg";
	public const string U_OBJ_N_PERCENT_IMG = "PercentImg";
	public const string U_OBJ_N_COMPLETE_IMG = "CompleteImg";

	public const string U_OBJ_N_LOCK_IMG = "LockImg";
	public const string U_OBJ_N_ICON_IMG = "IconImg";
	public const string U_OBJ_N_ITEM_IMG = "ItemImg";
	public const string U_OBJ_N_REWARD_IMG = "RewardImg";

	public const string U_OBJ_N_OK_BTN = "OKBtn";
	public const string U_OBJ_N_CANCEL_BTN = "CancelBtn";
	public const string U_OBJ_N_CLOSE_BTN = "CloseBtn";

	public const string U_OBJ_N_AGREE_BTN = "AgreeBtn";
	public const string U_OBJ_N_SERVICES_BTN = "ServicesBtn";
	public const string U_OBJ_N_PRIVACY_BTN = "PrivacyBtn";

	public const string U_OBJ_N_LOGIN_BTN = "LoginBtn";
	public const string U_OBJ_N_LOGOUT_BTN = "LogoutBtn";

	public const string U_OBJ_N_LOAD_BTN = "LoadBtn";
	public const string U_OBJ_N_SAVE_BTN = "SaveBtn";

	public const string U_OBJ_N_PLAY_BTN = "PlayBtn";
	public const string U_OBJ_N_STORE_BTN = "StoreBtn";
	public const string U_OBJ_N_SETTINGS_BTN = "SettingsBtn";

	public const string U_OBJ_N_PREV_BTN = "PrevBtn";
	public const string U_OBJ_N_NEXT_BTN = "NextBtn";
	public const string U_OBJ_N_RETRY_BTN = "RetryBtn";
	public const string U_OBJ_N_LEAVE_BTN = "LeaveBtn";
	public const string U_OBJ_N_CONTINUE_BTN = "ContinueBtn";

	public const string U_OBJ_N_ADS_BTN = "AdsBtn";
	public const string U_OBJ_N_ACQUIRE_BTN = "AcquireBtn";
	public const string U_OBJ_N_PURCHASE_BTN = "PurchaseBtn";
	public const string U_OBJ_N_RESTORE_BTN = "RestoreBtn";

	public const string U_OBJ_N_BG_SND_BTN = "BGSndBtn";
	public const string U_OBJ_N_FX_SNDS_BTN = "FXSndsBtn";
	public const string U_OBJ_N_NOTI_BTN = "NotiBtn";
	public const string U_OBJ_N_REVIEW_BTN = "ReviewBtn";
	public const string U_OBJ_N_SUPPORTS_BTN = "SupportsBtn";
	public const string U_OBJ_N_SYNC_BTN = "SyncBtn";

	public const string U_OBJ_N_EMPTY = "Empty";
	public const string U_OBJ_N_GAME_OBJ = "GameObj";
	public const string U_OBJ_N_ALERT_POPUP = "AlertPopup";
	public const string U_OBJ_N_TOAST_POPUP = "ToastPopup";

	public const string U_OBJ_N_TEXT = "Text";
	public const string U_OBJ_N_LOCALIZE_TEXT = "LocalizeText";

	public const string U_OBJ_N_IMG = "Img";
	public const string U_OBJ_N_RAW_IMG = "RawImg";
	public const string U_OBJ_N_FOCUS_IMG = "FocusImg";
	public const string U_OBJ_N_GAUGE_IMG = "GaugeImg";

	public const string U_OBJ_N_TEXT_BTN = "TextBtn";
	public const string U_OBJ_N_TEXT_SCALE_BTN = "TextScaleBtn";

	public const string U_OBJ_N_LOCALIZE_TEXT_BTN = "LocalizeTextBtn";
	public const string U_OBJ_N_LOCALIZE_TEXT_SCALE_BTN = "LocalizeTextScaleBtn";

	public const string U_OBJ_N_IMG_BTN = "ImgBtn";
	public const string U_OBJ_N_IMG_SCALE_BTN = "ImgScaleBtn";

	public const string U_OBJ_N_IMG_TEXT_BTN = "ImgTextBtn";
	public const string U_OBJ_N_IMG_TEXT_SCALE_BTN = "ImgTextScaleBtn";

	public const string U_OBJ_N_IMG_LOCALIZE_TEXT_BTN = "ImgLocalizeTextBtn";
	public const string U_OBJ_N_IMG_LOCALIZE_TEXT_SCALE_BTN = "ImgLocalizeTextScaleBtn";

	public const string U_OBJ_N_INPUT_FIELD = "InputField";
	public const string U_OBJ_N_DROPDOWN = "Dropdown";

	public const string U_OBJ_N_PAGE_VIEW = "PageView";
	public const string U_OBJ_N_SCROLL_VIEW = "ScrollView";
	public const string U_OBJ_N_RECYCLE_VIEW = "RecycleView";

	public const string U_OBJ_N_LINE_FX = "LineFX";
	public const string U_OBJ_N_PARTICLE_FX = "ParticleFX";

	public const string U_OBJ_N_SPRITE = "Sprite";
	public const string U_OBJ_N_DRAG_RESPONDER = "DragResponder";

	public const string U_OBJ_N_TOUCH_RESPONDER = "TouchResponder";
	public const string U_OBJ_N_BG_TOUCH_RESPONDER = "BGTouchResponder";

	public const string U_OBJ_N_SCREEN_F_TOUCH_RESPONDER = "ScreenFTouchResponder";
	public const string U_OBJ_N_INDICATOR_TOUCH_RESPONDER = "IndicatorTouchResponder";

	public const string U_OBJ_N_FMT_NUM_TEXT = "NumText_{0:00}";
	public const string U_OBJ_N_FMT_COUNTDOWN_TEXT = "CountdownText_{0:00}";
	public const string U_OBJ_N_FMT_POPUP_TOUCH_RESPONDER = "PopupTouchResponder_{0}";

	public const string U_IMG_N_SPRITE = "Sprite";
	public const string U_IMG_N_SPRITE_CLONE = "(Clone)";
	public const string U_IMG_N_UNKNOWN_SPRITE = "Unknown";

	public const string U_FUNC_N_ON_DRAG = "OnDrag";
	public const string U_FUNC_N_ON_POINTER_UP = "OnPointerUp";
	public const string U_FUNC_N_ON_POINTER_DOWN = "OnPointerDown";
	public const string U_FUNC_N_ON_POINTER_ENTER = "OnPointerEnter";
	public const string U_FUNC_N_ON_POINTER_EXIT = "OnPointerExit";

	public const string U_FUNC_N_INIT = "Init";
	public const string U_FUNC_N_RESET_LOCALIZE = "ResetLocalize";
	public const string U_FUNC_N_RESET_LEVEL_MODE = "ResetLevelMode";
	public const string U_FUNC_N_UPDATE_UIS_STATE = "UpdateUIsState";

	public const string U_FUNC_N_ON_INIT_SERVICES_MANAGER = "OnInitServicesManager";
	public const string U_FUNC_N_ON_INIT_ADS_MANAGER = "OnInitAdsManager";
	public const string U_FUNC_N_ON_INIT_FLURRY_MANAGER = "OnInitFlurryManager";
	public const string U_FUNC_N_ON_INIT_FACEBOOK_MANAGER = "OnInitFacebookManager";
	public const string U_FUNC_N_ON_INIT_FIREBASE_MANAGER = "OnInitFirebaseManager";
	public const string U_FUNC_N_ON_INIT_APPS_FLYER_MANAGER = "OnInitAppsFlyerManager";
	public const string U_FUNC_N_ON_INIT_GAME_ANALYTICS_MANAGER = "OnInitGameAnalyticsManager";
	public const string U_FUNC_N_ON_INIT_SINGULAR_MANAGER = "OnInitSingularManager";
	public const string U_FUNC_N_ON_INIT_GAME_CENTER_MANAGER = "OnInitGameCenterManager";
	public const string U_FUNC_N_ON_INIT_PURCHASE_MANAGER = "OnInitPurchaseManager";
	public const string U_FUNC_N_ON_INIT_NOTI_MANAGER = "OnInitNotiManager";

	public const string U_INPUT_E_N_JUMP = "Jump";
	public const string U_INPUT_E_N_VERTICAL = "Vertical";
	public const string U_INPUT_E_N_HORIZONTAL = "Horizontal";

	public const string U_PROPERTY_N_TEXT = "text";
	public const string U_PROPERTY_N_COLOR = "color";
	public const string U_PROPERTY_N_SPRITE = "sprite";

	public const string U_ASSET_N_LIGHTING_SETTINGS = "U_LightingSettings";
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
	public const string U_TAG_UIS_CAMERA = "UIsCamera";
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

#if !CAMERA_STACK_ENABLE
	public const string U_SORTING_L_UNDERGROUND_UIS = "UndergroundUIs";
	public const string U_SORTING_L_BACKGROUND_UIS = "BackgroundUIs";
	public const string U_SORTING_L_DEF_UIS = "DefaultUIs";
	public const string U_SORTING_L_FOREGROUND_UIS = "ForegroundUIs";
	public const string U_SORTING_L_OVERGROUND_UIS = "OvergroundUIs";
	public const string U_SORTING_L_TOP_UIS = "TopUIs";
	public const string U_SORTING_L_TOPMOST_UIS = "TopmostUIs";
	public const string U_SORTING_L_ABS_UIS = "AbsUIs";
#endif			// #if !CAMERA_STACK_ENABLE
	// 정렬 레이어 }

	// 씬 관리자
	public const string U_KEY_FMT_SCENE_M_TOUCH_RESPONDER = "SceneMTouchResponder_{0}";

	// 사운드 관리자
	public const string U_OBJ_N_SND_M_BG_SND = "BGSnd";
	public const string U_OBJ_N_SND_M_FX_SND = "FXSnd";

	// 디버그 콘솔
	public const string U_OBJ_N_DEBUG_C_LOG_WND = "DebugLogWindow";

	// 문자열 테이블
	public const string U_KEY_STR_T_ID = "ID";
	public const string U_KEY_STR_T_STR = "Str";

	// 값 테이블 {
	public const int U_IDX_VAL_T_INT_VALS = 0;
	public const int U_IDX_VAL_T_FLT_VALS = 1;
	public const int U_IDX_VAL_T_STR_VALS = 2;

	public const string U_KEY_VAL_T_ID = "ID";
	public const string U_KEY_VAL_T_VAL = "Val";
	public const string U_KEY_VAL_T_VAL_TYPE = "ValType";
	// 값 테이블 }
	
	// 서비스 관리자 {
	public const string U_KEY_SERVICES_M_RECEIPT = "json";
	public const string U_KEY_SERVICES_M_PAYLOAD = "Payload";
	public const string U_KEY_SERVICES_M_SIGNATURE = "signature";

	public const string U_KEY_SERVICES_M_INIT_CALLBACK = "ServicesMInitCallback";
	// 서비스 관리자 }

	// 유니티 메세지 전송자 {
	public const string U_KEY_UNITY_MS_APP_ID = "AppID";
	public const string U_KEY_UNITY_MS_VER = "Ver";
	public const string U_KEY_UNITY_MS_TIMEOUT = "Timeout";
	
	public const string U_KEY_UNITY_MS_ALERT_TITLE = "Title";
	public const string U_KEY_UNITY_MS_ALERT_MSG = "Msg";
	public const string U_KEY_UNITY_MS_ALERT_OK_BTN_TEXT = "OKBtnText";
	public const string U_KEY_UNITY_MS_ALERT_CANCEL_BTN_TEXT = "CancelBtnText";

	public const string U_KEY_UNITY_MS_MAIL_RECIPIENT = "Recipient";
	public const string U_KEY_UNITY_MS_MAIL_TITLE = "Title";
	public const string U_KEY_UNITY_MS_MAIL_MSG = "Msg";

	public const string U_KEY_UNITY_MS_VIBRATE_TYPE = "Type";
	public const string U_KEY_UNITY_MS_VIBRATE_STYLE = "Style";
	public const string U_KEY_UNITY_MS_VIBRATE_DURATION = "Duration";
	public const string U_KEY_UNITY_MS_VIBRATE_INTENSITY = "Intensity";

	public const string U_KEY_UNITY_MS_SHARE_MSG_CALLBACK = "UnityMSShareMsgCallback";
	
	public const string U_CLS_N_UNITY_MS_MSG_RECEIVER = "dante.distribution.android.CAndroidPlugin";
	public const string U_FUNC_N_UNITY_MS_MSG_HANDLER = "handleUnityMsg";
	// 유니티 메세지 전송자 }

	// 디바이스 메세지 수신자 {
	public const string U_KEY_DEVICE_MR_VER = KCDefine.U_KEY_UNITY_MS_VER;
	public const string U_KEY_DEVICE_MR_RESULT = "Result";

	public const string U_KEY_FMT_DEVICE_MR_HANDLE_MSG_CALLBACK = "DeviceMRHandleMsgCallback_{0}";
	public const string U_FUNC_N_DEVICE_MR_MSG_HANDLER = "HandleDeviceMsg";
	// 디바이스 메세지 수신자 }
	#endregion			// 기본

	#region 런타임 상수
	// 영역
	public static readonly Rect U_RECT_CAMERA = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

	// 색상 {
	public static readonly Color U_COLOR_NORM = Color.white;
	public static readonly Color U_COLOR_PRESS = new Color(0.75f, 0.75f, 0.75f, 1.0f);
	public static readonly Color U_COLOR_SEL = Color.white;
	public static readonly Color U_COLOR_HIGHLIGHT = Color.white;
	public static readonly Color U_COLOR_DISABLE = new Color(0.58f, 0.58f, 0.58f, 1.0f);
	public static readonly Color U_COLOR_TRANSPARENT = new Color(0.0f, 0.0f, 0.0f, 0.0f);

	public static readonly Color U_COLOR_BLIND_UIS = Color.black;
	public static readonly Color U_COLOR_SCREEN_FADE_IN = Color.black;
	public static readonly Color U_COLOR_SCREEN_FADE_OUT = KCDefine.U_COLOR_TRANSPARENT;
	
	public static readonly Color U_COLOR_POPUP_BG = new Color(0.0f, 0.0f, 0.0f, 0.75f);
	public static readonly Color U_COLOR_INDICATOR_BG = KCDefine.U_COLOR_POPUP_BG;

#if UNITY_EDITOR
	public static readonly Color U_COLOR_CAMERA_BG = new Color(0.15f, 0.15f, 0.15f, 1.0f);
#else
	public static readonly Color U_COLOR_CAMERA_BG = Color.black;
#endif			// #if UNITY_EDITOR
	// 색상 }

	// 크기
	public static readonly Vector3 U_MIN_SIZE_ALERT_POPUP = new Vector3(400.0f, 300.0f, 0.0f);

	// 태그
	public static readonly string[] U_TAGS = new string[] {
		KCDefine.U_TAG_ENEMY,
		KCDefine.U_TAG_OBSTACLE,
		KCDefine.U_TAG_UIS_CAMERA,
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

#if !CAMERA_STACK_ENABLE
		KCDefine.U_SORTING_L_UNDERGROUND_UIS,
		KCDefine.U_SORTING_L_BACKGROUND_UIS,
		KCDefine.U_SORTING_L_DEF_UIS,
		KCDefine.U_SORTING_L_FOREGROUND_UIS,
		KCDefine.U_SORTING_L_OVERGROUND_UIS,
		KCDefine.U_SORTING_L_TOP_UIS,
		KCDefine.U_SORTING_L_TOPMOST_UIS,
		KCDefine.U_SORTING_L_ABS_UIS
#endif			// #if !CAMERA_STACK_ENABLE
	};

	// 정렬 순서 {
	public static readonly STSortingOrderInfo U_SORTING_OI_OBJS_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 0,
		m_oLayer = KCDefine.U_SORTING_L_DEF
	};

#if CAMERA_STACK_ENABLE
	public static readonly STSortingOrderInfo U_SORTING_OI_UIS_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 1,
		m_oLayer = KCDefine.U_SORTING_L_DEF
	};
#else
	public static readonly STSortingOrderInfo U_SORTING_OI_UIS_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 0,
		m_oLayer = KCDefine.U_SORTING_L_DEF_UIS
	};
#endif			// #if CAMERA_STACK_ENABLE
	// 정렬 순서 }

	// 레이어 마스크 {
	public static readonly int[] U_LAYER_MASK_UIS_CAMERA = new int[] {
		KCDefine.U_LAYER_UIS
	};

	public static readonly int[] U_LAYER_MASK_MAIN_CAMERA = new int[] {
		KCDefine.U_LAYER_DEF,
		KCDefine.U_LAYER_TRANSPARENT_FX,
		KCDefine.U_LAYER_IGNORE_RAYCAST,
		KCDefine.U_LAYER_WATER,

#if !CAMERA_STACK_ENABLE
		KCDefine.U_LAYER_UIS
#endif			// #if !CAMERA_STACK_ENABLE
	};
	// 레이어 마스크 }
	
	// 동기화 객체
	public static readonly object U_LOCK_OBJ_COMMON = new object();
	public static readonly object U_LOCK_OBJ_TASK_M_UPDATE = new object();
	public static readonly object U_LOCK_OBJ_SCHEDULE_M_UPDATE = new object();

	// 경로 {
	public static readonly string U_DATA_P_COMMON_APP_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonAppInfo.bytes";
	public static readonly string U_DATA_P_COMMON_USER_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonUserInfo.bytes";
	public static readonly string U_DATA_P_COMMON_GAME_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonGameInfo.bytes";

	public static readonly string U_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfo_{"{0:000000000}"}";

	public static readonly string U_OBJ_P_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_Text";
	public static readonly string U_OBJ_P_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_TextBtn";
	public static readonly string U_OBJ_P_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_TextScaleBtn";

	public static readonly string U_OBJ_P_LOCALIZE_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_LocalizeText";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_LocalizeTextBtn";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_LocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_Img";
	public static readonly string U_OBJ_P_RAW_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_RawImg";
	public static readonly string U_OBJ_P_FOCUS_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_FocusImg";
	public static readonly string U_OBJ_P_GAUGE_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_GaugeImg";

	public static readonly string U_OBJ_P_IMG_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgBtn";
	public static readonly string U_OBJ_P_IMG_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgScaleBtn";

	public static readonly string U_OBJ_P_IMG_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgTextBtn";
	public static readonly string U_OBJ_P_IMG_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgTextScaleBtn";

	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgLocalizeTextBtn";
	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgLocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_INPUT_FIELD = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_InputField";
	public static readonly string U_OBJ_P_DROPDOWN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_Dropdown";

	public static readonly string U_OBJ_P_PAGE_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_PageView";
	public static readonly string U_OBJ_P_SCROLL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_ScrollView";
	public static readonly string U_OBJ_P_RECYCLE_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_RecycleView";

	public static readonly string U_OBJ_P_LINE_FX = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_LineFX";
	public static readonly string U_OBJ_P_PARTICLE_FX = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_ParticleFX";
	
	public static readonly string U_OBJ_P_SPRITE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_2D_ROOT}U_Sprite";

	public static readonly string U_OBJ_P_FPS_COUNTER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_FPSCounter";
	public static readonly string U_OBJ_P_TIMER_MANAGER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_TimerManager";

	public static readonly string U_OBJ_P_DEBUG_CONSOLE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_DebugConsole";
	public static readonly string U_OBJ_P_DEBUG_LOG_ITEM = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_DebugLogItem";

	public static readonly string U_OBJ_P_G_BG_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_BGSnd";
	public static readonly string U_OBJ_P_G_FX_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_FXSnd";

	public static readonly string U_OBJ_P_G_ALERT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_AlertPopup";
	public static readonly string U_OBJ_P_G_TOAST_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_ToastPopup";
	
	public static readonly string U_OBJ_P_G_STORE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_StorePopup";
	public static readonly string U_OBJ_P_G_SETTINGS_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SettingsPopup";
	public static readonly string U_OBJ_P_G_SYNC_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SyncPopup";
	public static readonly string U_OBJ_P_G_DAILY_MISSION_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_DailyMissionPopup";
	public static readonly string U_OBJ_P_G_FREE_REWARD_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_FreeRewardPopup";
	public static readonly string U_OBJ_P_G_DAILY_REWARD_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_DailyRewardPopup";
	public static readonly string U_OBJ_P_G_CHANGES_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SaleCoinsPopup";
	public static readonly string U_OBJ_P_G_REWARD_ACQUIRE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_RewardAcquirePopup";
	public static readonly string U_OBJ_P_G_CHANGES_ACQUIRE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SaleCoinsAcquirePopup";
	public static readonly string U_OBJ_P_G_CONTINUE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_ContinuePopup";
	public static readonly string U_OBJ_P_G_RESULT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_ResultPopup";
	public static readonly string U_OBJ_P_G_FOCUS_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_FocusPopup";
	public static readonly string U_OBJ_P_G_TUTORIAL_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_TutorialPopup";

	public static readonly string U_OBJ_P_G_DRAG_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_DragResponder";
	public static readonly string U_OBJ_P_G_TOUCH_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_TouchResponder";

	public static readonly string U_ASSET_P_G_BUILD_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BuildInfoTable";
	public static readonly string U_ASSET_P_G_BUILD_OPTS_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BuildOptsTable";
	public static readonly string U_ASSET_P_G_DEFINE_SYMBOL_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DefineSymbolTable";
	public static readonly string U_ASSET_P_G_PROJ_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProjInfoTable";
	public static readonly string U_ASSET_P_G_DEVICE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DeviceInfoTable";
	public static readonly string U_ASSET_P_G_SALE_ITEM_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleItemInfoTable";
	public static readonly string U_ASSET_P_G_SALE_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleProductInfoTable";
	public static readonly string U_ASSET_P_G_MISSION_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_MissionInfoTable";
	public static readonly string U_ASSET_P_G_REWARD_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_RewardInfoTable";
	public static readonly string U_ASSET_P_G_EPISODE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_EpisodeInfoTable";
	public static readonly string U_ASSET_P_G_TUTORIAL_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_TutorialInfoTable";
	
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_01 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_01";
	public static readonly string U_ASSET_P_LIGHTING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_UTILITY}U_LightingSettings";

	public static readonly string U_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfoTable";
	
	public static readonly string U_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleItemInfoTable";
	public static readonly string U_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleProductInfoTable";
	public static readonly string U_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_MissionInfoTable";
	public static readonly string U_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_RewardInfoTable";
	public static readonly string U_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_EpisodeInfoTable";
	public static readonly string U_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_TutorialInfoTable";
	
	public static readonly string U_TABLE_P_G_COMMON_VAL = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}G_ValTable_Common";
	public static readonly string U_TABLE_P_G_COMMON_STR = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_STRING_INFO_ROOT}G_StrTable_Common";

	public static readonly string U_TABLE_P_FMT_G_COMMON_VAL = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_VALUE_INFO_ROOT}{KCDefine.B_TEXT_FMT_2_UNDER_SCORE_COMBINE}";
	public static readonly string U_TABLE_P_FMT_G_COMMON_STR = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_STRING_INFO_ROOT}{KCDefine.B_TEXT_FMT_2_UNDER_SCORE_COMBINE}";

	public static readonly string U_TABLE_P_FMT_G_LOCALIZE_COMMON_VAL = string.Format(KCDefine.U_TABLE_P_FMT_G_COMMON_VAL, "G_ValTable_Common", "{0}");
	public static readonly string U_TABLE_P_FMT_G_LOCALIZE_COMMON_STR = string.Format(KCDefine.U_TABLE_P_FMT_G_COMMON_STR, "G_StrTable_Common", "{0}");

	public static readonly string U_TABLE_P_G_KOREAN_COMMON_STR = string.Format(KCDefine.U_TABLE_P_FMT_G_LOCALIZE_COMMON_STR, SystemLanguage.Korean);
	public static readonly string U_TABLE_P_G_ENGLISH_COMMON_STR = string.Format(KCDefine.U_TABLE_P_FMT_G_LOCALIZE_COMMON_STR, SystemLanguage.English);

	public static readonly string U_BASE_TABLE_P_G_LOCALIZE_COMMON_VAL = KCDefine.U_TABLE_P_G_COMMON_VAL;
	public static readonly string U_BASE_TABLE_P_G_LOCALIZE_COMMON_STR = KCDefine.U_TABLE_P_G_COMMON_STR;

	public static readonly string U_FONT_P_G_THAI = $"{KCDefine.B_DIR_P_FONTS}{KCDefine.B_DIR_P_GLOBAL}G_ThaiFont";

	public static readonly string U_SND_P_G_TOUCH_BEGIN = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchBegin";
	public static readonly string U_SND_P_G_TOUCH_END = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchEnd";

	public static readonly string U_SND_P_G_POPUP_SHOW = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_PopupShow";
	public static readonly string U_SND_P_G_POPUP_CLOSE = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_PopupClose";

	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_ASSET = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPAsset";
	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_RENDER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPRenderData";
	public static readonly string U_PIPELINE_P_G_UNIVERSAL_RP_SSAO_RENDER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPSSAORenderData";

	public static readonly string U_IMG_P_G_SPLASH = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_Splash";
	public static readonly string U_IMG_P_G_WHITE = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_UnityWhite";

#if UNITY_EDITOR
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.bytes";

	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json";

#if FIREBASE_MODULE_ENABLE
	public static readonly string U_RUNTIME_DATA_P_G_GAME_CONFIG = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_GAME_CONFIG}.json";
	public static readonly string U_RUNTIME_DATA_P_G_BUILD_VER_CONFIG = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json";
#endif			// #if FIREBASE_MODULE_ENABLE
#else
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.bytes";

	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json";

#if FIREBASE_MODULE_ENABLE
	public static readonly string U_RUNTIME_DATA_P_G_GAME_CONFIG = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_GAME_CONFIG}.json";
	public static readonly string U_RUNTIME_DATA_P_G_BUILD_VER_CONFIG = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json";
#endif			// #if FIREBASE_MODULE_ENABLE
#endif			// #if UNITY_EDITOR

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
	public const EQualityLevel U_QUALITY_LEVEL = EQualityLevel.AUTO;
	// 퀄리티 }

	// 스크립트 순서 {
	public const int U_SCRIPT_O_SINGLETON = sbyte.MaxValue;
	public const int U_SCRIPT_O_ADS_CORRECTOR = byte.MaxValue;
	public const int U_SCRIPT_O_ADS_INTERACTABLE = byte.MaxValue;

	public const int U_SCRIPT_O_INIT_SCENE_MANAGER = sbyte.MaxValue / 2;
	public const int U_SCRIPT_O_SETUP_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_AGREE_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_LATE_SETUP_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;

	public const int U_SCRIPT_O_START_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_SPLASH_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_PERMISSION_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;
	public const int U_SCRIPT_O_INTRO_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;

	public const int U_SCRIPT_O_OVERLAY_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 3;
	public const int U_SCRIPT_O_LOADING_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 3;

	public const int U_SCRIPT_O_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 2;
	// 스크립트 순서 }

	// 형식
	public const string U_TEXT_FMT_SCREEN_INFO_A = "Screen DPI: {0:0.0}\n";
	public const string U_TEXT_FMT_SCREEN_INFO_B = "Screen Size: {0:0.0}, {1:0.0}\n";
	public const string U_TEXT_FMT_SCREEN_INFO_C = "Banner Ads Height: {0:0.0}";

	// 광원 {
#if !LIGHTMAP_BAKE_ENABLE || REALTIME_LIGHTMAP_BAKE_ENABLE
	public const LightmapBakeType U_LIGHTMAP_BAKE_TYPE_DIRECTIONAL = LightmapBakeType.Realtime;
#else
	public const LightmapBakeType U_LIGHTMAP_BAKE_TYPE_DIRECTIONAL = LightmapBakeType.Mixed;
#endif			// #if !LIGHTMAP_BAKE_ENABLE || REALTIME_LIGHTMAP_BAKE_ENABLE
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

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	// 식별자
	public const string U_KEY_SERVICES_M_UPDATE_APPLE_LOGIN_STATE_CALLBACK = "ServicesMUpdateAppleLoginStateCallback";
	public const string U_KEY_SERVICES_M_LOGIN_WITH_APPLE_CALLBACK = "ServicesMLoginWithAppleCallback";

	// 이름
	public const string U_OBJ_N_SERVICES_M_LOGIN_WITH_APPLE = "LoginWithApple";
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if UNITY_ANDROID
	// 시간
	public const float U_DELTA_T_PERMISSION_M_REQUEST_CHECK = 0.25f;
	public const float U_MAX_DELTA_T_PERMISSION_M_REQUEST_CHECK = 1.0f;
#endif			// #if UNITY_ANDROID

#if HAPTIC_FEEDBACK_ENABLE
	// 이름
	public const string U_MODEL_N_IPHONE = "iPhone";
	public const string U_MODEL_N_IPAD = "iPad";
#endif			// #if HAPTIC_FEEDBACK_ENABLE

#if DEBUG || DEVELOPMENT_BUILD
	// 시간
	public const float U_DELTA_T_DYNAMIC_DEBUG = 1.0f;

	// 형식 {
	public const string U_TEXT_FMT_STATIC_DEBUG_MSG = "{0}\n\n{1}";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_MSG = "{0}\n\n{1}";

	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_A = "Resolution: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_B = "Design Resolution: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_C = "Canvas Size: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_D = "Screen DPI: <color=orange>{0:0.0}</color>, Banner Ads Height: <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_E = "Persistent Data Path: <color=orange>{0:0.0}</color>";

	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_A = "GC: <color=orange>{0:0.0}</color> MB, Used Heap: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_B = "Mono Heap: <color=orange>{0:0.0}</color> MB, Mono Used: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_C = "Temp Alloc: <color=orange>{0:0.0}</color> MB, Total Alloc: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_D = "Reserved: <color=orange>{0:0.0}</color> MB, Unused Reserved: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_E = "GPU Alloc: <color=orange>{0:0.0}</color> MB, Time Scale: <color=orange>{1:0.00000}</color>\n";
	// 형식 }

	// 이름 {
	public const string U_OBJ_N_SCREEN_DEBUG_UIS = "ScreenDebugUIs";
	public const string U_OBJ_N_SCREEN_DEBUG_TEXT_UIS = "DebugTextUIs";

	public const string U_OBJ_N_SCREEN_STATIC_DEBUG_TEXT = "StaticDebugText";
	public const string U_OBJ_N_SCREEN_DYNAMIC_DEBUG_TEXT = "DynamicDebugText";

	public const string U_OBJ_N_SCREEN_FPS_BTN = "FPSBtn";
	public const string U_OBJ_N_SCREEN_DEBUG_BTN = "DebugBtn";
	// 이름 }
#endif			// #if DEBUG || DEVELOPMENT_BUILD

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 이름
	public const string U_OBJ_N_FPS_C_STATIC_TEXT = "StaticInfoText";
	public const string U_OBJ_N_FPS_C_DYNAMIC_TEXT = "DynamicInfoText";
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if GAME_ANALYTICS_MODULE_ENABLE
	// 이름
	public const string U_OBJ_N_GAME_AM_GAME_ANALYTICS = "GameAnalytics";
#endif			// #if GAME_ANALYTICS_MODULE_ENABLE

#if SINGULAR_MODULE_ENABLE
	// 시간
	public const int U_TIMEOUT_SINGULAR_M_AGREE_TRACKING = 60;

	// 이름
	public const string U_OBJ_N_SINGULAR_M_SINGULAR = "Singular";
#endif			// #if SINGULAR_MODULE_ENABLE

#if ADS_MODULE_ENABLE
	// 비율
	public const float U_SCALE_LANDSCAPE_BANNER_ADS_HEIGHT = 0.68f;

	// 시간
	public const float U_DELTA_T_ADS_M_ADS_LOAD = 5.0f;
	public const float U_DELTA_T_REWARD_ATI_UPDATE = 0.5f;
	
	// 식별자 {
	public const string U_KEY_ADS_M_BANNER_ADS_ID = "AdsMBannerAdsID";
	public const string U_KEY_ADS_M_REWARD_ADS_ID = "AdsMRewardAdsID";
	public const string U_KEY_ADS_M_FULLSCREEN_ADS_ID = "AdsMFullscreenAdsID";

	public const string U_KEY_FMT_ADS_M_BANNER_ADS_LOADER_INFO = "AdsMBannerAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_REWARD_ADS_LOADER_INFO = "AdsMRewardAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_FULLSCREEN_ADS_LOADER_INFO = "AdsMFullscreenAdsLoaderInfo_{0}";
	public const string U_KEY_FMT_ADS_M_CONSENT_V_LOADER_INFO = "AdsMConsentVLoaderInfo_{0}";
	// 식별자 }

#if ADMOB_ENABLE
	// 식별자 {
	public const string U_ADS_ID_ADMOB_TEST_BANNER_ADS = "ca-app-pub-3940256099942544/6300978111";
	public const string U_ADS_ID_ADMOB_TEST_REWARD_ADS = "ca-app-pub-3940256099942544/5224354917";
	public const string U_ADS_ID_ADMOB_TEST_FULLSCREEN_ADS = "ca-app-pub-3940256099942544/1033173712";

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

#if FLURRY_MODULE_ENABLE
	// 시간
	public const long U_TIMEOUT_FLURRY_M_NETWORK_CONNECTION = 60 * KCDefine.B_UNIT_MILLI_SECS_PER_SECS;

	// 식별자
	public const string U_KEY_FLURRY_M_INIT_CALLBACK = "FlurryMInitCallback";
#endif			// #if FLURRY_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	// 식별자
	public const string U_KEY_FACEBOOK_M_INIT_CALLBACK = "FacebookMInitCallback";
	public const string U_KEY_FACEBOOK_M_LOGIN_CALLBACK = "FacebookMLoginCallback";
	public const string U_KEY_FACEBOOK_M_VIEW_STATE_SHOW_CALLBACK = "FacebookMViewStateShowCallback";
	public const string U_KEY_FACEBOOK_M_VIEW_STATE_CLOSE_CALLBACK = "FacebookMViewStateCloseCallback";
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	// 식별자 {
	public const string U_KEY_FIREBASE_M_INIT_CALLBACK = "FirebaseMInitCallback";

	public const string U_CONFIG_KEY_FIREBASE_M_GAME = "GameConfig";
	public const string U_CONFIG_KEY_FIREBASE_M_DEVICE = "DeviceConfig";
	public const string U_CONFIG_KEY_FIREBASE_M_BUILD_VER = "BuildVerConfig";
	// 식별자 }

	// 노드
	public const string U_NODE_FIREBASE_USER_INFOS = "UserInfos";
	public const string U_NODE_FIREBASE_POST_ITEM_INFOS = "PostItemInfos";
	
#if FIREBASE_AUTH_ENABLE
	// 식별자 {
	public const string U_KEY_FIREBASE_M_LOGIN_CALLBACK = "FirebaseMLoginCallback";	

#if UNITY_IOS
	public const string U_PROVIDER_ID_FIREBASE_M_APPLE_LOGIN = "apple.com";
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

#if APPS_FLYER_MODULE_ENABLE
	// 식별자
	public const string U_KEY_APPS_FM_INIT_CALLBACK = "AppsFMInitCallback";
#endif			// #if APPS_FLYER_MODULE_ENABLE

#if GAME_ANALYTICS_MODULE_ENABLE
	// 식별자
	public const string U_KEY_GAME_AM_INIT_CALLBACK = "GameAMInitCallback";
#endif			// #if GAME_ANALYTICS_MODULE_ENABLE

#if SINGULAR_MODULE_ENABLE
	// 식별자
	public const string U_KEY_SINGULAR_M_INIT_CALLBACK = "SingularMInitCallback";
#endif			// #if SINGULAR_MODULE_ENABLE

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
	public const string U_GROUP_ID_NOTI = "DefNotiGroup";

#if UNITY_IOS
	// 옵션
	public const AuthorizationOption U_AUTH_OPTS_NOTI = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
	public const PresentationOption U_PRESENT_OPTS_NOTI = PresentationOption.Alert | PresentationOption.Sound;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
	// 그룹 정보 {
	public const Importance U_IMPORTANCE_NOTI = Importance.Default;
	
	public const string U_GROUP_N_NOTI = KCDefine.U_GROUP_ID_NOTI;
	public const string U_GROUP_DESC_NOTI = KCDefine.U_GROUP_ID_NOTI;
	// 그룹 정보 }
#endif			// #if UNITY_ANDROID
#endif			// #if NOTI_ENABLE
	#endregion			// 조건부 상수

	#region 조건부 런타임 상수
#if UNITY_IOS
	// 버전
	public static readonly System.Version U_MIN_VER_CONSENT_VIEW = new System.Version(14, 0, 0);
	public static readonly System.Version U_MIN_VER_HAPTIC_FEEDBACK = new System.Version(10, 0, 0);
	public static readonly System.Version U_MIN_VER_LOGIN_WITH_APPLE = new System.Version(13, 0, 0);
	
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
	// 기타
	public static readonly STAdsRewardItemInfo U_INVALID_ADS_REWARD_ITEM_INFO = new STAdsRewardItemInfo() {
		m_oID = string.Empty,
		m_oVal = string.Empty
	};

	// 크기
	public static readonly Vector3 U_SIZE_PHONE_BANNER_ADS = new Vector3(320.0f, 50.0f, 0.0f);
	public static readonly Vector3 U_SIZE_TABLET_BANNER_ADS = new Vector3(728.0f, 90.0f, 0.0f);

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

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE || SINGULAR_MODULE_ENABLE
	// 경로
	public static readonly string U_ASSET_P_G_PLUGIN_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_PluginInfoTable";
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE || SINGULAR_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	// 권한
	public static readonly string[] U_PERMISSIONS_FACEBOOK = new string[] {
		"public_profile"
	};
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	// 시간
	public static readonly System.TimeSpan U_TIMEOUT_FIREBASE_SESSION = new System.TimeSpan(0, 0, 60);

	// 경로
	public static readonly string U_DATA_P_G_GAME_CONFIG = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_GLOBAL}G_GameConfig";
	public static readonly string U_DATA_P_G_BUILD_VER_CONFIG = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_GLOBAL}G_BuildVerConfig";
#endif			// #if FIREBASE_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	// 경로
	public static readonly string U_DATA_P_PURCHASE_PRODUCT_IDS = $"{KCDefine.B_DIR_P_WRITABLE}PurchaseProductIDs.bytes";
	public static readonly string U_ASSET_P_G_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProductInfoTable";
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 런타임 상수
}
