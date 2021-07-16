using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 문자열 테이블
public class CStrTable : CSingleton<CStrTable> {
	#region 변수
	private Dictionary<string, string> m_oStrList = new Dictionary<string, string>();
	private Dictionary<ulong, string> m_oEnumStrList = new Dictionary<ulong, string>();
	#endregion			// 변수

	#region 함수
	//! 상태를 리셋한다
	public virtual void Reset() {
		m_oStrList.Clear();
	}

	//! 문자열을 반환한다
	public string GetStr(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oStrList.ExGetVal(a_oKey, a_oKey);
	}

	//! 문자열을 추가한다
	public void AddStr(string a_oKey, string a_oStr, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oStr != null);

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oStrList.ExReplaceVal(a_oKey, a_oStr);
		} else {
			m_oStrList.ExAddVal(a_oKey, a_oStr);
		}
	}

	//! 문자열을 제거한다
	public void RemoveStr(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oStrList.ExRemoveVal(a_oKey);
	}

	//! 문자열을 로드한다
	public Dictionary<string, string> LoadStrs(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oCSVStr = CFunc.ReadStr(a_oFilePath);

		return this.DoLoadStrs(oCSVStr);
	}

	//! 문자열을 로드한다
	public Dictionary<string, string> LoadStrsFromRes(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.DoLoadStrs(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}

	//! 문자열을 로드한다
	private Dictionary<string, string> DoLoadStrs(string a_oCSVStr) {
		CAccess.Assert(a_oCSVStr.ExIsValid());
		var oStrInfoList = CSVParser.Parse(a_oCSVStr);

		for(int i = 0; i < oStrInfoList.Count; ++i) {
			int nReplace = int.Parse(oStrInfoList[i][KCDefine.U_KEY_REPLACE]);
			
			string oKey = oStrInfoList[i][KCDefine.U_KEY_STR_T_ID];
			string oStr = oStrInfoList[i][KCDefine.U_KEY_STR_T_STR];

			this.AddStr(oKey, oStr, nReplace != KCDefine.B_VAL_0_INT);
		}

		return m_oStrList;
	}
	#endregion			// 함수

	#region 제네릭 함수
	//! 열거형 문자열을 반환한다
	public string GetEnumStr<T>(T a_tKey) where T : struct {
		ulong nKey = (ulong)typeof(T).GetHashCode() << (sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE);
		return m_oEnumStrList.ExGetVal(nKey + (ulong)a_tKey.GetHashCode(), string.Empty);
	}

	//! 열거형 문자열을 추가한다
	public void AddEnumStr<T>(T a_tKey, string a_oStr, bool a_bIsReplace = false) where T : struct {
		ulong nKey = (ulong)typeof(T).GetHashCode() << (sizeof(int) * KCDefine.B_UNIT_BITS_PER_BYTE);
		nKey += (ulong)a_tKey.GetHashCode();

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oEnumStrList.ExReplaceVal(nKey, a_oStr);
		} else {
			m_oEnumStrList.ExAddVal(nKey, a_oStr);
		}		
	}

	//! 열거형 문자열을 로드한다
	public void LoadEnumStrs<T>(bool a_bIsReplace = false) where T : struct {
		var oEnumVals = CAccess.GetEnumVals<T>();

		for(int i = 0; i < oEnumVals.Length; ++i) {
			this.AddEnumStr(oEnumVals[i], oEnumVals[i].ToString(), a_bIsReplace);
		}
	}
	#endregion			// 제네릭 함수
}
