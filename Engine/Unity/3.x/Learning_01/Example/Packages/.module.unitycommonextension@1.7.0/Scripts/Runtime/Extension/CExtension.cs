using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

//! 기본 확장 클래스
public static partial class CExtension {
	#region 클래스 함수
	//! 시간을 비교한다
	public static int ExCompare(this System.DateTime a_stSender, System.DateTime a_stRhs) {
		double dblDeltaTime = a_stSender.ExGetDeltaTime(a_stRhs);

		// 시간이 동일 할 경우
		if(dblDeltaTime.ExIsEquals(KCDefine.B_VAL_0_DBL)) {
			return KCDefine.B_COMPARE_EQUALS;
		}

		return dblDeltaTime.ExIsLess(KCDefine.B_VAL_0_DBL) ? KCDefine.B_COMPARE_LESS : KCDefine.B_COMPARE_GREATE;
	}

	//! 문자열 => 합계로 변환한다
	public static int ExToSumVal(this string a_oSender) {
		CAccess.Assert(a_oSender != null);
		int nSumVal = KCDefine.B_VAL_0_INT;

		for(int i = 0; i < a_oSender.Length; ++i) {
			nSumVal += a_oSender[i];
		}

		return nSumVal;
	}
	
	//! 픽셀 => DPI 픽셀로 변환한다
	public static float ExPixelsToDPIPixels(this int a_nSender) {
		return a_nSender * (Screen.dpi / KCDefine.B_DPI);
	}

	//! 픽셀 => DPI 픽셀로 변환한다
	public static float ExPixelsToDPIPixels(this float a_fSender) {
		return a_fSender * (Screen.dpi / KCDefine.B_DPI);
	}

	//! 바이트 => 메가 바이트로 변환한다
	public static double ExByteToMegaByte(this uint a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	//! 바이트 => 메가 바이트로 변환한다
	public static double ExByteToMegaByte(this long a_oSender) {
		return a_oSender / (double)KCDefine.B_UNIT_BYTES_PER_MEGA_BYTE;
	}

	//! 문자열 => 시간으로 변환한다
	public static System.DateTime ExToTime(this string a_oSender, string a_oFmt) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFmt.ExIsValid());
		return System.DateTime.ParseExact(a_oSender, a_oFmt, CultureInfo.InvariantCulture);
	}

	//! 시간 => 문자열로 변환한다
	public static string ExToStr(this System.DateTime a_stSender, string a_oFmt) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToString(a_oFmt, CultureInfo.InvariantCulture);
	}

	//! 시간 => 긴 문자열로 변환한다
	public static string ExToLongStr(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS);
	}

	//! 시간 => 짧은 문자열로 변환한다
	public static string ExToShortStr(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToStr(KCDefine.B_DATE_T_FMT_YYYY_MM_DD);
	}

	//! 지역 시간 => PST 시간으로 변환한다
	public static System.DateTime ExToPSTTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		var stUTCTime = a_stSender.ToUniversalTime();

		return stUTCTime.AddHours(KCDefine.B_DELTA_T_UTC_TO_PST);
	}

	//! 지역 시간 => 특정 지역 시간으로 변환한다
	public static System.DateTime ExToZoneTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		var oTimeZoneInfo = System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID);

		return System.TimeZoneInfo.ConvertTime(a_stSender, System.TimeZoneInfo.Local, oTimeZoneInfo);
	}

	//! PST 시간 => 지역 시간으로 변환한다
	public static System.DateTime ExPSTToLocalTime(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		var stUTCTime = a_stSender.AddHours(-KCDefine.B_DELTA_T_UTC_TO_PST);

		return stUTCTime.ToLocalTime();
	}

	//! 특정 지역 시간 => 지역 시간으로 변환한다
	public static System.DateTime ExZoneToLocalTime(this System.DateTime a_stSender, string a_oTimeZoneID) {
		CAccess.Assert(a_stSender.ExIsValid() && a_oTimeZoneID.ExIsValid());
		var oTimeZoneInfo = System.TimeZoneInfo.FindSystemTimeZoneById(a_oTimeZoneID);

		return System.TimeZoneInfo.ConvertTime(a_stSender, oTimeZoneInfo, System.TimeZoneInfo.Local);
	}

	//! 값을 탐색한다
	public static int ExFindVal(this SimpleJSON.JSONArray a_oSender, System.Func<SimpleJSON.JSONNode, bool> a_oCompare) {
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
	//! 값을 추가한다
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oSender.IndexOf(a_tVal) <= KCDefine.B_IDX_INVALID) {
			a_oSender.Add(a_tVal);
		}
	}

	//! 값을 추가한다
	public static void ExAddVal<Key, Val>(this Dictionary<Key, Val> a_oSender, Key a_tKey, Val a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Add(a_tKey, a_tVal);
		}
	}

	//! 값을 추가한다
	public static void ExAddVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExAddVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	//! 값을 추가한다
	public static void ExAddVals<Key, Val>(this Dictionary<Key, Val> a_oSender, Dictionary<Key, Val> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 추가가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExAddVal(stKeyVal.Key, stKeyVal.Value, a_bIsEnableAssert);
			}
		}
	}

	//! 값을 대체한다
	public static void ExReplaceVal<T>(this List<T> a_oSender, T a_tVal, T a_tReplaceVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 대체가 가능 할 경우
		if(a_oSender != null) {
			int nIdx = a_oSender.IndexOf(a_tVal);

			// 값 추가가 가능 할 경우
			if(nIdx <= KCDefine.B_IDX_INVALID) {
				a_oSender.Add(a_tReplaceVal);
			} else {
				a_oSender[nIdx] = a_tReplaceVal;
			}
		}
	}
	
	//! 값을 대체한다
	public static void ExReplaceVal<Key, Val>(this Dictionary<Key, Val> a_oSender, Key a_tKey, Val a_tVal, bool a_bIsEnableAssert = true) {
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

	//! 값을 대체한다
	public static void ExReplaceVals<T>(this List<T> a_oSender, List<KeyValuePair<T, T>> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExReplaceVal(a_oValList[i].Key, a_oValList[i].Value, a_bIsEnableAssert);
			}
		}
	}

	//! 값을 대체한다
	public static void ExReplaceVals<Key, Val>(this Dictionary<Key, Val> a_oSender, Dictionary<Key, Val> a_oValDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValDict != null));

		// 값 대체가 가능 할 경우
		if(a_oSender != null && a_oValDict != null) {
			foreach(var stKeyVal in a_oValDict) {
				a_oSender.ExReplaceVal(stKeyVal.Key, stKeyVal.Value, a_bIsEnableAssert);
			}
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender.RemoveAt(a_nIdx);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVal<T>(this List<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.Contains(a_tVal)) {
			int nIdx = a_oSender.IndexOf(a_tVal);
			a_oSender.ExRemoveValAt(nIdx, a_bIsEnableAssert);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVal<T>(this List<T> a_oSender, System.Func<T, bool> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			int nIdx = a_oSender.ExFindVal(a_oCompare);

			// 인덱스가 유효 할 경우
			if(a_oSender.ExIsValidIdx(nIdx)) {
				a_oSender.ExRemoveValAt(nIdx);
			}
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVal<T>(this HashSet<T> a_oSender, T a_tVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.Contains(a_tVal)) {
			a_oSender.Remove(a_tVal);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVal<Key, Val>(this Dictionary<Key, Val> a_oSender, Key a_tKey, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Remove(a_tKey);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVal<Key, Val>(this Dictionary<Key, Val> a_oSender, System.Func<Val, bool> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			var stResult = a_oSender.ExFindVal(a_oCompare);

			// 키가 유효 할 경우
			if(stResult.Value) {
				a_oSender.ExRemoveVal(stResult.Key);
			}
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVals<T>(this List<T> a_oSender, List<T> a_oValList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oValList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oValList != null) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oValList[i], a_bIsEnableAssert);
			}
		}
	}

	//! 값을 제거한다
	public static void ExRemoveVals<Key, Val>(this Dictionary<Key, Val> a_oSender, List<Key> a_oKeyList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oKeyList != null));

		// 값 제거가 가능 할 경우
		if(a_oSender != null && a_oKeyList != null) {
			for(int i = 0; i < a_oKeyList.Count; ++i) {
				a_oSender.ExRemoveVal(a_oKeyList[i], a_bIsEnableAssert);
			}
		}
	}
	
	//! 값을 탐색한다
	public static int ExFindVal<T>(this T[] a_oSender, System.Func<T, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			// 값을 탐색했을 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}

	//! 값을 탐색한다
	public static int ExFindVal<T>(this List<T> a_oSender, System.Func<T, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		
		for(int i = 0; i < a_oSender.Count; ++i) {
			// 값을 탐색했을 경우
			if(a_oCompare(a_oSender[i])) {
				return i;
			}
		}

		return KCDefine.B_IDX_INVALID;
	}

	//! 값을 탐색한다
	public static KeyValuePair<Key, bool> ExFindVal<Key, Val>(this Dictionary<Key, Val> a_oSender, System.Func<Val, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		foreach(var stKeyVal in a_oSender) {
			// 값을 탐색했을 경우
			if(a_oCompare(stKeyVal.Value)) {
				return new KeyValuePair<Key, bool>(stKeyVal.Key, true);
			}
		}

		return new KeyValuePair<Key, bool>(default(Key), false);
	}
	
	//! 값을 교환한다
	public static void ExSwap<T>(this T[] a_oSender, int a_nIdxA, int a_nIdxB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender.ExIsValid());
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB))) {
			T tTemp = a_oSender[a_nIdxA];
			a_oSender[a_nIdxA] = a_oSender[a_nIdxB];
			a_oSender[a_nIdxB] = tTemp;
		}
	}
	
	//! 값을 교환한다
	public static void ExSwap<T>(this List<T> a_oSender, int a_nIdxA, int a_nIdxB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender.ExIsValid());
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB)));

		// 인덱스가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ExIsValidIdx(a_nIdxA) && a_oSender.ExIsValidIdx(a_nIdxB))) {
			T tTemp = a_oSender[a_nIdxA];
			a_oSender[a_nIdxA] = a_oSender[a_nIdxB];
			a_oSender[a_nIdxB] = tTemp;
		}
	}

	//! 값을 교환한다
	public static void ExSwap<Key, Val>(this Dictionary<Key, Val> a_oSender, Key a_tKeyA, Key a_tKeyB, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender.ExIsValid());
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender.ContainsKey(a_tKeyA) && a_oSender.ContainsKey(a_tKeyB)));

		// 키가 유효 할 경우
		if(a_oSender.ExIsValid() && (a_oSender.ContainsKey(a_tKeyA) && a_oSender.ContainsKey(a_tKeyB))) {
			Val tTemp = a_oSender[a_tKeyA];
			a_oSender[a_tKeyA] = a_oSender[a_tKeyB];
			a_oSender[a_tKeyB] = tTemp;
		}
	}

	//! 값을 재배치한다
	public static void ExShuffle<T>(this T[] a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재베치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				int nIdx = Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Length);
				a_oSender.ExSwap(i, nIdx, a_bIsEnableAssert);
			}
		}
	}

	//! 값을 재배치한다
	public static void ExShuffle<T>(this List<T> a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 값 재배치가 가능 할 경우
		if(a_oSender != null) {
			for(int i = 0; i < a_oSender.Count; ++i) {
				int nIdx = Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Count);
				a_oSender.ExSwap(i, nIdx, a_bIsEnableAssert);
			}
		}
	}

	//! 정렬을 수행한다
	public static void ExSort<T>(this T[] a_oSender, System.Comparison<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 정렬이 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			System.Array.Sort(a_oSender, a_oCompare);
		}
	}

	//! 정렬을 수행한다
	public static void ExSort<T>(this List<T> a_oSender, System.Comparison<T> a_oCompare, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCompare != null));

		// 정렬이 가능 할 경우
		if(a_oSender != null && a_oCompare != null) {
			a_oSender.Sort(a_oCompare);
		}
	}

	//! 리스트를 복사한다
	public static void ExCopyTo<T>(this List<T> a_oSender, List<T> a_oDestValList, System.Func<T, T> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValList != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValList != null && a_oCallback != null)) {
			a_oDestValList.Clear();

			for(int i = 0; i < a_oSender.Count; ++i) {
				var tVal = a_oCallback(a_oSender[i]);
				a_oDestValList.Add(tVal);
			}
		}
	}

	//! 딕셔너리를 복사한다
	public static void ExCopyTo<Key, Val>(this Dictionary<Key, Val> a_oSender, Dictionary<Key, Val> a_oDestValDict, System.Func<Val, Val> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestValDict != null && a_oCallback != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && (a_oDestValDict != null && a_oCallback != null)) {
			a_oDestValDict.Clear();

			foreach(var stKeyVal in a_oSender) {
				var tVal = a_oCallback(stKeyVal.Value);
				a_oDestValDict.Add(stKeyVal.Key, tVal);
			}
		}
	}

	//! 1 차원 배열 => 2 차원 배열로 복사한다
	public static void ExCopyToMultiArray<T>(this T[] a_oSender, T[,] a_oDestVals, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.Length; ++i) {
				int nRow = i / a_oDestVals.GetLength(KCDefine.B_VAL_1_INT);
				int nCol = i % a_oDestVals.GetLength(KCDefine.B_VAL_1_INT);

				a_oDestVals[nRow, nCol] = a_oSender[i];
			}
		}
	}

	//! 2 차원 배열 => 1 차원 배열로 복사한다
	public static void ExCopyToSingleArray<T>(this T[,] a_oSender, T[] a_oDestVals, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDestVals != null));

		// 복사가 가능 할 경우
		if(a_oSender != null && a_oDestVals != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					int nIdx = (i * a_oSender.GetLength(KCDefine.B_VAL_1_INT)) + j;
					a_oDestVals[nIdx] = a_oSender[i, j];
				}
			}
		}
	}

	//! 1 차원 배열 => 2 차원 배열로 변환한다
	public static T[,] ExToMultiArray<T>(this T[] a_oSender, int a_nNumRows, int a_nNumCols) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_nNumRows > KCDefine.B_VAL_0_INT && a_nNumCols > KCDefine.B_VAL_0_INT);

		var oConvertVals = new T[a_nNumRows, a_nNumCols];
		a_oSender.ExCopyToMultiArray(oConvertVals);

		return oConvertVals;
	}

	//! 2 차원 배열 => 1 차원 배열로 변환한다
	public static T[] ExToSingleArray<T>(this T[,] a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		var oConvertVals = new T[a_oSender.Length];
		a_oSender.ExCopyToSingleArray(oConvertVals);

		return oConvertVals;
	}

	//! 문자열 => 열거형 값으로 변환한다
	public static T ExToEnumVal<T>(this string a_oSender, bool a_bIsIgnoreCase = false) where T : struct {
		return (T)System.Enum.Parse(typeof(T), a_oSender, a_bIsIgnoreCase);
	}

	//! 문자열 => 열거형 값으로 변환한다
	public static bool ExToTryEnumVal<T>(this string a_oSender, out T a_tEnumVal, bool a_bIsIgnoreCase = false) where T : struct {
		return System.Enum.TryParse<T>(a_oSender, a_bIsIgnoreCase, out a_tEnumVal);
	}

	//! 배열 => 문자열로 변환한다
	public static string ExToStr<T>(this T[] a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Length; ++i) {
			oStrBuilder.Append(a_oSender[i]);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Length - KCDefine.B_VAL_1_INT) {
				oStrBuilder.Append(a_oToken);
			}
		}

		return oStrBuilder.ToString();
	}

	//! 리스트 => 문자열로 변환한다
	public static string ExToStr<T>(this List<T> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oStrBuilder.Append(a_oSender[i]);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Count - KCDefine.B_VAL_1_INT) {
				oStrBuilder.Append(a_oToken);
			}
		}

		return oStrBuilder.ToString();
	}
	
	//! 사전 => 문자열로 변환한다
	public static string ExToStr<Key, Val>(this Dictionary<Key, Val> a_oSender, string a_oToken) {
		CAccess.Assert(a_oSender != null && a_oToken.ExIsValid());

		int i = KCDefine.B_VAL_0_INT;
		var oStrBuilder = new System.Text.StringBuilder();

		foreach(var stKeyVal in a_oSender) {
			oStrBuilder.AppendFormat(KCDefine.B_TEXT_FMT_DICT, stKeyVal.Key, stKeyVal.Value);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Count - KCDefine.B_VAL_1_INT) {
				oStrBuilder.Append(a_oToken);
			}

			i += KCDefine.B_VAL_1_INT;
		}

		return oStrBuilder.ToString();
	}

	//! 객체 => 특정 타입으로 변환한다
	public static List<T> ExToTypes<T>(this object[] a_oSender) where T : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypes = new List<T>();

		for(int i = 0; i < a_oSender.Length; ++i) {
			var oConvertType = a_oSender[i] as T;

			// 타입 추가가 가능 할 경우
			if(oConvertType != null) {
				oConvertTypes.ExAddVal(oConvertType);
			}
		}

		return oConvertTypes;
	}

	//! 객체 => 특정 타입으로 변환한다
	public static List<DestT> ExToTypes<SrcT, DestT>(this List<SrcT> a_oSender) where SrcT : class where DestT : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypeList = new List<DestT>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			var oConvertType = a_oSender[i] as DestT;

			// 타입 추가가 가능 할 경우
			if(oConvertType != null) {
				oConvertTypeList.ExAddVal(oConvertType);
			}
		}

		return oConvertTypeList;
	}

	//! 객체 => 특정 타입으로 변환한다
	public static Dictionary<DestKey, DestVal> ExToTypes<SrcKey, SrcVal, DestKey, DestVal>(this Dictionary<SrcKey, SrcVal> a_oSender) where SrcKey : class where SrcVal : class where DestKey : class where DestVal : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypeDict = new Dictionary<DestKey, DestVal>();

		foreach(var stKeyVal in a_oSender) {
			var oConvertKey = stKeyVal.Key as DestKey;
			var oConvertVal = stKeyVal.Value as DestVal;

			// 타입 추가가 가능 할 경우
			if(oConvertKey != null && oConvertVal != null) {
				oConvertTypeDict.ExAddVal(oConvertKey, oConvertVal);
			}
		}

		return oConvertTypeDict;
	}

	//! 객체 => 특정 리스트 타입으로 변환한다
	public static List<DestT> ExToListTypes<SrcKey, SrcVal, DestT>(this Dictionary<SrcKey, SrcVal> a_oSender) where SrcKey : class where SrcVal : class where DestT : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypeList = new List<DestT>();

		foreach(var stKeyVal in a_oSender) {
			var oConvertVal = stKeyVal.Value as DestT;

			// 타입 추가가 가능 할 경우
			if(oConvertVal != null) {
				oConvertTypeList.ExAddVal(oConvertVal);
			}
		}

		return oConvertTypeList;
	}
	
	//! 함수를 호출한다
	public static object ExCallFunc<T>(this object a_oSender, string a_oName, BindingFlags a_eBindingFlags, object[] a_oParams) {
		CAccess.Assert(a_oName.ExIsValid());
		var oMethodInfo = typeof(T).GetMethod(a_oName, a_eBindingFlags);

		return oMethodInfo.Invoke(a_oSender, a_oParams);
	}
	
	//! 런타임 함수를 호출한다
	public static object ExRuntimeCallFunc<T>(this object a_oSender, string a_oName, object[] a_oParams) {
		CAccess.Assert(a_oName.ExIsValid());
		var oMethodInfos = typeof(T).GetRuntimeMethods();
		
		foreach(var oMethodInfo in oMethodInfos) {
			// 메서드 이름이 동일 할 경우
			if(oMethodInfo.Name.ExIsEquals(a_oName)) {
				return oMethodInfo.Invoke(a_oSender, a_oParams);
			}
		}

		return null;
	}
	#endregion			// 제네릭 클래스 함수
}
