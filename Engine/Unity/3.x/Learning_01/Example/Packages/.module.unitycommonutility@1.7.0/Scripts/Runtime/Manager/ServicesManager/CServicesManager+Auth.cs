using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
using UnityEngine.SignInWithApple;
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

//! 서비스 관리자 - 인증
public partial class CServicesManager : CSingleton<CServicesManager> {
	#region 함수
	//! 애플 로그인 상태를 갱신한다
	public void UpdateAppleLoginState(System.Action<CServicesManager, bool> a_oCallback) {
		CFunc.ShowLog("CServicesManager.UpdateAppleLoginState", KCDefine.B_LOG_COLOR_PLUGIN);

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		try {
			// 애플 로그인을 지원 할 경우
			if(this.IsInit && CAccess.IsSupportsLoginWithApple) {
				m_oAppleLoginStateCallback = a_oCallback;
				m_oLoginWithApple.GetCredentialState(this.AppleUserID, this.OnUpdateAppleLoginState);
			} else {
				throw new System.Exception(KCDefine.B_UNKNOWN_SUPPORT_MSG);
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning("CServicesManager.UpdateAppleLoginState Exception: {0}", oException.Message);
			a_oCallback?.Invoke(this, false);
		}
#else
		a_oCallback?.Invoke(this, false);
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	}

	//! 애플 로그인을 처리한다
	public void LoginWithApple(System.Action<CServicesManager, bool> a_oCallback) {
		CFunc.ShowLog("CServicesManager.LoginWithApple", KCDefine.B_LOG_COLOR_PLUGIN);

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		try {
			// 애플 로그인을 지원 할 경우
			if(this.IsInit && CAccess.IsSupportsLoginWithApple) {
				m_oAppleLoginCallback = a_oCallback;
				m_oLoginWithApple.Login(this.OnLoginWithApple);
			} else {
				throw new System.Exception(KCDefine.B_UNKNOWN_SUPPORT_MSG);
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning("CServicesManager.LoginWithApple Exception: {0}", oException.Message);
			a_oCallback?.Invoke(this, false);
		}
#else
		a_oCallback?.Invoke(this, false);
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	}

	//! 애플 로그아웃을 처리한다
	public void LogoutWithApple(System.Action<CServicesManager> a_oCallback) {
		CFunc.ShowLog("CServicesManager.LogoutWithApple", KCDefine.B_LOG_COLOR_PLUGIN);

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		// 로그인 되었을 경우
		if(this.IsInit && this.IsAppleLogin) {
			this.IsAppleLogin = false;
		}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

		a_oCallback?.Invoke(this);
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_IOS && APPLE_LOGIN_ENABLE
	//! 애플 로그인 상태가 갱신 되었을 경우
	private void OnUpdateAppleLoginState(SignInWithApple.CallbackArgs a_stArgs) {
		this.IsAppleLogin = a_stArgs.ExIsValidCredentialState() && a_stArgs.credentialState == UserCredentialState.Authorized;
		CFunc.ShowLog($"CServicesManager.OnUpdateAppleLoginState: {this.IsAppleLogin}", KCDefine.B_LOG_COLOR_PLUGIN);

		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_SERVICES_M_UPDATE_APPLE_LOGIN_STATE_CALLBACK, () => {
			CCommonAppInfoStorage.Inst.AppInfo.AppleUserID = this.IsAppleLogin ? this.AppleUserID : string.Empty;
			CCommonAppInfoStorage.Inst.AppInfo.AppleIDToken = this.IsAppleLogin ? this.AppleIDToken : string.Empty;

			CCommonAppInfoStorage.Inst.SaveAppInfo();
			CFunc.Invoke(ref m_oAppleLoginStateCallback, this, this.IsAppleLogin);
		});
	}

	//! 애플에 로그인 되었을 경우
	private void OnLoginWithApple(SignInWithApple.CallbackArgs a_stArgs) {
		this.IsAppleLogin = a_stArgs.ExIsValidUserInfo();
		CFunc.ShowLog($"CServicesManager.OnLoginWithApple: {this.IsAppleLogin}", KCDefine.B_LOG_COLOR_PLUGIN);

		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_SERVICES_M_LOGIN_WITH_APPLE_CALLBACK, () => {
			CCommonAppInfoStorage.Inst.AppInfo.AppleUserID = this.IsAppleLogin ? a_stArgs.userInfo.userId : string.Empty;
			CCommonAppInfoStorage.Inst.AppInfo.AppleIDToken = this.IsAppleLogin ? a_stArgs.userInfo.idToken : string.Empty;

			CCommonAppInfoStorage.Inst.SaveAppInfo();
			CFunc.Invoke(ref m_oAppleLoginCallback, this, this.IsAppleLogin);
		});
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 조건부 함수
}
