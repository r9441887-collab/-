using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class OnlyOneComponent<T> : MonoBehaviour
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00013096 File Offset: 0x00011296
	// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001309E File Offset: 0x0001129E
	public WinionHandler winionHandler { get; set; }

	// Token: 0x060007B5 RID: 1973 RVA: 0x000130A7 File Offset: 0x000112A7
	public void Start()
	{
		this.winionHandler = base.GetComponent<WinionHandler>();
		if (base.GetComponents<T>().Length > 1)
		{
			Object.Destroy(this);
		}
		this.useStart();
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void useStart()
	{
	}
}
