using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
	public partial struct STParams {
		public ulong m_nID;
		public EnhancedScroller m_oScroller;
		public Dictionary<ECallback, System.Action<CScrollerCellView, ulong>> m_oCallbackDict;
	}

	#region 변수
	/** =====> UI <===== */
	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>();

	/** =====> 객체 <===== */
	[SerializeField] private List<GameObject> m_oScrollerCellList = new List<GameObject>();
	#endregion			// 변수

	#region 프로퍼티
	public STParams Params { get; private set; }
	public Button SelBtn => m_oBtnDict.GetValueOrDefault(EKey.SEL_BTN);
	protected List<GameObject> ScrollerCellList => m_oScrollerCellList;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Awake() {
		// 버튼을 설정한다
		CFunc.SetupButtons(new List<(EKey, GameObject, UnityAction)>() {
			(EKey.SEL_BTN, this.gameObject, () => this.Params.m_oCallbackDict?.GetValueOrDefault(ECallback.SEL)?.Invoke(this, this.Params.m_nID))
		}, m_oBtnDict, false);
	}

	/** 초기화 */
	public virtual void Start() {
		// Do Something
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		this.Params = a_stParams;
	}
	#endregion			// 함수
}
