﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if STUDY_MODULE_ENABLE
#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

/** 메뉴 씬 관리자 */
public class CMenuSceneManager : CSceneManager {
	#region 변수
	/** =====> UI <===== */
	protected ScrollRect m_oScrollRect = null;
	protected List<TMP_Text> m_oEmptyTextList = new List<TMP_Text>();
	#endregion			// 변수

	#region 프로퍼티
	public override string SceneName => KCDefine.B_SCENE_N_MENU;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupAwake();
		}
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupStart();
		}
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
#if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
#if INPUT_SYSTEM_MODULE_ENABLE
			bool bIsEditorKeyDown = Keyboard.current.leftShiftKey.isPressed && Keyboard.current.eKey.wasPressedThisFrame;
#else
			bool bIsEditorKeyDown = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

			// 에디터 키를 눌렀을 경우
			if(bIsEditorKeyDown) {
				CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_LEVEL_EDITOR);
			}
#endif			// #if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
		}
	}

	/** 씬을 설정한다 */
	private void SetupAwake() {
		m_oScrollRect = this.UIsBase.ExFindComponent<ScrollRect>(KSDefine.MS_OBJ_N_SCROLL_VIEW);
		var oContents = m_oScrollRect.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);

		for(int i = 0; i < KCDefine.B_VAL_2_INT; ++i) {
			m_oEmptyTextList.Add(CFactory.CreateCloneObj<TMP_Text>(KCDefine.U_OBJ_N_EMPTY, KSDefine.MS_OBJ_P_TEXT, oContents));
		}

		for(int i = KSDefine.MS_DEF_SCENE_NAMES.Count; i < SceneManager.sceneCountInBuildSettings; ++i) {
			string oSceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

			// 텍스트를 설정한다 {
			var oText = CFactory.CreateCloneObj<TMP_Text>(string.Format(KSDefine.MS_OBJ_N_FMT_TEXT, i), KSDefine.MS_OBJ_P_TEXT, oContents);
			oText.color = oSceneName.Contains(KSDefine.MS_TOKEN_TITLE) ? KSDefine.MS_COLOR_TITLE : oText.color;

#if NEWTON_SOFT_JSON_MODULE_ENABLE
			oText.ExSetText(oSceneName, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			// 텍스트를 설정한다 }
			
			// 레벨 에디터 씬 일 경우
			if(oSceneName.Equals(KCDefine.B_SCENE_N_LEVEL_EDITOR)) {
				m_oEmptyTextList.Add(oText);
			}

			// 제목, 테스트, 레벨 에디터 씬 일 경우
			if(m_oEmptyTextList.Contains(oText) || oSceneName.Contains(KSDefine.MS_TOKEN_TITLE)) {
				oText.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
			} else {
				int nIdx = i;
				oText.GetComponentInChildren<Button>().onClick.AddListener(() => this.OnTouchText(nIdx));
			}
		}
	}

	/** 씬을 설정한다 */
	private void SetupStart() {
		for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
			m_oEmptyTextList[i].transform.SetAsLastSibling();
			m_oEmptyTextList[i].gameObject.ExSetEnableComponent<Button>(false, false);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
			m_oEmptyTextList[i].ExSetText(string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
		}
	}
	
	/** 텍스트를 눌렀을 경우 */
	private void OnTouchText(int a_nIdx) {
		CSceneLoader.Inst.LoadScene(SceneUtility.GetScenePathByBuildIndex(a_nIdx));
	}
	#endregion			// 함수
}
#endif			// #if STUDY_MODULE_ENABLE
