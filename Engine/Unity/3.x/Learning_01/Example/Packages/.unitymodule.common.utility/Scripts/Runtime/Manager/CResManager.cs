using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

//! 리소스 관리자
public class CResManager : CSingleton<CResManager> {
	#region 변수
	private Dictionary<System.Type, Dictionary<string, Object>> m_oResListContainer = new Dictionary<System.Type, Dictionary<string, Object>>() {
		[typeof(Shader)] = new Dictionary<string, Object>(),

		[typeof(Font)] = new Dictionary<string, Object>(),
		[typeof(Sprite)] = new Dictionary<string, Object>(),
		[typeof(Texture)] = new Dictionary<string, Object>(),
		[typeof(Material)] = new Dictionary<string, Object>(),
		[typeof(AudioClip)] = new Dictionary<string, Object>(),
		[typeof(TextAsset)] = new Dictionary<string, Object>(),
		[typeof(GameObject)] = new Dictionary<string, Object>(),
		[typeof(SpriteAtlas)] = new Dictionary<string, Object>(),
		[typeof(ScriptableObject)] = new Dictionary<string, Object>()
	};

	private Dictionary<System.Type, System.Func<string, Object>> m_oResCreatorList = new Dictionary<System.Type, System.Func<string, Object>>() {
		[typeof(Shader)] = Shader.Find,

		[typeof(Font)] = Resources.Load<Font>,
		[typeof(Sprite)] = Resources.Load<Sprite>,
		[typeof(Texture)] = Resources.Load<Texture>,
		[typeof(Material)] = Resources.Load<Material>,
		[typeof(AudioClip)] = Resources.Load<AudioClip>,
		[typeof(TextAsset)] = Resources.Load<TextAsset>,
		[typeof(GameObject)] = Resources.Load<GameObject>,
		[typeof(SpriteAtlas)] = Resources.Load<SpriteAtlas>,
		[typeof(ScriptableObject)] = Resources.Load<ScriptableObject>
	};
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		this.SetupDefReses();
	}

	//! 스프라이트 아틀라스를 로드한다
	public void LoadSpriteAtlas(string a_oFilePath, bool a_bIsAutoLoadSprites = true) {
		CAccess.Assert(a_oFilePath.ExIsValid());
		var oSpriteAtlas = this.GetRes<SpriteAtlas>(a_oFilePath);

		// 자동 로드 모드 일 경우
		if(a_bIsAutoLoadSprites) {
			CAccess.Assert(oSpriteAtlas.ExIsValid());
			var oSprites = new Sprite[oSpriteAtlas.spriteCount];

			oSpriteAtlas.GetSprites(oSprites);

			for(int i = 0; i < oSprites.Length; ++i) {
				string oTarget = KCDefine.U_IMG_N_SPRITE_CLONE;
				string oKey = oSprites[i].name.ExGetReplaceString(oTarget, string.Empty);

				this.AddRes<Sprite>(oKey, oSprites[i]);
			}
		}
	}

	//! 기본 리소스를 설정한다
	private void SetupDefReses() {
		// 스프라이트를 설정한다 {
		var stRect = Rect.MinMaxRect(KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_0, KCDefine.B_VALUE_FLT_1, KCDefine.B_VALUE_FLT_1);
		var oSprite = Sprite.Create(Texture2D.whiteTexture, stRect, KCDefine.B_ANCHOR_MIDDLE_CENTER, KCDefine.B_VALUE_FLT_1);
		
		oSprite.name = KCDefine.U_IMG_N_DEF_SPRITE;
		this.AddRes<Sprite>(KCDefine.U_IMG_N_DEF_SPRITE, oSprite);
		// 스프라이트를 설정한다 }
	}
	#endregion			// 함수

	#region 제네릭 함수
	//! 리소스를 반환한다
	public T GetRes<T>(string a_oKey, bool a_bIsAutoCreate = true) where T : Object {
		CAccess.Assert(a_oKey.ExIsValid() && m_oResListContainer.ContainsKey(typeof(T)));
		var oResList = m_oResListContainer[typeof(T)];

		// 리소스 생성이 가능 할 경우
		if(a_bIsAutoCreate && !oResList.ContainsKey(a_oKey)) {
			var oRes = m_oResCreatorList[typeof(T)](a_oKey) as T;
			oResList.Add(a_oKey, oRes);
		}

		return oResList[a_oKey] as T;
	}

	//! 스크립트 객체를 반환한다
	public T GetScriptableObj<T>(string a_oKey, bool a_bIsAutoCreate = true) where T : ScriptableObject {
		CAccess.Assert(a_oKey.ExIsValid());
		return this.GetRes<ScriptableObject>(a_oKey, a_bIsAutoCreate) as T;
	}

	//! 리소스를 추가한다
	public void AddRes<T>(string a_oKey, T a_tRes, bool a_bIsReplace = false) where T : Object {
		CAccess.Assert(a_tRes != null && a_oKey.ExIsValid());
		CAccess.Assert(m_oResListContainer.ContainsKey(typeof(T)));

		var oResList = m_oResListContainer[typeof(T)];

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			oResList.ExReplaceValue(a_oKey, a_tRes);
		} else {
			oResList.ExAddValue(a_oKey, a_tRes);
		}
	}

	//! 리소스 생성자를 추가한다
	public void AddResCreator<T>(System.Func<string, Object> a_oCreator, bool a_bIsReplace = false) where T : Object {
		CAccess.Assert(a_oCreator != null);

		// 대체 모드 일 경우
		if(a_bIsReplace) {
			m_oResCreatorList.ExReplaceValue(typeof(T), a_oCreator);
		} else {
			m_oResCreatorList.ExAddValue(typeof(T), a_oCreator);
		}
	}

	//! 리소스를 제거한다
	public void RemoveRes<T>(string a_oKey, bool a_bIsAutoUnload) where T : Object {
		CAccess.Assert(a_oKey.ExIsValid() && m_oResListContainer.ContainsKey(typeof(T)));
		var oResList = m_oResListContainer[typeof(T)];

		// 리소스가 존재 할 경우
		if(oResList.TryGetValue(a_oKey, out Object oRes)) {
			oResList.ExRemoveValue(a_oKey);

			// 자동 제거 모드 일 경우
			if(a_bIsAutoUnload) {
				Resources.UnloadAsset(oRes);
			}
		}
	}

	//! 리소스 생성자를 제거한다
	public void RemoveResCreator<T>() where T : Object {
		m_oResCreatorList.ExRemoveValue(typeof(T));
	}

	//! 리소스를 로드한다
	public T[] LoadReses<T>(string a_oFilePath) where T : Object {
		CAccess.Assert(a_oFilePath.ExIsValid());
		var oReses = Resources.LoadAll<T>(a_oFilePath);

		for(int i = 0; i < oReses.Length; ++i) {
			this.AddRes<T>(oReses[i].name, oReses[i]);
		}

		return oReses;
	}
	#endregion			// 제네릭 함수
}
