using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3xE01 {
	/** Example 150 */
	public class _3xE01_Example_150 : CStudySceneManager {
		#region 변수
		/** =====> 객체 <===== */
		private List<GameObject> m_oEnemyList = new List<GameObject>();

		[SerializeField] private GameObject m_oPlayer = null;
		[SerializeField] private GameObject m_oOriginEnemy = null;
		#endregion			// 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_3X_E01_EXAMPLE_150;
		#endregion			// 프로퍼티

		#region 함수
		/** 상태를 갱신한다 */
		public override void OnUpdate(float a_fDeltaTime) {
			base.OnUpdate(a_fDeltaTime);

			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAppRunning) {
				this.UpdateEnemyState(a_fDeltaTime);
				this.UpdatePlayerState(a_fDeltaTime);
			}
		}

		/** 적 상태를 갱신한다 */
		private void UpdateEnemyState(float a_fDeltaTime) {
			var oRemoveEnemyList = CCollectionManager.Inst.SpawnList<GameObject>();

			for(int i = 0; i < m_oEnemyList.Count; ++i) {
				m_oEnemyList[i].ExSetLocalPosY(500.0f * -a_fDeltaTime);

				// 화면을 벗어났을 경우
				if(m_oEnemyList[i].transform.localPosition.y.ExIsLessEquals(-KCDefine.B_SCREEN_HEIGHT)) {
					oRemoveEnemyList.Add(m_oEnemyList[i]);
				}
			}

			for(int i = 0; i < oRemoveEnemyList.Count; ++i) {
				Destroy(oRemoveEnemyList[i]);
			}

			m_oEnemyList.ExRemoveVals(oRemoveEnemyList);
			CCollectionManager.Inst.DespawnList(oRemoveEnemyList);
		}

		/** 플레이어 상태를 갱신한다 */
		private void UpdatePlayerState(float a_fDeltaTime) {
			var stPos = m_oPlayer.transform.localPosition.ExPosToPivotPos();
			stPos.x = Mathf.Clamp(stPos.x + ((Input.GetAxis(KCDefine.U_INPUT_N_HORIZONTAL) * 750.0f) * a_fDeltaTime), 50.0f, KCDefine.B_SCREEN_WIDTH - 50.0f);

			m_oPlayer.transform.localPosition = stPos.ExPivotPosToPos();
		}
		#endregion			// 함수
	}
}
