﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//! 씬 로더
public class CSceneLoader : CSingleton<CSceneLoader> {
	#region 함수
	//! 씬을 로드한다
	public void LoadScene(string a_oName, bool a_bIsShowIndicator = false, bool a_bIsAni = true, bool a_bIsUseLoadingScene = false, float a_fDuration = KCDefine.U_DEF_DURATION_SCREEN_FADE_IN_ANI, LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single) {
		// 인디케이터 출력 모드 일 경우
		if(a_bIsShowIndicator) {
			CIndicatorManager.Inst.Show(true);
		}

		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && a_bIsAni) {
			CSceneManager.RootSceneManager.StartScreenFadeInAni((a_oTouchResponder) => {
				CLoadingSceneManager.IsAni = true;
				this.DoLoadScene(a_oName, a_bIsShowIndicator, a_bIsUseLoadingScene, a_eLoadSceneMode);
			}, a_fDuration);
		} else {
			CLoadingSceneManager.IsAni = false;
			this.DoLoadScene(a_oName, a_bIsShowIndicator, a_bIsUseLoadingScene, a_eLoadSceneMode);
		}
	}

	//! 씬을 로드한다
	public void LoadSceneAsync(string a_oName, System.Action<AsyncOperation, bool> a_oCallback, float a_fDelay = KCDefine.B_VALUE_FLT_0, bool a_bIsShowIndicator = false, bool a_bIsAni = true, bool a_bIsUseLoadingScene = true, float a_fDuration = KCDefine.U_DEF_DURATION_SCREEN_FADE_IN_ANI, LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single) {
		// 인디케이터 출력 모드 일 경우
		if(a_bIsShowIndicator) {
			CIndicatorManager.Inst.Show(true);
		}

		var oEnumerator = this.DoLoadSceneAsync(a_oName, a_fDelay, a_bIsShowIndicator, a_bIsAni, a_bIsUseLoadingScene, a_fDuration, a_eLoadSceneMode, a_oCallback);
		StartCoroutine(oEnumerator);
	}

	//! 추가 씬을 로드한다
	public void LoadAdditiveScene(string a_oName, bool a_bIsShowIndicator = false) {
		CSceneLoader.Inst.LoadScene(a_oName, a_bIsShowIndicator, false, false, KCDefine.B_VALUE_FLT_0, LoadSceneMode.Additive);
	}

	//! 추가 씬을 로드한다
	public void LoadAdditiveScenes(string[] a_oNames, bool a_bIsShowIndicator = false) {
		for(int i = 0; i < a_oNames.Length; ++i) {
			this.LoadAdditiveScene(a_oNames[i], a_bIsShowIndicator);
		}
	}

	//! 추가 씬을 로드한다
	public void LoadAdditiveScenes(List<string> a_oNameList, bool a_bIsShowIndicator = false) {
		for(int i = 0; i < a_oNameList.Count; ++i) {
			this.LoadAdditiveScene(a_oNameList[i], a_bIsShowIndicator);
		}
	}

	//! 씬을 제거한다
	public void UnloadSceneAsync(string a_oName, System.Action<AsyncOperation, bool> a_oCallback, float a_fDelay = KCDefine.B_VALUE_FLT_0, bool a_bIsShowIndicator = false) {
		// 인디케이터 출력 모드 일 경우
		if(a_bIsShowIndicator) {
			CIndicatorManager.Inst.Show(true);
		}

		StartCoroutine(this.DoUnloadSceneAsync(a_oName, a_fDelay, a_oCallback));
	}

	//! 로딩 씬을 설정한다
	private void SetupLoadingScene(string a_oName, bool a_bIsShowIndicator, LoadSceneMode a_eLoadSceneMode, System.Action<AsyncOperation, bool> a_oCallback) {
		CLoadingSceneManager.IsShowIndicator = a_bIsShowIndicator;
		CLoadingSceneManager.NextSceneName = a_oName;
		CLoadingSceneManager.LoadSceneMode = a_eLoadSceneMode;
		CLoadingSceneManager.Callback = a_oCallback;
	}

	//! 씬을 로드한다
	private void DoLoadScene(string a_oName, bool a_bIsShowIndicator, bool a_bIsUseLoadingScene, LoadSceneMode a_eLoadSceneMode) {
		// 로딩 씬이 필요 없을 경우
		if(!a_bIsUseLoadingScene) {
			SceneManager.LoadScene(a_oName, a_eLoadSceneMode);
		} else {
			this.SetupLoadingScene(a_oName, a_bIsShowIndicator, a_eLoadSceneMode, null);
			SceneManager.LoadScene(KCDefine.B_SCENE_N_LOADING, a_eLoadSceneMode);
		}
	}

	//! 씬을 로드한다
	private IEnumerator DoLoadSceneAsync(string a_oName, float a_fDelay, bool a_bIsShowIndicator, bool a_bIsAni, bool a_bIsUseLoadingScene, float a_fDuration, LoadSceneMode a_eLoadSceneMode, System.Action<AsyncOperation, bool> a_oCallback) {
		yield return CFactory.CreateWaitForSeconds(a_fDelay);
		
		// 로딩 씬이 필요 없을 경우
		if(!a_bIsUseLoadingScene) {
			bool bIsActiveScene = false;

			var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_eLoadSceneMode);
			oAsyncOperation.allowSceneActivation = false;

			yield return CTaskManager.Inst.WaitAsyncOperation(oAsyncOperation, (a_oAsyncOperation, a_bIsComplete) => {
				a_oCallback?.Invoke(a_oAsyncOperation, a_bIsComplete);
				bool bIsCompleteLoading = a_oAsyncOperation.progress.ExIsGreateEquals(KCDefine.U_MAX_PERCENT_ASYNC_OPERATION);

				// 비동기 로딩이 완료 되었을 경우
				if(!bIsActiveScene && bIsCompleteLoading) {
					bIsActiveScene = true;

					// 애니메이션 모드 일 경우
					if(!this.IsIgnoreAni && a_bIsAni) {
						CSceneManager.RootSceneManager.StartScreenFadeInAni((a_oTouchResponder) => {
							a_oAsyncOperation.allowSceneActivation = true;
						}, a_fDuration);
					} else {
						a_oAsyncOperation.allowSceneActivation = true;
					}
				}
			});
		} else {
			CLoadingSceneManager.IsAni = a_bIsAni;
			this.SetupLoadingScene(a_oName, a_bIsShowIndicator, a_eLoadSceneMode, a_oCallback);

			// 애니메이션 모드 일 경우
			if(!this.IsIgnoreAni && a_bIsAni) {
				CSceneManager.RootSceneManager.StartScreenFadeInAni((a_oTouchResponder) => {
					SceneManager.LoadScene(KCDefine.B_SCENE_N_LOADING, a_eLoadSceneMode);
				}, a_fDuration);
			} else {
				SceneManager.LoadScene(KCDefine.B_SCENE_N_LOADING, a_eLoadSceneMode);
			}
		}
	}

	//! 씬을 제거한다
	private IEnumerator DoUnloadSceneAsync(string a_oName, float a_fDelay, System.Action<AsyncOperation, bool> a_oCallback) {
		yield return CFactory.CreateWaitForSeconds(a_fDelay);
		var oAsyncOperation = SceneManager.UnloadSceneAsync(a_oName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
		
		yield return CTaskManager.Inst.WaitAsyncOperation(oAsyncOperation, a_oCallback);
	}
	#endregion			// 함수
}
