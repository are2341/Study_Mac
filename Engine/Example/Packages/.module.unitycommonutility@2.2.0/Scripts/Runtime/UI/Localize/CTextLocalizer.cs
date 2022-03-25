using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 텍스트 지역화 */
public class CTextLocalizer : CComponent {
	/** 식별자 */
	private enum EKey {
		NONE = -1,
		TEXT,
		TMP_TEXT,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[SerializeField] private bool m_bIsIgnoreFontSetup = false;
	[SerializeField] private string m_oKey = string.Empty;
	[SerializeField] private EFontSet m_eFontSet = EFontSet._1;

	/** =====> UI <===== */
	private Dictionary<EKey, Text> m_oTextDict = new Dictionary<EKey, Text>() {
		[EKey.TEXT] = null
	};

	private Dictionary<EKey, TMP_Text> m_oTMPTextDict = new Dictionary<EKey, TMP_Text>() {
		[EKey.TMP_TEXT] = null
	};
	#endregion			// 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oTextDict[EKey.TEXT] = this.GetComponentInChildren<Text>();
		m_oTMPTextDict[EKey.TMP_TEXT] = this.GetComponentInChildren<TMP_Text>();
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.SetupStr();
	}
	
	/** 지역화를 리셋한다 */
	public virtual void ResetLocalize() {
		this.SetupStr();
	}

	/** 문자열을 변경한다 */
	public void SetStr(string a_oKey) {
		m_oKey = a_oKey;
		this.SetupStr();
	}

	/** 폰트 세트를 변경한다 */
	public void SetFontSet(EFontSet a_eSet) {
		m_eFontSet = a_eSet;
		this.SetupStr();
	}

	/** 문자열을 설정한다 */
	private void SetupStr() {
		// 폰트 설정 무시 모드 일 경우
		if(m_bIsIgnoreFontSetup) {
			m_oTextDict[EKey.TEXT]?.ExSetText<Text>(CStrTable.Inst.GetStr(m_oKey), false);
			m_oTMPTextDict[EKey.TMP_TEXT]?.ExSetText<TMP_Text>(CStrTable.Inst.GetStr(m_oKey), false);
		} else {
#if NEWTON_SOFT_JSON_MODULE_ENABLE
			m_oTextDict[EKey.TEXT]?.ExSetText(CStrTable.Inst.GetStr(m_oKey), CLocalizeInfoTable.Inst.GetFontSetInfo(m_eFontSet), false);
			m_oTMPTextDict[EKey.TMP_TEXT]?.ExSetText(CStrTable.Inst.GetStr(m_oKey), CLocalizeInfoTable.Inst.GetFontSetInfo(m_eFontSet), false);
#endif			// #if NEWTON_SOFT_JSON_MODULE_ENABLE
		}
	}
	#endregion			// 함수
}
