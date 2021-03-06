﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using Unity.Linq;
using EnhancedUI.EnhancedScroller;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
#if APPLE_LOGIN_ENABLE
using UnityEngine.SignInWithApple;
#endif			// #if APPLE_LOGIN_ENABLE

#if NOTI_MODULE_ENABLE
using Unity.Notifications.iOS;
#endif			// #if NOTI_MODULE_ENABLE
#endif			// #if UNITY_IOS

//! 유틸리티 접근 확장 클래스
public static partial class CAccessExtension {
	#region 클래스 변수
#if UNITY_IOS && NOTI_MODULE_ENABLE
	private static AuthorizationOption[] m_oAuthOpts = new AuthorizationOption[] {
		AuthorizationOption.Badge,
		AuthorizationOption.Sound,
		AuthorizationOption.Alert,
		AuthorizationOption.CarPlay
	};
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 클래스 변수

	#region 클래스 함수
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this EUserType a_eSender) {
		return a_eSender > EUserType.NONE && a_eSender < EUserType.MAX_VAL;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this EDeviceType a_eSender) {
		return a_eSender > EDeviceType.NONE && a_eSender < EDeviceType.MAX_VAL;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this SystemLanguage a_eSender) {
		return a_eSender >= SystemLanguage.Afrikaans && a_eSender < SystemLanguage.Unknown;
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this TextAsset a_oSender) {
		// 텍스트 에셋이 유효하지 않을 경우
		if(a_oSender == null) {
			return false;
		}
		
		return a_oSender.text.ExIsValid() || a_oSender.bytes.ExIsValid();
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValid(this SpriteAtlas a_oSender) {
		return a_oSender != null && a_oSender.spriteCount > KCDefine.B_VAL_0_INT;
	}

	//! 인덱스 유효 여부를 검사한다
	public static bool ExIsValidIdx(this SimpleJSON.JSONArray a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector2 a_stSender, Vector2 a_stRhs) {
		return a_stSender.x.ExIsEquals(a_stRhs.x) && a_stSender.y.ExIsEquals(a_stRhs.y);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector3 a_stSender, Vector3 a_stRhs) {
		bool bIsEquals = CAccessExtension.ExIsEquals((Vector2)a_stSender, (Vector2)a_stRhs);
		return bIsEquals && a_stSender.z.ExIsEquals(a_stRhs.z);
	}

	//! 동일 여부를 검사한다
	public static bool ExIsEquals(this Vector4 a_stSender, Vector4 a_stRhs) {
		bool bIsEquals = CAccessExtension.ExIsEquals((Vector3)a_stSender, (Vector3)a_stRhs);
		return bIsEquals && a_stSender.w.ExIsEquals(a_stRhs.w);
	}

	//! 색상을 반환한다
	public static Color ExGetAlphaColor(this Color a_stSender, float a_fAlpha) {
		a_stSender.a = a_fAlpha;
		return a_stSender;
	}

	//! X 축 간격을 반환한다
	public static float ExGetDeltaX(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).x;
	}

	//! Y 축 간격을 반환한다
	public static float ExGetDeltaY(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).y;
	}

	//! Z 축 간격을 반환한다
	public static float ExGetDeltaZ(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).z;
	}

	//! 비율 벡터를 반환한다
	public static Vector2 ExGetScaleVector(this Vector2 a_stSender, Vector2 a_stScale) {
		a_stSender.x *= a_stScale.x;
		a_stSender.y *= a_stScale.y;

		return a_stSender;
	}

	//! 비율 벡터를 반환한다
	public static Vector3 ExGetScaleVector(this Vector3 a_stSender, Vector3 a_stScale) {
		a_stSender.x *= a_stScale.x;
		a_stSender.y *= a_stScale.y;
		a_stSender.z *= a_stScale.z;

		return a_stSender;
	}

	//! 비율 벡터를 반환한다
	public static Vector4 ExGetScaleVector(this Vector4 a_stSender, Vector4 a_stScale) {
		a_stSender.x *= a_stScale.x;
		a_stSender.y *= a_stScale.y;
		a_stSender.z *= a_stScale.z;
		a_stSender.w *= a_stScale.w;

		return a_stSender;
	}
	
	//! 캔버스 월드 위치를 반환한다
	public static Vector3 ExGetWorldPos(this PointerEventData a_oSender) {
		CAccess.Assert(a_oSender != null);

		float fAspect = CAccess.ScreenSize.x / CAccess.ScreenSize.y;
		float fScreenWidth = KCDefine.B_SCREEN_HEIGHT * fAspect;

		float fNormPosX = ((a_oSender.position.x * KCDefine.B_VAL_2_FLT) / CAccess.ScreenSize.x) - KCDefine.B_VAL_1_FLT;
		var stNormPos = new Vector3(fNormPosX, ((a_oSender.position.y * KCDefine.B_VAL_2_FLT) / CAccess.ScreenSize.y) - KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_0_FLT);

		stNormPos.x *= (fScreenWidth / KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE;
		stNormPos.y *= (KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE;

		return stNormPos;
	}

	//! 캔버스 로컬 위치를 반환한다
	public static Vector3 ExGetLocalPos(this PointerEventData a_oSender, GameObject a_oObj) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		var stPos = a_oSender.ExGetWorldPos();

		return stPos.ExToLocal(a_oObj);
	}

	//! 캔버스 월드 비율 위치를 반환한다
	public static Vector3 ExGetWorldScalePos(this PointerEventData a_oSender, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null);
		var stPos = a_oSender.ExGetWorldPos();

		return stPos.ExGetScaleVector(a_stScale);
	}

	//! 캔버스 로컬 비율 위치를 반환한다
	public static Vector3 ExGetLocalScalePos(this PointerEventData a_oSender, GameObject a_oObj, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		var stPos = a_oSender.ExGetWorldScalePos(a_stScale);

		return stPos.ExToLocal(a_oObj);
	}

	//! 보정된 캔버스 월드 위치를 반환한다
	public static Vector3 ExGetCorrectWorldPos(this Vector3 a_stSender) {
		float fPosX = Mathf.Clamp(a_stSender.x, (CAccess.Resolution.x / -KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE, (CAccess.Resolution.x / KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE);
		float fPosY = Mathf.Clamp(a_stSender.y, (CAccess.Resolution.y / -KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE, (CAccess.Resolution.y / KCDefine.B_VAL_2_FLT) * KCDefine.B_UNIT_SCALE);

		return new Vector3(fPosX, fPosY, a_stSender.z);
	}

	//! 보정된 캔버스 월드 위치를 반환한다
	public static Vector3 ExGetCorrectWorldPos(this PointerEventData a_oSender) {
		CAccess.Assert(a_oSender != null);
		var stPos = a_oSender.ExGetWorldPos();

		return stPos.ExGetCorrectWorldPos();
	}

	//! 보정된 캔버스 로컬 위치를 반환한다
	public static Vector3 ExGetCorrectLocalPos(this PointerEventData a_oSender, GameObject a_oObj) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		var stPos = a_oSender.ExGetCorrectWorldPos();

		return stPos.ExToLocal(a_oObj);
	}

	//! 보정된 캔버스 월드 비율 위치를 반환한다
	public static Vector3 ExGetCorrectWorldScalePos(this Vector3 a_oSender, Vector3 a_stScale) {
		var stPos = a_oSender.ExGetScaleVector(a_stScale);
		return stPos.ExGetCorrectWorldPos();
	}

	//! 보정된 캔버스 월드 비율 위치를 반환한다
	public static Vector3 ExGetCorrectWorldScalePos(this PointerEventData a_oSender, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null);
		var stPos = a_oSender.ExGetWorldScalePos(a_stScale);

		return stPos.ExGetCorrectWorldPos();
	}

	//! 보정된 캔버스 로컬 비율 위치를 반환한다
	public static Vector3 ExGetCorrectLocalScalePos(this PointerEventData a_oSender, GameObject a_oObj, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		var stPos = a_oSender.ExGetCorrectWorldScalePos(a_stScale);

		return stPos.ExToLocal(a_oObj);
	}
	
	//! 스크롤 뷰 정규 위치를 반환한다
	public static Vector3 ExGetNormPos(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_oViewport != null && a_oContents != null);

		float fNormPosH = a_oSender.ExGetNormPosH(a_oViewport, a_oContents, a_stPos);
		return new Vector3(fNormPosH, a_oSender.ExGetNormPosV(a_oViewport, a_oContents, a_stPos), KCDefine.B_VAL_0_FLT);
	}

	//! 스크롤 뷰 수직 정규 위치를 반환한다
	public static float ExGetNormPosV(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_oViewport != null && a_oContents != null);

		var oViewportTrans = a_oViewport.transform as RectTransform;
		var oContentsTrans = a_oContents.transform as RectTransform;

		return Mathf.Clamp01((a_stPos.y - oViewportTrans.rect.height) / (oContentsTrans.rect.height - oViewportTrans.rect.height));
	}

	//! 스크롤 뷰 수평 정규 위치를 반환한다
	public static float ExGetNormPosH(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_oViewport != null && a_oContents != null);

		var oViewportTrans = a_oViewport.transform as RectTransform;
		var oContentsTrans = a_oContents.transform as RectTransform;

		return Mathf.Clamp01((a_stPos.x - oViewportTrans.rect.width) / (oContentsTrans.rect.width - oViewportTrans.rect.width));
	}

	//! 자식을 반환한다
	public static List<GameObject> ExGetChildren(this Scene a_stSender) {
		var oObjs = a_stSender.GetRootGameObjects();
		var oObjList = new List<GameObject>();

		// 객체가 존재 할 경우
		if(oObjs.ExIsValid()) {
			for(int i = 0; i < oObjs.Length; ++i) {
				var oChildObjList = oObjs[i].ExGetChildren();
				oObjList.AddRange(oChildObjList);
			}
		}

		return oObjList;
	}

	//! 자식을 반환한다
	public static List<GameObject> ExGetChildren(this GameObject a_oSender, bool a_bIsIncludeSelf = true) {
		CAccess.Assert(a_oSender != null);

		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants();

		foreach(var oObj in oEnumerator) {
			oObjList.Add(oObj);
		}

		return oObjList;
	}

	//! 부모를 반환한다
	public static List<GameObject> ExGetParents(this GameObject a_oSender, bool a_bIsIncludeSelf = true) {
		CAccess.Assert(a_oSender != null);

		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors();

		foreach(var oObj in oEnumerator) {
			oObjList.Add(oObj);
		}

		return oObjList;
	}

	//! 크기 형식 문자열을 반환한다
	public static string ExGetSizeFmtStr(this string a_oSender, int a_nSize) {
		CAccess.Assert(a_oSender != null);
		return string.Format(KCDefine.B_SIZE_FMT_STR, a_nSize, a_oSender);
	}

	//! 색상 형식 문자열을 반환한다
	public static string ExGetColorFmtStr(this string a_oSender, Color a_stColor) {
		CAccess.Assert(a_oSender != null);
		return string.Format(KCDefine.B_COLOR_FMT_STR, ColorUtility.ToHtmlStringRGBA(a_stColor), a_oSender);
	}

	//! 활성화 여부를 변경한다
	public static void ExSetEnable(this LayoutGroup a_oSender, bool a_bIsEnable) {
		CAccess.Assert(a_oSender != null);

		a_oSender.enabled = false;
		a_oSender.gameObject.ExSetEnableComponent<ContentSizeFitter>(a_bIsEnable);
	}

	//! 컬링 마스크를 변경한다
	public static void ExSetCullingMask(this Camera a_oSender, List<int> a_oLayerList, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);
		a_oSender.cullingMask = a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.cullingMask;

		// 레이어 리스트가 유효 할 경우
		if(a_oLayerList != null) {
			a_oSender.cullingMask |= a_oLayerList.ExToBits();
		}
	}

	//! 컬링 마스크를 변경한다
	public static void ExSetCullingMask(this Light a_oSender, List<int> a_oLayerList, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);
		a_oSender.cullingMask = a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.cullingMask;

		// 레이어가 유효 할 경우
		if(a_oLayerList.ExIsValid()) {
			for(int i = 0; i < a_oLayerList.Count; ++i) {
				a_oSender.cullingMask |= a_oLayerList.ExToBits();
			}
		}
	}

	//! 이벤트 마스크를 변경한다
	public static void ExSetEventMask(this PhysicsRaycaster a_oSender, List<int> a_oLayerList, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);
		
		var stLayerMask = a_oSender.eventMask;
		stLayerMask.value = a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.eventMask.value;

		// 레이어 리스트가 유효 할 경우
		if(a_oLayerList != null) {
			stLayerMask.value |= a_oLayerList.ExToBits();
		}

		a_oSender.eventMask = stLayerMask;
	}

	//! 정렬 순서를 변경한다
	public static void ExSetSortingOrder(this Canvas a_oSender, STSortingOrderInfo a_stOrderInfo) {
		CAccess.Assert(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid());

		a_oSender.sortingLayerName = a_stOrderInfo.m_oLayer;
		a_oSender.sortingOrder = a_stOrderInfo.m_nOrder;
	}

	//! 정렬 순서를 변경한다
	public static void ExSetSortingOrder(this SpriteRenderer a_oSender, STSortingOrderInfo a_stOrderInfo) {
		CAccess.Assert(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid());

		a_oSender.sortingLayerName = a_stOrderInfo.m_oLayer;
		a_oSender.sortingOrder = a_stOrderInfo.m_nOrder;
	}

	//! 정렬 순서를 변경한다
	public static void ExSetSortingOrder(this ParticleSystem a_oSender, STSortingOrderInfo a_stOrderInfo) {
		CAccess.Assert(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid());

		var oRenderer = a_oSender.GetComponent<Renderer>();
		oRenderer.sortingLayerName = a_stOrderInfo.m_oLayer;
		oRenderer.sortingOrder = a_stOrderInfo.m_nOrder;
	}

	//! 색상을 변경한다
	public static void ExSetStartColor(this ParticleSystem a_oSender, Color a_stMinColor, Color a_stMaxColor) {
		CAccess.Assert(a_oSender != null);

		var oMainModule = a_oSender.main;
		oMainModule.startColor = new ParticleSystem.MinMaxGradient(a_stMinColor, a_stMaxColor);
	}

	//! 비율을 변경한다
	public static void ExSetScale(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale = new Vector3(a_fVal, a_fVal, a_fVal);
	}

	//! X 축 비율을 변경한다
	public static void ExSetScaleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale = new Vector3(a_fVal, a_oSender.transform.localScale.y, a_oSender.transform.localScale.z);
	}

	//! Y 축 비율을 변경한다
	public static void ExSetScaleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale = new Vector3(a_oSender.transform.localScale.x, a_fVal, a_oSender.transform.localScale.z);
	}

	//! Z 축 비율을 변경한다
	public static void ExSetScaleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale = new Vector3(a_oSender.transform.localScale.x, a_oSender.transform.localScale.y, a_fVal);
	}

	//! 월드 각도를 변경한다
	public static void ExSetWorldAngle(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles = new Vector3(a_fVal, a_fVal, a_fVal);
	}

	//! 월드 X 축 각도를 변경한다
	public static void ExSetWorldAngleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles = new Vector3(a_fVal, a_oSender.transform.eulerAngles.y, a_oSender.transform.eulerAngles.z);
	}
	
	//! 월드 Y 축 각도를 변경한다
	public static void ExSetWorldAngleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles = new Vector3(a_oSender.transform.eulerAngles.x, a_fVal, a_oSender.transform.eulerAngles.z);
	}

	//! 월드 Z 축 각도를 변경한다
	public static void ExSetWorldAngleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles = new Vector3(a_oSender.transform.eulerAngles.x, a_oSender.transform.eulerAngles.y, a_fVal);
	}

	//! 로컬 각도를 변경한다
	public static void ExSetLocalAngle(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles = new Vector3(a_fVal, a_fVal, a_fVal);
	}

	//! 로컬 X 축 각도를 변경한다
	public static void ExSetLocalAngleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles = new Vector3(a_fVal, a_oSender.transform.localEulerAngles.y, a_oSender.transform.localEulerAngles.z);
	}
	
	//! 로컬 Y 축 각도를 변경한다
	public static void ExSetLocalAngleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles = new Vector3(a_oSender.transform.localEulerAngles.x, a_fVal, a_oSender.transform.localEulerAngles.z);
	}

	//! 로컬 Z 축 각도를 변경한다
	public static void ExSetLocalAngleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles = new Vector3(a_oSender.transform.localEulerAngles.x, a_oSender.transform.localEulerAngles.y, a_fVal);
	}

	//! 월드 위치를 변경한다
	public static void ExSetWorldPos(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position = new Vector3(a_fVal, a_fVal, a_fVal);
	}

	//! 월드 X 축 위치를 변경한다
	public static void ExSetWorldPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position = new Vector3(a_fVal, a_oSender.transform.position.y, a_oSender.transform.position.z);
	}
	
	//! 월드 Y 축 위치를 변경한다
	public static void ExSetWorldPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position = new Vector3(a_oSender.transform.position.x, a_fVal, a_oSender.transform.position.z);
	}

	//! 월드 Z 축 위치를 변경한다
	public static void ExSetWorldPosZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position = new Vector3(a_oSender.transform.position.x, a_oSender.transform.position.y, a_fVal);
	}

	//! 로컬 위치를 변경한다
	public static void ExSetLocalPos(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition = new Vector3(a_fVal, a_fVal, a_fVal);
	}

	//! 로컬 X 축 위치를 변경한다
	public static void ExSetLocalPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition = new Vector3(a_fVal, a_oSender.transform.localPosition.y, a_oSender.transform.localPosition.z);
	}
	
	//! 로컬 Y 축 위치를 변경한다
	public static void ExSetLocalPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition = new Vector3(a_oSender.transform.localPosition.x, a_fVal, a_oSender.transform.localPosition.z);
	}

	//! 로컬 Z 축 위치를 변경한다
	public static void ExSetLocalPosZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition = new Vector3(a_oSender.transform.localPosition.x, a_oSender.transform.localPosition.y, a_fVal);
	}

	//! 크기 간격을 변경한다
	public static void ExSetSizeDelta(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).sizeDelta = new Vector2(a_fVal, a_fVal);
	}

	//! X 축 크기 간격을 변경한다
	public static void ExSetSizeDeltaX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).sizeDelta = new Vector2(a_fVal, (a_oSender.transform as RectTransform).sizeDelta.y);
	}

	//! Y 축 크기 간격을 변경한다
	public static void ExSetSizeDeltaY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).sizeDelta = new Vector2((a_oSender.transform as RectTransform).sizeDelta.x, a_fVal);
	}

	//! 앵커 위치를 변경한다
	public static void ExSetAnchorPos(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).anchoredPosition = new Vector2(a_fVal, a_fVal);
	}

	//! X 축 앵커 위치를 변경한다
	public static void ExSetAnchorPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).anchoredPosition = new Vector2(a_fVal, (a_oSender.transform as RectTransform).anchoredPosition.y);
	}

	//! Y 축 앵커 위치를 변경한다
	public static void ExSetAnchorPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).anchoredPosition = new Vector2((a_oSender.transform as RectTransform).anchoredPosition.x, a_fVal);
	}

	//! 스크롤 위치를 변경한다
	public static void ExSetScrollPos(this EnhancedScroller a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		float fPos = Mathf.Clamp(a_fVal, KCDefine.B_VAL_0_FLT, a_oSender.ScrollSize);

		a_oSender.ScrollPosition = fPos;
	}
	
	//! 자식 객체를 탐색한다
	public static GameObject ExFindChild(this Scene a_stSender, string a_oName, bool a_bIsEnableSubName = false) {
		CAccess.Assert(a_oName.ExIsValid());
		var oObjs = a_stSender.GetRootGameObjects();

		// 객체가 존재 할 경우
		if(oObjs.ExIsValid()) {
			for(int i = 0; i < oObjs.Length; ++i) {
				var oObj = oObjs[i].ExFindChild(a_oName, true, a_bIsEnableSubName);

				// 자식 객체가 존재 할 경우
				if(oObj != null) {
					return oObj;
				}
			}
		}

		return null;
	}

	//! 자식 객체를 탐색한다
	public static GameObject ExFindChild(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.ExIsEquals(a_oName);
			
			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.ExIsContains(a_oName))) {
				return oObj;
			}
		}

		return null;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponent<T>(this GameObject a_oSender, bool a_bIsEnable) where T : Component {
		CAccess.Assert(a_oSender != null);
		var oComponent = a_oSender.GetComponentInChildren<T>() as Behaviour;

		// 컴포넌트가 존재 할 경우
		if(oComponent != null) {
			oComponent.enabled = a_bIsEnable;
		}
	}

	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponent<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		var oObj = a_stSender.ExFindChild(a_oName);

		oObj.ExSetEnableComponent<T>(a_bIsEnable);
	}

	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponent<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf);

		oObj.ExSetEnableComponent<T>(a_bIsEnable);
	}

	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponents<T>(this GameObject a_oSender, bool a_bIsEnable) where T : Component {
		CAccess.Assert(a_oSender != null);
		var oComponents = a_oSender.GetComponentsInChildren<T>();

		for(int i = 0; i < oComponents.Length; ++i) {
			var oComponent = oComponents[i] as Behaviour;

			// 컴포넌트가 존재 할 경우
			if(oComponent != null) {
				oComponent.enabled = a_bIsEnable;
			}
		}
	}

	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponents<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		var oObj = a_stSender.ExFindChild(a_oName);

		oObj.ExSetEnableComponents<T>(a_bIsEnable);
	}

	//! 컴포넌트 활성 여부를 변경한다
	public static void ExSetEnableComponents<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf);

		oObj.ExSetEnableComponents<T>(a_bIsEnable);
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractable<T>(this GameObject a_oSender, bool a_bIsEnable) where T : Selectable {
		CAccess.Assert(a_oSender != null);
		var oComponent = a_oSender.GetComponentInChildren<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponent != null) {
			oComponent.interactable = a_bIsEnable;
		}
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractable<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable) where T : Selectable {
		CAccess.Assert(a_oName.ExIsValid());
		var oObj = a_stSender.ExFindChild(a_oName);

		oObj.ExSetInteractable<T>(a_bIsEnable);
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractable<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true) where T : Selectable {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf);

		oObj.ExSetInteractable<T>(a_bIsEnable);
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractables<T>(this GameObject a_oSender, bool a_bIsEnable) where T : Selectable {
		CAccess.Assert(a_oSender != null);
		var oComponents = a_oSender.GetComponentsInChildren<T>();

		for(int i = 0; i < oComponents.Length; ++i) {
			var oComponent = oComponents[i];

			// 컴포넌트가 존재 할 경우
			if(oComponent != null) {
				oComponent.interactable = a_bIsEnable;
			}
		}
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractables<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable) where T : Selectable {
		CAccess.Assert(a_oName.ExIsValid());
		var oObj = a_stSender.ExFindChild(a_oName);

		oObj.ExSetInteractables<T>(a_bIsEnable);
	}

	//! 컴포넌트 상호 작용 여부를 변경한다
	public static void ExSetInteractables<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true) where T : Selectable {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf);

		oObj.ExSetInteractables<T>(a_bIsEnable);
	}
	#endregion			// 제네릭 클래스 함수
	
	#region 조건부 클래스 함수
#if UNITY_EDITOR
	//! 스크립트 순서를 변경한다
	public static void ExSetScriptOrder(this MonoBehaviour a_oSender, int a_nOrder) {
		CAccess.Assert(a_oSender != null);
		var oMonoScript = MonoScript.FromMonoBehaviour(a_oSender);

		CAccess.SetScriptOrder(oMonoScript, a_nOrder);
	}
#endif			// #if UNITY_EDITOR

#if UNITY_IOS && APPLE_LOGIN_ENABLE
	//! 유효 여부를 검사한다
	public static bool ExIsValidUserInfo(this SignInWithApple.CallbackArgs a_stSender) {
		return a_stSender.userInfo.userId.ExIsValid() && !a_stSender.error.ExIsValid();
	}

	//! 유효 여부를 검사한다
	public static bool ExIsValidCredentialState(this SignInWithApple.CallbackArgs a_stSender) {
		return a_stSender.credentialState != UserCredentialState.NotFound && !a_stSender.error.ExIsValid();
	}
#endif			// #if UNITY_IOS && APPLE_LOGIN_ENABLE

#if ADS_MODULE_ENABLE
	//! 유효 여부를 검사한다
	public static bool ExIsValid(this EAdsType a_eSender) {
		return a_eSender > EAdsType.NONE && a_eSender < EAdsType.MAX_VAL;
	}
#endif			// #if ADS_MODULE_ENABLE

#if UNITY_IOS && NOTI_MODULE_ENABLE
	//! 인증 옵션 유효 여부를 검사한다
	public static bool ExIsValidAuthOpts(this AuthorizationOption a_eSender) {
		int nSumVal = KCDefine.B_VAL_0_INT;

		for(int i = 0; i < CAccessExtension.m_oAuthOpts.Length; ++i) {
			nSumVal += (int)(a_eSender & CAccessExtension.m_oAuthOpts[i]);
		}

		return nSumVal != KCDefine.B_VAL_0_INT;
	}

	//! 인증 요청 완료 여부를 검사한다
	public static bool ExIsCompleteRequest(this AuthorizationRequest a_oSender) {
		return a_oSender != null && a_oSender.IsFinished;
	}
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
