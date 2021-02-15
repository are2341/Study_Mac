using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! UI 컴포넌트
public abstract class CUIComponent : CComponent {
	#region 변수
	protected Selectable[] m_oSelectables = null;
	#endregion			// 변수
	
	#region 함수
	//! 초기화
	public override void Start() {
		base.Start();
		m_oSelectables = this.GetComponentsInChildren<Selectable>();
	}

	//! 활성화 되었을 경우
	public virtual void OnEnable() {
		// 상호 작용 객체가 존재 할 경우
		if(m_oSelectables.ExIsValid()) {
			for(int i = 0; i < m_oSelectables.Length; ++i) {
				m_oSelectables[i].interactable = true;
			}
		}
	}

	//! 비활성화 되었을 경우
	public virtual void OnDisable() {
		// 상호 작용 객체가 존재 할 경우
		if(m_oSelectables.ExIsValid()) {
			for(int i = 0; i < m_oSelectables.Length; ++i) {
				m_oSelectables[i].interactable = false;
			}
		}
	}
	#endregion			// 함수
}
