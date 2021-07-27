using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 서브 중첩 씬 관리자
public partial class CSubOverlaySceneManager : COverlaySceneManager {
	#region 프로퍼티
	public override STSortingOrderInfo UIsCanvasSortingOrderInfo => KDefine.G_SORTING_OI_OVERLAY_SCENE_UIS_CANVAS;
	public override STSortingOrderInfo ObjsCanvasSortingOrderInfo => KDefine.G_SORTING_OI_OVERLAY_SCENE_OBJS_CANVAS;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		
		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupAwake();
		}
	}

	//! 초기화
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupStart();
			this.UpdateUIsState();
		}
	}

	//! 씬을 설정한다
	private void SetupAwake() {
#if DEBUG || DEVELOPMENT_BUILD
		this.SetupTestUIs();
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	//! 씬을 설정한다
	private void SetupStart() {
		// Do Nothing
	}

	//! UI 상태를 갱신한다
	private void UpdateUIsState() {
#if DEBUG || DEVELOPMENT_BUILD
		this.UpdateTestUIsState();
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}
	#endregion			// 함수

	#region 조건부 함수
#if DEBUG || DEVELOPMENT_BUILD
	//! 테스트 UI 를 설정한다
	private void SetupTestUIs() {
		// Do Nothing
	}

	//! 테스트 UI 상태를 갱신한다
	private void UpdateTestUIsState() {
		// Do Nothing
	}
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 조건부 함수
}
