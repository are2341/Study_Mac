using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 사운드 옵션 정보 */
[System.Serializable]
public struct STSndOptsInfo {
	public int m_nNumRealVoices;
	public int m_nNumVirtualVoices;
}

/** iOS 빌드 옵션 정보 */
[System.Serializable]
public struct STiOSBuildOptsInfo {
	public string m_oCameraDescription;
	public string m_oMotionDescription;
	public string m_oLocationDescription;
	public string m_oMicrophoneDescription;
}

/** 안드로이드 빌드 옵션 정보 */
[System.Serializable]
public struct STAndroidBuildOptsInfo {
	public bool m_bIsUseAPKExpansionFiles;
}

/** 독립 플랫폼 빌드 옵션 정보 */
[System.Serializable]
public struct STStandaloneBuildOptsInfo {
	// Do Something
}

/** 빌드 옵션 정보 */
[System.Serializable]
public struct STBuildOptsInfo {
	public bool m_bIsPreBakeCollisionMesh;
	public bool m_bIsPreserveFrameBufferAlpha;

	[Header("[iOS Build Opts Info]")] public STiOSBuildOptsInfo m_stiOSBuildOptsInfo;
	[Header("[Android Build Opts Info]")] public STAndroidBuildOptsInfo m_stAndroidBuildOptsInfo;
	[Header("[Standalone Build Opts Info]")] public STStandaloneBuildOptsInfo m_stStandaloneBuildOptsInfo;
}

/** 광원 옵션 정보 */
[System.Serializable]
public struct STLightOptsInfo {
	public EPowOfTwo m_eLightmapMaxSize;
	public ELightmapMode m_eLightmapMode;
	public ShadowmaskMode m_eShadowmaskMode;
	public ShadowResolution m_eShadowResolution;
	public LightmapCompression m_eLightmapCompression;
}

/** 유니버셜 렌더링 파이프라인 옵션 정보 */
[System.Serializable]
public struct STUniversalRPOptsInfo {
	public EPowOfTwo m_eLightCookieResolution;
	public EPowOfTwo m_eMainLightShadowResolution;
	public EPowOfTwo m_eAdditionalShadowResolution;
}

/** 렌더링 옵션 정보 */
[System.Serializable]
public struct STRenderingOptsInfo {
	public STLightOptsInfo m_stLightOptsInfo;
	public STUniversalRPOptsInfo m_stUniversalRPOptsInfo;
}

/** 퀄리티 옵션 정보 */
[System.Serializable]
public struct STQualityOptsInfo {
	public bool m_bIsUseSRPBatching;
	public bool m_bIsEnableRealtimeGI;
	public bool m_bIsEnableRealtimeEnvironmentLighting;
	
	public EQualityLevel m_eQualityLevel;
	public MixedLightingMode m_eMixedLightingMode;

#if UNITY_EDITOR
	public LightingSettings.Lightmapper m_eLightmapper;
#endif			// #if UNITY_EDITOR

	[Header("[Norm Quality Rendering Opts Info]")] public STRenderingOptsInfo m_stNormQualityRenderingOptsInfo;
	[Header("[High Quality Rendering Opts Info]")] public STRenderingOptsInfo m_stHighQualityRenderingOptsInfo;
	[Header("[Ultra Quality Rendering Opts Info]")] public STRenderingOptsInfo m_stUltraQualityRenderingOptsInfo;
}

/** 옵션 정보 테이블 */
public class COptsInfoTable : CScriptableObj<COptsInfoTable> {
	#region 변수
	[Header("=====> Snd Opts Info <=====")]
	[SerializeField] private STSndOptsInfo m_stSndOptsInfo;

	[Header("=====> Build Opts Info <=====")]
	[SerializeField] private STBuildOptsInfo m_stBuildOptsInfo;

	[Header("=====> Quality Opts Info <=====")]
	[SerializeField] private STQualityOptsInfo m_stQualityOptsInfo;
	#endregion			// 변수

	#region 프로퍼티
	public STSndOptsInfo SndOptsInfo => m_stSndOptsInfo;
	public STBuildOptsInfo BuildOptsInfo => m_stBuildOptsInfo;
	public STQualityOptsInfo QualityOptsInfo => m_stQualityOptsInfo;
	#endregion			// 프로퍼티

	#region 함수
	/** 렌더링 옵션 정보를 반환한다 */
	public STRenderingOptsInfo GetRenderingOptsInfo(EQualityLevel a_eQualityLevel) {
		switch(a_eQualityLevel) {
			case EQualityLevel.HIGH: return m_stQualityOptsInfo.m_stHighQualityRenderingOptsInfo;
			case EQualityLevel.ULTRA: return m_stQualityOptsInfo.m_stUltraQualityRenderingOptsInfo;
			default: return m_stQualityOptsInfo.m_stNormQualityRenderingOptsInfo;
		}
	}
	#endregion			// 함수

	#region 조건부 함수
#if UNITY_EDITOR
	/** 사운드 옵션 정보를 변경한다 */
	public void SetSndOptsInfo(STSndOptsInfo a_stSndOptsInfo) {
		m_stSndOptsInfo = a_stSndOptsInfo;
	}

	/** 빌드 옵션 정보를 변경한다 */
	public void SetBuildOptsInfo(STBuildOptsInfo a_stBuildOpts) {
		m_stBuildOptsInfo = a_stBuildOpts;
	}

	/** 퀄리티 옵션 정보를 설정한다 */
	public void SetQualityOptsInfo(STQualityOptsInfo a_stQualityOptsInfo) {
		m_stQualityOptsInfo = a_stQualityOptsInfo;
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
