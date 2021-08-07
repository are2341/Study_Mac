using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

//! 에디터 기본 팩토리
public static partial class EditorFactory {
	#region 클래스 함수
	//! 판매 아이템 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/SaleItemInfoTable")]
	public static void CreateSaleItemInfoTable() {
		CEditorFactory.CreateScriptableObj<CSaleItemInfoTable>(KCEditorDefine.B_ASSET_P_SALE_ITEM_INFO_TABLE);
	}

	//! 판매 상품 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/SaleProductInfoTable")]
	public static void CreateSaleProductInfoTable() {
		CEditorFactory.CreateScriptableObj<CSaleProductInfoTable>(KCEditorDefine.B_ASSET_P_SALE_PRODUCT_INFO_TABLE);
	}

	//! 미션 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/MissionInfoTable")]
	public static void CreateMissionInfoTable() {
		CEditorFactory.CreateScriptableObj<CMissionInfoTable>(KCEditorDefine.B_ASSET_P_MISSION_INFO_TABLE);
	}

	//! 보상 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/RewardInfoTable")]
	public static void CreateRewardInfoTable() {
		CEditorFactory.CreateScriptableObj<CRewardInfoTable>(KCEditorDefine.B_ASSET_P_REWARD_INFO_TABLE);
	}

	//! 에피소드 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/EpisodeInfoTable")]
	public static void CreateEpisodeInfoTable() {
		CEditorFactory.CreateScriptableObj<CEpisodeInfoTable>(KCEditorDefine.B_ASSET_P_EPISODE_INFO_TABLE);
	}

	//! 튜토리얼 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/ExtraCreate/TutorialInfoTable")]
	public static void CreateTutorialInfoTable() {
		CEditorFactory.CreateScriptableObj<CTutorialInfoTable>(KCEditorDefine.B_ASSET_P_TUTORIAL_INFO_TABLE);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
