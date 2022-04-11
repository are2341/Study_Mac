using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 메인 씬 상수 */
public static partial class KCDefine {
	#region 기본
	// AB 테스트 UI
	public const string MS_OBJ_N_AB_T_UIS_SET_UIS = "SetUIs";
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string MS_OBJ_P_LEVEL_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_MAIN_SCENE}MS_LevelScrollerCellView";
	public static readonly string MS_OBJ_P_STAGE_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_MAIN_SCENE}MS_StageScrollerCellView";
	public static readonly string MS_OBJ_P_CHAPTER_SCROLLER_CELL_VIEW = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_MAIN_SCENE}MS_ChapterScrollerCellView";
	#endregion			// 런타임 상수
}
