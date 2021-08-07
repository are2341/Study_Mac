using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 잔돈 획득 팝업
public class CChangesAcquirePopup : CSubPopup {
	//! 매개 변수
	public struct STParams {
		public int m_nNumChanges;
	}

	#region 변수
	private STParams m_stParams;
	private int m_nPrevNumChanges = 0;
	#endregion			// 변수

	#region 객체
	[SerializeField] private GameObject m_oSaveUIs = null;
	[SerializeField] private GameObject m_oFullUIs = null;
	#endregion			// 객체

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = false;
	}

	//! 초기화
	public virtual void Init(STParams a_stParams) {
		base.Init();

		m_stParams = a_stParams;
		m_nPrevNumChanges = CUserInfoStorage.Inst.UserInfo.NumChanges;

		CUserInfoStorage.Inst.AddNumChanges(a_stParams.m_nNumChanges);
		CUserInfoStorage.Inst.SaveUserInfo();
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}
	
	//! UI 상태를 변경한다
	private new void UpdateUIsState() {
		m_oSaveUIs?.SetActive(m_nPrevNumChanges < KDefine.G_MAX_NUM_CHANGES);
		m_oFullUIs?.SetActive(m_nPrevNumChanges >= KDefine.G_MAX_NUM_CHANGES);
	}
	#endregion			// 함수
}
