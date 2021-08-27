using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 전역 접근자
public static partial class Access {
	#region 클래스 함수
	//! 튜토리얼 메세지를 반환한다
	public static string GetTutorialMsg(ETutorialKinds a_eTutorialKinds, int a_nIdx = KCDefine.B_VAL_0_INT) {
		string oKey = string.Format(KDefine.G_KEY_FMT_TUTORIAL_MSG, a_eTutorialKinds, a_nIdx + KCDefine.B_VAL_1_INT);
		return CStrTable.Inst.GetStr(oKey);
	}
	#endregion			// 클래스 함수

	#region 추가 클래스 변수

	#endregion			// 추가 클래스 변수

	#region 추가 클래스 프로퍼티

	#endregion			// 추가 클래스 프로퍼티

	#region 추가 클래스 함수

	#endregion			// 추가 클래스 함수
}
