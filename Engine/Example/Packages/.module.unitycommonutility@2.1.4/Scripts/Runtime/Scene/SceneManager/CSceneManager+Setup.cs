using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem.UI;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

#if POST_PROCESSING_MODULE_ENABLE
using UnityEngine.Rendering.PostProcessing;
#endif			// #if POST_PROCESSING_MODULE_ENABLE

/** 씬 관리자 - 설정 */
public abstract partial class CSceneManager : CComponent {
	#region 함수
	/** 씬을 설정한다 */
	protected virtual void SetupScene(bool a_bIsMainSetup) {
		// 씬을 설정한다 {
		this.SetupLights();
		this.SetupDefObjs();
		this.SetupOffsets();

		// 주요 씬 일 경우
		if(this.IsActiveScene) {
			this.SetupActiveScene();
		} else {
			this.SetupAdditiveScene();
		}

		this.gameObject.ExReset();

		// 메인 설정이 일 경우
		if(a_bIsMainSetup) {
			// 캔버스 순서를 설정한다 {
			this.PopupUIsCanvas.overrideSorting = true;
			this.PopupUIsCanvas.overridePixelPerfect = false;
			
			this.TopmostUIsCanvas.overrideSorting = true;
			this.TopmostUIsCanvas.overridePixelPerfect = false;

			this.PopupUIsCanvas.ExSetSortingOrder(new STSortingOrderInfo() {
				m_nOrder = this.UIsCanvasSortingOrderInfo.m_nOrder + sbyte.MaxValue, m_oLayer = this.UIsCanvasSortingOrderInfo.m_oLayer
			});

			this.TopmostUIsCanvas.ExSetSortingOrder(new STSortingOrderInfo() {
				m_nOrder = this.UIsCanvasSortingOrderInfo.m_nOrder + byte.MaxValue, m_oLayer = this.UIsCanvasSortingOrderInfo.m_oLayer
			});

			this.UIsCanvas.ExSetSortingOrder(this.UIsCanvasSortingOrderInfo);
			// 캔버스 순서를 설정한다 }
			
			// 카메라를 설정한다 {
			// UI 카메라가 존재 할 경우
			if(this.UIsCamera != null) {
				this.UIsCamera.clearFlags = CameraClearFlags.Depth;
				this.UIsCamera.backgroundColor = this.ClearColor;

				this.UIsCamera.ExReset();
				this.UIsCamera.ExSetCullingMask(KCDefine.U_LAYER_MASKS_UIS_CAMERA);

				this.UIsCamera.gameObject.SetActive(true);
				this.UIsCamera.gameObject.ExSetEnableComponent<AudioListener>(false, false);

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
				var oCameraData = this.UIsCamera.gameObject.ExAddComponent<UniversalAdditionalCameraData>();

				// 카메라 데이터가 존재 할 경우
				if(oCameraData != null && QualitySettings.renderPipeline != null && GraphicsSettings.renderPipelineAsset != null) {
					oCameraData.renderShadows = false;
					oCameraData.renderPostProcessing = false;
					oCameraData.renderType = CameraRenderType.Overlay;

					this.SetupCameraData(oCameraData);
					oCameraData.ExSetRuntimeFieldVal<UniversalAdditionalCameraData>(KCDefine.U_FIELD_N_CLEAR_DEPTH, true);
				}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

#if POST_PROCESSING_MODULE_ENABLE
				var oPostProcessingVolume = this.UIsCamera.GetComponentInChildren<PostProcessVolume>();
				oPostProcessingVolume?.ExSetEnable(false, false);

				// 포스트 프로세싱 볼륨이 존재 할 경우
				if(oPostProcessingVolume != null && oPostProcessingVolume.sharedProfile != null) {
					oPostProcessingVolume.sharedProfile = null;
				}
#endif			// #if POST_PROCESSING_MODULE_ENABLE
			}

			// 메인 카메라가 존재 할 경우
			if(this.MainCamera != null) {
				this.MainCamera.clearFlags = this.IsActiveScene ? CameraClearFlags.SolidColor : CameraClearFlags.Nothing;
				this.MainCamera.backgroundColor = this.ClearColor;

				this.MainCamera.ExReset();
				this.MainCamera.ExSetCullingMask(KCDefine.U_LAYER_MASKS_MAIN_CAMERA);
				
				this.MainCamera.gameObject.SetActive(this.IsActiveScene);
				this.MainCamera.gameObject.ExSetEnableComponent<AudioListener>(this.IsActiveScene, false);

#if PHYSICS_RAYCASTER_ENABLE
#if MODE_2D_ENABLE
				var oRaycaster = this.MainCamera.gameObject.ExAddComponent<Physics2DRaycaster>();
#else
				var oRaycaster = this.MainCamera.gameObject.ExAddComponent<PhysicsRaycaster>();
#endif			// #if MODE_2D_ENABLE

				oRaycaster.ExSetEventMask(KCDefine.U_LAYER_MASKS_MAIN_CAMERA);
#endif			// #if PHYSICS_RAYCASTER_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
				var oCameraData = this.MainCamera.gameObject.ExAddComponent<UniversalAdditionalCameraData>();
				CSceneManager.m_oActiveSceneMainCameraData = CSceneManager.ActiveSceneMainCamera?.GetComponent<UniversalAdditionalCameraData>();

				// 카메라 데이터가 존재 할 경우
				if(oCameraData != null && QualitySettings.renderPipeline != null && GraphicsSettings.renderPipelineAsset != null) {
					oCameraData.renderShadows = true;
					oCameraData.renderType = CameraRenderType.Base;

#if POST_PROCESSING_MODULE_ENABLE
					oCameraData.renderPostProcessing = this.IsActiveScene && !this.IsIgnoreRenderPostProcessing;
#else
					oCameraData.renderPostProcessing = false;
#endif			// #if POST_PROCESSING_MODULE_ENABLE

					oCameraData.cameraStack?.Clear();
					this.SetupCameraData(oCameraData);
				}

				try {
					// 액티브 씬 메인 카메라 데이터가 존재 할 경우
					if(this.UIsCamera != null && CSceneManager.m_oActiveSceneMainCameraData != null && QualitySettings.renderPipeline != null && GraphicsSettings.renderPipelineAsset != null) {
						var oCameraList = CSceneManager.m_oActiveSceneMainCameraData.cameraStack ?? new List<Camera>();
						oCameraList.ExAddVals(this.OverlayCameraList);
						oCameraList.Sort((a_oLhs, a_oRhs) => a_oLhs.depth.CompareTo(a_oRhs.depth));
					}
				} catch(System.Exception oException) {
					CFunc.ShowLogWarning($"CSceneManager.SetupScene Exception: {oException.Message}");
				}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

#if POST_PROCESSING_MODULE_ENABLE
				var oPostProcessingVolume = this.MainCamera.gameObject.ExAddComponent<PostProcessVolume>();
				oPostProcessingVolume?.ExSetEnable(this.IsActiveScene && !this.IsIgnoreGlobalPostProcessingVolume, false);

				// 포스트 프로세싱 볼륨이 존재 할 경우
				if(oPostProcessingVolume != null && CSceneManager.QualityLevel != EQualityLevel.NONE) {
					var oPostProcessingSettingsPathDict = new Dictionary<EQualityLevel, string>() {
						[EQualityLevel.NORM] = KCDefine.U_ASSET_P_G_NORM_QUALITY_POST_PROCESSING_SETTINGS,
						[EQualityLevel.HIGH] = KCDefine.U_ASSET_P_G_HIGH_QUALITY_POST_PROCESSING_SETTINGS,
						[EQualityLevel.ULTRA] = KCDefine.U_ASSET_P_G_ULTRA_QUALITY_POST_PROCESSING_SETTINGS
					};

					bool bIsValidA = oPostProcessingVolume.sharedProfile != null;
					bool bIsValidB = oPostProcessingVolume.sharedProfile != null && !oPostProcessingVolume.sharedProfile.name.Equals(Path.GetFileNameWithoutExtension(oPostProcessingSettingsPathDict[CSceneManager.QualityLevel]));

					var oResult = oPostProcessingSettingsPathDict.ExFindVal((a_oPostProcessingSettingsPath) => oPostProcessingVolume.sharedProfile != null && Path.GetFileNameWithoutExtension(a_oPostProcessingSettingsPath).Equals(oPostProcessingVolume.sharedProfile.name));

					oPostProcessingVolume.isGlobal = true;
					oPostProcessingVolume.weight = KCDefine.B_VAL_1_FLT;
					oPostProcessingVolume.priority = KCDefine.B_VAL_0_FLT;
					
					// 포스트 프로세싱 설정이 없을 경우
					if((!bIsValidA || (bIsValidB && oResult.Item1)) && CAccess.IsExistsRes<PostProcessProfile>(oPostProcessingSettingsPathDict[CSceneManager.QualityLevel], true)) {
						oPostProcessingVolume.sharedProfile = Resources.Load<PostProcessProfile>(oPostProcessingSettingsPathDict[CSceneManager.QualityLevel]);
					}
				}
#endif			// #if POST_PROCESSING_MODULE_ENABLE
			}
			// 카메라를 설정한다 }
		}

		// 앱이 실행 중 일 경우
		if(a_bIsMainSetup && Application.isPlaying) {
			Canvas.ForceUpdateCanvases();
		}
	}

	/** 주요 씬을 설정한다 */
	protected virtual void SetupActiveScene() {
		this.SetupCamera();
		this.SetupRootObjs();

		// 이벤트 시스템을 설정한다
		CSceneManager.ActiveSceneUIsTop.ExSetEnableComponent<EventSystem>(true, false);
		CSceneManager.ActiveSceneUIsTop.ExSetEnableComponent<BaseInputModule>(true, false);

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			// 주요 씬 일 경우
			if(this.IsActiveScene) {
				CScheduleManager.Inst.AddComponent(this);
				CNavStackManager.Inst.AddComponent(this);

				this.StartScreenFadeOutAni(this.FadeOutAniDuration);
			}

			// 캔버스를 설정한다 {
			this.SetupCanvas(CSceneManager.ScreenBlindUIs?.GetComponentInParent<Canvas>(), false);
			this.SetupCanvas(CSceneManager.ScreenPopupUIs?.GetComponentInParent<Canvas>(), false);
			this.SetupCanvas(CSceneManager.ScreenTopmostUIs?.GetComponentInParent<Canvas>(), false);
			this.SetupCanvas(CSceneManager.ScreenAbsUIs?.GetComponentInParent<Canvas>(), false);

#if DEBUG || DEVELOPMENT_BUILD
			this.SetupCanvas(CSceneManager.ScreenDebugUIs?.GetComponentInParent<Canvas>(), false);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
			// 캔버스를 설정한다 }

			// 디버그 버튼을 설정한다 {
#if DEBUG || DEVELOPMENT_BUILD
			CSceneManager.ScreenFPSInfoBtn?.gameObject.SetActive(true);
			CSceneManager.ScreenFPSInfoBtn?.ExAddListener(CSceneManager.OnTouchScreenFPSInfoBtn);

			CSceneManager.ScreenDebugInfoBtn?.gameObject.SetActive(true);
			CSceneManager.ScreenDebugInfoBtn?.ExAddListener(CSceneManager.OnTouchScreenDebugInfoBtn);

			CSceneManager.ScreenTimeScaleUpBtn?.gameObject.SetActive(true);
			CSceneManager.ScreenTimeScaleUpBtn?.ExAddListener(CSceneManager.OnTouchScreenTimeScaleUpBtn);

			CSceneManager.ScreenTimeScaleDownBtn?.gameObject.SetActive(true);
			CSceneManager.ScreenTimeScaleDownBtn?.ExAddListener(CSceneManager.OnTouchScreenTimeScaleDownBtn);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
			// 디버그 버튼을 설정한다 }
		}
	}

	/** 추가 씬을 설정한다 */
	protected virtual void SetupAdditiveScene() {
		// UI 를 설정한다 {
		this.UIsTop = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_TOP);
		this.UIsBase = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_BASE);
		
		this.UIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.PivotUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_PIVOT_UIS);
		this.AnchorUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);
		this.CornerAnchorUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CORNER_ANCHOR_UIS);

		this.UpUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_UIS);
		this.DownUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
		this.LeftUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.RightUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

		this.UpLeftUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_LEFT_UIS);
		this.UpRightUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_RIGHT_UIS);
		this.DownLeftUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_LEFT_UIS);
		this.DownRightUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_RIGHT_UIS);

		this.PopupUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.TopmostUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);
		// UI 를 설정한다 }

		// 객체를 설정한다 {
		this.Base = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.ObjsBase = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_BASE);

		this.Objs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.PivotObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_PIVOT_OBJS);
		this.AnchorObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);
		this.StaticObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_STATIC_OBJS);
		this.AdditionalLights = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ADDITIONAL_LIGHTS);
		this.ReflectionProbes = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_REFLECTION_PROBES);
		this.LightProbeGroups = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LIGHT_PROBE_GROUPS);
		
		this.UpObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_OBJS);
		this.DownObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_OBJS);
		this.LeftObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.RightObjs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		// 객체를 설정한다 }

		// 카메라를 설정한다
		this.UIsCamera = this.gameObject.scene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UIS_CAMERA);
		this.MainCamera = this.gameObject.scene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		// 캔버스를 설정한다
		this.UIsCanvas = this.UIsBase.GetComponentInChildren<Canvas>();
		this.PopupUIsCanvas = this.PopupUIs.GetComponentInChildren<Canvas>();
		this.TopmostUIsCanvas = this.TopmostUIs.GetComponentInChildren<Canvas>();

		// 루트 객체를 설정한다
		this.SetupCamera();
		this.SetupRootObjs();

		// 고유 객체를 설정한다 {
		var oObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oObjs.Length; ++i) {
			var oEventSystems = oObjs[i].GetComponentsInChildren<EventSystem>(true);
			var oInputModules = oObjs[i].GetComponentsInChildren<BaseInputModule>(true);
			var oAudioListeners = oObjs[i].GetComponentsInChildren<AudioListener>(true);

			this.SetupUniqueComponents(oEventSystems.ExToTypes<Behaviour>(), false);
			this.SetupUniqueComponents(oInputModules.ExToTypes<Behaviour>(), false);
			this.SetupUniqueComponents(oAudioListeners.ExToTypes<Behaviour>(), false);
		}
		// 고유 객체를 설정한다 }
	}

	/** 광원을 설정한다 */
	protected virtual void SetupLights() {
		// 주요 씬 일 경우
		if(this.IsActiveScene) {
#if UNITY_EDITOR
			RenderSettings.ambientIntensity = KCDefine.B_VAL_1_FLT;
			RenderSettings.reflectionIntensity = KCDefine.B_VAL_1_FLT;

			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
			RenderSettings.subtractiveShadowColor = Color.black;
#endif			// #if UNITY_EDITOR
		}

		this.gameObject.scene.ExEnumerateComponents<Light>((a_oLight) => {
			a_oLight.shadows = a_oLight.name.Equals(KCDefine.U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT) ? LightShadows.Soft : LightShadows.Hard;
			a_oLight.intensity = a_oLight.name.Equals(KCDefine.U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT) ? KCDefine.B_VAL_1_FLT : Mathf.Clamp(a_oLight.intensity, KCDefine.B_VAL_0_FLT, KCDefine.B_ADDITIONAL_LIGHT_INTENSITY);
			a_oLight.renderMode = a_oLight.name.Equals(KCDefine.U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT) ? LightRenderMode.ForcePixel : LightRenderMode.Auto;
			a_oLight.shadowBias = KCDefine.B_VAL_1_FLT;
			a_oLight.shadowStrength = KCDefine.B_VAL_1_FLT;
			a_oLight.shadowNearPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;
			a_oLight.shadowNormalBias = KCDefine.B_VAL_1_FLT;
			a_oLight.shadowResolution = LightShadowResolution.FromQualitySettings;
			a_oLight.transform.localScale = Vector3.one;

			// 메인 방향 광원 일 경우
			if(this.IsActiveScene && a_oLight.name.Equals(KCDefine.U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT)) {
				a_oLight.gameObject.SetActive(true);
			} else {
				a_oLight.gameObject.SetActive(this.IsActiveScene ? a_oLight.gameObject.activeSelf : false);
			}

			// 메인 방향 광원 일 경우
			if(a_oLight.type == LightType.Directional && a_oLight.name.Equals(KCDefine.U_OBJ_N_SCENE_MAIN_DIRECTIONAL_LIGHT)) {
				a_oLight.transform.localEulerAngles = this.IsResetMainDirectionalLightAngle ? new Vector3(KCDefine.B_ANGLE_45_DEG, KCDefine.B_ANGLE_45_DEG, KCDefine.B_VAL_0_FLT) : a_oLight.transform.localEulerAngles;
				a_oLight.transform.localPosition = Vector3.zero;

#if UNITY_EDITOR
				a_oLight.lightmapBakeType = LightmapBakeType.Realtime;
				a_oLight.ExSetCullingMask(KCDefine.U_LAYER_MASKS_LIGHT);

				// 태양 광원이 없을 경우
				if(RenderSettings.sun == null || RenderSettings.sun != a_oLight) {
					RenderSettings.sun = a_oLight;
				}
#endif			// #if UNITY_EDITOR
			} else {
#if UNITY_EDITOR
				// 컬링 마스크 설정이 가능 할 경우
				if(!a_oLight.name.Contains(KCDefine.B_NAME_PATTERN_IGNORE_SETUP_CULLING_MASK)) {
					a_oLight.ExSetCullingMask(KCDefine.U_LAYER_MASKS_LIGHT);
				}

				a_oLight.lightmapBakeType = (a_oLight.lightmapBakeType == LightmapBakeType.Realtime) ? LightmapBakeType.Mixed : a_oLight.lightmapBakeType;
#endif			// #if UNITY_EDITOR
			}

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
			var oLightData = a_oLight.gameObject.ExAddComponent<UniversalAdditionalLightData>();

			// 광원 데이터가 존재 할 경우
			if(oLightData != null && QualitySettings.renderPipeline != null && GraphicsSettings.renderPipelineAsset != null) {
				oLightData.usePipelineSettings = true;
			}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

			return true;
		}, true);
	}

	/** 간격을 설정한다 */
	protected virtual void SetupOffsets() {
		// 캔버스 크기를 설정한다
		if(CSceneManager.ActiveSceneUIsCanvas != null && CSceneManager.ActiveSceneUIsCanvas.renderMode == RenderMode.ScreenSpaceCamera) {
			CSceneManager.CanvasSize = (CSceneManager.ActiveSceneUIsCanvas.transform as RectTransform).sizeDelta.ExTo3D();
			CSceneManager.CanvasScale = (CSceneManager.ActiveSceneUIsCanvas.transform as RectTransform).localScale;
		}

		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			CSceneManager.UpOffset = CSceneManager.CanvasSize.y * -CAccess.UpScreenScale;
			CSceneManager.DownOffset = CSceneManager.CanvasSize.y * CAccess.DownScreenScale;
			CSceneManager.LeftOffset = CSceneManager.CanvasSize.x * CAccess.LeftScreenScale;
			CSceneManager.RightOffset = CSceneManager.CanvasSize.x * -CAccess.RightScreenScale;
		}
	}

	/** 루트 객체를 설정한다 */
	protected virtual void SetupRootObjs() {
		this.UIs.ExReset();
		this.UIsTop.ExReset();
		this.PopupUIs.ExReset();
		this.TopmostUIs.ExReset();

		this.Objs.ExReset();
		this.AnchorObjs.ExReset();

		this.Base.ExReset();
		this.Base.transform.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, -this.PlaneDistance);

		this.ObjsBase.transform.localScale = CSceneManager.CanvasScale;
		this.ObjsBase.transform.localEulerAngles = Vector3.zero;
		this.ObjsBase.transform.localPosition = Vector3.zero;

		this.UpObjs.transform.localScale = Vector3.one;
		this.UpObjs.transform.localEulerAngles = Vector3.zero;
		this.UpObjs.transform.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT) + CSceneManager.UpOffset, KCDefine.B_VAL_0_FLT);

		this.DownObjs.transform.localScale = Vector3.one;
		this.DownObjs.transform.localEulerAngles = Vector3.zero;
		this.DownObjs.transform.localPosition = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT);

		this.LeftObjs.transform.localScale = Vector3.one;
		this.LeftObjs.transform.localEulerAngles = Vector3.zero;
		this.LeftObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT) + CSceneManager.LeftOffset, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);

		this.RightObjs.transform.localScale = Vector3.one;
		this.RightObjs.transform.localEulerAngles = Vector3.zero;
		this.RightObjs.transform.localPosition = new Vector3((CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT) + CSceneManager.RightOffset, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);

		// 루트 객체 간격을 설정한다
		CSceneManager.UpRootOffset = (CSceneManager.ActiveSceneUpUIs.transform as RectTransform).anchoredPosition.y - ((CSceneManager.ActiveSceneUIs.transform as RectTransform).anchoredPosition.y + (KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT));
		CSceneManager.DownRootOffset = (CSceneManager.ActiveSceneDownUIs.transform as RectTransform).anchoredPosition.y - ((CSceneManager.ActiveSceneUIs.transform as RectTransform).anchoredPosition.y - (KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT));
		CSceneManager.LeftRootOffset = (CSceneManager.ActiveSceneLeftUIs.transform as RectTransform).anchoredPosition.x - ((CSceneManager.ActiveSceneUIs.transform as RectTransform).anchoredPosition.x - (KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT));
		CSceneManager.RightRootOffset = (CSceneManager.ActiveSceneRightUIs.transform as RectTransform).anchoredPosition.x - ((CSceneManager.ActiveSceneUIs.transform as RectTransform).anchoredPosition.x + (KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT));

		// 기준 객체가 존재 할 경우
		if(this.PivotObjs != null) {
			this.PivotObjs.transform.localScale = Vector3.one;
			this.PivotObjs.transform.localEulerAngles = Vector3.zero;
			this.PivotObjs.transform.localPosition = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
		}

		// 정적 객체가 존재 할 경우
		if(this.StaticObjs != null) {
			this.StaticObjs.transform.localScale = Vector3.one;
			this.StaticObjs.transform.localEulerAngles = Vector3.zero;
			this.StaticObjs.transform.localPosition = Vector3.zero;
		}

		// 추가 광원이 존재 할 경우
		if(this.AdditionalLights != null) {
			this.AdditionalLights.transform.localScale = Vector3.one;
			this.AdditionalLights.transform.localEulerAngles = Vector3.zero;
			this.AdditionalLights.transform.localPosition = Vector3.zero;
		}

		// 반사 프로브가 존재 할 경우
		if(this.ReflectionProbes != null) {
			this.ReflectionProbes.transform.localScale = Vector3.one;
			this.ReflectionProbes.transform.localEulerAngles = Vector3.zero;
			this.ReflectionProbes.transform.localPosition = Vector3.zero;
		}

		// 광원 프로브 그룹이 존재 할 경우
		if(this.LightProbeGroups != null) {
			this.LightProbeGroups.transform.localScale = Vector3.one * KCDefine.B_UNIT_DIGITS_PER_HUNDRED;
			this.LightProbeGroups.transform.localEulerAngles = Vector3.zero;
			this.LightProbeGroups.transform.localPosition = Vector3.zero;
		}
	}

	/** 기본 객체를 설정한다 */
	protected virtual void SetupDefObjs() {
		// 씬 관리자를 설정한다
		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);
		CSceneManager.ActiveSceneManager = CSceneManager.ActiveScene.ExFindComponent<CSceneManager>(KCDefine.U_OBJ_N_SCENE_MANAGER);

		// UI 를 설정한다 {
		this.UIsTop = CSceneManager.ActiveSceneUIsTop = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_TOP);
		this.UIsBase = CSceneManager.ActiveSceneUIsBase = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS_BASE);

		this.UIs = CSceneManager.ActiveSceneUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UIS);
		this.PivotUIs = CSceneManager.ActiveScenePivotUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_PIVOT_UIS);
		this.AnchorUIs = CSceneManager.ActiveSceneAnchorUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);
		this.CornerAnchorUIs = CSceneManager.ActiveSceneCornerAnchorUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_CORNER_ANCHOR_UIS);

		this.UpUIs = CSceneManager.ActiveSceneUpUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_UIS);
		this.DownUIs = CSceneManager.ActiveSceneDownUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
		this.LeftUIs = CSceneManager.ActiveSceneLeftUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
		this.RightUIs = CSceneManager.ActiveSceneRightUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

		this.UpLeftUIs = CSceneManager.ActiveSceneUpLeftUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_LEFT_UIS);
		this.UpRightUIs = CSceneManager.ActiveSceneUpRightUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_RIGHT_UIS);
		this.DownLeftUIs = CSceneManager.ActiveSceneDownLeftUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_LEFT_UIS);
		this.DownRightUIs = CSceneManager.ActiveSceneDownRightUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_RIGHT_UIS);

		this.PopupUIs = CSceneManager.ActiveScenePopupUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_POPUP_UIS);
		this.TopmostUIs = CSceneManager.ActiveSceneTopmostUIs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS);

		this.TestUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_TEST_UIS);
		this.DesignResolutionGuideUIs = this.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DESIGN_RESOLUTION_GUIDE_UIS);
		// UI 를 설정한다 }

		// 객체를 설정한다 {
		this.Base = CSceneManager.ActiveSceneBase = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_BASE);
		this.ObjsBase = CSceneManager.ActiveSceneObjsBase = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS_BASE);

		this.Objs = CSceneManager.ActiveSceneObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_OBJS);
		this.PivotObjs = CSceneManager.ActiveScenePivotObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_PIVOT_OBJS);
		this.AnchorObjs = CSceneManager.ActiveSceneAnchorObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ANCHOR_OBJS);
		this.StaticObjs = CSceneManager.ActiveSceneStaticObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_STATIC_OBJS);
		this.AdditionalLights = CSceneManager.ActiveSceneAdditionalLights = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_ADDITIONAL_LIGHTS);
		this.ReflectionProbes = CSceneManager.ActiveSceneReflectionProbes = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_REFLECTION_PROBES);
		this.LightProbeGroups = CSceneManager.ActiveSceneLightProbeGroups = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LIGHT_PROBE_GROUPS);

		this.UpObjs = CSceneManager.ActiveSceneUpObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_UP_OBJS);
		this.DownObjs = CSceneManager.ActiveSceneDownObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_DOWN_OBJS);
		this.LeftObjs = CSceneManager.ActiveSceneLeftObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_LEFT_OBJS);
		this.RightObjs = CSceneManager.ActiveSceneRightObjs = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindChild(KCDefine.U_OBJ_N_SCENE_RIGHT_OBJS);
		// 객체를 설정한다 }

		// 카메라를 설정한다
		this.UIsCamera = CSceneManager.ActiveSceneUIsCamera = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_UIS_CAMERA);
		this.MainCamera = CSceneManager.ActiveSceneMainCamera = CSceneManager.ActiveSceneManager.gameObject.scene.ExFindComponent<Camera>(KCDefine.U_OBJ_N_SCENE_MAIN_CAMERA);

		// 캔버스를 설정한다
		this.UIsCanvas = CSceneManager.ActiveSceneUIsCanvas = CSceneManager.ActiveSceneUIsBase.GetComponentInChildren<Canvas>();
		this.PopupUIsCanvas = CSceneManager.ActiveScenePopupUIsCanvas = CSceneManager.ActiveScenePopupUIs.GetComponentInChildren<Canvas>();
		this.TopmostUIsCanvas = CSceneManager.ActiveSceneTopmostUIsCanvas = CSceneManager.ActiveSceneTopmostUIs.GetComponentInChildren<Canvas>();

		// 이벤트 시스템을 설정한다 {
		var oEventSystem = CSceneManager.ActiveSceneUIsTop.GetComponentInChildren<EventSystem>();
		
		// 이벤트 시스템이 존재 할 경우
		if(this.IsActiveScene && oEventSystem != null) {
			oEventSystem.sendNavigationEvents = false;
			oEventSystem.pixelDragThreshold = KCDefine.U_THRESHOLD_INPUT_M_MOVE;

#if INPUT_SYSTEM_MODULE_ENABLE
			// 입력 모듈이 없을 경우
			if(!oEventSystem.TryGetComponent<InputSystemUIInputModule>(out InputSystemUIInputModule oInputModule)) {
				oEventSystem.gameObject.ExRemoveComponent<BaseInputModule>(false);
				oInputModule = oEventSystem.gameObject.ExAddComponent<InputSystemUIInputModule>();
			}

			oInputModule.deselectOnBackgroundClick = true;

			oInputModule.moveRepeatRate = KCDefine.U_RATE_INPUT_M_MOVE_REPEAT;
			oInputModule.moveRepeatDelay = KCDefine.U_DELAY_INPUT_M_MOVE_REPEAT;

#if MULTI_TOUCH_ENABLE
			oInputModule.pointerBehavior = UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack;
#else
			oInputModule.pointerBehavior = UIPointerBehavior.SingleUnifiedPointer;
#endif			// #if MULTI_TOUCH_ENABLE
#else
			// 입력 모듈이 없을 경우
			if(!oEventSystem.TryGetComponent<StandaloneInputModule>(out StandaloneInputModule oInputModule)) {
				oEventSystem.gameObject.ExRemoveComponent<BaseInputModule>(false);
				oInputModule = oEventSystem.gameObject.ExAddComponent<StandaloneInputModule>();
			}

			oInputModule.repeatDelay = KCDefine.U_DELAY_INPUT_M_MOVE_REPEAT;
			oInputModule.inputActionsPerSecond = KCDefine.U_UNIT_INPUT_M_INPUT_ACTIONS_PER_SEC;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE
		}

		CSceneManager.ActiveSceneEventSystem = oEventSystem;
		CSceneManager.ActiveSceneBaseInputModule = oEventSystem?.GetComponentInChildren<BaseInputModule>();
		// 이벤트 시스템을 설정한다 }
	}

	/** 캔버스를 설정한다 */
	protected virtual void SetupCanvas(Canvas a_oCanvas, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCanvas != null);

		// 캔버스가 존재 할 경우
		if(a_oCanvas != null) {
			var oUIsObjsNameList = new List<string>() {
				KCDefine.U_OBJ_N_SCENE_UIS,
				KCDefine.U_OBJ_N_SCENE_TEST_UIS,
				KCDefine.U_OBJ_N_SCENE_PIVOT_UIS,
				KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS,
				KCDefine.U_OBJ_N_SCENE_CORNER_ANCHOR_UIS,

				KCDefine.U_OBJ_N_SCENE_UP_UIS,
				KCDefine.U_OBJ_N_SCENE_DOWN_UIS,
				KCDefine.U_OBJ_N_SCENE_LEFT_UIS,
				KCDefine.U_OBJ_N_SCENE_RIGHT_UIS,

				KCDefine.U_OBJ_N_SCENE_UP_LEFT_UIS,
				KCDefine.U_OBJ_N_SCENE_UP_RIGHT_UIS,
				KCDefine.U_OBJ_N_SCENE_DOWN_LEFT_UIS,
				KCDefine.U_OBJ_N_SCENE_DOWN_RIGHT_UIS,
				
				KCDefine.U_OBJ_N_SCENE_POPUP_UIS,
				KCDefine.U_OBJ_N_SCENE_TOPMOST_UIS,
				
				KCDefine.U_OBJ_N_SCREEN_BLIND_UIS,
				KCDefine.U_OBJ_N_SCREEN_POPUP_UIS,
				KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS,
				KCDefine.U_OBJ_N_SCREEN_ABS_UIS,

#if DEBUG || DEVELOPMENT_BUILD
				KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS
#endif			// #if DEBUG || DEVELOPMENT_BUILD
			};

			for(int i = 0; i < oUIsObjsNameList.Count; ++i) {
				var oUIsObjs = a_oCanvas.gameObject.ExFindChild(oUIsObjsNameList[i]);

				// UI 가 존재 할 경우
				if(oUIsObjs != null) {
					this.SetupDefUIsState(oUIsObjs, false);

					// 블라인드 UI 루트 일 경우
					if(oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS)) {
						this.SetupBlindUIsState(oUIsObjs, false);
					}

#if DEBUG || DEVELOPMENT_BUILD
					// 디버그 UI 루트 일 경우
					if(oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS)) {
						this.SetupDebugUIsState(oUIsObjs, false);
					}
#endif			// #if DEBUG || DEVELOPMENT_BUILD
				}
			}
			
			this.DoSetupCanvas(a_oCanvas, a_bIsEnableAssert);
		}
	}

	/** 카메라를 설정한다 */
	protected virtual void SetupCamera() {
		// 캔버스를 설정한다
		this.UIsCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		this.SetupCanvas(this.UIsCanvas, false);

		this.SetupUIsCamera(this.UIsCamera, false);
		this.SetupMainCamera(this.MainCamera, false);
	}

	/** UI 카메라를 설정한다 */
	protected virtual void SetupUIsCamera(Camera a_oCamera, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCamera != null);

		// 카메라가 존재 할 경우
		if(a_oCamera != null) {
			// 트랜스 폼을 설정한다
			a_oCamera.transform.localScale = Vector3.one;
			a_oCamera.transform.localEulerAngles = Vector3.zero;
			a_oCamera.transform.localPosition = this.IsResetUIsCameraPos ? Vector3.zero : a_oCamera.transform.localPosition;

			// 카메라 투영을 설정한다
			a_oCamera.depth = this.UIsCameraDepth;
			a_oCamera.ExSetup2D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE);
		}
	}

	/** 메인 카메라를 설정한다 */
	protected virtual void SetupMainCamera(Camera a_oCamera, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCamera != null);

		// 카메라가 존재 할 경우
		if(a_oCamera != null) {
			// 트랜스 폼을 설정한다 {
			a_oCamera.transform.localScale = Vector3.one;
			a_oCamera.transform.localPosition = this.IsResetMainCameraPos ? Vector3.zero : a_oCamera.transform.localPosition;

#if MODE_2D_ENABLE
			a_oCamera.transform.localEulerAngles = Vector3.zero;
#endif			// #if MODE_2D_ENABLE
			// 트랜스 폼을 설정한다 }
			
			// 카메라 투영을 설정한다 {
			a_oCamera.depth = this.MainCameraDepth;

#if MODE_2D_ENABLE
			a_oCamera.ExSetup2D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE);
#else
			a_oCamera.ExSetup3D(KCDefine.B_SCREEN_HEIGHT * KCDefine.B_UNIT_SCALE, this.PlaneDistance);
#endif			// #if MODE_2D_ENABLE
			// 카메라 투영을 설정한다 }
		}
	}

	/** 기본 UI 상태를 설정한다 */
	private void SetupDefUIsState(GameObject a_oUIsObjs, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);

		// 기본 UI 가 존재 할 경우
		if(a_oUIsObjs != null) {
			var stPos = Vector3.zero;
			var stSize = Vector3.zero;
			var stPivot = KCDefine.B_ANCHOR_MID_CENTER;
			var stAnchorMin = KCDefine.B_ANCHOR_MID_CENTER;
			var stAnchorMax = KCDefine.B_ANCHOR_MID_CENTER;

			bool bIsUpUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_UIS);
			bool bIsDownUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
			bool bIsLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
			bool bIsRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

			bool bIsUpLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_LEFT_UIS);
			bool bIsUpRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_RIGHT_UIS);
			bool bIsDownLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_LEFT_UIS);
			bool bIsDownRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_RIGHT_UIS);

			// 고정 UI 일 경우
			if(bIsUpUIs || bIsDownUIs || bIsLeftUIs || bIsRightUIs) {
				this.SetupAnchorUIsState(a_oUIsObjs, ref stPos, ref stSize, ref stPivot, false);
			}
			// 코너 고정 UI 일 경우
			else if(bIsUpLeftUIs || bIsUpRightUIs || bIsDownLeftUIs || bIsDownRightUIs) {
				this.SetupCornerAnchorUIsState(a_oUIsObjs, ref stPos, ref stSize, ref stPivot, ref stAnchorMin, ref stAnchorMax, false);
			} else {
				this.SetupNonAnchorUIsState(a_oUIsObjs, ref stPos, ref stSize, ref stAnchorMin, ref stAnchorMax, false);
			}

			(a_oUIsObjs.transform as RectTransform).pivot = stPivot;
			(a_oUIsObjs.transform as RectTransform).anchorMin = stAnchorMin;
			(a_oUIsObjs.transform as RectTransform).anchorMax = stAnchorMax;
			(a_oUIsObjs.transform as RectTransform).sizeDelta = stSize.ExTo2D();
			(a_oUIsObjs.transform as RectTransform).anchoredPosition = stPos.ExTo2D();

			(a_oUIsObjs.transform as RectTransform).localScale = Vector3.one;
			(a_oUIsObjs.transform as RectTransform).localEulerAngles = Vector3.zero;
		}
	}

	/** 블라인드 UI 상태를 설정한다 */
	private void SetupBlindUIsState(GameObject a_oUIsObjs, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);

		// 블라인드 UI 가 존재 할 경우
		if(a_oUIsObjs != null) {
			var oImgNameList = new List<string>() {
				KCDefine.U_OBJ_N_UP_BLIND_IMG, KCDefine.U_OBJ_N_DOWN_BLIND_IMG, KCDefine.U_OBJ_N_LEFT_BLIND_IMG, KCDefine.U_OBJ_N_RIGHT_BLIND_IMG
			};

			var oPivotList = new List<Vector3>() {
				KCDefine.B_ANCHOR_DOWN_CENTER, KCDefine.B_ANCHOR_UP_CENTER, KCDefine.B_ANCHOR_MID_RIGHT, KCDefine.B_ANCHOR_MID_LEFT
			};

			var oAnchorList = new List<Vector3>() {
				KCDefine.B_ANCHOR_UP_CENTER, KCDefine.B_ANCHOR_DOWN_CENTER, KCDefine.B_ANCHOR_MID_LEFT, KCDefine.B_ANCHOR_MID_RIGHT
			};

			var oPosList = new List<Vector3>() {
				Vector3.zero, Vector3.zero, Vector3.zero, new Vector3(KCDefine.B_VAL_0_FLT, CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT)
			};

			var oOffsetList = new List<Vector3>() {
				this.IsIgnoreBlindV ? Vector3.zero : new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				this.IsIgnoreBlindV ? Vector3.zero : new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y - KCDefine.B_SCREEN_HEIGHT) / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				this.IsIgnoreBlindH ? Vector3.zero : new Vector3((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT),
				this.IsIgnoreBlindH ? Vector3.zero : new Vector3((CSceneManager.CanvasSize.x - KCDefine.B_SCREEN_WIDTH) / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT)
			};
			
			float fDownOffset = Mathf.Clamp(oOffsetList[KCDefine.B_VAL_1_INT].y - CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT, float.MaxValue);
			oOffsetList[KCDefine.B_VAL_1_INT] = new Vector3(oOffsetList[KCDefine.B_VAL_1_INT].x, fDownOffset, oOffsetList[KCDefine.B_VAL_1_INT].y);

			for(int i = 0; i < oImgNameList.Count; ++i) {
				var oImg = a_oUIsObjs.ExFindComponent<Image>(oImgNameList[i]);
				oImg.rectTransform.pivot = oPivotList[i];
				oImg.rectTransform.anchorMin = oAnchorList[i];
				oImg.rectTransform.anchorMax = oAnchorList[i];
				oImg.rectTransform.sizeDelta = CSceneManager.CanvasSize.ExTo2D();
				oImg.rectTransform.anchoredPosition = oPosList[i].ExTo2D() + oOffsetList[i].ExTo2D();

#if UNITY_EDITOR
				oImg.color = KCDefine.U_COLOR_TRANSPARENT;
#else
				oImg.color = KCDefine.U_COLOR_BLIND_UIS;
#endif			// #if UNITY_EDITOR
			}
		}
	}

	/** 고정 UI 상태를 설정한다 */
	private void SetupAnchorUIsState(GameObject a_oUIsObjs, ref Vector3 a_rstPos, ref Vector3 a_rstSize, ref Vector3 a_rstPivot, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);

		// 고정 UI 가 존재 할 경우
		if(a_oUIsObjs != null) {
			bool bIsUpUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_UIS);
			bool bIsDownUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_UIS);
			bool bIsLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
			bool bIsRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);

			// 위쪽, 아래쪽 UI 일 경우
			if(bIsUpUIs || bIsDownUIs) {
				a_rstSize = new Vector3(KCDefine.B_SCREEN_WIDTH, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
				a_rstPivot = bIsUpUIs ? KCDefine.B_ANCHOR_UP_CENTER : KCDefine.B_ANCHOR_DOWN_CENTER;
				
				// 위쪽 UI 일 경우
				if(bIsUpUIs) {
					a_rstPos = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT) + CSceneManager.UpOffset, KCDefine.B_VAL_0_FLT);
				} else {
					a_rstPos = new Vector3(KCDefine.B_VAL_0_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT);
				}
			}
			// 왼쪽, 오른쪽 UI 일 경우
			else if(bIsLeftUIs || bIsRightUIs) {
				a_rstSize = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_SCREEN_HEIGHT, KCDefine.B_VAL_0_FLT);
				a_rstPivot = bIsLeftUIs ? KCDefine.B_ANCHOR_MID_LEFT : KCDefine.B_ANCHOR_MID_RIGHT;

				// 왼쪽 UI 일 경우
				if(bIsLeftUIs) {
					a_rstPos = new Vector3((CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT) + CSceneManager.LeftOffset, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
				} else {
					a_rstPos = new Vector3((CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT) + CSceneManager.RightOffset, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
				}
			}
		}
	}

	/** 코너 고정 UI 상태를 설정한다 */
	private void SetupCornerAnchorUIsState(GameObject a_oUIsObjs, ref Vector3 a_rstPos, ref Vector3 a_rstSize, ref Vector3 a_rstPivot, ref Vector3 a_rstAnchorMin, ref Vector3 a_rstAnchorMax, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);

		// 코너 UI 가 존재 할 경우
		if(a_oUIsObjs != null) {
			bool bIsUpLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_LEFT_UIS);
			bool bIsUpRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UP_RIGHT_UIS);
			bool bIsDownLeftUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_LEFT_UIS);
			bool bIsDownRightUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_DOWN_RIGHT_UIS);

			// 좌상단, 우상단 UI 일 경우
			if(bIsUpLeftUIs || bIsUpRightUIs) {
				a_rstSize = new Vector3(CSceneManager.CanvasSize.x, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
				a_rstPivot = bIsUpLeftUIs ? KCDefine.B_ANCHOR_UP_LEFT : KCDefine.B_ANCHOR_UP_RIGHT;
				a_rstAnchorMin = bIsUpLeftUIs ? KCDefine.B_ANCHOR_UP_LEFT : KCDefine.B_ANCHOR_UP_RIGHT;
				a_rstAnchorMax = bIsUpLeftUIs ? KCDefine.B_ANCHOR_UP_LEFT : KCDefine.B_ANCHOR_UP_RIGHT;
				
				// 좌상단 UI 일 경우
				if(bIsUpLeftUIs) {
					a_rstPos = new Vector3(CSceneManager.LeftOffset, CSceneManager.UpOffset, KCDefine.B_VAL_0_FLT);
				} else {
					a_rstPos = new Vector3(CSceneManager.RightOffset, CSceneManager.UpOffset, KCDefine.B_VAL_0_FLT);
				}
			}
			// 좌하단, 우하단 UI 일 경우
			else if(bIsDownLeftUIs || bIsDownRightUIs) {
				a_rstSize = new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_SCREEN_HEIGHT, KCDefine.B_VAL_0_FLT);
				a_rstPivot = bIsDownLeftUIs ? KCDefine.B_ANCHOR_DOWN_LEFT : KCDefine.B_ANCHOR_DOWN_RIGHT;
				a_rstAnchorMin = bIsDownLeftUIs ? KCDefine.B_ANCHOR_DOWN_LEFT : KCDefine.B_ANCHOR_DOWN_RIGHT;
				a_rstAnchorMax = bIsDownLeftUIs ? KCDefine.B_ANCHOR_DOWN_LEFT : KCDefine.B_ANCHOR_DOWN_RIGHT;

				// 좌하단 UI 일 경우
				if(bIsDownLeftUIs) {
					a_rstPos = new Vector3(CSceneManager.LeftOffset, CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT);
				} else {
					a_rstPos = new Vector3(CSceneManager.RightOffset, CSceneManager.DownOffset, KCDefine.B_VAL_0_FLT);
				}
			}
		}
	}

	/** 유동 UI 상태를 설정한다 */
	private void SetupNonAnchorUIsState(GameObject a_oUIsObjs, ref Vector3 a_rstPos, ref Vector3 a_rstSize, ref Vector3 a_rstAnchorMin, ref Vector3 a_rstAnchorMax, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);

		// 유동 UI 가 존재 할 경우
		if(a_oUIsObjs != null) {
			bool bIsUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_UIS);
			bool bIsTestUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_TEST_UIS);
			bool bIsBlindUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS);
			bool bIsAnchorUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_ANCHOR_UIS);
			bool bIsCornerAnchorUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_CORNER_ANCHOR_UIS);

#if DEBUG || DEVELOPMENT_BUILD
			bool bIsDebugUIs = a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_DEBUG_UIS);
#else
			bool bIsDebugUIs = false;
#endif			// #if DEBUG || DEVELOPMENT_BUILD

			bool bIsBaseUIs = bIsUIs || bIsTestUIs;
			bool bIsEnableCorrectUIs = bIsUIs || bIsTestUIs || bIsBlindUIs || bIsAnchorUIs || bIsCornerAnchorUIs || bIsDebugUIs;

			// 기준 UI 일 경우
			if(a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCENE_PIVOT_UIS)) {
				a_rstPos = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
			}
			// 크기 보정 가능한 UI 일 경우
			else if(bIsBaseUIs || bIsEnableCorrectUIs) {
				a_rstSize = bIsBaseUIs ? KCDefine.B_SCREEN_SIZE : CSceneManager.CanvasSize;

				// 블라인드, 고정, 코너, 디버그 UI 일 경우
				if(bIsBlindUIs || bIsAnchorUIs || bIsCornerAnchorUIs || bIsDebugUIs) {
					a_rstSize = Vector3.zero;
					a_rstAnchorMin = KCDefine.B_ANCHOR_DOWN_LEFT;
					a_rstAnchorMax = KCDefine.B_ANCHOR_UP_RIGHT;
				}
			}
			// 화면 UI 가 아닐 경우
			else if(!a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_POPUP_UIS) && !a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_TOPMOST_UIS) && !a_oUIsObjs.name.Equals(KCDefine.U_OBJ_N_SCREEN_ABS_UIS)) {
				a_rstPos = new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT);
			}
		}
	}

	/** 고유 컴포넌트를 설정한다 */
	private void SetupUniqueComponents(List<Behaviour> a_oComponentList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oComponentList != null);

		// 고유 컴포넌트가 존재 할 경우
		if(a_oComponentList != null) {
			for(int i = 0; i < a_oComponentList.Count; ++i) {
				a_oComponentList[i].enabled = this.IsActiveScene;
			}
		}
	}

	/** 캔버스를 설정한다 */
	private void DoSetupCanvas(Canvas a_oCanvas, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCanvas != null);

		// 캔버스가 존재 할 경우
		if(a_oCanvas != null) {
			// 캔버스를 설정한다 {
			a_oCanvas.pixelPerfect = false;
			a_oCanvas.planeDistance = this.PlaneDistance;
			
			a_oCanvas.worldCamera = this.UIsCamera;

			// 카메라 렌더 모드 일 경우
			if(a_oCanvas != this.UIsCanvas && a_oCanvas.renderMode == RenderMode.ScreenSpaceCamera) {
				a_oCanvas.sortingLayerName = KCDefine.U_SORTING_L_DEF;
			}
			// 캔버스를 설정한다 }

			// 캔버스 비율 처리자를 설정한다 {
			var oCanvasScaler = a_oCanvas.GetComponentInChildren<CanvasScaler>();

			// UI 비율 모드 설정이 필요 할 경우
			if(oCanvasScaler != null && a_oCanvas.name.Equals(KCDefine.U_OBJ_N_SCENE_UIS_BASE)) {
				oCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			}

			// 캔버스 비율 처리자 설정이 가능 할 경우
			if(oCanvasScaler != null && oCanvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize) {
				oCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				oCanvasScaler.referenceResolution = KCDefine.B_SCREEN_SIZE.ExTo2D();
				oCanvasScaler.referencePixelsPerUnit = KCDefine.B_UNIT_REF_PIXELS_PER_UNIT;
			}
			// 캔버스 비율 처리자를 설정한다 }
		}
	}
	#endregion			// 함수

	#region 클래스 함수
	/** 퀄리티를 설정한다 */
	public static void SetupQuality(EQualityLevel a_eQualityLevel, bool a_bIsEnableExpensiveChange = false) {
		CSceneManager.QualityLevel = a_eQualityLevel;

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
		CSceneManager.DoSetupQuality(a_eQualityLevel, true, true);
#else
		CSceneManager.DoSetupQuality(a_eQualityLevel, false, true);
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	}

	/** 퀄리티를 설정한다 */
	private static void DoSetupQuality(EQualityLevel a_eQualityLevel, bool a_bIsEnableSetupRenderingPipeline, bool a_bIsEnableExpensiveChange = false) {
#if UNITY_EDITOR
		var oQualityLevelList = new List<EQualityLevel>() {
			EQualityLevel.NORM, EQualityLevel.HIGH, EQualityLevel.ULTRA
		};

		var oIsSetupOptsList = new List<bool>() {
			GraphicsSettings.logWhenShaderIsCompiled,
			GraphicsSettings.videoShadersIncludeMode == VideoShadersIncludeMode.Always
		};

		// 설정 갱신이 필요 할 경우
		if(oIsSetupOptsList.Contains(false)) {
			GraphicsSettings.logWhenShaderIsCompiled = true;
			GraphicsSettings.videoShadersIncludeMode = VideoShadersIncludeMode.Always;
		}

		for(int i = 0; i < oQualityLevelList.Count; ++i) {
			var stRenderingOptsInfo = CPlatformOptsSetter.OptsInfoTable.GetRenderingOptsInfo(oQualityLevelList[i]);
			QualitySettings.SetQualityLevel((int)oQualityLevelList[i], false);

			var oIsSetupQualityOptsList = new List<bool>() {
				QualitySettings.streamingMipmapsActive == false,

				QualitySettings.softParticles,
				QualitySettings.realtimeReflectionProbes,
				QualitySettings.asyncUploadPersistentBuffer,
				QualitySettings.billboardsFaceCameraPosition,

				QualitySettings.maximumLODLevel == KCDefine.B_VAL_0_INT,
				QualitySettings.asyncUploadTimeSlice == KCDefine.U_QUALITY_ASYNC_UPLOAD_TIME_SLICE,
				QualitySettings.asyncUploadBufferSize == KCDefine.U_QUALITY_ASYNC_UPLOAD_BUFFER_SIZE,

				QualitySettings.lodBias.ExIsEquals(KCDefine.B_VAL_1_FLT),
				QualitySettings.resolutionScalingFixedDPIFactor.ExIsEquals(KCDefine.B_VAL_1_FLT),

				QualitySettings.shadows == UnityEngine.ShadowQuality.All,
				QualitySettings.vSyncCount == (int)EVSyncType.NEVER,
				QualitySettings.skinWeights == SkinWeights.FourBones,
				QualitySettings.shadowCascades == (int)EShadowCascadesOpts.FOUR_CASCADES,
				QualitySettings.shadowProjection == ShadowProjection.StableFit,
				QualitySettings.anisotropicFiltering == AnisotropicFiltering.Enable,

				QualitySettings.shadowDistance.ExIsEquals(KCDefine.U_DISTANCE_CAMERA_FAR_PLANE / KCDefine.B_VAL_2_FLT),
				QualitySettings.shadowCascade2Split.ExIsEquals(KCEditorDefine.U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT),
				QualitySettings.shadowCascade4Split.ExIsEquals(KCEditorDefine.U_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT),
				QualitySettings.shadowNearPlaneOffset.ExIsEquals(KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE),

				QualitySettings.antiAliasing == KCDefine.B_VAL_0_INT,
				QualitySettings.shadowmaskMode == stRenderingOptsInfo.m_stLightOptsInfo.m_eShadowmaskMode,
				QualitySettings.shadowResolution == stRenderingOptsInfo.m_stLightOptsInfo.m_eShadowResolution,
				QualitySettings.renderPipeline == a_bIsEnableSetupRenderingPipeline ? Resources.Load<RenderPipelineAsset>(CAccess.GetRenderingPipelinePath(oQualityLevelList[i])) : null
			};

			// 설정 갱신이 필요 할 경우
			if(oIsSetupQualityOptsList.Contains(false)) {
				QualitySettings.streamingMipmapsActive = false;

				QualitySettings.softParticles = true;
				QualitySettings.realtimeReflectionProbes = true;
				QualitySettings.asyncUploadPersistentBuffer = true;
				QualitySettings.billboardsFaceCameraPosition = true;

				QualitySettings.maximumLODLevel = KCDefine.B_VAL_0_INT;
				QualitySettings.asyncUploadTimeSlice = KCDefine.U_QUALITY_ASYNC_UPLOAD_TIME_SLICE;
				QualitySettings.asyncUploadBufferSize = KCDefine.U_QUALITY_ASYNC_UPLOAD_BUFFER_SIZE;

				QualitySettings.lodBias = KCDefine.B_VAL_1_FLT;
				QualitySettings.resolutionScalingFixedDPIFactor = KCDefine.B_VAL_1_FLT;

				QualitySettings.shadows = UnityEngine.ShadowQuality.All;
				QualitySettings.vSyncCount = (int)EVSyncType.NEVER;
				QualitySettings.skinWeights = SkinWeights.FourBones;
				QualitySettings.shadowCascades = (int)EShadowCascadesOpts.FOUR_CASCADES;
				QualitySettings.shadowProjection = ShadowProjection.StableFit;
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;

				QualitySettings.shadowDistance = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE / KCDefine.B_VAL_2_FLT;
				QualitySettings.shadowCascade2Split = KCEditorDefine.U_EDITOR_OPTS_CASCADE_2_SPLIT_PERCENT;
				QualitySettings.shadowCascade4Split = KCEditorDefine.U_EDITOR_OPTS_CASCADE_4_SPLIT_PERCENT;
				QualitySettings.shadowNearPlaneOffset = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;

				QualitySettings.antiAliasing = KCDefine.B_VAL_0_INT;
				QualitySettings.shadowmaskMode = stRenderingOptsInfo.m_stLightOptsInfo.m_eShadowmaskMode;
				QualitySettings.shadowResolution = stRenderingOptsInfo.m_stLightOptsInfo.m_eShadowResolution;
				QualitySettings.renderPipeline = a_bIsEnableSetupRenderingPipeline ? Resources.Load<RenderPipelineAsset>(CAccess.GetRenderingPipelinePath(oQualityLevelList[i])) : null;
			}
		}
#endif			// #if UNITY_EDITOR

		GraphicsSettings.renderPipelineAsset = a_bIsEnableSetupRenderingPipeline ? Resources.Load<RenderPipelineAsset>(CAccess.GetRenderingPipelinePath(a_eQualityLevel)) : null;
		GraphicsSettings.defaultRenderPipeline = a_bIsEnableSetupRenderingPipeline ? Resources.Load<RenderPipelineAsset>(CAccess.GetRenderingPipelinePath(a_eQualityLevel)) : null;

		QualitySettings.SetQualityLevel((int)a_eQualityLevel, a_bIsEnableExpensiveChange);
	}
	#endregion			// 클래스 함수

	#region 조건부 함수
#if UNITY_EDITOR
	/** 에디터 씬을 설정한다 */
	public void EditorSetupScene() {
		this.SetupScene(true);
		this.SetupScene(false);

		// 상태 갱신이 가능 할 경우
		if(CEditorAccess.IsEnableUpdateState) {
			CPlatformOptsSetter.SetupQuality();
		}
	}

	/** 스크립트 순서를 설정한다 */
	protected sealed override void SetupScriptOrder() {
		base.SetupScriptOrder();
		this.ExSetScriptOrder(this.ScriptOrder);
	}
#endif			// #if UNITY_EDITOR

#if DEBUG || DEVELOPMENT_BUILD
	/** 디버그 UI 상태를 설정한다 */
	private void SetupDebugUIsState(GameObject a_oUIsObjs, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oUIsObjs != null);
		
		// 디버그 UI 가 존재 할 경우
		if(a_oUIsObjs != null && CSceneManager.ScreenStaticDebugText != null) {
			CSceneManager.m_oStaticDebugStrBuilder.Clear();
			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_A, CSceneManager.UpOffset, CSceneManager.DownOffset, CSceneManager.LeftOffset, CSceneManager.RightOffset);
			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_B, CSceneManager.UpRootOffset, CSceneManager.DownRootOffset, CSceneManager.LeftRootOffset, CSceneManager.RightRootOffset);
			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_C, CAccess.ScreenSize.x, CAccess.ScreenSize.y);

#if ADS_MODULE_ENABLE
			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_D, CAccess.ScreenDPI, CAccess.GetBannerAdsHeight(KCDefine.U_SIZE_BANNER_ADS.y));
#endif			// #if ADS_MODULE_ENABLE

			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_E, (EQualityLevel)QualitySettings.GetQualityLevel(), Application.targetFrameRate);
			CSceneManager.m_oStaticDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_STATIC_DEBUG_INFO_F, KCDefine.B_DIR_P_WRITABLE);
		}
	}
#endif			// #if DEBUG || DEVELOPMENT_BUILD

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	/** 카메라 데이터를 설정한다 */
	private void SetupCameraData(UniversalAdditionalCameraData a_oCameraData, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCameraData != null);

		// 카메라 데이터가 존재 할 경우
		if(a_oCameraData != null) {
			a_oCameraData.antialiasing = AntialiasingMode.None;
			a_oCameraData.antialiasingQuality = AntialiasingQuality.Medium;
		}
	}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 조건부 함수
}
