using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class Chapter02_Event23 : EventBase
{
	// Token: 0x0600164C RID: 5708 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000A30EC File Offset: 0x000A12EC
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter02;
		}
		this.eventNum = 23;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000A3198 File Offset: 0x000A1398
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			GameManager.instance.gameData.Debug_0.gameObject.SetActive(false);
			this.light.SetActive(true);
			base.SettingEvent(true);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 1f;
				SoundManager.instance.bgmPlayer.pitch = 0.55f;
			}
			else
			{
				SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
				if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0f)
				{
					SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0f, false);
				}
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 0.55f;
			}
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this._Debug, Winion.Debug, true, false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Bo, Winion.Debug, true, false);
			this.Bo.transform.position = new Vector3(1f, 0f, 0f);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			this._Debug.transform.position = new Vector3(0f, 0f, 0f);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			this._Debug.winionAnimator.PlayAnimation("lyingDown", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Folder_Debug);
			int num = 4;
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			component.roomImage.sprite = component.roomSprite[0];
			component.SwitchLightOff(false, this.Fix);
			component.SwitchLightOff(false, this._Debug);
			ScreenCanvas.Instance.debugFolderInteraction.ResetHorizontal();
			int childCount = ScreenCanvas.Instance.debugFolderInteraction.console.transform.childCount;
			ScreenCanvas.Instance.debugFolderInteraction.console.SetActive(true);
			for (int i = 0; i < childCount; i++)
			{
				ScreenCanvas.Instance.debugFolderInteraction.console.transform.GetChild(i).gameObject.SetActive(true);
			}
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Grid, Winion.Ion, true, false);
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x00017CB5 File Offset: 0x00015EB5
	public override void CheckEventDetailStartCondition()
	{
		if (!this.isSetting)
		{
			this.isSetting = true;
			this.SettingCondition(this.curEventDetailNum);
		}
		if (this.curEventDetailNum == 0 && this.startEvent)
		{
			this.checkCondition = false;
			this.curEventDetailNum_00();
		}
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0009B1BC File Offset: 0x000993BC
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 3)
		{
			this.isSetting = false;
			this.checkCondition = true;
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, true, this.eventNum);
		}
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x00017CEF File Offset: 0x00015EEF
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x00017D05 File Offset: 0x00015F05
	private IEnumerator curEventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
			yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		}
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		yield return new WaitForSeconds(2f);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionLookAt.LookAtTarget(this._Debug.gameObject);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("lyingDown_wakingup", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		yield return new WaitForSeconds(1f);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("lyingDown", false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		SoundManager.instance.BGM_ChangeVolume_Tween(7f, 0f, false);
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<MemoryRecovery>.Instance.MemoryPlayAction = delegate
		{
			base.BlockDialogue(false);
			SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		};
		SingletoneBehaviour<MemoryRecovery>.Instance.SetPastTime();
		yield break;
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x00016ADE File Offset: 0x00014CDE
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040013B4 RID: 5044
	[Space]
	[Header("챕터 2의 이벤트 11번 얼굴윈도우 위치")]
	public GameObject BoFacePos00;

	// Token: 0x040013B5 RID: 5045
	public GameObject DebugFacePos00;

	// Token: 0x040013B6 RID: 5046
	public GameObject light;
}
