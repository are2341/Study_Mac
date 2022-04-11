using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 추적 설명 팝업 */
public partial class CTrackingDescPopup : CPopup {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		NEXT,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public Dictionary<ECallback, System.Action<CTrackingDescPopup>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;
	#endregion			// 변수

	#region 프로퍼티
	public override float ShowTimeScale => KCDefine.B_VAL_1_FLT;
	public override EAniType AniType => EAniType.NONE;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.IsIgnoreAni = true;
		this.IsIgnoreNavStackEvent = true;

		// 버튼을 설정한다
		this.Contents.ExFindChild(KCDefine.U_OBJ_N_CLOSE_BTN)?.SetActive(false);
		this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_NEXT_BTN).onClick.AddListener(this.OnTouchNextBtn);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchNextBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.NEXT)?.Invoke(this);
	}
	#endregion			// 함수
}
