﻿#if SCRIPT_TEMPLATE_ONLY
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
using MessagePack;
using Newtonsoft.Json;

/** 클리어 정보 */
[MessagePackObject][System.Serializable]
public partial class CClearInfo : CBaseInfo {
	#region 변수
	[JsonIgnore][IgnoreMember][System.NonSerialized] public STIDInfo m_stIDInfo;
	#endregion			// 변수

	#region 상수
	private const string KEY_INT_RECORD = "IntRecord";
	private const string KEY_REAL_RECORD = "RealRecord";

	private const string KEY_INT_BEST_RECORD = "IntBestRecord";
	private const string KEY_REAL_BEST_RECORD = "RealBestRecord";

	private const string KEY_NUM_SYMBOLS = "NumSymbols";
	#endregion			// 상수

	#region 프로퍼티
	[JsonIgnore][IgnoreMember] public long IntRecord { get { return long.Parse(m_oStrDict.GetValueOrDefault(KEY_INT_RECORD, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_INT_RECORD, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public long IntBestRecord { get { return long.Parse(m_oStrDict.GetValueOrDefault(KEY_INT_BEST_RECORD, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_INT_BEST_RECORD, $"{value}"); } }

	[JsonIgnore][IgnoreMember] public double RealRecord { get { return double.Parse(m_oStrDict.GetValueOrDefault(KEY_REAL_RECORD, KCDefine.B_STR_0_REAL)); } set { m_oStrDict.ExReplaceVal(KEY_REAL_RECORD, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public double RealBestRecord { get { return double.Parse(m_oStrDict.GetValueOrDefault(KEY_REAL_BEST_RECORD, KCDefine.B_STR_0_REAL)); } set { m_oStrDict.ExReplaceVal(KEY_REAL_BEST_RECORD, $"{value}"); } }

	[JsonIgnore][IgnoreMember] public int NumSymbols { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_NUM_SYMBOLS, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_NUM_SYMBOLS, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public ulong ULevelID => CFactory.MakeULevelID(m_stIDInfo.m_nID01, m_stIDInfo.m_nID02, m_stIDInfo.m_nID03);
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
		if(this.Ver.CompareTo(KDefine.G_VER_CLEAR_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CClearInfo() : base(KDefine.G_VER_CLEAR_INFO) {
		// Do Something
	}
	#endregion			// 함수
}

/** 캐릭터 게임 정보 */
[MessagePackObject][System.Serializable]
public partial class CCharacterGameInfo : CBaseInfo {
	#region 상수
	private const string KEY_DAILY_REWARD_ID = "DailyRewardID";
	private const string KEY_FREE_REWARD_ACQUIRE_TIMES = "FreeRewardAcquireTimes";

	private const string KEY_PREV_FREE_REWARD_TIME = "PrevFreeRewardTime";
	private const string KEY_PREV_DAILY_REWARD_TIME = "PrevDailyRewardTime";
	private const string KEY_PREV_DAILY_MISSION_TIME = "PrevDailyMissionTime";
	#endregion			// 상수

	#region 변수
	[Key(51)] public List<ulong> m_oUnlockULevelIDList = new List<ulong>();
	[Key(52)] public List<ulong> m_oUnlockUniqueStageIDList = new List<ulong>();
	[Key(53)] public List<ulong> m_oUnlockUniqueChapterIDList = new List<ulong>();

	[Key(61)] public List<EMissionKinds> m_oCompleteMissionKindsList = new List<EMissionKinds>();
	[Key(62)] public List<EMissionKinds> m_oCompleteDailyMissionKindsList = new List<EMissionKinds>();
	[Key(63)] public List<ETutorialKinds> m_oCompleteTutorialKindsList = new List<ETutorialKinds>();

	[Key(151)] public Dictionary<ulong, CClearInfo> m_oLevelClearInfoDict = new Dictionary<ulong, CClearInfo>();
	[Key(152)] public Dictionary<ulong, CClearInfo> m_oStageClearInfoDict = new Dictionary<ulong, CClearInfo>();
	[Key(153)] public Dictionary<ulong, CClearInfo> m_oChapterClearInfoDict = new Dictionary<ulong, CClearInfo>();
	#endregion			// 변수

	#region 프로퍼티
	[JsonIgnore][IgnoreMember] public int DailyRewardID { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_DAILY_REWARD_ID, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_DAILY_REWARD_ID, $"{value}"); } }
	[JsonIgnore][IgnoreMember] public int FreeRewardAcquireTimes { get { return int.Parse(m_oStrDict.GetValueOrDefault(KEY_FREE_REWARD_ACQUIRE_TIMES, KCDefine.B_STR_0_INT)); } set { m_oStrDict.ExReplaceVal(KEY_FREE_REWARD_ACQUIRE_TIMES, $"{value}"); } }
	
	[JsonIgnore][IgnoreMember] public System.DateTime PrevDailyMissionTime { get { return this.PrevDailyMissionTimeStr.ExIsValid() ? this.CorrectPrevDailyMissionTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_INT); } set { m_oStrDict.ExReplaceVal(KEY_PREV_DAILY_MISSION_TIME, value.ExToLongStr()); } }
	[JsonIgnore][IgnoreMember] public System.DateTime PrevDailyRewardTime { get { return this.PrevDailyRewardTimeStr.ExIsValid() ? this.CorrectPrevDailyRewardTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_INT); } set { m_oStrDict.ExReplaceVal(KEY_PREV_DAILY_REWARD_TIME, value.ExToLongStr()); } }
	[JsonIgnore][IgnoreMember] public System.DateTime PrevFreeRewardTime { get { return this.PrevFreeRewardTimeStr.ExIsValid() ? this.CorrectPrevFreeRewardTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS) : System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_INT); } set { m_oStrDict.ExReplaceVal(KEY_PREV_FREE_REWARD_TIME, value.ExToLongStr()); } }

	[JsonIgnore][IgnoreMember] private string PrevDailyMissionTimeStr => m_oStrDict.GetValueOrDefault(KEY_PREV_DAILY_MISSION_TIME, string.Empty);
	[JsonIgnore][IgnoreMember] private string PrevDailyRewardTimeStr => m_oStrDict.GetValueOrDefault(KEY_PREV_DAILY_REWARD_TIME, string.Empty);
	[JsonIgnore][IgnoreMember] private string PrevFreeRewardTimeStr => m_oStrDict.GetValueOrDefault(KEY_PREV_FREE_REWARD_TIME, string.Empty);

	[JsonIgnore][IgnoreMember] private string CorrectPrevDailyMissionTimeStr => this.PrevDailyMissionTimeStr.Contains(KCDefine.B_TOKEN_SLASH) ? this.PrevDailyMissionTimeStr : this.PrevDailyMissionTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS).ExToLongStr();
	[JsonIgnore][IgnoreMember] private string CorrectPrevDailyRewardTimeStr => this.PrevDailyRewardTimeStr.Contains(KCDefine.B_TOKEN_SLASH) ? this.PrevDailyRewardTimeStr : this.PrevDailyRewardTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS).ExToLongStr();
	[JsonIgnore][IgnoreMember] private string CorrectPrevFreeRewardTimeStr => this.PrevFreeRewardTimeStr.Contains(KCDefine.B_TOKEN_SLASH) ? this.PrevFreeRewardTimeStr : this.PrevFreeRewardTimeStr.ExToTime(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS).ExToLongStr();
	#endregion			// 프로퍼티

	#region IMessagePackSerializationCallbackReceiver
	/** 직렬화 될 경우 */
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();
	}

	/** 역직렬화 되었을 경우 */
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();
		
		m_oUnlockULevelIDList = m_oUnlockULevelIDList ?? new List<ulong>();
		m_oUnlockUniqueStageIDList = m_oUnlockUniqueStageIDList ?? new List<ulong>();
		m_oUnlockUniqueChapterIDList = m_oUnlockUniqueChapterIDList ?? new List<ulong>();

		m_oCompleteMissionKindsList = m_oCompleteMissionKindsList ?? new List<EMissionKinds>();
		m_oCompleteDailyMissionKindsList = m_oCompleteDailyMissionKindsList ?? new List<EMissionKinds>();
		m_oCompleteTutorialKindsList = m_oCompleteTutorialKindsList ?? new List<ETutorialKinds>();
		
		m_oLevelClearInfoDict = m_oLevelClearInfoDict ?? new Dictionary<ulong, CClearInfo>();
		m_oStageClearInfoDict = m_oStageClearInfoDict ?? new Dictionary<ulong, CClearInfo>();
		m_oChapterClearInfoDict = m_oChapterClearInfoDict ?? new Dictionary<ulong, CClearInfo>();

		// 버전이 다를 경우
		if(this.Ver.CompareTo(KDefine.G_VER_CHARACTER_GAME_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CCharacterGameInfo() : base(KDefine.G_VER_CHARACTER_GAME_INFO) {
		// Do Something
	}
	#endregion			// 함수
}

/** 게임 정보 */
[MessagePackObject][System.Serializable]
public partial class CGameInfo : CBaseInfo {
	#region 변수
	[Key(91)] public Dictionary<int, CCharacterGameInfo> m_oCharacterGameInfoDict = new Dictionary<int, CCharacterGameInfo>();
	#endregion			// 변수
	
	#region IMessagePackSerializationCallbackReceiver
	/** 직렬화 될 경우 */
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();
	}

	/** 역직렬화 되었을 경우 */
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();
		m_oCharacterGameInfoDict = m_oCharacterGameInfoDict ?? new Dictionary<int, CCharacterGameInfo>();

		// 버전이 다를 경우
		if(this.Ver.CompareTo(KDefine.G_VER_GAME_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CGameInfo() : base(KDefine.G_VER_GAME_INFO) {
		// Do Something
	}
	#endregion			// 함수
}

/** 게임 정보 저장소 */
public partial class CGameInfoStorage : CSingleton<CGameInfoStorage> {
	#region 프로퍼티
	public int PlayCharacterID { get; set; } = KDefine.G_ID_COMMON_CHARACTER;
	public EPlayMode PlayMode { get; set; } = EPlayMode.NONE;
	public CLevelInfo PlayLevelInfo { get; set; } = null;
	public List<EItemKinds> SelItemKindsList { get; private set; } = new List<EItemKinds>();
	public List<EItemKinds> FreeSelItemKindsList { get; private set; } = new List<EItemKinds>();

	public CGameInfo GameInfo { get; private set; } = new CGameInfo() {
		m_oCharacterGameInfoDict = new Dictionary<int, CCharacterGameInfo>() {
			[KDefine.G_ID_COMMON_CHARACTER] = new CCharacterGameInfo() {
				PrevDailyMissionTime = System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_REAL), PrevDailyRewardTime = System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_REAL), PrevFreeRewardTime = System.DateTime.Today.AddDays(-KCDefine.B_VAL_1_REAL)
			}
		}
	};
	#endregion			// 프로퍼티

	#region 함수
	/** 게임 정보를 리셋한다 */
	public virtual void ResetGameInfo(string a_oBase64Str) {
		CFunc.ShowLog($"CGameInfoStorage.ResetGameInfo: {a_oBase64Str}");
		this.GameInfo = a_oBase64Str.ExMsgPackBase64StrToObj<CGameInfo>();

		CAccess.Assert(this.GameInfo != null);
	}

	/** 선택 아이템을 리셋한다 */
	public virtual void ResetSelItems() {
		this.SelItemKindsList.Clear();
		this.FreeSelItemKindsList.Clear();
	}

	/** 캐릭터 게임 정보를 반환한다 */
	public CCharacterGameInfo GetCharacterGameInfo(int a_nCharacterID) {
		bool bIsValid = this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo);
		CAccess.Assert(bIsValid);

		return oCharacterGameInfo;
	}

	/** 캐릭터 게임 정보를 반환한다 */
	public bool TryGetCharacterGameInfo(int a_nCharacterID, out CCharacterGameInfo a_oOutCharacterGameInfo) {
		this.GameInfo.m_oCharacterGameInfoDict.TryGetValue(a_nCharacterID, out a_oOutCharacterGameInfo);
		return this.GameInfo.m_oCharacterGameInfoDict.ContainsKey(a_nCharacterID);
	}

	/** 레벨 클리어 정보를 반환한다 */
	public CClearInfo GetLevelClearInfo(int a_nCharacterID, int a_nLevelID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		bool bIsValid = this.TryGetLevelClearInfo(a_nCharacterID, a_nLevelID, out CClearInfo oLevelClearInfo, a_nStageID, a_nChapterID);
		CAccess.Assert(bIsValid);

		return oLevelClearInfo;
	}

	/** 스테이지 클리어 정보를 반환한다 */
	public CClearInfo GetStageClearInfo(int a_nCharacterID, int a_nStageID, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		bool bIsValid = this.TryGetStageClearInfo(a_nCharacterID, a_nStageID, out CClearInfo oStageClearInfo, a_nChapterID);
		CAccess.Assert(bIsValid);

		return oStageClearInfo;
	}

	/** 챕터 클리어 정보를 반환한다 */
	public CClearInfo GetChapterClearInfo(int a_nCharacterID, int a_nChapterID) {
		bool bIsValid = this.TryGetChapterClearInfo(a_nCharacterID, a_nChapterID, out CClearInfo oChapterClearInfo);
		CAccess.Assert(bIsValid);

		return oChapterClearInfo;
	}

	/** 레벨 클리어 정보를 반환한다 */
	public bool TryGetLevelClearInfo(int a_nCharacterID, int a_nLevelID, out CClearInfo a_oOutLevelClearInfo, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		a_oOutLevelClearInfo = this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo) ? oCharacterGameInfo.m_oLevelClearInfoDict.GetValueOrDefault(CFactory.MakeULevelID(a_nLevelID, a_nStageID, a_nChapterID)) : null;
		return a_oOutLevelClearInfo != null;
	}

	/** 스테이지 클리어 정보를 반환한다 */
	public bool TryGetStageClearInfo(int a_nCharacterID, int a_nStageID, out CClearInfo a_oOutStageClearInfo, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		a_oOutStageClearInfo = this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo) ? oCharacterGameInfo.m_oStageClearInfoDict.GetValueOrDefault(CFactory.MakeULevelID(KCDefine.B_VAL_0_INT, a_nStageID, a_nChapterID)) : null;
		return a_oOutStageClearInfo != null;
	}

	/** 챕터 클리어 정보를 반환한다 */
	public bool TryGetChapterClearInfo(int a_nCharacterID, int a_nChapterID, out CClearInfo a_oOutChapterClearInfo) {
		a_oOutChapterClearInfo = this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo) ? oCharacterGameInfo.m_oChapterClearInfoDict.GetValueOrDefault(CFactory.MakeULevelID(KCDefine.B_VAL_0_INT, KCDefine.B_VAL_0_INT, a_nChapterID)) : null;
		return a_oOutChapterClearInfo != null;
	}

	/** 레벨 클리어 정보를 추가한다 */
	public void AddLevelClearInfo(int a_nCharacterID, CClearInfo a_oClearInfo, bool a_bIsEnableAssert = true) {
		this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo);
		oCharacterGameInfo.m_oLevelClearInfoDict.TryAdd(a_oClearInfo.m_stIDInfo.UniqueID01, a_oClearInfo);
	}

	/** 스테이지 클리어 정보를 추가한다 */
	public void AddStageClearInfo(int a_nCharacterID, CClearInfo a_oClearInfo, bool a_bIsEnableAssert = true) {
		this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo);
		oCharacterGameInfo.m_oStageClearInfoDict.TryAdd(a_oClearInfo.m_stIDInfo.UniqueID02, a_oClearInfo);
	}

	/** 챕터 클리어 정보를 추가한다 */
	public void AddChapterClearInfo(int a_nCharacterID, CClearInfo a_oClearInfo, bool a_bIsEnableAssert = true) {
		this.TryGetCharacterGameInfo(a_nCharacterID, out CCharacterGameInfo oCharacterGameInfo);
		oCharacterGameInfo.m_oChapterClearInfoDict.TryAdd(a_oClearInfo.m_stIDInfo.UniqueID03, a_oClearInfo);
	}
	
	/** 게임 정보를 로드한다 */
	public CGameInfo LoadGameInfo() {
#if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
		return this.LoadGameInfo(KDefine.G_DATA_P_GAME_INFO);
#else
		return null;
#endif			// #if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
	}

	/** 게임 정보를 로드한다 */
	public CGameInfo LoadGameInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
#if MSG_PACK_ENABLE
			this.GameInfo = CFunc.ReadMsgPackObj<CGameInfo>(a_oFilePath);
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
			this.GameInfo = CFunc.ReadJSONObj<CGameInfo>(a_oFilePath);
#endif			// #if MSG_PACK_ENABLE

			CAccess.Assert(this.GameInfo != null);

			foreach(var stKeyVal in this.GameInfo.m_oCharacterGameInfoDict) {
				foreach(var stLevelKeyVal in stKeyVal.Value.m_oLevelClearInfoDict) {
					stLevelKeyVal.Value.m_stIDInfo = CFactory.MakeIDInfo(stLevelKeyVal.Key.ExULevelIDToLevelID(), stLevelKeyVal.Key.ExULevelIDToStageID(), stLevelKeyVal.Key.ExULevelIDToChapterID());
				}

				foreach(var stStageKeyVal in stKeyVal.Value.m_oStageClearInfoDict) {
					stStageKeyVal.Value.m_stIDInfo = CFactory.MakeIDInfo(KCDefine.B_VAL_0_INT, stStageKeyVal.Key.ExULevelIDToStageID(), stStageKeyVal.Key.ExULevelIDToChapterID());
				}

				foreach(var stChapterKeyVal in stKeyVal.Value.m_oChapterClearInfoDict) {
					stChapterKeyVal.Value.m_stIDInfo = CFactory.MakeIDInfo(KCDefine.B_VAL_0_INT, KCDefine.B_VAL_0_INT, stChapterKeyVal.Key.ExULevelIDToChapterID());
				}
			}
		}

		return this.GameInfo;
	}

	/** 게임 정보를 저장한다 */
	public void SaveGameInfo() {
#if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
		this.SaveGameInfo(KDefine.G_DATA_P_GAME_INFO);
#endif			// #if MSG_PACK_ENABLE || NEWTON_SOFT_JSON_MODULE_ENABLE
	}

	/** 게임 정보를 저장한다 */
	public void SaveGameInfo(string a_oFilePath) {
#if MSG_PACK_ENABLE
		CFunc.WriteMsgPackObj(a_oFilePath, this.GameInfo);
#elif NEWTON_SOFT_JSON_MODULE_ENABLE
		CFunc.WriteJSONObj(a_oFilePath, this.GameInfo);
#endif			// #if MSG_PACK_ENABLE
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY