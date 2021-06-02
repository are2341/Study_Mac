using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 서브 인트로 씬 관리자
public class CSubIntroSceneManager : CIntroSceneManager {
	#region 함수
	//! 씬을 설정한다
	protected override void Setup() {
		base.Setup();

#if STUDY_MODULE_ENABLE
		CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_MENU);
#else
		CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_TITLE);
#endif			// #if STUDY_MODULE_ENABLE
	}
	#endregion			// 함수
}
