using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 에디터 기본 상수 */
public static partial class KCDefine {
	#region 기본
	// 이름 {
	public const string E_OBJ_N_BLOCK_SPRITE = "BlockSprite";

	public const string E_OBJ_N_PORTRAIT_UIS = "PortraitUIs";
	public const string E_OBJ_N_LANDSCAPE_UIS = "LandscapeUIs";

	public const string E_OBJ_N_LEFT_EDITOR_UIS = "LeftEditorUIs";
	public const string E_OBJ_N_RIGHT_EDITOR_UIS = "RightEditorUIs";
	public const string E_OBJ_N_MID_EDITOR_UIS = "MidEditorUIs";

	// 중앙 에디터 UI
	public const string E_OBJ_N_ME_UIS_MSG_UIS = "MsgUIs";

	// 왼쪽 에디터 UI
	public const string E_OBJ_N_LE_UIS_SET_UIS = "BottomMenuUIs_01";

	// 에디터 입력 팝업
	public const string E_OBJ_N_EDITOR_INPUT_POPUP = "EditorInputPopup";
	public const string E_OBJ_N_EDITOR_IP_INPUT = "Input";

	// 에디터 레벨 생성 팝업 {
	public const string E_OBJ_N_EDITOR_LEVEL_CREATE_POPUP = "EditorLevelCreatePopup";
	public const string E_OBJ_N_EDITOR_LCP_NUM_LEVELS_INPUT = "NumLevelsInput";

	public const string E_OBJ_N_EDITOR_LCP_MIN_NUM_CELLS_X_INPUT = "MinNumCellsXInput";
	public const string E_OBJ_N_EDITOR_LCP_MAX_NUM_CELLS_X_INPUT = "MaxNumCellsXInput";

	public const string E_OBJ_N_EDITOR_LCP_MIN_NUM_CELLS_Y_INPUT = "MinNumCellsYInput";
	public const string E_OBJ_N_EDITOR_LCP_MAX_NUM_CELLS_Y_INPUT = "MaxNumCellsYInput";
	// 에디터 레벨 생성 팝업 }
	
	// 에디터 스크롤러 셀 뷰
	public const string E_OBJ_N_ESCV_COPY_BTN = "CopyBtn";
	public const string E_OBJ_N_ESCV_MOVE_BTN = "MoveBtn";
	public const string E_OBJ_N_ESCV_REMOVE_BTN = "RemoveBtn";
	// 이름 }
	#endregion			// 기본

	#region 런타임 상수
	// 경로 {
	public static readonly string E_OBJ_P_EDITOR_INPUT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_EditorInputPopup";
	public static readonly string E_OBJ_P_EDITOR_LEVEL_CREATE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_EditorLevelCreatePopup";

	public static readonly string E_OBJ_P_LEVEL_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_LevelEditorScrollerCellView";
	public static readonly string E_OBJ_P_STAGE_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_StageEditorScrollerCellView";
	public static readonly string E_OBJ_P_CHAPTER_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_ChapterEditorScrollerCellView";
	// 경로 }
	#endregion			// 런타임 상수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
