using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//! 경고 팝업
public class CAlertPopup : CPopup {
	//! 매개 변수
	public struct STParams {
		public string m_oTitle;
		public string m_oMsg;
		public string m_oOKBtnText;
		public string m_oCancelBtnText;
	}

	#region 변수
	private STParams m_stParams;
	private System.Action<CAlertPopup, bool> m_oCallback = null;
	#endregion			// 변수

	#region UI 변수
	protected Text m_oTitleText = null;
	protected Text m_oMsgText = null;

	protected Image m_oAlertBGImg = null;

	protected Button m_oOKBtn = null;
	protected Button m_oCancelBtn = null;

	protected HorizontalLayoutGroup m_oBMUIsLayoutGroup = null;
	#endregion			// UI 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oTitleText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_TITLE_TEXT);
		m_oMsgText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_MSG_TEXT);

		// 이미지를 설정한다
		m_oAlertBGImg = m_oContents.ExFindComponent<Image>(KCDefine.U_OBJ_N_ALERT_P_BG_IMG);

		// 버튼을 설정한다 {
		m_oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_OK_BTN);
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);

		m_oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_CANCEL_BTN);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 레이아웃을 설정한다
		m_oBMUIsLayoutGroup = m_oContents.ExFindComponent<HorizontalLayoutGroup>(KCDefine.U_OBJ_N_BOTTOM_MENU_UIS);
	}

	//! 초기화
	public virtual void Init(STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback) {
		base.Init();

		m_stParams = a_stParams;
		m_oCallback = a_oCallback;
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();

		// 컨텐츠 간격을 설정한다 {
		float fContentsWidth = m_oTitleText.rectTransform.rect.width;
		fContentsWidth = Mathf.Max(fContentsWidth, m_oMsgText.rectTransform.rect.width);
		fContentsWidth = Mathf.Max(fContentsWidth, (m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.x);

		float fContentsOffsetVA = m_oTitleText.rectTransform.anchoredPosition.y - m_oMsgText.rectTransform.anchoredPosition.y;
		float fContentsOffsetVB = m_oMsgText.rectTransform.anchoredPosition.y - (m_oBMUIsLayoutGroup.transform as RectTransform).anchoredPosition.y;

		var oContentsOffsetVList = new List<float>() {
			(m_oAlertBGImg.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT) - m_oTitleText.rectTransform.anchoredPosition.y,
			(m_oAlertBGImg.rectTransform.sizeDelta.y / -KCDefine.B_VAL_2_FLT) - (m_oBMUIsLayoutGroup.transform as RectTransform).anchoredPosition.y,
			fContentsOffsetVA - (m_oTitleText.rectTransform.sizeDelta.y + (m_oMsgText.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT)),
			fContentsOffsetVB - ((m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.y + (m_oMsgText.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT))
		};
		// 컨텐츠 간격을 설정한다 }

		this.UpdateUIsState();
		this.ExLateCallFunc((a_oSender, a_oParams) => this.DoSetupContents(oContentsOffsetVList.Sum(), m_oAlertBGImg.rectTransform.sizeDelta.x - fContentsWidth));
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// 텍스트를 갱신한다 {
		m_oTitleText.text = m_stParams.m_oTitle;
		m_oMsgText.text = m_stParams.m_oMsg;

		m_oOKBtn.GetComponentInChildren<Text>().text = m_stParams.m_oOKBtnText;
		m_oCancelBtn.GetComponentInChildren<Text>().text = m_stParams.m_oCancelBtnText.ExIsValid() ? m_stParams.m_oCancelBtnText : string.Empty;
		// 텍스트를 갱신한다 }
	}

	//! 확인 버튼을 눌렀을 경우
	protected virtual void OnTouchOKBtn() {
		CFunc.Invoke(ref m_oCallback, this, true);
		this.OnTouchCloseBtn();
	}

	//! 취소 버튼을 눌렀을 경우
	protected virtual void OnTouchCancelBtn() {
		CFunc.Invoke(ref m_oCallback, this, false);
		this.OnTouchCloseBtn();
	}

	//! 팝업 컨텐츠를 설정한다
	private void DoSetupContents(float a_fContentsOffsetV, float a_fContentsOffsetH) {
		// 이미지를 설정한다 {
		var stContentsSize = new Vector3(m_oTitleText.rectTransform.sizeDelta.x, m_oTitleText.rectTransform.sizeDelta.y, KCDefine.B_VAL_0_FLT);
		stContentsSize.x = Mathf.Max(stContentsSize.x, m_oMsgText.rectTransform.sizeDelta.x);
		stContentsSize.x = Mathf.Max(stContentsSize.x, (m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.x);

		stContentsSize.y += m_oMsgText.rectTransform.sizeDelta.y;
		stContentsSize.y += (m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.y;

		m_oAlertBGImg.gameObject.ExSetSizeDeltaX(Mathf.Max(stContentsSize.x + a_fContentsOffsetH, KCDefine.U_MIN_SIZE_ALERT_POPUP.x));
		m_oAlertBGImg.gameObject.ExSetSizeDeltaY(Mathf.Max(stContentsSize.y + a_fContentsOffsetV, KCDefine.U_MIN_SIZE_ALERT_POPUP.y));
		// 이미지를 설정한다 }
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_stParams, a_oCallback, KCDefine.B_POS_POPUP);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		return CAlertPopup.Create<T>(a_oName, oObj, a_oParent, a_stParams, a_oCallback, a_stPos);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stParams, a_oCallback, KCDefine.B_POS_POPUP);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oAlertPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
		oAlertPopup?.Init(a_stParams, a_oCallback);
		
		return oAlertPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
