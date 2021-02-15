using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 크기 조정자
public class CResizer : CUIComponent {
	#region 변수
	[SerializeField] private float m_fSizeRate = 1.0f;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Start() {
		base.Start();
		this.SetSizeRate(m_fSizeRate);
	}

	//! 크기 비율을 변경한다
	public void SetSizeRate(float a_fSizeRate) {
		m_fSizeRate = Mathf.Clamp(m_fSizeRate, KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_1);

		var oTrans = this.transform as RectTransform;
		oTrans.sizeDelta = CSceneManager.CanvasSize * m_fSizeRate;
	}
	#endregion			// 함수
}
