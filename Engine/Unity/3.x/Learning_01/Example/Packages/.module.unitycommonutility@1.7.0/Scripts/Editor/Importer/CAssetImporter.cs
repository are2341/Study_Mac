using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

//! 에셋 추가자
[InitializeOnLoad]
public class CAssetImporter : AssetPostprocessor {
	#region 함수
	//! 사운드를 추가했을 경우
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

	//! 텍스처를 추가했을 경우
	public virtual void OnPreprocessTexture() {
		var oTextureImporter = this.assetImporter as TextureImporter;

#if !MODE_2D_ENABLE
		oTextureImporter.mipmapEnabled = true;
#endif			// #if !MODE_2D_ENABLE

#if ALPHA_IS_TRANSPARENCY_ENABLE
		oTextureImporter.alphaIsTransparency = true;
#else
		oTextureImporter.alphaIsTransparency = false;
#endif			// #if ALPHA_IS_TRANSPARENCY_ENABLE

		// 포인트 필터가 필요 할 경우
		if(oTextureImporter.assetPath.ExIsContains(KCDefine.B_ASSET_N_PATTERN_FIX_POINT_FILTER)) {
			oTextureImporter.filterMode = FilterMode.Point;
		} else {
			oTextureImporter.filterMode = FilterMode.Bilinear;
		}

		// 기본 타입 일 경우
		if(oTextureImporter.textureType == TextureImporterType.Default || oTextureImporter.textureType == TextureImporterType.Sprite) {
			// 기본 텍스처 옵션을 설정한다 {
			var stDefSettings = oTextureImporter.GetDefaultPlatformTextureSettings();
			stDefSettings.format = oTextureImporter.assetPath.ExIsContains(KCDefine.B_ASSET_N_PATTERN_FIX_POINT_FILTER) ? TextureImporterFormat.RGBA32 : TextureImporterFormat.Automatic;
			stDefSettings.overridden = false;

			oTextureImporter.SetPlatformTextureSettings(stDefSettings);
			// 기본 텍스처 옵션을 설정한다 }

			// iOS 텍스처 옵션을 설정한다 {
			var stiOSSettings = new TextureImporterPlatformSettings();
			stDefSettings.CopyTo(stiOSSettings);

			stiOSSettings.name = KCDefine.B_PLATFORM_N_IOS;
			stiOSSettings.format = TextureImporterFormat.RGBA32;
			stiOSSettings.overridden = true;

			oTextureImporter.SetPlatformTextureSettings(stiOSSettings);
			// iOS 텍스처 옵션을 설정한다 }

			// 스프라이트 타입 일 경우
			if(oTextureImporter.textureType == TextureImporterType.Sprite) {
				oTextureImporter.wrapMode = TextureWrapMode.Repeat;
				oTextureImporter.sRGBTexture = true;
				oTextureImporter.mipmapEnabled = false;
				oTextureImporter.spritePackingTag = KCDefine.B_UNKNOWN_STR;
				oTextureImporter.spritePivot = KCDefine.B_ANCHOR_MID_CENTER;
				oTextureImporter.spritePixelsPerUnit = KCDefine.B_UNIT_REF_PIXELS;

				var oTextureSettings = new TextureImporterSettings();
				oTextureImporter.ReadTextureSettings(oTextureSettings);
				
				oTextureSettings.spriteMeshType = SpriteMeshType.FullRect;
				oTextureSettings.spriteGenerateFallbackPhysicsShape = true;
				oTextureImporter.SetTextureSettings(oTextureSettings);
			}
		}
	}
	#endregion			// 함수

	#region 클래스 함수
	//! DOTween Pro 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/DOTweenPro Pkgs")]
	public static void ImportDOTweenProPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_DOTWEEN_PRO, true);
	}

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

	//! Spine URP 쉐이더 패키지를 추가한다
	[MenuItem("Tools/Utility/Import/SpineURPShaders Pkgs")]
	public static void ImportSpineURPShadersPkgs() {
		AssetDatabase.ImportPackage(KCEditorDefine.B_ABS_PKGS_P_SPINE_URP_SHADERS, true);
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
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
