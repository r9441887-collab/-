using System;

// Token: 0x02000366 RID: 870
[Serializable]
public class DialogueData
{
	// Token: 0x04001635 RID: 5685
	public bool curDialogue_ing;

	// Token: 0x04001636 RID: 5686
	public Winion curDialogue_Winion = Winion.None;

	// Token: 0x04001637 RID: 5687
	public bool smallTalk_ing;

	// Token: 0x04001638 RID: 5688
	public bool selecting_PlayerOptions;

	// Token: 0x04001639 RID: 5689
	public bool selectOption01;

	// Token: 0x0400163A RID: 5690
	public bool selectOption02;

	// Token: 0x0400163B RID: 5691
	public bool isDebug;

	// Token: 0x0400163C RID: 5692
	public bool runNextEvent;

	// Token: 0x0400163D RID: 5693
	public int curEventNum;

	// Token: 0x0400163E RID: 5694
	public int curEventDetailNum;

	// Token: 0x0400163F RID: 5695
	public bool finishEvent;

	// Token: 0x04001640 RID: 5696
	public EventBase curEvent;

	// Token: 0x04001641 RID: 5697
	public bool stopDialogue;

	// Token: 0x04001642 RID: 5698
	public Winion curSmallTalkWinion = Winion.None;

	// Token: 0x04001643 RID: 5699
	public int curSmallTalkID;

	// Token: 0x04001644 RID: 5700
	public bool OpenBackLog;

	// Token: 0x04001645 RID: 5701
	public bool NoBacklogOpen;
}
