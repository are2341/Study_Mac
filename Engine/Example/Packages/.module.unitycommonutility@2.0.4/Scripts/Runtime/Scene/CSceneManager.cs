using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Coffee.UIExtensions;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if DEBUG || DEVELOPMENT_BUILD
using UnityEngine.Profiling;
#endif			// #if DEBUG || DEVELOPMENT_BUILD

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

/** 씬 관리자 */
public abstract partial class CSceneManager : CComponent {
	#region 변수
	private bool m_bIsEnableSetupTransforms = true;
	private List<STDespawnObjInfo> m_oDespawnObjInfoList = new List<STDespawnObjInfo>();
	private Dictionary<string, ObjectPool> m_oObjsPoolDict = new Dictionary<string, ObjectPool>();
	#endregion			// 변수

	#region 클래스 변수
	private static NetworkReachability m_ePrevNetworkReachability = NetworkReachability.NotReachable;
	private static Dictionary<string, CSceneManager> m_oSceneManagerDict = new Dictionary<string, CSceneManager>();
	private static Dictionary<string, STTouchResponderInfo> m_oTouchResponderInfoDict = new Dictionary<string, STTouchResponderInfo>();
	
#if DEBUG || DEVELOPMENT_BUILD
	private static int m_nNumFrames = 0;
	private static float m_fFPSInfoSkipTime = 0.0f;
	private static float m_fDebugInfoSkipTime = 0.0f;

	private static System.Text.StringBuilder m_oStaticDebugStrBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oDynamicDebugStrBuilder = new System.Text.StringBuilder();

	private static System.Text.StringBuilder m_oExtraStaticDebugStrBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oExtraDynamicDebugStrBuilder = new System.Text.StringBuilder();
#endif			// #if DEBUG || DEVELOPMENT_BUILD

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	private static UniversalAdditionalCameraData m_oActiveSceneMainCameraData = null;
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 클래스 변수

	#region 프로퍼티
	public abstract string SceneName { get; }

	/** =====> UI <===== */
	public GameObject UIsTop { get; private set; } = null;
	public GameObject UIsBase { get; private set; } = null;

	public GameObject UIs { get; private set; } = null;
	public GameObject TestUIs { get; private set; } = null;
	public GameObject PivotUIs { get; private set; } = null;
	public GameObject AnchorUIs { get; private set; } = null;
	public GameObject CornerAnchorUIs { get; private set; } = null;
	public GameObject DesignResolutionGuideUIs { get; private set; } = null;

	// 고정 UI
	public GameObject UpUIs { get; private set; } = null;
	public GameObject DownUIs { get; private set; } = null;
	public GameObject LeftUIs { get; private set; } = null;
	public GameObject RightUIs { get; private set; } = null;

	// 코너 고정 UI
	public GameObject UpLeftUIs { get; private set; } = null;
	public GameObject UpRightUIs { get; private set; } = null;
	public GameObject DownLeftUIs { get; private set; } = null;
	public GameObject DownRightUIs { get; private set; } = null;

	// 팝업 UI
	public GameObject PopupUIs { get; private set; } = null;
	public GameObject TopmostUIs { get; private set; } = null;

	// 객체 {
	public GameObject Base { get; private set; } = null;
	public GameObject ObjsBase { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject PivotObjs { get; private set; } = null;
	public GameObject AnchorObjs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;
	public GameObject AdditionalLights { get; private set; } = null;
	public GameObject ReflectionProbes { get; private set; } = null;
	public GameObject LightProbeGroups { get; private set; } = null;
	// 객체 }

	// 고정 객체
	public GameObject UpObjs { get; private set; } = null;
	public GameObject DownObjs { get; private set; } = null;
	public GameObject LeftObjs { get; private set; } = null;
	public GameObject RightObjs { get; private set; } = null;

	// 카메라
	public Camera UIsCamera { get; private set; } = null;
	public Camera MainCamera { get; private set; } = null;

	// 캔버스
	public Canvas UIsCanvas { get; private set; } = null;
	public Canvas PopupUIsCanvas { get; private set; } = null;
	public Canvas TopmostUIsCanvas { get; private set; } = null;

	public bool IsActiveScene => CSceneManager.ActiveScene == this.gameObject.scene;

	public virtual bool IsResetUIsCameraPos => true;
	public virtual bool IsResetMainCameraPos => true;
	public virtual bool IsResetMainDirectionalLightAngle => true;

	public virtual bool IsRealtimeFadeInAni => false;
	public virtual bool IsRealtimeFadeOutAni => false;

	public virtual float ExtraBlindVOffset => KCDefine.B_VAL_0_FLT;
	public virtual float ExtraBlindHOffset => KCDefine.B_VAL_0_FLT;

	public virtual float PlaneDistance => KCDefine.U_DISTANCE_CAMERA_PLANE;
	public virtual float FadeOutAniDuration => KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI;

	public virtual float UIsCameraDepth => KCDefine.U_DEPTH_UIS_CAMERA;
	public virtual float MainCameraDepth => KCDefine.U_DEPTH_MAIN_CAMERA;

	public virtual Color ClearColor => KCDefine.U_COLOR_CLEAR;
	public virtual Color ScreenFadeInColor => KCDefine.U_COLOR_SCREEN_FADE_IN;
	public virtual Color ScreenFadeOutColor => KCDefine.U_COLOR_SCREEN_FADE_OUT;
	public virtual STSortingOrderInfo UIsCanvasSortingOrderInfo => KCDefine.U_SORTING_OI_UIS_CANVAS;

#if MODE_PORTRAIT_ENABLE
	public virtual bool IsIgnoreBlindV => true;
	public virtual bool IsIgnoreBlindH => false;
#else
	public virtual bool IsIgnoreBlindV => false;
	public virtual bool IsIgnoreBlindH => true;
#endif			// #if MODE_PORTRAIT_ENABLE

#if UNITY_EDITOR
	public virtual int ScriptOrder => KCDefine.U_SCRIPT_O_SCENE_MANAGER;
#endif			// #if UNITY_EDITOR

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	public virtual List<Camera> OverlayCameraList {
		get {
			var oCameraList = new List<Camera>();

			// UI 카메라가 존재 할 경우
			if(this.UIsCamera != null) {
				oCameraList.ExAddVal(this.UIsCamera);
			}

			return oCameraList;
		}
	}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
	#endregion			// 프로퍼티

	#region 클래스 프로퍼티
	public static bool IsInit { get; set; } = false;
	public static bool IsSetup { get; set; } = false;
	public static bool IsLateSetup { get; set; } = false;

	public static bool IsAwake { get; protected set; } = false;
	public static bool IsAppQuit { get; protected set; } = false;

	public static EQualityLevel QualityLevel { get; private set; } = EQualityLevel.NONE;

	// 간격
	public static float UpOffset { get; private set; } = 0.0f;
	public static float DownOffset { get; private set; } = 0.0f;
	public static float LeftOffset { get; private set; } = 0.0f;
	public static float RightOffset { get; private set; } = 0.0f;
	
	// 최상단 간격
	public static float UpRootOffset { get; private set; } = 0.0f;
	public static float DownRootOffset { get; private set; } = 0.0f;
	public static float LeftRootOffset { get; private set; } = 0.0f;
	public static float RightRootOffset { get; private set; } = 0.0f;
	
	// 캔버스 크기
	public static Vector3 CanvasSize { get; protected set; } = Vector3.zero;
	public static Vector3 CanvasScale { get; protected set; } = Vector3.zero;

	// 카메라
	public static Camera ActiveSceneUIsCamera { get; private set; } = null;
	public static Camera ActiveSceneMainCamera { get; private set; } = null;

	// 캔버스
	public static Canvas ActiveSceneUIsCanvas { get; private set; } = null;
	public static Canvas ActiveScenePopupUIsCanvas { get; private set; } = null;
	public static Canvas ActiveSceneTopmostUIsCanvas { get; private set; } = null;

	// 이벤트
	public static EventSystem ActiveSceneEventSystem { get; private set; } = null;
	public static BaseInputModule ActiveSceneBaseInputModule { get; private set; } = null;

	// 관리자 {
	public static CScheduleManager ScheduleManager { get; set; } = null;
	public static CNavStackManager NavStackManager { get; set; } = null;
	public static CCollectionManager CollectionManager { get; set; } = null;

	public static CSceneManager ActiveSceneManager { get; private set; } = null;
	// 관리자 }

	// UI {
	public static GameObject ActiveSceneUIsTop { get; private set; } = null;
	public static GameObject ActiveSceneUIsBase { get; private set; } = null;

	public static GameObject ActiveSceneUIs { get; private set; } = null;
	public static GameObject ActiveScenePivotUIs { get; private set; } = null;
	public static GameObject ActiveSceneAnchorUIs { get; private set; } = null;
	public static GameObject ActiveSceneCornerAnchorUIs { get; private set; } = null;
	// UI }

	// 고정 UI
	public static GameObject ActiveSceneUpUIs { get; private set; } = null;
	public static GameObject ActiveSceneDownUIs { get; private set; } = null;
	public static GameObject ActiveSceneLeftUIs { get; private set; } = null;
	public static GameObject ActiveSceneRightUIs { get; private set; } = null;

	// 코너 고정 UI
	public static GameObject ActiveSceneUpLeftUIs { get; private set; } = null;
	public static GameObject ActiveSceneUpRightUIs { get; private set; } = null;
	public static GameObject ActiveSceneDownLeftUIs { get; private set; } = null;
	public static GameObject ActiveSceneDownRightUIs { get; private set; } = null;

	// 팝업 UI
	public static GameObject ActiveScenePopupUIs { get; private set; } = null;
	public static GameObject ActiveSceneTopmostUIs { get; private set; } = null;

	// 객체 {
	public static GameObject ActiveSceneBase { get; private set; } = null;
	public static GameObject ActiveSceneObjsBase { get; private set; } = null;

	public static GameObject ActiveSceneObjs { get; private set; } = null;
	public static GameObject ActiveScenePivotObjs { get; private set; } = null;
	public static GameObject ActiveSceneAnchorObjs { get; private set; } = null;
	public static GameObject ActiveSceneStaticObjs { get; private set; } = null;
	public static GameObject ActiveSceneAdditionalLights { get; private set; } = null;
	public static GameObject ActiveSceneReflectionProbes { get; private set; } = null;
	public static GameObject ActiveSceneLightProbeGroups { get; private set; } = null;
	// 객체 }

	// 고정 객체
	public static GameObject ActiveSceneUpObjs { get; private set; } = null;
	public static GameObject ActiveSceneDownObjs { get; private set; } = null;
	public static GameObject ActiveSceneLeftObjs { get; private set; } = null;
	public static GameObject ActiveSceneRightObjs { get; private set; } = null;

	// 화면 UI
	public static GameObject ScreenBlindUIs { get; protected set; } = null;
	public static GameObject ScreenPopupUIs { get; protected set; } = null;
	public static GameObject ScreenTopmostUIs { get; protected set; } = null;
	public static GameObject ScreenAbsUIs { get; protected set; } = null;

	public static bool IsAppInit => CSceneManager.IsInit && CSceneManager.IsSetup && CSceneManager.IsLateSetup;
	public static bool IsAppRunning => CSceneManager.IsInit && !CSceneManager.IsAppQuit;
	public static bool IsExistsMainCamera => Camera.main != null && CSceneManager.ActiveSceneMainCamera != null;

	public static Scene ActiveScene => SceneManager.GetActiveScene();
	public static string ActiveSceneName => CSceneManager.ActiveScene.name;

#if DEBUG || DEVELOPMENT_BUILD
	/** =====> UI <===== */
	public static TMP_Text ScreenFPSText { get; protected set; } = null;
	public static TMP_Text ScreenFrameTimeText { get; protected set; } = null;

	public static TMP_Text ScreenStaticDebugText { get; protected set; } = null;
	public static TMP_Text ScreenDynamicDebugText { get; protected set; } = null;

	public static Button ScreenFPSInfoBtn { get; protected set; } = null;
	public static Button ScreenDebugInfoBtn { get; protected set; } = null;

	public static Button ScreenTimeScaleUpBtn { get; protected set; } = null;
	public static Button ScreenTimeScaleDownBtn { get; protected set; } = null;

	/** =====> 객체 <===== */
	public static GameObject ScreenDebugUIs { get; protected set; } = null;
	public static GameObject ScreenFPSInfoUIs { get; protected set; } = null;
	public static GameObject ScreenDebugInfoUIs { get; protected set; } = null;
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = true;

#if DEBUG || DEVELOPMENT_BUILD
		CSceneManager.ScreenDebugUIs?.SetActive(true);
#endif			// #if DEBUG || DEVELOPMENT_BUILD

		// 씬이 처음 로딩 되었을 경우
		if(!CSceneManager.IsInit && !CSceneManager.IsAwake) {
#if DEBUG || DEVELOPMENT_BUILD
			CFunc.ShowLog($"Platform: {Application.platform}", KCDefine.B_LOG_COLOR_PLATFORM_INFO);
			CFunc.ShowLog($"Data Path: {Application.dataPath}/", KCDefine.B_LOG_COLOR_PLATFORM_INFO);
			CFunc.ShowLog($"Persistent Data Path: {Application.persistentDataPath}/", KCDefine.B_LOG_COLOR_PLATFORM_INFO);

#if UNITY_EDITOR
			CFunc.ShowLog($"External Data Path: {KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}", KCDefine.B_LOG_COLOR_PLATFORM_INFO);
#else
			CFunc.ShowLog($"External Data Path: {KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}", KCDefine.B_LOG_COLOR_PLATFORM_INFO);
#endif			// #if UNITY_EDITOR
#endif			// #if DEBUG || DEVELOPMENT_BUILD

			CSceneManager.IsInit = false;
			CSceneManager.IsAwake = true;
			CSceneManager.IsSetup = false;

			Time.timeScale = KCDefine.B_VAL_1_FLT;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Application.targetFrameRate = KCDefine.B_TARGET_FRAME_RATE;
			CSceneLoader.Inst.AwakeActiveSceneName = CSceneManager.ActiveSceneName;

			// 초기화 씬이 아닐 경우
			if(!CSceneLoader.Inst.AwakeActiveSceneName.Equals(KCDefine.B_SCENE_N_INIT)) {
				this.ExLateCallFunc((a_oSender) => {
					CScheduleManager.Inst.RemoveComponent(this);
					CNavStackManager.Inst.RemoveComponent(this);

					CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_INIT, false);
				}, KCDefine.U_DELAY_NEXT_SCENE_LOAD);
			}
		}
		
		this.SetupScene(true);
		this.DesignResolutionGuideUIs?.gameObject.SetActive(this.IsActiveScene && !Application.isPlaying);

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			CSceneManager.ScheduleManager = CScheduleManager.Inst;
			CSceneManager.NavStackManager = CNavStackManager.Inst;
			CSceneManager.CollectionManager = CCollectionManager.Inst;

#if DEBUG || DEVELOPMENT_BUILD
			this.TestUIs?.SetActive(true);
#else
			this.TestUIs?.SetActive(false);
#endif			// #if DEBUG || DEVELOPMENT_BUILD
		}
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		this.SetupScene(false);
		CIndicatorManager.Inst.Close(true);

#if UNITY_EDITOR
		// 주요 씬 일 경우
		if(this.IsActiveScene) {
			this.ExLateCallFunc((a_oSender) => {
				CFunc.SelObjs(new List<GameObject>() {
					this.UIsBase, this.Objs, this.gameObject
				});
			}, KCDefine.B_VAL_1_FLT, true);
			
			this.ExLateCallFunc((a_oSender) => { CFunc.SelObjs(null); EditorWindow.FocusWindowIfItsOpen(CEditorAccess.GameViewType); }, KCDefine.B_VAL_2_FLT, true);
		}
#endif			// #if UNITY_EDITOR

#if DEBUG || DEVELOPMENT_BUILD
		// 초기화 되었을 경우
		if(CSceneManager.IsAppInit) {
			this.SetupDebugUIsState(CSceneManager.ScreenDebugUIs, false);
		}

		CSceneManager.ScreenDebugUIs?.ExEnumerateComponents<RectTransform>((a_oTrans) => { LayoutRebuilder.ForceRebuildLayoutImmediate(a_oTrans); return true; });
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			// 네트워크 상태가 갱신 되었을 경우
			if(CSceneManager.m_ePrevNetworkReachability != Application.internetReachability) {
				CSceneManager.m_ePrevNetworkReachability = Application.internetReachability;
				this.OnUpdateNetworkReachability(Application.internetReachability);
			}

			// 트랜스 폼 설정이 가능 할 경우
			if(m_bIsEnableSetupTransforms) {
				m_bIsEnableSetupTransforms = false;
				this.gameObject.scene.ExEnumerateComponents<RectTransform>((a_oTrans) => { LayoutRebuilder.ForceRebuildLayoutImmediate(a_oTrans); return true; });
			}

			// 주요 씬 일 경우
			if(this.IsActiveScene) {
#if INPUT_SYSTEM_MODULE_ENABLE
				bool bIsBackKeyDown = Keyboard.current.escapeKey.wasPressedThisFrame;
				bool bIsResolutionKeyDown = Keyboard.current.leftShiftKey.isPressed && Keyboard.current.uKey.wasPressedThisFrame;
#else
				bool bIsBackKeyDown = Input.GetKeyDown(KeyCode.Escape);
				bool bIsResolutionKeyDown = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.U);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if DEBUG || DEVELOPMENT_BUILD
				this.UpdateDebugUIsState(a_fDeltaTime);
				this.UpdateFPSInfoUIsState(a_fDeltaTime);
#endif			// #if DEBUG || DEVELOPMENT_BUILD

				// 백 키를 눌렀을 경우
				if(CSceneManager.IsAppInit && bIsBackKeyDown) {
					CSndManager.Inst.PlayFXSnd(KCDefine.U_SND_P_G_TOUCH_END);
					CNavStackManager.Inst.SendNavStackEvent(ENavStackEvent.BACK_KEY_DOWN);
				}

#if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
				// 해상도 키를 눌렀을 경우
				if(CSceneManager.IsAppInit && bIsResolutionKeyDown) {
					Screen.SetResolution((int)CAccess.CorrectDesktopScreenSize.x, (int)CAccess.CorrectDesktopScreenSize.y, FullScreenMode.Windowed);
					CSceneLoader.Inst.LoadScene(this.SceneName);
				}
#endif			// #if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)
			}
		}
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
				// 주요 씬 일 경우
				if(this.IsActiveScene) {
					CSceneLoader.Inst.PrevActiveSceneName = this.SceneName;

					Resources.UnloadUnusedAssets();
					CSceneManager.CollectionManager?.Reset();
					System.GC.Collect(System.GC.MaxGeneration, System.GCCollectionMode.Default, true, true);

					// 터치 응답자가 존재 할 경우
					if(CSceneManager.m_oTouchResponderInfoDict.TryGetValue(KCDefine.U_OBJ_N_SCREEN_FADE_TOUCH_RESPONDER, out STTouchResponderInfo stTouchResponderInfo)) {
						stTouchResponderInfo.m_oAni?.Kill();
					}

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
					// 액티브 씬 메인 카메라 데이터가 존재 할 경우
					if(this.UIsCamera != null && CSceneManager.m_oActiveSceneMainCameraData != null && QualitySettings.renderPipeline != null && GraphicsSettings.renderPipelineAsset != null) {
						CSceneManager.m_oActiveSceneMainCameraData.cameraStack?.ExRemoveVal(this.UIsCamera, false);
					}
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
				}

				CSceneManager.ScheduleManager?.RemoveComponent(this);
				CSceneManager.NavStackManager?.RemoveComponent(this);

				CSceneManager.m_oSceneManagerDict.ExRemoveVal(this.SceneName);
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CSceneManager.OnDestroy Exception: {oException.Message}");
		}
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime) {
		base.OnLateUpdate(a_fDeltaTime);

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			var oRemoveDespawnObjInfoList = CCollectionManager.Inst.SpawnList<STDespawnObjInfo>();

			for(int i = 0; i < m_oDespawnObjInfoList.Count; ++i) {
				// 객체 제거가 가능 할 경우
				if(m_oDespawnObjInfoList[i].m_stDespawnTime.ExGetDeltaTime(System.DateTime.Now).ExIsLessEquals(KCDefine.B_VAL_0_DBL)) {
					oRemoveDespawnObjInfoList.Add(m_oDespawnObjInfoList[i]);
				}
			}

			try {
				for(int i = 0; i < oRemoveDespawnObjInfoList.Count; ++i) {
					m_oDespawnObjInfoList.ExRemoveVal((a_stDespawnObjInfo) => oRemoveDespawnObjInfoList[i].m_oObj == a_stDespawnObjInfo.m_oObj);

					// 객체 풀이 존재 할 경우
					if(m_oObjsPoolDict.TryGetValue(oRemoveDespawnObjInfoList[i].m_oKey, out ObjectPool oObjsPool)) {
						oObjsPool.Despawn(oRemoveDespawnObjInfoList[i].m_oObj, oRemoveDespawnObjInfoList[i].m_bIsDestroy);
					}
				}
			} finally {
				CCollectionManager.Inst.DespawnList(oRemoveDespawnObjInfoList);
			}
		}
	}
	
	/** 앱이 종료 되었을 경우 */
	public virtual void OnApplicationQuit() {
		this.DoQuitApp();
		CFunc.EnumerateComponents<CComponent>((a_oComponent) => { CFactory.RemoveObj(a_oComponent); return true; }, true);
	}

	/** 앱을 종료한다 */
	public void QuitApp() {
		this.DoQuitApp();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif			// #if UNITY_EDITOR
	}

	/** 객체 풀을 추가한다 */
	public void AddObjsPool(string a_oKey, ObjectPool a_oObjsPool) {
		CAccess.Assert(a_oObjsPool != null && a_oKey.ExIsValid());
		m_oObjsPoolDict.TryAdd(a_oKey, a_oObjsPool);
	}

	/** 객체 풀을 추가한다 */
	public void AddObjsPool(string a_oKey, string a_oObjPath, GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_OBJS_POOL) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oObjPath.ExIsValid());
		this.AddObjsPool(a_oKey, CResManager.Inst.GetRes<GameObject>(a_oObjPath), a_oParent, a_nNumObjs);
	}

	/** 객체 풀을 추가한다 */
	public void AddObjsPool(string a_oKey, GameObject a_oOrigin, GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_OBJS_POOL) {
		CAccess.Assert(a_oOrigin != null && a_oKey.ExIsValid());
		this.AddObjsPool(a_oKey, CFactory.CreateObjsPool(a_oOrigin, a_oParent, a_nNumObjs));
	}

	/** 객체 풀을 제거한다 */
	public void RemoveObjsPool(string a_oKey, bool a_bIsDestroy = true) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 객체 풀이 존재 할 경우
		if(m_oObjsPoolDict.TryGetValue(a_oKey, out ObjectPool oObjsPool)) {
			oObjsPool.DeAllocate(oObjsPool.CountInactive);
			oObjsPool.DespawnAllActiveObjects(a_bIsDestroy);

			m_oObjsPoolDict.ExRemoveVal(a_oKey);
		}
	}

	/** 화면 페이드 인 애니메이션을 시작한다 */
	public void StartScreenFadeInAni(System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI) {
		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni) {
			CSceneManager.ShowTouchResponder(CSceneManager.ScreenAbsUIs, KCDefine.U_OBJ_N_SCREEN_FADE_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_SCREEN_FADE_TOUCH_RESPONDER, this.ScreenFadeInColor, a_oCallback, true, a_fDuration, this.CreateScreenFadeInAni);
		} else {
			a_oCallback?.Invoke(null);
		}
	}

	/** 화면 페이드 아웃 애니메이션을 시작한다 */
	public void StartScreenFadeOutAni(float a_fDuration = KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI) {
		CSceneManager.CloseTouchResponder(KCDefine.U_OBJ_N_SCREEN_FADE_TOUCH_RESPONDER, this.ScreenFadeOutColor, null, a_fDuration, this.CreateScreenFadeOutAni);
	}

	/** 네트워크 상태가 갱신 되었을 경우 */
	protected virtual void OnUpdateNetworkReachability(NetworkReachability a_eNetworkReachability) {
		CFunc.ShowLog($"CSceneManager.OnUpdateNetworkReachability: {this.SceneName}, {a_eNetworkReachability}", KCDefine.B_LOG_COLOR_INFO);
	}

	/** 비동기 씬 로딩 상태가 갱신 되었을 경우 */
	protected virtual void OnUpdateAsyncSceneLoadingState(AsyncOperation a_oAsyncOperation, bool a_bIsComplete) {
		CFunc.ShowLog($"CSceneManager.OnUpdateAsyncSceneLoadingState: {this.SceneName}, {a_oAsyncOperation.progress}, {a_bIsComplete}", KCDefine.B_LOG_COLOR_INFO);
	}
	#endregion			// 함수

	#region 클래스 함수
	/** 터치 응답자를 출력한다 */
	public static void ShowTouchResponder(GameObject a_oParent, string a_oKey, string a_oObjPath, Color a_stColor, System.Action<GameObject> a_oCallback, bool a_bIsEnableNavStack = false, float a_fDuration = KCDefine.B_VAL_0_FLT, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 없을 경우
		if(!CSceneManager.m_oTouchResponderInfoDict.ContainsKey(a_oKey)) {
			var oTouchResponder = CFactory.CreateTouchResponder(string.Format(KCDefine.U_KEY_FMT_SCENE_M_TOUCH_RESPONDER, a_oKey), KCDefine.U_OBJ_P_G_TOUCH_RESPONDER, a_oParent, CSceneManager.CanvasSize, KCDefine.B_POS_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT);
			(oTouchResponder.transform as RectTransform).sizeDelta = Vector2.zero;
			(oTouchResponder.transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_DOWN_LEFT;
			(oTouchResponder.transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_UP_RIGHT;

			Tween oAni = null;

			// 애니메이션 모드 일 경우
			if(a_oAniCreator != null) {
				// 전환 효과가 존재 할 경우
				if(oTouchResponder.TryGetComponent<UITransitionEffect>(out UITransitionEffect oTransitionFX)) {
					oTransitionFX.updateMode = CSceneManager.ActiveSceneManager.IsRealtimeFadeInAni ? AnimatorUpdateMode.UnscaledTime : AnimatorUpdateMode.Normal;
				}

				oAni = a_oAniCreator(oTouchResponder, a_oKey, a_stColor, a_fDuration);
				(oAni as Sequence).AppendCallback(() => { oAni.Kill(); a_oCallback?.Invoke(oTouchResponder); });
			} else {
				var oImg = oTouchResponder.GetComponentInChildren<Image>();
				oImg.color = a_stColor;

				a_oCallback?.Invoke(oTouchResponder);
			}

			// 터치 전달자를 설정한다 {
			var oTouchDispatcher = oTouchResponder.GetComponentInChildren<CTouchDispatcher>();
			oTouchDispatcher.IsIgnoreNavStackEvent = true;
			oTouchDispatcher.DestroyCallback = (a_oSender) => CSceneManager.m_oTouchResponderInfoDict.ExRemoveVal(a_oKey);

			// 내비게이션 스택이 유효 할 경우
			if(a_bIsEnableNavStack) {
				CNavStackManager.Inst.AddComponent(oTouchDispatcher);
			}
			// 터치 전달자를 설정한다 }

			CSceneManager.m_oTouchResponderInfoDict.TryAdd(a_oKey, new STTouchResponderInfo() {
				m_oAni = oAni, m_oTouchResponder = oTouchResponder
			});
		}
	}
	
	/** 터치 응답자를 닫는다 */
	public static void CloseTouchResponder(string a_oKey, Color a_stColor, System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.B_VAL_0_FLT, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 존재 할 경우
		if(CSceneManager.m_oTouchResponderInfoDict.TryGetValue(a_oKey, out STTouchResponderInfo stTouchResponderInfo)) {
			var oTouchDispatcher = stTouchResponderInfo.m_oTouchResponder.GetComponentInChildren<CTouchDispatcher>();
			oTouchDispatcher.DestroyCallback = null;

			stTouchResponderInfo.m_oAni?.Kill();
			CSceneManager.m_oTouchResponderInfoDict.ExRemoveVal(a_oKey);

			// 내비게이션 스택 콜백이 존재 할 경우
			if(oTouchDispatcher.NavStackCallback != null) {
				CNavStackManager.Inst.RemoveComponent(oTouchDispatcher);
			}

			// 애니메이션 모드 일 경우
			if(a_oAniCreator != null) {
				var oAni = a_oAniCreator(stTouchResponderInfo.m_oTouchResponder, a_oKey, a_stColor, a_fDuration);
				oAni.AppendCallback(() => { oAni.Kill(); a_oCallback?.Invoke(stTouchResponderInfo.m_oTouchResponder); CFactory.RemoveObj(stTouchResponderInfo.m_oTouchResponder); });
			} else {
				CFactory.RemoveObj(stTouchResponderInfo.m_oTouchResponder);
			}
		}
	}

	/** 앱을 종료한다 */
	private void DoQuitApp() {
		DOTween.KillAll(false);

		CSceneManager.IsAwake = false;
		CSceneManager.IsAppQuit = true;
		
#if !UNITY_EDITOR
		Time.timeScale = KCDefine.B_VAL_0_FLT;
#endif			// #if !UNITY_EDITOR
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	/** 기즈모를 그린다 */
	public virtual void OnDrawGizmos() {
		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			var oCanvasPosList = new List<Vector3>() {
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT)
			};

			var oScreenPosList = new List<Vector3>() {
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT)
			};

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
			var oEditorScreenPosList = new List<Vector3>() {
				new Vector3((KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_SCREEN_WIDTH, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT)
			};
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

			for(int i = 0; i < oCanvasPosList.Count; ++i) {
				var stPrevColor = Gizmos.color;
				float fResolutionScale = CAccess.ResolutionScale * KCDefine.B_UNIT_SCALE;

				try {
					int nIdxA = (i + KCDefine.B_VAL_0_INT) % oCanvasPosList.Count;
					int nIdxB = (i + KCDefine.B_VAL_1_INT) % oCanvasPosList.Count;

					Gizmos.color = Color.white;
					Gizmos.DrawLine(oCanvasPosList[nIdxA] * fResolutionScale, oCanvasPosList[nIdxB] * fResolutionScale);

					Gizmos.color = Color.green;
					Gizmos.DrawLine(oScreenPosList[nIdxA] * fResolutionScale, oScreenPosList[nIdxB] * fResolutionScale);

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
					Gizmos.color = Color.cyan;
					Gizmos.DrawLine(oEditorScreenPosList[nIdxA] * fResolutionScale, oEditorScreenPosList[nIdxB] * fResolutionScale);
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
				} finally {
					Gizmos.color = stPrevColor;
				}
			}
		}
	}
#endif			// #if UNITY_EDITOR

#if DEBUG || DEVELOPMENT_BUILD
	/** GUI 를 그린다 */
	public virtual void OnGUI() {
		// 주요 씬 일 경우
		if(this.IsActiveScene && CSceneManager.ActiveSceneMainCamera != null) {
			var stPrevColor = GUI.color;
			
			try {
#if UNITY_STANDALONE
#if DEBUG || DEVELOPMENT_BUILD
				float fScreenHeight = CAccess.CorrectDesktopScreenSize.y;
#else
				float fScreenHeight = CAccess.DesktopScreenSize.y;
#endif			// #if DEBUG || DEVELOPMENT_BUILD
#else
				float fScreenHeight = CAccess.ScreenSize.y;
#endif			// #if UNITY_STANDALONE

#if UNITY_EDITOR
				// 초기화가 필요 할 경우
				if(!CSceneManager.IsInit) {
					var stTextureSize = Camera.main.pixelRect.size / KCDefine.B_VAL_4_FLT;
					var stTextureRect = new Rect(Camera.main.pixelRect.center - (stTextureSize / KCDefine.B_VAL_2_FLT), stTextureSize);

					GUI.color = Color.black;
					GUI.DrawTexture(Camera.main.pixelRect, Texture2D.whiteTexture, ScaleMode.StretchToFill);

					GUI.color = Color.white;
					GUI.DrawTexture(stTextureRect, Resources.Load<Texture2D>(KCDefine.U_IMG_P_G_SPLASH), ScaleMode.ScaleToFit);
				}
#endif			// #if UNITY_EDITOR
			} finally {
				GUI.color = stPrevColor;
			}
		}
	}

	/** 디버그 UI 상태를 갱신한다 */
	private void UpdateDebugUIsState(float a_fDeltaTime) {
		CSceneManager.m_fDebugInfoSkipTime += Mathf.Clamp01(Time.unscaledDeltaTime);

#if INPUT_SYSTEM_MODULE_ENABLE
		bool bIsTimeScaleKeyDown = Keyboard.current.leftShiftKey.isPressed && (Keyboard.current.equalsKey.isPressed || Keyboard.current.minusKey.isPressed);
#else
		bool bIsTimeScaleKeyDown = Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.Minus));
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

		// 시간 비율 키를 눌렀을 경우
		if(CSceneManager.IsAppInit && bIsTimeScaleKeyDown) {
#if INPUT_SYSTEM_MODULE_ENABLE
			float fVelocity = Keyboard.current.equalsKey.isPressed ? KCDefine.B_VAL_1_FLT : -KCDefine.B_VAL_1_FLT;
#else
			float fVelocity = Input.GetKey(KeyCode.Equals) ? KCDefine.B_VAL_1_FLT : -KCDefine.B_VAL_1_FLT;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

			Time.timeScale = Mathf.Clamp01(Time.timeScale + (fVelocity * Time.unscaledDeltaTime));
		}

		// 디버그 정보 갱신 주기가 지났을 경우
		if(CSceneManager.m_fDebugInfoSkipTime.ExIsGreateEquals(KCDefine.B_VAL_1_FLT)) {
			CSceneManager.m_fDebugInfoSkipTime = KCDefine.B_VAL_0_FLT;

			// 정적 디버그 텍스트가 존재 할 경우
			if(CSceneManager.ScreenStaticDebugText != null) {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
				string oStr = string.Format(KCDefine.U_TEXT_FMT_STATIC_DEBUG_MSG, CSceneManager.m_oStaticDebugStrBuilder.ToString(), CSceneManager.m_oExtraStaticDebugStrBuilder.ToString());
				CSceneManager.ScreenStaticDebugText.ExSetText(oStr, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			}

			// 동적 디버그 텍스트가 존재 할 경우
			if(CSceneManager.ScreenDynamicDebugText != null) {
				CSceneManager.m_oDynamicDebugStrBuilder.Clear();
				CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_INFO_A, Profiler.usedHeapSizeLong.ExByteToMegaByte(), Profiler.GetAllocatedMemoryForGraphicsDriver().ExByteToMegaByte());
				CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_INFO_B, Profiler.GetMonoHeapSizeLong().ExByteToMegaByte(), Profiler.GetMonoUsedSizeLong().ExByteToMegaByte());
				CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_INFO_C, Profiler.GetTempAllocatorSize().ExByteToMegaByte(), Profiler.GetTotalAllocatedMemoryLong().ExByteToMegaByte());
				CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_INFO_D, Profiler.GetTotalReservedMemoryLong().ExByteToMegaByte(), Profiler.GetTotalUnusedReservedMemoryLong().ExByteToMegaByte());
				CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_INFO_E, Time.timeScale);

#if NEWTON_SOFT_JSON_MODULE_ENABLE
				string oStr = string.Format(KCDefine.U_TEXT_FMT_DYNAMIC_DEBUG_MSG, CSceneManager.m_oDynamicDebugStrBuilder.ToString(), CSceneManager.m_oExtraDynamicDebugStrBuilder.ToString());
				CSceneManager.ScreenDynamicDebugText.ExSetText(oStr, CLocalizeInfoTable.Inst.GetFontSetInfo(EFontSet.A));
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			}
		}
	}

	/** FPS 정보 UI 상태를 갱신한다 */
	private void UpdateFPSInfoUIsState(float a_fDeltaTime) {
		CSceneManager.m_nNumFrames += KCDefine.B_VAL_1_INT;
		CSceneManager.m_fFPSInfoSkipTime += Mathf.Clamp01(Time.unscaledDeltaTime);

		// FPS 정보 갱신 주기가 지났을 경우
		if(CSceneManager.m_fFPSInfoSkipTime.ExIsGreateEquals(KCDefine.B_VAL_1_FLT)) {
			CSceneManager.ScreenFPSText?.ExSetText<TMP_Text>(string.Format(KCDefine.U_TEXT_FMT_FPS, CSceneManager.m_nNumFrames), false);
			CSceneManager.ScreenFrameTimeText?.ExSetText<TMP_Text>(string.Format(KCDefine.U_TEXT_FMT_FRAME_TIME, (CSceneManager.m_fFPSInfoSkipTime / CSceneManager.m_nNumFrames) * KCDefine.B_UNIT_MILLI_SECS_PER_SEC), false);

			CSceneManager.m_nNumFrames = KCDefine.B_VAL_0_INT;
			CSceneManager.m_fFPSInfoSkipTime -= KCDefine.B_VAL_1_FLT;
		}
	}

	/** 화면 FPS 정보 버튼을 눌렀을 경우 */
	private static void OnTouchScreenFPSInfoBtn() {
		CSceneManager.ScreenFPSInfoUIs.SetActive(!CSceneManager.ScreenFPSInfoUIs.activeSelf);
	}
	
	/** 화면 디버그 정보 버튼을 눌렀을 경우 */
	private static void OnTouchScreenDebugInfoBtn() {
		CSceneManager.ScreenDebugInfoUIs.SetActive(!CSceneManager.ScreenDebugInfoUIs.activeSelf);
	}

	/** 화면 시간 비율 증가 버튼을 눌렀을 경우 */
	private static void OnTouchScreenTimeScaleUpBtn() {
		Time.timeScale = Mathf.Clamp01(Time.timeScale + (KCDefine.B_VAL_1_FLT / (KCDefine.B_UNIT_DIGITS_PER_TEN * KCDefine.B_VAL_2_FLT)));
	}

	/** 화면 시간 비율 감소 버튼을 눌렀을 경우 */
	private static void OnTouchScreenTimeScaleDownBtn() {
		Time.timeScale = Mathf.Clamp01(Time.timeScale - (KCDefine.B_VAL_1_FLT / (KCDefine.B_UNIT_DIGITS_PER_TEN * KCDefine.B_VAL_2_FLT)));
	}
#endif			// #if DEBUG || DEVELOPMENT_BUILD
	#endregion			// 조건부 클래스 함수
}
