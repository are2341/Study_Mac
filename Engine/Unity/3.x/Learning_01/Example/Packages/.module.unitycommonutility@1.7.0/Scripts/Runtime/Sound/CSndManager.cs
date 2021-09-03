using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 사운드 관리자
public class CSndManager : CSingleton<CSndManager> {
	#region 변수
	private bool m_bIsMuteFXSnds = false;
	private float m_fFXSndsVolume = 0.0f;

	private CSnd m_oBGSnd = null;
	private string m_oBGSndPath = string.Empty;

	private Dictionary<string, int> m_oMaxNumDuplicateFXSndsDict = new Dictionary<string, int>();
	private Dictionary<string, List<CSnd>> m_oFXSndDictContainer = new Dictionary<string, List<CSnd>>();
	#endregion			// 변수

	#region 프로퍼티
	public bool IsDisableVibrate { get; set; } = false;

	public bool IsMuteBGSnd {
		get { return m_oBGSnd.IsMute; } 
		set { m_oBGSnd.IsMute = value; }
	}

	public bool IsMuteFXSnds {
		get { return m_bIsMuteFXSnds; } 
		set { m_bIsMuteFXSnds = value; this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.IsMute = value); }
	}

	public float BGSndVolume {
		get {  return m_oBGSnd.Volume; } 
		set { m_oBGSnd.Volume = value; }
	}

	public float FXSndsVolume {
		get { return m_fFXSndsVolume; } 
		set { m_fFXSndsVolume = value; this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.Volume = value); }
	}

	public bool IsPlayingBGSnd => m_oBGSnd.IsPlaying;
	#endregion			// 프로퍼티

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();

		m_oBGSnd = CFactory.CreateCloneObj<CSnd>(KCDefine.U_OBJ_N_SND_M_BG_SND, KCDefine.U_OBJ_P_G_BG_SND, this.gameObject);
		m_oBGSnd.transform.localPosition = Vector3.zero;
	}

	//! 최대 중첩 효과음 개수를 변경한다
	public void SetMaxNumDuplicateFXSnds(string a_oKey, int a_nNumDuplicateFXSnds) {
		CAccess.Assert(a_oKey.ExIsValid());
		m_oMaxNumDuplicateFXSndsDict.ExReplaceVal(a_oKey, a_nNumDuplicateFXSnds);
	}
	
	//! 진동을 시작한다
	public void Vibrate() {
#if UNITY_IOS || UNITY_ANDROID
		// 진동이 가능 할 경우
		if(!this.IsDisableVibrate && SystemInfo.supportsVibration) {
			Handheld.Vibrate();
		}
#endif			// #if UNITY_IOS || UNITY_ANDROID
	}

	//! 진동을 시작한다
	public void Vibrate(float a_fDuration, EVibrateType a_eType = EVibrateType.IMPACT, EVibrateStyle a_eStyle = EVibrateStyle.LIGHT, float a_fIntensity = KCDefine.U_INTENSITY_VIBRATE) {
#if (UNITY_IOS || UNITY_ANDROID) && HAPTIC_FEEDBACK_ENABLE
		// 진동이 가능 할 경우
		if(!this.IsDisableVibrate && SystemInfo.supportsVibration) {
			// 햅틱 피드백을 지원 할 경우
			if(!CAccess.IsSupportsHapticFeedback) {
				this.Vibrate();
			} else {
				float fDuration = Mathf.Clamp01(a_fDuration);
				float fIntensity = Mathf.Clamp01(a_fIntensity);

				CUnityMsgSender.Inst.SendVibrateMsg(a_eType, a_eStyle, fDuration, fIntensity);
			}
		}
#endif			// #if (UNITY_IOS || UNITY_ANDROID) && HAPTIC_FEEDBACK_ENABLE
	}

	//! 단일음을 재생힌다
	public void PlayOneShotSnd(string a_oFilePath, Vector3 a_stPos, float a_fVolume = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oFilePath.ExIsValid());

		// 효과음 재생이 가능 할 경우
		if(!this.IsMuteFXSnds) {
			float fVolume = a_fVolume.ExIsEquals(KCDefine.B_VAL_0_FLT) ? this.FXSndsVolume : a_fVolume;
			AudioSource.PlayClipAtPoint(CResManager.Inst.GetRes<AudioClip>(a_oFilePath), a_stPos, fVolume);
		}
	}

	//! 배경음을 재생힌다
	public CSnd PlayBGSnd(string a_oFilePath, float a_fVolume = KCDefine.B_VAL_0_FLT, bool a_bIsLoop = true) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return this.PlayBGSnd(a_oFilePath, CSceneManager.MainCamera.transform.position, a_fVolume, a_bIsLoop);
	}

	//! 배경음을 재생힌다
	public CSnd PlayBGSnd(string a_oFilePath, Vector3 a_stPos, float a_fVolume = KCDefine.B_VAL_0_FLT, bool a_bIsLoop = true) {
		CAccess.Assert(a_oFilePath.ExIsValid());

		// 배경음 재생이 가능 할 경우
		if(!m_oBGSnd.IsPlaying || !m_oBGSndPath.Equals(a_oFilePath)) {
			m_oBGSndPath = a_oFilePath;

			m_oBGSnd.IsMute = this.IsMuteBGSnd;
			m_oBGSnd.Volume = a_fVolume.ExIsEquals(KCDefine.B_VAL_0_FLT) ? this.BGSndVolume : a_fVolume;
			m_oBGSnd.transform.position = a_stPos;

			m_oBGSnd.PlaySnd(CResManager.Inst.GetRes<AudioClip>(a_oFilePath), a_bIsLoop, false);
		}

		return m_oBGSnd;
	}

	//! 효과음을 재생한다
	public CSnd PlayFXSnd(string a_oFilePath, float a_fVolume = KCDefine.B_VAL_0_FLT, bool a_bIsLoop = false) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		return this.PlayFXSnd(a_oFilePath, CSceneManager.MainCamera.transform.position, a_fVolume, a_bIsLoop);
	}

	//! 효과음을 재생한다
	public CSnd PlayFXSnd(string a_oFilePath, Vector3 a_stPos, float a_fVolume = KCDefine.B_VAL_0_FLT, bool a_bIsLoop = false) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		var oSnd = this.FindPlayableFXSnd(a_oFilePath);

		// 사운드가 존재 할 경우
		if(oSnd != null) {
			oSnd.IsMute = this.IsMuteFXSnds;
			oSnd.Volume = a_fVolume.ExIsEquals(KCDefine.B_VAL_0_FLT) ? this.FXSndsVolume : a_fVolume;
			oSnd.transform.position = a_stPos;

			bool bIs3DSnd = !CSceneManager.MainCamera.transform.position.ExIsEquals(a_stPos);
			oSnd.PlaySnd(CResManager.Inst.GetRes<AudioClip>(a_oFilePath), a_bIsLoop, bIs3DSnd);
		}

		return oSnd;
	}

	//! 배경음을 재개한다
	public void ResumeBGSnd() {
		m_oBGSnd.ResumeSnd();
	}

	//! 효과음을 재개한다
	public void ResumeFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.ResumeSnd());
	}

	//! 배경음을 정지한다
	public void PauseBGSnd() {
		m_oBGSnd.PauseSnd();
	}

	//! 효과음을 정지한다
	public void PauseFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.PauseSnd());
	}

	//! 배경음을 중지한다
	public void StopBGSnd() {
		m_oBGSnd.StopSnd();
	}

	//! 효과음을 중지한다
	public void StopFXSnds() {
		this.EnumerateFXSnds((a_oKey, a_oSnd) => a_oSnd.StopSnd());
	}

	//! 재생 가능한 효과음을 탐색한다
	private CSnd FindPlayableFXSnd(string a_oKey) {
		CAccess.Assert(a_oKey.ExIsValid());

		var oFXSndList = m_oFXSndDictContainer.ExGetVal(a_oKey, null);
		int nMaxNumDuplicateFXSnds = m_oMaxNumDuplicateFXSndsDict.ExGetVal(a_oKey, KCDefine.U_MAX_NUM_DUPLICATE_FX_SNDS);
		
		// 효과음이 없을 경우
		if(oFXSndList == null) {
			oFXSndList = new List<CSnd>();
			m_oFXSndDictContainer.ExAddVal(a_oKey, oFXSndList);
		}

		// 최대 중첩 개수를 벗어났을 경우
		if(oFXSndList.Count >= nMaxNumDuplicateFXSnds) {
			for(int i = 0; i < oFXSndList.Count; ++i) {
				// 효과음 재생이 가능 할 경우
				if(!oFXSndList[i].IsPlaying) {
					return oFXSndList[i];
				}
			}

			return null;
		}

		var oSnd = CFactory.CreateCloneObj<CSnd>(KCDefine.U_OBJ_N_SND_M_FX_SND, KCDefine.U_OBJ_P_G_FX_SND, this.gameObject);
		oSnd.transform.localPosition = Vector3.zero;
		
		oFXSndList.ExAddVal(oSnd);
		return oSnd;
	}

	//! 효과음을 순회한다
	private void EnumerateFXSnds(System.Action<string, CSnd> a_oCallback) {
		foreach(var stKeyVal in m_oFXSndDictContainer) {
			for(int i = 0; i < stKeyVal.Value.Count; ++i) {
				a_oCallback?.Invoke(stKeyVal.Key, stKeyVal.Value[i]);
			}
		}
	}
	#endregion			// 함수
}
