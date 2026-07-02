using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class WinionSleeping : MonoBehaviour
{
	// Token: 0x06000812 RID: 2066 RVA: 0x000425D4 File Offset: 0x000407D4
	private void Start()
	{
		if (base.GetComponents<WinionSleeping>().Length > 1)
		{
			Object.Destroy(this);
		}
		this.winionHandler = base.GetComponent<WinionHandler>();
		this.timer = 0f;
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.winionStatus.SetSleeping();
		this.winionHandler.ChangeCharacterState(CharacterState.Sleeping);
		this.winionHandler.winionAnimator.canChangeAnimation = true;
		this.winionHandler.winionAnimator.PlayAnimation("Sleeping", false);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0001345A File Offset: 0x0001165A
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= 30f)
		{
			this.WakeUp(false);
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00042660 File Offset: 0x00040860
	public void WakeUp(bool force)
	{
		this.winionHandler.winionBehaviour.isBusy = false;
		if (force)
		{
			base.StartCoroutine("AngryWake");
			return;
		}
		this.winionHandler.ChangeCharacterState(CharacterState.None);
		this.winionHandler.SetIdleByWinionStatus();
		this.winionHandler.winionMovement.canInterrupt = true;
		this.winionHandler.winionMovement.waitAndPlay = true;
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		this.winionHandler.winionStatus.ChangeFriendship(3f);
		Object.Destroy(this);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00013482 File Offset: 0x00011682
	private IEnumerator AngryWake()
	{
		this.winionHandler.winionMovement.SetTargetPosition(base.transform.position, true);
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.ChangeCharacterState(CharacterState.Angry);
		this.winionHandler.winionAnimator.PlayAnimation("Angry", false);
		yield return new WaitForSeconds(2f);
		this.winionHandler.ChangeCharacterState(CharacterState.None);
		this.winionHandler.SetIdleByWinionStatus();
		this.winionHandler.winionMovement.canInterrupt = true;
		this.winionHandler.winionMovement.waitAndPlay = true;
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		Object.Destroy(this);
		yield break;
	}

	// Token: 0x040008EE RID: 2286
	public WinionHandler winionHandler;

	// Token: 0x040008EF RID: 2287
	public float timer;
}
