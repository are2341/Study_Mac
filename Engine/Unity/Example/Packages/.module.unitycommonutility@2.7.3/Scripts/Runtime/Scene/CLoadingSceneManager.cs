using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace LoadingScene {
	/** 로딩 씬 관리자 */
	public abstract partial class CLoadingSceneManager : CSceneManager {
		/** 식별자 */
		private enum EKey {
			NONE = -1,
			IS_ANI,
			IS_ASYNC,
			DURATION,
			NEXT_SCENE_NANE,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		private static Dictionary<EKey, bool> m_oBoolDict = new Dictionary<EKey, bool>();
		private static Dictionary<EKey, float> m_oRealDict = new Dictionary<EKey, float>();
		private static Dictionary<EKey, string> m_oStrDict = new Dictionary<EKey, string>();
		#endregion			// 변수

		#region 프로퍼티
		public override string SceneName => KCDefine.B_SCENE_N_LOADING;

#if UNITY_EDITOR
		public override int ScriptOrder => KCDefine.U_SCRIPT_O_LOADING_SCENE_MANAGER;
#endif			// #if UNITY_EDITOR
		#endregion			// 프로퍼티

		#region 클래스 프로퍼티
		public static bool IsAni { get { return CLoadingSceneManager.m_oBoolDict.GetValueOrDefault(EKey.IS_ANI); } set { CLoadingSceneManager.m_oBoolDict.ExReplaceVal(EKey.IS_ANI, value); } }
		public static bool IsAsync { get { return CLoadingSceneManager.m_oBoolDict.GetValueOrDefault(EKey.IS_ASYNC); } set { CLoadingSceneManager.m_oBoolDict.ExReplaceVal(EKey.IS_ASYNC, value); } }
		public static float Duration { get { return CLoadingSceneManager.m_oRealDict.GetValueOrDefault(EKey.DURATION); } set { CLoadingSceneManager.m_oRealDict.ExReplaceVal(EKey.DURATION, value); } }
		public static string NextSceneName { get { return CLoadingSceneManager.m_oStrDict.GetValueOrDefault(EKey.NEXT_SCENE_NANE, string.Empty); } set { CLoadingSceneManager.m_oStrDict.ExReplaceVal(EKey.NEXT_SCENE_NANE, value); } }
		#endregion			// 클래스 프로퍼티
		
		#region 함수
		/** 초기화 */
		public override void Start() {
			base.Start();

			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				// 비동기 모드 일 경우
				if(CLoadingSceneManager.m_oBoolDict.GetValueOrDefault(EKey.IS_ASYNC)) {
					CSceneLoader.Inst.LoadSceneAsync(CLoadingSceneManager.m_oStrDict.GetValueOrDefault(EKey.NEXT_SCENE_NANE, string.Empty), this.OnUpdateAsyncSceneLoadingState, KCDefine.B_VAL_0_REAL, CLoadingSceneManager.m_oBoolDict.GetValueOrDefault(EKey.IS_ANI), CLoadingSceneManager.m_oRealDict.GetValueOrDefault(EKey.DURATION));
				} else {
					CSceneLoader.Inst.LoadScene(CLoadingSceneManager.m_oStrDict.GetValueOrDefault(EKey.NEXT_SCENE_NANE, string.Empty), CLoadingSceneManager.m_oBoolDict.GetValueOrDefault(EKey.IS_ANI), CLoadingSceneManager.m_oRealDict.GetValueOrDefault(EKey.DURATION));
				}
			}
		}
		#endregion			// 함수
	}
}
