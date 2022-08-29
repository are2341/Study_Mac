﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 사운드 관리자 */
public partial class CSndManager : CSingleton<CSndManager> {
	/** 식별자 */
	private enum EKey {
		NONE,
		IS_MUTE_FX_SNDS,
		IS_IGNORE_FX_SNDS_EFFECTS,
		IS_IGNORE_FX_SNDS_REVERB_ZONES,
		IS_IGNORE_FX_SNDS_LISTENER_EFFECTS,

		FX_SNDS_VOLUME,
		BG_SND_PATH,
		BG_SND,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private Dictionary<EKey, bool> m_oBoolDict = new Dictionary<EKey, bool>();
	private Dictionary<EKey, float> m_oRealDict = new Dictionary<EKey, float>();
	private Dictionary<EKey, string> m_oStrDict = new Dictionary<EKey, string>();

	private Dictionary<string, int> m_oMaxNumFXSndsDict = new Dictionary<string, int>();
	private Dictionary<string, double> m_oMinDelayFXSndsDict = new Dictionary<string, double>();
	private Dictionary<string, System.DateTime> m_oPrevFXSndsPlayTimeDict = new Dictionary<string, System.DateTime>();
	private Dictionary<string, List<CSnd>> m_oFXSndsDictContainer = new Dictionary<string, List<CSnd>>();
	private Dictionary<EKey, CSnd> m_oSndDict = new Dictionary<EKey, CSnd>();
	#endregion			// 변수

	#region 프로퍼티
	public bool IsDisableVibrate { get; set; } = false;

	public bool IsMuteBGSnd { get { return m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsMute; } set { m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsMute = value; } }
	public bool IsMuteFXSnds { get { return m_oBoolDict.GetValueOrDefault(EKey.IS_MUTE_FX_SNDS); } set { m_oBoolDict.ExReplaceVal(EKey.IS_MUTE_FX_SNDS, value); this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.IsMute = value); } }
	public bool IsIgnoreBGSndEffects { get { return m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreEffects; } set { m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreEffects = value; } }
	public bool IsIgnoreFXSndsEffects { get { return m_oBoolDict.GetValueOrDefault(EKey.IS_IGNORE_FX_SNDS_EFFECTS); } set { m_oBoolDict.ExReplaceVal(EKey.IS_IGNORE_FX_SNDS_EFFECTS, value); this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.IsIgnoreEffects = value); } }
	public bool IsIgnoreBGSndReverbZones { get { return m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreReverbZones; } set { m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreReverbZones = value; } }
	public bool IsIgnoreFXSndsReverbZones { get { return m_oBoolDict.GetValueOrDefault(EKey.IS_IGNORE_FX_SNDS_REVERB_ZONES); } set { m_oBoolDict.ExReplaceVal(EKey.IS_IGNORE_FX_SNDS_REVERB_ZONES, value); this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.IsIgnoreReverbZones = value); } }
	public bool IsIgnoreBGSndListenerEffects { get { return m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreListenerEffects; } set { m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreListenerEffects = value; } }
	public bool IsIgnoreFXSndsListenerEffects { get { return m_oBoolDict.GetValueOrDefault(EKey.IS_IGNORE_FX_SNDS_LISTENER_EFFECTS); } set { m_oBoolDict.ExReplaceVal(EKey.IS_IGNORE_FX_SNDS_LISTENER_EFFECTS, value); this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.IsIgnoreListenerEffects = value); } }
	public float BGSndVolume { get {  return m_oSndDict.GetValueOrDefault(EKey.BG_SND).Volume; } set { m_oSndDict.GetValueOrDefault(EKey.BG_SND).Volume = Mathf.Clamp01(value); } }
	public float FXSndsVolume { get { return m_oRealDict.GetValueOrDefault(EKey.FX_SNDS_VOLUME); } set { m_oRealDict.ExReplaceVal(EKey.FX_SNDS_VOLUME, Mathf.Clamp01(value)); this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.Volume = Mathf.Clamp01(value)); } }

	public bool IsPlayingBGSnd => m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsPlaying;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 사운드를 설정한다
		CFunc.SetupComponents(new List<(EKey, string, GameObject, GameObject)>() {
			(EKey.BG_SND, $"{EKey.BG_SND}", this.gameObject, CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_BG_SND))
		}, m_oSndDict, false);
	}

	/** 최대 효과음 개수를 변경한다 */
	public void SetMaxNumFXSnds(string a_oKey, int a_nNumFXSnds, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oKey.ExIsValid());
		m_oMaxNumFXSndsDict.ExReplaceVal(a_oKey, Mathf.Clamp(a_nNumFXSnds, KCDefine.B_VAL_0_INT, KCDefine.U_MAX_NUM_FX_SNDS));
	}

	/** 최소 효과음 지연을 변경한다 */
	public void SetMinDelayFXSnds(string a_oKey, float a_fDelay, bool a_bIsIgnoreMinDelay = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oKey.ExIsValid());
		m_oMinDelayFXSndsDict.ExReplaceVal(a_oKey, Mathf.Clamp(a_fDelay, a_bIsIgnoreMinDelay ? KCDefine.B_VAL_0_REAL : KCDefine.U_MIN_DELAY_FX_SNDS, float.MaxValue));
	}
	
	/** 진동을 시작한다 */
	public void Vibrate() {
#if UNITY_IOS || UNITY_ANDROID
		// 진동이 가능 할 경우
		if(!this.IsDisableVibrate && SystemInfo.supportsVibration) {
			Handheld.Vibrate();
		}
#endif			// #if UNITY_IOS || UNITY_ANDROID
	}

	/** 진동을 시작한다 */
	public void Vibrate(float a_fDuration, EVibrateType a_eType = EVibrateType.IMPACT, EVibrateStyle a_eStyle = EVibrateStyle.LIGHT, float a_fIntensity = KCDefine.U_INTENSITY_VIBRATE, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_eType.ExIsValid() && a_eStyle.ExIsValid()));

		// 진동이 가능 할 경우
		if(a_eType.ExIsValid() && a_eStyle.ExIsValid()) {
#if UNITY_IOS || UNITY_ANDROID
			// 진동이 가능 할 경우
			if(!this.IsDisableVibrate && SystemInfo.supportsVibration) {
				// 햅틱 피드백을 지원 할 경우
				if(!CAccess.IsSupportsHapticFeedback) {
					this.Vibrate();
				} else {
					CUnityMsgSender.Inst.SendVibrateMsg(a_eType, a_eStyle, Mathf.Clamp01(a_fDuration), Mathf.Clamp01(a_fIntensity));
				}
			}
#endif			// #if UNITY_IOS || UNITY_ANDROID
		}
	}

	/** 배경음을 재생한다 */
	public CSnd PlayBGSnd(string a_oSndPath, float a_fVolume = KCDefine.B_VAL_0_REAL, bool a_bIsLoop = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSndPath.ExIsValid());

		try {
			return this.PlayBGSnd(a_oSndPath, CSceneManager.ActiveSceneMainCamera.transform.position, a_fVolume, a_bIsLoop, a_bIsEnableAssert);
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CSndManager.PlayBGSnd Exception: {oException.Message}");
		}

		return null;
	}

	/** 배경음을 재생한다 */
	public CSnd PlayBGSnd(string a_oSndPath, Vector3 a_stPos, float a_fVolume = KCDefine.B_VAL_0_REAL, bool a_bIsLoop = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSndPath.ExIsValid());
		
		try {
			// 배경음 재생이 가능 할 경우
			if(!m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsPlaying || !m_oStrDict.GetValueOrDefault(EKey.BG_SND_PATH, string.Empty).Equals(a_oSndPath)) {
				m_oStrDict.ExReplaceVal(EKey.BG_SND_PATH, a_oSndPath);

				m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsMute = this.IsMuteBGSnd;
				m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreEffects = this.IsIgnoreBGSndEffects;
				m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreReverbZones = this.IsIgnoreBGSndReverbZones;
				m_oSndDict.GetValueOrDefault(EKey.BG_SND).IsIgnoreListenerEffects = this.IsIgnoreBGSndListenerEffects;

				m_oSndDict.GetValueOrDefault(EKey.BG_SND).Volume = a_fVolume.ExIsLessEquals(KCDefine.B_VAL_0_REAL) ? this.BGSndVolume : Mathf.Clamp01(a_fVolume);
				m_oSndDict.GetValueOrDefault(EKey.BG_SND).transform.position = a_stPos;

				m_oSndDict.GetValueOrDefault(EKey.BG_SND).PlaySnd(CResManager.Inst.GetRes<AudioClip>(a_oSndPath), a_bIsLoop, false);
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CSndManager.PlayBGSnd Exception: {oException.Message}");
		}

		return m_oSndDict.GetValueOrDefault(EKey.BG_SND);
	}

	/** 효과음을 재생한다 */
	public CSnd PlayFXSnds(string a_oSndPath, float a_fVolume = KCDefine.B_VAL_0_REAL, bool a_bIsLoop = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSndPath.ExIsValid());

		try {
			return this.PlayFXSnds(a_oSndPath, CSceneManager.ActiveSceneMainCamera.transform.position, a_fVolume, a_bIsLoop, a_bIsEnableAssert);
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CSndManager.PlayFXSnds Exception: {oException.Message}");
		}

		return null;
	}

	/** 효과음을 재생한다 */
	public CSnd PlayFXSnds(string a_oSndPath, Vector3 a_stPos, float a_fVolume = KCDefine.B_VAL_0_REAL, bool a_bIsLoop = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSndPath.ExIsValid());

		var oSnd = this.FindPlayableFXSnds(a_oSndPath);
		var stPrevPlayTime = m_oPrevFXSndsPlayTimeDict.GetValueOrDefault(a_oSndPath, System.DateTime.Today);
		double dblMinDelayFXSnds = m_oMinDelayFXSndsDict.GetValueOrDefault(a_oSndPath, KCDefine.U_MIN_DELAY_FX_SNDS);

		try {
			// 효과음 재생이 가능 할 경우
			if(oSnd != null && CSceneManager.IsExistsMainCamera && System.DateTime.Now.ExGetDeltaTime(stPrevPlayTime).ExIsGreateEquals(dblMinDelayFXSnds)) {
				oSnd.IsMute = this.IsMuteFXSnds;
				oSnd.IsIgnoreEffects = this.IsIgnoreFXSndsEffects;
				oSnd.IsIgnoreReverbZones = this.IsIgnoreFXSndsReverbZones;
				oSnd.IsIgnoreListenerEffects = this.IsIgnoreFXSndsListenerEffects;

				oSnd.Volume = a_fVolume.ExIsLessEquals(KCDefine.B_VAL_0_REAL) ? this.FXSndsVolume : Mathf.Clamp01(a_fVolume);
				oSnd.transform.position = a_stPos;

				m_oPrevFXSndsPlayTimeDict.ExReplaceVal(a_oSndPath, System.DateTime.Now);
				oSnd.PlaySnd(CResManager.Inst.GetRes<AudioClip>(a_oSndPath), a_bIsLoop, !CSceneManager.ActiveSceneMainCamera.transform.position.ExIsEquals(a_stPos));
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CSndManager.PlayFXSnds Exception: {oException.Message}");
		}

		return oSnd;
	}

	/** 배경음을 재개한다 */
	public void ResumeBGSnd() {
		m_oSndDict.GetValueOrDefault(EKey.BG_SND).ResumeSnd();
	}

	/** 효과음을 재개한다 */
	public void ResumeFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.ResumeSnd());
	}

	/** 배경음을 정지한다 */
	public void PauseBGSnd() {
		m_oSndDict.GetValueOrDefault(EKey.BG_SND).PauseSnd();
	}

	/** 효과음을 정지한다 */
	public void PauseFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.PauseSnd());
	}

	/** 배경음을 중지한다 */
	public void StopBGSnd() {
		m_oSndDict.GetValueOrDefault(EKey.BG_SND).StopSnd();
	}

	/** 효과음을 중지한다 */
	public void StopFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.StopSnd());
	}

	/** 재생 가능한 효과음을 탐색한다 */
	private CSnd FindPlayableFXSnds(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());

		var oFXSndsList = m_oFXSndsDictContainer.GetValueOrDefault(a_oKey) ?? new List<CSnd>();
		m_oFXSndsDictContainer.TryAdd(a_oKey, oFXSndsList);

		// 최대 중첩 개수를 벗어났을 경우
		if(oFXSndsList.Count >= m_oMaxNumFXSndsDict.GetValueOrDefault(a_oKey, KCDefine.U_MAX_NUM_FX_SNDS)) {
			for(int i = 0; i < oFXSndsList.Count; ++i) {
				// 효과음 재생이 가능 할 경우
				if(!oFXSndsList[i].IsPlaying) {
					return oFXSndsList[i];
				}
			}

			return null;
		}
		
		oFXSndsList.ExAddVal(CFactory.CreateCloneObj<CSnd>(KCDefine.U_OBJ_N_SND_M_FX_SNDS, CResManager.Inst.GetRes<GameObject>(KCDefine.U_OBJ_P_G_FX_SNDS), this.gameObject));
		return oFXSndsList.Last();
	}

	/** 효과음을 순회한다 */
	private void EnumerateFXSnds(System.Action<string, CSnd> a_oCallback) {
		foreach(var stKeyVal in m_oFXSndsDictContainer) {
			for(int i = 0; i < stKeyVal.Value.Count; ++i) {
				a_oCallback?.Invoke(stKeyVal.Key, stKeyVal.Value[i]);
			}
		}
	}
	#endregion			// 함수
}