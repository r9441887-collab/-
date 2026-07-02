using System;
using System.Collections.Generic;
using project.Scripts.CharacterScripts;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class WinionBehaviour : MonoBehaviour, IHandler
{
	// Token: 0x0600067A RID: 1658 RVA: 0x0003B030 File Offset: 0x00039230
	public void SetCanInterrupt(bool canInterrupt)
	{
		this.CanInterrupt = canInterrupt;
		Winion winionType = this.winionHandler.winionStatus.winionInfo.winionType;
		SingletoneBehaviour<WinionDistance>.Instance.distances[(int)winionType].canShake = canInterrupt;
		this.winionHandler.winionDragAndDrop.canDragAndDrop = canInterrupt;
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600067B RID: 1659 RVA: 0x000123A3 File Offset: 0x000105A3
	// (set) Token: 0x0600067C RID: 1660 RVA: 0x000123AB File Offset: 0x000105AB
	public WinionHandler winionHandler { get; set; }

	// Token: 0x0600067D RID: 1661 RVA: 0x0003B084 File Offset: 0x00039284
	public void ArriveAction()
	{
		this.winionHandler.winionMovement.haveDestination = false;
		this.moveRandomPos = true;
		Action action = this.arriveAction;
		if (action != null)
		{
			action();
		}
		if (!this.CanArriveAction)
		{
			return;
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			this.arriveAction = null;
			if (this.winionHandler.UIWinionEnabled)
			{
				return;
			}
		}
		if (this.moveRandomPos)
		{
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
			{
				if (this.beforeType != ActionType.Desktop)
				{
					this.beforeIndex = -1;
				}
				this.actionType = ActionType.Desktop;
			}
			else
			{
				if (DBManager.instance.dialogueData.curDialogue_ing)
				{
					return;
				}
				if (DBManager.instance.dialogueData.runNextEvent && this.winionHandler.winionMovement.stopEventRandomMovement)
				{
					if (this.beforeType != ActionType.Event)
					{
						this.beforeIndex = -1;
					}
					this.beforeType = this.actionType;
					this.actionType = ActionType.Event;
				}
			}
			ActionType actionType = this.actionType;
			if (actionType != ActionType.Desktop)
			{
				if (actionType != ActionType.Normal)
				{
					return;
				}
				this.winionHandler.winionMovement.MoveToRandomPosition();
			}
			else
			{
				if (!this.winionHandler.CanInterruptState(0))
				{
					return;
				}
				if (this.winionHandler.winionStatus.winionInfo.isDischarged)
				{
					return;
				}
				this.DesktopActions();
				return;
			}
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0003B1C8 File Offset: 0x000393C8
	private void DesktopActions()
	{
		this.isBusy = true;
		int num = this.GetDesktopActionIndex();
		int num2 = 0;
		if (this.beforeIndex == 3)
		{
			num = 1;
		}
		else
		{
			while (this.beforeIndex == num && num2++ < 100)
			{
				num = this.GetDesktopActionIndex();
			}
		}
		this.beforeIndex = num;
		switch (num)
		{
		case 0:
			this.isBusy = false;
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(this.winionHandler.GetWinionType(), true);
			return;
		case 1:
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(this.winionHandler.GetWinionType(), true);
			this.winionHandler.winionMovement.MoveToRandomPosition();
			this.isBusy = false;
			return;
		case 2:
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(this.winionHandler.GetWinionType(), false);
			ComponentHolderProtocol.AddComponent<WinionSleeping>(base.transform);
			return;
		case 3:
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(this.winionHandler.GetWinionType(), false);
			ComponentHolderProtocol.AddComponent<WinionPooping>(base.transform);
			return;
		case 4:
			this.SetCanInterrupt(false);
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(this.winionHandler.GetWinionType(), false);
			ComponentHolderProtocol.AddComponent<WinionGetBackStage>(base.transform);
			break;
		case 5:
			break;
		default:
			return;
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000123B4 File Offset: 0x000105B4
	public void WeightAppend(DesktopAction index, int value)
	{
		this.actionWeight.Add(value);
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0003B2F0 File Offset: 0x000394F0
	private int GetDesktopActionIndex()
	{
		this.actionWeight.Clear();
		this.WeightAppend(DesktopAction.DoNothing, 10);
		this.WeightAppend(DesktopAction.MoveToRandomPosition, 10);
		this.WeightAppend(DesktopAction.Sleeping, this.winionHandler.winionStatus.CanSleeping() ? 1 : 0);
		this.WeightAppend(DesktopAction.Poop, SingletoneBehaviour<DesktopController>.Instance.CanGetPoop() ? 2 : 0);
		this.WeightAppend(DesktopAction.GetBackStage, SingletoneBehaviour<DesktopController>.Instance.CanGetPictures() ? 1 : 0);
		this.WeightAppend(DesktopAction.DebugLog, 10);
		int num = 0;
		foreach (int num2 in this.actionWeight)
		{
			num += num2;
		}
		int num3 = Random.Range(0, num);
		int num4 = 0;
		int num5 = -1;
		for (int i = 0; i < this.actionWeight.Count; i++)
		{
			num4 += this.actionWeight[i];
			if (num3 < num4)
			{
				num5 = i;
				break;
			}
		}
		return num5;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x000123C2 File Offset: 0x000105C2
	public void StopDesktopAction()
	{
		this.RemoveComponent<WinionPooping>();
		this.RemoveComponent<WinionSleeping>();
		this.RemoveComponent<WinionFollowMouse>();
		this.RemoveComponent<WinionHangingMouse>();
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0003B3F8 File Offset: 0x000395F8
	public void RemoveComponent<T>() where T : Component
	{
		T component = base.GetComponent<T>();
		if (component != null)
		{
			Object.Destroy(component);
		}
	}

	// Token: 0x04000733 RID: 1843
	public bool CanInterrupt;

	// Token: 0x04000734 RID: 1844
	public ActionType actionType;

	// Token: 0x04000735 RID: 1845
	public ActionType beforeType;

	// Token: 0x04000737 RID: 1847
	public Action arriveAction;

	// Token: 0x04000738 RID: 1848
	public bool CanArriveAction = true;

	// Token: 0x04000739 RID: 1849
	public bool moveRandomPos = true;

	// Token: 0x0400073A RID: 1850
	public GameObject BombObject;

	// Token: 0x0400073B RID: 1851
	public GameObject LeftArm;

	// Token: 0x0400073C RID: 1852
	public GameObject RightArm;

	// Token: 0x0400073D RID: 1853
	public bool isBusy;

	// Token: 0x0400073E RID: 1854
	private int beforeIndex;

	// Token: 0x0400073F RID: 1855
	public List<int> actionWeight = new List<int>();

	// Token: 0x04000740 RID: 1856
	public GameObject TargetWindow;
}
