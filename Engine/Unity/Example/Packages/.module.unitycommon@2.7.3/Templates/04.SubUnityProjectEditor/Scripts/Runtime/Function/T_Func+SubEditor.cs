#if SCRIPT_TEMPLATE_ONLY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
/** 서브 에디터 함수 */
public static partial class Func {
	#region 클래스 함수
	
	#endregion			// 클래스 함수
}

/** 서브 레벨 에디터 씬 함수 */
public static partial class Func {
	#region 클래스 함수
	
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
	/** 에디터 셀 정보 설정 완료 여부를 검사한다 */
	private static bool IsSetupEditorCellInfos(CLevelInfo a_oLevelInfo, CEditorLevelCreateInfo a_oCreateInfo) {
		return true;
	}

	/** 에디터 레벨 정보를 설정한다 */
	public static void SetupEditorLevelInfo(CLevelInfo a_oLevelInfo, CEditorLevelCreateInfo a_oCreateInfo) {
		int nNumCellsX = Random.Range(a_oCreateInfo.m_stMinNumCells.x, a_oCreateInfo.m_stMaxNumCells.x + KCDefine.B_VAL_1_INT);
		int nNumCellsY = Random.Range(a_oCreateInfo.m_stMinNumCells.y, a_oCreateInfo.m_stMaxNumCells.y + KCDefine.B_VAL_1_INT);

		a_oLevelInfo.m_oCellInfoDictContainer.Clear();

		for(int i = 0; i < Mathf.Clamp(nNumCellsY, SampleEngineName.KDefine.E_MIN_NUM_CELLS.y, SampleEngineName.KDefine.E_MAX_NUM_CELLS.y); ++i) {
			var oCellInfoDict = new Dictionary<int, STCellInfo>();

			for(int j = 0; j < Mathf.Clamp(nNumCellsX, SampleEngineName.KDefine.E_MIN_NUM_CELLS.x, SampleEngineName.KDefine.E_MAX_NUM_CELLS.x); ++j) {
				oCellInfoDict.TryAdd(j, Factory.MakeCellInfo(new Vector3Int(j, i, KCDefine.B_IDX_INVALID)));
			}

			a_oLevelInfo.m_oCellInfoDictContainer.TryAdd(i, oCellInfoDict);
		}

		a_oLevelInfo.OnAfterDeserialize();
		Func.SetupEditorCellInfos(a_oLevelInfo, a_oCreateInfo);
	}
	
	/** 에디터 셀 정보를 설정한다 */
	private static void SetupEditorCellInfos(CLevelInfo a_oLevelInfo, CEditorLevelCreateInfo a_oCreateInfo) {
		int nTryTimes = KCDefine.B_VAL_0_INT;

		do {
			var oIdxVDictContainer = CCollectionManager.Inst.SpawnDict<int, List<Vector3Int>>();
			var oIdxHDictContainer = CCollectionManager.Inst.SpawnDict<int, List<Vector3Int>>();

			for(int i = 0; i < a_oLevelInfo.m_oCellInfoDictContainer.Count; ++i) {
				for(int j = 0; j < a_oLevelInfo.m_oCellInfoDictContainer[i].Count; ++j) {
					var oIdxVList = oIdxVDictContainer.ContainsKey(j) ? oIdxVDictContainer[j] : new List<Vector3Int>();
					var oIdxHList = oIdxHDictContainer.ContainsKey(i) ? oIdxHDictContainer[i] : new List<Vector3Int>();

					oIdxVDictContainer.TryAdd(j, oIdxVList);
					oIdxHDictContainer.TryAdd(i, oIdxHList);
					
					oIdxVList.Add(a_oLevelInfo.m_oCellInfoDictContainer[i][j].m_stIdx);
					oIdxHList.Add(a_oLevelInfo.m_oCellInfoDictContainer[i][j].m_stIdx);

					a_oLevelInfo.m_oCellInfoDictContainer[i][j].m_oObjKindsDictContainer.Clear();
					a_oLevelInfo.m_oCellInfoDictContainer[i][j].m_oObjKindsDictContainer.TryAdd(EObjType.BG, new List<EObjKinds>() { EObjKinds.BG_EMPTY_01 });
				}
			}
			
			try {
				for(int i = 0; i < oIdxVDictContainer.Count; ++i) {
					oIdxVDictContainer.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, oIdxVDictContainer.Count));
				}

				for(int i = 0; i < oIdxHDictContainer.Count; ++i) {
					oIdxHDictContainer.ExSwap(i, Random.Range(KCDefine.B_VAL_0_INT, oIdxHDictContainer.Count));
				}

				Func.SetupEditorCellInfos(a_oLevelInfo, a_oCreateInfo, oIdxVDictContainer, oIdxHDictContainer);
			} finally {
				CCollectionManager.Inst.DespawnDict(oIdxVDictContainer);
				CCollectionManager.Inst.DespawnDict(oIdxHDictContainer);
			}
		} while(nTryTimes++ < KDefine.LES_MAX_TRY_TIMES_SETUP_CELL_INFOS && !Func.IsSetupEditorCellInfos(a_oLevelInfo, a_oCreateInfo));
		
		a_oLevelInfo.OnAfterDeserialize();
	}

	/** 에디터 셀 정보를 설정한다 */
	private static void SetupEditorCellInfos(CLevelInfo a_oLevelInfo, CEditorLevelCreateInfo a_oCreateInfo, Dictionary<int, List<Vector3Int>> a_oIdxVDictContainer, Dictionary<int, List<Vector3Int>> a_oIdxHDictContainer) {
		// Do Something
	}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
#endif			// #if SCRIPT_TEMPLATE_ONLY