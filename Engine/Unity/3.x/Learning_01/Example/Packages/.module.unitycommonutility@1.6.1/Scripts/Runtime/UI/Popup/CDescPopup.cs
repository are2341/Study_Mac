using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 설명 팝업
public class CDescPopup : CPopup {
	#region 변수
	private System.Action<CDescPopup> m_oCallback = null;
	#endregion			// 변수

	#region 프로퍼티
	public override bool IsIgnoreAni => true;
	public override float ShowTimeScale => KCDefine.B_VAL_1_FLT;

	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = true;

		// 버튼을 설정한다
		var oNextBtn = m_oContents.ExFindComponent<Button>(KCDefine.LSS_OBJ_N_DESC_P_NEXT_BTN);
		oNextBtn.onClick.AddListener(this.OnTouchNextBtn);
	}

	//! 초기화
	public virtual void Init(System.Action<CDescPopup> a_oCallback) {
		base.Init();
		m_oCallback = a_oCallback;
	}

	//! 확인 버튼을 눌렀을 경우
	private void OnTouchNextBtn() {
		m_oCallback?.Invoke(this);
	}
	#endregion			// 함수
}
