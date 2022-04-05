using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 스터디 메뉴 씬 상수 */
public static partial class KSDefine {
	#region 기본
	// 이름
	public const string MS_OBJ_N_SCROLL_VIEW = "ScrollView";
	public const string MS_OBJ_N_FMT_TEXT = "Text_{0:00}";
	#endregion			// 기본

	#region 런타임 상수
	// 색상
	public static readonly Color MS_COLOR_TITLE = Color.red;

	// 경로
	public static readonly string MS_OBJ_P_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KSDefine.B_DIR_P_MENU_SCENE}MS_Btn";

	// 씬 이름
	public static readonly List<string> MS_DEF_SCENE_NAMES = new List<string>() {
		KCDefine.B_SCENE_N_INIT, KCDefine.B_SCENE_N_SETUP, KCDefine.B_SCENE_N_AGREE, KCDefine.B_SCENE_N_LATE_SETUP, KCDefine.B_SCENE_N_START, KCDefine.B_SCENE_N_SPLASH, KCDefine.B_SCENE_N_INTRO, KCDefine.B_SCENE_N_TITLE, KCDefine.B_SCENE_N_MAIN, KCDefine.B_SCENE_N_GAME, KCDefine.B_SCENE_N_LOADING, KCDefine.B_SCENE_N_OVERLAY, KCDefine.B_SCENE_N_MENU
	};
	#endregion			// 런타임 상수
}
