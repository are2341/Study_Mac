using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if NEVER_USE_THIS
//! 판매 상품 정보
[System.Serializable]
public struct STSaleProductInfo {
	public string m_oName;
	public string m_oDesc;

	public ESaleProductType m_eSaleProductType;
	public ESaleProductKinds m_eSaleProductKinds;

	public EPriceType m_ePriceType;
	public EPriceKinds m_ePriceKinds;

	public List<STItemInfo> m_oItemInfoList;

	#region 함수
	//! 생성자
	public STSaleProductInfo(SimpleJSON.JSONNode a_oSaleProductInfo) {
		m_oName = a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_NAME];
		m_oDesc = a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_DESC];
		
		m_eSaleProductType = (ESaleProductType)a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_SALE_PRODUCT_TYPE].AsInt;
		m_eSaleProductKinds = (ESaleProductKinds)a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_SALE_PRODUCT_KINDS].AsInt;

		m_ePriceType = (EPriceType)a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_PRICE_TYPE].AsInt;
		m_ePriceKinds = (EPriceKinds)a_oSaleProductInfo[KDefine.G_KEY_SALE_PIT_PRICE_KINDS].AsInt;

		m_oItemInfoList = new List<STItemInfo>();

		for(int i = 0; i < KDefine.G_MAX_NUM_SALE_PIT_ITEM_INFOS; ++i) {
			string oNumItemsKey = string.Format(KDefine.G_KEY_FMT_SALE_PIT_NUM_ITEMS, i + KCDefine.B_VAL_1_INT);
			string oItemKindsKey = string.Format(KDefine.G_KEY_FMT_SALE_PIT_ITEM_KINDS, i + KCDefine.B_VAL_1_INT);

			var stItemInfo = new STItemInfo() {
				m_nNumItems = a_oSaleProductInfo[oNumItemsKey].AsInt,
				m_eItemKinds = (EItemKinds)a_oSaleProductInfo[oItemKindsKey].AsInt
			};

			m_oItemInfoList.Add(stItemInfo);
		}
	}
	#endregion			// 함수
}

//! 판매 상품 정보 테이블
public class CSaleProductInfoTable : CScriptableObj<CSaleProductInfoTable> {
	#region 변수
	[Header("Sale Product Info")]
	[SerializeField] private List<STSaleProductInfo> m_oSaleProductInfoList = new List<STSaleProductInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public Dictionary<ESaleProductKinds, STSaleProductInfo> SaleProductInfoList { get; set; } = new Dictionary<ESaleProductKinds, STSaleProductInfo>();
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.SetupSaleProductInfos(m_oSaleProductInfoList, this.SaleProductInfoList);
	}

	//! 아이템 정보 포함 여부를 검사한다
	public bool IsContainsItemInfo(ESaleProductKinds a_eSaleProductKinds, EItemKinds a_eItemKinds) {
		return this.TryGetItemInfo(a_eSaleProductKinds, a_eItemKinds, out STItemInfo stItemInfo);
	}
	
	//! 판매 코인 개수를 반환한다
	public int GetNumSaleCoins(ESaleProductKinds a_eSaleProductKinds) {
		bool bIsValid = this.TryGetItemInfo(a_eSaleProductKinds, EItemKinds.GOODS_COIN, out STItemInfo stItemInfo);
		return bIsValid ? stItemInfo.m_nNumItems : KCDefine.B_VAL_0_INT;
	}

	//! 판매 상품 정보를 반환한다
	public STSaleProductInfo GetSaleProductInfo(ESaleProductKinds a_eSaleProductKinds) {
		bool bIsValid = this.TryGetSaleProductInfo(a_eSaleProductKinds, out STSaleProductInfo stSaleProductInfo);
		CAccess.Assert(bIsValid);

		return stSaleProductInfo;
	}

	//! 아이템 정보를 반환한다
	public STItemInfo GetItemInfo(ESaleProductKinds a_eSaleProductKinds, EItemKinds a_eItemKinds) {
		bool bIsValid = this.TryGetItemInfo(a_eSaleProductKinds, a_eItemKinds, out STItemInfo stItemInfo);
		CAccess.Assert(bIsValid);

		return stItemInfo;
	}

	//! 판매 상품 정보를 반환한다
	public bool TryGetSaleProductInfo(ESaleProductKinds a_eSaleProductKinds, out STSaleProductInfo a_stOutSaleProductInfo) {
		a_stOutSaleProductInfo = this.SaleProductInfoList.ExGetVal(a_eSaleProductKinds, KDefine.G_INVALID_SALE_PRODUCT_INFO);
		return this.SaleProductInfoList.ContainsKey(a_eSaleProductKinds);
	}

	//! 아이템 정보를 반환한다
	public bool TryGetItemInfo(ESaleProductKinds a_eSaleProductKinds, EItemKinds a_eItemKinds, out STItemInfo a_stOutItemInfo) {
		// 판매 상품 정보가 존재 할 경우
		if(this.TryGetSaleProductInfo(a_eSaleProductKinds, out STSaleProductInfo stSaleProductInfo)) {
			int nIdx = stSaleProductInfo.m_oItemInfoList.ExFindVal((a_stItemInfo) => a_stItemInfo.m_eItemKinds == a_eItemKinds);
			a_stOutItemInfo = stSaleProductInfo.m_oItemInfoList.ExIsValidIdx(nIdx) ? stSaleProductInfo.m_oItemInfoList[nIdx] : KDefine.G_INVALID_ITEM_INFO;

			return stSaleProductInfo.m_oItemInfoList.ExIsValidIdx(nIdx);
		}

		a_stOutItemInfo = KDefine.G_INVALID_ITEM_INFO;
		return false;
	}

	//! 판매 상품 정보를 로드한다
	public Dictionary<ESaleProductKinds, STSaleProductInfo> LoadSaleProductInfos(string a_oJSONStr) {
		CAccess.Assert(a_oJSONStr.ExIsValid());

		var oJSONNode = SimpleJSON.JSONNode.Parse(a_oJSONStr);
		var oSaleProductInfos = oJSONNode[KCDefine.B_KEY_JSON_COMMON_DATA];

		for(int i = 0; i < oSaleProductInfos.Count; ++i) {
			var stSaleProductInfo = new STSaleProductInfo(oSaleProductInfos[i]);
			bool bIsReplace = oSaleProductInfos[i][KCDefine.U_KEY_REPLACE].AsInt != KCDefine.B_VAL_0_INT;

			// 판매 상품 정보가 추가 가능 할 경우
			if(bIsReplace || !this.SaleProductInfoList.ContainsKey(stSaleProductInfo.m_eSaleProductKinds)) {
				this.SaleProductInfoList.ExReplaceVal(stSaleProductInfo.m_eSaleProductKinds, stSaleProductInfo);
			}
		}

#if UNITY_EDITOR
		this.SetupSaleProductInfoList(this.SaleProductInfoList, m_oSaleProductInfoList);
#endif			// #if UNITY_EDITOR

		return this.SaleProductInfoList;
	}

	//! 판매 상품 정보를 로드한다
	public Dictionary<ESaleProductKinds, STSaleProductInfo> LoadSaleProductInfosFromFile(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		string oJSONStr = CFunc.ReadStr(a_oFilePath);

		return this.LoadSaleProductInfos(oJSONStr);
	}

	//! 판매 상품 정보를 로드한다
	public Dictionary<ESaleProductKinds, STSaleProductInfo> LoadSaleProductInfosFromRes(string a_oFilePath) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		
		try {
			var oTextAsset = CResManager.Inst.GetRes<TextAsset>(a_oFilePath);
			return this.LoadSaleProductInfos(oTextAsset.text);
		} finally {
			CResManager.Inst.RemoveRes<TextAsset>(a_oFilePath, true);
		}
	}

	//! 판매 상품 정보를 설정한다
	private void SetupSaleProductInfos(List<STSaleProductInfo> a_oSaleProductInfoList, Dictionary<ESaleProductKinds, STSaleProductInfo> a_oOutSaleProductInfoList) {
		CAccess.Assert(a_oSaleProductInfoList != null && a_oOutSaleProductInfoList != null);

		for(int i = 0; i < a_oSaleProductInfoList.Count; ++i) {
			var stSaleProductInfo = a_oSaleProductInfoList[i];
			a_oOutSaleProductInfoList.ExAddVal(stSaleProductInfo.m_eSaleProductKinds, stSaleProductInfo);
		}
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	// 판매 상품 정보를 설정한다
	private void SetupSaleProductInfoList(Dictionary<ESaleProductKinds, STSaleProductInfo> a_oSaleProductInfoList, List<STSaleProductInfo> a_oOutSaleProductInfoList) {
		CAccess.Assert(a_oSaleProductInfoList != null && a_oOutSaleProductInfoList != null);
		a_oOutSaleProductInfoList.Clear();

		foreach(var stKeyVal in a_oSaleProductInfoList) {
			a_oOutSaleProductInfoList.ExAddVal(stKeyVal.Value);
		}

		a_oOutSaleProductInfoList.Sort((a_stLhs, a_stRhs) => (int)a_stLhs.m_eSaleProductKinds - (int)a_stRhs.m_eSaleProductKinds);
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
#endif			// #if NEVER_USE_THIS
