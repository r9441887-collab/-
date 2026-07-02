using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class EnterAndExit : MonoBehaviour
{
	// Token: 0x060008B9 RID: 2233 RVA: 0x00013B06 File Offset: 0x00011D06
	public virtual void OnMouseEnter()
	{
		this.isEnter = true;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00013B0F File Offset: 0x00011D0F
	public virtual void OnMouseExit()
	{
		this.isEnter = false;
	}

	// Token: 0x0400099D RID: 2461
	protected bool isEnter;
}
