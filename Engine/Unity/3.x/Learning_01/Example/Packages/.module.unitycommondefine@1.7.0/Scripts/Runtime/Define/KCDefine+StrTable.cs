using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	// 약관 동의 팝업 {
	public const string ST_KEY_AGREE_P_SERVICES_TITLE = "AGREE_P_SERVICES_TITLE";
	public const string ST_KEY_AGREE_P_PRIVACY_TITLE = "AGREE_P_PRIVACY_TITLE";
	public const string ST_KEY_AGREE_P_AGREE_TEXT = "AGREE_P_AGREE_TEXT";

	public const string ST_KEY_AGREE_P_EU_TITLE = "AGREE_P_EU_TITLE";
	public const string ST_KEY_AGREE_P_DESC_TEXT_01 = "AGREE_P_DESC_TEXT_01";
	public const string ST_KEY_AGREE_P_DESC_TEXT_02 = "AGREE_P_DESC_TEXT_02";
	public const string ST_KEY_AGREE_P_SERVICES_BTN_TEXT = "AGREE_P_SERVICES_BTN_TEXT";
	public const string ST_KEY_AGREE_P_PRIVACY_BTN_TEXT = "AGREE_P_PRIVACY_BTN_TEXT";

	public const string ST_KEY_AGREE_P_OK_BTN_TEXT = "AGREE_P_OK_BTN_TEXT";
	// 약관 동의 팝업 }

	// 설명 팝업 {
	public const string ST_KEY_DESC_P_TITLE = "DESC_P_TITLE";
	public const string ST_KEY_DESC_P_MSG = "DESC_P_MSG";

	public const string ST_KEY_DESC_P_DESC_MSG_01 = "DESC_P_DESC_MSG_01";
	public const string ST_KEY_DESC_P_DESC_MSG_02 = "DESC_P_DESC_MSG_02";
	public const string ST_KEY_DESC_P_DESC_MSG_03 = "DESC_P_DESC_MSG_03";

	public const string ST_KEY_DESC_P_NEXT_BTN_TEXT = "DESC_P_NEXT_BTN_TEXT";
	// 설명 팝업 }

	// 시작 씬 관리자
	public const string ST_KEY_START_SM_DOT_TEXT = "START_SM_DOT_TEXT";
	public const string ST_KEY_START_SM_LOADING_TEXT = "START_SM_LOADING_TEXT";

	// 공용 {
	public const string ST_KEY_COMMON_LOGIN_SUCCESS_MSG = "COMMON_LOGIN_SUCCESS_MSG";
	public const string ST_KEY_COMMON_LOGIN_FAIL_MSG = "COMMON_LOGIN_FAIL_MSG";
	
	public const string ST_KEY_COMMON_LOGOUT_SUCCESS_MSG = "COMMON_LOGOUT_SUCCESS_MSG";
	public const string ST_KEY_COMMON_LOGOUT_FAIL_MSG = "COMMON_LOGOUT_FAIL_MSG";

	public const string ST_KEY_COMMON_LOAD_SUCCESS_MSG = "COMMON_LOAD_SUCCESS_MSG";
	public const string ST_KEY_COMMON_LOAD_FAIL_MSG = "COMMON_LOAD_FAIL_MSG";

	public const string ST_KEY_COMMON_SAVE_SUCCESS_MSG = "COMMON_SAVE_SUCCESS_MSG";
	public const string ST_KEY_COMMON_SAVE_FAIL_MSG = "COMMON_SAVE_FAIL_MSG";

	public const string ST_KEY_COMMON_PURCHASE_SUCCESS_MSG = "COMMON_PURCHASE_SUCCESS_MSG";
	public const string ST_KEY_COMMON_PURCHASE_FAIL_MSG = "COMMON_PURCHASE_FAIL_MSG";

	public const string ST_KEY_COMMON_RESTORE_SUCCESS_MSG = "COMMON_RESTORE_SUCCESS_MSG";
	public const string ST_KEY_COMMON_RESTORE_FAIL_MSG = "COMMON_RESTORE_FAIL_MSG";

	public const string ST_KEY_COMMON_GET_TEXT = "COMMON_GET_TEXT";
	public const string ST_KEY_COMMON_NEXT_TEXT = "COMMON_NEXT_TEXT";
	public const string ST_KEY_COMMON_LEAVE_TEXT = "COMMON_LEAVE_TEXT";
	public const string ST_KEY_COMMON_SYNC_TEXT = "COMMON_SYNC_TEXT";
	public const string ST_KEY_COMMON_NOTICE_TEXT = "COMMON_NOTICE_TEXT";
	public const string ST_KEY_COMMON_WATCH_ADS_TEXT = "COMMON_WATCH_ADS_TEXT";

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

	// 상점 팝업
	public const string ST_KEY_STORE_P_TITLE = "STORE_P_TITLE";
	public const string ST_KEY_STORE_P_RESTORE_BTN_TEXT = "STORE_P_RESTORE_BTN_TEXT";

	// 설정 팝업 {
	public const string ST_KEY_SETTINGS_P_TITLE = "SETTINGS_P_TITLE";

	public const string ST_KEY_SETTINGS_P_BG_SND_BTN_TEXT = "SETTINGS_P_BG_SND_BTN_TEXT";
	public const string ST_KEY_SETTINGS_P_FX_SNDS_BTN_TEXT = "SETTINGS_P_FX_SNDS_BTN_TEXT";
	public const string ST_KEY_SETTINGS_P_NOTI_BTN_TEXT = "SETTINGS_P_NOTI_BTN_TEXT";
	public const string ST_KEY_SETTINGS_P_REVIEW_BTN_TEXT = "SETTINGS_P_REVIEW_BTN_TEXT";
	public const string ST_KEY_SETTINGS_P_SUPPORTS_BTN_TEXT = "SETTINGS_P_SUPPORTS_BTN_TEXT";
	// 설정 팝업 }

	// 동기화 팝업 {
	public const string ST_KEY_SYNC_P_TITLE = "SYNC_P_TITLE";

	public const string ST_KEY_SYNC_P_LOGIN_BTN_TEXT = "SYNC_P_LOGIN_BTN_TEXT";
	public const string ST_KEY_SYNC_P_LOGOUT_BTN_TEXT = "SYNC_P_LOGOUT_BTN_TEXT";

	public const string ST_KEY_SYNC_P_SAVE_BTN_TEXT = "SYNC_P_SAVE_BTN_TEXT";
	public const string ST_KEY_SYNC_P_LOAD_BTN_TEXT = "SYNC_P_LOAD_BTN_TEXT";
	// 동기화 팝업 }

	// 일일 미션 팝업
	public const string ST_KEY_DAILY_MP_TITLE = "DAILY_MP_TITLE";

	// 무료 보상 팝업
	public const string ST_KEY_FREE_RP_TITLE = "FREE_RP_TITLE";
	public const string ST_KEY_FREE_RP_ADS_BTN_TEXT = "FREE_RP_ADS_BTN_TEXT";

	// 일일 보상 팝업 {
	public const string ST_KEY_DAILY_RP_TITLE = "DAILY_RP_TITLE";

	public const string ST_KEY_DAILY_RP_ADS_BTN_TEXT = "DAILY_RP_ADS_BTN_TEXT";
	public const string ST_KEY_DAILY_RP_ACQUIRE_BTN_TEXT = "DAILY_RP_ACQUIRE_BTN_TEXT";
	// 일일 보상 팝업 }

	// 판매 코인 팝업
	public const string ST_KEY_SALE_CP_TITLE = "SALE_CP_TITLE";
	public const string ST_KEY_SALE_CP_OK_BTN_TEXT = "SALE_CP_OK_BTN_TEXT";

	// 보상 획득 팝업
	public const string ST_KEY_REWARD_AP_ACQUIRE_BTN_TEXT = "REWARD_AP_ACQUIRE_BTN_TEXT";
	#endregion			// 기본
}
