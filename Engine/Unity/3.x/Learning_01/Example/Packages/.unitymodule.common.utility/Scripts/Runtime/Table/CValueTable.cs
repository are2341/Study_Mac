using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 값 테이블
public class CValueTable : CSingleton<CValueTable> {
	#region 변수
	private Dictionary<string, bool> m_oBoolList = new Dictionary<string, bool>();
	private Dictionary<string, int> m_oIntList = new Dictionary<string, int>();
	private Dictionary<string, float> m_oFloatList = new Dictionary<string, float>();
	private Dictionary<string, string> m_oStringList = new Dictionary<string, string>();
	#endregion			// 변수

	#region 함수
	//! 상태를 리셋한다
	public virtual void Reset() {
		m_oBoolList.Clear();
		m_oIntList.Clear();
		m_oFloatList.Clear();
		m_oStringList.Clear();
	}

	//! 논리를 반환한다
	public bool GetBool(string a_oKey, bool a_bIsDefValue = false) {
		return m_oBoolList.ExGetValue(a_oKey, a_bIsDefValue);
	}

	//! 정수를 반환한다
	public int GetInt(string a_oKey, int a_nDefValue = KCDefine.B_VALUE_INT_0) {
		return m_oIntList.ExGetValue(a_oKey, a_nDefValue);
	}

	//! 실수를 반환한다
	public float GetFloat(string a_oKey, float a_fDefValue = KCDefine.B_VALUE_FLT_0) {
		return m_oFloatList.ExGetValue(a_oKey, a_fDefValue);
	}

	//! 문자열을 반환한다
	public string GetString(string a_oKey, string a_oDefString = KCDefine.B_EMPTY_STRING) {
		return m_oStringList.ExGetValue(a_oKey, a_oDefString);
	}

	//! 논리를 추가한다
	public void AddBool(string a_oKey, bool a_bIsValue, bool a_bIsReplace = false) {
		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oBoolList.ExReplaceValue(a_oKey, a_bIsValue);
		} else {
			m_oBoolList.ExAddValue(a_oKey, a_bIsValue);
		}
	}

	//! 정수를 추가한다
	public void AddInt(string a_oKey, int a_nValue, bool a_bIsReplace = false) {
		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oIntList.ExReplaceValue(a_oKey, a_nValue);
		} else {
			m_oIntList.ExAddValue(a_oKey, a_nValue);
		}
	}

	//! 실수를 추가한다
	public void AddFloat(string a_oKey, float a_fValue, bool a_bIsReplace = false) {
		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oFloatList.ExReplaceValue(a_oKey, a_fValue);
		} else {
			m_oFloatList.ExAddValue(a_oKey, a_fValue);
		}
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

	//! 논리를 제거한다
	public void RemoveBool(string a_oKey) {
		m_oBoolList.ExRemoveValue(a_oKey);
	}

	//! 정수를 제거한다
	public void RemoveInt(string a_oKey) {
		m_oIntList.ExRemoveValue(a_oKey);
	}

	//! 실수를 제거한다
	public void RemoveFloat(string a_oKey) {
		m_oFloatList.ExRemoveValue(a_oKey);
	}

	//! 문자열을 제거한다
	public void RemoveString(string a_oKey) {
		m_oStringList.ExRemoveValue(a_oKey);
	}

	//! 값을 로드한다
	public List<object> LoadValues(string a_oCSVString) {
		CAccess.Assert(a_oCSVString.ExIsValid());
		var oValueInfoList = CSVParser.Parse(a_oCSVString);

		for(int i = 0; i < oValueInfoList.Count; ++i) {
			string oKey = oValueInfoList[i][KCDefine.U_KEY_VALUE_T_ID];
			string oValue = oValueInfoList[i][KCDefine.U_KEY_VALUE_T_VALUE];

			int nReplace = int.Parse(oValueInfoList[i][KCDefine.U_KEY_VALUE_T_REPLACE]);
			var eValueType = (EValueType)int.Parse(oValueInfoList[i][KCDefine.U_KEY_VALUE_T_VALUE_TYPE]);

			switch(eValueType) {
				case EValueType.BOOL: this.AddBool(oKey, bool.Parse(oValue), nReplace != KCDefine.B_VALUE_INT_0); break;
				case EValueType.INT: this.AddInt(oKey, int.Parse(oValue), nReplace != KCDefine.B_VALUE_INT_0); break;
				case EValueType.FLOAT: this.AddFloat(oKey, float.Parse(oValue), nReplace != KCDefine.B_VALUE_INT_0); break;
				case EValueType.STRING: this.AddString(oKey, oValue, nReplace != KCDefine.B_VALUE_INT_0); break;
			}
		}

		return new List<object>() {
			m_oBoolList, m_oIntList, m_oFloatList, m_oStringList
		};
	}

	//! 값을 로드한다
	public List<object> LoadValuesFromFile(string a_oFilePath) {
		string oString = CFunc.ReadString(a_oFilePath);
		return this.LoadValues(oString);
	}

	//! 값을 로드한다
	public List<object> LoadValuesFromRes(string a_oFilePath) {
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.LoadValues(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}
	#endregion			// 함수
}
