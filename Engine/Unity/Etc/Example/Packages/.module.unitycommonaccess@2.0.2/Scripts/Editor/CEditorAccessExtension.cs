﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif			// #if UNITY_IOS

/** 에디터 기본 접근 확장 클래스 */
public static partial class CEditorAccessExtension {
	#region 클래스 함수
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EBuildMode a_eSender) {
		return a_eSender > EBuildMode.NONE && a_eSender < EBuildMode.MAX_VAL;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this BuildTarget a_eSender) {
		bool bIsMobile = a_eSender == BuildTarget.iOS || a_eSender == BuildTarget.Android;
		return bIsMobile || (a_eSender == BuildTarget.StandaloneOSX || a_eSender == BuildTarget.StandaloneWindows || a_eSender == BuildTarget.StandaloneWindows64);
	}

	/** 완료 여부를 검사한다 */
	public static bool ExIsComplete(this Request a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.IsCompleted && a_oSender.Status == StatusCode.Success;
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_IOS
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this PlistDocument a_oSender) {
		return a_oSender != null && a_oSender.root != null;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContains(this PlistElementArray a_oSender, string a_oStr) {
		CAccess.Assert(a_oSender != null && a_oStr.ExIsValid());
		return a_oSender.values.FindIndex((a_oElement) => a_oElement.AsString().Equals(a_oStr)) >= KCDefine.B_VAL_0_INT;
	}

	/** 포함 여부를 검사한다 */
	public static bool ExIsContainsAdsNetworkID(this PlistElementArray a_oSender, string a_oNetworkID) {
		CAccess.Assert(a_oSender != null && a_oNetworkID.ExIsValid());

		for(int i = 0; i < a_oSender.values.Count; ++i) {
			var oValDict = a_oSender.values[i].AsDict();

			// 광고 네트워크 식별자가 존재 할 경우
			if(oValDict.values.TryGetValue(KCEditorDefine.B_KEY_IOS_ADS_NETWORK_ID, out PlistElement oElement) && oElement.AsString().Equals(a_oNetworkID)) {
				return true;
			}
		}

		return false;
	}

	/** 배열을 반환한다 */
	public static PlistElementArray ExGetArray(this PlistDocument a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender.ExIsValid());

		try {
			return a_oSender.root[a_oKey].AsArray();
		} catch {
			return a_oSender.root.CreateArray(a_oKey);
		}
	}

	/** 딕셔너리를 반환한다 */
	public static PlistElementDict ExGetDict(this PlistDocument a_oSender, string a_oKey) {
		CAccess.Assert(a_oSender.ExIsValid());

		try {
			return a_oSender.root[a_oKey].AsDict();
		} catch {
			return a_oSender.root.CreateDict(a_oKey);
		}
	}

	/** 문자열을 추가한다 */
	public static void ExAddStr(this PlistElementArray a_oSender, string a_oStr) {
		CAccess.Assert(a_oSender != null && a_oStr.ExIsValid());

		// 문자열 추가가 가능 할 경우
		if(a_oSender != null && !a_oSender.ExIsContains(a_oStr)) {
			a_oSender.AddString(a_oStr);
		}
	}
#endif			// #if UNITY_IOS
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_EDITOR
