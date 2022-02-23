using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 지역화 정보 테이블 */
public class CLocalizeInfoTable : CScriptableObj<CLocalizeInfoTable> {
	#region 변수
	[Header("=====> Font Info <=====")]
	[SerializeField] private List<STFontInfo> m_oFontInfoList = new List<STFontInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public List<STFontInfo> FontInfoList => m_oFontInfoList;
	#endregion			// 프로퍼티

	#region 함수
	/** 폰트 정보를 반환한다 */
	public STFontInfo GetFontInfo(string a_oCountryCode, SystemLanguage a_eSystemLanguage) {
		bool bIsValid = this.TryGetFontInfo(a_oCountryCode, a_eSystemLanguage, out STFontInfo stFontInfo);
		CAccess.Assert(bIsValid);

		return stFontInfo;
	}

	/** 폰트 세트 정보를 반환한다 */
	public STFontSetInfo GetFontSetInfo(string a_oCountryCode, SystemLanguage a_eSystemLanguage, EFontSet a_eFontSet) {
		bool bIsValid = this.TryGetFontSetInfo(a_oCountryCode, a_eSystemLanguage, a_eFontSet, out STFontSetInfo stFontSetInfo);
		CAccess.Assert(bIsValid);

		return stFontSetInfo;
	}

	/** 폰트 정보를 반환한다 */
	public bool TryGetFontInfo(string a_oCountryCode, SystemLanguage a_eSystemLanguage, out STFontInfo a_stOutFontInfo) {
		int nIdxA = m_oFontInfoList.FindIndex((a_stFontInfo) => a_stFontInfo.m_eSystemLanguage == a_eSystemLanguage);
		int nIdxB = m_oFontInfoList.FindIndex((a_stFontInfo) => a_stFontInfo.m_oCountryCode.ExIsValid() && a_stFontInfo.m_oCountryCode.Equals(a_oCountryCode));
				
		a_stOutFontInfo = m_oFontInfoList.ExIsValidIdx(nIdxA) ? m_oFontInfoList[nIdxA] : m_oFontInfoList.ExIsValidIdx(nIdxB) ? m_oFontInfoList[nIdxB] : KCDefine.U_INVALID_FONT_INFO;
		return m_oFontInfoList.ExIsValidIdx(nIdxA) || m_oFontInfoList.ExIsValidIdx(nIdxB);
	}

	/** 폰트 세트 정보를 반환한다 */
	public bool TryGetFontSetInfo(string a_oCountryCode, SystemLanguage a_eSystemLanguage, EFontSet a_eFontSet, out STFontSetInfo a_stOutFontSetInfo) {
		// 폰트 정보가 존재 할 경우
		if(this.TryGetFontInfo(a_oCountryCode, a_eSystemLanguage, out STFontInfo stFontInfo)) {
			int nIdx = stFontInfo.m_oFontSetInfoList.FindIndex((a_stFontSetInfo) => a_stFontSetInfo.m_eSet == a_eFontSet);
			a_stOutFontSetInfo = stFontInfo.m_oFontSetInfoList.ExIsValidIdx(nIdx) ? stFontInfo.m_oFontSetInfoList[nIdx] : KCDefine.U_INVALID_FONT_SET_INFO;

			return stFontInfo.m_oFontSetInfoList.ExIsValidIdx(nIdx);
		}

		a_stOutFontSetInfo = KCDefine.U_INVALID_FONT_SET_INFO;
		return false;
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	/** 폰트 정보 변경한다 */
	public void SetFontInfos(List<STFontInfo> a_oFontInfoList) {
		m_oFontInfoList = a_oFontInfoList;
	}
#endif			// #if UNITY_EDITOR

#if NEWTON_SOFT_JSON_MODULE_ENABLE
	/** 폰트 세트 정보를 반환한다 */
	public STFontSetInfo GetFontSetInfo(EFontSet a_eFontSet) {
		bool bIsValid = this.TryGetFontSetInfo(CCommonAppInfoStorage.Inst.CountryCode, CCommonAppInfoStorage.Inst.SystemLanguage, a_eFontSet, out STFontSetInfo stFontSetInfo);
		return bIsValid ? stFontSetInfo : this.GetFontSetInfo(CCommonAppInfoStorage.Inst.CountryCode, KCDefine.B_DEF_LANGUAGE, a_eFontSet);
	}
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
	#endregion			// 조건부 함수
}
