﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

#if SCRIPT_TEMPLATE_ONLY
#if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
#region 기본
/** 서브 에디터 레벨 생성 정보 */
public class CSubEditorLevelCreateInfo : CEditorLevelCreateInfo {

}

/** 에디터 타입 랩퍼 */
[MessagePackObject]
public struct STEditorTypeWrapper {

}
#endregion			// 기본

#region 추가 타입

#endregion			// 추가 타입
#endif			// #if UNITY_STANDALONE && EDITOR_SCENE_TEMPLATES_MODULE_ENABLE && (DEBUG || DEVELOPMENT_BUILD)
#endif			// #if SCRIPT_TEMPLATE_ONLY
