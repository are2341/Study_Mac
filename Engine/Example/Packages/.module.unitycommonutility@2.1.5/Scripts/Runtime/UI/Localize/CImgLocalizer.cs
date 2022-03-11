using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 이미지 지역화 */
public class CImgLocalizer : CComponent {
	#region 변수
	private string m_oCountryCode = string.Empty;
	private SystemLanguage m_eSystemLanguage = SystemLanguage.Unknown;

	[SerializeField] private string m_oBasePath = string.Empty;

	/** =====> UI <===== */
	private Image m_oImg = null;
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oImg = this.GetComponentInChildren<Image>();

#if NEWTON_SOFT_JSON_MODULE_ENABLE
		m_oCountryCode = CCommonAppInfoStorage.Inst.CountryCode;
		m_eSystemLanguage = CCommonAppInfoStorage.Inst.SystemLanguage;
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.SetupImg();
	}

	/** 지역화를 리셋한다 */
	public virtual void ResetLocalize() {
		this.SetupImg();
	}

	/** 이미지를 변경한다 */
	public void SetImg(string a_oBasePath) {
		this.SetImg(a_oBasePath, m_oCountryCode, m_eSystemLanguage);
	}

	/** 이미지를 변경한다 */
	public void SetImg(string a_oBasePath, string a_oCountryCode, SystemLanguage a_eSystemLanguage) {
		m_oBasePath = a_oBasePath;
		m_oCountryCode = a_oCountryCode;
		m_eSystemLanguage = a_eSystemLanguage;

		this.SetupImg();
	}

	/** 이미지를 설정한다 */
	private void SetupImg() {
		string oDefPath = CFactory.MakeLocalizePath(m_oBasePath, KCDefine.B_DEF_LANGUAGE.ToString());
		string oImgPath = CFactory.MakeLocalizePath(m_oBasePath, oDefPath, m_oCountryCode, m_eSystemLanguage.ToString());

		m_oImg.sprite = CResManager.Inst.GetRes<Sprite>(oImgPath);
	}
	#endregion			// 함수
}
