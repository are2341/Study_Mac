using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 설정 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름 {
	public const string SS_OBJ_N_POPUP_UI = "PopupUI";
	public const string SS_OBJ_N_TOPMOST_UI = "TopmostUI";
	public const string SS_OBJ_N_ABS_UI = "AbsUI";

	public const string SS_OBJ_N_TIMER_MANAGER = "TimerManager";
	// 이름 }
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string SS_OBJ_P_SCREEN_DEBUG_UI = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenDebugUI";
	public static readonly string SS_OBJ_P_SCREEN_POPUP_UI = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenPopupUI";
	public static readonly string SS_OBJ_P_SCREEN_TOPMOST_UI = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenTopmostUI";
	public static readonly string SS_OBJ_P_SCREEN_ABS_UI = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenAbsUI";
	#endregion			// 런타임 상수

	#region 조건부 상수
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 이름
	public const string SS_OBJ_N_DEBUG_UI = "DebugUI";
	public const string SS_OBJ_N_DEBUG_CONSOLE = "DebugConsole";
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	// 이름
	public const string SS_OBJ_N_FPS_COUNTER = "FPSCounter";
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 조건부 상수
}
