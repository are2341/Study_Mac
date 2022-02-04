﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/** 기본 확장 클래스 */
public static partial class CExtension {
	#region 클래스 함수
	/** 값을 비교한다 */
	public static int ExCompare(this float a_fSender, float a_fRhs) {
		// 값이 동일 할 경우
		if(a_fSender.ExIsEquals(a_fRhs)) {
			return KCDefine.B_COMPARE_EQUALS;
		}
		
		return a_fSender.ExIsLess(a_fRhs) ? KCDefine.B_COMPARE_LESS : KCDefine.B_COMPARE_GREATE;
	}

	/** 값을 비교한다 */
	public static int ExCompare(this double a_dblSender, double a_dblRhs) {
		// 값이 동일 할 경우
		if(a_dblSender.ExIsEquals(a_dblRhs)) {
			return KCDefine.B_COMPARE_EQUALS;
		}

		return a_dblSender.ExIsLess(a_dblRhs) ? KCDefine.B_COMPARE_LESS : KCDefine.B_COMPARE_GREATE;
	}

	/** 시간을 비교한다 */
	public static int ExCompare(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		return a_stSender.ExGetDeltaTime(a_stRhs).ExCompare(KCDefine.B_VAL_0_DBL);
	}

	/** 숫자 => 개수 문자열로 변환한다 */
	public static string ExToNumStr(this int a_nSender, bool a_bIsEnableFltPoint = false) {
		return ((long)a_nSender).ExToNumStr(a_bIsEnableFltPoint);
	}

	/** 숫자 => 개수 문자열로 변환한다 */
	public static string ExToNumStr(this long a_nSender, bool a_bIsEnableFltPoint = false) {
		foreach(var stKeyVal in KCDefine.B_UNIT_NUM_TOKEN_INFO_DICT) {
			long nDigits = a_nSender / stKeyVal.Value;

			// 변환 가능 할 경우
			if(nDigits >= KCDefine.B_VAL_1_INT && nDigits < KCDefine.B_UNIT_DIGITS_PER_THOUSAND) {
				long nCorrectDigitsA = a_nSender / stKeyVal.Value;
				long nCorrectDigitsB = (a_nSender % stKeyVal.Value) / (stKeyVal.Value / KCDefine.B_UNIT_DIGITS_PER_TEN);
				
				return a_bIsEnableFltPoint ? string.Format(KCDefine.B_TEXT_FMT_NUM, nCorrectDigitsA, nCorrectDigitsB, stKeyVal.Key) : string.Format(KCDefine.B_TEXT_FMT_2_COMBINE, nCorrectDigitsA, stKeyVal.Key);
			}
		}
		
		return $"{a_nSender}";
	}

	/** 픽셀 => DPI 픽셀로 변환한다 */
	public static float ExPixelsToDPIPixels(this int a_nSender) {
		return a_nSender * (KCDefine.B_DEF_SCREEN_DPI / CAccess.ScreenDPI);
	}

	/** 픽셀 => DPI 픽셀로 변환한다 */
	public static float ExPixelsToDPIPixels(this float a_fSender) {
		return a_fSender * (KCDefine.B_DEF_SCREEN_DPI / CAccess.ScreenDPI);
	}
	
	/** DPI 픽셀 => 픽셀로 변환한다 */
	public static float ExDPIPixelsToPixels(this int a_nSender) {
		return a_nSender * (CAccess.ScreenDPI / KCDefine.B_DEF_SCREEN_DPI);
	}

	/** DPI 픽셀 => 픽셀로 변환한다 */
	public static float ExDPIPixelsToPixels(this float a_fSender) {
		return a_fSender * (CAccess.ScreenDPI / KCDefine.B_DEF_SCREEN_DPI);
	}

	/** 바이트 => 메가 바이트로 변환한다 */
	public static double ExByteToMegaByte(this uint a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	/** 바이트 => 메가 바이트로 변환한다 */
	public static double ExByteToMegaByte(this long a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	/** 문자열 => 시간으로 변환한다 */
	public static System.DateTime ExToTime(this string a_oSender, string a_oFmt) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFmt.ExIsValid());
		return System.DateTime.ParseExact(a_oSender, a_oFmt, CultureInfo.InvariantCulture);
	}

	/** 시간 => 문자열로 변환한다 */
	public static string ExToStr(this System.DateTime a_stSender, string a_oFmt) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToString(a_oFmt, CultureInfo.InvariantCulture);
	}

	/** 시간 => 긴 문자열로 변환한다 */
	public static string ExToLongStr(this System.DateTime a_stSender, bool a_bIsEnableSplash = true) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(a_bIsEnableSplash ? KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD_HH_MM_SS : KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS);
	}

	/** 시간 => 짧은 문자열로 변환한다 */
	public static string ExToShortStr(this System.DateTime a_stSender, bool a_bIsEnableSplash = true) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(a_bIsEnableSplash ? KCDefine.B_DATE_T_FMT_SLASH_YYYY_MM_DD : KCDefine.B_DATE_T_FMT_YYYY_MM_DD);
	}

	/** 지역 시간 => PST 시간으로 변환한다 */
	public static System.DateTime ExToPSTTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToUniversalTime().AddHours(KCDefine.B_DELTA_T_UTC_TO_PST);
	}

	/** 지역 시간 => 특정 지역 시간으로 변환한다 */
	public static System.DateTime ExToZoneTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		return System.TimeZoneInfo.ConvertTime(a_stSender, System.TimeZoneInfo.Local, System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID));
	}

	/** PST 시간 => 지역 시간으로 변환한다 */
	public static System.DateTime ExPSTToLocalTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.AddHours(-KCDefine.B_DELTA_T_UTC_TO_PST).ToLocalTime();
	}

	/** 특정 지역 시간 => 지역 시간으로 변환한다 */
	public static System.DateTime ExZoneToLocalTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		return System.TimeZoneInfo.ConvertTime(a_stSender, System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID), System.TimeZoneInfo.Local);
	}

	/** 값을 탐색한다 */
	public static int ExFindVal(this SimpleJSON.JSONArray a_oSender, System.Predicate<SimpleJSON.JSONNode> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 값을 탐색했을 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset<T>(this T[] a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 배열이 존재 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oSender[i] = default(T);
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset<T>(this T[,] a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 배열이 존재 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					a_oSender[i, j] = default(T);
				}
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.IndexOf(a_tVal).ExIsValidIdx()) {
			a_oSender.Add(a_tVal);
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExAddVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	/** 값을 추가한다 */
	public static void ExAddVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.TryAdd(stKeyVal.Key, stKeyVal.Value);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVal<T>(this List<T> a_oSender, T a_tVal, T a_tReplaceVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 대체가 가능 할 경우
		if(a_oSender != null) {
			int nIdx = a_oSender.IndexOf(a_tVal);

			// 값 추가가 가능 할 경우
			if(!nIdx.ExIsValidIdx()) {
				a_oSender.Add(a_tReplaceVal);
			} else {
				a_oSender[nIdx] = a_tReplaceVal;
			}
		}
	}
	
	/** 값을 대체한다 */
	public static void ExReplaceVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 대체가 가능 할 경우
		if(a_oSender != null) {
			// 값 추가가 가능 할 경우
			if(!a_oSender.ContainsKey(a_tKey)) {
				a_oSender.Add(a_tKey, a_tVal);
			} else {
				a_oSender[a_tKey] = a_tVal;
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVals<T>(this List<T> a_oSender, List<(T, T)> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExReplaceVal(a_oValList[i].Item1, a_oValList[i].Item2, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 대체한다 */
	public static void ExReplaceVals<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExReplaceVal(stKeyVal.Key, stKeyVal.Value, a_bIsEnableAssert);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender.RemoveAt(a_nIdx);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.Contains(a_tVal)) {
			a_oSender.ExRemoveValAt(a_oSender.IndexOf(a_tVal), a_bIsEnableAssert);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<T>(this List<T> a_oSender, System.Predicate<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			int nIdx = a_oSender.FindIndex(a_oCompare);

			// 인덱스가 유효 할 경우
			if(a_oSender.ExIsValidIdx(nIdx)) {
				a_oSender.ExRemoveValAt(nIdx);
			}
		}
	}
	
	/** 값을 제거한다 */
	public static void ExRemoveVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Remove(a_tKey);
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<V> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stResult = a_oSender.ExFindVal(a_oCompare);

			// 키가 유효 할 경우
			if(stResult.Item1) {
				a_oSender.ExRemoveVal(stResult.Item2);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	/** 값을 제거한다 */
	public static void ExRemoveVals<K, V>(this Dictionary<K, V> a_oSender, List<K> a_oKeyList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oKeyList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oKeyList != null) {
			for(int i = 0; i < a_oKeyList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oKeyList[i], a_bIsEnableAssert);
			}
		}
	}
	
	/** 값을 탐색한다 */
	public static int ExFindVal<T>(this T[] a_oSender, System.Predicate<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 값을 탐색했을 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}
	
	/** 값을 탐색한다 */
	public static (bool, K) ExFindVal<K, V>(this Dictionary<K, V> a_oSender, System.Predicate<V> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		foreach(var stKeyVal in a_oSender) {
			// 값을 탐색했을 경우
			if(a_oCompare(stKeyVal.Value)) {
				return (true, stKeyVal.Key);
			}
		}
		
		return (false, default(K));
	}
	
	/** 값을 교환한다 */
	public static void ExSwap<T>(this T[] a_oSender, int a_nIdxA, int a_nIdxB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB))) {
			T tTemp = a_oSender[a_nIdxA]; a_oSender[a_nIdxA] = a_oSender[a_nIdxB]; a_oSender[a_nIdxB] = tTemp;
		}
	}

	/** 값을 교환한다 */
	public static void ExSwap<T>(this T[,] a_oSender, Vector2Int a_stIdxA, Vector2Int a_stIdxB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_stIdxA) && a_oSender.ExIsValidIdx(a_stIdxB)));
		
		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_stIdxA) && a_oSender.ExIsValidIdx(a_stIdxB))) {
			T tTemp = a_oSender[a_stIdxA.y, a_stIdxA.x]; a_oSender[a_stIdxA.y, a_stIdxA.x] = a_oSender[a_stIdxB.y, a_stIdxB.x]; a_oSender[a_stIdxB.y, a_stIdxB.x] = tTemp;
		}
	}
	
	/** 값을 교환한다 */
	public static void ExSwap<T>(this List<T> a_oSender, int a_nIdxA, int a_nIdxB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB))) {
			T tTemp = a_oSender[a_nIdxA]; a_oSender[a_nIdxA] = a_oSender[a_nIdxB]; a_oSender[a_nIdxB] = tTemp;
		}
	}

	/** 값을 교환한다 */
	public static void ExSwap<K, V>(this Dictionary<K, V> a_oSender, K a_tKeyA, K a_tKeyB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValid() && a_oSender.ContainsKey(a_tKeyA) && a_oSender.ContainsKey(a_tKeyB)));

		// 키가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ContainsKey(a_tKeyA) && a_oSender.ContainsKey(a_tKeyB))) {
			V tTemp = a_oSender[a_tKeyA]; a_oSender[a_tKeyA] = a_oSender[a_tKeyB]; a_oSender[a_tKeyB] = tTemp;
		}
	}

	/** 값을 재배치한다 */
	public static void ExShuffle<T>(this T[] a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재베치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oSender.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Length), a_bIsEnableAssert);
			}
		}
	}

	/** 값을 재배치한다 */
	public static void ExShuffle<T>(this List<T> a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재배치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Count; ++i) {
				a_oSender.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Count), a_bIsEnableAssert);
			}
		}
	}

	/** 리스트를 복사한다 */
	public static void ExCopyTo<T>(this List<T> a_oSender, List<T> a_oDestValList, System.Func<T, T> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValList != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValList != null && a_oCallback != null)) {
			a_oDestValList.Clear();

			for(int i = 0; i < a_oSender.Count; ++i) {
				a_oDestValList.Add(a_oCallback(a_oSender[i]));
			}
		}
	}

	/** 딕셔너리를 복사한다 */
	public static void ExCopyTo<K, V>(this Dictionary<K, V> a_oSender, Dictionary<K, V> a_oDestValDict, System.Func<V, V> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValDict != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValDict != null && a_oCallback != null)) {
			a_oDestValDict.Clear();

			foreach(var stKeyVal in a_oSender) {
				a_oDestValDict.TryAdd(stKeyVal.Key, a_oCallback(stKeyVal.Value));
			}
		}
	}

	/** 1 차원 배열 => 2 차원 배열로 복사한다 */
	public static void ExCopyToMultiArray<T>(this T[] a_oSender, T[,] a_oDestVals, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				a_oDestVals.ExSetVal(new Vector2Int(i % a_oDestVals.GetLength(KCDefine.B_VAL_1_INT), i / a_oDestVals.GetLength(KCDefine.B_VAL_1_INT)), a_oSender[i], a_bIsEnableAssert);
			}
		}
	}

	/** 2 차원 배열 => 1 차원 배열로 복사한다 */
	public static void ExCopyToSingleArray<T>(this T[,] a_oSender, T[] a_oDestVals, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					a_oDestVals.ExSetVal((i * a_oSender.GetLength(KCDefine.B_VAL_1_INT)) + j, a_oSender[i, j], a_bIsEnableAssert);
				}
			}
		}
	}

	/** 1 차원 배열 => 2 차원 배열로 변환한다 */
	public static T[,] ExToMultiArray<T>(this T[] a_oSender, int a_nNumRows, int a_nNumCols) {
		CAccess.Assert(a_oSender != null && a_nNumRows > KCDefine.B_VAL_0_INT && a_nNumCols > KCDefine.B_VAL_0_INT);

		var oVals = new T[a_nNumRows, a_nNumCols];
		a_oSender.ExCopyToMultiArray(oVals);

		return oVals;
	}

	/** 2 차원 배열 => 1 차원 배열로 변환한다 */
	public static T[] ExToSingleArray<T>(this T[,] a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		var oVals = new T[a_oSender.Length];
		a_oSender.ExCopyToSingleArray(oVals);

		return oVals;
	}

	/** 문자열 => 열거형 값으로 변환한다 */
	public static T ExToEnumVal<T>(this string a_oSender, bool a_bIsIgnoreCase = false) where T : struct {
		return (T)System.Enum.Parse(typeof(T), a_oSender, a_bIsIgnoreCase);
	}

	/** 문자열 => 열거형 값으로 변환한다 */
	public static bool ExToTryEnumVal<T>(this string a_oSender, out T a_tEnumVal, bool a_bIsIgnoreCase = false) where T : struct {
		return System.Enum.TryParse<T>(a_oSender, a_bIsIgnoreCase, out a_tEnumVal);
	}

	/** 배열 => 문자열로 변환한다 */
	public static string ExToStr<T>(this T[] a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Length; ++i) {
			oStrBuilder.Append(a_oSender[i]);
			oStrBuilder.Append((i < a_oSender.Length - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);
		}

		return oStrBuilder.ToString();
	}

	/** 리스트 => 문자열로 변환한다 */
	public static string ExToStr<T>(this List<T> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oStrBuilder.Append(a_oSender[i]);
			oStrBuilder.Append((i < a_oSender.Count - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);
		}

		return oStrBuilder.ToString();
	}
	
	/** 사전 => 문자열로 변환한다 */
	public static string ExToStr<K, V>(this Dictionary<K, V> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());

		int i = KCDefine.B_VAL_0_INT;
		var oStrBuilder = new System.Text.StringBuilder();

		foreach(var stKeyVal in a_oSender) {
			oStrBuilder.AppendFormat(KCDefine.B_TEXT_FMT_DICT, stKeyVal.Key, stKeyVal.Value);
			oStrBuilder.Append((i < a_oSender.Count - KCDefine.B_VAL_1_INT) ? a_oToken : string.Empty);

			i += KCDefine.B_VAL_1_INT;
		}

		return oStrBuilder.ToString();
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static List<T> ExToTypes<T>(this object[] a_oSender) where T : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<T>();

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 타입 추가가 가능 할 경우
			if(a_oSender[i] as T != null) {
				oTypeList.Add(a_oSender[i] as T);
			}
		}

		return oTypeList;
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static List<DestT> ExToTypes<SrcT, DestT>(this List<SrcT> a_oSender) where SrcT : class where DestT : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<DestT>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 타입 추가가 가능 할 경우
			if(a_oSender[i] as DestT != null) {
				oTypeList.Add(a_oSender[i] as DestT);
			}
		}

		return oTypeList;
	}

	/** 객체 => 특정 타입으로 변환한다 */
	public static Dictionary<DestK, DestV> ExToTypes<SrcK, SrcV, DestK, DestV>(this Dictionary<SrcK, SrcV> a_oSender) where SrcK : class where SrcV : class where DestK : class where DestV : class {
		CAccess.Assert(a_oSender != null);
		var oTypeDict = new Dictionary<DestK, DestV>();

		foreach(var stKeyVal in a_oSender) {
			// 타입 추가가 가능 할 경우
			if(stKeyVal.Key as DestK != null && stKeyVal.Value as DestV != null) {
				oTypeDict.Add(stKeyVal.Key as DestK, stKeyVal.Value as DestV);
			}
		}

		return oTypeDict;
	}

	/** 객체 => 특정 리스트 타입으로 변환한다 */
	public static List<DestT> ExToListTypes<SrcK, SrcV, DestT>(this Dictionary<SrcK, SrcV> a_oSender) where SrcK : class where SrcV : class where DestT : class {
		CAccess.Assert(a_oSender != null);
		var oTypeList = new List<DestT>();

		foreach(var stKeyVal in a_oSender) {
			// 타입 추가가 가능 할 경우
			if(stKeyVal.Value as DestT != null) {
				oTypeList.Add(stKeyVal.Value as DestT);
			}
		}

		return oTypeList;
	}
	
	/** 함수를 호출한다 */
	public static object ExCallFunc<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, List<object> a_oParamsList) {
		CAccess.Assert(a_oName.ExIsValid());
		return typeof(T).GetMethod(a_oName, a_eBindingFlags).Invoke(a_oSender, a_oParamsList.ToArray());
	}
	
	/** 런타임 함수를 호출한다 */
	public static object ExRuntimeCallFunc<T>(this object a_oSender, string a_oName, List<object> a_oParamsList) {
		CAccess.Assert(a_oName.ExIsValid());
		var oMethodInfos = typeof(T).GetRuntimeMethods();
		
		foreach(var oMethodInfo in oMethodInfos) {
			// 메서드 이름이 동일 할 경우
			if(oMethodInfo.Name.Equals(a_oName)) {
				return oMethodInfo.Invoke(a_oSender, a_oParamsList.ToArray());
			}
		}

		return null;
	}
	#endregion			// 제네릭 클래스 함수
}
