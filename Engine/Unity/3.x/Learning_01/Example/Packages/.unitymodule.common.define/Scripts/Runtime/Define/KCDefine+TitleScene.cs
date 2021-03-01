using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 타이틀 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름
	public const string TS_OBJ_N_VERSION_TEXT = "VersionText";
	#endregion			// 기본

	#region 런타임 상수
	// 위치
	public static readonly Vector3 TS_POS_VERSION_TEXT = new Vector3(10.0f, -10.0f, 0.0f);

	// 경로
	public static readonly string TS_OBJ_P_VERSION_TEXT = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_TITLE_SCENE}TS_VersionText";
	#endregion			// 런타임 상수	
}
