using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

//! 객체 이름 에디터 윈도우
public class CObjNameEditorWnd : CEditorWnd<CObjNameEditorWnd> {
	#region 변수
	private string m_oTargetObjName = string.Empty;
	private string m_oReplaceObjName = string.Empty;
	#endregion			// 변수

	#region 함수
	//! GUI 를 그린다
	public void OnGUI() {
		m_oTargetObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_OBJ_NEW_TARGET, m_oTargetObjName, GUILayout.Width(KCEditorDefine.B_WIDTH_OBJ_NEW_NAME_INPUT));
		m_oReplaceObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_OBJ_NEW_REPLACE, m_oReplaceObjName, GUILayout.Width(KCEditorDefine.B_WIDTH_OBJ_NEW_NAME_INPUT));

		// 버튼을 눌렀을 경우
		if(GUILayout.Button(KCEditorDefine.B_TEXT_OBJ_NEW_APPLY_BTN, GUILayout.Width(KCEditorDefine.B_WIDTH_OBJ_NEW_APPLY_BTN))) {
			// 객체 이름이 유효 할 경우
			if(m_oTargetObjName.ExIsValid() && m_oReplaceObjName.ExIsValid()) {
				CFunc.EnumerateScenes((a_stScene) => {
					this.ReplaceSceneObjsName(a_stScene, m_oTargetObjName, m_oReplaceObjName);
					EditorSceneManager.MarkSceneDirty(a_stScene);

					return true;
				});
			}
		}
	}

	//! 씬 객체 이름을 변경한다
	private void ReplaceSceneObjsName(Scene a_stScene, string a_oTargetName, string a_oReplaceName) {
		var oObjList = a_stScene.ExGetChildren();
		
		for(int i = 0; i < oObjList.Count; ++i) {
			oObjList[i].name = oObjList[i].name.ExGetReplaceStr(a_oTargetName, a_oReplaceName);
		}
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 객체 이름 에디터 윈도우를 출력한다
	[MenuItem("Tools/Utility/Editor Window/Show ObjNameEditorWnd")]
	public static void ShowObjNameEditorWnd() {
		CObjNameEditorWnd.ShowEditorWnd(KCEditorDefine.B_OBJ_N_OBJ_N_EDITOR_POPUP, KCEditorDefine.B_MIN_SIZE_EDITOR_WND);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
