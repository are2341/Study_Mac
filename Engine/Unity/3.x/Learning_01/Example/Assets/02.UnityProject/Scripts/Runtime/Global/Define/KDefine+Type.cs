using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

#region 기본
//! 게임 속성
[System.Serializable]
public struct STGameConfig {

}

//! 아이템 정보
[System.Serializable]
public struct STItemInfo {
	public int m_nNumItems;
	public EItemKinds m_eItemKinds;
}

//! 타입 랩퍼
[MessagePackObject]
public struct STTypeWrapper {
	[Key(51)] public List<long> m_oLevelIDList;

	[Key(161)] public Dictionary<int, Dictionary<int, int>> m_oNumLevelInfosDictContainer;
	[Key(162)] public Dictionary<int, Dictionary<int, Dictionary<int, CLevelInfo>>> m_oLevelInfoDictContainer;
}
#endregion			// 기본

#region 추가 타입

#endregion			// 추가 타입
