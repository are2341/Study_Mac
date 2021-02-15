using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 약관 동의 팝업
public class CAgreePopup : CPopup {
	#region 변수
	private bool m_bIsAgreeServices = false;
	private bool m_bIsAgreePrivacy = false;

	private string m_oServicesURL = string.Empty;
	private string m_oPrivacyURL = string.Empty;

	private Image m_oServicesImg = null;
	private Image m_oPrivacyImg = null;
	#endregion			// 변수

	#region 객체
	[SerializeField] private GameObject m_oNormUI = null;
	[SerializeField] private GameObject m_oEUUI = null;

	[SerializeField] private GameObject m_oServicesUI = null;
	[SerializeField] private GameObject m_oPrivacyUI = null;
	#endregion			// 객체

	#region 프로퍼티
	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		this.IsIgnoreAni = true;
		this.IsIgnoreNavStackEvent = true;
	}

	//! 초기화
	public virtual void Init(string a_oServices, string a_oPrivacy, EAgreePopupType a_ePopupType) {
		m_oServicesURL = a_oServices;
		m_oPrivacyURL = a_oPrivacy;

		m_oNormUI.gameObject.SetActive(a_ePopupType == EAgreePopupType.NORM);
		m_oEUUI.gameObject.SetActive(a_ePopupType == EAgreePopupType.EU);

		// 일반 모드 일 경우
		if(a_ePopupType == EAgreePopupType.NORM) {
			// 텍스트를 설정한다 {
			var oServicesText = m_oServicesUI.ExFindComponent<Text>(KCDefine.AS_OBJ_N_SERVICES_TEXT);
			oServicesText.text = a_oServices;

			var oPrivacyText = m_oPrivacyUI.ExFindComponent<Text>(KCDefine.AS_OBJ_N_PRIVACY_TEXT);
			oPrivacyText.text = a_oPrivacy;
			// 텍스트를 설정한다 }

			// 버튼을 설정한다 {
			var oServicesBtn = m_oServicesUI.ExFindComponent<Button>(KCDefine.AS_OBJ_N_SERVICES_BTN);
			oServicesBtn.onClick.AddListener(this.OnTouchServicesBtn);

			var oPrivacyBtn = m_oPrivacyUI.ExFindComponent<Button>(KCDefine.AS_OBJ_N_PRIVACY_BTN);
			oPrivacyBtn.onClick.AddListener(this.OnTouchPrivacyBtn);
			// 버튼을 설정한다 }

			// 이미지를 설정한다
			m_oServicesImg = m_oServicesUI.ExFindComponent<Image>(KCDefine.AS_OBJ_N_SERVICES_IMG);
			m_oPrivacyImg = m_oPrivacyUI.ExFindComponent<Image>(KCDefine.AS_OBJ_N_PRIVACY_IMG);

			this.UpdateNormUIState();
		} else {
			// 텍스트를 설정한다 {
			var oServicesURLText = m_oEUUI.ExFindComponent<Button>(KCDefine.AS_OBJ_N_SERVICES_URL_TEXT);
			oServicesURLText.onClick.AddListener(this.OnTouchServicesURLText);

			var oPrivacyURLText = m_oEUUI.ExFindComponent<Button>(KCDefine.AS_OBJ_N_PRIVACY_URL_TEXT);
			oPrivacyURLText.onClick.AddListener(this.OnTouchPrivacyURLText);

			var oOKBtn = m_oEUUI.ExFindComponent<Button>(KCDefine.AS_OBJ_N_OK_BTN);
			oOKBtn.onClick.AddListener(this.OnTouchOKBtn);
			// 텍스트를 설정한다 }
		}
	}

	//! 서비스 버튼을 눌렀을 경우
	private void OnTouchServicesBtn() {
		m_bIsAgreeServices = !m_bIsAgreeServices;
		this.UpdateNormUIState();
	}

	//! 개인 정보 버튼을 눌렀을 경우
	private void OnTouchPrivacyBtn() {
		m_bIsAgreePrivacy = !m_bIsAgreePrivacy;
		this.UpdateNormUIState();
	}

	//! 서비스 URL 버튼을 눌렀을 경우
	private void OnTouchServicesURLText() {
		CFunc.OpenURL(m_oServicesURL);
	}

	//! 개인 정보 URL 버튼을 눌렀을 경우
	private void OnTouchPrivacyURLText() {
		CFunc.OpenURL(m_oPrivacyURL);
	}

	//! 확인 버튼을 눌렀을 경우
	private void OnTouchOKBtn() {
		this.Close();
	}

	//! 일반 UI 상태를 갱신한다
	private void UpdateNormUIState() {
		m_oServicesImg.gameObject.SetActive(m_bIsAgreeServices);
		m_oPrivacyImg.gameObject.SetActive(m_bIsAgreePrivacy);

		// 약관에 동의했을 경우
		if(m_bIsAgreeServices && m_bIsAgreePrivacy) {
			this.Close();
		}
	}
	#endregion			// 함수
}
