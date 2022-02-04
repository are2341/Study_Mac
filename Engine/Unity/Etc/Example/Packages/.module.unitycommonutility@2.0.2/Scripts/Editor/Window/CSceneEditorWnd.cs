using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

/** 씬 에디터 윈도우 */
public class CSceneEditorWnd : CEditorWnd<CSceneEditorWnd> {
	#region 변수
	private string m_oTargetObjName = string.Empty;
	private string m_oReplaceObjName = string.Empty;

	private string m_oTargetFontName = string.Empty;
	private string m_oReplaceFontPath = string.Empty;
	#endregion			// 변수

	#region 함수
	/** GUI 를 그린다 */
	public void OnGUI() {
		this.DrawFontEditorGUI();
		this.DrawObjNameEditorGUI();
	}

	/** 폰트 에디터 GUI 를 그린다 */
	private void DrawFontEditorGUI() {
		EditorGUILayout.LabelField(KCEditorDefine.B_TEXT_FONT_REPLACE, GUILayout.Width(this.WndWidth));

		m_oTargetFontName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_SEARCH, m_oTargetFontName, GUILayout.Width(this.WndWidth));
		m_oReplaceFontPath = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_REPLACE, m_oReplaceFontPath, GUILayout.Width(this.WndWidth));

		// 적용 버튼을 눌렀을 경우
		if(GUILayout.Button(KCEditorDefine.B_TEXT_APPLY, GUILayout.Width(this.WndWidth)) && m_oReplaceFontPath.ExIsValid()) {
			var oFont = Resources.Load<Font>(m_oReplaceFontPath);
			var oFontAsset = Resources.Load<TMP_FontAsset>(m_oReplaceFontPath);

			var oTextList = CEditorFunc.FindComponents<Text>();
			var oTMPTextList = CEditorFunc.FindComponents<TMP_Text>();

			for(int i = 0; i < oTextList.Count; ++i) {
				oTextList[i].font = (oTextList[i].font == null || (m_oTargetFontName.Length <= KCDefine.B_VAL_0_INT || m_oTargetFontName.Equals(oTextList[i].font.name))) ? oFont : oTextList[i].font;
				EditorSceneManager.MarkSceneDirty(oTextList[i].gameObject.scene);
			}

			for(int i = 0; i < oTMPTextList.Count; ++i) {
				oTMPTextList[i].font = (oTMPTextList[i].font == null || (m_oTargetFontName.Length <= KCDefine.B_VAL_0_INT || m_oTargetFontName.Equals(oTextList[i].font.name))) ? oFontAsset : oTMPTextList[i].font;
				EditorSceneManager.MarkSceneDirty(oTMPTextList[i].gameObject.scene);
			}
		}
	}

	/** 객체 이름 에디터 GUI 를 그린다 */
	private void DrawObjNameEditorGUI() {
		EditorGUILayout.Space();
		EditorGUILayout.LabelField(KCEditorDefine.B_TEXT_OBJ_NAME_REPLACE, GUILayout.Width(this.WndWidth));
		
		m_oTargetObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_SEARCH, m_oTargetObjName, GUILayout.Width(this.WndWidth));
		m_oReplaceObjName = EditorGUILayout.TextField(KCEditorDefine.B_TEXT_REPLACE, m_oReplaceObjName, GUILayout.Width(this.WndWidth));

		// 적용 버튼을 눌렀을 경우
		if(GUILayout.Button(KCEditorDefine.B_TEXT_APPLY, GUILayout.Width(this.WndWidth)) && (m_oTargetObjName.ExIsValid() && m_oReplaceObjName.ExIsValid())) {
			CFunc.EnumerateScenes((a_stScene) => {
				this.ReplaceSceneObjsName(a_stScene, m_oTargetObjName, m_oReplaceObjName);
				EditorSceneManager.MarkSceneDirty(a_stScene);

				return true;
			});
		}
	}
	
	/** 씬 객체 이름을 변경한다 */
	private void ReplaceSceneObjsName(Scene a_stScene, string a_oTargetName, string a_oReplaceName) {
		var oObjList = a_stScene.ExGetChildren();
		
		for(int i = 0; i < oObjList.Count; ++i) {
			oObjList[i].name = oObjList[i].name.ExGetReplaceStr(a_oTargetName, a_oReplaceName);
		}
	}
	#endregion			// 함수

	#region 클래스 함수
	/** 씬 에디터 윈도우를 출력한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_EDITOR_WND_BASE + "Show SceneEditorWnd", false, KCEditorDefine.B_SORTING_O_EDITOR_WND_MENU + 1)]
	public static void ShowSceneEditorWnd() {
		CSceneEditorWnd.ShowEditorWnd(KCEditorDefine.B_OBJ_N_SCENE_EDITOR_POPUP, KCEditorDefine.B_MIN_SIZE_EDITOR_WND);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
