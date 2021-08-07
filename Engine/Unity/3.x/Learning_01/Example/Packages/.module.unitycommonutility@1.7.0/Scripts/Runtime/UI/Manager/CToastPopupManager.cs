using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 토스트 팝업 관리자
public class CToastPopupManager : CSingleton<CToastPopupManager> {
	#region 변수
	private bool m_bIsEnableShow = true;
	private Queue<CToastPopup> m_oToastPopupQueue = new Queue<CToastPopup>();
	#endregion			// 변수
	
	#region 함수
	//! 상태를 갱신한다
	public virtual void Update() {
		// 토스트 팝업이 존재 할 경우
		if(m_bIsEnableShow && m_oToastPopupQueue.Count > KCDefine.B_VAL_0_INT) {
			m_bIsEnableShow = false;
			
			var oToastPopup = m_oToastPopupQueue.Dequeue();
			oToastPopup.gameObject.SetActive(true);
			oToastPopup.Show(null, (a_oSender) => m_bIsEnableShow = true);
		}
	}

	//! 토스트 팝업을 출력한다
	public void Show(CToastPopup.STParams a_stParams) {
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		this.Show(a_stParams, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 출력한다
	public void Show(CToastPopup.STParams a_stParams, Vector3 a_stPos) {
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var oToastPopup = this.CreateToastPopup(a_stParams, a_stPos);
		oToastPopup.gameObject.SetActive(false);

		m_oToastPopupQueue.Enqueue(oToastPopup);
	}

	//! 토스트 팝업을 생성한다
	protected virtual CToastPopup CreateToastPopup(CToastPopup.STParams a_stParams) {
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		return this.CreateToastPopup(a_stParams, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	protected virtual CToastPopup CreateToastPopup(CToastPopup.STParams a_stParams, Vector3 a_stPos) {
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		var oObj = CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_TOAST_POPUP);

		return CToastPopup.Create<CToastPopup>(KCDefine.U_OBJ_N_TOAST_P_TOAST_POPUP, oObj, CSceneManager.ScreenTopmostUIs, a_stParams, a_stPos);
	}
	#endregion			// 함수
}
