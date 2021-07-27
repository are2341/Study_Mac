using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 문자열 테이블 상수
public static partial class KCDefine {
	#region 기본
	// 에디터 종료 팝업
	public const string ST_KEY_EDITOR_QUIT_P_MSG = "EDITOR_QUIT_P_MSG";

	// 에디터 레벨 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_LP_MSG = "EDITOR_REMOVE_LP_MSG";

	// 에디터 스테이지 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_SP_MSG = "EDITOR_REMOVE_SP_MSG";

	// 에디터 챕터 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_CP_MSG = "EDITOR_REMOVE_CP_MSG";

	// 경고 팝업
	public const string ST_KEY_ALERT_P_TITLE = "ALERT_P_TITLE";
	public const string ST_KEY_ALERT_P_OK_BTN_TEXT = "ALERT_P_OK_BTN_TEXT";
	public const string ST_KEY_ALERT_P_CANCEL_BTN_TEXT = "ALERT_P_CANCEL_BTN_TEXT";

	// 종료 팝업
	public const string ST_KEY_QUIT_P_MSG = "QUIT_P_MSG";

	// 업데이트 팝업
	public const string ST_KEY_UPDATE_P_MSG = "UPDATE_P_MSG";

	// 결제 팝업
	public const string ST_KEY_PURCHASE_P_SUCCESS_MSG = "PURCHASE_P_SUCCESS_MSG";
	public const string ST_KEY_PURCHASE_P_FAIL_MSG = "PURCHASE_P_FAIL_MSG";

	// 복원 팝업
	public const string ST_KEY_RESTORE_P_SUCCESS_MSG = "RESTORE_P_SUCCESS_MSG";
	public const string ST_KEY_RESTORE_P_FAIL_MSG = "RESTORE_P_FAIL_MSG";

	// 시작 씬 관리자
	public const string ST_KEY_START_SM_DOT_TEXT = "START_SM_DOT_TEXT";
	public const string ST_KEY_START_SM_LOADING_TEXT = "START_SM_LOADING_TEXT";

	// 약관 동의 팝업 {
	public const string ST_KEY_AGREE_P_SERVICES_TITLE = "AGREE_P_SERVICES_TITLE";
	public const string ST_KEY_AGREE_P_PRIVACY_TITLE = "AGREE_P_PRIVACY_TITLE";
	public const string ST_KEY_AGREE_P_AGREE_TEXT = "AGREE_P_AGREE_TEXT";

	public const string ST_KEY_AGREE_P_EU_TITLE = "AGREE_P_EU_TITLE";
	public const string ST_KEY_AGREE_P_DESC_TEXT_01 = "AGREE_P_DESC_TEXT_01";
	public const string ST_KEY_AGREE_P_DESC_TEXT_02 = "AGREE_P_DESC_TEXT_02";
	public const string ST_KEY_AGREE_P_SERVICES_URL_TEXT = "AGREE_P_SERVICES_URL_TEXT";
	public const string ST_KEY_AGREE_P_PRIVACY_URL_TEXT = "AGREE_P_PRIVACY_URL_TEXT";

	public const string ST_KEY_AGREE_P_OK_BTN_TEXT = "AGREE_P_OK_BTN_TEXT";
	// 약관 동의 팝업 }

	// 공용 {
	public const string ST_KEY_COMMON_LEVEL_TEXT = "COMMON_LEVEL_TEXT";
	public const string ST_KEY_COMMON_STAGE_TEXT = "COMMON_STAGE_TEXT";
	public const string ST_KEY_COMMON_CHAPTER_TEXT = "COMMON_CHAPTER_TEXT";

	public const string ST_KEY_COMMON_LEVEL_PAGE_TEXT_FMT = "COMMON_LEVEL_PAGE_TEXT_FMT";
	public const string ST_KEY_COMMON_STAGE_PAGE_TEXT_FMT = "COMMON_STAGE_PAGE_TEXT_FMT";
	public const string ST_KEY_COMMON_CHAPTER_PAGE_TEXT_FMT = "COMMON_CHAPTER_PAGE_TEXT_FMT";

	public const string ST_KEY_COMMON_NUM_LEVELS_TEXT_FMT = "COMMON_NUM_LEVELS_TEXT_FMT";
	public const string ST_KEY_COMMON_NUM_STAGES_TEXT_FMT = "COMMON_NUM_STAGES_TEXT_FMT";
	public const string ST_KEY_COMMON_NUM_CHAPTERS_TEXT_FMT = "COMMON_NUM_CHAPTERS_TEXT_FMT";
	// 공용 }
	#endregion			// 기본
}
