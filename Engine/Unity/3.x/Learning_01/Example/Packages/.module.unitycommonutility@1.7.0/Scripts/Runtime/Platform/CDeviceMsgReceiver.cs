using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 디바이스 메세지 수신자
public class CDeviceMsgReceiver : CSingleton<CDeviceMsgReceiver> {
	#region 변수
	private Dictionary<string, System.Action<string, string>> m_oCallbackDict = new Dictionary<string, System.Action<string, string>>();
	#endregion			// 변수

	#region 함수
	//! 콜백을 추가한다
	public void AddCallback(string a_oKey, System.Action<string, string> a_oCallback) {
		m_oCallbackDict.ExAddVal(a_oKey, a_oCallback);
	}

	//! 콜백을 제거한다
	public void RemoveCallback(string a_oKey) {
		m_oCallbackDict.ExRemoveVal(a_oKey);
	}

	//! 디바이스 메세지를 처리한다
	private void HandleDeviceMsg(string a_oMsg) {
		var oJSONNode = SimpleJSON.JSON.Parse(a_oMsg) as SimpleJSON.JSONClass;
		
		string oCmd = oJSONNode[KCDefine.U_KEY_DEVICE_CMD];
		string oMsg = oJSONNode[KCDefine.U_KEY_DEVICE_MSG];
		string oKey = string.Format(KCDefine.U_KEY_FMT_DEVICE_MR_HANDLE_MSG_CALLBACK, oCmd);

		CScheduleManager.Inst.AddCallback(oKey, () => {
			CFunc.ShowLog($"CDeviceMsgReceiver.HandleDeviceMsg: {a_oMsg}", KCDefine.B_LOG_COLOR_PLUGIN);

			// 커맨드가 존재 할 경우
			if(m_oCallbackDict.ContainsKey(oCmd)) {
				var oCallback = m_oCallbackDict[oCmd];
				
				this.RemoveCallback(oCmd);
				oCallback?.Invoke(oCmd, oMsg);
			}
		});
	}
	#endregion			// 함수
}
