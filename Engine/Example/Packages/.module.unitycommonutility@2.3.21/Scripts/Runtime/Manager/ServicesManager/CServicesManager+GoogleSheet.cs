using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if (UNITY_STANDALONE && GOOGLE_SHEET_ENABLE) && (DEBUG || DEVELOPMENT_BUILD)
using GoogleSheetsToUnity;

/** 서비스 관리자 - 구글 시트 */
public partial class CServicesManager : CSingleton<CServicesManager> {
	#region 함수
	/** 구글 시트를 로드한다 */
	public void LoadGoogleSheet(string a_oID, string a_oName, System.Action<CServicesManager, GstuSpreadSheet, STGoogleSheetInfo, bool> a_oCallback, int a_nSrcIdx = KCDefine.B_VAL_0_INT, int a_nNumCells = KCDefine.U_MAX_NUM_GOOGLE_SHEET_CELLS) {
		CFunc.ShowLog($"CServicesManager.LoadGoogleSheet: {a_oID}, {a_oName}, {a_nSrcIdx}, {Mathf.Min(a_nNumCells, KCDefine.U_MAX_NUM_GOOGLE_SHEET_CELLS)}");
		CAccess.Assert(a_oID.ExIsValid() && a_oName.ExIsValid() && a_nSrcIdx > KCDefine.B_IDX_INVALID);

		int nSrcIdx = a_nSrcIdx + KCDefine.B_VAL_1_INT;
		int nNumCells = Mathf.Min(a_nNumCells, KCDefine.U_MAX_NUM_GOOGLE_SHEET_CELLS);

		string oSrcCellName = string.Format(KCDefine.U_CELL_N_FMT_GOOGLE_SHEET_SRC, nSrcIdx);
		string oDestCellName = string.Format(KCDefine.U_CELL_N_FMT_GOOGLE_SHEET_DEST, a_nSrcIdx + nNumCells);

		m_oCallbackDict02.ExReplaceVal(EServicesCallback.LOAD_GOOGLE_SHEET, a_oCallback);
		SpreadsheetManager.Read(new GSTU_Search(a_oID, a_oName, oSrcCellName, oDestCellName, KCDefine.U_COL_N_GOOGLE_SHEET_SRC, nSrcIdx), (a_oGoogleSheet) => this.OnLoadGoogleSheet(a_oGoogleSheet, a_oID, a_oName, a_nSrcIdx, nNumCells));
	}

	/** 구글 시트를 로드했을 경우 */
	private void OnLoadGoogleSheet(GstuSpreadSheet a_oGoogleSheet, string a_oID, string a_oName, int a_nSrcIdx, int a_nNumCells) {
		CFunc.ShowLog($"CServicesManager.OnLoadGoogleSheet: {a_oID}, {a_oName}, {a_oGoogleSheet.Cells.ExIsValid()}");

		CScheduleManager.Inst.AddCallback(KCDefine.U_KEY_SERVICES_M_LOAD_GOOGLE_SHEET_CALLBACK, () => {
			m_oCallbackDict02.GetValueOrDefault(EServicesCallback.LOAD_GOOGLE_SHEET)?.Invoke(this, a_oGoogleSheet, new STGoogleSheetInfo() {
				m_nSrcIdx = a_nSrcIdx, m_nNumCells = a_nNumCells, m_oID = a_oID, m_oName = a_oName
			}, a_oGoogleSheet.Cells.ExIsValid());
		});
	}
	#endregion			// 함수
}
#endif			// #if (UNITY_STANDALONE && GOOGLE_SHEET_ENABLE) && (DEBUG || DEVELOPMENT_BUILD)
