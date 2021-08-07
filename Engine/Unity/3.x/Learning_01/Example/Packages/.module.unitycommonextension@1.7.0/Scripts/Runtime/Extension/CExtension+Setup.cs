using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 설정 확장 클래스
public static partial class CExtension {
	#region 클래스 함수
	//! 2 차원 카메라를 설정한다
	public static void ExSetup2D(this Camera a_oSender, float a_fPlaneHeight, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_fPlaneHeight.ExIsGreate(KCDefine.B_VAL_0_FLT)));

		// 카메라가 존재 할 경우
		if(a_oSender != null && a_fPlaneHeight.ExIsGreate(KCDefine.B_VAL_0_FLT)) {
			a_oSender.orthographic = true;
			a_oSender.orthographicSize = a_fPlaneHeight / KCDefine.B_VAL_2_FLT;
		}
	}

	//! 3 차원 카메라를 설정한다
	public static void ExSetup3D(this Camera a_oSender, float a_fPlaneHeight, float a_fPlaneDistance, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);
		CAccess.Assert(!a_bIsEnableAssert || (a_fPlaneHeight.ExIsGreate(KCDefine.B_VAL_0_FLT) && a_fPlaneDistance.ExIsGreate(KCDefine.B_VAL_0_FLT)));

		// 카메라가 존재 할 경우
		if(a_oSender != null && (a_fPlaneHeight.ExIsGreate(KCDefine.B_VAL_0_FLT) && a_fPlaneDistance.ExIsGreate(KCDefine.B_VAL_0_FLT))) {
			float fFOV = Mathf.Atan((a_fPlaneHeight / KCDefine.B_VAL_2_FLT) / a_fPlaneDistance);

			a_oSender.orthographic = false;
			a_oSender.fieldOfView = (fFOV * KCDefine.B_VAL_2_FLT) * Mathf.Rad2Deg;
		}
	}
	#endregion			// 클래스 함수
}
