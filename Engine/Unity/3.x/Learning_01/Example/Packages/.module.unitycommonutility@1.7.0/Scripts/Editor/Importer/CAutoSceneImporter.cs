using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

//! 자동 씬 추가자
[InitializeOnLoad]
public static class CAutoSceneImporter {
	#region 클래스 함수
	//! 클래스 생성자
	static CAutoSceneImporter() {
		EditorApplication.projectChanged -= CAutoSceneImporter.ImportAllScenes;
		EditorApplication.projectChanged += CAutoSceneImporter.ImportAllScenes;
	}

	//! 모든 씬을 추가한다
	public static void ImportAllScenes() {
		var oAssetGUIDs = AssetDatabase.FindAssets(KCEditorDefine.B_SCENE_N_PATTERN, KCEditorDefine.B_SEARCH_P_SCENES);
		var oEditorBuildSettingsScenes = new EditorBuildSettingsScene[oAssetGUIDs.Length];

		for(int i = 0; i < oAssetGUIDs.Length; ++i) {
			string oFilePath = AssetDatabase.GUIDToAssetPath(oAssetGUIDs[i]);
			oEditorBuildSettingsScenes[i] = new EditorBuildSettingsScene(oFilePath, true);
		}

		EditorBuildSettings.scenes = oEditorBuildSettingsScenes;
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
