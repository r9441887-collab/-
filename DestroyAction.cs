using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class DestroyAction : MonoBehaviour
{
	// Token: 0x06001AC7 RID: 6855 RVA: 0x000193D4 File Offset: 0x000175D4
	private void OnDestroy()
	{
		Action action = this.destroyAction;
		if (action != null)
		{
			action();
		}
		this.destroyAction = null;
	}

	// Token: 0x040017D4 RID: 6100
	public Action destroyAction;
}
