using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class WinionFollowWinion : WinionFollowTarget
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x00012587 File Offset: 0x00010787
	public override void useStart()
	{
		base.winionHandler.winionMovement.SetArriveDistance(0.3f);
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0001259E File Offset: 0x0001079E
	public override Vector3 GetTargetPosition()
	{
		if (this.targetWinion == null)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return this.targetWinion.transform.position;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x000125D3 File Offset: 0x000107D3
	public override void FirstActionAfterArrive()
	{
		base.winionHandler.winionAnimator.PlayAnimation("Digging", false);
		Object.Destroy(this);
	}

	// Token: 0x0400075E RID: 1886
	public WinionHandler targetWinion;
}
