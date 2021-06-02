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

#if INPUT_SYSTEM_ENABLE
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif			// #if INPUT_SYSTEM_ENABLE

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_PIPELINE_MODULE_ENABLE

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

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
#if DEBUG || DEVELOPMENT_BUILD
			this.SubTestUIs?.SetActive(true);
#else
			this.SubTestUIs?.SetActive(false);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		}
		// 씬을 설정한다 }

		// 카메라를 설정한다 {
		// 서브 UI 카메라가 존재 할 경우
		if(this.SubUIsCamera != null) {
			this.SubUIsCamera.backgroundColor = this.ClearColor;

			this.SubUIsCamera.ExSetCullingMask(KCDefine.U_LAYER_MASK_UIS_CAMERA.ToList());
			this.SubUIsCamera.gameObject.ExSetEnableComponent<AudioListener>(false);

#if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
			this.SubUIsCamera.clearFlags = CameraClearFlags.Nothing;
			this.SubUIsCamera.gameObject.SetActive(false);

#if UNIVERSAL_PIPELINE_MODULE_ENABLE
			var oCameraData = this.SubUIsCamera.gameObject.ExAddComponent<UniversalAdditionalCameraData>();

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
			this.SubUIsCamera.clearFlags = CameraClearFlags.Depth;
			this.SubUIsCamera.gameObject.SetActive(true);
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE
		}

		//! 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera && this.SubMainCamera != null) {
			this.SubMainCamera.clearFlags = this.IsRootScene ? CameraClearFlags.SolidColor : CameraClearFlags.Nothing;
			this.SubMainCamera.backgroundColor = this.ClearColor;

			this.SubMainCamera.ExSetCullingMask(KCDefine.U_LAYER_MASK_MAIN_CAMERA.ToList());

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
		this.SubPopupUIsCanvas.overrideSorting = true;
		this.SubPopupUIsCanvas.overridePixelPerfect = false;
		
		this.SubTopmostUIsCanvas.overrideSorting = true;
		this.SubTopmostUIsCanvas.overridePixelPerfect = false;

		this.SubPopupUIsCanvas.ExSetSortingOrder(new STSortingOrderInfo() {
			m_nOrder = this.UIsCanvasSortingOrderInfo.m_nOrder + sbyte.MaxValue,
			m_oLayer = this.UIsCanvasSortingOrderInfo.m_oLayer
		});

		this.SubTopmostUIsCanvas.ExSetSortingOrder(new STSortingOrderInfo() {
			m_nOrder = this.UIsCanvasSortingOrderInfo.m_nOrder + byte.MaxValue,
			m_oLayer = this.UIsCanvasSortingOrderInfo.m_oLayer
		});

		this.SubUIsCanvas.ExSetSortingOrder(this.UIsCanvasSortingOrderInfo);
		this.SubObjsCanvas?.ExSetSortingOrder(this.ObjsCanvasSortingOrderInfo);
		// 캔버스 순서를 설정한다 }
	}

	//! 루트 씬을 설정한다
	protected virtual void SetupRootScene() {
		this.SetupCamera();
		this.SetupRootObjs();

		// 이벤트 시스템을 설정한다
		CSceneManager.UIsTop.ExSetEnableComponent<EventSystem>(true);
		CSceneManager.UIsTop.ExSetEnableComponent<BaseInputModule>(true);
		
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
			if(CSceneManager.IsAppInit) {
				bool bIsTestDevice = CCommonAppInfoStorage.Inst.IsTestDevice();
				CSceneManager.ScreenDebugConsole?.SetActive(bIsTestDevice);
			}

			this.SetupCanvas(CSceneManager.ScreenDebugUIs?.GetComponentInParent<Canvas>());
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			// 캔버스를 설정한다 }
		}
	}

	//! 서브 씬을 설정한다
	protected virtual void SetupSubScene() {
		var stScene = this.gameObject.scene;

		// UI 를 설정한다 {
		this.SubUIsTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_TOP);
		this.SubUIsBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_BASE);
		
		this.SubUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.SubAnchorUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);

		this.SubUpUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_UIS);
		this.SubDownUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
		this.SubLeftUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.SubRightUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

		this.SubPopupUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.SubTopmostUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);
		// UI 를 설정한다 }

		// 객체를 설정한다 {
		this.SubBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.SubObjsBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_BASE);

		this.SubObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.SubAnchorObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);

		this.SubUpObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_OBJS);
		this.SubDownObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_OBJS);
		this.SubLeftObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.SubRightObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		// 객체를 설정한다 }

		// 캔버스 객체를 설정한다 {
		this.SubObjsCanvasTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_TOP);
		this.SubObjsCanvasBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_BASE);

		this.SubCanvasObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);
		// 캔버스 객체를 설정한다 }

		// 카메라를 설정한다
		this.SubUIsCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UIS_CAMERA);
		this.SubMainCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		// 캔버스를 설정한다 {
		this.SubUIsCanvas = this.SubUIsBase.GetComponentInChildren<Canvas>();
		this.SubPopupUIsCanvas = this.SubPopupUIs.GetComponentInChildren<Canvas>();
		this.SubTopmostUIsCanvas = this.SubTopmostUIs.GetComponentInChildren<Canvas>();

		this.SubObjsCanvas = this.SubObjsCanvasBase?.GetComponentInChildren<Canvas>();
		// 캔버스를 설정한다 }

		// 루트 객체를 설정한다
		this.SetupCamera();
		this.SetupRootObjs();

		// 고유 객체를 설정한다 {
		var oObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			var oEventSystems = oObjs[i].GetComponentsInChildren<EventSystem>(true);
			var oInputModules = oObjs[i].GetComponentsInChildren<BaseInputModule>(true);
			var oAudioListeners = oObjs[i].GetComponentsInChildren<AudioListener>(true);

			this.SetupUniqueComponents(oEventSystems);
			this.SetupUniqueComponents(oInputModules);
			this.SetupUniqueComponents(oAudioListeners);
		}
		// 고유 객체를 설정한다 }
	}

	//! 광원을 설정한다
	protected virtual void SetupLights() {
		var oObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			var oLights = oObjs[i].GetComponentsInChildren<Light>(true);

			for(int j = 0; j < oLights.Length; ++j) {
				bool bIsMainLight = oLights[j].name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_MAIN_LIGHT);
				bool bIsDirectional = oLights[j].type == LightType.Directional;
				
				oLights[j].shadows = KCDefine.U_LIGHT_SHADOW_TYPE;

				// 메인 방향 광원 일 경우
				if(bIsMainLight && bIsDirectional) {
					oLights[j].transform.localScale = KCDefine.B_SCALE_NORM;
					oLights[j].transform.localEulerAngles = KCDefine.U_ANGLE_MAIN_LIGHT;
					oLights[j].transform.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, -this.PlaneDistance);
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
		if(CSceneManager.UIsCanvas != null && CSceneManager.UIsCanvas.renderMode == RenderMode.ScreenSpaceCamera) {
			var oTrans = CSceneManager.UIsCanvas.transform as RectTransform;
			
			CSceneManager.CanvasSize = oTrans.sizeDelta.ExTo3D();
			CSceneManager.CanvasScale = oTrans.localScale;
		}

		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			CSceneManager.UpUIsOffset = CSceneManager.CanvasSize.y * -CAccess.UpScreenScale;
			CSceneManager.UpObjOffset = CSceneManager.CanvasSize.y * -CAccess.UpScreenScale;

			CSceneManager.DownUIsOffset = CSceneManager.CanvasSize.y * CAccess.DownScreenScale;
			CSceneManager.DownObjOffset = CSceneManager.CanvasSize.y * CAccess.DownScreenScale;

			CSceneManager.LeftUIsOffset = CSceneManager.CanvasSize.x * CAccess.LeftScreenScale;
			CSceneManager.LeftObjOffset = CSceneManager.CanvasSize.x * CAccess.LeftScreenScale;

			CSceneManager.RightUIsOffset = CSceneManager.CanvasSize.x * -CAccess.RightScreenScale;
			CSceneManager.RightObjOffset = CSceneManager.CanvasSize.x * -CAccess.RightScreenScale;
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

		this.SubUIsTop.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubUIsTop.transform.localEulerAngles = Vector3.zero;
		this.SubUIsTop.transform.localPosition = Vector3.zero;

		this.SubObjsBase.transform.localScale = CSceneManager.CanvasScale;
		this.SubObjsBase.transform.localEulerAngles = Vector3.zero;
		this.SubObjsBase.transform.localPosition = Vector3.zero;

		this.SubObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubObjs.transform.localEulerAngles = Vector3.zero;
		this.SubObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);

		this.SubAnchorObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubAnchorObjs.transform.localEulerAngles = Vector3.zero;
		this.SubAnchorObjs.transform.localPosition = Vector3.zero;

		this.SubUpObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubUpObjs.transform.localEulerAngles = Vector3.zero;
		this.SubUpObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT) + CSceneManager.UpObjOffset, KCDefine.B_VAL_0_FLT);

		this.SubDownObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubDownObjs.transform.localEulerAngles = Vector3.zero;
		this.SubDownObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + CSceneManager.DownObjOffset, KCDefine.B_VAL_0_FLT);

		this.SubLeftObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubLeftObjs.transform.localEulerAngles = Vector3.zero;
		this.SubLeftObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT) + CSceneManager.LeftObjOffset, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);

		this.SubRightObjs.transform.localScale = KCDefine.B_SCALE_NORM;
		this.SubRightObjs.transform.localEulerAngles = Vector3.zero;
		this.SubRightObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT) + CSceneManager.RightObjOffset, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);

		this.SubUIs.transform.localPosition = Vector3.zero;
		this.SubPopupUIs.transform.localPosition = Vector3.zero;
		this.SubTopmostUIs.transform.localPosition = Vector3.zero;
		
		this.SubObjs.transform.localPosition = Vector3.zero;
		this.SubCanvasObjs.transform.localPosition = Vector3.zero;

		this.SubUpObjs.ExSetLocalPosX(KCDefine.B_VAL_0_FLT);
		this.SubDownObjs.ExSetLocalPosX(KCDefine.B_VAL_0_FLT);

		this.SubLeftObjs.ExSetLocalPosY(KCDefine.B_VAL_0_FLT);
		this.SubRightObjs.ExSetLocalPosY(KCDefine.B_VAL_0_FLT);

		// 서브 탑 객체 캔버스가 존재 할 경우
		if(this.SubObjsCanvasTop != null) {
			this.SubObjsCanvasTop.transform.localScale = KCDefine.B_SCALE_NORM;
			this.SubObjsCanvasTop.transform.localEulerAngles = Vector3.zero;
			this.SubObjsCanvasTop.transform.localPosition = Vector3.zero;
		}

		// 루트 객체 간격을 설정한다 {
		var oUIsTrans = CSceneManager.UIs.transform as RectTransform;
		var oUpUIsTrans = CSceneManager.UpUIs.transform as RectTransform;
		var oDownUIsTrans = CSceneManager.DownUIs.transform as RectTransform;
		var oLeftUIsTrans = CSceneManager.LeftUIs.transform as RectTransform;
		var oRightUIsTrans = CSceneManager.RightUIs.transform as RectTransform;

		CSceneManager.UpUIsRootOffset = (oUIsTrans.anchoredPosition.y + KCDefine.B_SCREEN_HEIGHT) - oUpUIsTrans.anchoredPosition.y;
		CSceneManager.DownUIsRootOffset = oUIsTrans.anchoredPosition.y - oDownUIsTrans.anchoredPosition.y;
		CSceneManager.LeftUIsRootOffset = oUIsTrans.anchoredPosition.x - oLeftUIsTrans.anchoredPosition.x;
		CSceneManager.RightUIsRootOffset = (oUIsTrans.anchoredPosition.x + KCDefine.B_SCREEN_WIDTH) - oRightUIsTrans.anchoredPosition.x;

		CSceneManager.UpObjsRootOffset = (CSceneManager.Objs.transform.localPosition.y + KCDefine.B_SCREEN_HEIGHT) - CSceneManager.UpObjs.transform.localPosition.y;
		CSceneManager.DownObjsRootOffset = CSceneManager.Objs.transform.localPosition.y - CSceneManager.DownObjs.transform.localPosition.y;
		CSceneManager.LeftObjsRootOffset = CSceneManager.Objs.transform.localPosition.x - CSceneManager.LeftObjs.transform.localPosition.x;
		CSceneManager.RightObjsRootOffset = (CSceneManager.Objs.transform.localPosition.x + KCDefine.B_SCREEN_WIDTH) - CSceneManager.RightObjs.transform.localPosition.x;
		// 루트 객체 간격을 설정한다 }
	}

	//! 기본 객체를 설정한다
	protected virtual void SetupDefObjs() {
		// 씬 관리자를 설정한다
		CSceneManager.m_oSubSceneManagerList.ExAddVal(this.SceneName, this);
		CSceneManager.RootSceneManager = CSceneManager.RootScene.ExFindComponent<CSceneManager>(KCDefine.U_OBJ_N_SCENE_MANAGER);

		var stScene = CSceneManager.RootSceneManager.gameObject.scene;

		// UI 를 설정한다 {
		this.SubUIsTop = CSceneManager.UIsTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_TOP);
		this.SubUIsBase = CSceneManager.UIsBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_BASE);

		this.SubUIs = CSceneManager.UIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.SubAnchorUIs = CSceneManager.AnchorUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);

		this.SubUpUIs = CSceneManager.UpUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_UIS);
		this.SubDownUIs = CSceneManager.DownUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
		this.SubLeftUIs = CSceneManager.LeftUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.SubRightUIs = CSceneManager.RightUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

		this.SubPopupUIs = CSceneManager.PopupUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.SubTopmostUIs = CSceneManager.TopmostUIs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);

		this.SubTestUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TEST_UIS);
		// UI 를 설정한다 }

		// 객체를 설정한다 {
		this.SubBase = CSceneManager.Base = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.SubObjsBase = CSceneManager.ObjsBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_BASE);
		this.SubObjs = CSceneManager.Objs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.SubAnchorObjs = CSceneManager.AnchorObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);

		this.SubUpObjs = CSceneManager.UpObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_OBJS);
		this.SubDownObjs = CSceneManager.DownObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_OBJS);
		this.SubLeftObjs = CSceneManager.LeftObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.SubRightObjs = CSceneManager.RightObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		// 객체를 설정한다 }

		// 캔버스 객체를 설정한다 {
		this.SubObjsCanvasTop = CSceneManager.ObjsCanvasTop = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_TOP);
		this.SubObjsCanvasBase = CSceneManager.ObjsCanvasBase = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_BASE);

		this.SubCanvasObjs = CSceneManager.CanvasObjs = stScene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);
		// 캔버스 객체를 설정한다 }

		// 카메라를 설정한다
		this.SubUIsCamera = CSceneManager.UIsCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UIS_CAMERA);
		this.SubMainCamera = CSceneManager.MainCamera = stScene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		// 캔버스를 설정한다 {
		this.SubUIsCanvas = CSceneManager.UIsCanvas = CSceneManager.UIsBase.GetComponentInChildren<Canvas>();
		this.SubPopupUIsCanvas = CSceneManager.PopupUIsCanvas = CSceneManager.PopupUIs.GetComponentInChildren<Canvas>();
		this.SubTopmostUIsCanvas = CSceneManager.TopmostUIsCanvas = CSceneManager.TopmostUIs.GetComponentInChildren<Canvas>();
		
		this.SubObjsCanvas = CSceneManager.ObjsCanvas = CSceneManager.ObjsCanvasBase?.GetComponentInChildren<Canvas>();
		// 캔버스를 설정한다 }

		// 기본 객체를 설정한다 {
		CSceneManager.EventSystem = CSceneManager.UIsTop.GetComponentInChildren<EventSystem>();
		CSceneManager.BaseInputModule = CSceneManager.EventSystem.GetComponentInChildren<BaseInputModule>();

#if INPUT_SYSTEM_ENABLE
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
#endif			// #if INPUT_SYSTEM_ENABLE
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

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS),

				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_POPUP_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS),
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_ABS_UIS),

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
				oObj.ExFindChild(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS)
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			};

			for(int i = 0; i < oUIObjs.Length; ++i) {
				// UI 객체가 존재 할 경우
				if(oUIObjs[i] != null) {
					string oName = oUIObjs[i].name;

					var stPos = Vector3.zero;
					var stSize = Vector3.zero;
					var stPivot = KCDefine.B_ANCHOR_MIDDLE_CENTER;

					bool bIsUpUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UP_UIS);
					bool bIsDownUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
					bool bIsLeftUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
					bool bIsRightUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

					// 상단, 하단 UI 루트 일 경우
					if(bIsUpUIs || bIsDownUIs) {
						stSize = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);

						// 상단 UI 루트 일 경우
						if(bIsUpUIs) {
							stPos = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT) + CSceneManager.UpUIsOffset, KCDefine.B_VAL_0_FLT);
							stPivot = KCDefine.B_ANCHOR_UP_LEFT;
						} else {
							stPos = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + CSceneManager.DownUIsOffset, KCDefine.B_VAL_0_FLT);
							stPivot = KCDefine.B_ANCHOR_DOWN_LEFT;
						}
					} 
					// 왼쪽, 오른쪽 UI 루트 일 경우
					else if(bIsLeftUIs || bIsRightUIs) {
						stSize = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_SCREEN_HEIGHT, KCDefine.B_VAL_0_FLT);

						// 왼쪽 UI 루트 일 경우
						if(bIsLeftUIs) {
							stPos = new Vector3((CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT) + CSceneManager.LeftUIsOffset, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
							stPivot = KCDefine.B_ANCHOR_DOWN_LEFT;
						} else {
							stPos = new Vector3((CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT) + CSceneManager.RightUIsOffset, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
							stPivot = KCDefine.B_ANCHOR_DOWN_RIGHT;
						}
					} else {
						bool bIsUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UIS);
						bool bIsAnchorUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);
						bool bIsBlindUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS);
						bool bIsCanvasObjs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
						bool bIsDebugUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS);
#else
						bool bIsDebugUIs = false;
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

						// 크기 보정 가능한 UI 객체 일 경우
						if(bIsUIs || bIsAnchorUIs || bIsBlindUIs || bIsCanvasObjs || bIsDebugUIs) {
							// UI 루트 일 경우
							if(bIsUIs || bIsCanvasObjs) {
								stSize = KCDefine.B_SCREEN_SIZE;
							} else {
								stSize = CSceneManager.CanvasSize;
							}
						} else {
							bool bIsScreenPopupUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_POPUP_UIS);
							bool bIsScreenTopmostUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS);
							bool bIsScreenAbsUIs = oName.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_ABS_UIS);

							// 화면 UI 객체 루트가 아닐 경우
							if(!bIsScreenPopupUIs && !bIsScreenTopmostUIs && !bIsScreenAbsUIs) {
								stPos = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
							}
						}
					}

					var oTrans = oUIObjs[i].transform as RectTransform;
					oTrans.pivot = stPivot;
					oTrans.anchorMin = KCDefine.B_ANCHOR_MIDDLE_CENTER;
					oTrans.anchorMax = KCDefine.B_ANCHOR_MIDDLE_CENTER;

					oTrans.sizeDelta = stSize.ExTo2D();
					oTrans.anchoredPosition = stPos.ExTo2D();
					oTrans.localEulerAngles = Vector3.zero;
				}

				// 블라인드 UI 루트 일 경우
				if(oUIObjs[i] != null && oUIObjs[i].name.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS)) {
					var oPivots = new Vector3[] {
						KCDefine.B_ANCHOR_MIDDLE_RIGHT.ExTo3D(),
						KCDefine.B_ANCHOR_MIDDLE_LEFT.ExTo3D(),
						KCDefine.B_ANCHOR_DOWN_CENTER.ExTo3D(),
						KCDefine.B_ANCHOR_UP_CENTER.ExTo3D()
					};

					var oAnchors = new Vector3[] {
						KCDefine.B_ANCHOR_MIDDLE_LEFT.ExTo3D(),
						KCDefine.B_ANCHOR_MIDDLE_RIGHT.ExTo3D(),
						KCDefine.B_ANCHOR_UP_CENTER.ExTo3D(),
						KCDefine.B_ANCHOR_DOWN_CENTER.ExTo3D()
					};

					var oPoses = new Vector3[] {
						Vector3.zero,
						Vector3.zero,
						Vector3.zero,
						new Vector3(KCDefine.B_VAL_0_FLT, CSceneManager.DownUIsOffset, KCDefine.B_VAL_0_FLT)
					};

					var stUpOffset = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
					var stDownOffset = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
					var stLeftOffset = new Vector3((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
					var stRightOffset = new Vector3((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);

					var oOffsets = new Vector3[] {
						this.IsIgnoreBlindV ? Vector3.zero : stUpOffset,
						this.IsIgnoreBlindV ? Vector3.zero : stDownOffset,
						this.IsIgnoreBlindH ? Vector3.zero : stLeftOffset,
						this.IsIgnoreBlindH ? Vector3.zero : stRightOffset
					};

					int nIdx = oOffsets.Length - KCDefine.B_VAL_1_INT;
					oOffsets[nIdx].y = Mathf.Max(KCDefine.B_VAL_0_FLT, oOffsets[oOffsets.Length - KCDefine.B_VAL_1_INT].y - CSceneManager.DownUIsOffset);

					var oImgs = new Image[] {
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_UP_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_DOWN_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_LEFT_BLIND_IMG),
						oUIObjs[i].ExFindComponent<Image>(KCDefine.U_OBJ_N_RIGHT_BLIND_IMG)
					};

					for(int j = 0; j < oImgs.Length; ++j) {
						var oImg = oImgs[j];
						oImg.color = CAccess.IsEditor ? KCDefine.U_COLOR_TRANSPARENT : KCDefine.U_COLOR_BLIND_UIS;
						oImg.rectTransform.pivot = oPivots[j];
						oImg.rectTransform.anchorMin = oAnchors[j];
						oImg.rectTransform.anchorMax = oAnchors[j];
						oImg.rectTransform.sizeDelta = CSceneManager.CanvasSize.ExTo2D();
						oImg.rectTransform.anchoredPosition = oPoses[j].ExTo2D() + oOffsets[j].ExTo2D();
					}
				}

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
				// 디버그 UI 루트 일 경우
				if(oUIObjs[i] != null && oUIObjs[i].name.ExIsEquals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS)) {
					// 정적 디버그 텍스트를 설정한다
					if(CSceneManager.ScreenStaticDebugText != null) {
						var oTrans = CSceneManager.ScreenStaticDebugText.rectTransform;
						oTrans.pivot = KCDefine.B_ANCHOR_UP_LEFT;
						oTrans.anchorMin = KCDefine.B_ANCHOR_UP_LEFT;
						oTrans.anchorMax = KCDefine.B_ANCHOR_UP_LEFT;
						oTrans.sizeDelta = new Vector2(CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT);
						oTrans.anchoredPosition = Vector2.zero;

						CSceneManager.m_oStaticDebugStrBuilder.Clear();
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_A, CAccess.ScreenSize.x, CAccess.ScreenSize.y);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_B, KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_C, CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_D, CSceneManager.UpUIsOffset, CSceneManager.DownUIsOffset, CSceneManager.LeftUIsOffset, CSceneManager.RightUIsOffset);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_E, CSceneManager.UpObjOffset, CSceneManager.DownObjOffset, CSceneManager.LeftObjOffset, CSceneManager.RightObjOffset);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_F, CSceneManager.UpUIsRootOffset, CSceneManager.DownUIsRootOffset, CSceneManager.LeftUIsRootOffset, CSceneManager.RightUIsRootOffset);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_G, CSceneManager.UpObjsRootOffset, CSceneManager.DownObjsRootOffset, CSceneManager.LeftObjsRootOffset, CSceneManager.RightObjsRootOffset);
						CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_H, CAccess.SafeArea.x, CAccess.SafeArea.y, CAccess.SafeArea.width, CAccess.SafeArea.height);

#if ADS_MODULE_ENABLE
						// 디바이스 타입이 유효 할 경우
						if(CCommonAppInfoStorage.Inst.DeviceType.ExIsValid()) {
							var stBannerAdsSize = (CCommonAppInfoStorage.Inst.DeviceType == EDeviceType.PHONE) ? KCDefine.U_SIZE_PHONE_BANNER_ADS : KCDefine.U_SIZE_TABLET_BANNER_ADS;
							float fBannerAdsHeight = CAccess.GetBannerAdsHeight(stBannerAdsSize.y);
							
							CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_FMT_STATIC_DEBUG_INFO_I, CAccess.DPI, fBannerAdsHeight);
						}
#endif			// #if ADS_MODULE_ENABLE
					}

					// 동적 디버그 텍스트를 설정한다
					if(CSceneManager.ScreenDynamicDebugText != null) {
						var oTrans = CSceneManager.ScreenDynamicDebugText.rectTransform;
						oTrans.pivot = KCDefine.B_ANCHOR_DOWN_LEFT;
						oTrans.anchorMin = KCDefine.B_ANCHOR_DOWN_LEFT;
						oTrans.anchorMax = KCDefine.B_ANCHOR_DOWN_LEFT;
						oTrans.sizeDelta = new Vector2(CSceneManager.CanvasSize.x, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT);
						oTrans.anchoredPosition = Vector2.zero;

						CSceneManager.m_oDynamicDebugStrBuilder.Clear();
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
			if(a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_BASE)) {
				a_oCanvas.worldCamera = CSceneManager.MainCamera;
			} else {
				a_oCanvas.worldCamera = (this.SubUIsCamera != null) ? this.SubUIsCamera : CSceneManager.MainCamera;
			}
#endif			// #if !CAMERA_STACK_ENABLE || UNIVERSAL_PIPELINE_MODULE_ENABLE

			bool bIsExtraCanvas = a_oCanvas != this.SubUIsCanvas && a_oCanvas != this.SubObjsCanvas;

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
			bool bIsUIsCanvas = a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UIS_BASE);

			// UI 비율 모드 설정이 필요 할 경우
			if(bIsUIsCanvas || a_oCanvas.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_OBJS_CANVAS_BASE)) {
				oCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			}

			// 캔버스 비율 처리자 설정이 가능 할 경우
			if(oCanvasScaler != null && oCanvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize) {
				oCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				oCanvasScaler.referenceResolution = KCDefine.B_SCREEN_SIZE.ExTo2D();
				oCanvasScaler.referencePixelsPerUnit = KCDefine.B_UNIT_REF_PIXELS;
			}
			// 캔버스 비율 처리자를 설정한다 }
		}
	}

	//! 카메라를 설정한다
	protected virtual void SetupCamera() {
		// 캔버스를 설정한다 {
		this.SubUIsCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		this.SetupCanvas(this.SubUIsCanvas);

		// 객체 캔버스가 존재 할 경우
		if(this.SubObjsCanvas != null) {
			this.SubObjsCanvas.renderMode = RenderMode.ScreenSpaceCamera;
			this.SetupCanvas(this.SubObjsCanvas);
		}
		// 캔버스를 설정한다 }

		this.SetupUIsCamera(this.SubUIsCamera);
		this.SetupMainCamera(this.SubMainCamera);
	}

	//! UI 카메라를 설정한다
	protected virtual void SetupUIsCamera(Camera a_oCamera, bool a_bIsResetPos = true) {
		// 카메라가 존재 할 경우
		if(a_oCamera != null) {
#if !CUSTOM_CAMERA_POS_ENABLE
			// 위치 리셋 모드 일 경우
			if(a_bIsResetPos) {
				a_oCamera.transform.position = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, -this.PlaneDistance);
			}
#endif			// #if !CUSTOM_CAMERA_POS_ENABLE

			// 트랜스 폼을 설정한다
			a_oCamera.transform.localScale = KCDefine.B_SCALE_NORM;
			a_oCamera.transform.localEulerAngles = Vector3.zero;

			// 카메라 영역을 설정한다
			a_oCamera.farClipPlane = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE;
			a_oCamera.nearClipPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;

			// 카메라 투영을 설정한다 {
			a_oCamera.rect = KCDefine.U_RECT_UIS_CAMERA;
			a_oCamera.depth = KCDefine.U_DEPTH_UIS_CAMERA;

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
				a_oCamera.transform.position = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, -this.PlaneDistance);
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

	//! 고유 컴포넌트를 설정한다
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
			CFunc.SetupQuality(KCDefine.U_QUALITY_LEVEL, true);
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
