using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class WinionPooping : MonoBehaviour
{
	// Token: 0x06000802 RID: 2050 RVA: 0x00041B40 File Offset: 0x0003FD40
	private void Start()
	{
		if (base.GetComponents<WinionSleeping>().Length > 1)
		{
			Object.Destroy(this);
		}
		this.timer = 0f;
		this.winionHandler = base.GetComponent<WinionHandler>();
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.ChangeCharacterState(CharacterState.Pooping);
		this.winionHandler.winionAnimator.PlayAnimation("Pooping", false);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x000133FC File Offset: 0x000115FC
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= 5f)
		{
			this.WakeUp(false);
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00041BAC File Offset: 0x0003FDAC
	public void WakeUp(bool force)
	{
		this.winionHandler.winionBehaviour.isBusy = false;
		if (force)
		{
			base.StartCoroutine("AngryWake");
			return;
		}
		SingletoneBehaviour<PoopManager>.Instance.CreatePoop(this.winionHandler);
		this.winionHandler.SetIdleByWinionStatus();
		this.winionHandler.winionMovement.canInterrupt = true;
		this.winionHandler.winionMovement.waitAndPlay = true;
		this.winionHandler.winionMovement.MoveToRandomPosition();
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		Object.Destroy(this);
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00013424 File Offset: 0x00011624
	private IEnumerator AngryWake()
	{
		this.winionHandler.winionMovement.SetTargetPosition(base.transform.position, true);
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.winionAnimator.PlayAnimation("Angry", false);
		yield return new WaitForSeconds(2f);
		this.winionHandler.SetIdleByWinionStatus();
		this.winionHandler.winionMovement.canInterrupt = true;
		this.winionHandler.winionMovement.waitAndPlay = true;
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		Object.Destroy(this);
		yield break;
	}

	// Token: 0x040008DF RID: 2271
	public WinionHandler winionHandler;

	// Token: 0x040008E0 RID: 2272
	public float timer;
}
