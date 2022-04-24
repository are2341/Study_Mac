using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

/** 서비스 관리자 - 분석 */
public partial class CServicesManager : CSingleton<CServicesManager> {
	#region 함수
	/** 분석 유저 식별자를 변경한다 */
	public void SetAnalyticsUserID(string a_oID) {
		CFunc.ShowLog($"CServicesManager.SetAnalyticsUserID: {a_oID}", KCDefine.B_LOG_COLOR_PLUGIN);
		CAccess.Assert(a_oID.ExIsValid());
		
#if (UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE
		// 초기화 되었을 경우
		if(this.IsInit) {
			Analytics.SetUserId(a_oID);
		}
#endif			// #if (UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE
	}

	/** 로그를 전송한다 */
	public void SendLog(string a_oName, Dictionary<string, object> a_oDataDict) {
		CFunc.ShowLog($"CServicesManager.SendLog: {a_oName}, {a_oDataDict}", KCDefine.B_LOG_COLOR_PLUGIN);
		CAccess.Assert(a_oName.ExIsValid());

#if ((UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE) && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)
		// 초기화 되었을 경우
		if(this.IsInit) {
			Analytics.CustomEvent(a_oName, a_oDataDict ?? new Dictionary<string, object>());
		}
#endif			// #if ((UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE) && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)
	}
	#endregion			// 함수

	#region 조건부 함수
#if PURCHASE_MODULE_ENABLE
	/** 결제 로그를 전송한다 */
	public void SendPurchaseLog(Product a_oProduct, int a_nNumProducts) {
		CFunc.ShowLog($"CServicesManager.SendPurchaseLog: {a_oProduct}, {a_nNumProducts}", KCDefine.B_LOG_COLOR_PLUGIN);
		CAccess.Assert(a_oProduct != null);
		
#if ((UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE) && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)
		var oDataDict = JsonUtility.FromJson<Dictionary<string, object>>(a_oProduct.receipt);

		// 초기화 되었을 경우
		if(this.IsInit && oDataDict.ExIsValid()) {
#if UNITY_IOS
			string oSignature = string.Empty;
#else
			var oPayload = oDataDict[KCDefine.U_KEY_SERVICES_M_PAYLOAD];
			var oAndroidDataDict = JsonUtility.FromJson<Dictionary<string, object>>(oPayload as string);

			string oSignature = oAndroidDataDict[KCDefine.U_KEY_SERVICES_M_SIGNATURE] as string;
#endif			// #if UNITY_IOS

			Analytics.Transaction(a_oProduct.definition.id, (decimal)a_nNumProducts, a_oProduct.metadata.isoCurrencyCode, a_oProduct.receipt, oSignature);
		}
#endif			// #if ((UNITY_IOS || UNITY_ANDROID) && SERVICES_ANALYTICS_ENABLE) && (ANALYTICS_TEST_ENABLE || STORE_DIST_BUILD)
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 함수
}
