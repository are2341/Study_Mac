using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
/** 서브 경고 팝업 */
public partial class CSubAlertPopup : CAlertPopup {
	#region 프로퍼티
	public override EAniType AniType => EAniType.DROPDOWN;
	#endregion			// 프로퍼티
	
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	
	/** 초기화 */
	public override void Init(STParams a_stParams) {
		base.Init(a_stParams);
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	protected new void UpdateUIsState() {
		base.UpdateUIsState();
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
