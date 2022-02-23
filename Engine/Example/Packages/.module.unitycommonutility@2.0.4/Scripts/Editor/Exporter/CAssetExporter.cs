using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

/** 에셋 익스포터 */
public static class CAssetExporter {
	#region 클래스 함수
	/** 텍스처 => PNG 이미지로 추출한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_EXPORT_BASE + "Texture to PNGImage", false, KCEditorDefine.B_SORTING_O_EXPORT_MENU + 1)]
	public static void ExportTextureToPNGImg() {
		var oTextureList = Selection.objects.ExIsValid() ? Selection.objects.ExToTypes<Texture2D>() : null;

		// 선택 된 텍스처가 없을 경우
		if(!oTextureList.ExIsValid()) {
			CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, KCEditorDefine.B_MSG_ALERT_P_EXPORT_TEXTURE_FAIL);
		} else {
			for(int i = 0; i < oTextureList.Count; ++i) {
				string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, oTextureList[i].name);
				CAssetExporter.SaveTexture(oFilePath, oTextureList[i]);
			}

			CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
		}
	}

	/** 기본 텍스처 => PNG 이미지로 추출한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_EXPORT_BASE + "DefTexture to PNGImage", false, KCEditorDefine.B_SORTING_O_EXPORT_MENU + 1)]
	public static void ExportDefTextureToPNGImg() {
		string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, Texture2D.whiteTexture.name);

		CAssetExporter.SaveTexture(oFilePath, Texture2D.whiteTexture);
		CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
	}

	/** 스프라이트 => PNG 이미지로 추출한다 */
	[MenuItem(KCEditorDefine.B_MENU_TOOLS_EXPORT_BASE + "Sprite to PNGImage", false, KCEditorDefine.B_SORTING_O_EXPORT_MENU + 1)]
	public static void ExportSpriteToPNGImg() {
		var oSpriteList = Selection.objects.ExIsValid() ? Selection.objects.ExToTypes<Sprite>() : null;

		// 선택 된 스프라이트가 없을 경우
		if(!oSpriteList.ExIsValid()) {
			CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, KCEditorDefine.B_MSG_ALERT_P_EXPORT_TEXTURE_FAIL);
		} else {
			for(int i = 0; i < oSpriteList.Count; ++i) {
				var oColors = oSpriteList[i].texture.GetPixels((int)oSpriteList[i].textureRect.x, (int)oSpriteList[i].textureRect.y, (int)oSpriteList[i].textureRect.width, (int)oSpriteList[i].textureRect.height, KCDefine.B_VAL_0_INT);
				string oFilePath = string.Format(KCEditorDefine.B_IMG_P_FMT_TEXTURE_TO_IMG, oSpriteList[i].name);

				var oTexture = new Texture2D((int)oSpriteList[i].textureRect.width, (int)oSpriteList[i].textureRect.height, oSpriteList[i].texture.format, KCDefine.B_VAL_1_INT, true);
				oTexture.ExSetPixels(oColors.ToList());

				CFunc.WriteBytes(oFilePath, oTexture.EncodeToPNG());
			}

			CEditorFunc.ShowAlertPopup(KCEditorDefine.B_TEXT_ALERT_P_TITLE, KCEditorDefine.B_MSG_ALERT_P_EXPORT_IMG_SUCCESS);
		}
	}

	/** 텍스처를 저장한다 */
	private static void SaveTexture(string a_oFilePath, Texture2D a_oTexture) {
		var oTexture = new Texture2D(a_oTexture.width, a_oTexture.height, a_oTexture.format, KCDefine.B_VAL_1_INT, true);
		oTexture.ExSetPixels(a_oTexture.GetPixels(KCDefine.B_VAL_0_INT).ToList());
		
		CFunc.WriteBytes(a_oFilePath, oTexture.EncodeToPNG());
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
