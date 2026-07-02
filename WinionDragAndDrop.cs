using System;
using project.Scripts.CharacterScripts;

// Token: 0x02000110 RID: 272
public class WinionDragAndDrop : DragAndDrop, IHandler
{
	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000684 RID: 1668 RVA: 0x000123FD File Offset: 0x000105FD
	// (set) Token: 0x06000685 RID: 1669 RVA: 0x00012405 File Offset: 0x00010605
	public WinionHandler winionHandler { get; set; }

	// Token: 0x06000686 RID: 1670 RVA: 0x0001240E File Offset: 0x0001060E
	public void SetActiveDrag(bool value)
	{
		this.canDragAndDrop = value;
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0003B428 File Offset: 0x00039628
	public override void OnMouseDown()
	{
		if (WinionHangingMouse.activeAlready)
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
		base.OnMouseDown();
		SingletoneBehaviour<WinionBatteryCenter>.Instance.SetTarget(this.winionHandler);
		if (this.winionHandler.characterState == CharacterState.Sleeping)
		{
			this.wasSleeping = true;
		}
		if (this.winionHandler.characterState == CharacterState.Pooping)
		{
			this.wasPooping = true;
		}
		this.elapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0003B4D0 File Offset: 0x000396D0
	public override void OnMouseUp()
	{
		if (WinionHangingMouse.activeAlready)
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
		base.OnMouseUp();
		if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - this.elapsedTime <= 150L)
		{
			if (this.winionHandler.winionStatus.winionInfo.isDischarged)
			{
				return;
			}
			this.winionHandler.winionMouseEvent.MouseEnter = false;
			ScreenCanvas.Instance.SetTarget(base.gameObject);
			if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
			{
				ScreenCanvas.Instance.TalkWinion();
			}
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0003B598 File Offset: 0x00039798
	public override void OnMouseDrag()
	{
		if (WinionHangingMouse.activeAlready)
		{
			return;
		}
		if (!this.canDragAndDrop)
		{
			return;
		}
		if (this.winionHandler.blockDialogue)
		{
			return;
		}
		if (MouseRaycast.isMouseOnUI)
		{
			return;
		}
		this.winionHandler.SetOutline(false);
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			if (this.DragEndWhileDialogue)
			{
				return;
			}
			this.DragEndWhileDialogue = true;
			base.OnMouseDragEnd();
			base.OnMouseUp();
			this.dragEndReturnPosition = true;
			this.winionHandler.winionDragAndDrop.isPickUp = false;
			this.winionHandler.winionMouseEvent.MouseEnter = false;
			ScreenCanvas.Instance.CloseOption();
			this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
			this.winionHandler.winionLookAt.lookAt = true;
			CharacterState characterState = this.winionHandler.characterState;
			if (characterState == CharacterState.FrontIdle)
			{
				this.winionHandler.winionAnimator.PlayAnimation("FrontIdle", false);
				return;
			}
			if (characterState == CharacterState.SideIdle)
			{
				this.winionHandler.winionAnimator.PlayAnimation("LeftIdle", false);
				return;
			}
			if (characterState != CharacterState.BackIdle)
			{
				return;
			}
			this.winionHandler.winionAnimator.PlayAnimation("BackIdle", false);
			return;
		}
		else
		{
			if (this.winionHandler.winionStatus.IsCharging)
			{
				return;
			}
			base.OnMouseDrag();
			return;
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0003B6D8 File Offset: 0x000398D8
	public override void OnMouseDragStart()
	{
		if (WinionHangingMouse.activeAlready)
		{
			return;
		}
		if (!this.canDragAndDrop)
		{
			return;
		}
		if (MouseRaycast.isMouseOnUI)
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
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		base.OnMouseDragStart();
		this.winionHandler.winionMovement.StopCurrentMove();
		this.winionHandler.winionAnimator.PlayAnimation("PickUp", false);
		this.winionHandler.characterState = CharacterState.PickUp;
		ScreenCanvas.Instance.preAnimationPickUp = true;
		this.winionHandler.winionMovement.SetNextMoveTimer(false);
		ScreenCanvas.Instance.ActiveWinionOption(true);
		ScreenCanvas.Instance.SetTarget(base.gameObject);
		DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0003B7BC File Offset: 0x000399BC
	public override void OnMouseDragEnd()
	{
		if (WinionHangingMouse.activeAlready)
		{
			return;
		}
		if (!this.canDragAndDrop)
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
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		base.OnMouseDragEnd();
		if (this.wasSleeping)
		{
			WinionSleeping component = base.GetComponent<WinionSleeping>();
			if (component != null)
			{
				component.WakeUp(true);
			}
			this.wasSleeping = false;
			return;
		}
		if (this.wasPooping)
		{
			WinionPooping component2 = base.GetComponent<WinionPooping>();
			if (component2 != null)
			{
				component2.WakeUp(true);
			}
			this.wasPooping = false;
			return;
		}
		this.winionHandler.winionMovement.ReadyNextMove();
		this.winionHandler.winionMovement.SetNextMoveTimer(true);
		this.dragEndReturnPosition = true;
		this.DragEndWhileDialogue = false;
		this.winionHandler.winionMouseEvent.MouseEnter = false;
		ScreenCanvas.Instance.ActiveWinionOption(true);
		ScreenCanvas.Instance.SetTarget(base.gameObject);
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0003B8CC File Offset: 0x00039ACC
	public override void OnMouseClickUp()
	{
		if (WinionHangingMouse.activeAlready)
		{
			return;
		}
		if (!this.canDragAndDrop)
		{
			return;
		}
		if (MouseRaycast.isMouseOnUI)
		{
			return;
		}
		if (this.winionHandler.winionStatus.IsCharging)
		{
			return;
		}
		if (this.winionHandler.automaticEvent)
		{
			return;
		}
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		this.winionHandler.winionMouseEvent.MouseEnter = false;
	}

	// Token: 0x04000742 RID: 1858
	public bool canDragAndDrop;

	// Token: 0x04000743 RID: 1859
	public bool dragEndReturnPosition;

	// Token: 0x04000744 RID: 1860
	private bool wasSleeping;

	// Token: 0x04000745 RID: 1861
	private bool wasPooping;

	// Token: 0x04000746 RID: 1862
	private long elapsedTime;

	// Token: 0x04000747 RID: 1863
	public bool DragEndWhileDialogue;
}
