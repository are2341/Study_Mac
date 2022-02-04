using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.TextCore.LowLevel;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.U2D;

/** 에셋 임포터 */
[InitializeOnLoad]
public class CAssetImporter : AssetPostprocessor {
	#region 변수
	private static List<string> m_oAudioImporterPlatformNameList = new List<string>() {
		KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_IOS, KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_ANDROID, KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_STANDALONE
	};

	private static List<string> m_oTextureImporterPlatformNameList = new List<string>() {
		KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_IOS, KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_ANDROID, KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_STANDALONE
	};
	#endregion			// 변수

	#region 함수
	/** 사운드를 추가 할 경우 */
	public virtual void OnPreprocessAudio() {
		var oAudioImporter = this.assetImporter as AudioImporter;
		oAudioImporter.ambisonic = true;
		oAudioImporter.forceToMono = false;
		oAudioImporter.preloadAudioData = true;
		oAudioImporter.loadInBackground = true;

		// 오디오 임포터 샘플을 설정한다 {
		var stDefSampleSettings = oAudioImporter.defaultSampleSettings;
		CAssetImporter.SetupAudioImporterSampleSettings(oAudioImporter, ref stDefSampleSettings, true);

		oAudioImporter.defaultSampleSettings = stDefSampleSettings;

		for(int i = 0; i < CAssetImporter.m_oAudioImporterPlatformNameList.Count; ++i) {
#if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE
			var stSampleSettings = oAudioImporter.GetOverrideSampleSettings(CAssetImporter.m_oAudioImporterPlatformNameList[i]);
			CAssetImporter.SetupAudioImporterSampleSettings(oAudioImporter, ref stSampleSettings, false);

			oAudioImporter.SetOverrideSampleSettings(CAssetImporter.m_oAudioImporterPlatformNameList[i], stSampleSettings);
#else
			oAudioImporter.ClearSampleSettingOverride(CAssetImporter.m_oAudioImporterPlatformNameList[i]);
#endif			// #if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE
		}
		// 오디오 임포터 샘플을 설정한다 }
	}

	/** 모델을 추가 할 경우 */
	public virtual void OnPreprocessModel() {
		var oModelImporter = this.assetImporter as ModelImporter;
		oModelImporter.generateSecondaryUV = true;
	}

	/** 텍스처를 추가 할 경우 */
	public virtual void OnPreprocessTexture() {
		var oTextureImporter = this.assetImporter as TextureImporter;
		oTextureImporter.mipmapEnabled = true;
		oTextureImporter.alphaIsTransparency = true;

		oTextureImporter.sRGBTexture = !oTextureImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_FIX_GAMMA_PIPELINE);
		oTextureImporter.ignorePngGamma = oTextureImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_FIX_GAMMA_PIPELINE);

		oTextureImporter.npotScale = KCEditorDefine.B_IGNORE_NON_POT_SCALE_TEXTURE_TYPE_LIST.Contains(oTextureImporter.textureType) ? TextureImporterNPOTScale.None : TextureImporterNPOTScale.ToNearest;
		oTextureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
		oTextureImporter.mipmapFilter = TextureImporterMipFilter.BoxFilter;

		oTextureImporter.spritePackingTag = string.Empty;
		oTextureImporter.spritePixelsPerUnit = KCDefine.B_UNIT_PIXELS_PER_UNIT;

		oTextureImporter.wrapMode = oTextureImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_FIX_CLAMP_WRAP) ? TextureWrapMode.Clamp : TextureWrapMode.Repeat;
		oTextureImporter.filterMode = oTextureImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER) ? FilterMode.Point : FilterMode.Bilinear;

		// 텍스처를 설정한다 {
		var oTextureSettings = new TextureImporterSettings();
		oTextureImporter.ReadTextureSettings(oTextureSettings);

		oTextureSettings.readable = oTextureImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_ENABLE_READ_WRITE);
		oTextureSettings.spriteGenerateFallbackPhysicsShape = true;
		oTextureSettings.spriteMeshType = SpriteMeshType.FullRect;

		oTextureImporter.SetTextureSettings(oTextureSettings);
		// 텍스처를 설정한다 }

		// 텍스처 임포터 플랫폼을 설정한다 {
		var oDefPlatformSettings = oTextureImporter.GetDefaultPlatformTextureSettings();
		CAssetImporter.SetupTextureImporterPlatformSettings(oTextureImporter, oDefPlatformSettings, true, false);

		oTextureImporter.SetPlatformTextureSettings(oDefPlatformSettings);

		for(int i = 0; i < CAssetImporter.m_oTextureImporterPlatformNameList.Count; ++i) {
			var oPlatformSettings = oTextureImporter.GetPlatformTextureSettings(CAssetImporter.m_oTextureImporterPlatformNameList[i]);
			CAssetImporter.SetupTextureImporterPlatformSettings(oTextureImporter, oPlatformSettings, false, false);

			oTextureImporter.SetPlatformTextureSettings(oPlatformSettings);
		}
		// 텍스처 임포터 플랫폼을 설정한다 }
	}
	#endregion			// 함수

	#region 클래스 함수
	/** 에셋을 추가했을 경우 */
	private static void OnPostprocessAllAssets(string[] a_oImportAssets, string[] a_oRemoveAssets, string[] a_oMoveAssets, string[] a_oMoveAssetPaths) {
		for(int i = 0; i < a_oImportAssets.Length; ++i) {
			var oSpriteAtlas = CEditorFunc.FindAsset<SpriteAtlas>(a_oImportAssets[i]);
			var oTMPFontAsset = CEditorFunc.FindAsset<TMP_FontAsset>(a_oImportAssets[i]);

			// 스프라이트 아틀라스 일 경우
			if(oSpriteAtlas != null && a_oImportAssets[i].Contains(KCDefine.B_FILE_EXTENSION_SPRITE_ATLAS)) {
				CAssetImporter.SetupSpriteAtlas(oSpriteAtlas, a_oImportAssets[i], a_oRemoveAssets, a_oMoveAssets, a_oMoveAssetPaths);
			}
			// TMP 폰트 에셋 일 경우
			else if(oTMPFontAsset != null && a_oImportAssets[i].Contains(KCDefine.B_FILE_EXTENSION_TMP_FONT_ASSET)) {
				CAssetImporter.SetupTMPFontAsset(oTMPFontAsset, a_oImportAssets[i], a_oRemoveAssets, a_oMoveAssets, a_oMoveAssetPaths);
			}
		}
	}

	/** 스프라이트 아틀라스를 설정한다 */
	private static void SetupSpriteAtlas(SpriteAtlas a_oSpriteAtlas, string a_oImportAsset, string[] a_oRemoveAssets, string[] a_oMoveAssets, string[] a_oMoveAssetPaths) {
		var oPackingSettings = a_oSpriteAtlas.GetPackingSettings();
		oPackingSettings.enableRotation = false;
		oPackingSettings.enableTightPacking = false;
		oPackingSettings.padding = KCDefine.B_VAL_4_INT;
		
		var oTextureSettings = a_oSpriteAtlas.GetTextureSettings();
		oTextureSettings.sRGB = !a_oImportAsset.Contains(KCDefine.B_NAME_PATTERN_FIX_GAMMA_PIPELINE);
		oTextureSettings.readable = a_oImportAsset.Contains(KCDefine.B_NAME_PATTERN_ENABLE_READ_WRITE);
		oTextureSettings.filterMode = a_oImportAsset.Contains(KCDefine.B_NAME_PATTERN_FIX_POINT_FILTER) ? FilterMode.Point : FilterMode.Bilinear;
		oTextureSettings.generateMipMaps = true;

		a_oSpriteAtlas.SetIncludeInBuild(true);
		a_oSpriteAtlas.SetPackingSettings(oPackingSettings);
		a_oSpriteAtlas.SetTextureSettings(oTextureSettings);

		// 텍스처 임포터 플랫폼을 설정한다 {
		var oDefPlatformSettings = a_oSpriteAtlas.GetPlatformSettings(KCEditorDefine.B_ASSET_IMPORTER_PLATFORM_N_DEF);
		CAssetImporter.SetupTextureImporterPlatformSettings(null, oDefPlatformSettings, true, false);

		a_oSpriteAtlas.SetPlatformSettings(oDefPlatformSettings);

		for(int i = 0; i < CAssetImporter.m_oTextureImporterPlatformNameList.Count; ++i) {
			var oPlatformSettings = a_oSpriteAtlas.GetPlatformSettings(CAssetImporter.m_oTextureImporterPlatformNameList[i]);
			CAssetImporter.SetupTextureImporterPlatformSettings(null, oPlatformSettings, false, false);

			a_oSpriteAtlas.SetPlatformSettings(oPlatformSettings);
		}
		// 텍스처 임포터 플랫폼을 설정한다 }
	}

	/** TMP 폰트 에셋을 설정한다 */
	private static void SetupTMPFontAsset(TMP_FontAsset a_oTMPFontAsset, string a_oImportAsset, string[] a_oRemoveAssets, string[] a_oMoveAssets, string[] a_oMoveAssetPaths) {
		a_oTMPFontAsset.isMultiAtlasTexturesEnabled = true;
		a_oTMPFontAsset.atlasPopulationMode = AtlasPopulationMode.Dynamic;

		a_oTMPFontAsset.ExSetRuntimePropertyVal<TMP_FontAsset>(KCEditorDefine.B_PROPERTY_N_CLEAR_DYNAMIC_DATA_ON_BUILD, true);
		a_oTMPFontAsset.ExSetRuntimePropertyVal<TMP_FontAsset>(KCEditorDefine.B_PROPERTY_N_ATLAS_RENDER_MODE, GlyphRenderMode.SDFAA);

		a_oTMPFontAsset.ExSetRuntimePropertyVal<TMP_FontAsset>(KCEditorDefine.B_PROPERTY_N_ATLAS_WIDTH, (int)EPOTVal.POT_2048);
		a_oTMPFontAsset.ExSetRuntimePropertyVal<TMP_FontAsset>(KCEditorDefine.B_PROPERTY_N_ATLAS_HEIGHT, (int)EPOTVal.POT_2048);
	}

	/** 오디오 임포터 샘플을 설정한다 */
	private static void SetupAudioImporterSampleSettings(AudioImporter a_oImporter, ref AudioImporterSampleSettings a_stSampleSettings, bool a_bIsDefPlatformSettings) {
		a_stSampleSettings.loadType = (a_oImporter != null && a_oImporter.assetPath.Contains(KCDefine.B_NAME_PATTERN_FIX_COMPRESS_IN_MEMORY)) ? AudioClipLoadType.CompressedInMemory : AudioClipLoadType.DecompressOnLoad;
		a_stSampleSettings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

#if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE
		bool bIsEnableSetupAudioFmt = a_bIsDefPlatformSettings;
#else
		bool bIsEnableSetupAudioFmt = true;
#endif			// #if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE

		// 오디오 포맷 설정이 가능 할 경우
		if(bIsEnableSetupAudioFmt) {
			a_stSampleSettings.quality = KCDefine.B_VAL_1_FLT * KCDefine.B_UNIT_NORM_VAL_TO_PERCENT;
			a_stSampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;
		}
	}

	/** 텍스처 임포터 플랫폼을 설정한다 */
	private static void SetupTextureImporterPlatformSettings(TextureImporter a_oImporter, TextureImporterPlatformSettings a_oPlatformSettings, bool a_bIsDefPlatformSettings, bool a_bIsEnableAssert = true) {
		CAccess.Assert(!a_bIsEnableAssert || a_oPlatformSettings != null);

		// 플랫폼 설정이 존재 할 경우
		if(a_oPlatformSettings != null) {
			a_oPlatformSettings.maxTextureSize = (int)EPOTVal.POT_2048;
			a_oPlatformSettings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
			a_oPlatformSettings.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;

#if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE
			bool bIsEnableSetupTextureFmt = a_bIsDefPlatformSettings;
			a_oPlatformSettings.overridden = true;
#else
			bool bIsEnableSetupTextureFmt = true;
			a_oPlatformSettings.overridden = false;
#endif			// #if ASSET_IMPORTER_PLATFORM_SETTINGS_OVERRIDE_ENABLE

			// 텍스처 포맷 설정이 가능 할 경우
			if(bIsEnableSetupTextureFmt) {
				a_oPlatformSettings.crunchedCompression = true;
				a_oPlatformSettings.allowsAlphaSplitting = false;

				a_oPlatformSettings.format = TextureImporterFormat.RGBA32;
				a_oPlatformSettings.compressionQuality = KCDefine.B_UNIT_NORM_VAL_TO_PERCENT;
				a_oPlatformSettings.textureCompression = TextureImporterCompression.Uncompressed;
			}
		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
