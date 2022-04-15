using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
/** 스테이지 스크롤러 셀 뷰 */
public partial class CStageScrollerCellView : CScrollerCellView {
	/** 매개 변수 */
	public new struct STParams {
		public CScrollerCellView.STParams m_stBaseParams;
	}

	#region 변수
	private STParams m_stParams;
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init(a_stParams.m_stBaseParams);
		m_stParams = a_stParams;

		for(int i = 0; i < this.ScrollerCellList.Count; ++i) {
			var stIDInfo = CFactory.MakeIDInfo(KCDefine.B_VAL_0_INT, i + m_stParams.m_stBaseParams.m_nID.ExUniqueLevelIDToStageID(), m_stParams.m_stBaseParams.m_nID.ExUniqueLevelIDToChapterID());
			int nNumStageInfos = CLevelInfoTable.Inst.GetNumStageInfos(stIDInfo.m_nChapterID);

			this.ScrollerCellList[i]?.SetActive(stIDInfo.m_nStageID < nNumStageInfos);

			// 스테이지 정보가 존재 할 경우
			if(stIDInfo.m_nStageID < nNumStageInfos) {
				this.UpdateScrollerCellState(this.ScrollerCellList[i], stIDInfo);
			}
		}
	}

	/** 스크롤러 셀 상태를 갱신한다 */
	protected virtual void UpdateScrollerCellState(GameObject a_oScrollerCell, STIDInfo a_stIDInfo) {
		// Do Something
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE