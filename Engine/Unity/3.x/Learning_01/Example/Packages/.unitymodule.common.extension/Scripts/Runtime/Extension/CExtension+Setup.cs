using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 설정 확장 클래스
public static partial class CExtension {
	#region 클래스 함수
	//! 2 차원 카메라를 설정한다
	public static void ExSetup2D(this Camera a_oSender, float a_fPlaneHeight) {
		CAccess.Assert(a_oSender != null && a_fPlaneHeight.ExIsGreate(KCDefine.B_VALUE_FLT_0));
		
		a_oSender.orthographic = true;
		a_oSender.orthographicSize = a_fPlaneHeight / 2.0f;
	}

	//! 3 차원 카메라를 설정한다
	public static void ExSetup3D(this Camera a_oSender, float a_fPlaneHeight, float a_fPlaneDistance) {
		CAccess.Assert(a_oSender != null);
		CAccess.Assert(a_fPlaneHeight.ExIsGreate(KCDefine.B_VALUE_FLT_0) && a_fPlaneDistance.ExIsGreate(KCDefine.B_VALUE_FLT_0));

		float fFOV = Mathf.Atan((a_fPlaneHeight / 2.0f) / a_fPlaneDistance);

		a_oSender.orthographic = false;
		a_oSender.fieldOfView = (fFOV * 2.0f) * Mathf.Rad2Deg;
	}
	#endregion			// 클래스 함수
}
