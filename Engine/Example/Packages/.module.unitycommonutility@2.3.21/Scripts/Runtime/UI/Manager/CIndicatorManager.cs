using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/** 인디케이터 관리자 */
public partial class CIndicatorManager : CSingleton<CIndicatorManager> {
	#region 변수
	private int m_nRefCount = 0;
	private Tween m_oIndicatorAni = null;
	#endregion         // 변수               

	#region 함수
	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
				m_oIndicatorAni?.Kill();
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CIndicatorManager.OnDestroy Exception: {oException.Message}");
		}
	}

	/** 인디케이터를 출력한다 */
	public void Show(bool a_bIsResetRefCount = false, bool a_bIsShowBlindUIs = true) {
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_1_INT : Mathf.Clamp(m_nRefCount + KCDefine.B_VAL_1_INT, KCDefine.B_VAL_0_INT, int.MaxValue);

		// 인디케이터 시작이 가능 할 경우
		if(m_nRefCount > KCDefine.B_VAL_0_INT) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(true);
#endif			// #if UNITY_IOS || UNITY_ANDROID

			// 블라인드 UI 출력 모드 일 경우
			if(a_bIsShowBlindUIs) {
#if (UNITY_EDITOR || UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
				CLocalizeInfoTable.Inst.TryGetFontSetInfo(string.Empty, SystemLanguage.English, EFontSet._1, out STFontSetInfo stFontSetInfo);
				var oTouchResponder = CSceneManager.ShowTouchResponder(CSceneManager.ScreenTopmostUIs ?? CSceneManager.ActiveSceneTopmostUIs, KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_INDICATOR_BG, null, true);

				var oIndicatorImg = CFactory.CreateCloneObj<Image>(KCDefine.U_OBJ_N_IMG, CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_IMG), oTouchResponder);
				oIndicatorImg.ExReset();
				oIndicatorImg.sprite = CResManager.Inst.GetRes<Sprite>(KCDefine.U_IMG_P_INDICATOR);
				oIndicatorImg.rectTransform.sizeDelta = oIndicatorImg.sprite.rect.size;

				CAccess.AssignVal(ref m_oIndicatorAni, oIndicatorImg.transform.DOLocalRotate(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, -KCDefine.B_ANGLE_360_DEG), KCDefine.B_VAL_1_FLT, RotateMode.LocalAxisAdd).SetAutoKill().SetEase(KCDefine.U_EASE_LINEAR).SetLoops(KCDefine.B_TIMES_INT_INFINITE).SetUpdate(true));
#else
				CSceneManager.ShowTouchResponder(CSceneManager.ScreenTopmostUIs ?? CSceneManager.ActiveSceneTopmostUIs, KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_INDICATOR_BG, null, true);
#endif			// #if (UNITY_EDITOR || UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
			}
		}
	}

	/** 인디케이터를 닫는다 */
	public void Close(bool a_bIsResetRefCount = false) {
		m_nRefCount = a_bIsResetRefCount ? KCDefine.B_VAL_0_INT : Mathf.Clamp(m_nRefCount - KCDefine.B_VAL_1_INT, KCDefine.B_VAL_0_INT, int.MaxValue);

		// 인디케이터 정지가 가능 할 경우
		if(m_nRefCount <= KCDefine.B_VAL_0_INT) {
#if UNITY_IOS || UNITY_ANDROID
			CUnityMsgSender.Inst.SendIndicatorMsg(false);
#endif			// #if UNITY_IOS || UNITY_ANDROID

#if (UNITY_EDITOR || UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
			m_oIndicatorAni?.Kill();
#endif			// #if (UNITY_EDITOR || UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)

			CSceneManager.CloseTouchResponder(KCDefine.U_OBJ_N_INDICATOR_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
		}
	}
	#endregion			// 함수
}
