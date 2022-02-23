using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Linq;
using DG.Tweening;
using MessagePack;
using Coffee.UIExtensions;
using DanielLochner.Assets.SimpleScrollSnap;

#if NEWTON_SOFT_JSON_MODULE_ENABLE
using Newtonsoft.Json;
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE

/** 유틸리티 확장 클래스 */
public static partial class CExtension {
	#region 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset(this Text a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 텍스트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.resizeTextMinSize = KCDefine.U_DEF_MIN_SIZE_FONT;
			a_oSender.resizeTextMaxSize = KCDefine.U_DEF_MAX_SIZE_FONT;

			// 컨텐츠 크기 조정자가 없을 경우
			if(!a_oSender.gameObject.TryGetComponent<ContentSizeFitter>(out ContentSizeFitter oSizeFitter)) {
				oSizeFitter = a_oSender.gameObject.ExAddComponent<ContentSizeFitter>();
				oSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				oSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this TMP_Text a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// TMP 텍스트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.fontSizeMin = KCDefine.U_DEF_MIN_SIZE_FONT;
			a_oSender.fontSizeMax = KCDefine.U_DEF_MAX_SIZE_FONT;
			
			// 컨텐츠 크기 조정자가 없을 경우
			if(!a_oSender.gameObject.TryGetComponent<ContentSizeFitter>(out ContentSizeFitter oSizeFitter)) {
				oSizeFitter = a_oSender.gameObject.ExAddComponent<ContentSizeFitter>();
				oSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				oSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Image a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 이미지가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.pixelsPerUnitMultiplier = KCDefine.B_VAL_1_FLT;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Selectable a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 상호 작용자가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.colors = a_oSender.colors.ExGetNormColor(KCDefine.U_COLOR_NORM);
			a_oSender.colors = a_oSender.colors.ExGetPressColor(KCDefine.U_COLOR_PRESS);
			a_oSender.colors = a_oSender.colors.ExGetSelColor(KCDefine.U_COLOR_SEL);
			a_oSender.colors = a_oSender.colors.ExGetHighlightColor(KCDefine.U_COLOR_HIGHLIGHT);
			a_oSender.colors = a_oSender.colors.ExGetDisableColor(KCDefine.U_COLOR_DISABLE);
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this ScrollRect a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 스크롤 영역이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.scrollSensitivity = KCDefine.U_UNIT_SCROLL_SENSITIVITY;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this CanvasRenderer a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 캔버스 렌더러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.cullTransparentMesh = true;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this HorizontalOrVerticalLayoutGroup a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 레이아웃 그룹이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.childScaleWidth = false;
			a_oSender.childScaleHeight = false;

			a_oSender.childControlWidth = a_oSender as VerticalLayoutGroup != null;
			a_oSender.childControlHeight = a_oSender as HorizontalLayoutGroup != null;

			a_oSender.childForceExpandWidth = a_oSender as VerticalLayoutGroup != null;
			a_oSender.childForceExpandHeight = a_oSender as HorizontalLayoutGroup != null;

			// 부모 레이아웃 그룹이 없을 경우
			if(!a_oSender.transform.parent.TryGetComponent<HorizontalOrVerticalLayoutGroup>(out HorizontalOrVerticalLayoutGroup oLayoutGroup)) {
				a_oSender.gameObject.ExSetSizeDelta(Vector3.zero, a_bIsEnableAssert);
			}

			// 컨텐츠 크기 조정자가 존재 할 경우
			if(a_oSender.TryGetComponent<ContentSizeFitter>(out ContentSizeFitter oSizeFitter)) {
				oSizeFitter.verticalFit = (a_oSender as VerticalLayoutGroup != null) ? ContentSizeFitter.FitMode.PreferredSize : oSizeFitter.verticalFit;
				oSizeFitter.horizontalFit = (a_oSender as HorizontalLayoutGroup != null) ? ContentSizeFitter.FitMode.PreferredSize : oSizeFitter.horizontalFit;
			}
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Camera a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 카메라가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.allowHDR = true;
			a_oSender.useOcclusionCulling = true;
			a_oSender.usePhysicalProperties = false;
			a_oSender.allowDynamicResolution = false;

			a_oSender.targetDisplay = KCDefine.B_VAL_0_INT;
			a_oSender.stereoTargetEye = StereoTargetEyeMask.None;

			a_oSender.rect = KCDefine.U_RECT_CAMERA;
			a_oSender.farClipPlane = KCDefine.U_DISTANCE_CAMERA_FAR_PLANE;
			a_oSender.nearClipPlane = KCDefine.U_DISTANCE_CAMERA_NEAR_PLANE;

#if MSAA_ENABLE
			a_oSender.allowMSAA = CAccess.IsEnableMSAA;
#else
			a_oSender.allowMSAA = false;
#endif			// #if MSAA_ENABLE
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this Renderer a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 렌더러가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.receiveShadows = true;
			a_oSender.staticShadowCaster = true;
			a_oSender.allowOcclusionWhenDynamic = true;

			a_oSender.shadowCastingMode = ShadowCastingMode.On;
		}
	}

	/** 상태를 리셋한다 */
	public static void ExReset(this LineRenderer a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 라인이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.alignment = LineAlignment.TransformZ;
		}
	}
	
	/** 상태를 리셋한다 */
	public static void ExReset(this GameObject a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localScale = Vector3.one;
			a_oSender.transform.localEulerAngles = Vector3.zero;
			a_oSender.transform.localPosition = Vector3.zero;
		}
	}

	/** 비율을 추가한다 */
	public static void ExAddScale(this GameObject a_oSender, Vector3 a_stScale, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localScale += a_stScale;
		}
	}

	/** X 축 비율을 추가한다 */
	public static void ExAddScaleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddScale(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** Y 축 비율을 추가한다 */
	public static void ExAddScaleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddScale(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** Z 축 비율을 추가한다 */
	public static void ExAddScaleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddScale(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal), a_bIsEnableAssert);
	}

	/** 월드 각도를 추가한다 */
	public static void ExAddWorldAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.eulerAngles += a_stAngle;
		}
	}

	/** 월드 X 축 각도를 추가한다 */
	public static void ExAddWorldAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}
	
	/** 월드 Y 축 각도를 추가한다 */
	public static void ExAddWorldAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 월드 Z 축 각도를 추가한다 */
	public static void ExAddWorldAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAngle(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal), a_bIsEnableAssert);
	}

	/** 로컬 각도를 추가한다 */
	public static void ExAddLocalAngle(this GameObject a_oSender, Vector3 a_stAngle, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localEulerAngles += a_stAngle;
		}
	}

	/** 로컬 X 축 각도를 추가한다 */
	public static void ExAddLocalAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}
	
	/** 로컬 Y 축 각도를 추가한다 */
	public static void ExAddLocalAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 로컬 Z 축 각도를 추가한다 */
	public static void ExAddLocalAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAngle(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal), a_bIsEnableAssert);
	}

	/** 월드 각도를 추가한다 */
	public static void ExAddWorldAxisAngle(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.rotation = Quaternion.AngleAxis(a_fVal, a_stAxis.normalized) * a_oSender.transform.rotation;
		}
	}

	/** 월드 X 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.right, a_bIsEnableAssert);
	}
	
	/** 월드 Y 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.up, a_bIsEnableAssert);
	}

	/** 월드 Z 축 각도를 추가한다 */
	public static void ExAddWorldAxisAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisAngle(a_fVal, Vector3.forward, a_bIsEnableAssert);
	}

	/** 로컬 각도를 추가한다 */
	public static void ExAddLocalAxisAngle(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localRotation = Quaternion.AngleAxis(a_fVal, a_stAxis) * a_oSender.transform.localRotation;
		}
	}

	/** 로컬 X 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.right, a_bIsEnableAssert);
	}
	
	/** 로컬 Y 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.up, a_bIsEnableAssert);
	}

	/** 로컬 Z 축 각도를 추가한다 */
	public static void ExAddLocalAxisAngleZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisAngle(a_fVal, a_oSender.transform.forward, a_bIsEnableAssert);
	}

	/** 월드 위치를 추가한다 */
	public static void ExAddWorldPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.position += a_stPos;
		}
	}

	/** 월드 X 축 위치를 추가한다 */
	public static void ExAddWorldPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}
	
	/** 월드 Y 축 위치를 추가한다 */
	public static void ExAddWorldPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 월드 Z 축 위치를 추가한다 */
	public static void ExAddWorldPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldPos(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal), a_bIsEnableAssert);
	}

	/** 로컬 위치를 추가한다 */
	public static void ExAddLocalPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localPosition += a_stPos;
		}
	}

	/** 로컬 X 축 위치를 추가한다 */
	public static void ExAddLocalPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}
	
	/** 로컬 Y 축 위치를 추가한다 */
	public static void ExAddLocalPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 로컬 Z 축 위치를 추가한다 */
	public static void ExAddLocalPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalPos(new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal), a_bIsEnableAssert);
	}

	/** 월드 위치를 추가한다 */
	public static void ExAddWorldAxisPos(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.position += a_stAxis.normalized * a_fVal;
		}
	}

	/** 월드 X 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.right, a_bIsEnableAssert);
	}

	/** 월드 Y 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.up, a_bIsEnableAssert);
	}

	/** 월드 Z 축 위치를 추가한다 */
	public static void ExAddWorldAxisPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddWorldAxisPos(a_fVal, Vector3.forward, a_bIsEnableAssert);
	}

	/** 로컬 위치를 추가한다 */
	public static void ExAddLocalAxisPos(this GameObject a_oSender, float a_fVal, Vector3 a_stAxis, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.transform.localPosition += a_stAxis.normalized * a_fVal;
		}
	}

	/** 로컬 X 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.right);
	}

	/** 로컬 Y 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.up);
	}

	/** 로컬 Z 축 위치를 추가한다 */
	public static void ExAddLocalAxisPosZ(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddLocalAxisPos(a_fVal, a_oSender.transform.forward);
	}

	/** 크기 간격을 추가한다 */
	public static void ExAddSizeDelta(this GameObject a_oSender, Vector3 a_stSize, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && (a_oSender.transform as RectTransform) != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && (a_oSender.transform as RectTransform) != null) {
			(a_oSender.transform as RectTransform).sizeDelta += new Vector2(a_stSize.x, a_stSize.y);
		}
	}

	/** X 축 크기 간격을 추가한다 */
	public static void ExAddSizeDeltaX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddSizeDelta(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** Y 축 크기 간격을 추가한다 */
	public static void ExAddSizeDeltaY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddSizeDelta(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 앵커 위치를 추가한다 */
	public static void ExAddAnchorPos(this GameObject a_oSender, Vector3 a_stPos, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && (a_oSender.transform as RectTransform) != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && (a_oSender.transform as RectTransform) != null) {
			(a_oSender.transform as RectTransform).anchoredPosition += new Vector2(a_stPos.x, a_stPos.y);
		}
	}

	/** X 축 앵커 위치를 추가한다 */
	public static void ExAddAnchorPosX(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddAnchorPos(new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** Y 축 앵커 위치를 추가한다 */
	public static void ExAddAnchorPosY(this GameObject a_oSender, float a_fVal, bool a_bIsEnableAssert = true) {
		a_oSender?.ExAddAnchorPos(new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT), a_bIsEnableAssert);
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Button a_oSender, UnityAction a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onClick.RemoveAllListeners();
			}

			a_oSender.onClick.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Slider a_oSender, UnityAction<float> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this Dropdown a_oSender, UnityAction<int> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this TMP_Dropdown a_oSender, UnityAction<int> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 리스너를 추가한다 */
	public static void ExAddListener(this TMP_InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onValueChanged.RemoveAllListeners();
			}

			a_oSender.onValueChanged.AddListener(a_oCallback);
		}
	}

	/** 편집 종료 리스너를 추가한다 */
	public static void ExAddEndEditListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onEndEdit.RemoveAllListeners();
			}

			a_oSender.onEndEdit.AddListener(a_oCallback);
		}
	}

	/** 편집 종료 리스너를 추가한다 */
	public static void ExAddEndEditListener(this TMP_InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 버튼이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onEndEdit.RemoveAllListeners();
			}

			a_oSender.onEndEdit.AddListener(a_oCallback);
		}
	}

	/** 페이지를 제거한다 */
	public static void ExRemovePages(this SimpleScrollSnap a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 스크롤 스냅이 존재 할 경우
		if(a_oSender != null) {
			while(a_oSender.NumberOfPanels > KCDefine.B_VAL_0_INT) {
				GameObject.DestroyImmediate(a_oSender.Pagination.transform.GetChild(a_oSender.NumberOfPanels - KCDefine.B_VAL_1_INT).gameObject);
				a_oSender.RemoveFromBack();
			}
		}
	}

	/** 효과를 재생한다 */
	public static void ExPlay(this AudioSource a_oSender, AudioClip a_oClip, bool a_bIsLoop, bool a_bIs3DSnd, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oClip != null));

		// 오디오 소스가 존재 할 경우
		if(a_oSender != null && a_oClip != null) {
			a_oSender.clip = a_oClip;
			a_oSender.loop = a_bIsLoop;

			a_oSender.spread = a_bIs3DSnd ? KCDefine.B_ANGLE_360_DEG : KCDefine.B_ANGLE_0_DEG;
			a_oSender.dopplerLevel = a_bIs3DSnd ? KCDefine.B_VAL_1_FLT : KCDefine.B_VAL_0_FLT;
			a_oSender.spatialBlend = a_bIs3DSnd ? KCDefine.B_VAL_1_FLT : KCDefine.B_VAL_0_FLT;
			a_oSender.reverbZoneMix = a_bIs3DSnd ? KCDefine.B_VAL_1_FLT : KCDefine.B_VAL_0_FLT;

			a_oSender.Play();
		}
	}

	/** 효과를 재생한다 */
	public static void ExPlay(this ParticleSystem a_oSender, bool a_bIsRemoveChildren = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 파티클이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.Stop(a_bIsRemoveChildren);
			a_oSender.Play(true);
		}
	}

	/** 효과를 재생한다 */
	public static Tween ExPlay(this CFXBase a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		a_oSender.effectFactor = a_fStartVal;

		return DOTween.To(() => a_oSender.effectFactor, (a_fVal) => a_oSender.effectFactor = a_fVal, a_fEndVal, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 효과를 재생한다 */
	public static Sequence ExPlay(this CFXBase a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<CFXBase, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.ExPlay(a_fStartVal, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 게이지 애니메이션을 시작한다 */
	public static Tween ExStartGaugeAni(this Image a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		a_oSender.fillAmount = a_fStartVal;

		return DOTween.To(() => a_oSender.fillAmount, (a_fVal) => a_oSender.fillAmount = a_fVal, a_fEndVal, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 게이지 애니메이션을 시작한다 */
	public static Sequence ExStartGaugeAni(this Image a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<Image, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.ExStartGaugeAni(a_fStartVal, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}
	
	/** 비율 애니메이션을 시작한다 */
	public static Tween ExStartScaleAni(this GameObject a_oSender, Vector3 a_stScale, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.transform.DOScale(a_stScale, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 비율 애니메이션을 시작한다 */
	public static Sequence ExStartScaleAni(this GameObject a_oSender, Vector3 a_stScale, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.ExStartScaleAni(a_stScale, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 월드 이동 애니메이션을 시작한다 */
	public static Tween ExStartWorldMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.transform.DOMove(a_stPos, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 월드 이동 애니메이션을 시작한다 */
	public static Sequence ExStartWorldMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.ExStartWorldMoveAni(a_stPos, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 로컬 이동 애니메이션을 시작한다 */
	public static Tween ExStartLocalMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		return a_oSender.transform.DOLocalMove(a_stPos, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 로컬 이동 애니메이션을 시작한다 */
	public static Sequence ExStartLocalMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		return CFactory.MakeSequence(a_oSender.ExStartLocalMoveAni(a_stPos, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 월드 경로 애니메이션을 시작한다 */
	public static Tween ExStartWorldPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		var ePathType = a_bIsLinear ? PathType.Linear : PathType.CatmullRom;

		return a_oSender.transform.DOPath(a_oPosList.ToArray(), a_fDuration, ePathType).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 월드 경로 애니메이션을 시작한다 */
	public static Tween ExStartWorldPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		return CFactory.MakeSequence(a_oSender.ExStartWorldPathAni(a_oPosList, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 로컬 경로 애니메이션을 시작한다 */
	public static Tween ExStartLocalPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		var ePathType = a_bIsLinear ? PathType.Linear : PathType.CatmullRom;

		return a_oSender.transform.DOLocalPath(a_oPosList.ToArray(), a_fDuration, ePathType).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 로컬 경로 애니메이션을 시작한다 */
	public static Tween ExStartLocalPathAni(this GameObject a_oSender, List<Vector3> a_oPosList, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null && a_oPosList != null);
		return CFactory.MakeSequence(a_oSender.ExStartLocalPathAni(a_oPosList, a_fDuration, a_eEase, a_bIsRealtime), (a_oSequenceSender) => a_oCallback?.Invoke(a_oSender, a_oSequenceSender), a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	/** 종류 => 타입으로 변환한다 */
	public static int ExKindsToType(this int a_nSender) {
		return a_nSender / KCDefine.B_UNIT_KINDS_PER_TYPE;
	}
	
	/** 종류 => 기본 종류로 변환한다 */
	public static int ExKindsToBaseKinds(this int a_nSender) {
		return a_nSender.ExKindsToType() * KCDefine.B_UNIT_KINDS_PER_TYPE;
	}

	/** 종류 => 종류 타입으로 변환한다 */
	public static int ExKindsToKindsType(this int a_nSender) {
		int nKindsType = a_nSender % KCDefine.B_UNIT_KINDS_PER_TYPE;
		return a_nSender.ExKindsToBaseKinds() + ((nKindsType / KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE) * KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE);
	}

	/** 종류 => 서브 종류 타입으로 변환한다 */
	public static int ExKindsToSubKindsType(this int a_nSender) {
		int nSubKindsType = a_nSender % KCDefine.B_UNIT_KINDS_PER_KINDS_TYPE;
		return a_nSender.ExKindsToKindsType() + ((nSubKindsType / KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE) * KCDefine.B_UNIT_KINDS_PER_SUB_KINDS_TYPE);
	}

	/** 고유 레벨 식별자 => 식별자로 변환한다 */
	public static int ExUniqueLevelIDToID(this long a_nSender) {
		return (int)(a_nSender % KCDefine.B_UNIT_IDS_PER_STAGE);
	}

	/** 고유 레벨 식별자 => 스테이지 식별자로 변환한다 */
	public static int ExUniqueLevelIDToStageID(this long a_nSender) {
		return (int)((a_nSender % KCDefine.B_UNIT_IDS_PER_CHAPTER) / KCDefine.B_UNIT_IDS_PER_STAGE);
	}

	/** 고유 레벨 식별자 => 챕터 식별자로 변환한다 */
	public static int ExUniqueLevelIDToChapterID(this long a_nSender) {
		return (int)(a_nSender / KCDefine.B_UNIT_IDS_PER_CHAPTER);
	}
	
	/** 위치 => 인덱스로 변환한다 */
	public static Vector2Int ExToIdx(this Vector2 a_stSender, Vector2 a_stPivotPos, Vector2 a_stSize) {
		CAccess.Assert(a_stSize.x.ExIsGreateEquals(KCDefine.B_VAL_0_FLT) && a_stSize.y.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		var stDelta = a_stSender - a_stPivotPos;

		return new Vector2Int((int)(stDelta.x / a_stSize.x), (int)(stDelta.y / -a_stSize.y));
	}

	/** 위치 => 인덱스로 변환한다 */
	public static Vector3Int ExToIdx(this Vector3 a_stSender, Vector3 a_stPivotPos, Vector3 a_stSize) {
		CAccess.Assert(a_stSize.x.ExIsGreateEquals(KCDefine.B_VAL_0_FLT) && a_stSize.y.ExIsGreateEquals(KCDefine.B_VAL_0_FLT) && a_stSize.z.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		var stDelta = a_stSender - a_stPivotPos;

		return new Vector3Int((int)(stDelta.x / a_stSize.x), (int)(stDelta.y / -a_stSize.y), (int)(stDelta.z / -a_stSize.z));
	}

	/** 인덱스 정보 => 인덱스로 변환한다 */
	public static Vector3Int ExToIdx(this STIdxInfo a_stSender) {
		return new Vector3Int(a_stSender.m_nX, a_stSender.m_nY, a_stSender.m_nZ);
	}

	/** 인덱스 => 위치로 변환한다 */
	public static Vector2 ExToPos(this Vector2Int a_stSender, Vector2 a_stOffset, Vector2 a_stSize) {
		return new Vector2((a_stSender.x * a_stSize.x) + a_stOffset.x, (a_stSender.y * -a_stSize.y) + a_stOffset.y);
	}

	/** 인덱스 => 위치로 변환한다 */
	public static Vector3 ExToPos(this Vector3Int a_stSender, Vector3 a_stOffset, Vector3 a_stSize) {
		return new Vector3((a_stSender.x * a_stSize.x) + a_stOffset.x, (a_stSender.y * -a_stSize.y) + a_stOffset.y, (a_stSender.z * -a_stSize.z) + a_stOffset.z);
	}

	/** 인덱스 => 인덱스 정보로 변환한다 */
	public static STIdxInfo ExToIdxInfo(this Vector3Int a_stSender) {
		return CFactory.MakeIdxInfo(a_stSender.x, a_stSender.y, a_stSender.z);
	}

	/** 인덱스 정보 => 인덱스로 변환한다 */
	public static List<Vector3Int> ExToIndices(this List<STIdxInfo> a_oSender) {
		CAccess.Assert(a_oSender != null);
		var oIdxList = new List<Vector3Int>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oIdxList.Add(a_oSender[i].ExToIdx());
		}

		return oIdxList;
	}

	/** 인덱스 => 위치로 변환한다 */
	public static List<Vector2> ExToPositions(this List<Vector2Int> a_oSender, Vector2 a_stOffset, Vector2 a_stSize) {
		CAccess.Assert(a_oSender != null);
		var oPosList = new List<Vector2>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oPosList.Add(a_oSender[i].ExToPos(a_stOffset, a_stSize));
		}

		return oPosList;
	}

	/** 인덱스 => 위치로 변환한다 */
	public static List<Vector3> ExToPositions(this List<Vector3Int> a_oSender, Vector3 a_stOffset, Vector3 a_stSize) {
		CAccess.Assert(a_oSender != null);
		var oPosList = new List<Vector3>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oPosList.Add(a_oSender[i].ExToPos(a_stOffset, a_stSize));
		}

		return oPosList;
	}
	
	/** 인덱스 => 인덱스 정보로 변환한다 */
	public static List<STIdxInfo> ExToIdxInfos(this List<Vector3Int> a_oSender) {
		CAccess.Assert(a_oSender != null);
		var oIdxInfoList = new List<STIdxInfo>();

		for(int i = 0; i < a_oSender.Count; ++i) {
			oIdxInfoList.Add(a_oSender[i].ExToIdxInfo());
		}

		return oIdxInfoList;
	}
	
	/** 3 차원 => 2 차원으로 변환한다 */
	public static Vector2 ExTo2D(this Vector3 a_stSender) {
		return new Vector2(a_stSender.x, a_stSender.y);
	}

	/** 3 차원 => 2 차원으로 변환한다 */
	public static Vector2Int ExTo2D(this Vector3Int a_stSender) {
		return new Vector2Int(a_stSender.x, a_stSender.y);
	}

	/** 2 차원 => 3 차원으로 변환한다 */
	public static Vector3 ExTo3D(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_FLT) {
		return new Vector3(a_stSender.x, a_stSender.y, a_fZ);
	}

	/** 2 차원 => 3 차원으로 변환한다 */
	public static Vector3Int ExTo3D(this Vector2Int a_stSender, int a_nZ = KCDefine.B_VAL_0_INT) {
		return new Vector3Int(a_stSender.x, a_stSender.y, a_nZ);
	}

	/** 문자열 => Base64 문자열로 변환한다 */
	public static string ExToBase64Str(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		return System.Convert.ToBase64String((a_oEncoding ?? System.Text.Encoding.Default).GetBytes(a_oSender));
	}

	/** 문자열 => 압축 된 문자열로 변환한다 */
	public static string ExToCompressStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());

		using(var oMemoryStream = new MemoryStream()) {
			var oBytes = (a_oEncoding ?? System.Text.Encoding.Default).GetBytes(a_oSender);

			using(var oGZipStream = new GZipStream(oMemoryStream, CompressionMode.Compress, true)) {
				oGZipStream.Write(oBytes, KCDefine.B_VAL_0_INT, oBytes.Length);
				var oCompressBytes = new byte[oMemoryStream.Length];

				oMemoryStream.Seek(KCDefine.B_VAL_0_INT, SeekOrigin.Begin);
				oMemoryStream.Read(oCompressBytes, KCDefine.B_VAL_0_INT, (int)oMemoryStream.Length);

				var oBufferBytes = new byte[oCompressBytes.Length + sizeof(int)];

				System.Buffer.BlockCopy(oCompressBytes, KCDefine.B_VAL_0_INT, oBufferBytes, sizeof(int), oCompressBytes.Length);
				System.Buffer.BlockCopy(System.BitConverter.GetBytes(oBytes.Length), KCDefine.B_VAL_0_INT, oBufferBytes, KCDefine.B_VAL_0_INT, sizeof(int));

				return System.Convert.ToBase64String(oBufferBytes);
			}
		}
	}

	/** Base64 문자열 => 바이트로 변환한다 */
	public static byte[] ExBase64StrToBytes(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return System.Convert.FromBase64String(a_oSender);
	}

	/** Base64 문자열 => 문자열로 변환한다 */
	public static string ExBase64StrToStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		return (a_oEncoding ?? System.Text.Encoding.Default).GetString(System.Convert.FromBase64String(a_oSender));
	}

	/** 압축 된 문자열 => 문자열로 변환한다 */
	public static string ExCompressStrToStr(this string a_oSender, System.Text.Encoding a_oEncoding = null) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Convert.FromBase64String(a_oSender);

		using(var oMemoryStream = new MemoryStream(oBytes, sizeof(int), oBytes.Length - sizeof(int))) {
			int nLength = System.BitConverter.ToInt32(oBytes, KCDefine.B_VAL_0_INT);
			var oDecompressBytes = new byte[nLength];

			using(var oGZipStream = new GZipStream(oMemoryStream, CompressionMode.Decompress, true)) {
				oGZipStream.Read(oDecompressBytes, KCDefine.B_VAL_0_INT, oDecompressBytes.Length);
				return (a_oEncoding ?? System.Text.Encoding.Default).GetString(oDecompressBytes);
			}
		}
	}

	/** 자식 객체를 탐색한다 */
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

	/** 자식 객체를 탐색한다 */
	public static GameObject ExFindChild(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
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
	
	/** 자식 객체를 탐색한다 */
	public static List<GameObject> ExFindChildren(this Scene a_stSender, string a_oName, bool a_bIsEnableSubName = false) {
		var oObjs = a_stSender.GetRootGameObjects();
		var oObjList = new List<GameObject>();

		// 객체가 존재 할 경우
		if(oObjs.ExIsValid()) {
			for(int i = 0; i < oObjs.Length; ++i) {
				var oChildObjList = oObjs[i].ExFindChildren(a_oName, true, a_bIsEnableSubName);
				oObjList.AddRange(oChildObjList);
			}
		}

		return oObjList;
	}

	/** 자식 객체를 탐색한다 */
	public static List<GameObject> ExFindChildren(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.Equals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.Contains(a_oName))) {
				oObjList.ExAddVal(oObj);
			}
		}

		return oObjList;
	}

	/** 부모 객체를 탐색한다 */
	public static GameObject ExFindParent(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.Equals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.Contains(a_oName))) {
				return oObj;
			}
		}

		return null;
	}

	/** 부모 객체를 탐색한다 */
	public static List<GameObject> ExFindParents(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.Equals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.Contains(a_oName))) {
				oObjList.ExAddVal(oObj);
			}
		}

		return oObjList;
	}

	/** 함수를 지연 호출한다 */
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.StartCoroutine(a_oSender.ExDoLateCallFunc(a_oCallback));
		}
	}

	/** 함수를 지연 호출한다 */
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback, float a_fDelay, bool a_bIsRealtime = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.StartCoroutine(a_oSender.ExDoLateCallFunc(a_oCallback, a_fDelay, a_bIsRealtime));
		}
	}

	/** 함수를 반복 호출한다 */
	public static void ExRepeatCallFunc(this MonoBehaviour a_oSender, System.Func<MonoBehaviour, bool, bool> a_oCallback, float a_fDeltaTime, float a_fMaxDeltaTime, bool a_bIsRealtime = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 컴포넌트가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.StartCoroutine(a_oSender.ExDoRepeatCallFunc(a_oCallback, a_fDeltaTime, a_fMaxDeltaTime, a_bIsRealtime));
		}
	}

	/** 메세지를 전송한다 */
	public static void ExSendMsg(this Scene a_stSender, string a_oName, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oName.ExIsValid() && a_oMsg.ExIsValid()));

		// 이름이 유효 할 경우
		if(a_oName.ExIsValid() && a_oMsg.ExIsValid()) {
			a_stSender.ExFindChild(a_oName)?.ExSendMsg(a_oMsg, a_oParams, a_bIsEnableAssert);
		}
	}

	/** 메세지를 전송한다 */
	public static void ExSendMsg(this GameObject a_oSender, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oMsg.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.SendMessage(a_oMsg, a_oParams, SendMessageOptions.DontRequireReceiver);
		}
	}

	/** 메세지를 전송한다 */
	public static void ExSendMsg(this GameObject a_oSender, string a_oName, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid() && a_oMsg.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && (a_oName.ExIsValid() && a_oMsg.ExIsValid())) {
			a_oSender.ExFindChild(a_oName)?.ExSendMsg(a_oMsg, a_oParams, a_bIsEnableAssert);
		}
	}

	/** 메세지를 전파한다 */
	public static void ExBroadcastMsg(this Scene a_stSender, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oMsg.ExIsValid());

		// 메세지가 유효 할 경우
		if(a_oMsg.ExIsValid()) {
			var oObjs = a_stSender.GetRootGameObjects();

			for(int i = 0; i < oObjs.Length; ++i) {
				oObjs[i].ExBroadcastMsg(a_oMsg, a_oParams, a_bIsEnableAssert);
			}
		}
	}

	/** 메세지를 전파한다 */
	public static void ExBroadcastMsg(this GameObject a_oSender, string a_oMsg, object a_oParams, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oMsg.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oMsg.ExIsValid()) {
			a_oSender.BroadcastMessage(a_oMsg, a_oParams, SendMessageOptions.DontRequireReceiver);
		}
	}

	/** 함수를 지연 호출한다 */
	private static IEnumerator ExDoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback) {
		CAccess.Assert(a_oSender != null);
		yield return new WaitForEndOfFrame();

		a_oCallback?.Invoke(a_oSender);
	}

	/** 함수를 지연 호출한다 */
	private static IEnumerator ExDoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour> a_oCallback, float a_fDelay, bool a_bIsRealtime) {
		CAccess.Assert(a_oSender != null && a_fDelay.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		// 리얼 타임 모드 일 경우
		if(a_bIsRealtime) {
			yield return new WaitForSecondsRealtime(a_fDelay);
		} else {
			yield return new WaitForSeconds(a_fDelay);
		}

		a_oCallback?.Invoke(a_oSender);
	}

	/** 함수를 반복 호출한다 */
	private static IEnumerator ExDoRepeatCallFunc(this MonoBehaviour a_oSender, System.Func<MonoBehaviour, bool, bool> a_oCallback, float a_fDeltaTime, double a_dblMaxDeltaTime, bool a_bIsRealtime) {
		CAccess.Assert(a_oSender != null && a_oCallback != null && a_fDeltaTime.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var stStartTime = System.DateTime.Now;
		double dblDeltaTime = KCDefine.B_VAL_0_FLT;

		do {
			// 리얼 타임 모드 일 경우
			if(a_bIsRealtime) {
				yield return new WaitForSecondsRealtime(a_fDeltaTime);
			} else {
				yield return new WaitForSeconds(a_fDeltaTime);
			}
			
			dblDeltaTime = System.DateTime.Now.ExGetDeltaTime(stStartTime);
		} while(a_oCallback(a_oSender, false) && dblDeltaTime.ExIsLess(a_dblMaxDeltaTime));

		a_oCallback(a_oSender, true);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component {
		CAccess.Assert(a_oSender != null);
		return a_oSender.TryGetComponent<T>(out T oComponent) ? oComponent : a_oSender.AddComponent<T>();
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponent<T>(this GameObject a_oSender, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			CFactory.RemoveObj(a_oSender.GetComponentInChildren<T>(), false, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponents<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			a_oSender.ExEnumerateComponents<T>((a_oComponent) => {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != a_oComponent.gameObject) {
					CFactory.RemoveObj(a_oComponent, false, a_bIsEnableAssert);
				}

				return true;
			});
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponentInParent<T>(this GameObject a_oSender, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			CFactory.RemoveObj(a_oSender.GetComponentInParent<T>(), false, a_bIsEnableAssert);
		}
	}

	/** 컴포넌트를 제거한다 */
	public static void ExRemoveComponentsInParent<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true, bool a_bIsEnableAssert = true) where T : Component {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 객체가 존재 할 경우
		if(a_oSender != null) {
			var oComponents = a_oSender.GetComponentsInParent<T>();

			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != oComponents[i].gameObject) {
					CFactory.RemoveObj(oComponents[i], false, a_bIsEnableAssert);
				}
			}
		}
	}

	/** 객체 => JSON 문자열로 변환한다 */
	public static string ExToMsgPackJSONStr<T>(this T a_tSender) {
		return MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize<T>(a_tSender));
	}

	/** 객체 => Base64 문자열로 변환한다 */
	public static string ExToMsgPackBase64Str<T>(this T a_tSender) {
		return System.Convert.ToBase64String(MessagePackSerializer.Serialize<T>(a_tSender));
	}

	/** JSON 문자열 => 객체로 변환한다 */
	public static T ExMsgPackJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.ConvertFromJson(a_oSender));
	}

	/** Base64 문자열 => 객체로 변환한다 */
	public static T ExMsgPackBase64StrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return MessagePackSerializer.Deserialize<T>(System.Convert.FromBase64String(a_oSender));
	}

	/** 컴포넌트를 탐색한다 */
	public static T ExFindComponent<T>(this Scene a_stSender, string a_oName, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return a_stSender.ExFindChild(a_oName, a_bIsEnableSubName)?.GetComponentInChildren<T>(a_bIsIncludeInactive);
	}
	
	/** 컴포넌트를 탐색한다 */
	public static T ExFindComponent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		return a_oSender?.ExFindChild(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName)?.GetComponentInChildren<T>(a_bIsIncludeInactive);
	}

	/** 컴포넌트를 탐색한다 */
	public static List<T> ExFindComponents<T>(this Scene a_stSender, string a_oName, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return a_stSender.ExFindChild(a_oName, a_bIsEnableSubName)?.GetComponentsInChildren<T>(a_bIsIncludeInactive).ToList();
	}

	/** 컴포넌트를 탐색한다 */
	public static List<T> ExFindComponents<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		return a_oSender?.ExFindChild(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName)?.GetComponentsInChildren<T>(a_bIsIncludeInactive).ToList();
	}

	/** 부모 컴포넌트를 탐색한다 */
	public static T ExFindComponentInParent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		return a_oSender?.ExFindParent(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName)?.GetComponentInParent<T>(a_bIsIncludeInactive);
	}

	/** 부모 컴포넌트를 탐색한다 */
	public static List<T> ExFindComponentsInParent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsIncludeInactive = false, bool a_bIsEnableSubName = false) where T : Component {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		return a_oSender?.ExFindParent(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName)?.GetComponentsInParent<T>(a_bIsIncludeInactive).ToList();
	}

	/** 배열을 순회한다 */
	public static void ExEnumerate<T>(this T[,] a_oSender, System.Func<T, Vector2Int, bool> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 배열이 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
				for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
					var stIdx = new Vector2Int(j, i);
					
					// 배열 순회가 불가능 할 경우
					if(!a_oCallback(a_oSender[i, j], stIdx)) {
						goto EXIT_ENUMERATE;
					}
				}
			}
		}

EXIT_ENUMERATE:
		return;
	}

	/** 컴포넌트를 순회한다 */
	public static void ExEnumerateComponents<T>(this Scene a_stSender, System.Func<T, bool> a_oCallback, bool a_bIsIncludeInactive = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oCallback != null);

		// 콜백이 존재 할 경우
		if(a_oCallback != null) {
			var oObjs = a_stSender.GetRootGameObjects();

			for(int i = 0; i < oObjs.Length; ++i) {
				var oComponents = oObjs[i].GetComponentsInChildren<T>(a_bIsIncludeInactive);

				for(int j = 0; j < oComponents.Length; ++j) {
					// 순회가 불가능 할 경우
					if(!a_oCallback(oComponents[j])) {
						goto EXIT_ENUMERATE_COMPONENTS;
					}
				}
			}
		}

EXIT_ENUMERATE_COMPONENTS:
		return;
	}

	/** 컴포넌트를 순회한다 */
	public static void ExEnumerateComponents<T>(this GameObject a_oSender, System.Func<T, bool> a_oCallback, bool a_bIsIncludeInactive = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oCallback != null));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oCallback != null) {
			var oComponents = a_oSender.GetComponentsInChildren<T>(a_bIsIncludeInactive);

			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 순회가 불가능 할 경우
				if(!a_oCallback(oComponents[i])) {
					break;
				}
			}
		}
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if DOTWEEN_ENABLE
	/** 애니메이션을 재생한다 */
	public static void ExPlay(this Tween a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.Play();
		}
	}

	/** 애니메이션을 재생한다 */
	public static void ExPlay(this DOTweenAnimation a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.CreateTween();
			a_oSender.tween?.ExPlay(a_bIsEnableAssert);
		}
	}

	/** 재생 리스너를 추가한다 */
	public static void ExAddPlayListener(this DOTweenAnimation a_oSender, UnityAction a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onPlay?.RemoveAllListeners();
			}

			a_oSender.hasOnPlay = true;

			a_oSender.onPlay = a_oSender.onPlay ?? new UnityEvent();
			a_oSender.onPlay.AddListener(a_oCallback);
		}
	}

	/** 완료 리스너를 추가한다 */
	public static void ExAddCompleteListener(this DOTweenAnimation a_oSender, UnityAction a_oCallback, bool a_bIsReset = true, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 애니메이션이 존재 할 경우
		if(a_oSender != null) {
			// 리셋 모드 일 경우
			if(a_bIsReset) {
				a_oSender.onComplete?.RemoveAllListeners();
			}

			a_oSender.hasOnComplete = true;

			a_oSender.onComplete = a_oSender.onComplete ?? new UnityEvent();
			a_oSender.onComplete.AddListener(a_oCallback);
		}
	}
#endif			// #if DOTWEEN_ENABLE
	#endregion			// 조건부 클래스 함수

	#region 조건부 제네릭 클래스 함수
#if NEWTON_SOFT_JSON_MODULE_ENABLE
	/** 객체 => JSON 문자열로 변환한다 */
	public static string ExToJSONStr<T>(this T a_tSender, bool a_bIsNeedRoot = false, bool a_bIsPretty = false) {
		object oObj = !a_bIsNeedRoot ? a_tSender as object : new Dictionary<string, object>() {
			[KCDefine.B_KEY_JSON_ROOT_DATA] = a_tSender
		};

		return JsonConvert.SerializeObject(oObj, a_bIsPretty ? Formatting.Indented : Formatting.None);
	}
	
	/** JSON 문자열 => 객체로 변환한다 */
	public static T ExJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return JsonConvert.DeserializeObject<T>(a_oSender);
	}
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
	#endregion			// 조건부 제네릭 클래스 함수
}
