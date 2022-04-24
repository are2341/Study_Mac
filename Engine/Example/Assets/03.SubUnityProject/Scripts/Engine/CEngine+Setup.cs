using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if EXTRA_SCRIPT_ENABLE && ENGINE_TEMPLATES_MODULE_ENABLE
namespace SampleEngineName {
	/** 엔진 - 설정 */
	public partial class CEngine : CComponent {
		#region 함수

		#endregion			// 함수

		#region 조건부 함수
#if RUNTIME_TEMPLATES_MODULE_ENABLE
		/** 엔진을 설정한다 */
		private void SetupEngine() {
			m_oBlockInfoDictContainers = new Dictionary<EBlockType, List<(EBlockKinds, CEBlock)>>[m_stParams.m_oLevelInfo.NumCells.y, m_stParams.m_oLevelInfo.NumCells.x];
			this.GridInfo = Factory.MakeGridInfo(m_stParams.m_oLevelInfo, Vector3.zero);
		}
		
		/** 레벨을 설정한다 */
		private void SetupLevel() {
			for(int i = 0; i < m_stParams.m_oLevelInfo.m_oCellInfoDictContainer.Count; ++i) {
				for(int j = 0; j < m_stParams.m_oLevelInfo.m_oCellInfoDictContainer[i].Count; ++j) {
					this.SetupCell(m_stParams.m_oLevelInfo.m_oCellInfoDictContainer[i][j]);
				}
			}
		}

		/** 셀을 설정한다 */
		private void SetupCell(CCellInfo a_oCellInfo) {
			var oBlockInfoDictContainer = new Dictionary<EBlockType, List<(EBlockKinds, CEBlock)>>();

			foreach(var stKeyVal in a_oCellInfo.m_oBlockKindsDictContainer) {
				var oBlockInfoList = new List<(EBlockKinds, CEBlock)>();

				for(int i = 0; i < stKeyVal.Value.Count; ++i) {
					// Do Something
				}

				oBlockInfoDictContainer.TryAdd(stKeyVal.Key, oBlockInfoList);
			}

			m_oBlockInfoDictContainers[a_oCellInfo.m_stIdx.y, a_oCellInfo.m_stIdx.x] = oBlockInfoDictContainer;
		}

		/** 그리드 라인을 설정한다 */
		private void SetupGridLine() {
			// Do Something
		}
#endif			// #if RUNTIME_TEMPLATES_MODULE_ENABLE
		#endregion			// 조건부 함수
	}
}
#endif			// #if EXTRA_SCRIPT_ENABLE && ENGINE_TEMPLATES_MODULE_ENABLE
