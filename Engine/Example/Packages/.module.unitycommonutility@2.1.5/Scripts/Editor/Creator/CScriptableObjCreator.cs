using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

#if PURCHASE_MODULE_ENABLE
using UnityEngine.Purchasing;
#endif			// #if PURCHASE_MODULE_ENABLE

/** 스크립트 객체 생성자 */
public static class CScriptableObjCreator {
	#region 클래스 함수
	/** 옵션 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "OptsInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateOptsInfoTable() {
		var oOptsInfoTable = CEditorFactory.CreateScriptableObj<COptsInfoTable>();

		oOptsInfoTable.SetEtcOptsInfo(new STEtcOptsInfo() {
			m_bIsEnableTitleScene = false
		});
		
		oOptsInfoTable.SetSndOptsInfo(new STSndOptsInfo() {
			m_nNumRealVoices = 32, m_nNumVirtualVoices = 512
		});

		oOptsInfoTable.SetBuildOptsInfo(new STBuildOptsInfo() {
			m_bIsUseSRPBatching = false, m_bIsPreBakeCollisionMesh = false, m_bIsPreserveFrameBufferAlpha = false, m_eColorSpace = ColorSpace.Gamma,

			m_stiOSBuildOptsInfo = new STiOSBuildOptsInfo() {
				m_bIsEnableMotion = false, m_oCameraDescription = string.Empty, m_oMotionDescription = string.Empty, m_oLocationDescription = string.Empty, m_oMicrophoneDescription = string.Empty
			},

			m_stAndroidBuildOptsInfo = new STAndroidBuildOptsInfo() {
				m_bIsUseAPKExpansionFiles = false
			},

			m_stStandaloneBuildOptsInfo = new STStandaloneBuildOptsInfo() {
				// Do Something
			}
		});

		oOptsInfoTable.SetQualityOptsInfo(new STQualityOptsInfo() {
			m_bIsEnableRealtimeGI = false,
			m_bIsEnableRealtimeEnvironmentLighting = false,

			m_eQualityLevel = EQualityLevel.NORM,
			m_eMixedLightingMode = MixedLightingMode.Subtractive,
			m_eLightmapper = LightingSettings.Lightmapper.ProgressiveCPU,

			m_stNormQualityRenderingOptsInfo = new STRenderingOptsInfo() {
				m_stLightOptsInfo = new STLightOptsInfo() {
					m_eLightmapMaxSize = EPowOfTwo._2048, m_eLightmapMode = ELightmapMode.NON_DIRECTIONAL, m_eShadowmaskMode = ShadowmaskMode.Shadowmask, m_eShadowResolution = ShadowResolution.Medium, m_eLightmapCompression = LightmapCompression.NormalQuality
				},

				m_stUniversalRPOptsInfo = new STUniversalRPOptsInfo() {
					m_eLightCookieResolution = EPowOfTwo._512, m_eMainLightShadowResolution = EPowOfTwo._2048, m_eAdditionalShadowResolution = EPowOfTwo._1024
				}
			},

			m_stHighQualityRenderingOptsInfo = new STRenderingOptsInfo() {
				m_stLightOptsInfo = new STLightOptsInfo() {
					m_eLightmapMaxSize = EPowOfTwo._4096, m_eLightmapMode = ELightmapMode.COMBINE_DIRECTIONAL, m_eShadowmaskMode = ShadowmaskMode.DistanceShadowmask, m_eShadowResolution = ShadowResolution.High, m_eLightmapCompression = LightmapCompression.HighQuality
				},

				m_stUniversalRPOptsInfo = new STUniversalRPOptsInfo() {
					m_eLightCookieResolution = EPowOfTwo._1024, m_eMainLightShadowResolution = EPowOfTwo._4096, m_eAdditionalShadowResolution = EPowOfTwo._2048
				}
			},

			m_stUltraQualityRenderingOptsInfo = new STRenderingOptsInfo() {
				m_stLightOptsInfo = new STLightOptsInfo() {
					m_eLightmapMaxSize = EPowOfTwo._4096, m_eLightmapMode = ELightmapMode.COMBINE_DIRECTIONAL, m_eShadowmaskMode = ShadowmaskMode.DistanceShadowmask, m_eShadowResolution = ShadowResolution.VeryHigh, m_eLightmapCompression = LightmapCompression.HighQuality
				},

				m_stUniversalRPOptsInfo = new STUniversalRPOptsInfo() {
					m_eLightCookieResolution = EPowOfTwo._1024, m_eMainLightShadowResolution = EPowOfTwo._4096, m_eAdditionalShadowResolution = EPowOfTwo._2048
				}
			}
		});
	}

	/** 빌드 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "BuildInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateBuildInfoTable() {
		var oBuildInfoTable = CEditorFactory.CreateScriptableObj<CBuildInfoTable>();

		oBuildInfoTable.SetJenkinsInfo(new STJenkinsInfo() {
			m_oUserID = "dante", m_oBranch = "main", m_oSrc = "03000111.Sample_Unity", m_oWorkspace = "/Users/dante/Documents/jenkins/workspace", m_oBuildToken = "JenkinsBuild", m_oAccessToken = "11736e2ed3cbca541394372e057ad0ba5a", m_oBuildURLFmt = "http://localhost:8080/{0}/buildWithParameters"
		});

		oBuildInfoTable.SetCommonBuildInfo(new STCommonBuildInfo() {
			// Do Something
		});

		oBuildInfoTable.SetiOSBuildInfo(new STiOSBuildInfo() {
			m_oTeamID = "8XBEE2A299", m_oTargetOSVer = "11.0", m_oDevProfileID = "", m_oStoreProfileID = ""
		});

		oBuildInfoTable.SetAndroidBuildInfo(new STAndroidBuildInfo() {
			m_oKeystorePath = "Keystore.keystore", m_oKeyaliasName = "Keystore", m_oKeystorePassword = "NSString132!", m_oKeyaliasPassword = "NSString132!", m_eMinSDKVer = AndroidSdkVersions.AndroidApiLevel22, m_eTargetSDKVer = AndroidSdkVersions.AndroidApiLevel30
		});

		oBuildInfoTable.SetStandaloneBuildInfo(new STStandaloneBuildInfo() {
			m_oCategory = "public.app-category.games"
		});
	}

	/** 전처리기 심볼 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "DefineSymbolInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateDefineSymbolInfoTable() {
		var oDefineSymbolInfoTable = CEditorFactory.CreateScriptableObj<CDefineSymbolInfoTable>();

		oDefineSymbolInfoTable.SetCommonDefineSymbols(new List<string>() {
			KCEditorDefine.DS_DEFINE_S_MSG_PACK_ENABLE,
			KCEditorDefine.DS_DEFINE_S_VISUAL_FX_GRAPH_ENABLE,
			KCEditorDefine.DS_DEFINE_S_UNIVERSAL_RENDERING_PIPELINE_ENABLE,
			KCEditorDefine.DS_DEFINE_S_ADDRESSABLES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_BURST_COMPILER_ENABLE,
			KCEditorDefine.DS_DEFINE_S_TEXTURE_COMPRESS_ENABLE,
			KCEditorDefine.DS_DEFINE_S_MODE_2D_ENABLE,
			KCEditorDefine.DS_DEFINE_S_MODE_PORTRAIT_ENABLE,
			KCEditorDefine.DS_DEFINE_S_SCENE_TEMPLATES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_EDITOR_SCENE_TEMPLATES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_PREFAB_TEMPLATES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_ENGINE_TEMPLATES_ENABLE,
			KCEditorDefine.DS_DEFINE_S_RUNTIME_TEMPLATES_ENABLE
		});

		oDefineSymbolInfoTable.SetSubCommonDefineSymbols(new List<string>() {
			KCEditorDefine.DS_DEFINE_S_MODULE_VER_2_0_0_OR_NEWER,
			KCEditorDefine.DS_DEFINE_S_DEVELOPMENT_PROJ
		});
	}

	/** 프로젝트 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "ProjInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateProjInfoTable() {
		var oProjInfoTable = CEditorFactory.CreateScriptableObj<CProjInfoTable>();
		oProjInfoTable.SetCompany("LKStudio");

		oProjInfoTable.SetPrivacyURL("https://www.ninetap.com/privacy_policy.html");
		oProjInfoTable.SetServicesURL("https://www.ninetap.com/terms_of_service.html");

		oProjInfoTable.SetProjName("03000111.Sample_Unity");
		oProjInfoTable.SetProductName("Sample_Unity");
		oProjInfoTable.SetShortProductName("Sample_Unity");

		oProjInfoTable.SetiOSAppleProjInfo(new STProjInfo() {
			m_stBuildVerInfo = new STBuildVerInfo() {
				m_nNum = 1, m_oVer = "0.0.1",
			},

			m_oAppID = "dante.distribution.sample", m_oStoreAppID = "1309472470", m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetAndroidGoogleProjInfo(new STProjInfo() {
			m_stBuildVerInfo = new STBuildVerInfo() {
				m_nNum = 1, m_oVer = "0.0.1",
			},

			m_oAppID = "dante.distribution.sample", m_oStoreAppID = string.Empty, m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetAndroidAmazonProjInfo(new STProjInfo() {
			m_stBuildVerInfo = new STBuildVerInfo() {
				m_nNum = 1, m_oVer = "0.0.1",
			},

			m_oAppID = "dante.distribution.sample", m_oStoreAppID = string.Empty, m_oSupportsMail = "are2341@nate.com"
		});
		
		oProjInfoTable.SetStandaloneMacSteamProjInfo(new STProjInfo() {
			m_stBuildVerInfo = new STBuildVerInfo() {
				m_nNum = 1, m_oVer = "0.0.1",
			},

			m_oAppID = "dante.distribution.sample", m_oStoreAppID = string.Empty, m_oSupportsMail = "are2341@nate.com"
		});

		oProjInfoTable.SetStandaloneWndsSteamProjInfo(new STProjInfo() {
			m_stBuildVerInfo = new STBuildVerInfo() {
				m_nNum = 1, m_oVer = "0.0.1",
			},

			m_oAppID = "dante.distribution.sample", m_oStoreAppID = string.Empty, m_oSupportsMail = "are2341@nate.com"
		});
	}

	/** 디바이스 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "DeviceInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateDeviceInfoTable() {
		var oDeviceInfoTable = CEditorFactory.CreateScriptableObj<CDeviceInfoTable>();

		oDeviceInfoTable.SetDeviceInfo(new STDeviceInfo() {
#if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE
			m_oiOSTestDeviceAdmobIDList = new List<string>() {
				"cda4cbaf3ee1d95d96c9316c45ca1163", "b18f2566214cc419bac71a7c3df368ad"
			},

			m_oAndroidTestDeviceAdmobIDList = new List<string>() {
				"20883ADA1C122EA63419F9AF1FAD52F0", "3E2176530B8BACFCD9C26AD844C0B3C3", "7B64C62090AB8F29E66895C06523A72B"
			}
#endif			// #if ADS_MODULE_ENABLE && ADMOB_ADS_ENABLE
		});

		oDeviceInfoTable.SetDeviceConfig(new STDeviceConfig() {
			m_oiOSTestDeviceAdsIDList = new List<string>() {
				"843D04C1-BA66-4CAA-8C28-42CE142E36F7"
			},

			m_oAndroidTestDeviceAdsIDList = new List<string>() {
				"2e9584fb-838a-4885-9ec4-2527eab41ad1", "b1db7f05-768b-4060-aa0e-e0424faedfb5", "ac5dfbfd-0501-4d6d-a1fc-4fc10aa6d823"
			}
		});
	}

	/** 지역화 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "LocalizeInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateLocalizeInfoTable() {
		var oLocalizeInfoTable = CEditorFactory.CreateScriptableObj<CLocalizeInfoTable>();

		oLocalizeInfoTable.SetLocalizeInfos(new List<STLocalizeInfo>() {
			new STLocalizeInfo() {
				m_oCountryCode = string.Empty,
				m_eSystemLanguage = SystemLanguage.Korean,
				
				m_oFontSetInfoList = new List<STFontSetInfo>() {
					new STFontSetInfo() {
						m_eSet = EFontSet.A, m_oPath = KCDefine.U_FONT_P_G_KOREAN
					},

					new STFontSetInfo() {
						m_eSet = EFontSet.B, m_oPath = KCDefine.U_FONT_P_G_KOREAN
					}
				}
			},

			new STLocalizeInfo() {
				m_oCountryCode = string.Empty,
				m_eSystemLanguage = SystemLanguage.English,
				
				m_oFontSetInfoList = new List<STFontSetInfo> () {
					new STFontSetInfo() {
						m_eSet = EFontSet.A, m_oPath = KCDefine.U_FONT_P_G_DEF
					},

					new STFontSetInfo() {
						m_eSet = EFontSet.B, m_oPath = KCDefine.U_FONT_P_G_ENGLISH
					}
				}
			}
		});
	}

	/** 플러그인 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "PluginInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreatePluginInfoTable() {
		var oPluginInfoTable = CEditorFactory.CreateScriptableObj<CPluginInfoTable>();

#if ADS_MODULE_ENABLE
		oPluginInfoTable.SetAdsPlatform(EAdsPlatform.NONE);
		oPluginInfoTable.SetBannerAdsPos(EBannerAdsPos.NONE);

#if ADMOB_ADS_ENABLE
		/*
		iOS Admob AppID: ca-app-pub-4429226069711533~9313515606
		Android Admob AppID: ca-app-pub-4429226069711533~7000833607
		*/

		oPluginInfoTable.SetiOSAdmobPluginInfo(new STAdmobPluginInfo() {
			m_oBannerAdsID = "ca-app-pub-4429226069711533/9321907041", m_oRewardAdsID = "ca-app-pub-4429226069711533/1443417026", m_oFullscreenAdsID = "ca-app-pub-4429226069711533/6695743706"
		});

		oPluginInfoTable.SetAndroidAdmobPluginInfo(new STAdmobPluginInfo() {
			m_oBannerAdsID = "ca-app-pub-4429226069711533/6026208607", m_oRewardAdsID = "ca-app-pub-4429226069711533/1279765492", m_oFullscreenAdsID = "ca-app-pub-4429226069711533/8460800259"
		});
#endif			// #if ADMOB_ADS_ENABLE

#if IRON_SRC_ADS_ENABLE
		oPluginInfoTable.SetEnableIronSrcBannerAds(false);
		oPluginInfoTable.SetEnableIronSrcRewardAds(false);
		oPluginInfoTable.SetEnableIronSrcFullscreenAds(false);

		oPluginInfoTable.SetiOSIronSrcPluginInfo(new STIronSrcPluginInfo() {
			m_oAppKey = "aca5425d"
		});

		oPluginInfoTable.SetAndroidIronSrcPluginInfo(new STIronSrcPluginInfo() {
			m_oAppKey = "b8c2c725"
		});
#endif			// #if IRON_SRC_ADS_ENABLE

#if APP_LOVIN_ADS_ENABLE
		oPluginInfoTable.SetAppLovinSDKKey(string.Empty);

		oPluginInfoTable.SetiOSAppLovinPluginInfo(new STAppLovinPluginInfo() {
			m_oBannerAdsID = string.Empty, m_oRewardAdsID = string.Empty, m_oFullscreenAdsID = string.Empty
		});

		oPluginInfoTable.SetAndroidAppLovinPluginInfo(new STAppLovinPluginInfo() {
			m_oBannerAdsID = string.Empty, m_oRewardAdsID = string.Empty, m_oFullscreenAdsID = string.Empty
		});
#endif			// #if APP_LOVIN_ADS_ENABLE
#endif			// #if ADS_MODULE_ENABLE

#if FLURRY_MODULE_ENABLE
		oPluginInfoTable.SetiOSFlurryAPIKey("B2Q3QRQD747P52R8HNFM");
		oPluginInfoTable.SetAndroidFlurryAPIKey("W729SWD7ZC95RH8RXD99");
#endif			// #if FLURRY_MODULE_ENABLE

#if APPS_FLYER_MODULE_ENABLE
		oPluginInfoTable.SetAppsFlyerPluginInfo(new STAppsFlyerPluginInfo() {
			m_oDevKey = "J7eXAem8sBRuHTr3iX58d5"
		});
#endif			// #if APPS_FLYER_MODULE_ENABLE
	}

	/** 저장소 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "StorageInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateStorageInfoTable() {
		var oStorageInfoTable = CEditorFactory.CreateScriptableObj<CStorageInfoTable>();

		oStorageInfoTable.SetGoogleDriveInfo(new STGoogleDriveInfo() {
			m_oAccessToken = string.Empty, m_oASetAccessToken = string.Empty, m_oBSetAccessToken = string.Empty
		});
	}
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if PURCHASE_MODULE_ENABLE
	/** 상품 정보 테이블을 생성한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_CREATE_BASE + "ProductInfoTable", false, KCEditorDefine.B_SORTING_O_CREATE_MENU + 1)]
	public static void CreateProductInfoTable() {
		var oProductInfoTable = CEditorFactory.CreateScriptableObj<CProductInfoTable>();

		oProductInfoTable.SetCommonProductInfos(new List<STProductInfo>() {
			new STProductInfo {
				m_oID = "dante.distribution.sample.iap.c.sample", m_eProductType = ProductType.Consumable
			},

			new STProductInfo {
				m_oID = "dante.distribution.sample.iap.nc.sample", m_eProductType = ProductType.NonConsumable
			}
		});
	}
#endif			// #if PURCHASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
#endif			// #if UNITY_EDITOR
