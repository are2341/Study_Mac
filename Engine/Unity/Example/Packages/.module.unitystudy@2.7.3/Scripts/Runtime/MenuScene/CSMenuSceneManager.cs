using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

#if STUDY_MODULE_ENABLE
#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

namespace MenuScene {
	/** 스터디 메뉴 씬 관리자 */
	public partial class CSMenuSceneManager : CSceneManager {
		/** 식별자 */
		private enum EKey {
			NONE,
			SCROLL_RECT,
			SCROLL_VIEW_CONTENTS,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		/** =====> UI <===== */
		private List<TMP_Text> m_oEmptyTextList = new List<TMP_Text>();
		private Dictionary<EKey, ScrollRect> m_oScrollRectDict = new Dictionary<EKey, ScrollRect>();

		/** =====> 객체 <===== */
		private Dictionary<EKey, GameObject> m_oUIsDict = new Dictionary<EKey, GameObject>();
		#endregion			// 변수

		#region 클래스 변수
		private static Vector3 m_stPrevScrollViewContentsPos = Vector3.up;
		#endregion			// 클래스 변수

		#region 프로퍼티
		public override bool IsIgnoreTestUIs => false;
		public override bool IsIgnoreOverlayScene => false;
		public override string SceneName => KCDefine.B_SCENE_N_MENU;

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

		/** 상태를 갱신한다 */
		public override void OnUpdate(float a_fDeltaTime) {
			base.OnUpdate(a_fDeltaTime);

			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAppRunning) {
#if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
#if INPUT_SYSTEM_MODULE_ENABLE
				bool bIsEditorKeyDown = Keyboard.current.leftShiftKey.isPressed && Keyboard.current.eKey.wasPressedThisFrame;
#else
				bool bIsEditorKeyDown = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				// 에디터 키를 눌렀을 경우
				if(bIsEditorKeyDown) {
					CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_LEVEL_EDITOR);
				}
#endif			// #if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
			}
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

			for(int i = KSDefine.MS_DEF_SCENE_NAMES.Count; i < SceneManager.sceneCountInBuildSettings; ++i) {
				string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);
				string oSceneName = Path.GetFileNameWithoutExtension(oScenePath);

				// 텍스트를 설정한다 {
				var oText = CFactory.CreateCloneObj<TMP_Text>(string.Format(KSDefine.MS_OBJ_N_FMT_TEXT, i), CResManager.Inst.GetRes<GameObject>(KSDefine.MS_OBJ_P_BTN), m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS));
				oText.color = oSceneName.Contains(KCDefine.B_TOKEN_TITLE) ? KSDefine.MS_COLOR_TITLE : oText.color;

#if NEWTON_SOFT_JSON_MODULE_ENABLE
				oText.ExSetText(oSceneName, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
				// 텍스트를 설정한다 }
				
				// 레벨 에디터 씬 일 경우
				if(oSceneName.Equals(KCDefine.B_SCENE_N_LEVEL_EDITOR)) {
					m_oEmptyTextList.Add(oText);
				}

				// 타이틀, 테스트, 레벨 에디터 씬 일 경우
				if(m_oEmptyTextList.Contains(oText) || oSceneName.Contains(KCDefine.B_TOKEN_TITLE)) {
					oText.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
				} else {
					int nIdx = i;
					oText.GetComponentInChildren<Button>().onClick.AddListener(() => this.OnTouchBtn(nIdx));
				}
			}

			for(int i = 0; i < KCDefine.B_UNIT_DIGITS_PER_TEN; ++i) {
				m_oEmptyTextList.Add(CFactory.CreateCloneObj<TMP_Text>(KCDefine.U_OBJ_N_GAME_OBJ, CResManager.Inst.GetRes<GameObject>(KSDefine.MS_OBJ_P_BTN), m_oUIsDict.GetValueOrDefault(EKey.SCROLL_VIEW_CONTENTS)));
			}
		}

		/** 씬을 설정한다 */
		private void StartSetup() {
			for(int i = 0; i < m_oEmptyTextList.Count; ++i) {
				m_oEmptyTextList[i].transform.SetAsLastSibling();
				m_oEmptyTextList[i].gameObject.ExSetEnableComponent<Button>(false, false);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
				m_oEmptyTextList[i].ExSetText(string.Empty, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet._1));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			}

			this.ExLateCallFunc((a_oSender) => m_oScrollRectDict.GetValueOrDefault(EKey.SCROLL_RECT).normalizedPosition = CSMenuSceneManager.m_stPrevScrollViewContentsPos);
		}
		
		/** 버튼을 눌렀을 경우 */
		private void OnTouchBtn(int a_nIdx) {
			CSMenuSceneManager.m_stPrevScrollViewContentsPos = m_oScrollRectDict.GetValueOrDefault(EKey.SCROLL_RECT).normalizedPosition;
			CSceneLoader.Inst.LoadScene(SceneUtility.GetScenePathByBuildIndex(a_nIdx));
		}
		#endregion			// 함수
	}
}
#endif			// #if STUDY_MODULE_ENABLE
