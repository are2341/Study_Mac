using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 서브 팝업
public abstract class CSubPopup : CPopup {
	#region 프로퍼티
	public override float ShowTimeScale => KCDefine.B_VAL_0_FLT;
	public override float CloseTimeScale => KCDefine.B_VAL_1_FLT;

	public override EAniType AniType => EAniType.DROPDOWN;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
	}
	
	//! 초기화
	public override void Init() {
		base.Init();
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// Do Something
	}
	#endregion			// 함수

	#region 추가 함수

	#endregion			// 추가 함수
}
