using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 게이지 처리자 */
public class CGaugeHandler : CComponent {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		MASK_IMG,
		PERCENT_IMG,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	/** =====> UI <===== */
	private Dictionary<EKey, Image> m_oImgDict = new Dictionary<EKey, Image>() {
		[EKey.MASK_IMG] = null,
		[EKey.PERCENT_IMG] = null
	};
	#endregion			// 변수

	#region 프로퍼티
	public float Percent {
		get {
			return m_oImgDict[EKey.MASK_IMG].fillAmount;
		} set {
			m_oImgDict[EKey.MASK_IMG].fillAmount = Mathf.Clamp01(value);
			m_oImgDict[EKey.PERCENT_IMG].fillAmount = KCDefine.B_VAL_1_FLT;

			this.SetupPercent();
		}
	}
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oImgDict[EKey.MASK_IMG] = this.GetComponentInChildren<Image>();
		m_oImgDict[EKey.MASK_IMG].fillMethod = Image.FillMethod.Horizontal;
		m_oImgDict[EKey.MASK_IMG].gameObject.ExAddComponent<Mask>().showMaskGraphic = false;

		m_oImgDict[EKey.PERCENT_IMG] = this.gameObject.ExFindComponent<Image>(KCDefine.U_OBJ_N_PERCENT_IMG);
		m_oImgDict[EKey.PERCENT_IMG].rectTransform.pivot = (m_oImgDict[EKey.MASK_IMG].fillOrigin <= (int)EFillOrigin._1) ? KCDefine.B_ANCHOR_MID_RIGHT : KCDefine.B_ANCHOR_MID_LEFT;
		m_oImgDict[EKey.PERCENT_IMG].rectTransform.anchorMin = (m_oImgDict[EKey.MASK_IMG].fillOrigin <= (int)EFillOrigin._1) ? KCDefine.B_ANCHOR_MID_LEFT : KCDefine.B_ANCHOR_MID_RIGHT;
		m_oImgDict[EKey.PERCENT_IMG].rectTransform.anchorMax = (m_oImgDict[EKey.MASK_IMG].fillOrigin <= (int)EFillOrigin._1) ? KCDefine.B_ANCHOR_MID_LEFT : KCDefine.B_ANCHOR_MID_RIGHT;
		m_oImgDict[EKey.PERCENT_IMG].rectTransform.sizeDelta = m_oImgDict[EKey.MASK_IMG].rectTransform.rect.size;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.SetupPercent();
	}

	/** 퍼센트를 설정한다 */
	private void SetupPercent() {
		float fPercent = (m_oImgDict[EKey.MASK_IMG].fillOrigin <= (int)EFillOrigin._1) ? m_oImgDict[EKey.MASK_IMG].fillAmount : KCDefine.B_VAL_1_FLT - m_oImgDict[EKey.MASK_IMG].fillAmount;
		m_oImgDict[EKey.PERCENT_IMG].gameObject.ExSetAnchorPosX(m_oImgDict[EKey.MASK_IMG].rectTransform.rect.width * fPercent);
	}
	#endregion			// 함수
}
