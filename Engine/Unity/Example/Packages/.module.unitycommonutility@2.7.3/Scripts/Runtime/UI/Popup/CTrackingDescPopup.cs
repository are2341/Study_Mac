using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 추적 설명 팝업 */
public partial class CTrackingDescPopup : CPopup {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		NEXT,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public partial struct STParams {
		public Dictionary<ECallback, System.Action<CTrackingDescPopup>> m_oCallbackDict;
	}

	#region 변수

	#endregion			// 변수

	#region 프로퍼티
	public STParams Params { get; private set; }
	public override float ShowTimeScale => KCDefine.B_VAL_1_REAL;
	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.IsIgnoreAni = true;
		this.IsIgnoreNavStackEvent = true;

		// 버튼을 설정한다 {
		CFunc.SetupButtons(new List<(string, GameObject, UnityAction)>() {
			(KCDefine.U_OBJ_N_NEXT_BTN, this.Contents, this.OnTouchNextBtn)
		}, false);

		this.CloseBtn?.gameObject.SetActive(false);
		// 버튼을 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		this.Params = a_stParams;
	}

	/** 닫혔을 경우 */
	protected override void OnClose() {
		base.OnClose();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsRunning && !CSceneManager.IsAppQuit) {
			this.gameObject.ExBroadcastMsg(KCDefine.U_FUNC_N_RESET_ANI, null);
		}
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchNextBtn() {
		this.Params.m_oCallbackDict?.GetValueOrDefault(ECallback.NEXT)?.Invoke(this);
	}
	#endregion			// 함수
}
