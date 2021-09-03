using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//! 토스트 팝업
public class CToastPopup : CPopup {
	//! 매개 변수
	public struct STParams {
		public float m_fDuration;
		public string m_oMsg;
	}
	
	#region 변수
	private STParams m_stParams;

	// =====> UI <=====
	protected Text m_oMsgText = null;
	#endregion			// 변수

	#region 프로퍼티
	public override EAniType AniType => EAniType.SCALE;
	#endregion			// 프로퍼티
	
	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = true;

		m_oMsgText = m_oContents.ExFindComponent<Text>(KCDefine.U_OBJ_N_MSG_TEXT);
	}

	//! 초기화
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;
	}

	//! 팝업 컨텐츠를 설정한다
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}

	//! UI 상태를 갱신한다
	protected void UpdateUIsState() {
		// 텍스트 상태를 갱신한다
		m_oContentsTrans.sizeDelta = CSceneManager.CanvasSize.ExTo2D();
		m_oMsgText.text = m_stParams.m_oMsg;
	}

	//! 팝업이 출력 되었을 경우
	protected override void OnShow() {
		base.OnShow();
		
		this.ExLateCallFunc((a_oSender, a_oParams) => {
			this.OnTouchCloseBtn();
		}, m_stParams.m_fDuration, true);
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams) where T : CToastPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		return CToastPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_stParams, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, STParams a_stParams, Vector3 a_stPos) where T : CToastPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);
		return CToastPopup.Create<T>(a_oName, oObj, a_oParent, a_stParams, a_stPos);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams) where T : CToastPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		return CToastPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_stParams, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, STParams a_stParams, Vector3 a_stPos) where T : CToastPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		CAccess.Assert(a_stParams.m_fDuration.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var oToastPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, KCDefine.B_POS_POPUP);
		oToastPopup?.Init(a_stParams);

		return oToastPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
