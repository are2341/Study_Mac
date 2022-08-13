using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
namespace SampleEngineName {
	/** 플레이어 객체 제어자 */
	public partial class CEPlayerObjController : CEController {
		/** 매개 변수 */
		public new partial struct STParams {
			public CEController.STParams m_stBaseParams;
		}

		#region 변수
		
		#endregion			// 변수

		#region 프로퍼티
		public new STParams Params { get; private set; }
		#endregion			// 프로퍼티

		#region 함수
		
		#endregion			// 함수
	}
}
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE && UTILITY_SCRIPT_TEMPLATES_MODULE_ENABLE
