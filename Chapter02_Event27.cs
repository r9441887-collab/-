using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class Chapter02_Event27 : EventBase
{
	// Token: 0x06001737 RID: 5943 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x000A9F3C File Offset: 0x000A813C
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter02;
		}
		this.eventNum = 27;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000A9FE8 File Offset: 0x000A81E8
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			SoundManager.instance.BGM_ChangeVolume_Tween(4f, 1f, false);
			SoundManager.instance.BGM_ChangePitch(0.5f, -0.2f);
			GameManager.instance.gameData.Debug_0.gameObject.SetActive(false);
			this.light.SetActive(true);
			DBManager.instance.dialogueController.StopSpeedDialogue();
			base.SettingEvent(true);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this.openPC = null;
			this.closePC = null;
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 0.3f;
			}
			else
			{
				this.Grid.winionAnimator.Grid_emptyness02 = false;
				this.Grid.winionAnimator.Bo_emptyness02 = false;
				this._Debug.winionAnimator.Debug_Fear = false;
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[1])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 0.3f;
			}
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Fix.winionStatus.winionInfo.isDeath = true;
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.Fix_Pos00);
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("Death", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Bo, Winion.Ion, true, false);
			this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionLookAt.SetActiveLookAt(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Grid, Winion.Ion, true, false);
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.Debug_Pos00);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			this._Debug.winionAnimator.PlayAnimation("LeftIdle", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.systemWinionAnimator.SetHorrorEye(true);
			SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this._Debug.transform);
			this.startEvent = true;
		}
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000180A9 File Offset: 0x000162A9
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

	// Token: 0x0600173C RID: 5948 RVA: 0x000AA3BC File Offset: 0x000A85BC
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 4)
		{
			this.isSetting = false;
			this.checkCondition = true;
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.event_haveAction = true;
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.event_Action = delegate
			{
				this.EndingScene();
				SingletoneBehaviour<WinionCalender>.Instance.powerBtn.event_haveAction = false;
				SingletoneBehaviour<WinionCalender>.Instance.powerBtn.event_Action = null;
			};
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(true, false, 0);
			SingletoneBehaviour<WinionCalender>.Instance.fadeAction = delegate
			{
				DBManager.instance.dialogueData.curEvent.SettingEvent(false);
				DBManager.instance.eventDialogueController.FinishEvent();
				SingletoneBehaviour<Events>.Instance.StartEvent(this.eventNum + 1);
				SingletoneBehaviour<WinionCalender>.Instance.fadeActionEnd = true;
				SingletoneBehaviour<WinionCalender>.Instance.fadeAction = null;
			};
		}
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000180E3 File Offset: 0x000162E3
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000180F9 File Offset: 0x000162F9
	private IEnumerator curEventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
			yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		}
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.SetHorrorEye(false);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(false, null);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.Instance.Play_SfxSound(SoundManager.SfxSound.ScreenAdjustmentSound, true, 0.2f, 0.4f);
		SystemWinion.instance.systemWinionAnimator.SetLaughEye(true);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(2f, 0f, 0.5f, false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 1.5f);
		yield return new WaitForSeconds(1.5f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(true, false, false);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(true, false, false);
		yield return new WaitForSeconds(0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(true, false, false);
		yield return new WaitForSeconds(0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1.5f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(2f, 0.5f, 0f, false);
		yield return new WaitForSeconds(1f);
		SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 0f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.BGM_ChangeVolume_Tween(15f, 1f, false);
		SoundManager.instance.bgmPlayer.pitch = 0.2f;
		SoundManager.Instance.Stop_SfxSound(SoundManager.SfxSound.ScreenAdjustmentSound, 0.3f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionAnimator.SetLaughEye(false);
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this._Debug.transform);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SystemWinion.instance.systemWinionAnimator.SetHorrorEye(true);
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this._Debug.transform);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SystemWinion.instance.SystemWinion_Empty(true);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x00018108 File Offset: 0x00016308
	public void EndingScene()
	{
		base.StartCoroutine(this.EndingScene_co());
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x00018117 File Offset: 0x00016317
	private IEnumerator EndingScene_co()
	{
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		SoundManager.instance.BGM_ChangeVolume_Tween(5f, 0f, false);
		Chapter02_Event27.<>c__DisplayClass31_0 CS$<>8__locals1 = new Chapter02_Event27.<>c__DisplayClass31_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Ion, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Bo, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Grid, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Fix, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Debug, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.TranshCan, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.MyPC, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.MailBox, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Vaccine, false);
			SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.BatteryCenter, false);
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		ScreenCanvas.Instance.RemoveUI(true);
		this.fixBlood.SetActive(false);
		this.black_BackGround.SetActive(true);
		this._Debug.transform.position = this.Debug_Pos01.position;
		SingletoneBehaviour<IconManager>.Instance.CloseAllFolder();
		yield return new WaitForSeconds(4f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.SetAutoChackIdle_Personal = false;
		this._Debug.winionMovement.SetTargetPosition(this.Debug_Pos02.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("FrontIdle", false);
		};
		yield return new WaitForSeconds(1f);
		SoundManager.instance.BGM_ChangeVolume_Tween(25f, 1f, false);
		SoundManager.instance.BGM_ChangePitch(0f, 0.5f);
		yield return new WaitUntil(() => this.DebugArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Debug_Pos03.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("FrontIdle", false);
		};
		yield return new WaitUntil(() => this.DebugArriveAction);
		this._Debug.winionAnimator.spriteRenderer.sortingLayerName = "Second";
		this._Debug.winionDialogue_upUI = true;
		SingletoneBehaviour<VaccineManager>.Instance.VaccineIcon.gameObject.GetComponent<IconDoubleClick>().targetWindow.GetComponent<UIWindow>().CantClose = true;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<RectTransform>().localPosition = Vector3.zero;
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Vaccine);
		SingletoneBehaviour<VaccineManager>.Instance.VaccineIcon.gameObject.GetComponent<IconDoubleClick>().targetWindow.GetComponent<UIWindow>().canMove = false;
		SingletoneBehaviour<VaccineManager>.Instance.SettingDebug();
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos02.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		base.BlockDialogue(false);
		this._Debug.Debug_HaveSomethingToSay_temp.SetActive(true);
		this._Debug.winionMovement.SetActiveMovement(false, true, true);
		this.MainCanvas.enabled = false;
		this.MainCanvas.isOnMouseOverUI = false;
		this.Event27Canvas.enabled = false;
		this.Event27Canvas.isOnMouseOverUI = false;
		ScreenCanvas.Instance.talkAction = delegate
		{
			this._Debug.Debug_HaveSomethingToSay_temp.SetActive(false);
			this.dialogueCount++;
		};
		DBManager.instance.dialogueController.SettingTalkEmotion_Action = delegate
		{
			this._Debug.Debug_HaveSomethingToSay_temp.SetActive(true);
		};
		base.StartCoroutine(this.DebugAnimation());
		yield return new WaitUntil(() => SingletoneBehaviour<VaccineManager>.Instance.KillDebug);
		DBManager.instance.dialogueData.NoBacklogOpen = true;
		yield return null;
		this._Debug.Debug_HaveSomethingToSay_temp.SetActive(false);
		base.BlockDialogue(true);
		this.stopAnimationCo = true;
		ScreenCanvas.Instance.talkAction = null;
		this.MainCanvas.enabled = true;
		this.Event27Canvas.enabled = true;
		yield return new WaitForSeconds(1f);
		AudioSource audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.oozing_Sound, true, 0.7f, 1f);
		audioSource.volume = 0f;
		SoundManager.instance.SfxSoundTween(audioSource, 0.6f, 10f, true, false);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(true, 2f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(true, 2f, true);
		if (this.leftEar && this.rightEar)
		{
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("FrontIdle_LastSmile_RightEar", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		}
		else if (this.leftEar && !this.rightEar)
		{
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("FrontIdle_LastSmile_LeftEar", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		}
		else
		{
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("FrontIdle_LastSmile", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		}
		yield return new WaitForSeconds(12f);
		Chapter02_Event27.<>c__DisplayClass31_1 CS$<>8__locals2 = new Chapter02_Event27.<>c__DisplayClass31_1();
		this.black_top.SetActive(true);
		this.black_top_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action action = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(23f, 0f, action, this.black_top_canvasGroup, 1f);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		ScreenCanvas.Instance.ResetUI();
		ScreenCanvas.Instance.ResetVaccinePos();
		SingletoneBehaviour<VaccineManager>.Instance.ResetDebugSetting();
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Vaccine, false);
		this._Debug.winionDialogue_upUI = false;
		SingletoneBehaviour<VaccineManager>.Instance.VaccineIcon.gameObject.GetComponent<IconDoubleClick>().targetWindow.GetComponent<UIWindow>().canMove = true;
		this._Debug.winionAnimator.spriteRenderer.sortingLayerName = "Winion";
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.oozing_Sound, 5f);
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 0f, false);
		yield return new WaitForSeconds(3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BodyExploding_01, false, 1f, 1f);
		yield return new WaitForSeconds(1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BodyExploding_02, false, 1f, 1f);
		yield return new WaitForSeconds(7f);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.TranshCan, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.MailBox, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.MyPC, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Vaccine, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.BatteryCenter, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Ion, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Bo, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Grid, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Fix, true);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Debug, true);
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.DeathSound, false, 0.6f, 1f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.NextDay, "");
		SingletoneBehaviour<VaccineManager>.Instance.scanButton.interactable = true;
		base.BlockDialogue(false);
		CS$<>8__locals2 = null;
		yield break;
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00018126 File Offset: 0x00016326
	private IEnumerator DebugAnimation()
	{
		yield return null;
		yield return new WaitUntil(() => this.dialogueCount == 4 || this.stopAnimationCo);
		if (!this.stopAnimationCo)
		{
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("LeftEar_TearReady", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
			yield return new WaitUntil(() => this.dialogueCount == 7 || this.stopAnimationCo);
			if (!this.stopAnimationCo)
			{
				this.strangeSystemWinion02.SetActive(true);
				this.strangeSystemWinion02_CanvasGroup.alpha = 0f;
				this.leftEar = true;
				this._Debug.winionAnimator.SetAnimationCanChange(true);
				this._Debug.winionAnimator.PlayAnimation("LeftEar_Tear", false);
				SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.TearSound, false, 1f, 1f);
				this._Debug.winionAnimator.EndFrameAction = delegate
				{
					this._Debug.winionAnimator.SetAnimationCanChange(true);
					this._Debug.winionAnimator.PlayAnimation("FrontIdle_LeftEar", false);
					this._Debug.winionAnimator.SetAnimationCanChange(false);
					this._Debug.winionAnimator.EndFrameAction = null;
					this.strangeSystemWinion02.SetActive(false);
					this.strangeSystemWinion02_CanvasGroup.alpha = 0f;
					this.LeftBlood.SetActive(true);
				};
				yield return new WaitUntil(() => this.dialogueCount == 9 || this.stopAnimationCo);
				if (!this.stopAnimationCo)
				{
					this._Debug.winionAnimator.SetAnimationCanChange(true);
					this._Debug.winionAnimator.PlayAnimation("RightEar_TearReady", false);
					this._Debug.winionAnimator.SetAnimationCanChange(false);
					yield return new WaitUntil(() => this.dialogueCount == 12 || this.stopAnimationCo);
					if (!this.stopAnimationCo)
					{
						this.strangeSystemWinion02.SetActive(true);
						this.strangeSystemWinion02_CanvasGroup.alpha = 0f;
						this.rightEar = true;
						this._Debug.winionAnimator.SetAnimationCanChange(true);
						this._Debug.winionAnimator.PlayAnimation("RightEar_Tear", false);
						SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.TearSound, false, 1f, 1f);
						this._Debug.winionAnimator.EndFrameAction = delegate
						{
							this._Debug.winionAnimator.SetAnimationCanChange(true);
							this._Debug.winionAnimator.PlayAnimation("FrontIdle_Blood", false);
							this._Debug.winionAnimator.SetAnimationCanChange(false);
							this._Debug.winionAnimator.EndFrameAction = null;
							this.strangeSystemWinion02.SetActive(false);
							this.strangeSystemWinion02_CanvasGroup.alpha = 0f;
							this.RightBlood.SetActive(true);
						};
					}
					else
					{
						this._Debug.winionAnimator.SetAnimationCanChange(true);
						this._Debug.winionAnimator.PlayAnimation("FrontIdle_LeftEar", false);
						this._Debug.winionAnimator.SetAnimationCanChange(false);
					}
				}
			}
			else
			{
				this._Debug.winionAnimator.SetAnimationCanChange(true);
				this._Debug.winionAnimator.PlayAnimation("FrontIdle", false);
				this._Debug.winionAnimator.SetAnimationCanChange(false);
			}
		}
		yield break;
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x00018135 File Offset: 0x00016335
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x04001453 RID: 5203
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public Transform Debug_Pos00;

	// Token: 0x04001454 RID: 5204
	public Transform Debug_Pos01;

	// Token: 0x04001455 RID: 5205
	public Transform Debug_Pos02;

	// Token: 0x04001456 RID: 5206
	public Transform Debug_Pos03;

	// Token: 0x04001457 RID: 5207
	public Transform Fix_Pos00;

	// Token: 0x04001458 RID: 5208
	[Space]
	[Header("챕터 2의 이벤트 11번 얼굴윈도우 위치")]
	public GameObject DebugFacePos00;

	// Token: 0x04001459 RID: 5209
	public GameObject DebugFacePos01;

	// Token: 0x0400145A RID: 5210
	public GameObject DebugFacePos02;

	// Token: 0x0400145B RID: 5211
	public GameObject light;

	// Token: 0x0400145C RID: 5212
	public GameObject black;

	// Token: 0x0400145D RID: 5213
	public CanvasGroup black_canvasGroup;

	// Token: 0x0400145E RID: 5214
	public GameObject black_top;

	// Token: 0x0400145F RID: 5215
	public CanvasGroup black_top_canvasGroup;

	// Token: 0x04001460 RID: 5216
	public GameObject fixBlood;

	// Token: 0x04001461 RID: 5217
	public GameObject black_BackGround;

	// Token: 0x04001462 RID: 5218
	public OnMouseOverUI MainCanvas;

	// Token: 0x04001463 RID: 5219
	public OnMouseOverUI Event27Canvas;

	// Token: 0x04001464 RID: 5220
	public GameObject LeftBlood;

	// Token: 0x04001465 RID: 5221
	public GameObject RightBlood;

	// Token: 0x04001466 RID: 5222
	public GameObject strangeSystemWinion01;

	// Token: 0x04001467 RID: 5223
	public GameObject strangeSystemWinion02;

	// Token: 0x04001468 RID: 5224
	public CanvasGroup strangeSystemWinion02_CanvasGroup;

	// Token: 0x04001469 RID: 5225
	private int dialogueCount;

	// Token: 0x0400146A RID: 5226
	private bool stopAnimationCo;

	// Token: 0x0400146B RID: 5227
	private bool leftEar;

	// Token: 0x0400146C RID: 5228
	private bool rightEar;
}
