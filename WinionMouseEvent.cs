using System;
using project.Scripts.CharacterScripts;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class WinionMouseEvent : EnterAndExit, IHandler
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060006EC RID: 1772 RVA: 0x00012840 File Offset: 0x00010A40
	// (set) Token: 0x060006ED RID: 1773 RVA: 0x00012848 File Offset: 0x00010A48
	public WinionHandler winionHandler { get; set; }

	// Token: 0x060006EE RID: 1774 RVA: 0x0003CEAC File Offset: 0x0003B0AC
	public override void OnMouseEnter()
	{
		base.OnMouseEnter();
		if (!this.canMouseEnter)
		{
			return;
		}
		if (!this.winionHandler.CanChangeAnimation())
		{
			return;
		}
		if (this.winionHandler.blockDialogue)
		{
			return;
		}
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			return;
		}
		if (this.winionHandler.winionStatus.IsCharging)
		{
			return;
		}
		if (MouseRaycast.isMouseOnUI)
		{
			return;
		}
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		if (!Application.isFocused)
		{
			return;
		}
		if (ScreenCanvas.Instance.WinionOption.activeSelf)
		{
			return;
		}
		if (this.MouseEnter || DragAndDrop.MouseClick)
		{
			return;
		}
		if (this.winionHandler.winionDragAndDrop.isPickUp)
		{
			return;
		}
		if (this.winionHandler.characterState == CharacterState.Sleeping)
		{
			return;
		}
		if (this.winionHandler.characterState == CharacterState.Pooping)
		{
			return;
		}
		if (this.winionHandler.characterState == CharacterState.Hanging || WinionHangingMouse.activeAlready)
		{
			this.winionHandler.SetOutline(false);
			return;
		}
		this.MouseEnter = true;
		this.MouseEnterWhileMoving = this.winionHandler.winionMovement.isMoving;
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.MouseEnterWhileWaiting = this.winionHandler.winionMovement.waitAndPlay;
		this.winionHandler.winionAnimator.PlayAnimation("FrontIdle", false);
		this.winionHandler.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
		this.winionHandler.ChangeCharacterState(CharacterState.FrontIdle);
		this.winionHandler.SetOutline(true);
		this.MouseEnterWhileLookat = this.winionHandler.winionLookAt.lookAt;
		this.winionHandler.winionLookAt.lookAt = false;
		this.winionHandler.winionMovement.StopCurrentMove();
		this.winionHandler.winionMovement.SetNextMoveTimer(false);
		this.winionHandler.winionAnimator.Interrupting();
		ScreenCanvas.Instance.ActiveWinionOption(true);
		ScreenCanvas.Instance.SetTarget(base.gameObject);
		SingletoneBehaviour<WinionBatteryCenter>.Instance.SetTarget(this.winionHandler);
		if (SingletoneBehaviour<InventoryManager>.Instance != null)
		{
			SingletoneBehaviour<InventoryManager>.Instance.targetWinion = this.winionHandler;
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
	public void OnMouseDown()
	{
		if (!this.canMouseEnter)
		{
			return;
		}
		if (!this.winionHandler.CanChangeAnimation())
		{
			return;
		}
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			return;
		}
		if (this.winionHandler.winionStatus.IsCharging)
		{
			return;
		}
		if (MouseRaycast.isMouseOnUI)
		{
			return;
		}
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		if (!ScreenCanvas.Instance.WinionOption.activeSelf)
		{
			this.MouseEnter = true;
			ScreenCanvas.Instance.ActiveWinionOption(true);
			ScreenCanvas.Instance.SetTarget(base.gameObject);
			SingletoneBehaviour<WinionBatteryCenter>.Instance.SetTarget(this.winionHandler);
		}
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0003D168 File Offset: 0x0003B368
	public void InitializeEvent()
	{
		if (this.winionHandler.blockDialogue)
		{
			return;
		}
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			return;
		}
		if (this.winionHandler.characterState == CharacterState.Sleeping)
		{
			return;
		}
		if (!this.canMouseEnter)
		{
			return;
		}
		if (!this.winionHandler.CanChangeAnimation())
		{
			this.MouseEnter = false;
			this.MouseEnterWhileMoving = false;
			SingletoneBehaviour<InventoryManager>.Instance.targetWinion = null;
			return;
		}
		if (this.winionHandler.winionStatus.IsCharging)
		{
			return;
		}
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		if (this.winionHandler.winionDragAndDrop.isPickUp)
		{
			return;
		}
		if (this.MouseEnterWhileMoving)
		{
			this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		}
		else if (this.MouseEnterWhileLookat)
		{
			this.winionHandler.winionLookAt.lookAt = true;
		}
		if (this.MouseEnterWhileWaiting)
		{
			this.winionHandler.winionMovement.SetNextMoveTimer(true);
		}
		this.winionHandler.SetOutline(false);
		this.MouseEnter = false;
		this.MouseEnterWhileMoving = false;
		if (SingletoneBehaviour<InventoryManager>.Instance != null)
		{
			SingletoneBehaviour<InventoryManager>.Instance.targetWinion = null;
		}
	}

	// Token: 0x040007C5 RID: 1989
	public bool canMouseEnter;

	// Token: 0x040007C6 RID: 1990
	public bool MouseEnter;

	// Token: 0x040007C7 RID: 1991
	public bool MouseEnterWhileMoving;

	// Token: 0x040007C8 RID: 1992
	public bool MouseEnterWhileLookat;

	// Token: 0x040007C9 RID: 1993
	public bool MouseEnterWhileWaiting;
}
