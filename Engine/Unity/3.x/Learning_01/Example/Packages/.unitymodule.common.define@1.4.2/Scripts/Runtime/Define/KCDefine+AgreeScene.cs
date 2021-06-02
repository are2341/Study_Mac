using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 약관 동의 씬 상수
public static partial class KCDefine {
	#region 기본
	// 이름
	public const string AS_OBJ_N_AGREE_POPUP = "AgreePopup";

	public const string AS_OBJ_N_SERVICES_TEXT = "ContentsText";
	public const string AS_OBJ_N_PRIVACY_TEXT = "ContentsText";

	public const string AS_OBJ_N_SERVICES_URL_TEXT = "ServicesURLText";
	public const string AS_OBJ_N_PRIVACY_URL_TEXT = "PrivacyURLText";

	public const string AS_OBJ_N_SERVICES_IMG = "CheckImg";
	public const string AS_OBJ_N_PRIVACY_IMG = "CheckImg";

	public const string AS_OBJ_N_SERVICES_BTN = "AgreeBtn";
	public const string AS_OBJ_N_PRIVACY_BTN = "AgreeBtn";

	public const string AS_OBJ_N_OK_BTN = "OKBtn";
	#endregion			// 기본

	#region 런타임 상수
	// 경로 {
	public static readonly string AS_DATA_P_SERVICES = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_AGREE_SCENE}AS_Services_{SystemLanguage.Korean}";
	public static readonly string AS_DATA_P_PRIVACY = $"{KCDefine.B_DIR_P_DATAS}{KCDefine.B_DIR_P_AGREE_SCENE}AS_Privacy_{SystemLanguage.Korean}";

	public static readonly string AS_OBJ_P_LANDSCAPE_AGREE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_AGREE_SCENE}AS_LandscapeAgreePopup";
	public static readonly string AS_OBJ_P_PORTRAIT_AGREE_POPUP = $"{KCDefine.B_DIR_P_PREFABS}{KCDefine.B_DIR_P_AGREE_SCENE}AS_PortraitAgreePopup";
	// 경로 }
	#endregion			// 런타임 상수
}
