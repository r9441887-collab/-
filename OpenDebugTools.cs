using System;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class OpenDebugTools : IconDoubleClick
{
	// Token: 0x06001D57 RID: 7511 RVA: 0x0001B1B1 File Offset: 0x000193B1
	public override void OpenIcon()
	{
		GameObject targetWindow = this.targetWindow;
		if (targetWindow == null)
		{
			return;
		}
		targetWindow.SetActive(true);
	}
}
