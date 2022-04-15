using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#if SCRIPT_TEMPLATE_ONLY
#if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
using UnityEditor;

/** 에디터 서브 전역 상수 */
public static partial class KEditorDefine {
	#region 런타임 상수
	// 경로 {
	public static List<string> G_EXTRA_DIR_P_PRELOAD_ASSET_LIST = new List<string>() {
		Path.GetDirectoryName($"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.B_DIR_P_FXS}"),
		Path.GetDirectoryName($"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.B_DIR_P_FONTS}"),
		Path.GetDirectoryName($"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.B_DIR_P_SOUNDS}"),
		Path.GetDirectoryName($"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.B_DIR_P_PREFABS}"),
		Path.GetDirectoryName($"{KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_RESOURCES}{KCDefine.B_DIR_P_SCRIPTABLES}"),

		$"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Resources/Fonts",
		$"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Resources/Pipelines",
		$"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Resources/Prefabs",
		$"{KCEditorDefine.B_DIR_P_PACKAGES}Module.UnityCommon/Resources/SpriteAtlases"
	};

	public static List<string> G_EXTRA_ASSET_P_PRELOAD_ASSET_LIST = new List<string>() {
		$"{KCEditorDefine.B_DIR_P_ASSETS}TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset"
	};
	// 경로 }
	#endregion			// 런타임 상수

	#region 추가 상수

	#endregion			// 추가 상수

	#region 추가 런타임 상수
	// 스크립트 순서
	public static Dictionary<System.Type, int> G_EXTRA_SCRIPT_ORDER_DICT = new Dictionary<System.Type, int>() {
		// Do Something
	};

	// 클래스 타입
	public static readonly Dictionary<string, System.Type> G_EXTRA_SCENE_MANAGER_TYPE_DICT = new Dictionary<string, System.Type>() {
		// Do Something
	};
	#endregion			// 추가 런타임 상수
}
#endif			// #if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY