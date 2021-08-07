using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 팩토리
public static class CEditorFactory {
	#region 클래스 함수
	//! 프리팹 인스턴스를 생성한다
	public static GameObject CreatePrefabInstance(string a_oName, string a_oObjPath, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CEditorFactory.CreatePrefabInstance(a_oName, oObj, a_oParent, a_bIsStayWorldState);
	}

	//! 프리팹 인스턴스를 생성한다
	public static GameObject CreatePrefabInstance(string a_oName, GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oObj = PrefabUtility.InstantiatePrefab(a_oOrigin) as GameObject;
		oObj.name = a_oName;
		oObj.transform.localScale = a_oOrigin.transform.localScale;
		
		oObj.transform.SetParent(a_oParent?.transform, a_bIsStayWorldState);
		oObj.transform.SetAsLastSibling();

		return oObj;
	}

	//! 에셋을 제거한다
	public static void RemoveAsset(string a_oFilePath, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oFilePath.ExIsValid());
		
		AssetDatabase.DeleteAsset(a_oFilePath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 에디터 윈도우를 생성한다
	public static T CreateEditorWnd<T>(string a_oName, Vector3 a_stMinSize) where T : EditorWindow {
		CAccess.Assert(a_oName.ExIsValid());

		var oPopup = EditorWindow.CreateWindow<T>(a_oName);
		oPopup.minSize = new Vector2(a_stMinSize.x, a_stMinSize.y);

		return oPopup;
	}

	//! 에셋을 생성한다
	public static T CreateAsset<T>(T a_tAsset, string a_oFilePath, bool a_bIsFocus = true) where T : Object {
		CAccess.Assert(a_tAsset != null && a_oFilePath.ExIsValid());

		// 에셋 생성이 가능 할 경우
		if(AssetDatabase.LoadAssetAtPath<T>(a_oFilePath) == null) {
			AssetDatabase.CreateAsset(a_tAsset, a_oFilePath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			// 포커스 모드 일 경우
			if(a_bIsFocus) {
				Selection.activeObject = a_tAsset;
				EditorUtility.FocusProjectWindow();
			}
		}
		
		return a_tAsset;
	}

	//! 스크립트 객체를 생성한다
	public static T CreateScriptableObj<T>(string a_oFilePath = KCDefine.B_EMPTY_STR) where T : ScriptableObject {
		string oFilePath = a_oFilePath.ExIsValid() ? a_oFilePath : string.Format(KCEditorDefine.B_ASSET_P_FMT_SCRIPTABLE_OBJ, typeof(T).ToString());
		var oScriptableObj = ScriptableObject.CreateInstance<T>();
		
		return CEditorFactory.CreateAsset<T>(oScriptableObj, oFilePath);
	}
	#endregion			// 제네릭 클래스 함수
}
#endif			// #if UNITY_EDITOR
