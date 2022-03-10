using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 스터디 씬 관리자 */
public abstract class CStudySceneManager : CSceneManager {
	#region 변수
	/** =====> UI <===== */
	protected Button m_oBackBtn = null;
	protected List<TMP_Text> m_oEmptyTextList = new List<TMP_Text>();

	/** =====> 객체 <===== */
	protected GameObject m_oContents = null;
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupAwake();
		}
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupStart();
			CSceneLoader.Inst.LoadAdditiveScene(KCDefine.B_SCENE_N_OVERLAY);
		}	
	}

	/** 내비게이션 스택 이벤트를 수신했을 경우 */
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);
		
#if STUDY_MODULE_ENABLE
		// 백 키 눌림 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			this.OnTouchBackBtn();
		}
#endif			// #if STUDY_MODULE_ENABLE
	}

	/** 백 버튼을 눌렀을 경우 */
	protected virtual void OnTouchBackBtn() {
		CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_MENU);
	}

	/** 씬을 설정한다 */
	private void SetupAwake() {
		// 버튼을 설정한다 {
		m_oBackBtn = CFactory.CreateCloneObj<Button>(KCDefine.U_OBJ_N_BACK_BTN, KCDefine.U_OBJ_P_G_BACK_BTN, this.UpLeftUIs);
		m_oBackBtn.onClick.AddListener(this.OnTouchBackBtn);

		(m_oBackBtn.transform as RectTransform).pivot = KCDefine.B_ANCHOR_UP_LEFT;
		(m_oBackBtn.transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
		(m_oBackBtn.transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
		(m_oBackBtn.transform as RectTransform).anchoredPosition = Vector2.zero;
		// 버튼을 설정한다 }

		// 컨텐츠를 설정한다
		var oScrollRect = this.UIsBase.ExFindComponent<ScrollRect>(KSDefine.SS_OBJ_N_SCROLL_VIEW);
		m_oContents = oScrollRect?.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);

		// 컨텐츠가 존재 할 경우
		if(m_oContents != null) {
			for(int i = 0; i < KCDefine.B_UNIT_DIGITS_PER_TEN; ++i) {
				m_oEmptyTextList.Add(CFactory.CreateCloneObj<TMP_Text>(KCDefine.U_OBJ_N_EMPTY, KSDefine.SS_OBJ_P_TEXT, m_oContents));
			}
		}
	}

	/** 씬을 설정한다 */
	private void SetupStart() {
		for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
			m_oEmptyTextList[i].transform.SetAsLastSibling();
			m_oEmptyTextList[i].gameObject.ExSetEnableComponent<Button>(false, false);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
			m_oEmptyTextList[i].ExSetText(string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
		}
	}
	#endregion			// 함수
}
