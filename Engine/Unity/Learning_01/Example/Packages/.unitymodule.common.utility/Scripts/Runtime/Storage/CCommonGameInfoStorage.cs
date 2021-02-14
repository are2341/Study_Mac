using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MessagePack;

//! 공용 게임 정보
[MessagePackObject]
[System.Serializable]
public class CCommonGameInfo : CCommonBaseInfo {
	#region 상수
	private const string KEY_IS_AGREE = "IsAgree";
	private const string KEY_IS_FIRST_START = "IsFirstStart";
	private const string KEY_IS_COMPLETE_TUTORIAL = "IsCompleteTutorial";

	private const string KEY_IS_MUTE_BG_SND = "IsMuteBGSnd";
	private const string KEY_IS_MUTE_FX_SNDS = "IsMuteFXSnds";
	private const string KEY_IS_DISABLE_VIBRATE = "IsDisableVibrate";
	private const string KEY_IS_DISABLE_NOTI = "IsDisableNoti";

	private const string KEY_BG_SND_VOLUME = "BGSndVolume";
	private const string KEY_FX_SNDS_VOLUME = "FXSndsVolume";
	#endregion			// 상수

	#region 프로퍼티
	[IgnoreMember] public bool IsAgree {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_AGREE, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_AGREE, value); }
	}

	[IgnoreMember] public bool IsFirstStart {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_FIRST_START, false); }
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_FIRST_START, value); }
	}

	[IgnoreMember] public bool IsCompleteTutorial {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_COMPLETE_TUTORIAL, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_COMPLETE_TUTORIAL, value); }
	}

	[IgnoreMember] public bool IsMuteBGSnd {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_MUTE_BG_SND, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_MUTE_BG_SND, value); }
	}

	[IgnoreMember] public bool IsMuteFXSnds {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_MUTE_FX_SNDS, value); }
	}

	[IgnoreMember] public bool IsDisableVibrate {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_DISABLE_VIBRATE, value); }
	}

	[IgnoreMember] public bool IsDisableNoti {
		get { return m_oBoolList.ExGetValue(CCommonGameInfo.KEY_IS_DISABLE_NOTI, false); } 
		set { m_oBoolList.ExReplaceValue(CCommonGameInfo.KEY_IS_DISABLE_NOTI, value); }
	}

	[IgnoreMember] public float BGSndVolume {
		get { return m_oFloatList.ExGetValue(CCommonGameInfo.KEY_BG_SND_VOLUME, 0.0f); }
		set { m_oFloatList.ExReplaceValue(CCommonGameInfo.KEY_BG_SND_VOLUME, value); }
	}

	[IgnoreMember] public float FXSndsVolume {
		get { return m_oFloatList.ExGetValue(CCommonGameInfo.KEY_FX_SNDS_VOLUME, 0.0f); }
		set { m_oFloatList.ExReplaceValue(CCommonGameInfo.KEY_FX_SNDS_VOLUME, value); }
	}
	#endregion			// 프로퍼티

	#region 함수
	//! 생성자
	public CCommonGameInfo() : base(KCDefine.U_VERSION_COMMON_GAME_INFO) {
		// Do Nothing
	}
	#endregion			// 함수
}	

//! 공용 게임 정보 저장소
public class CCommonGameInfoStorage : CSingleton<CCommonGameInfoStorage> {
	#region 프로퍼티
	public CCommonGameInfo GameInfo { get; private set; } = new CCommonGameInfo() {
		IsFirstStart = true,
		IsDisableVibrate = true,
		IsDisableNoti = true,

		BGSndVolume = KCDefine.B_VALUE_FLT_1,
		FXSndsVolume = KCDefine.B_VALUE_FLT_1
	};
	#endregion			// 프로퍼티

	#region 함수
	//! 게임 정보를 저장한다
	public void SaveGameInfo() {
		this.SaveGameInfo(KCDefine.U_DATA_P_COMMON_GAME_INFO);
	}

	//! 게임 정보를 저장한다
	public void SaveGameInfo(string a_oFilePath) {
		CFunc.WriteMsgPackObj(a_oFilePath, this.GameInfo);
	}

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
	#endregion			// 함수
}