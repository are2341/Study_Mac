using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MessagePack;

#region 기본
//! 작업 정보
public struct STTaskInfo {
	public Task m_oTask;
	public System.Action<Task> m_oCallback;
}

//! 콜백 정보
public struct STCallbackInfo {
	public string m_oKey;
	public System.Action m_oCallback;
}

//! 컴포넌트 정보
public struct STComponentInfo {
	public int m_nID;
	public Component m_oComponent;
}

//! 정렬 순서 정보
public struct STSortingOrderInfo {
	public int m_nOrder;
	public string m_oLayer;
}

//! 시퀀스 애니메이션 정보
public struct STSequenceInfo {
	public Sequence m_oSequence;
	public GameObject m_oObj;
}

//! 빌드 버전
[System.Serializable]
public struct STBuildVer {
	public int m_nNum;
	public string m_oVer;
}

//! 식별자 정보
[MessagePackObject]
[System.Serializable]
public struct STIDInfo {
	[Key(0)] public int m_nID;
	[Key(1)] public int m_nStageID;
	[Key(2)] public int m_nChapterID;
}

//! 인덱스 정보
[MessagePackObject]
[System.Serializable]
public struct STIdxInfo {
	[Key(0)] public int m_nX;
	[Key(1)] public int m_nY;
	[Key(2)] public int m_nZ;
}

//! 지급 아이템 정보
[System.Serializable]
public struct STPostItemInfo {
	public string m_oNumItems;
	public string m_oItemType;
	public string m_oItemKinds;
}

//! 디바이스 정보
[System.Serializable]
public struct STDeviceInfo {
	public List<string> m_oiOSAdmobIDList;
	public List<string> m_oAndroidAdmobIDList;
}

//! 디바이스 속성
[System.Serializable]
public struct STDeviceConfig {
	public List<string> m_oiOSAdsIDList;
	public List<string> m_oAndroidAdsIDList;
}

//! 빌드 버전 속성
[System.Serializable]
public struct STBuildVerConfig {
	public STBuildVer m_stiOSVer;

	public STBuildVer m_stGoogleVer;
	public STBuildVer m_stOneStoreVer;
	public STBuildVer m_stGalaxyStoreVer;
}

//! 공용 타입 래퍼
[MessagePackObject]
public struct STCommonTypeWrapper {
	[Key(0)] public List<int> m_oIntList;
	[Key(1)] public List<float> m_oFltList;
	[Key(2)] public List<string> m_oStrList;

	[Key(10)] public Dictionary<int, int> m_oIntIntDict;
	[Key(11)] public Dictionary<int, float> m_oIntFltDict;
	[Key(12)] public Dictionary<int, string> m_oIntStrDict;

	[Key(20)] public Dictionary<long, int> m_oLongIntDict;
	[Key(21)] public Dictionary<long, float> m_oLongFltDict;
	[Key(22)] public Dictionary<long, string> m_oLongStrDict;

	[Key(30)] public Dictionary<string, int> m_oStrIntDict;
	[Key(31)] public Dictionary<string, float> m_oStrFltDict;
	[Key(32)] public Dictionary<string, string> m_oStrStrDict;
}

//! 경로 정보
public class CPathInfo {
	public int m_nCost = 0;
	public Vector3Int m_stIdx = Vector3Int.zero;
	
	public CPathInfo m_oPrevPathInfo = null;
}
#endregion			// 기본

#region 조건부 타입
#if ADS_MODULE_ENABLE
//! 광고 보상 아이템 정보
public struct STAdsRewardItemInfo {
	public string m_oID;
	public string m_oVal;
}
#endif			// #if ADS_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
//! 알림 정보
public struct STNotiInfo {
	public bool m_bIsRepeat;
	public bool m_bIsShowForeground;

	public string m_oTitle;
	public string m_oSubTitle;
	public string m_oMsg;

	public System.DateTime m_stNotiTime;
}
#endif			// #if NOTI_MODULE_ENABLE

#if UNITY_EDITOR || UNITY_STANDALONE
//! 에디터 레벨 생성 정보
public class CEditorLevelCreateInfo {
	public int m_nNumLevels = 0;

	public Vector3Int m_stMinNumCells = Vector3Int.zero;
	public Vector3Int m_stMaxNumCells = Vector3Int.zero;
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
#endregion			// 조건부 타입
