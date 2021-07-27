using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//! 팝업
public abstract class CPopup : CUIsComponent {
	#region 변수
	[SerializeField] private string m_oShowSndPath = "Sounds/Global/G_PopupShow";
	[SerializeField] private string m_oCloseSndPath = "Sounds/Global/G_PopupClose";

	protected Sequence m_oShowAni = null;
	protected Sequence m_oCloseAni = null;

	protected Tween m_oBGAni = null;
	protected Image m_oBGImg = null;
	protected Button m_oCloseBtn = null;
	protected RectTransform m_oContentsTrans = null;

	protected System.Action<CPopup> m_oShowCallback = null;
	protected System.Action<CPopup> m_oCloseCallback = null;
	#endregion			// 변수

	#region 객체
	protected GameObject m_oContents = null;
	protected GameObject m_oTouchResponder = null;
	#endregion			// 객체

	#region 프로퍼티
	public bool IsShow { get; private set; } = false;
	public bool IsClose { get; private set; } = false;

	public virtual bool IsIgnoreBGAni => false;
	public virtual bool IsIgnoreCloseBtn => false;

	public virtual float ShowTimeScale => KCDefine.B_VAL_0_FLT;
	public virtual float CloseTimeScale => KCDefine.B_VAL_1_FLT;

	public virtual float ShowAniDelay => KCDefine.U_DELAY_POPUP_SHOW_ANI;

	public virtual Color BGColor => KCDefine.U_COLOR_POPUP_BG;
	public virtual EAniType AniType => EAniType.DROPDOWN;

	private bool IsEnableShow => !this.IsShow && !this.IsClose;
	private bool IsEnableClose => this.IsShow && !this.IsClose;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		CNavStackManager.Inst.AddComponent(this);

		// 컨텐츠를 설정한다
		m_oContents = this.gameObject.ExFindChild(KCDefine.U_OBJ_N_POPUP_CONTENTS);
		m_oContentsTrans = m_oContents.transform as RectTransform;
		
		// 터치 응답자를 설정한다 {
		m_oTouchResponder = this.CreateTouchResponder();

		// 터치 응답자가 존재 할 경우
		if(m_oTouchResponder != null) {
			m_oTouchResponder.transform.SetAsFirstSibling();
		}
		// 터치 응답자를 설정한다 }

		// 이미지를 설정한다
		m_oBGImg = this.CreateBGImg();

		// 버튼을 설정한다
		m_oCloseBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_POPUP_CLOSE_BTN);
		m_oCloseBtn?.onClick.AddListener(this.OnTouchCloseBtn);
	}

	//! 초기화
	public virtual void Init() {
		// 배경 이미지가 존재 할 경우
		if(m_oBGImg != null) {
			m_oBGImg.color = this.BGColor.ExGetAlphaColor(KCDefine.B_VAL_0_FLT);
		}
		
		// 비율 애니메이션 일 경우
		if(this.AniType == EAniType.SCALE) {
			m_oContentsTrans.localScale = new Vector3(KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_MIN_SCALE_POPUP);
			m_oContentsTrans.localPosition = Vector3.zero;
		} else {
			m_oContentsTrans.localScale = Vector3.one;

			// 낙하 애니메이션 일 경우
			if(this.AniType == EAniType.DROPDOWN) {
				m_oContentsTrans.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, CSceneManager.CanvasSize.y, KCDefine.B_VAL_0_FLT);
			}
			// 슬라이드 애니메이션 일 경우
			else if(this.AniType == EAniType.SLIDE_LEFT || this.AniType == EAniType.SLIDE_RIGHT) {
				float fPosX = (this.AniType == EAniType.SLIDE_LEFT) ? CSceneManager.CanvasSize.x : -CSceneManager.CanvasSize.x;
				m_oContentsTrans.localPosition = new Vector3(fPosX, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
			}
		}
	}

	//! 제거 되었을 경우
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			this.ResetAni();
		}
	}

	//! 애니메이션을 리셋한다
	public virtual void ResetAni() {
		m_oBGAni?.Kill();
		m_oShowAni?.Kill();
		m_oCloseAni?.Kill();
	}

	//! 출력 사운드 경로를 변경한다
	public void SetShowSndPath(string a_oSndPath) {
		m_oShowSndPath = a_oSndPath;	
	}

	//! 닫힘 사운드 경로를 변경한다
	public void SetCloseSndPath(string a_oSndPath) {
		m_oCloseSndPath = a_oSndPath;
	}

	//! 내비게이션 스택 이벤트를 수신했을 경우
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

	//! 배경 색상을 변경한다
	public void SetBGColor(Color a_stColor, bool a_bIsAni = true) {
		CAccess.Assert(m_oBGImg != null);
		m_oBGAni?.Kill();
		
#if DOTWEEN_ENABLE
		// 애니메이션 모드 일 경우
		if(a_bIsAni && !this.IsIgnoreAni) {
			m_oBGAni = m_oBGImg.DOColor(a_stColor, KCDefine.U_DURATION_ANI);
			m_oBGAni.SetUpdate(true);
		} else {
			m_oBGImg.color = a_stColor;
		}
#else
		m_oBGImg.color = a_stColor;
#endif			// #if DOTWEEN_ENABLE
	}

	//! 팝업을 출력한다
	public virtual void Show(System.Action<CPopup> a_oShowCallback, System.Action<CPopup> a_oCloseCallback) {
		// 객체가 존재 할 경우
		if(!this.IsDestroy && this.IsEnableShow) {
			this.IsShow = true;

			m_oShowCallback = a_oShowCallback;
			m_oCloseCallback = a_oCloseCallback;

			CSndManager.Inst.PlayFXSnd(m_oShowSndPath);

			// 딜레이가 없을 경우
			if(this.ShowAniDelay.ExIsLessEquals(KCDefine.B_DELTA_T_INTERMEDIATE)) {
				this.ExLateCallFunc(this.DoShow);
			} else {
				this.ExLateCallFunc(this.DoShow, this.ShowAniDelay, true);
			}
		}
	}

	//! 팝업을 닫는다
	public virtual void Close() {
		// 출력 상태 일 경우
		if(!this.IsDestroy && this.IsEnableClose) {
			this.IsClose = true;

			this.StartCloseAni();
			CSndManager.Inst.PlayFXSnd(m_oCloseSndPath);

			// 내비게이션 스택 콜백이 유효 할 경우
			if(this.NavStackCallback != null) {
				this.NavStackCallback = null;
				CNavStackManager.Inst.RemoveComponent(this);
			}
		}
	}

	//! 팝업 컨텐츠를 설정한다
	protected virtual void SetupContents() {
		// Do Nothing
	}

	//! 팝업이 출력 되었을 경우
	protected virtual void OnShow() {
		// Do Nothing
	}

	//! 팝업이 닫혔을 경우
	protected virtual void OnClose() {
		// Do Nothing
	}

	//! 출력 애니메이션이 완료 되었을 경우
	protected virtual void OnCompleteShowAni() {
		this.OnShow();
		CFunc.Invoke(ref m_oShowCallback, this);

		m_oContentsTrans.localPosition = Vector3.zero;
	}

	//! 닫기 애니메이션이 완료 되었을 경우
	protected virtual void OnCompleteCloseAni() {
		this.OnClose();
		CFunc.Invoke(ref m_oCloseCallback, this);

		Destroy(this.gameObject);
	}

	//! 닫기 버튼을 눌렀을 경우
	protected virtual void OnTouchCloseBtn() {
		// 닫기 버튼 처리가 가능 할 경우
		if(!this.IsIgnoreCloseBtn) {
			this.Close();
		}
	}

	//! 배경 이미지를 생성한다
	protected virtual Image CreateBGImg() {
		CAccess.Assert(m_oTouchResponder != null);
		return m_oTouchResponder.GetComponentInChildren<Image>();
	}

	//! 출력 애니메이션을 생성한다
	protected virtual Sequence CreateShowAni() {
#if DOTWEEN_ENABLE
		Tween oAni = null;

		// 비율 애니메이션 일 경우
		if(this.AniType == EAniType.SCALE) {
			oAni = m_oContentsTrans.DOScale(KCDefine.U_SCALE_POPUP, KCDefine.U_DURATION_POPUP_SCALE_ANI);
		} 
		// 낙하 애니메이션 일 경우
		else if(this.AniType == EAniType.DROPDOWN) {
			oAni = m_oContentsTrans.DOLocalMoveY(KCDefine.B_VAL_0_FLT, KCDefine.U_DURATION_POPUP_DROPDOWN_ANI);
		}
		// 슬라이드 애니메이션 일 경우
		else if(this.AniType == EAniType.SLIDE_LEFT || this.AniType == EAniType.SLIDE_RIGHT) {
			oAni = m_oContentsTrans.DOLocalMoveX(KCDefine.B_VAL_0_FLT, KCDefine.U_DURATION_POPUP_SLIDE_ANI);
		}

		oAni.SetEase(Ease.OutBack);
		return DOTween.Sequence().Append(oAni);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}

	//! 닫기 애니메이션을 생성한다
	protected virtual Sequence CreateCloseAni() {
#if DOTWEEN_ENABLE
		Tween oAni = null;

		// 비율 애니메이션 일 경우
		if(this.AniType == EAniType.SCALE) {
			oAni = m_oContentsTrans.DOScale(KCDefine.U_MIN_SCALE_POPUP, KCDefine.U_DURATION_POPUP_SCALE_ANI);
		} 
		// 낙하 애니메이션 일 경우
		else if(this.AniType == EAniType.DROPDOWN) {
			oAni = m_oContentsTrans.DOLocalMoveY(CSceneManager.CanvasSize.y, KCDefine.U_DURATION_POPUP_DROPDOWN_ANI);
		} 
		// 슬라이드 애니메이션 일 경우
		else if(this.AniType == EAniType.SLIDE_LEFT || this.AniType == EAniType.SLIDE_RIGHT) {
			float fPosX = (this.AniType == EAniType.SLIDE_LEFT) ? -CSceneManager.CanvasSize.x : CSceneManager.CanvasSize.x;
			oAni = m_oContentsTrans.DOLocalMoveX(fPosX, KCDefine.U_DURATION_POPUP_SLIDE_ANI);
		}
		
		oAni.SetEase(Ease.InBack);
		return DOTween.Sequence().Append(oAni);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}

	//! 터치 응답자를 생성한다
	protected virtual GameObject CreateTouchResponder() {
		string oName = string.Format(KCDefine.U_OBJ_N_FMT_POPUP_TOUCH_RESPONDER, this.gameObject.name);
		return CFactory.CreateTouchResponder(oName, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER, this.gameObject, CSceneManager.CanvasSize, Vector3.zero.ExToLocal(this.gameObject), KCDefine.U_COLOR_TRANSPARENT);
	}

	//! 팝업을 출력한다
	private void DoShow(MonoBehaviour a_oSender, object[] a_oParams) {
		// 출력 가능 할 경우
		if(!this.IsDestroy && !this.IsClose) {
			this.SetupContents();

			this.StartShowAni();
			this.SetBGColor(this.BGColor, !this.IsIgnoreBGAni);
		}
	}

	//! 출력 애니메이션을 시작한다
	private void StartShowAni() {
		this.ResetAni();
		Time.timeScale = this.ShowTimeScale;

		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && this.AniType != EAniType.NONE) {
			m_oShowAni = this.CreateShowAni();

			// 애니메이션이 유효 할 경우
			if(m_oShowAni != null) {
				m_oShowAni.SetUpdate(true);
				m_oShowAni.AppendCallback(this.OnCompleteShowAni);
			}
		} else {
			this.ExLateCallFunc((a_oSender, a_oParams) => this.OnCompleteShowAni());
		}
	}

	//! 닫기 애니메이션을 시작한다
	private void StartCloseAni() {
		this.ResetAni();
		Time.timeScale = this.CloseTimeScale;

		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && this.AniType != EAniType.NONE) {
			m_oCloseAni = this.CreateCloseAni();

			// 애니메이션이 유효 할 경우
			if(m_oCloseAni != null) {
				m_oCloseAni.SetUpdate(true);
				m_oCloseAni.AppendCallback(this.OnCompleteCloseAni);
			}
		} else {
			this.ExLateCallFunc((a_oSender, a_oParams) => this.OnCompleteCloseAni());
		}
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	//! 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent) where T : CPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CPopup.Create<T>(a_oName, a_oObjPath, a_oParent, KCDefine.B_POS_POPUP);
	}

	//! 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos) where T : CPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		return CPopup.Create<T>(a_oName, oObj, a_oParent, a_stPos);
	}

	//! 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent) where T : CPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, KCDefine.B_POS_POPUP);
	}

	//! 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos) where T : CPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
	}
	#endregion			// 제네릭 클래스 함수
}
