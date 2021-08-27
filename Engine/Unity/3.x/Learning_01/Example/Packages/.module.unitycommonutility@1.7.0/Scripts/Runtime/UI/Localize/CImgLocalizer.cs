using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 이미지 지역화
public class CImgLocalizer : CComponent {
	#region 변수
	[SerializeField] private string m_oBasePath = string.Empty;

	private string m_oLanguage = string.Empty;
	private string m_oCountryCode = string.Empty;

	// UI
	private Image m_oImg = null;
	#endregion			// 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		m_oImg = this.GetComponentInChildren<Image>();

		m_oLanguage = CCommonAppInfoStorage.Inst.AppInfo.Language.ToString();
		m_oCountryCode = CCommonAppInfoStorage.Inst.CountryCode;
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetupImg();
	}

	//! 지역화를 리셋한다
	public virtual void ResetLocalize() {
		this.SetupImg();
	}

	//! 이미지를 변경한다
	public void SetImg(string a_oBasePath) {
		this.SetImg(a_oBasePath, m_oLanguage, m_oCountryCode);
	}

	//! 이미지를 변경한다
	public void SetImg(string a_oBasePath, string a_oLanguage, string a_oCountryCode) {
		m_oBasePath = a_oBasePath;
		m_oLanguage = a_oLanguage;
		m_oCountryCode = a_oCountryCode;

		this.SetupImg();
	}

	//! 이미지를 설정한다
	private void SetupImg() {
		string oDefPath = CFactory.MakeLocalizePath(m_oBasePath, SystemLanguage.English.ToString());
		string oImgPath = CFactory.MakeLocalizePath(m_oBasePath, oDefPath, m_oLanguage, m_oCountryCode);

		m_oImg.sprite = CResManager.Inst.GetRes<Sprite>(oImgPath);
	}
	#endregion			// 함수
}
