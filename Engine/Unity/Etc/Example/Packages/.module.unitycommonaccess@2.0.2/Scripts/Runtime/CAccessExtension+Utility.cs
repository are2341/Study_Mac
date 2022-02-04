﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using Unity.Linq;
using EnhancedUI.EnhancedScroller;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

#if UNITY_IOS
#if NOTI_MODULE_ENABLE
using Unity.Notifications.iOS;
#endif			// #if NOTI_MODULE_ENABLE
#endif			// #if UNITY_IOS

/** 유틸리티 접근 확장 클래스 */
public static partial class CAccessExtension {
	#region 클래스 변수
#if UNITY_IOS && NOTI_MODULE_ENABLE
	private static List<AuthorizationOption> m_oAuthOptList = new List<AuthorizationOption>() {
		AuthorizationOption.Badge, AuthorizationOption.Sound, AuthorizationOption.Alert
	};
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 클래스 변수

	#region 클래스 함수
	/** 맥 여부를 검사한다 */
	public static bool ExIsMac(this EStandaloneType a_eSender) {
		return a_eSender == EStandaloneType.MAC_APPLE || a_eSender == EStandaloneType.MAC_STEAM;
	}

	/** 윈도우즈 여부를 검사한다 */
	public static bool ExIsWnds(this EStandaloneType a_eSender) {
		return a_eSender == EStandaloneType.WNDS_STEAM;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EUserType a_eSender) {
		return a_eSender > EUserType.NONE && a_eSender < EUserType.MAX_VAL;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EDeviceType a_eSender) {
		return a_eSender > EDeviceType.NONE && a_eSender < EDeviceType.MAX_VAL;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EFontSet a_eSender) {
		return a_eSender > EFontSet.NONE && a_eSender < EFontSet.MAX_VAL;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this SystemLanguage a_eSender) {
		return a_eSender >= SystemLanguage.Afrikaans && a_eSender < SystemLanguage.Unknown;
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this TextAsset a_oSender) {
		return a_oSender != null && (a_oSender.text.ExIsValid() || a_oSender.bytes.ExIsValid());
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this SpriteAtlas a_oSender) {
		return a_oSender != null && a_oSender.spriteCount > KCDefine.B_VAL_0_INT;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this SimpleJSON.JSONArray a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.Count;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this EnhancedScroller a_oSender, int a_nIdx) {
		CAccess.Assert(a_oSender != null);
		return a_nIdx > KCDefine.B_IDX_INVALID && a_nIdx < a_oSender.NumberOfCells;
	}
	
	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this Vector2 a_stSender, Vector2 a_stRhs) {
		return a_stSender.x.ExIsEquals(a_stRhs.x) && a_stSender.y.ExIsEquals(a_stRhs.y);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this Vector3 a_stSender, Vector3 a_stRhs) {
		return CAccessExtension.ExIsEquals((Vector2)a_stSender, (Vector2)a_stRhs) && a_stSender.z.ExIsEquals(a_stRhs.z);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this Vector4 a_stSender, Vector4 a_stRhs) {
		return CAccessExtension.ExIsEquals((Vector3)a_stSender, (Vector3)a_stRhs) && a_stSender.w.ExIsEquals(a_stRhs.w);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this STIdxInfo a_stSender, STIdxInfo a_stRhs) {
		return (a_stSender.m_nX == a_stRhs.m_nX) && (a_stSender.m_nY == a_stRhs.m_nY) && (a_stSender.m_nZ == a_stRhs.m_nZ);
	}
	
	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<Vector2> a_oSender, List<Vector2> a_oVecList) {
		CAccess.Assert(a_oSender != null && a_oVecList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].ExIsEquals(a_oVecList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oVecList.Count;
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<Vector3> a_oSender, List<Vector3> a_oVecList) {
		CAccess.Assert(a_oSender != null && a_oVecList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].ExIsEquals(a_oVecList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oVecList.Count;
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<Vector4> a_oSender, List<Vector4> a_oVecList) {
		CAccess.Assert(a_oSender != null && a_oVecList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].ExIsEquals(a_oVecList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oVecList.Count;
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<Vector2Int> a_oSender, List<Vector2Int> a_oVecList) {
		CAccess.Assert(a_oSender != null && a_oVecList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].Equals(a_oVecList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oVecList.Count;
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<Vector3Int> a_oSender, List<Vector3Int> a_oVecList) {
		CAccess.Assert(a_oSender != null && a_oVecList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].Equals(a_oVecList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oVecList.Count;
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this List<STIdxInfo> a_oSender, List<STIdxInfo> a_oIdxInfoList) {
		CAccess.Assert(a_oSender != null && a_oIdxInfoList != null);

		for(int i = 0; i < a_oSender.Count; ++i) {
			// 동일하지 않을 경우
			if(!a_oSender[i].Equals(a_oIdxInfoList[i])) {
				return false;
			}
		}

		return a_oSender.Count == a_oIdxInfoList.Count;
	}
	
	/** 색상을 반환한다 */
	public static Color ExGetAlphaColor(this Color a_stSender, float a_fAlpha) {
		return new Color(a_stSender.r, a_stSender.g, a_stSender.b, a_fAlpha);
	}

	/** 일반 색상을 반환한다 */
	public static ColorBlock ExGetNormColor(this ColorBlock a_stSender, Color a_stColor) {
		a_stSender.normalColor = a_stColor;
		return a_stSender;
	}

	/** 프레스 색상을 반환한다 */
	public static ColorBlock ExGetPressColor(this ColorBlock a_stSender, Color a_stColor) {
		a_stSender.pressedColor = a_stColor;
		return a_stSender;
	}

	/** 선택 색상을 반환한다 */
	public static ColorBlock ExGetSelColor(this ColorBlock a_stSender, Color a_stColor) {
		a_stSender.selectedColor = a_stColor;
		return a_stSender;
	}

	/** 하이라이트 색상을 반환한다 */
	public static ColorBlock ExGetHighlightColor(this ColorBlock a_stSender, Color a_stColor) {
		a_stSender.highlightedColor = a_stColor;
		return a_stSender;
	}

	/** 비활성 색상을 반환한다 */
	public static ColorBlock ExGetDisableColor(this ColorBlock a_stSender, Color a_stColor) {
		a_stSender.disabledColor = a_stColor;
		return a_stSender;
	}

	/** 레이어 값을 반환한다 */
	public static LayerMask ExGetLayerVal(this LayerMask a_stSender, int a_nVal) {
		a_stSender.value = a_nVal;
		return a_stSender;
	}

	/** X 축 간격을 반환한다 */
	public static float ExGetDeltaX(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).x;
	}

	/** Y 축 간격을 반환한다 */
	public static float ExGetDeltaY(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).y;
	}

	/** Z 축 간격을 반환한다 */
	public static float ExGetDeltaZ(this Vector3 a_stSender, Vector3 a_stRhs) {
		return (a_stSender - a_stRhs).z;
	}

	/** 비율 벡터를 반환한다 */
	public static Vector2 ExGetScaleVector(this Vector2 a_stSender, Vector2 a_stScale) {
		return new Vector2(a_stSender.x * a_stScale.x, a_stSender.y * a_stScale.y);
	}

	/** 비율 벡터를 반환한다 */
	public static Vector3 ExGetScaleVector(this Vector3 a_stSender, Vector3 a_stScale) {
		return new Vector3(a_stSender.x * a_stScale.x, a_stSender.y * a_stScale.y, a_stSender.z * a_stScale.z);
	}

	/** 비율 벡터를 반환한다 */
	public static Vector4 ExGetScaleVector(this Vector4 a_stSender, Vector4 a_stScale) {
		return new Vector4(a_stSender.x * a_stScale.x, a_stSender.y * a_stScale.y, a_stSender.z * a_stScale.z, a_stSender.w * a_stScale.w);
	}
	
	/** 캔버스 월드 위치를 반환한다 */
	public static Vector3 ExGetWorldPos(this PointerEventData a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.position.ExTo3D().ExGetWorldPos();
	}

	/** 캔버스 로컬 위치를 반환한다 */
	public static Vector3 ExGetLocalPos(this PointerEventData a_oSender, GameObject a_oObj) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		return a_oSender.ExGetWorldPos().ExToLocal(a_oObj);
	}

	/** 캔버스 월드 비율 위치를 반환한다 */
	public static Vector3 ExGetWorldScalePos(this PointerEventData a_oSender, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExGetWorldPos().ExGetScaleVector(a_stScale);
	}

	/** 캔버스 로컬 비율 위치를 반환한다 */
	public static Vector3 ExGetLocalScalePos(this PointerEventData a_oSender, GameObject a_oObj, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		return a_oSender.ExGetWorldScalePos(a_stScale).ExToLocal(a_oObj);
	}

	/** 보정된 캔버스 월드 위치를 반환한다 */
	public static Vector3 ExGetCorrectWorldPos(this PointerEventData a_oSender) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExGetWorldPos().ExGetCorrectWorldPos();
	}

	/** 보정된 캔버스 로컬 위치를 반환한다 */
	public static Vector3 ExGetCorrectLocalPos(this PointerEventData a_oSender, GameObject a_oObj) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		return a_oSender.ExGetCorrectWorldPos().ExToLocal(a_oObj);
	}

	/** 보정된 캔버스 월드 비율 위치를 반환한다 */
	public static Vector3 ExGetCorrectWorldScalePos(this PointerEventData a_oSender, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.ExGetWorldScalePos(a_stScale).ExGetCorrectWorldPos();
	}

	/** 보정된 캔버스 로컬 비율 위치를 반환한다 */
	public static Vector3 ExGetCorrectLocalScalePos(this PointerEventData a_oSender, GameObject a_oObj, Vector3 a_stScale) {
		CAccess.Assert(a_oSender != null && a_oObj != null);
		return a_oSender.ExGetCorrectWorldScalePos(a_stScale).ExToLocal(a_oObj);
	}

	/** 캔버스 월드 간격을 반환한다 */
	public static Vector3 ExGetWorldDelta(this PointerEventData a_oSender) {
		return a_oSender.pointerPressRaycast.screenPosition.ExTo3D().ExGetWorldPos() - a_oSender.pointerCurrentRaycast.screenPosition.ExTo3D().ExGetWorldPos();
	}

	/** 캔버스 로컬 간격을 반환한다 */
	public static Vector3 ExGetLocalDelta(this PointerEventData a_oSender, GameObject a_oObj) {
		return a_oSender.pointerPressRaycast.screenPosition.ExTo3D().ExGetWorldPos().ExToLocal(a_oObj) - a_oSender.pointerCurrentRaycast.screenPosition.ExTo3D().ExGetWorldPos().ExToLocal(a_oObj);
	}
	
	/** 스크롤 뷰 정규 위치를 반환한다 */
	public static Vector3 ExGetNormPos(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null && a_oViewport != null && a_oContents != null);
		return new Vector3(a_oSender.ExGetNormPosH(a_oViewport, a_oContents, a_stPos), a_oSender.ExGetNormPosV(a_oViewport, a_oContents, a_stPos), KCDefine.B_VAL_0_FLT);
	}

	/** 스크롤 뷰 수직 정규 위치를 반환한다 */
	public static float ExGetNormPosV(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null && a_oViewport != null && a_oContents != null);
		return Mathf.Clamp01((a_stPos.y - (a_oViewport.transform as RectTransform).rect.height) / ((a_oContents.transform as RectTransform).rect.height - (a_oViewport.transform as RectTransform).rect.height));
	}

	/** 스크롤 뷰 수평 정규 위치를 반환한다 */
	public static float ExGetNormPosH(this ScrollRect a_oSender, GameObject a_oViewport, GameObject a_oContents, Vector3 a_stPos) {
		CAccess.Assert(a_oSender != null && a_oViewport != null && a_oContents != null);
		return Mathf.Clamp01((a_stPos.x - (a_oViewport.transform as RectTransform).rect.width) / ((a_oContents.transform as RectTransform).rect.width - (a_oViewport.transform as RectTransform).rect.width));
	}

	/** 계층 경로를 반환한다 */
	public static string ExGetHierarchyPath(this GameObject a_oSender) {
		CAccess.Assert(a_oSender != null);

		var oParentList = a_oSender.ExGetParents();
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = oParentList.Count - KCDefine.B_VAL_1_INT; i >= 0; --i) {
			oStrBuilder.AppendFormat(KCDefine.B_TEXT_FMT_2_COMBINE, oParentList[i].name, (i <= KCDefine.B_VAL_0_INT) ? string.Empty : KCDefine.B_TOKEN_SPLASH);
		}

		return oStrBuilder.ToString();
	}

	/** 자식을 반환한다 */
	public static List<GameObject> ExGetChildren(this Scene a_stSender) {
		var oObjs = a_stSender.GetRootGameObjects();
		var oObjList = new List<GameObject>();

		// 객체가 존재 할 경우
		if(oObjs.ExIsValid()) {
			for(int i = 0; i < oObjs.Length; ++i) {
				oObjList.AddRange(oObjs[i].ExGetChildren());
			}
		}

		return oObjList;
	}

	/** 자식을 반환한다 */
	public static List<GameObject> ExGetChildren(this GameObject a_oSender, bool a_bIsIncludeSelf = true) {
		CAccess.Assert(a_oSender != null);
		return (a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants()).ToList();
	}

	/** 부모를 반환한다 */
	public static List<GameObject> ExGetParents(this GameObject a_oSender, bool a_bIsIncludeSelf = true) {
		CAccess.Assert(a_oSender != null);
		return (a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors()).ToList();
	}

	/** 크기 형식 문자열을 반환한다 */
	public static string ExGetSizeFmtStr(this string a_oSender, int a_nSize) {
		CAccess.Assert(a_oSender != null);
		return string.Format(KCDefine.B_TEXT_FMT_SIZE, a_nSize, a_oSender);
	}

	/** 색상 형식 문자열을 반환한다 */
	public static string ExGetColorFmtStr(this string a_oSender, Color a_stColor) {
		CAccess.Assert(a_oSender != null);
		return string.Format(KCDefine.B_TEXT_FMT_COLOR, ColorUtility.ToHtmlStringRGBA(a_stColor), a_oSender);
	}

	/** 활성화 여부를 변경한다 */
	public static void ExSetEnable(this Behaviour a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.enabled = a_bIsEnable;
		}
	}

	/** 활성화 여부를 변경한다 */
	public static void ExSetEnable(this LayoutGroup a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 레이아웃이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.enabled = a_bIsEnable;
			a_oSender.GetComponent<ContentSizeFitter>()?.ExSetEnable(a_bIsEnable, a_bIsEnableAssert);
		}
	}
	
	/** 상호 작용 여부를 변경한다 */
	public static void ExSetInteractable(this Selectable a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 선택자가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.interactable = a_bIsEnable;
		}
	}

	/** 상호 작용 여부를 변경한다 */
	public static void ExSetRaycastTarget(this Graphic a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 그래픽스가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.raycastTarget = a_bIsEnable;
		}
	}

	/** 너비를 변경한다 */
	public static void ExSetWidth(this LineRenderer a_oSender, float a_fWidth, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 라인이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.startWidth = a_fWidth;
			a_oSender.endWidth = a_fWidth;
		}
	}

	/** 태그를 변경한다 */
	public static void ExSetTag(this Component a_oSender, string a_oTag, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oTag != null));

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null && a_oTag != null && !a_oSender.CompareTag(a_oTag)) {
			a_oSender.tag = a_oTag;
		}
	}

	/** 텍스트를 변경한다 */
	public static void ExSetText(this Text a_oSender, string a_oStr, STFontSetInfo a_stFontSetInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()));

		// 텍스트가 존재 할 경우
		if(a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()) {
			a_oSender.text = a_oStr;
			a_oSender.font = Resources.Load<Font>(a_stFontSetInfo.m_oPath);
		}
	}

	/** 텍스트를 변경한다 */
	public static void ExSetText(this TMP_Text a_oSender, string a_oStr, STFontSetInfo a_stFontSetInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()));

		// 텍스트가 존재 할 경우
		if(a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()) {
			a_oSender.text = a_oStr;
			a_oSender.font = Resources.Load<TMP_FontAsset>(a_stFontSetInfo.m_oPath);
		}
	}

	/** 텍스트를 변경한다 */
	public static void ExSetText(this InputField a_oSender, string a_oStr, STFontSetInfo a_stFontSetInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()) {
			a_oSender.text = a_oStr;
			var oTexts = a_oSender.GetComponentsInChildren<Text>();

			for(int i = 0; i < oTexts.Length; ++i) {
				oTexts[i].font = Resources.Load<Font>(a_stFontSetInfo.m_oPath);
			}
		}
	}

	/** 텍스트를 변경한다 */
	public static void ExSetText(this TMP_InputField a_oSender, string a_oStr, STFontSetInfo a_stFontSetInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()));

		// 입력 필드가 존재 할 경우
		if(a_oSender != null && a_stFontSetInfo.m_oPath.ExIsValid()) {
			a_oSender.text = a_oStr;
			a_oSender.fontAsset = Resources.Load<TMP_FontAsset>(a_stFontSetInfo.m_oPath);

			var oTexts = a_oSender.GetComponentsInChildren<TMP_Text>();

			for(int i = 0; i < oTexts.Length; ++i) {
				oTexts[i].font = Resources.Load<TMP_FontAsset>(a_stFontSetInfo.m_oPath);
			}
		}
	}
	
	/** 위치를 설정한다 */
	public static void ExSetPositions(this LineRenderer a_oSender, List<Vector3> a_oPosList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oPosList != null));

		// 라인이 존재 할 경우
		if(a_oSender != null && a_oPosList != null) {
			a_oSender.positionCount = a_oPosList.Count;
			a_oSender.SetPositions(a_oPosList.ToArray());
		}
	}

	/** 일반 색상을 변경한다 */
	public static void ExSetNormColor(this Button a_oSender, Color a_stColor, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 버튼이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.colors = a_oSender.colors.ExGetNormColor(a_stColor);
		}
	}

	/** 선택 색상을 변경한다 */
	public static void ExSetSelColor(this Button a_oSender, Color a_stColor, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 버튼이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.colors = a_oSender.colors.ExGetSelColor(a_stColor);
		}
	}

	/** 비활성 색상을 변경한다 */
	public static void ExSetDisableColor(this Button a_oSender, Color a_stColor, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 버튼이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.colors = a_oSender.colors.ExGetDisableColor(a_stColor);
		}
	}

	/** 컬링 마스크를 변경한다 */
	public static void ExSetCullingMask(this Camera a_oSender, List<int> a_oLayerList, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oLayerList != null));

		// 카메라가 존재 할 경우
		if(a_oSender != null && a_oLayerList != null) {
			a_oSender.cullingMask = a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.cullingMask;
			a_oSender.cullingMask |= a_oLayerList.ExToBits();
		}
	}

	/** 컬링 마스크를 변경한다 */
	public static void ExSetCullingMask(this Light a_oSender, List<int> a_oLayerList, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oLayerList != null));

		// 광원이 존재 할 경우
		if(a_oSender != null && a_oLayerList != null) {
			a_oSender.cullingMask = a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.cullingMask;
			a_oSender.cullingMask |= a_oLayerList.ExToBits();
		}
	}

	/** 이벤트 마스크를 변경한다 */
	public static void ExSetEventMask(this PhysicsRaycaster a_oSender, List<int> a_oLayerList, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oLayerList != null));

		// 광선 추적이 존재 할 경우
		if(a_oSender != null && a_oLayerList != null) {
			var stLayerMask = a_oSender.eventMask.ExGetLayerVal(a_bIsReset ? KCDefine.B_VAL_0_INT : a_oSender.eventMask.value);
			stLayerMask.value |= a_oLayerList.ExToBits();
			
			a_oSender.eventMask = stLayerMask;
		}
	}

	/** 정렬 순서를 변경한다 */
	public static void ExSetSortingOrder(this Canvas a_oSender, STSortingOrderInfo a_stOrderInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()));

		// 캔버스가 존재 할 경우
		if(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()) {
			a_oSender.sortingOrder = a_stOrderInfo.m_nOrder;
			a_oSender.sortingLayerName = a_stOrderInfo.m_oLayer;
		}
	}

	/** 정렬 순서를 변경한다 */
	public static void ExSetSortingOrder(this Renderer a_oSender, STSortingOrderInfo a_stOrderInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()));

		// 렌더러가 존재 할 경우
		if(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()) {
			a_oSender.sortingOrder = a_stOrderInfo.m_nOrder;
			a_oSender.sortingLayerName = a_stOrderInfo.m_oLayer;
		}
	}

	/** 정렬 순서를 변경한다 */
	public static void ExSetSortingOrder(this ParticleSystem a_oSender, STSortingOrderInfo a_stOrderInfo, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()));

		// 파티클이 존재 할 경우
		if(a_oSender != null && a_stOrderInfo.m_oLayer.ExIsValid()) {
			a_oSender.GetComponent<ParticleSystemRenderer>()?.ExSetSortingOrder(a_stOrderInfo, a_bIsEnableAssert);
		}
	}

	/** 색상을 변경한다 */
	public static void ExSetStartColor(this ParticleSystem a_oSender, Color a_stMinColor, Color a_stMaxColor, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 파티클이 존재 할 경우
		if(a_oSender != null) {
			var oMainModule = a_oSender.main;
			oMainModule.startColor = new ParticleSystem.MinMaxGradient(a_stMinColor, a_stMaxColor);
		}
	}

	/** 비율을 변경한다 */
	public static void ExSetScale(this GameObject a_oSender, Vector3 a_stScale, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localScale = a_stScale;
		}
	}

	/** X 축 비율을 변경한다 */
	public static void ExSetScaleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetScale(new Vector3(a_fVal, a_oSender.transform.localScale.y, a_oSender.transform.localScale.z), a_bIsEnableAssert);
	}

	/** Y 축 비율을 변경한다 */
	public static void ExSetScaleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetScale(new Vector3(a_oSender.transform.localScale.x, a_fVal, a_oSender.transform.localScale.z), a_bIsEnableAssert);
	}

	/** Z 축 비율을 변경한다 */
	public static void ExSetScaleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetScale(new Vector3(a_oSender.transform.localScale.x, a_oSender.transform.localScale.y, a_fVal), a_bIsEnableAssert);
	}
	
	/** 월드 각도를 변경한다 */
	public static void ExSetWorldAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.eulerAngles = a_stAngle;
		}
	}

	/** 월드 X 축 각도를 변경한다 */
	public static void ExSetWorldAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldAngle(new Vector3(a_fVal, a_oSender.transform.eulerAngles.y, a_oSender.transform.eulerAngles.z), a_bIsEnableAssert);
	}
	
	/** 월드 Y 축 각도를 변경한다 */
	public static void ExSetWorldAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldAngle(new Vector3(a_oSender.transform.eulerAngles.x, a_fVal, a_oSender.transform.eulerAngles.z), a_bIsEnableAssert);
	}

	/** 월드 Z 축 각도를 변경한다 */
	public static void ExSetWorldAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldAngle(new Vector3(a_oSender.transform.eulerAngles.x, a_oSender.transform.eulerAngles.y, a_fVal), a_bIsEnableAssert);
	}

	/** 로컬 각도를 변경한다 */
	public static void ExSetLocalAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localEulerAngles = a_stAngle;
		}
	}

	/** 로컬 X 축 각도를 변경한다 */
	public static void ExSetLocalAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalAngle(new Vector3(a_fVal, a_oSender.transform.localEulerAngles.y, a_oSender.transform.localEulerAngles.z), a_bIsEnableAssert);
	}
	
	/** 로컬 Y 축 각도를 변경한다 */
	public static void ExSetLocalAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalAngle(new Vector3(a_oSender.transform.localEulerAngles.x, a_fVal, a_oSender.transform.localEulerAngles.z), a_bIsEnableAssert);
	}

	/** 로컬 Z 축 각도를 변경한다 */
	public static void ExSetLocalAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalAngle(new Vector3(a_oSender.transform.localEulerAngles.x, a_oSender.transform.localEulerAngles.y, a_fVal), a_bIsEnableAssert);
	}

	/** 월드 위치를 변경한다 */
	public static void ExSetWorldPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.position = a_stPos;
		}
	}

	/** 월드 X 축 위치를 변경한다 */
	public static void ExSetWorldPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldPos(new Vector3(a_fVal, a_oSender.transform.position.y, a_oSender.transform.position.z), a_bIsEnableAssert);
	}
	
	/** 월드 Y 축 위치를 변경한다 */
	public static void ExSetWorldPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldPos(new Vector3(a_oSender.transform.position.x, a_fVal, a_oSender.transform.position.z), a_bIsEnableAssert);
	}

	/** 월드 Z 축 위치를 변경한다 */
	public static void ExSetWorldPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetWorldPos(new Vector3(a_oSender.transform.position.x, a_oSender.transform.position.y, a_fVal), a_bIsEnableAssert);
	}

	/** 로컬 위치를 변경한다 */
	public static void ExSetLocalPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localPosition = a_stPos;
		}
	}

	/** 로컬 X 축 위치를 변경한다 */
	public static void ExSetLocalPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalPos(new Vector3(a_fVal, a_oSender.transform.localPosition.y, a_oSender.transform.localPosition.z), a_bIsEnableAssert);
	}
	
	/** 로컬 Y 축 위치를 변경한다 */
	public static void ExSetLocalPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalPos(new Vector3(a_oSender.transform.localPosition.x, a_fVal, a_oSender.transform.localPosition.z), a_bIsEnableAssert);
	}

	/** 로컬 Z 축 위치를 변경한다 */
	public static void ExSetLocalPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetLocalPos(new Vector3(a_oSender.transform.localPosition.x, a_oSender.transform.localPosition.y, a_fVal), a_bIsEnableAssert);
	}

	/** 크기 간격을 변경한다 */
	public static void ExSetSizeDelta(this GameObject a_oSender, Vector3 a_stSize, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && (a_oSender.transform as RectTransform) != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && (a_oSender.transform as RectTransform) != null) {
			(a_oSender.transform as RectTransform).sizeDelta = new Vector2(a_stSize.x, a_stSize.y);
		}
	}

	/** X 축 크기 간격을 변경한다 */
	public static void ExSetSizeDeltaX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetSizeDelta(new Vector2(a_fVal, (a_oSender.transform as RectTransform).sizeDelta.y), a_bIsEnableAssert);
	}

	/** Y 축 크기 간격을 변경한다 */
	public static void ExSetSizeDeltaY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetSizeDelta(new Vector2((a_oSender.transform as RectTransform).sizeDelta.x, a_fVal), a_bIsEnableAssert);
	}

	/** 앵커 위치를 변경한다 */
	public static void ExSetAnchorPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && (a_oSender.transform as RectTransform) != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && (a_oSender.transform as RectTransform) != null) {
			(a_oSender.transform as RectTransform).anchoredPosition = new Vector2(a_stPos.x, a_stPos.y);
		}
	}

	/** X 축 앵커 위치를 변경한다 */
	public static void ExSetAnchorPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetAnchorPos(new Vector2(a_fVal, (a_oSender.transform as RectTransform).anchoredPosition.y), a_bIsEnableAssert);
	}

	/** Y 축 앵커 위치를 변경한다 */
	public static void ExSetAnchorPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExSetAnchorPos(new Vector2((a_oSender.transform as RectTransform).anchoredPosition.x, a_fVal), a_bIsEnableAssert);
	}

	/** 스크롤 위치를 변경한다 */
	public static void ExSetScrollPos(this EnhancedScroller a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 스크롤러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.SetScrollPositionImmediately(Mathf.Clamp(a_fVal, KCDefine.B_VAL_0_FLT, a_oSender.ScrollSize));
		}
	}

	/** 델리게이트를 변경한다 */
	public static void ExSetDelegate(this EnhancedScroller a_oSender, IEnhancedScrollerDelegate a_oDelegate, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oDelegate != null));

		// 스크롤러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.Delegate = a_oDelegate;
		}
	}
	
	/** 데이터를 다시 로드한다 */
	public static void ExReloadData(this EnhancedScroller a_oSender, int a_nDataIdx, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 스크롤러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.ReloadData();
			a_oSender.ExSetScrollPos(a_oSender.GetScrollPositionForDataIndex(a_nDataIdx, EnhancedScroller.CellViewPositionEnum.Before), a_bIsEnableAssert);
		}
	}

	/** 캔버스 월드 위치를 반환한다 */
	private static Vector3 ExGetWorldPos(this Vector3 a_stSender) {
		float fNormPosX = ((a_stSender.x * KCDefine.B_VAL_2_FLT) / CAccess.ScreenSize.x) - KCDefine.B_VAL_1_FLT;
		float fNormPosY = ((a_stSender.y * KCDefine.B_VAL_2_FLT) / CAccess.ScreenSize.y) - KCDefine.B_VAL_1_FLT;

		float fScreenWidth = KCDefine.B_SCREEN_HEIGHT * (CAccess.ScreenSize.x / CAccess.ScreenSize.y);
		return new Vector3(fNormPosX * (fScreenWidth / KCDefine.B_VAL_2_FLT), fNormPosY * (KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT), a_stSender.z) * KCDefine.B_UNIT_SCALE;
	}

	/** 보정된 캔버스 월드 위치를 반환한다 */
	private static Vector3 ExGetCorrectWorldPos(this Vector3 a_stSender) {
		var stMinPos = new Vector3(CAccess.Resolution.x / -KCDefine.B_VAL_2_FLT, CAccess.Resolution.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * KCDefine.B_UNIT_SCALE;
		var stMaxPos = new Vector3(CAccess.Resolution.x / KCDefine.B_VAL_2_FLT, CAccess.Resolution.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * KCDefine.B_UNIT_SCALE;

		return new Vector3(Mathf.Clamp(a_stSender.x, stMinPos.x, stMaxPos.x), Mathf.Clamp(a_stSender.y, stMinPos.y, stMaxPos.y), a_stSender.z);
	}
	
	/** 2 차원 => 3 차원으로 변환한다 */
	private static Vector3 ExTo3D(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_FLT) {
		return new Vector3(a_stSender.x, a_stSender.y, a_fZ);
	}

	/** 자식 객체를 탐색한다 */
	private static GameObject ExFindChild(this Scene a_stSender, string a_oName, bool a_bIsEnableSubName = false) {
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

	/** 자식 객체를 탐색한다 */
	private static GameObject ExFindChild(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.Equals(a_oName);
			
			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.Contains(a_oName))) {
				return oObj;
			}
		}

		return null;
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 텍스트를 변경한다 */
	public static void ExSetText<T>(this object a_oSender, string a_oStr, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);
		a_oSender?.ExSetPropertyVal<T>(KCDefine.U_PROPERTY_N_TEXT, KCDefine.B_BINDING_F_PUBLIC_INSTANCE, a_oStr, a_bIsEnableAssert);
	}

	/** 색상을 변경한다 */
	public static void ExSetColor<T>(this object a_oSender, Color a_stColor, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);
		a_oSender?.ExSetPropertyVal<T>(KCDefine.U_PROPERTY_N_COLOR, KCDefine.B_BINDING_F_PUBLIC_INSTANCE, a_stColor, a_bIsEnableAssert);
	}

	/** 스프라이트를 변경한다 */
	public static void ExSetSprite<T>(this object a_oSender, Sprite a_oSprite, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);
		a_oSender?.ExSetPropertyVal<T>(KCDefine.U_PROPERTY_N_SPRITE, KCDefine.B_BINDING_F_PUBLIC_INSTANCE, a_oSprite, a_bIsEnableAssert);
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponent<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			(a_oSender.GetComponentInChildren<T>() as Behaviour)?.ExSetEnable(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponent<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetEnableComponent<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponent<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetEnableComponent<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponents<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			var oComponents = a_oSender.GetComponentsInChildren<T>();

			for(int i = 0; i < oComponents.Length; ++i) {
				(oComponents[i] as Behaviour)?.ExSetEnable(a_bIsEnable, a_bIsEnableAssert);
			}
		}
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponents<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetEnableComponents<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 활성 여부를 변경한다 */
	public static void ExSetEnableComponents<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetEnableComponents<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 상호 작용 여부를 변경한다 */
	public static void ExSetInteractable<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.GetComponentInChildren<T>()?.ExSetInteractable(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 상호 작용 여부를 변경한다 */
	public static void ExSetInteractable<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetInteractable<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 상호 작용 여부를 변경한다 */
	public static void ExSetInteractable<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null && a_oName.ExIsValid());

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetInteractable<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTarget<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.GetComponentInChildren<T>()?.ExSetRaycastTarget(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTarget<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetRaycastTarget<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTarget<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null && a_oName.ExIsValid());

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetRaycastTarget<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetInteractables<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			var oComponents = a_oSender.GetComponentsInChildren<T>();

			for(int i = 0; i < oComponents.Length; ++i) {
				oComponents[i]?.ExSetInteractable(a_bIsEnable, a_bIsEnableAssert);
			}
		}
	}

	/** 컴포넌트 상호 작용 여부를 변경한다 */
	public static void ExSetInteractables<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetInteractables<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 상호 작용 여부를 변경한다 */
	public static void ExSetInteractables<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Selectable {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetInteractables<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}
	
	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTargets<T>(this GameObject a_oSender, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			var oComponents = a_oSender.GetComponentsInChildren<T>();

			for(int i = 0; i < oComponents.Length; ++i) {
				oComponents[i]?.ExSetRaycastTarget(a_bIsEnable, a_bIsEnableAssert);
			}
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTargets<T>(this Scene a_stSender, string a_oName, bool a_bIsEnable, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || a_oName.ExIsValid());

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSetRaycastTargets<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트 광선 추적 타겟 여부를 변경한다 */
	public static void ExSetRaycastTargets<T>(this GameObject a_oSender, string a_oName, bool a_bIsEnable, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Graphic {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf)?.ExSetRaycastTargets<T>(a_bIsEnable, a_bIsEnableAssert);
		}
	}
	#endregion			// 제네릭 클래스 함수
	
	#region 조건부 클래스 함수
#if UNITY_EDITOR
	/** 스크립트 순서를 변경한다 */
	public static void ExSetScriptOrder(this MonoBehaviour a_oSender, int a_nOrder, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null) {
			CAccess.SetScriptOrder(MonoScript.FromMonoBehaviour(a_oSender), a_nOrder, a_bIsEnableAssert);
		}
	}
#endif			// #if UNITY_EDITOR

#if ADS_MODULE_ENABLE
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EAdsPlatform a_eSender) {
		return a_eSender > EAdsPlatform.NONE && a_eSender < EAdsPlatform.MAX_VAL;
	}
#endif			// #if ADS_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this EPurchasePlatform a_eSender) {
		return a_eSender > EPurchasePlatform.NONE && a_eSender < EPurchasePlatform.MAX_VAL;
	}
#endif			// #if PURCHASE_MODULE_ENABLE

#if UNITY_IOS && NOTI_MODULE_ENABLE
	/** 인증 옵션 유효 여부를 검사한다 */
	public static bool ExIsValidAuthOpts(this AuthorizationOption a_eSender) {
		int nSumVal = KCDefine.B_VAL_0_INT;

		for(int i = 0; i < CAccessExtension.m_oAuthOptList.Count; ++i) {
			nSumVal += (int)(a_eSender & CAccessExtension.m_oAuthOptList[i]);
		}

		return nSumVal != KCDefine.B_VAL_0_INT;
	}
#endif			// #if UNITY_IOS && NOTI_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
