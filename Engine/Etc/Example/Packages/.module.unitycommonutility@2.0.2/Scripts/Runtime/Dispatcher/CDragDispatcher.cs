using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/** 드래그 전달자 */
public class CDragDispatcher : CComponent, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler {
	#region 프로퍼티
	public System.Action<CDragDispatcher, PointerEventData> BeginCallback { get; set; } = null;
	public System.Action<CDragDispatcher, PointerEventData> DragCallback { get; set; } = null;
	public System.Action<CDragDispatcher, PointerEventData> EndCallback { get; set; } = null;
	public System.Action<CDragDispatcher, PointerEventData> ScrollCallback { get; set; } = null;
	#endregion			// 프로퍼티

	#region IBeginDragHandler
	/** 드래그를 시작했을 경우 */
	public virtual void OnBeginDrag(PointerEventData a_oEventData) {
		this.BeginCallback?.Invoke(this, a_oEventData);
	}
	#endregion			// IBeginDragHandler

	#region IDragHandler
	/** 드래그 중 일 경우 */
	public virtual void OnDrag(PointerEventData a_oEventData) {
		this.DragCallback?.Invoke(this, a_oEventData);
	}
	#endregion			// IDragHandler

	#region IEndDragHandler
	/** 드래그를 종료했을 경우 */
	public virtual void OnEndDrag(PointerEventData a_oEventData) {
		this.EndCallback?.Invoke(this, a_oEventData);
	}
	#endregion			// IEndDragHandler

	#region IScrollHandler
	/** 스크롤 중 일 경우 */
	public virtual void OnScroll(PointerEventData a_oEventData) {
		this.ScrollCallback?.Invoke(this, a_oEventData);
	}
	#endregion			// IScrollHandler
}
