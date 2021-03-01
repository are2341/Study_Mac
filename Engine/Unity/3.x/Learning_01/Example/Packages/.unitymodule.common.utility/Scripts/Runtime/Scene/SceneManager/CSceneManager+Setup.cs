using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif			// #if UNITY_EDITOR

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

//! 씬 관리자 - 설정
public abstract partial class CSceneManager : CComponent {
	#region 함수
	//! 씬을 설정한다
	protected virtual void SetupScene() {
		// 씬을 설정한다 {
		this.SetupLights();
		this.SetupDefObjs();

		// 루트 씬 일 경우
		if(this.IsRootScene) {
			this.SetupRootScene();
		} else {
			this.SetupSubScene();
		}
		// 씬을 설정한다 }

		// 카메라를 설정한다 {
		// 서브 UI 카메라가 존재 할 경우
		if(this.SubUICamera != null) {
			this.SubUICamera.backgroundColor = this.ClearColor;

			this.SubUICamera.ExSetCullingMask(KCDefine.U_DEF_LAYER_MASK_UI_CAMERA.ToList());
			this.SubUICamera.gameObject.ExSetEnableComponent<AudioListener>(false);

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
			this.SubUICamera.clearFlags = CameraClearFlags.Nothing;
			this.SubUICamera.gameObject.SetActive(false);

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
			var oCameraData = this.SubUICamera.gameObject.ExAddComponent<UniversalAdditionalCameraData>();

			// 카메라 데이터가 존재 할 경우
			if(oCameraData != null) {
				oCameraData.renderType = CameraRenderType.Overlay;

#if LIGHT_ENABLE && SHADOW_ENABLE
				oCameraData.renderShadows = true;
#else
				oCameraData.renderShadows = false;
#endif			// #if LIGHT_ENABLE && SHADOW_ENABLE

#if UNITY_POST_PROCESSING_STACK_V2
				oCameraData.renderPostProcessing = true;
#else
				oCameraData.renderPostProcessing = false;
#endif			// #if UNITY_POST_PROCESSING_STACK_V2
			}
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE
#else
			this.SubUICamera.clearFlags = CameraClearFlags.Depth;
			this.SubUICamera.gameObject.SetActive(true);
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
		}

		//! 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera && this.SubMainCamera != null) {
			this.SubMainCamera.clearFlags = this.IsRootScene ? CameraClearFlags.SolidColor : CameraClearFlags.Nothing;
			this.SubMainCamera.backgroundColor = this.ClearColor;

			this.SubMainCamera.ExSetCullingMask(KCDefine.U_DEF_LAYER_MASK_MAIN_CAMERA.ToList());

			this.SubMainCamera.gameObject.ExSetEnableComponent<AudioListener>(this.IsRootScene);
			this.SubMainCamera.gameObject.SetActive(this.IsRootScene);

#if PHYSICS_RAYCASTER_ENABLE
#if MODE_2D_ENABLE
			var oRaycaster = this.SubMainCamera.gameObject.ExAddComponent<Physics2DRaycaster>();
#else
			var oRaycaster = this.SubMainCamera.gameObject.ExAddComponent<PhysicsRaycaster>();
#endif			// #if MODE_2D_ENABLE

			oRaycaster.ExSetEventMask(KCDefine.DEF_CULLING_MASK_MAIN_CAMERA.ToList());
#endif			// #if PHYSICS_RAYCASTER_ENABLE
			
#if UNIVERSAL_PIPELINE_MODULE_ENABLE
			var oCameraData = CSceneManager.MainCamera.gameObject.ExAddComponent<UniversalAdditionalCameraData>();

			// 카메라 데이터가 존재 할 경우
			if(oCameraData != null) {
				oCameraData.renderType = CameraRenderType.Base;
				oCameraData.cameraStack?.Clear();

#if LIGHT_ENABLE && SHADOW_ENABLE
				oCameraData.renderShadows = true;
#else
				oCameraData.renderShadows = false;
#endif			// #if LIGHT_ENABLE && SHADOW_ENABLE

#if UNITY_POST_PROCESSING_STACK_V2
				oCameraData.renderPostProcessing = true;
#else
				oCameraData.renderPostProcessing = false;
#endif			// #if UNITY_POST_PROCESSING_STACK_V2
			}
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE
		}
		// 카메라를 설정한다 }

		// 캔버스 순서를 설정한다 {
		this.SubPopupUICanvas.overrideSorting = true;
		this.SubPopupUICanvas.overridePixelPerfect = false;
		
		this.SubTopmostUICanvas.overrideSorting = true;
		this.SubTopmostUICanvas.overridePixelPerfect = false;

		this.SubPopupUICanvas.ExSetSortingOrder(new STSortingOrderInfo() {
			m_nOrder = this.UICanvasSortingOrderInfo.m_nOrder + sbyte.MaxValue,
			m_oLayer = this.UICanvasSortingOrderInfo.m_oLayer
		});

		this.SubTopmostUICanvas.ExSetSortingOrder(new STSortingOrderInfo() {
			m_nOrder = this.UICanvasSortingOrderInfo.m_nOrder + byte.MaxValue,
			m_oLayer = this.UICanvasSortingOrderInfo.m_oLayer
		});

		this.SubUICanvas.ExSetSortingOrder(this.UICanvasSortingOrderInfo);
		this.SubObjCanvas?.ExSetSortingOrder(this.ObjCanvasSortingOrderInfo);
		// 캔버스 순서를 설정한다 }
	}

	//! 루트 씬을 설정한다
	protected virtual void SetupRootScene() {
		this.SetupCamera();
		this.SetupRootObjs();

		// 이벤트 시스템을 설정한다
		CSceneManager.UITop.ExSetEnableComponent<EventSystem>(true);
		CSceneManager.UITop.ExSetEnableComponent<BaseInputModule>(true);
		
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
		// 디버그 버튼이 존재 할 경우
		if(CSceneManager.ScreenDebugBtn != null) {
			CSceneManager.ScreenDebugBtn.gameObject.SetActive(true);

			CSceneManager.ScreenDebugBtn.onClick.RemoveAllListeners();
			CSceneManager.ScreenDebugBtn.onClick.AddListener(CSceneManager.OnTouchDebugBtn);
		}
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
		// FPS 버튼이 존재 할 경우
		if(CSceneManager.ScreenFPSBtn != null) {
			CSceneManager.ScreenFPSBtn.gameObject.SetActive(true);

			CSceneManager.ScreenFPSBtn.onClick.RemoveAllListeners();
			CSceneManager.ScreenFPSBtn.onClick.AddListener(CSceneManager.OnTouchFPSBtn);
		}
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			// 루트 씬 일 경우
			if(this.IsRootScene && !this.SceneName.ExIsEquals(KCDefine.B_SCENE_N_AGREE)) {
				CScheduleManager.Inst.AddComponent(this);
				CNavStackManager.Inst.AddComponent(this);

				this.StartScreenFadeOutAni(this.FadeOutAniDuration);
			}

			// 캔버스를 설정한다 {
			this.SetupCanvas(CSceneManager.ScreenBlindUIs?.GetComponentInParent<Canvas>());
			this.SetupCanvas(CSceneManager.ScreenPopupUIs?.GetComponentInParent<Canvas>());
			this.SetupCanvas(CSceneManager.ScreenTopmostUIs?.GetComponentInParent<Canvas>());
			this.SetupCanvas(CSceneManager.ScreenAbsUIs?.GetComponentInParent<Canvas>());

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			// 앱이 초기화 되었을 경우
			if(CSceneManager.IsAppInit && CSceneManager.ScreenDebugConsole != null) {
				bool bIsTestDevice = CCommonAppInfoStorage.Inst.IsTestDevice();
				CSceneManager.ScreenDebugConsole.SetActive(bIsTestDevice);
			}
			
			this.SetupCanvas(CSceneManager.ScreenDebugUIs?.GetComponentInParent<Canvas>());
			this.SetupCanvas(CSceneManager.ScreenDebugConsole?.GetComponentInParent<Canvas>());
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			// 캔버스를 설정한다 }
		}
	}

	//! 서브 씬을 설정한다
	protected virtual void SetupSubScene() {
		// 서브 객체를 설정한다 {
		var stScene = this.gameObject.scene;

		this.SubUITop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UI_TOP);
		this.SubUIBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UI_BASE);
		this.SubUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.SubAnchorUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);

		this.SubLeftUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.SubRightUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);
		this.SubTopUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOP_UIS);
		this.SubBottomUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BOTTOM_UIS);

		this.SubPopupUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.SubTopmostUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);

		this.SubBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.SubObjBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_BASE);
		this.SubObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.SubAnchorObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);

		this.SubLeftObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.SubRightObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		this.SubTopObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOP_OBJS);
		this.SubBottomObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BOTTOM_OBJS);

		this.SubObjCanvasTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_TOP);
		this.SubObjCanvasBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_BASE);
		this.SubCanvasObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);

		this.SubUICamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UI_CAMERA);
		this.SubMainCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		this.SubUICanvas = this.SubUIBase.GetComponentInChildren<Canvas>();
		this.SubPopupUICanvas = this.SubPopupUIs.GetComponentInChildren<Canvas>();
		this.SubTopmostUICanvas = this.SubTopmostUIs.GetComponentInChildren<Canvas>();

		this.SubObjCanvas = this.SubObjCanvasBase?.GetComponentInChildren<Canvas>();
		// 서브 객체를 설정한다 }

		// 루트 객체를 설정한다
		this.SetupCamera();
		this.SetupRootObjs();

		// 유일한 객체를 설정한다 {
		var oObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			var oEventSystems = oObjs[i].GetComponentsInChildren<EventSystem>(true);
			var oInputModules = oObjs[i].GetComponentsInChildren<BaseInputModule>(true);
			var oAudioListeners = oObjs[i].GetComponentsInChildren<AudioListener>(true);

			this.SetupUniqueComponents(oEventSystems);
			this.SetupUniqueComponents(oInputModules);
			this.SetupUniqueComponents(oAudioListeners);
		}
		// 유일한 객체를 설정한다 }
	}

	//! 광원을 설정한다
	protected virtual void SetupLights() {
		var oObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			var oLights = oObjs[i].GetComponentsInChildren<Light>(true);

			for(int j = 0; j < oLights.Length; ++j) {
				bool bIsMainLight = oLights[j].name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_MAIN_LIGHT);
				bool bIsDirectional = oLights[j].type == LightType.Directional;
				
				oLights[j].shadows = KCDefine.U_DEF_LIGHT_SHADOW_TYPE;

				// 메인 방향 광원 일 경우
				if(bIsMainLight && bIsDirectional) {
					oLights[j].transform.localScale = KCDefine.B_SCALE_NORM;
					oLights[j].transform.localEulerAngles = KCDefine.U_DEF_ANGLE_MAIN_LIGHT;
					oLights[j].transform.localPosition = new Vector3(KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_0, -this.PlaneDistance);
				}

#if LIGHT_ENABLE
				oLights[j].gameObject.SetActive(!bIsMainLight || this.IsRootScene);
#else
				oLights[j].gameObject.SetActive(false);
#endif			// #if LIGHT_ENABLE
			}
		}
	}

	//! 간격을 설정한다
	protected virtual void SetupOffsets() {
		// 캔버스 크기를 설정한다
		if(CSceneManager.UICanvas != null && CSceneManager.UICanvas.renderMode == RenderMode.ScreenSpaceCamera) {
			var oTrans = CSceneManager.UICanvas.transform as RectTransform;
			
			CSceneManager.CanvasSize = oTrans.sizeDelta;
			CSceneManager.CanvasScale = oTrans.localScale;
		}

		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			float fLeftScale = CAccess.GetLeftScreenScale();
			float fRightScale = CAccess.GetRightScreenScale();
			float fTopScale = CAccess.GetTopScreenScale();
			float fBottomScale = CAccess.GetBottomScreenScale();

			// 간격을 설정한다 {
			CSceneManager.LeftUIOffset = CSceneManager.CanvasSize.x * fLeftScale;
			CSceneManager.LeftObjOffset = CSceneManager.CanvasSize.x * fLeftScale;

			CSceneManager.RightUIOffset = CSceneManager.CanvasSize.x * -fRightScale;
			CSceneManager.RightObjOffset = CSceneManager.CanvasSize.x * -fRightScale;

			CSceneManager.TopUIOffset = CSceneManager.CanvasSize.y * -fTopScale;
			CSceneManager.TopObjOffset = CSceneManager.CanvasSize.y * -fTopScale;

			CSceneManager.BottomUIOffset = CSceneManager.CanvasSize.y * fBottomScale;
			CSceneManager.BottomObjOffset = CSceneManager.CanvasSize.y * fBottomScale;
			// 간격을 설정한다 }
		}
	}

	//! 루트 객체를 설정한다
	protected virtual void SetupRootObjs() {
		this.transform.localScale = KCDefine.B_SCALE_NORM;
		this.transform.localEulerAngles = Vector3.zero;
		this.transform.localPosition = Vector3.zero;

		this.SubBase.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubBase.transform.localEulerAngles = Vector3.zero;
		this.SubBase.transform.localPosition = Vector3.zero;

		this.SubUITop.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubUITop.transform.localEulerAngles = Vector3.zero;
		this.SubUITop.transform.localPosition = Vector3.zero;

		this.SubObjBase.transform.localScale = CSceneManager.CanvasScale;
		this.SubObjBase.transform.localEulerAngles = Vector3.zero;
		this.SubObjBase.transform.localPosition = Vector3.zero;

		this.SubObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubObjs.transform.localEulerAngles = Vector3.zero;
		this.SubObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f, KCDefine.B_VALUE_FLT_0);

		this.SubAnchorObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubAnchorObjs.transform.localEulerAngles = Vector3.zero;
		this.SubAnchorObjs.transform.localPosition = Vector3.zero;

		this.SubLeftObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubLeftObjs.transform.localEulerAngles = Vector3.zero;
		this.SubLeftObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / -2.0f) + CSceneManager.LeftObjOffset, KCDefine.B_SCREEN_HEIGHT / -2.0f, KCDefine.B_VALUE_FLT_0);

		this.SubRightObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubRightObjs.transform.localEulerAngles = Vector3.zero;
		this.SubRightObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / 2.0f) + CSceneManager.RightObjOffset, KCDefine.B_SCREEN_HEIGHT / -2.0f, KCDefine.B_VALUE_FLT_0);

		this.SubTopObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubTopObjs.transform.localEulerAngles = Vector3.zero;
		this.SubTopObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -2.0f, (CSceneManager.CanvasSize.y / 2.0f) + CSceneManager.TopObjOffset, KCDefine.B_VALUE_FLT_0);

		this.SubBottomObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubBottomObjs.transform.localEulerAngles = Vector3.zero;
		this.SubBottomObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -2.0f, (CSceneManager.CanvasSize.y / -2.0f) + CSceneManager.BottomObjOffset, KCDefine.B_VALUE_FLT_0);

		// 서브 탑 객체 캔버스가 존재 할 경우
		if(this.SubObjCanvasTop != null) {
			this.SubObjCanvasTop.transform.localScale = KCDefine.B_SCALE_NORM;
			this.SubObjCanvasTop.transform.localEulerAngles = Vector3.zero;
			this.SubObjCanvasTop.transform.localPosition = Vector3.zero;
		}

#if MODE_CENTER_ENABLE
		this.SubUIs.transform.localPosition = Vector3.zero;
		this.SubPopupUIs.transform.localPosition = Vector3.zero;
		this.SubTopmostUIs.transform.localPosition = Vector3.zero;
		
		this.SubObjs.transform.localPosition = Vector3.zero;
		this.SubCanvasObjs.transform.localPosition = Vector3.zero;

		this.SubLeftObjs.transform.ExSetLocalPosY(KCDefine.B_VALUE_FLT_0);
		this.SubRightObjs.transform.ExSetLocalPosY(KCDefine.B_VALUE_FLT_0);

		this.SubTopObjs.transform.ExSetLocalPosX(KCDefine.B_VALUE_FLT_0);
		this.SubBottomObjs.transform.ExSetLocalPosX(KCDefine.B_VALUE_FLT_0);
#endif			// #if MODE_CENTER_ENABLE

		// 루트 객체 간격을 설정한다 {
		var oUIsTrans = CSceneManager.UIs.transform as RectTransform;
		var oLeftUIsTrans = CSceneManager.LeftUIs.transform as RectTransform;
		var oRightUIsTrans = CSceneManager.RightUIs.transform as RectTransform;
		var oTopUIsTrans = CSceneManager.TopUIs.transform as RectTransform;
		var oBottomUIsTrans = CSceneManager.BottomUIs.transform as RectTransform;

		CSceneManager.LeftUIsOffset = oUIsTrans.anchoredPosition.x - oLeftUIsTrans.anchoredPosition.x;
		CSceneManager.RightUIsOffset = (oUIsTrans.anchoredPosition.x + KCDefine.B_SCREEN_WIDTH) - oRightUIsTrans.anchoredPosition.x;
		CSceneManager.TopUIsOffset = (oUIsTrans.anchoredPosition.y + KCDefine.B_SCREEN_HEIGHT) - oTopUIsTrans.anchoredPosition.y;
		CSceneManager.BottomUIsOffset = oUIsTrans.anchoredPosition.y - oBottomUIsTrans.anchoredPosition.y;

		CSceneManager.LeftObjsOffset = CSceneManager.Objs.transform.localPosition.x - CSceneManager.LeftObjs.transform.localPosition.x;
		CSceneManager.RightObjsOffset = (CSceneManager.Objs.transform.localPosition.x + KCDefine.B_SCREEN_WIDTH) - CSceneManager.RightObjs.transform.localPosition.x;
		CSceneManager.TopObjsOffset = (CSceneManager.Objs.transform.localPosition.y + KCDefine.B_SCREEN_HEIGHT) - CSceneManager.TopObjs.transform.localPosition.y;
		CSceneManager.BottomObjsOffset = CSceneManager.Objs.transform.localPosition.y - CSceneManager.BottomObjs.transform.localPosition.y;
		// 루트 객체 간격을 설정한다 }
	}

	//! 기본 객체를 설정한다
	protected virtual void SetupDefObjs() {
		// 씬 관리자를 설정한다
		CSceneManager.m_oSubSceneManagerList.ExAddValue(this.SceneName, this);
		CSceneManager.RootSceneManager = CSceneManager.RootScene.ExFindComponent<CSceneManager>(KCDefine.U_OBJ_N_SCENE_MANAGER);

		// 기본 객체를 설정한다 {
		var stScene = CSceneManager.RootSceneManager.gameObject.scene;

		this.SubUITop = CSceneManager.UITop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UI_TOP);
		this.SubUIBase = CSceneManager.UIBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UI_BASE);
		this.SubUIs = CSceneManager.UIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.SubAnchorUIs = CSceneManager.AnchorUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);

		this.SubLeftUIs = CSceneManager.LeftUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.SubRightUIs = CSceneManager.RightUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);
		this.SubTopUIs = CSceneManager.TopUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOP_UIS);
		this.SubBottomUIs = CSceneManager.BottomUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BOTTOM_UIS);

		this.SubPopupUIs = CSceneManager.PopupUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.SubTopmostUIs = CSceneManager.TopmostUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);

		this.SubBase = CSceneManager.Base = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.SubObjBase = CSceneManager.ObjBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_BASE);
		this.SubObjs = CSceneManager.Objs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.SubAnchorObjs = CSceneManager.AnchorObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);

		this.SubLeftObjs = CSceneManager.LeftObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.SubRightObjs = CSceneManager.RightObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		this.SubTopObjs = CSceneManager.TopObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOP_OBJS);
		this.SubBottomObjs = CSceneManager.BottomObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BOTTOM_OBJS);

		this.SubObjCanvasTop = CSceneManager.ObjCanvasTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_TOP);
		this.SubObjCanvasBase = CSceneManager.ObjCanvasBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_BASE);
		this.SubCanvasObjs = CSceneManager.CanvasObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);

		this.SubUICamera = CSceneManager.UICamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UI_CAMERA);
		this.SubMainCamera = CSceneManager.MainCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		this.SubUICanvas = CSceneManager.UICanvas = CSceneManager.UIBase.GetComponentInChildren<Canvas>();
		this.SubPopupUICanvas = CSceneManager.PopupUICanvas = CSceneManager.PopupUIs.GetComponentInChildren<Canvas>();
		this.SubTopmostUICanvas = CSceneManager.TopmostUICanvas = CSceneManager.TopmostUIs.GetComponentInChildren<Canvas>();

		this.SubObjCanvas = CSceneManager.ObjCanvas = CSceneManager.ObjCanvasBase?.GetComponentInChildren<Canvas>();

		CSceneManager.EventSystem = CSceneManager.UITop.GetComponentInChildren<EventSystem>();
		CSceneManager.BaseInputModule = CSceneManager.EventSystem.GetComponentInChildren<BaseInputModule>();

#if INPUT_SYSTEM_MODULE_ENABLE
		var oInputModule = CSceneManager.BaseInputModule as InputSystemUIInputModule;

		// 입력 모듈이 없을 경우
		if(!oInputModule) {
			CSceneManager.EventSystem.gameObject.ExRemoveComponent<BaseInputModule>();
			CSceneManager.BaseInputModule = CSceneManager.EventSystem.gameObject.ExAddComponent<InputSystemUIInputModule>();

#if UNITY_EDITOR
			// 에디터 모드 일 경우
			if(!Application.isPlaying) {
				EditorSceneManager.MarkSceneDirty(this.gameObject.scene);
			}
#endif			// #if UNITY_EDITOR
		}
#else
		var oInputModule = CSceneManager.BaseInputModule as StandaloneInputModule;

		// 입력 모듈이 없을 경우
		if(!oInputModule) {
			CSceneManager.EventSystem.gameObject.ExRemoveComponent<BaseInputModule>();
			CSceneManager.BaseInputModule = CSceneManager.EventSystem.gameObject.ExAddComponent<StandaloneInputModule>();
		}
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE
		// 기본 객체를 설정한다 }
	}

	//! 캔버스를 설정한다
	protected virtual void SetupCanvas(Canvas a_oCanvas) {
		// 캔버스가 존재 할 경우
		if(a_oCanvas != null) {
			var oObj = a_oCanvas.gameObject;

			// UI 객체를 설정한다 {
			var oUIObjs = new GameObject[] {
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_BOTTOM_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_POPUP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_ABS_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_DEBUG_C_LOG_POPUP),

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS)
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			};

			var oExtraUIObjNames = new string[] {
				KCDefine.U_OBJ_N_DEBUG_C_LOG_POPUP
			};

			for(int i = 0; i < oUIObjs.Length; ++i) {
				// UI 객체가 존재 할 경우
				if(oUIObjs[i] != null) {
					string oName = oUIObjs[i].name;

					var stPos = Vector2.zero;
					var stSize = Vector2.zero;
					var stPivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

					bool bIsLeftUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
					bool bIsRightUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);
					bool bIsTopUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_TOP_UIS);
					bool bIsBottomUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_BOTTOM_UIS);

					// 좌측, 우측 UI 루트 일 경우
					if(bIsLeftUIs || bIsRightUIs) {
						stSize = new Vector2(KCDefine.B_VALUE_FLT_0, KCDefine.B_SCREEN_HEIGHT);

						// 좌측 UI 루트 일 경우
						if(bIsLeftUIs) {
							stPos = new Vector2((CSceneManager.CanvasSize.x / -2.0f) + CSceneManager.LeftUIOffset, KCDefine.B_SCREEN_HEIGHT / -2.0f);
							stPivot = KCDefine.B_ANCHOR_BOTTOM_LEFT;
						} else {
							stPos = new Vector2((CSceneManager.CanvasSize.x / 2.0f) + CSceneManager.RightUIOffset, KCDefine.B_SCREEN_HEIGHT / -2.0f);
							stPivot = KCDefine.B_ANCHOR_BOTTOM_RIGHT;
						}
					}
					// 상단, 하단 UI 루트 일 경우
					else if(bIsTopUIs || bIsBottomUIs) {
						stSize = new Vector2(KCDefine.B_SCREEN_WIDTH, KCDefine.B_VALUE_FLT_0);

						// 상단 UI 루트 일 경우
						if(bIsTopUIs) {
							stPos = new Vector2(KCDefine.B_SCREEN_WIDTH / -2.0f, (CSceneManager.CanvasSize.y / 2.0f) + CSceneManager.TopUIOffset);
							stPivot = KCDefine.B_ANCHOR_TOP_LEFT;
						} else {
							stPos = new Vector2(KCDefine.B_SCREEN_WIDTH / -2.0f, (CSceneManager.CanvasSize.y / -2.0f) + CSceneManager.BottomUIOffset);
							stPivot = KCDefine.B_ANCHOR_BOTTOM_LEFT;
						}
					} else {
						bool bIsUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UIS);
						bool bIsAnchorUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);
						bool bIsBlindUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS);
						bool bIsCanvasObjs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);

						bool bIsDebugUIs = false;

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
						bIsDebugUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS);
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

						// 크기 보정 가능한 UI 객체 일 경우
						if(bIsUIs || bIsAnchorUIs || bIsBlindUIs || bIsCanvasObjs || bIsDebugUIs) {
							// UI 루트 일 경우
							if(bIsUIs || bIsCanvasObjs) {
								stSize = KCDefine.B_SCREEN_SIZE;

#if !MODE_CENTER_ENABLE
								// 캔버스 객체 루트 일 경우
								if(bIsCanvasObjs) {
									stPos = new Vector2(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f);
									stPivot = KCDefine.B_ANCHOR_BOTTOM_LEFT;
								}
#endif			// #if !MODE_CENTER_ENABLE
							} else {
								stSize = CSceneManager.CanvasSize;
							}
						} else {
#if MODE_CENTER_ENABLE
							bool bIsScreenPopupUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_POPUP_UIS);
							bool bIsScreenTopmostUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS);
							bool bIsScreenAbsUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_ABS_UIS);

							// 화면 UI 객체 루트가 아닐 경우
							if(!bIsScreenPopupUIs && !bIsScreenTopmostUIs && !bIsScreenAbsUIs) {
								stPos = new Vector2(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f);	
							}
#else
							stPos = new Vector2(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f);
#endif			// #if MODE_CENTER_ENABLE
						}
					}

					int nIdx = oExtraUIObjNames.ExFindValue((a_oObjName) => oUIObjs[i].name.ExIsEquals(a_oObjName));

					// 추가 UI 객체 일 경우
					if(!oExtraUIObjNames.ExIsValidIdx(nIdx)) {
						var oTrans = oUIObjs[i].transform as RectTransform;
						oTrans.pivot = stPivot;
						oTrans.anchorMin = KCDefine.B_ANCHOR_MIDDLE_CENTER;
						oTrans.anchorMax = KCDefine.B_ANCHOR_MIDDLE_CENTER;

						oTrans.sizeDelta = stSize;
						oTrans.anchoredPosition = stPos;
						oTrans.localEulerAngles = Vector3.zero;
					}
				}

				// 디버그 로드 팝업 일 경우
				if(oUIObjs[i] != null && oUIObjs[i].name.ExIsEquals(KCDefine.U_OBJ_N_DEBUG_C_LOG_POPUP)) {
					var oTrans = oUIObjs[i].transform as RectTransform;
					oTrans.pivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;
					oTrans.anchorMin = KCDefine.B_ANCHOR_TOP_LEFT;
					oTrans.anchorMax = KCDefine.B_ANCHOR_TOP_LEFT;
					oTrans.anchoredPosition = KCDefine.U_POS_DEBUG_C_DEBUG_LOG_POPUP;
				}

				// 블라인드 UI 루트 일 경우
				if(oUIObjs[i] != null && oUIObjs[i].name.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS)) {
					var oPivots = new Vector2[] {
						KCDefine.B_ANCHOR_MIDDLE_RIGHT,
						KCDefine.B_ANCHOR_MIDDLE_LEFT,
						KCDefine.B_ANCHOR_BOTTOM_CENTER,
						KCDefine.B_ANCHOR_TOP_CENTER
					};

					var oAnchors = new Vector2[] {
						KCDefine.B_ANCHOR_MIDDLE_LEFT,
						KCDefine.B_ANCHOR_MIDDLE_RIGHT,
						KCDefine.B_ANCHOR_TOP_CENTER,
						KCDefine.B_ANCHOR_BOTTOM_CENTER
					};

					var oPoses = new Vector2[] {
						Vector2.zero,
						Vector2.zero,
						Vector2.zero,
						new Vector2(KCDefine.B_VALUE_FLT_0, CSceneManager.BottomUIOffset)
					};

					var stLeftOffset = new Vector2((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / 2.0f, KCDefine.B_VALUE_FLT_0);
					var stRightOffset = new Vector2((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / -2.0f, KCDefine.B_VALUE_FLT_0);
					var stTopOffset = new Vector2(KCDefine.B_VALUE_FLT_0, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / -2.0f);
					var stBottomOffset = new Vector2(KCDefine.B_VALUE_FLT_0, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / 2.0f);

					var oOffsets = new Vector2[] {
						this.IsIgnoreHBlind ? Vector2.zero : stLeftOffset,
						this.IsIgnoreHBlind ? Vector2.zero : stRightOffset,
						this.IsIgnoreVBlind ? Vector2.zero : stTopOffset,
						this.IsIgnoreVBlind ? Vector2.zero : stBottomOffset
					};

					int nIdx = oOffsets.Length - KCDefine.B_VALUE_INT_1;
					oOffsets[nIdx].y = Mathf.Max(KCDefine.B_VALUE_FLT_0, oOffsets[oOffsets.Length - KCDefine.B_VALUE_INT_1].y - CSceneManager.BottomUIOffset);

					var oImgs = new Image[] {
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_LEFT_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_RIGHT_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_TOP_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_BOTTOM_BLIND_IMG)
					};

					for(int j = 0; j < oImgs.Length; ++j) {
						var oImg = oImgs[j];
						oImg.color = CAccess.IsEditor() ? KCDefine.U_COLOR_TRANSPARENT : KCDefine.U_DEF_COLOR_BLIND_UI;
						oImg.rectTransform.pivot = oPivots[j];
						oImg.rectTransform.anchorMin = oAnchors[j];
						oImg.rectTransform.anchorMax = oAnchors[j];
						oImg.rectTransform.sizeDelta = CSceneManager.CanvasSize;
						oImg.rectTransform.anchoredPosition = oPoses[j] + oOffsets[j];
					}
				}

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
				// 디버그 UI 루트 일 경우
				if(oUIObjs[i] != null && oUIObjs[i].name.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS)) {
					var stSafeArea = CAccess.GetSafeArea();
					var stScreenSize = CAccess.GetScreenSize();

					// 정적 디버그 텍스트를 설정한다
					if(CSceneManager.ScreenStaticDebugText != null) {
						var oTrans = CSceneManager.ScreenStaticDebugText.rectTransform;
						oTrans.pivot = KCDefine.B_ANCHOR_TOP_LEFT;
						oTrans.anchorMin = KCDefine.B_ANCHOR_TOP_LEFT;
						oTrans.anchorMax = KCDefine.B_ANCHOR_TOP_LEFT;
						oTrans.sizeDelta = new Vector2(CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y / 2.0f);
						oTrans.anchoredPosition = Vector2.zero;

						CSceneManager.m_oStaticDebugStringBuilder.Clear();
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_A, stScreenSize.x, stScreenSize.y);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_B, KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_C, CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_D, CSceneManager.LeftUIOffset, CSceneManager.RightUIOffset, CSceneManager.TopUIOffset, CSceneManager.BottomUIOffset);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_E, CSceneManager.LeftObjOffset, CSceneManager.RightObjOffset, CSceneManager.TopObjOffset, CSceneManager.BottomObjOffset);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_F, CSceneManager.LeftUIsOffset, CSceneManager.RightUIsOffset, CSceneManager.TopUIsOffset, CSceneManager.BottomUIsOffset);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_G, CSceneManager.LeftObjsOffset, CSceneManager.RightObjsOffset, CSceneManager.TopObjsOffset, CSceneManager.BottomObjsOffset);
						CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_H, stSafeArea.x, stSafeArea.y, stSafeArea.width, stSafeArea.height);

#if ADS_MODULE_ENABLE
						// 디바이스 타입이 유효 할 경우
						if(CCommonAppInfoStorage.Inst.DeviceType.ExIsValid()) {
							var stPhoneBannerAdsSize = KCDefine.U_SIZE_PHONE_BANNER_ADS;
							var stBannerAdsSize = (CCommonAppInfoStorage.Inst.DeviceType == EDeviceType.PHONE) ? stPhoneBannerAdsSize : KCDefine.U_SIZE_TABLET_BANNER_ADS;

							float fDPI = CAccess.GetDPI();
							float fBannerAdsHeight = CAccess.GetBannerAdsHeight(stBannerAdsSize.y);
							
							CSceneManager.m_oStaticDebugStringBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_I, fDPI, fBannerAdsHeight);
						}
#endif			// #if ADS_MODULE_ENABLE
					}

					// 동적 디버그 텍스트를 설정한다
					if(CSceneManager.ScreenDynamicDebugText != null) {
						var oTrans = CSceneManager.ScreenDynamicDebugText.rectTransform;
						oTrans.pivot = KCDefine.B_ANCHOR_BOTTOM_LEFT;
						oTrans.anchorMin = KCDefine.B_ANCHOR_BOTTOM_LEFT;
						oTrans.anchorMax = KCDefine.B_ANCHOR_BOTTOM_LEFT;
						oTrans.sizeDelta = new Vector2(CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y / 2.0f);
						oTrans.anchoredPosition = Vector2.zero;

						CSceneManager.m_oDynamicDebugStringBuilder.Clear();
					}
				}
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			}
			// UI 객체를 설정한다 }

			// 캔버스를 설정한다 {
#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
			a_oCanvas.worldCamera = CSceneManager.MainCamera;
#else
			// 기본 객체 캔버스 일 경우
			if(a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_BASE)) {
				a_oCanvas.worldCamera = CSceneManager.MainCamera;
			} else {
				a_oCanvas.worldCamera = (this.SubUICamera != null) ? this.SubUICamera : CSceneManager.MainCamera;
			}
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE

			bool bIsExtraCanvas = a_oCanvas != this.SubUICanvas && a_oCanvas != this.SubObjCanvas;

			// 카메라 렌더 모드 일 경우
			if(bIsExtraCanvas && a_oCanvas.renderMode == RenderMode.ScreenSpaceCamera) {
				a_oCanvas.planeDistance = this.PlaneDistance;
				a_oCanvas.sortingLayerName = KCDefine.U_SORTING_L_DEF;
			}

#if PIXELS_PERFECT_ENABLE
			a_oCanvas.pixelPerfect = true;
#else
			a_oCanvas.pixelPerfect = false;
#endif			// #if PIXELS_PERFECT_ENABLE
			// 캔버스를 설정한다 }

			// 캔버스 비율 처리자를 설정한다 {
			var oCanvasScaler = a_oCanvas.GetComponentInChildren<CanvasScaler>();
			bool bIsUICanvas = a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UI_BASE);

			// UI 비율 모드 설정이 필요 할 경우
			if(bIsUICanvas || a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_OBJ_CANVAS_BASE)) {
				oCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			}

			// 캔버스 비율 처리자 설정이 가능 할 경우
			if(oCanvasScaler != null && oCanvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize) {
				oCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				oCanvasScaler.referenceResolution = KCDefine.B_SCREEN_SIZE;
				oCanvasScaler.referencePixelsPerUnit = KCDefine.B_REF_PIXELS_UNIT;
			}
			// 캔버스 비율 처리자를 설정한다 }
		}
	}

	//! 카메라를 설정한다
	protected virtual void SetupCamera() {
		// 캔버스를 설정한다 {
		this.SubUICanvas.renderMode = RenderMode.ScreenSpaceCamera;
		this.SetupCanvas(this.SubUICanvas);

		// 객체 캔버스가 존재 할 경우
		if(this.SubObjCanvas != null) {
			this.SubObjCanvas.renderMode = RenderMode.ScreenSpaceCamera;
			this.SetupCanvas(this.SubObjCanvas);
		}
		// 캔버스를 설정한다 }

		this.SetupUICamera(this.SubUICamera);
		this.SetupMainCamera(this.SubMainCamera);
	}

	//! UI 카메라를 설정한다
	protected virtual void SetupUICamera(Camera a_oCamera, bool a_bIsResetPos = true) {
		// 카메라가 존재 할 경우
		if(a_oCamera != null) {
#if !CUSTOM_CAMERA_POS_ENABLE
			// 위치 리셋 모드 일 경우
			if(a_bIsResetPos) {
				a_oCamera.transform.position = new Vector3(KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_0, -this.PlaneDistance);
			}
#endif			// #if !CUSTOM_CAMERA_POS_ENABLE

			// 트랜스 폼을 설정한다
			a_oCamera.transform.localScale = KCDefine.B_SCALE_NORM;
			a_oCamera.transform.localEulerAngles = Vector3.zero;

			// 카메라 영역을 설정한다
			a_oCamera.farClipPlane = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE;
			a_oCamera.nearClipPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;

			// 카메라 투영을 설정한다 {
			a_oCamera.rect = KCDefine.U_RECT_UI_CAMERA;
			a_oCamera.depth = KCDefine.U_DEPTH_UI_CAMERA;

			a_oCamera.ExSetup2D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE);
			// 카메라 투영을 설정한다 }
		}
	}

	//! 메인 카메라를 설정한다
	protected virtual void SetupMainCamera(Camera a_oCamera, bool a_bIsResetPos = true) {
		// 카메라가 존재 할 경우
		if(a_oCamera != null) {
#if !CUSTOM_CAMERA_POS_ENABLE
			// 위치 리셋 모드 일 경우
			if(a_bIsResetPos) {
				a_oCamera.transform.position = new Vector3(KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_0, -this.PlaneDistance);
			}
#endif			// #if !CUSTOM_CAMERA_POS_ENABLE

			// 트랜스 폼을 설정한다 {
			a_oCamera.transform.localScale = KCDefine.B_SCALE_NORM;

#if MODE_2D_ENABLE
			a_oCamera.transform.localEulerAngles = Vector3.zero;
#endif			// #if MODE_2D_ENABLE
			// 트랜스 폼을 설정한다 }

			// 카메라 영역을 설정한다
			a_oCamera.farClipPlane = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE;
			a_oCamera.nearClipPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;

			// 카메라 투영을 설정한다 {
			a_oCamera.rect = KCDefine.U_RECT_MAIN_CAMERA;
			a_oCamera.depth = KCDefine.U_DEPTH_MAIN_CAMERA;
			
#if MODE_2D_ENABLE
			a_oCamera.ExSetup2D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE);
#else
			a_oCamera.ExSetup3D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE, this.PlaneDistance);
#endif			// #if MODE_2D_ENABLE
			// 카메라 투영을 설정한다 }
		}
	}

	//! 유일한 컴포넌트를 설정한다
	private void SetupUniqueComponents(Behaviour[] a_oComponents) {
		for(int i = 0; i < a_oComponents.Length; ++i) {
			a_oComponents[i].enabled = this.IsRootScene;
		}
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	//! 에디터 씬을 설정한다
	public void EditorSetupScene() {
#if UNIVERSAL_PIPELINE_MODULE_ENABLE
		// 퀄리티 설정이 가능 할 경우
		if(QualitySettings.renderPipeline == null || GraphicsSettings.renderPipelineAsset == null) {
			CFunc.SetupQuality(KCDefine.U_DEF_QUALITY_LEVEL, true);
		}
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE

		this.SetupScene();

		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			this.SetupOffsets();
		}
	}

	//! 스크립트 순서를 설정한다
	protected sealed override void SetupScriptOrder() {
		base.SetupScriptOrder();
		this.ExSetScriptOrder(this.ScriptOrder);
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
