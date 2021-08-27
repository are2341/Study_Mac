using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Timers;

//! 스케줄 관리자
public class CScheduleManager : CSingleton<CScheduleManager> {
	#region 변수
	private float m_fSkipTime = 0.0f;

	private List<STCallbackInfo> m_oCallbackInfoList = new List<STCallbackInfo>();
	private List<STCallbackInfo> m_oAddCallbackInfoList = new List<STCallbackInfo>();
	private List<STCallbackInfo> m_oRemoveCallbackInfoList = new List<STCallbackInfo>();

	private List<STComponentInfo> m_oComponentInfoList = new List<STComponentInfo>();
	private List<STComponentInfo> m_oAddComponentInfoList = new List<STComponentInfo>();
	private List<STComponentInfo> m_oRemoveComponentInfoList = new List<STComponentInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public float DeltaTime { get; private set; } = 0.0f;
	public float UnscaleDeltaTime { get; private set; } = 0.0f;
	#endregion			// 프로퍼티

	#region 함수
	//! 상태를 리셋한다
	public virtual void Reset() {
		m_oCallbackInfoList.Clear();
		m_oAddCallbackInfoList.Clear();
		m_oRemoveCallbackInfoList.Clear();

		m_oComponentInfoList.Clear();
		m_oAddComponentInfoList.Clear();
		m_oRemoveComponentInfoList.Clear();
	}

	//! 상태를 갱신한다
	public virtual void Update() {
		// 컴포넌트 정보 갱신이 필요 할 경우
		if(m_oAddComponentInfoList.Count > KCDefine.B_VAL_0_INT || m_oRemoveComponentInfoList.Count > KCDefine.B_VAL_0_INT) {
			this.UpdateComponentInfosState();
		}

		float fMaxDeltaTime = KCDefine.B_VAL_1_FLT / (float)Application.targetFrameRate;
		fMaxDeltaTime = Mathf.Abs(fMaxDeltaTime * KCDefine.B_VAL_9_FLT);

		this.DeltaTime = Mathf.Clamp(Time.deltaTime, KCDefine.B_VAL_0_FLT, fMaxDeltaTime);
		this.UnscaleDeltaTime = Mathf.Clamp(Time.unscaledDeltaTime, KCDefine.B_VAL_0_FLT, fMaxDeltaTime);

		for(int i = 0; i < m_oComponentInfoList.Count; ++i) {
			var oComponent = m_oComponentInfoList[i].m_oComponent as CComponent;

			// 객체가 없을 경우
			if(oComponent.gameObject == null) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(!oComponent.IsDestroy && (oComponent.enabled && oComponent.gameObject.activeSelf)) {
				oComponent.OnUpdate(this.DeltaTime);
			}
		}
	}

	//! 상태를 갱신한다
	public virtual void LateUpdate() {
		m_fSkipTime += this.UnscaleDeltaTime;

		for(int i = 0; i < m_oComponentInfoList.Count; ++i) {
			var oComponent = m_oComponentInfoList[i].m_oComponent as CComponent;

			// 상태 갱신이 가능 할 경우
			if(!oComponent.IsDestroy && (oComponent.enabled && oComponent.gameObject.activeSelf)) {
				oComponent.OnLateUpdate(this.DeltaTime);
			}
		}

		// 콜백 호출 주기가 지났을 경우
		if(m_fSkipTime.ExIsGreateEquals(KCDefine.U_DELTA_T_SCHEDULE_M_CALLBACK)) {
			m_fSkipTime = KCDefine.B_VAL_0_FLT;
			
			lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
				// 콜백 정보 갱신이 필요 할 경우
				if(m_oAddCallbackInfoList.Count > KCDefine.B_VAL_0_INT || m_oRemoveCallbackInfoList.Count > KCDefine.B_VAL_0_INT) {
					this.UpdateCallbackInfosState();
				}

				for(int i = 0; i < m_oCallbackInfoList.Count; ++i) {
					m_oCallbackInfoList[i].m_oCallback?.Invoke();
				}

				m_oCallbackInfoList.Clear();
			}
		}
	}

	//! 콜백을 추가한다
	public void AddCallback(string a_oKey, System.Action a_oCallback) {
		lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
			int nIdx = m_oCallbackInfoList.ExFindVal((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(a_oKey));

			// 콜백이 없을 경우
			if(!m_oCallbackInfoList.ExIsValidIdx(nIdx)) {
				m_oAddCallbackInfoList.ExAddVal(new STCallbackInfo() {
					m_oKey = a_oKey,
					m_oCallback = a_oCallback	
				});
			}
		}
	}

	//! 컴포넌트를 추가한다
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.ExFindVal((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 없을 경우
		if(!m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			m_oAddComponentInfoList.ExAddVal(new STComponentInfo() {
				m_nID = nID,
				m_oComponent = a_oComponent	
			});
		}
	}

	//! 타이머를 추가한다
	public void AddTimer(CComponent a_oComponent, float a_fDeltaTime, uint a_nRepeatTimes, UnityAction a_oCallback, bool a_bIsRealtime = false) {
		var eTimerMode = a_bIsRealtime ? TimerMode.REALTIME : TimerMode.NORM;
		var oTimer = new Timer(a_fDeltaTime, a_nRepeatTimes, a_oCallback, eTimerMode);
		
		TimersManager.SetTimer(a_oComponent, oTimer);
	}

	//! 반복 타이머를 추가한다
	public void AddRepeatTimer(CComponent a_oComponent, float a_fDeltaTime, UnityAction a_oCallback, bool a_bIsRealtime = false) {
		this.AddTimer(a_oComponent, a_fDeltaTime, Timer.INFINITE, a_oCallback, a_bIsRealtime);
	}

	//! 콜백을 제거한다
	public void RemoveCallback(string a_oKey) {
		lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
			int nIdx = m_oCallbackInfoList.ExFindVal((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(a_oKey));

			// 콜백이 존재 할 경우
			if(m_oCallbackInfoList.ExIsValidIdx(nIdx)) {
				m_oRemoveCallbackInfoList.ExAddVal(new STCallbackInfo() {
					m_oKey = a_oKey,
					m_oCallback = null	
				});
			}
		}
	}

	//! 컴포넌트를 제거한다
	public void RemoveComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.ExFindVal((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 존재 할 경우
		if(m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			m_oRemoveComponentInfoList.ExAddVal(new STComponentInfo() {
				m_nID = nID,
				m_oComponent = a_oComponent	
			});
		}
	}

	//! 타이머를 제거한다
	public void RemoveTimer(CComponent a_oComponent) {
		TimersManager.ClearTimer(new System.WeakReference(a_oComponent));
	}

	//! 타이머를 제거한다
	public void RemoveTimer(UnityAction a_oCallback) {
		TimersManager.ClearTimer(a_oCallback);
	}

	//! 스케쥴 콜백을 수신했을 경우
	private void OnReceiveScheduleCallback(CComponent a_oSender) {
		this.RemoveComponent(a_oSender);
	}

	//! 콜백 정보 상태를 갱신한다
	private void UpdateCallbackInfosState() {
		for(int i = 0; i < m_oAddCallbackInfoList.Count; ++i) {
			var stCallbackInfo = m_oAddCallbackInfoList[i];
			m_oCallbackInfoList.ExAddVal(stCallbackInfo);
		}

		for(int i = 0; i < m_oRemoveCallbackInfoList.Count; ++i) {
			var stCallbackInfo = m_oRemoveCallbackInfoList[i];
			m_oCallbackInfoList.ExRemoveVal((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(stCallbackInfo.m_oKey));
		}
		
		m_oAddCallbackInfoList.Clear();
		m_oRemoveCallbackInfoList.Clear();
	}

	//! 컴포넌트 정보 상태를 갱신한다
	private void UpdateComponentInfosState() {
		for(int i = 0; i < m_oAddComponentInfoList.Count; ++i) {
			var stComponentInfo = m_oAddComponentInfoList[i];
			m_oComponentInfoList.ExAddVal(stComponentInfo);

			var oComponent = stComponentInfo.m_oComponent as CComponent;
			oComponent.ScheduleCallback = this.OnReceiveScheduleCallback;
		}

		for(int i = 0; i < m_oRemoveComponentInfoList.Count; ++i) {
			var oComponent = m_oRemoveComponentInfoList[i].m_oComponent as CComponent;
			oComponent.ScheduleCallback = null;

			var stComponentInfo = m_oRemoveComponentInfoList[i];
			m_oComponentInfoList.ExRemoveVal((a_stComponentInfo) => a_stComponentInfo.m_nID == stComponentInfo.m_nID);
		}

		m_oAddComponentInfoList.Clear();
		m_oRemoveComponentInfoList.Clear();
	}
	#endregion			// 함수
}
