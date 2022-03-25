using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

/** 유틸리티 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 고유 레벨 식별자를 생성한다 */
	public static long MakeUniqueLevelID(int a_nID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return CFactory.MakeUniqueStageID(a_nStageID, a_nChapterID) + a_nID;
	}

	/** 고유 스테이지 식별자를 생성한다 */
	public static long MakeUniqueStageID(int a_nID, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return CFactory.MakeUniqueChapterID(a_nChapterID) + (a_nID * (long)KCDefine.B_UNIT_IDS_PER_STAGE);
	}

	/** 고유 챕터 식별자를 생성한다 */
	public static long MakeUniqueChapterID(int a_nID) {
		return a_nID * (long)KCDefine.B_UNIT_IDS_PER_CHAPTER;
	}
	
	/** 식별자 정보를 생성한다 */
	public static STIDInfo MakeIDInfo(int a_nID, int a_nStageID = KCDefine.B_VAL_0_INT, int a_nChapterID = KCDefine.B_VAL_0_INT) {
		return new STIDInfo() {
			m_nID = a_nID, m_nStageID = a_nStageID, m_nChapterID = a_nChapterID
		};
	}

	/** 인덱스 정보를 생성한다 */
	public static STIdxInfo MakeIdxInfo(int a_nX, int a_nY, int a_nZ = KCDefine.B_IDX_INVALID) {
		return new STIdxInfo() {
			m_nX = a_nX, m_nY = a_nY, m_nZ = a_nZ
		};
	}
	
	/** 경로 정보를 생성한다 */
	public static CPathInfo MakePathInfo(Vector3Int a_stIdx, int a_nCost = KCDefine.B_VAL_0_INT) {
		return new CPathInfo() {
			m_nCost = a_nCost, m_stIdx = a_stIdx, m_oPrevPathInfo = null
		};
	}

	/** 그래디언트를 생성한다 */
	public static Gradient MakeGradient(Color a_stColor) {
		return new Gradient() {
			colorKeys = new GradientColorKey[] {
				new GradientColorKey(a_stColor, KCDefine.B_VAL_0_FLT), new GradientColorKey(a_stColor, KCDefine.B_VAL_1_FLT)
			},

			alphaKeys = new GradientAlphaKey[] {
				new GradientAlphaKey(a_stColor.a, KCDefine.B_VAL_0_FLT), new GradientAlphaKey(a_stColor.a, KCDefine.B_VAL_1_FLT)
			}
		};
	}

	/** 애니메이션을 생성한다 */
	public static Tween MakeAni(DOGetter<float> a_oGetter, DOSetter<float> a_oSetter, System.Action a_oInitCallback, System.Action<float> a_oSetterCallback, float a_fVal, float a_fDuration, Ease a_eEase = KCDefine.U_EASE_ANI, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oGetter != null && a_oSetter != null);
		a_oInitCallback?.Invoke();
		
		return DOTween.To(a_oGetter, (a_fVal) => { a_oSetter(a_fVal); a_oSetterCallback?.Invoke(a_fVal); }, a_fVal, a_fDuration).SetAutoKill().SetEase(a_eEase).SetUpdate(a_bIsRealtime);
	}

	/** 시퀀스를 생성한다 */
	public static Sequence MakeSequence(Tween a_oAni, System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VAL_0_FLT, bool a_bIsJoin = false, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oAni != null);

		return CFactory.MakeSequence(new List<Tween>() {
			a_oAni
		}, a_oCallback, a_fDelay, a_bIsJoin, a_bIsRealtime);
	}

	/** 시퀀스를 생성한다 */
	public static Sequence MakeSequence(List<Tween> a_oAniList, System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VAL_0_FLT, bool a_bIsJoin = false, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oAniList.ExIsValid());
		var oSequence = DOTween.Sequence().SetAutoKill().SetUpdate(a_bIsRealtime);

		for(int i = 0; i < a_oAniList.Count; ++i) {
			// 조인 모드 일 경우
			if(a_bIsJoin) {
				oSequence.Join(a_oAniList[i]);
			} else {
				oSequence.Append(a_oAniList[i]);
			}
		}

		var oDelaySequence = DOTween.Sequence().SetAutoKill().SetDelay(a_fDelay).SetUpdate(a_bIsRealtime).Append(oSequence);
		return oDelaySequence.AppendCallback(() => a_oCallback?.Invoke(oDelaySequence));
	}

	/** 객체를 생선한다 */
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj(a_oName, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 객체를 생선한다 */
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj(a_oName, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 객체를 생선한다 */
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());

		var oObj = new GameObject(a_oName);
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		oObj.ExSetParent(a_oParent, a_bIsStayWorldState);
		return oObj;
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oObjPath, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);
	}
	
	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oObj = GameObject.Instantiate(a_oOrigin, a_oParent?.transform, a_bIsStayWorldState);
		oObj.name = a_oName;
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;
		
		return oObj;
	}

	/** 터치 응답자를 생성한다 */
	public static GameObject CreateTouchResponder(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateTouchResponder(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 터치 응답자를 생성한다 */
	public static GameObject CreateTouchResponder(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oObj = CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent);
		oObj.GetComponentInChildren<Image>().color = a_stColor;
		
		// 트랜스 폼이 존재 할 경우
		if(oObj.transform as RectTransform != null) {
			(oObj.transform as RectTransform).pivot = KCDefine.B_ANCHOR_MID_CENTER;;
			(oObj.transform as RectTransform).anchorMin = KCDefine.B_ANCHOR_MID_CENTER;
			(oObj.transform as RectTransform).anchorMax = KCDefine.B_ANCHOR_MID_CENTER;
			(oObj.transform as RectTransform).sizeDelta = a_stSize;
			(oObj.transform as RectTransform).anchoredPosition = a_stPos;
		}

		return oObj;
	}

	/** 드래그 응답자를 생성한다 */
	public static GameObject CreateDragResponder(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateDragResponder(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 드래그 응답자를 생성한다 */
	public static GameObject CreateDragResponder(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 객체 풀을 생성한다 */
	public static ObjectPool CreateObjsPool(string a_oObjPath, GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_OBJS_POOL) {
		CAccess.Assert(a_oObjPath.ExIsValid());
		return CFactory.CreateObjsPool(Resources.Load<GameObject>(a_oObjPath), a_oParent, a_nNumObjs);
	}
	
	/** 객체 풀을 생성한다 */
	public static ObjectPool CreateObjsPool(GameObject a_oOrigin, GameObject a_oParent, int a_nNumObjs = KCDefine.U_SIZE_OBJS_POOL) {
		CAccess.Assert(a_oOrigin != null);
		return new ObjectPool(a_oOrigin, a_oParent?.transform, a_nNumObjs);
	}

	/** 객체를 제거한다 */
	public static void RemoveObj(Object a_oObj, bool a_bIsRemoveAsset = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oObj != null);

		// 객체가 존재 할 경우
		if(a_oObj != null) {
			// 앱이 실행 중 일 경우
			if(Application.isPlaying) {
				GameObject.Destroy(a_oObj);
			} else {
				GameObject.DestroyImmediate(a_oObj, a_bIsRemoveAsset);
			}
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	/** 객체를 생선한다 */
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj<T>(a_oName, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 객체를 생선한다 */
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj<T>(a_oName, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 객체를 생선한다 */
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj(a_oName, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState)?.AddComponent<T>();
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oObjPath, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, Vector3.one, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState)?.GetComponentInChildren<T>();
	}

	/** 터치 응답자를 생성한다 */
	public static T CreateTouchResponder<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateTouchResponder<T>(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 터치 응답자를 생성한다 */
	public static T CreateTouchResponder<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor)?.GetComponentInChildren<T>();
	}

	/** 드래그 응답자를 생성한다 */
	public static T CreateDragResponder<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateDragResponder<T>(a_oName, Resources.Load<GameObject>(a_oObjPath), a_oParent, a_stSize, a_stPos, a_stColor);
	}

	/** 드래그 응답자를 생성한다 */
	public static T CreateDragResponder<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stSize, Vector3 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateDragResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor)?.GetComponentInChildren<T>();
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if FIREBASE_MODULE_ENABLE
	/** 유저 정보 노드를 생성한다 */
	public static List<string> MakeUserInfoNodes() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_USER_INFOS
		};
	}

	/** 결제 정보 노드를 생성한다 */
	public static List<string> MakePurchaseInfoNodes() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_PURCHASE_INFOS
		};
	}

	/** 지급 아이템 정보 노드를 생성한다 */
	public static List<string> MakePostItemInfoNodes() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_POST_ITEM_INFOS
		};
	}
#endif			// #if FIREBASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
