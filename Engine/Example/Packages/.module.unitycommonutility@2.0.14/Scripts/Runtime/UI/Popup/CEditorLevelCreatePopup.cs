using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 에디터 레벨 생성 팝업 */
public class CEditorLevelCreatePopup : CPopup {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,
		OK_CANCEL,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public Dictionary<ECallback, System.Action<CEditorLevelCreatePopup, CEditorLevelCreateInfo, bool>> m_oCallbackDict;
	}

	#region 변수
	private STParams m_stParams;

	/** =====> UI <===== */
	protected InputField m_oNumLevelsInput = null;

	protected InputField m_oMinNumCellsXInput = null;
	protected InputField m_oMaxNumCellsXInput = null;

	protected InputField m_oMinNumCellsYInput = null;
	protected InputField m_oMaxNumCellsYInput = null;
	#endregion			// 변수
	
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.IsIgnoreAni = true;

		// 입력 필드를 설정한다 {
		m_oNumLevelsInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_LCP_NUM_LEVELS_INPUT);

		m_oMinNumCellsXInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_LCP_MIN_NUM_CELLS_X_INPUT);
		m_oMaxNumCellsXInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_LCP_MAX_NUM_CELLS_X_INPUT);

		m_oMinNumCellsYInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_LCP_MIN_NUM_CELLS_Y_INPUT);
		m_oMaxNumCellsYInput = m_oContents.ExFindComponent<InputField>(KCDefine.E_OBJ_N_EDITOR_LCP_MAX_NUM_CELLS_Y_INPUT);
		// 입력 필드를 설정한다 }

		// 버튼을 설정한다
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN)?.onClick.AddListener(this.OnTouchOKBtn);
		m_oContents.ExFindComponent<Button>(KCDefine.U_OBJ_N_CANCEL_BTN)?.onClick.AddListener(this.OnTouchCancelBtn);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;

		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	protected void UpdateUIsState() {
		// Do Something
	}

	/** 에디터 레벨 생성 정보를 생성한다 */
	protected virtual CEditorLevelCreateInfo CreateEditorLevelCreateInfo() {
		int.TryParse(m_oNumLevelsInput.text, out int nNumLevels);

		int.TryParse(m_oMinNumCellsXInput.text, out int nMinNumCellsX);
		int.TryParse(m_oMaxNumCellsXInput.text, out int nMaxNumCellsX);

		int.TryParse(m_oMinNumCellsYInput.text, out int nMinNumCellsY);
		int.TryParse(m_oMaxNumCellsYInput.text, out int nMaxNumCellsY);
		
		return new CEditorLevelCreateInfo() {
			m_nNumLevels = nNumLevels,
			m_stMinNumCells = new Vector3Int(Mathf.Min(nMinNumCellsX, nMaxNumCellsX), Mathf.Min(nMinNumCellsY, nMaxNumCellsY), KCDefine.B_VAL_0_INT),
			m_stMaxNumCells = new Vector3Int(Mathf.Max(nMinNumCellsX, nMaxNumCellsX), Mathf.Max(nMinNumCellsY, nMaxNumCellsY), KCDefine.B_VAL_0_INT)
		};
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, this.CreateEditorLevelCreateInfo(), true);
		this.OnTouchCloseBtn();
	}

	/** 취소 버튼을 눌렀을 경우 */
	private void OnTouchCancelBtn() {
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.OK_CANCEL)?.Invoke(this, null, false);
		this.OnTouchCloseBtn();
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
