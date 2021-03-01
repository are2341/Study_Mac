using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
프로젝트 리스트
:
- E01 (왕초보 따라하여 게임 만들기)
- E02 (유니티로 배우는 실전 게임 개발)
*/
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
