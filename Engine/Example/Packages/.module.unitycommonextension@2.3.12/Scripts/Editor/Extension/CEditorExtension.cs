using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

/** 에디터 기본 접근 확장 */
public static partial class CEditorExtension {
    #region 클래스 함수
	/** 상태를 리셋한다 */
	public static void ExReset(this LightProbeGroup a_oSender, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oSender != null);

		// 라인이 존재 할 경우
		if(a_oSender != null) {
			a_oSender.probePositions = new Vector3[] {
				new Vector3(-KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT),
				new Vector3(-KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT),
				new Vector3(KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT),
				new Vector3(KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT),

				new Vector3(-KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT),
				new Vector3(-KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT),
				new Vector3(KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT),
				new Vector3(KCDefine.B_VAL_1_FLT, -KCDefine.B_VAL_1_FLT, KCDefine.B_VAL_1_FLT)
			};
		}
	}
	
	/** 픽셀을 변경한다 */
	public static void ExSetPixels(this Texture2D a_oSender, List<Color> a_oColorList, int a_nMipLevel = KCDefine.B_VAL_0_INT, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oColorList != null));

		// 색상이 존재 할 경우
		if(a_oSender != null && a_oColorList != null) {
			a_oSender.SetPixels(a_oColorList.ToArray(), a_nMipLevel);
			a_oSender.Apply();
		}
	}

	/** 직렬화 속성 값을 변경한다 */
	public static void ExSetPropertyVal(this SerializedObject a_oSender, string a_oName, System.Action<SerializedProperty> a_oCallback, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || (a_oSender != null && a_oName.ExIsValid()));

		// 객체가 존재 할 경우
		if(a_oSender != null && a_oName.ExIsValid()) {
			a_oCallback?.Invoke(a_oSender.FindProperty(a_oName));

			a_oSender.ApplyModifiedProperties();
			a_oSender.Update();
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
