﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

//! 공용 기본 정보
[Union(0, typeof(CCommonAppInfo))]
[Union(1, typeof(CCommonUserInfo))]
[Union(2, typeof(CCommonGameInfo))]
[MessagePackObject]
[System.Serializable]
public abstract class CCommonBaseInfo : IMessagePackSerializationCallbackReceiver {
	#region 변수
	[Key(0)] public STVer m_stVer;

	[Key(1)] public Dictionary<string, bool> m_oBoolList = new Dictionary<string, bool>();
	[Key(2)] public Dictionary<string, int> m_oIntList = new Dictionary<string, int>();
	[Key(3)] public Dictionary<string, float> m_oFltList = new Dictionary<string, float>();
	[Key(4)] public Dictionary<string, string> m_oStrList = new Dictionary<string, string>();
	#endregion			// 변수

	#region 인터페이스
	//! 직렬화 될 경우
	public virtual void OnBeforeSerialize() {
		// Do Nothing
	}

	//! 역직렬화 되었을 경우
	public virtual void OnAfterDeserialize() {
		m_stVer.m_oExtraInfoList = m_stVer.m_oExtraInfoList ?? new Dictionary<string, string>();
		
		m_oBoolList = m_oBoolList ?? new Dictionary<string, bool>();
		m_oIntList = m_oIntList ?? new Dictionary<string, int>();
		m_oFltList = m_oFltList ?? new Dictionary<string, float>();
		m_oStrList = m_oStrList ?? new Dictionary<string, string>();
	}
	#endregion			// 인터페이스

	#region 함수
	//! 생성자
	public CCommonBaseInfo(string a_oVer) {
		m_stVer = CFactory.MakeDefVer(a_oVer);
	}
	#endregion			// 함수
}
