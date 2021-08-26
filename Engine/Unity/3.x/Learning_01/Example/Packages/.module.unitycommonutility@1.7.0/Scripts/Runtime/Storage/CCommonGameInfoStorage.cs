using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

//! 공용 게임 정보
[MessagePackObject]
[System.Serializable]
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
	[IgnoreMember] public bool IsMuteBGSnd {
		get { return m_oIntDict.ExGetVal(CCommonGameInfo.KEY_IS_MUTE_BG_SND, KCDefine.B_VAL_0_INT) != KCDefine.B_VAL_0_INT; } 
		set { m_oIntDict.ExReplaceVal(CCommonGameInfo.KEY_IS_MUTE_BG_SND, value ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT); }
	}

	[IgnoreMember] public bool IsMuteFXSnds {
		get { return m_oIntDict.ExGetVal(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, KCDefine.B_VAL_0_INT) != KCDefine.B_VAL_0_INT; } 
		set { m_oIntDict.ExReplaceVal(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, value ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT); }
	}

	[IgnoreMember] public bool IsDisableVibrate {
		get { return m_oIntDict.ExGetVal(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, KCDefine.B_VAL_0_INT) != KCDefine.B_VAL_0_INT; } 
		set { m_oIntDict.ExReplaceVal(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, value ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT); }
	}

	[IgnoreMember] public bool IsDisableNoti {
		get { return m_oIntDict.ExGetVal(CCommonGameInfo.KEY_IS_DISABLE_NOTI, KCDefine.B_VAL_0_INT) != KCDefine.B_VAL_0_INT; } 
		set { m_oIntDict.ExReplaceVal(CCommonGameInfo.KEY_IS_DISABLE_NOTI, value ? KCDefine.B_VAL_1_INT : KCDefine.B_VAL_0_INT); }
	}

	[IgnoreMember] public float BGSndVolume {
		get { return m_oFltDict.ExGetVal(CCommonGameInfo.KEY_BG_SND_VOLUME, KCDefine.B_VAL_0_FLT); }
		set { m_oFltDict.ExReplaceVal(CCommonGameInfo.KEY_BG_SND_VOLUME, value); }
	}

	[IgnoreMember] public float FXSndsVolume {
		get { return m_oFltDict.ExGetVal(CCommonGameInfo.KEY_FX_SNDS_VOLUME, KCDefine.B_VAL_0_FLT); }
		set { m_oFltDict.ExReplaceVal(CCommonGameInfo.KEY_FX_SNDS_VOLUME, value); }
	}
	#endregion			// 프로퍼티
}	

//! 공용 게임 정보 저장소
public class CCommonGameInfoStorage : CSingleton<CCommonGameInfoStorage> {
	#region 프로퍼티
	public CCommonGameInfo GameInfo { get; private set; } = new CCommonGameInfo() {
		IsDisableVibrate = true,
		IsDisableNoti = true,

		BGSndVolume = KCDefine.B_VAL_1_FLT,
		FXSndsVolume = KCDefine.B_VAL_1_FLT
	};
	#endregion			// 프로퍼티

	#region 함수
	//! 게임 정보를 로드한다
	public CCommonGameInfo LoadGameInfo() {
		return this.LoadGameInfo(KCDefine.U_DATA_P_COMMON_GAME_INFO);
	}

	//! 게임 정보를 로드한다
	public CCommonGameInfo LoadGameInfo(string a_oFilePath) {
		// 파일이 존재 할 경우
		if(File.Exists(a_oFilePath)) {
			this.GameInfo = CFunc.ReadMsgPackObj<CCommonGameInfo>(a_oFilePath);
			CAccess.Assert(this.GameInfo != null);
		}

		return this.GameInfo;
	}

	//! 게임 정보를 저장한다
	public void SaveGameInfo() {
		this.SaveGameInfo(KCDefine.U_DATA_P_COMMON_GAME_INFO);
	}

	//! 게임 정보를 저장한다
	public void SaveGameInfo(string a_oFilePath) {
		CFunc.WriteMsgPackObj(a_oFilePath, this.GameInfo);
	}
	#endregion			// 함수
}