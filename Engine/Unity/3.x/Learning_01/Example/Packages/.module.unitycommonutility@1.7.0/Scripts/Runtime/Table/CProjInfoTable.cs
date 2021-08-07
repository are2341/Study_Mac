using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 프로젝트 정보
[System.Serializable]
public struct STProjInfo {
	public STBuildVer m_stBuildVer;
	public string m_oAppID;

	public string m_oStoreURL;
	public string m_oSupportsMail;
}

//! 프로젝터 정보 테이블
public class CProjInfoTable : CScriptableObj<CProjInfoTable> {
	#region 변수
	[Header("Company Info")]
	[SerializeField] private string m_oCompanyName = string.Empty;

	[SerializeField] private string m_oServicesURL = string.Empty;
	[SerializeField] private string m_oPrivacyURL = string.Empty;

	[Header("Common Proj Info")]
	[SerializeField] private string m_oProjName = string.Empty;
	[SerializeField] private string m_oProductName = string.Empty;
	[SerializeField] private string m_oShortProductName = string.Empty;

	[Header("Standalone Proj Info")]
	[SerializeField] private STProjInfo m_stMacProjInfo;
	[SerializeField] private STProjInfo m_stWndsProjInfo;

	[Header("iOS Proj Info")]
	[SerializeField] private STProjInfo m_stiOSProjInfo;

	[Header("Android Proj Info")]
	[SerializeField] private STProjInfo m_stGoogleProjInfo;
	[SerializeField] private STProjInfo m_stOneStoreProjInfo;
	[SerializeField] private STProjInfo m_stGalaxyStoreProjInfo;
	#endregion			// 변수

	#region 프로퍼티
	public STProjInfo ProjInfo { get; private set; }
	public string CompanyName => m_oCompanyName;

	public string ServicesURL => m_oServicesURL;
	public string PrivacyURL => m_oPrivacyURL;

	public string ProjName => m_oProjName;
	public string ProductName => m_oProductName;
	public string ShortProductName => m_oShortProductName;

	public STProjInfo MacProjInfo => m_stMacProjInfo;
	public STProjInfo WndsProjInfo => m_stWndsProjInfo;

	public STProjInfo iOSProjInfo => m_stiOSProjInfo;

	public STProjInfo GoogleProjInfo => m_stGoogleProjInfo;
	public STProjInfo OneStoreProjInfo => m_stOneStoreProjInfo;
	public STProjInfo GalaxyStoreProjInfo => m_stGalaxyStoreProjInfo;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

#if UNITY_IOS
		this.ProjInfo = m_stiOSProjInfo;
#elif UNITY_ANDROID
#if ONE_STORE
		this.ProjInfo = m_stOneStoreProjInfo;
#elif GALAXY_STORE
		this.ProjInfo = m_stGalaxyStoreProjInfo;
#else
		this.ProjInfo = m_stGoogleProjInfo;
#endif			// #if ONE_STORE
#elif UNITY_STANDALONE
#if UNITY_STANDALONE_WIN
		this.ProjInfo = m_stWndsProjInfo;
#else
		this.ProjInfo = m_stMacProjInfo;
#endif			// #if UNITY_STANDALONE_WIN
#endif			// #if UNITY_IOS
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	//! 회사 이름을 변경한다
	public void SetCompanyName(string a_oName) {
		m_oCompanyName = a_oName;
	}

	//! 서비스 URL 을 변경한다
	public void SetServicesURL(string a_oURL) {
		m_oServicesURL = a_oURL;
	}

	//! 개인 정보 URL 을 변경한다
	public void SetPrivacyURL(string a_oURL) {
		m_oPrivacyURL = a_oURL;
	}

	//! 프로젝트 이름을 변경한다
	public void SetProjName(string a_oProjName) {
		m_oProjName = a_oProjName;
	}

	//! 제품 이름을 변경한다
	public void SetProductName(string a_oName) {
		m_oProductName = a_oName;
	}

	//! 단순 제품 이름을 변경한다
	public void SetShortProductName(string a_oName) {
		m_oShortProductName = a_oName;
	}

	//! 맥 프로젝트 정보를 변경한다
	public void SetMacProjInfo(STProjInfo a_STProjInfo) {
		m_stMacProjInfo = a_STProjInfo;
	}

	//! 윈도우즈 프로젝트 정보를 변경한다
	public void SetWndsProjInfo(STProjInfo a_STProjInfo) {
		m_stWndsProjInfo = a_STProjInfo;
	}

	//! iOS 프로젝트 정보를 변경한다
	public void SetiOSProjInfo(STProjInfo a_STProjInfo) {
		m_stiOSProjInfo = a_STProjInfo;
	}

	//! 구글 프로젝트 정보를 변경한다
	public void SetGoogleProjInfo(STProjInfo a_STProjInfo) {
		m_stGoogleProjInfo = a_STProjInfo;
	}

	//! 원 스토어 프로젝트 정보를 변경한다
	public void SetOneStoreProjInfo(STProjInfo a_STProjInfo) {
		m_stOneStoreProjInfo = a_STProjInfo;
	}

	//! 갤럭시 스토어 프로젝트 정보를 변경한다
	public void SetGalaxyStoreProjInfo(STProjInfo a_STProjInfo) {
		m_stGalaxyStoreProjInfo = a_STProjInfo;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
