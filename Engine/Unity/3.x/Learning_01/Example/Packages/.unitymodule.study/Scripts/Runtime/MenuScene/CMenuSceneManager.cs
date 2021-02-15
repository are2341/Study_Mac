using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if STUDY_MODULE_ENABLE
//! 메뉴 씬 관리자
public class CMenuSceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KCDefine.B_SCENE_N_MENU;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			var oContents = this.SubUIRoot.ExFindChild(KSDefine.MS_OBJ_N_SCROLL_V_CONTENTS);
			int nNumDefScenes = KSDefine.MS_NUM_DEF_SCENES + KCDefine.B_VALUE_INT_1;
			
			for(int i = nNumDefScenes; i < SceneManager.sceneCountInBuildSettings; ++i) {
				int nIdx = i;
				string oName = string.Format(KSDefine.MS_OBJ_N_FMT_TEXT, i);
				string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);

				var oTextObj = CFactory.CreateCloneObj(oName, KSDefine.MS_OBJ_P_TEXT, oContents);

				// 텍스트를 설정한다
				var oText = oTextObj.GetComponentInChildren<Text>();
				oText.text = Path.GetFileNameWithoutExtension(oScenePath);

				// 버튼을 설정한다
				var oBtn = oTextObj.GetComponentInChildren<Button>();
				oBtn.onClick.AddListener(() => this.OnTouchText(nIdx));

				// 제목 씬 일 경우
				if(oScenePath.Contains(KSDefine.MS_TOKEN_TITLE)) {
					oText.color = KSDefine.MS_COLOR_TITLE;
					oBtn.onClick.RemoveAllListeners();
				}
			}
		}
	}

	//! 내비게이션 스택 이벤트를 수신했을 경우
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);

		// 백 키 눌림 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			var oDataList = new Dictionary<string, string>() {
				[KCDefine.U_KEY_ALERT_P_TITLE] = CStringTable.Inst.GetString(KCDefine.ST_KEY_ALERT_P_TITLE),
				[KCDefine.U_KEY_ALERT_P_MSG] = CStringTable.Inst.GetString(KCDefine.ST_KEY_QUIT_P_MSG),
				[KCDefine.U_KEY_ALERT_P_OK_BTN_TEXT] = CStringTable.Inst.GetString(KCDefine.ST_KEY_ALERT_P_OK_BTN_TEXT),
				[KCDefine.U_KEY_ALERT_P_CANCEL_BTN_TEXT] = CStringTable.Inst.GetString(KCDefine.ST_KEY_ALERT_P_CANCEL_BTN_TEXT)
			};
			
			var oAlertPopup = CAlertPopup.Create<CAlertPopup>(KCDefine.U_OBJ_N_ALERT_POPUP, KCDefine.U_OBJ_P_G_ALERT_POPUP, this.SubPopupUIRoot, oDataList, this.OnReceiveAlertPopupResult);
			oAlertPopup.Show(null, null);
		}
	}

	//! 경고 팝업 결과를 수신했을 경우
	private void OnReceiveAlertPopupResult(CAlertPopup a_oSender, bool a_bIsOK) {
		// 확인 버튼을 눌렀을 경우
		if(a_bIsOK) {
			CFunc.QuitApp();
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
