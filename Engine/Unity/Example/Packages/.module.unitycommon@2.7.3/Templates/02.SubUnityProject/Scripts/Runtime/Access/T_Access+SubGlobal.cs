﻿#if SCRIPT_TEMPLATE_ONLY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE
/** 서브 전역 접근자 */
public static partial class Access {
	#region 클래스 함수

	#endregion			// 클래스 함수
}

/** 서브 타이틀 씬 접근자 */
public static partial class Access {
	#region 클래스 함수

	#endregion			// 클래스 함수
}

/** 서브 메인 씬 접근자 */
public static partial class Access {
	#region 클래스 함수

	#endregion			// 클래스 함수
}

/** 서브 게임 씬 접근자 */
public static partial class Access {
	#region 클래스 함수
	/** 배경 스프라이트를 반환한다 */
	public static Sprite GetBGSprite(string a_oImgPathFmt, int a_nLevelID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return CResManager.Inst.GetRes<Sprite>(string.Format(a_oImgPathFmt, a_nChapterID + KCDefine.B_VAL_1_INT, a_nStageID + KCDefine.B_VAL_1_INT, a_nLevelID + KCDefine.B_VAL_1_INT));
	}
	#endregion			// 클래스 함수
}

/** 서브 로딩 씬 접근자 */
public static partial class Access {
	#region 클래스 함수

	#endregion			// 클래스 함수
}

/** 서브 중첩 씬 접근자 */
public static partial class Access {
	#region 클래스 함수

	#endregion			// 클래스 함수
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY