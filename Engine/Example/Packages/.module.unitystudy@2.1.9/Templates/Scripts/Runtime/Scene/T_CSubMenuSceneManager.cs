using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if SCRIPT_TEMPLATE_ONLY
#if STUDY_MODULE_ENABLE && SCENE_TEMPLATES_MODULE_ENABLE
/** 서브 메뉴 씬 관리자 */
public class CSubMenuSceneManager : CMenuSceneManager {
	#region 프로퍼티
	public override bool IsIgnoreOverlayScene => false;
	#endregion			// 프로퍼티

	#region 추가 변수

	#endregion			// 추가 변수

	#region 추가 프로퍼티

	#endregion			// 추가 프로퍼티
	
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupAwake();
		}
	}

	/** 내비게이션 스택 이벤트를 수신했을 경우 */
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);

		// 백 키 눌림 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			Func.ShowQuitPopup(this.OnReceiveQuitPopupResult);
		}
	}

	/** 씬을 설정한다 */
	private void SetupAwake() {
		// Do Something
	}

	/** 종료 팝업 결과를 수신했을 경우 */
	private void OnReceiveQuitPopupResult(CAlertPopup a_oSender, bool a_bIsOK) {
		// 확인 버튼을 눌렀을 경우
		if(a_bIsOK) {
			a_oSender.IsIgnoreAni = true;
			this.ExLateCallFunc((a_oSender) => this.QuitApp());
		}
	}
	#endregion			// 함수

	#region 추가 함수

	#endregion			// 추가 함수
}
#endif			// #if STUDY_MODULE_ENABLE && SCENE_TEMPLATES_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY
