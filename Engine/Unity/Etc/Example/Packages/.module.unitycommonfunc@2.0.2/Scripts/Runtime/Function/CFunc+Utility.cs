using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_ANDROID
using UnityEngine.Android;
#endif			// #if UNITY_ANDROID

/** 유틸리티 함수 */
public static partial class CFunc {
	#region 클래스 함수
	/** 객체를 탐색한다 */
	public static GameObject FindObj(string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		GameObject oObj = null;

		CFunc.EnumerateScenes((a_stScene) => { oObj = a_stScene.ExFindChild(a_oName); return oObj == null; });
		return oObj;
	}

	/** 객체를 탐색한다 */
	public static List<GameObject> FindObjs(string a_oName) {
		CAccess.Assert(a_oName.ExIsValid());
		var oObjList = new List<GameObject>();

		CFunc.EnumerateScenes((a_stScene) => { var oChildObjList = a_stScene.ExFindChildren(a_oName); oObjList.AddRange(oChildObjList); return true; });
		return oObjList;
	}

	/** 경로를 탐색한다 */
	public static List<Vector3Int> FindPath(Vector3Int a_stIdx, List<Vector3Int> a_oOffsetList, System.Func<CPathInfo, bool> a_oFindCallback, System.Func<CPathInfo, Vector3Int, bool> a_oMoveCallback, System.Func<CPathInfo, Vector3Int, int> a_oCostCallback) {
		CAccess.Assert(a_oFindCallback != null && a_oMoveCallback != null && a_oCostCallback != null);

		var oVisitIdxList = new List<Vector3Int>();
		var oOpenPathInfoList = new List<CPathInfo>();
		var oClosePathInfoList = new List<CPathInfo>();

		oOpenPathInfoList.Add(CFactory.MakePathInfo(a_stIdx));

		while(oOpenPathInfoList.Count > KCDefine.B_VAL_0_INT) {
			var oPathInfo = oOpenPathInfoList[KCDefine.B_VAL_0_INT];

			oClosePathInfoList.Add(oPathInfo);
			oOpenPathInfoList.ExRemoveValAt(KCDefine.B_VAL_0_INT);

			// 경로를 탐색했을 경우
			if(a_oFindCallback(oPathInfo)) {
				var oIdxList = new List<Vector3Int>();

				while(oPathInfo != null) {
					oIdxList.Insert(KCDefine.B_VAL_0_INT, oPathInfo.m_stIdx);
					oPathInfo = oPathInfo.m_oPrevPathInfo;
				}

				return oIdxList;
			}

			for(int i = 0; i < a_oOffsetList.Count; ++i) {
				var stNextIdx = oPathInfo.m_stIdx + a_oOffsetList[i];

				// 이동이 불가능 할 경우
				if(!a_oMoveCallback(oPathInfo, stNextIdx)) {
					continue;
				}

				int nIdx = oClosePathInfoList.FindIndex((a_oPathInfo) => stNextIdx.Equals(a_oPathInfo.m_stIdx));
				int nCost = a_oCostCallback(oPathInfo, stNextIdx);

				// 탐색이 가능 할 경우
				if(!oVisitIdxList.Contains(stNextIdx)) {
					var oNextPathInfo = CFactory.MakePathInfo(stNextIdx, nCost);
					oNextPathInfo.m_oPrevPathInfo = oPathInfo;

					oVisitIdxList.Add(stNextIdx);
					oOpenPathInfoList.Add(oNextPathInfo);
				}
				// 경로 정보 설정이 가능 할 경우
				else if(nIdx.ExIsValidIdx() && nCost < oClosePathInfoList[nIdx].m_nCost) {
					oClosePathInfoList[nIdx].m_nCost = nCost;
					oClosePathInfoList[nIdx].m_oPrevPathInfo = oPathInfo;
				}
			}

			oOpenPathInfoList.Sort((a_oLhs, a_oRhs) => a_oLhs.m_nCost.CompareTo(a_oRhs.m_nCost));
		}

		return KCDefine.B_EMPTY_3D_INT_VEC_LIST;
	}

	/** 메세지를 전송한다 */
	public static void SendMsg(string a_oName, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oName.ExIsValid() && a_oMsg.ExIsValid()));

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid() && a_oMsg.ExIsValid()) {
			CFunc.FindObj(a_oName)?.ExSendMsg(a_oMsg, a_oParams, a_bIsEnableAssert);
		}
	}

	/** 메세지를 전파한다 */
	public static void BroadcastMsg(string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oMsg.ExIsValid());

		// 메세지가 유효 할 경우
		if(a_oMsg.ExIsValid()) {
			CFunc.EnumerateScenes((a_stScene) => { a_stScene.ExBroadcastMsg(a_oMsg, a_oParams, a_bIsEnableAssert); return true; });
		}
	}

	/** 권한을 요청한다 */
	public static void RequestPermission(MonoBehaviour a_oComponent, string a_oPermission, System.Action<string, bool> a_oCallback, bool a_bIsRealtime = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oComponent != null && a_oPermission.ExIsValid()));

		// 컴포넌트가 존재 할 경우
		if(a_oComponent != null && a_oPermission.ExIsValid()) {
#if UNITY_ANDROID
			// 권한이 유효 할 경우
			if(CAccess.IsEnablePermission(a_oPermission)) {
				a_oCallback?.Invoke(a_oPermission, true);
			} else {
				Permission.RequestUserPermission(a_oPermission);
				
				a_oComponent.ExRepeatCallFunc((a_oSender, a_bIsComplete) => {
					// 완료 되었을 경우
					if(a_bIsComplete) {
						a_oCallback?.Invoke(a_oPermission, CAccess.IsEnablePermission(a_oPermission));
					}

					return !CAccess.IsEnablePermission(a_oPermission);
				}, KCDefine.U_DELTA_T_PERMISSION_M_REQUEST_CHECK, KCDefine.U_MAX_DELTA_T_PERMISSION_M_REQUEST_CHECK, a_bIsRealtime);
			}
#else
			a_oCallback?.Invoke(a_oPermission, false);
#endif			// #if UNITY_ANDROID
		}
	}

	/** 씬을 순회한다 */
	public static void EnumerateScenes(System.Func<Scene, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCallback != null);

		// 콜백이 존재 할 경우
		if(a_oCallback != null) {
			for(int i = 0; i < SceneManager.sceneCount; ++i) {
				var stScene = SceneManager.GetSceneAt(i);

				// 씬 순회가 불가능 할 경우
				if(!a_oCallback(stScene)) {
					break;
				}
			}
		}
	}

	/** 객체를 순회한다 */
	public static void EnumerateRootObjs(System.Func<GameObject, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCallback != null);

		// 콜백이 존재 할 경우
		if(a_oCallback != null) {
			CFunc.EnumerateScenes((a_stScene) => {
				var oObjs = a_stScene.GetRootGameObjects();

				for(int i = 0; i < oObjs.Length; ++i) {
					// 객체 순회가 불가능 할 경우
					if(!a_oCallback(oObjs[i])) {
						return false;
					}
				}

				return true;
			}, a_bIsEnableAssert);
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 컴포넌트를 탐색한다 */
	public static T FindComponent<T>(string a_oName) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFunc.FindObj(a_oName)?.GetComponentInChildren<T>();
	}

	/** 컴포넌트를 탐색한다 */
	public static List<T> FindComponents<T>(string a_oName) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFunc.FindObj(a_oName)?.GetComponentsInChildren<T>().ToList();
	}

	/** 객체를 순회한다 */
	public static void EnumerateComponents<T>(System.Func<T, bool> a_oCallback, bool a_bIsIncludeInactive = false, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oCallback != null);

		// 콜백이 존재 할 경우
		if(a_oCallback != null) {
			CFunc.EnumerateScenes((a_stScene) => {
				var oObjs = a_stScene.GetRootGameObjects();

				for(int i = 0; i < oObjs.Length; ++i) {
					var oComponents = oObjs[i].GetComponentsInChildren<T>(a_bIsIncludeInactive);

					for(int j = 0; j < oComponents.Length; ++j) {
						// 순회가 불가능 할 경우
						if(!a_oCallback(oComponents[j])) {
							goto EXIT_ENUMERATE_COMPONENTS;
						}
					}
				}

EXIT_ENUMERATE_COMPONENTS:
				return true;
			}, a_bIsEnableAssert);
		}
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	/** 객체를 선택한다 */
	public static void SelObj(GameObject a_oObj, bool a_bIsPing = false) {
		Selection.activeGameObject = a_oObj;

		// 핑 모드 일 경우
		if(a_bIsPing && a_oObj != null) {
			EditorGUIUtility.PingObject(a_oObj);
		}
	}

	/** 객체를 선택한다 */
	public static void SelObjs(List<GameObject> a_oObjList, bool a_bIsPing = false) {
		Selection.objects = (a_oObjList != null) ? a_oObjList.ToArray() : null;
		
		// 핑 모드 일 경우
		if(a_bIsPing && a_oObjList.ExIsValid()) {
			EditorGUIUtility.PingObject(a_oObjList[KCDefine.B_VAL_0_INT]);
		}
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 클래스 함수
}
