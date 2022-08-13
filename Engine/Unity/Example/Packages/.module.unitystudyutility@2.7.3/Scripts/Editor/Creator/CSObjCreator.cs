using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

/** 스터디 객체 생성자 */
public static class CSObjCreator {
	#region 클래스 함수
	/** 스터디 버튼을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_GAME_OBJECT_UI_BASE + "Study/Button", false, 101)]
	public static void CreateStudyBtn() {
		CObjCreator.CreateObj(KSDefine.SS_OBJ_N_STUDY_BTN, KSDefine.SS_OBJ_P_BTN);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
