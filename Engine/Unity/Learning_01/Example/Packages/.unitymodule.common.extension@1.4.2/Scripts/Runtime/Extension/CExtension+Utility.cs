﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Linq;
using DG.Tweening;
using Coffee.UIExtensions;
using MessagePack;
using Leguar.TotalJSON;

//! 유틸리티 확장 클래스
public static partial class CExtension {
	#region 클래스 함수
	//! 클릭 리스너를 추가한다
	public static void ExAddClickListener(this Button a_oSender, UnityAction a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onClick != null) {
			a_oSender.onClick.RemoveAllListeners();
		}

		a_oSender.onClick.AddListener(a_oCallback);
	}

	//! 값 변경 리스너를 추가한다
	public static void ExAddValChangeListener(this Slider a_oSender, UnityAction<float> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onValueChanged != null) {
			a_oSender.onValueChanged.RemoveAllListeners();
		}

		a_oSender.onValueChanged.AddListener(a_oCallback);
	}

	//! 값 변경 리스너를 추가한다
	public static void ExAddValChangeListener(this Scrollbar a_oSender, UnityAction<float> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onValueChanged != null) {
			a_oSender.onValueChanged.RemoveAllListeners();
		}
		
		a_oSender.onValueChanged.AddListener(a_oCallback);
	}

	//! 값 변경 리스너를 추가한다
	public static void ExAddValChangeListener(this Dropdown a_oSender, UnityAction<int> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onValueChanged != null) {
			a_oSender.onValueChanged.RemoveAllListeners();
		}

		a_oSender.onValueChanged.AddListener(a_oCallback);
	}

	//! 값 변경 리스너를 추가한다
	public static void ExAddValChangeListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onValueChanged != null) {
			a_oSender.onValueChanged.RemoveAllListeners();
		}

		a_oSender.onValueChanged.AddListener(a_oCallback);
	}

	//! 값 변경 리스너를 추가한다
	public static void ExAddValChangeListener(this ScrollRect a_oSender, UnityAction<Vector2> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onValueChanged != null) {
			a_oSender.onValueChanged.RemoveAllListeners();
		}

		a_oSender.onValueChanged.AddListener(a_oCallback);
	}

	//! 편집 종료 리스너를 추가한다
	public static void ExAddEndEditListener(this InputField a_oSender, UnityAction<string> a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onEndEdit != null) {
			a_oSender.onEndEdit.RemoveAllListeners();
		}

		a_oSender.onEndEdit.AddListener(a_oCallback);
	}
	
	//! X 축 비율을 추가한다
	public static void ExAddScaleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale += new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
	}

	//! Y 축 비율을 추가한다
	public static void ExAddScaleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale += new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! Z 축 비율을 추가한다
	public static void ExAddScaleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localScale += new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! 월드 X 축 각도를 추가한다
	public static void ExAddWorldAngleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles += new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
	}
	
	//! 월드 Y 축 각도를 추가한다
	public static void ExAddWorldAngleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles += new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! 월드 Z 축 각도를 추가한다
	public static void ExAddWorldAngleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.eulerAngles += new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! 로컬 X 축 각도를 추가한다
	public static void ExAddLocalAngleX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles += new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
	}
	
	//! 로컬 Y 축 각도를 추가한다
	public static void ExAddLocalAngleY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles += new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! 로컬 Z 축 각도를 추가한다
	public static void ExAddLocalAngleZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localEulerAngles += new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! 월드 X 축 위치를 추가한다
	public static void ExAddWorldPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position += new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
	}
	
	//! 월드 Y 축 위치를 추가한다
	public static void ExAddWorldPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position += new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! 월드 Z 축 위치를 추가한다
	public static void ExAddWorldPosZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.position += new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! 로컬 X 축 위치를 추가한다
	public static void ExAddLocalPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition += new Vector3(a_fVal, KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT);
	}
	
	//! 로컬 Y 축 위치를 추가한다
	public static void ExAddLocalPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition += new Vector3(KCDefine.B_VAL_0_FLT, a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! 로컬 Z 축 위치를 추가한다
	public static void ExAddLocalPosZ(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null);
		a_oSender.transform.localPosition += new Vector3(KCDefine.B_VAL_0_FLT, KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! X 축 크기 간격을 추가한다
	public static void ExAddSizeDeltaX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).sizeDelta += new Vector2(a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! Y 축 크기 간격을 추가한다
	public static void ExAddSizeDeltaY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).sizeDelta += new Vector2(KCDefine.B_VAL_0_FLT, a_fVal);
	}

	//! X 축 앵커 위치를 추가한다
	public static void ExAddAnchorPosX(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).anchoredPosition += new Vector2(a_fVal, KCDefine.B_VAL_0_FLT);
	}

	//! Y 축 앵커 위치를 추가한다
	public static void ExAddAnchorPosY(this GameObject a_oSender, float a_fVal) {
		CAccess.Assert(a_oSender != null && (a_oSender.transform as RectTransform) != null);
		(a_oSender.transform as RectTransform).anchoredPosition += new Vector2(KCDefine.B_VAL_0_FLT, a_fVal);
	}
	
	//! 효과를 재생한다
	public static void ExPlay(this ParticleSystem a_oSender, bool a_bIsReset = true, bool a_bIsRemoveChildren = false) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset) {
			a_oSender.Stop(a_bIsRemoveChildren);
		}

		a_oSender.Play();
	}

	//! 효과를 재생한다
	public static Tween ExPlay(this CFXBase a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		a_oSender.effectFactor = a_fStartVal;

		var oAni = DOTween.To(() => a_oSender.effectFactor, (a_fVal) => a_oSender.effectFactor = a_fVal, a_fEndVal, a_fDuration);
		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 효과를 재생한다
	public static Sequence ExPlay(this CFXBase a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<CFXBase, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExPlay(a_fStartVal, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 애니메이션을 시작한다
	public static Tween ExStartAni(this Image a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		a_oSender.fillAmount = a_fStartVal;

		var oAni = DOTween.To(() => a_oSender.fillAmount, (a_fVal) => a_oSender.fillAmount = a_fVal, a_fEndVal, a_fDuration);
		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 애니메이션을 시작한다
	public static Sequence ExStartAni(this Image a_oSender, float a_fStartVal, float a_fEndVal, float a_fDuration, System.Action<Image, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartAni(a_fStartVal, a_fEndVal, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 비율 애니메이션을 시작한다
	public static Tween ExStartScaleAni(this GameObject a_oSender, Vector3 a_stScale, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.transform.DOScale(a_stScale, a_fDuration);
		
		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 비율 애니메이션을 시작한다
	public static Sequence ExStartScaleAni(this GameObject a_oSender, Vector3 a_stScale, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartScaleAni(a_stScale, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 월드 이동 애니메이션을 시작한다
	public static Tween ExStartWorldMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.transform.DOMove(a_stPos, a_fDuration);

		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 월드 이동 애니메이션을 시작한다
	public static Sequence ExStartWorldMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartWorldMoveAni(a_stPos, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 로컬 이동 애니메이션을 시작한다
	public static Tween ExStartLocalMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.transform.DOLocalMove(a_stPos, a_fDuration);

		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 로컬 이동 애니메이션을 시작한다
	public static Sequence ExStartLocalMoveAni(this GameObject a_oSender, Vector3 a_stPos, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartLocalMoveAni(a_stPos, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 월드 경로 애니메이션을 시작한다
	public static Tween ExStartWorldPathAni(this GameObject a_oSender, Vector3[] a_oPoses, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false) {
		CAccess.Assert(a_oSender != null);

		var ePathType = a_bIsLinear ? PathType.Linear : PathType.CatmullRom;
		var oAni = a_oSender.transform.DOPath(a_oPoses, a_fDuration, ePathType);

		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 월드 경로 애니메이션을 시작한다
	public static Tween ExStartWorldPathAni(this GameObject a_oSender, Vector3[] a_oPoses, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartWorldPathAni(a_oPoses, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 로컬 경로 애니메이션을 시작한다
	public static Tween ExStartLocalPathAni(this GameObject a_oSender, Vector3[] a_oPoses, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false) {
		CAccess.Assert(a_oSender != null);

		var ePathType = a_bIsLinear ? PathType.Linear : PathType.CatmullRom;
		var oAni = a_oSender.transform.DOLocalPath(a_oPoses, a_fDuration, ePathType);

		return oAni.SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	//! 로컬 경로 애니메이션을 시작한다
	public static Tween ExStartLocalPathAni(this GameObject a_oSender, Vector3[] a_oPoses, float a_fDuration, System.Action<GameObject, Sequence> a_oCallback, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false, bool a_bIsLinear = false, float a_fDelay = KCDefine.B_VAL_0_FLT) {
		CAccess.Assert(a_oSender != null);
		var oAni = a_oSender.ExStartLocalPathAni(a_oPoses, a_fDuration, a_eEase, a_bIsRealtime);

		return CFactory.CreateSequence(oAni, (a_oSequenceSender) => {
			a_oCallback?.Invoke(a_oSender, a_oSequenceSender);
		}, a_fDelay, a_eEase, false, a_bIsRealtime);
	}

	//! 위치 => 인덱스로 변경한다
	public static Vector2Int ExToIdx(this Vector2 a_stSender, Vector2 a_stBasePos, Vector2 a_stSize) {
		CAccess.Assert(a_stSize.x.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		CAccess.Assert(a_stSize.y.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var stDelta = a_stSender - a_stBasePos;
		return new Vector2Int((int)(stDelta.x / a_stSize.x), (int)(stDelta.y / -a_stSize.y));
	}

	//! 위치 => 인덱스로 변경한다
	public static Vector3Int ExToIdx(this Vector3 a_stSender, Vector3 a_stBasePos, Vector3 a_stSize) {
		CAccess.Assert(a_stSize.x.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		CAccess.Assert(a_stSize.y.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));
		CAccess.Assert(a_stSize.z.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		var stDelta = a_stSender - a_stBasePos;
		return new Vector3Int((int)(stDelta.x / a_stSize.x), (int)(stDelta.y / -a_stSize.y), (int)(stDelta.z / -a_stSize.z));
	}

	//! 인덱스 => 위치로 변경한다
	public static Vector2 ExToPos(this Vector2Int a_stSender, Vector2 a_stOffset, Vector2 a_stSize) {
		return new Vector2((a_stSender.x * a_stSize.x) + a_stOffset.x, (a_stSender.y * -a_stSize.y) + a_stOffset.y);
	}

	//! 인덱스 => 위치로 변경한다
	public static Vector3 ExToPos(this Vector3Int a_stSender, Vector3 a_stOffset, Vector3 a_stSize) {
		return new Vector3((a_stSender.x * a_stSize.x) + a_stOffset.x, (a_stSender.y * -a_stSize.y) + a_stOffset.y, (a_stSender.z * -a_stSize.z) + a_stOffset.z);
	}

	//! 3 차원 => 2 차원으로 변경한다
	public static Vector2 ExTo2D(this Vector3 a_stSender) {
		return new Vector2(a_stSender.x, a_stSender.y);
	}

	//! 2 차원 => 3 차원으로 변경한다
	public static Vector3 ExTo3D(this Vector2 a_stSender, float a_fZ = KCDefine.B_VAL_0_FLT) {
		return new Vector3(a_stSender.x, a_stSender.y, a_fZ);
	}

	//! 문자열 => Base64 문자열로 변환한다
	public static string ExToBase64Str(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Text.Encoding.Default.GetBytes(a_oSender);

		return System.Convert.ToBase64String(oBytes);
	}

	//! 문자열 => 압축 된 문자열로 변환한다
	public static string ExToCompressStr(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());

		using(var oMemoryStream = new MemoryStream()) {
			var oBytes = System.Text.Encoding.Default.GetBytes(a_oSender);

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

	//! Base64 문자열 => 바이트로 변환한다
	public static byte[] ExBase64StrToBytes(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return System.Convert.FromBase64String(a_oSender);
	}

	//! Base64 문자열 => 문자열로 변환한다
	public static string ExBase64StrToStr(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Convert.FromBase64String(a_oSender);

		return System.Text.Encoding.Default.GetString(oBytes);
	}

	//! 압축 된 문자열 => 문자열로 변환한다
	public static string ExCompressStrToStr(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Convert.FromBase64String(a_oSender);

		using(var oMemoryStream = new MemoryStream(oBytes, sizeof(int), oBytes.Length - sizeof(int))) {
			int nLength = System.BitConverter.ToInt32(oBytes, KCDefine.B_VAL_0_INT);
			var oDecompressBytes = new byte[nLength];

			using(var oGZipStream = new GZipStream(oMemoryStream, CompressionMode.Decompress, true)) {
				oGZipStream.Read(oDecompressBytes, KCDefine.B_VAL_0_INT, oDecompressBytes.Length);
				return System.Text.Encoding.Default.GetString(oDecompressBytes);
			}
		}
	}
	
	//! 자식 객체를 탐색한다
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

	//! 자식 객체를 탐색한다
	public static List<GameObject> ExFindChildren(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.DescendantsAndSelf() : a_oSender.Descendants();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.ExIsEquals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.ExIsContains(a_oName))) {
				oObjList.ExAddVal(oObj);
			}
		}

		return oObjList;
	}

	//! 부모 객체를 탐색한다
	public static GameObject ExFindParent(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.ExIsEquals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.ExIsContains(a_oName))) {
				return oObj;
			}
		}

		return null;
	}

	//! 부모 객체를 탐색한다
	public static List<GameObject> ExFindParents(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) {
		var oObjList = new List<GameObject>();
		var oEnumerator = a_bIsIncludeSelf ? a_oSender.AncestorsAndSelf() : a_oSender.Ancestors();

		foreach(var oObj in oEnumerator) {
			bool bIsEquals = oObj.name.ExIsEquals(a_oName);

			// 이름이 동일 할 경우
			if(bIsEquals || (a_bIsEnableSubName && oObj.name.ExIsContains(a_oName))) {
				oObjList.ExAddVal(oObj);
			}
		}

		return oObjList;
	}

	//! 함수를 지연 호출한다
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour, object[]> a_oCallback, object[] a_oParams = null) {
		var oEnumerator = a_oSender.ExDoLateCallFunc(a_oCallback, a_oParams);
		a_oSender.StartCoroutine(oEnumerator);
	}

	//! 함수를 지연 호출한다
	public static void ExLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour, object[]> a_oCallback, float a_fDelay, bool a_bIsRealtime = false, object[] a_oParams = null) {
		var oEnumerator = a_oSender.ExDoLateCallFunc(a_oCallback, a_fDelay, a_bIsRealtime, a_oParams);
		a_oSender.StartCoroutine(oEnumerator);
	}

	//! 함수를 반복 호출한다
	public static void ExRepeatCallFunc(this MonoBehaviour a_oSender, System.Func<MonoBehaviour, object[], bool, bool> a_oCallback, float a_fDeltaTime, float a_fMaxDeltaTime, bool a_bIsRealtime = false, object[] a_oParams = null) {	
		var oEnumerator = a_oSender.ExDoRepeatCallFunc(a_oCallback, a_fDeltaTime, a_fMaxDeltaTime, a_bIsRealtime, a_oParams);
		a_oSender.StartCoroutine(oEnumerator);
	}

	//! 메세지를 전송한다
	public static void ExSendMsg(this Scene a_stSender, string a_oName, string a_oMsg, object a_oParams) {
		var oObj = a_stSender.ExFindChild(a_oName);
		oObj.ExSendMsg(a_oMsg, a_oParams);
	}

	//! 메세지를 전송한다
	public static void ExSendMsg(this GameObject a_oSender, string a_oMsg, object a_oParams) {
		a_oSender?.SendMessage(a_oMsg, a_oParams, SendMessageOptions.DontRequireReceiver);
	}

	//! 메세지를 전송한다
	public static void ExSendMsg(this GameObject a_oSender, string a_oName, string a_oMsg, object a_oParams) {
		CAccess.Assert(a_oSender != null && a_oName.ExIsValid());
		var oObj = a_oSender.ExFindChild(a_oName);
		
		oObj.ExSendMsg(a_oMsg, a_oParams);
	}

	//! 메세지를 전파한다
	public static void ExBroadcastMsg(this Scene a_stSender, string a_oMsg, object a_oParams) {
		var oObjs = a_stSender.GetRootGameObjects();

		// 객체가 존재 할 경우
		if(oObjs.ExIsValid()) {
			for(int i = 0; i < oObjs.Length; ++i) {
				oObjs[i].ExBroadcastMsg(a_oMsg, a_oParams);
			}
		}
	}

	//! 메세지를 전파한다
	public static void ExBroadcastMsg(this GameObject a_oSender, string a_oMsg, object a_oParams) {
		CAccess.Assert(a_oSender != null);
		a_oSender.BroadcastMessage(a_oMsg, a_oParams, SendMessageOptions.DontRequireReceiver);
	}

	//! 함수를 지연 호출한다
	private static IEnumerator ExDoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour, object[]> a_oCallback, object[] a_oParams) {
		CAccess.Assert(a_oSender != null);
		yield return new WaitForEndOfFrame();

		a_oCallback?.Invoke(a_oSender, a_oParams);
	}

	//! 함수를 지연 호출한다
	private static IEnumerator ExDoLateCallFunc(this MonoBehaviour a_oSender, System.Action<MonoBehaviour, object[]> a_oCallback, float a_fDelay, bool a_bIsRealtime, object[] a_oParams) {
		CAccess.Assert(a_oSender != null && a_fDelay.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

		// 리얼 타임 모드 일 경우
		if(a_bIsRealtime) {
			yield return new WaitForSecondsRealtime(a_fDelay);
		} else {
			yield return new WaitForSeconds(a_fDelay);
		}

		a_oCallback?.Invoke(a_oSender, a_oParams);
	}

	//! 함수를 반복 호출한다
	private static IEnumerator ExDoRepeatCallFunc(this MonoBehaviour a_oSender, System.Func<MonoBehaviour, object[], bool, bool> a_oCallback, float a_fDeltaTime, double a_dblMaxDeltaTime, bool a_bIsRealtime, object[] a_oParams) {
		CAccess.Assert(a_oSender != null && a_oCallback != null);
		CAccess.Assert(a_fDeltaTime.ExIsGreateEquals(KCDefine.B_VAL_0_FLT));

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
		} while(a_oCallback(a_oSender, a_oParams, false) && dblDeltaTime.ExIsLess(a_dblMaxDeltaTime));

		a_oCallback(a_oSender, a_oParams, true);
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 값을 교환한다
	public static void ExSwap<T>(this T[,] a_oSender, Vector2Int a_stIdxA, Vector2Int a_stIdxB) {
		CAccess.Assert(a_oSender.ExIsValidIdx(a_stIdxA));
		CAccess.Assert(a_oSender.ExIsValidIdx(a_stIdxB));

		T tTemp = a_oSender[a_stIdxA.y, a_stIdxA.x];
		a_oSender[a_stIdxA.y, a_stIdxA.x] = a_oSender[a_stIdxB.y, a_stIdxB.x];
		a_oSender[a_stIdxB.y, a_stIdxB.x] = tTemp;
	}

	//! 컴포넌트를 추가한다
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component {
		// 컴포넌트가 존재 할 경우
		if(a_oSender.TryGetComponent<T>(out T oComponent)) {
			return oComponent;
		}

		return a_oSender.AddComponent<T>();
	}

	//! 컴포넌트를 제거한다
	public static void ExRemoveComponent<T>(this GameObject a_oSender) where T : Component {
		var oComponent = a_oSender.GetComponentInChildren<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponent != null) {
			CFactory.RemoveObj(oComponent);
		}
	}

	//! 컴포넌트를 제거한다
	public static void ExRemoveComponents<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true) where T : Component {
		var oComponents = a_oSender.GetComponentsInChildren<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponents.ExIsValid()) {
			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != oComponents[i].gameObject) {
					CFactory.RemoveObj(oComponents[i]);
				}
			}
		}
	}

	//! 컴포넌트를 제거한다
	public static void ExRemoveComponentInParent<T>(this GameObject a_oSender) where T : Component {
		var oComponent = a_oSender.GetComponentInParent<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponent != null) {
			CFactory.RemoveObj(oComponent);
		}
	}

	//! 컴포넌트를 제거한다
	public static void ExRemoveComponentsInParent<T>(this GameObject a_oSender, bool a_bIsIncludeSelf = true) where T : Component {
		var oComponents = a_oSender.GetComponentsInParent<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponents.ExIsValid()) {
			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 제거가 가능 할 경우
				if(a_bIsIncludeSelf || a_oSender != oComponents[i].gameObject) {
					CFactory.RemoveObj(oComponents[i]);
				}
			}
		}
	}

	//! 객체 => JSON 문자열로 변환한다
	public static string ExToJSONStr<T>(this T a_tSender, bool a_bIsNeedRoot = false, bool a_bIsPretty = false) {
		object oObj = !a_bIsNeedRoot ? a_tSender as object : new Dictionary<string, object>() {
			[KCDefine.B_KEY_JSON_ROOT_DATA] = a_tSender
		};

		var oJSON = JSON.Serialize(oObj, new SerializeSettings() {
			AllowNonStringDictionaryKeys = true
		});

		return a_bIsPretty ? oJSON.CreatePrettyString() : oJSON.CreateString();
	}

	//! 객체 => JSON 문자열로 변환한다
	public static string ExToMsgPackJSONStr<T>(this T a_tSender) {
		var oBytes = MessagePackSerializer.Serialize<T>(a_tSender);
		return MessagePackSerializer.ConvertToJson(oBytes);
	}

	//! 객체 => Base64 문자열로 변환한다
	public static string ExToMsgPackBase64Str<T>(this T a_tSender) {
		var oBytes = MessagePackSerializer.Serialize<T>(a_tSender);
		return System.Convert.ToBase64String(oBytes);	
	}

	//! JSON 문자열 => 객체로 변환한다
	public static T ExJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		return JSON.ParseString(a_oSender).Deserialize<T>();
	}

	//! JSON 문자열 => 객체로 변환한다
	public static T ExMsgPackJSONStrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = MessagePackSerializer.ConvertFromJson(a_oSender);

		return MessagePackSerializer.Deserialize<T>(oBytes);
	}

	//! Base64 문자열 => 객체로 변환한다
	public static T ExMsgPackBase64StrToObj<T>(this string a_oSender) {
		CAccess.Assert(a_oSender.ExIsValid());
		var oBytes = System.Convert.FromBase64String(a_oSender);

		return MessagePackSerializer.Deserialize<T>(oBytes);
	}

	//! 컴포넌트를 탐색한다
	public static T ExFindComponent<T>(this Scene a_stSender, string a_oName, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_stSender.ExFindChild(a_oName, a_bIsEnableSubName);
		return oObj?.GetComponentInChildren<T>();
	}
	
	//! 컴포넌트를 탐색한다
	public static T ExFindComponent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName);
		return oObj?.GetComponentInChildren<T>();
	}

	//! 컴포넌트를 탐색한다
	public static T[] ExFindComponents<T>(this Scene a_stSender, string a_oName, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_stSender.ExFindChild(a_oName, a_bIsEnableSubName);
		return oObj?.GetComponentsInChildren<T>();
	}

	//! 컴포넌트를 탐색한다
	public static T[] ExFindComponents<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_oSender.ExFindChild(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName);
		return oObj?.GetComponentsInChildren<T>();
	}

	//! 부모 컴포넌트를 탐색한다
	public static T ExFindComponentInParent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_oSender.ExFindParent(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName);
		return oObj?.GetComponentInParent<T>();
	}

	//! 부모 컴포넌트를 탐색한다
	public static T[] ExFindComponentsInParent<T>(this GameObject a_oSender, string a_oName, bool a_bIsIncludeSelf = true, bool a_bIsEnableSubName = false) where T : Component {
		var oObj = a_oSender.ExFindParent(a_oName, a_bIsIncludeSelf, a_bIsEnableSubName);
		return oObj?.GetComponentsInParent<T>();
	}

	//! 배열을 순회한다
	public static void ExEnumerate<T>(this T[,] a_oSender, System.Func<T, Vector2Int, bool> a_oCallback) {
		CAccess.Assert(a_oSender != null && a_oCallback != null);

		for(int i = 0; i < a_oSender.GetLength(KCDefine.B_VAL_0_INT); ++i) {
			for(int j = 0; j < a_oSender.GetLength(KCDefine.B_VAL_1_INT); ++j) {
				var stIdx = new Vector2Int(j, i);
				
				// 배열 순회가 불가능 할 경우
				if(!a_oCallback(a_oSender[i, j], stIdx)) {
					goto EXIT_FOR;
				}
			}
		}

EXIT_FOR:
		return;
	}

	//! 컴포넌트를 순회한다
	public static void ExEnumerateComponents<T>(this GameObject a_oSender, System.Func<T, int, bool> a_oCallback) {
		CAccess.Assert(a_oSender != null && a_oCallback != null);
		var oComponents = a_oSender.GetComponentsInChildren<T>();

		// 컴포넌트가 존재 할 경우
		if(oComponents.ExIsValid()) {
			for(int i = 0; i < oComponents.Length; ++i) {
				// 컴포넌트 순회가 불가능 할 경우
				if(!a_oCallback(oComponents[i], i)) {
					break;
				}
			}
		}
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if DOTWEEN_ENABLE
	//! 재생 리스너를 추가한다
	public static void ExAddPlayListener(this DOTweenAnimation a_oSender, UnityAction a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onPlay != null) {
			a_oSender.onPlay.RemoveAllListeners();
		}

		a_oSender.hasOnPlay = true;
		a_oSender.onPlay.AddListener(a_oCallback);
	}

	//! 완료 리스너를 추가한다
	public static void ExAddCompleteListener(this DOTweenAnimation a_oSender, UnityAction a_oCallback, bool a_bIsReset = true) {
		CAccess.Assert(a_oSender != null);

		// 리셋 모드 일 경우
		if(a_bIsReset && a_oSender.onComplete != null) {
			a_oSender.onComplete.RemoveAllListeners();
		}

		a_oSender.hasOnComplete = true;
		a_oSender.onComplete.AddListener(a_oCallback);
	}
#endif			// #if DOTWEEN_ENABLE
	#endregion			// 조건부 클래스 함수
}
