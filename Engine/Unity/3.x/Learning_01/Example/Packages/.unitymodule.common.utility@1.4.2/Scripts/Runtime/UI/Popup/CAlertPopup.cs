using System.Collections;
using System.Collections.Generic;
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
	protected STParams m_stParams;
	protected System.Action<CAlertPopup, bool> m_oCallback = null;
	#endregion			// 변수

	#region UI 변수
	protected Text m_oTitleText = null;
	protected Text m_oMsgText = null;
	protected Text m_oOKBtnText = null;
	protected Text m_oCancelBtnText = null;

	protected Image m_oAlertBGImg = null;

	protected Button m_oOKBtn = null;
	protected Button m_oCancelBtn = null;
	#endregion			// UI 변수

	#region 프로퍼티
	public Vector3 MinBGSize { get; private set; } = Vector3.zero;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 버튼을 설정한다 {
		m_oOKBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_OK_BTN);
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);

		m_oCancelBtn = m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ALERT_P_CANCEL_BTN);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 텍스트를 설정한다
		m_oTitleText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_TITLE_TEXT);
		m_oMsgText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_ALERT_P_MSG_TEXT);
		m_oOKBtnText = m_oOKBtn.GetComponentInChildren<Text>();
		m_oCancelBtnText = m_oCancelBtn.GetComponentInChildren<Text>();

		// 이미지를 설정한다
		m_oAlertBGImg = m_oContents.ExFindComponent<Image>(KCDefine.U_OBJ_N_ALERT_P_BG_IMG);
	}

	//! 초기화
	public virtual void Init(STParams a_stParams, System.Action<CAlertPopup, bool> a_oCallback) {
		base.Init();

		m_stParams = a_stParams;
		m_oCallback = a_oCallback;

		this.UpdateUIsState();
	}

	//! 최소 배경 크기를 변경한다
	public void SetMinBGSize(Vector3 a_stSize) {
		this.MinBGSize = a_stSize;
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// 텍스트 상태를 갱신한다
		m_oTitleText.text = m_stParams.m_oTitle;
		m_oMsgText.text = m_stParams.m_oMsg;
		m_oOKBtnText.text = m_stParams.m_oOKBtnText;
		m_oCancelBtnText.text = m_stParams.m_oCancelBtnText.ExIsValid() ? m_stParams.m_oCancelBtnText : string.Empty;
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		
		var oBGImgTrans = m_oAlertBGImg.rectTransform;
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
		float fOffsetVTitle = CValTable.Inst.GetFlt(KCDefine.VT_KEY_OFFSET_V_ALERT_P_TITLE);
		oMsgTextTrans.anchoredPosition = Vector2.zero;
		oTitleTextTrans.anchoredPosition = new Vector2(KCDefine.B_VAL_0_FLT, (oMsgTextTrans.rect.height / KCDefine.B_VAL_2_FLT) + (oTitleTextTrans.rect.height / KCDefine.B_VAL_2_FLT) + fOffsetVTitle);

		fBGWidth = Mathf.Max(fBGWidth, oTitleTextTrans.rect.width);
		fBGHeight = Mathf.Max(fBGHeight, Mathf.Abs(oTitleTextTrans.anchoredPosition.y) + (oTitleTextTrans.rect.height / KCDefine.B_VAL_2_FLT));
		// 텍스트를 설정한다 }

		// 버튼을 설정한다 {
		float fOffsetVBtn = CValTable.Inst.GetFlt(KCDefine.VT_KEY_OFFSET_V_ALERT_P_BTN);
		float fOffsetHBtn = CValTable.Inst.GetFlt(KCDefine.VT_KEY_OFFSET_H_ALERT_P_BTN);

		// 취소 버튼 텍스트가 유효하지 않을 경우
		if(m_oCancelBtnText.text.Length <= KCDefine.B_VAL_0_INT) {
			oOKBtnTrans.anchoredPosition = new Vector2(KCDefine.B_VAL_0_FLT, -((oMsgTextTrans.rect.height / KCDefine.B_VAL_2_FLT) + (oOKBtnTrans.rect.height / KCDefine.B_VAL_2_FLT) + fOffsetVBtn));
			oCancelBtnTrans.anchoredPosition = new Vector2(KCDefine.B_VAL_0_FLT, -((oMsgTextTrans.rect.height / KCDefine.B_VAL_2_FLT) + (oOKBtnTrans.rect.height / KCDefine.B_VAL_2_FLT) + fOffsetVBtn));

			m_oCancelBtn.gameObject.SetActive(false);
		} else {
			float fPosY = -((oMsgTextTrans.rect.height / KCDefine.B_VAL_2_FLT) + (oOKBtnTrans.rect.height / KCDefine.B_VAL_2_FLT) + fOffsetVBtn);

			oOKBtnTrans.anchoredPosition = new Vector2(-((oOKBtnTrans.rect.width / KCDefine.B_VAL_2_FLT) + (fOffsetHBtn / KCDefine.B_VAL_2_FLT)), fPosY);
			oCancelBtnTrans.anchoredPosition = new Vector2((oOKBtnTrans.rect.width / KCDefine.B_VAL_2_FLT) + (fOffsetHBtn / KCDefine.B_VAL_2_FLT), fPosY);
		}

		float fOKBtnDeltaX = Mathf.Abs(oOKBtnTrans.anchoredPosition.x);
		float fCancelBtnDeltaX = Mathf.Abs(oCancelBtnTrans.anchoredPosition.x);

		float fBtnHeight = Mathf.Max(oOKBtnTrans.rect.height, oCancelBtnTrans.rect.height);

		fBGWidth = Mathf.Max(fBGWidth, (fOKBtnDeltaX + (oOKBtnTrans.rect.width / KCDefine.B_VAL_2_FLT)) + (fCancelBtnDeltaX + (oCancelBtnTrans.rect.width / KCDefine.B_VAL_2_FLT)));
		fBGHeight = Mathf.Max(fBGHeight, Mathf.Abs(oOKBtnTrans.anchoredPosition.y) + (fBtnHeight / KCDefine.B_VAL_2_FLT));
		// 버튼을 설정한다 }

		// 배경을 설정한다 {
		float fOffsetVBG = CValTable.Inst.GetFlt(KCDefine.VT_KEY_OFFSET_V_ALERT_P_BG);
		float fOffsetHBG = CValTable.Inst.GetFlt(KCDefine.VT_KEY_OFFSET_H_ALERT_P_BG);

		oBGImgTrans.sizeDelta = new Vector2(fBGWidth + fOffsetHBG, (fBGHeight * KCDefine.B_VAL_2_FLT) + fOffsetVBG);
		// 배경을 설정한다 }

		// 위치를 보정한다 {
		float fVExtraOffsetTitle = CValTable.Inst.GetFlt(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_TITLE);
		float fVExtraOffsetMsg = CValTable.Inst.GetFlt(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_MSG);
		float fVExtraOffsetBtn = CValTable.Inst.GetFlt(KCDefine.VT_KEY_EXTRA_OFFSET_V_ALERT_P_BTN);

		oTitleTextTrans.anchoredPosition += new Vector2(KCDefine.B_VAL_0_FLT, fVExtraOffsetTitle);
		oMsgTextTrans.anchoredPosition += new Vector2(KCDefine.B_VAL_0_FLT, fVExtraOffsetMsg);

		oOKBtnTrans.anchoredPosition += new Vector2(KCDefine.B_VAL_0_FLT, -fVExtraOffsetBtn);
		oCancelBtnTrans.anchoredPosition += new Vector2(KCDefine.B_VAL_0_FLT, -fVExtraOffsetBtn);
		// 위치를 보정한다 }
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
