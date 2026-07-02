using System;
using Coffee.UIEffects;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class WindowShadow : MonoBehaviour
{
	// Token: 0x060008A2 RID: 2210 RVA: 0x000139B2 File Offset: 0x00011BB2
	private void Start()
	{
		base.GetComponent<UIShadow>().style = ShadowStyle.Shadow3;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}
}
