using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using MessagePack;

/*
MsgPack 키 인덱스 지정 방식
:
- 5 ~ 29 : 복합 형식 데이터
- 31 ~ 69 : 리스트 형식 데이터
- 71 ~ 109 : 딕셔너리 형식 데이터

복합 형식 데이터 세부 범위
:
- 5 ~ 19 : 값 형식 데이터
- 21 ~ 29 : 참조 형식 데이터

리스트 형식 데이터 세부 범위
:
- 31 ~ 39 : 값 (기본) 형식 데이터 (1 ~ 4 : 기본, 5 ~ 9 : 열거형)
- 41 ~ 49 : 값 (복합) 형식 데이터
- 51 ~ 59 : 참조 (기본) 형식 데이터
- 61 ~ 69 : 참조 (복합) 형식 데이터

딕셔너리 형식 데이터 세부 범위
:
- 71 ~ 79 : 값 (기본) 형식 데이터 (1 ~ 4 : 기본, 5 ~ 9 : 열거형)
- 81 ~ 89 : 값 (복합) 형식 데이터
- 91 ~ 99 : 참조 (기본) 형식 데이터
- 101 ~ 109 : 참조 (복합) 형식 데이터
*/
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
public struct STSequenceAniInfo {
	public Sequence m_oSequence;
	public GameObject m_oObj;
}

//! 버전
[MessagePackObject]
[System.Serializable]
public struct STVersion {
	[Key(0)] public string m_oVersion;
	[Key(1)] public Dictionary<string, string> m_oExtraInfoList;
}

//! 빌드 버전
[System.Serializable]
public struct STBuildVersion {
	public int m_nNumber;
	public string m_oVersion;
}

//! 지급 아이템
[System.Serializable]
public struct STPostItem {
	public string m_oID;
	public string m_oValue;
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
public struct STBuildVersionConfig {
	public STBuildVersion m_stiOSVersion;

	public STBuildVersion m_stGoogleVersion;
	public STBuildVersion m_stOneStoreVersion;
	public STBuildVersion m_stGalaxyStoreVersion;
}

//! 공용 커스텀 타입 래퍼
[MessagePackObject]
public struct STCommonCustomTypeWrapper {
	[Key(0)] public List<bool> m_oBoolList;
	[Key(1)] public List<int> m_oIntList;
	[Key(2)] public List<float> m_oFloatList;
	[Key(3)] public List<string> m_oStringList;

	[Key(4)] public Dictionary<string, bool> m_oBoolMapList;
	[Key(5)] public Dictionary<string, int> m_oIntMapList;
	[Key(6)] public Dictionary<string, float> m_oFloatMapList;
	[Key(7)] public Dictionary<string, string> m_oStringMapList;
}
#endregion			// 기본

#region 조건부 타입
#if ADS_MODULE_ENABLE
//! 광고 보상 아이템
public struct STAdsRewardItem {
	public string m_oID;
	public string m_oValue;
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
#endregion			// 조건부 타입
