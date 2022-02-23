using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if SCRIPT_TEMPLATE_ONLY
#if RUNTIME_TEMPLATES_MODULE_ENABLE
/** 서브 타이틀 씬 관리자 */
public partial class CSubTitleSceneManager : CTitleSceneManager {
#if DEBUG || DEVELOPMENT_BUILD
	/** 테스트 UI */
	[System.Serializable]
	private struct STTestUIs {
		// Do Something
	}
#endif			// #if DEBUG || DEVELOPMENT_BUILD

	#region 변수
	private bool m_bIsLoadLevelEditorScene = false;

	/** =====> UI <===== */
#if DEBUG || DEVELOPMENT_BUILD
	[SerializeField] private STTestUIs m_stTestUIs;
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 변수

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

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupStart();
			this.UpdateUIsState();

			// 최초 시작 일 경우
			if(CCommonAppInfoStorage.Inst.IsFirstStart) {
				this.UpdateFirstStartState();
			}
			
#if !TITLE_SCENE_ENABLE
			// 레벨 에디터 씬을 로드하지 않았을 경우
			if(!m_bIsLoadLevelEditorScene) {
				CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_MAIN);
			}
#endif			// #if !TITLE_SCENE_ENABLE
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
		// 버튼을 설정한다
		var oPlayBtn = this.UIsBase.ExFindComponent<Button>(KCDefine.U_OBJ_N_PLAY_BTN);
		oPlayBtn?.ExAddListener(this.OnTouchPlayBtn, true, false);

#if DEBUG || DEVELOPMENT_BUILD
		this.SetupTestUIs();
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 씬을 설정한다 */
	private void SetupStart() {
		// 최초 플레이 일 경우
		if(CCommonAppInfoStorage.Inst.AppInfo.IsFirstPlay) {
			this.UpdateFirstPlayState();
		} else {
#if TITLE_SCENE_ENABLE
			// 업데이트가 가능 할 경우
			if(!CAppInfoStorage.Inst.IsIgnoreUpdate && CCommonAppInfoStorage.Inst.IsEnableUpdate()) {
				CAppInfoStorage.Inst.IsIgnoreUpdate = true;
				this.ExLateCallFunc((a_oSender) => Func.ShowUpdatePopup(this.OnReceiveUpdatePopupResult));
			}
#endif			// #if TITLE_SCENE_ENABLE
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
#if DEBUG || DEVELOPMENT_BUILD
		this.UpdateTestUIsState();
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 최초 시작 상태를 갱신한다 */
	private void UpdateFirstStartState() {
		LogFunc.SendLaunchLog();
		LogFunc.SendSplashLog();
		
		CCommonAppInfoStorage.Inst.IsFirstStart = false;
		
#if (!UNITY_EDITOR && UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
		m_bIsLoadLevelEditorScene = true;
		CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_LEVEL_EDITOR);
#endif			// #if (!UNITY_EDITOR && UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
	}

	/** 최초 플레이 상태를 갱신한다 */
	private void UpdateFirstPlayState() {
		CCommonAppInfoStorage.Inst.AppInfo.IsFirstPlay = false;
		CCommonAppInfoStorage.Inst.SaveAppInfo();

		// 약관 동의 팝업이 닫혔을 경우
		if(CAppInfoStorage.Inst.IsCloseAgreePopup) {
			LogFunc.SendAgreeLog();
		}
	}

	/** 종료 팝업 결과를 수신했을 경우 */
	private void OnReceiveQuitPopupResult(CAlertPopup a_oSender, bool a_bIsOK) {
		// 확인 버튼을 눌렀을 경우
		if(a_bIsOK) {
			a_oSender.IsIgnoreAni = true;
			this.ExLateCallFunc((a_oSender) => this.QuitApp());
		}
	}

	/** 업데이트 팝업 결과를 수신했을 경우 */
	private void OnReceiveUpdatePopupResult(CAlertPopup a_oSender, bool a_bIsOK) {
		// 확인 버튼을 눌렀을 경우
		if(a_bIsOK) {
			Application.OpenURL(Access.StoreURL);
		}
	}

	/** 플레이 버튼을 눌렀을 경우 */
	private void OnTouchPlayBtn() {
		// Do Something
	}
	#endregion			// 함수

	#region 조건부 함수
#if DEBUG || DEVELOPMENT_BUILD
	/** 테스트 UI 를 설정한다 */
	private void SetupTestUIs() {
		// Do Something
	}

	/** 테스트 UI 상태를 갱신한다 */
	private void UpdateTestUIsState() {
		// Do Something
	}
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 조건부 함수

	#region 추가 함수
#if DEBUG || DEVELOPMENT_BUILD

#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 추가 함수
}
#endif			// #if RUNTIME_TEMPLATES_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY
