using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 문자열 테이블
public class CStringTable : CSingleton<CStringTable> {
	#region 변수
	private Dictionary<string, string> m_oStringList = new Dictionary<string, string>();
	private Dictionary<ulong, string> m_oEnumStringList = new Dictionary<ulong, string>();
	#endregion			// 변수

	#region 함수
	//! 상태를 리셋한다
	public virtual void Reset() {
		m_oStringList.Clear();
	}

	//! 문자열을 반환한다
	public string GetString(string a_oKey) {
		return m_oStringList.ExGetValue(a_oKey, a_oKey);
	}

	//! 문자열을 추가한다
	public void AddString(string a_oKey, string a_oString, bool a_bIsReplace = false) {
		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oStringList.ExReplaceValue(a_oKey, a_oString);
		} else {
			m_oStringList.ExAddValue(a_oKey, a_oString);
		}
	}

	//! 문자열을 제거한다
	public void RemoveString(string a_oKey) {
		m_oStringList.ExRemoveValue(a_oKey);
	}

	//! 문자열을 로드한다
	public Dictionary<string, string> LoadStrings(string a_oCSVString) {
		CAccess.Assert(a_oCSVString.ExIsValid());
		var oStringInfoList = CSVParser.Parse(a_oCSVString);

		for(int i = 0; i < oStringInfoList.Count; ++i) {
			int nReplace = int.Parse(oStringInfoList[i][KCDefine.U_KEY_STRING_T_REPLACE]);
			
			string oKey = oStringInfoList[i][KCDefine.U_KEY_STRING_T_ID];
			string oString = oStringInfoList[i][KCDefine.U_KEY_STRING_T_STRING];

			this.AddString(oKey, oString, nReplace != KCDefine.B_VALUE_INT_0);
		}

		return m_oStringList;
	}

	//! 문자열을 로드한다
	public Dictionary<string, string> LoadStringsFromFile(string a_oFilePath) {
		string oString = CFunc.ReadString(a_oFilePath);
		return this.LoadStrings(oString);
	}

	//! 문자열을 로드한다
	public Dictionary<string, string> LoadStringsFromRes(string a_oFilePath) {
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.LoadStrings(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}
	#endregion			// 함수

	#region 제네릭 함수
	//! 열거형 문자열을 반환한다
	public string GetEnumString<T>(T a_tKey) where T : struct {
		ulong nKey = (ulong)typeof(T).GetHashCode() << (sizeof(int) * KCDefine.B_UNIT_BYTE_TO_BIT);
		return m_oEnumStringList.ExGetValue(nKey + (ulong)a_tKey.GetHashCode(), string.Empty);
	}

	//! 열거형 문자열을 추가한다
	public void AddEnumString<T>(T a_tKey, string a_oString, bool a_bIsReplace = false) where T : struct {
		ulong nKey = (ulong)typeof(T).GetHashCode() << (sizeof(int) * KCDefine.B_UNIT_BYTE_TO_BIT);
		nKey += (ulong)a_tKey.GetHashCode();

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oEnumStringList.ExReplaceValue(nKey, a_oString);
		} else {
			m_oEnumStringList.ExAddValue(nKey, a_oString);
		}		
	}

	//! 열거형 문자열을 로드한다
	public void LoadEnumStrings<T>(bool a_bIsReplace = false) where T : struct {
		var oEnumValues = CAccess.GetEnumValues<T>();

		for(int i = 0; i < oEnumValues.Length; ++i) {
			this.AddEnumString(oEnumValues[i], oEnumValues[i].ToString(), a_bIsReplace);
		}
	}
	#endregion			// 제네릭 함수
}
