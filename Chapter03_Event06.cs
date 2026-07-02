using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class Chapter03_Event06 : EventBase
{
	// Token: 0x06001899 RID: 6297 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000B3408 File Offset: 0x000B1608
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 6;
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

	// Token: 0x0600189B RID: 6299 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000B34B8 File Offset: 0x000B16B8
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
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 0.7f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1f);
				SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 0.7f;
			}
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
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

	// Token: 0x0600189D RID: 6301 RVA: 0x00018884 File Offset: 0x00016A84
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

	// Token: 0x0600189E RID: 6302 RVA: 0x000B0F1C File Offset: 0x000AF11C
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

	// Token: 0x0600189F RID: 6303 RVA: 0x000188BE File Offset: 0x00016ABE
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000188D4 File Offset: 0x00016AD4
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
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		Chapter03_Event06.<>c__DisplayClass22_0 CS$<>8__locals1 = new Chapter03_Event06.<>c__DisplayClass22_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1.5f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.light.SetActive(true);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos01);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(2f);
		this.ION.SetAutoChackIdle_Personal = false;
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
			this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.ION.winionAnimator.SetAnimationCanChange(false);
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1.5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		yield return new WaitUntil(() => this.IONArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos01.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.SetAutoChackIdle_Personal = true;
		Chapter03_Event06.<>c__DisplayClass22_1 CS$<>8__locals2 = new Chapter03_Event06.<>c__DisplayClass22_1();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1.5f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.Fix.SetActiveWorldWinion(true);
		this.Fix.SetActiveUIWinion(false);
		this.Fix.winionMovement.SettingPos_SetTargetPos(this.friend01_Pos01);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		this._Debug.SetActiveWorldWinion(true);
		this._Debug.SetActiveUIWinion(false);
		this._Debug.winionMovement.SettingPos_SetTargetPos(this.friend02_Pos01);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionLookAt.LookAtTarget(this.ION.gameObject);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos03);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.oozing_Sound, true, 0.7f, 1f).pitch = 0.4f;
		this.light.SetActive(false);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1.5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.IONArriveAction = false;
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.BizitPos04.position, true);
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
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos02.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SystemWinion.instance.SystemWinion_Empty(false);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this.ION.transform);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 3f);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Fix.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("LeftIdle", false);
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.oozing_Sound, 3f);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x0400151B RID: 5403
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x0400151C RID: 5404
	public Transform BizitPos01;

	// Token: 0x0400151D RID: 5405
	public Transform BizitPos02;

	// Token: 0x0400151E RID: 5406
	public Transform BizitPos03;

	// Token: 0x0400151F RID: 5407
	public Transform BizitPos04;

	// Token: 0x04001520 RID: 5408
	public Transform friend01_Pos00;

	// Token: 0x04001521 RID: 5409
	public Transform friend01_Pos01;

	// Token: 0x04001522 RID: 5410
	public Transform friend02_Pos00;

	// Token: 0x04001523 RID: 5411
	public Transform friend02_Pos01;

	// Token: 0x04001524 RID: 5412
	public GameObject light;

	// Token: 0x04001525 RID: 5413
	public GameObject black;

	// Token: 0x04001526 RID: 5414
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001527 RID: 5415
	[Space]
	[Header("챕터 3의 이벤트 6번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;

	// Token: 0x04001528 RID: 5416
	public GameObject BizitFacePos01;

	// Token: 0x04001529 RID: 5417
	public GameObject BizitFacePos02;
}
