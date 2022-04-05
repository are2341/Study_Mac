using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
/** 판매 코인 팝업 */
public partial class CSaleCoinsPopup : CSubPopup {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		NUM_SALE_COINS_TEXT,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	/** =====> UI <===== */
	private Dictionary<EKey, TMP_Text> m_oTextDict = new Dictionary<EKey, TMP_Text>() {
		[EKey.NUM_SALE_COINS_TEXT] = null
	};

	/** =====> 객체 <===== */
	[SerializeField] private GameObject m_oSaveUIs = null;
	[SerializeField] private GameObject m_oFullUIs = null;
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트를 설정한다
		m_oTextDict[EKey.NUM_SALE_COINS_TEXT] = this.Contents.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_NUM_SALE_COINS_TEXT);

		// 버튼을 설정한다
		this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_OK_BTN)?.onClick.AddListener(this.OnTouchOKBtn);
		this.Contents.ExFindComponent<Button>(KCDefine.U_OBJ_N_PURCHASE_BTN)?.onClick.AddListener(this.OnTouchPurchaseBtn);
	}

	/** 초기화 */
	public override void Init() {
		base.Init();
	}

	/** 팝업 컨텐츠를 설정한다 */
	protected override void SetupContents() {
		base.SetupContents();
		this.UpdateUIsState();
	}
	
	/** UI 상태를 변경한다 */
	private new void UpdateUIsState() {
		base.UpdateUIsState();
		long nNumSaleCoins = CUserInfoStorage.Inst.UserInfo.NumSaleCoins;

		m_oSaveUIs?.SetActive(nNumSaleCoins < KDefine.G_MAX_NUM_SALE_COINS);
		m_oFullUIs?.SetActive(nNumSaleCoins >= KDefine.G_MAX_NUM_SALE_COINS);

		// 텍스트를 갱신한다
		m_oTextDict[EKey.NUM_SALE_COINS_TEXT]?.ExSetText($"{nNumSaleCoins}", EFontSet._1, false);
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn() {
		this.OnTouchCloseBtn();
	}

	/** 결제 버튼을 눌렀을 경우 */
	private void OnTouchPurchaseBtn() {
#if PURCHASE_MODULE_ENABLE
		Func.PurchaseProduct(KDefine.G_PRODUCT_ID_SALE_COINS, this.OnPurchaseProduct);
#endif			// #if PURCHASE_MODULE_ENABLE
	}
	#endregion			// 함수

	#region 조건부 함수
#if PURCHASE_MODULE_ENABLE
	/** 상품이 결제 되었을 경우 */
	private void OnPurchaseProduct(CPurchaseManager a_oSender, string a_oProductID, bool a_bIsSuccess) {
		// 결제 되었을 경우
		if(a_bIsSuccess) {
			Func.AcquireProduct(a_oProductID);
			Func.OnPurchaseProduct(a_oSender, a_oProductID, a_bIsSuccess, null);
		}

		this.UpdateUIsState();
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 함수
}
#endif			// #if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
