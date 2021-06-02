using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 서브 설정 씬 관리자
public class CSubSetupSceneManager : CSetupSceneManager {
	#region 변수
#if LOCALIZE_TEST_ENABLE
	[SerializeField] private SystemLanguage m_eLanguage = SystemLanguage.Unknown;
#endif			// #if LOCALIZE_TEST_ENABLE
	#endregion			// 변수

	#region 함수
	//! 씬을 설정한다
	protected override void Setup() {
		base.Setup();

		// 저장소를 로드한다
		CAppInfoStorage.Inst.LoadAppInfo();
		CUserInfoStorage.Inst.LoadUserInfo();
		CGameInfoStorage.Inst.LoadGameInfo();

#if LOCALIZE_TEST_ENABLE
		CCommonAppInfoStorage.Inst.AppInfo.Language = m_eLanguage;
#endif			// #if LOCALIZE_TEST_ENABLE

		// 언어가 유효하지 않을 경우
		if(!CCommonAppInfoStorage.Inst.AppInfo.Language.ExIsValid()) {
			CCommonAppInfoStorage.Inst.AppInfo.Language = Application.systemLanguage;
		}

		CCommonAppInfoStorage.Inst.SaveAppInfo();
	}
	#endregion			// 함수
}
