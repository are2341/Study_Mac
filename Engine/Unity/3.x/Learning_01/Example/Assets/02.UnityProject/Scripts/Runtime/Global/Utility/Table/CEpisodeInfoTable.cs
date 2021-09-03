using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Leguar.TotalJSON;

//! 레벨 정보
[System.Serializable]
public struct STLevelInfo {
	public string m_oName;
	public string m_oDesc;

	public int m_nID;
	public int m_nStageID;
	public int m_nChapterID;

	public ELevelMode m_eLevelMode;
	public ELevelKinds m_eLevelKinds;
	public ERewardKinds m_eRewardKinds;
	public ETutorialKinds m_eTutorialKinds;

	public Dictionary<ETargetKinds, int> m_oNumTargetsDict;
	public Dictionary<ETargetKinds, int> m_oUnlockNumTargetsDict;

	#region 함수
	//! 생성자
	public STLevelInfo(SimpleJSON.JSONNode a_oLevelInfo) {
		m_oName = a_oLevelInfo[KCDefine.U_KEY_NAME];
		m_oDesc = a_oLevelInfo[KCDefine.U_KEY_DESC];

		m_nID = a_oLevelInfo[KCDefine.U_KEY_ID].AsInt;
		m_nStageID = a_oLevelInfo[KCDefine.U_KEY_STAGE_ID].AsInt;
		m_nChapterID = a_oLevelInfo[KCDefine.U_KEY_CHAPTER_ID].AsInt;

		m_eLevelMode = (ELevelMode)a_oLevelInfo[KCDefine.U_KEY_LEVEL_MODE].AsInt;
		m_eLevelKinds = (ELevelKinds)a_oLevelInfo[KCDefine.U_KEY_LEVEL_KINDS].AsInt;
		m_eRewardKinds = (ERewardKinds)a_oLevelInfo[KCDefine.U_KEY_REWARD_KINDS].AsInt;
		m_eTutorialKinds = (ETutorialKinds)a_oLevelInfo[KCDefine.U_KEY_TUTORIAL_KINDS].AsInt;

		m_oNumTargetsDict = new Dictionary<ETargetKinds, int>();
		m_oUnlockNumTargetsDict = new Dictionary<ETargetKinds, int>();

		for(int i = 0; i < KDefine.G_MAX_NUM_TARGET_KINDS; ++i) {
			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oNumTargetsDict.Add((ETargetKinds)a_oLevelInfo[oTargetKindsKey].AsInt, a_oLevelInfo[oNumTargetsKey].AsInt);
		}

		for(int i = 0; i < KDefine.G_MAX_NUM_UNLOCK_TARGET_KINDS; ++i) {
			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oUnlockNumTargetsDict.Add((ETargetKinds)a_oLevelInfo[oUnlockTargetKindsKey].AsInt, a_oLevelInfo[oUnlockNumTargetsKey].AsInt);
		}
	}

	//! 타겟 개수를 반환한다
	public int GetNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}

	//! 잠금 해제 타겟 개수를 반환한다
	public int GetUnlockNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oUnlockNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR || UNITY_STANDALONE
	//! 레벨 정보를 설정한다
	public void SetupLevelInfo(SimpleJSON.JSONNode a_oLevelInfo) {
		a_oLevelInfo.Add(KCDefine.U_KEY_NAME, m_oName ?? string.Empty);
		a_oLevelInfo.Add(KCDefine.U_KEY_DESC, m_oDesc ?? string.Empty);

		a_oLevelInfo.Add(KCDefine.U_KEY_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nID));
		a_oLevelInfo.Add(KCDefine.U_KEY_STAGE_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nStageID));
		a_oLevelInfo.Add(KCDefine.U_KEY_CHAPTER_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nChapterID));

		a_oLevelInfo.Add(KCDefine.U_KEY_LEVEL_MODE, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eLevelMode));
		a_oLevelInfo.Add(KCDefine.U_KEY_LEVEL_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eLevelKinds));
		a_oLevelInfo.Add(KCDefine.U_KEY_REWARD_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eRewardKinds));
		a_oLevelInfo.Add(KCDefine.U_KEY_TUTORIAL_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eTutorialKinds));

		var oNumTargetsKeyList = m_oNumTargetsDict.Keys.ToList();
		var oUnlockNumTargetsKeyList = m_oUnlockNumTargetsDict.Keys.ToList();

		for(int i = 0; i < oNumTargetsKeyList.Count; ++i) {
			var eKey = oNumTargetsKeyList[i];

			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oLevelInfo.Add(oNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oNumTargetsDict[eKey]));
			a_oLevelInfo.Add(oTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}

		for(int i = 0; i < oUnlockNumTargetsKeyList.Count; ++i) {
			var eKey = oUnlockNumTargetsKeyList[i];

			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oLevelInfo.Add(oUnlockNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oUnlockNumTargetsDict[eKey]));
			a_oLevelInfo.Add(oUnlockTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}
	}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	#endregion			// 조건부 함수
}

//! 스테이지 정보
[System.Serializable]
public struct STStageInfo {
	public string m_oName;
	public string m_oDesc;

	public int m_nID;
	public int m_nChapterID;

	public EStageKinds m_eStageKinds;
	public ERewardKinds m_eRewardKinds;
	public ETutorialKinds m_eTutorialKinds;

	public Dictionary<ETargetKinds, int> m_oNumTargetsDict;
	public Dictionary<ETargetKinds, int> m_oUnlockNumTargetsDict;

	#region 함수
	//! 생성자
	public STStageInfo(SimpleJSON.JSONNode a_oStageInfo) {
		m_oName = a_oStageInfo[KCDefine.U_KEY_NAME];
		m_oDesc = a_oStageInfo[KCDefine.U_KEY_DESC];

		m_nID = a_oStageInfo[KCDefine.U_KEY_ID].AsInt;
		m_nChapterID = a_oStageInfo[KCDefine.U_KEY_CHAPTER_ID].AsInt;

		m_eStageKinds = (EStageKinds)a_oStageInfo[KCDefine.U_KEY_STAGE_KINDS].AsInt;
		m_eRewardKinds = (ERewardKinds)a_oStageInfo[KCDefine.U_KEY_REWARD_KINDS].AsInt;
		m_eTutorialKinds = (ETutorialKinds)a_oStageInfo[KCDefine.U_KEY_TUTORIAL_KINDS].AsInt;

		m_oNumTargetsDict = new Dictionary<ETargetKinds, int>();
		m_oUnlockNumTargetsDict = new Dictionary<ETargetKinds, int>();

		for(int i = 0; i < KDefine.G_MAX_NUM_TARGET_KINDS; ++i) {
			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oNumTargetsDict.Add((ETargetKinds)a_oStageInfo[oTargetKindsKey].AsInt, a_oStageInfo[oNumTargetsKey].AsInt);
		}

		for(int i = 0; i < KDefine.G_MAX_NUM_UNLOCK_TARGET_KINDS; ++i) {
			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oUnlockNumTargetsDict.Add((ETargetKinds)a_oStageInfo[oUnlockTargetKindsKey].AsInt, a_oStageInfo[oUnlockNumTargetsKey].AsInt);
		}
	}

	//! 타겟 개수를 반환한다
	public int GetNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}

	//! 잠금 해제 타겟 개수를 반환한다
	public int GetUnlockNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oUnlockNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR || UNITY_STANDALONE
	//! 스테이지 정보를 설정한다
	public void SetupStageInfo(SimpleJSON.JSONNode a_oStageInfo) {
		a_oStageInfo.Add(KCDefine.U_KEY_NAME, m_oName ?? string.Empty);
		a_oStageInfo.Add(KCDefine.U_KEY_DESC, m_oDesc ?? string.Empty);

		a_oStageInfo.Add(KCDefine.U_KEY_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nID));
		a_oStageInfo.Add(KCDefine.U_KEY_CHAPTER_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nChapterID));

		a_oStageInfo.Add(KCDefine.U_KEY_STAGE_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eStageKinds));
		a_oStageInfo.Add(KCDefine.U_KEY_REWARD_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eRewardKinds));
		a_oStageInfo.Add(KCDefine.U_KEY_TUTORIAL_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eTutorialKinds));

		var oNumTargetsKeyList = m_oNumTargetsDict.Keys.ToList();
		var oUnlockNumTargetsKeyList = m_oUnlockNumTargetsDict.Keys.ToList();

		for(int i = 0; i < oNumTargetsKeyList.Count; ++i) {
			var eKey = oNumTargetsKeyList[i];

			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oStageInfo.Add(oNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oNumTargetsDict[eKey]));
			a_oStageInfo.Add(oTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}

		for(int i = 0; i < oUnlockNumTargetsKeyList.Count; ++i) {
			var eKey = oUnlockNumTargetsKeyList[i];

			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oStageInfo.Add(oUnlockNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oUnlockNumTargetsDict[eKey]));
			a_oStageInfo.Add(oUnlockTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}
	}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	#endregion			// 조건부 함수
}

//! 챕터 정보
[System.Serializable]
public struct STChapterInfo {
	public string m_oName;
	public string m_oDesc;

	public int m_nID;

	public EChapterKinds m_eChapterKinds;
	public ERewardKinds m_eRewardKinds;
	public ETutorialKinds m_eTutorialKinds;

	public Dictionary<ETargetKinds, int> m_oNumTargetsDict;
	public Dictionary<ETargetKinds, int> m_oUnlockNumTargetsDict;

	#region 함수
	//! 생성자
	public STChapterInfo(SimpleJSON.JSONNode a_oChapterInfo) {
		m_oName = a_oChapterInfo[KCDefine.U_KEY_NAME];
		m_oDesc = a_oChapterInfo[KCDefine.U_KEY_DESC];

		m_nID = a_oChapterInfo[KCDefine.U_KEY_ID].AsInt;

		m_eChapterKinds = (EChapterKinds)a_oChapterInfo[KCDefine.U_KEY_CHAPTER_KINDS].AsInt;
		m_eRewardKinds = (ERewardKinds)a_oChapterInfo[KCDefine.U_KEY_REWARD_KINDS].AsInt;
		m_eTutorialKinds = (ETutorialKinds)a_oChapterInfo[KCDefine.U_KEY_TUTORIAL_KINDS].AsInt;

		m_oNumTargetsDict = new Dictionary<ETargetKinds, int>();
		m_oUnlockNumTargetsDict = new Dictionary<ETargetKinds, int>();

		for(int i = 0; i < KDefine.G_MAX_NUM_TARGET_KINDS; ++i) {
			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oNumTargetsDict.Add((ETargetKinds)a_oChapterInfo[oTargetKindsKey].AsInt, a_oChapterInfo[oNumTargetsKey].AsInt);
		}

		for(int i = 0; i < KDefine.G_MAX_NUM_UNLOCK_TARGET_KINDS; ++i) {
			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			m_oUnlockNumTargetsDict.Add((ETargetKinds)a_oChapterInfo[oUnlockTargetKindsKey].AsInt, a_oChapterInfo[oUnlockNumTargetsKey].AsInt);
		}
	}

	//! 타겟 개수를 반환한다
	public int GetNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}

	//! 잠금 해제 타겟 개수를 반환한다
	public int GetUnlockNumTargets(ETargetKinds a_eTargetKinds) {
		return m_oUnlockNumTargetsDict.ExGetVal(a_eTargetKinds, KCDefine.B_VAL_0_INT);
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR || UNITY_STANDALONE
	//! 챕터 정보를 설정한다
	public void SetupChapterInfo(SimpleJSON.JSONNode a_oChapterInfo) {
		a_oChapterInfo.Add(KCDefine.U_KEY_NAME, m_oName ?? string.Empty);
		a_oChapterInfo.Add(KCDefine.U_KEY_DESC, m_oDesc ?? string.Empty);

		a_oChapterInfo.Add(KCDefine.U_KEY_ID, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_nID));

		a_oChapterInfo.Add(KCDefine.U_KEY_CHAPTER_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eChapterKinds));
		a_oChapterInfo.Add(KCDefine.U_KEY_REWARD_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eRewardKinds));
		a_oChapterInfo.Add(KCDefine.U_KEY_TUTORIAL_KINDS, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)m_eTutorialKinds));

		var oNumTargetsKeyList = m_oNumTargetsDict.Keys.ToList();
		var oUnlockNumTargetsKeyList = m_oUnlockNumTargetsDict.Keys.ToList();

		for(int i = 0; i < oNumTargetsKeyList.Count; ++i) {
			var eKey = oNumTargetsKeyList[i];

			string oNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oChapterInfo.Add(oNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oNumTargetsDict[eKey]));
			a_oChapterInfo.Add(oTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}

		for(int i = 0; i < oUnlockNumTargetsKeyList.Count; ++i) {
			var eKey = oUnlockNumTargetsKeyList[i];

			string oUnlockNumTargetsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_NUM_TARGETS, i + KCDefine.B_VAL_1_INT);
			string oUnlockTargetKindsKey = string.Format(KCDefine.U_KEY_FMT_UNLOCK_TARGET_KINDS, i + KCDefine.B_VAL_1_INT);

			a_oChapterInfo.Add(oUnlockNumTargetsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, m_oUnlockNumTargetsDict[eKey]));
			a_oChapterInfo.Add(oUnlockTargetKindsKey, string.Format(KCDefine.B_TEXT_FMT_1_DIGITS, (int)eKey));
		}
	}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	#endregion			// 조건부 함수
}

//! 에피소드 정보 테이블
public class CEpisodeInfoTable : CScriptableObj<CEpisodeInfoTable> {
	#region 변수
	[Header("Level Info")]
	[SerializeField] private List<STLevelInfo> m_oLevelInfoList = new List<STLevelInfo>();

	[Header("Stage Info")]
	[SerializeField] private List<STStageInfo> m_oStageInfoList = new List<STStageInfo>();

	[Header("Chapter Info")]
	[SerializeField] private List<STChapterInfo> m_oChapterInfoList = new List<STChapterInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public Dictionary<long, STLevelInfo> LevelInfoDict { get; private set; } = new Dictionary<long, STLevelInfo>();
	public Dictionary<long, STStageInfo> StageInfoDict { get; private set; } = new Dictionary<long, STStageInfo>();
	public Dictionary<long, STChapterInfo> ChapterInfoDict { get; private set; } = new Dictionary<long, STChapterInfo>();
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		for(int i = 0; i < m_oLevelInfoList.Count; ++i) {
			long nLevelID = CFactory.MakeUniqueLevelID(i, m_oLevelInfoList[i].m_nStageID, m_oLevelInfoList[i].m_nChapterID);
			this.LevelInfoDict.ExAddVal(nLevelID, m_oLevelInfoList[i]);
		}

		for(int i = 0; i < m_oStageInfoList.Count; ++i) {
			long nStageID = CFactory.MakeUniqueStageID(i, m_oStageInfoList[i].m_nChapterID);
			this.StageInfoDict.ExAddVal(nStageID, m_oStageInfoList[i]);
		}

		for(int i = 0; i < m_oChapterInfoList.Count; ++i) {
			long nChapterID = CFactory.MakeUniqueChapterID(i);
			this.ChapterInfoDict.ExAddVal(nChapterID, m_oChapterInfoList[i]);
		}
	}

	//! 레벨 정보를 반환한다
	public STLevelInfo GetLevelInfo(int a_nID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		bool bIsValid = this.TryGetLevelInfo(a_nID, out STLevelInfo stLevelInfo, a_nStageID, a_nChapterID);
		CAccess.Assert(bIsValid);

		return stLevelInfo;
	}

	//! 스테이지 정보를 반환한다
	public STStageInfo GetStageInfo(int a_nID, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		bool bIsValid = this.TryGetStageInfo(a_nID, out STStageInfo stStageInfo, a_nChapterID);
		CAccess.Assert(bIsValid);

		return stStageInfo;
	}

	//! 챕터 정보를 반환한다
	public STChapterInfo GetChapterInfo(int a_nID) {
		bool bIsValid = this.TryGetChapterInfo(a_nID, out STChapterInfo stChapterInfo);
		CAccess.Assert(bIsValid);

		return stChapterInfo;
	}

	//! 레벨 정보를 반환한다
	public bool TryGetLevelInfo(int a_nID, out STLevelInfo a_stOutLevelInfo, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		long nLevelID = CFactory.MakeUniqueLevelID(a_nID, a_nStageID, a_nChapterID);
		a_stOutLevelInfo = this.LevelInfoDict.ExGetVal(nLevelID, KDefine.G_INVALID_LEVEL_INFO);

		return this.LevelInfoDict.ContainsKey(nLevelID);
	}

	//! 스테이지 정보를 반환한다
	public bool TryGetStageInfo(int a_nID, out STStageInfo a_stOutStageInfo, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		long nStageID = CFactory.MakeUniqueStageID(a_nID, a_nChapterID);
		a_stOutStageInfo = this.StageInfoDict.ExGetVal(nStageID, KDefine.G_INVALID_STAGE_INFO);

		return this.StageInfoDict.ContainsKey(nStageID);
	}

	//! 챕터 정보를 반환한다
	public bool TryGetChapterInfo(int a_nID, out STChapterInfo a_stOutChapterInfo) {
		long nChapterID = CFactory.MakeUniqueChapterID(a_nID);
		a_stOutChapterInfo = this.ChapterInfoDict.ExGetVal(nChapterID, KDefine.G_INVALID_CHAPTER_INFO);

		return this.ChapterInfoDict.ContainsKey(nChapterID);
	}

	//! 에피소드 정보를 로드한다	
	public List<object> LoadEpisodeInfos() {
#if UNITY_EDITOR || UNITY_STANDALONE
		return this.LoadEpisodeInfos(KCDefine.U_RUNTIME_TABLE_P_G_EPISODE_INFO);
#else
		return this.LoadEpisodeInfos(KCDefine.U_TABLE_P_G_EPISODE_INFO);
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	}

	//! 에피소드 정보를 로드한다
	public List<object> LoadEpisodeInfos(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
#if UNITY_EDITOR || UNITY_STANDALONE
		string oJSONStr = CFunc.ReadStr(a_oFilePath);
		return this.DoLoadEpisodeInfos(oJSONStr);
#else
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.DoLoadEpisodeInfos(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	}

	//! 에피소드 정보를 로드한다
	private List<object> DoLoadEpisodeInfos(string a_oJSONStr) {
		CAccess.Assert(a_oJSONStr.ExIsValid());
				
		var oJSONNode = SimpleJSON.JSON.Parse(a_oJSONStr) as SimpleJSON.JSONClass;
		var oLevelInfos = oJSONNode[KCDefine.U_KEY_LEVEL];
		var oStageInfos = oJSONNode[KCDefine.U_KEY_STAGE];
		var oChapterInfos = oJSONNode[KCDefine.U_KEY_CHAPTER];

		for(int i = 0; i < oLevelInfos.Count; ++i) {
			var stLevelInfo = new STLevelInfo(oLevelInfos[i]);
			long nLevelID = CFactory.MakeUniqueLevelID(stLevelInfo.m_nID, stLevelInfo.m_nStageID, stLevelInfo.m_nChapterID);

			// 레벨 정보가 추가 가능 할 경우
			if(!this.LevelInfoDict.ContainsKey(nLevelID) || oLevelInfos[i][KCDefine.U_KEY_REPLACE].AsInt != KCDefine.B_VAL_0_INT) {
				this.LevelInfoDict.ExReplaceVal(nLevelID, stLevelInfo);
			}
		}

		for(int i = 0; i < oStageInfos.Count; ++i) {
			var stStageInfo = new STStageInfo(oStageInfos[i]);
			long nStageID = CFactory.MakeUniqueStageID(stStageInfo.m_nID, stStageInfo.m_nChapterID);

			// 스테이지 정보가 추가 가능 할 경우
			if(!this.StageInfoDict.ContainsKey(nStageID) || oStageInfos[i][KCDefine.U_KEY_REPLACE].AsInt != KCDefine.B_VAL_0_INT) {
				this.StageInfoDict.ExReplaceVal(nStageID, stStageInfo);
			}
		}

		for(int i = 0; i < oChapterInfos.Count; ++i) {
			var stChapterInfo = new STChapterInfo(oChapterInfos[i]);
			long nChapterID = CFactory.MakeUniqueChapterID(stChapterInfo.m_nID);

			// 챕터 정보가 추가 가능 할 경우
			if(!this.ChapterInfoDict.ContainsKey(nChapterID) || oChapterInfos[i][KCDefine.U_KEY_REPLACE].AsInt != KCDefine.B_VAL_0_INT) {
				this.ChapterInfoDict.ExReplaceVal(nChapterID, stChapterInfo);
			}
		}

		return new List<object>() {
			this.LevelInfoDict, this.StageInfoDict, this.ChapterInfoDict
		};
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR || UNITY_STANDALONE
	//! 에피소드 정보를 저장한다
	public void SaveEpisodeInfos() {
		var oJSONNode = new SimpleJSON.JSONClass();
		var oLevelInfos = new SimpleJSON.JSONArray();
		var oStageInfos = new SimpleJSON.JSONArray();
		var oChapterInfos = new SimpleJSON.JSONArray();

		foreach(var stKeyVal in this.LevelInfoDict) {
			var oLevelInfo = new SimpleJSON.JSONClass();
			stKeyVal.Value.SetupLevelInfo(oLevelInfo);

			oLevelInfos.Add(oLevelInfo);
		}

		foreach(var stKeyVal in this.StageInfoDict) {
			var oStageInfo = new SimpleJSON.JSONClass();
			stKeyVal.Value.SetupStageInfo(oStageInfo);

			oStageInfos.Add(oStageInfo);
		}

		foreach(var stKeyVal in this.ChapterInfoDict) {
			var oChapterInfo = new SimpleJSON.JSONClass();
			stKeyVal.Value.SetupChapterInfo(oChapterInfo);

			oChapterInfos.Add(oChapterInfo);
		}

		oJSONNode.Add(KCDefine.U_KEY_LEVEL, oLevelInfos);
		oJSONNode.Add(KCDefine.U_KEY_STAGE, oStageInfos);
		oJSONNode.Add(KCDefine.U_KEY_CHAPTER, oChapterInfos);

		var oJSONStr = oJSONNode.ToString();
		CFunc.WriteStr(KCDefine.U_RUNTIME_TABLE_P_G_EPISODE_INFO, JSON.ParseString(oJSONStr).CreatePrettyString());
	}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
	#endregion			// 조건부 함수

	#region 추가 함수

	#endregion			// 추가 함수
}
