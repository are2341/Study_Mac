using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

//! 에디터 옵션
[System.Serializable]
public struct STEditorOpts {
	public bool m_bIsAsyncShaderCompile;
	public bool m_bIsBurstCompileEnable;
	public bool m_bIsCacheShaderPreprocessor;
	public bool m_bIsUseLegacyProbeSampleCount;
	public bool m_bIsEnableTextureStreamingInPlayMode;
	public bool m_bIsEnableTextureStreamingInEditMode;

#if UNITY_EDITOR
	public CacheServerMode m_eCacheServerMode;
	public LineEndingsMode m_eLineEndingMode;
	public ETextureCompressionType m_eTextureCompressionType;
#endif			// #if UNITY_EDITOR
}

//! 사운드 옵션
[System.Serializable]
public struct STSndOpts {
	public bool m_bIsDisable;
	public bool m_bIsVirtualizeEffect;

	public int m_nSampleRate;
	public int m_nNumRealVoices;
	public int m_nNumVirtualVoices;

	public float m_fGlobalVolume;
	public float m_fRolloffScale;
	public float m_fDopplerFactor;

	public AudioSpeakerMode m_eSpeakerMode;

#if UNITY_EDITOR
	public EDSPBufferSize m_eDSPBufferSize;
#endif			// #if UNITY_EDITOR
}

//! 공용 빌드 옵션
[System.Serializable]
public struct STCommonBuildOpts {
	public bool m_bIsRunInBackground;
	public bool m_bIsPreBakeCollisionMesh;
	public bool m_bIsMuteOtherAudioSource;
	public bool m_bIsEnableInternalProfiler;
	public bool m_bIsPreserveFrameBufferAlpha;
	public bool m_bIsEnableVulkanSRGBWrite;
	public bool m_bIsEnableLightmapStreaming;
	
	public int m_nLightmapStreamingPriority;
	public uint m_nNumVulkanSwapChainBuffers;

	public string m_oAOTCompileOpts;
	public List<Object> m_oPreloadAssetList;

#if UNITY_EDITOR
	public EAccelerometerFrequency m_eAccelerometerFrequency;
	public ELightmapEncodingQuality m_eLightmapEncodingQuality;
#endif			// #if UNITY_EDITOR
}

//! 독립 플랫폼 빌드 옵션
[System.Serializable]
public struct STStandaloneBuildOpts {
	public bool m_bIsForceSingleInstance;
	public bool m_bIsCaptureSingleScreen;

	public FullScreenMode m_eFullscreenMode;
}

//! iOS 빌드 옵션
[System.Serializable]
public struct STiOSBuildOpts {
	public bool m_bIsEnableProMotion;
	public bool m_bIsRequreARKitSupports;
	public bool m_bIsRequirePersistentWIFI;
	public bool m_bIsAutoAddCapabilities;

	public string m_oCameraDescription;
	public string m_oLocationDescription;
	public string m_oMicrophoneDescription;
	
#if UNITY_EDITOR
	public iOSTargetDevice m_eTargetDevice;
	public iOSStatusBarStyle m_eStatusBarStyle;
	public iOSAppInBackgroundBehavior m_eBackgroundBehavior;
#endif			// #if UNITY_EDITOR
}

//! 안드로이드 빌드 옵션
[System.Serializable]
public struct STAndroidBuildOpts {
	public bool m_bIsRenderOutside;
	public bool m_bIsTVCompatibility;

	public int m_nAppBundleSize;

#if UNITY_EDITOR
	public AndroidBlitType m_eBlitType;
	public EAspectRatioMode m_eAspectRatioMode;
	public AndroidPreferredInstallLocation m_ePreferredInstallLocation;
#endif			// #if UNITY_EDITOR
}

//! 빌드 옵션 테이블
public class CBuildOptsTable : CScriptableObj<CBuildOptsTable> {
	#region 변수
	[Header("Editor Opts")]
	[SerializeField] private STEditorOpts m_stEditorOpts;

	[Header("Snd Opts")]
	[SerializeField] private STSndOpts m_stSndOpts;

	[Header("Build Opts")]
	[SerializeField] private STCommonBuildOpts m_stCommonBuildOpts;
	[SerializeField] private STStandaloneBuildOpts m_stStandaloneBuildOpts;
	[SerializeField] private STiOSBuildOpts m_stiOSBuildOpts;
	[SerializeField] private STAndroidBuildOpts m_stAndroidBuildOpts;
	#endregion			// 변수

	#region 프로퍼티
	public STEditorOpts EditorOpts => m_stEditorOpts;
	public STSndOpts SndOpts => m_stSndOpts;

	public STCommonBuildOpts CommonBuildOpts => m_stCommonBuildOpts;
	public STStandaloneBuildOpts StandaloneBuildOpts => m_stStandaloneBuildOpts;
	public STiOSBuildOpts iOSBuildOpts => m_stiOSBuildOpts;
	public STAndroidBuildOpts AndroidBuildOpts => m_stAndroidBuildOpts;
	#endregion			// 프로퍼티

	#region 조건부 함수
#if UNITY_EDITOR
	//! 에디터 옵션을 변경한다
	public void SetEditorOpts(STEditorOpts a_stEditorOpts) {
		m_stEditorOpts = a_stEditorOpts;
	}

	//! 사운드 옵션을 변경한다
	public void SetSndOpts(STSndOpts a_stSndOpts) {
		m_stSndOpts = a_stSndOpts;
	}

	//! 공용 빌드 옵션 변경한다
	public void SetCommonBuildOpts(STCommonBuildOpts a_stBuildOpts) {
		m_stCommonBuildOpts = a_stBuildOpts;
	}

	//! 독립 플랫폼 빌드 옵션을 변경한다
	public void SetStandaloneBuildOpts(STStandaloneBuildOpts a_stBuildOpts) {
		m_stStandaloneBuildOpts = a_stBuildOpts;
	}

	//! iOS 빌드 옵션을 변경한다
	public void SetiOSBuildOpts(STiOSBuildOpts a_stBuildOpts) {
		m_stiOSBuildOpts = a_stBuildOpts;
	}

	//! 안드로이드 빌드 옵션을 변경한다
	public void SetAndroidBuildOpts(STAndroidBuildOpts a_stBuildOpts) {
		m_stAndroidBuildOpts = a_stBuildOpts;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
