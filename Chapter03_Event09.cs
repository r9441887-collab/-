using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000343 RID: 835
public class Chapter03_Event09 : EventBase
{
	// Token: 0x0600191B RID: 6427 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000B7028 File Offset: 0x000B5228
	public override void Init()
	{
		base.SettingWinion();
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter03)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		}
		this.eventNum = 9;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		if (ScreenCanvas.Instance.moveUI)
		{
			this.ION.SetSortOrder(false, 0);
			this._Debug.SetSortOrder(false, 0);
			this.Fix.SetSortOrder(false, 0);
			ScreenCanvas.Instance.ResetUI();
		}
		this.Fix.ResetScale(2f);
		this.Bo.ResetScale(2.2f);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000B7138 File Offset: 0x000B5338
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			this.light.SetActive(true);
			base.SettingEvent(true);
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
				SoundManager.instance.bgmPlayer.pitch = 0.6f;
			}
			else
			{
				this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.6f;
			}
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			this.light.SetActive(true);
			this.light02.SetActive(true);
			this.light03.SetActive(false);
			this.light04.SetActive(false);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SettingWinionRoom(false);
			foreach (GameObject gameObject in this.console_SystemWinionRoom)
			{
				gameObject.SetActive(false);
			}
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x00018A72 File Offset: 0x00016C72
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

	// Token: 0x06001920 RID: 6432 RVA: 0x000A19FC File Offset: 0x0009FBFC
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 13)
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

	// Token: 0x06001921 RID: 6433 RVA: 0x00018AAC File Offset: 0x00016CAC
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x00018AC2 File Offset: 0x00016CC2
	private IEnumerator curEventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		DBManager.instance.dialogueController.PermissionSpeedDialogue();
		AudioSource audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0.5f, 1f);
		audioSource.pitch = 1f;
		audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0.5f, 1f);
		audioSource.pitch = 0.15f;
		SystemWinion.instance.inSystemWinionRoom = true;
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		yield return new WaitForSeconds(2f);
		Chapter03_Event09.<>c__DisplayClass17_0 CS$<>8__locals1 = new Chapter03_Event09.<>c__DisplayClass17_0();
		this.light.SetActive(true);
		this.light_canvasGroup.alpha = 1f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		CS$<>8__locals1.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(4f, 0f, FadeOutAction, this.light_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.light.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		Chapter03_Event09.<>c__DisplayClass17_1 CS$<>8__locals2 = new Chapter03_Event09.<>c__DisplayClass17_1();
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.black;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 4f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 4f);
		ScreenCanvas.Instance.RemoveHomeUI();
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.SystemWinionRoom, false);
		SystemWinion.instance.inSystemWinionRoom = false;
		this.light02.SetActive(false);
		this.light03.SetActive(true);
		this.ION.SetActiveWorldWinion(true);
		this.ION.SetActiveUIWinion(false);
		this.ION.winionStatus.winionInfo.isDeath = false;
		this.ION.winionStatus.isBizit = true;
		this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Bizit_Sad", false);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SystemWinion.instance.SystemWinion_Empty(false);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this.ION.transform);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 1.5f);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 1f, false);
		SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Bizit", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		AudioSource audioSource_ = null;
		Chapter03_Event09.<>c__DisplayClass17_2 CS$<>8__locals3 = new Chapter03_Event09.<>c__DisplayClass17_2();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		CS$<>8__locals3.finish_fadeOut = false;
		audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0f, 1f);
		SoundManager.instance.SfxSoundTween(audioSource, 0.5f, 3f, true, false);
		audioSource.pitch = 1f;
		audioSource_ = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0f, 1f);
		SoundManager.instance.SfxSoundTween(audioSource_, 0.5f, 3f, true, false);
		audioSource_.pitch = 0.15f;
		ScreenCanvas.Instance.ResetHomeUI();
		this.light02.SetActive(true);
		this.light03.SetActive(false);
		this.ION.SetActiveWorldWinion(false);
		this.ION.SetActiveUIWinion(false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1.5f);
		SystemWinion.instance.inSystemWinionRoom = true;
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		SystemWinion.instance.SystemWinion_Empty(true);
		SoundManager.instance.BGM_ChangeVolume_Tween(5f, 0f, false);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals3 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.black_canvasGroup.alpha = 1f;
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HeartSound_Nervous, true, 1f, 1f);
		AudioSource audioSource_horror = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase01, true, 1f, 1f);
		SoundManager.instance.SfxSoundTween(audioSource_horror, 2f, 5f, false, true);
		this.black_canvasGroup.alpha = 1f;
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.6f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		this.black.SetActive(false);
		Chapter03_Event09.<>c__DisplayClass17_3 CS$<>8__locals4 = new Chapter03_Event09.<>c__DisplayClass17_3();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals4.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals4.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		SingletoneBehaviour<IconManager>.Instance.CloseAllFolder();
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		this.light04.SetActive(true);
		audioSource.volume = 0f;
		SoundManager.instance.SfxSoundTween(audioSource, -4f, 3f, false, true);
		SoundManager.instance.SfxSoundTween(audioSource_, -4f, 3f, false, true);
		yield return new WaitForSeconds(3.5f);
		SoundManager.instance.SfxSoundTween(audioSource, 0.5f, 2f, false, true);
		SoundManager.instance.SfxSoundTween(audioSource_, -0.1f, 2f, false, true);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals4 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		AudioSource audioSource_horror_ = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase01, true, 1f, 1f);
		SoundManager.instance.SfxSoundTween(audioSource_horror_, 2f, 5f, false, true);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		Chapter03_Event09.<>c__DisplayClass17_4 CS$<>8__locals5 = new Chapter03_Event09.<>c__DisplayClass17_4();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals5.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals5.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
		CS$<>8__locals5.finish_fadeOut = false;
		audioSource.volume = 0f;
		SoundManager.instance.SfxSoundTween(audioSource, -4f, 3f, false, true);
		SoundManager.instance.SfxSoundTween(audioSource_, -4f, 3f, false, true);
		yield return new WaitForSeconds(3.5f);
		SoundManager.instance.SfxSoundTween(audioSource, -0.3f, 2f, false, true);
		SoundManager.instance.SfxSoundTween(audioSource_, -0.05f, 2f, false, true);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
		CS$<>8__locals5.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals5 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(1.5f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.black_canvasGroup.alpha = 1f;
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		audioSource_horror_.Stop();
		audioSource_horror_.clip = null;
		audioSource_horror.Stop();
		audioSource_horror.clip = null;
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 0.1f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 0.1f);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WhiteNoiseSound, false, 1f, 1f);
		this.black_canvasGroup.alpha = 1f;
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.6f);
		this.black.SetActive(true);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HeartSound_Nervous, 6f);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryComplete, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x04001570 RID: 5488
	[Space]
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x04001571 RID: 5489
	public GameObject light;

	// Token: 0x04001572 RID: 5490
	public CanvasGroup light_canvasGroup;

	// Token: 0x04001573 RID: 5491
	public GameObject light02;

	// Token: 0x04001574 RID: 5492
	public GameObject light03;

	// Token: 0x04001575 RID: 5493
	public GameObject light04;

	// Token: 0x04001576 RID: 5494
	public GameObject black;

	// Token: 0x04001577 RID: 5495
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001578 RID: 5496
	[Space]
	[Header("챕터 3의 이벤트 2번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;

	// Token: 0x04001579 RID: 5497
	public List<GameObject> console_SystemWinionRoom;
}
