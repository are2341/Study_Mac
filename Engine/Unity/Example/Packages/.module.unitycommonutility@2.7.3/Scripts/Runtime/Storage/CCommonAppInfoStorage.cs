﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if NEWTON_SOFT_JSON_MODULE_ENABLE
using MessagePack;
using Newtonsoft.Json;

/** 공용 앱 정보 */
[MessagePackObject][System.Serializable]
public partial class CCommonAppInfo : CCommonBaseInfo {
	#region 변수
	[Key(81)] public List<string> m_oSendLogList = new List<string>();
	#endregion			// 변수

	#region 상수
	private const string KEY_IS_AGREE = "IsAgree";
	private const string KEY_IS_AGREE_TRACKING = "IsAgreeTracking";

	private const string KEY_IS_FIRST_PLAY = "IsFirstPlay";
	private const string KEY_IS_ENABLE_SHOW_TRACKING_DP = "IsEnableShowTrackingDescPopup";

	private const string KEY_APP_RUNNING_TIMES = "AppRunningTimes";

	private const string KEY_DEVICE_ID = "DeviceID";
	private const string KEY_INSTALL_TIME = "InstallTime";
	private const string KEY_PREV_PLAY_TIME = "PrevPlayTime";

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	private const string KEY_APPLE_USER_ID = "AppleUserID";
	private const string KEY_APPLE_ID_TOKEN = "AppleIDToken";
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 상수

	#region 프로퍼티
	[JsonIgnore][IgnoreMember] public bool IsAgree { get { return bool.Parse(m_oStrDict.GetValueOrDefault(KEY_IS_AGREE, KCDefine.B_TEXT_FALSE)); } set { m_oStrDict.ExReplaceVal(KEY_IS_AGREE, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public bool IsAgreeTracking { get { return bool.Parse(m_oStrDict.GetValueOrDefault(KEY_IS_AGREE_TRACKING, KCDefine.B_TEXT_FALSE)); } set { m_oStrDict.ExReplaceVal(KEY_IS_AGREE_TRACKING, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public bool IsFirstPlay { get { return bool.Parse(m_oStrDict.GetValueOrDefault(KEY_IS_FIRST_PLAY, KCDefine.B_TEXT_FALSE)); } set { m_oStrDict.ExReplaceVal(KEY_IS_FIRST_PLAY, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public bool IsEnableShowTrackingDescPopup { get { return bool.Parse(m_oStrDict.GetValueOrDefault(KEY_IS_ENABLE_SHOW_TRACKING_DP, KCDefine.B_TEXT_FALSE)); } set { m_oStrDict.ExReplaceVal(KEY_IS_ENABLE_SHOW_TRACKING_DP, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public int AppRunningTimes { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_APP_RUNNING_TIMES, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_APP_RUNNING_TIMES, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public string DeviceID { get { return m_oStrDict.GetValueOrDefault(KEY_DEVICE_ID, string.Empty); } set { m_oStrDict.ExReplaceVal(KEY_DEVICE_ID, value); } }
	[JsonIgnore][IgnoreMember] public System.DateTime InstallTime { get { return this.InstallTimeStr.ExIsValid() ? this.CorrectInstallTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now; } set { m_oStrDict.ExReplaceVal(KEY_INSTALL_TIME, value.ExToLongStr()); } }
	[JsonIgnore][IgnoreMember] public System.DateTime PrevPlayTime { get { return this.PrevPlayTimeStr.ExIsValid() ? this.CorrectPrevPlayTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now; } set { m_oStrDict.ExReplaceVal(KEY_PREV_PLAY_TIME, value.ExToLongStr()); } }
	
	[JsonIgnore][IgnoreMember] public System.DateTime PSTInstallTime => InstallTime.ExToPSTTime();
	[JsonIgnore][IgnoreMember] public System.DateTime UTCInstallTime => InstallTime.ToUniversalTime();

	[JsonIgnore][IgnoreMember] private string InstallTimeStr => m_oStrDict.GetValueOrDefault(KEY_INSTALL_TIME, string.Empty);
	[JsonIgnore][IgnoreMember] private string PrevPlayTimeStr => m_oStrDict.GetValueOrDefault(KEY_PREV_PLAY_TIME, string.Empty);

	[JsonIgnore][IgnoreMember] private string CorrectInstallTimeStr => this.InstallTimeStr.Contains(KCDefine.B_TOKEN_SLASH) ? this.InstallTimeStr : this.InstallTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS).ExToLongStr();
	[JsonIgnore][IgnoreMember] private string CorrectPrevPlayTimeStr => this.PrevPlayTimeStr.Contains(KCDefine.B_TOKEN_SLASH) ? this.PrevPlayTimeStr : this.PrevPlayTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS).ExToLongStr();

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	[JsonIgnore][IgnoreMember] public string AppleUserID {
		get { return m_oStrDict.GetValueOrDefault(KEY_APPLE_USER_ID, string.Empty); }
		set { m_oStrDict.ExReplaceVal(KEY_APPLE_USER_ID, value); }
	}

	[JsonIgnore][IgnoreMember] public string AppleIDToken {
		get { return m_oStrDict.GetValueOrDefault(KEY_APPLE_ID_TOKEN, string.Empty); }
		set { m_oStrDict.ExReplaceVal(KEY_APPLE_ID_TOKEN, value); }
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 프로퍼티

	#region IMessagePackSerializationCallbackReceiver
	/** 직렬화 될 경우 */
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();
	}
	
	/** 역직렬화 되었을 경우 */
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();
		this.PrevPlayTime = System.DateTime.Now;

		m_oSendLogList = m_oSendLogList ?? new List<string>();

		// 버전이 다를 경우
		if(this.Ver.CompareTo(KCDefine.U_VER_COMMON_APP_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CCommonAppInfo() : base(KCDefine.U_VER_COMMON_APP_INFO) {
		// Do Something
	}
	#endregion			// 함수
}

/** 공용 앱 정보 저장소 */
public partial class CCommonAppInfoStorage : CSingleton<CCommonAppInfoStorage> {
	#region 프로퍼티
	public bool IsFirstStart { get; set; } = false;
	public bool IsValidStoreVer { get; private set; } = false;
	public bool IsSetupStoreVer { get; private set; } = false;

	public string Platform { get; private set; } = string.Empty;
	public string StoreVer { get; private set; } = string.Empty;

	public string StoreURL { get; set; } = string.Empty;
	public string CountryCode { get; set; } = string.Empty;

	public EDeviceType DeviceType { get; set; } = EDeviceType.NONE;
	public SystemLanguage SystemLanguage { get; set; } = SystemLanguage.Unknown;
	
	public CCommonAppInfo AppInfo { get; private set; } = new CCommonAppInfo() {
		IsFirstPlay = true, IsEnableShowTrackingDescPopup = true, InstallTime = System.DateTime.Now, PrevPlayTime = System.DateTime.Now
	};

#if PLAYFAB_MODULE_ENABLE
	public System.DateTime ServerTime { get; set; } = System.DateTime.Now;
#endif			// #if PLAYFAB_MODULE_ENABLE
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

#if UNITY_IOS
		this.Platform = CAccess.GetiOSName(EiOSType.APPLE);
#elif UNITY_ANDROID
#if ANDROID_AMAZON_PLATFORM
		var eAndroidType = EAndroidType.AMAZON;
#else
		var eAndroidType = EAndroidType.GOOGLE;
#endif			// #if ANDROID_AMAZON_PLATFORM

		this.Platform = CAccess.GetAndroidName(eAndroidType);
#elif UNITY_STANDALONE
#if STANDALONE_WNDS_STEAM_PLATFORM
		var eStandaloneType = EStandaloneType.WNDS_STEAM;
#else
		var eStandaloneType = EStandaloneType.MAC_STEAM;
#endif			// #if STANDALONE_MAC_STEAM_PLATFORM

		this.Platform = CAccess.GetStandaloneName(eStandaloneType);
#endif			// #if UNITY_IOS
	}

	/** 앱 정보를 리셋한다 */
	public void ResetAppInfo(string a_oJSONStr) {
		CFunc.ShowLog($"CCommonUserInfoStorage.ResetAppInfo: {a_oJSONStr}");
		CAccess.Assert(a_oJSONStr.ExIsValid());

		this.AppInfo = a_oJSONStr.ExMsgPackBase64StrToObj<CCommonAppInfo>();
		CAccess.Assert(this.AppInfo != null);
	}
	
	/** 스토어 버전을 설정한다 */
	public void SetupStoreVer() {
#if STORE_VER_CHECK_ENABLE
#if UNITY_ANDROID
		string oVer = $"{CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_nNum}";
#else
		string oVer = CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_oVer;
#endif			// #if UNITY_ANDROID

		CUnityMsgSender.Inst.SendGetStoreVerMsg(CProjInfoTable.Inst.GetAppID(CProjInfoTable.Inst.ProjInfo), oVer, KCDefine.U_TIMEOUT_NETWORK_CONNECTION, this.HandleGetStoreVerMsg);
#endif			// #if STORE_VER_CHECK_ENABLE
	}

	/** 약관 동의 필요 여부를 검사한다 */
	public bool IsNeedsAgree(string a_oCountryCode) {
		return a_oCountryCode.ToUpper().ExIsEU() || a_oCountryCode.ToUpper().Equals(KCDefine.B_KOREA_COUNTRY_CODE);
	}

	/** 업데이트 가능 여부를 검사한다 */
	public bool IsEnableUpdate() {
		return (this.IsSetupStoreVer && this.IsValidStoreVer) && this.IsEnableUpdate(this.StoreVer);
	}

	/** 업데이트 가능 여부를 검사한다 */
	public bool IsEnableUpdate(string a_oVer) {
#if UNITY_ANDROID
		int nBuildNum = KCDefine.B_VAL_0_INT;
		CAccess.Assert(int.TryParse(a_oVer, NumberStyles.Any, null, out nBuildNum));

		return this.IsEnableUpdateByBuildNum(nBuildNum);
#else
		return this.IsEnableUpdateByBuildVer(a_oVer);
#endif			// #if UNITY_ANDROID
	}

	/** 업데이트 가능 여부를 검사한다 */
	public bool IsEnableUpdate(STBuildVerInfo a_stBuildVerInfo) {
		return this.IsEnableUpdateByBuildNum(a_stBuildVerInfo.m_nNum) || this.IsEnableUpdateByBuildVer(a_stBuildVerInfo.m_oVer);
	}

	/** 업데이트 가능 여부를 검사한다 */
	public bool IsEnableUpdateByBuildNum(int a_nLatestNum) {
		CAccess.Assert(a_nLatestNum.ExIsValidBuildNum());
		CAccess.Assert(CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_nNum.ExIsValidBuildNum());

		return a_nLatestNum > CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_nNum;
	}

	/** 업데이트 가능 여부를 검사한다 */
	public bool IsEnableUpdateByBuildVer(string a_oVer) {
		var bIsValidVer01 = System.Version.TryParse(a_oVer, out System.Version oVer01);
		var bIsValidVer02 = System.Version.TryParse(CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_oVer, out System.Version oVer02);

		CAccess.Assert(bIsValidVer01 && bIsValidVer02);
		return oVer01.CompareTo(oVer02) >= KCDefine.B_COMPARE_GREATE;
	}

	/** 앱 실행 횟수를 추가한다 */
	public void AddAppRunningTimes(int a_nTimes) {
		this.AppInfo.AppRunningTimes = Mathf.Clamp(this.AppInfo.AppRunningTimes + a_nTimes, KCDefine.B_VAL_0_INT, int.MaxValue);
	}

	/** 앱 정보를 로드한다 */
	public CCommonAppInfo LoadAppInfo() {
		return this.LoadAppInfo(KCDefine.U_DATA_P_COMMON_APP_INFO);
	}

	/** 앱 정보를 로드한다 */
	public CCommonAppInfo LoadAppInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
#if MSG_PACK_ENABLE
			this.AppInfo = CFunc.ReadMsgPackObj<CCommonAppInfo>(a_oFilePath);
#else
			this.AppInfo = CFunc.ReadJSONObj<CCommonAppInfo>(a_oFilePath);
#endif			// #if MSG_PACK_ENABLE

			CAccess.Assert(this.AppInfo != null);
		}

		return this.AppInfo;
	}

	/** 앱 정보를 저장한다 */
	public void SaveAppInfo() {
		this.SaveAppInfo(KCDefine.U_DATA_P_COMMON_APP_INFO);
	}

	/** 앱 정보를 저장한다 */
	public void SaveAppInfo(string a_oFilePath) {
#if MSG_PACK_ENABLE
		CFunc.WriteMsgPackObj(a_oFilePath, this.AppInfo);
#else
		CFunc.WriteJSONObj(a_oFilePath, this.AppInfo);
#endif			// #if MSG_PACK_ENABLE
	}
	#endregion			// 함수

	#region 조건부 함수
#if STORE_VER_CHECK_ENABLE
	/** 스토어 버전 반환 메세지를 처리한다 */
	private void HandleGetStoreVerMsg(string a_oCmd, string a_oMsg) {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		var oDataDict = a_oMsg.ExJSONStrToObj<Dictionary<string, string>>();
		bool bIsValid = bool.TryParse(oDataDict[KCDefine.U_KEY_DEVICE_MR_RESULT], out bool bIsValidStoreVer);

		this.IsSetupStoreVer = true;
		this.IsValidStoreVer = bIsValid && bIsValidStoreVer;

		this.StoreVer = oDataDict[KCDefine.U_KEY_DEVICE_MR_VER];
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	}
#endif			// #if STORE_VER_CHECK_ENABLE
	#endregion			// 조건부 함수
}
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE