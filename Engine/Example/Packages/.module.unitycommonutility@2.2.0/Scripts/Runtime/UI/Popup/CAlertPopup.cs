using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 경고 팝업 */
public class CAlertPopup : CPopup {
	/** 문자열 */
	public enum EStr {
		NONE = -1,
		TITLE,
		MSG,
		OK_BTN,
		CANCEL_BTN,
		[HideInInspector] MAX_VAL
	}

	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		OK_CANCEL,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public Dictionary<EStr, string> m_oStrDict;
		public Dictionary<ECallback, System.Action<CAlertPopup, bool>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;

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
		m_oTitleText = this.Contents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_TITLE_TEXT);
		m_oMsgText = this.Contents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_MSG_TEXT);

		// 이미지를 설정한다
		m_oAlertBGImg = this.Contents.ExFindComponent<Image>(KCDefine.U_OBJ_N_BG_IMG);

		// 버튼을 설정한다 {
		m_oOKBtn = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN);
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);

		m_oCancelBtn = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 레이아웃을 설정한다
		m_oBMUIsLayoutGroup = this.Contents.ExFindComponent<HorizontalLayoutGroup>(KCDefine.U_OBJ_N_BOTTOM_MENU_UIS);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		m_oCancelBtn.gameObject.SetActive(m_stParams.m_oStrDict.ContainsKey(EStr.CANCEL_BTN));

		// 컨텐츠 간격을 설정한다 {
		float fContentsWidth = m_oTitleText.rectTransform.sizeDelta.x;
		fContentsWidth = Mathf.Max(fContentsWidth, m_oMsgText.rectTransform.sizeDelta.x);
		fContentsWidth = Mathf.Max(fContentsWidth, (m_oBMUIsLayoutGroup.transform as RectTransform).sizeDelta.x);

		float fMinOffsetY = Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMin.y);
		float fMaxOffsetY = Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMax.y);

		float fContentsOffsetVA = ((m_oAlertBGImg.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT) - fMaxOffsetY) - m_oMsgText.rectTransform.anchoredPosition.y;
		float fContentsOffsetVB = ((m_oAlertBGImg.rectTransform.sizeDelta.y / KCDefine.B_VAL_2_FLT) - fMinOffsetY) - m_oMsgText.rectTransform.anchoredPosition.y;

		var oContentsOffsetVList = new List<float>() {
			Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMin.y),
			Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMax.y),
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
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		m_oTitleText.ExSetText(m_stParams.m_oStrDict[EStr.TITLE], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
		m_oMsgText.ExSetText(m_stParams.m_oStrDict[EStr.MSG], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));

		m_oOKBtn.GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oStrDict[EStr.OK_BTN], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
		m_oCancelBtn.GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oStrDict.GetValueOrDefault(EStr.CANCEL_BTN, string.Empty), CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
		// 텍스트를 갱신한다 }
	}

	/** 확인 버튼을 눌렀을 경우 */
	protected virtual void OnTouchOKBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, true);
		this.OnTouchCloseBtn();
	}

	/** 취소 버튼을 눌렀을 경우 */
	protected virtual void OnTouchCancelBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, false);
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
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_stParams, KCDefine.B_POS_POPUP);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, CResManager.Inst.GetRes<GameObject>(a_oObjPath), a_oParent, a_stParams, a_stPos);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stParams, KCDefine.B_POS_POPUP);
	}

	/** 경고 팝업을 생성한다 */
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oAlertPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
		oAlertPopup?.Init(a_stParams);
		
		return oAlertPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
