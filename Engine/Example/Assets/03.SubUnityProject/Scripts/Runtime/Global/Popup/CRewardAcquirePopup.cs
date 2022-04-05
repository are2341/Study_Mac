using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
/** 보상 획득 팝업 */
public partial class CRewardAcquirePopup : CSubPopup {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		ACQUIRE_BTN,
		REWARD_ADS_BTN,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public ERewardQuality m_eQuality;
		public ERewardAcquirePopupType m_eAgreePopup;
		
		public List<STItemInfo> m_oItemInfoList;
	}

	#region 상수
	private const string KEY_ACQUIRE_BTN = "BlindBtn";
	private const string KEY_REWARD_ADS_BTN = "BlindBtn";
	#endregion			// 상수

	#region 변수
	private STParams m_stParams;

	/** =====> UI <===== */
	private Dictionary<EKey, Button> m_oBtnDict = new Dictionary<EKey, Button>() {
		[EKey.ACQUIRE_BTN] = null,
		[EKey.REWARD_ADS_BTN] = null
	};

	/** =====> 객체 <===== */
	[SerializeField] private GameObject m_oRewardUIs = null;
	[SerializeField] private List<GameObject> m_oItemUIsList = new List<GameObject>();
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.IsIgnoreAni = true;
		this.IsIgnoreNavStackEvent = true;

		// 버튼을 설정한다 {
		m_oBtnDict[EKey.ACQUIRE_BTN] = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_ACQUIRE_BTN);
		m_oBtnDict[EKey.ACQUIRE_BTN]?.onClick.AddListener(this.OnTouchAcquireBtn);

		m_oBtnDict[EKey.REWARD_ADS_BTN] = this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_REWARD_ADS_BTN);
		m_oBtnDict[EKey.REWARD_ADS_BTN]?.onClick.AddListener(this.OnTouchRewardAdsBtn);
		// 버튼을 설정한다 }
	}
	
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init();
		m_stParams = a_stParams;
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}
	
	/** UI 상태를 갱신한다 */
	private new void UpdateUIsState() {
		// 보상 아이템 UI 상태를 갱신한다
		for(int i = 0; i < m_oItemUIsList.Count; ++i) {
			var oItemUIs = m_oItemUIsList[i];
			oItemUIs.SetActive(i < m_stParams.m_oItemInfoList.Count);
			
			// 보상 정보가 존재 할 경우
			if(i < m_stParams.m_oItemInfoList.Count) {
				this.UpdateItemUIsState(oItemUIs, m_stParams.m_oItemInfoList[i]);
			}
		}
	}

	/** 보상 아이템 UI 상태를 갱신한다 */
	private void UpdateItemUIsState(GameObject a_oItemUIs, STItemInfo a_stItemInfo) {
		var oNumText = a_oItemUIs.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_NUM_TEXT);
		oNumText?.ExSetText(string.Format(KCDefine.B_TEXT_FMT_CROSS, a_stItemInfo.m_nNumItems), EFontSet._1, false);
	}

	/** 획득 버튼을 눌렀을 경우 */
	private void OnTouchAcquireBtn() {
		this.AcquireItems(false);
	}

	/** 보상 광고 버튼을 눌렀을 경우 */
	private void OnTouchRewardAdsBtn() {
#if ADS_MODULE_ENABLE
		Func.ShowRewardAds(this.OnCloseRewardAds);
#endif			// #if ADS_MODULE_ENABLE
	}

	/** 아이템을 획득한다 */
	private void AcquireItems(bool a_bIsWatchRewardAds) {
		m_oBtnDict[EKey.ACQUIRE_BTN]?.ExSetInteractable(false);
		m_oBtnDict[EKey.REWARD_ADS_BTN]?.ExSetInteractable(false);

#if ADS_MODULE_ENABLE
		m_oBtnDict[EKey.REWARD_ADS_BTN]?.gameObject.ExRemoveComponent<CRewardAdsTouchInteractable>();
#endif			// #if ADS_MODULE_ENABLE

		for(int i = 0; i < m_stParams.m_oItemInfoList.Count; ++i) {
			Func.AcquireItem(m_stParams.m_oItemInfoList[i], a_bIsWatchRewardAds ? m_stParams.m_oItemInfoList[i].m_nNumItems : KCDefine.B_VAL_0_INT);
		}

		this.OnTouchCloseBtn();
	}
	#endregion			// 함수

	#region 조건부 함수
#if ADS_MODULE_ENABLE
	/** 보상 광고가 닫혔을 경우 */
	private void OnCloseRewardAds(CAdsManager a_oSender, STAdsRewardInfo a_stAdsRewardInfo, bool a_bIsSuccess) {
		// 광고를 시청했을 경우
		if(a_bIsSuccess) {
			this.AcquireItems(true);
		}
	}
#endif			// #if ADS_MODULE_ENABLE
	#endregion			// 조건부 함수
}
#endif			// #if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
