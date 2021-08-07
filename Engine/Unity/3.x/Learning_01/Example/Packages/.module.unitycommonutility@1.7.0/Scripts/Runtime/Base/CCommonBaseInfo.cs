using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

//! 공용 기본 정보
[Union(0, typeof(CCommonAppInfo))]
[Union(1, typeof(CCommonUserInfo))]
[Union(2, typeof(CCommonGameInfo))]
[MessagePackObject]
[System.Serializable]
public abstract class CCommonBaseInfo : IMessagePackSerializationCallbackReceiver {
	#region 변수
	[Key(0)] public Dictionary<string, int> m_oIntDict = new Dictionary<string, int>();
	[Key(1)] public Dictionary<string, float> m_oFltDict = new Dictionary<string, float>();
	[Key(2)] public Dictionary<string, string> m_oStrDict = new Dictionary<string, string>();
	#endregion			// 변수

	#region 인터페이스
	//! 직렬화 될 경우
	public virtual void OnBeforeSerialize() {
		// Do Something
	}

	//! 역직렬화 되었을 경우
	public virtual void OnAfterDeserialize() {
		// Do Something
	}
	#endregion			// 인터페이스
}
