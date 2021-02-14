using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 기본 팩토리
public static partial class CFactory {
	#region 클래스 함수
	//! 버전을 생성한다
	public static STVersion MakeDefVersion(string a_oVersion) {
		CAccess.Assert(a_oVersion.ExIsValid());

		return new STVersion() {
			m_oVersion = a_oVersion,
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

		string oFilePath = string.Empty;

		// 언어가 유효 할 경우
		if(a_oLanguage.ExIsValidLanguage()) {
			oFilePath = CFactory.MakeLocalizePath(a_oBasePath, a_oLanguage);
		} else {
			oFilePath = CFactory.MakeLocalizePath(a_oBasePath, a_oCountryCode);
		}

		return CAccess.IsExistsRes<TextAsset>(oFilePath, true) ? oFilePath : a_oDefLocalizePath;
	}

	//! 정수 랜덤 값을 생성한다
	public static int[] MakeIntRandomValues(int a_nMin, int a_nMax, int a_nNumValues) {
		CAccess.Assert(a_nMin <= a_nMax);
		CAccess.Assert(a_nNumValues > KCDefine.B_VALUE_INT_0);

		return CFactory.MakeValues<int>(a_nNumValues, (a_nIdx) => Random.Range(a_nMin, a_nMax + KCDefine.B_VALUE_INT_1));
	}
	
	//! 실수 랜덤 값을 생성한다
	public static float[] MakeFloatRandomValues(float a_fMin, float a_fMax, int a_nNumValues) {
		CAccess.Assert(a_fMin.ExIsLessEquals(a_fMax));
		CAccess.Assert(a_nNumValues > KCDefine.B_VALUE_INT_0);

		return CFactory.MakeValues<float>(a_nNumValues, (a_nIdx) => Random.Range(a_fMin, a_fMax));
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
	public static IEnumerator CreateWaitForSeconds(float a_fDeltaTime, bool a_bIsRealtime = false) {
		CAccess.Assert(a_fDeltaTime.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));
		
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
	public static T[] MakeValues<T>(int a_nNumValues, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null);
		CAccess.Assert(a_nNumValues > KCDefine.B_VALUE_INT_0);
		
		var oValues = new T[a_nNumValues];

		for(int i = 0; i < a_nNumValues; ++i) {
			oValues[i] = a_oCallback.Invoke(i);
		}

		return oValues;
	}

	//! 섞인 값을 생성한다
	public static T[] MakeShuffleValues<T>(int a_nNumValues, System.Func<int, T> a_oCallback) {
		CAccess.Assert(a_oCallback != null);
		CAccess.Assert(a_nNumValues > KCDefine.B_VALUE_INT_0);

		var oValues = CFactory.MakeValues<T>(a_nNumValues, a_oCallback);

		for(int i = 0; i < oValues.Length; ++i) {
			int nIdx = Random.Range(KCDefine.B_VALUE_INT_0, oValues.Length);

			T tTemp = oValues[i];
			oValues[i] = oValues[nIdx];
			oValues[nIdx] = tTemp;
		}

		return oValues;
	}
	#endregion			// 제네릭 클래스 함수
}
