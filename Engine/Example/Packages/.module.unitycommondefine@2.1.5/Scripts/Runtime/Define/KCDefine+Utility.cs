using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE
using GoogleMobileAds.Api;
#endif			// #if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE

#if NOTI_MODULE_ENABLE
#if UNITY_IOS
using Unity.Notifications.iOS;
#elif UNITY_ANDROID
using Unity.Notifications.Android;
#endif			// #if UNITY_IOS
#endif			// #if NOTI_MODULE_ENABLE

/** 유틸리티 상수 */
public static partial class KCDefine {
	#region 기본
	// 개수 {
	public const int U_MAX_NUM_LAYERS = 32;
	public const int U_MAX_NUM_FX_SNDS = 10;

	public const int U_MAX_NUM_LEVEL_INFOS = 9999;
	public const int U_MAX_NUM_STAGE_INFOS = 999;
	public const int U_MAX_NUM_CHAPTER_INFOS = 99;
	// 개수 }

	// 크기 {
	public const int U_SIZE_OBJS_POOL = 100;

	public const int U_DEF_MIN_SIZE_FONT = 10;
	public const int U_DEF_MAX_SIZE_FONT = 14;
	// 크기 }

	// 길이
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
	public const float U_DISTANCE_CAMERA_PLANE = 500.0f * KCDefine.B_UNIT_SCALE;
	public const float U_DISTANCE_CAMERA_FAR_PLANE = 100000.0f * KCDefine.B_UNIT_SCALE;
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

	public const float U_MIN_DELAY_FX_SNDS = 0.01f;
	public const float U_DELTA_T_SCHEDULE_M_CALLBACK = 0.25f;
	
	public const float U_DURATION_ANI = 0.25f;
	public const float U_DURATION_SCREEN_FADE_IN_ANI = 0.25f;
	public const float U_DURATION_SCREEN_FADE_OUT_ANI = KCDefine.U_DURATION_SCREEN_FADE_IN_ANI;

	public const float U_DURATION_POPUP_SCALE_ANI = 0.25f;
	public const float U_DURATION_POPUP_DROPDOWN_ANI = 0.35f;
	public const float U_DURATION_POPUP_SLIDE_ANI = 0.35f;

	public const float U_DURATION_LIGHT_VIBRATE = 0.05f;
	public const float U_DURATION_MEDIUM_VIBRATE = 0.1f;
	public const float U_DURATION_HEAVY_VIBRATE = 0.15f;

	public const float U_TIMEOUT_ASYNC_TASK = 30.0f;
	public const float U_TIMEOUT_NETWORK_CONNECTION = 30.0f;
	// 시간 }

	// 레이어
	public const int U_LAYER_DEF = 0;
	public const int U_LAYER_TRANSPARENT_FX = 1;
	public const int U_LAYER_IGNORE_RAYCAST = 2;
	public const int U_LAYER_WATER = 4;
	public const int U_LAYER_UIS = 5;
	public const int U_LAYER_CUSTOM = 11;

	// 정렬 순서
	public const int U_SORTING_O_SCREEN_POPUP_UIS = 0;
	public const int U_SORTING_O_SCREEN_TOPMOST_UIS = 1;
	public const int U_SORTING_O_SCREEN_ABS_UIS = 2;
	public const int U_SORTING_O_SCREEN_BLIND_UIS = 3;
	public const int U_SORTING_O_SCREEN_DEBUG_UIS = 4;

	// 애니메이션
	public const Ease U_EASE_ANI = Ease.OutQuad;
	public const Ease U_EASE_UNSET = Ease.Unset;
	public const Ease U_EASE_LINEAR = Ease.Linear;

	// 형식 {
#if UNITY_IOS
	public const string U_FMT_STORE_URL = "https://itunes.apple.com/app/id{0}";
	public const string U_FMT_MORE_GAMES_URL = "https://apps.apple.com/us/developer/ninetap/id{0}#see-all/i-phonei-pad-apps";
#else
	public const string U_FMT_STORE_URL = "https://play.google.com/store/apps/details?id={0}";
	public const string U_FMT_MORE_GAMES_URL = "https://play.google.com/store/apps/developer?id={0}";
#endif			// #if UNITY_IOS
	// 형식 }

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
	public const string U_KEY_MISSION = "Mission";
	public const string U_KEY_TUTORIAL = "Tutorial";

	public const string U_KEY_LEVEL = "Level";
	public const string U_KEY_STAGE = "Stage";
	public const string U_KEY_CHAPTER = "Chapter";

	public const string U_KEY_ID = "ID";
	public const string U_KEY_STAGE_ID = "StageID";
	public const string U_KEY_CHAPTER_ID = "ChapterID";

	public const string U_KEY_POS_X = "PosX";
	public const string U_KEY_POS_Y = "PosY";
	public const string U_KEY_POS_Z = "PosZ";

	public const string U_KEY_SIZE_X = "SizeX";
	public const string U_KEY_SIZE_Y = "SizeY";
	public const string U_KEY_SIZE_Z = "SizeZ";

	public const string U_KEY_DIFFICULTY = "Difficulty";
	public const string U_KEY_LEVEL_KINDS = "LevelKinds";	
	public const string U_KEY_STAGE_KINDS = "StageKinds";
	public const string U_KEY_CHAPTER_KINDS = "ChapterKinds";

	public const string U_KEY_PRICE_KINDS = "PriceKinds";
	public const string U_KEY_REWARD_KINDS = "RewardKinds";
	public const string U_KEY_PRODUCT_KINDS = "ProductKinds";
	public const string U_KEY_MISSION_KINDS = "MissionKinds";
	public const string U_KEY_SALE_ITEM_KINDS = "SaleItemKinds";
	public const string U_KEY_SALE_PRODUCT_KINDS = "SaleProductKinds";

	public const string U_KEY_FX_KINDS = "FXKinds";
	public const string U_KEY_NEXT_FX_KINDS = "NextFXKinds";

	public const string U_KEY_BLOCK_KINDS = "BlockKinds";
	public const string U_KEY_NEXT_BLOCK_KINDS = "NextBlockKinds";

	public const string U_KEY_TUTORIAL_KINDS = "TutorialKinds";
	public const string U_KEY_NEXT_TUTORIAL_KINDS = "NextTutorialKinds";

	public const string U_KEY_FMT_NUM_ITEMS = "NumItems_{0:00}";
	public const string U_KEY_FMT_ITEM_KINDS = "ItemKinds_{0:00}";

	public const string U_KEY_FMT_RECORD = "Record_{0:00}";
	public const string U_KEY_FMT_NUM_TARGETS = "NumTargets_{0:00}";
	public const string U_KEY_FMT_NUM_UNLOCK_TARGETS = "NumUnlockTargets_{0:00}";

	public const string U_KEY_FMT_TARGET_KINDS = "TargetKinds_{0:00}";
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
	public const string U_OBJ_N_SCENE_CORNER_ANCHOR_UIS = "CornerAnchorUIs";
	public const string U_OBJ_N_SCENE_DESIGN_RESOLUTION_GUIDE_UIS = "DesignResolutionGuideUIs";

	public const string U_OBJ_N_SCENE_UP_UIS = "UpUIs";
	public const string U_OBJ_N_SCENE_DOWN_UIS = "DownUIs";
	public const string U_OBJ_N_SCENE_LEFT_UIS = "LeftUIs";
	public const string U_OBJ_N_SCENE_RIGHT_UIS = "RightUIs";

	public const string U_OBJ_N_SCENE_UP_LEFT_UIS = "UpLeftUIs";
	public const string U_OBJ_N_SCENE_UP_RIGHT_UIS = "UpRightUIs";
	public const string U_OBJ_N_SCENE_DOWN_LEFT_UIS = "DownLeftUIs";
	public const string U_OBJ_N_SCENE_DOWN_RIGHT_UIS = "DownRightUIs";

	public const string U_OBJ_N_SCENE_POPUP_UIS = "PopupUIs";
	public const string U_OBJ_N_SCENE_TOPMOST_UIS = "TopmostUIs";
	public const string U_OBJ_N_SCENE_ABS_UIS = "AbsUIs";

	public const string U_OBJ_N_SCENE_BASE = "Base";
	public const string U_OBJ_N_SCENE_OBJS_BASE = "ObjsRoot";
	public const string U_OBJ_N_SCENE_OBJS = "Objs";
	public const string U_OBJ_N_SCENE_PIVOT_OBJS = "PivotObjs";
	public const string U_OBJ_N_SCENE_ANCHOR_OBJS = "AnchorObjs";
	public const string U_OBJ_N_SCENE_STATIC_OBJS = "StaticObjs";
	public const string U_OBJ_N_SCENE_ADDITIONAL_LIGHTS = "AdditionalLights";
	public const string U_OBJ_N_SCENE_REFLECTION_PROBES = "ReflectionProbes";
	public const string U_OBJ_N_SCENE_LIGHT_PROBE_GROUPS = "LightProbeGroups";

	public const string U_OBJ_N_SCENE_UP_OBJS = "UpObjs";
	public const string U_OBJ_N_SCENE_DOWN_OBJS = "DownObjs";
	public const string U_OBJ_N_SCENE_LEFT_OBJS = "LeftObjs";
	public const string U_OBJ_N_SCENE_RIGHT_OBJS = "RightObjs";

	public const string U_OBJ_N_SCENE_MANAGER = "SceneManager";
	public const string U_OBJ_N_SCENE_UIS_CAMERA = "UIsCamera";
	public const string U_OBJ_N_SCENE_MAIN_CAMERA = "MainCamera";
	public const string U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT = "MainDirectionalLight";

	public const string U_OBJ_N_SCREEN_DEBUG_UIS = "ScreenDebugUIs";
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
	public const string U_OBJ_N_SCENE_EVENT_SYSTEM = "EventSystem";

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
	public const string U_OBJ_N_PAGE_TEXT = "PageText";

	public const string U_OBJ_N_NAME_TEXT = "NameText";
	public const string U_OBJ_N_DESC_TEXT = "DescText";

	public const string U_OBJ_N_LEVEL_TEXT = "LevelText";
	public const string U_OBJ_N_STAGE_TEXT = "StageText";
	public const string U_OBJ_N_CHAPTER_TEXT = "ChapterText";

	public const string U_OBJ_N_VER_TEXT = "VerText";
	public const string U_OBJ_N_NUM_TEXT = "NumText";
	public const string U_OBJ_N_TIME_TEXT = "TimeText";
	public const string U_OBJ_N_TIMES_TEXT = "TimesText";
	public const string U_OBJ_N_PRICE_TEXT = "PriceText";
	public const string U_OBJ_N_LOADING_TEXT = "LoadingText";
	public const string U_OBJ_N_COUNTDOWN_TEXT = "CountdownText";

	public const string U_OBJ_N_RECORD_TEXT = "RecordText";
	public const string U_OBJ_N_BEST_RECORD_TEXT = "BestRecordText";

	public const string U_OBJ_N_NUM_COINS_TEXT = "NumCoinsText";
	public const string U_OBJ_N_NUM_SALE_COINS_TEXT = "NumSaleCoinsText";

	public const string U_OBJ_N_NUM_MARKS_TEXT = "NumMarksText";
	public const string U_OBJ_N_NUM_CLEAR_MARKS_TEXT = "NumClearMarksText";
	public const string U_OBJ_N_NUM_CLEAR_MARKS_STATE_TEXT = "NumClearMarksStateText";

	public const string U_OBJ_N_BG_IMG = "BGImg";
	public const string U_OBJ_N_COINS_IMG = "CoinsImg";
	public const string U_OBJ_N_BLIND_IMG = "BlindImg";
	public const string U_OBJ_N_SPLASH_IMG = "SplashImg";

	public const string U_OBJ_N_MARK_IMG = "MarkImg";
	public const string U_OBJ_N_CLEAR_MARK_IMG = "ClearMarkImg";

	public const string U_OBJ_N_CHECK_IMG = "CheckImg";
	public const string U_OBJ_N_RIBBON_IMG = "RibbonImg";
	public const string U_OBJ_N_PERCENT_IMG = "PercentImg";
	public const string U_OBJ_N_COMPLETE_IMG = "CompleteImg";

	public const string U_OBJ_N_LOCK_IMG = "LockImg";
	public const string U_OBJ_N_ICON_IMG = "IconImg";
	public const string U_OBJ_N_ITEM_IMG = "ItemImg";
	public const string U_OBJ_N_REWARD_IMG = "RewardImg";

	public const string U_OBJ_N_OK_BTN = "OKBtn";
	public const string U_OBJ_N_BACK_BTN = "BackBtn";
	public const string U_OBJ_N_CANCEL_BTN = "CancelBtn";
	public const string U_OBJ_N_CLOSE_BTN = "CloseBtn";

	public const string U_OBJ_N_AGREE_BTN = "AgreeBtn";
	public const string U_OBJ_N_PRIVACY_BTN = "PrivacyBtn";
	public const string U_OBJ_N_SERVICES_BTN = "ServicesBtn";

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

	public const string U_OBJ_N_PAUSE_BTN = "PauseBtn";
	public const string U_OBJ_N_ACQUIRE_BTN = "AcquireBtn";
	public const string U_OBJ_N_PURCHASE_BTN = "PurchaseBtn";
	public const string U_OBJ_N_RESTORE_BTN = "RestoreBtn";
	public const string U_OBJ_N_REWARD_ADS_BTN = "RewardAdsBtn";

	public const string U_OBJ_N_BG_SND_BTN = "BGSndBtn";
	public const string U_OBJ_N_FX_SNDS_BTN = "FXSndsBtn";
	public const string U_OBJ_N_NOTI_BTN = "NotiBtn";
	public const string U_OBJ_N_REVIEW_BTN = "ReviewBtn";
	public const string U_OBJ_N_SUPPORTS_BTN = "SupportsBtn";
	public const string U_OBJ_N_SYNC_BTN = "SyncBtn";

	public const string U_OBJ_N_EMPTY = "Empty";
	public const string U_OBJ_N_GAME_OBJ = "GameObj";
	public const string U_OBJ_N_ALERT_POPUP = "AlertPopup";

	public const string U_OBJ_N_TEXT = "Text";
	public const string U_OBJ_N_LOCALIZE_TEXT = "LocalizeText";

	public const string U_OBJ_N_TMP_TEXT = "TMPText";
	public const string U_OBJ_N_TMP_LOCALIZE_TEXT = "TMPLocalizeText";

	public const string U_OBJ_N_IMG = "Img";
	public const string U_OBJ_N_RAW_IMG = "RawImg";
	public const string U_OBJ_N_FOCUS_IMG = "FocusImg";
	public const string U_OBJ_N_GAUGE_IMG = "GaugeImg";

	public const string U_OBJ_N_TEXT_BTN = "TextBtn";
	public const string U_OBJ_N_TEXT_SCALE_BTN = "TextScaleBtn";

	public const string U_OBJ_N_TMP_TEXT_BTN = "TMPTextBtn";
	public const string U_OBJ_N_TMP_TEXT_SCALE_BTN = "TMPTextScaleBtn";

	public const string U_OBJ_N_LOCALIZE_TEXT_BTN = "LocalizeTextBtn";
	public const string U_OBJ_N_LOCALIZE_TEXT_SCALE_BTN = "LocalizeTextScaleBtn";

	public const string U_OBJ_N_TMP_LOCALIZE_TEXT_BTN = "TMPLocalizeTextBtn";
	public const string U_OBJ_N_TMP_LOCALIZE_TEXT_SCALE_BTN = "TMPLocalizeTextScaleBtn";

	public const string U_OBJ_N_IMG_BTN = "ImgBtn";
	public const string U_OBJ_N_IMG_SCALE_BTN = "ImgScaleBtn";

	public const string U_OBJ_N_IMG_TEXT_BTN = "ImgTextBtn";
	public const string U_OBJ_N_IMG_TEXT_SCALE_BTN = "ImgTextScaleBtn";

	public const string U_OBJ_N_TMP_IMG_TEXT_BTN = "TMPImgTextBtn";
	public const string U_OBJ_N_TMP_IMG_TEXT_SCALE_BTN = "TMPImgTextScaleBtn";

	public const string U_OBJ_N_IMG_LOCALIZE_TEXT_BTN = "ImgLocalizeTextBtn";
	public const string U_OBJ_N_IMG_LOCALIZE_TEXT_SCALE_BTN = "ImgLocalizeTextScaleBtn";

	public const string U_OBJ_N_TMP_IMG_LOCALIZE_TEXT_BTN = "TMPImgLocalizeTextBtn";
	public const string U_OBJ_N_TMP_IMG_LOCALIZE_TEXT_SCALE_BTN = "TMPImgLocalizeTextScaleBtn";

	public const string U_OBJ_N_DROPDOWN = "Dropdown";
	public const string U_OBJ_N_INPUT_FIELD = "InputField";

	public const string U_OBJ_N_TMP_DROPDOWN = "TMPDropdown";
	public const string U_OBJ_N_TMP_INPUT_FIELD = "TMPInputField";

	public const string U_OBJ_N_PAGE_VIEW = "PageView";
	public const string U_OBJ_N_SCROLL_VIEW = "ScrollView";
	public const string U_OBJ_N_RECYCLE_VIEW = "RecycleView";

	public const string U_OBJ_N_LEVEL_SCROLL_VIEW = "LevelScrollView";
	public const string U_OBJ_N_STAGE_SCROLL_VIEW = "StageScrollView";
	public const string U_OBJ_N_CHAPTER_SCROLL_VIEW = "ChapterScrollView";

	public const string U_OBJ_N_SPRITE = "Sprite";
	public const string U_OBJ_N_LINE_FX = "LineFX";
	public const string U_OBJ_N_PARTICLE_FX = "ParticleFX";
	public const string U_OBJ_N_REFLECTION_PROBE = "ReflectionProbe";
	public const string U_OBJ_N_LIGHT_PROBE_GROUP = "LightProbeGroup";
	
	public const string U_OBJ_N_DRAG_RESPONDER = "DragResponder";
	public const string U_OBJ_N_TOUCH_RESPONDER = "TouchResponder";
	public const string U_OBJ_N_BG_TOUCH_RESPONDER = "BGTouchResponder";

	public const string U_OBJ_N_INDICATOR_TOUCH_RESPONDER = "IndicatorTouchResponder";
	public const string U_OBJ_N_SCREEN_FADE_TOUCH_RESPONDER = "ScreenFadeTouchResponder";

	public const string U_OBJ_N_FMT_NUM_TEXT = "NumText_{0:00}";
	public const string U_OBJ_N_FMT_CLEAR_MARK_IMG = "ClearMarkImg_{0:00}";
	public const string U_OBJ_N_FMT_POPUP_TOUCH_RESPONDER = "PopupTouchResponder_{0}";

	public const string U_IMG_N_DEF_MESH = "DefMesh";
	public const string U_IMG_N_DEF_SPRITE = "DefSprite";

	public const string U_IMG_N_CLONE_SPRITE = "(Clone)";
	public const string U_IMG_N_UNKNOWN_SPRITE = "Unknown";

	public const string U_FUNC_N_ON_DRAG = "OnDrag";
	public const string U_FUNC_N_ON_POINTER_UP = "OnPointerUp";
	public const string U_FUNC_N_ON_POINTER_DOWN = "OnPointerDown";
	public const string U_FUNC_N_ON_POINTER_ENTER = "OnPointerEnter";
	public const string U_FUNC_N_ON_POINTER_EXIT = "OnPointerExit";

	public const string U_FUNC_N_INIT = "Init";
	public const string U_FUNC_N_RESET_LOCALIZE = "ResetLocalize";
	public const string U_FUNC_N_RESET_DIFFICULTY = "ResetDifficulty";
	public const string U_FUNC_N_UPDATE_UIS_STATE = "UpdateUIsState";

	public const string U_INPUT_N_FIRE = "Fire";
	public const string U_INPUT_N_JUMP = "Jump";
	public const string U_INPUT_N_VERTICAL = "Vertical";
	public const string U_INPUT_N_HORIZONTAL = "Horizontal";

	public const string U_PROPERTY_N_TEXT = "text";
	public const string U_PROPERTY_N_FONT = "font";
	public const string U_PROPERTY_N_COLOR = "color";
	public const string U_PROPERTY_N_SPRITE = "sprite";
	public const string U_PROPERTY_N_TMP_TEXT = "text";
	public const string U_PROPERTY_N_TMP_FONT = "font";
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
	public const string U_TAG_SCENE_MANAGER = "SceneManager";
	public const string U_TAG_MAIN_DIRECTIONAL_LIGHT = "MainDirectionalLight";
	// 태그 }

	// 정렬 레이어 {
	public const string U_SORTING_L_ABS = "Abs";
	public const string U_SORTING_L_DEF = "Default";

	public const string U_SORTING_L_TOP = "Top";
	public const string U_SORTING_L_TOPMOST = "Topmost";

	public const string U_SORTING_L_FOREGROUND = "Foreground";
	public const string U_SORTING_L_OVERGROUND = "Overground";

	public const string U_SORTING_L_BACKGROUND = "Background";
	public const string U_SORTING_L_UNDERGROUND = "Underground";

	public const string U_SORTING_L_OVERLAY_ABS = "OverlayAbs";
	public const string U_SORTING_L_OVERLAY_DEF = "OverlayDefault";

	public const string U_SORTING_L_OVERLAY_TOP = "OverlayTop";
	public const string U_SORTING_L_OVERLAY_TOPMOST = "OverlayTopmost";

	public const string U_SORTING_L_OVERLAY_FOREGROUND = "OverlayForeground";
	public const string U_SORTING_L_OVERLAY_OVERGROUND = "OverlayOverground";

	public const string U_SORTING_L_OVERLAY_BACKGROUND = "OverlayBackground";
	public const string U_SORTING_L_OVERLAY_UNDERGROUND = "OverlayUnderground";
	// 정렬 레이어 }

	// 씬 관리자
	public const string U_KEY_FMT_SCENE_M_TOUCH_RESPONDER = "SceneMTouchResponder_{0}";

	// 사운드 관리자
	public const string U_OBJ_N_SND_M_BG_SND = "BGSnd";
	public const string U_OBJ_N_SND_M_FX_SND = "FXSnd";
	
	// 입력 모듈 {
	public const int U_THRESHOLD_INPUT_M_MOVE = 10;

	public const float U_RATE_INPUT_M_MOVE_REPEAT = 0.1f;
	public const float U_DELAY_INPUT_M_MOVE_REPEAT = 0.5f;
	public const float U_UNIT_INPUT_M_INPUT_ACTIONS_PER_SEC = 10.0f;
	// 입력 모듈 }

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
	// 기타 {
	public static readonly STLocalizeInfo U_INVALID_LOCALIZE_INFO = new STLocalizeInfo() {
		m_eSystemLanguage = SystemLanguage.Unknown
	};

	public static readonly STFontSetInfo U_INVALID_FONT_SET_INFO = new STFontSetInfo() {
		m_eSet = EFontSet.NONE
	};
	// 기타 }

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

	public static readonly Color U_COLOR_CLEAR = Color.black;
	public static readonly Color U_COLOR_POPUP_BG = new Color(0.0f, 0.0f, 0.0f, 0.75f);
	public static readonly Color U_COLOR_INDICATOR_BG = KCDefine.U_COLOR_POPUP_BG;
	// 색상 }

	// 버전
	public static readonly System.Version U_VER_COMMON_APP_INFO = new System.Version(1, 0, 0);
	public static readonly System.Version U_VER_COMMON_GAME_INFO = new System.Version(1, 0, 0);
	public static readonly System.Version U_VER_COMMON_USER_INFO = new System.Version(1, 0, 0);

	// 크기
	public static readonly Vector3 U_POS_VER_TEXT = new Vector3(10.0f, -10.0f, 0.0f);
	public static readonly Vector3 U_MIN_SIZE_ALERT_POPUP = new Vector3(400.0f, 300.0f, 0.0f);

	// 태그
	public static readonly List<string> U_TAG_LIST = new List<string>() {
		KCDefine.U_TAG_ENEMY, KCDefine.U_TAG_OBSTACLE, KCDefine.U_TAG_UIS_CAMERA, KCDefine.U_TAG_SCENE_MANAGER, KCDefine.U_TAG_MAIN_DIRECTIONAL_LIGHT
	};

	// 정렬 레이어
	public static readonly List<string> U_SORTING_LAYER_LIST = new List<string>() {
		KCDefine.U_SORTING_L_UNDERGROUND, KCDefine.U_SORTING_L_BACKGROUND, KCDefine.U_SORTING_L_DEF, KCDefine.U_SORTING_L_FOREGROUND, KCDefine.U_SORTING_L_OVERGROUND, KCDefine.U_SORTING_L_TOP, KCDefine.U_SORTING_L_TOPMOST, KCDefine.U_SORTING_L_ABS, KCDefine.U_SORTING_L_OVERLAY_UNDERGROUND, KCDefine.U_SORTING_L_OVERLAY_BACKGROUND, KCDefine.U_SORTING_L_OVERLAY_DEF, KCDefine.U_SORTING_L_OVERLAY_FOREGROUND, KCDefine.U_SORTING_L_OVERLAY_OVERGROUND, KCDefine.U_SORTING_L_OVERLAY_TOP, KCDefine.U_SORTING_L_OVERLAY_TOPMOST, KCDefine.U_SORTING_L_OVERLAY_ABS
	};

	// 정렬 순서
	public static readonly STSortingOrderInfo U_SORTING_OI_UIS_CANVAS = new STSortingOrderInfo() {
		m_nOrder = 1, m_oLayer = KCDefine.U_SORTING_L_DEF
	};

	// 레이어 마스크 {
	public static readonly List<int> U_LAYER_MASKS_LIGHT = new List<int>() {
		KCDefine.U_LAYER_DEF, KCDefine.U_LAYER_TRANSPARENT_FX, KCDefine.U_LAYER_IGNORE_RAYCAST, KCDefine.U_LAYER_WATER
	};

	public static readonly List<int> U_LAYER_MASKS_UIS_CAMERA = new List<int>() {
		KCDefine.U_LAYER_UIS
	};

	public static readonly List<int> U_LAYER_MASKS_MAIN_CAMERA = new List<int>() {
		KCDefine.U_LAYER_DEF, KCDefine.U_LAYER_TRANSPARENT_FX, KCDefine.U_LAYER_IGNORE_RAYCAST, KCDefine.U_LAYER_WATER
	};
	// 레이어 마스크 }
	
	// 동기화 객체
	public static readonly object U_LOCK_OBJ_COMMON = new object();
	public static readonly object U_LOCK_OBJ_TASK_M_UPDATE = new object();
	public static readonly object U_LOCK_OBJ_SCHEDULE_M_UPDATE = new object();

	// 경로 {
	public static readonly string U_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfo_{"{0:000000000}"}";
	public static readonly string U_DATA_P_FMT_G_LEVEL_INFO_SET_A = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}A/{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfo_{"{0:000000000}"}";
	public static readonly string U_DATA_P_FMT_G_LEVEL_INFO_SET_B = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}B/{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfo_{"{0:000000000}"}";

	public static readonly string U_OBJ_P_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_Text";
	public static readonly string U_OBJ_P_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_TextBtn";
	public static readonly string U_OBJ_P_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_TextScaleBtn";

	public static readonly string U_OBJ_P_TMP_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_TMPText";
	public static readonly string U_OBJ_P_TMP_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPTextBtn";
	public static readonly string U_OBJ_P_TMP_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPTextScaleBtn";

	public static readonly string U_OBJ_P_LOCALIZE_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_LocalizeText";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_LocalizeTextBtn";
	public static readonly string U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_LocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_TMP_LOCALIZE_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_TEXT_ROOT}U_TMPLocalizeText";
	public static readonly string U_OBJ_P_TMP_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPLocalizeTextBtn";
	public static readonly string U_OBJ_P_TMP_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPLocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_Img";
	public static readonly string U_OBJ_P_RAW_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_RawImg";
	public static readonly string U_OBJ_P_FOCUS_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_FocusImg";
	public static readonly string U_OBJ_P_GAUGE_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_IMAGE_ROOT}U_GaugeImg";

	public static readonly string U_OBJ_P_IMG_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgBtn";
	public static readonly string U_OBJ_P_IMG_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}U_ImgScaleBtn";

	public static readonly string U_OBJ_P_IMG_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_ImgTextBtn";
	public static readonly string U_OBJ_P_IMG_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_ImgTextScaleBtn";

	public static readonly string U_OBJ_P_TMP_IMG_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPImgTextBtn";
	public static readonly string U_OBJ_P_TMP_IMG_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPImgTextScaleBtn";

	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_ImgLocalizeTextBtn";
	public static readonly string U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}Text/U_ImgLocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_TMP_IMG_LOCALIZE_TEXT_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPImgLocalizeTextBtn";
	public static readonly string U_OBJ_P_TMP_IMG_LOCALIZE_TEXT_SCALE_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_BUTTON_ROOT}TextMeshPro/U_TMPImgLocalizeTextScaleBtn";

	public static readonly string U_OBJ_P_DROPDOWN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_Dropdown";
	public static readonly string U_OBJ_P_INPUT_FIELD = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_InputField";

	public static readonly string U_OBJ_P_TMP_DROPDOWN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_TMPDropdown";
	public static readonly string U_OBJ_P_TMP_INPUT_FIELD = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_INPUT_ROOT}U_TMPInputField";

	public static readonly string U_OBJ_P_PAGE_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_PageView";
	public static readonly string U_OBJ_P_SCROLL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_ScrollView";
	public static readonly string U_OBJ_P_RECYCLE_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_SCROLL_VIEW_ROOT}U_RecycleView";

	public static readonly string U_OBJ_P_SPRITE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_2D_ROOT}U_Sprite";
	public static readonly string U_OBJ_P_LINE_FX = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_LineFX";
	public static readonly string U_OBJ_P_PARTICLE_FX = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_ParticleFX";
	public static readonly string U_OBJ_P_TIMER_MANAGER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_EXTERNAL_ROOT}U_TimerManager";
	public static readonly string U_OBJ_P_REFLECTION_PROBE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_ReflectionProbe";
	public static readonly string U_OBJ_P_LIGHT_PROBE_GROUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_UTILITY}{KCDefine.B_DIR_P_FX_ROOT}U_LightProbeGroup";

	public static readonly string U_OBJ_P_G_VER_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}G_VerText";
	public static readonly string U_OBJ_P_G_BACK_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}G_BackBtn";
	
	public static readonly string U_OBJ_P_G_BG_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_BGSnd";
	public static readonly string U_OBJ_P_G_FX_SND = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SOUND_ROOT}G_FXSnd";

	public static readonly string U_OBJ_P_G_ALERT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_AlertPopup";
	public static readonly string U_OBJ_P_G_STORE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_StorePopup";
	public static readonly string U_OBJ_P_G_SETTINGS_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SettingsPopup";
	public static readonly string U_OBJ_P_G_SYNC_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SyncPopup";
	public static readonly string U_OBJ_P_G_DAILY_MISSION_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_DailyMissionPopup";
	public static readonly string U_OBJ_P_G_FREE_REWARD_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_FreeRewardPopup";
	public static readonly string U_OBJ_P_G_DAILY_REWARD_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_DailyRewardPopup";
	public static readonly string U_OBJ_P_G_SALE_COINS_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SaleCoinsPopup";
	public static readonly string U_OBJ_P_G_REWARD_ACQUIRE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_RewardAcquirePopup";
	public static readonly string U_OBJ_P_G_SALE_COINS_ACQUIRE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_SaleCoinsAcquirePopup";
	public static readonly string U_OBJ_P_G_CONTINUE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_ContinuePopup";
	public static readonly string U_OBJ_P_G_RESULT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_ResultPopup";
	public static readonly string U_OBJ_P_G_FOCUS_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_FocusPopup";
	public static readonly string U_OBJ_P_G_TUTORIAL_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_TutorialPopup";

	public static readonly string U_OBJ_P_G_DRAG_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_DragResponder";
	public static readonly string U_OBJ_P_G_TOUCH_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_TouchResponder";
	public static readonly string U_OBJ_P_G_INDICATOR_TOUCH_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_IndicatorTouchResponder";
	public static readonly string U_OBJ_P_G_SCREEN_FADE_TOUCH_RESPONDER = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_RESPONDER_ROOT}G_ScreenFadeTouchResponder";

	public static readonly string U_ASSET_P_G_NORM_QUALITY_UNIVERSAL_RP = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_NormQualityUniversalRPAsset";
	public static readonly string U_ASSET_P_G_HIGH_QUALITY_UNIVERSAL_RP = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_HighQualityUniversalRPAsset";
	public static readonly string U_ASSET_P_G_ULTRA_QUALITY_UNIVERSAL_RP = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UltraQualityUniversalRPAsset";

	public static readonly string U_ASSET_P_G_NORM_QUALITY_LIGHTING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_NormQualityLightingSettings";
	public static readonly string U_ASSET_P_G_HIGH_QUALITY_LIGHTING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_HighQualityLightingSettings";
	public static readonly string U_ASSET_P_G_ULTRA_QUALITY_LIGHTING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_UltraQualityLightingSettings";

	public static readonly string U_ASSET_P_G_NORM_QUALITY_POST_PROCESSING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_NormQualityPostProcessingSettings";
	public static readonly string U_ASSET_P_G_HIGH_QUALITY_POST_PROCESSING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_HighQualityPostProcessingSettings";
	public static readonly string U_ASSET_P_G_ULTRA_QUALITY_POST_PROCESSING_SETTINGS = $"{KCDefine.B_DIR_P_SETTINGS}{KCDefine.B_DIR_P_GLOBAL}G_UltraQualityPostProcessingSettings";

	public static readonly string U_ASSET_P_G_UNIVERSAL_RP_RENDERER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPRendererData";
	public static readonly string U_ASSET_P_G_UNIVERSAL_RP_SSAO_RENDERER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPSSAORendererData";

	public static readonly string U_ASSET_P_G_UNIVERSAL_RP_2D_RENDERER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRP2DRendererData";
	public static readonly string U_ASSET_P_G_UNIVERSAL_RP_2D_SSAO_RENDERER_DATA = $"{KCDefine.B_DIR_P_PIPELINES}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRP2DSSAORendererData";

	public static readonly string U_ASSET_P_G_OPTS_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_OptsInfoTable";
	public static readonly string U_ASSET_P_G_BUILD_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BuildInfoTable";
	public static readonly string U_ASSET_P_G_DEFINE_SYMBOL_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DefineSymbolInfoTable";
	public static readonly string U_ASSET_P_G_PROJ_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProjInfoTable";
	public static readonly string U_ASSET_P_G_DEVICE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_DeviceInfoTable";
	public static readonly string U_ASSET_P_G_LOCALIZE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_LocalizeInfoTable";
	public static readonly string U_ASSET_P_G_STORAGE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_StorageInfoTable";

	public static readonly string U_ASSET_P_G_SALE_ITEM_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleItemInfoTable";
	public static readonly string U_ASSET_P_G_SALE_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleProductInfoTable";
	public static readonly string U_ASSET_P_G_MISSION_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_MissionInfoTable";
	public static readonly string U_ASSET_P_G_REWARD_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_RewardInfoTable";
	public static readonly string U_ASSET_P_G_EPISODE_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_EpisodeInfoTable";
	public static readonly string U_ASSET_P_G_TUTORIAL_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_TutorialInfoTable";
	public static readonly string U_ASSET_P_G_FX_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_FXInfoTable";
	public static readonly string U_ASSET_P_G_BLOCK_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_BlockInfoTable";

	public static readonly string U_ASSET_P_SPRITE_ATLAS_01 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_01";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_02 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_02";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_03 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_03";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_04 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_04";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_05 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_05";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_06 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_06";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_07 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_07";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_08 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_08";
	public static readonly string U_ASSET_P_SPRITE_ATLAS_09 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_UTILITY}U_SpriteAtlas_09";
	
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_01 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_01";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_02 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_02";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_03 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_03";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_04 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_04";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_05 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_05";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_06 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_06";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_07 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_07";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_08 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_08";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_09 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_09";

	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_01 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}01";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_02 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}02";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_03 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}03";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_04 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}04";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_05 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}05";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_06 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}06";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_07 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}07";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_08 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}08";
	public static readonly string U_ASSET_P_G_SPRITE_ATLAS_FIX_PF_09 = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}09";

	public static readonly string U_ASSET_P_FMT_SPRITE_ATLAS = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}U_SpriteAtlas_{"{0:00}"}";
	public static readonly string U_ASSET_P_FMT_G_SPRITE_ATLAS = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas_{"{0:00}"}";
	public static readonly string U_ASSET_P_FMT_G_FIX_PF_SPRITE_ATLAS = $"{KCDefine.B_DIR_P_SPRITE_ATLASES}{KCDefine.B_DIR_P_GLOBAL}G_SpriteAtlas{KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER}{"{0:00}"}";

	public static readonly string U_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfoTable";
	public static readonly string U_TABLE_P_G_LEVEL_INFO_SET_A = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}A/{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfoTable";
	public static readonly string U_TABLE_P_G_LEVEL_INFO_SET_B = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}B/{KCDefine.B_DIR_P_LEVEL_INFO_ROOT}G_LevelInfoTable";

	public static readonly string U_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_EpisodeInfoTable";
	public static readonly string U_TABLE_P_G_EPISODE_INFO_SET_A = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}A/G_EpisodeInfoTable";
	public static readonly string U_TABLE_P_G_EPISODE_INFO_SET_B = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}B/G_EpisodeInfoTable";

	public static readonly string U_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleItemInfoTable";
	public static readonly string U_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_SaleProductInfoTable";
	public static readonly string U_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_MissionInfoTable";
	public static readonly string U_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_RewardInfoTable";
	public static readonly string U_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_TutorialInfoTable";
	public static readonly string U_TABLE_P_G_FX_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_FXInfoTable";
	public static readonly string U_TABLE_P_G_BLOCK_INFO = $"{KCDefine.B_DIR_P_TABLES}{KCDefine.B_DIR_P_GLOBAL}G_BlockInfoTable";
	
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

	public static readonly string U_FONT_P_G_DEF = $"Fonts & Materials/LiberationSans SDF";
	public static readonly string U_FONT_P_G_KOREAN = $"{KCDefine.B_DIR_P_FONTS}{KCDefine.B_DIR_P_GLOBAL}G_KoreanFont";
	public static readonly string U_FONT_P_G_ENGLISH = $"{KCDefine.B_DIR_P_FONTS}{KCDefine.B_DIR_P_GLOBAL}G_EnglishFont";
	
	public static readonly string U_SND_P_G_TOUCH_BEGIN = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchBegin";
	public static readonly string U_SND_P_G_TOUCH_END = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_TouchEnd";

	public static readonly string U_SND_P_G_POPUP_SHOW = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_PopupShow";
	public static readonly string U_SND_P_G_POPUP_CLOSE = $"{KCDefine.B_DIR_P_SOUNDS}{KCDefine.B_DIR_P_GLOBAL}G_PopupClose";

	public static readonly string U_MAT_P_G_UNIVERSAL_RP_LIT = $"{KCDefine.B_DIR_P_MATERIALS}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPLitMat";
	public static readonly string U_MAT_P_G_UNIVERSAL_RP_UNLIT = $"{KCDefine.B_DIR_P_MATERIALS}{KCDefine.B_DIR_P_GLOBAL}G_UniversalRPUnlitMat";

	public static readonly string U_IMG_P_G_ARROW = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_Arrow";
	public static readonly string U_IMG_P_G_WHITE = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_UnityWhite";
	public static readonly string U_IMG_P_G_SPLASH = $"{KCDefine.B_DIR_P_IMAGES}{KCDefine.B_DIR_P_GLOBAL}G_Splash";

#if MSG_PACK_ENABLE
	public static readonly string U_DATA_P_COMMON_APP_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonAppInfo.bytes";
	public static readonly string U_DATA_P_COMMON_USER_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonUserInfo.bytes";
	public static readonly string U_DATA_P_COMMON_GAME_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonGameInfo.bytes";
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
	public static readonly string U_DATA_P_COMMON_APP_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonAppInfo.json";
	public static readonly string U_DATA_P_COMMON_USER_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonUserInfo.json";
	public static readonly string U_DATA_P_COMMON_GAME_INFO = $"{KCDefine.B_DIR_P_WRITABLE}CommonGameInfo.json";
#endif			// #if MSG_PACK_ENABLE

#if UNITY_EDITOR
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_A}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_B}.bytes";

	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_A}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_B}.json";

	public static readonly string U_RUNTIME_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_FX_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_FX_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_BLOCK_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_BLOCK_INFO}.json";

#if MSG_PACK_ENABLE
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A}.bytes";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B}.bytes";
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.json";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A}.json";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B}.json";
#endif			// #if MSG_PACK_ENABLE

#if FIREBASE_MODULE_ENABLE
	public static readonly string U_RUNTIME_DATA_P_G_GAME_CONFIG = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_GAME_CONFIG}.json";
	public static readonly string U_RUNTIME_DATA_P_G_BUILD_VER_CONFIG = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_DATA_P_G_BUILD_VER_CONFIG}.json";
#endif			// #if FIREBASE_MODULE_ENABLE
#else
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_A}.bytes";
	public static readonly string U_RUNTIME_TABLE_P_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_LEVEL_INFO_SET_B}.bytes";

	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_A}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_EPISODE_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_EPISODE_INFO_SET_B}.json";

	public static readonly string U_RUNTIME_TABLE_P_G_SALE_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_ITEM_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_SALE_PRODUCT_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_SALE_PRODUCT_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_MISSION_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_MISSION_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_REWARD_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_REWARD_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_TUTORIAL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_TUTORIAL_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_FX_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_FX_INFO}.json";
	public static readonly string U_RUNTIME_TABLE_P_G_BLOCK_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_BLOCK_INFO}.json";

#if MSG_PACK_ENABLE
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.bytes";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A}.bytes";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B}.bytes";
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO}.json";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_A = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_A}.json";
	public static readonly string U_RUNTIME_DATA_P_FMT_G_LEVEL_INFO_SET_B = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_DATA_P_FMT_G_LEVEL_INFO_SET_B}.json";
#endif			// #if MSG_PACK_ENABLE

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
	// 퀄리티
	public const int U_QUALITY_ASYNC_UPLOAD_TIME_SLICE = 2;
	public const int U_QUALITY_ASYNC_UPLOAD_BUFFER_SIZE = 16;

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
	public const int U_SCRIPT_O_INTRO_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 1;

	public const int U_SCRIPT_O_OVERLAY_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 3;
	public const int U_SCRIPT_O_LOADING_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 3;

	public const int U_SCRIPT_O_SCENE_MANAGER = KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER + 2;
	// 스크립트 순서 }
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
	// 이름
	public const string U_MODEL_N_IPAD = "iPad";
	public const string U_MODEL_N_IPHONE = "iPhone";

#if APPLE_LOGIN_ENABLE
	// 식별자
	public const string U_KEY_SERVICES_M_UPDATE_APPLE_LOGIN_STATE_CALLBACK = "ServicesMUpdateAppleLoginStateCallback";
	public const string U_KEY_SERVICES_M_UPDATE_FAIL_APPLE_LOGIN_STATE_CALLBACK = "ServicesMUpdateFailAppleLoginStateCallback";

	public const string U_KEY_SERVICES_M_LOGIN_WITH_APPLE_CALLBACK = "ServicesMLoginWithAppleCallback";
	public const string U_KEY_SERVICES_M_LOGIN_FAIL_WITH_APPLE_CALLBACK = "ServicesMLoginFailWithAppleCallback";
#endif			// #if APPLE_LOGIN_ENABLE
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
	// 시간
	public const float U_DELTA_T_PERMISSION_M_REQUEST_CHECK = 0.25f;
	public const float U_MAX_DELTA_T_PERMISSION_M_REQUEST_CHECK = 1.0f;
#endif			// #if UNITY_ANDROID

#if DEBUG || DEVELOPMENT_BUILD
	// 형식 {
	public const string U_TEXT_FMT_STATIC_DEBUG_MSG = "{0}\n\n{1}";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_MSG = "{0}\n\n{1}";

	public const string U_TEXT_FMT_FPS = "FPS: <color=orange>{0:0.0}</color>";
	public const string U_TEXT_FMT_FRAME_TIME = "Frame Time: <color=orange>{0:0.0}</color> ms";

	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_A = "Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_B = "Root Offset: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>, <color=orange>{2:0.0}</color>, <color=orange>{3:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_C = "Screen Size: <color=orange>{0:0.0}</color>, <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_D = "Screen DPI: <color=orange>{0:0.0}</color>, Banner Ads Height: <color=orange>{1:0.0}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_E = "Quality Level: <color=orange>{0}</color>, Target Frame Rate: <color=orange>{1}</color>\n";
	public const string U_TEXT_FMT_STATIC_DEBUG_INFO_F = "Persistent Data Path: <color=orange>{0}</color>";

	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_A = "Used Heap: <color=orange>{0:0.0}</color> MB, GPU Alloc: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_B = "Mono Heap: <color=orange>{0:0.0}</color> MB, Mono Used: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_C = "Temp Alloc: <color=orange>{0:0.0}</color> MB, Total Alloc: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_D = "Reserved: <color=orange>{0:0.0}</color> MB, Unused Reserved: <color=orange>{1:0.0}</color> MB\n";
	public const string U_TEXT_FMT_DYNAMIC_DEBUG_INFO_E = "Time Scale: <color=orange>{0:0.00}</color>";
	// 형식 }

	// 이름 {
	public const string U_OBJ_N_SCREEN_FPS_INFO_UIS = "FPSInfoUIs";
	public const string U_OBJ_N_SCREEN_DEBUG_INFO_UIS = "DebugInfoUIs";

	public const string U_OBJ_N_SCREEN_FPS_TEXT = "FPSText";
	public const string U_OBJ_N_SCREEN_FRAME_TIME_TEXT = "FrameTimeText";

	public const string U_OBJ_N_SCREEN_STATIC_DEBUG_TEXT = "StaticDebugText";
	public const string U_OBJ_N_SCREEN_DYNAMIC_DEBUG_TEXT = "DynamicDebugText";

	public const string U_OBJ_N_SCREEN_FPS_INFO_BTN = "FPSInfoBtn";
	public const string U_OBJ_N_SCREEN_DEBUG_INFO_BTN = "DebugInfoBtn";

	public const string U_OBJ_N_SCREEN_TIME_SCALE_UP_BTN = "TimeScaleUpBtn";
	public const string U_OBJ_N_SCREEN_TIME_SCALE_DOWN_BTN = "TimeScaleDownBtn";
	// 이름 }
#endif			// #if DEBUG || DEVELOPMENT_BUILD

#if ADS_MODULE_ENABLE
	// 시간
	public const float U_DELTA_T_ADS_M_ADS_LOAD = 5.0f;
	public const float U_DELTA_T_REWARD_ATI_UPDATE = 0.5f;
	
	// 식별자 {
	public const string U_KEY_ADS_M_BANNER_ADS_ID = "AdsMBannerAdsID";
	public const string U_KEY_ADS_M_REWARD_ADS_ID = "AdsMRewardAdsID";
	public const string U_KEY_ADS_M_FULLSCREEN_ADS_ID = "AdsMFullscreenAdsID";

	public const string U_KEY_FMT_ADS_M_LOAD_FAIL_BANNER_ADS_INFO = "AdsMLoadFailBannerAdsInfo_{0}";
	public const string U_KEY_FMT_ADS_M_LOAD_FAIL_REWARD_ADS_INFO = "AdsMLoadFailRewardAdsInfo_{0}";
	public const string U_KEY_FMT_ADS_M_LOAD_FAIL_FULLSCREEN_ADS_INFO = "AdsMLoadFailFullscreenAdsInfo_{0}";
	// 식별자 }

#if ADMOB_ADS_ENABLE
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
	public const string U_KEY_ADS_M_ADMOB_REWARD_ADS_RECEIVE_ADS_REWARD_CALLBACK = "AdsMAdmobRewardAdsReceiveAdsRewardCallback";

	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_LOAD_CALLBACK = "AdsMAdmobFullscreenAdsLoadCallback";
	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMAdmobFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_ADMOB_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMAdmobFullscreenAdsCloseCallback";
	// 식별자 }
#endif			// #if ADMOB_ADS_ENABLE

#if IRON_SRC_ADS_ENABLE
	// 식별자 {
	public const string U_KEY_ADS_M_IRON_SRC_INIT_CALLBACK = "AdsMIronSrcInitCallback";

	public const string U_KEY_ADS_M_IRON_SRC_BANNER_ADS_LOAD_CALLBACK = "AdsMIronSrcBannerAdsLoadCallback";
	public const string U_KEY_ADS_M_IRON_SRC_BANNER_ADS_LOAD_FAIL_CALLBACK = "AdsMIronSrcBannerAdsLoadFailCallback";

	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_CLOSE_CALLBACK = "AdsMIronSrcRewardAdsCloseCallback";
	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_RECEIVE_ADS_REWARD_CALLBACK = "AdsMIronSrcRewardAdsReceiveAdsRewardCallback";
	public const string U_KEY_ADS_M_IRON_SRC_REWARD_ADS_CHANGE_STATE_CALLBACK = "AdsMIronSrcRewardAdsChangeStateCallback";

	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_LOAD_CALLBACK = "AdsMIronSrcFullscreenAdsLoadCallback";
	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMIronSrcFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_IRON_SRC_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMIronSrcFullscreenAdsCloseCallback";
	// 식별자 }
#endif			// #if IRON_SRC_ADS_ENABLE

#if APP_LOVIN_ADS_ENABLE
	// 식별자 {
	public const string U_KEY_ADS_M_APP_LOVIN_INIT_CALLBACK = "AdsMAppLovinInitCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_BANNER_ADS_LOAD_CALLBACK = "AdsMAppLovinBannerAdsLoadCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_BANNER_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinBannerAdsLoadFailCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinRewardAdsLoadFailCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_CLOSE_CALLBACK = "AdsMAppLovinRewardAdsCloseCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_REWARD_ADS_RECEIVE_ADS_REWARD_CALLBACK = "AdsMAppLovinRewardAdsReceiveAdsRewardCallback";

	public const string U_KEY_ADS_M_APP_LOVIN_FULLSCREEN_ADS_LOAD_FAIL_CALLBACK = "AdsMAppLovinFullscreenAdsLoadFailCallback";
	public const string U_KEY_ADS_M_APP_LOVIN_FULLSCREEN_ADS_CLOSE_CALLBACK = "AdsMAppLovinFullscreenAdsCloseCallback";
	// 식별자 }
#endif			// #if APP_LOVIN_ADS_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
	// 시간
	public const long U_TIMEOUT_FLURRY_M_NETWORK_CONNECTION = 60 * KCDefine.B_UNIT_MILLI_SECS_PER_SEC;

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
	public const string U_KEY_FIREBASE_M_GAME_CONFIG = "GameConfig";
	public const string U_KEY_FIREBASE_M_DEVICE_CONFIG = "DeviceConfig";
	public const string U_KEY_FIREBASE_M_BUILD_VER_CONFIG = "BuildVerConfig";

	public const string U_KEY_FIREBASE_M_INIT_CALLBACK = "FirebaseMInitCallback";
	// 식별자 }

	// 노드
	public const string U_NODE_FIREBASE_USER_INFOS = "UserInfos";
	public const string U_NODE_FIREBASE_PURCHASE_INFOS = "PurchaseInfos";
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
	// 시간
	public const int U_TIMEOUT_APPS_FM_AGREE_TRACKING = 60;

	// 식별자
	public const string U_KEY_APPS_FM_INIT_CALLBACK = "AppsFMInitCallback";
#endif			// #if APPS_FLYER_MODULE_ENABLE

#if GAME_CENTER_MODULE_ENABLE
	// 식별자
	public const string U_KEY_GAME_CM_INIT_CALLBACK = "GameCMInitCallback";
	public const string U_KEY_GAME_CM_LOGIN_CALLBACK = "GameCMLoginCallback";
	public const string U_KEY_GAME_CM_UPDATE_RECORD_CALLBACK = "GameCMUpdateRecordCallback";
	public const string U_KEY_GAME_CM_UPDATE_ACHIEVEMENT_CALLBACK = "GameCMUpdateAchievementCallback";
#endif			// #if GAME_CENTER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	// 기타
	public const string U_PAYLOAD_PURCHASE_M_PURCHASE = "PurchaseMPurchase";

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
	public const string U_GROUP_ID_NOTI = "DefNotiGroup";
	public const string U_KEY_NOTI_M_INIT_CALLBACK = "NotiMInitCallback";

#if UNITY_IOS
	// 옵션
	public const PresentationOption U_PRESENT_OPTS_NOTI = PresentationOption.Alert | PresentationOption.Sound;
	public const AuthorizationOption U_AUTHORIZATION_OPTS_NOTI = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
	// 그룹 정보 {
	public const string U_GROUP_N_NOTI = KCDefine.U_GROUP_ID_NOTI;
	public const string U_GROUP_DESC_NOTI = KCDefine.U_GROUP_ID_NOTI;
	
	public const Importance U_IMPORTANCE_NOTI = Importance.Default;
	// 그룹 정보 }
#endif			// #if UNITY_ANDROID
#endif			// #if NOTI_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	// 이름
	public const string U_FIELD_N_CLEAR_DEPTH = "m_ClearDepth";
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 조건부 상수

	#region 조건부 런타임 상수
#if UNITY_IOS
	// 버전
	public static readonly System.Version U_MIN_VER_CONSENT_VIEW = new System.Version(14, 0, 0);
	public static readonly System.Version U_MIN_VER_HAPTIC_FEEDBACK = new System.Version(10, 0, 0);

	// 햅틱 피드백 지원 모델
	public static readonly List<DeviceGeneration> U_HAPTIC_FEEDBACK_SUPPORTS_MODEL_LIST = new List<DeviceGeneration>() {
		DeviceGeneration.iPhone7, DeviceGeneration.iPhone7Plus, DeviceGeneration.iPhone8, DeviceGeneration.iPhone8Plus, DeviceGeneration.iPhoneX, DeviceGeneration.iPhoneXR, DeviceGeneration.iPhoneXS, DeviceGeneration.iPhoneXSMax, DeviceGeneration.iPhone11, DeviceGeneration.iPhone11Pro, DeviceGeneration.iPhone11ProMax, DeviceGeneration.iPhoneUnknown
	};
#endif			// #if UNITY_IOS

#if ADS_MODULE_ENABLE
	// 기타
	public static readonly STAdsRewardInfo U_INVALID_ADS_REWARD_INFO = new STAdsRewardInfo() {
		m_oID = string.Empty, m_oVal = string.Empty
	};

	// 크기
	public static readonly Vector3 U_SIZE_BANNER_ADS = new Vector3(320.0f, 50.0f, 0.0f);

#if ADMOB_ADS_ENABLE
	// 크기
	public static readonly AdSize U_SIZE_ADMOB_BANNER_ADS = new AdSize(320, 50);
#endif			// #if ADMOB_ADS_ENABLE

#if IRON_SRC_ADS_ENABLE
	// 크기
	public static readonly IronSourceBannerSize U_SIZE_IRON_SRC_BANNER_ADS = new IronSourceBannerSize(320, 50);
#endif			// #if IRON_SRC_ADS_ENABLE

#if APP_LOVIN_ADS_ENABLE
	// 색상
	public static readonly Color U_COLOR_APP_LOVIN_BANNER_BG = Color.black;
#endif			// #if APP_LOVIN_ADS_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE
	// 경로
	public static readonly string U_ASSET_P_G_PLUGIN_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_PluginInfoTable";
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || FIREBASE_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	// 권한
	public static readonly List<string> U_PERMISSION_LIST_FACEBOOK = new List<string>() {
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
	// 경로 {
	public static readonly string U_ASSET_P_G_PRODUCT_INFO_TABLE = $"{KCDefine.B_DIR_P_SCRIPTABLES}{KCDefine.B_DIR_P_GLOBAL}G_ProductInfoTable";

#if MSG_PACK_ENABLE
	public static readonly string U_DATA_P_PURCHASE_PRODUCT_IDS = $"{KCDefine.B_DIR_P_WRITABLE}PurchaseProductIDs.bytes";
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
	public static readonly string U_DATA_P_PURCHASE_PRODUCT_IDS = $"{KCDefine.B_DIR_P_WRITABLE}PurchaseProductIDs.json";
#endif			// #if MSG_PACK_ENABLE
	// 경로 }
#endif			// #if PURCHASE_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
	// 경로 {
#if MSG_PACK_ENABLE
	public static readonly string U_DATA_P_NOTI_GROUP_IDS = $"{KCDefine.B_DIR_P_WRITABLE}NotiGroupIDs.bytes";
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
	public static readonly string U_DATA_P_NOTI_GROUP_IDS = $"{KCDefine.B_DIR_P_WRITABLE}NotiGroupIDs.json";
#endif			// #if MSG_PACK_ENABLE
	// 경로 }
#endif			// #if NOTI_MODULE_ENABLE
	#endregion			// 조건부 런타임 상수
}
