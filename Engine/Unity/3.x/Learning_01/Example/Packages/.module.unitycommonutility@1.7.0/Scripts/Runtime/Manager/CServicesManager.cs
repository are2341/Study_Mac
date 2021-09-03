using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
using UnityEngine.SignInWithApple;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

//! 서비스 관리자
public partial class CServicesManager : CSingleton<CServicesManager> {
	//! 콜백 매개 변수
	public struct STCallbackParams {
		public System.Action<CServicesManager, bool> m_oCallback;
	}

	#region 변수
	private STCallbackParams m_stCallbackParams;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	private SignInWithApple m_oLoginWithApple = null;

	private System.Action<CServicesManager, bool> m_oAppleLoginCallback = null;
	private System.Action<CServicesManager, bool> m_oAppleLoginStateCallback = null;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 변수

	#region 프로퍼티
	public bool IsInit { get; private set; } = false;
	public bool IsAppleLogin { get; private set; } = false;

	public string AppleUserID {
		get {
#if UNITY_IOS && APPLE_LOGIN_ENABLE
			return this.IsInit ? CCommonAppInfoStorage.Inst.AppInfo.AppleUserID : string.Empty;
#else
			return string.Empty;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
		}
	}

	public string AppleIDToken {
		get {
#if UNITY_IOS && APPLE_LOGIN_ENABLE
			return this.IsInit ? CCommonAppInfoStorage.Inst.AppInfo.AppleIDToken : string.Empty;
#else
			return string.Empty;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
		}
	}
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		m_oLoginWithApple = CFactory.CreateObj<SignInWithApple>(KCDefine.U_OBJ_N_SERVICES_M_LOGIN_WITH_APPLE, this.gameObject);
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	}

	//! 초기화
	public virtual void Init(STCallbackParams a_stCallbackParams) {
		CFunc.ShowLog("CServicesManager.Init", KCDefine.B_LOG_COLOR_PLUGIN);
		
#if UNITY_IOS || UNITY_ANDROID
		// 초기화 되었을 경우
		if(this.IsInit) {
			a_stCallbackParams.m_oCallback?.Invoke(this, true);
		} else {
			m_stCallbackParams = a_stCallbackParams;

#if SERVICES_ANALYTICS_ENABLE && (ANALYTICS_TEST_ENABLE || ADHOC_BUILD || STORE_BUILD)
			Analytics.enabled = true;
			Analytics.limitUserTracking = true;
			Analytics.deviceStatsEnabled = true;
			Analytics.initializeOnStartup = true;
#else
			Analytics.enabled = false;
			Analytics.limitUserTracking = false;
			Analytics.deviceStatsEnabled = false;
			Analytics.initializeOnStartup = false;
#endif			// #if SERVICES_ANALYTICS_ENABLE && (ANALYTICS_TEST_ENABLE || ADHOC_BUILD || STORE_BUILD)

			this.ExLateCallFunc((a_oSender, a_oParams) => this.OnInit());
		}
#else
		a_stCallbackParams.m_oCallback?.Invoke(this, false);
#endif			// #if UNITY_IOS || UNITY_ANDROID
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_IOS || UNITY_ANDROID
	// 초기화 되었을 경우
	private void OnInit() {
		CFunc.ShowLog("CServicesManager.OnInit", KCDefine.B_LOG_COLOR_PLUGIN);

		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_SERVICES_M_INIT_CALLBACK, () => {	
			this.IsInit = true;
			CFunc.Invoke(ref m_stCallbackParams.m_oCallback, this, this.IsInit);
		});
	}
#endif			// #if UNITY_IOS || UNITY_ANDROID
	#endregion			// 조건부 함수
}
