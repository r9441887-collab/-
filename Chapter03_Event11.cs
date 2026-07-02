using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000351 RID: 849
public class Chapter03_Event11 : EventBase
{
	// Token: 0x0600197E RID: 6526 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x000BA570 File Offset: 0x000B8770
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter03)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		}
		this.eventNum = 11;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x000BA61C File Offset: 0x000B881C
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			this.light.SetActive(false);
			base.SettingEvent(true);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			if (this.awake)
			{
				SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
				if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0.1f)
				{
					SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0.1f, false);
				}
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.6f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[1])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.6f;
			}
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.isBizit = true;
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().BlackGroup.alpha = 1f;
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SettingWinionRoom(true);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos00_systemWinionPos);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.GridPos00_systemWinionPos);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, true);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, true);
			int num = 7;
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			component.SystemWinionRoomColor(true, this.Grid);
			component.SystemWinionRoomColor(true, this.Bo);
			this.Bo.winionAnimator.Bo_emptyness02 = true;
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
			this.startEvent = true;
		}
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x00018C70 File Offset: 0x00016E70
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

	// Token: 0x06001983 RID: 6531 RVA: 0x000BA8D4 File Offset: 0x000B8AD4
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 14)
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

	// Token: 0x06001984 RID: 6532 RVA: 0x00018CAA File Offset: 0x00016EAA
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00018CC0 File Offset: 0x00016EC0
	private IEnumerator curEventDetailNum_00_co()
	{
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().BlackGroupFadeIn();
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0.5f, 1f).pitch = 1f;
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		DBManager.instance.dialogueController.StopSpeedDialogue();
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		SystemWinion.instance.inSystemWinionRoom = true;
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.GridArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Grid, Vector3.zero, this.GridPos01_systemWinionPos, delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		});
		yield return new WaitForSeconds(2.5f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.BoArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Bo, Vector3.zero, this.BoPos00_systemWinionPos, delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		});
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.Bo_emptyness02 = false;
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.GridArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Grid, Vector3.zero, this.BoPos01_systemWinionPos, delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		});
		yield return new WaitForSeconds(2f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.BoArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Bo, Vector3.zero, this.BoPos01_systemWinionPos, delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		});
		yield return new WaitForSeconds(4f);
		Chapter03_Event11.<>c__DisplayClass33_1 CS$<>8__locals2 = new Chapter03_Event11.<>c__DisplayClass33_1();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos00_systemWinionPos);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.GridPos00_systemWinionPos);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		CS$<>8__locals2.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0.5f, 1f).pitch = 0.15f;
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.SetAutoChackIdle_Personal = false;
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.GridArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Grid, Vector3.zero, this.GridPos01_systemWinionPos, delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		});
		yield return new WaitForSeconds(2.5f);
		this.Bo.SetAutoChackIdle_Personal = false;
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.BoArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Bo, Vector3.zero, this.BoPos00_systemWinionPos, delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		});
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos01.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 5f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 5f);
		SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.SystemWinionBGM, 5f);
		SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
		SoundManager.instance.bgmPlayer.pitch = 0.3f;
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Ion, true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		DBManager.instance.ingame_Language.systemWinionFolder_title.text = DBManager.instance.GetSettingString("시스템위니언", 0, 1, 0);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos02.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos02.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.Grid.SetAutoChackIdle_Personal = true;
		this.Bo.SetAutoChackIdle_Personal = true;
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2.5f);
		Chapter03_Event11.<>c__DisplayClass33_2 CS$<>8__locals3 = new Chapter03_Event11.<>c__DisplayClass33_2();
		this.black.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, false);
		this.black.GetComponent<Image>().color = Color.white;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(4f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Ion, false);
		CS$<>8__locals3.finish_fadeOut = false;
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		this.light.SetActive(false);
		this.light02.SetActive(true);
		this.background.SetActive(true);
		ScreenCanvas.Instance.RemoveUI(true);
		this.ION.SetActiveWorldWinion(true);
		this.ION.SetActiveUIWinion(false);
		this.Fix.SetActiveWorldWinion(true);
		this.Fix.SetActiveUIWinion(false);
		this.Fix.winionStatus.isSystemWinion = true;
		this.Fix.SetScale(2.6f);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
		this.Fix.winionMovement.SettingPos_SetTargetPos(this.SystemWinionPos00);
		this.Fix.winionAnimator.PlayAnimation("FrontIdle", false);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle_Bizit_LastBack", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		this.ION.winionLookAt.LookAtTarget(this.Fix.gameObject);
		this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(6f, 0.2f, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.SystemWinionRoom, false);
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals3 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos01.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.BGM_ChangePitch(45f, 0.45f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		Chapter03_Event11.<>c__DisplayClass33_3 CS$<>8__locals4 = new Chapter03_Event11.<>c__DisplayClass33_3();
		this.black.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, false);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals4.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals4.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0f, false);
		this.light.SetActive(true);
		this.light02.SetActive(false);
		this.background.SetActive(false);
		ScreenCanvas.Instance.ResetUI();
		this.ION.SetActiveWorldWinion(false);
		this.ION.SetActiveUIWinion(false);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this.Fix.winionStatus.isSystemWinion = false;
		this.Fix.ResetScale(2f);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Ion, true);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos01_systemWinionPos);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.BoPos00_systemWinionPos);
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals4 = null;
		FadeOutAction = null;
		bool startDialogue = false;
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos02.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos02.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.GetComponent<Image>().color = Color.black;
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this.systemWinionLine.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.systemWinionLine_Btn.onClick.RemoveAllListeners();
		this.systemWinionLine_Btn.onClick.AddListener(delegate
		{
			startDialogue = true;
			SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1f, 10f);
			SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.SystemWinionCut, false, 1f, 1f);
		});
		yield return new WaitUntil(() => startDialogue);
		this.systemWinionLine_Btn.onClick.RemoveAllListeners();
		this.systemWinionLine.GetComponent<Image>().sprite = this.systemWinionCutLine_sprite;
		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(3f);
		Chapter03_Event11.<>c__DisplayClass33_4 CS$<>8__locals5 = new Chapter03_Event11.<>c__DisplayClass33_4();
		this.black.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, false);
		this.black.GetComponent<Image>().color = Color.black;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals5.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals5.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(5f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
		CS$<>8__locals5.finish_fadeOut = false;
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		this.ION.winionStatus.isBizit = false;
		SystemWinion.instance.inSystemWinionRoom = false;
		yield return new WaitForSeconds(2f);
		SoundManager.instance.BGM_ChangePitch(5f, 0.35f);
		yield return new WaitForSeconds(5f);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Ion, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, false);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
		this.cutScene[0].SetActive(true);
		this.cutScene_CanvasGroup[0].alpha = 1f;
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, true, 1f, 1f).pitch = 0.8f;
		yield return new WaitForSeconds(5f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
		CS$<>8__locals5.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals5 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(3f);
		int num;
		for (int i = 1; i < this.cutScene.Count; i = num + 1)
		{
			Chapter03_Event11.<>c__DisplayClass33_5 CS$<>8__locals6 = new Chapter03_Event11.<>c__DisplayClass33_5();
			RectTransform component = this.cutScene[i].GetComponent<RectTransform>();
			CS$<>8__locals6.waitTween = false;
			if (i == 1)
			{
				component.localPosition = new Vector3(0f, -100f, 0f);
				CS$<>8__locals6.waitTween = true;
				ShortcutExtensions.DOLocalMove(component, new Vector3(0f, 290f, 0f), 5f, false).SetEase(Ease.OutSine).SetRelative(true)
					.OnComplete(delegate
					{
						CS$<>8__locals6.waitTween = false;
					});
			}
			if (i == 6)
			{
				component.localPosition = new Vector3(0f, -150f, 0f);
				ShortcutExtensions.DOLocalMove(component, new Vector3(0f, 300f, 0f), 5f, false).SetEase(Ease.OutSine).SetRelative(true);
			}
			if (i == 9)
			{
				SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, 2f);
			}
			if (i == 11)
			{
				component.localScale = Vector3.one * 1.1f;
				ShortcutExtensions.DOScale(component, 1.25f, 5f).SetEase(Ease.OutSine);
			}
			if (i == 14)
			{
				component.localScale = Vector3.one * 1.3f;
				ShortcutExtensions.DOScale(component, 1f, 5f).SetEase(Ease.OutSine);
			}
			if (i == 15)
			{
				SoundManager.instance.BGM_ChangePitch(20f, 0.3f);
			}
			this.cutScene[i].SetActive(true);
			this.cutScene_CanvasGroup[i].alpha = 0f;
			yield return TweenExtensions.WaitForCompletion(this.cutScene_CanvasGroup[i].DOFade(1f, 2f));
			yield return new WaitUntil(() => !CS$<>8__locals6.waitTween);
			yield return new WaitForSeconds(2f);
			CS$<>8__locals6 = null;
			num = i;
		}
		yield return new WaitForSeconds(2f);
		SystemWinion.instance.systemWinionLastLine = true;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SystemWinion.instance.systemWinionLastLine = false;
		yield return new WaitForSeconds(2f);
		Chapter03_Event11.<>c__DisplayClass33_6 CS$<>8__locals7 = new Chapter03_Event11.<>c__DisplayClass33_6();
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.black;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals7.finish_fadeOut = false;
		Action action = delegate
		{
			CS$<>8__locals7.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(8f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals7.finish_fadeOut);
		CS$<>8__locals7.finish_fadeOut = false;
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 0f, false);
		CS$<>8__locals7 = null;
		yield return new WaitForSeconds(3f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.FewDaysAfter, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x00015987 File Offset: 0x00013B87
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040015B6 RID: 5558
	[Space]
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public RectTransform GridPos00_systemWinionPos;

	// Token: 0x040015B7 RID: 5559
	public RectTransform GridPos01_systemWinionPos;

	// Token: 0x040015B8 RID: 5560
	public RectTransform GridPos02_systemWinionPos;

	// Token: 0x040015B9 RID: 5561
	public RectTransform BoPos00_systemWinionPos;

	// Token: 0x040015BA RID: 5562
	public RectTransform BoPos01_systemWinionPos;

	// Token: 0x040015BB RID: 5563
	public RectTransform BoPos02_systemWinionPos;

	// Token: 0x040015BC RID: 5564
	public Transform BizitPos00;

	// Token: 0x040015BD RID: 5565
	public Transform SystemWinionPos00;

	// Token: 0x040015BE RID: 5566
	public GameObject light;

	// Token: 0x040015BF RID: 5567
	public GameObject light02;

	// Token: 0x040015C0 RID: 5568
	public GameObject black;

	// Token: 0x040015C1 RID: 5569
	public CanvasGroup black_canvasGroup;

	// Token: 0x040015C2 RID: 5570
	public GameObject background;

	// Token: 0x040015C3 RID: 5571
	public GameObject systemWinionLine;

	// Token: 0x040015C4 RID: 5572
	public Button systemWinionLine_Btn;

	// Token: 0x040015C5 RID: 5573
	public Sprite systemWinionCutLine_sprite;

	// Token: 0x040015C6 RID: 5574
	[Space]
	[Header("챕터 3의 이벤트 2번 얼굴윈도우 위치")]
	public GameObject BoFacePos00;

	// Token: 0x040015C7 RID: 5575
	public GameObject BoFacePos01;

	// Token: 0x040015C8 RID: 5576
	public GameObject BoFacePos02;

	// Token: 0x040015C9 RID: 5577
	public GameObject GridFacePos00;

	// Token: 0x040015CA RID: 5578
	public GameObject GridFacePos01;

	// Token: 0x040015CB RID: 5579
	public GameObject GridFacePos02;

	// Token: 0x040015CC RID: 5580
	public GameObject BizitFacePos00;

	// Token: 0x040015CD RID: 5581
	public GameObject BizitFacePos01;

	// Token: 0x040015CE RID: 5582
	public List<GameObject> cutScene;

	// Token: 0x040015CF RID: 5583
	public List<CanvasGroup> cutScene_CanvasGroup;
}
