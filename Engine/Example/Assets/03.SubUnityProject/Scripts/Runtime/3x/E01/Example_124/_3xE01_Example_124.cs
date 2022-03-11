using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3xE01 {
	/** Example 124 */
	public class _3xE01_Example_124 : CStudySceneManager {
		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_3X_E01_EXAMPLE_124;
		#endregion			// 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake() {
			base.Awake();

			// 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				int nLhs = 10;
				int nRhs = 20;

				CFunc.ShowLog($"{nLhs} + {nRhs} = {nLhs + nRhs}");
				CFunc.ShowLog($"{nLhs} - {nRhs} = {nLhs - nRhs}");
				CFunc.ShowLog($"{nLhs} * {nRhs} = {nLhs * nRhs}");
				CFunc.ShowLog($"{nLhs} / {nRhs} = {nLhs / (float)nRhs}");
			}
		}
		#endregion			// 함수
	}
}
