using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if STUDY_MODULE_ENABLE
//! 메뉴 씬 관리자
public class CMenuSceneManager : CSceneManager {
	#region UI 변수
	protected Text m_oEmptyText = null;
	protected ScrollRect m_oScrollView = null;
	#endregion			// UI 변수

	#region 프로퍼티
	public override string SceneName => KCDefine.B_SCENE_N_MENU;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			m_oScrollView = this.SubUIs.ExFindComponent<ScrollRect>(KSDefine.MS_OBJ_N_SCROLL_VIEW);

			var oContents = m_oScrollView.gameObject.ExFindChild(KCDefine.U_OBJ_N_SCROLL_V_CONTENTS);
			int nNumDefScenes = KSDefine.MS_NUM_DEF_SCENES + KCDefine.B_VAL_1_INT;
			
			for(int i = nNumDefScenes; i < SceneManager.sceneCountInBuildSettings + KCDefine.B_VAL_1_INT; ++i) {
				int nIdx = i;
				string oName = string.Format(KSDefine.MS_OBJ_N_FMT_TEXT, i);
				string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);

				// 텍스트를 설정한다
				var oText = CFactory.CreateCloneObj<Text>(oName, KSDefine.MS_OBJ_P_TEXT, oContents);
				oText.text = (i < SceneManager.sceneCountInBuildSettings) ? Path.GetFileNameWithoutExtension(oScenePath) : string.Empty;

				// 빈 텍스트 일 경우
				if(i >= SceneManager.sceneCountInBuildSettings) {
					m_oEmptyText = oText;

					oText.name = KCDefine.U_OBJ_N_EMPTY;
					oText.gameObject.ExSetEnableComponent<Button>(false);
				} else {
					// 버튼을 설정한다
					var oBtn = oText.gameObject.GetComponentInChildren<Button>();
					oBtn.onClick.AddListener(() => this.OnTouchText(nIdx));

					// 제목 씬 일 경우
					if(oScenePath.Contains(KSDefine.MS_TOKEN_TITLE)) {
						oText.color = KSDefine.MS_COLOR_TITLE;
						oBtn.onClick.RemoveAllListeners();
					}
				}
			}
		}
	}

	//! 초기화
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			m_oEmptyText.transform.SetAsLastSibling();
		}
	}
	
	//! 텍스트를 눌렀을 경우
	private void OnTouchText(int a_nIdx) {
		string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIdx);
		CSceneLoader.Inst.LoadScene(oScenePath, false);
	}
	#endregion			// 함수
}
#endif			// #if STUDY_MODULE_ENABLE
