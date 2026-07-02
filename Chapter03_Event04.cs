using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class Chapter03_Event04 : EventBase
{
	// Token: 0x0600184C RID: 6220 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000B1BFC File Offset: 0x000AFDFC
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 4;
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

	// Token: 0x0600184E RID: 6222 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000B1CAC File Offset: 0x000AFEAC
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
				SoundManager.instance.bgmPlayer.pitch = 1.1f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.bgmPlayer.volume = 1f;
				}
				SoundManager.instance.BGM_ChangePitch(5f, 1.1f);
			}
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.ION.winionAnimator.SetAnimationCanChange(false);
			this.Fix.SetActiveWorldWinion(true);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionStatus.isFriend01 = true;
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.friend01_Pos00);
			this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this._Debug.SetActiveWorldWinion(true);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionStatus.isFriend02 = true;
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.friend02_Pos00);
			this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
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
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.systemWinionAnimator.SetIdleEye();
			SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 0f);
			this.startEvent = true;
			return;
		}
		if (curEventDetailNum != 4)
		{
			return;
		}
		this.light01.SetActive(false);
		this.light02.SetActive(true);
		SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
		SoundManager.instance.BGM_ChangePitch(10f, 1f);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos01);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		SystemWinion.instance.SystemWinion_Empty(true);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 0f);
		this.startEvent = true;
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000B2138 File Offset: 0x000B0338
	public override void CheckEventDetailStartCondition()
	{
		if (!this.isSetting)
		{
			this.isSetting = true;
			this.SettingCondition(this.curEventDetailNum);
		}
		int curEventDetailNum = this.curEventDetailNum;
		if (curEventDetailNum != 0)
		{
			if (curEventDetailNum != 4)
			{
				return;
			}
			if (this.startEvent)
			{
				this.checkCondition = false;
				this.curEventDetailNum_04();
			}
		}
		else if (this.startEvent)
		{
			this.checkCondition = false;
			this.curEventDetailNum_00();
			return;
		}
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x000B219C File Offset: 0x000B039C
	public override void EndEvent()
	{
		this.endDialogue = true;
		int curEventDetailNum = this.curEventDetailNum;
		if (curEventDetailNum != 3 && curEventDetailNum == 6)
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

	// Token: 0x06001852 RID: 6226 RVA: 0x0001874B File Offset: 0x0001694B
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x00018761 File Offset: 0x00016961
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
		this.ION.winionAnimator.SetAnimationCanChange(true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.PlayAnimation("Smile", true);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Bizit_Smile", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		bool finish_fadeOut = false;
		Action action = delegate
		{
			finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1.5f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => finish_fadeOut);
		this.isSetting = false;
		this.checkCondition = true;
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x00018770 File Offset: 0x00016970
	private void curEventDetailNum_04()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_04_co());
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x00018786 File Offset: 0x00016986
	private IEnumerator EventDetailNum_04_co()
	{
		Chapter03_Event04.<>c__DisplayClass20_0 CS$<>8__locals1 = new Chapter03_Event04.<>c__DisplayClass20_0();
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1.5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.BizitPos02.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("BackIdle", false);
			this.ION.winionAnimator.SetAnimationCanChange(false);
		};
		yield return new WaitUntil(() => this.IONArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040014F4 RID: 5364
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x040014F5 RID: 5365
	public Transform BizitPos01;

	// Token: 0x040014F6 RID: 5366
	public Transform BizitPos02;

	// Token: 0x040014F7 RID: 5367
	public Transform friend01_Pos00;

	// Token: 0x040014F8 RID: 5368
	public Transform friend02_Pos00;

	// Token: 0x040014F9 RID: 5369
	public GameObject light01;

	// Token: 0x040014FA RID: 5370
	public GameObject light02;

	// Token: 0x040014FB RID: 5371
	public GameObject black;

	// Token: 0x040014FC RID: 5372
	public CanvasGroup black_canvasGroup;

	// Token: 0x040014FD RID: 5373
	[Space]
	[Header("챕터 3의 이벤트 4번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;

	// Token: 0x040014FE RID: 5374
	public GameObject BizitFacePos01;
}
