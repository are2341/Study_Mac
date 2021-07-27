using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 서브 팝업
public abstract class CSubPopup : CPopup {
	#region 프로퍼티
	public override float ShowTimeScale => KCDefine.B_VAL_0_FLT;
	public override EAniType AniType => EAniType.DROPDOWN;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Init() {
		base.Init();
	}
	#endregion			// 함수
}
