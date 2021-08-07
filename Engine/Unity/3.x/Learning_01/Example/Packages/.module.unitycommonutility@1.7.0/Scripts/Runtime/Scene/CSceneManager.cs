using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Coffee.UIExtensions;

#if INPUT_SYSTEM_MODULE_ENABLE
using UnityEngine.InputSystem;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
using UnityEngine.Profiling;
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

//! 씬 관리자
public abstract partial class CSceneManager : CComponent {
	#region 변수
	private bool m_bIsSetupTransforms = false;
	private Dictionary<string, ObjectPool> m_oObjsPoolDict = new Dictionary<string, ObjectPool>();
	#endregion			// 변수

	#region 클래스 변수
	private static Dictionary<string, STSequenceInfo> m_oSequenceInfoDict = new Dictionary<string, STSequenceInfo>();
	
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	private static float m_fDebugSkipTime = 0.0f;

	private static System.Text.StringBuilder m_oStaticDebugStrBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oDynamicDebugStrBuilder = new System.Text.StringBuilder();

	private static System.Text.StringBuilder m_oExtraStaticDebugStrBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oExtraDynamicDebugStrBuilder = new System.Text.StringBuilder();
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 클래스 변수

	#region 클래스 컴포넌트
	private static Dictionary<string, CSceneManager> m_oSubSceneManagerDict = new Dictionary<string, CSceneManager>();
	#endregion			// 클래스 컴포넌트

	#region 프로퍼티
	public abstract string SceneName { get; }

	// UI {
	public GameObject SubUIsTop { get; private set; } = null;
	public GameObject SubUIsBase { get; private set; } = null;

	public GameObject SubUIs { get; private set; } = null;
	public GameObject SubTestUIs { get; private set; } = null;
	public GameObject SubPivotUIs { get; private set; } = null;
	public GameObject SubAnchorUIs { get; private set; } = null;
	// UI }

	// 고정 UI
	public GameObject SubUpUIs { get; private set; } = null;
	public GameObject SubDownUIs { get; private set; } = null;
	public GameObject SubLeftUIs { get; private set; } = null;
	public GameObject SubRightUIs { get; private set; } = null;

	// 팝업 UI
	public GameObject SubPopupUIs { get; private set; } = null;
	public GameObject SubTopmostUIs { get; private set; } = null;

	// 객체 {
	public GameObject SubBase { get; private set; } = null;
	public GameObject SubObjsBase { get; private set; } = null;

	public GameObject SubObjs { get; private set; } = null;
	public GameObject SubPivotObjs { get; private set; } = null;
	public GameObject SubAnchorObjs { get; private set; } = null;
	// 객체 }

	// 고정 객체
	public GameObject SubUpObjs { get; private set; } = null;
	public GameObject SubDownObjs { get; private set; } = null;
	public GameObject SubLeftObjs { get; private set; } = null;
	public GameObject SubRightObjs { get; private set; } = null;

	// 캔버스 객체 {
	public GameObject SubObjsCanvasTop { get; private set; } = null;
	public GameObject SubObjsCanvasBase { get; private set; } = null;

	public GameObject SubCanvasObjs { get; private set; } = null;
	public GameObject SubCanvasPivotObjs { get; private set; } = null;
	// 캔버스 객체 }

	// 카메라
	public Camera SubUIsCamera { get; private set; } = null;
	public Camera SubMainCamera { get; private set; } = null;

	// 캔버스 {
	public Canvas SubUIsCanvas { get; private set; } = null;
	public Canvas SubPopupUIsCanvas { get; private set; } = null;
	public Canvas SubTopmostUIsCanvas { get; private set; } = null;

	public Canvas SubObjsCanvas { get; private set; } = null;
	// 캔버스 }

	public bool IsRootScene => CSceneManager.RootScene == this.gameObject.scene;

	public virtual bool IsRealtimeFadeInAni => false;
	public virtual bool IsRealtimeFadeOutAni => false;

	public virtual float PlaneDistance => KCDefine.U_DISTANCE_CAMERA_PLANE;
	public virtual float FadeOutAniDuration => KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI;

	public virtual float UIsCameraDepth => KCDefine.U_DEPTH_UIS_CAMERA;
	public virtual float MainCameraDepth => KCDefine.U_DEPTH_MAIN_CAMERA;

	public virtual Color ClearColor => KCDefine.U_COLOR_CAMERA_BG;
	
	public virtual Color ScreenFadeInColor => KCDefine.U_COLOR_SCREEN_FADE_IN;
	public virtual Color ScreenFadeOutColor => KCDefine.U_COLOR_SCREEN_FADE_OUT;

	public virtual STSortingOrderInfo UIsCanvasSortingOrderInfo => KCDefine.U_SORTING_OI_UIS_CANVAS;
	public virtual STSortingOrderInfo ObjsCanvasSortingOrderInfo => KCDefine.U_SORTING_OI_OBJS_CANVAS;
	
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
	#endregion			// 프로퍼티

	#region 클래스 프로퍼티
	public static bool IsInit { get; set; } = false;
	public static bool IsSetup { get; set; } = false;
	public static bool IsLateSetup { get; set; } = false;

	public static bool IsAwake { get; private set; } = false;
	public static bool IsAppQuit { get; protected set; } = false;

	// UI 간격
	public static float UpUIsOffset { get; private set; } = 0.0f;
	public static float DownUIsOffset { get; private set; } = 0.0f;
	public static float LeftUIsOffset { get; private set; } = 0.0f;
	public static float RightUIsOffset { get; private set; } = 0.0f;

	// 최상단 UI 간격
	public static float UpUIsRootOffset { get; private set; } = 0.0f;
	public static float DownUIsRootOffset { get; private set; } = 0.0f;
	public static float LeftUIsRootOffset { get; private set; } = 0.0f;
	public static float RightUIsRootOffset { get; private set; } = 0.0f;

	// 객체 간격
	public static float UpObjOffset { get; private set; } = 0.0f;
	public static float DownObjOffset { get; private set; } = 0.0f;
	public static float LeftObjOffset { get; private set; } = 0.0f;
	public static float RightObjOffset { get; private set; } = 0.0f;

	// 최상단 객체 간격
	public static float UpObjsRootOffset { get; private set; } = 0.0f;
	public static float DownObjsRootOffset { get; private set; } = 0.0f;
	public static float LeftObjsRootOffset { get; private set; } = 0.0f;
	public static float RightObjsRootOffset { get; private set; } = 0.0f;

	// 캔버스 크기
	public static Vector3 CanvasSize { get; protected set; } = Vector3.zero;
	public static Vector3 CanvasScale { get; protected set; } = Vector3.zero;

	// 카메라
	public static Camera UIsCamera { get; private set; } = null;
	public static Camera MainCamera { get; private set; } = null;

	// 캔버스 {
	public static Canvas UIsCanvas { get; private set; } = null;
	public static Canvas PopupUIsCanvas { get; private set; } = null;
	public static Canvas TopmostUIsCanvas { get; private set; } = null;

	public static Canvas ObjsCanvas { get; private set; } = null;
	// 캔버스 }

	// 기본 객체 {
	public static EventSystem EventSystem { get; private set; } = null;
	public static BaseInputModule BaseInputModule { get; private set; } = null;

	public static CSceneManager RootSceneManager { get; private set; } = null;
	// 기본 객체 }

	// UI {
	public static GameObject UIsTop { get; private set; } = null;
	public static GameObject UIsBase { get; private set; } = null;

	public static GameObject UIs { get; private set; } = null;
	public static GameObject PivotUIs { get; private set; } = null;
	public static GameObject AnchorUIs { get; private set; } = null;
	// UI }

	// 고정 UI
	public static GameObject UpUIs { get; private set; } = null;
	public static GameObject DownUIs { get; private set; } = null;
	public static GameObject LeftUIs { get; private set; } = null;
	public static GameObject RightUIs { get; private set; } = null;

	// 팝업 UI
	public static GameObject PopupUIs { get; private set; } = null;
	public static GameObject TopmostUIs { get; private set; } = null;

	// 화면 UI
	public static GameObject ScreenBlindUIs { get; protected set; } = null;
	public static GameObject ScreenPopupUIs { get; protected set; } = null;
	public static GameObject ScreenTopmostUIs { get; protected set; } = null;
	public static GameObject ScreenAbsUIs { get; protected set; } = null;

	// 객체 {
	public static GameObject Base { get; private set; } = null;
	public static GameObject ObjsBase { get; private set; } = null;

	public static GameObject Objs { get; private set; } = null;
	public static GameObject PivotObjs { get; private set; } = null;
	public static GameObject AnchorObjs { get; private set; } = null;
	// 객체 }

	// 고정 객체
	public static GameObject UpObjs { get; private set; } = null;
	public static GameObject DownObjs { get; private set; } = null;
	public static GameObject LeftObjs { get; private set; } = null;
	public static GameObject RightObjs { get; private set; } = null;

	// 캔버스 객체 {
	public static GameObject ObjsCanvasTop { get; private set; } = null;
	public static GameObject ObjsCanvasBase { get; private set; } = null;

	public static GameObject CanvasObjs { get; private set; } = null;
	public static GameObject CanvasPivotObjs { get; private set; } = null;
	// 캔버스 객체 }

	public static string AwakeSceneName { get; private set; } = string.Empty;

	public static bool IsAppInit => CSceneManager.IsInit && CSceneManager.IsSetup && CSceneManager.IsLateSetup;
	public static bool IsAppRunning => CSceneManager.IsInit && !CSceneManager.IsAppQuit;
	public static bool IsExistsMainCamera => Camera.main != null && CSceneManager.MainCamera != null;

	public static Scene RootScene => SceneManager.GetActiveScene();
	public static string RootSceneName => CSceneManager.RootScene.name;

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	public static Text ScreenStaticDebugText { get; protected set; } = null;
	public static Text ScreenDynamicDebugText { get; protected set; } = null;

	public static Button ScreenFPSBtn { get; protected set; } = null;
	public static Button ScreenDebugBtn { get; protected set; } = null;

	public static GameObject ScreenDebugUIs { get; protected set; } = null;
	public static GameObject ScreenDebugTexts { get; protected set; } = null;

	public static GameObject ScreenDebugConsole { get; protected set; } = null;
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	public static Text ScreenStaticFPSText { get; protected set; } = null;
	public static Text ScreenDynamicFPSText { get; protected set; } = null;
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 클래스 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.IsIgnoreNavStackEvent = true;

		// 씬이 처음 로딩 되었을 경우
		if(!CSceneManager.IsAwake) {
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
			CSceneManager.IsSetup = false;

			CSceneManager.IsAwake = true;
			CSceneManager.AwakeSceneName = CSceneManager.RootSceneName;
			
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Application.targetFrameRate = KCDefine.B_TARGET_FRAME_RATE;

			// 초기화 씬이 아닐 경우
			if(!CSceneManager.AwakeSceneName.ExIsEquals(KCDefine.B_SCENE_N_INIT)) {
				this.ExLateCallFunc((a_oSender, a_oParams) => {
					CScheduleManager.Inst.RemoveComponent(this);
					CNavStackManager.Inst.RemoveComponent(this);

					CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_INIT, false, false);
				}, KCDefine.U_DELAY_INIT);
			}
		}

		this.SetupScene();
	}

	//! 초기화
	public override void Start() {
		base.Start();
		CIndicatorManager.Inst.Close();

#if UNITY_EDITOR
		// 루트 씬 일 경우
		if(this.IsRootScene) {
			CFunc.SelObjs(new GameObject[] {
				this.SubUIs, 
				this.SubObjs, 
				this.SubCanvasObjs
			});
		}
#endif			// #if UNITY_EDITOR
	}

	//! 상태를 갱신한다
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			// 루트 씬 일 경우
			if(this.IsRootScene) {
#if INPUT_SYSTEM_MODULE_ENABLE
				bool bIsBackKeyDown = Keyboard.current.escapeKey.wasPressedThisFrame;
#else
				bool bIsBackKeyDown = Input.GetKeyDown(KeyCode.Escape);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				// 백 키를 눌렀을 경우
				if(CSceneManager.IsAppInit && bIsBackKeyDown) {
					bool bIsEnableBack = true;
					CSndManager.Inst.PlayFXSnd(KCDefine.U_SND_P_G_TOUCH_END);

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
					var oCanvasGroup = CSceneManager.ScreenDebugConsole.GetComponentInChildren<CanvasGroup>();
					bIsEnableBack = oCanvasGroup.alpha.ExIsLessEquals(KCDefine.B_VAL_0_FLT);
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

					// 백 키 처리가 가능 할 경우
					if(bIsEnableBack) {
						CNavStackManager.Inst.SendNavStackEvent(ENavStackEvent.BACK_KEY_DOWN);
					}
				}

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
#if INPUT_SYSTEM_MODULE_ENABLE
				bool bIsTimeScaleKeyDown = Keyboard.current.rightShiftKey.isPressed && (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed);
#else
				bool bIsTimeScaleKeyDown = Input.GetKey(KeyCode.RightShift) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow));
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				// 방향 키를 눌렀을 경우
				if(CSceneManager.IsAppInit && bIsTimeScaleKeyDown) {
#if INPUT_SYSTEM_MODULE_ENABLE
					float fVelocity = Keyboard.current.upArrowKey.isPressed ? KCDefine.B_VAL_1_FLT : -KCDefine.B_VAL_1_FLT;
#else
					float fVelocity = Input.GetKey(KeyCode.UpArrow) ? KCDefine.B_VAL_1_FLT : -KCDefine.B_VAL_1_FLT;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

					float fTime = KCDefine.B_VAL_1_FLT / (float)Application.targetFrameRate;
					Time.timeScale = Mathf.Clamp(Time.timeScale + (fVelocity * fTime), KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_1_FLT);
				}

				// 디버그 UI 루트가 존재 할 경우
				if(CSceneManager.ScreenDebugUIs != null) {
#if INPUT_SYSTEM_MODULE_ENABLE
					bool bIsDebugKeyDown = Keyboard.current.rightShiftKey.isPressed && Keyboard.current.digit1Key.wasPressedThisFrame;
					bool bIsDynamicDebugKeyDown = Keyboard.current.rightShiftKey.isPressed && Keyboard.current.digit2Key.wasPressedThisFrame;
#else
					bool bIsDebugKeyDown = Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha1);
					bool bIsDynamicDebugKeyDown = Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha2);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

					// 디버그 키를 눌렀을 경우
					if(bIsDebugKeyDown) {
						CSceneManager.OnTouchDebugBtn();
					}
					// 동적 디버그 키를 눌렀을 경우
					else if(bIsDynamicDebugKeyDown) {
						CSceneManager.m_oExtraDynamicDebugStrBuilder.Clear();
					}
				}

				CSceneManager.m_fDebugSkipTime += CScheduleManager.Inst.UnscaleDeltaTime;

				// 디버그 정보 갱신 주기가 지났을 경우
				if(CSceneManager.m_fDebugSkipTime.ExIsGreateEquals(KCDefine.U_DELTA_T_DYNAMIC_DEBUG)) {
					CSceneManager.m_fDebugSkipTime = KCDefine.B_VAL_0_FLT;

					// 정적 디버그 텍스트가 존재 할 경우
					if(CSceneManager.ScreenStaticDebugText != null) {
						string oStaticDebugStr = CSceneManager.m_oStaticDebugStrBuilder.ToString();
						string oExtraStaticDebugStr = CSceneManager.m_oExtraStaticDebugStrBuilder.ToString();

						CSceneManager.ScreenStaticDebugText.text = string.Format(KCDefine.U_FMT_STATIC_DEBUG_MSG, oStaticDebugStr, oExtraStaticDebugStr);
					}

					// 동적 디버그 텍스트가 존재 할 경우
					if(CSceneManager.ScreenDynamicDebugText != null) {
						CSceneManager.m_oDynamicDebugStrBuilder.Clear();
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_A, System.GC.GetTotalMemory(false).ExByteToMegaByte(), Profiler.usedHeapSizeLong.ExByteToMegaByte());
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_B, Profiler.GetMonoHeapSizeLong().ExByteToMegaByte(), Profiler.GetMonoUsedSizeLong().ExByteToMegaByte());
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_C, Profiler.GetTempAllocatorSize().ExByteToMegaByte(), Profiler.GetTotalAllocatedMemoryLong().ExByteToMegaByte());
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_D, Profiler.GetTotalReservedMemoryLong().ExByteToMegaByte(), Profiler.GetTotalUnusedReservedMemoryLong().ExByteToMegaByte());
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_E, Profiler.GetAllocatedMemoryForGraphicsDriver().ExByteToMegaByte(), Time.timeScale);
						CSceneManager.m_oDynamicDebugStrBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_F, KCDefine.B_DIR_P_WRITABLE);

						string oDynamicDebugStr = CSceneManager.m_oDynamicDebugStrBuilder.ToString();
						string oExtraDynamicDebugStr = CSceneManager.m_oExtraDynamicDebugStrBuilder.ToString();

						CSceneManager.ScreenDynamicDebugText.text = string.Format(KCDefine.U_FMT_DYNAMIC_DEBUG_MSG, oDynamicDebugStr, oExtraDynamicDebugStr);
					}
				}
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
			}

			// 트랜스 폼 설정이 가능 할 경우
			if(!m_bIsSetupTransforms) {
				m_bIsSetupTransforms = true;
				var oObjs = this.gameObject.scene.GetRootGameObjects();

				for(int i = oObjs.Length - KCDefine.B_VAL_1_INT; i >= KCDefine.B_VAL_0_INT; --i) {
					var oTransforms = oObjs[i].GetComponentsInChildren<RectTransform>();

					for(int j = oTransforms.Length - KCDefine.B_VAL_1_INT; j >= KCDefine.B_VAL_0_INT; --j) {
						LayoutRebuilder.ForceRebuildLayoutImmediate(oTransforms[j]);
					}
				}
			}
		}
	}

	//! 제거 되었을 경우
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			System.GC.Collect();
			Resources.UnloadUnusedAssets();

			// 루트 씬 일 경우
			if(this.IsRootScene) {
				CScheduleManager.Inst.RemoveComponent(this);
				CNavStackManager.Inst.RemoveComponent(this);

				// 화면 터치 응답자가 존재 할 경우
				if(CSceneManager.m_oSequenceInfoDict.ContainsKey(KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER)) {
					var stSequenceInfo = CSceneManager.m_oSequenceInfoDict[KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER];
					stSequenceInfo.m_oSequence?.Kill();
				}
			}
			
			CSceneManager.m_oSubSceneManagerDict.ExRemoveVal(this.SceneName);
		}
	}
	
	//! 앱이 종료 되었을 경우
	public virtual void OnApplicationQuit() {
		DOTween.KillAll(false);
		CSceneManager.IsAppQuit = true;
	}

	//! 객체 풀을 반환한다
	public ObjectPool GetObjsPool(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oObjsPoolDict.ExGetVal(a_oKey, null);
	}

	//! 앱을 종료한다
	public void QuitApp() {
		this.ExLateCallFunc((a_oSender, a_oParams) => {
			CSceneManager.IsAppQuit = true;
			Time.timeScale = KCDefine.B_VAL_0_FLT;

#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif			// #if UNITY_EDITOR
		});
	}

	//! 객체 풀을 추가한다
	public void AddObjsPool(string a_oKey, ObjectPool a_oObjsPool) {
		CAccess.Assert(a_oObjsPool != null && a_oKey.ExIsValid());
		m_oObjsPoolDict.ExAddVal(a_oKey, a_oObjsPool);
	}

	//! 객체 풀을 추가한다
	public void AddObjsPool(string a_oKey, string a_oObjPath, GameObject a_oParent) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		this.AddObjsPool(a_oKey, oObj, a_oParent);
	}

	//! 객체 풀을 추가한다
	public void AddObjsPool(string a_oKey, GameObject a_oOrigin, GameObject a_oParent) {
		CAccess.Assert(a_oOrigin != null && a_oKey.ExIsValid());
		var oObjsPool = CFactory.CreateObjsPool(a_oOrigin, a_oParent);

		this.AddObjsPool(a_oKey, oObjsPool);
	}

	//! 객체 풀을 제거한다
	public void RemoveObjsPool(string a_oKey, bool a_bIsDestroy = true) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 객체 풀이 존재 할 경우
		if(m_oObjsPoolDict.TryGetValue(a_oKey, out ObjectPool oObjsPool)) {
			oObjsPool.DeAllocate(oObjsPool.CountInactive);
			oObjsPool.DespawnAllActiveObjects(a_bIsDestroy);

			m_oObjsPoolDict.ExRemoveVal(a_oKey);
		}
	}

	//! 객체를 활성화한다
	public GameObject SpawnObj(string a_oKey, string a_oName) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj(a_oKey, a_oName, Vector3.zero);
	}

	//! 객체를 활성화한다
	public GameObject SpawnObj(string a_oKey, string a_oName, Vector3 a_stPos) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj(a_oKey, a_oName, Vector3.one, Vector3.zero, a_stPos);
	}

	//! 객체를 활성화한다
	public GameObject SpawnObj(string a_oKey, string a_oName, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());

		var oObj = m_oObjsPoolDict[a_oKey].Spawn(a_oName);
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		return oObj;
	}

	//! 객체를 비활성화한다
	public void DespawnObj(string a_oKey, GameObject a_oObj, bool a_bIsDestroy = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 객체 비활성화가 가능 할 경우
		if(m_oObjsPoolDict.ContainsKey(a_oKey)) {
			m_oObjsPoolDict[a_oKey].Despawn(a_oObj, a_bIsDestroy);
		}
	}

	//! 화면 페이드 인 애니메이션을 시작한다
	public void StartScreenFadeInAni(System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI) {
		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni) {
			string oName = KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER;
			CSceneManager.ShowTouchResponder(CSceneManager.ScreenAbsUIs, oName, this.ScreenFadeInColor, a_oCallback, true, a_fDuration, this.CreateScreenFadeInAni);
		} else {
			a_oCallback?.Invoke(null);
		}
	}

	//! 화면 페이드 아웃 애니메이션을 시작한다
	public void StartScreenFadeOutAni(float a_fDuration = KCDefine.U_DURATION_SCREEN_FADE_OUT_ANI) {
		string oName = KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER;
		CSceneManager.CloseTouchResponder(oName, this.ScreenFadeOutColor, null, a_fDuration, this.CreateScreenFadeOutAni);
	}

	//! 화면 페이드 인 애니메이션을 생성한다
	protected virtual Sequence CreateScreenFadeInAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
#if DOTWEEN_ENABLE
		var oImg = a_oTarget.GetComponentInChildren<Image>();
		var oAni = oImg.DOColor(a_stColor, a_fDuration).SetEase(KCDefine.U_EASE_ANI);

		return CFactory.MakeSequence(oAni, null, KCDefine.B_VAL_0_FLT, KCDefine.U_EASE_ANI, false, this.IsRealtimeFadeInAni);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}

	//! 화면 페이드 아웃 애니메이션을 생성한다
	protected virtual Sequence CreateScreenFadeOutAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
#if DOTWEEN_ENABLE
		var oImg = a_oTarget.GetComponentInChildren<Image>();
		var oAni = oImg.DOColor(a_stColor, a_fDuration).SetEase(KCDefine.U_EASE_ANI);

		return CFactory.MakeSequence(oAni, null, KCDefine.B_VAL_0_FLT, KCDefine.U_EASE_ANI, false, this.IsRealtimeFadeOutAni);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 터치 응답자를 반환한다
	public static GameObject GetTouchResponder(string a_oKey) {
		return CSceneManager.m_oSequenceInfoDict.ContainsKey(a_oKey) ? CSceneManager.m_oSequenceInfoDict[a_oKey].m_oObj : null;
	}
	
	//! 터치 응답자를 출력한다
	public static void ShowTouchResponder(GameObject a_oParent, string a_oKey, Color a_stColor, System.Action<GameObject> a_oCallback, bool a_bIsEnableNavStack = false, float a_fDuration = KCDefine.B_VAL_0_FLT, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 없을 경우
		if(!CSceneManager.m_oSequenceInfoDict.ContainsKey(a_oKey)) {
			string oName = string.Format(KCDefine.U_KEY_FMT_SCENE_M_TOUCH_RESPONDER, a_oKey);
			var oTouchResponder = CFactory.CreateTouchResponder(oName, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER, a_oParent, CSceneManager.CanvasSize, KCDefine.B_POS_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT);

			Sequence oSequence = null;

			// 애니메이션 모드 일 경우
			if(a_oAniCreator != null) {
				var oTransitionFX = oTouchResponder.ExAddComponent<UITransitionEffect>();
				oTransitionFX.updateMode = AnimatorUpdateMode.Normal;

				oSequence = a_oAniCreator(oTouchResponder, a_oKey, a_stColor, a_fDuration);
				CAccess.Assert(oSequence != null);

				oSequence.AppendCallback(() => {
					oSequence.Kill();
					a_oCallback?.Invoke(oTouchResponder);
				});
			} else {
				var oImg = oTouchResponder.GetComponentInChildren<Image>();
				oImg.color = a_stColor;

				a_oCallback?.Invoke(oTouchResponder);
			}

			// 터치 전달자를 설정한다 {
			var oTouchDispatcher = oTouchResponder.GetComponentInChildren<CTouchDispatcher>();
			oTouchDispatcher.IsIgnoreNavStackEvent = true;
			oTouchDispatcher.DestroyCallback = (a_oSender) => CSceneManager.m_oSequenceInfoDict.ExRemoveVal(a_oKey);

			// 내비게이션 스택이 유효 할 경우
			if(a_bIsEnableNavStack) {
				CNavStackManager.Inst.AddComponent(oTouchDispatcher);
			}
			// 터치 전달자를 설정한다 }

			CSceneManager.m_oSequenceInfoDict.ExAddVal(a_oKey, new STSequenceInfo() {
				m_oObj = oTouchResponder,
				m_oSequence = oSequence
			});
		}
	}
	
	//! 터치 응답자를 닫는다
	public static void CloseTouchResponder(string a_oKey, Color a_stColor, System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.B_VAL_0_FLT, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 존재 할 경우
		if(CSceneManager.m_oSequenceInfoDict.TryGetValue(a_oKey, out STSequenceInfo stSequenceInfo)) {
			var oTouchDispatcher = stSequenceInfo.m_oObj.GetComponentInChildren<CTouchDispatcher>();
			CAccess.Assert(oTouchDispatcher != null);

			oTouchDispatcher.DestroyCallback = null;

			stSequenceInfo.m_oSequence?.Kill();
			CSceneManager.m_oSequenceInfoDict.ExRemoveVal(a_oKey);

			// 내비게이션 스택 콜백이 존재 할 경우
			if(oTouchDispatcher.NavStackCallback != null) {
				CNavStackManager.Inst.RemoveComponent(oTouchDispatcher);
			}

			// 애니메이션 모드 일 경우
			if(a_oAniCreator != null) {
				var oAni = a_oAniCreator(stSequenceInfo.m_oObj, a_oKey, a_stColor, a_fDuration);
				CAccess.Assert(oAni != null);

				oAni.AppendCallback(() => {
					oAni.Kill();
					a_oCallback?.Invoke(stSequenceInfo.m_oObj);

					Destroy(stSequenceInfo.m_oObj);
				});
			} else {
				Destroy(stSequenceInfo.m_oObj);
			}
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 함수
	//! 객체를 활성화한다
	public T SpawnObj<T>(string a_oKey, string a_oName) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj<T>(a_oKey, a_oName, Vector3.zero);
	}

	//! 객체를 활성화한다
	public T SpawnObj<T>(string a_oKey, string a_oName, Vector3 a_stPos) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj<T>(a_oKey, a_oName, Vector3.one, Vector3.zero, a_stPos);
	}

	//! 객체를 활성화한다
	public T SpawnObj<T>(string a_oKey, string a_oName, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		var oObj = this.SpawnObj(a_oKey, a_oName, a_stScale, a_stAngle, a_stPos);

		return oObj?.GetComponentInChildren<T>();
	}
	#endregion			// 제네릭 함수

	#region 제네릭 클래스 함수
	//! 서브 씬 관리자를 반환한다
	public static T GetSubSceneManager<T>(string a_oKey) where T : CSceneManager {
		return CSceneManager.m_oSubSceneManagerDict.ExGetVal(a_oKey, null) as T;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! GUI 를 그린다
	public virtual void OnGUI() {
		//! 초기화가 필요 할 경우
		if(!CSceneManager.IsInit && Camera.main != null) {
			var stPrevColor = GUI.color;

			try {
				GUI.color = Color.black;
				GUI.DrawTexture(Camera.main.pixelRect, Texture2D.whiteTexture, ScaleMode.StretchToFill);

				var oTexture = Resources.Load<Texture2D>(KCDefine.U_IMG_P_G_SPLASH);
				var stTextureSize = Camera.main.pixelRect.size / KCDefine.B_VAL_4_FLT;
				var stTextureRect = new Rect(Camera.main.pixelRect.center - (stTextureSize / KCDefine.B_VAL_2_FLT), stTextureSize);

				GUI.color = Color.white;
				GUI.DrawTexture(stTextureRect, oTexture, ScaleMode.ScaleToFit);
			} finally {
				GUI.color = stPrevColor;
			}
		}
	}

	//! 가이드 라인을 그린다
	public virtual void OnDrawGizmos() {
		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			var oCanvasPositions = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};

			var oScreenPositions = new Vector3[] {
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
			var oEditorScreenPositions = new Vector3[] {
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT)
			};
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

#if ADS_MODULE_ENABLE
			var stBannerAdsSize = (CAccess.DeviceType == EDeviceType.PHONE) ? KCDefine.U_SIZE_PHONE_BANNER_ADS : KCDefine.U_SIZE_TABLET_BANNER_ADS;
			float fBannerAdsHeight = CAccess.GetBannerAdsHeight(stBannerAdsSize.y);

			var oAdsScreenPositions = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + fBannerAdsHeight, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + fBannerAdsHeight, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};
#endif			// #if ADS_MODULE_ENABLE

			for(int i = 0; i < oCanvasPositions.Length; ++i) {
				var stPrevColor = Gizmos.color;

				try {
					int nIdxA = (i + KCDefine.B_VAL_0_INT) % oCanvasPositions.Length;;
					int nIdxB = (i + KCDefine.B_VAL_1_INT) % oCanvasPositions.Length;

					Gizmos.color = Color.white;
					Gizmos.DrawLine(oCanvasPositions[nIdxA], oCanvasPositions[nIdxB]);

					Gizmos.color = Color.green;
					Gizmos.DrawLine(oScreenPositions[nIdxA], oScreenPositions[nIdxB]);

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
					Gizmos.color = Color.cyan;
					Gizmos.DrawLine(oEditorScreenPositions[nIdxA], oEditorScreenPositions[nIdxB]);
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

#if ADS_MODULE_ENABLE
					Gizmos.color = Color.red;
					Gizmos.DrawLine(oAdsScreenPositions[nIdxA], oAdsScreenPositions[nIdxB]);
#endif			// #if ADS_MODULE_ENABLE
				} finally {
					Gizmos.color = stPrevColor;
				}
			}
		}
	}
#endif			// #if UNITY_EDITOR

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	//! 정적 문자열을 변경한다
	public static void SetStaticStr(string a_oStr) {
		CSceneManager.m_oExtraStaticDebugStrBuilder.Clear();
		CSceneManager.m_oExtraStaticDebugStrBuilder.Append(a_oStr);
	}

	//! 동적 문자열을 변경한다
	public static void SetDynamicStr(string a_oStr) {
		CSceneManager.m_oExtraDynamicDebugStrBuilder.Clear();
		CSceneManager.m_oExtraDynamicDebugStrBuilder.Append(a_oStr);
	}

	//! 디버그 버튼을 눌렀을 경우
	private static void OnTouchDebugBtn() {
		CSceneManager.ScreenDebugTexts.SetActive(!CSceneManager.ScreenDebugTexts.activeSelf);
	}
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

#if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	//! FPS 버튼을 눌렀을 경우
	private static void OnTouchFPSBtn() {
		CSceneManager.ScreenStaticFPSText.enabled = !CSceneManager.ScreenStaticFPSText.enabled;
		CSceneManager.ScreenDynamicFPSText.enabled = !CSceneManager.ScreenDynamicFPSText.enabled;
	}
#endif			// #if FPS_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 조건부 클래스 함수
}
