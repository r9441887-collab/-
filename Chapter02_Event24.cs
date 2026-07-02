using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002EC RID: 748
public class Chapter02_Event24 : EventBase
{
	// Token: 0x06001665 RID: 5733 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000A39D8 File Offset: 0x000A1BD8
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter02;
		}
		this.eventNum = 24;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		this._Debug_0 = GameManager.instance.gameData.Debug_0;
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x00017D37 File Offset: 0x00015F37
	public override void Change_Language()
	{
		base.StartCoroutine(this.Change_Language_co());
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x00017D46 File Offset: 0x00015F46
	private IEnumerator Change_Language_co()
	{
		yield return new WaitUntil(() => ChapterSetter.CompleteTranslateIngame);
		this.messages_text.text = DBManager.instance.GetSettingString("c2_e24_0", 0, 0, 1);
		yield break;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000A3A98 File Offset: 0x000A1C98
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
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this._Debug_0.gameObject.SetActive(true);
			this._Debug_0.winionMovement.SettingPos_SetTargetPos(this.Debug_0_Pos00);
			this._Debug_0.winionAnimator.PlayAnimation("BackIdle", false);
			this._Debug_0.winionAnimator.SetAnimationCanChange(false);
			SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
			if (SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity != 0.1f)
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0.1f, false);
			}
			if (this.awake)
			{
				this._Debug_0.Adjust_AlphaValue(0f, 0f);
				SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 1f;
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			else
			{
				this._Debug_0.Adjust_AlphaValue(0f, 0f);
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
					SoundManager.instance.bgmPlayer.volume = 1f;
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			this.ION.winionMovement.SettingPos_SetTargetPos(this.Ion_pos01);
			this.Bo.winionMovement.SettingPos_SetTargetPos(this.Bo_pos01);
			this.Grid.winionMovement.SettingPos_SetTargetPos(this.Grid_pos01);
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.Fix_pos01);
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.Debug_pos01);
			this.ION.winionLookAt.LookAtTarget(this.Fix.gameObject);
			this.Grid.winionLookAt.LookAtTarget(this.Fix.gameObject);
			this.Bo.winionLookAt.LookAtTarget(this.Fix.gameObject);
			this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x00017D55 File Offset: 0x00015F55
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

	// Token: 0x0600166C RID: 5740 RVA: 0x000A3D90 File Offset: 0x000A1F90
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 18)
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

	// Token: 0x0600166D RID: 5741 RVA: 0x00017D8F File Offset: 0x00015F8F
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x00017DA5 File Offset: 0x00015FA5
	private IEnumerator curEventDetailNum_00_co()
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
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.IONFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
		this.ION.winionLookAt.LookAtTarget(this._Debug.gameObject);
		this.Grid.winionLookAt.LookAtTarget(this._Debug.gameObject);
		this.Bo.winionLookAt.LookAtTarget(this._Debug.gameObject);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.Fix_pos02.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.PlayAnimation("FrontIdle", false);
		};
		yield return new WaitUntil(() => this.FixArriveAction);
		SoundManager.instance.BGM_ChangePitch(6f, 0.8f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this._Debug.winionLookAt.LookAtTarget(null);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this._Debug.winionAnimator.Debug_Angry = true;
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		yield return new WaitForSeconds(1f);
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetMoveSpeed(MoveSpeed.Fast, false);
		this._Debug.winionMovement.SetTargetPosition(this.Debug_pos02.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.SetActiveLookAt(false);
			this.ION.winionLookAt.SetActiveLookAt(false);
			this.Grid.winionLookAt.SetActiveLookAt(false);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			this._Debug.winionAnimator.Debug_Angry = false;
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this._Debug, Winion.Debug, true, false);
		};
		yield return new WaitUntil(() => this.DebugArriveAction);
		Chapter02_Event24.<>c__DisplayClass39_1 CS$<>8__locals2 = new Chapter02_Event24.<>c__DisplayClass39_1();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		int num = 4;
		Winion winion = Winion.Debug;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionOutofFolder(num, winion, false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		this.ION.SetActiveWorldWinion(false);
		this.ION.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Fix.winionMovement.SettingPos_SetTargetPos(this.Fix_pos03);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("worry", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		SystemWinion.instance.SystemWinion_Empty(false);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this.Fix.transform);
		CS$<>8__locals2.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos01.GetComponent<RectTransform>().localPosition);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		Chapter02_Event24.<>c__DisplayClass39_2 CS$<>8__locals3 = new Chapter02_Event24.<>c__DisplayClass39_2();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		this.dawnLight.SetActive(true);
		SystemWinion.instance.SystemWinion_Empty(true);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this.Fix, Winion.Debug, true, false);
		ScreenCanvas.Instance.debugFolderInteraction.light02.SetActive(true);
		int num2 = 4;
		SingletoneBehaviour<WinionFolderManager>.Instance.windows[num2].GetComponent<WinionFileSelector>().UIWinionColor_Gradient(this.Fix, "#DCA5FF", "#9D9D9D", "#6B6B6B");
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Folder_Debug);
		CS$<>8__locals3.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals3 = null;
		FadeOutAction = null;
		DBManager.instance.dialogueController.StopSpeedDialogue();
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos02.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		yield return new WaitForSeconds(2f);
		Chapter02_Event24.<>c__DisplayClass39_3 CS$<>8__locals4 = new Chapter02_Event24.<>c__DisplayClass39_3();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals4.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals4.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		ScreenCanvas.Instance.debugFolderInteraction.light02.SetActive(true);
		this.depression_Light.SetActive(true);
		SystemWinion.instance.SystemWinion_Empty(true);
		this._Debug.SetActiveWorldWinion(true);
		this._Debug.SetActiveUIWinion(false);
		SoundManager.instance.BGM_ChangeVolume_Tween(8f, 0f, false);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoFolder(this._Debug, Winion.Debug, true, false);
		this._Debug.transform.position = new Vector3(1f, 0f, 0f);
		this.Fix.transform.position = new Vector3(0f, 0f, 0f);
		int num3 = 4;
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num3].GetComponent<WinionFileSelector>();
		component.roomImage.sprite = component.roomSprite[2];
		ScreenCanvas.Instance.debugFolderInteraction.Blood01.SetActive(true);
		component.UIWinionColor_Gradient(this._Debug, "#DCA5FF", "#9D9D9D", "#6B6B6B");
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal(220f);
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal_Left(40);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("LeftIdle_Hurt", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("BackIdle", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		CS$<>8__locals4.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals4.finish_fadeOut);
		CS$<>8__locals4.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals4 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, 0.5f, false);
		SoundManager.instance.BGM_ChangePitch(2f, 0.35f);
		this.message.SetActive(true);
		SoundManager.instance.Play_BGM(SoundManager.BGM.GloomyBGM, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, 1f, false);
		this.closeBtn.gameObject.SetActive(false);
		bool _closeBtn = false;
		this.preBtn.onClick.RemoveAllListeners();
		this.preBtn.onClick.AddListener(delegate
		{
			if (this.curMessageCount != 0)
			{
				this.curMessageCount--;
				this.messages_text.text = DBManager.instance.GetSettingString("c2_e24_0", 0, this.curMessageCount, 1);
			}
		});
		this.nextBtn.onClick.RemoveAllListeners();
		UnityAction <>9__37;
		this.nextBtn.onClick.AddListener(delegate
		{
			if (this.curMessageCount != 7)
			{
				this.curMessageCount++;
				this.messages_text.text = DBManager.instance.GetSettingString("c2_e24_0", 0, this.curMessageCount, 1);
			}
			if (this.curMessageCount == 7 && !this.closeBtn.gameObject.activeSelf)
			{
				this.closeBtn.gameObject.SetActive(true);
				UnityEvent onClick = this.closeBtn.onClick;
				UnityAction unityAction;
				if ((unityAction = <>9__37) == null)
				{
					unityAction = (<>9__37 = delegate
					{
						_closeBtn = true;
						SoundManager.instance.bgmPlayer.volume = 0f;
						SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, 0f, false);
					});
				}
				onClick.AddListener(unityAction);
			}
		});
		yield return new WaitUntil(() => _closeBtn);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Fix);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Fix, this.FixFacePos02.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(true);
		ScreenCanvas.Instance.debugFolderInteraction.Blood02.SetActive(true);
		this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
		SoundManager.instance.bgmPlayer.pitch = 1f;
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Debug);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Debug, this.DebugFacePos01.GetComponent<RectTransform>().localPosition);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.BGM_ChangePitch(2f, 0.9f);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		float curPitch = SoundManager.instance.bgmPlayer.pitch;
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SoundManager.instance.BGM_ChangePitch(2f, 0.65f);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(3f, 0f, 0.5f, false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(3.5f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(5f, 0.5f, 0f, false);
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal(30f);
		ScreenCanvas.Instance.debugFolderInteraction.SetHorizontal_Left(200);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SoundManager.instance.BGM_ChangePitch(15f, curPitch);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Fix, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Debug, false);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x00016ADE File Offset: 0x00014CDE
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x040013BD RID: 5053
	[Space]
	[Header("챕터 2의 이벤트 24번의 필요 변수들")]
	public Transform Ion_pos01;

	// Token: 0x040013BE RID: 5054
	public Transform Bo_pos01;

	// Token: 0x040013BF RID: 5055
	public Transform Grid_pos01;

	// Token: 0x040013C0 RID: 5056
	public Transform Fix_pos01;

	// Token: 0x040013C1 RID: 5057
	public Transform Fix_pos02;

	// Token: 0x040013C2 RID: 5058
	public Transform Debug_pos01;

	// Token: 0x040013C3 RID: 5059
	public Transform Debug_pos02;

	// Token: 0x040013C4 RID: 5060
	public Transform Fix_pos03;

	// Token: 0x040013C5 RID: 5061
	[Space]
	[Header("챕터 2의 이벤트 13번 얼굴윈도우 위치")]
	public GameObject IONFacePos00;

	// Token: 0x040013C6 RID: 5062
	public GameObject BoFacePos00;

	// Token: 0x040013C7 RID: 5063
	public GameObject GridFacePos00;

	// Token: 0x040013C8 RID: 5064
	public GameObject FixFacePos00;

	// Token: 0x040013C9 RID: 5065
	public GameObject DebugFacePos00;

	// Token: 0x040013CA RID: 5066
	public GameObject FixFacePos01;

	// Token: 0x040013CB RID: 5067
	public GameObject FixFacePos02;

	// Token: 0x040013CC RID: 5068
	public GameObject DebugFacePos01;

	// Token: 0x040013CD RID: 5069
	public Transform Debug_0_Pos00;

	// Token: 0x040013CE RID: 5070
	public GameObject light;

	// Token: 0x040013CF RID: 5071
	public GameObject dawnLight;

	// Token: 0x040013D0 RID: 5072
	public GameObject depression_Light;

	// Token: 0x040013D1 RID: 5073
	public GameObject black;

	// Token: 0x040013D2 RID: 5074
	public CanvasGroup black_canvasGroup;

	// Token: 0x040013D3 RID: 5075
	public GameObject message;

	// Token: 0x040013D4 RID: 5076
	public Button closeBtn;

	// Token: 0x040013D5 RID: 5077
	public Button nextBtn;

	// Token: 0x040013D6 RID: 5078
	public Button preBtn;

	// Token: 0x040013D7 RID: 5079
	public TMP_Text messages_text;

	// Token: 0x040013D8 RID: 5080
	public List<GameObject> messages;

	// Token: 0x040013D9 RID: 5081
	public int curMessageCount;

	// Token: 0x040013DA RID: 5082
	public WinionHandler _Debug_0;
}
