using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 레벨 에디터 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름 {
	public const string LES_OBJ_N_LEVEL_SCROLL_VIEW = "LevelScrollView";
	public const string LES_OBJ_N_STAGE_SCROLL_VIEW = "StageScrollView";
	public const string LES_OBJ_N_CHAPTER_SCROLL_VIEW = "ChapterScrollView";

	// 왼쪽 에디터 UI
	public const string LES_OBJ_N_LE_UIS_ADD_LEVEL_BTN = "AddLevelBtn";
	public const string LES_OBJ_N_LE_UIS_ADD_STAGE_BTN = "AddStageBtn";
	public const string LES_OBJ_N_LE_UIS_ADD_CHAPTER_BTN = "AddChapterBtn";

	// 오른쪽 에디터 UI {
	public const string LES_OBJ_N_RE_UIS_TITLE_TEXT = "TitleText";

	public const string LES_OBJ_N_RE_UIS_APPLY_BTN = "ApplyBtn";
	public const string LES_OBJ_N_RE_UIS_LOAD_LEVEL_BTN = "LoadLevelBtn";
	
	public const string LES_OBJ_N_RE_UIS_LEVEL_INPUT = "LevelInput";
	public const string LES_OBJ_N_RE_UIS_NUM_CELLS_X_INPUT = "NumCellsXInput";
	public const string LES_OBJ_N_RE_UIS_NUM_CELLS_Y_INPUT = "NumCellsYInput";
	// 오른쪽 에디터 UI }

	// 중앙 에디터 UI {
	public const string LES_OBJ_N_ME_UIS_LEVEL_TEXT = "LevelText";

	public const string LES_OBJ_N_ME_UIS_PREV_LEVEL_BTN = "PrevLevelBtn";
	public const string LES_OBJ_N_ME_UIS_NEXT_LEVEL_BTN = "NextLevelBtn";

	public const string LES_OBJ_N_ME_UIS_COPY_LEVEL_BTN = "CopyLevelBtn";
	public const string LES_OBJ_N_ME_UIS_MOVE_LEVEL_BTN = "MoveLevelBtn";
	public const string LES_OBJ_N_ME_UIS_REMOVE_LEVEL_BTN = "RemoveLevelBtn";

	public const string LES_OBJ_N_ME_UIS_SAVE_BTN = "SaveBtn";
	public const string LES_OBJ_N_ME_UIS_RESET_BTN = "ResetBtn";
	public const string LES_OBJ_N_ME_UIS_TEST_BTN = "TestBtn";
	// 중앙 에디터 UI }
	// 이름 }
	#endregion			// 기본
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
