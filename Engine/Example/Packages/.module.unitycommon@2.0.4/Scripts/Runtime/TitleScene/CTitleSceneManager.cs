using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

/** 타이틀 씬 관리자 */
public class CTitleSceneManager : CSceneManager {
	#region 변수
	/** =====> UI <===== */
	protected TMP_Text m_oVerText = null;
	#endregion			// 변수

	#region 프로퍼티
	public override bool IsRealtimeFadeInAni => true;
	public override bool IsRealtimeFadeOutAni => true;
	
	public override string SceneName => KCDefine.B_SCENE_N_TITLE;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		Time.timeScale = KCDefine.B_VAL_1_FLT;

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			// 텍스트를 설정한다 {
			var oVerText = this.UIsBase.ExFindComponent<TMP_Text>(KCDefine.TS_OBJ_N_VER_TEXT);

			m_oVerText = oVerText ?? CFactory.CreateCloneObj<TMP_Text>(KCDefine.TS_OBJ_N_VER_TEXT, KCDefine.TS_OBJ_P_VER_TEXT, this.UpUIs);
			m_oVerText.rectTransform.pivot = KCDefine.B_ANCHOR_UP_LEFT;
			m_oVerText.rectTransform.anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
			m_oVerText.rectTransform.anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
			m_oVerText.rectTransform.anchoredPosition = KCDefine.TS_POS_VER_TEXT.ExTo2D();
			// 텍스트를 설정한다 }
		}
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
			m_oVerText.text = CAccess.GetVerStr(CProjInfoTable.Inst.ProjInfo.m_stBuildVerInfo.m_oVer, CCommonUserInfoStorage.Inst.UserInfo.UserType);
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
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
	#endregion			// 함수
}
