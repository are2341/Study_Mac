using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
using UnityEditor;

/** 에디터 서브 전역 상수 */
public static partial class KEditorDefine {
	#region 런타임 상수
	// 스크립트 순서
	public static Dictionary<System.Type, int> B_EXTRA_SCRIPT_ORDER_DICT = new Dictionary<System.Type, int>() {
		[typeof(_4xE01._4xE01Example_048)] = KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_5xE01._5xE01Example_042)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE01._5xE01Example_116)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_5xE02._5xE02Example_074)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE03._5xE03Example_058)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE04._5xE04Example_049)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_2018xE01._2018xE01Example_048)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_2019xE01._2019xE01Example_124)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER
	};
	#endregion			// 런타임 상수
}
#endif			// #if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
