using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/** 팝업 */
public abstract class CPopup : CComponent {
	/** 팝업 콜백 */
	private enum EPopupCallback {
		NONE = -1,
		SHOW,
		CLOSE,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private Tween m_oBGAni = null;
	private Sequence m_oShowAni = null;
	private Sequence m_oCloseAni = null;
	private Dictionary<EPopupCallback, System.Action<CPopup>> m_oCallbackDict = new Dictionary<EPopupCallback, System.Action<CPopup>>();

	[SerializeField] private string m_oShowSndPath = "Sounds/Global/G_PopupShow";
	[SerializeField] private string m_oCloseSndPath = "Sounds/Global/G_PopupClose";

	/** =====> 객체 <===== */
	protected GameObject m_oContents = null;
	protected GameObject m_oContentsUIs = null;
	protected GameObject m_oTouchResponder = null;
	#endregion			// 변수

	#region 프로퍼티
	public bool IsShow { get; private set; } = false;
	public bool IsClose { get; private set; } = false;

	public virtual bool IsIgnoreBGAni => false;
	public virtual bool IsIgnoreCloseBtn => false;

	public virtual float ShowTimeScale => KCDefine.B_VAL_0_FLT;
	public virtual float CloseTimeScale => KCDefine.B_VAL_1_FLT;

	public virtual float ShowAniDelay => KCDefine.U_DELAY_POPUP_SHOW_ANI;

	public virtual EAniType AniType => EAniType.DROPDOWN;
	public virtual Color BGColor => KCDefine.U_COLOR_POPUP_BG;
	protected virtual Image BGImg => m_oTouchResponder?.GetComponentInChildren<Image>();

	private bool IsEnableShow => !this.IsShow && !this.IsClose;
	private bool IsEnableClose => this.IsShow && !this.IsClose;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CNavStackManager.Inst.AddComponent(this);

		m_oContents = this.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);
		m_oContentsUIs = this.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS_UIS);

		// 터치 응답자를 설정한다
		m_oTouchResponder = this.CreateTouchResponder();
		m_oTouchResponder?.transform.SetAsFirstSibling();

		// 버튼을 설정한다
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CLOSE_BTN)?.onClick.AddListener(this.OnTouchCloseBtn);
	}

	/** 초기화 */
	public virtual void Init() {
		switch(this.AniType) {
			case EAniType.SCALE: {
				m_oContents.transform.localScale = new Vector3(KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_MIN_SCALE_POPUP);
				m_oContents.transform.localPosition = Vector3.zero;
			} break;
			case EAniType.DROPDOWN: case EAniType.SLIDE_LEFT: case EAniType.SLIDE_RIGHT: {
				m_oContents.transform.localScale = Vector3.one;

				// 낙하 애니메이션 일 경우
				if(this.AniType == EAniType.DROPDOWN) {
					m_oContents.transform.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, CSceneManager.CanvasSize.y, KCDefine.B_VAL_0_FLT);
				} else {
					m_oContents.transform.localPosition = new Vector3((this.AniType == EAniType.SLIDE_LEFT) ? CSceneManager.CanvasSize.x : -CSceneManager.CanvasSize.x, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
				}
			} break;
		}

		this.BGImg.color = this.BGColor.ExGetAlphaColor(KCDefine.B_VAL_0_FLT);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
				this.ResetAni();
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CPopup.OnDestroy Exception: {oException.Message}");
		}
	}

	/** 애니메이션을 리셋한다 */
	public virtual void ResetAni() {
		m_oBGAni?.Kill();
		m_oShowAni?.Kill();
		m_oCloseAni?.Kill();
	}

	/** 출력 사운드 경로를 변경한다 */
	public void SetShowSndPath(string a_oSndPath) {
		m_oShowSndPath = a_oSndPath;	
	}

	/** 닫힘 사운드 경로를 변경한다 */
	public void SetCloseSndPath(string a_oSndPath) {
		m_oCloseSndPath = a_oSndPath;
	}

	/** 배경 색상을 변경한다 */
	public void SetBGColor(Color a_stColor, bool a_bIsAni = true, float a_fDuration = KCDefine.U_DURATION_ANI) {
		m_oBGAni?.Kill();
		
		// 애니메이션 모드 일 경우
		if(a_bIsAni && !this.IsIgnoreAni) {
			m_oBGAni = this.BGImg.DOColor(a_stColor, a_fDuration).SetUpdate(true);
		} else {
			this.BGImg.color = a_stColor;
		}
	}

	/** 내비게이션 스택 이벤트를 수신했을 경우 */
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);

		// 상단 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.TOP) {
			Time.timeScale = this.ShowTimeScale;
		}
		// 백 키 눌림 이벤트 일 경우
		else if(!this.IsIgnoreNavStackEvent && a_eEvent == ENavStackEvent.REMOVE) {
			this.Close();
		}
	}

	/** 팝업을 출력한다 */
	public virtual void Show(System.Action<CPopup> a_oShowCallback, System.Action<CPopup> a_oCloseCallback) {
		// 객체가 존재 할 경우
		if(!this.IsDestroy && this.IsEnableShow) {
			this.IsShow = true;
			CSndManager.Inst.PlayFXSnd(m_oShowSndPath);

			m_oCallbackDict.ExReplaceVal(EPopupCallback.SHOW, a_oShowCallback);
			m_oCallbackDict.ExReplaceVal(EPopupCallback.CLOSE, a_oCloseCallback);

			this.ExLateCallFunc(this.DoShow, Mathf.Max(this.ShowAniDelay, KCDefine.B_DELTA_T_INTERMEDIATE), true);
		}
	}

	/** 팝업을 닫는다 */
	public virtual void Close() {
		// 출력 상태 일 경우
		if(!this.IsDestroy && this.IsEnableClose) {
			this.IsClose = true;
			CSndManager.Inst.PlayFXSnd(m_oCloseSndPath);

			this.StartCloseAni();

			// 내비게이션 스택 콜백이 유효 할 경우
			if(this.NavStackCallback != null) {
				this.NavStackCallback = null;
				CNavStackManager.Inst.RemoveComponent(this);
			}
		}
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected virtual void SetupContents() {
		// 컴포넌트를 설정한다
		m_oContents.ExEnumerateComponents<RectTransform>((a_nTrans) => { LayoutRebuilder.ForceRebuildLayoutImmediate(a_nTrans); return true; });
	}

	/** 팝업이 출력 되었을 경우 */
	protected virtual void OnShow() {
		// Do Something
	}

	/** 팝업이 닫혔을 경우 */
	protected virtual void OnClose() {
		// Do Something
	}

	/** 출력 애니메이션이 완료 되었을 경우 */
	protected virtual void OnCompleteShowAni() {
		this.OnShow();
		m_oCallbackDict.GetValueOrDefault(EPopupCallback.SHOW)?.Invoke(this);
		
		m_oContents.transform.localPosition = Vector3.zero;
	}

	/** 닫기 애니메이션이 완료 되었을 경우 */
	protected virtual void OnCompleteCloseAni() {
		this.OnClose();
		m_oCallbackDict.GetValueOrDefault(EPopupCallback.CLOSE)?.Invoke(this);

		Destroy(this.gameObject);
	}

	/** 닫기 버튼을 눌렀을 경우 */
	protected virtual void OnTouchCloseBtn() {
		// 닫기 버튼 처리가 가능 할 경우
		if(!this.IsIgnoreCloseBtn) {
			this.Close();
		}
	}

	/** 출력 애니메이션을 생성한다 */
	protected virtual Tween MakeShowAni() {
		switch(this.AniType) {
			case EAniType.SCALE: return m_oContents.transform.DOScale(KCDefine.U_SCALE_POPUP, KCDefine.U_DURATION_POPUP_SCALE_ANI).SetEase(Ease.OutBack);
			case EAniType.DROPDOWN: return m_oContents.transform.DOLocalMoveY(KCDefine.B_VAL_0_FLT, KCDefine.U_DURATION_POPUP_DROPDOWN_ANI).SetEase(Ease.OutBack);
			case EAniType.SLIDE_LEFT: case EAniType.SLIDE_RIGHT: return m_oContents.transform.DOLocalMoveX(KCDefine.B_VAL_0_FLT, KCDefine.U_DURATION_POPUP_SLIDE_ANI).SetEase(Ease.OutBack);
		}

		return null;
	}

	/** 닫기 애니메이션을 생성한다 */
	protected virtual Tween MakeCloseAni() {
		switch(this.AniType) {
			case EAniType.SCALE: return m_oContents.transform.DOScale(KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_DURATION_POPUP_SCALE_ANI).SetEase(Ease.InBack);
			case EAniType.DROPDOWN: return m_oContents.transform.DOLocalMoveY(CSceneManager.CanvasSize.y, KCDefine.U_DURATION_POPUP_DROPDOWN_ANI).SetEase(Ease.InBack);
			case EAniType.SLIDE_LEFT: case EAniType.SLIDE_RIGHT: return m_oContents.transform.DOLocalMoveX((this.AniType == EAniType.SLIDE_LEFT) ? -CSceneManager.CanvasSize.x : CSceneManager.CanvasSize.x, KCDefine.U_DURATION_POPUP_SLIDE_ANI).SetEase(Ease.InBack);
		}

		return null;
	}

	/** 터치 응답자를 생성한다 */
	protected virtual GameObject CreateTouchResponder() {
		string oName = string.Format(KCDefine.U_OBJ_N_FMT_POPUP_TOUCH_RESPONDER, this.gameObject.name);
		return CFactory.CreateTouchResponder(oName, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER, this.gameObject, CSceneManager.CanvasSize, Vector3.zero, KCDefine.U_COLOR_TRANSPARENT);
	}

	/** 팝업을 출력한다 */
	private void DoShow(MonoBehaviour a_oSender) {
		// 출력 가능 할 경우
		if(!this.IsDestroy && !this.IsClose) {
			this.SetupContents();
			this.StartShowAni();
			this.SetBGColor(this.BGColor, !this.IsIgnoreBGAni);
		}
	}

	/** 출력 애니메이션을 시작한다 */
	private void StartShowAni() {
		this.ResetAni();
		Time.timeScale = this.ShowTimeScale;

		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && this.AniType != EAniType.NONE) {
			m_oShowAni = CFactory.MakeSequence(this.MakeShowAni(), (a_oSender) => this.OnCompleteShowAni(), KCDefine.B_VAL_0_FLT, false, true);
		} else {
			this.ExLateCallFunc((a_oSender) => this.OnCompleteShowAni());
		}
	}

	/** 닫기 애니메이션을 시작한다 */
	private void StartCloseAni() {
		this.ResetAni();
		Time.timeScale = this.CloseTimeScale;

		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && this.AniType != EAniType.NONE) {
			m_oCloseAni = CFactory.MakeSequence(this.MakeCloseAni(), (a_oSender) => this.OnCompleteCloseAni(), KCDefine.B_VAL_0_FLT, false, true);
		} else {
			this.ExLateCallFunc((a_oSender) => this.OnCompleteCloseAni());
		}
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	/** 팝업을 생성한다 */
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent) where T : CPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CPopup.Create<T>(a_oName, a_oObjPath, a_oParent, KCDefine.B_POS_POPUP);
	}

	/** 팝업을 생성한다 */
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos) where T : CPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		return CPopup.Create<T>(a_oName, oObj, a_oParent, a_stPos);
	}

	/** 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent) where T : CPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, KCDefine.B_POS_POPUP);
	}

	/** 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos) where T : CPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
	}
	#endregion			// 제네릭 클래스 함수
}
