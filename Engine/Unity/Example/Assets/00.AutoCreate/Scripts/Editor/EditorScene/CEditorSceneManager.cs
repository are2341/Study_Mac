using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor.SceneManagement;
using EnhancedHierarchy;

#if EDITOR_COROUTINE_ENABLE
using Unity.EditorCoroutines.Editor;
#endif			// #if EDITOR_COROUTINE_ENABLE

/** 에디터 씬 관리자 */
[InitializeOnLoad]
public static partial class CEditorSceneManager {
	#region 클래스 변수
	private static bool m_bIsEnableSetup = false;
	private static bool m_bIsEnableSetupDependencies = false;

	private static float m_fUpdateSkipTime = 0.0f;
	private static float m_fDependencySkipTime = 0.0f;
	private static float m_fDefineSymbolSkipTime = 0.0f;

	private static ListRequest m_oListRequest = null;
	private static List<string> m_oSampleSceneNameList = new List<string>();
	private static List<AddRequest> m_oAddRequestList = new List<AddRequest>();
	#endregion			// 클래스 변수
	
	#region 클래스 함수
	/** 생성자 */
	static CEditorSceneManager() {
		// 플레이 모드가 아닐 경우
		if(!EditorApplication.isPlaying) {
			CEditorSceneManager.m_oSampleSceneNameList.ExAddVal(KCDefine.B_SCENE_N_SAMPLE);
			CEditorSceneManager.m_oSampleSceneNameList.ExAddVal(KCDefine.B_SCENE_N_MENU_SAMPLE);
			CEditorSceneManager.m_oSampleSceneNameList.ExAddVal(KCDefine.B_SCENE_N_STUDY_SAMPLE);
			CEditorSceneManager.m_oSampleSceneNameList.ExAddVal(KCDefine.B_SCENE_N_EDITOR_SAMPLE);

			CEditorSceneManager.SetupCallbacks();
		}
	}

	/** 스크립트가 로드 되었을 경우 */
	[UnityEditor.Callbacks.DidReloadScripts]
	public static void OnLoadScript() {
#if EDITOR_COROUTINE_ENABLE
		EditorCoroutineUtility.StartCoroutineOwnerless(CEditorSceneManager.SetupEditorSceneManager());
#else
		CEditorSceneManager.m_bIsEnableSetup = true;
#endif			// #if EDITOR_COROUTINE_ENABLE
	}
	
	/** 상태를 갱신한다 */
	private static void Update() {
		// 상태 갱신이 가능 할 경우
		if(CEditorAccess.IsEnableUpdateState) {
			CEditorSceneManager.m_fUpdateSkipTime += Mathf.Clamp01(Time.deltaTime);

			// 상태 갱신이 가능 할 경우
			if(CEditorSceneManager.m_bIsEnableSetup) {
				Preferences.Tooltips.Value = false;
				Preferences.SelectOnTree.Value = false;

				CEditorSceneManager.m_bIsEnableSetup = false;
				CEditorSceneManager.m_bIsEnableSetupDependencies = true;
				CEditorSceneManager.m_oListRequest = Client.List(true, true);

				CEditorSceneManager.SetupCallbacks();

				EditorApplication.update -= CEditorSceneManager.UpdateDependencyState;
				EditorApplication.update += CEditorSceneManager.UpdateDependencyState;
			}

			// 갱신 주기가 지났을 경우
			if(CEditorSceneManager.m_fUpdateSkipTime.ExIsGreateEquals(KCEditorDefine.B_DELTA_T_SCENE_M_SCRIPT_UPDATE)) {
				CEditorSceneManager.m_fUpdateSkipTime = KCDefine.B_VAL_0_REAL;
				CEditorSceneManager.SetupExtraPreloadAssets();

				CFunc.EnumerateRootObjs((a_oObj) => {
					// 프리팹 최상단 객체 일 경우
					if(KCEditorDefine.B_OBJ_N_PREFAB_ROOT_OBJ_LIST.Contains(a_oObj.name) && !CEditorSceneManager.m_oSampleSceneNameList.Contains(a_oObj.scene.name)) {
						CEditorSceneManager.SetupPrefabObjs(a_oObj);
					}

					return true;
				});

				CFunc.EnumerateScenes((a_stScene) => { CSampleSceneManager.SetupSceneManager(a_stScene, KEditorDefine.B_SCENE_MANAGER_TYPE_DICT); return true; });

#if EXTRA_SCRIPT_MODULE_ENABLE
				CFunc.EnumerateScenes((a_stScene) => { CSampleSceneManager.SetupSceneManager(a_stScene, KEditorDefine.G_EXTRA_SCENE_MANAGER_TYPE_DICT); return true; });
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE

				var oMonoScripts = MonoImporter.GetAllRuntimeMonoScripts();

				for(int i = 0; i < oMonoScripts.Length; ++i) {
					// 스크립트가 존재 할 경우
					if(oMonoScripts[i] != null) {
						var oType = oMonoScripts[i].GetClass();
						
						// 스크립트 순서 설정이 가능 할 경우
						if(oType != null && KEditorDefine.B_SCRIPT_ORDER_DICT.TryGetValue(oType, out int nOrder)) {
							CAccess.SetScriptOrder(oMonoScripts[i], nOrder);
						}

#if EXTRA_SCRIPT_MODULE_ENABLE
						// 스크립트 순서 설정이 가능 할 경우
						if(oType != null && KEditorDefine.G_EXTRA_SCRIPT_ORDER_DICT.TryGetValue(oType, out int nExtraOrder)) {
							CAccess.SetScriptOrder(oMonoScripts[i], nExtraOrder);
						}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE
					}
				}
			}
		}
	}

	/** 상태를 갱신한다 */
	private static void LateUpdate() {
		bool bIsEnableUpdate = CEditorAccess.IsEnableUpdateState && !CEditorSceneManager.m_oAddRequestList.ExIsValid();
		CEditorSceneManager.m_fDefineSymbolSkipTime = bIsEnableUpdate ? CEditorSceneManager.m_fDefineSymbolSkipTime + Time.deltaTime : KCDefine.B_VAL_0_REAL;

		for(int i = 0; i < CEditorSceneManager.m_oAddRequestList.Count; ++i) {
			// 에러가 존재 할 경우
			if(CEditorSceneManager.m_oAddRequestList[i].Error != null) {
				CFunc.ShowLog($"CEditorSceneManager.LateUpdate: {CEditorSceneManager.m_oAddRequestList[i].Error.message}");
				CEditorSceneManager.m_oAddRequestList.ExRemoveValAt(i, false);
				
				break;
			}
		}

		// 상태 갱신이 가능 할 경우
		if(bIsEnableUpdate && CEditorSceneManager.m_fDefineSymbolSkipTime.ExIsGreateEquals(KCDefine.B_VAL_1_REAL)) {
			var oDefineSymbolInfoTable = CEditorFunc.FindAsset<CDefineSymbolInfoTable>(KCEditorDefine.B_ASSET_P_DEFINE_SYMBOL_INFO_TABLE);

			// 전처리기 심볼 정보 테이블이 존재 할 경우
			if(oDefineSymbolInfoTable != null) {
				CEditorSceneManager.m_fDefineSymbolSkipTime = KCDefine.B_VAL_0_REAL;

				foreach(var stKeyVal in KCEditorDefine.DS_DEFINE_S_REPLACE_MODULE_DICT) {
					var oDefineSymbolLists = new List<List<string>>() {
						oDefineSymbolInfoTable.EditorCommonDefineSymbolList,
						oDefineSymbolInfoTable.EditorSubCommonDefineSymbolList,

						oDefineSymbolInfoTable.EditoriOSAppleDefineSymbolList,

						oDefineSymbolInfoTable.EditorAndroidDefineSymbolList,
						oDefineSymbolInfoTable.EditorAndroidGoogleDefineSymbolList,
						oDefineSymbolInfoTable.EditorAndroidAmazonDefineSymbolList,
						
						oDefineSymbolInfoTable.EditorStandaloneDefineSymbolList,
						oDefineSymbolInfoTable.EditorStandaloneMacSteamDefineSymbolList,
						oDefineSymbolInfoTable.EditorStandaloneWndsSteamDefineSymbolList
					};

					for(int i = 0; i < oDefineSymbolLists.Count; ++i) {
						// 전처리기 심볼 갱신이 필요 할 경우
						if(oDefineSymbolLists[i].Contains(stKeyVal.Key)) {
							EditorUtility.SetDirty(oDefineSymbolInfoTable);
							oDefineSymbolLists[i].ExReplaceVal(stKeyVal.Key, stKeyVal.Value);
						}
					}
				}

				// 전처리기 심볼 갱신이 필요 할 경우
				if(EditorUtility.IsDirty(oDefineSymbolInfoTable)) {
					CEditorFunc.UpdateAssetDBState();
				}
			}
		}
	}

	/** 독립 패키지 상태를 갱신한다 */
	private static void UpdateDependencyState() {
		// 상태 갱신이 가능 할 경우
		if(CEditorAccess.IsEnableUpdateState) {
			bool bIsEnableSetup = CEditorSceneManager.m_bIsEnableSetupDependencies && (CEditorSceneManager.m_oListRequest != null && CEditorSceneManager.m_oListRequest.Result != null && CEditorSceneManager.m_oListRequest.IsCompleted);
			CEditorSceneManager.m_fDependencySkipTime += Mathf.Clamp01(Time.deltaTime);

			// 갱신 주기가 지났을 경우
			if(bIsEnableSetup && CEditorSceneManager.m_fDependencySkipTime.ExIsGreateEquals(KCEditorDefine.B_DELTA_T_SCENE_M_SCRIPT_UPDATE)) {
				CEditorSceneManager.m_fDependencySkipTime = KCDefine.B_VAL_0_REAL;
				CEditorSceneManager.m_bIsEnableSetupDependencies = false;

				try {
					CEditorSceneManager.SetupDependencies();
				} finally {
					EditorApplication.update -= CEditorSceneManager.UpdateDependencyState;
					CEditorSceneManager.m_oListRequest = null;
				}
			}			
		}
	}

	/** 에디터 씬 관리자를 설정한다 */
	private static IEnumerator SetupEditorSceneManager() {
		do {
			yield return null;
		} while(!CEditorAccess.IsEnableUpdateState);

		yield return CFactory.CreateWaitForSecs(KCDefine.B_VAL_1_REAL);
		CEditorSceneManager.m_bIsEnableSetup = true;
	}

	/** 콜백을 설정한다 */
	private static void SetupCallbacks() {
		EditorApplication.update -= CEditorSceneManager.Update;
		EditorApplication.update += CEditorSceneManager.Update;

		EditorApplication.update -= CEditorSceneManager.LateUpdate;
		EditorApplication.update += CEditorSceneManager.LateUpdate;
	}

	/** 종속 패키지를 설정한다 */
	private static void SetupDependencies() {
		var oPkgsInfoList = CEditorSceneManager.m_oListRequest.Result.ToList();

		foreach(var stKeyVal in KEditorDefine.B_UNITY_PKGS_DEPENDENCY_DICT) {
			int nIdx = oPkgsInfoList.FindIndex((a_oPkgsInfo) => a_oPkgsInfo.name.Equals(stKeyVal.Key));

			// 독립 패키지가 없을 경우
			if(!oPkgsInfoList.ExIsValidIdx(nIdx)) {
				// 버전이 유효 할 경우
				if(stKeyVal.Value.ExIsValidBuildVer()) {
					string oID = string.Format(KEditorDefine.B_UNITY_PKGS_ID_FMT, stKeyVal.Key, stKeyVal.Value);
					CEditorSceneManager.m_oAddRequestList.ExAddVal(Client.Add(oID));
				} else {
#if !SAMPLE_PROJ
					CEditorSceneManager.m_oAddRequestList.ExAddVal(Client.Add(stKeyVal.Value));
#endif			// #if !SAMPLE_PROJ
				}
			}
		}
	}

	/** 미리 로드 할 추가 에셋을 설정한다 */
	private static void SetupExtraPreloadAssets() {
#if EXTRA_SCRIPT_MODULE_ENABLE
		var oPreloadAssetList = PlayerSettings.GetPreloadedAssets().ToList();
		
		for(int i = 0; i < KEditorDefine.G_EXTRA_DIR_P_PRELOAD_ASSET_LIST.Count; ++i) {
			// 디렉토리가 존재 할 경우
			if(AssetDatabase.IsValidFolder(KEditorDefine.G_EXTRA_DIR_P_PRELOAD_ASSET_LIST[i])) {
				var oAssetList = CEditorFunc.FindAssets<Object>(string.Empty, new List<string>() { KEditorDefine.G_EXTRA_DIR_P_PRELOAD_ASSET_LIST[i] });

				for(int j = 0; j < oAssetList.Count; ++j) {
					// 디렉토리 에셋이 아닐 경우
					if(oAssetList[j].GetType() != typeof(DefaultAsset)) {
						oPreloadAssetList.ExAddVal(oAssetList[j], (a_oAsset) => (a_oAsset != null && oAssetList[j] != null) && oAssetList[j].name.Equals(a_oAsset.name));
					}
				}
			}
		}

		for(int i = 0; i < KEditorDefine.G_EXTRA_ASSET_P_PRELOAD_ASSET_LIST.Count; ++i) {
			var oAsset = CEditorFunc.FindAsset<Object>(KEditorDefine.G_EXTRA_ASSET_P_PRELOAD_ASSET_LIST[i]);
			oPreloadAssetList.ExAddVal(oAsset, (a_oAsset) => (a_oAsset != null && oAsset != null) && oAsset.name.Equals(a_oAsset.name));
		}

		PlayerSettings.SetPreloadedAssets(oPreloadAssetList.ToArray());
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE
	}

	/** 프리팹 객체를 설정한다 */
	private static void SetupPrefabObjs(GameObject a_oObj) {
		// 프리팹 설정이 가능 할 경우
		if(!PrefabUtility.IsPrefabAssetMissing(a_oObj) && CFunc.FindComponent<CSampleSceneManager>(a_oObj.scene) == null) {
			string oDirPath = (!a_oObj.scene.name.Contains(KCDefine.B_EDITOR_SCENE_N_PATTERN_01) && !a_oObj.scene.name.Contains(KCDefine.B_EDITOR_SCENE_N_PATTERN_02)) ? KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_PREFABS : KCEditorDefine.B_DIR_P_SUB_UNITY_PROJ_EDITOR_PREFABS;
			string oPrefabPath = string.Format(KCDefine.B_TEXT_FMT_3_SLASH_COMBINE, Path.GetDirectoryName(oDirPath).Replace(KCDefine.B_TOKEN_REV_SLASH, KCDefine.B_TOKEN_SLASH), CEditorAccess.GetSceneDirName(a_oObj.scene), string.Format(KCDefine.B_TEXT_FMT_2_UNDER_SCORE_COMBINE, a_oObj.scene.name, a_oObj.name));
			string oFinalPrefabPath = string.Format(KCDefine.B_TEXT_FMT_2_COMBINE, oPrefabPath, KCDefine.B_FILE_EXTENSION_PREFAB);

			// 프리팹이 없을 경우
			if(!CEditorAccess.IsExistsAsset(oFinalPrefabPath)) {
				EditorSceneManager.MarkSceneDirty(a_oObj.scene);
				CEditorFactory.MakeDirectories(Path.GetDirectoryName(oPrefabPath).Replace(KCDefine.B_TOKEN_REV_SLASH, KCDefine.B_TOKEN_SLASH));

				PrefabUtility.SaveAsPrefabAssetAndConnect(a_oObj, oFinalPrefabPath, InteractionMode.AutomatedAction);
			}
			
			// 베리언트 프리팹 일 경우
			if(PrefabUtility.GetPrefabAssetType(a_oObj) == PrefabAssetType.Variant) {
				do {
					PrefabUtility.UnpackPrefabInstance(a_oObj, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
				} while(PrefabUtility.GetPrefabAssetType(a_oObj) != PrefabAssetType.NotAPrefab);

				CEditorFactory.RemoveAsset(oFinalPrefabPath);
			}
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR