using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//! 터치 사운드 재생자
public class CTouchSndPlayer : CUIsComponent, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
	#region 변수
	[SerializeField] private string m_oBeginSndPath = "Sounds/Global/G_TouchBegin";
	[SerializeField] private string m_oEndSndPath = "Sounds/Global/G_TouchEnd";
	#endregion			// 변수

	#region 프로퍼티
	public bool IsTouch { get; private set; } = false;
	#endregion			// 프로퍼티

	#region 인터페이스
	//! 영역에 들어왔을 경우
	public virtual void OnPointerEnter(PointerEventData a_oEventData) {
		this.IsTouch = true;
	}

	//! 영역에서 벗어났을 경우
	public virtual void OnPointerExit(PointerEventData a_oEventData) {
		this.IsTouch = false;
	}

	//! 터치를 시작했을 경우
	public virtual void OnPointerDown(PointerEventData a_oEventData) {
		// 터치 시작 사운드 경로가 유효 할 경우
		if(m_oBeginSndPath.ExIsValid()) {
			CSndManager.Inst.PlayFXSnd(m_oBeginSndPath);
		}

		this.IsTouch = true;
	}

	//! 터치를 종료했을 경우
	public virtual void OnPointerUp(PointerEventData a_oEventData) {
		// 터치 종료 사운드 경로가 유효 할 경우
		if(this.IsTouch && m_oEndSndPath.ExIsValid()) {
			CSndManager.Inst.PlayFXSnd(m_oEndSndPath);
		}

		this.IsTouch = false;
	}
	#endregion			// 인터페이스

	#region 함수
	//! 터치 시작 사운드 경로를 변경한다
	public void SetBeginSndPath(string a_oSndPath) {
		m_oBeginSndPath = a_oSndPath;
	}

	//! 터치 종료 사운드 경로를 변경한다
	public void SetEndSndPath(string a_oSndPath) {
		m_oEndSndPath = a_oSndPath;
	}
	#endregion			// 함수
}
