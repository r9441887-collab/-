using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class Chapter02_Event26 : EventBase
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000A784C File Offset: 0x000A5A4C
	public override void Init()
	{
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter02;
		}
		this.eventNum = 26;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000A78F0 File Offset: 0x000A5AF0
	public override void SettingCondition(int curEventDetailNum)
	{
		if (this.systemWinion == null)
		{
			this.systemWinion = GameManager.instance.gameData.systemWinion;
		}
		if (curEventDetailNum != 0)
		{
			return;
		}
		GameManager.instance.gameData.Debug_0.gameObject.SetActive(false);
		this.light.SetActive(true);
		this.light02.SetActive(false);
		DBManager.instance.dialogueController.StopSpeedDialogue();
		base.SettingEvent(true);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
		SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
		if (this.awake)
		{
			SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
			SoundManager.instance.bgmPlayer.volume = 0f;
			SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
			SoundManager.instance.bgmPlayer.pitch = 0.4f;
		}
		else
		{
			SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
			if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0f)
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0f, false);
			}
			SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
			SoundManager.instance.bgmPlayer.volume = 0f;
			SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
			SoundManager.instance.bgmPlayer.pitch = 0.4f;
		}
		int childCount = ScreenCanvas.Instance.debugFolderInteraction.console.transform.childCount;
		ScreenCanvas.Instance.debugFolderInteraction.console.SetActive(true);
		for (int i = 0; i < childCount; i++)
		{
			ScreenCanvas.Instance.debugFolderInteraction.console.transform.GetChild(i).gameObject.SetActive(true);
		}
		this.ION.SetActiveWorldWinion(false);
		this.ION.SetActiveUIWinion(false);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this.Fix.winionStatus.winionInfo.isDeath = true;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this._Debug, Winion.Debug, true, false);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Bo, Winion.Debug, true, false);
		this.Bo.transform.position = new Vector3(1f, 0f, 0f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this._Debug.transform.position = new Vector3(0f, 0f, 0f);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionAnimator.winionEmptiness = true;
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this._Debug.winionAnimator.PlayAnimation("lyingDown", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Folder_Debug);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Grid, Winion.Ion, true, false);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		int num = 4;
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
		component.roomImage.sprite = component.roomSprite[0];
		component.SwitchLightOff(false, this.Fix);
		component.SwitchLightOff(false, this._Debug);
		ScreenCanvas.Instance.debugFolderInteraction.ResetHorizontal();
		this.openPC = delegate
		{
			DOTween.Kill(this.light_canvasGroup, false);
			this.light_canvasGroup.DOFade(0f, 2f);
		};
		this.closePC = delegate
		{
			DOTween.Kill(this.light_canvasGroup, false);
			this.light_canvasGroup.DOFade(1f, 0.5f);
		};
		SystemWinion.instance.SystemWinion_Empty(true);
		this.startEvent = true;
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000A7D44 File Offset: 0x000A5F44
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
			if (curEventDetailNum != 8)
			{
				return;
			}
			if (this.startEvent)
			{
				this.checkCondition = false;
				this.curEventDetailNum_08();
			}
		}
		else if (this.startEvent)
		{
			this.checkCondition = false;
			this.curEventDetailNum_00();
			return;
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000A7DA8 File Offset: 0x000A5FA8
	public override void EndEvent()
	{
		this.endDialogue = true;
		int curEventDetailNum = this.curEventDetailNum;
		if (curEventDetailNum != 7)
		{
			if (curEventDetailNum == 10)
			{
				this.isSetting = false;
				this.checkCondition = true;
				SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, true, this.eventNum);
			}
		}
		else
		{
			this.isSetting = false;
			this.checkCondition = true;
		}
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x00017FB9 File Offset: 0x000161B9
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x00017FCF File Offset: 0x000161CF
	private IEnumerator curEventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
			yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		}
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		yield return new WaitForSeconds(1.5f);
		int childCount = ScreenCanvas.Instance.debugFolderInteraction.console.transform.childCount;
		int num;
		for (int i = 0; i < childCount; i = num + 1)
		{
			ScreenCanvas.Instance.debugFolderInteraction.console.transform.GetChild(i).gameObject.SetActive(false);
			yield return new WaitForSeconds(0.25f);
			num = i;
		}
		ScreenCanvas.Instance.debugFolderInteraction.console.SetActive(false);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("lyingDown_wakingup", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.LookAtTarget(this._Debug.gameObject);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.Debug_Fear = true;
		this._Debug.winionAnimator.PlayAnimation("LeftIdle", false);
		this._Debug.winionLookAt.LookAtTarget(this.Bo.gameObject);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		GUIUtility.systemCopyBuffer = "I Love My Friends";
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Folder_Debug, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		int num2 = 4;
		Winion winion = Winion.Debug;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num2, winion, false);
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Debug_Pos00.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("BackIdle", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		};
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(4f);
		num2 = 4;
		winion = Winion.Bo;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num2, winion, false);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos00.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionLookAt.LookAtTarget(this._Debug.gameObject);
		};
		yield return new WaitUntil(() => this.DebugArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		ScreenCanvas.Instance.RemoveUI(true);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		if (DBManager.instance.eventDialogueController.language == 0)
		{
			this.exSprite_EN.SetActive(true);
			for (int j = 0; j < this.exSpriteList_EN.Count; j++)
			{
				if (j == 0)
				{
					this.exSpriteList_EN[j].SetActive(true);
				}
				else
				{
					this.exSpriteList_EN[j].SetActive(false);
				}
			}
		}
		else
		{
			this.exSprite_KR.SetActive(true);
			for (int k = 0; k < this.exSpriteList_KR.Count; k++)
			{
				if (k == 0)
				{
					this.exSpriteList_KR[k].SetActive(true);
				}
				else
				{
					this.exSpriteList_KR[k].SetActive(false);
				}
			}
		}
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black_canvasGroup.alpha = 1f;
		this.black.SetActive(true);
		if (DBManager.instance.eventDialogueController.language == 0)
		{
			for (int l = 0; l < this.exSpriteList_EN.Count; l++)
			{
				this.exSpriteList_EN[l].SetActive(false);
				this.exSprite_EN.SetActive(false);
			}
		}
		else
		{
			for (int m = 0; m < this.exSpriteList_KR.Count; m++)
			{
				this.exSpriteList_KR[m].SetActive(false);
				this.exSprite_KR.SetActive(false);
			}
		}
		this._Debug.SetActiveWorldWinion(true);
		this._Debug.SetActiveUIWinion(false);
		ScreenCanvas.Instance.ResetUI();
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		WinionFileSelector.CanEnterPass = true;
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, true);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos01.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Bo, Winion.Ion, true, false);
			ScreenCanvas.Instance.ionFolderInteraction.SetHorizontal_Left(230);
		};
		FixPasswordPaste.canPaste = true;
		yield return new WaitUntil(() => this.BoArriveAction);
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00017FDE File Offset: 0x000161DE
	private void curEventDetailNum_08()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_08_co());
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x00017FF4 File Offset: 0x000161F4
	private IEnumerator curEventDetailNum_08_co()
	{
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		Chapter02_Event26.<>c__DisplayClass37_0 CS$<>8__locals1 = new Chapter02_Event26.<>c__DisplayClass37_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		yield return new WaitForSeconds(2f);
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.DeathSound, false, 0.6f, 1f);
		ScreenCanvas.Instance.debugFolderInteraction.ResetHorizontal();
		ScreenCanvas.Instance.debugFolderInteraction.fix_passOut.gameObject.SetActive(false);
		SoundManager.instance.Play_BGM(SoundManager.BGM.GloomyBGM, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.bgmPlayer.pitch = 0.7f;
		SoundManager.instance.BGM_ChangeVolume_Tween(15f, 1f, false);
		SingletoneBehaviour<BottomLineManager>.Instance.SetTime("01:39");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0f, false);
		SingletoneBehaviour<VaccineManager>.Instance.FixKillVideo.gameObject.SetActive(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Vaccine, false);
		int num = 0;
		Winion winion = Winion.Grid;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num, winion, false);
		num = 0;
		winion = Winion.Bo;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num, winion, false);
		this.light.SetActive(true);
		WinionFileSelector.CanEnterPass = false;
		this.Fix.SetActiveWorldWinion(true);
		this.Fix.SetActiveUIWinion(false);
		this.fixBlood.SetActive(true);
		this.Fix.winionStatus.winionInfo.isDeath = true;
		this.Fix.transform.position = this.Fix_Pos00.position;
		this.Fix.winionMovement.SetActiveMovement(false, true, false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("Death", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.transform.position = this.Grid_Pos00.position;
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionAnimator.winionEmptiness = false;
		this.Grid.winionAnimator.Grid_emptyness02 = true;
		this.Grid.winionLookAt.LookAtTarget(this.Fix.gameObject);
		this.Bo.winionAnimator.Bo_emptyness02 = true;
		this.Bo.transform.position = this.Bo_Pos02.position;
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Bo.winionLookAt.LookAtTarget(this.Fix.gameObject);
		ScreenCanvas.Instance.ionFolderInteraction.ResetHorizontal();
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.transform.position = this.Debug_Pos00.position;
		this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		CS$<>8__locals1.finish_fadeOut = false;
		yield return new WaitForSeconds(4f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos01.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Debug_Pos01.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this._Debug.winionAnimator.PlayAnimation("LeftIdle", false);
		};
		yield return new WaitUntil(() => this.DebugArriveAction);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		this.memory_List[0].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[0].SetActive(false);
		this.memory_List[1].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[1].SetActive(false);
		this.memory_obj.SetActive(false);
		this.light.SetActive(true);
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		this.memory_List[2].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[2].SetActive(false);
		this.memory_List[3].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[3].SetActive(false);
		this.memory_obj.SetActive(false);
		this.light.SetActive(true);
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		this.memory_List[4].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[4].SetActive(false);
		this.memory_List[5].SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.memory_List[5].SetActive(false);
		this.memory_obj.SetActive(false);
		this.light.SetActive(true);
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		this.memory_List[6].SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.memory_List[6].SetActive(false);
		this.memory_List[7].SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.memory_List[7].SetActive(false);
		this.memory_obj.SetActive(false);
		this.light.SetActive(true);
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		this.light02.SetActive(true);
		this.light02_canvasGroup.alpha = 0f;
		this.light02_canvasGroup.DOFade(1f, 10f);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		int i = 0;
		Time.timeScale = 1f;
		int count = 0;
		while (count < 2)
		{
			if (Time.timeScale != 3f)
			{
				Time.timeScale += 0.1f;
			}
			this.memory_List[i].SetActive(true);
			yield return new WaitForSeconds(0.4f);
			this.memory_List[i].SetActive(false);
			int num2 = i;
			i = num2 + 1;
			if (i == this.memory_List.Count)
			{
				i = 0;
				num2 = count;
				count = num2 + 1;
			}
		}
		this.light.SetActive(true);
		this.memory_obj.SetActive(false);
		Time.timeScale = 1f;
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		this.eventDialogueController.StartNextDialogue(true, 1.2f, true);
		ScreenCanvas.Instance.RemoveUI(true);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.memory_obj.SetActive(true);
		this.light.SetActive(false);
		count = 0;
		i = 0;
		float waitTime = 0.4f;
		while (i < 3)
		{
			this.memory_List[count].SetActive(true);
			yield return new WaitForSeconds(waitTime);
			this.memory_List[count].SetActive(false);
			int num2 = count;
			count = num2 + 1;
			if (count == this.memory_List.Count)
			{
				count = 0;
				num2 = i;
				i = num2 + 1;
				if (waitTime != 0.4f)
				{
					waitTime *= 0.9f;
					if (waitTime < 0.4f)
					{
						waitTime = 0.4f;
					}
				}
			}
		}
		this.memory_obj.SetActive(false);
		ScreenCanvas.Instance.ResetUI();
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(true);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.winionMovement.SetActiveMovement(false, true, false);
		this.Grid.winionMovement.SetActiveMovement(false, true, false);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(3f);
		SoundManager.instance.BGM_ChangeVolume_Tween(12f, 0f, false);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		bool finish_fadeOut = false;
		Action action = delegate
		{
			finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		yield return new WaitUntil(() => SoundManager.instance.bgmPlayer.volume == 0f);
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
		SingletoneBehaviour<WinionCalender>.Instance.NextDay("", "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x000A7E48 File Offset: 0x000A6048
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		if (eventDetailNum == 7)
		{
			if (dialogueNum == 0)
			{
				if (DBManager.instance.eventDialogueController.language == 0)
				{
					for (int i = 0; i < this.exSpriteList_EN.Count; i++)
					{
						if (i == 0)
						{
							this.exSpriteList_EN[i].SetActive(true);
						}
						else
						{
							this.exSpriteList_EN[i].SetActive(false);
						}
					}
				}
				else
				{
					for (int j = 0; j < this.exSpriteList_KR.Count; j++)
					{
						if (j == 0)
						{
							this.exSpriteList_KR[j].SetActive(true);
						}
						else
						{
							this.exSpriteList_KR[j].SetActive(false);
						}
					}
				}
			}
			if (dialogueNum == 1)
			{
				if (DBManager.instance.eventDialogueController.language == 0)
				{
					for (int k = 0; k < this.exSpriteList_EN.Count; k++)
					{
						if (k == 1)
						{
							this.exSpriteList_EN[k].SetActive(true);
						}
						else
						{
							this.exSpriteList_EN[k].SetActive(false);
						}
					}
				}
				else
				{
					for (int l = 0; l < this.exSpriteList_KR.Count; l++)
					{
						if (l == 1)
						{
							this.exSpriteList_KR[l].SetActive(true);
						}
						else
						{
							this.exSpriteList_KR[l].SetActive(false);
						}
					}
				}
			}
			if (dialogueNum == 2)
			{
				if (DBManager.instance.eventDialogueController.language == 0)
				{
					for (int m = 0; m < this.exSpriteList_EN.Count; m++)
					{
						if (m == 2)
						{
							this.exSpriteList_EN[m].SetActive(true);
						}
						else
						{
							this.exSpriteList_EN[m].SetActive(false);
						}
					}
					return;
				}
				for (int n = 0; n < this.exSpriteList_KR.Count; n++)
				{
					if (n == 2)
					{
						this.exSpriteList_KR[n].SetActive(true);
					}
					else
					{
						this.exSpriteList_KR[n].SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x04001423 RID: 5155
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public Transform Bo_Pos00;

	// Token: 0x04001424 RID: 5156
	public Transform Bo_Pos01;

	// Token: 0x04001425 RID: 5157
	public Transform Debug_Pos00;

	// Token: 0x04001426 RID: 5158
	public Transform Bo_Pos02;

	// Token: 0x04001427 RID: 5159
	public Transform Grid_Pos00;

	// Token: 0x04001428 RID: 5160
	public Transform Debug_Pos01;

	// Token: 0x04001429 RID: 5161
	public Transform Fix_Pos00;

	// Token: 0x0400142A RID: 5162
	[Space]
	[Header("챕터 2의 이벤트 11번 얼굴윈도우 위치")]
	public GameObject BoFacePos00;

	// Token: 0x0400142B RID: 5163
	public GameObject BoFacePos01;

	// Token: 0x0400142C RID: 5164
	public GameObject GridFacePos00;

	// Token: 0x0400142D RID: 5165
	public GameObject DebugFacePos00;

	// Token: 0x0400142E RID: 5166
	public GameObject DebugFacePos01;

	// Token: 0x0400142F RID: 5167
	public GameObject light;

	// Token: 0x04001430 RID: 5168
	public GameObject light02;

	// Token: 0x04001431 RID: 5169
	public CanvasGroup light02_canvasGroup;

	// Token: 0x04001432 RID: 5170
	public GameObject black;

	// Token: 0x04001433 RID: 5171
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001434 RID: 5172
	public GameObject exSprite_KR;

	// Token: 0x04001435 RID: 5173
	public List<GameObject> exSpriteList_KR;

	// Token: 0x04001436 RID: 5174
	public GameObject exSprite_EN;

	// Token: 0x04001437 RID: 5175
	public List<GameObject> exSpriteList_EN;

	// Token: 0x04001438 RID: 5176
	public GameObject memory_obj;

	// Token: 0x04001439 RID: 5177
	public CanvasGroup memory_canvasGroup;

	// Token: 0x0400143A RID: 5178
	public GameObject memory;

	// Token: 0x0400143B RID: 5179
	public List<GameObject> memory_List;

	// Token: 0x0400143C RID: 5180
	public GameObject fixBlood;

	// Token: 0x0400143D RID: 5181
	[Space]
	[Header("챕터 2의 이벤트 6번 필요 UI")]
	public GameObject Light;

	// Token: 0x0400143E RID: 5182
	public CanvasGroup light_canvasGroup;
}
