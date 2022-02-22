using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 약관 동의 팝업 */
public class CAgreePopup : CPopup {
	/** 매개 변수 */
	public struct STParams {
		public string m_oPrivacy;
		public string m_oServices;

		public EAgreePopupType m_ePopupType;
	}

	#region 변수
	private STParams m_stParams;

	private bool m_bIsAgreePrivacy = false;
	private bool m_bIsAgreeServices = false;
	
	/** =====> UI <===== */
	// 한국 UI {
	private Text m_oKRUIsPrivacyText = null;
	private Text m_oKRUIsServicesText = null;

	private Image m_oKRUIsPrivacyCheckImg = null;
	private Image m_oKRUIsServicesCheckImg = null;
	// 한국 UI }

	/** =====> 객체 <===== */
	[SerializeField] private GameObject m_oKRUIs = null;
	[SerializeField] private GameObject m_oEUUIs = null;
	
	[SerializeField] private GameObject m_oKRUIsPrivacyUIs = null;
	[SerializeField] private GameObject m_oKRUIsServicesUIs = null;
	#endregion			// 변수
	
	#region 프로퍼티
	public override float ShowTimeScale => KCDefine.B_VAL_1_FLT;
	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.IsIgnoreAni = true;
		this.IsIgnoreNavStackEvent = true;

		this.SetupKRUIs();
		this.SetupEUUIs();
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	/** 한국 UI 를 설정한다 */
	private void SetupKRUIs() {
		// 텍스트를 설정한다
		m_oKRUIsPrivacyText = m_oKRUIsPrivacyUIs.ExFindComponent<Text>(KCDefine.U_OBJ_N_DESC_TEXT);
		m_oKRUIsServicesText = m_oKRUIsServicesUIs.ExFindComponent<Text>(KCDefine.U_OBJ_N_DESC_TEXT);

		// 이미지를 설정한다
		m_oKRUIsPrivacyCheckImg = m_oKRUIsPrivacyUIs.ExFindComponent<Image>(KCDefine.U_OBJ_N_CHECK_IMG);
		m_oKRUIsServicesCheckImg = m_oKRUIsServicesUIs.ExFindComponent<Image>(KCDefine.U_OBJ_N_CHECK_IMG);

		// 버튼을 설정한다
		m_oKRUIsPrivacyUIs.ExFindComponent<Button>(KCDefine.U_OBJ_N_AGREE_BTN).onClick.AddListener(this.OnTouchKRUIsPrivacyBtn);
		m_oKRUIsServicesUIs.ExFindComponent<Button>(KCDefine.U_OBJ_N_AGREE_BTN).onClick.AddListener(this.OnTouchKRUIsServicesBtn);
	}

	/** 유럽 연합 UI 를 설정한다 */
	private void SetupEUUIs() {
		// 버튼을 설정한다
		m_oEUUIs.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN).onClick.AddListener(this.OnTouchEUUIsOKBtn);
		m_oEUUIs.ExFindComponent<Button>(KCDefine.U_OBJ_N_PRIVACY_BTN).onClick.AddListener(this.OnTouchEUUIsPrivacyBtn);
		m_oEUUIs.ExFindComponent<Button>(KCDefine.U_OBJ_N_SERVICES_BTN).onClick.AddListener(this.OnTouchEUUIsServicesBtn);
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		this.UpdateKRUIsState();

		m_oKRUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.KR);
		m_oEUUIs.gameObject.SetActive(m_stParams.m_ePopupType == EAgreePopupType.EU);

		// 약관에 동의했을 경우
		if(m_bIsAgreePrivacy && m_bIsAgreeServices) {
			this.OnTouchCloseBtn();
		}
	}

	/** 한국 UI 상태를 갱신한다 */
	private void UpdateKRUIsState() {
		// 텍스트를 갱신한다
		m_oKRUIsPrivacyText.text = m_stParams.m_oPrivacy;
		m_oKRUIsServicesText.text = m_stParams.m_oServices;

		// 이미지를 갱신한다
		m_oKRUIsPrivacyCheckImg.gameObject.SetActive(m_bIsAgreePrivacy);
		m_oKRUIsServicesCheckImg.gameObject.SetActive(m_bIsAgreeServices);
	}

	/** 한국 UI 개인 정보 버튼을 눌렀을 경우 */
	private void OnTouchKRUIsPrivacyBtn() {
		m_bIsAgreePrivacy = !m_bIsAgreePrivacy;
		this.UpdateUIsState();
	}

	/** 한국 UI 서비스 버튼을 눌렀을 경우 */
	private void OnTouchKRUIsServicesBtn() {
		m_bIsAgreeServices = !m_bIsAgreeServices;
		this.UpdateUIsState();
	}

	/** 유럽 연합 UI 확인 버튼을 눌렀을 경우 */
	private void OnTouchEUUIsOKBtn() {
		this.OnTouchCloseBtn();
	}

	/** 유럽 연합 UI 개인 정보 URL 버튼을 눌렀을 경우 */
	private void OnTouchEUUIsPrivacyBtn() {
		Application.OpenURL(m_stParams.m_oPrivacy);
	}

	/** 유럽 연합 UI 서비스 URL 버튼을 눌렀을 경우 */
	private void OnTouchEUUIsServicesBtn() {
		Application.OpenURL(m_stParams.m_oServices);
	}
	#endregion			// 함수
}
