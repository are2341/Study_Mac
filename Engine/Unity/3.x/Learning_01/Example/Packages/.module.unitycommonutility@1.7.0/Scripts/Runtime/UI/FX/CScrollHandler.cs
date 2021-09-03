using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DanielLochner.Assets.SimpleScrollSnap;

//! 스크롤 처리자
public class CScrollHandler : CComponent, IScrollHandler {
	#region 변수
	[SerializeField] private Vector3 m_stScrollDelta = new Vector3(200.0f, 200.0f, 0.0f);

	// =====> UI <=====
	private ScrollRect m_oScrollRect = null;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		m_oScrollRect = this.transform.parent?.GetComponentInParent<ScrollRect>();

		// 터치 전달자를 설정한다
		var oTouchDispatcher = this.gameObject.ExAddComponent<CTouchDispatcher>();
		oTouchDispatcher.BeginCallback = this.OnTouchBegin;
		oTouchDispatcher.EndCallback = this.OnTouchEnd;

		// 드래그 전달자를 설정한다
		var oDragDispatcher = this.gameObject.ExAddComponent<CDragDispatcher>();
		oDragDispatcher.BeginCallback = this.OnBeginDrag;
		oDragDispatcher.DragCallback = this.OnDrag;
		oDragDispatcher.EndCallback = this.OnEndDrag;
	}

	//! 스크롤 간격을 변경한다
	public void SetScrollDelta(Vector3 a_stScrollDelta) {
		m_stScrollDelta = a_stScrollDelta;
	}

	//! 스크를 중 일 경우
	public void OnScroll(PointerEventData a_oEventData) {
		m_oScrollRect?.OnScroll(a_oEventData);
	}

	//! 스크롤 가능 할 경우
	private bool IsEnableScroll(ScrollRect a_oScrollRect, PointerEventData a_oEventData) {
		CAccess.Assert(a_oScrollRect != null && a_oEventData != null);
		var stDelta = a_oEventData.ExGetLocalDelta(this.GetComponentInParent<Canvas>().gameObject);
		
		return a_oScrollRect.vertical ? Mathf.Abs(stDelta.y) >= Mathf.Abs(m_stScrollDelta.y) : Mathf.Abs(stDelta.x) >= Mathf.Abs(m_stScrollDelta.x);
	}

	//! 터치를 시작했을 경우
	private void OnTouchBegin(CTouchDispatcher a_oSender, PointerEventData a_oEventData) {
		var oPageView = m_oScrollRect?.GetComponent<SimpleScrollSnap>();
		oPageView?.OnPointerDown(a_oEventData);
	}

	//! 터치를 종료했을 경우
	private void OnTouchEnd(CTouchDispatcher a_oSender, PointerEventData a_oEventData) {
		var oPageView = m_oScrollRect?.GetComponent<SimpleScrollSnap>();
		oPageView?.OnPointerUp(a_oEventData);
	}

	//! 드래그를 시작했을 경우
	private void OnBeginDrag(CDragDispatcher a_oSender, PointerEventData a_oEventData) {
		m_oScrollRect?.OnBeginDrag(a_oEventData);

		var oPageView = m_oScrollRect?.GetComponent<SimpleScrollSnap>();
		oPageView?.OnBeginDrag(a_oEventData);
	}

	//! 드래그 중 일 경우
	private void OnDrag(CDragDispatcher a_oSender, PointerEventData a_oEventData) {
		// 스크롤 가능 할 경우
		if(m_oScrollRect != null && this.IsEnableScroll(m_oScrollRect, a_oEventData)) {
			m_oScrollRect.OnDrag(a_oEventData);

			var oPageView = m_oScrollRect.GetComponent<SimpleScrollSnap>();
			oPageView?.OnDrag(a_oEventData);
		}
	}

	//! 드래그를 종료했을 경우
	private void OnEndDrag(CDragDispatcher a_oSender, PointerEventData a_oEventData) {
		m_oScrollRect?.OnEndDrag(a_oEventData);

		var oPageView = m_oScrollRect?.GetComponent<SimpleScrollSnap>();
		oPageView?.OnEndDrag(a_oEventData);
	}
	#endregion			// 함수
}
