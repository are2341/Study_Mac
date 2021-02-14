using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 기본
//! 마우스 버튼
public enum EMouseBtn {
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	MAX_VALUE
}

//! 방향
public enum EDirection {
	NONE = -1,
	
	UP,
	DOWN,
	LEFT,
	RIGHT,
	
	UP_LEFT,
	UP_RIGHT,
	DOWN_LEFT,
	DOWN_RIGHT,

	LEFT_UP,
	LEFT_DOWN,
	RIGHT_UP,
	RIGHT_DOWN,
	
	VERTICAL,
	HORIZONTAL,
	DIAGONAL,
	
	MAX_VALUE
}

//! 디바이스 방향
public enum EOrientation {
	NONE = -1,
	PORTRAIT,
	LANDSCAPE,
	MAX_VALUE
}

//! 퀄리티 수준
public enum EQualityLevel {
	NONE = -1,
	VERY_LOW,
	LOW,
	MEDIUM,
	HIGH,
	VERY_HIGH,
	ULTRA,
	AUTO,
	MAX_VALUE
}

//! 그림자 옵션
public enum EShadowCascadesOpts {
	NONE = -1,
	ONE_CASCADES,
	TWO_CASCADES,
	THREE_CASCADES,
	FOUR_CASCADES,
	MAX_VALUE
}

//! 애니메이션 타입
public enum EAniType {
	NONE = -1,
	SCALE,
	DROPDOWN,
	SLIDE_LEFT,
	SLIDE_RIGHT,
	MAX_VALUE
};

//! 유저 타입
public enum EUserType {
	NONE = -1,
	USER_A,
	USER_B,
	MAX_VALUE
}

//! 디바이스 타입
public enum EDeviceType {
	NONE = -1,
	PHONE,
	TABLET,
	MAX_VALUE
}

//! 수직 동기화 타입
public enum EVSyncType {
	NONE = -1,
	NEVER,
	EVERY,
	SECOND,
	MAX_VALUE
}

//! 값 타입
public enum EValueType {
	NONE = -1,
	BOOL,
	INT,
	FLOAT,
	STRING,
	MAX_VALUE
}

//! 진동 타입
public enum EVibrateType {
	NONE = -1,
	SELECTION,
	NOTIFICATION,
	IMPACT,
	MAX_VALUE
}

//! 진동 스타일
public enum EVibrateStyle {
	NONE = -1,
	LIGHT,
	MEDIUM,
	HEAVY,
	MAX_VALUE
}

//! 내비게이션 스택 이벤트 타입
public enum ENavStackEvent {
	NONE = -1,
	TOP,
	REMOVE,
	BACK_KEY_DOWN,
	MAX_VALUE
}

//! 시작 씬 이벤트 타입
public enum EStartSceneEvent {
	NONE = -1,
	LOAD_SETUP_SCENE,
	LOAD_AGREE_SCENE,
	LOAD_LATE_SETUP_SCENE,
	LOAD_PERMISSION_SCENE,
	LOAD_INTRO_SCENE,
	MAX_VALUE
}

//! 약관 동의 팝업 타입
public enum EAgreePopupType {
	NONE = -1,
	NORM,
	EU,
	MAX_VALUE
}
#endregion			// 기본

#region 조건부 상수
#if UNITY_EDITOR
//! DSP 버퍼 크기
public enum EDSPBufferSize {
	NONE = -1,
	BEST_LATENCY = 256,
	GOOD_LATENCY = 512,
	BEST_PERFORMANCE = 1024,
	MAX_VALUE	
}

//! 종횡비 모드
public enum EAspectRatioMode {
	NONE = -1,
	LEGACY_WIDE_SCREEN,
	NATIVE_ASPECT_RATIO,
	CUSTOM,
	MAX_VALUE
}

//! 가속도 갱신 주기
public enum EAccelerometerFrequency {
	NONE = -1,
	DISABLE,
	FREQUENCY_15_HZ = 15,
	FREQUENCY_30_HZ = 30,
	FREQUENCY_60_HZ = 60,
	FREQUENCY_100_HZ = 100,
	MAX_VALUE
}

//! 광원 맵 엔코딩 퀄리티
public enum ELightmapEncodingQuality {
	NONE = -1,
	LOW,
	NORM,
	HIGH,
	MAX_VALUE
}

//! 빌드 타입
public enum EBuildType {
	NONE = -1,
	DEBUG,
	RELEASE,
	ADHOC,
	STORE,
	MAX_VALUE
}

//! 독립 플랫폼 타입
public enum EStandaloneType {
	NONE = -1,
	MAC,
	WNDS,
	MAX_VALUE
}

//! 안드로이드 타입
public enum EAndroidType {
	NONE = -1,
	GOOGLE,
	ONE_STORE,
	GALAXY_STORE,
	MAX_VALUE
}

//! 텍스처 압축 타입
public enum ETextureCompressionType {
	NONE = -1,
	LEGACY,
	DEFAULT,
	CUSTOM,
	MAX_VALUE
}
#endif			// #if UNITY_EDITOR

#if ADS_MODULE_ENABLE
//! 광고 타입
public enum EAdsType {
	NONE = -1,

#if ADMOB_ENABLE
	ADMOB,
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
	IRON_SRC,
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
	APP_LOVIN,
#endif			// #if APP_LOVIN_ENABLE

	MAX_VALUE
}

//! 배너 광고 위치
public enum EBannerAdsPos {
	NONE = -1,
	TOP,
	BOTTOM,
	MAX_VALUE
}
#endif			// #if ADS_MODULE_ENABLE
#endregion			// 조건부 상수
