using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//! 유틸리티 팩토리
public static partial class CFactory {
	#region 클래스 함수
	//! 시퀀스를 생성한다
	public static Sequence CreateSequence(Tween a_oAni, System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VALUE_FLT_0, Ease a_eEase = KCDefine.U_DEF_EASE_ANI, bool a_bIsJoin = false, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oAni != null);

		return CFactory.CreateSequence(new List<Tween>() {
			a_oAni
		}, a_oCallback, a_fDelay, a_eEase, a_bIsJoin, a_bIsRealtime);
	}

	//! 시퀀스를 생성한다
	public static Sequence CreateSequence(List<Tween> a_oAniList, System.Action<Sequence> a_oCallback, float a_fDelay = KCDefine.B_VALUE_FLT_0, Ease a_eEase = KCDefine.U_DEF_EASE_ANI, bool a_bIsJoin = false, bool a_bIsRealtime = false) {
		CAccess.Assert(a_oAniList.ExIsValid());
		var oSequence = DOTween.Sequence().SetAutoKill().SetUpdate(a_bIsRealtime);

		for(int i = 0; i < a_oAniList.Count; ++i) {
			a_oAniList[i].SetEase(a_eEase);
			
			// 조인 모드 일 경우
			if(a_bIsJoin) {
				oSequence.Join(a_oAniList[i]);
			} else {
				oSequence.Append(a_oAniList[i]);
			}
		}

		var oDelaySequence = DOTween.Sequence().SetAutoKill().SetUpdate(a_bIsRealtime);
		oDelaySequence.SetDelay(a_fDelay);
		oDelaySequence.Append(oSequence);
		oDelaySequence.AppendCallback(() => a_oCallback?.Invoke(oDelaySequence));

		return oDelaySequence;
	}

	//! 객체를 생선한다
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj(a_oName, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 객체를 생선한다
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj(a_oName, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 객체를 생선한다
	public static GameObject CreateObj(string a_oName, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid());

		var oObj = new GameObject(a_oName);
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		// 부모가 존재 할 경우
		if(a_oParent != null) {
			oObj.transform.SetParent(a_oParent.transform, a_bIsStayWorldState);
		}

		return oObj;
	}

	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oObjPath, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateCloneObj(a_oName, oObj, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);
	}
	
	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oObj = GameObject.Instantiate(a_oOrigin, Vector3.zero, Quaternion.identity);
		oObj.name = a_oName;
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		// 부모가 존재 할 경우
		if(a_oParent != null) {
			oObj.transform.SetParent(a_oParent.transform, a_bIsStayWorldState);
		}

		return oObj;
	}

	//! 터치 응답자를 생성한다
	public static GameObject CreateTouchResponder(string a_oName, string a_oObjPath, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateTouchResponder(a_oName, oObj, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	//! 터치 응답자를 생성한다
	public static GameObject CreateTouchResponder(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());

		var oObj = CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent);
		var oImg = oObj.GetComponentInChildren<Image>();

		var oTrans = oObj.transform as RectTransform;
		oTrans.sizeDelta = a_stSize;
		oTrans.anchoredPosition = a_stPos;

		oImg.color = a_stColor;
		return oObj;
	}

	//! 드래그 응답자를 생성한다
	public static GameObject CreateDragResponder(string a_oName, string a_oObjPath, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateDragResponder(a_oName, oObj, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	//! 드래그 응답자를 생성한다
	public static GameObject CreateDragResponder(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	//! 객체 풀을 생성한다
	public static ObjectPool CreateObjPool(string a_oObjPath, GameObject a_oParent, int a_nNumObjs = KCDefine.U_DEF_SIZE_OBJ_POOL) {
		CAccess.Assert(a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateObjPool(oObj, a_oParent, a_nNumObjs);
	}
	
	//! 객체 풀을 생성한다
	public static ObjectPool CreateObjPool(GameObject a_oOrigin, GameObject a_oParent, int a_nNumObjs = KCDefine.U_DEF_SIZE_OBJ_POOL) {
		CAccess.Assert(a_oOrigin != null);

		// 부모가 존재 할 경우
		if(a_oParent != null) {
			return new ObjectPool(a_oOrigin, a_oParent.transform, a_nNumObjs);
		}

		return new ObjectPool(a_oOrigin, null, a_nNumObjs);
	}

	//! 객체를 제거한다
	public static void RemoveObj(Object a_oObj, bool a_bIsRemoveAsset = false) {
		CAccess.Assert(a_oObj != null);

		// 앱이 실행 중 일 경우
		if(Application.isPlaying) {
			GameObject.Destroy(a_oObj);
		} else {
			GameObject.DestroyImmediate(a_oObj, a_bIsRemoveAsset);
		}
	}
	#endregion			// 클래스 함수

	#region 제네릭 클래스 함수
	//! 객체를 생선한다
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj<T>(a_oName, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 객체를 생선한다
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		return CFactory.CreateObj<T>(a_oName, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 객체를 생선한다
	public static T CreateObj<T>(string a_oName, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid());
		var oObj = CFactory.CreateObj(a_oName, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);

		return oObj.AddComponent<T>();
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oObjPath, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oObjPath, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateCloneObj<T>(a_oName, oObj, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		return CFactory.CreateCloneObj<T>(a_oName, a_oOrigin, a_oParent, KCDefine.B_SCALE_NORM, Vector3.zero, a_stPos, a_bIsStayWorldState);
	}

	//! 사본 객체를 생성한다
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos, bool a_bIsStayWorldState = false) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		var oObj = CFactory.CreateCloneObj(a_oName, a_oOrigin, a_oParent, a_stScale, a_stAngle, a_stPos, a_bIsStayWorldState);

		return oObj?.GetComponentInChildren<T>();
	}

	//! 터치 응답자를 생성한다
	public static T CreateTouchResponder<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateTouchResponder<T>(a_oName, oObj, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	//! 터치 응답자를 생성한다
	public static T CreateTouchResponder<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		var oObj = CFactory.CreateTouchResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);

		return oObj?.GetComponentInChildren<T>();
	}

	//! 드래그 응답자를 생성한다
	public static T CreateDragResponder<T>(string a_oName, string a_oObjPath, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oName.ExIsValid() && a_oObjPath.ExIsValid());
		var oObj = Resources.Load<GameObject>(a_oObjPath);

		return CFactory.CreateDragResponder<T>(a_oName, oObj, a_oParent, a_stSize, a_stPos, a_stColor);
	}

	//! 드래그 응답자를 생성한다
	public static T CreateDragResponder<T>(string a_oName, GameObject a_oOrigin, GameObject a_oParent, Vector2 a_stSize, Vector2 a_stPos, Color a_stColor) where T : Component {
		CAccess.Assert(a_oOrigin != null && a_oName.ExIsValid());
		var oObj = CFactory.CreateDragResponder(a_oName, a_oOrigin, a_oParent, a_stSize, a_stPos, a_stColor);

		return oObj?.GetComponentInChildren<T>();
	}
	#endregion			// 제네릭 클래스 함수

	#region 조건부 클래스 함수
#if FIREBASE_MODULE_ENABLE
	//! 지급 아이템 노드를 생성한다
	public static List<string> MakePostItemNodeList() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_POST_ITEM_LIST
		};
	}

	//! 유저 정보 노드를 생성한다
	public static List<string> MakeUserInfoNodeList() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_USER_INFO_LIST
		};
	}

	//! 결제 정보 노드를 생성한다
	public static List<string> MakePurchaseInfoList() {
		return new List<string>() {
			KCDefine.U_NODE_FIREBASE_PURCHASE_INFO_LIST
		};
	}
#endif			// #if FIREBASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
