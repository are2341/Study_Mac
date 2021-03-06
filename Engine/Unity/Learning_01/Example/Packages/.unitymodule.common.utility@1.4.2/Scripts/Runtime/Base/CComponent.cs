﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 컴포넌트
public abstract class CComponent : MonoBehaviour {
	#region 프로퍼티
	public bool IsDestroy { get; private set; } = false;
	
	public bool IsIgnoreEnableEvent { get; set; } = false;
	public bool IsIgnoreNavStackEvent { get; set; } = false;

	public System.Action<CComponent> DestroyCallback { get; set; } = null;
	public System.Action<CComponent> ScheduleCallback { get; set; } = null;
	public System.Action<CComponent> NavStackCallback { get; set; } = null;

	public virtual bool IsIgnoreAni => false;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public virtual void Awake() {
		// Do Nothing
	}

	//! 초기화
	public virtual void Start() {
#if UNITY_EDITOR
		this.SetupScriptOrder();
#endif			// #if UNITY_EDITOR
	}

	//! 상태를 갱신한다
	public virtual void OnUpdate(float a_fDeltaTime) {
		// Do Nothing
	}

	//! 상태를 갱신한다
	public virtual void OnLateUpdate(float a_fDeltaTime) {
		// Do Nothing
	}

	//! 제거 되었을 경우
	public virtual void OnDestroy() {
		this.IsDestroy = true;

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			this.DestroyCallback?.Invoke(this);
			this.ScheduleCallback?.Invoke(this);
			this.NavStackCallback?.Invoke(this);
		}
	}

	//! 내비게이션 스택 이벤트를 수신했을 경우
	public virtual void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		// 백 키 눌림 이벤트 일 경우
		if(!this.IsIgnoreNavStackEvent && a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			CNavStackManager.Inst.RemoveComponent(this);
		}
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 설정한다
	protected virtual void SetupScriptOrder() {
		// Do Nothing
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
