using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coffee.UIExtensions {
	//! 효과
	public abstract class CFXBase : UIEffectBase {
		#region 프로퍼티
		public abstract float effectFactor { get; set; }
		#endregion			// 프로퍼티
	}
}
