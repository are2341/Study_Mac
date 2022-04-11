using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 에디터 스크롤러 셀 뷰 */
public partial class CEditorScrollerCellView : CScrollerCellView {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		NAME_TEXT,
		MOVE_BTN,
		REMOVE_BTN,
		[HideInInspector] MAX_VAL
	}

	/** 콜백 */
	public new enum ECallback {
		NONE = -1,
		COPY,
		MOVE,
		REMOVE,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public new struct STParams {
		public CScrollerCellView.STParams m_stBaseParams;
		public Dictionary<ECallback, System.Action<CEditorScrollerCellView, long>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;

	/** =====> UI <===== */
	private Dictionary<EKey, Text> m_oTextDict = new Dictionary<EKey, Text>() {
		[EKey.NAME_TEXT] = null
	};

	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>() {
		[EKey.MOVE_BTN] = null,
		[EKey.REMOVE_BTN] = null
	};
	#endregion			// 변수

	#region 프로퍼티
	public Text NameText => m_oTextDict[EKey.NAME_TEXT];

	public Button MoveBtn => m_oBtnDict[EKey.MOVE_BTN];
	public Button RemoveBtn => m_oBtnDict[EKey.REMOVE_BTN];
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oTextDict[EKey.NAME_TEXT] = this.gameObject.ExFindComponent<Text>(KCDefine.U_OBJ_N_NAME_TEXT);

		// 버튼을 설정한다 {
		m_oBtnDict[EKey.MOVE_BTN] = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_MOVE_BTN);
		m_oBtnDict[EKey.MOVE_BTN]?.onClick.AddListener(this.OnTouchMoveBtn);

		m_oBtnDict[EKey.REMOVE_BTN] = this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_REMOVE_BTN);
		m_oBtnDict[EKey.REMOVE_BTN]?.onClick.AddListener(this.OnTouchRemoveBtn);
		
		this.gameObject.ExFindComponent<Button>(KCDefine.E_OBJ_N_ESCV_COPY_BTN)?.onClick.AddListener(this.OnTouchCopyBtn);
		// 버튼을 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init(a_stParams.m_stBaseParams);
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// Do Something
	}

	/** 복사 버튼을 눌렀을 경우 */
	private void OnTouchCopyBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.COPY)?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}

	/** 이동 버튼을 눌렀을 경우 */
	private void OnTouchMoveBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.MOVE)?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}

	/** 제거 버튼을 눌렀을 경우 */
	private void OnTouchRemoveBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.REMOVE)?.Invoke(this, m_stParams.m_stBaseParams.m_nID);
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
