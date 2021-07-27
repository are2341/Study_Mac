using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 터치 상호 작용 처리자
public class CTouchInteractable : CUIsComponent {
	#region 함수
	//! 상호 작용 여부를 변경한다
	public void SetInteractable(bool a_bIsEnable) {
		// 터치 상태 일 경우
		if(a_bIsEnable) {
			this.OnEnable();
		} else {
			this.OnDisable();
		}

		this.gameObject.ExSetInteractable<Button>(a_bIsEnable, false);
		this.gameObject.ExSetEnableComponent<CTouchScaler>(a_bIsEnable, false);
		this.gameObject.ExSetEnableComponent<CTouchSndPlayer>(a_bIsEnable, false);
	}
	#endregion			// 함수
}
