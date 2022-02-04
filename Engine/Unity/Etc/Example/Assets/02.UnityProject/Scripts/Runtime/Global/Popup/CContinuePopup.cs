using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if RUNTIME_TEMPLATES_MODULE_ENABLE
/** 이어하기 팝업 */
public class CContinuePopup : CSubPopup {
	/** 매개 변수 */
	public struct STParams {
		public int m_nContinueTimes;
		public CLevelInfo m_oLevelInfo;
	}

	/** 콜백 매개 변수 */
	public struct STCallbackParams {
		public System.Action<CContinuePopup> m_oRetryCallback;
		public System.Action<CContinuePopup> m_oContinueCallback;
		public System.Action<CContinuePopup> m_oLeaveCallback;
	}

	#region 변수
	private STParams m_stParams;
	private STCallbackParams m_stCallbackParams;

	/** =====> UI <===== */
	private TMP_Text m_oPriceText = null;
	#endregion			// 변수

	#region 추가 변수

	#endregion			// 추가 변수

	#region 프로퍼티
	public override bool IsIgnoreCloseBtn => true;
	#endregion			// 프로퍼티

	#region 추가 프로퍼티

	#endregion			// 추가 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oPriceText = m_oContents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_PRICE_TEXT);

		// 버튼을 설정한다
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_RETRY_BTN)?.onClick.AddListener(this.OnTouchRetryBtn);
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CONTINUE_BTN)?.onClick.AddListener(this.OnTouchContinueBtn);
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_LEAVE_BTN)?.onClick.AddListener(this.OnTouchLeaveBtn);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams, STCallbackParams a_stCallbackParams) {
		base.Init();

		m_stParams = a_stParams;
		m_stCallbackParams = a_stCallbackParams;
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}

	/** 닫기 버튼을 눌렀을 경우 */
	protected override void OnTouchCloseBtn() {
		base.OnTouchCloseBtn();
		this.OnTouchLeaveBtn();
	}
	
	/** UI 상태를 갱신한다 */
	private new void UpdateUIsState() {
		base.UpdateUIsState();
		
		// 텍스트를 갱신한다
		m_oPriceText?.ExSetText($"{CSaleItemInfoTable.Inst.GetSaleItemInfo(ESaleItemKinds.CONSUMABLE_CONTINUE).IntPrice}", EFontSet.A, false);
	}
	
	/** 재시도 버튼을 눌렀을 경우 */
	private void OnTouchRetryBtn() {
		m_stCallbackParams.m_oRetryCallback?.Invoke(this);
	}

	/** 이어하기 버튼을 눌렀을 경우 */
	private void OnTouchContinueBtn() {
		var stSaleItemInfo = CSaleItemInfoTable.Inst.GetSaleItemInfo(ESaleItemKinds.CONSUMABLE_CONTINUE);

		// 코인이 부족 할 경우
		if(CUserInfoStorage.Inst.UserInfo.NumCoins < stSaleItemInfo.IntPrice) {
			CSceneManager.GetSceneManager<CSubOverlaySceneManager>(KCDefine.B_SCENE_N_OVERLAY)?.ShowStorePopup();
		} else {
			Func.BuyItem(stSaleItemInfo);
			m_stCallbackParams.m_oContinueCallback?.Invoke(this);
		}
	}

	/** 나가기 버튼을 눌렀을 경우 */
	private void OnTouchLeaveBtn() {
		m_stCallbackParams.m_oLeaveCallback?.Invoke(this);
	}
	#endregion			// 함수

	#region 추가 함수

	#endregion			// 추가 함수
}
#endif			// #if RUNTIME_TEMPLATES_MODULE_ENABLE
