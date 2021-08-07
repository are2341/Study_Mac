using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 서브 에디터 레벨 생성 팝업
public class CSubEditorLevelCreatePopup : CEditorLevelCreatePopup {
	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
	}

	//! 초기화
	public override void Init(System.Action<CEditorLevelCreatePopup, CEditorLevelCreateInfo> a_oCallback) {
		base.Init(a_oCallback);
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected new void UpdateUIsState() {
		// Do Something
	}

	//! 에디터 레벨 생성 정보를 생성한다
	protected override CEditorLevelCreateInfo CreateEditorLevelCreateInfo() {
		var oCreateInfo = base.CreateEditorLevelCreateInfo();

		return new CSubEditorLevelCreateInfo() {
			m_nNumLevels = oCreateInfo.m_nNumLevels,

			m_stMinNumCells = oCreateInfo.m_stMinNumCells,
			m_stMaxNumCells = oCreateInfo.m_stMaxNumCells
		};
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
