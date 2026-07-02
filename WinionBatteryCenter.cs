using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class WinionBatteryCenter : SingletoneBehaviour<WinionBatteryCenter>
{
	// Token: 0x06001E7C RID: 7804 RVA: 0x000DBA48 File Offset: 0x000D9C48
	private void Update()
	{
		if (this.BatteryCenter.activeSelf && this.autoClose)
		{
			this.autoCloseTimer += Time.deltaTime;
			if (this.autoCloseTimer >= 3f)
			{
				this.autoCloseTimer = 0f;
				this.autoClose = true;
				this.CloseCenter();
			}
		}
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x0001BB79 File Offset: 0x00019D79
	public void SetTarget(WinionHandler _target)
	{
		this.targetWinion = _target;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x0001BB82 File Offset: 0x00019D82
	private IEnumerator CheckCanCharge()
	{
		if (!this.ChargerOpen)
		{
			yield break;
		}
		if (this.targetWinion == null)
		{
			yield break;
		}
		yield return new WaitForEndOfFrame();
		this.isCharging = true;
		this.chargeWinion = this.targetWinion;
		this.chargeWinion.ChangeCharacterState(CharacterState.Charging);
		this.chargeWinion.winionAnimator.SetAnimationCanChange(true);
		this.chargeWinion.winionStatus.IsCharging = true;
		if (this.chargeWinion.winionStatus.winionInfo.battery >= 80)
		{
			base.StartCoroutine("StartAngryRoutine");
			yield break;
		}
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.RechargeSound, false, 1f, 1f);
		this.autoClose = false;
		this.chargeWinion.winionMovement.SetTargetPosition(base.transform.position, false);
		this.chargeWinion.winionMovement.SetActiveMovement(false, false, true);
		this.chargeWinion.ChangeCharacterState(CharacterState.Charging);
		this.chargeWinion.winionAnimator.PlayAnimation("Lay", false);
		this.chargeWinion.winionStatus.winionInfo.battery = 100;
		this.chargeWinion.winionStatus.winionInfo.isDischarged = false;
		base.StartCoroutine("StartChargeRoutine");
		ScreenCanvas.Instance.OnMouseExit();
		yield break;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0001BB91 File Offset: 0x00019D91
	public void StartCharge()
	{
		base.StartCoroutine("CheckCanCharge");
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x000DBAA4 File Offset: 0x000D9CA4
	public void EndCharge()
	{
		this.chargeWinion.winionStatus.IsCharging = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(this.chargeWinion.winionStatus.winionInfo.winionType, Winion.BatteryCenter);
		this.isCharging = false;
		this.targetWinion = null;
		this.chargeWinion = null;
		this.autoClose = true;
		Action endChargeAction = this.EndChargeAction;
		if (endChargeAction == null)
		{
			return;
		}
		endChargeAction();
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x0001BB9F File Offset: 0x00019D9F
	public void CloseCenter()
	{
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.BatteryCenter, false);
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x0001BBAD File Offset: 0x00019DAD
	private IEnumerator StartAngryRoutine()
	{
		yield return new WaitForEndOfFrame();
		this.chargeWinion.ChangeCharacterState(CharacterState.Charging);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.NegativeRetro, false, 1f, 1f);
		this.chargeWinion.winionAnimator.PlayAnimation("Angry", false);
		yield return new WaitForSeconds(2f);
		if (!DBManager.instance.dialogueData.runNextEvent && !this.chargeWinion.winionMovement.stopEventRandomMovement)
		{
			this.chargeWinion.winionMovement.MoveToRandomPosition();
		}
		this.chargeWinion.winionMovement.InitArriveTime(1.5f, 3f);
		this.EndCharge();
		yield break;
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x0001BBBC File Offset: 0x00019DBC
	private IEnumerator StartChargeRoutine()
	{
		this.coverAnimator.PlayAnimation("CoverOpen", false);
		this.coverAnimator.isLoop = false;
		this.coverAnimator.isEnd = false;
		while (!this.coverAnimator.isEnd)
		{
			yield return null;
		}
		this.chargeWinion.winionAnimator.PlayAnimation("Charge", false);
		this.coverAnimator.PlayAnimation("ChargeStart", false);
		this.coverAnimator.isLoop = false;
		this.coverAnimator.isEnd = false;
		while (!this.coverAnimator.isEnd)
		{
			yield return null;
		}
		this.coverAnimator.PlayAnimation("ChargeWait", false);
		this.coverAnimator.isLoop = true;
		this.coverAnimator.isEnd = false;
		yield return new WaitForSeconds(2f);
		this.coverAnimator.PlayAnimation("ChargeEnd", false);
		this.coverAnimator.isLoop = false;
		this.coverAnimator.isEnd = false;
		while (!this.coverAnimator.isEnd)
		{
			yield return null;
		}
		this.coverAnimator.PlayAnimation("CoverClose", false);
		this.coverAnimator.isLoop = false;
		this.coverAnimator.isEnd = false;
		while (!this.coverAnimator.isEnd)
		{
			yield return null;
		}
		this.coverAnimator.PlayAnimation("CoverIdle", false);
		this.coverAnimator.isLoop = true;
		this.chargeWinion.winionMovement.SetActiveMovement(true, false, false);
		this.EndCharge();
		yield break;
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x000DBB10 File Offset: 0x000D9D10
	public void SetBatteryCenter(bool value)
	{
		this.ChargerOpen = value;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(value);
		}
	}

	// Token: 0x04001CB0 RID: 7344
	public bool ChargerOpen;

	// Token: 0x04001CB1 RID: 7345
	public WinionHandler targetWinion;

	// Token: 0x04001CB2 RID: 7346
	public WinionHandler chargeWinion;

	// Token: 0x04001CB3 RID: 7347
	public float dist;

	// Token: 0x04001CB4 RID: 7348
	public float nearDist = 0.3f;

	// Token: 0x04001CB5 RID: 7349
	public bool autoClose;

	// Token: 0x04001CB6 RID: 7350
	public float autoCloseTimer;

	// Token: 0x04001CB7 RID: 7351
	public GameObject BatteryCenter;

	// Token: 0x04001CB8 RID: 7352
	public Transform ChargePosition;

	// Token: 0x04001CB9 RID: 7353
	public CustomAnimator baseAnimator;

	// Token: 0x04001CBA RID: 7354
	public CustomAnimator coverAnimator;

	// Token: 0x04001CBB RID: 7355
	public bool isCharging;

	// Token: 0x04001CBC RID: 7356
	public Action EndChargeAction;
}
