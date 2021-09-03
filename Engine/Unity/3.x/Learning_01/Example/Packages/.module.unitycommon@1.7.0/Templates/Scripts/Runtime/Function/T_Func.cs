﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if NEVER_USE_THIS
#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

//! 기본 함수
public static partial class Func {
	#region 클래스 변수
#if ADS_MODULE_ENABLE
	private static bool m_bIsWatchRewardAds = false;
	private static bool m_bIsWatchFullscreenAds = false;

	private static STAdsRewardItemInfo m_stRewardItemInfo;

	private static System.Action<CAdsManager, STAdsRewardItemInfo, bool> m_oRewardAdsCallback = null;
	private static System.Action<CAdsManager, bool> m_oFullscreenAdsCallback = null;
#endif			// #if ADS_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	private static System.Action<CFacebookManager, bool> m_oFacebookLoginCallback = null;
	private static System.Action<CFacebookManager> m_oFacebookLogoutCallback = null;
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	private static System.Action<CFirebaseManager, bool> m_oFirebaseLoginCallback = null;
	private static System.Action<CFirebaseManager> m_oFirebaseLogoutCallback = null;

	private static System.Action<CFirebaseManager, bool> m_oUserInfoSaveCallback = null;
	private static System.Action<CFirebaseManager, bool> m_oPostItemInfosSaveCallback = null;

	private static System.Action<CFirebaseManager, string, bool> m_oUserInfoLoadCallback = null;
	private static System.Action<CFirebaseManager, string, bool> m_oPostItemInfosLoadCallback = null;
#endif			// #if FIREBASE_MODULE_ENABLE

#if GAME_CENTER_MODULE_ENABLE
	private static System.Action<CGameCenterManager, bool> m_oGameCenterLoginCallback = null;
	private static System.Action<CGameCenterManager> m_oGameCenterLogoutCallback = null;

	private static System.Action<CGameCenterManager, bool> m_oScoreUpdateCallback = null;
	private static System.Action<CGameCenterManager, bool> m_oAchievementUpdateCallback = null;
#endif			// #if GAME_CENTER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	private static System.Action<CPurchaseManager, string, bool> m_oPurchaseCallback = null;
	private static System.Action<CPurchaseManager, List<Product>, bool> m_oRestoreCallback = null;
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 클래스 변수

	#region 클래스 함수
	//! 지역화 문자열을 설정한다
	public static void SetupLocalizeStrs() {
		string oLanguage = CCommonAppInfoStorage.Inst.AppInfo.Language.ToString();
		string oCountryCode = CCommonAppInfoStorage.Inst.CountryCode;

		Func.SetupLocalizeStrs(oLanguage, oCountryCode);
	}

	//! 지역화 문자열을 설정한다
	public static void SetupLocalizeStrs(string a_oLanguage, string a_oCountryCode, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCountryCode.ExIsValid());

		// 국가 코드가 존재 할 경우
		if(a_oCountryCode.ExIsValid()) {
			string oBasePath = KCDefine.U_BASE_TABLE_P_G_LOCALIZE_COMMON_STR;
			string oFilePath = CFactory.MakeLocalizePath(oBasePath, KCDefine.U_TABLE_P_G_ENGLISH_COMMON_STR, a_oLanguage, a_oCountryCode);
			
			CStrTable.Inst.LoadStrsFromRes(oFilePath);
		}
	}

	//! 경고 팝업을 출력한다
	public static void ShowAlertPopup(string a_oMsg, System.Action<CAlertPopup, bool> a_oCallback, bool a_bIsEnableCancelBtn = true) {
		var stParams = new CAlertPopup.STParams() {
			m_oTitle = CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_NOTI_TEXT),
			m_oMsg = a_oMsg,
			m_oOKBtnText = CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_OK_TEXT),
			m_oCancelBtnText = a_bIsEnableCancelBtn ? CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_CANCEL_TEXT) : string.Empty
		};

		var stCallbackParams = new CAlertPopup.STCallbackParams() {
			m_oCallback = a_oCallback
		};

		Func.ShowAlertPopup(stParams, stCallbackParams);
	}

	//! 경고 팝업을 출력한다
	public static void ShowAlertPopup(CAlertPopup.STParams a_stParams, CAlertPopup.STCallbackParams a_stCallbackParams) {
		// 경고 팝업이 없을 경우
		if(CSceneManager.ScreenPopupUIs.ExFindChild(KCDefine.U_OBJ_N_ALERT_POPUP) == null) {
			var oAlertPopup = CAlertPopup.Create<CAlertPopup>(KCDefine.U_OBJ_N_ALERT_POPUP, KCDefine.U_OBJ_P_G_ALERT_POPUP, CSceneManager.ScreenPopupUIs, a_stParams, a_stCallbackParams);
			oAlertPopup.Show(null, null);
		}
	}

	//! 토스트 팝업을 출력한다
	public static void ShowToastPopup(CToastPopup.STParams a_stParams) {
		Func.ShowToastPopup(a_stParams, Vector3.zero);
	}

	//! 토스트 팝업을 출력한다
	public static void ShowToastPopup(CToastPopup.STParams a_stParams, Vector3 a_stPos) {
		a_stParams.m_fDuration = a_stParams.m_fDuration.ExIsLessEquals(KCDefine.B_VAL_0_FLT) ? KCDefine.U_DURATION_TOAST_POPUP : a_stParams.m_fDuration;
		CToastPopupManager.Inst.Show(a_stParams, a_stPos);
	}

	//! 종료 팝업을 출력한다
	public static void ShowQuitPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_QUIT_P_MSG), a_oCallback);
	}
	
	//! 업데이트 팝업을 출력한다
	public static void ShowUpdatePopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_UPDATE_P_MSG), a_oCallback);
	}

	//! 로드 팝업을 출력한다
	public static void ShowLoadPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_LOAD_P_MSG), a_oCallback);
	}

	//! 저장 팝업을 출력한다
	public static void ShowSavePopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_SAVE_P_MSG), a_oCallback);
	}

	//! 로그인 성공 팝업을 출력한다
	public static void ShowLoginSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOGIN_SUCCESS_MSG), a_oCallback, false);
	}

	//! 로그인 실패 팝업을 출력한다
	public static void ShowLoginFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOGIN_SUCCESS_MSG), a_oCallback, false);
	}

	//! 로그아웃 성공 팝업을 출력한다
	public static void ShowLogoutSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOGOUT_SUCCESS_MSG), a_oCallback, false);
	}

	//! 로그아웃 실패 팝업을 출력한다
	public static void ShowLogoutFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOGOUT_SUCCESS_MSG), a_oCallback, false);
	}

	//! 로드 성공 팝업을 출력한다
	public static void ShowLoadSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOAD_SUCCESS_MSG), a_oCallback, false);
	}

	//! 로드 실패 팝업을 출력한다
	public static void ShowLoadFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_LOAD_FAIL_MSG), a_oCallback, false);
	}

	//! 저장 성공 팝업을 출력한다
	public static void ShowSaveSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_SAVE_SUCCESS_MSG), a_oCallback, false);
	}

	//! 저장 실패 팝업을 출력한다
	public static void ShowSaveFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_SAVE_FAIL_MSG), a_oCallback, false);
	}

	//! 결제 성공 팝업을 출력한다
	public static void ShowPurchaseSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_PURCHASE_SUCCESS_MSG), a_oCallback, false);
	}

	//! 결제 실패 팝업을 출력한다
	public static void ShowPurchaseFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_PURCHASE_FAIL_MSG), a_oCallback, false);
	}

	//! 복원 성공 팝업을 출력한다
	public static void ShowRestoreSuccessPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_RESTORE_SUCCESS_MSG), a_oCallback, false);
	}

	//! 복원 실패 팝업을 출력한다
	public static void ShowRestoreFailPopup(System.Action<CAlertPopup, bool> a_oCallback) {
		Func.ShowAlertPopup(CStrTable.Inst.GetStr(KCDefine.ST_KEY_COMMON_RESTORE_FAIL_MSG), a_oCallback, false);
	}

	//! 약관 동의 팝업을 출력한다
	public static void ShowAgreePopup(GameObject a_oParent, System.Action<CPopup> a_oInitCallback, System.Action<CPopup> a_oShowCallback = null, System.Action<CPopup> a_oCloseCallback = null) {
#if MODE_PORTRAIT_ENABLE
		string oObjPath = KCDefine.AS_OBJ_P_PORTRAIT_AGREE_POPUP;
#else
		string oObjPath = KCDefine.AS_OBJ_P_LANDSCAPE_AGREE_POPUP;
#endif			// #if MODE_PORTRAIT_ENABLE

		Func.ShowPopup<CAgreePopup>(KCDefine.AS_OBJ_N_AGREE_POPUP, oObjPath, a_oParent, a_oInitCallback, a_oShowCallback, a_oCloseCallback);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 팝업을 출력한다
	public static void ShowPopup<T>(string a_oName, string a_oObjPath, GameObject a_oParent, System.Action<CPopup> a_oInitCallback, System.Action<CPopup> a_oShowCallback = null, System.Action<CPopup> a_oCloseCallback = null) where T : CPopup {
		// 팝업이 없을 경우
		if(a_oParent.ExFindChild(a_oName) == null) {
			var oPopup = CPopup.Create<T>(a_oName, a_oObjPath, a_oParent);
			a_oInitCallback?.Invoke(oPopup);

			oPopup.Show(a_oShowCallback, a_oCloseCallback);
		}
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if ADS_MODULE_ENABLE
	//! 보상 광고를 출력한다
	public static void ShowRewardAds(System.Action<CAdsManager, STAdsRewardItemInfo, bool> a_oCallback) {
		Func.ShowRewardAds(CPluginInfoTable.Inst.DefAdsType, a_oCallback);
	}
	
	//! 보상 광고를 출력한다
	public static void ShowRewardAds(EAdsType a_eAdsType, System.Action<CAdsManager, STAdsRewardItemInfo, bool> a_oCallback) {
		// 보상 광고 출력이 가능 할 경우
		if(CAdsManager.Inst.IsLoadRewardAds(a_eAdsType)) {
			CIndicatorManager.Inst.Show(true);

			CSceneManager.RootSceneManager.ExLateCallFunc((a_oSender, a_oParams) => {
				Func.m_bIsWatchRewardAds = false;
				Func.m_stRewardItemInfo = KCDefine.U_INVALID_ADS_REWARD_ITEM_INFO;

				Func.m_oRewardAdsCallback = a_oCallback;
				CAdsManager.Inst.ShowRewardAds(a_eAdsType, Func.OnReceiveUserReward, Func.OnCloseRewardAds);
			});
		} else {
			a_oCallback?.Invoke(CAdsManager.Inst, KCDefine.U_INVALID_ADS_REWARD_ITEM_INFO, false);
		}
	}

	//! 전면 광고를 출력한다
	public static void ShowFullscreenAds(System.Action<CAdsManager, bool> a_oCallback) {
		Func.ShowFullscreenAds(CPluginInfoTable.Inst.DefAdsType, a_oCallback);
	}

	//! 전면 광고를 출력한다
	public static void ShowFullscreenAds(EAdsType a_eAdsType, System.Action<CAdsManager, bool> a_oCallback) {
		// 전면 광고 출력이 가능 할 경우
		if(CAppInfoStorage.Inst.IsEnableShowFullscreenAds && CAdsManager.Inst.IsLoadFullscreenAds(a_eAdsType)) {
			CIndicatorManager.Inst.Show(true);

			CSceneManager.RootSceneManager.ExLateCallFunc((a_oSender, a_oParams) => {
				// 전면 광고 출력이 가능 할 경우
				if(CAppInfoStorage.Inst.IsEnableShowFullscreenAds) {
					Func.m_bIsWatchFullscreenAds = true;
					Func.m_oFullscreenAdsCallback = a_oCallback;
					
					CAdsManager.Inst.ShowFullscreenAds(a_eAdsType, null, Func.OnCloseFullscreenAds);
				} else {
					CIndicatorManager.Inst.Close();
					a_oCallback?.Invoke(CAdsManager.Inst, false);
				}
			}, KCDefine.B_VAL_1_FLT, true);
		} else {
			// 광고 누적 횟수 갱신이 가능 할 경우
			if(CAppInfoStorage.Inst.IsEnableUpdateAdsSkipTimes) {
				CAppInfoStorage.Inst.AddAdsSkipTimes(KCDefine.B_VAL_1_INT);
			}

			a_oCallback?.Invoke(CAdsManager.Inst, false);
		}
	}

	//! 보상 광고가 닫혔을 경우
	private static void OnCloseRewardAds(CAdsManager a_oSender) {
		CIndicatorManager.Inst.Close();
		CAppInfoStorage.Inst.PrevRewardAdsTime = System.DateTime.Now;

		CAppInfoStorage.Inst.AddRewardAdsWatchTimes(KCDefine.B_VAL_1_INT);
		CAppInfoStorage.Inst.SaveAppInfo();

		CFunc.Invoke(ref Func.m_oRewardAdsCallback, a_oSender, Func.m_stRewardItemInfo, Func.m_bIsWatchRewardAds);
	}

	//! 유저 보상을 수신했을 경우
	private static void OnReceiveUserReward(CAdsManager a_oSender, STAdsRewardItemInfo a_stRewardItemInfo, bool a_bIsSuccess) {
		Func.m_bIsWatchRewardAds = a_bIsSuccess;
		Func.m_stRewardItemInfo = a_stRewardItemInfo;
	}

	//! 전면 광고가 닫혔을 경우
	private static void OnCloseFullscreenAds(CAdsManager a_oSender) {
		CIndicatorManager.Inst.Close();

		CAppInfoStorage.Inst.AdsSkipTimes = KCDefine.B_VAL_0_INT;
		CAppInfoStorage.Inst.PrevAdsTime = System.DateTime.Now;

		CAppInfoStorage.Inst.AddFullscreenAdsWatchTimes(KCDefine.B_VAL_1_INT);
		CAppInfoStorage.Inst.SaveAppInfo();
		
		CFunc.Invoke(ref Func.m_oFullscreenAdsCallback, a_oSender, Func.m_bIsWatchFullscreenAds);
	}
#endif			// #if ADS_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
	//! 페이스 북 로그인을 처리한다
	public static void FacebookLogin(System.Action<CFacebookManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oFacebookLoginCallback = a_oCallback;

		CFacebookManager.Inst.Login(KCDefine.U_PERMISSIONS_FACEBOOK.ToList(), Func.OnFacebookLogin);
	}

	//! 페이스 북 로그아웃을 처리한다
	public static void FacebookLogout(System.Action<CFacebookManager> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oFacebookLogoutCallback = a_oCallback;

		CFacebookManager.Inst.Logout(a_oCallback);
	}

	//! 페이스 북에 로그인 되었을 경우
	private static void OnFacebookLogin(CFacebookManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oFacebookLoginCallback, a_oSender, a_bIsSuccess);
	}

	//! 페이스 북에서 로그아웃 되었을 경우
	private static void OnFacebookLogout(CFacebookManager a_oSender) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oFacebookLogoutCallback, a_oSender);
	}
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
	//! 파이어 베이스 로그인을 처리한다
	public static void FirebaseLogin(System.Action<CFirebaseManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oFirebaseLoginCallback = a_oCallback;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		CServicesManager.Inst.LoginWithApple(Func.OnFirebaseAppleLogin);
#elif (UNITY_IOS || UNITY_ANDROID) && FACEBOOK_MODULE_ENABLE
		CFacebookManager.Inst.Login(KCDefine.U_PERMISSIONS_FACEBOOK.ToList(), Func.OnFirebaseFacebookLogin);
#else
		CFirebaseManager.Inst.Login(Func.OnFirebaseLogin);
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	}

	//! 파이어 베이스 로그아웃을 처리한다
	public static void FirebaseLogout(System.Action<CFirebaseManager> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oFirebaseLogoutCallback = a_oCallback;

#if UNITY_IOS && APPLE_LOGIN_ENABLE
		CServicesManager.Inst.LogoutWithApple(Func.OnFirebaseAppleLogout);
#elif (UNITY_IOS || UNITY_ANDROID) && FACEBOOK_MODULE_ENABLE
		CFacebookManager.Inst.Logout(Func.OnFirebaseFacebookLogout);
#else
		CFirebaseManager.Inst.Logout(Func.OnFirebaseLogout);
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	}

	//! 유저 정보를 로드한다
	public static void LoadUserInfo(System.Action<CFirebaseManager, string, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oUserInfoLoadCallback = a_oCallback;

		// 로그인 되었을 경우
		if(CFirebaseManager.Inst.IsLogin) {
			var oNodeList = Factory.MakeUserInfoNodes();
			CFirebaseManager.Inst.LoadDB(oNodeList, Func.OnLoadUserInfo);
		} else {
			Func.OnLoadUserInfo(CFirebaseManager.Inst, string.Empty, false);
		}
	}

	//! 지급 아이템 정보를 로드한다
	public static void LoadPostItemInfos(System.Action<CFirebaseManager, string, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oPostItemInfosLoadCallback = a_oCallback;

		// 로그인 되었을 경우
		if(CFirebaseManager.Inst.IsLogin) {
			var oNodeList = Factory.MakePostItemInfoNodes();
			CFirebaseManager.Inst.LoadDB(oNodeList, Func.OnLoadPostItemInfos);
		} else {
			Func.OnLoadPostItemInfos(CFirebaseManager.Inst, string.Empty, false);
		}
	}

	//! 유저 정보를 저장한다
	public static void SaveUserInfo(System.Action<CFirebaseManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oUserInfoSaveCallback = a_oCallback;

		// 로그인 되었을 경우
		if(CFirebaseManager.Inst.IsLogin) {
			var oNodeList = Factory.MakeUserInfoNodes();

			var oJSONNode = new SimpleJSON.JSONClass();
			oJSONNode.Add(KCDefine.B_KEY_JSON_USER_INFO_DATA, CUserInfoStorage.Inst.UserInfo.ExToMsgPackJSONStr());
			oJSONNode.Add(KCDefine.B_KEY_JSON_GAME_INFO_DATA, CGameInfoStorage.Inst.GameInfo.ExToMsgPackBase64Str());
			oJSONNode.Add(KCDefine.B_KEY_JSON_COMMON_APP_INFO_DATA, CCommonAppInfoStorage.Inst.AppInfo.ExToMsgPackJSONStr());
			oJSONNode.Add(KCDefine.B_KEY_JSON_COMMON_USER_INFO_DATA, CCommonUserInfoStorage.Inst.UserInfo.ExToMsgPackJSONStr());

			CFirebaseManager.Inst.SaveDB(oNodeList, oJSONNode.ToString(), Func.OnSaveUserInfo);
		} else {
			Func.OnSaveUserInfo(CFirebaseManager.Inst, false);
		}
	}

	//! 지급 아이템 정보를 저장한다
	public static void SavePostItemInfos(List<STPostItemInfo> a_oPostItemInfoList, System.Action<CFirebaseManager, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oPostItemInfoList != null);

		// 지급 아이템이 존재 할 경우
		if(a_oPostItemInfoList != null) {
			CIndicatorManager.Inst.Show(true);
			Func.m_oPostItemInfosSaveCallback = a_oCallback;

			// 로그인 되었을 경우
			if(CFirebaseManager.Inst.IsLogin) {
				var oNodeList = Factory.MakePostItemInfoNodes();
				CFirebaseManager.Inst.SaveDB(oNodeList, a_oPostItemInfoList.ExToJSONStr(true), Func.OnSavePostItemInfos);
			} else {
				Func.OnSavePostItemInfos(CFirebaseManager.Inst, false);
			}
		}
	}

	//! 파이어 베이스에 로그인 되었을 경우
	private static void OnFirebaseLogin(CFirebaseManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oFirebaseLoginCallback, a_oSender, a_bIsSuccess);
	}

	//! 파이어 베이스에서 로그아웃 되었을 경우
	private static void OnFirebaseLogout(CFirebaseManager a_oSender) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oFirebaseLogoutCallback, a_oSender);
	}

	//! 유저 정보가 로드 되었을 경우
	private static void OnLoadUserInfo(CFirebaseManager a_oSender, string a_oJSONStr, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oUserInfoLoadCallback, a_oSender, a_oJSONStr, a_bIsSuccess);
	}

	//! 지급 아이템 정보가 로드 되었을 경우
	private static void OnLoadPostItemInfos(CFirebaseManager a_oSender, string a_oJSONStr, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oPostItemInfosLoadCallback, a_oSender, a_oJSONStr, a_bIsSuccess);
	}

	//! 유저 정보가 저장 되었을 경우
	private static void OnSaveUserInfo(CFirebaseManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oUserInfoSaveCallback, a_oSender, a_bIsSuccess);
	}

	//! 지급 아이템 정보가 저장 되었을 경우
	private static void OnSavePostItemInfos(CFirebaseManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oPostItemInfosSaveCallback, a_oSender, a_bIsSuccess);
	}
	
#if UNITY_IOS && APPLE_LOGIN_ENABLE
	//! 애플에 로그인 되었을 경우
	private static void OnFirebaseAppleLogin(CServicesManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();

		// 로그인 되었을 경우
		if(a_bIsSuccess) {
			CIndicatorManager.Inst.Show(true);
			CFirebaseManager.Inst.LoginWithApple(a_oSender.AppleUserID, a_oSender.AppleIDToken, Func.OnFirebaseLogin);
		} else {
			Func.OnFirebaseLogin(CFirebaseManager.Inst, false);
		}
	}

	//! 애플에서 로그아웃 되었을 경우
	private static void OnFirebaseAppleLogout(CServicesManager a_oSender) {
		CFirebaseManager.Inst.Logout(Func.OnFirebaseLogout);
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if (UNITY_IOS || UNITY_ANDROID) && FACEBOOK_MODULE_ENABLE
	//! 페이스 북에 로그인 되었을 경우
	private static void OnFirebaseFacebookLogin(CFacebookManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();

		// 로그인 되었을 경우
		if(a_bIsSuccess) {
			CIndicatorManager.Inst.Show(true);
			CFirebaseManager.Inst.LoginWithFacebook(a_oSender.AccessToken, Func.OnFirebaseLogin);
		} else {
			Func.OnFirebaseLogin(CFirebaseManager.Inst, false);
		}
	}

	//! 페이스 북에서 로그아웃 되었을 경우
	private static void OnFirebaseFacebookLogout(CFacebookManager a_oSender) {
		CFirebaseManager.Inst.Logout(Func.OnFirebaseLogout);
	}
#endif			// #if (UNITY_IOS || UNITY_ANDROID) && FACEBOOK_MODULE_ENABLE
#endif			// #if FIREBASE_MODULE_ENABLE

#if GAME_CENTER_MODULE_ENABLE
	//! 게임 센터 로그인을 처리한다
	public static void GameCenterLogin(System.Action<CGameCenterManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oGameCenterLoginCallback = a_oCallback;

		CGameCenterManager.Inst.Login(Func.OnGameCenterLogin);
	}

	//! 게임 센터 로그아웃을 처리한다
	public static void GameCenterLogout(System.Action<CGameCenterManager> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oGameCenterLogoutCallback = a_oCallback;

		CGameCenterManager.Inst.Logout(Func.OnGameCenterLogout);
	}

	//! 게임 센터에 로그인 되었을 경우
	public static void UpdateScore(string a_oLeaderboardID, long a_nScore, System.Action<CGameCenterManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oScoreUpdateCallback = a_oCallback;

		CGameCenterManager.Inst.UpdateScore(a_oLeaderboardID, a_nScore, Func.OnUpdateScore);
	}

	//! 게임 센터에서 로그아웃 되었을 경우
	public static void UpdateAchievement(string a_oAchievementID, double a_dblPercent, System.Action<CGameCenterManager, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);
		Func.m_oAchievementUpdateCallback = a_oCallback;

		CGameCenterManager.Inst.UpdateAchievement(a_oAchievementID, a_dblPercent, Func.OnUpdateAchievement);
	}

	//! 게임 센터에 로그인 되었을 경우
	private static void OnGameCenterLogin(CGameCenterManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oGameCenterLoginCallback, a_oSender, a_bIsSuccess);
	}

	//! 게임 센터에서 로그아웃 되었을 경우
	private static void OnGameCenterLogout(CGameCenterManager a_oSender) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oGameCenterLogoutCallback, a_oSender);
	}

	//! 점수가 갱신 되었을 경우
	private static void OnUpdateScore(CGameCenterManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oScoreUpdateCallback, a_oSender, a_bIsSuccess);
	}

	//! 업적이 갱신 되었을 경우
	private static void OnUpdateAchievement(CGameCenterManager a_oSender, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oAchievementUpdateCallback, a_oSender, a_bIsSuccess);
	}
#endif			// #if GAME_CENTER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	//! 상품을 결제한다
	public static void PurchaseProduct(int a_nID, System.Action<CPurchaseManager, string, bool> a_oCallback) {
		var stProductInfo = CProductInfoTable.Inst.GetProductInfo(a_nID);
		Func.PurchaseProduct(stProductInfo.m_oID, a_oCallback);
	}

	//! 상품을 결제한다
	public static void PurchaseProduct(ESaleProductKinds a_eSaleProductKinds, System.Action<CPurchaseManager, string, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		int nID = Access.GetSaleProductID(a_eSaleProductKinds);
		CAccess.Assert(!a_bIsEnableAssert || KDefine.G_KINDS_SALE_PIT_SALE_PRODUCTS.ExIsValidIdx(nID));

		// 상품이 존재 할 경우
		if(KDefine.G_KINDS_SALE_PIT_SALE_PRODUCTS.ExIsValidIdx(nID)) {
			Func.PurchaseProduct(nID, a_oCallback);
		}
	}
	
	//! 상품을 결제한다
	public static void PurchaseProduct(string a_oID, System.Action<CPurchaseManager, string, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oID.ExIsValid());

		// 식별자가 유효 할 경우
		if(a_oID.ExIsValid()) {
			CIndicatorManager.Inst.Show(true);

			Func.m_oPurchaseCallback = a_oCallback;
			CPurchaseManager.Inst.PurchaseProduct(a_oID, Func.OnPurchaseProduct);
		}
	}

	//! 상품을 복원한다
	public static void RestoreProducts(System.Action<CPurchaseManager, List<Product>, bool> a_oCallback) {
		CIndicatorManager.Inst.Show(true);

		Func.m_oRestoreCallback = a_oCallback;
		CPurchaseManager.Inst.RestoreProducts(Func.OnRestoreProducts);
	}

	//! 상품이 결제 되었을 경우
	private static void OnPurchaseProduct(CPurchaseManager a_oSender, string a_oProductID, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();

		// 결제 되었을 경우
		if(a_bIsSuccess) {
			CIndicatorManager.Inst.Show(true);

			CPurchaseManager.Inst.ConfirmPurchase(a_oProductID, (a_oSender, a_oConfirmProductID, a_bIsSuccess) => {
				CIndicatorManager.Inst.Close();
				CFunc.Invoke(ref Func.m_oPurchaseCallback, a_oSender, a_oConfirmProductID, a_bIsSuccess);
			});
		} else {
			CFunc.Invoke(ref Func.m_oPurchaseCallback, a_oSender, a_oProductID, a_bIsSuccess);
		}
	}

	//! 상품이 복원 되었을 경우
	private static void OnRestoreProducts(CPurchaseManager a_oSender, List<Product> a_oProductList, bool a_bIsSuccess) {
		CIndicatorManager.Inst.Close();
		CFunc.Invoke(ref Func.m_oRestoreCallback, a_oSender, a_oProductList, a_bIsSuccess);
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수

	#region 추가 클래스 함수

	#endregion			// 추가 클래스 함수
}
#endif			// #if NEVER_USE_THIS
