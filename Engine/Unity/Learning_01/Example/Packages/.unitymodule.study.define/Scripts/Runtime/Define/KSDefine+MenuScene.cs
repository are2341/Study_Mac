using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 스터디 메뉴 씬 상수
public static partial class KSDefine {
	#region 기본
	// 개수
	public const int MS_NUM_DEF_SCENES = 10;

	// 텍스트
	public const string MS_TOKEN_TITLE = "=====";

	// 이름
	public const string MS_OBJ_N_SCROLL_V_CONTENTS = "Contents";
	public const string MS_OBJ_N_FMT_TEXT = "Text_{0:00}";
	#endregion			// 기본

	#region 런타임 상수
	// 색상
	public static readonly Color MS_COLOR_TITLE = Color.red;

	// 경로
	public static readonly string MS_OBJ_P_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KSDefine.B_DIR_P_MENU_SCENE}MS_Text";
	#endregion			// 런타임 상수
}
