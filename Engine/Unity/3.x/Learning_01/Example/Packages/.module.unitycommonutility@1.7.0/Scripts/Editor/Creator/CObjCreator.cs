using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

//! 객체 생성자
public static class CObjCreator {
	#region 클래스 함수
	//! 게임 객체를 생성한다
	[MenuItem("GameObject/Utility/GameObject %#[", false, 1)]
	public static void CreateGameObj() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_GAME_OBJ, string.Empty, true);
	}

	//! 게임 객체를 생성한다
	[MenuItem("GameObject/Utility/ChildGameObject %#]", false, 1)]
	public static void CreateChildGameObj() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_GAME_OBJ, string.Empty);
	}

	//! 텍스트를 생성한다
	[MenuItem("GameObject/Utility/UI/Text/Text", false, 1)]
	public static void CreateText() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT, KCDefine.U_OBJ_P_TEXT);
	}

	//! 텍스트를 생성한다
	[MenuItem("GameObject/Utility/UI/Text/LocalizeText", false, 1)]
	public static void CreateLocalizeText() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT, KCDefine.U_OBJ_P_LOCALIZE_TEXT);
	}

	//! 입력 필드를 생성한다
	[MenuItem("GameObject/Utility/UI/Text/InputField", false, 1)]
	public static void CreateInputField() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_INPUT_FIELD, KCDefine.U_OBJ_P_INPUT_FIELD);
	}

	//! 이미지를 생성한다
	[MenuItem("GameObject/Utility/UI/Image/Image", false, 1)]
	public static void CreateImg() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG, KCDefine.U_OBJ_P_IMG);
	}

	//! 이미지를 생성한다
	[MenuItem("GameObject/Utility/UI/Image/RawImage", false, 1)]
	public static void CreateRawImg() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_RAW_IMG, KCDefine.U_OBJ_P_RAW_IMG);
	}
	
	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Text + Button/TextButton", false, 1)]
	public static void CreateTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT_BTN, KCDefine.U_OBJ_P_TEXT_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Text + Button/TextScaleButton", false, 1)]
	public static void CreateTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_TEXT_SCALE_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/LocalizeText + Button/LocalizeTextButton", false, 1)]
	public static void CreateLocalizeTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT_BTN, KCDefine.U_OBJ_P_LOCALIZE_TEXT_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/LocalizeText + Button/LocalizeTextScaleButton", false, 1)]
	public static void CreateLocalizeTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + Button/ImageButton", false, 1)]
	public static void CreateImgBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_BTN, KCDefine.U_OBJ_P_IMG_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + Button/ImageScaleButton", false, 1)]
	public static void CreateImgScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_SCALE_BTN, KCDefine.U_OBJ_P_IMG_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + Text + Button/ImageTextButton", false, 1)]
	public static void CreateImgTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_TEXT_BTN, KCDefine.U_OBJ_P_IMG_TEXT_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + Text + Button/ImageTextScaleButton", false, 1)]
	public static void CreateImgTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_IMG_TEXT_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + LocalizeText + Button/ImageLocalizeTextButton", false, 1)]
	public static void CreateImgLocalizeTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_LOCALIZE_TEXT_BTN, KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Utility/UI/Button/Image + LocalizeText + Button/ImageLocalizeTextScaleButton", false, 1)]
	public static void CreateImgLocalizeTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_LOCALIZE_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN);
	}

	//! 페이지 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Utility/UI/ScrollView/PageView", false, 1)]
	public static void CreatePageView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_PAGE_VIEW, KCDefine.U_OBJ_P_PAGE_VIEW);
	}

	//! 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Utility/UI/ScrollView/ScrollView", false, 1)]
	public static void CreateScrollView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_SCROLL_VIEW, KCDefine.U_OBJ_P_SCROLL_VIEW);
	}

	//! 재사용 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Utility/UI/ScrollView/RecycleView", false, 1)]
	public static void CreateRecycleView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_RECYCLE_VIEW, KCDefine.U_OBJ_P_RECYCLE_VIEW);
	}

	//! 터치 응답자를 생성한다
	[MenuItem("GameObject/Utility/UI/Responder/TouchResponder", false, 1)]
	public static void CreateTouchResponder() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER);
	}

	//! 드래그 응답자를 생성한다
	[MenuItem("GameObject/Utility/UI/Responder/DragResponder", false, 1)]
	public static void CreateDragResponder() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_DRAG_RESPONDER, KCDefine.U_OBJ_P_G_DRAG_RESPONDER);
	}

	//! 파티클 효과를 생성한다
	[MenuItem("GameObject/Utility/FX/ParticleFX", false, 1)]
	public static void CreateParticleFX() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_PARTICLE_FX, KCDefine.U_OBJ_P_PARTICLE_FX);
	}

	//! 스프라이트를 생성한다
	[MenuItem("GameObject/Utility/2D/Sprite", false, 1)]
	public static void CreateSprite() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_SPRITE, KCDefine.U_OBJ_P_SPRITE);
	}

	//! 객체를 생성한다
	private static GameObject CreateObj(string a_oName, string a_oFilePath, bool a_bIsIgnoreParent = false) {
		var oParent = a_bIsIgnoreParent ? null : CEditorAccess.GetActiveObj();
		var oPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

		GameObject oObj = null;

		// 부모가 없을 경우
		if(oParent == null && oPrefabStage != null) {
			oParent = oPrefabStage.prefabContentsRoot;
		}

		// 파일 경로가 유효 할 경우
		if(a_oFilePath.ExIsValid()) {
			var oOrigin = Resources.Load<GameObject>(a_oFilePath);
			oObj = CFactory.CreateCloneObj(a_oName, oOrigin, oParent);
		} else {
			oObj = CFactory.CreateObj(a_oName, oParent);
		}

		// 에디터 모드 일 경우
		if(!Application.isPlaying) {
			CFunc.SelObj(oObj);
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}

		// 버튼이 존재 할 경우
		if(oObj.TryGetComponent<Button>(out Button oBtn)) {
			var stColorBlock = oBtn.colors;
			stColorBlock.normalColor = KCDefine.U_COLOR_NORM;
			stColorBlock.pressedColor = KCDefine.U_COLOR_PRESS;
			stColorBlock.selectedColor = KCDefine.U_COLOR_SEL;
			stColorBlock.highlightedColor = KCDefine.U_COLOR_HIGHLIGHT;
			stColorBlock.disabledColor = KCDefine.U_COLOR_DISABLE;
			
			oBtn.colors = stColorBlock;
		}

		// 부모 객체가 존재 할 경우
		if(oParent != null) {
			oObj.layer = oParent.layer;

			// UI 부모 객체 일 경우
			if(oParent.TryGetComponent<RectTransform>(out RectTransform oTrans)) {
				var oObjTrans = oObj.ExAddComponent<RectTransform>();
				oObjTrans.sizeDelta = Vector2.zero;
			}
		}

		// 캔버스 렌더러가 존재 할 경우
		if(oObj.TryGetComponent<CanvasRenderer>(out CanvasRenderer oRenderer)) {
			oRenderer.cullTransparentMesh = false;	
		}

		Undo.RegisterCreatedObjectUndo(oObj, a_oName);
		return oObj;
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
