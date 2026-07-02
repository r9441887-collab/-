using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class MazeDoorAutoOpen : MonoBehaviour
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0000FD87 File Offset: 0x0000DF87
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && this.FirstEnter)
		{
			this.FirstEnter = false;
			this.door.isLocked = false;
			this.door.Opening(false, false);
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000FDC3 File Offset: 0x0000DFC3
	private void OnDisable()
	{
		this.FirstEnter = true;
	}

	// Token: 0x04000320 RID: 800
	public DoorInteraction door;

	// Token: 0x04000321 RID: 801
	public bool FirstEnter = true;
}
