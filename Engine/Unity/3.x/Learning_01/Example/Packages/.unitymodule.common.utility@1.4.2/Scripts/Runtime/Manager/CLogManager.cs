using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//! 로그 관리자
public class CLogManager : CSingleton<CLogManager> {
	#region 변수
	private System.Text.StringBuilder m_oStrBuilder = new System.Text.StringBuilder();
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		Application.logMessageReceived -= this.OnReceiveLog;
		Application.logMessageReceived += this.OnReceiveLog;

		// 파일이 존재 할 경우
		if(File.Exists(KCDefine.B_DATA_P_LOG)) {
			var oBytes = CFunc.ReadBytes(KCDefine.B_DATA_P_LOG);
			string oStr = System.Text.Encoding.Default.GetString(oBytes, KCDefine.B_VAL_0_INT, oBytes.Length);
				
			m_oStrBuilder.Append(oStr);
		}
	}

	//! 제거 되었을 경우
	public override void OnDestroy() {
		base.OnDestroy();

		// 앱이 실행 중 일 경우
		if(CSceneManager.IsAppRunning) {
			Application.logMessageReceived -= this.OnReceiveLog;
		}
	}

	//! 로그를 수신했을 경우
	private void OnReceiveLog(string a_oCondition, string a_oStackTrace, LogType a_eLogType) {
		// 에러 로그 일 경우
		if(a_eLogType != LogType.Log && a_eLogType != LogType.Warning) {
			m_oStrBuilder.AppendFormat(KCDefine.U_FMT_LOG_MSG, System.DateTime.Now.ToString(), a_eLogType, a_oCondition, a_oStackTrace);

			// 최대 길이를 벗어났을 경우
			if(m_oStrBuilder.Length > KCDefine.U_MAX_LENGTH_LOG) {
				m_oStrBuilder.Remove(KCDefine.B_VAL_0_INT, m_oStrBuilder.Length - KCDefine.U_MAX_LENGTH_LOG);
			}

			CFunc.WriteStr(KCDefine.B_DATA_P_LOG, m_oStrBuilder.ToString());
		}
	}
	#endregion			// 함수
}
