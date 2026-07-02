using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000361 RID: 865
public class Chapter03_Event13 : EventBase
{
	// Token: 0x06001A0A RID: 6666 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x000BF140 File Offset: 0x000BD340
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 12;
		GameManager.instance.gameData.finish = true;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		this.SaveButton.interactable = false;
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x000BF1F4 File Offset: 0x000BD3F4
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			SystemWinion.instance.SystemWinion_Empty(true);
			if (ScreenCanvas.Instance.moveUI)
			{
				ScreenCanvas.Instance.ResetUI();
			}
			DBManager.instance.IsCutSceneSetting(false);
			bool creditEnd = EndingCredit.CreditEnd;
			EndingCredit.CreditEnd = false;
			SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 0f);
			SoundManager.instance.BGM_ChangeVolume_Tween((float)(creditEnd ? 15 : 5), 1f, false);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			SingletoneBehaviour<MailManager>.Instance.isReservationMail = true;
			SingletoneBehaviour<MailManager>.Instance.AddNewMailDelay(29);
			SingletoneBehaviour<MailManager>.Instance.AddNewMailDelay(30);
			SingletoneBehaviour<MailManager>.Instance.AddNewMailDelay(31);
			SingletoneBehaviour<MailManager>.Instance.AddNewMailDelay(32);
			SingletoneBehaviour<MailManager>.Instance.AddNewMailDelay(33);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_15", 1);
			PlayerPrefs.SetInt("CanOpenDesktopMode", 1);
		}
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x00018EDC File Offset: 0x000170DC
	public override void CheckEventDetailStartCondition()
	{
		if (!this.isSetting)
		{
			this.isSetting = true;
			this.SettingCondition(this.curEventDetailNum);
		}
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000BF368 File Offset: 0x000BD568
	public override void EndEvent()
	{
		this.endDialogue = true;
		int curEventDetailNum = this.curEventDetailNum;
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x0400161C RID: 5660
	public Button SaveButton;
}
