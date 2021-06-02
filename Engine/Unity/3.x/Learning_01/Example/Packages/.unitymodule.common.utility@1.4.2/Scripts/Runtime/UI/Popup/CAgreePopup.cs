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
	private Image m_oServicesImg = null;
	private Image m_oPrivacyImg = null;
	#endregion			// UI 변수

	#region 객체
	[SerializeField] private GameObject m_oNormUIs = null;
	[SerializeField] private GameObject m_oEUUIs = null;

	[SerializeField] private GameObject m_oServicesUIs = null;
	[SerializeField] private GameObject m_oPrivacyUIs = null;
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
	}

	//! 초기화
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	private void UpdateUIsState() {
		m_oNormUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.NORM);
		m_oEUUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.EU);

		// 일반 모드 일 경우
		if(m_stParams.m_ePopupType == EAgreePopupType.NORM) {
			// 텍스트를 설정한다 {
			var oServicesText = m_oServicesUIs.ExFindComponent<Text>(KCDefine.AS_OBJ_N_SERVICES_TEXT);
			oServicesText.text = m_stParams.m_oServices;

			var oPrivacyText = m_oPrivacyUIs.ExFindComponent<Text>(KCDefine.AS_OBJ_N_PRIVACY_TEXT);
			oPrivacyText.text = m_stParams.m_oPrivacy;
			// 텍스트를 설정한다 }

			// 버튼을 설정한다 {
			var oServicesBtn = m_oServicesUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_SERVICES_BTN);
			oServicesBtn.onClick.AddListener(this.OnTouchServicesBtn);

			var oPrivacyBtn = m_oPrivacyUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_PRIVACY_BTN);
			oPrivacyBtn.onClick.AddListener(this.OnTouchPrivacyBtn);
			// 버튼을 설정한다 }

			// 이미지를 설정한다
			m_oServicesImg = m_oServicesUIs.ExFindComponent<Image>(KCDefine.AS_OBJ_N_SERVICES_IMG);
			m_oPrivacyImg = m_oPrivacyUIs.ExFindComponent<Image>(KCDefine.AS_OBJ_N_PRIVACY_IMG);

			this.UpdateNormUIsState();
		} else {
			// 텍스트를 설정한다 {
			var oServicesURLText = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_SERVICES_URL_TEXT);
			oServicesURLText.onClick.AddListener(this.OnTouchServicesURLText);

			var oPrivacyURLText = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_PRIVACY_URL_TEXT);
			oPrivacyURLText.onClick.AddListener(this.OnTouchPrivacyURLText);

			var oOKBtn = m_oEUUIs.ExFindComponent<Button>(KCDefine.AS_OBJ_N_OK_BTN);
			oOKBtn.onClick.AddListener(this.OnTouchOKBtn);
			// 텍스트를 설정한다 }
		}
	}

	//! 서비스 버튼을 눌렀을 경우
	private void OnTouchServicesBtn() {
		m_bIsAgreeServices = !m_bIsAgreeServices;
		this.UpdateNormUIsState();
	}

	//! 개인 정보 버튼을 눌렀을 경우
	private void OnTouchPrivacyBtn() {
		m_bIsAgreePrivacy = !m_bIsAgreePrivacy;
		this.UpdateNormUIsState();
	}

	//! 서비스 URL 버튼을 눌렀을 경우
	private void OnTouchServicesURLText() {
		CFunc.OpenURL(m_stParams.m_oServices);
	}

	//! 개인 정보 URL 버튼을 눌렀을 경우
	private void OnTouchPrivacyURLText() {
		CFunc.OpenURL(m_stParams.m_oPrivacy);
	}

	//! 확인 버튼을 눌렀을 경우
	private void OnTouchOKBtn() {
		this.Close();
	}

	//! 일반 UI 상태를 갱신한다
	private void UpdateNormUIsState() {
		m_oServicesImg.gameObject.SetActive(m_bIsAgreeServices);
		m_oPrivacyImg.gameObject.SetActive(m_bIsAgreePrivacy);

		// 약관에 동의했을 경우
		if(m_bIsAgreeServices && m_bIsAgreePrivacy) {
			this.Close();
		}
	}
	#endregion			// 함수
}
