using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3xE01 {
	/** Example 52 */
	public class _3xE01_Example_052 : CStudySceneManager {
		#region 변수
		/** =====> 객체 <===== */
		private List<GameObject> m_oBulletList = new List<GameObject>();

		[SerializeField] private GameObject m_oPlayer = null;
		[SerializeField] private GameObject m_oOriginBullet = null;
		#endregion			// 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_3X_E01_EXAMPLE_052;
		#endregion			// 프로퍼티

		#region 함수
		/** 상태를 갱신한다 */
		public override void OnUpdate(float a_fDeltaTime) {
			base.OnUpdate(a_fDeltaTime);

			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAppRunning) {
				this.UpdateBulletState(a_fDeltaTime);
				this.UpdatePlayerState(a_fDeltaTime);

				// 발사 키를 눌렀을 경우
				if(Input.GetKeyDown(KeyCode.Space)) {
					var oBullet = CFactory.CreateCloneObj("Bullet", m_oOriginBullet, this.Objs);
					oBullet.transform.localPosition = m_oPlayer.transform.localPosition;

					m_oBulletList.Add(oBullet);
				}
			}
		}

		/** 총알 상태를 갱신한다 */
		private void UpdateBulletState(float a_fDeltaTime) {
			var oRemoveBulletList = CCollectionManager.Inst.SpawnList<GameObject>();

			for(int i = 0; i < m_oBulletList.Count; ++i) {
				m_oBulletList[i].ExAddLocalPosY(1500.0f * a_fDeltaTime);

				// 화면을 벗어났을 경우
				if(m_oBulletList[i].transform.localPosition.y.ExIsGreateEquals(KCDefine.B_SCREEN_HEIGHT)) {
					oRemoveBulletList.Add(m_oBulletList[i]);
				}
			}

			for(int i = 0; i < oRemoveBulletList.Count; ++i) {
				Destroy(oRemoveBulletList[i]);
			}

			m_oBulletList.ExRemoveVals(oRemoveBulletList);
			CCollectionManager.Inst.DespawnList(oRemoveBulletList);
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
