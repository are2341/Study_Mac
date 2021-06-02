using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 위치 보정자
public class CPosCorrector : CUIsComponent {
	#region 변수
	[SerializeField] private Space m_eSpace = Space.Self;
	[SerializeField] private Vector3 m_stOffset = Vector3.zero;
	[SerializeField] private GameObject m_oTarget = null;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Start() {
		base.Start();
		this.SetupPos();
	}

	//! 간격을 변경한다
	public void SetOffset(Vector3 a_stOffset) {
		m_stOffset = a_stOffset;
		this.SetupPos();
	}

	//! 목표를 변경한다
	public void SetTarget(GameObject a_oTarget) {
		m_oTarget = a_oTarget;
		this.SetupPos();
	}

	//! 위치를 설정한다
	private void SetupPos() {
		CAccess.Assert(m_oTarget != null);

		// 월드 모드 일 경우
		if(m_eSpace == Space.World) {
			this.transform.position = m_oTarget.transform.position + m_stOffset;
		} else {
			this.transform.localPosition = m_oTarget.transform.localPosition + m_stOffset;
		}
	}
	#endregion			// 함수
}
