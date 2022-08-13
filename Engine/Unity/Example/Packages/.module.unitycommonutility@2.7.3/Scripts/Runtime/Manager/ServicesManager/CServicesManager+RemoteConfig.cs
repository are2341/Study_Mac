using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if REMOTE_CONFIG_ENABLE
using Unity.Services.RemoteConfig;
#endif			// #if REMOTE_CONFIG_ENABLE

/** 서비스 관리자 - 원격 속성 */
public partial class CServicesManager : CSingleton<CServicesManager> {
	#region 함수
	/** 원격 속성을 로드한다 */
	public void LoadRemoteConfig(string a_oKey, System.Action<CServicesManager, string, bool> a_oCallback) {
#if REMOTE_CONFIG_ENABLE

#else

#endif			// #if REMOTE_CONFIG_ENABLE
	}
	#endregion			// 함수

	#region 조건부 함수

	#endregion			// 조건부 함수
}
