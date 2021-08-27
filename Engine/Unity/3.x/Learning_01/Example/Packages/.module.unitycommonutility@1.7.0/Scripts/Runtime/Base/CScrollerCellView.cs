using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

//! 스크롤러 셀 뷰
public abstract class CScrollerCellView : EnhancedScrollerCellView {
	//! 매개 변수
	public struct STParams {
		public EnhancedScroller m_oScroller;
	}

	//! 콜백 매개 변수
	public struct STCallbackParams {
		public System.Action<CScrollerCellView, long> m_oSelCallback;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;
	#endregion			// 변수

	#region 프로퍼티
	public EnhancedScroller Scroller => m_stParams.m_oScroller;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public virtual void Awake() {
		// 컴포넌트를 설정한다
		this.gameObject.ExEnumerateComponents<CComponent>((a_oComponent) => {
			a_oComponent.IsIgnoreEnableEvent = true;
			return true;
		});
	}

	//! 초기화
	public virtual void Start() {
		// Do Something
	}

	//! 초기화
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;
	}
	#endregion			// 함수
}
