using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/** 사운드 */
public partial class CSnd : CComponent {
	#region 변수
	private AudioSource m_oAudioSrc = null;
	#endregion			// 변수

	#region 프로퍼티
	public bool IsMute { get { return m_oAudioSrc.mute; } set { m_oAudioSrc.mute = value; } }
	public bool IsIgnoreEffects { get { return m_oAudioSrc.bypassEffects; } set { m_oAudioSrc.bypassEffects = value; } }
	public bool IsIgnoreReverbZones { get { return m_oAudioSrc.bypassReverbZones; } set { m_oAudioSrc.bypassReverbZones = value; } }
	public bool IsIgnoreListenerEffects { get { return m_oAudioSrc.bypassListenerEffects; } set { m_oAudioSrc.bypassListenerEffects = value; } }
	public float Volume { get { return m_oAudioSrc.volume; } set { m_oAudioSrc.volume = value; } }

	public bool IsPlaying => m_oAudioSrc.isPlaying;
	#endregion			// 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oAudioSrc = this.GetComponentInChildren<AudioSource>();
		m_oAudioSrc.playOnAwake = false;
	}

	/** 사운드를 재생한다 */
	public void PlaySnd(AudioClip a_oAudioClip, bool a_bIsLoop, bool a_bIs3DSnd) {
		m_oAudioSrc.pitch = KCDefine.B_VAL_1_REAL;
		m_oAudioSrc.panStereo = KCDefine.B_VAL_0_REAL;
		
		m_oAudioSrc.ExPlay(a_oAudioClip, a_bIsLoop, a_bIs3DSnd, false);
	}

	/** 사운드를 재개한다 */
	public void ResumeSnd() {
		m_oAudioSrc.UnPause();
	}

	/** 사운드를 정지한다 */
	public void PauseSnd() {
		m_oAudioSrc.Pause();
	}

	/** 사운드를 중지한다 */
	public void StopSnd() {
		m_oAudioSrc.Stop();
	}
	#endregion			// 함수
}
