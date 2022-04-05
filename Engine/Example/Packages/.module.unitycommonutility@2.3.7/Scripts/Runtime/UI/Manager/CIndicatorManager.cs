using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 인디케이터 관리자 */
public partial class CIndicatorManager : CSingleton<CIndicatorManager> {
	#region 변수
	private int m_nRefCount = 0;
	#endregion			// 변수

	#region 함수
	/** 인디케이터를 출력한다 */
	public void Show(bool a_bIsResetRefCount = false, bool a_bIsShowBlindUIs = true) {
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_1_INT : Mathf.Clamp(m_nRefCount + KCDefine.B_VAL_1_INT, KCDefine.B_VAL_0_INT, int.MaxValue);

		// 인디케이터 시작이 가능 할 경우
		if(m_nRefCount > KCDefine.B_VAL_0_INT) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(true);
#endif			// #if UNITY_IOS || UNITY_ANDROID

			// 블라인드 UI 출력 모드 일 경우
			if(a_bIsShowBlindUIs) {
				var oParent = CSceneManager.ScreenTopmostUIs ?? CSceneManager.ActiveSceneTopmostUIs;
				CSceneManager.ShowTouchResponder(oParent, KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_INDICATOR_BG, null, true);
			}
		}
	}

	/** 인디케이터를 닫는다 */
	public void Close(bool a_bIsResetRefCount = false) {
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_0_INT : Mathf.Clamp(m_nRefCount - KCDefine.B_VAL_1_INT, KCDefine.B_VAL_0_INT, int.MaxValue);

		// 인디케이터 정지가 가능 할 경우
		if(m_nRefCount <= KCDefine.B_VAL_0_INT) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(false);
#endif			// #if UNITY_IOS || UNITY_ANDROID

			CSceneManager.CloseTouchResponder(KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
		}
	}
	#endregion			// 함수
}
