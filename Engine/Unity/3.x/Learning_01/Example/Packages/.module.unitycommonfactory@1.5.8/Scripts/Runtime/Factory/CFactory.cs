using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 기본 팩토리
public static partial class CFactory {
	#region 클래스 함수
	//! 버전을 생성한다
	public static STVer MakeDefVer(string a_oVer) {
		CAccess.Assert(a_oVer.ExIsValid());

		return new STVer() {
			m_oVer = a_oVer,
			m_oExtraInfoList = new Dictionary<string, string>()
		};
	}

	//! 지역화 경로를 생성한다
	public static string MakeLocalizePath(string a_oBasePath, string a_oLanguage) {
		CAccess.Assert(a_oBasePath.ExIsValid() && a_oLanguage.ExIsValid());

		var oFileName = Path.GetFileNameWithoutExtension(a_oBasePath);
		var oLocalizeFileName = string.Format(KCDefine.B_NAME_FMT_UNDER_SCORE_COMBINE, oFileName, a_oLanguage);

		return a_oBasePath.ExGetReplaceFileNamePath(oLocalizeFileName);
	}

	//! 지역화 경로를 생성한다
	public static string MakeLocalizePath(string a_oBasePath, string a_oDefLocalizePath, string a_oLanguage, string a_oCountryCode) {
		CAccess.Assert(a_oBasePath.ExIsValid());
		CAccess.Assert(a_oDefLocalizePath.ExIsValid() && a_oCountryCode.ExIsValid());

		string oFilePath = a_oLanguage.ExIsValidLanguage() ? CFactory.MakeLocalizePath(a_oBasePath, a_oLanguage) : CFactory.MakeLocalizePath(a_oBasePath, a_oCountryCode);
		return CAccess.IsExistsRes<TextAsset>(oFilePath, true) ? oFilePath : a_oDefLocalizePath;
	}

	//! 정수 값을 생성한다
	public static int[] MakeIntVals(int a_nMin, int a_nNumVals) {
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);
		var oVals = new int[a_nNumVals];

		for(int i = 0; i < oVals.Length; ++i) {
			oVals[i] = a_nMin + i;
		}

		return oVals;
	}

	//! 정수 랜덤 값을 생성한다
	public static int[] MakeIntRandVals(int a_nMin, int a_nMax, int a_nNumVals) {
		CAccess.Assert(a_nMin <= a_nMax);
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);

		return CFactory.MakeVals<int>(a_nNumVals, (a_nIdx) => Random.Range(a_nMin, a_nMax + KCDefine.B_VAL_1_INT));
	}
	
	//! 정수 랜덤 유일 값을 생성한다
	public static int[] MakeIntRandUniqueVals(int a_nMin, int a_nMax, int a_nNumVals) {
		CAccess.Assert(a_nMin <= a_nMax);
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT && a_nNumVals <= (a_nMax - a_nMin) + KCDefine.B_VAL_1_INT);

		var oVals = new int[a_nNumVals];
		var oRandVals = CFactory.MakeIntVals(a_nMin, (a_nMax - a_nMin) + KCDefine.B_VAL_1_INT);

		oRandVals.ExShuffle();
		System.Array.Copy(oRandVals, oVals, a_nNumVals);

		return oVals;
	}
	
	//! 실수 랜덤 값을 생성한다
	public static float[] MakeFloatRandVals(float a_fMin, float a_fMax, int a_nNumVals) {
		CAccess.Assert(a_fMin.ExIsLessEquals(a_fMax));
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);

		return CFactory.MakeVals<float>(a_nNumVals, (a_nIdx) => Random.Range(a_fMin, a_fMax));
	}

	//! 디렉토리를 생성한다
	public static DirectoryInfo CreateDir(string a_oDirPath) {
		CAccess.Assert(a_oDirPath.ExIsValid());

		// 디렉토리가 없을 경우
		if(!Directory.Exists(a_oDirPath)) {
			Directory.CreateDirectory(a_oDirPath);
		}

		return new DirectoryInfo(a_oDirPath);
	}

	//! 파일을 제거한다
	public static void RemoveFile(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());

		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
			File.Delete(a_oFilePath);
		}
	}

	//! 디렉토리를 제거한다
	public static void RemoveDir(string a_oDirPath, bool a_bIsRecursive = true) {
		CAccess.Assert(a_oDirPath.ExIsValid());

		// 디렉토리가 존재 할 경우
		if(Directory.Exists(a_oDirPath)) {
			Directory.Delete(a_oDirPath, a_bIsRecursive);
		}
	}

	//! 대기 객체를 생성한다
	public static IEnumerator CreateWaitForSecs(float a_fDeltaTime, bool a_bIsRealtime = false) {
		CAccess.Assert(a_fDeltaTime.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		
		// 리얼 타임 모드 일 경우
		if(a_bIsRealtime) {
			yield return new WaitForSecondsRealtime(a_fDeltaTime);
		} else {
			yield return new WaitForSeconds(a_fDeltaTime);
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 값을 생성한다
	public static T[] MakeVals<T>(int a_nNumVals, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null);
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);
		
		var oVals = new T[a_nNumVals];

		for(int i = 0; i < a_nNumVals; ++i) {
			oVals[i] = a_oCallback.Invoke(i);
		}

		return oVals;
	}

	//! 섞인 값을 생성한다
	public static T[] MakeShuffleVals<T>(int a_nNumVals, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null);
		CAccess.Assert(a_nNumVals > KCDefine.B_VAL_0_INT);

		var oVals = CFactory.MakeVals<T>(a_nNumVals, a_oCallback);

		for(int i = 0; i < oVals.Length; ++i) {
			int nIdx = Random.Range(KCDefine.B_VAL_0_INT, oVals.Length);
			oVals.ExSwap(i, nIdx);
		}

		return oVals;
	}

	//! 값을 교환한다
	private static void ExSwap<T>(this T[] a_oSender, int a_nIdxA, int a_nIdxB) {
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxA));
		CAccess.Assert(a_oSender.ExIsValidIdx(a_nIdxB));

		T tTemp = a_oSender[a_nIdxA];
		a_oSender[a_nIdxA] = a_oSender[a_nIdxB];
		a_oSender[a_nIdxB] = tTemp;
	}

	//! 값을 섞는다
	private static void ExShuffle<T>(this T[] a_oSender) {
		CAccess.Assert(a_oSender != null);

		for(int i = 0; i < a_oSender.Length; ++i) {
			int nIdx = Random.Range(KCDefine.B_VAL_0_INT, a_oSender.Length);
			a_oSender.ExSwap(i, nIdx);
		}
	}
	#endregion			// 제네릭 클래스 함수
}
