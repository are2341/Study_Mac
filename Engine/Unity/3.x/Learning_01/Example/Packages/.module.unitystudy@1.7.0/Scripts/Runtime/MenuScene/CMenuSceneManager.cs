using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if STUDY_MODULE_ENABLE
//! 메뉴 씬 관리자
public class CMenuSceneManager : CSceneManager {
	#region 변수
	// UI
	protected ScrollRect m_oScrollRect = null;
	protected List<Text> m_oEmptyTextList = new List<Text>();
	#endregion			// 변수

	#region 프로퍼티
	public override string SceneName => KCDefine.B_SCENE_N_MENU;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			m_oScrollRect = this.SubUIs.ExFindComponent<ScrollRect>(KSDefine.MS_OBJ_N_SCROLL_VIEW);

			var oContents = m_oScrollRect.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);
			int nNumDefScenes = KSDefine.MS_NUM_DEF_SCENES + KCDefine.B_VAL_1_INT;

			for(int i = nNumDefScenes; i < SceneManager.sceneCountInBuildSettings + KCDefine.B_VAL_2_INT; ++i) {
				string oName = string.Format(KSDefine.MS_OBJ_N_FMT_TEXT, i);
				string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);
				string oSceneName = Path.GetFileNameWithoutExtension(oScenePath);

				// 텍스트를 설정한다
				var oText = CFactory.CreateCloneObj<Text>(oName, KSDefine.MS_OBJ_P_TEXT, oContents);
				oText.text = oSceneName;

				// 빈 텍스트 일 경우
				if(oSceneName.ExIsEquals(KCDefine.B_SCENE_N_LEVEL_EDITOR) || i >= SceneManager.sceneCountInBuildSettings) {
					m_oEmptyTextList.Add(oText);

					oText.name = KCDefine.U_OBJ_N_EMPTY;
					oText.text = string.Empty;
					oText.gameObject.ExSetEnableComponent<Button>(false, false);
				} else {
					int nIdx = i;
					
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
			for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
				m_oEmptyTextList[i].transform.SetAsLastSibling();
			}
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
