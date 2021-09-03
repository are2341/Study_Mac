﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if UNITY_ANDROID
using UnityEngine.Android;
#endif			// #if UNITY_ANDROID

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

//! 유틸리티 접근자
public static partial class CAccess {
	#region 클래스 프로퍼티
	public static bool IsEnableShowConsentView {
		get {
#if !UNITY_EDITOR && UNITY_IOS
			var oVer = new System.Version(Device.systemVersion);
			return oVer.CompareTo(KCDefine.U_MIN_VER_CONSENT_VIEW) >= KCDefine.B_COMPARE_EQUALS;
#elif !UNITY_EDITOR && UNITY_ANDROID
			return false;
#else
			return true;
#endif			// #if !UNITY_EDITOR && UNITY_IOS
		}
	}

	public static bool IsSupportsHapticFeedback {
		get {
#if (!UNITY_EDITOR && UNITY_IOS) && HAPTIC_FEEDBACK_ENABLE
			var oVer = new System.Version(Device.systemVersion);
			int nCompare = oVer.CompareTo(KCDefine.U_MIN_VER_HAPTIC_FEEDBACK);

			// 햅틱 피드백 지원 버전 일 경우
			if(nCompare >= KCDefine.B_COMPARE_EQUALS) {
				string oModel = Device.generation.ToString();
				
				for(int i = 0; i < KCDefine.U_HAPTIC_FEEDBACK_SUPPORTS_MODELS.Length; ++i) {
					var eModel = KCDefine.U_HAPTIC_FEEDBACK_SUPPORTS_MODELS[i];

					// 햅틱 피드백 지원 모델 일 경우
					if(eModel == Device.generation && oModel.Contains(KCDefine.U_MODEL_N_IPHONE)) {
						return true;
					}
				}
			}

			return false;
#elif (!UNITY_EDITOR && UNITY_ANDROID) && HAPTIC_FEEDBACK_ENABLE
			return true;
#else
			return false;
#endif			// #if (!UNITY_EDITOR && UNITY_IOS) && HAPTIC_FEEDBACK_ENABLE
		}
	}

	public static float ResolutionScale {
		get {
			float fScale = KCDefine.B_VAL_1_FLT;
			float fAspect = KCDefine.B_SCREEN_WIDTH / (float)KCDefine.B_SCREEN_HEIGHT;

			// 화면 너비를 벗어났을 경우
			if(CAccess.ScreenSize.x.ExIsLess(CAccess.ScreenSize.y * fAspect)) {
				fScale = CAccess.ScreenSize.x / (CAccess.ScreenSize.y * fAspect);
			}
			
			return fScale;
		}
	}

	public static EDeviceType DeviceType {
		get {
#if UNITY_IOS
			string oModel = Device.generation.ToString();
			return oModel.Contains(KCDefine.U_MODEL_N_IPAD) ? EDeviceType.TABLET : EDeviceType.PHONE;
#else
			return EDeviceType.PHONE;
#endif			// #if UNITY_IOS

			// FIXME: 추후 테블릿 여부 검사 로직 수정 필요
			// float fScreenWidth = CAccess.ScreenSize.x / Screen.dpi;
			// float fScreenHeight = CAccess.ScreenSize.y / Screen.dpi;
			// float fDiagonalInches = Mathf.Sqrt(Mathf.Pow(fScreenWidth, KCDefine.B_VAL_2_FLT) + Mathf.Pow(fScreenHeight, KCDefine.B_VAL_2_FLT));

			// return fDiagonalInches.ExIsGreate(KCDefine.U_UNIT_TABLET_INCHES) ? EDeviceType.TABLET : EDeviceType.PHONE;
		}
	}

	public static Vector3 ScreenSize {
		get {
#if UNITY_EDITOR
			return new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, KCDefine.B_VAL_0_FLT);
#else
			return new Vector3(Screen.width, Screen.height, KCDefine.B_VAL_0_FLT);
#endif			// #if UNITY_EDITOR
		}
	}

	public static Rect SafeArea {
		get {
#if UNITY_EDITOR
			return new Rect(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, Camera.main.pixelWidth, Camera.main.pixelHeight);
#else
			return Screen.safeArea;
#endif			// #if UNITY_EDITOR
		}
	}

	public static bool IsEditor => Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
	public static bool IsStandalone => Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer;

	public static bool IsMac => Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
	public static bool IsWnds => Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
	public static bool IsDesktop => CAccess.IsMac || CAccess.IsWnds;

	public static bool IsMobile => Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
	public static bool IsConsole => Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne;
	public static bool IsHandheldConsole => Application.platform == RuntimePlatform.Stadia || Application.platform == RuntimePlatform.Switch;

	public static float UpScreenScale => (CAccess.ScreenSize.y - (CAccess.SafeArea.y + CAccess.SafeArea.height)) / CAccess.ScreenSize.y;
	public static float DownScreenScale => CAccess.SafeArea.y / CAccess.ScreenSize.y;
	public static float LeftScreenScale => CAccess.SafeArea.x / CAccess.ScreenSize.x;
	public static float RightScreenScale => (CAccess.ScreenSize.x - (CAccess.SafeArea.x + CAccess.SafeArea.width)) / CAccess.ScreenSize.x;

	public static float DPI => Screen.dpi;
	public static Vector3 Resolution => KCDefine.B_SCREEN_SIZE * CAccess.ResolutionScale;
	#endregion			// 클래스 프로퍼티

	#region 클래스 함수
	//! 권한 유효 여부를 검사한다
	public static bool IsEnablePermission(string a_oPermission) {
		CAccess.Assert(a_oPermission.ExIsValid());

#if UNITY_ANDROID
		return Permission.HasUserAuthorizedPermission(a_oPermission);
#else
		return false;
#endif			// #if UNITY_ANDROID
	}
	
	//! 배너 광고 높이를 반환한다
	public static float GetBannerAdsHeight(float a_fHeight) {
		CAccess.Assert(a_fHeight.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		float fPercent = KCDefine.B_SCREEN_HEIGHT / CAccess.ScreenSize.y;
		float fBannerAdsHeight = CAccess.GetBannerAdsScreenHeight(a_fHeight);
		
		return (fBannerAdsHeight * fPercent) / CAccess.ResolutionScale;
	}

	//! 배너 광고 화면 높이를 반환한다
	public static float GetBannerAdsScreenHeight(float a_fHeight) {
#if UNITY_EDITOR || MODE_PORTRAIT_ENABLE
		return a_fHeight * (CAccess.DPI / KCDefine.B_DPI);
#else
		return (a_fHeight * KCDefine.U_SCALE_LANDSCAPE_BANNER_ADS_HEIGHT) * (CAccess.DPI / KCDefine.B_DPI);
#endif			// #if UNITY_EDITOR || MODE_PORTRAIT_ENABLE
	}
	
	//! 값을 할당한다
	public static void AssignVal(ref DG.Tweening.Tween a_oLhs, DG.Tweening.Tween a_oRhs) {
		a_oLhs?.Kill();
		a_oLhs = a_oRhs;
	}
	
	//! 값을 할당한다
	public static void AssignVal(ref Sequence a_oLhs, DG.Tweening.Tween a_oRhs) {
		a_oLhs?.Kill();
		a_oLhs = a_oRhs as Sequence;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 리소스 존재 여부를 검사한다
	public static bool IsExistsRes<T>(string a_oFilePath, bool a_bIsAutoUnload = false) where T : Object {
		CAccess.Assert(a_oFilePath.ExIsValid());

		var oRes = Resources.Load<T>(a_oFilePath);
		bool bIsExistsRes = !typeof(T).Equals(typeof(TextAsset)) ? oRes != null : (oRes as TextAsset).ExIsValid();
		
		// 자동 제거 모드 일 경우
		if(bIsExistsRes && a_bIsAutoUnload) {
			Resources.UnloadAsset(oRes);
		}

		return bIsExistsRes;
	}

	//! 값을 할당한다
	public static void AssignVal<T>(ref T a_tLhs, T a_tRhs, T a_tDefVal = null) where T : class {
		a_tLhs = a_tRhs ?? a_tDefVal;
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void SetScriptOrder(MonoScript a_oScript, int a_nOrder, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oScript != null);

		// 스크립트가 존재 할 경우
		if(a_oScript != null) {
			int nOrder = MonoImporter.GetExecutionOrder(a_oScript);

			// 기존 순서와 다를 경우
			if(nOrder != a_nOrder) {
				MonoImporter.SetExecutionOrder(a_oScript, a_nOrder);
			}
		}
	}
#endif			// #if UNITY_EDITOR

#if PURCHASE_MODULE_ENABLE
	//! 가격 문자열을 반환한다
	public static string GetPriceStr(Product a_oProduct) {
		CAccess.Assert(a_oProduct != null);

		decimal dclPrice = a_oProduct.metadata.localizedPrice;
		string oCurrencyCode = a_oProduct.metadata.isoCurrencyCode;
		
		return string.Format(KCDefine.B_TEXT_FMT_2_SPACE_COMBINE, oCurrencyCode, dclPrice);		
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수

	#region 조건부 클래스 프로퍼티
#if UNITY_IOS && APPLE_LOGIN_ENABLE
	//! 애플 로그인 지원 여부를 검사한다
	public static bool IsSupportsLoginWithApple {
		get {
#if UNITY_EDITOR
			return false;
#else
			var oVer = new System.Version(Device.systemVersion);
			int nCompare = oVer.CompareTo(KCDefine.U_MIN_VER_LOGIN_WITH_APPLE);
			
			return nCompare >= KCDefine.B_COMPARE_EQUALS;
#endif			// #if UNITY_EDITOR
		}
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE
	#endregion			// 조건부 클래스 프로퍼티
}
