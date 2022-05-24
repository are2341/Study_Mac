using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

/** 자동 씬 임포터 */
[InitializeOnLoad]
public static class CAutoSceneImporter {
	#region 클래스 함수
	/** 클래스 생성자 */
	static CAutoSceneImporter() {
		// 플레이 모드가 아닐 경우
		if(!EditorApplication.isPlaying) {
			EditorApplication.projectChanged -= CAutoSceneImporter.ImportAllScenes;
			EditorApplication.projectChanged += CAutoSceneImporter.ImportAllScenes;
		}
	}

	/** 모든 씬을 추가한다 */
	public static void ImportAllScenes() {
		var oEditorBuildSettingsSceneList = new List<EditorBuildSettingsScene>();

		try {
			for(int i = 0; i < KCEditorDefine.B_SEARCH_P_SCENE_LIST.Count; ++i) {
				// 디렉토리가 존재 할 경우
				if(AssetDatabase.IsValidFolder(KCEditorDefine.B_SEARCH_P_SCENE_LIST[i])) {
					var oAssetGUIDs = AssetDatabase.FindAssets(KCEditorDefine.B_SCENE_N_PATTERN, new string[] { KCEditorDefine.B_SEARCH_P_SCENE_LIST[i] });

					for(int j = 0; j < oAssetGUIDs.Length; ++j) {
						oEditorBuildSettingsSceneList.ExAddVal(new EditorBuildSettingsScene(AssetDatabase.GUIDToAssetPath(oAssetGUIDs[j]), true));
					}
				}
			}
		} finally {
			EditorBuildSettings.scenes = oEditorBuildSettingsSceneList.ToArray();
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
