using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 설정 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름 {
	public const string SS_OBJ_N_POPUP_UIS = "PopupUIs";
	public const string SS_OBJ_N_TOPMOST_UIS = "TopmostUIs";
	public const string SS_OBJ_N_ABS_UIS = "AbsUIs";

	public const string SS_OBJ_N_TIMER_MANAGER = "TimerManager";
	// 이름 }
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string SS_OBJ_P_SCREEN_DEBUG_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenDebugUIs";
	public static readonly string SS_OBJ_P_SCREEN_POPUP_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenPopupUIs";
	public static readonly string SS_OBJ_P_SCREEN_TOPMOST_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenTopmostUIs";
	public static readonly string SS_OBJ_P_SCREEN_ABS_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenAbsUIs";
	#endregion			// 런타임 상수

	#region 조건부 상수
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 이름
	public const string SS_OBJ_N_DEBUG_UIS = "DebugUIs";
	public const string SS_OBJ_N_DEBUG_CONSOLE = "DebugConsole";
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 이름
	public const string SS_OBJ_N_FPS_COUNTER = "FPSCounter";
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 조건부 상수
}
