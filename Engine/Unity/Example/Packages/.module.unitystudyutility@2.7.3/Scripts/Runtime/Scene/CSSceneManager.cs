using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace StudyScene {
	/** 스터디 씬 관리자 */
	public abstract partial class CSSceneManager : CSceneManager {
		/** 식별자 */
		private enum EKey {
			NONE,
			BACK_BTN,
			SCROLL_RECT,
			SCROLL_VIEW_CONTENTS,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		/** =====> UI <===== */
		private List<TMP_Text> m_oEmptyTextList = new List<TMP_Text>();
		private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>();
		private Dictionary<EKey, ScrollRect> m_oScrollRectDict = new Dictionary<EKey, ScrollRect>();

		/** =====> 객체 <===== */
		private Dictionary<EKey, GameObject> m_oUIsDict = new Dictionary<EKey, GameObject>();
		#endregion			// 변수

		#region 프로퍼티
		public override bool IsIgnoreTestUIs => false;
		public override bool IsIgnoreOverlayScene => false;
		public override bool IsIgnoreBGTouchResponder => false;
		
		public ScrollRect ScrollRect => m_oScrollRectDict.GetValueOrDefault(EKey.SCROLL_RECT);
		public GameObject ScrollViewContents => m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS);
		#endregion			// 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake() {
			base.Awake();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				this.AwakeSetup();
			}
		}

		/** 초기화 */
		public override void Start() {
			base.Start();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				this.StartSetup();
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
		private void AwakeSetup() {
			// 객체를 설정한다
			CFunc.SetupObjs(new List<(EKey, string, GameObject)>() {
				(EKey.SCROLL_VIEW_CONTENTS, KCDefine.U_OBJ_N_CONTENTS, this.UIs.ExFindChild(KCDefine.U_OBJ_N_SCROLL_VIEW))
			}, m_oUIsDict, false);

			// 스크롤 영역을 설정한다
			CFunc.SetupComponents(new List<(EKey, GameObject)>() {
				(EKey.SCROLL_RECT, this.UIs.ExFindChild(KCDefine.U_OBJ_N_SCROLL_VIEW))
			}, m_oScrollRectDict, false);

			// 버튼을 설정한다 {
			CFunc.SetupButtons(new List<(EKey, string, GameObject, GameObject, UnityAction)>() {
				(EKey.BACK_BTN, $"{EKey.BACK_BTN}", this.UpLeftUIs, CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_BACK_BTN), this.OnTouchBackBtn)
			}, m_oBtnDict, false);

			(m_oBtnDict.GetValueOrDefault(EKey.BACK_BTN).transform as RectTransform).pivot = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict.GetValueOrDefault(EKey.BACK_BTN).transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict.GetValueOrDefault(EKey.BACK_BTN).transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict.GetValueOrDefault(EKey.BACK_BTN).transform as RectTransform).anchoredPosition = Vector3.zero;
			// 버튼을 설정한다 }

			// 컨텐츠가 존재 할 경우
			if(m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS) != null) {
				for(int i = 0; i < KCDefine.B_UNIT_DIGITS_PER_TEN; ++i) {
					m_oEmptyTextList.Add(CFactory.CreateCloneObj<TMP_Text>(KCDefine.U_OBJ_N_GAME_OBJ, CResManager.Inst.GetRes<GameObject>(KSDefine.SS_OBJ_P_BTN), m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS)));
				}
			}
		}
		
		/** 씬을 설정한다 */
		private void StartSetup() {
			var oTexts = m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS).GetComponentsInChildren<TMP_Text>();

			for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
				m_oEmptyTextList[i].transform.SetAsLastSibling();
				m_oEmptyTextList[i].gameObject.ExSetEnableComponent<Button>(false, false);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
				m_oEmptyTextList[i].ExSetText(string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			}

			for(int i = 0; i < oTexts.Length; ++i) {
				// 타이틀 일 경우
				if(oTexts[i].name.Contains(KCDefine.B_TOKEN_TITLE)) {
					oTexts[i].color = KSDefine.MS_COLOR_TITLE;
					oTexts[i].GetComponentInChildren<Button>()?.onClick.RemoveAllListeners();
				}
			}
		}
		#endregion			// 함수
	}
}
