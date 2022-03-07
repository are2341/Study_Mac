using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/** 내비게이션 스택 관리자 */
public class CNavStackManager : CSingleton<CNavStackManager> {
	#region 변수
	private List<STComponentInfo> m_oComponentInfoList = new List<STComponentInfo>();
	#endregion			// 변수
	
	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset() {
		base.Reset();
		m_oComponentInfoList.Clear();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
			CSceneManager.NavStackManager = null;
		}
	}

	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.FindIndex((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 없을 경우
		if(!m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			m_oComponentInfoList.Add(new STComponentInfo() {
				m_nID = nID, m_oComponent = a_oComponent
			});

			a_oComponent.NavStackCallback = this.OnReceiveNavStackCallback;
			a_oComponent.OnReceiveNavStackEvent(ENavStackEvent.TOP);
		}
	}

	/** 컴포넌트를 제거한다 */
	public void RemoveComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();
		int nIdx = m_oComponentInfoList.FindIndex((a_stComponentInfo) => nID == a_stComponentInfo.m_nID);

		// 컴포넌트가 존재 할 경우
		if(m_oComponentInfoList.ExIsValidIdx(nIdx)) {
			for(int i = nIdx; i < m_oComponentInfoList.Count; ++i) {
				var oComponent = m_oComponentInfoList[i].m_oComponent as CComponent;
				oComponent.NavStackCallback = null;

				// 이벤트 전송이 가능 할 경우
				if(!oComponent.IsDestroy) {
					oComponent.OnReceiveNavStackEvent(ENavStackEvent.REMOVE);
				}
			}

			m_oComponentInfoList.RemoveRange(nIdx, m_oComponentInfoList.Count - nIdx);
			
			// 컴포넌트 정보가 유효 할 경우
			if(m_oComponentInfoList.ExIsValid()) {
				(m_oComponentInfoList.Last().m_oComponent as CComponent).OnReceiveNavStackEvent(ENavStackEvent.TOP);
			}
		}
	}

	/** 내비게이션 스택 이벤트를 전송한다 */
	public void SendNavStackEvent(ENavStackEvent a_eEvent) {
		// 이벤트 전송이 가능 할 경우
		if(m_oComponentInfoList.Count > KCDefine.B_VAL_0_INT) {
			int nIdx = m_oComponentInfoList.Count - KCDefine.B_VAL_1_INT;
			(m_oComponentInfoList[nIdx].m_oComponent as CComponent).OnReceiveNavStackEvent(a_eEvent);
		}
	}

	/** 내비게이션 스택 콜백을 수신했을 경우 */
	private void OnReceiveNavStackCallback(CComponent a_oSender) {
		this.RemoveComponent(a_oSender);
	}
	#endregion			// 함수
}
