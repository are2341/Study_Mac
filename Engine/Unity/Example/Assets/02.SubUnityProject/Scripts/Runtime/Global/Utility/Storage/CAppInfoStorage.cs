using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
using MessagePack;
using Newtonsoft.Json;

/** 앱 정보 */
[MessagePackObject][System.Serializable]
public partial class CAppInfo : CBaseInfo {
	#region 상수
#if ADS_MODULE_ENABLE
	private const string KEY_REWARD_ADS_WATCH_TIMES = "RewardAdsWatchTimes";
	private const string KEY_FULLSCREEN_ADS_WATCH_TIMES = "FullscreenAdsWatchTimes";
#endif			// #if ADS_MODULE_ENABLE
	#endregion			// 상수

	#region 프로퍼티
#if ADS_MODULE_ENABLE
	[JsonIgnore][IgnoreMember] public int RewardAdsWatchTimes { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_REWARD_ADS_WATCH_TIMES, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_REWARD_ADS_WATCH_TIMES, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public int FullscreenAdsWatchTimes { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_FULLSCREEN_ADS_WATCH_TIMES, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_FULLSCREEN_ADS_WATCH_TIMES, $"{value}"); } }
#endif			// #if ADS_MODULE_ENABLE
	#endregion			// 프로퍼티

	#region IMessagePackSerializationCallbackReceiver
	/** 직렬화 될 경우 */
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();
	}

	/** 역직렬화 되었을 경우 */
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();

		// 버전이 다를 경우
		if(this.Ver.CompareTo(KDefine.G_VER_APP_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CAppInfo() : base(KDefine.G_VER_APP_INFO) {
		// Do Something
	}
	#endregion			// 함수
}

/** 앱 정보 저장소 */
public partial class CAppInfoStorage : CSingleton<CAppInfoStorage> {
	#region 프로퍼티
	public bool IsIgnoreUpdate { get; set; } = false;
	public bool IsCloseAgreePopup { get; set; } = false;
	
	public CAppInfo AppInfo { get; private set; } = new CAppInfo();

#if ADS_MODULE_ENABLE
	public int AdsSkipTimes { get; set; } = KCDefine.B_VAL_0_INT;
	public System.DateTime PrevAdsTime { get; set; } = System.DateTime.Now;
	public System.DateTime PrevRewardAdsTime { get; set; } = System.DateTime.Now;

	public bool IsEnableShowFullscreenAds {
		get {
			double dblAdsDelay = CValTable.Inst.GetReal(KCDefine.VT_KEY_DELAY_ADS);
			double dblAdsDeltaTime = CValTable.Inst.GetReal(KCDefine.VT_KEY_DELTA_T_ADS);

			double dblDeltaTime01 = System.DateTime.Now.ExGetDeltaTime(CAppInfoStorage.Inst.PrevAdsTime);
			double dblDeltaTime02 = System.DateTime.Now.ExGetDeltaTime(CAppInfoStorage.Inst.PrevRewardAdsTime);

			bool bIsEnable = dblDeltaTime01.ExIsGreateEquals(dblAdsDelay) && dblDeltaTime02.ExIsGreateEquals(dblAdsDeltaTime);
			return bIsEnable && this.AdsSkipTimes >= KDefine.G_MAX_TIMES_ADS_SKIP && CGameInfoStorage.Inst.GetCharacterGameInfo(CGameInfoStorage.Inst.PlayCharacterID).m_oLevelClearInfoDict.Count >= KDefine.G_MAX_NUM_ADS_SKIP_CLEAR_INFOS;
		}
	}
	
	public bool IsEnableUpdateAdsSkipTimes => true;
#endif			// #if ADS_MODULE_ENABLE
	#endregion			// 프로퍼티

	#region 함수
	/** 앱 정보를 로드한다 */
	public CAppInfo LoadAppInfo() {
#if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
		return this.LoadAppInfo(KDefine.G_DATA_P_APP_INFO);
#else
		return null;
#endif			// #if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
	}

	/** 앱 정보를 로드한다 */
	public CAppInfo LoadAppInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
#if MSG_PACK_ENABLE
			this.AppInfo = CFunc.ReadMsgPackObj<CAppInfo>(a_oFilePath);
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
			this.AppInfo = CFunc.ReadJSONObj<CAppInfo>(a_oFilePath);
#endif			// #if MSG_PACK_ENABLE

			CAccess.Assert(this.AppInfo != null);
		}

		return this.AppInfo;
	}

	/** 앱 정보를 저장한다 */
	public void SaveAppInfo() {
#if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
		this.SaveAppInfo(KDefine.G_DATA_P_APP_INFO);
#endif			// #if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
	}

	/** 앱 정보를 저장한다 */
	public void SaveAppInfo(string a_oFilePath) {
#if MSG_PACK_ENABLE
		CFunc.WriteMsgPackObj(a_oFilePath, this.AppInfo);
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
		CFunc.WriteJSONObj(a_oFilePath, this.AppInfo);
#endif			// #if MSG_PACK_ENABLE
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE