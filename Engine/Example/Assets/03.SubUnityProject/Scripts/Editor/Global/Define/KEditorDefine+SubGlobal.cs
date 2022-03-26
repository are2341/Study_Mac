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
	public static Dictionary<System.Type, int> G_EXTRA_SCRIPT_ORDER_DICT = new Dictionary<System.Type, int>() {
		[typeof(_3xE01._3xE01Example_052)] = KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_3xE01._3xE01Example_124)] = KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_4xE01._4xE01Example_048)] = KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_5xE01._5xE01Example_042)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE01._5xE01Example_116)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_5xE02._5xE02Example_074)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE03._5xE03Example_058)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_5xE04._5xE04Example_049)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,

		[typeof(_2018xE01._2018xE01Example_048)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER,
		[typeof(_2019xE01._2019xE01Example_124)] =  KCDefine.U_SCRIPT_O_SCENE_MANAGER
	};

	// 클래스 타입
	public static readonly Dictionary<string, System.Type> G_EXTRA_SCENE_MANAGER_TYPE_DICT = new Dictionary<string, System.Type>() {
		[KDefine.G_SCENE_N_3X_E01_EXAMPLE_052] = typeof(_3xE01._3xE01Example_052),
		[KDefine.G_SCENE_N_3X_E01_EXAMPLE_124] = typeof(_3xE01._3xE01Example_124),

		[KDefine.G_SCENE_N_4X_E01_EXAMPLE_048] = typeof(_4xE01._4xE01Example_048),

		[KDefine.G_SCENE_N_5X_E01_EXAMPLE_042] = typeof(_5xE01._5xE01Example_042),
		[KDefine.G_SCENE_N_5X_E01_EXAMPLE_116] = typeof(_5xE01._5xE01Example_116),

		[KDefine.G_SCENE_N_5X_E02_EXAMPLE_074] = typeof(_5xE02._5xE02Example_074),
		[KDefine.G_SCENE_N_5X_E03_EXAMPLE_058] = typeof(_5xE03._5xE03Example_058),
		[KDefine.G_SCENE_N_5X_E04_EXAMPLE_049] = typeof(_5xE04._5xE04Example_049),

		[KDefine.G_SCENE_N_2018X_E01_EXAMPLE_048] = typeof(_2018xE01._2018xE01Example_048),
		[KDefine.G_SCENE_N_2019X_E01_EXAMPLE_124] = typeof(_2019xE01._2019xE01Example_124)
	};
	#endregion			// 런타임 상수
}
#endif			// #if UNITY_EDITOR && EXTRA_SCRIPT_ENABLE
