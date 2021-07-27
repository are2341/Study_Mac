using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 접근 확장
public static partial class CEditorExtension {
    #region 클래스 함수
	//! 직렬화 속성 값을 변경한다
	public static void ExSetPropertyVal(this SerializedObject a_oSender, string a_oName, System.Action<SerializedProperty> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			var oProperty = a_oSender.FindProperty(a_oName);
			a_oCallback?.Invoke(oProperty);

			a_oSender.ApplyModifiedProperties();
			a_oSender.Update();
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
