using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/** 씬 관리자 - 팩토리 */
public abstract partial class CSceneManager : CComponent {
	#region 함수
	/** 객체를 활성화한다 */
	public GameObject SpawnObj(string a_oKey, string a_oName) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj(a_oKey, a_oName, Vector3.zero);
	}

	/** 객체를 활성화한다 */
	public GameObject SpawnObj(string a_oKey, string a_oName, Vector3 a_stPos) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj(a_oKey, a_oName, Vector3.one, Vector3.zero, a_stPos);
	}

	/** 객체를 활성화한다 */
	public GameObject SpawnObj(string a_oKey, string a_oName, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos) {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid() && m_oObjsPoolDict.ContainsKey(a_oKey));

		var oObj = m_oObjsPoolDict[a_oKey].Spawn(a_oName);
		oObj.transform.localScale = a_stScale;
		oObj.transform.localEulerAngles = a_stAngle;
		oObj.transform.localPosition = a_stPos;

		return oObj;
	}

	/** 객체를 비활성화한다 */
	public void DespawnObj(string a_oKey, GameObject a_oObj, double a_dblDelay = KCDefine.B_VAL_0_DBL, bool a_bIsDestroy = false, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oKey.ExIsValid() && m_oObjsPoolDict.ContainsKey(a_oKey)));

		// 객체 풀이 존재 할 경우
		if(a_oKey.ExIsValid() && m_oObjsPoolDict.TryGetValue(a_oKey, out ObjectPool oObjsPool)) {
			m_oDespawnObjInfoList.Add(new STDespawnObjInfo() {
				m_bIsDestroy = a_bIsDestroy, m_oKey = a_oKey, m_stDespawnTime = System.DateTime.Now.AddSeconds(a_dblDelay), m_oObj = a_oObj
			});
		}
	}

	/** 화면 페이드 인 애니메이션을 생성한다 */
	protected virtual Tween MakeScreenFadeInAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
		CAccess.Assert(a_oTarget != null && a_oKey.ExIsValid());
		return a_oTarget.GetComponentInChildren<Image>().DOColor(a_stColor, a_fDuration).SetEase(KCDefine.U_EASE_ANI).SetUpdate(this.IsRealtimeFadeInAni);
	}

	/** 화면 페이드 아웃 애니메이션을 생성한다 */
	protected virtual Tween MakeScreenFadeOutAni(GameObject a_oTarget, string a_oKey, Color a_stColor, float a_fDuration) {
		CAccess.Assert(a_oTarget != null && a_oKey.ExIsValid());
		return a_oTarget.GetComponentInChildren<Image>().DOColor(a_stColor, a_fDuration).SetEase(KCDefine.U_EASE_ANI).SetUpdate(this.IsRealtimeFadeOutAni);
	}
	#endregion			// 함수

	#region 제네릭 함수
	/** 객체를 활성화한다 */
	public T SpawnObj<T>(string a_oKey, string a_oName) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj<T>(a_oKey, a_oName, Vector3.zero);
	}

	/** 객체를 활성화한다 */
	public T SpawnObj<T>(string a_oKey, string a_oName, Vector3 a_stPos) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj<T>(a_oKey, a_oName, Vector3.one, Vector3.zero, a_stPos);
	}

	/** 객체를 활성화한다 */
	public T SpawnObj<T>(string a_oKey, string a_oName, Vector3 a_stScale, Vector3 a_stAngle, Vector3 a_stPos) where T : Component {
		CAccess.Assert(a_oKey.ExIsValid() && a_oName.ExIsValid());
		return this.SpawnObj(a_oKey, a_oName, a_stScale, a_stAngle, a_stPos)?.GetComponentInChildren<T>();
	}
	#endregion			// 제네릭 함수
}
