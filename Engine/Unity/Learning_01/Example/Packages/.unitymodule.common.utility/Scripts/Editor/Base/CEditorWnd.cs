using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 윈도우
public abstract class CEditorWnd<T> : EditorWindow where T : CEditorWnd<T> {
	#region 클래스 변수
	private static T m_tInst = null;
	#endregion			// 클래스 변수

	#region 함수
	//! 제거 되었을 경우
	public virtual void OnDestroy() {
		CEditorWnd<T>.m_tInst = null;
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 에디터 윈도우를 출력한다
	public static void ShowEditorWnd(string a_oName, Vector2 a_stMinSize, bool a_bIsImmediate = true) {
		// 인스턴스가 없을 경우
		if(CEditorWnd<T>.m_tInst == null) {
			CEditorWnd<T>.m_tInst = CEditorFactory.CreateEditorWnd<T>(a_oName, a_stMinSize);
			CEditorWnd<T>.m_tInst.Show(a_bIsImmediate);
			CEditorWnd<T>.m_tInst.Focus();
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
