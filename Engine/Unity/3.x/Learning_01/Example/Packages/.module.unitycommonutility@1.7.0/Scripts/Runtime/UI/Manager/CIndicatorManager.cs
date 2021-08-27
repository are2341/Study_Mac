using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 인디케이터 관리자
public class CIndicatorManager : CSingleton<CIndicatorManager> {
	#region 변수
	private int m_nRefCount = 0;
	#endregion			// 변수

	#region 함수
	//! 인디케이터를 출력한다
	public void Show(bool a_bIsShowIndicator, bool a_bIsShowBlindUIs = true, bool a_bIsResetRefCount = false) {
		m_nRefCount = Mathf.Min(int.MaxValue, m_nRefCount + KCDefine.B_VAL_1_INT);
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_1_INT : m_nRefCount;

		// 인디케이터 시작이 가능 할 경우
		if(m_nRefCount > KCDefine.B_VAL_0_INT) {
			// 인디케이터 출력 모드 일 경우
			if(a_bIsShowIndicator) {
#if UNITY_IOS || UNITY_ANDROID
				CUnityMsgSender.Inst.SendIndicatorMsg(true);
#endif			// #if UNITY_IOS || UNITY_ANDROID
			}

#if !UNITY_EDITOR
			// 블라인드 UI 출력 모드 일 경우
			if(a_bIsShowBlindUIs) {
				var oParent = CSceneManager.ScreenTopmostUIs ?? CSceneManager.TopmostUIs;
				CSceneManager.ShowTouchResponder(oParent, KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_INDICATOR_BG, null, true);
			}
#endif			// #if !UNITY_EDITOR
		}
	}

	//! 인디케이터를 닫는다
	public void Close(bool a_bIsResetRefCount = false) {
		m_nRefCount = Mathf.Max(KCDefine.B_VAL_0_INT, m_nRefCount - KCDefine.B_VAL_1_INT);
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_0_INT : m_nRefCount;

		// 인디케이터 정지가 가능 할 경우
		if(m_nRefCount <= KCDefine.B_VAL_0_INT) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(false);
#endif			// #if UNITY_IOS || UNITY_ANDROID

#if !UNITY_EDITOR
			CSceneManager.CloseTouchResponder(KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
#endif			// #if !UNITY_EDITOR
		}
	}
	#endregion			// 함수
}
