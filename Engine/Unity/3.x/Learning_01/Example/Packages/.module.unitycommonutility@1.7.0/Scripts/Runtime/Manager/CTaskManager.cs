using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
#if GOOGLE_REVIEW_ENABLE
using Google.Play.Review;
#endif			// #if GOOGLE_REVIEW_ENABLE

#if GOOGLE_UPDATE_ENABLE
using Google.Play.AppUpdate;
#endif			//#if GOOGLE_UPDATE_ENABLE
#endif			// #if UNITY_ANDROID

#if FIREBASE_MODULE_ENABLE
using Firebase.Extensions;
#endif			// #if FIREBASE_MODULE_ENABLE

//! 태스크 관리자
public class CTaskManager : CSingleton<CTaskManager> {
	#region 변수
	private Dictionary<int, STTaskInfo> m_oTaskInfoDict = new Dictionary<int, STTaskInfo>();
	#endregion			// 변수

	#region 함수
	//! 비동기 작업을 대기한다
	public void WaitAsyncTask(Task a_oTask, System.Action<Task> a_oCallback) {
		CAccess.Assert(a_oTask != null && !m_oTaskInfoDict.ContainsKey(a_oTask.Id));

#if FIREBASE_MODULE_ENABLE
		m_oTaskInfoDict.ExAddVal(a_oTask.Id, new STTaskInfo() {
			m_oTask = a_oTask,
			m_oCallback = a_oCallback
		});
		
		a_oTask.ContinueWithOnMainThread(this.OnCompleteAsyncTask, CancellationToken.None);
#else
		StartCoroutine(this.DoWaitAsyncTask(a_oTask, a_oCallback));
#endif			// #if FIREBASE_MODULE_ENABLE
	}

	//! 비동기 작업을 대기한다
	public IEnumerator WaitAsyncOperation(AsyncOperation a_oAsyncOperation, System.Action<AsyncOperation, bool> a_oCallback, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oAsyncOperation != null);

		do {
			yield return CFactory.CreateWaitForSecs(KCDefine.B_DELTA_T_ASYNC_OPERATION, a_bIsRealtime);
			a_oCallback?.Invoke(a_oAsyncOperation, false);
		} while(!a_oAsyncOperation.isDone);

		yield return CFactory.CreateWaitForSecs(KCDefine.B_DELTA_T_ASYNC_OPERATION, a_bIsRealtime);
		a_oCallback?.Invoke(a_oAsyncOperation, true);
	}

	//! 비동기 작업이 완료 되었을 경우
	private void OnCompleteAsyncTask(Task a_oTask) {
		// 비동기 작업 정보가 존재 할 경우
		if(m_oTaskInfoDict.TryGetValue(a_oTask.Id, out STTaskInfo stTaskInfo)) {
			m_oTaskInfoDict.ExRemoveVal(a_oTask.Id);
			stTaskInfo.m_oCallback?.Invoke(stTaskInfo.m_oTask);
		}
	}

	//! 비동기 작업을 대기한다
	private IEnumerator DoWaitAsyncTask(Task a_oTask, System.Action<Task> a_oCallback) {
		CAccess.Assert(a_oTask != null);
		float fSkipTime = KCDefine.B_VAL_0_FLT;

		do {
			yield return new WaitForEndOfFrame();
			fSkipTime += CScheduleManager.Inst.UnscaleDeltaTime;
		} while(!a_oTask.ExIsComplete() && fSkipTime.ExIsLess(KCDefine.U_TIMEOUT_ASYNC_TASK));

		yield return new WaitForEndOfFrame();
		a_oCallback?.Invoke(a_oTask);
	}
	#endregion			// 함수

	#region 제네릭 함수
	//! 비동기 작업을 대기한다
	public void WaitAsyncTask<T>(Task<T> a_oTask, System.Action<Task<T>> a_oCallback) {
		this.WaitAsyncTask(a_oTask as Task, (a_oAsyncTask) => a_oCallback?.Invoke(a_oAsyncTask as Task<T>));
	}
	#endregion			// 제네릭 함수

	#region 조건부 함수
#if UNITY_ANDROID
#if GOOGLE_REVIEW_ENABLE
	//! 평가 흐름을 대기한다
	public IEnumerator WaitReviewFlow(ReviewManager a_oReviewManager, System.Action<CTaskManager, ReviewErrorCode> a_oCallback) {
		var oReviewFlow = a_oReviewManager.RequestReviewFlow();
		CAccess.Assert(oReviewFlow != null);

		yield return oReviewFlow;

		// 스토어 평가를 지원하지 않을 경우
		if(oReviewFlow.Error != ReviewErrorCode.NoError) {
			a_oCallback?.Invoke(this, oReviewFlow.Error);
		} else {
			var oReviewInfo = oReviewFlow.GetResult();
			yield return this.DoWaitReviewFlow(a_oReviewManager, oReviewInfo, a_oCallback);
		}
	}

	//! 평가 흐름을 대기한다
	private IEnumerator DoWaitReviewFlow(ReviewManager a_oReviewManager, PlayReviewInfo a_oReviewInfo, System.Action<CTaskManager, ReviewErrorCode> a_oCallback) {
		var oReviewFlow = a_oReviewManager.LaunchReviewFlow(a_oReviewInfo);
		CAccess.Assert(oReviewFlow != null);

		yield return oReviewFlow;
		a_oCallback?.Invoke(this, oReviewFlow.Error);
	}
#endif			// #if GOOGLE_REVIEW_ENABLE

#if GOOGLE_UPDATE_ENABLE
	//! 업데이트 작업을 대기한다
	public IEnumerator WaitUpdateOperation(AppUpdateManager a_oUpdateManager, System.Action<CTaskManager, int, bool> a_oCallback) {
		var oUpdateOperation = a_oUpdateManager.GetAppUpdateInfo();
		CAccess.Assert(oUpdateOperation != null);

		yield return oUpdateOperation;

		// 업데이트 정보를 갱신했을 경우
		if(oUpdateOperation.IsSuccessful) {
			var oResult = oUpdateOperation.GetResult();
			a_oCallback?.Invoke(this, oResult.AvailableVersionCode, true);
		} else {
			a_oCallback?.Invoke(this, CProjInfoTable.Inst.ProjInfo.m_stBuildVer.m_nNum, false);
		}
	}
#endif			// #if GOOGLE_UPDATE_ENABLE
#endif			// #if UNITY_ANDROID
	#endregion			// 조건부 함수
}
