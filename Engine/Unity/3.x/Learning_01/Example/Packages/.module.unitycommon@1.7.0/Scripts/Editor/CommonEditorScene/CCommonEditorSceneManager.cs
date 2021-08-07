﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

//! 공용 에디터 씬 관리자
public static partial class CCommonEditorSceneManager {
	#region 클래스 변수
	private static bool m_bIsEnableSetup = false;

	private static float m_fSkipTime = 0.0f;
	private static float m_fHierarchySkipTime = 0.0f;
	
	private static GUIStyle m_oTextGUIStyle = new GUIStyle() {
		alignment = TextAnchor.MiddleRight,
		fontStyle = FontStyle.Bold
	};

	private static GUIStyle m_oOutlineGUIStyle = new GUIStyle() {
		alignment = TextAnchor.MiddleRight,
		fontStyle = FontStyle.Bold
	};

	private static Dictionary<string, string> m_oSortingLayerDict = new Dictionary<string, string>() {
		[KCDefine.U_SORTING_L_UNDERGROUND] = "U",
		[KCDefine.U_SORTING_L_BACKGROUND] = "B",
		[KCDefine.U_SORTING_L_DEF] = "D",
		[KCDefine.U_SORTING_L_FOREGROUND] = "F",
		[KCDefine.U_SORTING_L_OVERGROUND] = "O",
		[KCDefine.U_SORTING_L_TOP] = "T",
		[KCDefine.U_SORTING_L_TOPMOST] = "TM",
		[KCDefine.U_SORTING_L_ABS] = "A",
		
#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
		[KCDefine.U_SORTING_L_UNDERGROUND_UIS] = "UU",
		[KCDefine.U_SORTING_L_BACKGROUND_UIS] = "BU",
		[KCDefine.U_SORTING_L_DEF_UIS] = "DU",
		[KCDefine.U_SORTING_L_FOREGROUND_UIS] = "FU",
		[KCDefine.U_SORTING_L_OVERGROUND_UIS] = "OU",
		[KCDefine.U_SORTING_L_TOP_UIS] = "TU",
		[KCDefine.U_SORTING_L_TOPMOST_UIS] = "TMU",
		[KCDefine.U_SORTING_L_ABS_UIS] = "AU"
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
	};
	#endregion			// 클래스 변수

	#region 클래스 함수
	//! 생성자
	static CCommonEditorSceneManager() {
		// GUI 스타일을 설정한다 {
		CCommonEditorSceneManager.m_oTextGUIStyle.normal = new GUIStyleState() {
			textColor = KCEditorDefine.B_COLOR_HIERARCHY_TEXT
		};

		CCommonEditorSceneManager.m_oOutlineGUIStyle.normal = new GUIStyleState() {
			textColor = KCEditorDefine.B_COLOR_HIERARCHY_OUTLINE
		};
		// GUI 스타일을 설정한다 }

		CCommonEditorSceneManager.SetupCallbacks();
	}

	//! 스크립트가 로드 되었을 경우
	[UnityEditor.Callbacks.DidReloadScripts]
	public static void OnLoadScript() {
		CCommonEditorSceneManager.m_bIsEnableSetup = true;
	}

	//! 렌더러를 리셋한다
	[MenuItem("Tools/Utility/Reset/Renderers")]
	public static void ResetRenderers() {
		var oRendererList = CEditorFunc.FindComponents<Renderer>();

		for(int i = 0; i < oRendererList.Count; ++i) {
			oRendererList[i].allowOcclusionWhenDynamic = true;

			// 에디터 모드 일 경우
			if(!Application.isPlaying) {
				EditorSceneManager.MarkSceneDirty(oRendererList[i].gameObject.scene);
			}
		}
	}

	//! 캔버스 렌더러를 리셋한다
	[MenuItem("Tools/Utility/Reset/Canvas Renderers")]
	public static void ResetCanvasRenderers() {
		var oCanvasRendererList = CEditorFunc.FindComponents<CanvasRenderer>();

		for(int i = 0; i < oCanvasRendererList.Count; ++i) {
			oCanvasRendererList[i].cullTransparentMesh = true;

			// 에디터 모드 일 경우
			if(!Application.isPlaying) {
				EditorSceneManager.MarkSceneDirty(oCanvasRendererList[i].gameObject.scene);
			}
		}
	}

	//! 레이아웃 그룹을 리셋한다
	[MenuItem("Tools/Utility/Reset/Horizontal or Vertical LayoutGroups")]
	public static void ResetLayoutGroups() {
		var oLayoutGroupList = CEditorFunc.FindComponents<HorizontalOrVerticalLayoutGroup>();

		for(int i = 0; i < oLayoutGroupList.Count; ++i) {
			oLayoutGroupList[i].childScaleWidth = false;
			oLayoutGroupList[i].childScaleHeight = false;

			oLayoutGroupList[i].childControlWidth = true;
			oLayoutGroupList[i].childControlHeight = false;

			oLayoutGroupList[i].childForceExpandWidth = true;
			oLayoutGroupList[i].childForceExpandHeight = false;

			// 에디터 모드 일 경우
			if(!Application.isPlaying) {
				EditorSceneManager.MarkSceneDirty(oLayoutGroupList[i].gameObject.scene);
			}

			// 부모 레이아웃 그룹이 없을 경우
			if(oLayoutGroupList[i].transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>() == null) {
				(oLayoutGroupList[i].transform as RectTransform).sizeDelta = Vector2.zero;
			}

			// 컨텐츠 크기 조정자가 존재 할 경우
			if(oLayoutGroupList[i].TryGetComponent<ContentSizeFitter>(out ContentSizeFitter oSizeFitter)) {
				oSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
		}
	}

	//! 상태를 갱신한다
	private static void Update() {
		// 상태 갱신이 가능 할 경우
		if(CEditorAccess.IsEnableUpdateState) {
			CCommonEditorSceneManager.m_fSkipTime += Time.deltaTime;
			CCommonEditorSceneManager.m_fHierarchySkipTime += Time.deltaTime;

			// 갱신 주기가 지났을 경우
			if(CCommonEditorSceneManager.m_fSkipTime.ExIsGreateEquals(KCEditorDefine.B_DELTA_T_EDITOR_SM_SCENE_UPDATE)) {
				CCommonEditorSceneManager.m_fSkipTime = KCDefine.B_VAL_0_FLT;

				CCommonEditorSceneManager.SetupScene();
				CCommonEditorSceneManager.SetupLightOpts();

#if INPUT_SYSTEM_MODULE_ENABLE
				CCommonEditorSceneManager.SetupInputSystem();
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				// 설정 가능 할 경우
				if(CCommonEditorSceneManager.m_bIsEnableSetup) {
					CCommonEditorSceneManager.m_bIsEnableSetup = false;
					CAutoSceneImporter.ImportAllScenes();
					
					CPlatformOptsSetter.SetupPlayerOpts();
					CPlatformOptsSetter.SetupEditorOpts();
					CPlatformOptsSetter.SetupProjOpts();
					CPlatformOptsSetter.SetupPluginProjs();

#if UNITY_ANDROID
					// 플러그인이 없을 경우
					if(!File.Exists(KCEditorDefine.B_DEST_PLUGIN_P_ANDROID)) {
						CEditorFunc.ExecuteCmdLine(KCEditorDefine.B_PLUGIN_BUILD_CMD_ANDROID);
					}
#endif			// #if UNITY_ANDROID
				}
				
				// 갱신 주기가 지났을 경우
				if(CCommonEditorSceneManager.m_fHierarchySkipTime.ExIsGreateEquals(KCEditorDefine.B_DELTA_T_HIERARCHY_UPDATE)) {
					CCommonEditorSceneManager.m_fHierarchySkipTime = KCDefine.B_VAL_0_FLT;

					CFunc.EnumerateObjs((a_oObjs) => {
						for(int j = 0; j < a_oObjs.Length; ++j) {
							var oEnumerator = a_oObjs[j].DescendantsAndSelf();
							var oRemoveObjList = new List<GameObject>();

							foreach(var oObj in oEnumerator) {
								int nNumMissingScripts = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(oObj);

								// 객체 제거가 필요 할 경우
								if(PrefabUtility.IsPrefabAssetMissing(oObj)) {
									oRemoveObjList.ExAddVal(oObj);
								}

								// 스크립트 제거가 필요 할 경우
								if(nNumMissingScripts > KCDefine.B_VAL_0_INT) {
									GameObjectUtility.RemoveMonoBehavioursWithMissingScript(oObj);
									EditorSceneManager.MarkSceneDirty(a_oObjs[j].scene);
								}
							}

							for(int i = 0; i < oRemoveObjList.Count; ++i) {
								CFactory.RemoveObj(oRemoveObjList[i]);
							}
						}

						return true;
					});
				}
			}

			// 기즈모를 그릴 수 있을 경우
			if(CEditorAccess.IsEnableDrawGizmos) {
				CFunc.EnumerateScenes((a_stScene) => {
					var oSceneManager = a_stScene.ExFindComponent<CSceneManager>(KCDefine.U_OBJ_N_SCENE_MANAGER);
					oSceneManager?.EditorSetupScene();

					return true;
				});
			}
		}
	}

	//! 계층 뷰 UI 상태를 갱신한다
	private static void UpdateHierarchyUIState(int a_nInstanceID, Rect a_stRect) {
		var oObj = EditorUtility.InstanceIDToObject(a_nInstanceID) as GameObject;

		// 객체가 존재 할 경우
		if(oObj != null) {
			var oComponents = oObj.GetComponents<Component>();

			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트가 존재 할 경우
				if(oComponents[i] != null) {
					var oType = oComponents[i].GetType();

					var oSortingLayerProperty = oType.GetProperty(KCEditorDefine.B_PROPERTY_N_SORTING_LAYER, KCDefine.B_BINDING_F_PUBLIC_INSTANCE);
					var oSortingOrderProperty = oType.GetProperty(KCEditorDefine.B_PROPERTY_N_SORTING_ORDER, KCDefine.B_BINDING_F_PUBLIC_INSTANCE);

					string oSortingLayer = (string)oSortingLayerProperty?.GetValue(oComponents[i]);
					oSortingLayer = oSortingLayer.ExIsValid() ? CCommonEditorSceneManager.m_oSortingLayerDict.ExGetVal(oSortingLayer, string.Empty) : string.Empty;

					// 프로퍼티가 존재 할 경우
					if(oSortingOrderProperty != null && oSortingLayer.ExIsValid()) {
						a_stRect.position += new Vector2((a_stRect.size.x + KCEditorDefine.B_OFFSET_HIERARCHY_TEXT) * -1.0f, KCDefine.B_VAL_0_FLT);
						string oStr = string.Format(KCEditorDefine.B_SORTING_OI_FMT, oSortingLayer, oSortingOrderProperty.GetValue(oComponents[i]));

						var oRects = new Rect[] {
							new Rect(a_stRect.x + KCEditorDefine.B_OFFSET_HIERARCHY_OUTLINE, a_stRect.y, a_stRect.width, a_stRect.height),
							new Rect(a_stRect.x - KCEditorDefine.B_OFFSET_HIERARCHY_OUTLINE, a_stRect.y, a_stRect.width, a_stRect.height),
							new Rect(a_stRect.x, a_stRect.y + KCEditorDefine.B_OFFSET_HIERARCHY_OUTLINE, a_stRect.width, a_stRect.height),
							new Rect(a_stRect.x, a_stRect.y - KCEditorDefine.B_OFFSET_HIERARCHY_OUTLINE, a_stRect.width, a_stRect.height),
						};

						for(int j = 0; j < oRects.Length; ++j) {
							GUI.Label(oRects[j], oStr, CCommonEditorSceneManager.m_oOutlineGUIStyle);
						}

						GUI.Label(a_stRect, oStr, CCommonEditorSceneManager.m_oTextGUIStyle);
					}
				}
			}
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
