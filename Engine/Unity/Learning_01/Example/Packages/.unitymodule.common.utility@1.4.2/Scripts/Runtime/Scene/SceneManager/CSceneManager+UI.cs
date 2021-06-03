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
	//! GUI 를 그린다
	public virtual void OnGUI() {
		//! 초기화가 필요 할 경우
		if(!CSceneManager.IsInit && Camera.main != null) {
			var stPrevColor = GUI.color;

			try {
				GUI.color = Color.black;
				GUI.DrawTexture(Camera.main.pixelRect, Texture2D.whiteTexture, ScaleMode.StretchToFill);

				var oTexture = Resources.Load<Texture2D>(KCDefine.U_IMG_P_G_SPLASH);
				var stTextureSize = Camera.main.pixelRect.size / KCDefine.B_VAL_4_FLT;
				var stTextureRect = new Rect(Camera.main.pixelRect.center - (stTextureSize / KCDefine.B_VAL_2_FLT), stTextureSize);

				GUI.color = Color.white;
				GUI.DrawTexture(stTextureRect, oTexture, ScaleMode.ScaleToFit);
			} finally {
				GUI.color = stPrevColor;
			}
		}
	}

	//! 가이드 라인을 그린다
	public virtual void OnDrawGizmos() {
		// 메인 카메라가 존재 할 경우
		if(CSceneManager.IsExistsMainCamera) {
			var oCanvasPoses = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};

			var oScreenPoses = new Vector3[] {
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(KCDefine.B_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT, KCDefine.B_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};

#if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE
			var oEditorScreenPoses = new Vector3[] {
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / -KCDefine.B_VAL_2_FLT) - KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT),
				new Vector3((KCDefine.B_WORLD_SCREEN_WIDTH / KCDefine.B_VAL_2_FLT) + KCDefine.B_WORLD_SCREEN_WIDTH, KCDefine.B_WORLD_SCREEN_HEIGHT / -KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT)
			};
#endif			// #if UNITY_STANDALONE && MODE_PORTRAIT_ENABLE

#if ADS_MODULE_ENABLE
			var stBannerAdsSize = (CAccess.DeviceType == EDeviceType.PHONE) ? KCDefine.U_SIZE_PHONE_BANNER_ADS : KCDefine.U_SIZE_TABLET_BANNER_ADS;
			float fBannerAdsHeight = CAccess.GetBannerAdsHeight(stBannerAdsSize.y);

			var oAdsScreenPoses = new Vector3[] {
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + fBannerAdsHeight, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / -KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, CSceneManager.CanvasSize.y / KCDefine.B_VAL_2_FLT, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale),
				new Vector3(CSceneManager.CanvasSize.x / KCDefine.B_VAL_2_FLT, (CSceneManager.CanvasSize.y / -KCDefine.B_VAL_2_FLT) + fBannerAdsHeight, KCDefine.B_VAL_0_FLT) * (KCDefine.B_UNIT_SCALE * CAccess.ResolutionScale)
			};
#endif			// #if ADS_MODULE_ENABLE

			for(int i = 0; i < oCanvasPoses.Length; ++i) {
				var stPrevColor = Gizmos.color;

				try {
					int nIdxA = (i + KCDefine.B_VAL_0_INT) % oCanvasPoses.Length;;
					int nIdxB = (i + KCDefine.B_VAL_1_INT) % oCanvasPoses.Length;

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
				} finally {
					Gizmos.color = stPrevColor;
				}
			}
		}
	}
#endif			// #if UNITY_EDITOR
	#endregion			// 조건부 함수
}
