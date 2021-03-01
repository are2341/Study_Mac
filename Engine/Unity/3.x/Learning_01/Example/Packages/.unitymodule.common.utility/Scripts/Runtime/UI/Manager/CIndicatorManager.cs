using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 인디케이터 관리자
public class CIndicatorManager : CSingleton<CIndicatorManager> {
	#region 프로퍼티
	public int RefCount { get; private set; } = 0;
	#endregion			// 프로퍼티

	#region 함수
	//! 인디케이터를 출력한다
	public void Show(bool a_bIsShowIndicator, bool a_bIsShowBlindUI = true, bool a_bIsResetRefCount = false) {
		this.RefCount = Mathf.Min(int.MaxValue, this.RefCount + KCDefine.B_VALUE_INT_1);
		this.RefCount = a_bIsResetRefCount ? KCDefine.B_VALUE_INT_1 : this.RefCount;

		// 인디케이터 시작이 가능 할 경우
		if(this.RefCount > KCDefine.B_VALUE_INT_0) {
			// 인디케이터 출력 모드 일 경우
			if(a_bIsShowIndicator) {
#if UNITY_IOS || UNITY_ANDROID
				CUnityMsgSender.Inst.SendIndicatorMsg(true);
#endif			// #if UNITY_IOS || UNITY_ANDROID
			}

			// 블라인드 UI 를 출력한다
			if(a_bIsShowBlindUI) {
				var oParent = CSceneManager.ScreenTopmostUIs ?? CSceneManager.TopmostUIs;
				CSceneManager.ShowTouchResponder(oParent, KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_DEF_COLOR_INDICATOR_BG, null, true);
			}
		}
	}

	//! 인디케이터를 닫는다
	public void Close(bool a_bIsResetRefCount = false) {
		this.RefCount = Mathf.Max(KCDefine.B_VALUE_INT_0, this.RefCount - KCDefine.B_VALUE_INT_1);
		this.RefCount = a_bIsResetRefCount ? KCDefine.B_VALUE_INT_0 : this.RefCount;

		// 인디케이터 정지가 가능 할 경우
		if(this.RefCount <= KCDefine.B_VALUE_INT_0) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(false);
#endif			// #if UNITY_IOS || UNITY_ANDROID

			CSceneManager.CloseTouchResponder(KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
		}
	}
	#endregion			// 함수
}
