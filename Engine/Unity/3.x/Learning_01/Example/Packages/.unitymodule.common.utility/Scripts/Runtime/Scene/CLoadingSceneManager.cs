using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//! 로딩 씬 관리자
public abstract class CLoadingSceneManager : CSceneManager {
	#region 프로퍼티
	public virtual float FadeInAniDuration => KCDefine.U_DEF_DURATION_SCREEN_FADE_IN_ANI;
	public override string SceneName => KCDefine.B_SCENE_N_LOADING;

#if UNITY_EDITOR
	public override int ScriptOrder => KCDefine.U_SCRIPT_O_LOADING_SCENE_MANAGER;
#endif			// #if UNITY_EDITOR
	#endregion			// 프로퍼티

	#region 클래스 프로퍼티
	public static bool IsAni { get; set; } = false;
	public static bool IsShowIndicator { get; set; } = false;

	public static string NextSceneName { get; set; } = string.Empty;
	public static LoadSceneMode LoadSceneMode { get; set; } = LoadSceneMode.Single;
	public static System.Action<AsyncOperation, bool> Callback { get; set; } = null;
	#endregion			// 클래스 프로퍼티

	#region 추상 함수
	//! 씬을 비동기 로드 중일 경우
	protected abstract void OnLoadSceneAsync(AsyncOperation a_oAsyncOperation, bool a_bIsComplete);
	#endregion			// 추상 함수

	#region 함수
	//! 초기화
	public override void Start() {
		base.Start();

		bool bIsAni = CLoadingSceneManager.IsAni;
		string oSceneName = CLoadingSceneManager.NextSceneName;

		// 인디케이터 출력 상태 일 경우
		if(this.IsRootScene && CLoadingSceneManager.IsShowIndicator) {
			CIndicatorManager.Inst.Show(true);
		}

		// 콜백이 없을 경우
		if(CLoadingSceneManager.Callback == null) {
			CSceneLoader.Inst.LoadScene(oSceneName, false, bIsAni, false, this.FadeInAniDuration, CLoadingSceneManager.LoadSceneMode);
			
			// 추가 모드 일 경우
			if(CLoadingSceneManager.LoadSceneMode == LoadSceneMode.Additive) {
				CSceneLoader.Inst.UnloadSceneAsync(KCDefine.B_SCENE_N_LOADING, null);
			}
		} else {
			CSceneLoader.Inst.LoadSceneAsync(oSceneName, this.OnChangeLoadingState, KCDefine.B_VALUE_FLT_0, false, bIsAni, false, this.FadeInAniDuration, CLoadingSceneManager.LoadSceneMode);
		}
	}

	//! 로딩 상태가 변경 되었을 경우
	private void OnChangeLoadingState(AsyncOperation a_oAsyncOperation, bool a_bIsComplete) {
		this.OnLoadSceneAsync(a_oAsyncOperation, a_bIsComplete);
		CLoadingSceneManager.Callback?.Invoke(a_oAsyncOperation, a_bIsComplete);

		// 추가 모드 일 경우
		if(a_bIsComplete && CLoadingSceneManager.LoadSceneMode == LoadSceneMode.Additive) {
			CSceneLoader.Inst.UnloadSceneAsync(KCDefine.B_SCENE_N_LOADING, null);
		}
	}
	#endregion			// 함수
}
