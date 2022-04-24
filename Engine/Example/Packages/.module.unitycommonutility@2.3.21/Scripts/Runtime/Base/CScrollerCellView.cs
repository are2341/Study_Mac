using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

/** 스크롤러 셀 뷰 */
public abstract partial class CScrollerCellView : EnhancedScrollerCellView {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		SEL_BTN,
		[HideInInspector] MAX_VAL
	}

	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		SEL,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public long m_nID;
		public EnhancedScroller m_oScroller;
		public Dictionary<ECallback, System.Action<CScrollerCellView, long>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;

	/** =====> UI <===== */
	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>() {
		[EKey.SEL_BTN] = null
	};

	/** =====> 객체 <===== */
	[SerializeField] private List<GameObject> m_oScrollerCellList = new List<GameObject>();
	#endregion			// 변수

	#region 프로퍼티
	public Button SelBtn => m_oBtnDict[EKey.SEL_BTN];
	public EnhancedScroller Scroller => m_stParams.m_oScroller;
	protected List<GameObject> ScrollerCellList => m_oScrollerCellList;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Awake() {
		// 버튼을 설정한다
		m_oBtnDict[EKey.SEL_BTN] = this.GetComponentInChildren<Button>();
		m_oBtnDict[EKey.SEL_BTN]?.onClick.AddListener(() => m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.SEL)?.Invoke(this, m_stParams.m_nID));
	}

	/** 초기화 */
	public virtual void Start() {
		// Do Something
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		m_stParams = a_stParams;
	}
	#endregion			// 함수
}
