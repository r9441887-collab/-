using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class TMP_FontChanger : MonoBehaviour
{
	// Token: 0x0600042E RID: 1070 RVA: 0x00010B15 File Offset: 0x0000ED15
	private void Start()
	{
		DOVirtual.Float(0f, 1f, 1f, null).OnComplete(delegate
		{
			this._TextMeshPro = Object.FindObjectsOfType<TextMeshPro>().ToList<TextMeshPro>();
			this._TextMeshProUGUI = Object.FindObjectsOfType<TextMeshProUGUI>().ToList<TextMeshProUGUI>();
		});
	}

	// Token: 0x04000477 RID: 1143
	[SerializeField]
	public TMP_FontAsset FontAsset;

	// Token: 0x04000478 RID: 1144
	public List<TextMeshPro> _TextMeshPro;

	// Token: 0x04000479 RID: 1145
	public List<TextMeshProUGUI> _TextMeshProUGUI;
}
