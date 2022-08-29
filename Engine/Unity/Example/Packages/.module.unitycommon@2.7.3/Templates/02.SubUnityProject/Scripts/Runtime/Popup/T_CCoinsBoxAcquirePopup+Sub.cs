#if SCRIPT_TEMPLATE_ONLY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
/** 코인 상자 획득 팝업 */
public partial class CCoinsBoxAcquirePopup : CSubPopup {
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		CFunc.SetupComponents(new List<(EKey, string, GameObject)>() {
			(EKey.NUM_COINS_TEXT, $"{EKey.NUM_COINS_TEXT}", this.Contents)
		}, m_oTextDict, false);

		#region 추가
		this.SubAwakeSetup();
		#endregion			// 추가
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		this.Params = a_stParams;

		m_oIntDict.ExReplaceVal(EKey.PREV_NUM_COINS_BOX_COINS, Access.GetItemTargetVal(CGameInfoStorage.Inst.PlayCharacterID, EItemKinds.GOODS_COINS_BOX_COINS, ETargetKinds.ABILITY, (int)EAbilityKinds.STAT_NUMS));

		Func.Acquire(CGameInfoStorage.Inst.PlayCharacterID, Factory.MakeTargetInfo(ETargetKinds.ITEM_NUMS, (int)EItemKinds.GOODS_COINS_BOX_COINS, new STValInfo() {
			m_nVal = a_stParams.m_nNumCoinsBoxCoins, m_eValType = EValType.INT
		}), true);

		#region 추가
		this.SubInit();
		#endregion			// 추가
	}
	
	/** UI 상태를 변경한다 */
	private new void UpdateUIsState() {
		base.UpdateUIsState();

		m_oSaveUIs?.SetActive(m_oIntDict.GetValueOrDefault(EKey.PREV_NUM_COINS_BOX_COINS) < KDefine.G_MAX_NUM_COINS_BOX_COINS);
		m_oFullUIs?.SetActive(m_oIntDict.GetValueOrDefault(EKey.PREV_NUM_COINS_BOX_COINS) >= KDefine.G_MAX_NUM_COINS_BOX_COINS);
		
		// 텍스트를 갱신한다
		m_oTextDict.GetValueOrDefault(EKey.NUM_COINS_TEXT)?.ExSetText($"{m_oIntDict.GetValueOrDefault(EKey.PREV_NUM_COINS_BOX_COINS)}", EFontSet._1, false);

		#region 추가
		this.SubUpdateUIsState();
		#endregion			// 추가
	}
	#endregion			// 함수
}

/** 서브 코인 상자 획득 팝업 */
public partial class CCoinsBoxAcquirePopup : CSubPopup {
	/** 서브 식별자 */
	private enum ESubKey {
		NONE = -1,
		[HideInInspector] MAX_VAL
	}

	#region 변수

	#endregion			// 변수

	#region 프로퍼티

	#endregion			// 프로퍼티

	#region 함수
	/** 팝업을 설정한다 */
	private void SubAwakeSetup() {
		// Do Something
	}

	/** 초기화한다 */
	private void SubInit() {
		// Do Something
	}

	/** UI 상태를 갱신한다 */
	private void SubUpdateUIsState() {
		// Do Something
	}
	#endregion			// 함수
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY