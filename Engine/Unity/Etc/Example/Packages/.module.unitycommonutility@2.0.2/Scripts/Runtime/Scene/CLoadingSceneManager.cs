using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** 로딩 씬 관리자 */
public abstract class CLoadingSceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KCDefine.B_SCENE_N_LOADING;

#if UNITY_EDITOR
	public override int ScriptOrder => KCDefine.U_SCRIPT_O_LOADING_SCENE_MANAGER;
#endif			// #if UNITY_EDITOR
	#endregion			// 프로퍼티

	#region 클래스 프로퍼티
	public static bool IsAni { get; set; } = false;
	public static bool IsAsync { get; set; } = false;
	public static float Duration { get; set; } = 0.0f;
	public static string NextSceneName { get; set; } = string.Empty;
	#endregion			// 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Start() {
		base.Start();

		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			// 비동기 모드 일 경우
			if(CLoadingSceneManager.IsAsync) {
				CSceneLoader.Inst.LoadSceneAsync(CLoadingSceneManager.NextSceneName, this.OnUpdateAsyncSceneLoadingState, KCDefine.B_VAL_0_FLT, CLoadingSceneManager.IsAni, CLoadingSceneManager.Duration);
			} else {
				CSceneLoader.Inst.LoadScene(CLoadingSceneManager.NextSceneName, CLoadingSceneManager.IsAni, CLoadingSceneManager.Duration);
			}
		}
	}
	#endregion			// 함수
}
