using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 플랫폼 빌더
public static partial class CPlatformBuilder {
	#region 클래스 프로퍼티
	public static bool IsAutoPlay { get; private set; } = false;
	public static bool IsEnableEditorScene { get; private set; } = false;

	public static EBuildType BuildType { get; private set; } = EBuildType.NONE;

	public static EStandaloneType StandaloneType { get; private set; } = EStandaloneType.NONE;
	public static EAndroidType AndroidType { get; private set; } = EAndroidType.NONE;
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	//! 플랫폼을 빌드한다
	private static void BuildPlatform(BuildPlayerOptions a_oPlayerOpts) {
		var oDefineSymbolList = new List<string>();

		try {
			// 전처리기 심볼을 설정한다 {
			// 배포 빌드 모드 일 경우
			if(CPlatformBuilder.BuildType == EBuildType.STORE) {
				var oRemoveDefineSymbolList = new List<string>() {
					KCEditorDefine.DS_DEFINE_S_LOCALIZE_TEST_ENABLE,
					KCEditorDefine.DS_DEFINE_S_ANALYTICS_TEST_ENABLE
				};

				for(int i = 0; i < oRemoveDefineSymbolList.Count; ++i) {
					bool bIsContains = CPlatformOptsSetter.IsContainsDefineSymbol(a_oPlayerOpts.targetGroup, oRemoveDefineSymbolList[i]);

					// 전처리기 심볼이 포함 되었을 경우
					if(bIsContains) {
						CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, oRemoveDefineSymbolList[i]);
						oDefineSymbolList.ExAddVal(oRemoveDefineSymbolList[i]);
					}
				}
			}
			
			// 모바일 일 경우
			if(a_oPlayerOpts.targetGroup == BuildTargetGroup.iOS || a_oPlayerOpts.targetGroup == BuildTargetGroup.Android) {
				CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_RECEIPT_CHECK_ENABLE);
			}
			
			CEditorFunc.SetupDefineSymbols(CPlatformOptsSetter.DefineSymbolDictContainer);
			// 전처리기 심볼을 설정한다 }

			// 전처리기 심볼을 저장한다 {
			string oFilePath = string.Format(KCEditorDefine.B_ASSET_P_FMT_DEFINE_S_OUTPUT, a_oPlayerOpts.targetGroup);
			string oDefineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(a_oPlayerOpts.targetGroup);

			CFunc.WriteStr(oFilePath, oDefineSymbols);
			// 전처리기 심볼을 저장한다 }

			// 플랫폼을 설정한다 {
			CPlatformOptsSetter.SetupEditorOpts();
			CPlatformOptsSetter.SetupPlayerOpts();

			CEditorFunc.ChangePlatform(a_oPlayerOpts.targetGroup, a_oPlayerOpts.target);
			// 플랫폼을 설정한다 }

			// 빌드 옵션을 설정한다 {
			// 자동 실행 모드 일 경우
			if(CPlatformBuilder.IsAutoPlay) {
				a_oPlayerOpts.options |= BuildOptions.AutoRunPlayer;

#if PROFILER_ENABLE
				a_oPlayerOpts.options |= BuildOptions.Development | BuildOptions.ConnectWithProfiler;
#endif			// #if PROFILER_ENABLE
			}
			
			a_oPlayerOpts.options &= ~(BuildOptions.SymlinkLibraries | BuildOptions.CompressWithLz4 | BuildOptions.CompressWithLz4HC);
			
#if LZ4_COMPRESS_ENABLE
#if DEBUG || DEVELOPMENT_BUILD
			a_oPlayerOpts.options |= BuildOptions.CompressWithLz4;
#else
			a_oPlayerOpts.options |= BuildOptions.CompressWithLz4HC;
#endif			// #if DEBUG || DEVELOPMENT_BUILD			
#endif			// #if LZ4_COMPRESS_ENABLE
			// 빌드 옵션을 설정한다 }

			// 씬 경로를 설정한다 {
			var oScenePathList = new List<string>();

			for(int i = 0; i < EditorBuildSettings.scenes.Length; ++i) {
				string oScenePath = EditorBuildSettings.scenes[i].path;

				bool bIsContainsA = oScenePath.Contains(KCDefine.B_EDITOR_SCENE_N_PATTERN_A);
				bool bIsContainsB = oScenePath.Contains(KCDefine.B_EDITOR_SCENE_N_PATTERN_B);

				// 씬 추가가 가능 할 경우
				if(CPlatformBuilder.IsEnableEditorScene || (!bIsContainsA && !bIsContainsB)) {
					oScenePathList.ExAddVal(oScenePath);
				}
			}

			a_oPlayerOpts.scenes = oScenePathList.ToArray();
			// 씬 경로를 설정한다 }

			// 배치 모드가 아닐 경우
			if(!Application.isBatchMode) {
				CEditorFunc.UpdateAssetDBState();
			}

			// 빌드 가능 할 경우
			if(!BuildPipeline.isBuildingPlayer) {
				CPlatformOptsSetter.SetupGraphicAPIs();
				BuildPipeline.BuildPlayer(a_oPlayerOpts);
			}
		} finally {
			CPlatformBuilder.IsAutoPlay = false;
			CPlatformBuilder.IsEnableEditorScene = false;

			CPlatformBuilder.BuildType = EBuildType.NONE;

			// 전처리기 심볼을 리셋한다 {
			for(int i = 0; i < oDefineSymbolList.Count; ++i) {
				CPlatformOptsSetter.AddDefineSymbol(a_oPlayerOpts.targetGroup, oDefineSymbolList[i]);
			}

			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_FPS_ENABLE);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_ADS_TEST_ENABLE);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_ROBO_TEST_ENABLE);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_LOGIC_TEST_ENABLE);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_RECEIPT_CHECK_ENABLE);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_ADHOC_BUILD);
			CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_STORE_BUILD);

			// 독립 플랫폼 일 경우
			if(a_oPlayerOpts.targetGroup == BuildTargetGroup.Standalone) {
				CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_MAC);
				CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_WNDS);
			}
			// 안드로이드 일 경우
			else if(a_oPlayerOpts.targetGroup == BuildTargetGroup.Android) {
				CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_GOOGLE);
				CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_ONE_STORE);
				CPlatformOptsSetter.RemoveDefineSymbol(a_oPlayerOpts.targetGroup, KCEditorDefine.DS_DEFINE_S_GALAXY_STORE);
			}

			CEditorFunc.SetupDefineSymbols(CPlatformOptsSetter.DefineSymbolDictContainer);
			// 전처리기 심볼을 리셋한다 }
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
