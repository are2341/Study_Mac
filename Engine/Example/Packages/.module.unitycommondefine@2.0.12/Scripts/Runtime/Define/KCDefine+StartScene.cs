﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 시작 씬 상수 */
public static partial class KCDefine {
	#region 기본
	// 시간
	public const float SS_DELTA_T_UPDATE_STATE = 0.5f;
	
	// 이름
	public const string SS_OBJ_N_LOADING_GAUGE = "LoadingGauge";
	public const string SS_FUNC_N_START_SCENE_EVENT = "OnReceiveStartSceneEvent";
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string SS_OBJ_P_LOADING_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_START_SCENE}SS_LoadingText";
	public static readonly string SS_OBJ_P_LOADING_GAUGE = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_START_SCENE}SS_LoadingGauge";
	#endregion			// 런타임 상수
}