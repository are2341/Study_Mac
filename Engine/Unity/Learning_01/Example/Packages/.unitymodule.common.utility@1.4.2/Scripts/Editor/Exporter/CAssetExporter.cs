﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 에셋 추출자
public static class CAssetExporter {
	#region 클래스 함수
	//! 텍스처 => PNG 이미지로 추출한다
	[MenuItem("Tools/Utility/Export/Texture to PNGImage")]
	public static void ExportTextureToPNGImg() {
		var oTextureList = Selection.objects.ExIsValid() ? Selection.objects.ExToTypes<Texture2D>() : null;

		// 선택 된 텍스처가 없을 경우
		if(!oTextureList.ExIsValid()) {
			CAssetExporter.ShowExportFailPopup(KCEditorDefine.B_MSG_ALERT_P_EXPORT_TEXTURE_FAIL);
		} else {
			for(int i = 0; i < oTextureList.Count; ++i) {
				string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, oTextureList[i].name);
				CAssetExporter.SaveTexture(oFilePath, oTextureList[i]);
			}

			CAssetExporter.ShowExportSuccessPopup(KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
		}
	}

	//! 기본 텍스처 => PNG 이미지로 추출한다
	[MenuItem("Tools/Utility/Export/DefTexture to PNGImage")]
	public static void ExportDefTextureToPNGImg() {
		string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, Texture2D.whiteTexture.name);

		CAssetExporter.SaveTexture(oFilePath, Texture2D.whiteTexture);
		CAssetExporter.ShowExportSuccessPopup(KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
	}

	//! 스프라이트 => PNG 이미지로 추출한다
	[MenuItem("Tools/Utility/Export/Sprite to PNGImage")]
	public static void ExportSpriteToPNGImg() {
		var oSpriteList = Selection.objects.ExIsValid() ? Selection.objects.ExToTypes<Sprite>() : null;

		// 선택 된 스프라이트가 없을 경우
		if(!oSpriteList.ExIsValid()) {
			CAssetExporter.ShowExportFailPopup(KCEditorDefine.B_MSG_ALERT_P_EXPORT_SPRITE_FAIL);
		} else {
			for(int i = 0; i < oSpriteList.Count; ++i) {
				var oTexture = new Texture2D((int)oSpriteList[i].textureRect.width, (int)oSpriteList[i].textureRect.height, TextureFormat.RGBA32, KCDefine.B_VAL_1_INT, true);
				var oColors = oSpriteList[i].texture.GetPixels((int)oSpriteList[i].textureRect.x, (int)oSpriteList[i].textureRect.y, (int)oSpriteList[i].textureRect.width, (int)oSpriteList[i].textureRect.height, KCDefine.B_VAL_0_INT);

				oTexture.SetPixels(oColors);
				oTexture.Apply();

				string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, oSpriteList[i].name);
				CFunc.WriteBytes(oFilePath, oTexture.EncodeToPNG());
			}

			CAssetExporter.ShowExportSuccessPopup(KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
		}
	}

	//! 텍스처를 저장한다
	private static void SaveTexture(string a_oFilePath, Texture2D a_oTexture) {
		var oTexture = new Texture2D(a_oTexture.width, a_oTexture.height, TextureFormat.RGBA32, KCDefine.B_VAL_1_INT, true);
		oTexture.SetPixels(a_oTexture.GetPixels(KCDefine.B_VAL_0_INT));
		oTexture.Apply();

		var oBytes = oTexture.EncodeToPNG();
		CFunc.WriteBytes(a_oFilePath, oBytes);		
	}

	//! 추출 성공 팝업을 출력한다
	private static void ShowExportSuccessPopup(string a_oMsg) {
		CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, a_oMsg, KCEditorDefine.B_TEXT_ALERT_P_OK_BTN, string.Empty);
	}

	//! 추출 에러 팝업을 출력한다
	private static void ShowExportFailPopup(string a_oMsg) {
		CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, a_oMsg, KCEditorDefine.B_TEXT_ALERT_P_OK_BTN, string.Empty);
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
