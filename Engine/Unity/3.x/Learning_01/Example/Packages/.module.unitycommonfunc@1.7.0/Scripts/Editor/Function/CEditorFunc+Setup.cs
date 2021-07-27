﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 설정 함수
public static partial class CEditorFunc {
	#region 클래스 함수
	//! 전처리기 심볼을 설정한다
	public static void SetupDefineSymbols(Dictionary<BuildTargetGroup, List<string>> a_oDefineSymbolDictContainer, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oDefineSymbolDictContainer.ExIsValid());

		// 전처리기 심볼이 존재 할 경우
		if(a_oDefineSymbolDictContainer.ExIsValid()) {
			foreach(var stKeyVal in a_oDefineSymbolDictContainer) {
				var oDefineSymbolList = new List<string>();

				for(int i = 0; i < stKeyVal.Value.Count; ++i) {
					oDefineSymbolList.ExAddVal(stKeyVal.Value[i]);
				}

				PlayerSettings.SetScriptingDefineSymbolsForGroup(stKeyVal.Key, oDefineSymbolList.ExToStr(KCEditorDefine.B_TOKEN_DEFINE_SYMBOL));
			}
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
