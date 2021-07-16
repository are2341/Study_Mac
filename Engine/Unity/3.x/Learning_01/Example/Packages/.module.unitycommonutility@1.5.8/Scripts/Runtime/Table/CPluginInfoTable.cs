using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ADS_MODULE_ENABLE
#if ADMOB_ENABLE
//! 애드 몹 플러그인 정보
[System.Serializable]
public struct STAdmobPluginInfo {
	[HideInInspector] public string m_oAppID;

	public string m_oBannerAdsID;
	public string m_oRewardAdsID;
	public string m_oFullscreenAdsID;
	public string m_oResumeAdsID;
}
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
//! 아이언 소스 플러그인 정보
[System.Serializable]
public struct STIronSrcPluginInfo {
	public string m_oAppKey;

	public string m_oBannerAdsID;
	public string m_oRewardAdsID;
	public string m_oFullscreenAdsID;
}
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
//! 앱 로빈 플러그인 정보
[System.Serializable]
public struct STAppLovinPluginInfo {
	public string m_oBannerAdsID;
	public string m_oRewardAdsID;
	public string m_oFullscreenAdsID;
}
#endif			// APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if SINGULAR_MODULE_ENABLE
//! 싱귤러 플러그인 정보
[System.Serializable]
public struct STSingularPluginInfo {
	public string m_oAPIKey;
    public string m_oAPISecret;
}
#endif			// #if SINGULAR_MODULE_ENABLE

//! 플러그인 정보 테이블
public class CPluginInfoTable : CScriptableObj<CPluginInfoTable> {
	#region 변수
#if ADS_MODULE_ENABLE
	[Header("Ads Plugin Info")]
	[SerializeField] private EAdsType m_eDefAdsType = EAdsType.NONE;
	[SerializeField] private EBannerAdsPos m_eBannerAdsPos = EBannerAdsPos.NONE;

#if APP_LOVIN_ENABLE
	[SerializeField] private string m_oAppLovinSDKKey = string.Empty;
#endif			// #if APP_LOVIN_ENABLE

#if ADMOB_ENABLE
	[SerializeField] private STAdmobPluginInfo m_stiOSAdmobPluginInfo;
	[SerializeField] private STAdmobPluginInfo m_stAndroidAdmobPluginInfo;
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	[SerializeField] private STIronSrcPluginInfo m_stiOSIronSrcPluginInfo;
	[SerializeField] private STIronSrcPluginInfo m_stAndroidIronSrcPluginInfo;
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	[SerializeField] private STAppLovinPluginInfo m_stiOSAppLovinPluginInfo;
	[SerializeField] private STAppLovinPluginInfo m_stAndroidAppLovinPluginInfo;
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
	[Header("Flurry Plugin Info")]
	[SerializeField] private string m_oiOSFlurryAPIKey = string.Empty;
	[SerializeField] private string m_oAndroidFlurryAPIKey = string.Empty;
#endif			// #if FLURRY_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE
	[Header("Firebase Plugin Info")]
	[SerializeField] private string m_oFirebaseDBURL = string.Empty;
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE

#if SINGULAR_MODULE_ENABLE
	[Header("Singular Plugin Info")]
	[SerializeField] private STSingularPluginInfo m_stSingularPluginInfo;
#endif			// #if SINGULAR_MODULE_ENABLE
	#endregion			// 변수

	#region 프로퍼티
#if ADS_MODULE_ENABLE
	public EAdsType DefAdsType => m_eDefAdsType;
	public EBannerAdsPos BannerAdsPos => m_eBannerAdsPos;

#if ADMOB_ENABLE
	public STAdmobPluginInfo AdmobPluginInfo { get; private set; }
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	public STIronSrcPluginInfo IronSrcPluginInfo { get; private set; }
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	public string AppLovinSDKKey => m_oAppLovinSDKKey;
	public STAppLovinPluginInfo AppLovinPluginInfo { get; private set; }
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
	public string FlurryAPIKey { get; private set; }
#endif			// #if FLURRY_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE
	public string FirebaseDBURL => m_oFirebaseDBURL;
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE

#if SINGULAR_MODULE_ENABLE
	public STSingularPluginInfo SingularPluginInfo => m_stSingularPluginInfo;
#endif			// #if SINGULAR_MODULE_ENABLE
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		
#if ADS_MODULE_ENABLE
#if ADMOB_ENABLE
		STAdmobPluginInfo stAdmobPluginInfo;

#if UNITY_IOS
		stAdmobPluginInfo = m_stiOSAdmobPluginInfo;
#else
		stAdmobPluginInfo = m_stAndroidAdmobPluginInfo;
#endif			// #if UNITY_IOS

#if ADS_TEST_ENABLE
		stAdmobPluginInfo.m_oBannerAdsID = KCDefine.U_ADS_ID_ADMOB_TEST_BANNER_ADS;
		stAdmobPluginInfo.m_oRewardAdsID = KCDefine.U_ADS_ID_ADMOB_TEST_REWARD_ADS;
		stAdmobPluginInfo.m_oFullscreenAdsID = KCDefine.U_ADS_ID_ADMOB_TEST_FULLSCREEN_ADS;
		stAdmobPluginInfo.m_oResumeAdsID = KCDefine.U_ADS_ID_ADMOB_TEST_RESUME_ADS;
#endif			// #if ADS_TEST_ENABLE

		this.AdmobPluginInfo = stAdmobPluginInfo;
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
#if UNITY_IOS
		this.IronSrcPluginInfo = m_stiOSIronSrcPluginInfo;
#else
		this.IronSrcPluginInfo = m_stAndroidIronSrcPluginInfo;
#endif			// #if UNITY_IOS
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
#if UNITY_IOS
		this.AppLovinPluginInfo = m_stiOSAppLovinPluginInfo;
#else
		this.AppLovinPluginInfo = m_stAndroidAppLovinPluginInfo;
#endif			// #if UNITY_IOS
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
#if UNITY_IOS
		this.FlurryAPIKey = m_oiOSFlurryAPIKey;
#else
		this.FlurryAPIKey = m_oAndroidFlurryAPIKey;
#endif			// #if UNITY_IOS
#endif			// #if FLURRY_MODULE_ENABLE
	}
	#endregion			// 함수

	#region 조건부 함수
#if ADS_MODULE_ENABLE
	//! 배너 광고 식별자를 반환한다
	public string GetBannerAdsID(EAdsType a_eAdsType) {
		CAccess.Assert(a_eAdsType.ExIsValid());

#if ADMOB_ENABLE
		// 애드 몹 일 경우
		if(a_eAdsType == EAdsType.ADMOB) {
			return this.AdmobPluginInfo.m_oBannerAdsID;
		}
#endif			// #if ADMOB_ENABLE
		
#if IRON_SRC_ENABLE
		// 아이언 소스 일 경우
		if(a_eAdsType == EAdsType.IRON_SRC) {
			return this.IronSrcPluginInfo.m_oBannerAdsID;
		}
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
		// 앱 로빈 일 경우
		if(a_eAdsType == EAdsType.APP_LOVIN) {
			return this.AppLovinPluginInfo.m_oBannerAdsID;
		}
#endif			// #if APP_LOVIN_ENABLE

		return string.Empty;
	}

	//! 보상 광고 식별자를 반환한다
	public string GetRewardAdsID(EAdsType a_eAdsType) {
		CAccess.Assert(a_eAdsType.ExIsValid());

#if ADMOB_ENABLE
		// 애드 몹 일 경우
		if(a_eAdsType == EAdsType.ADMOB) {
			return this.AdmobPluginInfo.m_oRewardAdsID;
		}
#endif			// #if ADMOB_ENABLE
		
#if IRON_SRC_ENABLE
		// 아이언 소스 일 경우
		if(a_eAdsType == EAdsType.IRON_SRC) {
			return this.IronSrcPluginInfo.m_oRewardAdsID;
		}
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
		// 앱 로빈 일 경우
		if(a_eAdsType == EAdsType.APP_LOVIN) {
			return this.AppLovinPluginInfo.m_oRewardAdsID;
		}
#endif			// #if APP_LOVIN_ENABLE

		return string.Empty;
	}

	//! 전면 광고 식별자를 반환한다
	public string GetFullscreenAdsID(EAdsType a_eAdsType) {
		CAccess.Assert(a_eAdsType.ExIsValid());

#if ADMOB_ENABLE
		// 애드 몹 일 경우
		if(a_eAdsType == EAdsType.ADMOB) {
			return this.AdmobPluginInfo.m_oFullscreenAdsID;
		}
#endif			// #if ADMOB_ENABLE
		
#if IRON_SRC_ENABLE
		// 아이언 소스 일 경우
		if(a_eAdsType == EAdsType.IRON_SRC) {
			return this.IronSrcPluginInfo.m_oFullscreenAdsID;
		}
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
		// 앱 로빈 일 경우
		if(a_eAdsType == EAdsType.APP_LOVIN) {
			return this.AppLovinPluginInfo.m_oFullscreenAdsID;
		}
#endif			// #if APP_LOVIN_ENABLE

		return string.Empty;
	}
#endif			// #if ADS_MODULE_ENABLE

#if UNITY_EDITOR
#if ADS_MODULE_ENABLE
	//! 기본 광고 타입을 변경한다
	public void SetDefAdsType(EAdsType a_eAdsType) {
		m_eDefAdsType = a_eAdsType;
	}

	//! 배너 광고 위치를 변경한다
	public void SetBannerAdsPos(EBannerAdsPos a_eAdsPos) {
		m_eBannerAdsPos = a_eAdsPos;
	}

#if ADMOB_ENABLE
	//! iOS 애드 몹 플러그인 정보를 변경한다
	public void SetiOSAdmobPluginInfo(STAdmobPluginInfo a_stPluginInfo) {
		m_stiOSAdmobPluginInfo = a_stPluginInfo;
	}

	//! 안드로이드 애드 몹 플러그인 정보를 변경한다
	public void SetAndroidAdmobPluginInfo(STAdmobPluginInfo a_stPluginInfo) {
		m_stAndroidAdmobPluginInfo = a_stPluginInfo;
	}
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	//! iOS 아이언 소스 플러그인 정보를 변경한다
	public void SetiOSIronSrcPluginInfo(STIronSrcPluginInfo a_stPluginInfo) {
		m_stiOSIronSrcPluginInfo = a_stPluginInfo;
	}

	//! 안드로이드 아이언 소스 플러그인 정보를 변경한다
	public void SetAndroidIronSrcPluginInfo(STIronSrcPluginInfo a_stPluginInfo) {
		m_stAndroidIronSrcPluginInfo = a_stPluginInfo;
	}
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	//! 앱 로빈 SDK 키를 변경한다
	public void SetAppLovinSDKKey(string a_oKey) {
		m_oAppLovinSDKKey = a_oKey;
	}

	//! iOS 앱 로빈 플러그인 정보를 변경한다
	public void SetiOSAppLovinPluginInfo(STAppLovinPluginInfo a_stPluginInfo) {
		m_stiOSAppLovinPluginInfo = a_stPluginInfo;
	}

	//! 안드로이드 앱 로빈 플러그인 정보를 변경한다
	public void SetAndroidAppLovinPluginInfo(STAppLovinPluginInfo a_stPluginInfo) {
		m_stAndroidAppLovinPluginInfo = a_stPluginInfo;
	}
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
	//! iOS 플러리 API 키를 변경한다
	public void SetiOSFlurryAPIKey(string a_oKey) {
		m_oiOSFlurryAPIKey = a_oKey;
	}

	//! 안드로이드 플러리 API 키를 변경한다
	public void SetAndroidFlurryAPIKey(string a_oKey) {
		m_oAndroidFlurryAPIKey = a_oKey;
	}
#endif			// #if FLURRY_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE
	//! 파이어 베이스 데이터 베이스 URL 을 변경한다
	public void SetFirebaseDBURL(string a_oURL) {
		m_oFirebaseDBURL = a_oURL;
	}
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE

#if SINGULAR_MODULE_ENABLE
	//! 싱귤러 플러그인 정보를 변경한다
	public void SetSingularPluginInfo(STSingularPluginInfo a_stPluginInfo) {
		m_stSingularPluginInfo = a_stPluginInfo;
	}
#endif			// #if SINGULAR_MODULE_ENABLE
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
