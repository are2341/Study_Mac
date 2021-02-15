using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif			// #if UNITY_EDITOR

//! 씬 관리자 - UI
public abstract partial class CSceneManager : CComponent {
	#region 조건부 함수
#if UNITY_EDITOR
	//! 가이드 라인을 그린다
	public virtual void OnDrawGizmos() {
		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			float fScale = CAccess.GetResolutionScale();

			var oCanvasPoses = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -2.0f, CSceneManager.CanvasSize.y / -2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / -2.0f, CSceneManager.CanvasSize.y / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / 2.0f, CSceneManager.CanvasSize.y / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / 2.0f, CSceneManager.CanvasSize.y / -2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale)
			};

			var oScreenPoses = new Vector3[] {
				new Vector3(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / -2.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / 2.0f, KCDefine.B_SCREEN_HEIGHT / -2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale)
			};

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
			var oEditorScreenPoses = new Vector3[] {
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -2.0f) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / 2.0f, 0.0f),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / 2.0f) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -2.0f, 0.0f)
			};
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

#if ADS_MODULE_ENABLE
			var stPhoneBannerAdsSize = KCDefine.U_SIZE_PHONE_BANNER_ADS;
			var stBannerAdsSize = (CAccess.GetDeviceType() == EDeviceType.PHONE) ? stPhoneBannerAdsSize : KCDefine.U_SIZE_TABLET_BANNER_ADS;

			float fBannerAdsHeight = CAccess.GetBannerAdsHeight(stBannerAdsSize.y);

			var oAdsScreenPoses = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -2.0f, (CSceneManager.CanvasSize.y / -2.0f) + fBannerAdsHeight, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / -2.0f, CSceneManager.CanvasSize.y / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / 2.0f, CSceneManager.CanvasSize.y / 2.0f, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale),
				new Vector3(CSceneManager.CanvasSize.x / 2.0f, (CSceneManager.CanvasSize.y / -2.0f) + fBannerAdsHeight, KCDefine.B_VALUE_FLT_0) * (KCDefine.B_UNIT_SCALE * fScale)
			};
#endif			// #if ADS_MODULE_ENABLE

			for(int i = 0; i < oCanvasPoses.Length; ++i) {
				var stPrevColor = Gizmos.color;

				int nIdxA = (i + KCDefine.B_VALUE_INT_0) % oCanvasPoses.Length;;
				int nIdxB = (i + KCDefine.B_VALUE_INT_1) % oCanvasPoses.Length;

				Gizmos.color = Color.white;
				Gizmos.DrawLine(oCanvasPoses[nIdxA], oCanvasPoses[nIdxB]);

				Gizmos.color = Color.green;
				Gizmos.DrawLine(oScreenPoses[nIdxA], oScreenPoses[nIdxB]);

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(oEditorScreenPoses[nIdxA], oEditorScreenPoses[nIdxB]);
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

#if ADS_MODULE_ENABLE
				Gizmos.color = Color.red;
				Gizmos.DrawLine(oAdsScreenPoses[nIdxA], oAdsScreenPoses[nIdxB]);
#endif			// #if ADS_MODULE_ENABLE

				Gizmos.color = stPrevColor;
			}
		}
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
