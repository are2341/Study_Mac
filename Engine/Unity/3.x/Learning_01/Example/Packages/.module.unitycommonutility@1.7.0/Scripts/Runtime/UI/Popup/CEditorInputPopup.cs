using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 에디터 입력 팝업
public class CEditorInputPopup : CPopup {
	//! 콜백 매개 변수
	public struct STCallbackParams {
		public System.Action<CEditorInputPopup, string> m_oCallback;
	}

	#region 변수
	private STCallbackParams m_stCallbackParams;

	// =====> UI <=====
	protected InputField m_oInput = null;
	#endregion			// 변수
	
	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreAni = true;

		// 입력 필드를 설정한다
		m_oInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_IP_INPUT);
		
		// 버튼을 설정한다 {
		var oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN);
		oOKBtn?.onClick.AddListener(this.OnTouchOKBtn);

		var oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN);
		oCancelBtn?.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }
	}

	//! 초기화
	public virtual void Init(STCallbackParams a_stCallbackParams) {
		base.Init();
		m_stCallbackParams = a_stCallbackParams;

		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// Do Something
	}

	//! 확인 버튼을 눌렀을 경우
	private void OnTouchOKBtn() {
		m_stCallbackParams.m_oCallback?.Invoke(this, m_oInput?.text);
		this.OnTouchCloseBtn();
	}

	//! 취소 버튼을 눌렀을 경우
	private void OnTouchCancelBtn() {
		this.OnTouchCloseBtn();
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
