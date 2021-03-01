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
	[MenuItem("GameObject/Create Others/GameObject %#[")]
	public static void CreateGameObj() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_GAME_OBJ, string.Empty, true);
	}

	//! 게임 객체를 생성한다
	[MenuItem("GameObject/Create Others/ChildGameObject %#]")]
	public static void CreateChildGameObj() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_GAME_OBJ, string.Empty);
	}

	//! 텍스트를 생성한다
	[MenuItem("GameObject/Create Others/UI/Text/Text")]
	public static void CreateText() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT, KCDefine.U_OBJ_P_TEXT);
	}

	//! 텍스트를 생성한다
	[MenuItem("GameObject/Create Others/UI/Text/LocalizeText")]
	public static void CreateLocalizeText() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT, KCDefine.U_OBJ_P_LOCALIZE_TEXT);
	}

	//! 이미지를 생성한다
	[MenuItem("GameObject/Create Others/UI/Image/Image")]
	public static void CreateImg() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG, KCDefine.U_OBJ_P_IMG);
	}

	//! 이미지를 생성한다
	[MenuItem("GameObject/Create Others/UI/Image/RawImage")]
	public static void CreateRawImg() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_RAW_IMG, KCDefine.U_OBJ_P_RAW_IMG);
	}
	
	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Text + Button/TextButton")]
	public static void CreateTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT_BTN, KCDefine.U_OBJ_P_TEXT_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Text + Button/TextScaleButton")]
	public static void CreateTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_TEXT_SCALE_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/LocalizeText + Button/LocalizeTextButton")]
	public static void CreateLocalizeTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT_BTN, KCDefine.U_OBJ_P_LOCALIZE_TEXT_BTN);
	}

	//! 텍스트 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/LocalizeText + Button/LocalizeTextScaleButton")]
	public static void CreateLocalizeTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_LOCALIZE_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_LOCALIZE_TEXT_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + Button/ImageButton")]
	public static void CreateImgBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_BTN, KCDefine.U_OBJ_P_IMG_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + Button/ImageScaleButton")]
	public static void CreateImgScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_SCALE_BTN, KCDefine.U_OBJ_P_IMG_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + Text + Button/ImageTextButton")]
	public static void CreateImgTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_TEXT_BTN, KCDefine.U_OBJ_P_IMG_TEXT_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + Text + Button/ImageTextScaleButton")]
	public static void CreateImgTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_IMG_TEXT_SCALE_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + LocalizeText + Button/ImageLocalizeTextButton")]
	public static void CreateImgLocalizeTextBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_LOCALIZE_TEXT_BTN, KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_BTN);
	}

	//! 이미지 버튼을 생성한다
	[MenuItem("GameObject/Create Others/UI/Button/Image + LocalizeText + Button/ImageLocalizeTextScaleButton")]
	public static void CreateImgLocalizeTextScaleBtn() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_IMG_LOCALIZE_TEXT_SCALE_BTN, KCDefine.U_OBJ_P_IMG_LOCALIZE_TEXT_SCALE_BTN);
	}

	//! 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Create Others/UI/ScrollView/ScrollView")]
	public static void CreateScrollView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_SCROLL_VIEW, KCDefine.U_OBJ_P_SCROLL_VIEW);
	}

	//! 재사용 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Create Others/UI/ScrollView/RecycleScrollView")]
	public static void CreateRecycleScrollView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_RECYCLE_SCROLL_VIEW, KCDefine.U_OBJ_P_RECYCLE_SCROLL_VIEW);
	}

	//! 페이지 스크롤 뷰를 생성한다
	[MenuItem("GameObject/Create Others/UI/ScrollView/PageScrollView")]
	public static void CreatePageScrollView() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_PAGE_SCROLL_VIEW, KCDefine.U_OBJ_P_PAGE_SCROLL_VIEW);
	}

	//! 터치 응답자를 생성한다
	[MenuItem("GameObject/Create Others/UI/Responder/TouchResponder")]
	public static void CreateTouchResponder() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_TOUCH_RESPONDER, KCDefine.U_OBJ_P_G_TOUCH_RESPONDER);
	}

	//! 드래그 응답자를 생성한다
	[MenuItem("GameObject/Create Others/UI/Responder/DragResponder")]
	public static void CreateDragResponder() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_DRAG_RESPONDER, KCDefine.U_OBJ_P_G_DRAG_RESPONDER);
	}

	//! 파티클 효과를 생성한다
	[MenuItem("GameObject/Create Others/FX/ParticleFX")]
	public static void CreateParticleFX() {
		CObjCreator.CreateObj(KCEditorDefine.B_OBJ_N_PARTICLE_FX, KCDefine.U_OBJ_P_PARTICLE_FX);
	}

	//! 스프라이트를 생성한다
	[MenuItem("GameObject/Create Others/2D/Sprite")]
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

		// 부모 객체가 존재 할 경우
		if(oParent != null) {
			oObj.layer = oParent.layer;
		}

		// 에디터 모드 일 경우
		if(!Application.isPlaying) {
			CFunc.SelectObj(oObj);
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}

		// 버튼이 존재 할 경우
		if(oObj.TryGetComponent<Button>(out Button oBtn)) {
			var stColorBlock = oBtn.colors;
			stColorBlock.normalColor = KCDefine.U_DEF_COLOR_NORM;
			stColorBlock.pressedColor = KCDefine.U_DEF_COLOR_PRESS;
			stColorBlock.selectedColor = KCDefine.U_DEF_COLOR_SELECT;
			stColorBlock.highlightedColor = KCDefine.U_DEF_COLOR_HIGHLIGHT;
			stColorBlock.disabledColor = KCDefine.U_DEF_COLOR_DISABLE;
			
			oBtn.colors = stColorBlock;
		}

		// 부모 객체가 존재 할 경우
		if(oParent?.GetComponent<RectTransform>() != null) {
			var oTrans = oObj.ExAddComponent<RectTransform>();
			oTrans.sizeDelta = Vector2.zero;

#if !MODE_CENTER_ENABLE
			bool bIsUIs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_UIS);
			bool bIsLeftUIs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_LEFT_UIS);
			bool bIsRightUIs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_RIGHT_UIS);
			bool bIsTopUIs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_TOP_UIS);
			bool bIsBottomUIs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_BOTTOM_UIS);
			bool bIsCanvasObjs = oParent.name.ExIsEquals(KCDefine.U_OBJ_N_SCENE_CANVAS_OBJS);

			// UI 객체 일 경우
			if(bIsUIs || bIsLeftUIs || bIsRightUIs || bIsTopUIs || bIsBottomUIs || bIsCanvasObjs) {
				Vector2 stAnchor = KCDefine.B_ANCHOR_BOTTOM_LEFT;

				// 우측 UI 루트 일 경우
				if(bIsRightUIs) {
					stAnchor = KCDefine.B_ANCHOR_BOTTOM_RIGHT;
				}
				// 상단 UI 루트 일 경우
				else if(bIsTopUIs) {
					stAnchor = KCDefine.B_ANCHOR_TOP_LEFT;
				}

				oTrans.anchorMin = stAnchor;
				oTrans.anchorMax = stAnchor;
			}
#endif			// #if !MODE_CENTER_ENABLE
		}

		Undo.RegisterCreatedObjectUndo(oObj, a_oName);
		return oObj;
	}
	#endregion			// 클래스 함수
}
#endif			// #if UNITY_EDITOR
