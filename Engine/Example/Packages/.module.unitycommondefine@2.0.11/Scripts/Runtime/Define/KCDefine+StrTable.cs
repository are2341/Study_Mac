using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 문자열 테이블 상수 */
public static partial class KCDefine {
	#region 기본
	// 에디터 종료 팝업
	public const string ST_KEY_EDITOR_QUIT_P_MSG = "EDITOR_QUIT_P_MSG";

	// 에디터 리셋 팝업
	public const string ST_KEY_EDITOR_RESET_P_MSG = "EDITOR_RESET_P_MSG";

	// 에디터 A 세트 팝업
	public const string ST_KEY_EDITOR_A_SET_P_MSG = "EDITOR_A_SET_P_MSG";

	// 에디터 B 세트 팝업
	public const string ST_KEY_EDITOR_B_SET_P_MSG = "EDITOR_B_SET_P_MSG";

	// 에디터 테이블 다시 로드 팝업
	public const string ST_KEY_EDITOR_TABLE_RL_MSG = "EDITOR_TABLE_RL_MSG";

	// 에디터 레벨 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_LP_MSG = "EDITOR_REMOVE_LP_MSG";

	// 에디터 스테이지 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_SP_MSG = "EDITOR_REMOVE_SP_MSG";

	// 에디터 챕터 제거 팝업
	public const string ST_KEY_EDITOR_REMOVE_CP_MSG = "EDITOR_REMOVE_CP_MSG";

	// 종료 팝업
	public const string ST_KEY_QUIT_P_MSG = "QUIT_P_MSG";

	// 그만두기 팝업
	public const string ST_KEY_LEAVE_P_MSG = "LEAVE_P_MSG";

	// 업데이트 팝업
	public const string ST_KEY_UPDATE_P_MSG = "UPDATE_P_MSG";

	// 로드 팝업
	public const string ST_KEY_LOAD_P_MSG = "LOAD_P_MSG";

	// 저장 팝업
	public const string ST_KEY_SAVE_P_MSG = "SAVE_P_MSG";

	// 추적 설명 팝업 {
	public const string ST_KEY_TRACKING_DP_TITLE = "TRACKING_DP_TITLE";
	public const string ST_KEY_TRACKING_DP_MSG = "TRACKING_DP_MSG";

	public const string ST_KEY_TRACKING_DP_DESC_MSG_01 = "TRACKING_DP_DESC_MSG_01";
	public const string ST_KEY_TRACKING_DP_DESC_MSG_02 = "TRACKING_DP_DESC_MSG_02";
	public const string ST_KEY_TRACKING_DP_DESC_MSG_03 = "TRACKING_DP_DESC_MSG_03";
	// 추적 설명 팝업 }

	// 시작 씬 관리자
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

	public const string ST_KEY_COMMON_OK_TEXT = "COMMON_OK_TEXT";
	public const string ST_KEY_COMMON_CANCEL_TEXT = "COMMON_CANCEL_TEXT";
	public const string ST_KEY_COMMON_AGREE_TEXT = "COMMON_AGREE_TEXT";
	public const string ST_KEY_COMMON_RESULT_TEXT = "COMMON_RESULT_TEXT";

	public const string ST_KEY_COMMON_GET_TEXT = "COMMON_GET_TEXT";
	public const string ST_KEY_COMMON_STORE_TEXT = "COMMON_STORE_TEXT";
	public const string ST_KEY_COMMON_EVENT_TEXT = "COMMON_EVENT_TEXT";
	public const string ST_KEY_COMMON_NEXT_TEXT = "COMMON_NEXT_TEXT";
	public const string ST_KEY_COMMON_HOME_TEXT = "COMMON_HOME_TEXT";
	public const string ST_KEY_COMMON_PLAY_TEXT = "COMMON_PLAY_TEXT";
	public const string ST_KEY_COMMON_RETRY_TEXT = "COMMON_RETRY_TEXT";
	public const string ST_KEY_COMMON_LEAVE_TEXT = "COMMON_LEAVE_TEXT";
	public const string ST_KEY_COMMON_SYNC_TEXT = "COMMON_SYNC_TEXT";

	public const string ST_KEY_COMMON_LOGIN_TEXT = "COMMON_LOGIN_TEXT";
	public const string ST_KEY_COMMON_APPLE_LOGIN_TEXT = "COMMON_APPLE_LOGIN_TEXT";
	public const string ST_KEY_COMMON_FACEBOOK_LOGIN_TEXT = "COMMON_FACEBOOK_LOGIN_TEXT";

	public const string ST_KEY_COMMON_LOGOUT_TEXT = "COMMON_LOGOUT_TEXT";
	public const string ST_KEY_COMMON_DISCONNECT_TEXT = "COMMON_DISCONNECT_TEXT";

	public const string ST_KEY_COMMON_LOAD_TEXT = "COMMON_LOAD_TEXT";
	public const string ST_KEY_COMMON_SAVE_TEXT = "COMMON_SAVE_TEXT";
	public const string ST_KEY_COMMON_CONTINUE_TEXT = "COMMON_CONTINUE_TEXT";
	
	public const string ST_KEY_COMMON_NOTI_TEXT = "COMMON_NOTI_TEXT";
	public const string ST_KEY_COMMON_REVIEW_TEXT = "COMMON_REVIEW_TEXT";
	public const string ST_KEY_COMMON_SUPPORTS_TEXT = "COMMON_SUPPORTS_TEXT";
	public const string ST_KEY_COMMON_BG_SND_TEXT = "COMMON_BG_SND_TEXT";
	public const string ST_KEY_COMMON_FX_SNDS_TEXT = "COMMON_FX_SNDS_TEXT";
	public const string ST_KEY_COMMON_WATCH_ADS_TEXT = "COMMON_WATCH_ADS_TEXT";
	public const string ST_KEY_COMMON_RESTORE_PAYMENT_TEXT = "COMMON_RESTORE_PAYMENT_TEXT";

	public const string ST_KEY_COMMON_LEVEL_TEXT = "COMMON_LEVEL_TEXT";
	public const string ST_KEY_COMMON_STAGE_TEXT = "COMMON_STAGE_TEXT";
	public const string ST_KEY_COMMON_CHAPTER_TEXT = "COMMON_CHAPTER_TEXT";

	public const string ST_KEY_COMMON_LEVEL_NUM_TEXT_FMT = "COMMON_LEVEL_NUM_TEXT_FMT";
	public const string ST_KEY_COMMON_STAGE_NUM_TEXT_FMT = "COMMON_STAGE_NUM_TEXT_FMT";
	public const string ST_KEY_COMMON_CHAPTER_NUM_TEXT_FMT = "COMMON_CHAPTER_NUM_TEXT_FMT";

	public const string ST_KEY_COMMON_LEVEL_PAGE_TEXT_FMT = "COMMON_LEVEL_PAGE_TEXT_FMT";
	public const string ST_KEY_COMMON_STAGE_PAGE_TEXT_FMT = "COMMON_STAGE_PAGE_TEXT_FMT";
	public const string ST_KEY_COMMON_CHAPTER_PAGE_TEXT_FMT = "COMMON_CHAPTER_PAGE_TEXT_FMT";
	// 공용 }

	// 상점 팝업
	public const string ST_KEY_STORE_P_TITLE = "STORE_P_TITLE";

	// 설정 팝업
	public const string ST_KEY_SETTINGS_P_TITLE = "SETTINGS_P_TITLE";

	// 동기화 팝업
	public const string ST_KEY_SYNC_P_TITLE = "SYNC_P_TITLE";

	// 일일 미션 팝업
	public const string ST_KEY_DAILY_MP_TITLE = "DAILY_MP_TITLE";

	// 무료 보상 팝업
	public const string ST_KEY_FREE_RP_TITLE = "FREE_RP_TITLE";

	// 일일 보상 팝업
	public const string ST_KEY_DAILY_RP_TITLE = "DAILY_RP_TITLE";

	// 판매 코인 팝업
	public const string ST_KEY_SALE_CP_TITLE = "SALE_CP_TITLE";

	// 이어하기 팝업
	public const string ST_KEY_CONTINUE_P_TITLE = "CONTINUE_P_TITLE";

	// 결과 팝업
	public const string ST_KEY_RESULT_P_TITLE = "RESULT_P_TITLE";
	#endregion			// 기본
}
