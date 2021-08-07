using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 에디터 스크롤러 셀 뷰
public class CEditorScrollerCellView : CScrollerCellView {
	//! 매개 변수
	public struct STParams {
		public STIDInfo m_stIDInfo;
		public EnhancedScroller m_oScroller;
	}
	
	//! 콜백 매개 변수
	public struct STCallbackParams {
		public System.Action<CEditorScrollerCellView, STIDInfo> m_oSelCallback;
		public System.Action<CEditorScrollerCellView, STIDInfo> m_oCopyCallback;
		public System.Action<CEditorScrollerCellView, STIDInfo> m_oMoveCallback;
		public System.Action<CEditorScrollerCellView, STIDInfo> m_oRemoveCallback;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;
	#endregion			// 변수

	#region UI 변수
	protected Text m_oNameText = null;

	protected Button m_oSelBtn = null;
	protected Button m_oMoveBtn = null;
	protected Button m_oRemoveBtn = null;
	#endregion			// UI 변수

	#region 프로퍼티
	public Text NameText => m_oNameText;

	public Button SelBtn => m_oSelBtn;
	public Button MoveBtn => m_oMoveBtn;
	public Button RemoveBtn => m_oRemoveBtn;

	public EnhancedScroller Scroller => m_stParams.m_oScroller;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oNameText = this.gameObject.ExFindComponent<Text>(KCDefine.E_OBJ_N_ESCV_NAME_TEXT);

		// 버튼을 설정한다 {
		m_oSelBtn = this.GetComponentInChildren<Button>();
		m_oSelBtn?.ExSetDisableColor(KCDefine.U_COLOR_NORM, false);
		m_oSelBtn?.onClick.AddListener(this.OnTouchSelBtn);

		m_oMoveBtn = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_MOVE_BTN);
		m_oMoveBtn?.onClick.AddListener(this.OnTouchMoveBtn);

		m_oRemoveBtn = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_REMOVE_BTN);
		m_oRemoveBtn?.onClick.AddListener(this.OnTouchRemoveBtn);

		var oCopyBtn = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_COPY_BTN);
		oCopyBtn?.onClick.AddListener(this.OnTouchCopyBtn);
		// 버튼을 설정한다 }

		// 컴포넌트를 설정한다
		this.gameObject.ExEnumerateComponents<CComponent>((a_oComponent, a_nIdx) => {
			a_oComponent.IsIgnoreEnableEvent = true;
			return true;
		});
	}

	//! 초기화
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		base.Init();

		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;

		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// Do Something
	}

	//! 선택 버튼을 눌렀을 경우
	private void OnTouchSelBtn() {
		m_stCallbackParams.m_oSelCallback?.Invoke(this, m_stParams.m_stIDInfo);
	}

	//! 복사 버튼을 눌렀을 경우
	private void OnTouchCopyBtn() {
		m_stCallbackParams.m_oCopyCallback?.Invoke(this, m_stParams.m_stIDInfo);
	}

	//! 이동 버튼을 눌렀을 경우
	private void OnTouchMoveBtn() {
		m_stCallbackParams.m_oMoveCallback?.Invoke(this, m_stParams.m_stIDInfo);
	}

	//! 제거 버튼을 눌렀을 경우
	private void OnTouchRemoveBtn() {
		m_stCallbackParams.m_oRemoveCallback?.Invoke(this, m_stParams.m_stIDInfo);
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
