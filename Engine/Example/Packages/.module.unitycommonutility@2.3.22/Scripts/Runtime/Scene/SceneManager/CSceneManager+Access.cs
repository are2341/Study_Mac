using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 씬 관리자 - 접근 */
public abstract partial class CSceneManager : CComponent {
	#region 함수
	/** 객체 풀을 반환한다 */
	public ObjectPool GetObjsPool(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oObjsPoolDict.GetValueOrDefault(a_oKey);
	}
	#endregion			// 함수

	#region 클래스 함수
	/** 터치 응답자를 반환한다 */
	public static GameObject GetTouchResponder(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		return CSceneManager.m_oTouchResponderInfoDict.TryGetValue(a_oKey, out STTouchResponderInfo stTouchResponderInfo) ? stTouchResponderInfo.m_oTouchResponder : null;
	}

	/** 정적 디버그 문자열을 변경한다 */
	public static void SetStaticDebugStr(string a_oStr) {
#if DEBUG || DEVELOPMENT_BUILD
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_STATIC_DEBUG_STR_BUILDER].Clear();
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_STATIC_DEBUG_STR_BUILDER].AppendLine(a_oStr);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 동적 디버그 문자열을 변경한다 */
	public static void SetDynamicDebugStr(string a_oStr) {
#if DEBUG || DEVELOPMENT_BUILD
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_DYNAMIC_DEBUG_STR_BUILDER].Clear();
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_DYNAMIC_DEBUG_STR_BUILDER].AppendLine(a_oStr);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 정적 디버그 문자열을 추가한다 */
	public static void AddStaticDebugStr(string a_oStr) {
#if DEBUG || DEVELOPMENT_BUILD
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_STATIC_DEBUG_STR_BUILDER].AppendLine(a_oStr);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 동적 디버그 문자열을 추가한다 */
	public static void AddDynamicDebugStr(string a_oStr) {
#if DEBUG || DEVELOPMENT_BUILD
		CSceneManager.m_oStrBuilderDict[EKey.EXTRA_DYNAMIC_DEBUG_STR_BUILDER].AppendLine(a_oStr);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 씬 관리자를 반환한다 */
	public static T GetSceneManager<T>(string a_oKey) where T : CSceneManager {
		CAccess.Assert(a_oKey.ExIsValid());
		return CSceneManager.m_oSceneManagerDict.GetValueOrDefault(a_oKey) as T;
	}
	#endregion			// 제네릭 클래스 함수
}
