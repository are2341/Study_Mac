using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

//! 스크립트 객체 생성자
public static class CScriptableObjCreator {
	#region 클래스 함수
	//! 전처리기 심볼 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/DefineSymbolTable")]
	public static void CreateDefineSymbolTable() {
		var oDefineSymbolTable = CEditorFactory.CreateScriptableObj<CDefineSymbolTable>();

		oDefineSymbolTable.SetCommonDefineSymbolList(new List<string>() {
			KCEditorDefine.DS_DEFINE_S_DOTWEEN_ENABLE,
			KCEditorDefine.DS_DEFINE_S_GOOGLE_REVIEW_ENABLE,
			KCEditorDefine.DS_DEFINE_S_GOOGLE_UPDATE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_SECURITY_ENABLE,
			KCEditorDefine.DS_DEFINE_S_DYNAMIC_BATCHING_ENABLE,
			KCEditorDefine.DS_DEFINE_S_VISUAL_FX_GRAPH_ENABLE,
			KCEditorDefine.DS_DEFINE_S_UNIVERSAL_PIPELINE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_HAPTIC_FEEDBACK_ENABLE,
			KCEditorDefine.DS_DEFINE_S_LIGHTMAP_BAKE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_LIGHTMAP_AUTO_BAKE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_LIGHTMAP_SHADOW_BAKE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_LZ4_COMPRESS_ENABLE,
			KCEditorDefine.DS_DEFINE_S_INPUT_SYSTEM_ENABLE,
			KCEditorDefine.DS_DEFINE_S_ADDRESSABLES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_BURST_COMPILER_ENABLE,
			KCEditorDefine.DS_DEFINE_S_ADAPTIVE_PERFORMANCE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_MSG_PACK_ENABLE,
			KCEditorDefine.DS_DEFINE_S_MODE_2D_ENABLE
		});
	}

	//! 디바이스 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/DeviceInfoTable")]
	public static void CreateDeviceInfoTable() {
		var oDeviceInfoTable = CEditorFactory.CreateScriptableObj<CDeviceInfoTable>();

		oDeviceInfoTable.SetDeviceInfo(new STDeviceInfo() {
#if ADS_MODULE_ENABLE && ADMOB_ENABLE
			m_oiOSAdmobIDList = new List<string>() {
				"cda4cbaf3ee1d95d96c9316c45ca1163",
				"b18f2566214cc419bac71a7c3df368ad"
			},

			m_oAndroidAdmobIDList = new List<string>() {
				"20883ADA1C122EA63419F9AF1FAD52F0",
				"3E2176530B8BACFCD9C26AD844C0B3C3",
				"7B64C62090AB8F29E66895C06523A72B"
			}
#endif			// #if ADS_MODULE_ENABLE && ADMOB_ENABLE
		});

		oDeviceInfoTable.SetDeviceConfig(new STDeviceConfig() {
			m_oiOSAdsIDList = new List<string>() {
				"843D04C1-BA66-4CAA-8C28-42CE142E36F7"
			},

			m_oAndroidAdsIDList = new List<string>() {
				"2e9584fb-838a-4885-9ec4-2527eab41ad1",
				"b1db7f05-768b-4060-aa0e-e0424faedfb5",
				"ac5dfbfd-0501-4d6d-a1fc-4fc10aa6d823"
			}
		});
	}

	//! 빌드 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/BuildInfoTable")]
	public static void CreateBuildInfoTable() {
		var oBuildInfoTable = CEditorFactory.CreateScriptableObj<CBuildInfoTable>();

		oBuildInfoTable.SetJenkinsInfo(new STJenkinsInfo() {
			m_oUserID = "dante",

			m_oBranch = "master",
			m_oSrc = "000011.Sample_Unity",
			m_oWorkspace = "/Users/dante/Documents/jenkins/workspace",
			
			m_oBuildToken = "JenkinsBuild",
			m_oAccessToken = "11736e2ed3cbca541394372e057ad0ba5a",

			m_oBuildURLFmt = "http://localhost:8080/{0}/buildWithParameters"
		});

		oBuildInfoTable.SetStandaloneBuildInfo(new STStandaloneBuildInfo() {
			m_oCategory = "public.app-category.games"
		});

		oBuildInfoTable.SetiOSBuildInfo(new STiOSBuildInfo() {
			m_oTeamID = "8XBEE2A299",
			m_oTargetOSVer = "11.0",

			m_oDevProfileID = "c964937f-b8a3-4a09-b910-f74280cde2e7",
			m_oAdhocProfileID = "867d6edd-c3d5-46e8-bcbc-ff8de0e99b5f",
			m_oStoreProfileID = "5cc0de0a-785f-47c6-ab1e-b7274b35dca3",

			m_oURLSchemeList = new List<string>() {
				"sample"
			},

			m_eiPadLaunchScreenType = iOSLaunchScreenType.Default,
			m_eiPhoneLaunchScreenType = iOSLaunchScreenType.Default
		});

		oBuildInfoTable.SetAndroidBuildInfo(new STAndroidBuildInfo() {
			m_oKeystorePath = "Keystore.keystore",
			m_oKeyaliasName = "Keystore",

			m_oKeystorePassword = "NSString132!",
			m_oKeyaliasPassword = "NSString132!",

			m_eMinSDKVer = AndroidSdkVersions.AndroidApiLevel21,
			m_eTargetSDKVer = AndroidSdkVersions.AndroidApiLevel29
		});
	}

	//! 빌드 옵션 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/BuildOptsTable")]
	public static void CreateBuildOptsTable() {
		var oBuildOptsTable = CEditorFactory.CreateScriptableObj<CBuildOptsTable>();

		oBuildOptsTable.SetEditorOpts(new STEditorOpts() {
			m_bIsAsyncShaderCompile = true,
			m_bIsBurstCompileEnable = true,
			m_bIsCacheShaderPreprocessor = false,
			m_bIsUseLegacyProbeSampleCount = true,
			m_bIsEnableTextureStreamingInPlayMode = true,
			m_bIsEnableTextureStreamingInEditMode = true,

			m_eCacheServerMode = CacheServerMode.AsPreferences,
			m_eLineEndingMode = LineEndingsMode.Windows,
			m_eTextureCompressionType = ETextureCompressionType.DEFAULT
		});

		oBuildOptsTable.SetSndOpts(new STSndOpts() {
			m_bIsDisable = false,
			m_bIsVirtualizeEffect = true,

			m_nSampleRate = 0,
			m_nNumRealVoices = 32,
			m_nNumVirtualVoices = 512,

			m_fGlobalVolume = 1.0f,
			m_fRolloffScale = 1.0f,
			m_fDopplerFactor = 1.0f,

			m_eSpeakerMode = AudioSpeakerMode.Mono,
			m_eDSPBufferSize = EDSPBufferSize.BEST_PERFORMANCE
		});

		oBuildOptsTable.SetCommonBuildOpts(new STCommonBuildOpts() {
			m_bIsGPUSkinning = true,
			m_bIsMTRendering = true,
			m_bIsRunInBackground = false,
			m_bIsPreBakeCollisionMesh = false,
			m_bIsUse32BitDisplayBuffer = true,
			m_bIsMuteOtherAudioSource = false,
			m_bIsEnableFrameTimingStats = true,
			m_bIsEnableInternalProfiler = false,
			m_bIsPreserveFrameBufferAlpha = false,
			m_bIsEnableVulkanSRGBWrite = false,
			m_bIsEnableLightmapStreaming = true,
			
			m_nLightmapStreamingPriority = 0,
			m_nNumVulkanSwapChainBuffers = 3,

			m_oAOTCompileOpts = string.Empty,
			m_oPreloadAssetList = null,
			
			m_eAccelerometerFrequency = EAccelerometerFrequency.FREQUENCY_60_HZ,
			m_eLightmapEncodingQuality = ELightmapEncodingQuality.LOW
		});

		oBuildOptsTable.SetStandaloneBuildOpts(new STStandaloneBuildOpts() {
			m_bIsForceSingleInstance = true,
			m_bIsCaptureSingleScreen = false,
			
			m_eFullscreenMode = FullScreenMode.Windowed
		});

		oBuildOptsTable.SetiOSBuildOpts(new STiOSBuildOpts() {
			m_bIsEnableProMotion = false,
			m_bIsRequreARKitSupports = false,
			m_bIsRequirePersistentWIFI = false,
			m_bIsAutoAddCapabilities = true,

			m_oCameraDescription = string.Empty,
			m_oLocationDescription = string.Empty,
			m_oMicrophoneDescription = string.Empty,

			m_eTargetDevice = iOSTargetDevice.iPhoneAndiPad,
			m_eStatusBarStyle = iOSStatusBarStyle.Default,
			m_eBackgroundBehavior = iOSAppInBackgroundBehavior.Suspend
		});

		oBuildOptsTable.SetAndroidBuildOpts(new STAndroidBuildOpts() {
			m_bIsRenderOutside = true,
			m_bIsTVCompatibility = false,
			
			m_nAppBundleSize = 150,

			m_eBlitType = AndroidBlitType.Always,
			m_eAspectRatioMode = EAspectRatioMode.NATIVE_ASPECT_RATIO,
			m_ePreferredInstallLocation = AndroidPreferredInstallLocation.ForceInternal
		});
	}

	//! 프로젝트 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/ProjInfoTable")]
	public static void CreateProjInfoTable() {
		var oProjInfoTable = CEditorFactory.CreateScriptableObj<CProjInfoTable>();
		oProjInfoTable.SetCompanyName("LK Studio");

		oProjInfoTable.SetServicesURL("https://www.ninetap.com/terms_of_service.html");
		oProjInfoTable.SetPrivacyURL("https://www.ninetap.com/privacy_policy.html");

		oProjInfoTable.SetProjName("Sample_Unity");
		oProjInfoTable.SetProductName("Sample");
		oProjInfoTable.SetShortProductName("Sample");

		oProjInfoTable.SetMacProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = string.Empty,
			m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetWndsProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = string.Empty,
			m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetiOSProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = "https://itunes.apple.com/app/id1309472470",
			m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetGoogleProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = "https://play.google.com/store/apps/details?id=dante.distribution.sample",
			m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetOneStoreProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = string.Empty,
			m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetGalaxyStoreProjInfo(new STProjInfo() {
			m_stBuildVer = new STBuildVer() {
				m_nNum = 1,
				m_oVer = "1.0.0",
			},

			m_oAppID = "dante.distribution.sample",

			m_oStoreURL = string.Empty,
			m_oSupportsMail = "are2341@nate.com"
		});
	}

	//! 플러그인 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/PluginInfoTable")]
	public static void CreatePluginInfoTable() {
		var oPluginInfoTable = CEditorFactory.CreateScriptableObj<CPluginInfoTable>();

#if ADS_MODULE_ENABLE
		oPluginInfoTable.SetDefAdsType(EAdsType.NONE);
		oPluginInfoTable.SetBannerAdsPos(EBannerAdsPos.NONE);

#if ADMOB_ENABLE
		oPluginInfoTable.SetiOSAdmobPluginInfo(new STAdmobPluginInfo() {
			m_oAppID = "ca-app-pub-4429226069711533~9313515606",

			m_oBannerAdsID = "ca-app-pub-4429226069711533/9321907041",
			m_oRewardAdsID = "ca-app-pub-4429226069711533/1443417026",
			m_oFullscreenAdsID = "ca-app-pub-4429226069711533/6695743706",
			m_oResumeAdsID = "ca-app-pub-4429226069711533/3884880906"
		});

		oPluginInfoTable.SetAndroidAdmobPluginInfo(new STAdmobPluginInfo() {
			m_oAppID = "ca-app-pub-4429226069711533~7000833607",

			m_oBannerAdsID = "ca-app-pub-4429226069711533/6026208607",
			m_oRewardAdsID = "ca-app-pub-4429226069711533/1279765492",
			m_oFullscreenAdsID = "ca-app-pub-4429226069711533/8460800259",
			m_oResumeAdsID = "ca-app-pub-4429226069711533/6339653854"
		});
#endif			// #if ADMOB_ENABLE

#if IRON_SRC_ENABLE
		oPluginInfoTable.SetiOSIronSrcPluginInfo(new STIronSrcPluginInfo() {
			m_oAppKey = "aca5425d",

			m_oBannerAdsID = IronSourceAdUnits.BANNER,
			m_oRewardAdsID = IronSourceAdUnits.REWARDED_VIDEO,
			m_oFullscreenAdsID = IronSourceAdUnits.INTERSTITIAL
		});

		oPluginInfoTable.SetAndroidIronSrcPluginInfo(new STIronSrcPluginInfo() {
			m_oAppKey = "b8c2c725",

			m_oBannerAdsID = IronSourceAdUnits.BANNER,
			m_oRewardAdsID = IronSourceAdUnits.REWARDED_VIDEO,
			m_oFullscreenAdsID = IronSourceAdUnits.INTERSTITIAL
		});
#endif			// #if IRON_SRC_ENABLE

#if APP_LOVIN_ENABLE
		oPluginInfoTable.SetAppLovinSDKKey(string.Empty);

		oPluginInfoTable.SetiOSAppLovinPluginInfo(new STAppLovinPluginInfo() {
			m_oBannerAdsID = string.Empty,
			m_oRewardAdsID = string.Empty,
			m_oFullscreenAdsID = string.Empty
		});

		oPluginInfoTable.SetAndroidAppLovinPluginInfo(new STAppLovinPluginInfo() {
			m_oBannerAdsID = string.Empty,
			m_oRewardAdsID = string.Empty,
			m_oFullscreenAdsID = string.Empty
		});
#endif			// #if APP_LOVIN_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
		oPluginInfoTable.SetiOSFlurryAPIKey("KMVH2DR22CBQWMZJ6XJ8");
		oPluginInfoTable.SetAndroidFlurryAPIKey("9B4TJSM4BFJ9N4J2X83D");
#endif			// #if FLURRY_MODULE_ENABLE

#if TENJIN_MODULE_ENABLE
		oPluginInfoTable.SetTenjinAPIKey("ZAI7IG4Y9G6YW1H3DTMNUX7GBFT8FLWS");
#endif			// #if TENJIN_MODULE_ENABLE

#if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE
		oPluginInfoTable.SetFirebaseDBURL("https://sample-61739440.firebaseio.com");
#endif			// #if FIREBASE_MODULE_ENABLE && FIREBASE_DB_ENABLE

#if SINGULAR_MODULE_ENABLE
		oPluginInfoTable.SetSingularPluginInfo(new STSingularPluginInfo() {
			m_oAPIKey = string.Empty,
			m_oAPISecret = string.Empty
		});
#endif			// #if SINGULAR_MODULE_ENABLE
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if PURCHASE_MODULE_ENABLE
	//! 상품 정보 테이블을 생성한다
	[MenuItem("Tools/Utility/Create/ProductInfoTable")]
	public static void CreateProductInfoTable() {
		var oProductInfoTable = CEditorFactory.CreateScriptableObj<CProductInfoTable>();

		oProductInfoTable.SetCommonProductInfoList(new List<STProductInfo>() {
			new STProductInfo {
				m_oID = "dante.distribution.sample.iap.c.sample",
				m_eProductType = ProductType.Consumable
			},

			new STProductInfo {
				m_oID = "dante.distribution.sample.iap.nc.sample",
				m_eProductType = ProductType.NonConsumable
			}
		});
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_EDITOR
