using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//! 토스트 팝업
public class CToastPopup : CPopup {
	#region 변수
	protected float m_fDuration = 0.0f;
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
	}

	//! 초기화
	public virtual void Init(string a_oMsg, float a_fDuration) {
		m_fDuration = a_fDuration;
		m_oContentsRootTrans.sizeDelta = CSceneManager.CanvasSize;
		
		m_oMsgText = m_oContentsRoot.ExFindComponent<Text>(KCDefine.U_OBJ_N_TOAST_P_MSG_TEXT);
		m_oMsgText.text = a_oMsg;
	}

	//! 팝업이 출력 되었을 경우
	protected override void OnShow() {
		base.OnShow();
		
		this.ExLateCallFunc(m_fDuration, (a_oSender, a_oParams) => {
			this.Close();
		}, true);
	}
	#endregion			// 함수

	#region 제네릭 클래스 함수
	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, string a_oMsg, float a_fDuration) where T : CToastPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));

		return CToastPopup.Create<T>(a_oName, a_oObjPath, a_oParent, a_oMsg, a_fDuration, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, string a_oObjPath, GameObject a_oParent, string a_oMsg, float a_fDuration, Vector3 a_stPos) where T : CToastPopup {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));

		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);
		return CToastPopup.Create<T>(a_oName, oObj, a_oParent, a_oMsg, a_fDuration, a_stPos);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, string a_oMsg, float a_fDuration) where T : CToastPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));

		return CToastPopup.Create<T>(a_oName, a_oOrigin, a_oParent, a_oMsg, a_fDuration, KCDefine.B_POS_POPUP);
	}

	//! 토스트 팝업을 생성한다
	public static T Create<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, string a_oMsg, float a_fDuration, Vector3 a_stPos) where T : CToastPopup {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		CAccess.Assert(a_fDuration.ExIsGreateEquals(KCDefine.B_VALUE_FLT_0));

		var oToastPopup = CPopup.Create<T>(a_oName, a_oOrigin, a_oParent, KCDefine.B_POS_POPUP);
		oToastPopup?.Init(a_oMsg, a_fDuration);

		return oToastPopup;
	}
	#endregion			// 제네릭 클래스 함수
}
