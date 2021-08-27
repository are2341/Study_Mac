using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

//! 터치 비율 처리자
public class CTouchScaler : CComponent, IPointerDownHandler, IPointerUpHandler {
	#region 변수
	[SerializeField] private float m_fDuration = KCDefine.U_DURATION_ANI;
	[SerializeField] private float m_fTouchScale = KCDefine.U_SCALE_TOUCH;

	private Vector3 m_stOriginScale = Vector3.one;
	private Tween m_oAni = null;
	#endregion			// 변수

	#region 인터페이스
	//! 터치를 시작했을 경우
	public virtual void OnPointerDown(PointerEventData a_oEventData) {
		this.StartPressAni();
	}

	//! 터치를 종료했을 경우
	public virtual void OnPointerUp(PointerEventData a_oEventData) {
		this.StartReleaseAni();
	}
	#endregion			// 인터페이스

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.SetOriginScale(m_stOriginScale);
	}
	
	//! 제거 되었을 경우
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
			this.ResetAni();
		}
	}

	//! 애니메이션을 리셋한다
	public virtual void ResetAni() {
		m_oAni?.Kill();
	}

	//! 애니메이션 시간을 변경한다
	public void SetDuration(float a_fDuration) {
		m_fDuration = a_fDuration;
	}

	//! 터치 비율을 변경한다
	public void SetTouchScale(float a_fScale) {
		m_fTouchScale = a_fScale;
	}

	//! 원본 비율을 변경한다
	public void SetOriginScale(Vector3 a_stScale) {
		m_stOriginScale = a_stScale;
	}

	//! 비율을 변경한다
	public void SetScale(Vector3 a_stScale, bool a_bIsAni = true) {
		this.ResetAni();

#if DOTWEEN_ENABLE
		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni && a_bIsAni) {
			m_oAni = this.transform.DOScale(a_stScale, m_fDuration);
			m_oAni.SetEase(Ease.Linear).SetUpdate(true);
		} else {
			this.transform.localScale = a_stScale;
		}
#else
		this.transform.localScale = a_stScale;
#endif			// #if DOTWEEN_ENABLE
	}

	//! 누르기 시작 애니메이션을 시작한다
	private void StartPressAni() {
		this.SetScale(m_stOriginScale * m_fTouchScale);
	}

	//! 누르기 종료 애니메이션을 시작한다
	private void StartReleaseAni() {
		this.SetScale(m_stOriginScale);
	}
	#endregion			// 함수
}
