using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class WinionFollowMouse : WinionFollowTarget
{
	// Token: 0x060006A4 RID: 1700 RVA: 0x000124AC File Offset: 0x000106AC
	public override void useStart()
	{
		if (base.GetComponents<WinionFollowMouse>().Length > 1)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x000124BF File Offset: 0x000106BF
	public override Vector3 GetTargetPosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x000124D0 File Offset: 0x000106D0
	public override void FirstActionAfterArrive()
	{
		Action action = this.arriveAction;
		if (action != null)
		{
			action();
		}
		Object.Destroy(this);
	}

	// Token: 0x0400075A RID: 1882
	public Action arriveAction;
}
