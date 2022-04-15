using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if EXTRA_SCRIPT_ENABLE && ENGINE_TEMPLATES_MODULE_ENABLE
namespace SampleEngineName {
	/** 엔진 - 팩토리 */
	public partial class CEngine : CComponent {
		#region 함수
		/** 효과를 생성한다 */
		private CFX CreateFX(EFXKinds a_eFXKinds, Vector3 a_stPos) {
			var oFX = CFactory.CreateCloneObj<CFX>(KDefine.E_OBJ_N_FX, CResManager.Inst.GetRes<GameObject>(KDefine.E_OBJ_P_FX), m_stParams.m_oBlockObjs);
			oFX.transform.localPosition = a_stPos;

			oFX.Init(new CFX.STParams() {
				m_stFXInfo = CFXInfoTable.Inst.GetFXInfo(a_eFXKinds), m_oEngine = this
			});

			return oFX;
		}

		/** 블럭을 생성한다 */
		private CBlock CreateBlock(EBlockKinds a_eBlockKinds, Vector3 a_stPos, Vector3Int a_stIdx) {
			var oBlock = CFactory.CreateCloneObj<CBlock>(KDefine.E_OBJ_N_BLOCK, CResManager.Inst.GetRes<GameObject>(KDefine.E_OBJ_P_BLOCK), m_stParams.m_oBlockObjs);
			oBlock.Idx = a_stIdx;
			oBlock.transform.localPosition = a_stPos;

			oBlock.Init(new CBlock.STParams() {
				m_stBlockInfo = CBlockInfoTable.Inst.GetBlockInfo(a_eBlockKinds), m_oEngine = this
			});

			return oBlock;
		}
		#endregion			// 함수
	}
}
#endif			// #if EXTRA_SCRIPT_ENABLE && ENGINE_TEMPLATES_MODULE_ENABLE