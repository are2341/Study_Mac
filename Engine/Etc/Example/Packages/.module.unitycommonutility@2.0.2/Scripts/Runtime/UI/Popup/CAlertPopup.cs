using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 경고 팝업 */
public class CAlertPopup : CPopup {
	/** 매개 변수 */
	public struct STParams {
		public string m_oTitle;
		public string m_oMsg;
		public string m_oOKBtnText;
		public string m_oCancelBtnText;
	}

	/** 콜백 매개 변수 */
	public struct STCallbackParams {
		public System.Action<CAlertPopup, bool> m_oCallback;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;

	/** =====> UI <===== */
	protected TMP_Text m_oTitleText = null;
	protected TMP_Text m_oMsgText = null;

	protected Image m_oAlertBGImg = null;

	protected Button m_oOKBtn = null;
	protected Button m_oCancelBtn = null;

	protected HorizontalLayoutGroup m_oBMUIsLayoutGroup = null;
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oTitleText = m_oContents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_TITLE_TEXT);
		m_oMsgText = m_oContents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_MSG_TEXT);

		// 이미지를 설정한다
		m_oAlertBGImg = m_oContents.ExFindComponent<Image>(KCDefine.U_OBJ_N_BG_IMG);

		// 버튼을 설정한다 {
		m_oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN);
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);

		m_oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 레이아웃을 설정한다
		m_oBMUIsLayoutGroup = m_oContents.ExFindComponent<HorizontalLayoutGroup>(KCDefine.U_OBJ_N_BOTTOM_MENU_UIS);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		base.Init();

		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		m_oCancelBtn.gameObject.SetActive(m_stParams.m_oCancelBtnText.ExIsValid());

		// 컨텐츠 간격을 설정한다 {
		float fContentsWidth = m_oTitleText.rectTransform.sizeDelta.x;
		fContentsWidth = Mathf.Max(fContentsWidth, m_oMsgText.rectTransform.sizeDelta.x);
		fContentsWidth = Mathf.Max(fContentsWidth, (m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.x);

		float fMinOffsetY = Mathf.Abs((m_oContentsUIs.transform as RectTransform).offsetMin.y);
		float fMaxOffsetY = Mathf.Abs((m_oContentsUIs.transform as RectTransform).offsetMax.y);

		float fContentsOffsetVA = ((m_oAlertBGImg.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT) - fMaxOffsetY) - m_oMsgText.rectTransform.anchoredPosition.y;
		float fContentsOffsetVB = ((m_oAlertBGImg.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT) - fMinOffsetY) - m_oMsgText.rectTransform.anchoredPosition.y;

		var oContentsOffsetVList = new List<float>() {
			Mathf.Abs((m_oContentsUIs.transform as RectTransform).offsetMin.y),
			Mathf.Abs((m_oContentsUIs.transform as RectTransform).offsetMax.y),
			fContentsOffsetVA - (m_oTitleText.rectTransform.sizeDelta.y + (m_oMsgText.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT)),
			fContentsOffsetVB - ((m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.y + (m_oMsgText.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT))
		};
		// 컨텐츠 간격을 설정한다 }
		
		this.UpdateUIsState();
		this.ExLateCallFunc((a_oSender) => this.DoSetupContents(oContentsOffsetVList.Sum(), m_oAlertBGImg.rectTransform.sizeDelta.x - fContentsWidth));
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// 텍스트를 갱신한다 {
		m_oTitleText.ExSetText(m_stParams.m_oTitle, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
		m_oMsgText.ExSetText(m_stParams.m_oMsg, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
		
		m_oOKBtn.GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oOKBtnText, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
		m_oCancelBtn.GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oCancelBtnText.ExIsValid() ? m_stParams.m_oCancelBtnText : string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
		// 텍스트를 갱신한다 }
	}

	/** 확인 버튼을 눌렀을 경우 */
	protected virtual void OnTouchOKBtn() {
		CFunc.Invoke(ref m_stCallbackParams.m_oCallback, this, true);
		this.OnTouchCloseBtn();
	}

	/** 취소 버튼을 눌렀을 경우 */
	protected virtual void OnTouchCancelBtn() {
		CFunc.Invoke(ref m_stCallbackParams.m_oCallback, this, false);
		this.OnTouchCloseBtn();
	}

	/** 팝업 컨텐츠를 설정한다 */
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
	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, STCallbackParams a_stCallbackParams) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_stParams, a_stCallbackParams, KCDefine.B_POS_POPUP);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, STCallbackParams a_stCallbackParams, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, CResManager.Inst.GetRes<GameObject>(a_oObjPath), a_oParent, a_stParams, a_stCallbackParams, a_stPos);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, STCallbackParams a_stCallbackParams) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stParams, a_stCallbackParams, KCDefine.B_POS_POPUP);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, STCallbackParams a_stCallbackParams, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oAlertPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
		oAlertPopup?.Init(a_stParams, a_stCallbackParams);
		
		return oAlertPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
