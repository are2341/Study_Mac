using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
/** 레벨 에디터 씬 상수 */
public static partial class KCDefine {
	#region 기본
	// 중앙 에디터 UI {
	public const string LES_OBJ_N_ME_UIS_PREV_BTN = "PrevBtn";
	public const string LES_OBJ_N_ME_UIS_NEXT_BTN = "NextBtn";

	public const string LES_OBJ_N_ME_UIS_SAVE_BTN = "SaveBtn";
	public const string LES_OBJ_N_ME_UIS_RESET_BTN = "ResetBtn";
	public const string LES_OBJ_N_ME_UIS_TEST_BTN = "TestBtn";
	public const string LES_OBJ_N_ME_UIS_TABLE_RELOAD_BTN = "TableReloadBtn";

	public const string LES_OBJ_N_ME_UIS_COPY_LEVEL_BTN = "CopyLevelBtn";
	public const string LES_OBJ_N_ME_UIS_MOVE_LEVEL_BTN = "MoveLevelBtn";
	public const string LES_OBJ_N_ME_UIS_REMOVE_LEVEL_BTN = "RemoveLevelBtn";
	// 중앙 에디터 UI }

	// 왼쪽 에디터 UI {
	public const string LES_OBJ_N_LE_UIS_A_SET_BTN = "ASetBtn";
	public const string LES_OBJ_N_LE_UIS_B_SET_BTN = "BSetBtn";

	public const string LES_OBJ_N_LE_UIS_ADD_LEVEL_BTN = "AddLevelBtn";
	public const string LES_OBJ_N_LE_UIS_ADD_STAGE_BTN = "AddStageBtn";
	public const string LES_OBJ_N_LE_UIS_ADD_CHAPTER_BTN = "AddChapterBtn";

	public const string LES_OBJ_N_LE_UIS_STAGE_SCROLL_VIEW_A = "StageScrollView_A";
	public const string LES_OBJ_N_LE_UIS_STAGE_SCROLL_VIEW_B = "StageScrollView_B";
	// 왼쪽 에디터 UI }

	// 오른쪽 에디터 UI {
	public const string LES_OBJ_N_RE_UIS_PREV_BTN = "PrevBtn";
	public const string LES_OBJ_N_RE_UIS_NEXT_BTN = "NextBtn";

	public const string LES_OBJ_N_RE_UIS_APPLY_BTN = "ApplyBtn";
	public const string LES_OBJ_N_RE_UIS_LOAD_LEVEL_BTN = "LoadLevelBtn";
	public const string LES_OBJ_N_RE_UIS_REMOVE_LEVEL_BTN = "RemoveLevelBtn";
	
	public const string LES_OBJ_N_RE_UIS_LEVEL_INPUT = "LevelInput";
	public const string LES_OBJ_N_RE_UIS_NUM_CELLS_X_INPUT = "NumCellsXInput";
	public const string LES_OBJ_N_RE_UIS_NUM_CELLS_Y_INPUT = "NumCellsYInput";
	
	public const string LES_OBJ_N_RE_UIS_PAGE_VIEW = "PageView";
	// 오른쪽 에디터 UI }
	#endregion			// 기본
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
