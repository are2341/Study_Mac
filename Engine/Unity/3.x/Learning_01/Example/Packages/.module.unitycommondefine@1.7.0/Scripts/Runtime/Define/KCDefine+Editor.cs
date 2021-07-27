using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE
//! 에디터 기본 상수
public static partial class KCDefine {
	#region 기본
	// 이름 {
	public const string E_OBJ_N_PORTRAIT_UIS = "PortraitUIs";
	public const string E_OBJ_N_LANDSCAPE_UIS = "LandscapeUIs";

	public const string E_OBJ_N_LEFT_EDITOR_UIS = "LeftEditorUIs";
	public const string E_OBJ_N_RIGHT_EDITOR_UIS = "RightEditorUIs";
	public const string E_OBJ_N_MID_EDITOR_UIS = "MidEditorUIs";

	// 에디터 입력 팝업 {
	public const string E_OBJ_N_EDITOR_INPUT_POPUP = "EditorInputPopup";
	public const string E_OBJ_N_EDITOR_IP_INPUT = "Input";

	public const string E_OBJ_N_EDITOR_IP_OK_BTN = "OKBtn";
	public const string E_OBJ_N_EDITOR_IP_CANCEL_BTN = "CancelBtn";
	// 에디터 입력 팝업 }
	
	// 에디터 스크롤러 셀 뷰 {
	public const string E_OBJ_N_ESCV_NAME_TEXT = "NameText";

	public const string E_OBJ_N_ESCV_COPY_BTN = "CopyBtn";
	public const string E_OBJ_N_ESCV_MOVE_BTN = "MoveBtn";
	public const string E_OBJ_N_ESCV_REMOVE_BTN = "RemoveBtn";
	// 에디터 스크롤러 셀 뷰 }
	// 이름 }
	#endregion			// 기본

	#region 런타임 상수
	// 경로 {
	public static readonly string E_OBJ_P_EDITOR_INPUT_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_POPUP}G_EditorInputPopup";

	public static readonly string E_OBJ_P_LEVEL_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_LevelEditorScrollerCellView";
	public static readonly string E_OBJ_P_STAGE_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_StageEditorScrollerCellView";
	public static readonly string E_OBJ_P_CHAPTER_EDITOR_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_GLOBAL}{KCDefine.B_DIR_P_SCROLL_VIEW}G_ChapterEditorScrollerCellView";
	// 경로 }
	#endregion			// 런타임 상수
}
#endif			// #if UNITY_EDITOR || UNITY_STANDALONE
