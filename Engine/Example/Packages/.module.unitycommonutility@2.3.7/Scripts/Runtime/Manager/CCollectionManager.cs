using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 컬렉션 관리자 */
public partial class CCollectionManager : CSingleton<CCollectionManager> {
	#region 변수
	private Dictionary<System.Type, List<IList>> m_oListContainer = new Dictionary<System.Type, List<IList>>();
	private Dictionary<System.Type, List<IDictionary>> m_oDictContainer = new Dictionary<System.Type, List<IDictionary>>();
	#endregion			// 변수

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset() {
		base.Reset();
		
		m_oListContainer.Clear();
		m_oDictContainer.Clear();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAwake || CSceneManager.IsAppRunning) {
				CSceneManager.CollectionManager = null;
			}
		} catch(System.Exception oException) {
			CFunc.ShowLogWarning($"CCollectionManager.OnDestroy Exception: {oException.Message}");
		}
	}

	/** 리스트를 활성화한다 */
	public List<T> SpawnList<T>(List<T> a_oDefValList = null) {
		var oListContainer = this.GetListContainer(typeof(List<T>));
		CAccess.Assert(oListContainer != null);

		var oList = oListContainer.ExGetVal(KCDefine.B_VAL_0_INT, null) ?? new List<T>();
		a_oDefValList?.ExCopyTo(oList as List<T>, (a_tVal) => a_tVal);

		oListContainer.ExRemoveVal(oList);
		return oList as List<T>;
	}

	/** 딕셔너리를 활성화한다 */
	public Dictionary<K, V> SpawnDict<K, V>(Dictionary<K, V> a_oDefValDict = null) {
		var oDictContainer = this.GetDictContainer(typeof(Dictionary<K, V>));
		CAccess.Assert(oDictContainer != null);

		var oDict = oDictContainer.ExGetVal(KCDefine.B_VAL_0_INT, null) ?? new Dictionary<K, V>();
		a_oDefValDict?.ExCopyTo(oDict as Dictionary<K, V>, (a_tVal) => a_tVal);

		oDictContainer.ExRemoveVal(oDict);
		return oDict as Dictionary<K, V>;
	}

	/** 리스트를 비활성화한다 */
	public void DespawnList<T>(List<T> a_oList, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oList != null);

		// 리스트가 존재 할 경우
		if(a_oList != null) {
			a_oList.Clear();
			this.GetListContainer(typeof(List<T>)).ExAddVal(a_oList);
		}
	}

	/** 딕셔너리를 비활성화한다 */
	public void DespawnDict<K, V>(Dictionary<K, V> a_oDict, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oDict != null);

		// 딕셔너리가 존재 할 경우
		if(a_oDict != null) {
			a_oDict.Clear();
			this.GetDictContainer(typeof(Dictionary<K, V>)).ExAddVal(a_oDict);
		}
	}

	/** 리스트를 반환한다 */
	private List<IList> GetListContainer(System.Type a_oType) {
		var oListContainer = m_oListContainer.GetValueOrDefault(a_oType, null) ?? new List<IList>();
		m_oListContainer.TryAdd(a_oType, oListContainer);

		return oListContainer;
	}

	/** 딕셔너리를 반환한다 */
	private List<IDictionary> GetDictContainer(System.Type a_oType) {
		var oDictContainer = m_oDictContainer.GetValueOrDefault(a_oType, null) ?? new List<IDictionary>();
		m_oDictContainer.TryAdd(a_oType, oDictContainer);

		return oDictContainer;
	}
	#endregion			// 함수
}
