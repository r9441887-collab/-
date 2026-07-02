using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class Chapter03_Event02 : EventBase
{
	// Token: 0x06001819 RID: 6169 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000B0AD4 File Offset: 0x000AECD4
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 2;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		ScreenCanvas.Instance.RemoveHomeUI();
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000B0B78 File Offset: 0x000AED78
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			base.SettingEvent(true);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
			if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0.1f)
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0.1f, false);
			}
			SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.SystemWinionRoom, false);
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 1.1f;
			}
			else
			{
				SystemWinion.instance.inSystemWinionRoom = false;
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 1.1f;
			}
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, false);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.ION.winionAnimator.SetAnimationCanChange(false);
			this.ION.winionAnimator.spriteRenderer.color = Color.black;
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionStatus.isFriend01 = true;
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionStatus.isFriend02 = true;
			this.Grid.Adjust_AlphaValue(0.5f, 0f);
			this.Grid.winionMovement.SettingPos_SetTargetPos(this.Grid_Pos00);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.systemWinionAnimator.SetIdleEye();
			SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
			this.startEvent = true;
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x0001860A File Offset: 0x0001680A
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

	// Token: 0x0600181E RID: 6174 RVA: 0x000B0F1C File Offset: 0x000AF11C
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 6)
		{
			this.isSetting = false;
			this.checkCondition = true;
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(true, true, this.eventNum);
		}
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x00018644 File Offset: 0x00016844
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x0001865A File Offset: 0x0001685A
	private IEnumerator EventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		this.Grid.Adjust_AlphaValue(0f, 6f);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		Color curColor = this.ION.winionAnimator.spriteRenderer.color;
		DOVirtual.Float(0f, 1f, 6f, delegate(float f)
		{
			curColor = new Color(f, f, f);
			this.ION.winionAnimator.spriteRenderer.color = curColor;
		});
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.PlayAnimation("Smile", true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.PlayAnimation("Smile", true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Bizit_Smile", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040014DE RID: 5342
	[Space]
	[Header("챕터 3의 이벤트 2번의 필요 변수들")]
	public Transform Grid_Pos00;

	// Token: 0x040014DF RID: 5343
	public Transform BizitPos00;

	// Token: 0x040014E0 RID: 5344
	[Space]
	[Header("챕터 3의 이벤트 2번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;
}
