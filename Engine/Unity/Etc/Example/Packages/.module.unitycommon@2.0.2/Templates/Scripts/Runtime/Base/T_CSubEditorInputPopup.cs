using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if NEVER_USE_THIS
#if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
/** 서브 에디터 입력 팝업 */
public class CSubEditorInputPopup : CEditorInputPopup {
	#region 추가 변수

	#endregion			// 추가 변수

	#region 추가 프로퍼티

	#endregion			// 추가 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 초기화 */
	public override void Init(STCallbackParams a_stCallbackParams) {
		base.Init(a_stCallbackParams);
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

	#region 추가 함수

	#endregion			// 추가 함수
}
#endif			// #if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
#endif			// #if NEVER_USE_THIS
