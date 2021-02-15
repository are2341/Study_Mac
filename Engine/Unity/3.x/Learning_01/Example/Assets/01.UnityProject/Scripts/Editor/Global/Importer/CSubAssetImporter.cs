using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//! 서브 에셋 추가자
[InitializeOnLoad]
public class CSubAssetImporter : CAssetImporter {
	#region 함수
	//! 사운드를 추가했을 경우
	public override void OnPreprocessAudio() {
		base.OnPreprocessAudio();
	}

	//! 텍스처를 추가했을 경우
	public virtual void OnPreprocessTexture() {
		base.OnPreprocessTexture();
	}
	#endregion			// 함수
}
#endif			// #if UNITY_EDITOR
