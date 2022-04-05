using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 경고 팝업 */
public partial class CAlertPopup : CPopup {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		TITLE_TEXT,
		MSG_TEXT,
		BG_IMG,
		OK_BTN,
		CANCEL_BTN,
		BM_UIS_LAYOUT_GROUP,
		[HideInInspector] MAX_VAL
	}

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
	private Dictionary<EKey, TMP_Text> m_oTextDict = new Dictionary<EKey, TMP_Text>() {
		[EKey.TITLE_TEXT] = null,
		[EKey.MSG_TEXT] = null
	};

	private Dictionary<EKey, Image> m_oImgDict = new Dictionary<EKey, Image>() {
		[EKey.BG_IMG] = null
	};

	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>() {
		[EKey.OK_BTN] = null,
		[EKey.CANCEL_BTN] = null
	};

	private Dictionary<EKey, LayoutGroup> m_oLayoutGroupDict = new Dictionary<EKey, LayoutGroup>() {
		[EKey.BM_UIS_LAYOUT_GROUP] = null
	};
	#endregion			// 변수

	#region 프로퍼티
	protected TMP_Text TitleText => m_oTextDict[EKey.TITLE_TEXT];
	protected TMP_Text MsgText => m_oTextDict[EKey.MSG_TEXT];

	protected Image AlertBGImg => m_oImgDict[EKey.BG_IMG];

	protected Button OKBtn => m_oBtnDict[EKey.OK_BTN];
	protected Button CancelBtn => m_oBtnDict[EKey.CANCEL_BTN];
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oTextDict[EKey.TITLE_TEXT] = this.Contents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_TITLE_TEXT);
		m_oTextDict[EKey.MSG_TEXT] = this.Contents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_MSG_TEXT);

		// 이미지를 설정한다
		m_oImgDict[EKey.BG_IMG] = this.Contents.ExFindComponent<Image>(KCDefine.U_OBJ_N_BG_IMG);

		// 버튼을 설정한다 {
		m_oBtnDict[EKey.OK_BTN] = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN);
		m_oBtnDict[EKey.OK_BTN].onClick.AddListener(this.OnTouchOKBtn);

		m_oBtnDict[EKey.CANCEL_BTN] = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN);
		m_oBtnDict[EKey.CANCEL_BTN].onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }

		// 레이아웃을 설정한다
		m_oLayoutGroupDict[EKey.BM_UIS_LAYOUT_GROUP] = this.Contents.ExFindComponent<LayoutGroup>(KCDefine.U_OBJ_N_BOTTOM_MENU_UIS);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();

		float fMsgTextHeight = m_oTextDict[EKey.MSG_TEXT].rectTransform.sizeDelta.y;
		m_oBtnDict[EKey.CANCEL_BTN].gameObject.SetActive(m_stParams.m_oStrDict.ContainsKey(EStr.CANCEL_BTN));

		// 컨텐츠 크기를 설정한다 {
		float fContentsWidth = m_oTextDict[EKey.TITLE_TEXT].rectTransform.sizeDelta.x;
		fContentsWidth = Mathf.Max(fContentsWidth, m_oTextDict[EKey.MSG_TEXT].rectTransform.sizeDelta.x);
		fContentsWidth = Mathf.Max(fContentsWidth, (m_oLayoutGroupDict[EKey.BM_UIS_LAYOUT_GROUP].transform as RectTransform).sizeDelta.x);

		float fContentsHeight = Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMin.y);
		fContentsHeight += Mathf.Abs((this.ContentsUIs.transform as RectTransform).offsetMax.y);
		fContentsHeight += Mathf.Abs(m_oTextDict[EKey.TITLE_TEXT].transform.localPosition.y);
		fContentsHeight += Mathf.Abs(m_oLayoutGroupDict[EKey.BM_UIS_LAYOUT_GROUP].transform.localPosition.y);
		// 컨텐츠 크기를 설정한다 }

		this.UpdateUIsState();
		this.ExLateCallFunc((a_oSender) => this.DoSetupContents(fMsgTextHeight, new Vector3(fContentsWidth, fContentsHeight, KCDefine.B_VAL_0_FLT)));
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// 텍스트를 갱신한다 {		
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		m_oTextDict[EKey.TITLE_TEXT].ExSetText(m_stParams.m_oStrDict[EStr.TITLE], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
		m_oTextDict[EKey.MSG_TEXT].ExSetText(m_stParams.m_oStrDict[EStr.MSG], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));

		m_oBtnDict[EKey.OK_BTN].GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oStrDict[EStr.OK_BTN], CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
		m_oBtnDict[EKey.CANCEL_BTN].GetComponentInChildren<TMP_Text>().ExSetText(m_stParams.m_oStrDict.GetValueOrDefault(EStr.CANCEL_BTN, string.Empty), CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
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
	private void DoSetupContents(float a_fMsgTextHeight, Vector3 a_stContentsSize) {
		float fContentsOffsetV = m_oTextDict[EKey.MSG_TEXT].rectTransform.sizeDelta.y - a_fMsgTextHeight;
		float fContentsOffsetH = m_oImgDict[EKey.BG_IMG].rectTransform.sizeDelta.x - a_stContentsSize.x;

		// 이미지를 설정한다 {
		var stContentsSize = new Vector3(m_oTextDict[EKey.TITLE_TEXT].rectTransform.sizeDelta.x, m_oTextDict[EKey.TITLE_TEXT].rectTransform.sizeDelta.y, KCDefine.B_VAL_0_FLT);
		stContentsSize.x = Mathf.Max(stContentsSize.x, m_oTextDict[EKey.MSG_TEXT].rectTransform.sizeDelta.x);
		stContentsSize.x = Mathf.Max(stContentsSize.x, (m_oLayoutGroupDict[EKey.BM_UIS_LAYOUT_GROUP].transform as RectTransform).sizeDelta.x);

		m_oImgDict[EKey.BG_IMG].gameObject.ExSetSizeDeltaX(Mathf.Max(stContentsSize.x + fContentsOffsetH, KCDefine.U_MIN_SIZE_ALERT_POPUP.x));
		m_oImgDict[EKey.BG_IMG].gameObject.ExSetSizeDeltaY(Mathf.Max(a_stContentsSize.y + fContentsOffsetV, KCDefine.U_MIN_SIZE_ALERT_POPUP.y));
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
