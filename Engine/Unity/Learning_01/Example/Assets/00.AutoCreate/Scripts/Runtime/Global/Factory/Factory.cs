using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 기본 팩토리
public static partial class Factory {
	#region 클래스 함수
	
	#endregion			// 클래스 함수

	#region 조건부 클래스 함수
#if FIREBASE_MODULE_ENABLE
	//! 유저 정보 노드를 생성한다
	public static List<string> MakeUserInfoNodes() {
		return CFactory.MakeUserInfoNodes();
	}

	//! 지급 아이템 정보 노드를 생성한다
	public static List<string> MakePostItemInfoNodes() {
		return CFactory.MakePostItemInfoNodes();
	}
#endif			// #if FIREBASE_MODULE_ENABLE
	#endregion			// 조건부 클래스 함수
}
