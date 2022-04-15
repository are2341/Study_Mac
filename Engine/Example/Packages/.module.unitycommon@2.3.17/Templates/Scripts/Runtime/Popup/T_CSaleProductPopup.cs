using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if SCRIPT_TEMPLATE_ONLY
#if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

/** 판매 상품 팝업 */
public partial class CSaleProductPopup : CSubPopup {
	/** 콜백 */
	public enum ECallback {
		NONE = -1,

#if PURCHASE_MODULE_ENABLE
		PURCHASE,
#endif			// #if PURCHASE_MODULE_ENABLE

		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public ESaleProductKinds m_eSaleProductKinds;

#if PURCHASE_MODULE_ENABLE
		public Dictionary<ECallback, System.Action<CPurchaseManager, string, bool>> m_oCallbackDict;
#endif			// #if PURCHASE_MODULE_ENABLE
	}

	#region 변수
	private STParams m_stParams;
	
	/** =====> 객체 <===== */
	[SerializeField] private List<GameObject> m_oSpecialPkgsUIsDict = new List<GameObject>();
	#endregion			// 변수

	#region 함수
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
		for(int i = 0; i < m_oSpecialPkgsUIsDict.Count; ++i) {
			this.UpdateSpecialPkgsUIsState(m_oSpecialPkgsUIsDict[i], CSaleProductInfoTable.Inst.GetSaleProductInfo(KDefine.G_SALE_PRODUCT_KINDS_PRODUCT_SPECIAL_PKGS_LIST[i]));
		}
	}

	/** 특수 패키지 UI 상태를 갱신한다 */
	private void UpdateSpecialPkgsUIsState(GameObject a_oSpecialPkgsUIs, STSaleProductInfo a_stSaleProductInfo) {
		// 텍스트를 설정한다 {
		var oPriceText = a_oSpecialPkgsUIs.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_PRICE_TEXT);
		oPriceText?.ExSetText(string.Format(KCDefine.B_TEXT_FMT_USD_PRICE, a_stSaleProductInfo.m_oPrice), EFontSet._1, false);

		a_oSpecialPkgsUIs.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_NAME_TEXT)?.ExSetText(CStrTable.Inst.GetStr(a_stSaleProductInfo.m_oName), EFontSet._1, false);
		a_oSpecialPkgsUIs.ExFindComponent<TMP_Text>(KCDefine.U_OBJ_N_PRICE_TEXT)?.ExSetText(string.Format(KCDefine.B_TEXT_FMT_USD_PRICE, a_stSaleProductInfo.m_oPrice), EFontSet._1, false);

#if !UNITY_EDITOR && PURCHASE_MODULE_ENABLE
		// 상품이 존재 할 경우
		if(Access.GetProduct(Access.GetSaleProductID(a_stSaleProductInfo.m_eSaleProductKinds)) != null) {
			int nID = Access.GetSaleProductID(a_stSaleProductInfo.m_eSaleProductKinds);
			oPriceText?.ExSetText(Access.GetPriceStr(nID), EFontSet._1, false);
		}
#endif			// #if !UNITY_EDITOR && PURCHASE_MODULE_ENABLE
		// 텍스트를 설정한다 }

		// 버튼을 설정한다 {
		var oPurchaseBtn = a_oSpecialPkgsUIs?.ExFindComponent<Button>(KCDefine.U_OBJ_N_PURCHASE_BTN);
		oPurchaseBtn?.ExAddListener(() => this.OnTouchPurchaseBtn(a_stSaleProductInfo));

#if PURCHASE_MODULE_ENABLE
		// 비소모 상품 일 경우
		if(a_stSaleProductInfo.m_eProductType == ProductType.NonConsumable) {
			var stProductInfo = CProductInfoTable.Inst.GetProductInfo(Access.GetSaleProductID(a_stSaleProductInfo.m_eSaleProductKinds));
			oPurchaseBtn?.ExSetInteractable(!CPurchaseManager.Inst.IsPurchaseNonConsumableProduct(stProductInfo.m_oID));
		}
#endif			// #if PURCHASE_MODULE_ENABLE
		// 버튼을 설정한다 }
	}

	/** 결제 버튼을 눌렀을 경우 */
	private void OnTouchPurchaseBtn(STSaleProductInfo a_stSaleProductInfo) {
#if PURCHASE_MODULE_ENABLE
		CSceneManager.GetSceneManager<OverlayScene.CSubOverlaySceneManager>(KCDefine.B_SCENE_N_OVERLAY)?.PurchaseProduct(a_stSaleProductInfo.m_eSaleProductKinds, this.OnPurchaseProduct);
#endif			// #if PURCHASE_MODULE_ENABLE
	}
	#endregion			// 함수

	#region 조건부 함수
#if PURCHASE_MODULE_ENABLE
	/** 상품이 결제 되었을 경우 */
	private void OnPurchaseProduct(CPurchaseManager a_oSender, string a_oProductID, bool a_bIsSuccess) {
		// 결제 되었을 경우
		if(a_bIsSuccess) {
			// Do Something
		}

		this.UpdateUIsState();
		m_stParams.m_oCallbackDict?.GetValueOrDefault(ECallback.PURCHASE)?.Invoke(a_oSender, a_oProductID, a_bIsSuccess);
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 함수
}
#endif			// #if EXTRA_SCRIPT_ENABLE && RUNTIME_TEMPLATES_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY