using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 약관 동의 팝업
public class CAgreePopup : CPopup {
	//! 매개 변수
	public struct STParams {
		public string m_oServices;
		public string m_oPrivacy;

		public EAgreePopupType m_ePopupType;
	}

	#region 변수
	private STParams m_stParams;

	private bool m_bIsAgreeServices = false;
	private bool m_bIsAgreePrivacy = false;
	#endregion			// 변수

	#region UI 변수
	// 일반 UI
	private Text m_oNUIsServicesText = null;
	private Text m_oNUIsPrivacyText = null;

	private Image m_oNUIsServicesCheckImg = null;
	private Image m_oNUIsPrivacyCheckImg = null;
	#endregion			// UI 변수

	#region 객체
	[SerializeField] private GameObject m_oNormUIs = null;
	[SerializeField] private GameObject m_oEUUIs = null;

	[SerializeField] private GameObject m_oNUIsServicesUIs = null;
	[SerializeField] private GameObject m_oNUIsPrivacyUIs = null;
	#endregion			// 객체

	#region 프로퍼티
	public override bool IsIgnoreAni => true;
	public override float ShowTimeScale => KCDefine.B_VAL_1_FLT;
	
	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = true;

		this.SetupNormUIs();
		this.SetupEUUIs();
	}

	//! 초기화
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	//! 일반 UI 를 설정한다
	private void SetupNormUIs() {
		// 텍스트를 설정한다
		m_oNUIsServicesText = m_oNUIsServicesUIs.ExFindComponent<Text>(KCDefine.AS_OBJ_N_AGREE_P_CONTENTS_TEXT);
		m_oNUIsPrivacyText = m_oNUIsPrivacyUIs.ExFindComponent<Text>(KCDefine.AS_OBJ_N_AGREE_P_CONTENTS_TEXT);

		// 이미지를 설정한다
		m_oNUIsServicesCheckImg = m_oNUIsServicesUIs.ExFindComponent<Image>(KCDefine.AS_OBJ_N_AGREE_P_CHECK_IMG);
		m_oNUIsPrivacyCheckImg = m_oNUIsPrivacyUIs.ExFindComponent<Image>(KCDefine.AS_OBJ_N_AGREE_P_CHECK_IMG);

		// 버튼을 설정한다 {
		var oServicesBtn = m_oNUIsServicesUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_AGREE_P_AGREE_BTN);
		oServicesBtn.onClick.AddListener(this.OnTouchNUIsServicesBtn);

		var oPrivacyBtn = m_oNUIsPrivacyUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_AGREE_P_AGREE_BTN);
		oPrivacyBtn.onClick.AddListener(this.OnTouchNUIsPrivacyBtn);
		// 버튼을 설정한다 }
	}

	//! EU UI 를 설정한다
	private void SetupEUUIs() {
		// 버튼을 설정한다 {
		var oServicesBtn = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_AGREE_P_SERVICES_BTN);
		oServicesBtn.onClick.AddListener(this.OnTouchEUUIsServicesBtn);

		var oPrivacyBtn = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_AGREE_P_PRIVACY_BTN);
		oPrivacyBtn.onClick.AddListener(this.OnTouchEUUIsPrivacyBtn);

		var oOKBtn = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_AGREE_P_OK_BTN);
		oOKBtn.onClick.AddListener(this.OnTouchEUUIsOKBtn);
		// 버튼을 설정한다 }
	}

	//! UI 상태를 갱신한다
	private void UpdateUIsState() {
		this.UpdateNormUIsState();

		m_oNormUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.NORM);
		m_oEUUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.EU);

		// 약관에 동의했을 경우
		if(m_bIsAgreeServices && m_bIsAgreePrivacy) {
			this.Close();
		}
	}

	//! 일반 UI 상태를 갱신한다
	private void UpdateNormUIsState() {
		// 텍스트를 갱신한다
		m_oNUIsServicesText.text = m_stParams.m_oServices;
		m_oNUIsPrivacyText.text = m_stParams.m_oPrivacy;

		// 이미지를 갱신한다
		m_oNUIsServicesCheckImg.gameObject.SetActive(m_bIsAgreeServices);
		m_oNUIsPrivacyCheckImg.gameObject.SetActive(m_bIsAgreePrivacy);
	}

	//! 일반 UI 서비스 버튼을 눌렀을 경우
	private void OnTouchNUIsServicesBtn() {
		m_bIsAgreeServices = !m_bIsAgreeServices;
		this.UpdateUIsState();
	}

	//! 일반 UI 개인 정보 버튼을 눌렀을 경우
	private void OnTouchNUIsPrivacyBtn() {
		m_bIsAgreePrivacy = !m_bIsAgreePrivacy;
		this.UpdateUIsState();
	}

	//! EU UI 서비스 URL 버튼을 눌렀을 경우
	private void OnTouchEUUIsServicesBtn() {
		Application.OpenURL(m_stParams.m_oServices);
	}

	//! EU UI 개인 정보 URL 버튼을 눌렀을 경우
	private void OnTouchEUUIsPrivacyBtn() {
		Application.OpenURL(m_stParams.m_oPrivacy);
	}

	//! EU UI 확인 버튼을 눌렀을 경우
	private void OnTouchEUUIsOKBtn() {
		this.Close();
	}
	#endregion			// 함수
}
