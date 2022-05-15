using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 약관 동의 팝업 */
public partial class CAgreePopup : CPopup {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		KR_UIS_PRIVACY_TEXT,
		KR_UIS_SERVICES_TEXT,
		KR_UIS_PRIVACY_CHECK_IMG,
		KR_UIS_SERVICES_CHECK_IMG,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public string m_oPrivacy;
		public string m_oServices;

		public EAgreePopup m_eAgreePopup;
	}

	#region 변수
	private STParams m_stParams;

	private bool m_bIsKRUIsAgreePrivacy = false;
	private bool m_bIsKRUIsAgreeServices = false;

	/** =====> UI <===== */
	private Dictionary<EKey, Text> m_oTextDict = new Dictionary<EKey, Text>() {
		[EKey.KR_UIS_PRIVACY_TEXT] = null,
		[EKey.KR_UIS_SERVICES_TEXT] = null,
	};

	private Dictionary<EKey, Image> m_oImgDict = new Dictionary<EKey, Image>() {
		[EKey.KR_UIS_PRIVACY_CHECK_IMG] = null,
		[EKey.KR_UIS_SERVICES_CHECK_IMG] = null
	};

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

	/** 닫혔을 경우 */
	protected override void OnClose() {
		base.OnClose();
		
		// 앱이 실행 중 일 경우
		if(CSceneManager.IsRunning && !CSceneManager.IsAppQuit) {
			this.gameObject.ExBroadcastMsg(KCDefine.U_FUNC_N_RESET_ANI, null);
		}
	}

	/** 한국 UI 를 설정한다 */
	private void SetupKRUIs() {
		// 텍스트를 설정한다
		m_oTextDict[EKey.KR_UIS_PRIVACY_TEXT] = m_oKRUIsPrivacyUIs.ExFindComponent<Text>(KCDefine.U_OBJ_N_DESC_TEXT);
		m_oTextDict[EKey.KR_UIS_SERVICES_TEXT] = m_oKRUIsServicesUIs.ExFindComponent<Text>(KCDefine.U_OBJ_N_DESC_TEXT);

		// 이미지를 설정한다
		m_oImgDict[EKey.KR_UIS_PRIVACY_CHECK_IMG] = m_oKRUIsPrivacyUIs.ExFindComponent<Image>(KCDefine.U_OBJ_N_CHECK_IMG);
		m_oImgDict[EKey.KR_UIS_SERVICES_CHECK_IMG] = m_oKRUIsServicesUIs.ExFindComponent<Image>(KCDefine.U_OBJ_N_CHECK_IMG);

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

		m_oKRUIs.gameObject.SetActive(m_stParams.m_eAgreePopup == EAgreePopup.KR);
		m_oEUUIs.gameObject.SetActive(m_stParams.m_eAgreePopup == EAgreePopup.EU);

		// 약관에 동의했을 경우
		if(m_bIsKRUIsAgreePrivacy && m_bIsKRUIsAgreeServices) {
			this.OnTouchCloseBtn();
		}
	}

	/** 한국 UI 상태를 갱신한다 */
	private void UpdateKRUIsState() {
		// 텍스트를 갱신한다
		m_oTextDict[EKey.KR_UIS_PRIVACY_TEXT].text = m_stParams.m_oPrivacy;
		m_oTextDict[EKey.KR_UIS_SERVICES_TEXT].text = m_stParams.m_oServices;

		// 이미지를 갱신한다
		m_oImgDict[EKey.KR_UIS_PRIVACY_CHECK_IMG].gameObject.SetActive(m_bIsKRUIsAgreePrivacy);
		m_oImgDict[EKey.KR_UIS_SERVICES_CHECK_IMG].gameObject.SetActive(m_bIsKRUIsAgreeServices);
	}

	/** 한국 UI 개인 정보 버튼을 눌렀을 경우 */
	private void OnTouchKRUIsPrivacyBtn() {
		m_bIsKRUIsAgreePrivacy = !m_bIsKRUIsAgreePrivacy;
		this.UpdateUIsState();
	}

	/** 한국 UI 서비스 버튼을 눌렀을 경우 */
	private void OnTouchKRUIsServicesBtn() {
		m_bIsKRUIsAgreeServices = !m_bIsKRUIsAgreeServices;
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
