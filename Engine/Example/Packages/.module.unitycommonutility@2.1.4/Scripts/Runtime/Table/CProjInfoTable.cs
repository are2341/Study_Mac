using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 프로젝트 정보 */
[System.Serializable]
public struct STProjInfo {
	public string m_oAppID;
	public string m_oStoreAppID;
	public string m_oSupportsMail;
	
	public STBuildVerInfo m_stBuildVerInfo;
}

/** 프로젝터 정보 테이블 */
public class CProjInfoTable : CScriptableObj<CProjInfoTable> {
	#region 변수
	[Header("=====> Company Info <=====")]
	[SerializeField] private string m_oCompany = string.Empty;
	[SerializeField] private string m_oPrivacyURL = string.Empty;
	[SerializeField] private string m_oServicesURL = string.Empty;

	[Header("=====> Common Proj Info <=====")]
	[SerializeField] private string m_oProjName = string.Empty;
	[SerializeField] private string m_oProductName = string.Empty;
	[SerializeField] private string m_oShortProductName = string.Empty;

	[Header("=====> iOS Proj Info <=====")]
	[SerializeField] private STProjInfo m_stiOSAppleProjInfo;

	[Header("=====> Android Proj Info <=====")]
	[SerializeField] private STProjInfo m_stAndroidGoogleProjInfo;
	[SerializeField] private STProjInfo m_stAndroidAmazonProjInfo;

	[Header("=====> Standalone Proj Info <=====")]
	[SerializeField] private STProjInfo m_stStandaloneMacSteamProjInfo;
	[SerializeField] private STProjInfo m_stStandaloneWndsSteamProjInfo;
	#endregion			// 변수

	#region 프로퍼티
	public STProjInfo ProjInfo { get; private set; }
	public string Company => m_oCompany;

	public string PrivacyURL => m_oPrivacyURL;
	public string ServicesURL => m_oServicesURL;

	public string ProjName => m_oProjName;
	public string ProductName => m_oProductName;
	public string ShortProductName => m_oShortProductName;

	public STProjInfo iOSAppleProjInfo => m_stiOSAppleProjInfo;

	public STProjInfo AndroidGoogleProjInfo => m_stAndroidGoogleProjInfo;
	public STProjInfo AndroidAmazonProjInfo => m_stAndroidAmazonProjInfo;

	public STProjInfo StandaloneMacSteamProjInfo => m_stStandaloneMacSteamProjInfo;
	public STProjInfo StandaloneWndsSteamProjInfo => m_stStandaloneWndsSteamProjInfo;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		
#if UNITY_IOS
		this.ProjInfo = m_stiOSAppleProjInfo;
#elif UNITY_ANDROID
#if ANDROID_AMAZON_PLATFORM
		this.ProjInfo = m_stAndroidAmazonProjInfo;
#else
		this.ProjInfo = m_stAndroidGoogleProjInfo;
#endif			// #if ANDROID_AMAZON_PLATFORM
#elif UNITY_STANDALONE
#if STANDALONE_WNDS_STEAM_PLATFORM
		this.ProjInfo = m_stStandaloneWndsSteamProjInfo;
#else
		this.ProjInfo = m_stStandaloneMacSteamProjInfo;
#endif			// #if STANDALONE_MAC_STEAM_PLATFORM
#endif			// #if UNITY_IOS
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	/** 회사를 변경한다 */
	public void SetCompany(string a_oName) {
		m_oCompany = a_oName;
	}

	/** 개인 정보 URL 을 변경한다 */
	public void SetPrivacyURL(string a_oURL) {
		m_oPrivacyURL = a_oURL;
	}

	/** 서비스 URL 을 변경한다 */
	public void SetServicesURL(string a_oURL) {
		m_oServicesURL = a_oURL;
	}

	/** 프로젝트 이름을 변경한다 */
	public void SetProjName(string a_oName) {
		m_oProjName = a_oName;
	}

	/** 제품 이름을 변경한다 */
	public void SetProductName(string a_oName) {
		m_oProductName = a_oName;
	}

	/** 단순 제품 이름을 변경한다 */
	public void SetShortProductName(string a_oName) {
		m_oShortProductName = a_oName;
	}

	/** iOS 애플 프로젝트 정보를 변경한다 */
	public void SetiOSAppleProjInfo(STProjInfo a_stProjInfo) {
		m_stiOSAppleProjInfo = a_stProjInfo;
	}

	/** 안드로이드 구글 프로젝트 정보를 변경한다 */
	public void SetAndroidGoogleProjInfo(STProjInfo a_stProjInfo) {
		m_stAndroidGoogleProjInfo = a_stProjInfo;
	}

	/** 안드로이드 아마존 프로젝트 정보를 변경한다 */
	public void SetAndroidAmazonProjInfo(STProjInfo a_stProjInfo) {
		m_stAndroidAmazonProjInfo = a_stProjInfo;
	}
	
	/** 맥 스팀 프로젝트 정보를 변경한다 */
	public void SetStandaloneMacSteamProjInfo(STProjInfo a_stProjInfo) {
		m_stStandaloneMacSteamProjInfo = a_stProjInfo;
	}

	/** 윈도우즈 스팀 프로젝트 정보를 변경한다 */
	public void SetStandaloneWndsSteamProjInfo(STProjInfo a_stProjInfo) {
		m_stStandaloneWndsSteamProjInfo = a_stProjInfo;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
