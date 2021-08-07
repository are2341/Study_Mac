using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 스터디 스터디 씬 상수
public static partial class KSDefine {
	#region 기본
	// 이름
	public const string SS_OBJ_N_BACK_BTN = "BackBtn";
	public const string SS_OBJ_N_SCROLL_VIEW = "ScrollView";
	#endregion			// 기본

	#region 런타임 상수
	// 위치
	public static readonly Vector3 SS_POS_BACK_BTN = new Vector3(20.0f, -20.0f, 0.0f);
	
	// 경로
	public static readonly string SS_OBJ_P_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KSDefine.B_DIR_P_STUDY_SCENE}SS_Text";
	public static readonly string SS_OBJ_P_BACK_BTN = $"{KCDefine.B_DIR_P_PREFABS}{KSDefine.B_DIR_P_STUDY_SCENE}SS_BackBtn";
	#endregion			// 런타임 상수
}
