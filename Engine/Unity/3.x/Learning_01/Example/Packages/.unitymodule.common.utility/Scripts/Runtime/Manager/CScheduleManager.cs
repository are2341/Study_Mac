using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	//! 상태를 갱신한다
	public virtual void Update() {
		bool bIsNeedUpdate = m_oAddComponentInfoList.Count > KCDefine.B_VALUE_INT_0;
		bIsNeedUpdate = bIsNeedUpdate || m_oRemoveComponentInfoList.Count > KCDefine.B_VALUE_INT_0;

		// 상태 갱신이 필요 할 경우
		if(bIsNeedUpdate || m_oComponentInfoList.Count > KCDefine.B_VALUE_INT_0) {
			this.UpdateComponentState();

			float fMaxDeltaTime = KCDefine.B_VALUE_FLT_1 / (float)Application.targetFrameRate;
			fMaxDeltaTime = Mathf.Abs(fMaxDeltaTime * 2.0f);

			this.DeltaTime = Mathf.Clamp(Time.deltaTime, KCDefine.B_VALUE_FLT_0, fMaxDeltaTime);
			this.UnscaleDeltaTime = Mathf.Clamp(Time.unscaledDeltaTime, KCDefine.B_VALUE_FLT_0, fMaxDeltaTime);

			for(int i = 0; i < m_oComponentInfoList.Count; ++i) {
				var oComponent = m_oComponentInfoList[i].m_oComponent as CComponent;

				// 갱신 함수 호출이 가능 할 경우
				if(!oComponent.IsDestroy && oComponent.gameObject.activeSelf) {
					oComponent.OnUpdate(this.DeltaTime);
				}
			}
		}
	}

	//! 상태를 갱신한다
	public virtual void LateUpdate() {
		m_fSkipTime += this.UnscaleDeltaTime;

		for(int i = 0; i < m_oComponentInfoList.Count; ++i) {
			var oComponent = m_oComponentInfoList[i].m_oComponent as CComponent;

			// 갱신 함수 호출이 가능 할 경우
			if(!oComponent.IsDestroy && oComponent.gameObject.activeSelf) {
				oComponent.OnLateUpdate(this.DeltaTime);
			}
		}

		// 콜백 호출 주기가 지났을 경우
		if(m_fSkipTime.ExIsGreateEquals(KCDefine.U_DELTA_T_SCHEDULE_M_CALLBACK)) {
			m_fSkipTime = KCDefine.B_VALUE_FLT_0;
			
			lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
				bool bIsNeedUpdate = m_oAddCallbackInfoList.Count > KCDefine.B_VALUE_INT_0;
				bIsNeedUpdate = bIsNeedUpdate || m_oRemoveCallbackInfoList.Count > KCDefine.B_VALUE_INT_0;

				// 상태 갱신이 필요 할 경우
				if(bIsNeedUpdate || m_oCallbackInfoList.Count > KCDefine.B_VALUE_INT_0) {
					this.UpdateCallbackState();

					for(int i = 0; i < m_oCallbackInfoList.Count; ++i) {
						m_oCallbackInfoList[i].m_oCallback?.Invoke();
					}

					m_oCallbackInfoList.Clear();
				}
			}
		}
	}

	//! 콜백을 추가한다
	public void AddCallback(string a_oKey, System.Action a_oCallback) {
		lock(KCDefine.U_LOCK_OBJ_SCHEDULE_M_UPDATE) {
			int nIdx = m_oCallbackInfoList.ExFindValue((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(a_oKey));

			// 콜백이 없을 경우
			if(!m_oCallbackInfoList.ExIsValidIdx(nIdx)) {
				m_oAddCallbackInfoList.ExAddValue(new STCallbackInfo() {
					m_oKey = a_oKey,
					m_oCallback = a_oCallback	
				});
			}
		}
	}

	//! 컴포넌트를 추가한다
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.ExFindValue((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 없을 경우
		if(!m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			m_oAddComponentInfoList.ExAddValue(new STComponentInfo() {
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
			int nIdx = m_oCallbackInfoList.ExFindValue((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(a_oKey));

			// 콜백 제거가 가능 할 경우
			if(m_oCallbackInfoList.ExIsValidIdx(nIdx)) {
				m_oRemoveCallbackInfoList.ExAddValue(new STCallbackInfo() {
					m_oKey = a_oKey,
					m_oCallback = null	
				});
			}
		}
	}

	//! 컴포넌트를 제거한다
	public void RemoveComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.ExFindValue((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트 제거가 가능 할 경우
		if(m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			m_oRemoveComponentInfoList.ExAddValue(new STComponentInfo() {
				m_nID = nID,
				m_oComponent = a_oComponent	
			});
		}
	}

	//! 타이머를 제거한다
	public void RemoveTimer(UnityAction a_oCallback) {
		TimersManager.ClearTimer(a_oCallback);
	}

	//! 스케쥴 콜백을 수신했을 경우
	private void OnReceiveScheduleCallback(CComponent a_oSender) {
		this.RemoveComponent(a_oSender);
	}

	//! 컴포넌트 상태를 갱신한다
	private void UpdateComponentState() {
		for(int i = 0; i < m_oAddComponentInfoList.Count; ++i) {
			var stComponentInfo = m_oAddComponentInfoList[i];
			m_oComponentInfoList.ExAddValue(stComponentInfo);

			var oComponent = stComponentInfo.m_oComponent as CComponent;
			oComponent.ScheduleCallback = this.OnReceiveScheduleCallback;
		}

		for(int i = 0; i < m_oRemoveComponentInfoList.Count; ++i) {
			var oComponent = m_oRemoveComponentInfoList[i].m_oComponent as CComponent;
			oComponent.ScheduleCallback = null;

			int nIdx = m_oComponentInfoList.ExFindValue((a_stComponentInfo) => a_stComponentInfo.m_nID == m_oRemoveComponentInfoList[i].m_nID);
			m_oComponentInfoList.ExRemoveValueAt(nIdx);
		}

		m_oAddComponentInfoList.Clear();
		m_oRemoveComponentInfoList.Clear();
	}

	//! 콜백 상태를 갱신한다
	private void UpdateCallbackState() {
		for(int i = 0; i < m_oAddCallbackInfoList.Count; ++i) {
			var stCallbackInfo = m_oAddCallbackInfoList[i];
			m_oCallbackInfoList.ExAddValue(stCallbackInfo);
		}

		for(int i = 0; i < m_oRemoveCallbackInfoList.Count; ++i) {
			int nIdx = m_oCallbackInfoList.ExFindValue((a_stCallbackInfo) => a_stCallbackInfo.m_oKey.ExIsEquals(m_oRemoveCallbackInfoList[i].m_oKey));
			m_oCallbackInfoList.ExRemoveValueAt(nIdx);
		}

		m_oAddCallbackInfoList.Clear();
		m_oRemoveCallbackInfoList.Clear();
	}
	#endregion			// 함수
}
