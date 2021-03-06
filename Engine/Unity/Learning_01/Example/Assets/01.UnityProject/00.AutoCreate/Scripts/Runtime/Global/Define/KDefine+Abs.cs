using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 기본 상수
public static partial class KDefine {
	#region 기본
	// 버전 {
	public const string B_VER_APP_INFO = "1.0.0";
	public const string B_VER_USER_INFO = "1.0.0";
	public const string B_VER_GAME_INFO = "1.0.0";
	
	public const string B_VER_CLEAR_INFO = "1.0.0";
	// 버전 }
	#endregion			// 기본

	#region 런타임 상수
	// 경로
	public static readonly string B_DATA_P_APP_INFO = $"{KCDefine.B_DIR_P_WRITABLE}AppInfo.bytes";
	public static readonly string B_DATA_P_USER_INFO = $"{KCDefine.B_DIR_P_WRITABLE}UserInfo.bytes";
	public static readonly string B_DATA_P_GAME_INFO = $"{KCDefine.B_DIR_P_WRITABLE}GameInfo.bytes";
	#endregion			// 런타임 상수
}
