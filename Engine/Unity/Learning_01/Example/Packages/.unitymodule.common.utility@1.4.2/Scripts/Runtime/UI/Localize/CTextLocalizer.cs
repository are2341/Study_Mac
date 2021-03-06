﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 텍스트 지역화
public class CTextLocalizer : CUIsComponent {
	#region 변수
	[SerializeField] private string m_oKey = string.Empty;
	[SerializeField] private string m_oThaiFontPath = "Fonts/Global/G_ThaiFont";
	
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
	}

	//! 초기화
	public override void Start() {
		base.Start();
		this.SetStr(m_oKey);
	}
	
	//! 지역화를 리셋한다
	public virtual void ResetLocalize() {
		this.SetStr(m_oKey);
	}

	//! 문자열을 변경한다
	public void SetStr(string a_oKey) {
		m_oKey = a_oKey;
		m_oOriginFont = m_oOriginFont ?? m_oText.font;

		m_oText.text = CStrTable.Inst.GetStr(a_oKey);
		m_oText.font = ThaiFontAdjuster.IsThaiString(m_oText.text) ? CResManager.Inst.GetRes<Font>(m_oThaiFontPath) : m_oOriginFont;
	}

	//! 태국어 폰트 경로를 변경한다
	public void SetThaiFontPath(string a_oFontPath) {
		m_oThaiFontPath = a_oFontPath;
		this.ResetLocalize();
	}
	#endregion			// 함수
}
