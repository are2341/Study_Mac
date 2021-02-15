using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 초기화 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름
	public const string IS_OBJ_N_BLIND_UI = "BlindUI";
	#endregion			// 기본
	
	#region 런타임 상수
	// 경로
	public static readonly string IS_OBJ_P_SCREEN_BLIND_UI = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_INIT_SCENE}IS_ScreenBlindUI";
	public static readonly string IS_OBJ_P_SCREEN_BLIND_IMG = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_INIT_SCENE}IS_ScreenBlindImg";
	#endregion			// 런타임 상수
}
