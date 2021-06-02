using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

//! 서비스 관리자 - 분석
public partial class CServicesManager : CSingleton<CServicesManager> {
	#region 함수
	//! 분석 유저 식별자를 변경한다
	public void SetAnalyticsUserID(string a_oID) {
		CAccess.Assert(a_oID.ExIsValid());
		CFunc.ShowLog($"CServicesManager.SetAnalyticsUserID: {a_oID}", KCDefine.B_LOG_COLOR_PLUGIN);
		
#if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
		// 초기화 되었을 경우
		if(this.IsInit) {
			Analytics.SetUserId(a_oID);
		}
#endif			// #if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
	}

	//! 로그를 전송한다
	public void SendLog(string a_oName, Dictionary<string, object> a_oDataList) {
		CAccess.Assert(a_oName.ExIsValid());
		CFunc.ShowLog($"CServicesManager.SendLog: {a_oName}, {a_oDataList}", KCDefine.B_LOG_COLOR_PLUGIN);

#if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
#if ANALYTICS_TEST_ENABLE || (ADHOC_BUILD || STORE_BUILD)
		// 초기화 되었을 경우
		if(this.IsInit) {
			var oDataList = a_oDataList ?? new Dictionary<string, string>();

			oDataList.ExAddVal(KCDefine.L_LOG_KEY_DEVICE_ID, CCommonAppInfoStorage.Inst.AppInfo.DeviceID);
			oDataList.ExAddVal(KCDefine.L_LOG_KEY_PLATFORM, CCommonAppInfoStorage.Inst.Platform);

#if AUTO_LOG_PARAMS_ENABLE
			oDataList.ExAddVal(KCDefine.L_LOG_KEY_USER_TYPE, CCommonUserInfoStorage.Inst.UserInfo.UserType.ToString());
			oDataList.ExAddVal(KCDefine.L_LOG_KEY_LOG_TIME, System.DateTime.UtcNow.ExToLongStr());
			oDataList.ExAddVal(KCDefine.L_LOG_KEY_INSTALL_TIME, CCommonAppInfoStorage.Inst.AppInfo.UTCInstallTime.ExToLongStr());
#endif			// #if AUTO_LOG_PARAMS_ENABLE

			Analytics.CustomEvent(a_oName, oDataList);
		}
#endif			// #if ANALYTICS_TEST_ENABLE || (ADHOC_BUILD || STORE_BUILD)
#endif			// #if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
	}
	#endregion			// 함수

	#region 조건부 함수
#if PURCHASE_MODULE_ENABLE
	//! 결제 로그를 전송한다
	public void SendPurchaseLog(Product a_oProduct, int a_nNumProducts) {
		CAccess.Assert(a_oProduct != null);
		CFunc.ShowLog($"CServicesManager.SendPurchaseLog: {a_oProduct}, {a_nNumProducts}", KCDefine.B_LOG_COLOR_PLUGIN);
		
#if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
#if ANALYTICS_TEST_ENABLE || (ADHOC_BUILD || STORE_BUILD)
		var oDataList = JsonUtility.FromJson<Dictionary<string, object>>(a_oProduct.receipt);

		// 초기화 되었을 경우
		if(this.IsInit && oDataList.ExIsValid()) {
#if UNITY_IOS
			string oSignature = string.Empty;
#else
			var oPayload = oDataList[KCDefine.U_KEY_SERVICES_M_PAYLOAD];
			var oAndroidDataList = JsonUtility.FromJson<Dictionary<string, object>>(oPayload as string);

			string oSignature = oAndroidDataList[KCDefine.U_KEY_SERVICES_M_SIGNATURE] as string;
#endif			// #if UNITY_IOS

			Analytics.Transaction(a_oProduct.definition.id, (decimal)a_nNumProducts, a_oProduct.metadata.isoCurrencyCode, a_oProduct.receipt, oSignature);
		}
#endif			// #if ANALYTICS_TEST_ENABLE || (ADHOC_BUILD || STORE_BUILD)
#endif			// #if SERVICES_ANALYTICS_ENABLE && (UNITY_IOS || UNITY_ANDROID)
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 함수
}
