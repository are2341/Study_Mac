using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 전처리기 심볼 정보 테이블 */
public partial class CDefineSymbolInfoTable : CScriptableObj<CDefineSymbolInfoTable> {
	#region 상수
	private static readonly List<string> MODULE_VER_DEFINE_SYMBOL_LIST = new List<string>() {
		"MODULE_VER_2_0_0_OR_NEWER",
		"MODULE_VER_2_1_0_OR_NEWER",
		"MODULE_VER_2_2_0_OR_NEWER",
		"MODULE_VER_2_3_0_OR_NEWER",
		"MODULE_VER_2_4_0_OR_NEWER"
	};
	#endregion			// 상수

	#region 변수
	[Header("=====> Common Define Symbol <=====")]
	[SerializeField] private List<string> m_oCommonDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oSubCommonDefineSymbolList = new List<string>();

	[Header("=====> iOS Define Symbol <=====")]
	[SerializeField] private List<string> m_oiOSAppleDefineSymbolList = new List<string>();

	[Header("=====> Android Define Symbol <=====")]
	[SerializeField] private List<string> m_oAndroidDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oAndroidGoogleDefineSymbolList = new List<string>();
	[SerializeField] private List<string> m_oAndroidAmazonDefineSymbolList = new List<string>();

	[Header("=====> Standalone Define Symbol <=====")]
	[SerializeField] private List<string> m_oStandaloneDefineSymbolList = new List<string>();
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

	public List<string> EditorAndroidDefineSymbolList => m_oAndroidDefineSymbolList;
	public List<string> EditorAndroidGoogleDefineSymbolList => m_oAndroidGoogleDefineSymbolList;
	public List<string> EditorAndroidAmazonDefineSymbolList => m_oAndroidAmazonDefineSymbolList;

	public List<string> EditorStandaloneDefineSymbolList => m_oStandaloneDefineSymbolList;
	public List<string> EditorStandaloneMacSteamDefineSymbolList => m_oStandaloneMacSteamDefineSymbolList;
	public List<string> EditorStandaloneWndsSteamDefineSymbolList => m_oStandaloneWndsSteamDefineSymbolList;
#endif			// #if UNITY_EDITOR
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		var oDefineSymbolListContainer = new List<List<string>>() {
			m_oCommonDefineSymbolList, m_oSubCommonDefineSymbolList, CDefineSymbolInfoTable.MODULE_VER_DEFINE_SYMBOL_LIST
		};

		for(int i = 0; i < oDefineSymbolListContainer.Count; ++i) {
			this.iOSDefineSymbolList.ExAddVals(oDefineSymbolListContainer[i]);
			this.AndroidDefineSymbolList.ExAddVals(oDefineSymbolListContainer[i]);
			this.StandaloneDefineSymbolList.ExAddVals(oDefineSymbolListContainer[i]);
		}

#if UNITY_IOS
		this.iOSDefineSymbolList.ExAddVals(m_oiOSAppleDefineSymbolList);
#elif UNITY_ANDROID
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidDefineSymbolList);

#if ANDROID_AMAZON_PLATFORM
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidAmazonDefineSymbolList);
#else
		this.AndroidDefineSymbolList.ExAddVals(m_oAndroidGoogleDefineSymbolList);
#endif			// #if ANDROID_AMAZON_PLATFORM
#elif UNITY_STANDALONE
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneDefineSymbolList);

#if STANDALONE_WNDS_STEAM_PLATFORM
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneWndsSteamDefineSymbolList);
#else
		this.StandaloneDefineSymbolList.ExAddVals(m_oStandaloneMacSteamDefineSymbolList);
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

	/** 독립 플랫폼 전처리기 심볼을 변경한다 */
	public void SetStandaloneDefineSymbols(List<string> a_oDefineSymbolList) {
		m_oStandaloneDefineSymbolList = a_oDefineSymbolList;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
