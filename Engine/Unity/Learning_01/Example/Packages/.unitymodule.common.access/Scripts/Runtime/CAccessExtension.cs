﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

//! 기본 접근 확장 클래스
public static partial class CAccessExtension {
	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this System.DateTime a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VALUE_LONG_0;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this System.TimeSpan a_stSender) {
		return a_stSender.Ticks >= KCDefine.B_VALUE_LONG_0;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this string a_oSender) {
		return a_oSender != null && a_oSender.Length > KCDefine.B_VALUE_INT_0;
	}

	//! 빌드 번호 유효 여부를 검사한다
	public static bool ExIsValidBuildNumber(this int a_nSender) {
		return a_nSender >= KCDefine.B_MIN_BUILD_NUMBER;
	}

	//! 빌드 버전 유효 여부를 검사한다
	public static bool ExIsValidBuildVersion(this string a_oSender) {
		return System.Version.TryParse(a_oSender, out System.Version stVersion);
	}

	//! 언어 유효 여부를 검사한다
	public static bool ExIsValidLanguage(this string a_oSender) {
		// 언어가 유효하지 않을 경우
		if(!a_oSender.ExIsValid()) {
			return false;
		}

		return !a_oSender.ExIsEquals(KCDefine.B_UNKNOWN_LANGUAGE);
	}

	//! 국가 코드 유효 여부를 검사한다
	public static bool ExIsValidCountryCode(this string a_oSender) {
		// 국가 코드가 유효하지 않을 경우
		if(!a_oSender.ExIsValid()) {
			return false;
		}

		string oCountryCode = a_oSender.ToUpper();
		return !oCountryCode.ExIsEquals(KCDefine.B_UNKNOWN_COUNTRY_CODE);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this float a_fSender, float a_fRhs) {
		return Mathf.Approximately(a_fSender, a_fRhs);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this double a_dblSender, double a_dblRhs) {
		double dblDeltaTime = System.Math.Abs(a_dblSender) - System.Math.Abs(a_dblRhs);
		return dblDeltaTime >= -double.Epsilon && dblDeltaTime <= double.Epsilon;
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this string a_oSender, string a_oString) {
		CAccess.Assert(a_oSender != null && a_oString != null);
		return a_oSender.Equals(a_oString);
	}

	//! 포함 여부를 검사한다
	public static bool ExIsContains(this string a_oSender, string a_oString) {
		CAccess.Assert(a_oSender != null && a_oString != null);
		return a_oSender.Contains(a_oString);
	}

	//! 작음 여부를 검사한다
	public static bool ExIsLess(this float a_fSender, float a_fRhs) {
		return a_fSender < a_fRhs - float.Epsilon;
	}

	//! 작거나 같음 여부를 검사한다
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsLess(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	//! 큰 여부를 검사한다
	public static bool ExIsGreate(this float a_fSender, float a_fRhs) {
		return a_fSender > a_fRhs + float.Epsilon;
	}

	//! 크거나 같음 여부를 검사한다
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsGreate(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	//! 작음 여부를 검사한다
	public static bool ExIsLess(this double a_dblSender, double a_dblRhs) {
		return a_dblSender < a_dblRhs - double.Epsilon;
	}

	//! 작거나 같음 여부를 검사한다
	public static bool ExIsLessEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsLess(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	//! 큰 여부를 검사한다
	public static bool ExIsGreate(this double a_dblSender, double a_dblRhs) {
		return a_dblSender > a_dblRhs + double.Epsilon;
	}

	//! 크거나 같음 여부를 검사한다
	public static bool ExIsGreateEquals(this double a_dblSender, double a_dblRhs) {
		return a_dblSender.ExIsGreate(a_dblRhs) || a_dblSender.ExIsEquals(a_dblRhs);
	}

	//! 완료 여부를 검사한다
	public static bool ExIsComplete(this Task a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.IsCompleted && !a_oSender.IsFaulted && !a_oSender.IsCanceled;
	}

	//! 유럽 연합 여부를 검사한다
	public static bool ExIsEU(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		string oCountryCode = a_oSender.ToUpper();

		for(int i = 0; i < KCDefine.B_EU_COUNTRY_CODES.Length; ++i) {
			// 국가 코드가 동일 할 경우
			if(oCountryCode.ExIsEquals(KCDefine.B_EU_COUNTRY_CODES[i])) {
				return true;
			}
		}

		return false;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTime(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalSeconds;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerMinutes(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalMinutes;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerHours(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalHours;
	}

	//! 시간 간격을 반환한다
	public static double ExGetDeltaTimePerDays(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).TotalDays;
	}

	//! 시간 간격을 반환한다
	public static long ExGetDeltaTimePerTicks(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		CAccess.Assert(a_stSender.ExIsValid() && a_stRhs.ExIsValid());
		return (a_stSender - a_stRhs).Ticks;
	}

	//! 이전 인덱스를 반환한다
	public static Vector2Int ExGetPrevIdx(this Vector2Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_2D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);

		return a_stSender + KCDefine.B_IDX_OFFSETS_PREV_2D[(int)a_eDirection];
	}
	
	//! 다음 인덱스를 반환한다
	public static Vector2Int ExGetNextIdx(this Vector2Int a_stSender, EDirection a_eDirection) {
		CAccess.Assert(!a_stSender.Equals(KCDefine.B_IDX_INVALID_2D));
		CAccess.Assert(a_eDirection >= EDirection.UP && a_eDirection <= EDirection.RIGHT_DOWN);

		return a_stSender + KCDefine.B_IDX_OFFSETS_NEXT_2D[(int)a_eDirection];
	}

	//! 변경 된 문자열을 반환한다
	public static string ExGetReplaceString(this string a_oSender, string a_oTarget, string a_oReplace, int a_nReplaceTimes = KCDefine.B_VALUE_INT_1) {
		CAccess.Assert(a_oSender != null && a_oTarget.ExIsValid());

		for(int i = 0; i < a_nReplaceTimes && a_oSender.ExIsContains(a_oTarget); ++i) {
			a_oSender = a_oSender.Replace(a_oTarget, a_oReplace);
		}

		return a_oSender;
	}

	//! 파일 이름이 변경 된 경로를 반환한다
	public static string ExGetReplaceFileNamePath(this string a_oSender, string a_oFileName, bool a_bIsResetExtension = false) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFileName.ExIsValid());
		var oFileName = a_bIsResetExtension ? Path.GetFileName(a_oSender) : Path.GetFileNameWithoutExtension(a_oSender);

		return a_oSender.ExGetReplaceString(oFileName, a_oFileName);
	}

	//! 리스트 => 비트로 변환한다
	public static int ExToBits(this List<int> a_oSender) {
		CAccess.Assert(a_oSender != null);
		int nValue = KCDefine.B_VALUE_INT_0;

		for(int i = 0; i < a_oSender.Count; ++i) {
			nValue |= 1 << a_oSender[i];
		}

		return nValue;
	}

	//! 로컬 => 월드로 변환한다
	public static Vector3 ExToWorld(this Vector3 a_stSender, GameObject a_oObj, bool a_bIsCoord = true) {
		return a_bIsCoord ? a_oObj.transform.TransformPoint(a_stSender) : a_oObj.transform.TransformDirection(a_stSender);
	}

	//! 월드 => 로컬로 변환한다
	public static Vector3 ExToLocal(this Vector3 a_stSender, GameObject a_oObj, bool a_bIsCoord = true) {
		return a_bIsCoord ? a_oObj.transform.InverseTransformPoint(a_stSender) : a_oObj.transform.InverseTransformDirection(a_stSender);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[] a_oSender) {
		return a_oSender != null && a_oSender.Length > KCDefine.B_VALUE_INT_0;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this T[,] a_oSender) {
		// 배열이 유효하지 않을 경우
		if(a_oSender == null) {
			return false;
		}

		bool bIsValid = a_oSender.GetLength(KCDefine.B_VALUE_INT_0) > KCDefine.B_VALUE_INT_0;
		return bIsValid && a_oSender.GetLength(KCDefine.B_VALUE_INT_1) > KCDefine.B_VALUE_INT_0;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<T>(this List<T> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VALUE_INT_0;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid<Key, Value>(this Dictionary<Key, Value> a_oSender) {
		return a_oSender != null && a_oSender.Count > KCDefine.B_VALUE_INT_0;
	}

	//! 인덱스 유효 여부를 검사한다
	public static bool ExIsValidIdx<T>(this T[] a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Length;
	}

	//! 인덱스 유효 여부를 검사한다
	public static bool ExIsValidIdx<T>(this T[,] a_oSender, Vector2Int a_stIdx) {
		CAccess.Assert(a_oSender != null);

		int nNumRows = a_oSender.GetLength(KCDefine.B_VALUE_INT_0);
		int nNumCols = a_oSender.GetLength(KCDefine.B_VALUE_INT_1);

		bool bIsValid = a_stIdx.y > KCDefine.B_IDX_INVALID && a_stIdx.y < nNumRows;
		return bIsValid && a_stIdx.x > KCDefine.B_IDX_INVALID && a_stIdx.x < nNumCols;
	}

	//! 인덱스 유효 여부룰 검사한다
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	//! 포함 여부를 검사한다
	public static bool ExIsContains<T>(this T[] a_oSender, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 값이 동일 할 경우
			if(a_oSender[i].Equals(a_tValue)) {
				return true;
			}
		}

		return false;
	}

	//! 포함 여부를 검사한다
	public static bool ExIsContains<T>(this T[,] a_oSender, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VALUE_INT_0); ++i) {
			for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VALUE_INT_1); ++j) {
				// 값이 동일 할 경우
				if(a_oSender[i, j].Equals(a_tValue)) {
					return true;
				}
			}
		}

		return false;
	}

	//! 완료 여부를 검사한다
	public static bool ExIsComplete<T>(this Task<T> a_oSender) {
		CAccess.Assert(a_oSender != null);
		var oTask = a_oSender as Task;

		return oTask.ExIsComplete() && a_oSender.Result != null;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this T[] a_oSender, int a_nIdx, T a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefValue;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this T[,] a_oSender, Vector2Int a_stIdx, T a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_stIdx) ? a_oSender[a_stIdx.y, a_stIdx.x] : a_tDefValue;
	}

	//! 값을 반환한다
	public static T ExGetValue<T>(this List<T> a_oSender, int a_nIdx, T a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExIsValidIdx(a_nIdx) ? a_oSender[a_nIdx] : a_tDefValue;
	}

	//! 값을 반환한다
	public static Value ExGetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tDefValue) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ContainsKey(a_tKey) ? a_oSender[a_tKey] : a_tDefValue;
	}

	//! 필드 값을 반환한다
	public static object ExGetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		CAccess.Assert(a_oName.ExIsValid());
		var oFieldInfo = typeof(T).GetField(a_oName, a_eBindingFlags);

		return oFieldInfo.GetValue(a_oSender);
	}

	//! 런타임 필드 값을 반환한다
	public static object ExGetRuntimeFieldValue<T>(this object a_oSender, string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		var oFieldInfos = typeof(T).GetRuntimeFields();

		foreach(var oFieldInfo in oFieldInfos) {
			// 필드 이름이 동일 할 경우
			if(oFieldInfo.Name.ExIsEquals(a_oName)) {
				return oFieldInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	//! 프로퍼티 값을 반환한다
	public static object ExGetPropertyValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags) {
		CAccess.Assert(a_oName.ExIsValid());
		var oPropertyInfo = typeof(T).GetProperty(a_oName, a_eBindingFlags);

		return oPropertyInfo.GetValue(a_oSender);
	}

	//! 런타임 프로퍼티 값을 반환한다
	public static object ExGetRuntimePropertyValue<T>(this object a_oSender, string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		var oPropertyInfos = typeof(T).GetRuntimeProperties();
		
		foreach(var oPropertyInfo in oPropertyInfos) {
			// 프로퍼티 이름과 동일 할 경우
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				return oPropertyInfo.GetValue(a_oSender);
			}
		}

		return null;
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this T[] a_oSender, int a_nIdx, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender[a_nIdx] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<T>(this List<T> a_oSender, int a_nIdx, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender[a_nIdx] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 키가 유효 할 경우
		if(a_oSender.ContainsKey(a_tKey)) {
			a_oSender[a_tKey] = a_tValue;
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<T>(this T[] a_oSender, List<int> a_oIdxList, List<T> a_oValueList) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_oIdxList != null && a_oValueList != null);

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			a_oSender.ExSetValue(a_oIdxList[i], a_oValueList[i]);
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<T>(this List<T> a_oSender, List<int> a_oIdxList, List<T> a_oValueList) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_oIdxList != null && a_oValueList != null);

		for(int i = 0; i < a_oIdxList.Count; ++i) {
			a_oSender.ExSetValue(a_oIdxList[i], a_oValueList[i]);
		}
	}

	//! 값을 변경한다
	public static void ExSetValues<Key, Value>(this Dictionary<Key, Value> a_oSender, Dictionary<Key, Value> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		foreach(var stKeyValue in a_oValueList) {
			a_oSender.ExSetValue(stKeyValue.Key, stKeyValue.Value);
		}
	}

	//! 필드 값을 변경한다
	public static void ExSetFieldValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		CAccess.Assert(a_oName.ExIsValid());
		var oFieldInfo = typeof(T).GetField(a_oName, a_eBindingFlags);

		oFieldInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 필드 값을 변경한다
	public static void ExSetRuntimeFieldValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		CAccess.Assert(a_oName.ExIsValid());
		var oFieldInfos = typeof(T).GetRuntimeFields();

		foreach(var oFieldInfo in oFieldInfos) {
			// 필드 이름이 동일 할 경우
			if(oFieldInfo.Name.ExIsEquals(a_oName)) {
				oFieldInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}

	//! 프로퍼티 값을 변경한다
	public static void ExSetPropertyValue<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object a_oValue) {
		CAccess.Assert(a_oName.ExIsValid());
		var oPropertyInfo = typeof(T).GetProperty(a_oName, a_eBindingFlags);

		oPropertyInfo.SetValue(a_oSender, a_oValue);
	}

	//! 런타임 프로퍼티 값을 변경한다
	public static void ExSetRuntimePropertyValue<T>(this object a_oSender, string a_oName, object a_oValue) {
		CAccess.Assert(a_oName.ExIsValid());
		var oPropertyInfos = typeof(T).GetRuntimeProperties();
		
		foreach(var oPropertyInfo in oPropertyInfos) {
			// 프로퍼티 이름이 동일 할 경우
			if(oPropertyInfo.Name.ExIsEquals(a_oName)) {
				oPropertyInfo.SetValue(a_oSender, a_oValue);
			}
		}
	}
	#endregion			// 제네릭 클래스 함수
}
