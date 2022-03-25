using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if ENGINE_TEMPLATES_MODULE_ENABLE
namespace SampleEngineName {
	/** 엔진 접근 확장 클래스 */
	public static partial class AccessExtension {
		#region 클래스 함수
		/** 인덱스 유효 여부를 검사한다 */
		public static bool ExIsValidIdx(this Dictionary<EBlockType, List<(EBlockKinds, CBlock)>>[,] a_oSender, Vector3Int a_stIdx) {
			return a_oSender.ExIsValidIdx<Dictionary<EBlockType, List<(EBlockKinds, CBlock)>>>(a_stIdx) && a_oSender[a_stIdx.y, a_stIdx.x] != null;
		}
		#endregion			// 클래스 함수

		#region 추가 클래스 함수

		#endregion			// 추가 클래스 함수
	}
}
#endif			// #if ENGINE_TEMPLATES_MODULE_ENABLE
