using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000309 RID: 777
public class Chapter03_Event00 : EventBase
{
	// Token: 0x06001783 RID: 6019 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000ABDE8 File Offset: 0x000A9FE8
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 0;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x000ABE80 File Offset: 0x000AA080
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			base.SettingEvent(true);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.6f, false);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			if (this.awake)
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.GloomyBGM, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 0.8f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[6])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.GloomyBGM, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 0.8f;
			}
			DBManager.instance.dialogueController.StopSpeedDialogue();
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Fix.winionMouseEvent.canMouseEnter = false;
			this._Debug.winionMouseEvent.canMouseEnter = false;
			this.Fix.winionStatus.winionInfo.isDeath = true;
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.Fix_Pos00);
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("Death", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
			this._Debug.winionStatus.winionInfo.isDeath = true;
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.Debug_Pos00);
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("Death", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionMovement.SettingPos_SetTargetPos(this.Grid_Pos00);
			this.Bo.winionMovement.SettingPos_SetTargetPos(this.Bo_Pos00);
			this.Grid.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this.Bo.winionLookAt.LookAtTarget(this._Debug.gameObject);
			base.SettingHaveEventWinion(true, this.Grid);
			base.SettingHaveEventWinion(true, this.Bo);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.Bo_emptyness02 = true;
			this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("FrontIdle_WhitenessDownEye", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionAnimator.Grid_emptyness02 = true;
			SystemWinion.instance.SystemWinion_Empty(true);
			return;
		}
		if (curEventDetailNum == 4)
		{
			if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[1])
			{
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				SoundManager.instance.bgmPlayer.pitch = 0.45f;
			}
			SingletoneBehaviour<BottomLineManager>.Instance.SetTime("15:51");
			base.SettingHaveEventWinion(true, this.Grid);
			base.SettingHaveEventWinion(true, this.Bo);
			return;
		}
		if (curEventDetailNum != 6)
		{
			return;
		}
		base.SettingHaveEventWinion(true, this.Grid);
		base.SettingHaveEventWinion(true, this.Bo);
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
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
				if (curEventDetailNum != 6)
				{
					return;
				}
				if (this.startEvent)
				{
					this.checkCondition = false;
					base.SettingHaveEventWinion(false, this.Grid);
					base.SettingHaveEventWinion(false, this.Bo);
					this.curEventDetailNum_06();
				}
			}
			else if (this.startEvent)
			{
				this.checkCondition = false;
				base.SettingHaveEventWinion(false, this.Grid);
				base.SettingHaveEventWinion(false, this.Bo);
				this.curEventDetailNum_04();
				return;
			}
		}
		else if (this.startEvent)
		{
			this.checkCondition = false;
			base.SettingHaveEventWinion(false, this.Grid);
			base.SettingHaveEventWinion(false, this.Bo);
			this.curEventDetailNum_00();
			return;
		}
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000AC3C4 File Offset: 0x000AA5C4
	public override void EndEvent()
	{
		this.endDialogue = true;
		int curEventDetailNum = this.curEventDetailNum;
		if (curEventDetailNum != 3)
		{
			if (curEventDetailNum != 5)
			{
				if (curEventDetailNum == 14)
				{
					this.isSetting = false;
					this.checkCondition = true;
					SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(true, true, this.eventNum);
				}
			}
			else
			{
				this.isSetting = false;
				this.checkCondition = true;
			}
		}
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x0001825B File Offset: 0x0001645B
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x00018271 File Offset: 0x00016471
	private IEnumerator EventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		DBManager.instance.dialogueData.NoBacklogOpen = false;
		yield return null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 0f, false);
		Chapter03_Event00.<>c__DisplayClass26_0 CS$<>8__locals1 = new Chapter03_Event00.<>c__DisplayClass26_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		CS$<>8__locals1.finish_fadeOut = false;
		this.fixBlood01.SetActive(true);
		ScreenCanvas.Instance.debugFolderInteraction.death_Winion.SetActive(true);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Grid, Winion.Debug, true, false);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Bo, Winion.Debug, true, false);
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal(175f);
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal_Left(125);
		int num = 4;
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
		component.UIWinionColorLightOff(true, this.Grid, "#97A4B4", true);
		component.UIWinionColorLightOff(true, this.Bo, "#97A4B4", true);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.Grid_emptyness02 = false;
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		this.Bo.winionAnimator.Bo_emptyness02 = false;
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(2f);
		this.AfterText.GetComponent<TextMeshProUGUI>().text = DayInfo.AfterHours;
		yield return TweenExtensions.WaitForCompletion(this.AfterText.DOFade(1f, 1.5f));
		yield return new WaitForSeconds(1f);
		yield return TweenExtensions.WaitForCompletion(this.AfterText.DOFade(0f, 1.5f));
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		yield return new WaitUntil(() => SoundManager.instance.bgmPlayer.volume == 0f);
		this.isSetting = false;
		this.checkCondition = true;
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x00018280 File Offset: 0x00016480
	private void curEventDetailNum_04()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_04_co());
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x00018296 File Offset: 0x00016496
	private IEnumerator EventDetailNum_04_co()
	{
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("Sad", false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos01.GetComponent<RectTransform>().localPosition);
		this.Grid.winionAnimator.Grid_emptyness02 = true;
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		yield return new WaitForSeconds(1f);
		int num = 4;
		Winion winion = Winion.Grid;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num, winion, false);
		this.Grid.winionAnimator.Grid_emptyness02 = false;
		num = 4;
		winion = Winion.Bo;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num, winion, false);
		ScreenCanvas.Instance.debugFolderInteraction.ResetHorizontal();
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
		component.UIWinionColorLightOff(false, this.Grid, "", true);
		component.UIWinionColorLightOff(false, this.Bo, "", true);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.GridArriveAction = false;
		this.Grid.winionMovement.SetActiveMovement(true, true, false);
		this.Grid.winionMovement.SetTargetPosition(this.Grid_Pos01.position, true);
		this.Grid.winionBehaviour.arriveAction = delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionBehaviour.moveRandomPos = false;
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("FrontIdle_DownEye", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
		};
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos01.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("Sad", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		};
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000182A5 File Offset: 0x000164A5
	private void curEventDetailNum_06()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_06_co());
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000182BB File Offset: 0x000164BB
	private IEnumerator EventDetailNum_06_co()
	{
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos02.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos01.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.1f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.3f, 1.5f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.2f, 1.2f);
		yield return new WaitForSeconds(0.4f);
		float num = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		ScreenCanvas.Instance.ShakeUI(num);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.2f, 1.2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.Instance.BGM_ChangeVolume_Tween(2f, 0f, false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.3f, 1.5f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.4f, 1.3f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.3f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.4f);
		float num2 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num2, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		ScreenCanvas.Instance.ShakeUI(num2);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.2f);
		this.Grid.winionAnimator.Grid_emptyness02 = true;
		this.Bo.winionAnimator.Bo_emptyness02 = true;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		this.brokenScreen.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.3f, false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.5f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.7f, 1.2f);
		yield return new WaitForSeconds(0.5f);
		float num3 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num3, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition2 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num3, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition2;
		});
		ScreenCanvas.Instance.ShakeUI(num3);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(true, 1.1f, false);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		this.brokenScreen.GetComponent<Image>().sprite = this.screenImage[0];
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 0.3f, 0.5f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.45f, false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.3f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.5f);
		float num4 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num4, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition3 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num4, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition3;
		});
		ScreenCanvas.Instance.ShakeUI(num4);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(1f, 1f, 0.5f, true);
		this.black.SetActive(false);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		this.eventDialogueController.StartNextDialogue(true, 1.7f, false);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.5f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		float num5 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num5, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition4 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num5, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition4;
		});
		ScreenCanvas.Instance.ShakeUI(num5);
		yield return new WaitForSeconds(0.4f);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.7f, 1.2f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.5f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.4f);
		float num6 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num6, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition5 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num6, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition5;
		});
		ScreenCanvas.Instance.ShakeUI(num6);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		this.brokenScreen.GetComponent<Image>().sprite = this.screenImage[1];
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.5f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.4f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		float num7 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num7, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition6 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num7, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition6;
		});
		ScreenCanvas.Instance.ShakeUI(num7);
		this.black.SetActive(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		this.eventDialogueController.StartNextDialogue(true, 1.5f, false);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.7f, 1.2f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.5f);
		yield return new WaitForSeconds(0.3f);
		float num8 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num8, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition7 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num8, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition7;
		});
		ScreenCanvas.Instance.ShakeUI(num8);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.4f, 1f, 0.5f, true);
		yield return new WaitForSeconds(0.6f);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.5f);
		yield return new WaitForSeconds(0.3f);
		float num9 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num9, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition8 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num9, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition8;
		});
		ScreenCanvas.Instance.ShakeUI(num9);
		yield return new WaitForSeconds(0.6f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.eventDialogueController.StartNextDialogue(true, 1.4f, false);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.GridArriveAction = false;
		this.Grid.winionMovement.SetActiveMovement(true, true, false);
		this.Grid.winionMovement.SetMoveSpeed(MoveSpeed.Fast, false);
		this.Grid.winionMovement.SetTargetPosition(this.Grid_Pos02.position, true);
		this.Grid.winionBehaviour.arriveAction = delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionBehaviour.moveRandomPos = false;
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Grid.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
		};
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetMoveSpeed(MoveSpeed.Fast, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos02.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			this.Bo.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
		};
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		this.brokenScreen.GetComponent<Image>().sprite = this.screenImage[2];
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 0.8f, 0.5f, true);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		float num10 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num10, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition9 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num10, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition9;
		});
		ScreenCanvas.Instance.ShakeUI(num10);
		this.black.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.5f);
		yield return new WaitForSeconds(0.2f);
		float num11 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num11, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition10 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num11, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition10;
		});
		ScreenCanvas.Instance.ShakeUI(num11);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.5f);
		yield return new WaitForSeconds(0.2f);
		float num12 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num12, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition11 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num12, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition11;
		});
		ScreenCanvas.Instance.ShakeUI(num12);
		this.black.SetActive(false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.4f, 1f, 0.5f, true);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.3f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.5f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.5f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.5f);
		float num13 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num13, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition12 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num13, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition12;
		});
		ScreenCanvas.Instance.ShakeUI(num13);
		this.black.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.3f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.5f);
		float num14 = 0.2f;
		ShortcutExtensions.DOShakePosition(Camera.main, num14, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition13 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num14, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition13;
		});
		ScreenCanvas.Instance.ShakeUI(num14);
		this.black.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		this.brokenScreen.GetComponent<Image>().sprite = this.screenImage[3];
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 1f, 0.5f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.5f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.8f, 1.3f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.5f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.5f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.6f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		float num15 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num15, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition14 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num15, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition14;
		});
		ScreenCanvas.Instance.ShakeUI(num15);
		this.black.SetActive(false);
		yield return new WaitUntil(() => this.GridArriveAction);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.7f, 1.2f);
		yield return new WaitForSeconds(0.3f);
		float num16 = 0.4f;
		ShortcutExtensions.DOShakePosition(Camera.main, num16, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition15 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num16, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition15;
		});
		ScreenCanvas.Instance.ShakeUI(num16);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.4f, 1f, 0.5f, true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.4f);
		float num17 = 0.4f;
		ShortcutExtensions.DOShakePosition(Camera.main, num17, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition16 = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num17, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition16;
		});
		ScreenCanvas.Instance.ShakeUI(num17);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 0.9f, 1.2f);
		yield return new WaitForSeconds(0.4f);
		float num18 = 0.3f;
		ShortcutExtensions.DOShakePosition(Camera.main, num18, Vector3.one * 0.05f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		Vector3 originalPosition = this.brokenScreen.GetComponent<RectTransform>().anchoredPosition;
		this.brokenScreen.GetComponent<RectTransform>().DOShakeAnchorPos(num18, 50f, 50, 90f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
		{
			this.brokenScreen.GetComponent<RectTransform>().anchoredPosition = originalPosition;
		});
		ScreenCanvas.Instance.ShakeUI(num18);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 1f, 1.2f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound03, false, 1f, 1.5f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 0.5f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		yield return new WaitForSeconds(5f);
		this.Fix.winionMouseEvent.canMouseEnter = true;
		this._Debug.winionMouseEvent.canMouseEnter = true;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(2f, 0f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(2f, 0f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(2f, 0f, 0.5f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, true);
		MyPcWindowResolution.chapter = HorrorChapter.Chapter3;
		Cursor.lockState = 1;
		SceneLoader.LoadScene("HorrorSceneForLoading", false, false);
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x04001480 RID: 5248
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform Grid_Pos00;

	// Token: 0x04001481 RID: 5249
	public Transform Grid_Pos01;

	// Token: 0x04001482 RID: 5250
	public Transform Grid_Pos02;

	// Token: 0x04001483 RID: 5251
	public Transform Bo_Pos00;

	// Token: 0x04001484 RID: 5252
	public Transform Bo_Pos01;

	// Token: 0x04001485 RID: 5253
	public Transform Bo_Pos02;

	// Token: 0x04001486 RID: 5254
	public Transform Fix_Pos00;

	// Token: 0x04001487 RID: 5255
	public Transform Debug_Pos00;

	// Token: 0x04001488 RID: 5256
	[Space]
	[Header("챕터 3의이벤트 0번의 필요 변수들")]
	public GameObject GridFacePos00;

	// Token: 0x04001489 RID: 5257
	public GameObject GridFacePos01;

	// Token: 0x0400148A RID: 5258
	public GameObject GridFacePos02;

	// Token: 0x0400148B RID: 5259
	public GameObject BoFacePos00;

	// Token: 0x0400148C RID: 5260
	public GameObject BoFacePos01;

	// Token: 0x0400148D RID: 5261
	public GameObject black;

	// Token: 0x0400148E RID: 5262
	public CanvasGroup black_canvasGroup;

	// Token: 0x0400148F RID: 5263
	public GameObject fixBlood01;

	// Token: 0x04001490 RID: 5264
	public GameObject brokenScreen;

	// Token: 0x04001491 RID: 5265
	public List<Sprite> screenImage;

	// Token: 0x04001492 RID: 5266
	public CanvasGroup AfterText;
}
