using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MessagePack;

//! 공용 앱 정보
[MessagePackObject]
[System.Serializable]
public sealed class CCommonAppInfo : CCommonBaseInfo {
	#region 상수
	private const string KEY_IS_AGREE = "IsAgree";
	private const string KEY_IS_AGREE_TRACKING = "IsAgreeTracking";

	private const string KEY_IS_FIRST_PLAY = "IsFirstPlay";
	private const string KEY_IS_ENABLE_SHOW_DESC_POPUP = "IsEnableShowDescPopup";

	private const string KEY_LANGUAGE = "Language";
	private const string KEY_DEVICE_ID = "DeviceID";
	private const string KEY_INSTALL_TIME = "InstallTime";
	private const string KEY_LAST_PLAY_TIME = "LastPlayTime";

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	private const string KEY_APPLE_USER_ID = "AppleUserID";
	private const string KEY_APPLE_ID_TOKEN = "AppleIDToken";
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 상수

	#region 프로퍼티
	[IgnoreMember] public System.DateTime InstallTime { get; set; } = System.DateTime.Now;
	[IgnoreMember] public System.DateTime LastPlayTime { get; set; } = System.DateTime.Now;

	[IgnoreMember] public bool IsAgree {
		get { return m_oBoolList.ExGetVal(CCommonAppInfo.KEY_IS_AGREE, false); } 
		set { m_oBoolList.ExReplaceVal(CCommonAppInfo.KEY_IS_AGREE, value); }
	}

	[IgnoreMember] public bool IsAgreeTracking {
		get { return m_oBoolList.ExGetVal(CCommonAppInfo.KEY_IS_AGREE_TRACKING, false); } 
		set { m_oBoolList.ExReplaceVal(CCommonAppInfo.KEY_IS_AGREE_TRACKING, value); }
	}

	[IgnoreMember] public bool IsFirstPlay {
		get { return m_oBoolList.ExGetVal(CCommonAppInfo.KEY_IS_FIRST_PLAY, false); }
		set { m_oBoolList.ExReplaceVal(CCommonAppInfo.KEY_IS_FIRST_PLAY, value); }
	}

	[IgnoreMember] public bool IsEnableShowDescPopup {
		get { return m_oBoolList.ExGetVal(CCommonAppInfo.KEY_IS_ENABLE_SHOW_DESC_POPUP, false); }
		set { m_oBoolList.ExReplaceVal(CCommonAppInfo.KEY_IS_ENABLE_SHOW_DESC_POPUP, value); }
	}
	
	[IgnoreMember] public SystemLanguage Language {
		get { return (SystemLanguage)m_oIntList.ExGetVal(CCommonAppInfo.KEY_LANGUAGE, (int)SystemLanguage.Unknown); } 
		set { m_oIntList.ExReplaceVal(CCommonAppInfo.KEY_LANGUAGE, (int)value); }
	}

	[IgnoreMember] public string DeviceID {
		get { return m_oStrList.ExGetVal(CCommonAppInfo.KEY_DEVICE_ID, string.Empty); } 
		set { m_oStrList.ExReplaceVal(CCommonAppInfo.KEY_DEVICE_ID, value); }
	}

	[IgnoreMember] public System.DateTime PSTInstallTime => this.InstallTime.ExToPSTTime();
	[IgnoreMember] public System.DateTime UTCInstallTime => this.InstallTime.ToUniversalTime();

	[IgnoreMember] private string InstallTimeStr => m_oStrList.ExGetVal(CCommonAppInfo.KEY_INSTALL_TIME, string.Empty);
	[IgnoreMember] private string LastPlayTimeStr => m_oStrList.ExGetVal(CCommonAppInfo.KEY_LAST_PLAY_TIME, string.Empty);

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	[IgnoreMember] public string AppleUserID {
		get { return m_oStrList.ExGetVal(CCommonAppInfo.KEY_APPLE_USER_ID, string.Empty); } 
		set { m_oStrList.ExReplaceVal(CCommonAppInfo.KEY_APPLE_USER_ID, value); }
	}

	[IgnoreMember] public string AppleIDToken {
		get { return m_oStrList.ExGetVal(CCommonAppInfo.KEY_APPLE_ID_TOKEN, string.Empty); } 
		set { m_oStrList.ExReplaceVal(CCommonAppInfo.KEY_APPLE_ID_TOKEN, value); }
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 프로퍼티

	#region 인터페이스
	//! 직렬화 될 경우
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();

		m_oStrList.ExReplaceVal(CCommonAppInfo.KEY_INSTALL_TIME, this.InstallTime.ExToLongStr());
		m_oStrList.ExReplaceVal(CCommonAppInfo.KEY_LAST_PLAY_TIME, this.LastPlayTime.ExToLongStr());
	}

	//! 역직렬화 되었을 경우
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();
		
		this.InstallTime = this.InstallTime.ExIsValid() ? this.InstallTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now;
		this.LastPlayTime = this.LastPlayTime.ExIsValid() ? this.LastPlayTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now;
	}
	#endregion			// 인터페이스

	#region 함수
	//! 생성자
	public CCommonAppInfo() : base(KCDefine.U_VER_COMMON_APP_INFO) {
		// Do Nothing
	}
	#endregion			// 함수
}

//! 공용 앱 정보 저장소
public class CCommonAppInfoStorage : CSingleton<CCommonAppInfoStorage> {
	#region 프로퍼티
	public bool IsFirstStart { get; set; } = false;
	public bool IsValidStoreVer { get; private set; } = false;

	public bool IsSetupAdsID { get; private set; } = false;
	public bool IsSetupStoreVer { get; private set; } = false;

	public string CountryCode { get; set; } = string.Empty;
	
	public string AdsID { get; private set; } = string.Empty;
	public string Platform { get; private set; } = string.Empty;
	public string StoreVer { get; private set; } = string.Empty;

	public EDeviceType DeviceType { get; private set; } = EDeviceType.NONE;
	public STDeviceConfig DeviceConfig { get; set; }
	
	public CCommonAppInfo AppInfo { get; private set; } = new CCommonAppInfo() {
		IsFirstPlay = true,
		IsEnableShowDescPopup = true
	};
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

#if UNITY_IOS
		this.Platform = KCDefine.B_PLATFORM_N_IOS;
#elif UNITY_ANDROID
#if ONE_STORE
		this.Platform = KCDefine.B_PLATFORM_N_ONE_STORE;
#elif GALAXY_STORE
		this.Platform = KCDefine.B_PLATFORM_N_GALAXY_STORE;
#else
		this.Platform = KCDefine.B_PLATFORM_N_GOOGLE;
#endif			// #if ONE_STORE
#elif UNITY_STANDALONE
#if UNITY_STANDALONE_WIN
		this.Platform = KCDefine.B_PLATFORM_N_WNDS;
#else
		this.Platform = KCDefine.B_PLATFORM_N_MAC;
#endif			// #if UNITY_STANDALONE_WIN
#endif			// #if UNITY_IOS
	}

	//! 약관 동의 필요 여부를 검사한다
	public bool IsNeedAgree(string a_oCountryCode) {
		string oCountryCode = a_oCountryCode.ToUpper();
		return oCountryCode.ExIsEU() || oCountryCode.ExIsEquals(KCDefine.B_KOREA_COUNTRY_CODE);
	}

	//! 테스트 디바이스 여부를 검사한다
	public bool IsTestDevice() {
		return this.IsSetupAdsID && this.IsTestDevice(this.AdsID);
	}
	
	//! 테스트 디바이스 여부를 검사한다
	public bool IsTestDevice(string a_oAdsID) {
		var oAdsIDList = new List<string>();
		oAdsIDList.ExAddVal(KCDefine.U_ADS_ID_TEST_DEVICE);

#if UNITY_IOS
		oAdsIDList.ExAddVals(this.DeviceConfig.m_oiOSAdsIDList);
#elif UNITY_ANDROID
		oAdsIDList.ExAddVals(this.DeviceConfig.m_oAndroidAdsIDList);
#endif			// #if UNITY_IOS

		int nIdx = oAdsIDList.ExFindVal((a_oTestAdsID) => a_oAdsID.ExIsEquals(a_oTestAdsID));
		return oAdsIDList.ExIsValidIdx(nIdx);
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate() {
		bool bIsEnable = this.IsSetupStoreVer && this.IsValidStoreVer;
		return bIsEnable && this.IsNeedUpdate(this.StoreVer);
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate(string a_oLatestVer) {
#if UNITY_ANDROID
		int nBuildNum = KCDefine.B_VAL_0_INT;
		CAccess.Assert(int.TryParse(a_oLatestVer, out nBuildNum));

		return this.IsNeedUpdateByBuildNum(nBuildNum);
#else
		return this.IsNeedUpdateByBuildVer(a_oLatestVer);
#endif			// #if UNITY_ANDROID
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate(STBuildVer a_stLatestBuildVer) {
		bool bIsNeedUpdate = this.IsNeedUpdateByBuildNum(a_stLatestBuildVer.m_nNum);
		return bIsNeedUpdate || this.IsNeedUpdateByBuildVer(a_stLatestBuildVer.m_oVer);
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdateByBuildNum(int a_nLatestNum) {
		CAccess.Assert(a_nLatestNum.ExIsValidBuildNum());
		CAccess.Assert(CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_nNum.ExIsValidBuildNum());

		return a_nLatestNum > CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_nNum;
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdateByBuildVer(string a_oLatestVer) {
		System.Version oVerA;
		System.Version oVerB;

		var bIsValidVerA = System.Version.TryParse(a_oLatestVer, out oVerA);
		var bIsValidVerB = System.Version.TryParse(CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_oVer, out oVerB);

		CAccess.Assert(bIsValidVerA && bIsValidVerB);
		return oVerA.CompareTo(oVerB) >= KCDefine.B_COMPARE_GREATE;
	}

	//! 디바이스 타입을 설정한다
	public void SetupDeviceType() {
		this.DeviceType = CAccess.DeviceType;
	}
	
	//! 광고 식별자를 설정한다
	public void SetupAdsID() {
		// 광고 식별자를 지원하지 않을 경우
		if(!Application.RequestAdvertisingIdentifierAsync(this.OnReceiveAdsID)) {
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			this.OnReceiveAdsID(KCDefine.U_ADS_ID_TEST_DEVICE, true, string.Empty);
#endif			// if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
		}
	}
	
	//! 스토어 버전을 설정한다
	public void SetupStoreVer() {
#if STORE_VER_CHECK_ENABLE
#if UNITY_ANDROID
		string oVer = string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_nNum);
#else
		string oVer = CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_oVer;
#endif			// #if UNITY_ANDROID

		CUnityMsgSender.Inst.SendGetStoreVerMsg(CProjInfoTable.Inst.ProjInfo.m_oAppID, oVer, KCDefine.U_TIMEOUT_NETWORK_CONNECTION, this.HandleGetStoreVerMsg);
#endif			// #if STORE_VER_CHECK_ENABLE
	}

	//! 앱 정보를 저장한다
	public void SaveAppInfo() {
		this.SaveAppInfo(KCDefine.U_DATA_P_COMMON_APP_INFO);
	}

	//! 앱 정보를 저장한다
	public void SaveAppInfo(string a_oFilePath) {
		CFunc.WriteMsgPackObj(a_oFilePath, this.AppInfo);
	}

	//! 앱 정보를 로드한다
	public CCommonAppInfo LoadAppInfo() {
		return this.LoadAppInfo(KCDefine.U_DATA_P_COMMON_APP_INFO);
	}

	//! 앱 정보를 로드한다
	public CCommonAppInfo LoadAppInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
			this.AppInfo = CFunc.ReadMsgPackObj<CCommonAppInfo>(a_oFilePath);
			CAccess.Assert(this.AppInfo != null);
		}

		return this.AppInfo;
	}

	//! 광고 식별자를 수신했을 경우
	private void OnReceiveAdsID(string a_oAdsID, bool a_bIsEnableTracking, string a_oErrorMsg) {
		CFunc.ShowLog($"CCommonAppInfoStorage.OnReceiveAdsID: {a_oAdsID}, {a_bIsEnableTracking}, {a_oErrorMsg}", KCDefine.B_LOG_COLOR_SETUP);

		// 광고 식별자가 유효 할 경우
		if(!a_oErrorMsg.ExIsValid()) {
			this.AdsID = a_oAdsID;
			this.IsSetupAdsID = true;
		}
	}
	#endregion			// 함수

	#region 조건부 함수
#if STORE_VER_CHECK_ENABLE
	//! 스토어 버전 반환 메세지를 처리한다
	private void HandleGetStoreVerMsg(string a_oCmd, string a_oMsg) {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		var oDataList = a_oMsg.ExJSONStrToObj<Dictionary<string, string>>();
		bool bIsValid = bool.TryParse(oDataList[KCDefine.U_KEY_DEVICE_MR_RESULT], out bool bIsValidStoreVer);

		this.IsSetupStoreVer = true;
		this.IsValidStoreVer = bIsValid && bIsValidStoreVer;

		this.StoreVer = oDataList[KCDefine.U_KEY_DEVICE_MR_VER];
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	}
#endif			// #if STORE_VER_CHECK_ENABLE
	#endregion			// 조건부 함수
}
