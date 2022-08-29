using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
using MessagePack;

namespace SampleEngineName {
	#region 기본
	/** 그리드 정보 */
	public partial struct STGridInfo {
		public Bounds m_stBounds;

		public Vector3 m_stSize;
		public Vector3 m_stScale;
		public Vector3 m_stPivotPos;
	}

	/** 엔진 타입 랩퍼 */
	[MessagePackObject]
	public partial struct STEngineTypeWrapper {
		// Do Something
	}
	#endregion			// 기본
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE