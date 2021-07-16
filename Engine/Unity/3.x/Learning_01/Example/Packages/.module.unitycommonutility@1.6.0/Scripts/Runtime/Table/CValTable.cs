using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 값 테이블
public class CValTable : CSingleton<CValTable> {
	#region 변수
	private Dictionary<string, bool> m_oBoolList = new Dictionary<string, bool>();
	private Dictionary<string, int> m_oIntList = new Dictionary<string, int>();
	private Dictionary<string, float> m_oFltList = new Dictionary<string, float>();
	private Dictionary<string, string> m_oStrList = new Dictionary<string, string>();
	#endregion			// 변수

	#region 함수
	//! 상태를 리셋한다
	public virtual void Reset() {
		m_oBoolList.Clear();
		m_oIntList.Clear();
		m_oFltList.Clear();
		m_oStrList.Clear();
	}

	//! 논리를 반환한다
	public bool GetBool(string a_oKey, bool a_bIsDefVal = false) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oBoolList.ExGetVal(a_oKey, a_bIsDefVal);
	}

	//! 정수를 반환한다
	public int GetInt(string a_oKey, int a_nDefVal = KCDefine.B_VAL_0_INT) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oIntList.ExGetVal(a_oKey, a_nDefVal);
	}

	//! 실수를 반환한다
	public float GetFlt(string a_oKey, float a_fDefVal = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oFltList.ExGetVal(a_oKey, a_fDefVal);
	}

	//! 문자열을 반환한다
	public string GetStr(string a_oKey, string a_oDefStr = KCDefine.B_EMPTY_STR) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oStrList.ExGetVal(a_oKey, a_oDefStr);
	}

	//! 논리를 추가한다
	public void AddBool(string a_oKey, bool a_bIsVal, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oBoolList.ExReplaceVal(a_oKey, a_bIsVal);
		} else {
			m_oBoolList.ExAddVal(a_oKey, a_bIsVal);
		}
	}

	//! 정수를 추가한다
	public void AddInt(string a_oKey, int a_nVal, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oIntList.ExReplaceVal(a_oKey, a_nVal);
		} else {
			m_oIntList.ExAddVal(a_oKey, a_nVal);
		}
	}

	//! 실수를 추가한다
	public void AddFlt(string a_oKey, float a_fVal, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oFltList.ExReplaceVal(a_oKey, a_fVal);
		} else {
			m_oFltList.ExAddVal(a_oKey, a_fVal);
		}
	}

	//! 문자열을 추가한다
	public void AddStr(string a_oKey, string a_oStr, bool a_bIsReplace = false) {
		CAccess.Assert(a_oStr != null && a_oKey.ExIsValid());

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oStrList.ExReplaceVal(a_oKey, a_oStr);
		} else {
			m_oStrList.ExAddVal(a_oKey, a_oStr);
		}
	}
	
	//! 논리를 제거한다
	public void RemoveBool(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oBoolList.ExRemoveVal(a_oKey);
	}

	//! 정수를 제거한다
	public void RemoveInt(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oIntList.ExRemoveVal(a_oKey);
	}

	//! 실수를 제거한다
	public void RemoveFlt(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oFltList.ExRemoveVal(a_oKey);
	}

	//! 문자열을 제거한다
	public void RemoveStr(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oStrList.ExRemoveVal(a_oKey);
	}

	//! 값을 로드한다
	public List<object> LoadVals(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oCSVStr = CFunc.ReadStr(a_oFilePath);

		return this.DoLoadVals(oCSVStr);
	}

	//! 값을 로드한다
	public List<object> LoadValsFromRes(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.DoLoadVals(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}

	//! 값을 로드한다
	private List<object> DoLoadVals(string a_oCSVStr) {
		CAccess.Assert(a_oCSVStr.ExIsValid());
		var oValInfoList = CSVParser.Parse(a_oCSVStr);

		for(int i = 0; i < oValInfoList.Count; ++i) {
			string oKey = oValInfoList[i][KCDefine.U_KEY_VAL_T_ID];
			string oVal = oValInfoList[i][KCDefine.U_KEY_VAL_T_VAL];

			int nReplace = int.Parse(oValInfoList[i][KCDefine.U_KEY_REPLACE]);
			var eValType = (EValType)int.Parse(oValInfoList[i][KCDefine.U_KEY_VAL_T_VAL_TYPE]);

			switch(eValType) {
				case EValType.BOOL: this.AddBool(oKey, int.Parse(oVal) != KCDefine.B_VAL_0_INT, nReplace != KCDefine.B_VAL_0_INT); break;
				case EValType.INT: this.AddInt(oKey, int.Parse(oVal), nReplace != KCDefine.B_VAL_0_INT); break;
				case EValType.FLT: this.AddFlt(oKey, float.Parse(oVal), nReplace != KCDefine.B_VAL_0_INT); break;
				case EValType.STR: this.AddStr(oKey, oVal, nReplace != KCDefine.B_VAL_0_INT); break;
			}
		}

		return new List<object>() {
			m_oBoolList, m_oIntList, m_oFltList, m_oStrList
		};
	}
	#endregion			// 함수
}
