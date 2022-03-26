using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if SCRIPT_TEMPLATE_ONLY
#if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
using UnityEditor;

/** 에디터 서브 전역 상수 */
public static partial class KEditorDefine {
	#region 런타임 상수
	// 스크립트 순서
	public static Dictionary<System.Type, int> G_EXTRA_SCRIPT_ORDER_DICT = new Dictionary<System.Type, int>() {
		// Do Something
	};

	// 클래스 타입
	public static readonly Dictionary<string, System.Type> G_EXTRA_SCENE_MANAGER_TYPE_DICT = new Dictionary<string, System.Type>() {
		// Do Something
	};
	#endregion			// 런타임 상수
}
#endif			// #if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY
