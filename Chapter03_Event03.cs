using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class Chapter03_Event03 : EventBase
{
	// Token: 0x06001836 RID: 6198 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000B150C File Offset: 0x000AF70C
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 3;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		if (!ScreenCanvas.Instance.removeHomeUI)
		{
			ScreenCanvas.Instance.RemoveHomeUI();
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000B15BC File Offset: 0x000AF7BC
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
			SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
			if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0.1f)
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0.1f, false);
			}
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 1f;
				SoundManager.instance.BGM_ChangePitch(4f, 1f);
			}
			else
			{
				SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1f);
				SystemWinion.instance.systemWinionAnimator.SetIdleEye();
				SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
				SystemWinion.instance.inSystemWinionRoom = false;
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
					SoundManager.instance.bgmPlayer.volume = 0f;
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(4f, 1f, false);
				}
				if (SoundManager.instance.bgmPlayer.pitch != 1f)
				{
					SoundManager.instance.BGM_ChangePitch(4f, 1f);
				}
			}
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
			this.ION.winionAnimator.PlayAnimation("FrontIdle", false);
			this.ION.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this.Fix.SetActiveWorldWinion(true);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionStatus.isFriend01 = true;
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.friend01_Pos00);
			this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
			this._Debug.SetActiveWorldWinion(true);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionStatus.isFriend02 = true;
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.friend02_Pos00);
			this._Debug.winionLookAt.LookAtTarget(this.ION.gameObject);
			Color color;
			if (ColorUtility.TryParseHtmlString("#8C8C8C", ref color))
			{
				this._Debug.winionAnimator.spriteRenderer.color = color;
				this.Fix.winionAnimator.spriteRenderer.color = color;
			}
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000186C9 File Offset: 0x000168C9
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

	// Token: 0x0600183B RID: 6203 RVA: 0x000B1954 File Offset: 0x000AFB54
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 1)
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

	// Token: 0x0600183C RID: 6204 RVA: 0x00018703 File Offset: 0x00016903
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x00018719 File Offset: 0x00016919
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
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.PlayAnimation("FrontIdle_Bizit_Sad", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040014EA RID: 5354
	[Space]
	[Header("챕터 3의 이벤트 3번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x040014EB RID: 5355
	public Transform friend01_Pos00;

	// Token: 0x040014EC RID: 5356
	public Transform friend02_Pos00;

	// Token: 0x040014ED RID: 5357
	[Space]
	[Header("챕터 3의 이벤트 3번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;
}
