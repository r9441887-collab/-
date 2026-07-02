using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class Chapter03_Event12 : EventBase
{
	// Token: 0x060019C6 RID: 6598 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x000BC920 File Offset: 0x000BAB20
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 12;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000BC9B8 File Offset: 0x000BABB8
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
		if (this.isMoving)
		{
			Vector3 vector = this.background_Rect.anchoredPosition;
			if (vector.y > this.targetYPosition)
			{
				this.background_Rect.anchoredPosition += new Vector2(0f, -this.speed * Time.deltaTime);
				return;
			}
			this.isMoving = false;
			this.background_Rect.anchoredPosition = new Vector2(vector.x, this.targetYPosition);
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000BCA4C File Offset: 0x000BAC4C
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
			SystemWinion.instance.SystemWinion_Empty(true);
			SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.2f, 0f, false);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0f, false);
			if (this.awake)
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0f, 0f, true);
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 1f;
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			else
			{
				SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0f, 0f, true);
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			this.Bo.winionMovement.SettingPos_SetTargetPos(this.Bo_Pos00);
			this.Grid.winionMovement.SettingPos_SetTargetPos(this.Grid_Pos00);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.Grid.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos01);
			this.ION.winionStatus.isRescueTeam = true;
			this.ION.SetScale(1.8f);
			this.Fix.SetActiveWorldWinion(true);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos01_0);
			this.Fix.winionStatus.isRescueTeam = true;
			this._Debug.SetActiveWorldWinion(true);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos01_1);
			this._Debug.winionStatus.isRescueTeam = true;
			base.SettingHaveEventWinion(true, this.Bo);
			base.SettingHaveEventWinion(true, this.Grid);
		}
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x00018DBF File Offset: 0x00016FBF
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

	// Token: 0x060019CB RID: 6603 RVA: 0x000BCD64 File Offset: 0x000BAF64
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 11)
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

	// Token: 0x060019CC RID: 6604 RVA: 0x00018DF9 File Offset: 0x00016FF9
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x00018E0F File Offset: 0x0001700F
	private IEnumerator EventDetailNum_00_co()
	{
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		DBManager.instance.dialogueController.StopSpeedDialogue();
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Grid.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		yield return new WaitForSeconds(1.5f);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.Winion_Pos00.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
		};
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.Winion_Pos00_0.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
		};
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Winion_Pos00_1.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitForSeconds(4f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitUntil(() => this.IONArriveAction);
		this.Bo.winionLookAt.LookAtTarget(this.ION.gameObject);
		this.Grid.winionLookAt.LookAtTarget(this.ION.gameObject);
		yield return new WaitForSeconds(1f);
		AudioSource walkie = SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.WalkieTalkie, false, 1f, 1f);
		yield return new WaitUntil(() => !walkie.isPlaying);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.Winion_Pos01.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitForSeconds(2f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		yield return new WaitForSeconds(2f);
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.Winion_Pos01_0.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
		};
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Winion_Pos01_1.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos01.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
		};
		this.GridArriveAction = false;
		this.Grid.winionMovement.SetActiveMovement(true, true, false);
		this.Grid.winionMovement.SetTargetPosition(this.Grid_Pos01.position, true);
		this.Grid.winionBehaviour.arriveAction = delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionBehaviour.moveRandomPos = false;
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitForSeconds(4f);
		Chapter03_Event12.<>c__DisplayClass46_1 CS$<>8__locals2 = new Chapter03_Event12.<>c__DisplayClass46_1();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		this.ION.transform.position = this.Winion_Pos01.position;
		this.Fix.transform.position = this.Winion_Pos01_0.position;
		this._Debug.transform.position = this.Winion_Pos01_1.position;
		this.Grid.transform.position = this.Grid_Pos01.position;
		this.Bo.transform.position = this.Bo_Pos01.position;
		DBManager.instance.IsCutSceneSetting(true);
		yield return new WaitForSeconds(1f);
		this.Bo.winionMovement.SettingPos_SetTargetPos(this.Bo_Pos02);
		this.Grid.winionMovement.SettingPos_SetTargetPos(this.Grid_Pos02);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos02);
		this.Fix.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos02_0);
		this._Debug.winionMovement.SettingPos_SetTargetPos(this.Winion_Pos02_1);
		this.background.SetActive(true);
		ScreenCanvas.Instance.RemoveUI(true);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, true, 1f, 1f);
		SoundManager.instance.BGM_ChangeVolume_Tween(15f, 0f, false);
		SingletoneBehaviour<IconManager>.Instance.CloseAllFolder();
		CS$<>8__locals2.finish_fadeOut = false;
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(1f);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.Winion_Pos03.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("BackWalk", false);
			this.ION.winionAnimator.SetAnimationCanChange(false);
		};
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.Winion_Pos03_0.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("BackWalk", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
		};
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Winion_Pos03_1.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("BackWalk", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		};
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos03.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("BackWalk", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		};
		this.GridArriveAction = false;
		this.Grid.winionMovement.SetActiveMovement(true, true, false);
		this.Grid.winionMovement.SetTargetPosition(this.Grid_Pos03.position, true);
		this.Grid.winionBehaviour.arriveAction = delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionBehaviour.moveRandomPos = false;
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("BackWalk", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
		};
		yield return new WaitUntil(() => this.GridArriveAction);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.isMoving = true;
		yield return new WaitUntil(() => !this.isMoving);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.Winion_Pos01.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("BackWalk", false);
			this.ION.winionAnimator.SetAnimationCanChange(false);
		};
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.Winion_Pos01_0.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("BackWalk", false);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
		};
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.Winion_Pos01_1.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.winionAnimator.SetAnimationCanChange(true);
			this._Debug.winionAnimator.PlayAnimation("BackWalk", false);
			this._Debug.winionAnimator.SetAnimationCanChange(false);
		};
		this.Bo.SetAutoChackIdle_Personal = false;
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos04.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		};
		this.Grid.SetAutoChackIdle_Personal = false;
		this.GridArriveAction = false;
		this.Grid.winionMovement.SetActiveMovement(true, true, false);
		this.Grid.winionMovement.SetTargetPosition(this.Grid_Pos04.position, true);
		this.Grid.winionBehaviour.arriveAction = delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionBehaviour.moveRandomPos = false;
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, 5f);
		};
		yield return new WaitUntil(() => this.GridArriveAction);
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos01.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.Play_BGM(SoundManager.BGM.TempCredit, false, 0f);
		SoundManager.Instance.bgmPlayer.DOFade(1f, 10f);
		this.eventDialogueController.StartNextDialogue(true, 2f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(true, 2f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(true, 2f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("ShakeHand", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(0.5f);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("ShakeHand", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.Bo_Pos01.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitForSeconds(0.5f);
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
		};
		yield return new WaitForSeconds(4f);
		DBManager.instance.dialogueData.OpenBackLog = true;
		this.ION.SetScale(2f);
		this.ION.winionStatus.isRescueTeam = false;
		this.Fix.winionStatus.isRescueTeam = false;
		this._Debug.winionStatus.isRescueTeam = false;
		this.ION.Adjust_AlphaValue(0f, 0f);
		this.Fix.Adjust_AlphaValue(0f, 0f);
		this._Debug.Adjust_AlphaValue(0f, 0f);
		yield return new WaitForSeconds(1f);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Fix.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this._Debug.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		yield return new WaitForEndOfFrame();
		this.Fix.SetActiveWorldWinion(true);
		this.Fix.SetActiveUIWinion(false);
		this._Debug.SetActiveWorldWinion(true);
		this._Debug.SetActiveUIWinion(false);
		this.ION.winionMovement.SettingPos_SetTargetPos(this.ION_Pos00);
		this.Fix.winionMovement.SettingPos_SetTargetPos(this.Fix_Pos00);
		this._Debug.winionMovement.SettingPos_SetTargetPos(this.Debug_Pos00);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("BackIdle", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("BackIdle", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		this.ION.Adjust_AlphaValue(0.5f, 2f);
		this.Fix.Adjust_AlphaValue(0.5f, 2f);
		this._Debug.Adjust_AlphaValue(0.5f, 2f);
		yield return new WaitForSeconds(1f);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("ShakeBackHand", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1.5f);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("ShakeBackHand", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(0.5f);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.PlayAnimation("ShakeBackHand", false);
		this._Debug.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(4f);
		Chapter03_Event12.<>c__DisplayClass46_2 CS$<>8__locals3 = new Chapter03_Event12.<>c__DisplayClass46_2();
		this.illustration_img.SetActive(true);
		this.illustration_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(3f, 0f, FadeOutAction, this.illustration_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this.ION.SetActiveWorldWinion(false);
		this.ION.SetActiveUIWinion(false);
		this.Bo.SetActiveWorldWinion(false);
		this.Bo.SetActiveUIWinion(false);
		this.Grid.SetActiveWorldWinion(false);
		this.Grid.SetActiveUIWinion(false);
		this.Fix.SetActiveWorldWinion(false);
		this.Fix.SetActiveUIWinion(false);
		this._Debug.SetActiveWorldWinion(false);
		this._Debug.SetActiveUIWinion(false);
		CS$<>8__locals3.finish_fadeOut = false;
		SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_9", 1);
		yield return new WaitForSeconds(9f);
		this.endingGredit.SetActive(true);
		SingletoneBehaviour<EndingCredit>.Instance.PlayEndingCredit();
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, FadeOutAction, this.illustration_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		this.illustration_img.SetActive(false);
		CS$<>8__locals3 = null;
		FadeOutAction = null;
		yield return new WaitUntil(() => EndingCredit.CreditEnd);
		DBManager.instance.dialogueData.OpenBackLog = false;
		SingletoneBehaviour<WinionCalender>.Instance.NextDay("", "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x000BCDEC File Offset: 0x000BAFEC
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		if (eventDetailNum == 1)
		{
			if (dialogueNum == 4)
			{
				this.Grid.winionLookAt.SetActiveLookAt(false);
				this.Bo.winionLookAt.SetActiveLookAt(false);
				this.Bo.winionAnimator.SetAnimationCanChange(true);
				this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
				this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
				this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
				this.Grid.winionAnimator.SetAnimationCanChange(true);
				this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
			}
			if (dialogueNum == 10)
			{
				this.Bo.winionAnimator.SetAnimationCanChange(true);
				this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			}
		}
	}

	// Token: 0x040015E7 RID: 5607
	[Space]
	[Space]
	[Header("챕터 1의 이벤트 1번의 필요 변수들")]
	public Transform Grid_Pos00;

	// Token: 0x040015E8 RID: 5608
	public Transform Grid_Pos01;

	// Token: 0x040015E9 RID: 5609
	public Transform Grid_Pos02;

	// Token: 0x040015EA RID: 5610
	public Transform Grid_Pos03;

	// Token: 0x040015EB RID: 5611
	public Transform Grid_Pos04;

	// Token: 0x040015EC RID: 5612
	public Transform Bo_Pos00;

	// Token: 0x040015ED RID: 5613
	public Transform Bo_Pos01;

	// Token: 0x040015EE RID: 5614
	public Transform Bo_Pos02;

	// Token: 0x040015EF RID: 5615
	public Transform Bo_Pos03;

	// Token: 0x040015F0 RID: 5616
	public Transform Bo_Pos04;

	// Token: 0x040015F1 RID: 5617
	public Transform Winion_Pos00;

	// Token: 0x040015F2 RID: 5618
	public Transform Winion_Pos01;

	// Token: 0x040015F3 RID: 5619
	public Transform Winion_Pos02;

	// Token: 0x040015F4 RID: 5620
	public Transform Winion_Pos03;

	// Token: 0x040015F5 RID: 5621
	public Transform Winion_Pos00_0;

	// Token: 0x040015F6 RID: 5622
	public Transform Winion_Pos01_0;

	// Token: 0x040015F7 RID: 5623
	public Transform Winion_Pos02_0;

	// Token: 0x040015F8 RID: 5624
	public Transform Winion_Pos03_0;

	// Token: 0x040015F9 RID: 5625
	public Transform Winion_Pos00_1;

	// Token: 0x040015FA RID: 5626
	public Transform Winion_Pos01_1;

	// Token: 0x040015FB RID: 5627
	public Transform Winion_Pos02_1;

	// Token: 0x040015FC RID: 5628
	public Transform Winion_Pos03_1;

	// Token: 0x040015FD RID: 5629
	public Transform ION_Pos00;

	// Token: 0x040015FE RID: 5630
	public Transform Fix_Pos00;

	// Token: 0x040015FF RID: 5631
	public Transform Debug_Pos00;

	// Token: 0x04001600 RID: 5632
	public GameObject GridFacePos00;

	// Token: 0x04001601 RID: 5633
	public GameObject GridFacePos01;

	// Token: 0x04001602 RID: 5634
	public GameObject BoFacePos00;

	// Token: 0x04001603 RID: 5635
	public GameObject BoFacePos01;

	// Token: 0x04001604 RID: 5636
	public GameObject black;

	// Token: 0x04001605 RID: 5637
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001606 RID: 5638
	public GameObject illustration_img;

	// Token: 0x04001607 RID: 5639
	public CanvasGroup illustration_canvasGroup;

	// Token: 0x04001608 RID: 5640
	public GameObject background;

	// Token: 0x04001609 RID: 5641
	public RectTransform background_Rect;

	// Token: 0x0400160A RID: 5642
	private float speed = 140f;

	// Token: 0x0400160B RID: 5643
	private float targetYPosition = -298f;

	// Token: 0x0400160C RID: 5644
	private bool isMoving;

	// Token: 0x0400160D RID: 5645
	public GameObject endingGredit;
}
