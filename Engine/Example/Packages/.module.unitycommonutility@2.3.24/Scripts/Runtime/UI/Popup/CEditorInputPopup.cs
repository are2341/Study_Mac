using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 에디터 입력 팝업 */
public partial class CEditorInputPopup : CPopup {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		OK_CANCEL,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public Dictionary<ECallback, System.Action<CEditorInputPopup, string, bool>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;

	/** =====> UI <===== */
	protected InputField m_oInput = null;
	#endregion			// 변수

	#region 프로퍼티
	public override bool IsIgnoreRebuildLayout => false;
	#endregion			// 프로퍼티
	
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.IsIgnoreAni = true;

		// 입력 필드를 설정한다
		m_oInput = this.Contents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_IP_INPUT);
		
		// 버튼을 설정한다
		this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN)?.onClick.AddListener(this.OnTouchOKBtn);
		this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN)?.onClick.AddListener(this.OnTouchCancelBtn);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// Do Something
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, (m_oInput != null) ? m_oInput.text : string.Empty, true);
		this.OnTouchCloseBtn();
	}

	/** 취소 버튼을 눌렀을 경우 */
	private void OnTouchCancelBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, string.Empty, false);
		this.OnTouchCloseBtn();
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
