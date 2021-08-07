using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 서브 에디터 스크롤러 셀 뷰
public class CSubEditorScrollerCellView : CEditorScrollerCellView {
	//! 매개 변수
	public new struct STParams {
		public CEditorScrollerCellView.STParams m_stBaseParams;
	}
	
	//! 콜백 매개 변수
	public new struct STCallbackParams {
		public CEditorScrollerCellView.STCallbackParams m_stBaseCallbackParams;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
	}

	//! 초기화
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		base.Init(a_stParams.m_stBaseParams, a_stCallbackParams.m_stBaseCallbackParams);

		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;

		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected new void UpdateUIsState() {
		// Do Something
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
