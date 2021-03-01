using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 토스트 팝업 관리자
public class CToastPopupManager : CSingleton<CToastPopupManager> {
	#region 변수
	private bool m_bIsEnableShow = true;
	private Queue<CToastPopup> m_oToastPopupList = new Queue<CToastPopup>();
	#endregion			// 변수
	
	#region 함수
	//! 상태를 갱신한다
	public virtual void Update() {
		// 토스트 팝업이 존재 할 경우
		if(m_bIsEnableShow && m_oToastPopupList.Count > KCDefine.B_VALUE_INT_0) {
			m_bIsEnableShow = false;
			
			var oToastPopup = m_oToastPopupList.Dequeue();
			oToastPopup.gameObject.SetActive(true);
			oToastPopup.Show(null, (a_oSender) => m_bIsEnableShow = true);
		}
	}

	//! 토스트 팝업을 출력한다
	public void Show(string a_oMsg, float a_fDuration = KCDefine.U_DEF_DURATION_TOAST_POPUP) {
		var oToastPopup = this.CreateToastPopup(a_oMsg, a_fDuration);
		oToastPopup.gameObject.SetActive(false);

		m_oToastPopupList.Enqueue(oToastPopup);
	}

	//! 토스트 팝업을 출력한다
	public void Show(string a_oMsg, Vector3 a_stPos, float a_fDuration = KCDefine.U_DEF_DURATION_TOAST_POPUP) {
		var oToastPopup = this.CreateToastPopup(a_oMsg, a_fDuration, a_stPos);
		oToastPopup.gameObject.SetActive(false);

		m_oToastPopupList.Enqueue(oToastPopup);
	}

	//! 토스트 팝업을 생성한다
	protected virtual CToastPopup CreateToastPopup(string a_oMsg, float a_fDuration) {
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));
		return this.CreateToastPopup(a_oMsg, a_fDuration, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	protected virtual CToastPopup CreateToastPopup(string a_oMsg, float a_fDuration, Vector3 a_stPos) {
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));
		var oObj = CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_TOAST_POPUP);

		return CToastPopup.Create<CToastPopup>(KCDefine.U_OBJ_N_TOAST_P_TOAST_POPUP, oObj, CSceneManager.ScreenTopmostUIs, a_oMsg, a_fDuration, a_stPos);
	}
	#endregion			// 함수
}
