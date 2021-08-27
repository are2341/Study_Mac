using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

//! 씬 에디터 윈도우
public class CSceneEditorWnd : CEditorWnd<CSceneEditorWnd> {
	#region 변수
	private string m_oTargetObjName = string.Empty;
	private string m_oReplaceObjName = string.Empty;

	private string m_oFontPath = string.Empty;
	#endregion			// 변수

	#region 함수
	//! GUI 를 그린다
	public void OnGUI() {
		EditorGUILayout.LabelField(KCEditorDefine.B_TEXT_OBJ_NAME_REPLACE, GUILayout.Width(KCEditorDefine.B_WIDTH_TEXT));

		m_oTargetObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_SEARCH, m_oTargetObjName, GUILayout.Width(KCEditorDefine.B_WIDTH_INPUT));
		m_oReplaceObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_REPLACE, m_oReplaceObjName, GUILayout.Width(KCEditorDefine.B_WIDTH_INPUT));

		// 적용 버튼을 눌렀을 경우
		if(GUILayout.Button(KCEditorDefine.B_TEXT_APPLY, GUILayout.Width(KCEditorDefine.B_WIDTH_BTN))) {
			// 객체 이름이 유효 할 경우
			if(m_oTargetObjName.ExIsValid() && m_oReplaceObjName.ExIsValid()) {
				CFunc.EnumerateScenes((a_stScene) => {
					this.ReplaceSceneObjsName(a_stScene, m_oTargetObjName, m_oReplaceObjName);
					EditorSceneManager.MarkSceneDirty(a_stScene);

					return true;
				});
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField(KCEditorDefine.B_TEXT_FONT_REPLACE, GUILayout.Width(KCEditorDefine.B_WIDTH_TEXT));

		m_oFontPath = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_PATH, m_oFontPath, GUILayout.Width(KCEditorDefine.B_WIDTH_INPUT));

		// 적용 버튼을 눌렀을 경우
		if(GUILayout.Button(KCEditorDefine.B_TEXT_APPLY, GUILayout.Width(KCEditorDefine.B_WIDTH_BTN))) {
			var oFont = Resources.Load<Font>(m_oFontPath);
			var oTextList = CEditorFunc.FindComponents<Text>();

			for(int i = 0; i < oTextList.Count; ++i) {
				oTextList[i].font = oFont;
				EditorSceneManager.MarkSceneDirty(oTextList[i].gameObject.scene);
			}
		}
	}

	//! 씬 객체 이름을 변경한다
	private void ReplaceSceneObjsName(Scene a_stScene, string a_oTargetName, string a_oReplaceName) {
		var oObjList = a_stScene.ExGetChildren();
		
		for(int i = 0; i < oObjList.Count; ++i) {
			oObjList[i].name = oObjList[i].name.ExGetReplaceStr(a_oTargetName, a_oReplaceName);
		}

		EditorSceneManager.MarkSceneDirty(a_stScene);
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 씬 에디터 윈도우를 출력한다
	[MenuItem("Tools/Utility/Editor Window/Show SceneEditorWnd")]
	public static void ShowSceneEditorWnd() {
		CSceneEditorWnd.ShowEditorWnd(KCEditorDefine.B_OBJ_N_OBJ_N_EDITOR_POPUP, KCEditorDefine.B_MIN_SIZE_EDITOR_WND);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
