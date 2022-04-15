using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
using AppleAuth;
using AppleAuth.Native;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
using GoogleSheetsToUnity;
#endif			// #if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)

/** 서비스 관리자 */
public partial class CServicesManager : CSingleton<CServicesManager> {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		INIT,
		[HideInInspector] MAX_VAL
	}

	/** 서비스 콜백 */
	private enum EServicesCallback {
		NONE = -1,
		LOAD_GOOGLE_SHEET,

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		LOGIN_WITH_APPLE,
		UPDATE_APPLE_LOGIN_STATE,
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public Dictionary<ECallback, System.Action<CServicesManager, bool>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;
	private Dictionary<EServicesCallback, System.Action<CServicesManager, bool>> m_oCallbackDictA = new Dictionary<EServicesCallback, System.Action<CServicesManager, bool>>();

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	private IAppleAuthManager m_oAppleAuthManager = null;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if (UNITY_STANDALONE && GOOGLE_SHEET_ENABLE) && (DEBUG || DEVELOPMENT_BUILD)
	private Dictionary<EServicesCallback, System.Action<CServicesManager, GstuSpreadSheet, STGoogleSheetInfo, bool>> m_oCallbackDictB = new Dictionary<EServicesCallback, System.Action<CServicesManager, GstuSpreadSheet, STGoogleSheetInfo, bool>>();
#endif			// #if (UNITY_STANDALONE && GOOGLE_SHEET_ENABLE) && (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 변수

	#region 프로퍼티
	public bool IsInit { get; private set; } = false;
	public bool IsAppleLogin { get; private set; } = false;

	public string AppleUserID {
		get {
#if UNITY_IOS && (APPLE_LOGIN_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE)
			return this.IsInit ? CCommonAppInfoStorage.Inst.AppInfo.AppleUserID : string.Empty;
#else
			return string.Empty;
#endif			// #if UNITY_IOS && (APPLE_LOGIN_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE)
		}
	}

	public string AppleIDToken {
		get {
#if UNITY_IOS && (APPLE_LOGIN_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE)
			return this.IsInit ? CCommonAppInfoStorage.Inst.AppInfo.AppleIDToken : string.Empty;
#else
			return string.Empty;
#endif			// #if UNITY_IOS && (APPLE_LOGIN_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE)
		}
	}
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		CFunc.ShowLog("CServicesManager.Init", KCDefine.B_LOG_COLOR_PLUGIN);
		
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		// 초기화 되었을 경우
		if(this.IsInit) {
			a_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.INIT)?.Invoke(this, true);
		} else {
			m_stParams = a_stParams;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
			// 애플 로그인을 지원 할 경우
			if(AppleAuthManager.IsCurrentPlatformSupported) {
				var oPayloadDeserializer = new PayloadDeserializer();
				m_oAppleAuthManager = new AppleAuthManager(oPayloadDeserializer);
			}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if SERVICES_ANALYTICS_ENABLE && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)
			Analytics.enabled = true;
			Analytics.limitUserTracking = true;
			Analytics.deviceStatsEnabled = true;
			Analytics.initializeOnStartup = true;
#else
			Analytics.enabled = false;
			Analytics.limitUserTracking = false;
			Analytics.deviceStatsEnabled = false;
			Analytics.initializeOnStartup = false;
#endif			// #if SERVICES_ANALYTICS_ENABLE && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)

			this.ExLateCallFunc((a_oSender) => this.OnInit());
		}
#else
		a_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.INIT)?.Invoke(this, false);
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_IOS || UNITY_ANDROID
	// 초기화 되었을 경우
	private void OnInit() {
		CFunc.ShowLog("CServicesManager.OnInit", KCDefine.B_LOG_COLOR_PLUGIN);

		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_SERVICES_M_INIT_CALLBACK, () => {
			this.IsInit = true;
			m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.INIT)?.Invoke(this, this.IsInit);
		});
	}

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	//! 상태를 갱신한다
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);
		m_oAppleAuthManager?.Update();
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
#endif			// #if UNITY_IOS || UNITY_ANDROID
	#endregion			// 조건부 함수
}