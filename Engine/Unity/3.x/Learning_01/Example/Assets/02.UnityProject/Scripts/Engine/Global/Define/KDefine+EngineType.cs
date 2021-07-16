using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

namespace SampleEngineName {
#region 기본
	//! 그리드 정보
	public struct STGridInfo {
		public Vector3 m_stGridSize;
		public Vector3 m_stGridScale;
		public Vector3 m_stGridPivotPos;
		
		public Bounds m_stGridBounds;
	}
	
	//! 엔진 타입 랩퍼
	[MessagePackObject]
	public struct STEngineTypeWrapper {

	}
#endregion			// 기본
}
