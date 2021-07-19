using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 옵션 설정자
[InitializeOnLoad]
public static partial class CPlatformOptsSetter {
	#region 클래스 변수
	private static CBuildInfoTable m_oBuildInfoTable = null;
	private static CBuildOptsTable m_oBuildOptsTable = null;
	private static CProjInfoTable m_oProjInfoTable = null;
	private static CDefineSymbolTable m_oDefineSymbolTable = null;
	#endregion			// 클래스 변수

	#region 클래스 프로퍼티
	public static Dictionary<BuildTargetGroup, List<string>> DefineSymbolListContainer { get; private set; } = new Dictionary<BuildTargetGroup, List<string>>();

	public static CBuildInfoTable BuildInfoTable {
		get {
			// 빌드 정보 테이블이 없을 경우
			if(CPlatformOptsSetter.m_oBuildInfoTable == null) {
				CPlatformOptsSetter.m_oBuildInfoTable = AssetDatabase.LoadAssetAtPath<CBuildInfoTable>(KCEditorDefine.B_ASSET_P_BUILD_INFO_TABLE);
				CPlatformOptsSetter.m_oBuildInfoTable?.Awake();
			}

			return CPlatformOptsSetter.m_oBuildInfoTable;
		} set {
			CPlatformOptsSetter.m_oBuildInfoTable = value;	
		}
	}

	public static CBuildOptsTable BuildOptsTable {
		get {
			// 빌드 옵션 테이블이 없을 경우
			if(CPlatformOptsSetter.m_oBuildOptsTable == null) {
				CPlatformOptsSetter.m_oBuildOptsTable = AssetDatabase.LoadAssetAtPath<CBuildOptsTable>(KCEditorDefine.B_ASSET_P_BUILD_OPTS_TABLE);
				CPlatformOptsSetter.m_oBuildOptsTable?.Awake();
			}

			return CPlatformOptsSetter.m_oBuildOptsTable;
		} set {
			CPlatformOptsSetter.m_oBuildOptsTable = value;
		}
	}

	public static CProjInfoTable ProjInfoTable {
		get {
			// 프로젝트 정보 테이블이 없을 경우
			if(CPlatformOptsSetter.m_oProjInfoTable == null) {
				CPlatformOptsSetter.m_oProjInfoTable = AssetDatabase.LoadAssetAtPath<CProjInfoTable>(KCEditorDefine.B_ASSET_P_PROJ_INFO_TABLE);
				CPlatformOptsSetter.m_oProjInfoTable?.Awake();
			}

			return CPlatformOptsSetter.m_oProjInfoTable;
		} set {
			CPlatformOptsSetter.m_oProjInfoTable = value;
		}
	}

	public static CDefineSymbolTable DefineSymbolTable {
		get {
			// 전처리기 테이블이 없을 경우
			if(CPlatformOptsSetter.m_oDefineSymbolTable == null) {
				CPlatformOptsSetter.m_oDefineSymbolTable = AssetDatabase.LoadAssetAtPath<CDefineSymbolTable>(KCEditorDefine.B_ASSET_P_DEFINE_SYMBOL_TABLE);
				CPlatformOptsSetter.m_oDefineSymbolTable?.Awake();
			}

			return CPlatformOptsSetter.m_oDefineSymbolTable;
		} set {
			CPlatformOptsSetter.m_oDefineSymbolTable = value;
		}
	}
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	//! 생성자
	static CPlatformOptsSetter() {
		CPlatformOptsSetter.EditorInitialize();
	}

	//! 초기화
	public static void EditorInitialize() {
		// 전처리기 심볼 테이블이 존재 할 경우
		if(CPlatformOptsSetter.DefineSymbolTable != null) {
			CPlatformOptsSetter.DefineSymbolListContainer = CPlatformOptsSetter.DefineSymbolListContainer ?? new Dictionary<BuildTargetGroup, List<string>>();
			CPlatformOptsSetter.DefineSymbolListContainer.Clear();

			CPlatformOptsSetter.DefineSymbolListContainer.Add(BuildTargetGroup.Standalone, CPlatformOptsSetter.DefineSymbolTable.StandaloneDefineSymbolList);
			CPlatformOptsSetter.DefineSymbolListContainer.Add(BuildTargetGroup.iOS, CPlatformOptsSetter.DefineSymbolTable.iOSDefineSymbolList);
			CPlatformOptsSetter.DefineSymbolListContainer.Add(BuildTargetGroup.Android, CPlatformOptsSetter.DefineSymbolTable.AndroidDefineSymbolList);

			// 배치 모드 일 경우
			if(Application.isBatchMode) {
				// 윈도우즈 일 경우
				if(CPlatformBuilder.StandaloneType == EStandaloneType.WNDS) {
					CPlatformOptsSetter.DefineSymbolListContainer[BuildTargetGroup.Standalone].ExAddVals(CPlatformOptsSetter.DefineSymbolTable.EditorWndsDefineSymbolList);
				} else {
					CPlatformOptsSetter.DefineSymbolListContainer[BuildTargetGroup.Standalone].ExAddVals(CPlatformOptsSetter.DefineSymbolTable.EditorMacDefineSymbolList);
				}

				// 원 스토어 일 경우
				if(CPlatformBuilder.AndroidType == EAndroidType.ONE_STORE) {
					CPlatformOptsSetter.DefineSymbolListContainer[BuildTargetGroup.Android].ExAddVals(CPlatformOptsSetter.DefineSymbolTable.EditorOneStoreDefineSymbolList);
				}
				// 갤럭시 스토어 일 경우
				else if(CPlatformBuilder.AndroidType == EAndroidType.GALAXY_STORE) {
					CPlatformOptsSetter.DefineSymbolListContainer[BuildTargetGroup.Android].ExAddVals(CPlatformOptsSetter.DefineSymbolTable.EditorGalaxyStoreDefineSymbolList);
				} else {
					CPlatformOptsSetter.DefineSymbolListContainer[BuildTargetGroup.Android].ExAddVals(CPlatformOptsSetter.DefineSymbolTable.EditorGoogleDefineSymbolList);
				}
			}

			foreach(var stKeyVal in CPlatformOptsSetter.DefineSymbolListContainer) {
				bool bIsContainsStudy = stKeyVal.Value.Contains(KCEditorDefine.DS_DEFINE_S_STUDY_ENABLE);
				bool bIsContainsPurchase = stKeyVal.Value.Contains(KCEditorDefine.DS_DEFINE_S_PURCHASE_ENABLE);

				// iOS 일 경우
				if(stKeyVal.Key == BuildTargetGroup.iOS) {
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_NO_GPGS);
				}

				// 입력 시스템 심볼이 존재 할 경우
				if(stKeyVal.Value.Contains(KCEditorDefine.DS_DEFINE_S_INPUT_SYSTEM_ENABLE)) {
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_ENABLE_INPUT_SYSTEM);
				}

				// 스터디 전처리기 심볼이 존재 할 경우
				if(bIsContainsStudy || stKeyVal.Value.Contains(KCEditorDefine.DS_DEFINE_S_STUDY_MODULE_ENABLE)) {
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_LIGHT_ENABLE);
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_SHADOW_ENABLE);
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_SOFT_SHADOW_ENABLE);
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_LIGHTMAP_SHADOW_BAKE_MASK_MODE_ENABLE);
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_ULTRA_QUALITY_LEVEL_ENABLE);
				}
				
				// 결제 전처리기 심볼이 존재 할 경우
				if(bIsContainsPurchase || stKeyVal.Value.Contains(KCEditorDefine.DS_DEFINE_S_PURCHASE_MODULE_ENABLE)) {
					stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_UNITY_PURCHASING);
				}
				
				stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_IL2CPP_ENABLE);
				stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_USE_AUTO_CREATE);
				stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_ODIN_INSPECTOR);
				stKeyVal.Value.ExAddVal(KCEditorDefine.DS_DEFINE_S_ODIN_INSPECTOR_3);
			}
		}
	}

	//! 전처리기 심볼 포함 여부를 검사한다
	public static bool IsContainsDefineSymbol(BuildTargetGroup a_eTargetGroup, string a_oDefineSymbol) {
		bool bIsContains = CPlatformOptsSetter.DefineSymbolListContainer.ContainsKey(a_eTargetGroup);
		return bIsContains && CPlatformOptsSetter.DefineSymbolListContainer[a_eTargetGroup].Contains(a_oDefineSymbol);
	}

	//! 전처리기 심볼을 추가한다
	public static void AddDefineSymbol(BuildTargetGroup a_eTargetGroup, string a_oDefineSymbol) {
		// 전처리기 심볼이 유효하지 않을 경우
		if(!CPlatformOptsSetter.DefineSymbolListContainer.ExIsValid()) {
			CPlatformOptsSetter.EditorInitialize();
		}

		// 전처리기 심볼 추가가 가능 할 경우
		if(CPlatformOptsSetter.DefineSymbolListContainer.ContainsKey(a_eTargetGroup)) {
			CPlatformOptsSetter.DefineSymbolListContainer[a_eTargetGroup].ExAddVal(a_oDefineSymbol);
		}
	}

	//! 전처리기 심볼을 제거한다
	public static void RemoveDefineSymbol(BuildTargetGroup a_eTargetGroup, string a_oDefineSymbol) {
		// 전처리기 심볼 제거가 가능 할 경우
		if(CPlatformOptsSetter.DefineSymbolListContainer.ContainsKey(a_eTargetGroup)) {
			CPlatformOptsSetter.DefineSymbolListContainer[a_eTargetGroup].ExRemoveVal(a_oDefineSymbol);
		}
	}

	//! 맥으로 변경한다
	[MenuItem("Tools/Utility/Change Platform/Mac")]
	public static void ChangeMac() {
		CEditorFunc.ChangePlatform(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
	}

	//! 윈도우즈로 변경한다
	[MenuItem("Tools/Utility/Change Platform/Windows")]
	public static void ChangeWnds() {
		CEditorFunc.ChangePlatform(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
	}

	//! iOS 로 변경한다
	[MenuItem("Tools/Utility/Change Platform/iOS")]
	public static void ChangeiOS() {
		CEditorFunc.ChangePlatform(BuildTargetGroup.iOS, BuildTarget.iOS);
	}

	//! 안드로이드 변경한다
	[MenuItem("Tools/Utility/Change Platform/Android")]
	public static void ChangeAndroid() {
		CEditorFunc.ChangePlatform(BuildTargetGroup.Android, BuildTarget.Android);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
