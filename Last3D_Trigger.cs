using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class Last3D_Trigger : MonoBehaviour
{
	// Token: 0x06000527 RID: 1319 RVA: 0x00034220 File Offset: 0x00032420
	private void OnTriggerEnter(Collider other)
	{
		if (this.firstEnter && other.tag == "Player" && this.FunctionName != "")
		{
			this.firstEnter = false;
			((this.SendToTarget == null) ? SingletoneBehaviour<Last3D_Manager>.Instance.transform : this.SendToTarget).SendMessage(this.FunctionName);
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0003428C File Offset: 0x0003248C
	private void OnTriggerExit(Collider other)
	{
		if (this.checkExit && other.tag == "Player" && this.FunctionName != "")
		{
			this.firstEnter = true;
			((this.SendToTarget == null) ? SingletoneBehaviour<SystemWinionRoomManager>.Instance.transform : this.SendToTarget).SendMessage(this.FunctionName + "_Exit");
		}
	}

	// Token: 0x040005B1 RID: 1457
	public Transform SendToTarget;

	// Token: 0x040005B2 RID: 1458
	public string FunctionName;

	// Token: 0x040005B3 RID: 1459
	public bool firstEnter = true;

	// Token: 0x040005B4 RID: 1460
	public bool checkExit;
}
