using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 값 테이블 */
public partial class CValTable : CSingleton<CValTable> {
	#region 변수
	private Dictionary<string, int> m_oIntDict = new Dictionary<string, int>();
	private Dictionary<string, float> m_oRealDict = new Dictionary<string, float>();
	private Dictionary<string, string> m_oStrDict = new Dictionary<string, string>();
	#endregion			// 변수

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset() {
		base.Reset();

		m_oIntDict?.Clear();
		m_oRealDict?.Clear();
		m_oStrDict?.Clear();
	}

	/** 정수를 반환한다 */
	public int GetInt(string a_oKey, int a_nDefVal = KCDefine.B_VAL_0_INT) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oIntDict.GetValueOrDefault(a_oKey, a_nDefVal);
	}

	/** 실수를 반환한다 */
	public float GetReal(string a_oKey, float a_fDefVal = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oRealDict.GetValueOrDefault(a_oKey, a_fDefVal);
	}

	/** 문자열을 반환한다 */
	public string GetStr(string a_oKey, string a_oDefStr = KCDefine.B_TEXT_EMPTY) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oStrDict.GetValueOrDefault(a_oKey, a_oDefStr);
	}

	/** 정수를 추가한다 */
	public void AddInt(string a_oKey, int a_nVal, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 정수 추가가 가능 할 경우
		if(a_bIsReplace || !m_oIntDict.ContainsKey(a_oKey)) {
			m_oIntDict.ExReplaceVal(a_oKey, a_nVal);
		}
	}

	/** 실수를 추가한다 */
	public void AddReal(string a_oKey, float a_fVal, bool a_bIsReplace = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 실수 추가가 가능 할 경우
		if(a_bIsReplace || !m_oRealDict.ContainsKey(a_oKey)) {
			m_oRealDict.ExReplaceVal(a_oKey, a_fVal);
		}
	}

	/** 문자열을 추가한다 */
	public void AddStr(string a_oKey, string a_oStr, bool a_bIsReplace = false) {
		CAccess.Assert(a_oStr != null && a_oKey.ExIsValid());

		// 문자열 추가가 가능 할 경우
		if(a_bIsReplace || !m_oStrDict.ContainsKey(a_oKey)) {
			m_oStrDict.ExReplaceVal(a_oKey, a_oStr);
		}
	}

	/** 정수를 제거한다 */
	public void RemoveInt(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oIntDict.ExRemoveVal(a_oKey);
	}

	/** 실수를 제거한다 */
	public void RemoveFlt(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oRealDict.ExRemoveVal(a_oKey);
	}

	/** 문자열을 제거한다 */
	public void RemoveStr(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oStrDict.ExRemoveVal(a_oKey);
	}

	/** 값을 로드한다 */
	public List<object> LoadVals(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return this.DoLoadVals(CFunc.ReadStr(a_oFilePath));
	}

	/** 값을 로드한다 */
	public List<object> LoadValsFromRes(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.DoLoadVals(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}

	/** 값을 로드한다 */
	private List<object> DoLoadVals(string a_oCSVStr) {
		CAccess.Assert(a_oCSVStr.ExIsValid());
		var oValInfoList = CSVParser.Parse(a_oCSVStr);

		for(int i = 0; i < oValInfoList.Count; ++i) {
			var eValType = (EValType)int.Parse(oValInfoList[i][KCDefine.U_KEY_VAL_T_VAL_TYPE]);
			int nReplace = int.Parse(oValInfoList[i][KCDefine.U_KEY_REPLACE]);

			string oKey = oValInfoList[i][KCDefine.U_KEY_VAL_T_ID];
			string oVal = oValInfoList[i][KCDefine.U_KEY_VAL_T_VAL];

			switch(eValType) {
				case EValType.INT: this.AddInt(oKey, int.Parse(oVal), nReplace != KCDefine.B_VAL_0_INT); break;
				case EValType.REAL: this.AddReal(oKey, float.Parse(oVal), nReplace != KCDefine.B_VAL_0_INT); break;
				case EValType.STR: this.AddStr(oKey, oVal, nReplace != KCDefine.B_VAL_0_INT); break;
			}
		}

		return new List<object>() {
			m_oIntDict, m_oRealDict, m_oStrDict
		};
	}
	#endregion			// 함수
}
