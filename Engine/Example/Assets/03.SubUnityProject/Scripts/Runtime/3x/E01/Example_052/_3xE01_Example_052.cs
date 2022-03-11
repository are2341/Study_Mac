using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3xE01 {
	/** Example 52 */
	public class _3xE01_Example_052 : CStudySceneManager {
		#region 변수
		/** =====> 객체 <===== */
		[SerializeField] private GameObject m_oPlayer = null;
		[SerializeField] private GameObject m_oOriginEnemy = null;
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
				// Do Something
			}			
		}
		#endregion			// 함수
	}
}
