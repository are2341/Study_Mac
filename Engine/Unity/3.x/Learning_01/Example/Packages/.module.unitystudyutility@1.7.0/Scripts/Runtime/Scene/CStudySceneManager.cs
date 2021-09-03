﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 스터디 씬 관리자
public abstract class CStudySceneManager : CSceneManager {
	#region 변수
	// =====> UI <=====
	protected Text m_oEmptyText = null;
	protected Button m_oBackBtn = null;
	protected ScrollRect m_oScrollRect = null;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			// 버튼을 설정한다 {
			m_oBackBtn = CFactory.CreateCloneObj<Button>(KSDefine.SS_OBJ_N_BACK_BTN, KSDefine.SS_OBJ_P_BACK_BTN, this.SubUpUIs, KSDefine.SS_POS_BACK_BTN);
			m_oBackBtn.onClick.AddListener(this.OnTouchBackBtn);

			(m_oBackBtn.transform as RectTransform).pivot = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBackBtn.transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBackBtn.transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
			// 버튼을 설정한다 }

			// 컨텐츠를 설정한다
			m_oScrollRect = this.SubUIs.ExFindComponent<ScrollRect>(KSDefine.SS_OBJ_N_SCROLL_VIEW);
		}
	}

	//! 초기화
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			// 스크롤 영역이 존재 할 경우
			if(m_oScrollRect != null) {
				var oContents = m_oScrollRect.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);

				// 텍스트를 설정한다 {
				m_oEmptyText = CFactory.CreateCloneObj<Text>(KCDefine.U_OBJ_N_EMPTY, KSDefine.SS_OBJ_P_TEXT, oContents);
				m_oEmptyText.text = string.Empty;

				m_oEmptyText.transform.SetAsLastSibling();
				m_oEmptyText.gameObject.ExSetEnableComponent<Button>(false, false);
				// 텍스트를 설정한다 }
			}
		}	
	}

	//! 내비게이션 스택 이벤트를 수신했을 경우
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);
		
#if STUDY_MODULE_ENABLE
		// 백 키 눌림 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			this.OnTouchBackBtn();
		}
#endif			// #if STUDY_MODULE_ENABLE
	}

	//! 백 버튼을 눌렀을 경우
	private void OnTouchBackBtn() {
		CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_MENU);
	}
	#endregion			// 함수
}
