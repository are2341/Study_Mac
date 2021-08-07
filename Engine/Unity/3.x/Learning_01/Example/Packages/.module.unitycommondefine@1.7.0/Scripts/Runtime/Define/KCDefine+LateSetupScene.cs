using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 지연 설정 씬 상수
public static partial class KCDefine {
	#region 기본
	// 설명 팝업
	public const string LSS_OBJ_N_DESC_POPUP = "DescPopup";
	public const string LSS_OBJ_N_DESC_P_NEXT_BTN = "NextBtn";
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string LSS_OBJ_P_DESC_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_LATE_SETUP_SCENE}LSS_DescPopup";
	#endregion			// 런타임 상수
}
