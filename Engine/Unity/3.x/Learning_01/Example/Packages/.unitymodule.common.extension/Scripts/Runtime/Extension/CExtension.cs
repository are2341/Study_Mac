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
		if(dblDeltaTime.ExIsEquals(KCDefine.B_VALUE_DBL_0)) {
			return KCDefine.B_COMPARE_R_EQUALS;
		}

		return dblDeltaTime.ExIsLess(KCDefine.B_VALUE_DBL_0) ? KCDefine.B_COMPARE_R_LESS : KCDefine.B_COMPARE_R_GREATE;
	}

	//! 문자열 => 합계로 변환한다
	public static int ExToSumValue(this string a_oSender) {
		CAccess.Assert(a_oSender != null);
		int nSumValue = KCDefine.B_VALUE_INT_0;

		for(int i = 0; i < a_oSender.Length; ++i) {
			nSumValue += a_oSender[i];
		}

		return nSumValue;
	}
	
	//! 픽셀 => DPI 픽셀로 변환한다
	public static float ExPixelsToDPIPixels(this int a_nSender) {
		return a_nSender * (Screen.dpi / KCDefine.B_DEF_DPI);
	}

	//! 픽셀 => DPI 픽셀로 변환한다
	public static float ExPixelsToDPIPixels(this float a_fSender) {
		return a_fSender * (Screen.dpi / KCDefine.B_DEF_DPI);
	}

	//! 바이트 => 메가 바이트로 변환한다
	public static double ExByteToMegaByte(this uint a_oSender) {
		return a_oSender / KCDefine.B_UNIT_BYTE_TO_MEGA_BYTE;
	}

	//! 바이트 => 메가 바이트로 변환한다
	public static double ExByteToMegaByte(this long a_oSender) {
		return a_oSender / KCDefine.B_UNIT_BYTE_TO_MEGA_BYTE;
	}

	//! 문자열 => 시간으로 변환한다
	public static System.DateTime ExToTime(this string a_oSender, string a_oFormat) {
		CAccess.Assert(a_oSender.ExIsValid() && a_oFormat.ExIsValid());
		return System.DateTime.ParseExact(a_oSender, a_oFormat, CultureInfo.InvariantCulture);
	}

	//! 시간 => 문자열로 변환한다
	public static string ExToString(this System.DateTime a_stSender, string a_oFormat) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ToString(a_oFormat, CultureInfo.InvariantCulture);
	}

	//! 시간 => 긴 문자열로 변환한다
	public static string ExToLongString(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToString(KCDefine.B_DATE_T_FMT_YYYY_MM_DD_HH_MM_SS);
	}

	//! 시간 => 짧은 문자열로 변환한다
	public static string ExToShortString(this System.DateTime a_stSender) {
		CAccess.Assert(a_stSender.ExIsValid());
		return a_stSender.ExToString(KCDefine.B_DATE_T_FMT_YYYY_MM_DD);
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
	public static int ExFindValue(this SimpleJSON.JSONArray a_oSender, System.Func<SimpleJSON.JSONNode, bool> a_oCompare) {
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
	public static void ExAddValue<T>(this List<T> a_oSender, T a_tValue) {
		CAccess.Assert(a_oSender != null);
		int nIdx = a_oSender.IndexOf(a_tValue);

		// 값 추가가 가능 할 경우
		if(nIdx <= KCDefine.B_IDX_INVALID) {
			a_oSender.Add(a_tValue);
		}
	}

	//! 값을 추가한다
	public static void ExAddValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 값 추가가 가능 할 경우		
		if(!a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Add(a_tKey, a_tValue);
		}
	}

	//! 값을 추가한다
	public static void ExAddValues<T>(this List<T> a_oSender, List<T> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		for(int i = 0; i < a_oValueList.Count; ++i) {
			a_oSender.ExAddValue(a_oValueList[i]);
		}
	}

	//! 값을 추가한다
	public static void ExAddValues<Key, Value>(this Dictionary<Key, Value> a_oSender, Dictionary<Key, Value> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		foreach(var stKeyValue in a_oValueList) {
			a_oSender.ExAddValue(stKeyValue.Key, stKeyValue.Value);
		}
	}

	//! 값을 대체한다
	public static void ExReplaceValue<T>(this List<T> a_oSender, T a_tValue, T a_tReplaceValue) {
		CAccess.Assert(a_oSender != null);
		int nIdx = a_oSender.IndexOf(a_tValue);

		// 값 추가가 가능 할 경우
		if(nIdx <= KCDefine.B_IDX_INVALID) {
			a_oSender.Add(a_tReplaceValue);
		} else {
			a_oSender[nIdx] = a_tReplaceValue;
		}
	}

	//! 값을 대체한다
	public static void ExReplaceValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey, Value a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 값 추가가 가능 할 경우
		if(!a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Add(a_tKey, a_tValue);
		} else {
			a_oSender[a_tKey] = a_tValue;
		}
	}

	//! 값을 대체한다
	public static void ExReplaceValues<T>(this List<T> a_oSender, List<KeyValuePair<T, T>> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		for(int i = 0; i < a_oValueList.Count; ++i) {
			a_oSender.ExReplaceValue(a_oValueList[i].Key, a_oValueList[i].Value);
		}
	}

	//! 값을 대체한다
	public static void ExReplaceValues<Key, Value>(this Dictionary<Key, Value> a_oSender, Dictionary<Key, Value> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		foreach(var stKeyValue in a_oValueList) {
			a_oSender.ExReplaceValue(stKeyValue.Key, stKeyValue.Value);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValueAt<T>(this List<T> a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender.ExIsValidIdx(a_nIdx)) {
			a_oSender.RemoveAt(a_nIdx);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValue<T>(this List<T> a_oSender, T a_tValue) {
		CAccess.Assert(a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender.Contains(a_tValue)) {
			int nIdx = a_oSender.IndexOf(a_tValue);
			a_oSender.ExRemoveValueAt(nIdx);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValue<T>(this List<T> a_oSender, System.Func<T, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		int nIdx = a_oSender.ExFindValue(a_oCompare);

		// 값 제거가 가능 할 경우
		if(a_oSender.ExIsValidIdx(nIdx)) {
			a_oSender.ExRemoveValueAt(nIdx);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValue<Key, Value>(this Dictionary<Key, Value> a_oSender, Key a_tKey) {
		CAccess.Assert(a_oSender != null);

		// 값 제거가 가능 할 경우
		if(a_oSender.ContainsKey(a_tKey)) {
			a_oSender.Remove(a_tKey);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValue<Key, Value>(this Dictionary<Key, Value> a_oSender, System.Func<Value, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		var stResult = a_oSender.ExFindValue(a_oCompare);

		// 값 제거가 가능 할 경우
		if(stResult.Value) {
			a_oSender.ExRemoveValue(stResult.Key);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValues<T>(this List<T> a_oSender, List<T> a_oValueList) {
		CAccess.Assert(a_oSender != null && a_oValueList != null);

		for(int i = 0; i < a_oValueList.Count; ++i) {
			a_oSender.ExRemoveValue(a_oValueList[i]);
		}
	}

	//! 값을 제거한다
	public static void ExRemoveValues<Key, Value>(this Dictionary<Key, Value> a_oSender, List<Key> a_oKeyList) {
		CAccess.Assert(a_oSender != null && a_oKeyList != null);

		for(int i = 0; i < a_oKeyList.Count; ++i) {
			a_oSender.ExRemoveValue(a_oKeyList[i]);
		}
	}
	
	//! 값을 탐색한다
	public static int ExFindValue<T>(this T[] a_oSender, System.Func<T, bool> a_oCompare) {
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
	public static int ExFindValue<T>(this List<T> a_oSender, System.Func<T, bool> a_oCompare) {
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
	public static KeyValuePair<Key, bool> ExFindValue<Key, Value>(this Dictionary<Key, Value> a_oSender, System.Func<Value, bool> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);

		foreach(var stKeyValue in a_oSender) {
			// 값을 탐색했을 경우
			if(a_oCompare(stKeyValue.Value)) {
				return new KeyValuePair<Key, bool>(stKeyValue.Key, true);
			}
		}

		return new KeyValuePair<Key, bool>(default(Key), false);
	}
	
	//! 값을 교환한다
	public static void ExSwap<T>(this T[] a_oSender, int a_nIdxA, int a_nIdxB) {
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxA));
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxB));

		T tTemp = a_oSender[a_nIdxA];
		a_oSender[a_nIdxA] = a_oSender[a_nIdxB];
		a_oSender[a_nIdxB] = tTemp;
	}

	//! 값을 교환한다
	public static void ExSwap<T>(this T[,] a_oSender, Vector2Int a_stIdxA, Vector2Int a_stIdxB) {
		CAccess.Assert(a_oSender.ExIsValidIdx(a_stIdxA));
		CAccess.Assert(a_oSender.ExIsValidIdx(a_stIdxB));

		T tTemp = a_oSender[a_stIdxA.y, a_stIdxA.x];
		a_oSender[a_stIdxA.y, a_stIdxA.x] = a_oSender[a_stIdxB.y, a_stIdxB.x];
		a_oSender[a_stIdxB.y, a_stIdxB.x] = tTemp;
	}

	//! 값을 교환한다
	public static void ExSwap<T>(this List<T> a_oSender, int a_nIdxA, int a_nIdxB) {
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxA));
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxB));

		T tTemp = a_oSender[a_nIdxA];
		a_oSender[a_nIdxA] = a_oSender[a_nIdxB];
		a_oSender[a_nIdxB] = tTemp;
	}

	//! 값을 섞는다
	public static void ExShuffle<T>(this T[] a_oSender) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			int nIdx = Random.Range(KCDefine.B_VALUE_INT_0, a_oSender.Length);
			a_oSender.ExSwap(i, nIdx);
		}
	}

	//! 값을 섞는다
	public static void ExShuffle<T>(this List<T> a_oSender) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			int nIdx = Random.Range(KCDefine.B_VALUE_INT_0, a_oSender.Count);
			a_oSender.ExSwap(i, nIdx);
		}
	}

	//! 정렬을 수행한다
	public static void ExSort<T>(this T[] a_oSender, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		System.Array.Sort(a_oSender, a_oCompare);
	}

	//! 정렬을 수행한다
	public static void ExSort<T>(this List<T> a_oSender, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		a_oSender.Sort(a_oCompare);
	}

	//! 안전 정렬을 수행한다
	public static void ExStableSort<T>(this T[] a_oSender, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		var oValues = new T[a_oSender.Length];

		CExtension.DoStableSort(a_oSender, oValues, KCDefine.B_VALUE_INT_0, a_oSender.Length - KCDefine.B_VALUE_INT_1, a_oCompare);
	}

	//! 안전 정렬을 수행한다
	public static void ExStableSort<T>(this List<T> a_oSender, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oSender != null && a_oCompare != null);
		var oValues = new T[a_oSender.Count];

		CExtension.DoStableSort(a_oSender, oValues, KCDefine.B_VALUE_INT_0, a_oSender.Count - KCDefine.B_VALUE_INT_1, a_oCompare);
	}

	//! 리스트를 복사한다
	public static void ExCopyTo<T>(this List<T> a_oSender, List<T> a_oDestValueList, System.Func<T, T> a_oCallback) {
		CAccess.Assert(a_oSender != null && a_oDestValueList != null && a_oCallback != null);
		a_oDestValueList.Clear();

		for(int i = 0; i < a_oSender.Count; ++i) {
			var tValue = a_oCallback(a_oSender[i]);
			a_oDestValueList.Add(tValue);
		}
	}

	//! 딕셔너리를 복사한다
	public static void ExCopyTo<Key, Value>(this Dictionary<Key, Value> a_oSender, Dictionary<Key, Value> a_oDestValueList, System.Func<Value, Value> a_oCallback) {
		CAccess.Assert(a_oSender != null && a_oDestValueList != null && a_oCallback != null);
		a_oDestValueList.Clear();

		foreach(var stKeyValue in a_oSender) {
			var tValue = a_oCallback(stKeyValue.Value);
			a_oDestValueList.Add(stKeyValue.Key, tValue);
		}
	}

	//! 1 차원 배열 => 2 차원 배열로 복사한다
	public static void ExCopyToMultiArray<T>(this T[] a_oSender, T[,] a_oDestValues) {
		CAccess.Assert(a_oSender != null && a_oDestValues != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			int nRow = i / a_oDestValues.GetLength(KCDefine.B_VALUE_INT_1);
			int nCol = i % a_oDestValues.GetLength(KCDefine.B_VALUE_INT_1);

			a_oDestValues[nRow, nCol] = a_oSender[i];
		}
	}

	//! 2 차원 배열 => 1 차원 배열로 복사한다
	public static void ExCopyToSingleArray<T>(this T[,] a_oSender, T[] a_oDestValues) {
		CAccess.Assert(a_oSender != null && a_oDestValues != null);

		for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VALUE_INT_0); ++i) {
			for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VALUE_INT_1); ++j) {
				int nIdx = (i * a_oSender.GetLength(KCDefine.B_VALUE_INT_1)) + j;
				a_oDestValues[nIdx] = a_oSender[i, j];
			}
		}
	}

	//! 1 차원 배열 => 2 차원 배열로 변환한다
	public static T[,] ExToMultiArray<T>(this T[] a_oSender, int a_nNumRows, int a_nNumCols) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_nNumRows > KCDefine.B_VALUE_INT_0 && a_nNumCols > KCDefine.B_VALUE_INT_0);

		var oConvertValues = new T[a_nNumRows, a_nNumCols];
		a_oSender.ExCopyToMultiArray(oConvertValues);

		return oConvertValues;
	}

	//! 2 차원 배열 => 1 차원 배열로 변환한다
	public static T[] ExToSingleArray<T>(this T[,] a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		var oConvertValues = new T[a_oSender.Length];
		a_oSender.ExCopyToSingleArray(oConvertValues);

		return oConvertValues;
	}

	//! 문자열 => 열거형 값으로 변환한다
	public static T ExToEnumValue<T>(this string a_oSender, bool a_bIsIgnoreCase = false) where T : struct {
		return (T)System.Enum.Parse(typeof(T), a_oSender, a_bIsIgnoreCase);
	}

	//! 문자열 => 열거형 값으로 변환한다
	public static bool ExToTryEnumValue<T>(this string a_oSender, out T a_tEnumValue, bool a_bIsIgnoreCase = false) where T : struct {
		return System.Enum.TryParse<T>(a_oSender, a_bIsIgnoreCase, out a_tEnumValue);
	}

	//! 배열 => 문자열로 변환한다
	public static string ExToString<T>(this T[] a_oSender, string a_oSeparateToken) {
		CAccess.Assert(a_oSender != null && a_oSeparateToken.ExIsValid());
		var oStringBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Length; ++i) {
			oStringBuilder.Append(a_oSender[i]);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Length - KCDefine.B_VALUE_INT_1) {
				oStringBuilder.Append(a_oSeparateToken);
			}
		}

		return oStringBuilder.ToString();
	}

	//! 리스트 => 문자열로 변환한다
	public static string ExToString<T>(this List<T> a_oSender, string a_oSeparateToken) {
		CAccess.Assert(a_oSender != null && a_oSeparateToken.ExIsValid());
		var oStringBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oStringBuilder.Append(a_oSender[i]);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Count - KCDefine.B_VALUE_INT_1) {
				oStringBuilder.Append(a_oSeparateToken);
			}
		}

		return oStringBuilder.ToString();
	}
	
	//! 사전 => 문자열로 변환한다
	public static string ExToString<Key, Value>(this Dictionary<Key, Value> a_oSender, string a_oSeparateToken) {
		CAccess.Assert(a_oSender != null && a_oSeparateToken.ExIsValid());

		int i = KCDefine.B_VALUE_INT_0;
		var oStringBuilder = new System.Text.StringBuilder();

		foreach(var stKeyValue in a_oSender) {
			oStringBuilder.AppendFormat(KCDefine.B_DICTIONARY_FMT_STRING, stKeyValue.Key, stKeyValue.Value);

			// 토큰 추가가 가능 할 경우
			if(i < a_oSender.Count - KCDefine.B_VALUE_INT_1) {
				oStringBuilder.Append(a_oSeparateToken);
			}

			i += 1;
		}

		return oStringBuilder.ToString();
	}

	//! 객체 => 특정 타입으로 변환한다
	public static List<T> ExToTypes<T>(this object[] a_oSender) where T : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypes = new List<T>();

		for(int i = 0; i < a_oSender.Length; ++i) {
			var oConvertType = a_oSender[i] as T;

			// 타입 추가가 가능 할 경우
			if(oConvertType != null) {
				oConvertTypes.ExAddValue(oConvertType);
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
				oConvertTypeList.ExAddValue(oConvertType);
			}
		}

		return oConvertTypeList;
	}

	//! 객체 => 특정 타입으로 변환한다
	public static Dictionary<DestKey, DestValue> ExToTypes<SrcKey, SrcValue, DestKey, DestValue>(this Dictionary<SrcKey, SrcValue> a_oSender) where SrcKey : class where SrcValue : class where DestKey : class where DestValue : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypeList = new Dictionary<DestKey, DestValue>();

		foreach(var stKeyValue in a_oSender) {
			var oConvertKey = stKeyValue.Key as DestKey;
			var oConvertValue = stKeyValue.Value as DestValue;

			// 타입 추가가 가능 할 경우
			if(oConvertKey != null && oConvertValue != null) {
				oConvertTypeList.ExAddValue(oConvertKey, oConvertValue);
			}
		}

		return oConvertTypeList;
	}

	//! 객체 => 특정 리스트 타입으로 변환한다
	public static List<DestT> ExToListTypes<SrcKey, SrcValue, DestT>(this Dictionary<SrcKey, SrcValue> a_oSender) where SrcKey : class where SrcValue : class where DestT : class {
		CAccess.Assert(a_oSender != null);
		var oConvertTypeList = new List<DestT>();

		foreach(var stKeyValue in a_oSender) {
			var oConvertValue = stKeyValue.Value as DestT;

			// 타입 추가가 가능 할 경우
			if(oConvertValue != null) {
				oConvertTypeList.ExAddValue(oConvertValue);
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

	//! 안전 정렬을 수행한다
	private static void DoStableSort<T>(T[] a_oValues, T[] a_oTempValues, int a_nLeft, int a_nRight, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oValues != null);
		CAccess.Assert(a_oTempValues != null && a_oCompare != null);

		// 정렬이 가능 할 경우
		if(a_nLeft < a_nRight) {
			int nCount = KCDefine.B_VALUE_INT_0;
			int nMiddle = (a_nLeft + a_nRight) / 2;

			int nLeftIndex = a_nLeft;
			int nRightIndex = nMiddle + KCDefine.B_VALUE_INT_1;

			// 정렬 범위를 분활한다 {
			CExtension.DoStableSort<T>(a_oValues, 
				a_oTempValues, a_nLeft, nMiddle, a_oCompare);

			CExtension.DoStableSort<T>(a_oValues, 
				a_oTempValues, nMiddle + KCDefine.B_VALUE_INT_1, a_nRight, a_oCompare);
			// 정렬 범위를 분활한다 }

			// 정렬을 수행한다 {
			while(nLeftIndex <= nMiddle && nRightIndex <= a_nRight) {
				while(nLeftIndex <= nMiddle && a_oCompare.Invoke(a_oValues[nLeftIndex], 
					a_oValues[nRightIndex]) <= KCDefine.B_COMPARE_R_EQUALS) 
				{
					a_oTempValues[nCount++] = a_oValues[nLeftIndex++];
				}

				while(nRightIndex <= a_nRight && !(a_oCompare.Invoke(a_oValues[nLeftIndex], 
					a_oValues[nRightIndex]) <= KCDefine.B_COMPARE_R_EQUALS)) 
				{
					a_oTempValues[nCount++] = a_oValues[nRightIndex++];
				}
			}
			
			while(nLeftIndex <= nMiddle) {
				a_oTempValues[nCount++] = a_oValues[nLeftIndex++];
			}

			while(nRightIndex <= a_nRight) {
				a_oTempValues[nCount++] = a_oValues[nRightIndex++];
			}

			for(int i = 0; i < nCount; ++i) {
				a_oValues[a_nLeft + i] = a_oTempValues[i];
			}
			// 정렬을 수행한다 }
		}
	}

	//! 안정 정렬을 수행한다
	private static void DoStableSort<T>(List<T> a_oValueList, T[] a_oTempValues, int a_nLeft, int a_nRight, System.Comparison<T> a_oCompare) {
		CAccess.Assert(a_oValueList != null);
		CAccess.Assert(a_oTempValues != null && a_oCompare != null);

		// 정렬이 가능 할 경우
		if(a_nLeft < a_nRight) {
			int nCount = KCDefine.B_VALUE_INT_0;
			int nMiddle = (a_nLeft + a_nRight) / 2;

			int nLeftIndex = a_nLeft;
			int nRightIndex = nMiddle + KCDefine.B_VALUE_INT_1;

			// 정렬 범위를 분활한다 {
			CExtension.DoStableSort<T>(a_oValueList, 
				a_oTempValues, a_nLeft, nMiddle, a_oCompare);

			CExtension.DoStableSort<T>(a_oValueList, 
				a_oTempValues, nMiddle + KCDefine.B_VALUE_INT_1, a_nRight, a_oCompare);
			// 정렬 범위를 분활한다 }

			// 정렬을 수행한다 {
			while(nLeftIndex <= nMiddle && nRightIndex <= a_nRight) {
				while(nLeftIndex <= nMiddle && a_oCompare.Invoke(a_oValueList[nLeftIndex], 
					a_oValueList[nRightIndex]) <= KCDefine.B_COMPARE_R_EQUALS) 
				{
					a_oTempValues[nCount++] = a_oValueList[nLeftIndex++];
				}

				while(nRightIndex <= a_nRight && !(a_oCompare.Invoke(a_oValueList[nLeftIndex], 
					a_oValueList[nRightIndex]) <= KCDefine.B_COMPARE_R_EQUALS)) 
				{
					a_oTempValues[nCount++] = a_oValueList[nRightIndex++];
				}
			}

			while(nLeftIndex <= nMiddle) {
				a_oTempValues[nCount++] = a_oValueList[nLeftIndex++];
			}

			while(nRightIndex <= a_nRight) {
				a_oTempValues[nCount++] = a_oValueList[nRightIndex++];
			}

			for(int i = 0; i < nCount; ++i) {
				a_oValueList[a_nLeft + i] = a_oTempValues[i];
			}
			// 정렬을 수행한다 }
		}
	}
	#endregion			// 제네릭 클래스 함수
}
