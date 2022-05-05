using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StudyScene {
	/** 스터디 씬 관리자 */
	public abstract partial class CStudySceneManager : CSceneManager {
		/** 식별자 */
		private enum EKey {
			NONE,
			BACK_BTN,
			SCROLL_VIEW_CONTENTS,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		/** =====> UI <===== */
		private List<TMP_Text> m_oEmptyTextList = new List<TMP_Text>();

		private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>() {
			[EKey.BACK_BTN] = null
		};

		/** =====> 객체 <===== */
		private Dictionary<EKey, GameObject> m_oUIsDict = new Dictionary<EKey, GameObject>() {
			[EKey.SCROLL_VIEW_CONTENTS] = null
		};
		#endregion			// 변수

		#region 프로퍼티
		public override bool IsIgnoreOverlayScene => false;
		protected GameObject ScrollViewContents => m_oUIsDict[EKey.SCROLL_VIEW_CONTENTS];
		#endregion			// 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake() {
			base.Awake();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				this.SetupAwake();
			}
		}

		/** 초기화 */
		public override void Start() {
			base.Start();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				this.SetupStart();
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
			m_oBtnDict[EKey.BACK_BTN] = CFactory.CreateCloneObj<Button>(KCDefine.U_OBJ_N_BACK_BTN, CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_BACK_BTN), this.UpLeftUIs);
			m_oBtnDict[EKey.BACK_BTN].onClick.AddListener(this.OnTouchBackBtn);

			(m_oBtnDict[EKey.BACK_BTN].transform as RectTransform).pivot = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict[EKey.BACK_BTN].transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict[EKey.BACK_BTN].transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
			(m_oBtnDict[EKey.BACK_BTN].transform as RectTransform).anchoredPosition = Vector3.zero;
			// 버튼을 설정한다 }

			// 컨텐츠를 설정한다 {
			var oScrollRect = this.UIsBase.ExFindComponent<ScrollRect>(KSDefine.SS_OBJ_N_SCROLL_VIEW);
			m_oUIsDict[EKey.SCROLL_VIEW_CONTENTS] = oScrollRect?.gameObject.ExFindChild(KCDefine.U_OBJ_N_CONTENTS);

			// 컨텐츠가 존재 할 경우
			if(m_oUIsDict[EKey.SCROLL_VIEW_CONTENTS] != null) {
				for(int i = 0; i < KCDefine.B_UNIT_DIGITS_PER_TEN; ++i) {
					m_oEmptyTextList.Add(CFactory.CreateCloneObj<TMP_Text>(KCDefine.U_OBJ_N_EMPTY, CResManager.Inst.GetRes<GameObject>(KSDefine.SS_OBJ_P_BTN), m_oUIsDict[EKey.SCROLL_VIEW_CONTENTS]));
				}
			}
			// 컨텐츠를 설정한다 }
		}
		
		/** 씬을 설정한다 */
		private void SetupStart() {
			for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
				m_oEmptyTextList[i].transform.SetAsLastSibling();
				m_oEmptyTextList[i].gameObject.ExSetEnableComponent<Button>(false, false);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
				m_oEmptyTextList[i].ExSetText(string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			}
		}
		#endregion			// 함수
	}
}
