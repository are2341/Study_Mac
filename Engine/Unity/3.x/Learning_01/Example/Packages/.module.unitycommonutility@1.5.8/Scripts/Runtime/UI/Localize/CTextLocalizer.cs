using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 텍스트 지역화
public class CTextLocalizer : CUIsComponent {
	#region 변수
	[SerializeField] private string m_oKey = string.Empty;
	[SerializeField] private string m_oThaiFontPath = "Fonts/Global/G_ThaiFont";

	private Font m_oThaiFont = null;
	private Font m_oOriginFont = null;
	#endregion			// 변수

	#region UI 변수
	private Text m_oText = null;
	#endregion			// UI 변수

	#region 함수
	//! 초기화
	public override void Awake() {
		base.Awake();
		m_oText = this.GetComponentInChildren<Text>();

		m_oThaiFont = CResManager.Inst.GetRes<Font>(m_oThaiFontPath);
		m_oOriginFont = m_oText.font;
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetupStr();
	}
	
	//! 지역화를 리셋한다
	public virtual void ResetLocalize() {
		this.SetupStr();
	}

	//! 문자열을 변경한다
	public void SetStr(string a_oKey) {
		m_oKey = a_oKey;
		this.SetupStr();
	}

	//! 태국어 폰트 변경한다
	public void SetThaiFont(string a_oFontPath) {
		m_oThaiFont = CResManager.Inst.GetRes<Font>(a_oFontPath);
		this.SetupStr();
	}

	//! 원본 폰트를 변경한다
	public void SetOriginFont(string a_oFontPath) {
		m_oOriginFont = CResManager.Inst.GetRes<Font>(a_oFontPath);
		this.SetupStr();
	}

	//! 문자열을 설정한다
	private void SetupStr() {
		m_oText.text = CStrTable.Inst.GetStr(m_oKey);
		m_oText.font = ThaiFontAdjuster.IsThaiString(m_oText.text) ? m_oThaiFont : m_oOriginFont;
	}
	#endregion			// 함수
}
