using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 스터디 씬 관리자
public abstract class CStudySceneManager : CSceneManager {
	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			var oBackBtn = CFactory.CreateCloneObj<Button>(KSDefine.SS_OBJ_N_BACK_BTN, KSDefine.SS_OBJ_P_BACK_BTN, this.SubTopUIs, KSDefine.SS_POS_BACK_BTN);
			oBackBtn.onClick.AddListener(this.OnTouchBackBtn);
		}
	}

	//! 내비게이션 스택 이벤트를 수신했을 경우
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);
		
		// 백 키 눌림 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_MENU);
		}
	}

	//! 백 버튼을 눌렀을 경우
	private void OnTouchBackBtn() {
		this.OnReceiveNavStackEvent(ENavStackEvent.BACK_KEY_DOWN);
	}
	#endregion			// 함수
}
