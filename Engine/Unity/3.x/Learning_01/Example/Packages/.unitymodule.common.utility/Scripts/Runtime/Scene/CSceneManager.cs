using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using SimpleFileBrowser;
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
	private Dictionary<string, ObjectPool> m_oObjPoolList = new Dictionary<string, ObjectPool>();
	#endregion			// 변수

	#region 클래스 변수
	private static FileBrowser.OnSuccess m_oDialogOKCallback = null;
	private static FileBrowser.OnCancel m_oDialogCancelCallback = null;

	private static Dictionary<string, STSequenceAniInfo> m_oSequenceAniInfoList = new Dictionary<string, STSequenceAniInfo>();

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	private static float m_fDebugSkipTime = 0.0f;

	private static System.Text.StringBuilder m_oStaticDebugStringBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oDynamicDebugStringBuilder = new System.Text.StringBuilder();

	private static System.Text.StringBuilder m_oExtraStaticDebugStringBuilder = new System.Text.StringBuilder();
	private static System.Text.StringBuilder m_oExtraDynamicDebugStringBuilder = new System.Text.StringBuilder();
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	#endregion			// 클래스 변수

	#region 클래스 컴포넌트
	private static Dictionary<string, CSceneManager> m_oSubSceneManagerList = new Dictionary<string, CSceneManager>();
	#endregion			// 클래스 컴포넌트

	#region 프로퍼티
	public abstract string SceneName { get; }

	// 카메라
	public Camera SubUICamera { get; private set; } = null;
	public Camera SubMainCamera { get; private set; } = null;

	// 캔버스 {
	public Canvas SubUICanvas { get; private set; } = null;
	public Canvas SubPopupUICanvas { get; private set; } = null;
	public Canvas SubTopmostUICanvas { get; private set; } = null;

	public Canvas SubObjCanvas { get; private set; } = null;
	// 캔버스 }

	// UI 루트
	public GameObject SubUITop { get; private set; } = null;
	public GameObject SubUIBase { get; private set; } = null;
	public GameObject SubUIs { get; private set; } = null;
	public GameObject SubAnchorUIs { get; private set; } = null;

	// 고정 UI 루트
	public GameObject SubLeftUIs { get; private set; } = null;
	public GameObject SubRightUIs { get; private set; } = null;
	public GameObject SubTopUIs { get; private set; } = null;
	public GameObject SubBottomUIs { get; private set; } = null;

	// 팝업 UI 루트
	public GameObject SubPopupUIs { get; private set; } = null;
	public GameObject SubTopmostUIs { get; private set; } = null;

	// 객체 루트
	public GameObject SubBase { get; private set; } = null;
	public GameObject SubObjBase { get; private set; } = null;
	public GameObject SubObjs { get; private set; } = null;
	public GameObject SubAnchorObjs { get; private set; } = null;

	// 고정 객체 루트
	public GameObject SubLeftObjs { get; private set; } = null;
	public GameObject SubRightObjs { get; private set; } = null;
	public GameObject SubTopObjs { get; private set; } = null;
	public GameObject SubBottomObjs { get; private set; } = null;

	// 객체 캔버스 루트
	public GameObject SubObjCanvasTop { get; private set; } = null;
	public GameObject SubObjCanvasBase { get; private set; } = null;
	public GameObject SubCanvasObjs { get; private set; } = null;
	
	public bool IsRootScene => CSceneManager.RootScene == this.gameObject.scene;

	public virtual float PlaneDistance => KCDefine.U_DEF_DISTANCE_CAMERA_PLANE;
	public virtual float FadeOutAniDuration => KCDefine.U_DEF_DURATION_SCREEN_FADE_OUT_ANI;

	public virtual float UICameraDepth => KCDefine.U_DEPTH_UI_CAMERA;
	public virtual float MainCameraDepth => KCDefine.U_DEPTH_MAIN_CAMERA;

	public virtual Color ClearColor => KCDefine.U_DEF_COLOR_CAMERA_BG;
	
	public virtual Color ScreenFadeInColor => KCDefine.U_DEF_COLOR_SCREEN_FADE_IN;
	public virtual Color ScreenFadeOutColor => KCDefine.U_DEF_COLOR_SCREEN_FADE_OUT;

	public virtual STSortingOrderInfo UICanvasSortingOrderInfo => KCDefine.U_DEF_SORTING_OI_UI_CANVAS;
	public virtual STSortingOrderInfo ObjCanvasSortingOrderInfo => KCDefine.U_DEF_SORTING_OI_OBJ_CANVAS;
	
#if MODE_PORTRAIT_ENABLE
	public virtual bool IsIgnoreVBlind => true;
	public virtual bool IsIgnoreHBlind => false;
#else
	public virtual bool IsIgnoreVBlind => false;
	public virtual bool IsIgnoreHBlind => true;
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
	public static bool IsAppQuit { get; private set; } = false;

	// 고정 UI 간격
	public static float LeftUIOffset { get; private set; } = 0.0f;
	public static float RightUIOffset { get; private set; } = 0.0f;
	public static float TopUIOffset { get; private set; } = 0.0f;
	public static float BottomUIOffset { get; private set; } = 0.0f;

	// 고정 객체 간격
	public static float LeftObjOffset { get; private set; } = 0.0f;
	public static float RightObjOffset { get; private set; } = 0.0f;
	public static float TopObjOffset { get; private set; } = 0.0f;
	public static float BottomObjOffset { get; private set; } = 0.0f;

	// 고정 UI 루트 간격
	public static float LeftUIsOffset { get; private set; } = 0.0f;
	public static float RightUIsOffset { get; private set; } = 0.0f;
	public static float TopUIsOffset { get; private set; } = 0.0f;
	public static float BottomUIsOffset { get; private set; } = 0.0f;

	// 고정 객체 루트 간격
	public static float LeftObjsOffset { get; private set; } = 0.0f;
	public static float RightObjsOffset { get; private set; } = 0.0f;
	public static float TopObjsOffset { get; private set; } = 0.0f;
	public static float BottomObjsOffset { get; private set; } = 0.0f;

	// 캔버스 크기
	public static Vector2 CanvasSize { get; protected set; } = Vector2.zero;
	public static Vector3 CanvasScale { get; protected set; } = Vector3.zero;

	// 카메라
	public static Camera UICamera { get; private set; } = null;
	public static Camera MainCamera { get; private set; } = null;

	// 캔버스 {
	public static Canvas UICanvas { get; private set; } = null;
	public static Canvas PopupUICanvas { get; private set; } = null;
	public static Canvas TopmostUICanvas { get; private set; } = null;

	public static Canvas ObjCanvas { get; private set; } = null;
	// 캔버스 }
	
	public static EventSystem EventSystem { get; private set; } = null;
	public static BaseInputModule BaseInputModule { get; private set; } = null;
	public static CSceneManager RootSceneManager { get; private set; } = null;

	// UI 루트
	public static GameObject UITop { get; private set; } = null;
	public static GameObject UIBase { get; private set; } = null;
	public static GameObject UIs { get; private set; } = null;
	public static GameObject AnchorUIs { get; private set; } = null;

	// 고정 UI 루트
	public static GameObject LeftUIs { get; private set; } = null;
	public static GameObject RightUIs { get; private set; } = null;
	public static GameObject TopUIs { get; private set; } = null;
	public static GameObject BottomUIs { get; private set; } = null;

	// 팝업 UI 루트
	public static GameObject PopupUIs { get; private set; } = null;
	public static GameObject TopmostUIs { get; private set; } = null;

	// 객체 루트
	public static GameObject Base { get; private set; } = null;
	public static GameObject ObjBase { get; private set; } = null;
	public static GameObject Objs { get; private set; } = null;
	public static GameObject AnchorObjs { get; private set; } = null;

	// 고정 객체 루트
	public static GameObject LeftObjs { get; private set; } = null;
	public static GameObject RightObjs { get; private set; } = null;
	public static GameObject TopObjs { get; private set; } = null;
	public static GameObject BottomObjs { get; private set; } = null;

	// 객체 캔버스 루트
	public static GameObject ObjCanvasTop { get; private set; } = null;
	public static GameObject ObjCanvasBase { get; private set; } = null;
	public static GameObject CanvasObjs { get; private set; } = null;

	// 화면 UI 루트
	public static GameObject ScreenBlindUIs { get; protected set; } = null;
	public static GameObject ScreenPopupUIs { get; protected set; } = null;
	public static GameObject ScreenTopmostUIs { get; protected set; } = null;
	public static GameObject ScreenAbsUIs { get; protected set; } = null;

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
			CFunc.ShowLog($"External Data Path: {KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}", KCDefine.B_LOG_COLOR_PLATFORM_INFO);
#endif			// #if DEBUG || DEVELOPMENT_BUILD

			CSceneManager.IsInit = false;
			CSceneManager.IsSetup = false;

			CSceneManager.IsAwake = true;
			CSceneManager.AwakeSceneName = CSceneManager.RootSceneName;
			
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Application.targetFrameRate = KCDefine.B_DEF_TARGET_FRAME_RATE;

			// 초기화 씬이 아닐 경우
			if(!CSceneManager.AwakeSceneName.ExIsEquals(KCDefine.B_SCENE_N_INIT)) {
				this.ExLateCallFunc(KCDefine.U_DELAY_INIT, (a_oSender, a_oParams) => {
					CScheduleManager.Inst.RemoveComponent(this);
					CNavStackManager.Inst.RemoveComponent(this);

					CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_INIT, false, false);
				});
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
			CFunc.SelectObjs(new GameObject[] {
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

#if !ROBO_TEST_ENABLE
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
				bIsEnableBack = oCanvasGroup.alpha.ExIsLessEquals(KCDefine.B_VALUE_FLT_0);
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)

				// 백 키 처리가 가능 할 경우
				if(bIsEnableBack) {
					// 경로 창이 출력 상태 일 경우
					if(CSceneManager.m_oSequenceAniInfoList.ContainsKey(KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER)) {
						CSceneManager.OnTouchDialogCancelBtn();

						var oFileBrowserObj = GameObject.Find(KCDefine.U_OBJ_N_FILE_BROWSER_UI);
						oFileBrowserObj?.SetActive(false);
					} else {
						CNavStackManager.Inst.SendNavStackEvent(ENavStackEvent.BACK_KEY_DOWN);
					}
				}
			}

#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
#if INPUT_SYSTEM_MODULE_ENABLE
			bool bIsTimeScaleKeyDown = Keyboard.current.leftShiftKey.isPressed && (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed);
#else
			bool bIsTimeScaleKeyDown = Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow));
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

			// 방향 키를 눌렀을 경우
			if(CSceneManager.IsAppInit && bIsTimeScaleKeyDown) {
#if INPUT_SYSTEM_MODULE_ENABLE
				float fVelocity = Keyboard.current.upArrowKey.isPressed ? KCDefine.B_VALUE_FLT_1 : -KCDefine.B_VALUE_FLT_1;
#else
				float fVelocity = Input.GetKey(KeyCode.UpArrow) ? KCDefine.B_VALUE_FLT_1 : -KCDefine.B_VALUE_FLT_1;
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				float fTime = KCDefine.B_VALUE_FLT_1 / (float)Application.targetFrameRate;
				Time.timeScale = Mathf.Clamp(Time.timeScale + (fVelocity * fTime), KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_1);
			}

			// 디버그 UI 루트가 존재 할 경우
			if(CSceneManager.ScreenDebugUIs != null) {
#if INPUT_SYSTEM_MODULE_ENABLE
				bool bIsDebugKeyDown = Keyboard.current.leftShiftKey.isPressed && Keyboard.current.digit1Key.wasPressedThisFrame;
				bool bIsDynamicDebugKeyDown = Keyboard.current.leftShiftKey.isPressed && Keyboard.current.digit2Key.wasPressedThisFrame;
#else
				bool bIsDebugKeyDown = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1);
				bool bIsDynamicDebugKeyDown = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2);
#endif			// #if INPUT_SYSTEM_MODULE_ENABLE

				// 디버그 키를 눌렀을 경우
				if(bIsDebugKeyDown) {
					CSceneManager.OnTouchDebugBtn();
				}
				// 동적 디버그 키를 눌렀을 경우
				else if(bIsDynamicDebugKeyDown) {
					CSceneManager.m_oExtraDynamicDebugStringBuilder.Clear();
				}
			}

			CSceneManager.m_fDebugSkipTime += CScheduleManager.Inst.UnscaleDeltaTime;

			// 디버그 정보 갱신 주기가 지났을 경우
			if(CSceneManager.m_fDebugSkipTime.ExIsGreateEquals(KCDefine.U_DELTA_T_DYNAMIC_DEBUG)) {
				CSceneManager.m_fDebugSkipTime = KCDefine.B_VALUE_FLT_0;

				// 정적 디버그 텍스트가 존재 할 경우
				if(CSceneManager.ScreenStaticDebugText != null) {
					string oStaticDebugString = CSceneManager.m_oStaticDebugStringBuilder.ToString();
					string oExtraStaticDebugString = CSceneManager.m_oExtraStaticDebugStringBuilder.ToString();

					CSceneManager.ScreenStaticDebugText.text = string.Format(KCDefine.U_FMT_STATIC_DEBUG_MSG, oStaticDebugString, oExtraStaticDebugString);
				}

				// 동적 디버그 텍스트가 존재 할 경우
				if(CSceneManager.ScreenDynamicDebugText != null) {
					double dblGCMemory = System.GC.GetTotalMemory(false).ExByteToMegaByte();
					double dblGPUMemory = Profiler.GetAllocatedMemoryForGraphicsDriver().ExByteToMegaByte();
					double dblUsedHeapMemory = Profiler.usedHeapSizeLong.ExByteToMegaByte();

					double dblMonoHeapMemory = Profiler.GetMonoHeapSizeLong().ExByteToMegaByte();
					double dblMonoUsedMemory = Profiler.GetMonoUsedSizeLong().ExByteToMegaByte();

					double dblTempAllocMemory = Profiler.GetTempAllocatorSize().ExByteToMegaByte();
					double dblTotalAllocMemory = Profiler.GetTotalAllocatedMemoryLong().ExByteToMegaByte();

					double dblTotalReservedMemory = Profiler.GetTotalReservedMemoryLong().ExByteToMegaByte();
					double dblTotalUnusedReservedMemory = Profiler.GetTotalUnusedReservedMemoryLong().ExByteToMegaByte();

					double dblGPUAllocMemory = Profiler.GetAllocatedMemoryForGraphicsDriver().ExByteToMegaByte();

					CSceneManager.m_oDynamicDebugStringBuilder.Clear();
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_A, dblGCMemory, dblUsedHeapMemory);
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_B, dblMonoHeapMemory, dblMonoUsedMemory);
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_C, dblTempAllocMemory, dblTotalAllocMemory);
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_D, dblTotalReservedMemory, dblTotalUnusedReservedMemory);
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_E, dblGPUAllocMemory);
					CSceneManager.m_oDynamicDebugStringBuilder.AppendFormat(KCDefine.U_FMT_DYNAMIC_DEBUG_INFO_F, Time.timeScale);

					string oDynamicDebugString = CSceneManager.m_oDynamicDebugStringBuilder.ToString();
					string oExtraDynamicDebugString = CSceneManager.m_oExtraDynamicDebugStringBuilder.ToString();

					CSceneManager.ScreenDynamicDebugText.text = string.Format(KCDefine.U_FMT_DYNAMIC_DEBUG_MSG, oDynamicDebugString, oExtraDynamicDebugString);
				}
			}
#endif			// #if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
		}
#endif			// #if !ROBO_TEST_ENABLE
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
				if(CSceneManager.m_oSequenceAniInfoList.ContainsKey(KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER)) {
					var stSequenceAniInfo = CSceneManager.m_oSequenceAniInfoList[KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER];
					stSequenceAniInfo.m_oSequence?.Kill();
				}
			}
			
			CSceneManager.m_oSubSceneManagerList.ExRemoveValue(this.SceneName);
		}
	}
	
	//! 앱이 종료 되었을 경우
	public virtual void OnApplicationQuit() {
		DOTween.KillAll(true);
		CSceneManager.IsAppQuit = true;
	}

	//! 객체 풀을 반환한다
	public ObjectPool GetObjPool(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());
		return m_oObjPoolList.ExGetValue(a_oKey, null);
	}

	//! 객체 풀을 추가한다
	public void AddObjPool(string a_oKey, ObjectPool a_oObjPool) {
		CAccess.Assert(a_oObjPool != null && a_oKey.ExIsValid());
		m_oObjPoolList.ExAddValue(a_oKey, a_oObjPool);
	}

	//! 객체 풀을 추가한다
	public void AddObjPool(string a_oKey, string a_oObjPath, GameObject a_oParent) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = CResManager.Inst.GetRes<GameObject>(a_oObjPath);

		this.AddObjPool(a_oKey, oObj, a_oParent);
	}

	//! 객체 풀을 추가한다
	public void AddObjPool(string a_oKey, GameObject a_oOrigin, GameObject a_oParent) {
		CAccess.Assert(a_oOrigin != null && a_oKey.ExIsValid());
		var oObjPool = CFactory.CreateObjPool(a_oOrigin, a_oParent);

		this.AddObjPool(a_oKey, oObjPool);
	}

	//! 객체 풀을 제거한다
	public void RemoveObjPool(string a_oKey, bool a_bIsDestroy = true) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 객체 풀이 존재 할 경우
		if(m_oObjPoolList.TryGetValue(a_oKey, out ObjectPool oObjPool)) {
			oObjPool.DeAllocate(oObjPool.CountInactive);
			oObjPool.DespawnAllActiveObjects(a_bIsDestroy);

			m_oObjPoolList.ExRemoveValue(a_oKey);
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
		return this.SpawnObj(a_oKey, a_oName, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos);
	}

	//! 객체를 활성화한다
	public GameObject SpawnObj(string a_oKey, string a_oName, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());

		var oObj = m_oObjPoolList[a_oKey].Spawn(a_oName);
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		return oObj;
	}

	//! 객체를 비활성화한다
	public void DespawnObj(string a_oKey, GameObject a_oObj, bool a_bIsDestroy = false) {
		CAccess.Assert(a_oKey.ExIsValid());

		// 객체 비활성화가 가능 할 경우
		if(m_oObjPoolList.ContainsKey(a_oKey)) {
			m_oObjPoolList[a_oKey].Despawn(a_oObj, a_bIsDestroy);
		}
	}

	//! 화면 페이드 인 애니메이션을 시작한다
	public virtual void StartScreenFadeInAni(System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.U_DEF_DURATION_SCREEN_FADE_OUT_ANI) {
		// 애니메이션 모드 일 경우
		if(!this.IsIgnoreAni) {
			string oName = KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER;
			CSceneManager.ShowTouchResponder(CSceneManager.ScreenAbsUIs, oName, this.ScreenFadeInColor, a_oCallback, true, a_fDuration, this.CreateScreenFadeInAni);
		} else {
			a_oCallback?.Invoke(null);
		}
	}

	//! 화면 페이드 아웃 애니메이션을 시작한다
	public virtual void StartScreenFadeOutAni(float a_fDuration = KCDefine.U_DEF_DURATION_SCREEN_FADE_OUT_ANI) {
		string oName = KCDefine.U_OBJ_N_SCREEN_F_TOUCH_RESPONDER;
		CSceneManager.CloseTouchResponder(oName, this.ScreenFadeOutColor, null, a_fDuration, this.CreateScreenFadeOutAni);
	}

	//! 화면 페이드 인 애니메이션을 생성한다
	protected virtual Sequence CreateScreenFadeInAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
#if DOTWEEN_ENABLE
		var oImg = a_oTarget.GetComponentInChildren<Image>();
		var oAni = oImg.DOColor(a_stColor, a_fDuration).SetEase(KCDefine.U_DEF_EASE_ANI);

		return CFactory.CreateSequence(oAni, null);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}

	//! 화면 페이드 아웃 애니메이션을 생성한다
	protected virtual Sequence CreateScreenFadeOutAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
#if DOTWEEN_ENABLE
		return this.CreateScreenFadeInAni(a_oTarget, a_oKey, a_stColor, a_fDuration);
#else
		return DOTween.Sequence();
#endif			// #if DOTWEEN_ENABLE
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 경로 창 확인 버튼을 눌렀을 경우
	private static void OnTouchDialogOKBtn(string[] a_oFilePaths) {
		CFunc.Invoke(ref CSceneManager.m_oDialogOKCallback, a_oFilePaths);
		CSceneManager.CloseTouchResponder(KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
	}

	//! 경로 창 취소 버튼을 눌렀을 경우
	private static void OnTouchDialogCancelBtn() {
		CFunc.Invoke(ref CSceneManager.m_oDialogCancelCallback);
		CSceneManager.CloseTouchResponder(KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT, null);
	}

	//! 터치 응답자를 반환한다
	public static GameObject GetTouchResponder(string a_oKey) {
		return CSceneManager.m_oSequenceAniInfoList.ContainsKey(a_oKey) ? CSceneManager.m_oSequenceAniInfoList[a_oKey].m_oObj : null;
	}
	
	//! 터치 응답자를 출력한다
	public static void ShowTouchResponder(GameObject a_oParent, string a_oKey, Color a_stColor, System.Action<GameObject> a_oCallback, bool a_bIsEnableNavStack = false, float a_fDuration = KCDefine.B_VALUE_FLT_0, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 없을 경우
		if(!CSceneManager.m_oSequenceAniInfoList.ContainsKey(a_oKey)) {
			string oName = string.Format(KCDefine.U_KEY_FMT_SCENE_M_TOUCH_RESPONDER, a_oKey);
			Sequence oSequence = null;

			var oTouchResponder = CFactory.CreateTouchResponder(oName, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER, a_oParent, CSceneManager.CanvasSize, KCDefine.B_POS_TOUCH_RESPONDER, KCDefine.U_COLOR_TRANSPARENT);
			
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
			oTouchDispatcher.DestroyCallback = (a_oSender) => CSceneManager.m_oSequenceAniInfoList.ExRemoveValue(a_oKey);

			// 내비게이션 스택이 유효 할 경우
			if(a_bIsEnableNavStack) {
				CNavStackManager.Inst.AddComponent(oTouchDispatcher);
			}
			// 터치 전달자를 설정한다 }

			CSceneManager.m_oSequenceAniInfoList.ExAddValue(a_oKey, new STSequenceAniInfo() {
				m_oObj = oTouchResponder,
				m_oSequence = oSequence
			});
		}
	}
	
	//! 터치 응답자를 닫는다
	public static void CloseTouchResponder(string a_oKey, Color a_stColor, System.Action<GameObject> a_oCallback, float a_fDuration = KCDefine.B_VALUE_FLT_0, System.Func<GameObject, string, Color, float, Sequence> a_oAniCreator = null) {
		// 터치 응답자가 존재 할 경우
		if(CSceneManager.m_oSequenceAniInfoList.TryGetValue(a_oKey, out STSequenceAniInfo stSequenceAniInfo)) {
			var oTouchDispatcher = stSequenceAniInfo.m_oObj.GetComponentInChildren<CTouchDispatcher>();
			CAccess.Assert(oTouchDispatcher != null);

			oTouchDispatcher.DestroyCallback = null;

			stSequenceAniInfo.m_oSequence?.Kill();
			CSceneManager.m_oSequenceAniInfoList.ExRemoveValue(a_oKey);

			// 내비게이션 스택 콜백이 존재 할 경우
			if(oTouchDispatcher.NavStackCallback != null) {
				CNavStackManager.Inst.RemoveComponent(oTouchDispatcher);
			}

			// 애니메이션 모드 일 경우
			if(a_oAniCreator != null) {
				var oAni = a_oAniCreator(stSequenceAniInfo.m_oObj, a_oKey, a_stColor, a_fDuration);
				CAccess.Assert(oAni != null);

				oAni.AppendCallback(() => {
					oAni.Kill();
					a_oCallback?.Invoke(stSequenceAniInfo.m_oObj);

					Destroy(stSequenceAniInfo.m_oObj);
				});
			} else {
				Destroy(stSequenceAniInfo.m_oObj);
			}
		}
	}

	//! 저장 경로 창을 출력한다
	public static void ShowSaveDialog(FileBrowser.OnSuccess a_oOKCallback, FileBrowser.OnCancel a_oCancelCallback, string a_oDefFilePath = KCDefine.B_EMPTY_STRING) {
		// 경로 창이 출력 상태 일 경우
		if(CSceneManager.m_oSequenceAniInfoList.ContainsKey(KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER)) {
			a_oCancelCallback?.Invoke();
		} else {
			CSceneManager.m_oDialogOKCallback = a_oOKCallback;
			CSceneManager.m_oDialogCancelCallback = a_oCancelCallback;

			CSceneManager.ShowTouchResponder(CSceneManager.ScreenTopmostUIs, KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER, KCDefine.U_DEF_COLOR_POPUP_BG, null);
			FileBrowser.ShowSaveDialog(CSceneManager.OnTouchDialogOKBtn, CSceneManager.OnTouchDialogCancelBtn, FileBrowser.PickMode.Files, false, a_oDefFilePath);
		}
	}

	//! 로드 경로 창을 출력한다
	public static void ShowLoadDialog(FileBrowser.OnSuccess a_oOKCallback, FileBrowser.OnCancel a_oCancelCallback, string a_oDefFilePath = KCDefine.B_EMPTY_STRING) {
		// 경로 창이 출력 상태 일 경우
		if(CSceneManager.m_oSequenceAniInfoList.ContainsKey(KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER)) {
			a_oCancelCallback?.Invoke();
		} else {
			CSceneManager.m_oDialogOKCallback = a_oOKCallback;
			CSceneManager.m_oDialogCancelCallback = a_oCancelCallback;

			CSceneManager.ShowTouchResponder(CSceneManager.ScreenTopmostUIs, KCDefine.U_KEY_SCENE_M_DIALOG_TOUCH_RESPONDER, KCDefine.U_DEF_COLOR_POPUP_BG, null);
			FileBrowser.ShowLoadDialog(CSceneManager.OnTouchDialogOKBtn, CSceneManager.OnTouchDialogCancelBtn, FileBrowser.PickMode.Files, false, a_oDefFilePath);
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
		return this.SpawnObj<T>(a_oKey, a_oName, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos);
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
		return CSceneManager.m_oSubSceneManagerList.ExGetValue(a_oKey, null) as T;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if LOGIC_TEST_ENABLE || (DEBUG || DEVELOPMENT_BUILD)
	//! 정적 문자열을 변경한다
	public static void SetStaticString(string a_oString) {
		CSceneManager.m_oExtraStaticDebugStringBuilder.Clear();
		CSceneManager.m_oExtraStaticDebugStringBuilder.Append(a_oString);
	}

	//! 동적 문자열을 변경한다
	public static void SetDynamicString(string a_oString) {
		CSceneManager.m_oExtraDynamicDebugStringBuilder.Clear();
		CSceneManager.m_oExtraDynamicDebugStringBuilder.Append(a_oString);
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
