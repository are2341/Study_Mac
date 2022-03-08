﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Timers;

/** 스케줄 관리자 */
public class CScheduleManager : CSingleton<CScheduleManager> {
	#region 변수
	private float m_fUpdateSkipTime = 0.0f;
	
	private CListWrapper<STCallbackInfo> m_oCallbackInfoListWrapper = new CListWrapper<STCallbackInfo>();
	private CListWrapper<STComponentInfo> m_oComponentInfoListWrapper = new CListWrapper<STComponentInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public float DeltaTime { get; private set; } = 0.0f;
	public float UnscaleDeltaTime { get; private set; } = 0.0f;
	#endregion			// 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset() {
		base.Reset();

		m_oCallbackInfoListWrapper.m_oList.Clear();
		m_oCallbackInfoListWrapper.m_oAddList.Clear();
		m_oCallbackInfoListWrapper.m_oRemoveList.Clear();

		m_oComponentInfoListWrapper.m_oList.Clear();
		m_oComponentInfoListWrapper.m_oAddList.Clear();
		m_oComponentInfoListWrapper.m_oRemoveList.Clear();
	}

	/** 상태를 갱신한다 */
	public virtual void Update() {
		float fMaxDeltaTime = KCDefine.B_VAL_1_FLT / (float)Application.targetFrameRate;
		fMaxDeltaTime = Mathf.Abs(fMaxDeltaTime * KCDefine.B_VAL_2_FLT);

		this.DeltaTime = Mathf.Clamp(Time.deltaTime, KCDefine.B_VAL_0_FLT, fMaxDeltaTime);
		this.UnscaleDeltaTime = Mathf.Clamp(Time.unscaledDeltaTime, KCDefine.B_VAL_0_FLT, fMaxDeltaTime);

		this.UpdateComponentInfosState();

		for(int i = 0; i < m_oComponentInfoListWrapper.m_oList.Count; ++i) {
			var oComponent = m_oComponentInfoListWrapper.m_oList[i].m_oComponent as CComponent;

			// 제거 되었을 경우
			if(oComponent.IsDestroy) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(oComponent.enabled && oComponent.gameObject.activeSelf) {
				oComponent.OnUpdate(this.DeltaTime);
			}
		}
	}

	/** 상태를 갱신한다 */
	public virtual void LateUpdate() {
		m_fUpdateSkipTime += this.UnscaleDeltaTime;

		for(int i = 0; i < m_oComponentInfoListWrapper.m_oList.Count; ++i) {
			var oComponent = m_oComponentInfoListWrapper.m_oList[i].m_oComponent as CComponent;

			// 제거 되었을 경우
			if(oComponent.IsDestroy) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(oComponent.enabled && oComponent.gameObject.activeSelf) {
				oComponent.OnLateUpdate(this.DeltaTime);
			}
		}

		// 콜백 호출 주기가 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreateEquals(KCDefine.U_DELTA_T_SCHEDULE_M_CALLBACK)) {
			m_fUpdateSkipTime = KCDefine.B_VAL_0_FLT;

			lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
				this.UpdateCallbackInfosState();

				for(int i = 0; i < m_oCallbackInfoListWrapper.m_oList.Count; ++i) {
					m_oCallbackInfoListWrapper.m_oList[i].m_oCallback?.Invoke();
				}

				m_oCallbackInfoListWrapper.m_oList.Clear();
			}
		}
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
			CSceneManager.ScheduleManager = null;
		}
	}

	/** 콜백을 추가한다 */
	public void AddCallback(string a_oKey, System.Action a_oCallback) {
		lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
			int nIdx = m_oCallbackInfoListWrapper.m_oList.FindIndex((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.Equals(a_oKey));

			// 콜백이 없을 경우
			if(!m_oCallbackInfoListWrapper.m_oList.ExIsValidIdx(nIdx)) {
				m_oCallbackInfoListWrapper.m_oAddList.ExAddVal(new STCallbackInfo() {
					m_oKey = a_oKey, m_oCallback = a_oCallback
				});
			}
		}
	}

	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoListWrapper.m_oList.FindIndex((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 없을 경우
		if(!m_oComponentInfoListWrapper.m_oList.ExIsValidIdx(nIdx)) {
			m_oComponentInfoListWrapper.m_oAddList.ExAddVal(new STComponentInfo() {
				m_nID = nID, m_oComponent = a_oComponent
			});
		}
	}

	/** 타이머를 추가한다 */
	public void AddTimer(CComponent a_oComponent, float a_fDeltaTime, uint a_nRepeatTimes, UnityAction a_oCallback, bool a_bIsRealtime = false) {
		var eTimerMode = a_bIsRealtime ? TimerMode.REALTIME : TimerMode.NORM;
		TimersManager.SetTimer(a_oComponent, new Timer(a_fDeltaTime, a_nRepeatTimes, a_oCallback, eTimerMode));
	}

	/** 반복 타이머를 추가한다 */
	public void AddRepeatTimer(CComponent a_oComponent, float a_fDeltaTime, UnityAction a_oCallback, bool a_bIsRealtime = false) {
		this.AddTimer(a_oComponent, a_fDeltaTime, Timer.INFINITE, a_oCallback, a_bIsRealtime);
	}

	/** 콜백을 제거한다 */
	public void RemoveCallback(string a_oKey) {
		lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
			int nIdx = m_oCallbackInfoListWrapper.m_oList.FindIndex((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.Equals(a_oKey));

			// 콜백이 존재 할 경우
			if(m_oCallbackInfoListWrapper.m_oList.ExIsValidIdx(nIdx)) {
				m_oCallbackInfoListWrapper.m_oRemoveList.ExAddVal(new STCallbackInfo() {
					m_oKey = a_oKey, m_oCallback = null
				});
			}
		}
	}

	/** 컴포넌트를 제거한다 */
	public void RemoveComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoListWrapper.m_oList.FindIndex((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 존재 할 경우
		if(m_oComponentInfoListWrapper.m_oList.ExIsValidIdx(nIdx)) {
			m_oComponentInfoListWrapper.m_oRemoveList.ExAddVal(new STComponentInfo() {
				m_nID = nID, m_oComponent = a_oComponent
			});
		}
	}

	/** 타이머를 제거한다 */
	public void RemoveTimer(CComponent a_oComponent) {
		TimersManager.ClearTimer(new System.WeakReference(a_oComponent));
	}

	/** 타이머를 제거한다 */
	public void RemoveTimer(UnityAction a_oCallback) {
		TimersManager.ClearTimer(a_oCallback);
	}

	/** 콜백 정보 상태를 갱신한다 */
	private void UpdateCallbackInfosState() {
		for(int i = 0; i < m_oCallbackInfoListWrapper.m_oAddList.Count; ++i) {
			m_oCallbackInfoListWrapper.m_oList.ExAddVal(m_oCallbackInfoListWrapper.m_oAddList[i]);
		}

		for(int i = 0; i < m_oCallbackInfoListWrapper.m_oRemoveList.Count; ++i) {
			var stCallbackInfo = m_oCallbackInfoListWrapper.m_oRemoveList[i];
			m_oCallbackInfoListWrapper.m_oList.ExRemoveVal((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.Equals(stCallbackInfo.m_oKey));
		}
		
		m_oCallbackInfoListWrapper.m_oAddList.Clear();
		m_oCallbackInfoListWrapper.m_oRemoveList.Clear();
	}

	/** 컴포넌트 정보 상태를 갱신한다 */
	private void UpdateComponentInfosState() {
		for(int i = 0; i < m_oComponentInfoListWrapper.m_oAddList.Count; ++i) {
			m_oComponentInfoListWrapper.m_oList.ExAddVal(m_oComponentInfoListWrapper.m_oAddList[i]);
			(m_oComponentInfoListWrapper.m_oAddList[i].m_oComponent as CComponent).ScheduleCallback = this.OnReceiveScheduleCallback;
		}

		for(int i = 0; i < m_oComponentInfoListWrapper.m_oRemoveList.Count; ++i) {
			(m_oComponentInfoListWrapper.m_oRemoveList[i].m_oComponent as CComponent).ScheduleCallback = null;
			m_oComponentInfoListWrapper.m_oList.ExRemoveVal((a_stComponentInfo) => a_stComponentInfo.m_nID == m_oComponentInfoListWrapper.m_oRemoveList[i].m_nID);
		}

		m_oComponentInfoListWrapper.m_oAddList.Clear();
		m_oComponentInfoListWrapper.m_oRemoveList.Clear();
	}

	/** 스케쥴 콜백을 수신했을 경우 */
	private void OnReceiveScheduleCallback(CComponent a_oSender) {
		this.RemoveComponent(a_oSender);
	}
	#endregion			// 함수
}