using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace E052 {
	//! Example 52
	public class E01Example_052 : CStudySceneManager {
		#region 객체
		[SerializeField] private GameObject m_oPlayer = null;
		[SerializeField] private GameObject m_oOriginEnemy = null;
		[SerializeField] private GameObject m_oOriginBullet = null;

		private List<GameObject> m_oEnemyList = new List<GameObject>();
		private List<GameObject> m_oBulletList = new List<GameObject>();
		#endregion			// 객체

		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_E01_EXAMPLE_052;
		#endregion			// 프로퍼티

		#region 함수
		//! 상태를 갱신한다
		public override void OnUpdate(float a_fDeltaTime) {
			base.OnUpdate(a_fDeltaTime);

			this.UpdateEnemyState(a_fDeltaTime);
			this.UpdatePlayerState(a_fDeltaTime);
		}

		//! 적 상태를 갱신한다
		private void UpdateEnemyState(float a_fDeltaTime) {

		}

		//! 플레이어 상태를 갱신한다
		private void UpdatePlayerState(float a_fDeltaTime) {
			var stDir = Vector3.zero;

			// 이동 키를 눌렀을 경우
			if(Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed) {
				stDir = Keyboard.current.aKey.isPressed ? Vector3.left : Vector3.right;
			}

			m_oPlayer.transform.localPosition += (stDir * KCDefine.B_SCREEN_WIDTH) * a_fDeltaTime;
		}
		#endregion			// 함수
	}
}
