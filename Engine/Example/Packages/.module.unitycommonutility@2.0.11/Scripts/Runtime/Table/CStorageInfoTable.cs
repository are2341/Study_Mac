using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 구글 드라이브 정보 */
[System.Serializable]
public struct STGoogleDriveInfo {
	public string m_oAccessToken;
	public string m_oASetAccessToken;
	public string m_oBSetAccessToken;
}

/** 저장소 정보 테이블 */
public class CStorageInfoTable : CScriptableObj<CStorageInfoTable> {
	#region 변수
	[Header("=====> Cloud Info <=====")]
	[SerializeField] private STGoogleDriveInfo m_stGoogleDriveInfo;
	#endregion			// 변수

	#region 프로퍼티
	public string GoogleDriveAccessToken {
		get {
#if AB_TEST_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE
			return (CCommonUserInfoStorage.Inst.UserInfo.UserType == EUserType.A) ? m_stGoogleDriveInfo.m_oASetAccessToken : m_stGoogleDriveInfo.m_oBSetAccessToken;
#else
			return m_stGoogleDriveInfo.m_oAccessToken;
#endif			// #if AB_TEST_ENABLE && NEWTON_SOFT_JSON_MODULE_ENABLE
		}
	}
	#endregion			// 프로퍼티

	#region 조건부 함수
#if UNITY_EDITOR
	/** 구글 드라이브 정보를 변경한다 */
	public void SetGoogleDriveInfo(STGoogleDriveInfo a_stGoogleDriveInfo) {
		m_stGoogleDriveInfo = a_stGoogleDriveInfo;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
