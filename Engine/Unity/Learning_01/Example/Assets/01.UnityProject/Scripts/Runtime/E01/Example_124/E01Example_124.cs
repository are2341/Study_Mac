using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace E01 {
	//! Example 124
	public class E01Example_124 : CStudySceneManager {
		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_E01_EXAMPLE_124;
		#endregion			// 프로퍼티

		#region 함수
		//! 초기화
		public override void Awake() {
			base.Awake();

			// 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				int nValA = 10;
				int nValB = 20;

				CFunc.ShowLog($"{nValA} + {nValB} = {nValA + nValB}");
				CFunc.ShowLog($"{nValA} - {nValB} = {nValA - nValB}");
				CFunc.ShowLog($"{nValA} * {nValB} = {nValA * nValB}");
				CFunc.ShowLog($"{nValA} / {nValB} = {nValA / nValB}");
				CFunc.ShowLog($"{nValA} % {nValB} = {nValA % nValB}");
			}
		}
		#endregion			// 함수
	}
}
