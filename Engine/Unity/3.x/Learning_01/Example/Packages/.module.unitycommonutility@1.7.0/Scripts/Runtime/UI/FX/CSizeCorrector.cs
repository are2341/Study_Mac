using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 크기 보정자
public class CSizeCorrector : CComponent {
	#region 변수
	[SerializeField] private Vector3 m_stSizeRate = Vector3.one;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetSizeRate(m_stSizeRate);
	}

	//! 상태를 갱신한다
	public override void OnLateUpdate(float a_fDeltaTime) {
		base.OnLateUpdate(a_fDeltaTime);

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			var stSize = CSceneManager.CanvasSize * m_stSizeRate.ExTo2D();

			(this.transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, stSize.y);
			(this.transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stSize.x);
		}
	}

	//! 크기 비율을 변경한다
	public void SetSizeRate(Vector3 a_stRate) {
		m_stSizeRate.x = Mathf.Clamp(a_stRate.x, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
		m_stSizeRate.y = Mathf.Clamp(a_stRate.y, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
		m_stSizeRate.z = Mathf.Clamp(a_stRate.z, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
	}
	#endregion			// 함수
}
