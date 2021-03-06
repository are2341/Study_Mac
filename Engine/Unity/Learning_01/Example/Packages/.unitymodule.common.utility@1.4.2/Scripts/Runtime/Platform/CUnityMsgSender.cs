﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_IOS
using UnityEngine.iOS;
#elif UNITY_ANDROID
#if GOOGLE_REVIEW_ENABLE
using Google.Play.Review;
#endif			// #if GOOGLE_REVIEW_ENABLE

#if GOOGLE_UPDATE_ENABLE
using Google.Play.AppUpdate;
#endif			// #if GOOGLE_UPDATE_ENABLE
#endif			// #if UNITY_IOS

//! 유니티 메세지 전송자
public class CUnityMsgSender : CSingleton<CUnityMsgSender> {
	#region 변수
	private System.Action<NativeShare.ShareResult, string> m_oShareCallback = null;
	
#if !UNITY_EDITOR && UNITY_ANDROID
	private AndroidJavaClass m_oAndroidPlugin = new AndroidJavaClass(KCDefine.U_CLS_N_UNITY_MS_MSG_RECEIVER);
#endif			// #if !UNITY_EDITOR && UNITY_ANDROID
	#endregion			// 변수
	
	#region 함수
	//! 초기화 메세지를 전송한다
	public void SendInitMsg(string a_oBuildMode, EOrientation a_eOrientation) {
		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_BUILD_MODE] = a_oBuildMode,
			[KCDefine.U_KEY_UNITY_MS_ORIENTATION] = ((int)a_eOrientation).ToString()
		};

		this.SendUnityMsg(KCDefine.B_CMD_INIT, oDataList.ExToJSONStr(), null);
	}
	
	//! 디바이스 식별자 반환 메세지를 전송한다
	public void SendGetDeviceIDMsg(System.Action<string, string> a_oCallback) {
		this.SendUnityMsg(KCDefine.B_CMD_GET_DEVICE_ID, string.Empty, a_oCallback);
	}

	//! 국가 코드 반환 메세지를 전송한다
	public void SendGetCountryCodeMsg(System.Action<string, string> a_oCallback) {
		this.SendUnityMsg(KCDefine.B_CMD_GET_COUNTRY_CODE, string.Empty, a_oCallback);
	}

	//! 스토어 버전 반환 메세지를 전송한다
	public void SendGetStoreVerMsg(string a_oAppID, string a_oVer, float a_fTimeout, System.Action<string, string> a_oCallback) {
		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_APP_ID] = a_oAppID,
			[KCDefine.U_KEY_UNITY_MS_VER] = a_oVer,
			[KCDefine.U_KEY_UNITY_MS_TIMEOUT] = a_fTimeout.ToString()
		};

		this.SendUnityMsg(KCDefine.B_CMD_GET_STORE_VER, oDataList.ExToJSONStr(), a_oCallback);
	}

	//! 광고 추적 여부 변경 메세지를 전송한다
	public void SendSetEnableAdsTrackingMsg(bool a_bIsEnable) {
		this.SendUnityMsg(KCDefine.B_CMD_SET_ENABLE_ADS_TRACKING, a_bIsEnable.ToString(), null);
	}

	//! 경고 창 출력 메세지를 전송한다
	public void SendShowAlertMsg(string a_oTitle, string a_oMsg, string a_oOKBtnText, string a_oCancelBtnText, System.Action<string, string> a_oCallback) {
		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_ALERT_TITLE] = a_oTitle,
			[KCDefine.U_KEY_UNITY_MS_ALERT_MSG] = a_oMsg,
			[KCDefine.U_KEY_UNITY_MS_ALERT_OK_BTN_TEXT] = a_oOKBtnText,
			[KCDefine.U_KEY_UNITY_MS_ALERT_CANCEL_BTN_TEXT] = a_oCancelBtnText
		};

		this.SendUnityMsg(KCDefine.B_CMD_SHOW_ALERT, oDataList.ExToJSONStr(), a_oCallback);
	}

	//! 토스트 출력 메세지를 전송한다
	public void SendShowToastMsg(string a_oMsg) {
		this.SendUnityMsg(KCDefine.B_CMD_SHOW_TOAST, a_oMsg, null);
	}

	//! 동의 뷰 출력 메세지를 전송한다
	public void SendShowConsentViewMsg(System.Action<string, string> a_oCallback) {
#if UNITY_IOS
		this.SendUnityMsg(KCDefine.B_CMD_SHOW_CONSENT_VIEW, string.Empty, a_oCallback);
#else
		this.ExLateCallFunc((a_oSender, a_oParams) => a_oCallback?.Invoke(KCDefine.B_CMD_SHOW_CONSENT_VIEW, KCDefine.B_TRUE_STR));
#endif			// #if UNITY_IOS
	}

	//! 평가 창 출력 메세지를 전송한다
	public void SendShowReviewMsg() {
		CFunc.ShowLog("CUnityMsgSender.SendShowReviewMsg", KCDefine.B_LOG_COLOR_PLUGIN);

#if UNITY_IOS
		// 스토어 평가를 지원하지 않을 경우
		if(!Device.RequestStoreReview()) {	
			CFunc.OpenURL(CProjInfoTable.Inst.ProjInfo.m_oStoreURL);
		}
#elif UNITY_ANDROID && GOOGLE_REVIEW_ENABLE
		var oReviewManager = new ReviewManager();
		StartCoroutine(CTaskManager.Inst.WaitReviewFlow(oReviewManager, this.HandleShowReviewMsg));
#endif			// #if UNITY_IOS
	}

	//! 진동 메세지를 전송한다
	public void SendVibrateMsg(EVibrateType a_eType, EVibrateStyle a_eStyle, float a_fDuration, float a_fIntensity) {
		bool bIsEnableType = a_eType > EVibrateType.NONE && a_eType < EVibrateType.MAX_VAL;
		bool bIsEnableStyle = a_eStyle > EVibrateStyle.NONE && a_eStyle < EVibrateStyle.MAX_VAL;

		CAccess.Assert(bIsEnableType && bIsEnableStyle);
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_VIBRATE_TYPE] = ((int)a_eType).ToString(),
			[KCDefine.U_KEY_UNITY_MS_VIBRATE_STYLE] = ((int)a_eStyle).ToString(),
			[KCDefine.U_KEY_UNITY_MS_VIBRATE_DURATION] = a_fDuration.ToString(),
			[KCDefine.U_KEY_UNITY_MS_VIBRATE_INTENSITY] = a_fIntensity.ToString()
		};

		this.SendUnityMsg(KCDefine.B_CMD_VIBRATE, oDataList.ExToJSONStr(), null);
	}

	//! 추적 메세지를 전송한다
	public void SendTrackingMsg(string a_oName, Dictionary<string, string> a_oDataList, bool a_bIsStart) {
		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_TRACKING_NAME] = a_oName,
			[KCDefine.U_KEY_UNITY_MS_TRACKING_DATAS] = (a_oDataList != null) ? a_oDataList.ExToJSONStr() : string.Empty,
			[KCDefine.U_KEY_UNITY_MS_TRACKING_IS_START] = a_bIsStart.ToString()
		};

		this.SendUnityMsg(KCDefine.B_CMD_TRACKING, oDataList.ExToJSONStr(), null);
	}

	//! 인디케이터 메세지를 전송한다
	public void SendIndicatorMsg(bool a_bIsShow) {
		this.SendUnityMsg(KCDefine.B_CMD_INDICATOR, a_bIsShow.ToString(), null);
	}

	//! 광고 초기화 메세지를 전송한다
	public void SendInitAdsMsg(string a_oResumeAdsID, List<string> a_oAdmobIDList, System.Action<string, string> a_oCallback) {
		string oAdmobIDs = string.Empty;

		// 애드 몹 식별자가 유효 할 경우
		if(a_oAdmobIDList.ExIsValid()) {
			var oAdmobIDList = new SimpleJSON.JSONArray();

			for(int i = 0; i < a_oAdmobIDList.Count; ++i) {
				oAdmobIDList.Add(a_oAdmobIDList[i]);
			}

			oAdmobIDs = oAdmobIDList.ToString();
		}

		var oDataList = new Dictionary<string, string>() {
			[KCDefine.U_KEY_UNITY_MS_RESUME_ADS_ID] = a_oResumeAdsID,
			[KCDefine.U_KEY_UNITY_MS_ADMOB_IDS] = oAdmobIDs
		};

		this.SendUnityMsg(KCDefine.B_CMD_INIT_ADS, oDataList.ExToJSONStr(), a_oCallback);
	}

	//! 재개 광고 로드 메세지를 전송한다
	public void SendLoadResumeAdsMsg(System.Action<string, string> a_oCallback) {
		this.SendUnityMsg(KCDefine.B_CMD_LOAD_RESUME_ADS, string.Empty, a_oCallback);
	}

	//! 재개 광고 출력 메세지를 전송한다
	public void SendShowResumeAdsMsg(System.Action<string, string> a_oCallback) {
		this.SendUnityMsg(KCDefine.B_CMD_SHOW_RESUME_ADS, string.Empty, a_oCallback);
	}

	//! 메일 메세지를 전송한다
	public void SendMailMsg(string a_oRecipient, string a_oTitle, string a_oMsg) {
		CFunc.ShowLog($"CUnityMsgSender.SendMailMsg: {a_oRecipient}, {a_oTitle}, {a_oMsg}", KCDefine.B_LOG_COLOR_PLUGIN);
		var oGraphicsAPI = SystemInfo.graphicsDeviceType;

		string oApp = CProjInfoTable.Inst.ShortProductName;
		string oVer = CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_oVer;
		string oPlatform = CCommonAppInfoStorage.Inst.Platform;
		string oProcessor = SystemInfo.processorType;
		string oGraphics = SystemInfo.graphicsDeviceName;
		string oOS = SystemInfo.operatingSystem;
		string oUserID = CCommonAppInfoStorage.Inst.AppInfo.DeviceID;

		string oMsg = string.Format(KCDefine.B_MAIL_MSG_FMT, oApp, oVer, oPlatform, oProcessor, oGraphics, oGraphicsAPI, oOS, oUserID, a_oMsg);
		string oURL = string.Format(KCDefine.B_MAIL_URL_FMT, a_oRecipient, System.Uri.EscapeUriString(a_oTitle), System.Uri.EscapeUriString(oMsg));

		CFunc.OpenURL(oURL);
	}

	//! 공유 메세지를 전송한다
	public void SendShareMsg(string a_oMsg, System.Action<NativeShare.ShareResult, string> a_oCallback, string a_oFilePath = KCDefine.B_EMPTY_STR) {
		CFunc.ShowLog($"CUnityMsgSender.SendShareMsg: {a_oMsg}", KCDefine.B_LOG_COLOR_PLUGIN);

#if UNITY_IOS || UNITY_ANDROID
		m_oShareCallback = a_oCallback;

		var oShare = new NativeShare();
		oShare.SetText(a_oMsg);
		oShare.SetCallback(this.HandleShareMsg);

		// 파일 경로가 유효 할 경우
		if(a_oFilePath.ExIsValid()) {
			oShare.AddFile(a_oFilePath);
		}

		oShare.Share();
#else
		a_oCallback?.Invoke(NativeShare.ShareResult.NotShared, KCDefine.B_UNKNOWN_ERROR_MSG);
#endif			// #if UNITY_IOS || UNITY_ANDROID
	}

	//! 공유 메세지를 처리한다
	private void HandleShareMsg(NativeShare.ShareResult a_eResult, string a_oTarget) {
		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_UNITY_MS_SHARE_MSG_CALLBACK, () => {
			CFunc.ShowLog($"CUnityMsgSender.HandleShareMsg: {a_eResult}, {a_oTarget}", KCDefine.B_LOG_COLOR_PLUGIN);
			CFunc.Invoke(ref m_oShareCallback, a_eResult, a_oTarget);
		});
	}

	//! 유니티 메세지를 전송한다
	private void SendUnityMsg(string a_oCmd, string a_oMsg, System.Action<string, string> a_oCallback) {
		// 인디케이터 메세지가 아닐 경우
		if(!a_oCmd.ExIsEquals(KCDefine.B_CMD_INDICATOR)) {
			CFunc.ShowLog($"CUnityMsgSender.SendUnityMsg: {a_oCmd}, {a_oMsg}", KCDefine.B_LOG_COLOR_PLUGIN);
		}
		
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		// 콜백이 유효 할 경우
		if(a_oCallback != null) {
			CDeviceMsgReceiver.Inst.AddCallback(a_oCmd, a_oCallback);
		}

#if UNITY_IOS
		CUnityMsgSender.HandleUnityMsg(a_oCmd, a_oMsg);
#else
		// 스토어 버전 반환 메세지 일 경우
		if(a_oCmd == KCDefine.B_CMD_GET_STORE_VER) {
			var oUpdateManager = new AppUpdateManager();
			StartCoroutine(CTaskManager.Inst.WaitUpdateOperation(oUpdateManager, this.HandleGetStoreVerMsg));
		} else {
			m_oAndroidPlugin.CallStatic(KCDefine.U_FUNC_N_UNITY_MS_MSG_HANDLER, a_oCmd, a_oMsg);
		}
#endif			// #if UNITY_IOS
#else
		this.ExLateCallFunc((a_oSender, a_oParams) => a_oCallback?.Invoke(a_oCmd, string.Empty));
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_ANDROID
#if GOOGLE_REVIEW_ENABLE
	//! 평가 창 출력 메세지를 처리한다
	private void HandleShowReviewMsg(CTaskManager a_oSender, ReviewErrorCode a_eErrorCode) {
		CFunc.ShowLog($"CUnityMsgSender.HandleShowReviewMsg: {a_eErrorCode}", KCDefine.B_LOG_COLOR_PLUGIN);

		// 스토어 평가를 지원하지 않을 경우		
		if(a_eErrorCode != ReviewErrorCode.NoError) {
			CFunc.OpenURL(CProjInfoTable.Inst.ProjInfo.m_oStoreURL);
		}
	}
#endif			// #if GOOGLE_REVIEW_ENABLE

#if GOOGLE_UPDATE_ENABLE
	//! 스토어 버전 반환 메세지를 처리한다
	private void HandleGetStoreVerMsg(CTaskManager a_oSender, int a_nVer, bool a_bIsSuccess) {
		CFunc.ShowLog($"CUnityMsgSender.HandleGetStoreVerMsg: {a_nVer}, {a_bIsSuccess}", KCDefine.B_LOG_COLOR_PLUGIN);
		string oVer = string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, a_nVer);

		var oMsg = new SimpleJSON.JSONClass();
		oMsg.Add(KCDefine.U_KEY_DEVICE_MR_VER, oVer);
		oMsg.Add(KCDefine.U_KEY_DEVICE_MR_RESULT, a_bIsSuccess.ToString());

		var oDeviceMsg = new SimpleJSON.JSONClass();
		oDeviceMsg.Add(KCDefine.U_KEY_DEVICE_CMD, KCDefine.B_CMD_GET_STORE_VER);
		oDeviceMsg.Add(KCDefine.U_KEY_DEVICE_MSG, oMsg.ToString());
		
		CDeviceMsgReceiver.Inst.SendMessage(KCDefine.U_FUNC_N_DEVICE_MR_MSG_HANDLER, oDeviceMsg.ToString(), SendMessageOptions.DontRequireReceiver);
	}
#endif			// #if GOOGLE_UPDATE_ENABLE
#endif			// #if UNITY_ANDROID
	#endregion			// 조건부 함수

	#region 조건부 클래스 함수
#if !UNITY_EDITOR && UNITY_IOS
	[DllImport("__Internal")]
	private static extern void HandleUnityMsg(string a_oCmd, string a_oMsg);
#endif			// #if !UNITY_EDITOR && UNITY_IOS
	#endregion			// 조건부 클래스 함수
}
