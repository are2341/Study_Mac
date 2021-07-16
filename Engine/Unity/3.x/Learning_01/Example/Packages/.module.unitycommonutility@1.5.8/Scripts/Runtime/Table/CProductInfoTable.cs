using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;

//! 상품 정보
[System.Serializable]
public struct STProductInfo {
	public string m_oID;
	public ProductType m_eProductType;
}
#endif			// #if PURCHASE_MODULE_ENABLE

//! 상품 정보 테이블
public class CProductInfoTable : CScriptableObj<CProductInfoTable> {
#if PURCHASE_MODULE_ENABLE
	#region 변수
	[Header("Common Product Info")]
	[SerializeField] private List<STProductInfo> m_oCommonProductInfoList = new List<STProductInfo>();

	[Header("iOS Product Info")]
	[SerializeField] private List<STProductInfo> m_oiOSProductInfoList = new List<STProductInfo>();

	[Header("Android Product Info")]
	[SerializeField] private List<STProductInfo> m_oGoogleProductInfoList = new List<STProductInfo>();
	[SerializeField] private List<STProductInfo> m_oOneStoreProductInfoList = new List<STProductInfo>();
	[SerializeField] private List<STProductInfo> m_oGalaxyStoreProductInfoList = new List<STProductInfo>();
	#endregion			// 변수

	#region 프로퍼티
	public List<STProductInfo> ProductInfoList { get; private set; } = new List<STProductInfo>();
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.ProductInfoList.ExAddVals(m_oCommonProductInfoList);

#if UNITY_IOS
		this.ProductInfoList.ExAddVals(m_oiOSProductInfoList);
#elif UNITY_ANDROID
#if ONE_STORE
		this.ProductInfoList.ExAddVals(m_oOneStoreProductInfoList);
#elif GALAXY_STORE
		this.ProductInfoList.ExAddVals(m_oGalaxyStoreProductInfoList);
#else
		this.ProductInfoList.ExAddVals(m_oGoogleProductInfoList);
#endif			// #if ONE_STORE
#endif			// #if UNITY_IOS
	}

	//! 상품 정보 인덱스를 반환한다
	public int GetProductInfoIdx(string a_oID) {
		int nIdx = this.ProductInfoList.ExFindVal((a_stProductInfo) => a_stProductInfo.m_oID.ExIsEquals(a_oID));
		return this.ProductInfoList.ExIsValidIdx(nIdx) ? nIdx : KCDefine.B_IDX_INVALID;
	}

	//! 상품 정보를 반환한다
	public STProductInfo GetProductInfo(int a_nIdx) {
		bool bIsValid = this.TryGetProductInfo(a_nIdx, out STProductInfo stProductInfo);
		CAccess.Assert(bIsValid);

		return stProductInfo;
	}

	//! 상품 정보를 반환한다
	public STProductInfo GetProductInfo(string a_oID) {
		bool bIsValid = this.TryGetProductInfo(a_oID, out STProductInfo stProductInfo);
		CAccess.Assert(bIsValid);

		return stProductInfo;
	}

	//! 상품 정보를 반환한다
	public bool TryGetProductInfo(int a_nIdx, out STProductInfo a_stOutProductInfo) {
		a_stOutProductInfo = this.ProductInfoList.ExIsValidIdx(a_nIdx) ? this.ProductInfoList[a_nIdx] : default(STProductInfo);
		return this.ProductInfoList.ExIsValidIdx(a_nIdx);
	}

	//! 상품 정보를 반환한다
	public bool TryGetProductInfo(string a_oID, out STProductInfo a_stOutProductInfo) {
		int nIdx = this.ProductInfoList.ExFindVal((a_stProductInfo) => a_stProductInfo.m_oID.ExIsEquals(a_oID));
		return this.TryGetProductInfo(nIdx, out a_stOutProductInfo);
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	//! 공용 상품 정보를 변경한다
	public void SetCommonProductInfoList(List<STProductInfo> a_oProductInfoList) {
		m_oCommonProductInfoList = a_oProductInfoList;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
#endif			// #if PURCHASE_MODULE_ENABLE
}
