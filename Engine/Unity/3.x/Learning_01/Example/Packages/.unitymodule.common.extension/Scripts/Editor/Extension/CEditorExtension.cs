using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 접근 확장
public static partial class CEditorExtension {
    #region 클래스 함수
	//! 직렬화 속성 값을 변경한다
	public static void ExSetPropertyValue(this SerializedObject a_oSender, string a_oName, System.Action<SerializedProperty> a_oCallback) {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		
		var oProperty = a_oSender.FindProperty(a_oName);
		a_oCallback?.Invoke(oProperty);

		a_oSender.ApplyModifiedProperties();
		a_oSender.Update();
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
