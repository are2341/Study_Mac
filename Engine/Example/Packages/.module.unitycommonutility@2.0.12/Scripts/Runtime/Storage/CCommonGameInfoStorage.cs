using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

#if NEWTON_SOFT_JSON_MODULE_ENABLE
using Newtonsoft.Json;

/** 공용 게임 정보 */
[MessagePackObject][System.Serializable]
public class CCommonGameInfo : CCommonBaseInfo {
	#region 상수
	private const string KEY_IS_MUTE_BG_SND = "IsMuteBGSnd";
	private const string KEY_IS_MUTE_FX_SNDS = "IsMuteFXSnds";
	private const string KEY_IS_DISABLE_VIBRATE = "IsDisableVibrate";
	private const string KEY_IS_DISABLE_NOTI = "IsDisableNoti";

	private const string KEY_BG_SND_VOLUME = "BGSndVolume";
	private const string KEY_FX_SNDS_VOLUME = "FXSndsVolume";
	#endregion			// 상수

	#region 프로퍼티
	[JsonIgnore][IgnoreMember] public bool IsMuteBGSnd {
		get { return bool.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_IS_MUTE_BG_SND, KCDefine.B_TEXT_FALSE)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_IS_MUTE_BG_SND, $"{value}"); }
	}

	[JsonIgnore][IgnoreMember] public bool IsMuteFXSnds {
		get { return bool.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, KCDefine.B_TEXT_FALSE)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, $"{value}"); }
	}

	[JsonIgnore][IgnoreMember] public bool IsDisableVibrate {
		get { return bool.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, KCDefine.B_TEXT_FALSE)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, $"{value}"); }
	}

	[JsonIgnore][IgnoreMember] public bool IsDisableNoti {
		get { return bool.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_IS_DISABLE_NOTI, KCDefine.B_TEXT_FALSE)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_IS_DISABLE_NOTI, $"{value}"); }
	}

	[JsonIgnore][IgnoreMember] public float BGSndVolume {
		get { return float.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_BG_SND_VOLUME, KCDefine.B_STR_0_FLT)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_BG_SND_VOLUME, $"{value}"); }
	}

	[JsonIgnore][IgnoreMember] public float FXSndsVolume {
		get { return float.Parse(m_oStrDict.GetValueOrDefault(CCommonGameInfo.KEY_FX_SNDS_VOLUME, KCDefine.B_STR_0_FLT)); }
		set { m_oStrDict.ExReplaceVal(CCommonGameInfo.KEY_FX_SNDS_VOLUME, $"{value}"); }
	}
	#endregion			// 프로퍼티

	#region IMessagePackSerializationCallbackReceiver
	/** 직렬화 될 경우 */
	public override void OnBeforeSerialize() {
		base.OnBeforeSerialize();
	}
	
	/** 역직렬화 되었을 경우 */
	public override void OnAfterDeserialize() {
		base.OnAfterDeserialize();

		// 버전이 다를 경우
		if(this.Ver.CompareTo(KCDefine.U_VER_COMMON_GAME_INFO) < KCDefine.B_COMPARE_EQUALS) {
			// Do Something
		}
	}
	#endregion			// IMessagePackSerializationCallbackReceiver

	#region 함수
	/** 생성자 */
	public CCommonGameInfo() : base(KCDefine.U_VER_COMMON_GAME_INFO) {
		// Do Something
	}
	#endregion			// 함수
}	

/** 공용 게임 정보 저장소 */
public class CCommonGameInfoStorage : CSingleton<CCommonGameInfoStorage> {
	#region 프로퍼티
	public CCommonGameInfo GameInfo { get; private set; } = new CCommonGameInfo() {
		IsDisableVibrate = true, IsDisableNoti = true, BGSndVolume = KCDefine.B_VAL_1_FLT, FXSndsVolume = KCDefine.B_VAL_1_FLT
	};
	#endregion			// 프로퍼티

	#region 함수
	/** 게임 정보를 로드한다 */
	public CCommonGameInfo LoadGameInfo() {
		return this.LoadGameInfo(KCDefine.U_DATA_P_COMMON_GAME_INFO);
	}

	/** 게임 정보를 로드한다 */
	public CCommonGameInfo LoadGameInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
#if MSG_PACK_ENABLE
			this.GameInfo = CFunc.ReadMsgPackObj<CCommonGameInfo>(a_oFilePath);
#else
			this.GameInfo = CFunc.ReadJSONObj<CCommonGameInfo>(a_oFilePath);
#endif			// #if MSG_PACK_ENABLE

			CAccess.Assert(this.GameInfo != null);
		}

		return this.GameInfo;
	}

	/** 게임 정보를 저장한다 */
	public void SaveGameInfo() {
		this.SaveGameInfo(KCDefine.U_DATA_P_COMMON_GAME_INFO);
	}

	/** 게임 정보를 저장한다 */
	public void SaveGameInfo(string a_oFilePath) {
#if MSG_PACK_ENABLE
		CFunc.WriteMsgPackObj(a_oFilePath, this.GameInfo);
#else
		CFunc.WriteJSONObj(a_oFilePath, this.GameInfo);
#endif			// #if MSG_PACK_ENABLE
	}
	#endregion			// 함수
}
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE