﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

#if UNITY_IOS
using UnityEngine.iOS;
#endif			// #if UNITY_IOS

#if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE
using UnityEngine.Rendering.Universal;
#endif			// #if UNIVERSAL_RENDERING_PIPELINE_MODULE_ENABLE

namespace InitScene {
	/** 초기화 씬 관리자 */
	public abstract partial class CInitSceneManager : CSceneManager {
		#region 변수
		protected List<string> m_oSpriteAtlasPathList = new List<string>();
		#endregion			// 변수

		#region 클래스 변수
		/** =====> 객체 <===== */
		private static GameObject m_oBlindUIs = null;
		#endregion         // 클래스 변수

		#region 프로퍼티
		public override string SceneName => KCDefine.B_SCENE_N_INIT;

#if UNITY_EDITOR
		public override int ScriptOrder => KCDefine.U_SCRIPT_O_INIT_SCENE_MANAGER;
#endif			// #if UNITY_EDITOR
		#endregion			// 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake() {
			base.Awake();

			for(int i = 0; i < KCDefine.U_ASSET_P_SPRITE_ATLAS_LIST.Count; ++i) {
				m_oSpriteAtlasPathList.ExAddVal(KCDefine.U_ASSET_P_SPRITE_ATLAS_LIST[i]);
			}
		}

		/** 초기화 */
		public sealed override void Start() {
			base.Start();

			for(int i = 0; i < m_oSpriteAtlasPathList.Count; ++i) {
				CResManager.Inst.LoadSpriteAtlas(m_oSpriteAtlasPathList[i]);
			}

			StartCoroutine(this.OnStart());
		}

		/** 씬을 설정한다 */
		protected virtual void Setup() {
			this.SetupBlindUIs();
			DOTween.SetTweensCapacity(KCDefine.U_SIZE_DOTWEEN_ANI, KCDefine.U_SIZE_DOTWEEN_SEQUENCE_ANI);

			// 테이블을 로드한다
			CValTable.Inst.LoadValsFromRes(KCDefine.U_TABLE_P_G_COMMON_VAL);
			CStrTable.Inst.LoadStrsFromRes(KCDefine.U_TABLE_P_G_COMMON_STR);

			// 저장소를 로드한다 {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
			CCommonAppInfoStorage.Inst.LoadAppInfo();
			CCommonUserInfoStorage.Inst.LoadUserInfo();
			CCommonGameInfoStorage.Inst.LoadGameInfo();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			// 저장소를 로드한다 }
			
			// 사운드 관리자를 설정한다 {
#if MODE_2D_ENABLE
			CSndManager.Inst.IsIgnoreBGSndEffects = true;
			CSndManager.Inst.IsIgnoreFXSndsEffects = true;

			CSndManager.Inst.IsIgnoreBGSndReverbZones = true;
			CSndManager.Inst.IsIgnoreFXSndsReverbZones = true;

			CSndManager.Inst.IsIgnoreBGSndListenerEffects = true;
			CSndManager.Inst.IsIgnoreFXSndsListenerEffects = true;
#else
			CSndManager.Inst.IsIgnoreBGSndEffects = false;
			CSndManager.Inst.IsIgnoreFXSndsEffects = false;

			CSndManager.Inst.IsIgnoreBGSndReverbZones = false;
			CSndManager.Inst.IsIgnoreFXSndsReverbZones = false;

			CSndManager.Inst.IsIgnoreBGSndListenerEffects = false;
			CSndManager.Inst.IsIgnoreFXSndsListenerEffects = false;
#endif			// #if MODE_2D_ENABLE

#if NEWTON_SOFT_JSON_MODULE_ENABLE
			CSndManager.Inst.BGSndVolume = CCommonGameInfoStorage.Inst.GameInfo.BGSndVolume;
			CSndManager.Inst.FXSndsVolume = CCommonGameInfoStorage.Inst.GameInfo.FXSndsVolume;
			
			CSndManager.Inst.IsMuteBGSnd = CCommonGameInfoStorage.Inst.GameInfo.IsMuteBGSnd;
			CSndManager.Inst.IsMuteFXSnds = CCommonGameInfoStorage.Inst.GameInfo.IsMuteFXSnds;
			CSndManager.Inst.IsDisableVibrate = CCommonGameInfoStorage.Inst.GameInfo.IsDisableVibrate;
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			// 사운드 관리자를 설정한다 }

#if (UNITY_EDITOR || UNITY_STANDALONE) && (DEBUG || DEVELOPMENT_BUILD)
			Screen.SetResolution((int)CAccess.CorrectDesktopScreenSize.x, (int)CAccess.CorrectDesktopScreenSize.y, FullScreenMode.MaximizedWindow);
#else
			Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
#endif           // #if UNITY_STANDALONE && (DEBUG || DEVELOPMENT_BUILD)

			// 디바이스 정보를 설정한다 {
			var oTargetFrameInfoDict = new Dictionary<RuntimePlatform, (long, long)>() {
				// 모바일
				[RuntimePlatform.Android] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_MOBILE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_MOBILE_TARGET_FRAME_RATE)),
				[RuntimePlatform.IPhonePlayer] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_MOBILE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_MOBILE_TARGET_FRAME_RATE)),

				// 콘솔
				[RuntimePlatform.PS4] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_TARGET_FRAME_RATE)),
				[RuntimePlatform.PS5] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_TARGET_FRAME_RATE)),
				[RuntimePlatform.XboxOne] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_CONSOLE_TARGET_FRAME_RATE)),

				// 휴대용 콘솔
				[RuntimePlatform.Switch] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_HANDHELD_CONSOLE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_HANDHELD_CONSOLE_TARGET_FRAME_RATE)),
				[RuntimePlatform.Stadia] = (CValTable.Inst.GetInt(KCDefine.VT_KEY_HANDHELD_CONSOLE_QUALITY_LEVEL), CValTable.Inst.GetInt(KCDefine.VT_KEY_HANDHELD_CONSOLE_TARGET_FRAME_RATE))
			};

			long nTargetFrameRate = oTargetFrameInfoDict.ContainsKey(Application.platform) ? oTargetFrameInfoDict[Application.platform].Item2 : CValTable.Inst.GetInt(KCDefine.VT_KEY_DEF_TARGET_FRAME_RATE);
			Application.targetFrameRate = (int)Mathf.Max(KCDefine.B_TARGET_FRAME_RATE, Mathf.Min(Screen.currentResolution.refreshRate, nTargetFrameRate));

#if MULTI_TOUCH_ENABLE
			Input.multiTouchEnabled = true;
#else
			Input.multiTouchEnabled = false;
#endif			// #if MULTI_TOUCH_ENABLE

#if UNITY_EDITOR
			CSceneManager.SetupQuality(COptsInfoTable.Inst.QualityOptsInfo.m_eQualityLevel, true);
#else
			CSceneManager.SetupQuality(oTargetFrameInfoDict.ContainsKey(Application.platform) ? (EQualityLevel)oTargetFrameInfoDict[Application.platform].Item1 : (EQualityLevel)CValTable.Inst.GetInt(KCDefine.VT_KEY_DEF_QUALITY_LEVEL), true);
#endif			// #if UNITY_EDITOR
			// 디바이스 정보를 설정한다 }
		}

		/** 다음 씬을 로드한다 */
		protected void LoadNextScene() {
			CSceneLoader.Inst.LoadScene(KCDefine.B_SCENE_N_START, false);
		}

		/** 블라인드 이미지를 생성한다 */
		protected virtual Image CreateBlindImg(string a_oName, GameObject a_oParent) {
			return CFactory.CreateCloneObj<Image>(a_oName, CResManager.Inst.GetRes<GameObject>(KCDefine.IS_OBJ_P_SCREEN_BLIND_IMG), a_oParent);
		}
		
		/** 스플래시를 출력한다 */
		protected abstract void ShowSplash();

		/** 초기화 */
		private IEnumerator OnStart() {
			yield return CFactory.CreateWaitForSecs(KCDefine.U_DELAY_INIT);

			// iOS 를 설정한다 {
#if UNITY_IOS
			Device.hideHomeButton = false;

			Device.SetNoBackupFlag(KCDefine.B_DIR_P_WRITABLE);
			Device.SetNoBackupFlag(KCDefine.U_IMG_P_SCREENSHOT);
#endif			// #if UNITY_IOS
			// iOS 를 설정한다 }

			// 관리자를 생성한다 {
			CSndManager.Create();
			CResManager.Create();
			CTaskManager.Create();
			CScheduleManager.Create();
			CNavStackManager.Create();
			CIndicatorManager.Create();
			CCollectionManager.Create();
			
#if ADS_MODULE_ENABLE
			CAdsManager.Create();
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
			CFlurryManager.Create();
#endif			// #if FLURRY_MODULE_ENABLE

#if FACEBOOK_MODULE_ENABLE
			CFacebookManager.Create();
#endif			// #if FACEBOOK_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE
			CFirebaseManager.Create();
#endif			// #if FIREBASE_MODULE_ENABLE

#if GAME_CENTER_MODULE_ENABLE
			CGameCenterManager.Create();
#endif			// #if GAME_CENTER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
			CPurchaseManager.Create();
#endif			// #if PURCHASE_MODULE_ENABLE

#if NOTI_MODULE_ENABLE
			CNotiManager.Create();
#endif			// #if NOTI_MODULE_ENABLE
			// 관리자를 생성한다 }

			// 로더를 생성한다
			CSceneLoader.Create();

			// 디바이스 연동 객체를 생성한다
			CUnityMsgSender.Create();
			CDeviceMsgReceiver.Create();

			// 테이블을 생성한다 {
			CValTable.Create();
			CStrTable.Create();

			COptsInfoTable.Create(KCDefine.U_ASSET_P_G_OPTS_INFO_TABLE);
			CBuildInfoTable.Create(KCDefine.U_ASSET_P_G_BUILD_INFO_TABLE);
			CProjInfoTable.Create(KCDefine.U_ASSET_P_G_PROJ_INFO_TABLE);
			CLocalizeInfoTable.Create(KCDefine.U_ASSET_P_G_LOCALIZE_INFO_TABLE);
			CDefineSymbolInfoTable.Create(KCDefine.U_ASSET_P_G_DEFINE_SYMBOL_INFO_TABLE);
			CDeviceInfoTable.Create(KCDefine.U_ASSET_P_G_DEVICE_INFO_TABLE);

#if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE
			CPluginInfoTable.Create(KCDefine.U_ASSET_P_G_PLUGIN_INFO_TABLE);
#endif			// #if ADS_MODULE_ENABLE || FLURRY_MODULE_ENABLE || APPS_FLYER_MODULE_ENABLE

#if PURCHASE_MODULE_ENABLE
			CProductInfoTable.Create(KCDefine.U_ASSET_P_G_PRODUCT_INFO_TABLE);
#endif			// #if PURCHASE_MODULE_ENABLE
			// 테이블을 생성한다 }

			// 저장소를 생성한다 {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
			CCommonAppInfoStorage.Create();
			CCommonUserInfoStorage.Create();
			CCommonGameInfoStorage.Create();
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
			// 저장소를 생성한다 }

			this.Setup();
			yield return CFactory.CreateWaitForSecs(KCDefine.U_DELAY_INIT);

			this.ShowSplash();

			CSceneManager.IsInit = true;
			yield return CFactory.CreateWaitForSecs(KCDefine.U_DELAY_INIT);
		}

		/** 블라인드 UI 를 설정한다 */
		private void SetupBlindUIs() {
			// 블라인드 UI 가 없을 경우
			if(CInitSceneManager.m_oBlindUIs == null) {
				CInitSceneManager.m_oBlindUIs = CFactory.CreateCloneObj(KCDefine.U_OBJ_N_BLIND_UIS, CResManager.Inst.GetRes<GameObject>(KCDefine.IS_OBJ_P_SCREEN_BLIND_UIS), null);

				try {
					CSceneManager.ScreenBlindUIs = CInitSceneManager.m_oBlindUIs.ExFindChild(KCDefine.U_OBJ_N_SCREEN_BLIND_UIS);

					// 블라인드 이미지를 설정한다 {
					var oImgList = new List<Image>() {
						this.CreateBlindImg(KCDefine.U_OBJ_N_UP_BLIND_IMG, CSceneManager.ScreenBlindUIs), this.CreateBlindImg(KCDefine.U_OBJ_N_DOWN_BLIND_IMG, CSceneManager.ScreenBlindUIs), this.CreateBlindImg(KCDefine.U_OBJ_N_LEFT_BLIND_IMG, CSceneManager.ScreenBlindUIs), this.CreateBlindImg(KCDefine.U_OBJ_N_RIGHT_BLIND_IMG, CSceneManager.ScreenBlindUIs)
					};
					
					for(int i = 0; i < oImgList.Count; ++i) {
						oImgList[i].color = KCDefine.U_COLOR_TRANSPARENT;
						oImgList[i].raycastTarget = false;
					}
					// 블라인드 이미지를 설정한다 }
				} finally {
					DontDestroyOnLoad(CInitSceneManager.m_oBlindUIs);
					CFunc.SetupScreenUIs(CInitSceneManager.m_oBlindUIs, KCDefine.U_SORTING_O_SCREEN_BLIND_UIS);
				}
			}
		}
		#endregion			// 함수
	}
}