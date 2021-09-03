using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.U2D;

//! 에셋 추가자
[InitializeOnLoad]
public class CAssetImporter : AssetPostprocessor {
	#region 함수
	//! 사운드를 추가 할 경우
	public virtual void OnPreprocessAudio() {
		var oAudioImporter = this.assetImporter as AudioImporter;
		oAudioImporter.ambisonic = false;
		oAudioImporter.forceToMono = true;
		oAudioImporter.preloadAudioData = true;
		oAudioImporter.loadInBackground = true;
		
		// 샘플링 옵션을 설정한다 {
		var stSettings = oAudioImporter.defaultSampleSettings;
		stSettings.loadType = AudioClipLoadType.DecompressOnLoad;
		stSettings.compressionFormat = AudioCompressionFormat.Vorbis;
		stSettings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

		oAudioImporter.defaultSampleSettings = stSettings;
		// 샘플링 옵션을 설정한다 }
	}

	//! 텍스처를 추가 할 경우
	public virtual void OnPreprocessTexture() {
		var oTextureImporter = this.assetImporter as TextureImporter;
		oTextureImporter.alphaIsTransparency = true;
		oTextureImporter.alphaSource = TextureImporterAlphaSource.FromInput;

		oTextureImporter.spritePackingTag = string.Empty;
		oTextureImporter.spritePixelsPerUnit = KCDefine.B_UNIT_REF_PIXELS;

		oTextureImporter.wrapMode = oTextureImporter.assetPath.Contains(KCDefine.B_ASSET_N_PATTERN_FIX_CLAMP_WRAP) ? TextureWrapMode.Clamp : TextureWrapMode.Repeat;
		oTextureImporter.filterMode = oTextureImporter.assetPath.Contains(KCDefine.B_ASSET_N_PATTERN_FIX_POINT_FILTER) ? FilterMode.Point : FilterMode.Bilinear;
		
		oTextureImporter.sRGBTexture = !oTextureImporter.assetPath.Contains(KCDefine.B_ASSET_N_PATTERN_FIX_LINEAR_CORRECTION);
		oTextureImporter.ignorePngGamma = oTextureImporter.assetPath.Contains(KCDefine.B_ASSET_N_PATTERN_FIX_LINEAR_CORRECTION);
		oTextureImporter.mipmapEnabled = oTextureImporter.assetPath.Contains(KCDefine.B_ASSET_N_PATTERN_FIX_MIP_MAP);

		// 텍스처를 설정한다 {
		var oTextureSettings = new TextureImporterSettings();
		oTextureImporter.ReadTextureSettings(oTextureSettings);

		oTextureSettings.spriteMeshType = SpriteMeshType.FullRect;
		oTextureSettings.spriteGenerateFallbackPhysicsShape = true;

		var oDefSettings = oTextureImporter.GetDefaultPlatformTextureSettings();
		oDefSettings.overridden = false;
		oDefSettings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
		oDefSettings.format = (oTextureImporter.textureType != TextureImporterType.Cookie && oTextureImporter.textureType != TextureImporterType.SingleChannel) ? TextureImporterFormat.RGBA32 : TextureImporterFormat.Automatic;

		oTextureImporter.SetTextureSettings(oTextureSettings);
		oTextureImporter.SetPlatformTextureSettings(oDefSettings);
		// 텍스처를 설정한다 }
	}
	#endregion			// 함수

	#region 클래스 함수
	//! 2D Toolkit 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/2DToolkit Pkgs")]
	public static void Import2DToolkitPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_2D_TOOLKIT, true);
	}

	//! Bitmap Font Importer 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/BitmapFontImporter Pkgs")]
	public static void ImportBitmapFontImporterPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_BITMAP_FONT_IMPORTER, true);
	}

	//! Build Report Tool 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/BuildReportTool Pkgs")]
	public static void ImportBuildReportToolPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_BUILD_REPORT_TOOL, true);
	}

	//! NGUI 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/NGUI Pkgs")]
	public static void ImportNGUIPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_NGUI, true);
	}

	//! Odin Inspector 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/OdinInspector Pkgs")]
	public static void ImportOdinInspectorPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_ODIN_INSPECTOR, true);
	}

	//! Zenject 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/Zenject Pkgs")]
	public static void ImportZenjectPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_ZENJECT, true);
	}

	//! Spine 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/Spine Pkgs")]
	public static void ImportSpinePkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_SPINE, true);
	}
	
	//! Simple Scroll Snap 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/RestClient Pkgs")]
	public static void ImportRestClientPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_REST_CLIENT, true);
	}
	
	//! Sprite Trail 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/SpriteTrail Pkgs")]
	public static void ImportSpriteTrailPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_SPRITE_TRAIL, true);
	}

	//! Ultimate Status Bar 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/UltimateStatusBar Pkgs")]
	public static void ImportUltimateStatusBarPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_ULTIMATE_STATUS_BAR, true);
	}

	//! Lean GUI 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/LeanGUI Pkgs")]
	public static void ImportLeanGUIPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_LEAN_GUI, true);
	}

	//! Lean Touch 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/LeanTouch Pkgs")]
	public static void ImportLeanTouchPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_LEAN_TOUCH, true);
	}

	//! 에셋을 추가했을 경우
	private static void OnPostprocessAllAssets(string[] a_oImportAssets, string[] a_oRemoveAssets, string[] a_oMoveAssets, string[] a_oMoveAssetPaths) {
		for(int i = 0; i < a_oImportAssets.Length; ++i) {
			var oSpriteAtlas = CEditorFunc.FindAsset<SpriteAtlas>(a_oImportAssets[i]);

			// 스프라이트 아틀라스가 존재 할 경우
			if(oSpriteAtlas != null && a_oImportAssets[i].Contains(KCDefine.B_FILE_EXTENSION_SPRITE_ATLAS)) {
				var oPackingSettings = oSpriteAtlas.GetPackingSettings();
				oPackingSettings.enableRotation = false;
				oPackingSettings.enableTightPacking = false;
				oPackingSettings.padding = KCDefine.B_VAL_4_INT;

				var oTextureSettings = oSpriteAtlas.GetTextureSettings();
				oTextureSettings.sRGB = !a_oImportAssets[i].Contains(KCDefine.B_ASSET_N_PATTERN_FIX_LINEAR_CORRECTION);
				oTextureSettings.filterMode = a_oImportAssets[i].Contains(KCDefine.B_ASSET_N_PATTERN_FIX_POINT_FILTER) ? FilterMode.Point : FilterMode.Bilinear;
				oTextureSettings.generateMipMaps = a_oImportAssets[i].Contains(KCDefine.B_ASSET_N_PATTERN_FIX_MIP_MAP);

				var oPlatformSettings = oSpriteAtlas.GetPlatformSettings(KCDefine.B_PLATFORM_N_DEF_TEXTURE);
				oPlatformSettings.overridden = false;
				oPlatformSettings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
				oPlatformSettings.format = TextureImporterFormat.RGBA32;
				
				oSpriteAtlas.SetPackingSettings(oPackingSettings);
				oSpriteAtlas.SetTextureSettings(oTextureSettings);
				oSpriteAtlas.SetPlatformSettings(oPlatformSettings);
			}

		}
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
