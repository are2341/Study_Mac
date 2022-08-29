using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if STUDY_MODULE_ENABLE && SCENE_TEMPLATES_MODULE_ENABLE
namespace MenuScene {
	/** 스터디 서브 메뉴 씬 관리자 */
	public partial class CSSubMenuSceneManager : CSMenuSceneManager {
		#region 함수
		/** 초기화 */
		public override void Awake() {
			base.Awake();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				this.AwakeSetup();
			}
		}

		/** 내비게이션 스택 이벤트를 수신했을 경우 */
		public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
			base.OnReceiveNavStackEvent(a_eEvent);

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
			// 백 키 눌림 이벤트 일 경우
			if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
				Func.ShowQuitPopup(this.OnReceiveQuitPopupResult);
			}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
		}

		/** 씬을 설정한다 */
		private void AwakeSetup() {
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
	}
}
#endif			// #if STUDY_MODULE_ENABLE && SCENE_TEMPLATES_MODULE_ENABLE