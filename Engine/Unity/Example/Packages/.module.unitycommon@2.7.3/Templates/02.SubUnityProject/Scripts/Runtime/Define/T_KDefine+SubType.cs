﻿#if SCRIPT_TEMPLATE_ONLY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if EXTRA_SCRIPT_MODULE_ENABLE
using MessagePack;

#region 기본
/** 서브 타입 랩퍼 */
[MessagePackObject]
public partial struct STSubTypeWrapper {
	// Do Something
}
#endregion			// 기본
#endif			// #if EXTRA_SCRIPT_MODULE_ENABLE
#endif			// #if SCRIPT_TEMPLATE_ONLY