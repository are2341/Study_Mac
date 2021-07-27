using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 에디터 입력 팝업
public class CEditorInputPopup : CPopup {
	#region 변수
	private System.Action<CEditorInputPopup, string> m_oCallback = null;
	#endregion			// 변수

	#region UI 변수
	private InputField m_oInput = null;
	#endregion			// UI 변수

	#region 프로퍼티
	public override bool IsIgnoreAni => true;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 입력 필드를 설정한다
		m_oInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_IP_INPUT);
		
		// 버튼을 설정한다 {
		var oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.E_OBJ_N_EDITOR_IP_OK_BTN);
		oOKBtn?.onClick.AddListener(this.OnTouchOKBtn);

		var oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.E_OBJ_N_EDITOR_IP_CANCEL_BTN);
		oCancelBtn?.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }
	}

	//! 초기화
	public virtual void Init(System.Action<CEditorInputPopup, string> a_oCallback) {
		base.Init();
		m_oCallback = a_oCallback;
	}

	//! 확인 버튼을 눌렀을 경우
	private void OnTouchOKBtn() {
		m_oCallback?.Invoke(this, m_oInput?.text);
		this.OnTouchCloseBtn();
	}

	//! 취소 버튼을 눌렀을 경우
	private void OnTouchCancelBtn() {
		this.OnTouchCloseBtn();
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
