using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 튜토리얼 팝업
public class CTutorialPopup : CFocusPopup {
	//! 매개 변수
	public new struct STParams {
		public CFocusPopup.STParams m_stBaseParams;
		public ETutorialKinds m_eTutorialKinds;
	}

	//! 콜백 매개 변수
	public new struct STCallbackParams {
		public CFocusPopup.STCallbackParams m_stBaseCallbackParams;
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
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}
	
	//! UI 상태를 갱신한다
	private new void UpdateUIsState() {
		base.UpdateUIsState();
	}
	#endregion			// 함수

	#region 추가 변수

	#endregion			// 추가 변수
	
	#region 추가 프로퍼티

	#endregion			// 추가 프로퍼티

	#region 추가 함수

	#endregion			// 추가 함수
}
