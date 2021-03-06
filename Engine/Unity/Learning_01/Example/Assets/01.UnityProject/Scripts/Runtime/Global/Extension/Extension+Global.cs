using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 전역 확장 클래스
public static partial class Extension {
	#region 클래스 함수
	//! 효과를 재생한다
	public static void ExPlay(this ParticleSystem a_oSender, System.Action<CEventDispatcher> a_oCallback, bool a_bIsReset = true, bool a_bIsRemoveChildren = false) {
		CAccess.Assert(a_oSender != null);
		var oEventDispatcher = a_oSender.GetComponentInChildren<CEventDispatcher>();

		// 이벤트 전달자가 존재 할 경우
		if(oEventDispatcher != null) {
			oEventDispatcher.ParticleEventCallback = a_oCallback;
		}

		a_oSender.ExPlay(a_bIsReset, a_bIsRemoveChildren);
	}
	#endregion			// 클래스 함수
}
