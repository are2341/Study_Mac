using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 전역 상수
public static partial class KDefine {
	#region 기본
	// 개수
	public const int G_MAX_NUM_CHANGES = 0;

	// 횟수
	public const int G_MAX_TIMES_FREE_REWARD = 0;

	// 아이템 정보 테이블 {
	public const string G_KEY_ITEM_IT_NAME = "Name";
	public const string G_KEY_ITEM_IT_DESC = "Desc";

	public const string G_KEY_ITEM_IT_PRICE = "Price";
	public const string G_KEY_ITEM_IT_PRICE_TYPE = "PriceType";
	public const string G_KEY_ITEM_IT_PRICE_KINDS = "PriceKinds";

	public const string G_KEY_ITEM_IT_NUM_ITEMS = "NumItems";
	public const string G_KEY_ITEM_IT_ITEM_KINDS = "ItemKinds";

	public const string G_KEY_ITEM_IT_REPLACE = "Replace";
	// 아이템 정보 테이블 }

	// 씬 이름
	public const string G_SCENE_N_E01_EXAMPLE_052 = "E01.Example_052";
	public const string G_SCENE_N_E01_EXAMPLE_124 = "E01.Example_124";
	#endregion			// 기본

	#region 런타임 상수
	// 경로 {
#if UNITY_EDITOR
	public static readonly string G_RUNTIME_TABLE_P_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_ITEM_INFO}.json";
#else
	public static readonly string G_RUNTIME_TABLE_P_ITEM_INFO = $"{KCDefine.B_ABS_DIR_P_RUNTIME_EXTERNAL_DATAS}{KCDefine.U_TABLE_P_G_ITEM_INFO}.json";
#endif			// #if UNITY_EDITOR
	// 경로 }
	#endregion			// 런타임 상수
}
