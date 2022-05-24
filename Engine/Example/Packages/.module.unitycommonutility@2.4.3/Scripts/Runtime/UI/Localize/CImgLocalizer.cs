using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 이미지 지역화 */
public partial class CImgLocalizer : CComponent {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		COUNTRY_CODE,
		IMG,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[SerializeField] private string m_oBasePath = string.Empty;
	private SystemLanguage m_oSystemLanguage = SystemLanguage.Unknown;

	private Dictionary<EKey, string> m_oStrDict = new Dictionary<EKey, string>() {
		[EKey.COUNTRY_CODE] = string.Empty
	};

	/** =====> UI <===== */
	private Dictionary<EKey, Image> m_oImgDict = new Dictionary<EKey, Image>() {
		[EKey.IMG] = null
	};
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oImgDict[EKey.IMG] = this.GetComponentInChildren<Image>();

#if NEWTON_SOFT_JSON_MODULE_ENABLE
		m_oSystemLanguage = CCommonAppInfoStorage.Inst.SystemLanguage;
		m_oStrDict[EKey.COUNTRY_CODE] = CCommonAppInfoStorage.Inst.CountryCode;
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
		this.SetImg(a_oBasePath, m_oStrDict[EKey.COUNTRY_CODE], m_oSystemLanguage);
	}

	/** 이미지를 변경한다 */
	public void SetImg(string a_oBasePath, string a_oCountryCode, SystemLanguage a_eSystemLanguage) {
		m_oBasePath = a_oBasePath;
		m_oSystemLanguage = a_eSystemLanguage;
		m_oStrDict[EKey.COUNTRY_CODE] = a_oCountryCode;

		this.SetupImg();
	}

	/** 이미지를 설정한다 */
	private void SetupImg() {
		string oDefPath = CFactory.MakeLocalizePath(m_oBasePath, KCDefine.B_DEF_LANGUAGE.ToString());
		string oImgPath = CFactory.MakeLocalizePath(m_oBasePath, oDefPath, m_oStrDict[EKey.COUNTRY_CODE], m_oSystemLanguage.ToString());

		m_oImgDict[EKey.IMG].sprite = CResManager.Inst.GetRes<Sprite>(oImgPath);
	}
	#endregion			// 함수
}
