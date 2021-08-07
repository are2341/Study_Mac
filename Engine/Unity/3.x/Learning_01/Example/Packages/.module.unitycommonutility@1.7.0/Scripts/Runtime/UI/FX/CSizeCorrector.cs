using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 크기 보정자
public class CSizeCorrector : CUIsComponent {
	#region 변수
	[SerializeField] private float m_fSizeRate = 1.0f;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Start() {
		base.Start();
		this.SetupSize();
	}

	//! 크기를 리셋한다
	public virtual void ResetSize() {
		this.SetupSize();
	}

	//! 크기 비율을 변경한다
	public void SetSizeRate(float a_fRate) {
		m_fSizeRate = Mathf.Clamp(a_fRate, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
		this.SetupSize();
	}

	//! 크기를 설정한다
	private void SetupSize() {
		var stSize = CSceneManager.CanvasSize * m_fSizeRate;

		var oTrans = this.transform as RectTransform;
		oTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, stSize.y);
		oTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stSize.x);
	}
	#endregion			// 함수
}
