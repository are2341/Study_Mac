using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 경고 팝업
public class CAlertPopup : CPopup {
	#region 변수
	protected System.Action<CAlertPopup, bool> m_oCallback = null;
	#endregion			// 변수

	#region UI 변수
	protected Text m_oTitleText = null;
	protected Text m_oMsgText = null;
	protected Text m_oOKBtnText = null;
	protected Text m_oCancelBtnText = null;

	protected Image m_oBGImg = null;

	protected Button m_oOKBtn = null;
	protected Button m_oCancelBtn = null;
	#endregion			// UI 변수

	#region 프로퍼티
	public Vector2 MinBGSize { get; set; } = Vector2.zero;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public virtual void Init(Dictionary<string, string> a_oDataList, System.Action<CAlertPopup, bool> a_oCallback) {
		m_oCallback = a_oCallback;

		// 버튼을 설정한다 {
		m_oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_OK_BTN);
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);

		m_oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_CANCEL_BTN);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 텍스트를 설정한다 {
		bool bIsContains = a_oDataList.ContainsKey(KCDefine.U_KEY_ALERT_P_CANCEL_BTN_TEXT);
		string oCancelBtnString = bIsContains ? a_oDataList[KCDefine.U_KEY_ALERT_P_CANCEL_BTN_TEXT] : string.Empty;

		m_oTitleText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_TITLE_TEXT);
		m_oTitleText.text = a_oDataList[KCDefine.U_KEY_ALERT_P_TITLE];

		m_oMsgText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_MSG_TEXT);
		m_oMsgText.text = a_oDataList[KCDefine.U_KEY_ALERT_P_MSG];

		m_oOKBtnText = m_oOKBtn.GetComponentInChildren<Text>();
		m_oOKBtnText.text = a_oDataList[KCDefine.U_KEY_ALERT_P_OK_BTN_TEXT];

		m_oCancelBtnText = m_oCancelBtn.GetComponentInChildren<Text>();
		m_oCancelBtnText.text = oCancelBtnString;
		// 텍스트를 설정한다 }

		// 이미지를 설정한다
		m_oBGImg = m_oContents.ExFindComponent<Image>(KCDefine.U_OBJ_N_ALERT_P_BG_IMG);
	}

	//! 확인 버튼을 눌렀을 경우
	protected virtual void OnTouchOKBtn() {
		CFunc.Invoke(ref m_oCallback, this, true);
		this.Close();
	}

	//! 취소 버튼을 눌렀을 경우
	protected virtual void OnTouchCancelBtn() {
		CFunc.Invoke(ref m_oCallback, this, false);
		this.Close();
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		
		var oBGImgTrans = m_oBGImg.rectTransform;
		oBGImgTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

		var oTitleTextTrans = m_oTitleText.rectTransform;
		oTitleTextTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

		var oMsgTextTrans = m_oMsgText.rectTransform;
		oMsgTextTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

		var oOKBtnTrans = m_oOKBtn.transform as RectTransform;
		oOKBtnTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

		var oCancelBtnTrans = m_oCancelBtn.transform as RectTransform;
		oCancelBtnTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;
		
		float fBGWidth = Mathf.Max(oMsgTextTrans.rect.width, this.MinBGSize.x);
		float fBGHeight = Mathf.Max(oMsgTextTrans.rect.height, this.MinBGSize.y);

		// 텍스트를 설정한다 {
		float fOffsetVTitle = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_OFFSET_V_ALERT_P_TITLE);
		oMsgTextTrans.anchoredPosition = Vector2.zero;
		oTitleTextTrans.anchoredPosition = new Vector2(KCDefine.B_VALUE_FLT_0, (oMsgTextTrans.rect.height / 2.0f) + (oTitleTextTrans.rect.height / 2.0f) + fOffsetVTitle);

		fBGWidth = Mathf.Max(fBGWidth, oTitleTextTrans.rect.width);
		fBGHeight = Mathf.Max(fBGHeight, Mathf.Abs(oTitleTextTrans.anchoredPosition.y) + (oTitleTextTrans.rect.height / 2.0f));
		// 텍스트를 설정한다 }

		// 버튼을 설정한다 {
		float fOffsetVBtn = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_OFFSET_V_ALERT_P_BTN);
		float fOffsetHBtn = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_OFFSET_H_ALERT_P_BTN);

		// 취소 버튼 텍스트가 유효하지 않을 경우
		if(m_oCancelBtnText.text.Length <= KCDefine.B_VALUE_INT_0) {
			oOKBtnTrans.anchoredPosition = new Vector2(KCDefine.B_VALUE_FLT_0, -((oMsgTextTrans.rect.height / 2.0f) + (oOKBtnTrans.rect.height / 2.0f) + fOffsetVBtn));
			oCancelBtnTrans.anchoredPosition = new Vector2(KCDefine.B_VALUE_FLT_0, -((oMsgTextTrans.rect.height / 2.0f) + (oOKBtnTrans.rect.height / 2.0f) + fOffsetVBtn));

			m_oCancelBtn.gameObject.SetActive(false);
		} else {
			float fPosY = -((oMsgTextTrans.rect.height / 2.0f) + (oOKBtnTrans.rect.height / 2.0f) + fOffsetVBtn);

			oOKBtnTrans.anchoredPosition = new Vector2(-((oOKBtnTrans.rect.width / 2.0f) + (fOffsetHBtn / 2.0f)), fPosY);
			oCancelBtnTrans.anchoredPosition = new Vector2((oOKBtnTrans.rect.width / 2.0f) + (fOffsetHBtn / 2.0f), fPosY);
		}

		float fOKBtnDeltaX = Mathf.Abs(oOKBtnTrans.anchoredPosition.x);
		float fCancelBtnDeltaX = Mathf.Abs(oCancelBtnTrans.anchoredPosition.x);

		float fBtnHeight = Mathf.Max(oOKBtnTrans.rect.height, oCancelBtnTrans.rect.height);

		fBGWidth = Mathf.Max(fBGWidth, (fOKBtnDeltaX + (oOKBtnTrans.rect.width / 2.0f)) + (fCancelBtnDeltaX + (oCancelBtnTrans.rect.width / 2.0f)));
		fBGHeight = Mathf.Max(fBGHeight, Mathf.Abs(oOKBtnTrans.anchoredPosition.y) + (fBtnHeight / 2.0f));
		// 버튼을 설정한다 }

		// 배경을 설정한다 {
		float fOffsetVBG = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_OFFSET_V_ALERT_P_BG);
		float fOffsetHBG = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_OFFSET_H_ALERT_P_BG);

		oBGImgTrans.sizeDelta = new Vector2(fBGWidth + fOffsetHBG, (fBGHeight * 2.0f) + fOffsetVBG);
		// 배경을 설정한다 }

		// 위치를 보정한다 {
		float fVExtraOffsetTitle = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_TITLE);
		float fVExtraOffsetMsg = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_MSG);
		float fVExtraOffsetBtn = CValueTable.Inst.GetFloat(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_BTN);

		oTitleTextTrans.anchoredPosition += new Vector2(KCDefine.B_VALUE_FLT_0, fVExtraOffsetTitle);
		oMsgTextTrans.anchoredPosition += new Vector2(KCDefine.B_VALUE_FLT_0, fVExtraOffsetMsg);

		oOKBtnTrans.anchoredPosition += new Vector2(KCDefine.B_VALUE_FLT_0, -fVExtraOffsetBtn);
		oCancelBtnTrans.anchoredPosition += new Vector2(KCDefine.B_VALUE_FLT_0, -fVExtraOffsetBtn);
		// 위치를 보정한다 }
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Dictionary<string, string> a_oDataList, System.Action<CAlertPopup, bool> a_oCallback) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_oDataList, a_oCallback, KCDefine.B_POS_POPUP);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Dictionary<string, string> a_oDataList, System.Action<CAlertPopup, bool> a_oCallback, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		return CAlertPopup.Create<T>(a_oName, oObj, a_oParent, a_oDataList, a_oCallback, a_stPos);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Dictionary<string, string> a_oDataList, System.Action<CAlertPopup, bool> a_oCallback) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CAlertPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_oDataList, a_oCallback, KCDefine.B_POS_POPUP);
	}

	//! 경고 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Dictionary<string, string> a_oDataList, System.Action<CAlertPopup, bool> a_oCallback, Vector3 a_stPos) where T : CAlertPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oAlertPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stPos);
		oAlertPopup?.Init(a_oDataList, a_oCallback);
		
		return oAlertPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
