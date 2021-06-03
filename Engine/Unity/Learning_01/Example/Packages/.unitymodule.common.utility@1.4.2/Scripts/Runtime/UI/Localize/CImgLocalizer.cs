using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 이미지 지역화
public class CImgLocalizer : CUIsComponent {
	#region 변수
	[SerializeField] private string m_oBasePath = string.Empty;
	#endregion			// 변수

	#region UI 변수
	private Image m_oImg = null;
	#endregion			// UI 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		m_oImg = this.GetComponentInChildren<Image>();
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetImg(m_oBasePath);
	}

	//! 지역화를 리셋한다
	public virtual void ResetLocalize() {
		this.SetImg(m_oBasePath);
	}

	//! 이미지를 변경한다
	public void SetImg(string a_oBasePath) {
		string oLanguage = CCommonAppInfoStorage.Inst.AppInfo.Language.ToString();
		string oCountryCode = CCommonAppInfoStorage.Inst.CountryCode;

		this.SetImg(a_oBasePath, oLanguage, oCountryCode);
	}

	//! 이미지를 변경한다
	public void SetImg(string a_oBasePath, string a_oLanguage, string a_oCountryCode) {
		string oDefLocalizePath = CFactory.MakeLocalizePath(a_oBasePath, SystemLanguage.English.ToString());
		string oFilePath = CFactory.MakeLocalizePath(a_oBasePath, oDefLocalizePath, a_oLanguage, a_oCountryCode);

		m_oBasePath = a_oBasePath;
		m_oImg.sprite = CResManager.Inst.GetRes<Sprite>(oFilePath);
	}
	#endregion			// 함수
}
