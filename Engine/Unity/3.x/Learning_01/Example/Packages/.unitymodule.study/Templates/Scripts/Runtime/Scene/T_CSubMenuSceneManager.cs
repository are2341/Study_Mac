﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if NEVER_USE_THIS
#if STUDY_MODULE_ENABLE
//! 서브 메뉴 씬 관리자
public class CSubMenuSceneManager : CMenuSceneManager {
	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		// 초기화 되었을 경우
		if(CSceneManager.IsInit) {
			// Do Nothing
		}
	}
	#endregion			// 함수
}
#endif			// #if STUDY_MODULE_ENABLE
#endif			// #if NEVER_USE_THIS
