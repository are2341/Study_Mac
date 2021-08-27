using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 게이지 처리자
public class CGaugeHandler : CComponent {
	#region 변수
	// UI
	private Image m_oMaskImg = null;
	private Image m_oPercentImg = null;
	#endregion			// 변수

	#region 프로퍼티
	public float Percent {
		get {
			return m_oMaskImg.fillAmount;
		} set {
			m_oMaskImg.fillAmount = Mathf.Clamp(value, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
			m_oPercentImg.fillAmount = KCDefine.B_VAL_1_FLT;

			this.SetupPercent();
		}
	}
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		m_oMaskImg = this.GetComponentInChildren<Image>();
		m_oMaskImg.fillMethod = Image.FillMethod.Horizontal;

		m_oPercentImg = this.gameObject.ExFindComponent<Image>(KCDefine.U_OBJ_N_PERCENT_IMG);
		m_oPercentImg.rectTransform.pivot = (m_oMaskImg.fillOrigin <= KCDefine.B_VAL_0_INT) ? KCDefine.B_ANCHOR_MID_RIGHT : KCDefine.B_ANCHOR_MID_LEFT;
		m_oPercentImg.rectTransform.anchorMin = (m_oMaskImg.fillOrigin <= KCDefine.B_VAL_0_INT) ? KCDefine.B_ANCHOR_MID_LEFT : KCDefine.B_ANCHOR_MID_RIGHT;
		m_oPercentImg.rectTransform.anchorMax = (m_oMaskImg.fillOrigin <= KCDefine.B_VAL_0_INT) ? KCDefine.B_ANCHOR_MID_LEFT : KCDefine.B_ANCHOR_MID_RIGHT;
		m_oPercentImg.rectTransform.sizeDelta = m_oMaskImg.rectTransform.rect.size;

		var oMask = m_oMaskImg.gameObject.ExAddComponent<Mask>();
		oMask.showMaskGraphic = false;
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetupPercent();
	}

	//! 퍼센트를 설정한다
	private void SetupPercent() {
		float fPercent = (m_oMaskImg.fillOrigin <= KCDefine.B_VAL_0_INT) ? m_oMaskImg.fillAmount : KCDefine.B_VAL_1_FLT - m_oMaskImg.fillAmount;
		m_oPercentImg.gameObject.ExSetAnchorPosX(m_oMaskImg.rectTransform.rect.width * fPercent);
	}
	#endregion			// 함수
}
