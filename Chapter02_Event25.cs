using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F4 RID: 756
public class Chapter02_Event25 : EventBase
{
	// Token: 0x060016AA RID: 5802 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000A57EC File Offset: 0x000A39EC
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter02;
		}
		this.eventNum = 25;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		this._Debug_0 = GameManager.instance.gameData.Debug_0;
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000A58AC File Offset: 0x000A3AAC
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum == 0)
		{
			this.light01.SetActive(false);
			this.light02.SetActive(false);
			DBManager.instance.dialogueController.StopSpeedDialogue();
			base.SettingEvent(true);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this._Debug_0.gameObject.SetActive(true);
			this._Debug_0.winionMovement.SettingPos_SetTargetPos(this.Debug_0_Pos00);
			if (this.awake)
			{
				SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
				if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0.1f)
				{
					SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0.1f, false);
				}
				this._Debug_0.Adjust_AlphaValue(0f, 0f);
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 0f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.7f;
			}
			else
			{
				this._Debug_0.Adjust_AlphaValue(0f, 0f);
				if (SoundManager.instance.bgmPlayer.volume != 0f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(30f, 0f, false);
				}
				SoundManager.instance.BGM_ChangePitch(5f, 0.7f);
			}
			ScreenCanvas.Instance.debugFolderInteraction.Blood01.SetActive(false);
			ScreenCanvas.Instance.debugFolderInteraction.Blood02.SetActive(false);
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Debug, Winion.System, this.fixPos05_systemWinionPos);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Fix, Winion.System, this.fixPos00_systemWinionPos);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Fix, true);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Debug, true);
			int num = 7;
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			component.SystemWinionRoomColor(true, this.Fix);
			component.SystemWinionRoomColor(true, this._Debug);
			SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, true, false);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x00017E82 File Offset: 0x00016082
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

	// Token: 0x060016AF RID: 5807 RVA: 0x0009DEE0 File Offset: 0x0009C0E0
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 19)
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

	// Token: 0x060016B0 RID: 5808 RVA: 0x00017EBC File Offset: 0x000160BC
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x00017ED2 File Offset: 0x000160D2
	private IEnumerator curEventDetailNum_00_co()
	{
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0.5f, 1f).pitch = 1f;
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		SystemWinion.instance.inSystemWinionRoom = true;
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this.DebugArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Debug, Vector3.zero, this.fixPos04_systemWinionPos, delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionBehaviour.arriveAction = null;
		});
		yield return new WaitForSeconds(1f);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.FixArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Fix, Vector3.zero, this.fixPos01_systemWinionPos, delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
			this.Fix.winionBehaviour.arriveAction = null;
		});
		yield return new WaitUntil(() => this.FixArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.FixArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Fix, Vector3.zero, this.fixPos02_systemWinionPos, delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
			this.Fix.winionBehaviour.arriveAction = null;
		});
		yield return new WaitUntil(() => this.FixArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		Chapter02_Event25.<>c__DisplayClass37_0 CS$<>8__locals1 = new Chapter02_Event25.<>c__DisplayClass37_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action action = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Debug, Winion.System, this.debugPos01_systemWinionPos);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("BackIdle", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Fix, Winion.System, this.fixPos00_systemWinionPos);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		CS$<>8__locals1.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().finishWinionAppear = true;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SystemWinionAppear();
		yield return new WaitUntil(() => !SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().finishWinionAppear);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0.5f, 1f).pitch = 0.15f;
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos00.GetComponent<RectTransform>().localPosition);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("BackIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.FixArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Fix, Vector3.zero, this.fixPos03_systemWinionPos, delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this.Fix.winionBehaviour.arriveAction = null;
		});
		yield return new WaitForSeconds(3.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos01.GetComponent<RectTransform>().localPosition);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitUntil(() => this.FixArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("BackIdle", false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this.DebugArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Debug, Vector3.zero, this.fixPos00_systemWinionPos, delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this._Debug.winionBehaviour.arriveAction = null;
		});
		yield return new WaitUntil(() => this.DebugArriveAction);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		yield return new WaitForSeconds(1f);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("BackIdle", false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.SystemWinionBGM, 5f);
		SoundManager.instance.BGM_ChangeVolume_Tween(20f, 0.7f, false);
		SoundManager.instance.bgmPlayer.pitch = 0.6f;
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 5f);
		SoundManager.instance.Set_SfxSoundVoulme(SoundManager.SfxSound.Misc_Noise_01, 0.2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WhiteNoiseSound, true, 0.6f, 0.8f);
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.BGM_ChangeVolume_Tween(5f, 0f, false);
		SoundManager.instance.Set_SfxSoundVoulme(SoundManager.SfxSound.Misc_Noise_01, 0.5f);
		Chapter02_Event25.<>c__DisplayClass37_1 CS$<>8__locals2 = new Chapter02_Event25.<>c__DisplayClass37_1();
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.WhiteNoiseSound, 2f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.DeathSound, false, 0.6f, 1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0f, 0f);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Debug, Winion.System, this.debugPos02_systemWinionPos);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Fix, Winion.System, this.fixPos04_systemWinionPos);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		CS$<>8__locals2.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.FixArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Fix, Vector3.zero, this.fixPos05_systemWinionPos, delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
			this.Fix.winionBehaviour.arriveAction = null;
		});
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos02.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		yield return new WaitForSeconds(1f);
		yield return new WaitUntil(() => this.FixArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Stop_BGM(0f);
		Chapter02_Event25.<>c__DisplayClass37_2 CS$<>8__locals3 = new Chapter02_Event25.<>c__DisplayClass37_2();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 2f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		int num = 7;
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
		component.SystemWinionRoomColor(false, this.Fix);
		component.SystemWinionRoomColor(false, this._Debug);
		SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(Winion.Debug, Winion.TrashCan);
		SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(Winion.Fix, Winion.TrashCan);
		this.black_BackGround.SetActive(true);
		ScreenCanvas.Instance.RemoveUI(true);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.SystemWinionRoom, false);
		this.light02.SetActive(false);
		this.light01.SetActive(true);
		this.Fix.transform.position = this.Fix_pos01.position;
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetTargetPosition(this.Fix_pos02.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
		};
		this._Debug.transform.position = this.Debug_pos01.position;
		this.DebugArriveAction = true;
		this._Debug.winionMovement.SetTargetPosition(this.Debug_pos02.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		};
		CS$<>8__locals3.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals3 = null;
		FadeOutAction = null;
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Fix, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Debug, false);
		SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
		SoundManager.instance.BGM_ChangePitch(0f, 1f);
		yield return new WaitUntil(() => this.FixArriveAction);
		yield return new WaitUntil(() => this.DebugArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos03.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos02.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this._Debug.winionAnimator.debug_bright = true;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		Chapter02_Event25.<>c__DisplayClass37_3 CS$<>8__locals4 = new Chapter02_Event25.<>c__DisplayClass37_3();
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.white;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals4.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals4.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		SystemWinion.instance.inSystemWinionRoom = false;
		SoundManager.instance.BGM_ChangeVolume_Tween(27f, 0.4f, false);
		this.CutScene.SetActive(true);
		CS$<>8__locals4.finish_fadeOut = false;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, false);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals4 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(1.5f);
		int num2;
		for (int i = 1; i < this.cutSceneSprites.Count; i = num2 + 1)
		{
			if (i == 2 || i == this.cutSceneSprites.Count - 1)
			{
				Chapter02_Event25.<>c__DisplayClass37_4 CS$<>8__locals5 = new Chapter02_Event25.<>c__DisplayClass37_4();
				this.black.SetActive(true);
				this.black_canvasGroup.alpha = 0f;
				CS$<>8__locals5.finish_fadeOut = false;
				FadeOutAction = delegate
				{
					CS$<>8__locals5.finish_fadeOut = true;
				};
				SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
				yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
				CS$<>8__locals5.finish_fadeOut = false;
				if (i == 2)
				{
					this.cutScene_img_fix.SetActive(true);
				}
				else
				{
					if (this.cutScene_img_fix.activeSelf)
					{
						this.cutScene_img_fix.SetActive(false);
					}
					this.cutScene_img.sprite = this.cutSceneSprites[i];
				}
				SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1f, 0f, FadeOutAction, this.black_canvasGroup);
				yield return new WaitUntil(() => CS$<>8__locals5.finish_fadeOut);
				CS$<>8__locals5.finish_fadeOut = false;
				this.black.SetActive(false);
				CS$<>8__locals5 = null;
				FadeOutAction = null;
			}
			else
			{
				if (this.cutScene_img_fix.activeSelf)
				{
					this.cutScene_img_fix.SetActive(false);
				}
				this.cutScene_img.sprite = this.cutSceneSprites[i];
			}
			if (i == this.cutSceneSprites.Count - 1)
			{
				ShortcutExtensions.DOScale(this.cutScene_img.transform, 1.2f, 10f);
			}
			yield return new WaitForSeconds(1.5f);
			this._Debug.winionAnimator.debug_bright = false;
			num2 = i;
		}
		yield return new WaitUntil(() => SoundManager.instance.bgmPlayer.volume == 0.4f);
		ScreenCanvas.Instance.ResetUI();
		yield return new WaitForSeconds(5f);
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.black;
		this.black_canvasGroup.alpha = 0f;
		SoundManager.instance.BGM_ChangeVolume_Tween(7f, 0f, false);
		yield return TweenExtensions.WaitForCompletion(this.black_canvasGroup.DOFade(1f, 7f));
		yield return new WaitForSeconds(1.5f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryComplete, "");
		yield break;
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x00016ADE File Offset: 0x00014CDE
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040013F1 RID: 5105
	[Space]
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public RectTransform fixPos00_systemWinionPos;

	// Token: 0x040013F2 RID: 5106
	public RectTransform fixPos01_systemWinionPos;

	// Token: 0x040013F3 RID: 5107
	public RectTransform fixPos02_systemWinionPos;

	// Token: 0x040013F4 RID: 5108
	public RectTransform fixPos03_systemWinionPos;

	// Token: 0x040013F5 RID: 5109
	public RectTransform fixPos04_systemWinionPos;

	// Token: 0x040013F6 RID: 5110
	public RectTransform fixPos05_systemWinionPos;

	// Token: 0x040013F7 RID: 5111
	public RectTransform debugPos01_systemWinionPos;

	// Token: 0x040013F8 RID: 5112
	public RectTransform debugPos02_systemWinionPos;

	// Token: 0x040013F9 RID: 5113
	public Transform Fix_pos01;

	// Token: 0x040013FA RID: 5114
	public Transform Fix_pos02;

	// Token: 0x040013FB RID: 5115
	public Transform Debug_pos01;

	// Token: 0x040013FC RID: 5116
	public Transform Debug_pos02;

	// Token: 0x040013FD RID: 5117
	public Transform Debug_0_Pos00;

	// Token: 0x040013FE RID: 5118
	[Space]
	[Header("챕터 2의 이벤트 25번 얼굴윈도우 위치")]
	public GameObject FixFacePos00;

	// Token: 0x040013FF RID: 5119
	public GameObject FixFacePos01;

	// Token: 0x04001400 RID: 5120
	public GameObject FixFacePos02;

	// Token: 0x04001401 RID: 5121
	public GameObject FixFacePos03;

	// Token: 0x04001402 RID: 5122
	public GameObject DebugFacePos00;

	// Token: 0x04001403 RID: 5123
	public GameObject DebugFacePos01;

	// Token: 0x04001404 RID: 5124
	public GameObject DebugFacePos02;

	// Token: 0x04001405 RID: 5125
	public GameObject light01;

	// Token: 0x04001406 RID: 5126
	public GameObject light02;

	// Token: 0x04001407 RID: 5127
	public GameObject black;

	// Token: 0x04001408 RID: 5128
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001409 RID: 5129
	public GameObject black_BackGround;

	// Token: 0x0400140A RID: 5130
	public GameObject CutScene;

	// Token: 0x0400140B RID: 5131
	public Image cutScene_img;

	// Token: 0x0400140C RID: 5132
	public GameObject cutScene_img_fix;

	// Token: 0x0400140D RID: 5133
	public List<Sprite> cutSceneSprites;

	// Token: 0x0400140E RID: 5134
	public WinionHandler _Debug_0;
}
