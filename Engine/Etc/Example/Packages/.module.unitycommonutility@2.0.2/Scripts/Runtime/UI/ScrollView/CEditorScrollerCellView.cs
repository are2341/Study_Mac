using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 에디터 스크롤러 셀 뷰 */
public class CEditorScrollerCellView : CScrollerCellView {
	/** 매개 변수 */
	public new struct STParams {
		public CScrollerCellView.STParams m_stBaseParams;
	}
	
	/** 콜백 매개 변수 */
	public new struct STCallbackParams {
		public CScrollerCellView.STCallbackParams m_stBaseCallbackParams;

		public System.Action<CEditorScrollerCellView, long> m_oCopyCallback;
		public System.Action<CEditorScrollerCellView, long> m_oMoveCallback;
		public System.Action<CEditorScrollerCellView, long> m_oRemoveCallback;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;
	#endregion			// 변수

	#region 프로퍼티
	public Text NameText { get; private set; } = null;

	public Button SelBtn { get; private set; } = null;
	public Button MoveBtn { get; private set; } = null;
	public Button RemoveBtn { get; private set; } = null;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		this.NameText = this.gameObject.ExFindComponent<Text>(KCDefine.U_OBJ_N_NAME_TEXT);

		// 버튼을 설정한다 {
		this.SelBtn = this.GetComponentInChildren<Button>();
		this.SelBtn?.ExSetDisableColor(KCDefine.U_COLOR_NORM, false);
		this.SelBtn?.onClick.AddListener(this.OnTouchSelBtn);

		this.MoveBtn = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_MOVE_BTN);
		this.MoveBtn?.onClick.AddListener(this.OnTouchMoveBtn);

		this.RemoveBtn = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_REMOVE_BTN);
		this.RemoveBtn?.onClick.AddListener(this.OnTouchRemoveBtn);
		
		this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_COPY_BTN)?.onClick.AddListener(this.OnTouchCopyBtn);
		// 버튼을 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		base.Init(a_stParams.m_stBaseParams, a_stCallbackParams.m_stBaseCallbackParams);

		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;

		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// Do Something
	}

	/** 선택 버튼을 눌렀을 경우 */
	private void OnTouchSelBtn() {
		m_stCallbackParams.m_stBaseCallbackParams.m_oSelCallback?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}

	/** 복사 버튼을 눌렀을 경우 */
	private void OnTouchCopyBtn() {
		m_stCallbackParams.m_oCopyCallback?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}

	/** 이동 버튼을 눌렀을 경우 */
	private void OnTouchMoveBtn() {
		m_stCallbackParams.m_oMoveCallback?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}

	/** 제거 버튼을 눌렀을 경우 */
	private void OnTouchRemoveBtn() {
		m_stCallbackParams.m_oRemoveCallback?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
