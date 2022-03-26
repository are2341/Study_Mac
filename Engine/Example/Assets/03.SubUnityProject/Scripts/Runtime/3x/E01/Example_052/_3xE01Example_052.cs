using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if EXTRA_SCRIPT_ENABLE
namespace _3xE01 {
	/** Example 52 */
	public class _3xE01Example_052 : CStudySceneManager {
		#region 변수
		/** =====> 객체 <===== */
		[SerializeField] private GameObject m_oPlayer = null;
		[SerializeField] private GameObject m_oOriginBullet = null;

		private List<GameObject> m_oBulletList = new List<GameObject>();
		#endregion			// 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_3X_E01_EXAMPLE_052;
		public override EProjection MainCameraProjection => EProjection._2D;
		#endregion			// 프로퍼티

		#region 함수
		/** 상태를 갱신한다 */
		public override void OnUpdate(float a_fDeltaTime) {
			base.OnUpdate(a_fDeltaTime);

			// 앱이 실행 중 일 경우
			if(CSceneManager.IsAppRunning) {
				this.UpdateBulletState(a_fDeltaTime);

				var oContents = m_oPlayer.ExFindChild("Contents");
				float fHorizontal = Input.GetAxis(KCDefine.U_INPUT_N_HORIZONTAL);

				var stPos = m_oPlayer.transform.localPosition;
				stPos.x = Mathf.Clamp(stPos.x + (fHorizontal * this.ScreenWidth) * a_fDeltaTime, this.ScreenWidth / -KCDefine.B_VAL_2_FLT + (oContents.transform.localScale.x / KCDefine.B_VAL_2_FLT), this.ScreenWidth / KCDefine.B_VAL_2_FLT - (oContents.transform.localScale.x / KCDefine.B_VAL_2_FLT));

				m_oPlayer.transform.localPosition = stPos;
			}
		}

		/** 총알 상태를 갱신한다 */
		private void UpdateBulletState(float a_fDeltaTime) {
			var oRemoveBulletList = CCollectionManager.Inst.SpawnList<GameObject>();

			for(int i = 0; i < m_oBulletList.Count; ++i) {
				var stPos = m_oBulletList[i].transform.localPosition;
				var oContents = m_oBulletList[i].ExFindChild("Contents");

				// 화면 범위를 벗어났을 경우
				if(stPos.y.ExIsEquals(CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT)) {
					oRemoveBulletList.ExAddVal(m_oBulletList[i]);
				} else {
					m_oBulletList[i].ExAddLocalPosY(1500.0f * a_fDeltaTime);
				}
			}

			// 발사 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space)) {
				m_oBulletList.ExAddVal(CFactory.CreateCloneObj("Bullet", m_oOriginBullet, this.Objs, m_oPlayer.transform.localPosition));
			}
			
			try {
				m_oBulletList.ExRemoveVals(oRemoveBulletList);

				for(int i = 0; i < oRemoveBulletList.Count; ++i) {
					CFactory.RemoveObj(oRemoveBulletList[i]);
				}
			} finally {
				CCollectionManager.Inst.DespawnList(oRemoveBulletList);
			}
		}
		#endregion			// 함수
	}
}
#endif			// #if EXTRA_SCRIPT_ENABLE
