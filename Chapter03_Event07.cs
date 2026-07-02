using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class Chapter03_Event07 : EventBase
{
	// Token: 0x060018C0 RID: 6336 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000B4454 File Offset: 0x000B2654
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 7;
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

	// Token: 0x060018C2 RID: 6338 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000B4504 File Offset: 0x000B2704
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
				SoundManager.instance.bgmPlayer.pitch = 0.7f;
			}
			else
			{
				SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1f);
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(20f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 0.7f;
			}
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
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
			this.Fix.winionLookAt.LookAtTarget(this._Debug.gameObject);
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
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.systemWinionAnimator.SetIdleEye();
			SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this._Debug.transform);
			this.startEvent = true;
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x00018928 File Offset: 0x00016B28
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

	// Token: 0x060018C5 RID: 6341 RVA: 0x000B0F1C File Offset: 0x000AF11C
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

	// Token: 0x060018C6 RID: 6342 RVA: 0x00018962 File Offset: 0x00016B62
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x00018978 File Offset: 0x00016B78
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
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this.DebugArriveAction = false;
		this._Debug.winionMovement.SetActiveMovement(true, true, false);
		this._Debug.winionMovement.SetTargetPosition(this.friend02_Pos01.position, true);
		this._Debug.winionBehaviour.arriveAction = delegate
		{
			this.DebugArriveAction = true;
			this._Debug.winionBehaviour.moveRandomPos = false;
			this._Debug.winionBehaviour.arriveAction = null;
			this._Debug.winionMovement.SetActiveMovement(false, true, false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
		};
		yield return new WaitForSeconds(1.5f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		Chapter03_Event07.<>c__DisplayClass25_0 CS$<>8__locals1 = new Chapter03_Event07.<>c__DisplayClass25_0();
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
		if (!this.DebugArriveAction)
		{
			this._Debug.transform.position = this.friend02_Pos01.position;
		}
		this.ION.winionLookAt.SetActiveLookAt(false);
		this._Debug.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.ION.transform.position = this.BizitPos01.position;
		this.Fix.transform.position = this.friend01_Pos01.position;
		SystemWinion.instance.SystemWinion_Empty(true);
		this.ION.SetAutoChackIdle_Personal = false;
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.BizitPos02.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		};
		yield return new WaitForSeconds(1.5f);
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.friend01_Pos02.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
		};
		yield return new WaitForSeconds(0.5f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1.5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		yield return new WaitForSeconds(0.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.IONArriveAction);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos01.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.FixArriveAction);
		this.ION.winionLookAt.LookAtTarget(this.Fix.gameObject);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.PlayAnimation("FrontIdle_Smile", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		Chapter03_Event07.<>c__DisplayClass25_1 CS$<>8__locals2 = new Chapter03_Event07.<>c__DisplayClass25_1();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		this.ION.SetAutoChackIdle_Personal = true;
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1.5f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.Fix.transform.position = this.friend01_Pos03.position;
		this.Fix.winionLookAt.LookAtTarget(this.ION.gameObject);
		this.ION.transform.position = this.BizitPos03.position;
		this.ION.winionLookAt.LookAtTarget(this.Fix.gameObject);
		SystemWinion.instance.SystemWinion_Empty(false);
		SystemWinion.instance.systemWinionAnimator.SetIdleEye();
		yield return new WaitForSeconds(2f);
		SystemWinion.instance.systemWinionAnimator.SetActiveLookAtTarget(true, this.ION.transform);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1.5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos02.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos03.GetComponent<RectTransform>().localPosition);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Fix.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(0.5f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionLookAt.SetActiveLookAt(false);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.BizitPos04.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
		};
		this.Fix.winionLookAt.SetActiveLookAt(false);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("BackIdle", false);
		this.Fix.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Fix.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		yield return new WaitForSeconds(2f);
		Chapter03_Event07.<>c__DisplayClass25_2 CS$<>8__locals3 = new Chapter03_Event07.<>c__DisplayClass25_2();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals3.finish_fadeOut = false;
		Action action = delegate
		{
			CS$<>8__locals3.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1.5f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals3.finish_fadeOut);
		CS$<>8__locals3.finish_fadeOut = false;
		CS$<>8__locals3 = null;
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x04001535 RID: 5429
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x04001536 RID: 5430
	public Transform BizitPos01;

	// Token: 0x04001537 RID: 5431
	public Transform BizitPos02;

	// Token: 0x04001538 RID: 5432
	public Transform BizitPos03;

	// Token: 0x04001539 RID: 5433
	public Transform BizitPos04;

	// Token: 0x0400153A RID: 5434
	public Transform friend01_Pos00;

	// Token: 0x0400153B RID: 5435
	public Transform friend01_Pos01;

	// Token: 0x0400153C RID: 5436
	public Transform friend01_Pos02;

	// Token: 0x0400153D RID: 5437
	public Transform friend01_Pos03;

	// Token: 0x0400153E RID: 5438
	public Transform friend02_Pos00;

	// Token: 0x0400153F RID: 5439
	public Transform friend02_Pos01;

	// Token: 0x04001540 RID: 5440
	public GameObject light;

	// Token: 0x04001541 RID: 5441
	public GameObject black;

	// Token: 0x04001542 RID: 5442
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001543 RID: 5443
	[Space]
	[Header("챕터 3의 이벤트 6번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;

	// Token: 0x04001544 RID: 5444
	public GameObject BizitFacePos01;

	// Token: 0x04001545 RID: 5445
	public GameObject BizitFacePos02;

	// Token: 0x04001546 RID: 5446
	public GameObject BizitFacePos03;
}
