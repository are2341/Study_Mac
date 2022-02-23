using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 설정 씬 상수 */
public static partial class KCDefine {
	#region 기본
	// 이름
	public const string SS_OBJ_N_TIMER_MANAGER = "TimerManager";
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string SS_OBJ_P_SCREEN_DEBUG_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenDebugUIs";
	public static readonly string SS_OBJ_P_SCREEN_POPUP_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenPopupUIs";
	public static readonly string SS_OBJ_P_SCREEN_TOPMOST_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenTopmostUIs";
	public static readonly string SS_OBJ_P_SCREEN_ABS_UIS = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_SETUP_SCENE}SS_ScreenAbsUIs";
	#endregion			// 런타임 상수
}
