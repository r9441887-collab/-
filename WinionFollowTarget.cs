using System;
using project.Scripts.CharacterScripts;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class WinionFollowTarget : MonoBehaviour, IHandler
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000124F1 File Offset: 0x000106F1
	// (set) Token: 0x060006A9 RID: 1705 RVA: 0x000124F9 File Offset: 0x000106F9
	public WinionHandler winionHandler { get; set; }

	// Token: 0x060006AA RID: 1706 RVA: 0x0003C100 File Offset: 0x0003A300
	private void Awake()
	{
		this.winionHandler = base.GetComponent<WinionHandler>();
		Vector3 vector = this.GetTargetPosition();
		this.targetPosition = this.winionHandler.winionMovement.SetTargetPosition(vector, false);
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00012502 File Offset: 0x00010702
	private void Start()
	{
		this.winionHandler.winionMovement.isArrive = false;
		this.useStart();
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void useStart()
	{
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0003C138 File Offset: 0x0003A338
	private void FixedUpdate()
	{
		if (this.winionHandler.winionStatus.IsCharging)
		{
			return;
		}
		if (this.winionHandler.winionStatus.winionInfo.isDischarged)
		{
			Object.Destroy(this);
			return;
		}
		if (this.winionHandler.CanChangeAnimation())
		{
			Vector3 vector = this.GetTargetPosition();
			this.targetPosition = this.winionHandler.winionMovement.SetTargetPosition(vector, false);
			this.CheckDistance();
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
	public void CheckDistance()
	{
		if (this.winionHandler.winionMovement.isArrive)
		{
			if (!this.arriveTarget && this.winionHandler.winionMovement.canFollowing)
			{
				this.winionHandler.winionMovement.waitTime = 2.1474836E+09f;
				this.winionHandler.winionMovement.canFollowing = false;
				this.arriveTarget = true;
				this.FirstActionAfterArrive();
			}
			if (Vector3.Distance(base.transform.position, this.targetPosition) > 0.55f && this.arriveTarget && !this.winionHandler.winionMovement.canFollowing)
			{
				this.SecondActionAfterArrive();
			}
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0001251B File Offset: 0x0001071B
	public virtual void FirstActionAfterArrive()
	{
		this.winionHandler.winionAnimator.PlayAnimation("Digging", false);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00012533 File Offset: 0x00010733
	public virtual void SecondActionAfterArrive()
	{
		this.arriveTarget = false;
		this.winionHandler.winionMovement.StopCurrentMove();
		this.winionHandler.winionMovement.InitArriveTime(1.5f, 3f);
		this.winionHandler.SetIdleByWinionStatus();
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00012571 File Offset: 0x00010771
	public virtual Vector3 GetTargetPosition()
	{
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0400075C RID: 1884
	public bool arriveTarget;

	// Token: 0x0400075D RID: 1885
	public Vector3 targetPosition;
}
