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
	private const string KEY_LANGUAGE = "Language";
	private const string KEY_DEVICE_ID = "DeviceID";
	private const string KEY_INSTALL_TIME = "InstallTime";
	private const string KEY_LAST_PLAY_TIME = "LastPlayTime";
	#endregion			// 상수

	#region 프로퍼티
	[IgnoreMember] public System.DateTime InstallTime { get; set; } = System.DateTime.Now;
	[IgnoreMember] public System.DateTime LastPlayTime { get; set; } = System.DateTime.Now;
	#endregion			// 프로퍼티

	#region 프로퍼티
	[IgnoreMember] public SystemLanguage Language {
		get { return (SystemLanguage)m_oIntList.ExGetValue(CCommonAppInfo.KEY_LANGUAGE, (int)SystemLanguage.Unknown); } 
		set { m_oIntList.ExReplaceValue(CCommonAppInfo.KEY_LANGUAGE, (int)value); }
	}

	[IgnoreMember] public string DeviceID {
		get { return m_oStringList.ExGetValue(CCommonAppInfo.KEY_DEVICE_ID, KCDefine.B_UNKNOWN_DEVICE_ID); } 
		set { m_oStringList.ExReplaceValue(CCommonAppInfo.KEY_DEVICE_ID, value); }
	}

	[IgnoreMember] public System.DateTime UTCInstallTime => this.InstallTime.ToUniversalTime();

	[IgnoreMember] private string InstallTimeString => m_oStringList.ExGetValue(CCommonAppInfo.KEY_INSTALL_TIME, string.Empty);
	[IgnoreMember] private string LastPlayTimeString => m_oStringList.ExGetValue(CCommonAppInfo.KEY_LAST_PLAY_TIME, string.Empty);
	#endregion			// 프로퍼티

	#region 인터페이스
	//! 직렬화 될 경우
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();

		m_oStringList.ExReplaceValue(CCommonAppInfo.KEY_INSTALL_TIME, this.InstallTime.ExToLongString());
		m_oStringList.ExReplaceValue(CCommonAppInfo.KEY_LAST_PLAY_TIME, this.LastPlayTime.ExToLongString());
	}

	//! 역직렬화 되었을 경우
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();
		
		this.InstallTime = this.InstallTime.ExIsValid() ? this.InstallTimeString.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now;
		this.LastPlayTime = this.LastPlayTime.ExIsValid() ? this.LastPlayTimeString.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Now;
	}
	#endregion			// 인터페이스

	#region 함수
	//! 생성자
	public CCommonAppInfo() : base(KCDefine.U_VERSION_COMMON_APP_INFO) {
		// Do Nothing
	}
	#endregion			// 함수
}

//! 공용 앱 정보 저장소
public class CCommonAppInfoStorage : CSingleton<CCommonAppInfoStorage> {
	#region 프로퍼티
	public bool IsEnableTracking { get; private set; } = false;
	public bool IsValidStoreVersion { get; private set; } = false;

	public bool IsSetupAdsID { get; private set; } = false;
	public bool IsSetupStoreVersion { get; private set; } = false;

	public string CountryCode { get; set; } = string.Empty;
	
	public string AdsID { get; private set; } = string.Empty;
	public string Platform { get; private set; } = string.Empty;
	public string StoreVersion { get; private set; } = string.Empty;

	public EDeviceType DeviceType { get; private set; } = EDeviceType.NONE;
	public STDeviceConfig DeviceConfig { get; set; }

	public CCommonAppInfo AppInfo { get; private set; } = new CCommonAppInfo();
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
		oAdsIDList.ExAddValue(KCDefine.U_ADS_ID_TEST_DEVICE);

#if UNITY_IOS
		oAdsIDList.ExAddValues(this.DeviceConfig.m_oiOSAdsIDList);
#elif UNITY_ANDROID
		oAdsIDList.ExAddValues(this.DeviceConfig.m_oAndroidAdsIDList);
#endif			// #if UNITY_IOS

		int nIdx = oAdsIDList.ExFindValue((a_oTestAdsID) => a_oAdsID.ExIsEquals(a_oTestAdsID));
		return nIdx > KCDefine.B_IDX_INVALID;
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate() {
		bool bIsEnable = this.IsSetupStoreVersion && this.IsValidStoreVersion;
		return bIsEnable && this.IsNeedUpdate(this.StoreVersion);
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate(string a_oLatestVersion) {
#if UNITY_ANDROID
		int nBuildNumber = KCDefine.B_VALUE_INT_0;
		CAccess.Assert(int.TryParse(a_oLatestVersion, out nBuildNumber));

		return this.IsNeedUpdateByBuildNumber(nBuildNumber);
#else
		return this.IsNeedUpdateByBuildVersion(a_oLatestVersion);
#endif			// #if UNITY_ANDROID
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdate(STBuildVersion a_stLatestBuildVersion) {
		bool bIsNeedUpdate = this.IsNeedUpdateByBuildNumber(a_stLatestBuildVersion.m_nNumber);
		return bIsNeedUpdate || this.IsNeedUpdateByBuildVersion(a_stLatestBuildVersion.m_oVersion);
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdateByBuildNumber(int a_nLatestNumber) {
		CAccess.Assert(a_nLatestNumber.ExIsValidBuildNumber());
		CAccess.Assert(CProjInfoTable.Inst.ProjInfo.m_stBuildVersion.m_nNumber.ExIsValidBuildNumber());

		return a_nLatestNumber > CProjInfoTable.Inst.ProjInfo.m_stBuildVersion.m_nNumber;
	}

	//! 업데이트 필요 여부를 검사한다
	public bool IsNeedUpdateByBuildVersion(string a_oLatestVersion) {
		System.Version oVersionA;
		System.Version oVersionB;

		var bIsValidVersionA = System.Version.TryParse(a_oLatestVersion, out oVersionA);
		var bIsValidVersionB = System.Version.TryParse(CProjInfoTable.Inst.ProjInfo.m_stBuildVersion.m_oVersion, out oVersionB);

		CAccess.Assert(bIsValidVersionA && bIsValidVersionB);
		return oVersionA.CompareTo(oVersionB) >= KCDefine.B_COMPARE_R_GREATE;
	}

	//! 디바이스 타입을 설정한다
	public void SetupDeviceType() {
		this.DeviceType = CAccess.GetDeviceType();
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
	public void SetupStoreVersion() {
#if UNITY_ANDROID
		string oVersion = CProjInfoTable.Inst.ProjInfo.m_stBuildVersion.m_nNumber.ToString();
#else
		string oVersion = CProjInfoTable.Inst.ProjInfo.m_stBuildVersion.m_oVersion;
#endif			// #if UNITY_ANDROID

		CUnityMsgSender.Inst.SendGetStoreVersionMsg(CProjInfoTable.Inst.ProjInfo.m_oAppID, oVersion, KCDefine.U_DEF_TIMEOUT_NETWORK_CONNECTION, this.HandleGetStoreVersionMsg);
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
		CFunc.ShowLog("CCommonAppInfoStorage.OnReceiveAdsID: {0}, {1}, {2}", KCDefine.B_LOG_COLOR_SETUP, a_oAdsID, a_bIsEnableTracking, a_oErrorMsg);

		// 광고 식별자가 유효 할 경우
		if(!a_oErrorMsg.ExIsValid()) {
			this.AdsID = a_oAdsID;
			this.IsSetupAdsID = true;
			this.IsEnableTracking = a_bIsEnableTracking;
		}
	}

	//! 스토어 버전 반환 메세지를 처리한다
	private void HandleGetStoreVersionMsg(string a_oCmd, string a_oMsg) {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		var oDataList = a_oMsg.ExJSONStringToObj<Dictionary<string, string>>();
		
		bool bIsValidStoreVersion = false;
		bool bIsEnableParse = bool.TryParse(oDataList[KCDefine.U_KEY_DEVICE_MR_RESULT], out bIsValidStoreVersion);

		this.IsSetupStoreVersion = true;
		this.StoreVersion = oDataList[KCDefine.U_KEY_DEVICE_MR_VERSION];

		this.IsValidStoreVersion = bIsEnableParse && bIsValidStoreVersion;
#endif			// #if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
	}
	#endregion			// 함수
}
