using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
/** 설정 팝업 */
public partial class CSettingsPopup : CSubPopup {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		BG_SND_BTN,
		FX_SNDS_BTN,
		VIBRATE_BTN,
		NOTI_BTN,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	/** =====> UI <===== */
	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>();
	#endregion			// 변수

	#region 함수
	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}
	
	/** 배경음 버튼을 눌렀을 경우 */
	private void OnTouchBGSndBtn() {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		CCommonGameInfoStorage.Inst.GameInfo.IsMuteBGSnd = !CCommonGameInfoStorage.Inst.GameInfo.IsMuteBGSnd;
		CCommonGameInfoStorage.Inst.SaveGameInfo();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE

		this.UpdateUIsState();
	}

	/** 효과음 버튼을 눌렀을 경우 */
	private void OnTouchFXSndsBtn() {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		CCommonGameInfoStorage.Inst.GameInfo.IsMuteFXSnds = !CCommonGameInfoStorage.Inst.GameInfo.IsMuteFXSnds;
		CCommonGameInfoStorage.Inst.SaveGameInfo();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE

		this.UpdateUIsState();
	}

	/** 진동 버튼을 눌렀을 경우 */
	private void OnTouchVibrateBtn() {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		CCommonGameInfoStorage.Inst.GameInfo.IsDisableVibrate = !CCommonGameInfoStorage.Inst.GameInfo.IsDisableVibrate;
		CCommonGameInfoStorage.Inst.SaveGameInfo();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE

		this.UpdateUIsState();
		CSndManager.Inst.Vibrate(KCDefine.U_DURATION_HEAVY_VIBRATE);
	}

	/** 알림 버튼을 눌렀을 경우 */
	private void OnTouchNotiBtn() {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
		CCommonGameInfoStorage.Inst.GameInfo.IsDisableNoti = !CCommonGameInfoStorage.Inst.GameInfo.IsDisableNoti;
		CCommonGameInfoStorage.Inst.SaveGameInfo();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
		
		this.UpdateUIsState();
	}

	/** 평가 버튼을 눌렀을 경우 */
	private void OnTouchReviewBtn() {
		CUnityMsgSender.Inst.SendShowReviewMsg();
	}

	/** 지원 버튼을 눌렀을 경우 */
	private void OnTouchSupportsBtn() {
		CUnityMsgSender.Inst.SendMailMsg(CProjInfoTable.Inst.CompanyInfo.m_oSupportsMail, string.Empty, string.Empty);
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE