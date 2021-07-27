using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

//! 기본 정보
[Union(0, typeof(CAppInfo))]
[Union(1, typeof(CUserInfo))]
[Union(2, typeof(CGameInfo))]
[Union(3, typeof(CClearInfo))]
[Union(4, typeof(CCellInfo))]
[Union(5, typeof(CLevelInfo))]
[MessagePackObject]
[System.Serializable]
public abstract class CBaseInfo : IMessagePackSerializationCallbackReceiver {
	#region 변수
	[Key(0)] public STVer m_stVer;

	[Key(1)] public Dictionary<string, bool> m_oBoolDict = new Dictionary<string, bool>();
	[Key(2)] public Dictionary<string, int> m_oIntDict = new Dictionary<string, int>();
	[Key(3)] public Dictionary<string, float> m_oFltDict = new Dictionary<string, float>();
	[Key(4)] public Dictionary<string, string> m_oStrDict = new Dictionary<string, string>();
	#endregion			// 변수

	#region 인터페이스
	//! 직렬화 될 경우
	public virtual void OnBeforeSerialize() {
		// Do Nothing
	}

	//! 역직렬화 되었을 경우
	public virtual void OnAfterDeserialize() {
		m_stVer.m_oExtraInfoDict = m_stVer.m_oExtraInfoDict ?? new Dictionary<string, string>();

		m_oBoolDict = m_oBoolDict ?? new Dictionary<string, bool>();
		m_oIntDict = m_oIntDict ?? new Dictionary<string, int>();
		m_oFltDict = m_oFltDict ?? new Dictionary<string, float>();
		m_oStrDict = m_oStrDict ?? new Dictionary<string, string>();
	}
	#endregion			// 인터페이스

	#region 함수
	//! 생성자
	public CBaseInfo(string a_oVer) {
		m_stVer = CFactory.MakeDefVer(a_oVer);
	}
	#endregion			// 함수
}
