using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 전처리기 심볼 정보 테이블 */
public class CDefineSymbolInfoTable : CScriptableObj<CDefineSymbolInfoTable> {
	#region 변수
	[Header("=====> Common Define Symbol <=====")]
	[SerializeField] private List<string> m_oCommonDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oSubCommonDefineSymbolList = new List<string>();

	[Header("=====> iOS Define Symbol <=====")]
	[SerializeField] private List<string> m_oiOSAppleDefineSymbolList = new List<string>();

	[Header("=====> Android Define Symbol <=====")]
	[SerializeField] private List<string> m_oAndroidGoogleDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oAndroidAmazonDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oAndroidOneStoreDefineSymbolList = new List<string>();

	[Header("=====> Standalone Define Symbol <=====")]
	[SerializeField] private List<string> m_oStandaloneMacAppleDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oStandaloneMacSteamDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oStandaloneWndsSteamDefineSymbolList = new List<string>();
	#endregion			// 변수

	#region 프로퍼티
	public List<string> iOSDefineSymbolList { get; private set; } = new List<string>();
	public List<string> AndroidDefineSymbolList { get; private set; } = new List<string>();
	public List<string> StandaloneDefineSymbolList { get; private set; } = new List<string>();

#if UNITY_EDITOR
	public List<string> EditorCommonDefineSymbolList => m_oCommonDefineSymbolList;
	public List<string> EditorSubCommonDefineSymbolList => m_oSubCommonDefineSymbolList;

	public List<string> EditoriOSAppleDefineSymbolList => m_oiOSAppleDefineSymbolList;

	public List<string> EditorAndroidGoogleDefineSymbolList => m_oAndroidGoogleDefineSymbolList;
	public List<string> EditorAndroidAmazonDefineSymbolList => m_oAndroidAmazonDefineSymbolList;
	public List<string> EditorAndroidOneStoreDefineSymbolList => m_oAndroidOneStoreDefineSymbolList;

	public List<string> EditorStandaloneMacAppleDefineSymbolList => m_oStandaloneMacAppleDefineSymbolList;
	public List<string> EditorStandaloneMacSteamDefineSymbolList => m_oStandaloneMacSteamDefineSymbolList;
	public List<string> EditorStandaloneWndsSteamDefineSymbolList => m_oStandaloneWndsSteamDefineSymbolList;
#endif			// #if UNITY_EDITOR
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.iOSDefineSymbolList.ExAddVals(m_oCommonDefineSymbolList);
		this.iOSDefineSymbolList.ExAddVals(m_oSubCommonDefineSymbolList);
		
		this.AndroidDefineSymbolList.ExAddVals(m_oCommonDefineSymbolList);
		this.AndroidDefineSymbolList.ExAddVals(m_oSubCommonDefineSymbolList);

		this.StandaloneDefineSymbolList.ExAddVals(m_oCommonDefineSymbolList);
		this.StandaloneDefineSymbolList.ExAddVals(m_oSubCommonDefineSymbolList);

#if UNITY_IOS
		this.iOSDefineSymbolList.ExAddVals(m_oiOSAppleDefineSymbolList);
#elif UNITY_ANDROID
#if ANDROID_AMAZON_PLATFORM
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidAmazonDefineSymbolList);
#elif ANDROID_ONE_STORE_PLATFORM
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidOneStoreDefineSymbolList);
#else
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidGoogleDefineSymbolList);
#endif			// #if ANDROID_AMAZON_PLATFORM
#elif UNITY_STANDALONE
#if STANDALONE_MAC_STEAM_PLATFORM
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneMacSteamDefineSymbolList);
#elif STANDALONE_WNDS_STEAM_PLATFORM
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneWndsSteamDefineSymbolList);
#else
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneMacAppleDefineSymbolList);
#endif			// #if STANDALONE_MAC_STEAM_PLATFORM
#endif			// #if UNITY_ANDROID
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	/** 공용 전처리기 심볼을 변경한다 */
	public void SetCommonDefineSymbols(List<string> a_oDefineSymbolList) {
		m_oCommonDefineSymbolList = a_oDefineSymbolList;
	}

	/** 서브 공용 전처리기 심볼을 변경한다 */
	public void SetSubCommonDefineSymbols(List<string> a_oDefineSymbolList) {
		m_oSubCommonDefineSymbolList = a_oDefineSymbolList;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
