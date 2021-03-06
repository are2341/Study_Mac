﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 스플래시 씬 상수
public static partial class KCDefine {
	#region 기본
	// 시간
	public const float SS_DELAY_SPLASH_SM_NEXT_SCENE_LOAD = 2.0f;

	// 이름
	public const string SS_OBJ_N_SPLASH_SM_LOGO_IMG = "LogoImg";
	#endregion			// 기본

	#region 런타임 상수
	// 색상
	public static readonly Color SS_COLOR_SPLASH_SM_BG = new Color(0x29 / (float)KCDefine.B_UNIT_NORM_VAL_TO_BYTE, 
		0x4c / (float)KCDefine.B_UNIT_NORM_VAL_TO_BYTE, 0x94 / (float)KCDefine.B_UNIT_NORM_VAL_TO_BYTE);

	// 위치
	public static readonly Vector3 SS_POS_SPLASH_SM_LOGO_IMG = new Vector3(0.0f, 25.0f, 0.0f);
	#endregion			// 런타임 상수
}
