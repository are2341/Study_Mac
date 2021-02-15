using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//! 전처리기 심볼 테이블
public class CDefineSymbolTable : CScriptableObj<CDefineSymbolTable> {
	#region 변수
	[Header("Common Define Symbol")]
	[SerializeField] private List<string> m_oCommonDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oSubCommonDefineSymbolList = new List<string>();

	[Header("Standalone Define Symbol")]
	[SerializeField] private List<string> m_oStandaloneDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oMacDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oWndsDefineSymbolList = new List<string>();

	[Header("iOS Define Symbol")]
	[SerializeField] private List<string> m_oiOSDefineSymbolList = new List<string>();

	[Header("Android Define Symbol")]
	[SerializeField] private List<string> m_oAndroidDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oGoogleDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oOneStoreDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oGalaxyStoreDefineSymbolList = new List<string>();
	#endregion			// 변수

	#region 프로퍼티
	public List<string> StandaloneDefineSymbolList { get; private set; } = new List<string>();
	public List<string> iOSDefineSymbolList { get; private set; } = new List<string>();
	public List<string> AndroidDefineSymbolList { get; private set; } = new List<string>();

#if UNITY_EDITOR
	public List<string> EditorCommonDefineSymbolList => m_oCommonDefineSymbolList;
	public List<string> EditorSubCommonDefineSymbolList => m_oSubCommonDefineSymbolList;

	public List<string> EditorStandaloneDefineSymbolList => m_oStandaloneDefineSymbolList;
	public List<string> EditorMacDefineSymbolList => m_oMacDefineSymbolList;
	public List<string> EditorWndsDefineSymbolList => m_oWndsDefineSymbolList;

	public List<string> EditoriOSDefineSymbolList => m_oiOSDefineSymbolList;

	public List<string> EditorAndroidDefineSymbolList => m_oAndroidDefineSymbolList;
	public List<string> EditorGoogleDefineSymbolList => m_oGoogleDefineSymbolList;
	public List<string> EditorOneStoreDefineSymbolList => m_oOneStoreDefineSymbolList;
	public List<string> EditorGalaxyStoreDefineSymbolList => m_oGalaxyStoreDefineSymbolList;
#endif			// #if UNITY_EDITOR
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		this.StandaloneDefineSymbolList.ExAddValues(m_oCommonDefineSymbolList);
		this.StandaloneDefineSymbolList.ExAddValues(m_oSubCommonDefineSymbolList);
		this.StandaloneDefineSymbolList.ExAddValues(m_oStandaloneDefineSymbolList);

		this.iOSDefineSymbolList.ExAddValues(m_oCommonDefineSymbolList);
		this.iOSDefineSymbolList.ExAddValues(m_oSubCommonDefineSymbolList);
		this.iOSDefineSymbolList.ExAddValues(m_oiOSDefineSymbolList);
		
		this.AndroidDefineSymbolList.ExAddValues(m_oCommonDefineSymbolList);
		this.AndroidDefineSymbolList.ExAddValues(m_oSubCommonDefineSymbolList);
		this.AndroidDefineSymbolList.ExAddValues(m_oAndroidDefineSymbolList);

#if UNITY_ANDROID
#if ONE_STORE
		this.AndroidDefineSymbolList.ExAddValues(m_oOneStoreDefineSymbolList);
#elif GALAXY_STORE
		this.AndroidDefineSymbolList.ExAddValues(m_oGalaxyStoreDefineSymbolList);
#else
		this.AndroidDefineSymbolList.ExAddValues(m_oGoogleDefineSymbolList);
#endif			// #if ONE_STORE
#elif UNITY_STANDALONE
#if UNITY_STANDALONE_WIN
		this.StandaloneDefineSymbolList.ExAddValues(m_oWndsDefineSymbolList);
#else
		this.StandaloneDefineSymbolList.ExAddValues(m_oMacDefineSymbolList);
#endif			// #if UNITY_STANDALONE_WIN
#endif			// #if UNITY_ANDROID
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	//! 공용 전처리기 심볼 리스트를 변경한다
	public void SetCommonDefineSymbolList(List<string> a_oDefineSymbolList) {
		m_oCommonDefineSymbolList = a_oDefineSymbolList;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
