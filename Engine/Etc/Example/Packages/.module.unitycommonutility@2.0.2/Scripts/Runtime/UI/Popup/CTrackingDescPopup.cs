using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 추적 설명 팝업 */
public class CTrackingDescPopup : CPopup {
	/** 콜백 매개 변수 */
	public struct STCallbackParams {
		public System.Action<CTrackingDescPopup> m_oCallback;
	}

	#region 변수
	private STCallbackParams m_stCallbackParams;
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
		m_oCloseBtn?.gameObject.SetActive(false);
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_NEXT_BTN).onClick.AddListener(this.OnTouchNextBtn);
	}

	/** 초기화 */
	public virtual void Init(STCallbackParams a_stCallbackParams) {
		base.Init();
		m_stCallbackParams = a_stCallbackParams;
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchNextBtn() {
		m_stCallbackParams.m_oCallback?.Invoke(this);
	}
	#endregion			// 함수
}
