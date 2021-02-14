using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace E01 {
	//! Example 124
	public class Example_124 : CStudySceneManager {
		#region 프로퍼티
		public override string SceneName => KDefine.G_SCENE_N_E01_EXAMPLE_124;
		#endregion			// 프로퍼티

		#region 함수
		//! 초기화
		public override void Awake() {
			base.Awake();

			// 초기화 되었을 경우
			if(CSceneManager.IsAppInit) {
				int nLhs = 10;
				int nRhs = 20;

				CFunc.ShowLog("===== 사칙 연산 결과 =====");
				CFunc.ShowLog("{0} + {1} = {2}", nLhs, nRhs, this.GetSumValue(nLhs, nRhs));
				CFunc.ShowLog("{0} - {1} = {2}", nLhs, nRhs, this.GetSubValue(nLhs, nRhs));
				CFunc.ShowLog("{0} * {1} = {2}", nLhs, nRhs, this.GetMultiplyValue(nLhs, nRhs));
				CFunc.ShowLog("{0} / {1} = {2}", nLhs, nRhs, this.GetDivideValue(nLhs, nRhs));
			}
		}

		//! 덧셈 결과를 반환한다
		private int GetSumValue(int a_nLhs, int a_nRhs) {
			return a_nLhs + a_nRhs;
		}

		//! 뺄셈 결과를 반환한다
		private int GetSubValue(int a_nLhs, int a_nRhs) {
			return a_nLhs - a_nRhs;
		}

		//! 곱셈 결과를 반환한다
		private int GetMultiplyValue(int a_nLhs, int a_nRhs) {
			return a_nLhs * a_nRhs;
		}

		//! 나눗셈 결과를 반환한다
		private float GetDivideValue(int a_nLhs, int a_nRhs) {
			return a_nLhs / (float)a_nRhs;
		}
		#endregion			// 함수
	}
}
