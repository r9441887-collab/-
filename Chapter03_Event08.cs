using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class Chapter03_Event08 : EventBase
{
	// Token: 0x060018EC RID: 6380 RVA: 0x000189DD File Offset: 0x00016BDD
	public override void useStart()
	{
		this.Init();
		this._camera = Camera.main;
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000B566C File Offset: 0x000B386C
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 8;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
		if (ScreenCanvas.Instance.removeHomeUI)
		{
			ScreenCanvas.Instance.ResetHomeUI();
		}
		ScreenCanvas.Instance.RemoveUI(true);
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x000B5724 File Offset: 0x000B3924
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
				SoundManager.instance.bgmPlayer.pitch = 0.6f;
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
				if (SoundManager.instance.bgmPlayer.pitch != 0.6f)
				{
					SoundManager.instance.BGM_ChangePitch(15f, 0.6f);
				}
			}
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
			this.ION.winionLookAt.SetActiveLookAt(false);
			this.ION.SetSortOrder(true, 10);
			this._Debug.SetActiveWorldWinion(true);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionStatus.isFriend02 = false;
			this._Debug.winionStatus.isWinion01 = true;
			this._Debug.winionMovement.SettingPos_SetTargetPos(this.winion01_Pos00);
			this._Debug.winionLookAt.SetActiveLookAt(false);
			this._Debug.SetSortOrder(true, 10);
			Color color;
			if (ColorUtility.TryParseHtmlString("#BBFFBD", ref color))
			{
				this._Debug.winionAnimator.spriteRenderer.color = color;
			}
			this.Fix.SetActiveWorldWinion(true);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionStatus.isFriend01 = false;
			this.Fix.winionStatus.isWinion02 = true;
			this.Fix.winionAnimator.PlayAnimation("FrontIdle", false);
			this.Fix.winionMovement.SettingPos_SetTargetPos(this.winion02_Pos00);
			this.Fix.winionLookAt.SetActiveLookAt(false);
			this.Fix.SetSortOrder(true, 15);
			this.Fix.SetScale(2.2f);
			if (ColorUtility.TryParseHtmlString("#FFBAE3", ref color))
			{
				this.Fix.winionAnimator.spriteRenderer.color = color;
			}
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.SetActiveLookAtTarget(true, this._Debug.gameObject.transform);
			this.startEvent = true;
		}
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x000189F0 File Offset: 0x00016BF0
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

	// Token: 0x060018F1 RID: 6385 RVA: 0x000B5ADC File Offset: 0x000B3CDC
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 17)
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

	// Token: 0x060018F2 RID: 6386 RVA: 0x00018A2A File Offset: 0x00016C2A
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x00018A40 File Offset: 0x00016C40
	private IEnumerator EventDetailNum_00_co()
	{
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 1.5f);
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		DBManager.instance.dialogueController.StopSpeedDialogue();
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2.5f);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionLookAt.LookAtTarget(this._Debug.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this._Debug.winionAnimator.SetAnimationCanChange(true);
		this._Debug.winionLookAt.LookAtTarget(this.ION.gameObject);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(false, 1.5f);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this.ION.winionLookAt.LookAtTarget(this.Fix.gameObject);
		this._Debug.winionLookAt.LookAtTarget(this.Fix.gameObject);
		DummyWinionAnimator.LookAtTarget(this.Fix.gameObject);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this.Bo.winionMovement.SetMoveSpeed(MoveSpeed.Slow, false);
		this.Bo.SetActiveWorldWinion(true);
		this.Bo.SetActiveUIWinion(false);
		this.Bo.winionStatus.isWatchWinion = true;
		this.Bo.winionMovement.SettingPos_SetTargetPos(this.watchWinion_Pos00);
		this.Bo.winionLookAt.LookAtTarget(this.Fix.gameObject);
		this.Bo.SetScale(2.8f);
		yield return new WaitForSeconds(0.5f);
		this.black.SetActive(false);
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 0f, false);
		DummyWinionAnimator.PlayDummyMovePosition(this.Bo.gameObject);
		DummyWinionAnimator.LookAtTarget(this.Bo.gameObject);
		this.Fix.winionLookAt.LookAtTarget(this.Bo.gameObject);
		this.ION.winionLookAt.LookAtTarget(this.Bo.gameObject);
		this._Debug.winionLookAt.LookAtTarget(this.Bo.gameObject);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound, false, 0.4f, 1f);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, true, 1f, 1f);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.watchWinion_Pos01.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			DummyWinionAnimator.StopDummyMovePosition();
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		};
		yield return new WaitForSeconds(4f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this.Bo.winionMovement.SetMoveSpeed(MoveSpeed.Normal, false);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		this.Winions01.SetActive(false);
		this.Winions02.SetActive(true);
		this.ION.transform.position = this.BizitPos01.position;
		this._Debug.transform.position = this.winion01_Pos01.position;
		yield return new WaitForSeconds(1f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.WatchWinion_WalkSound, 2f);
		this.black.SetActive(false);
		SoundManager.instance.Play_BGM(SoundManager.BGM.GloomyBGM, true, 1f);
		SoundManager.instance.bgmPlayer.volume = 0f;
		SoundManager.instance.bgmPlayer.pitch = 0.8f;
		SoundManager.instance.BGM_ChangeVolume_Tween(30f, 1f, false);
		this.eventDialogueController.StartNextDialogue(true, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.WatchWinionFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(1f);
		this.black.SetActive(false);
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
		this.FixArriveAction = false;
		this.Fix.winionMovement.SetActiveMovement(true, true, false);
		this.Fix.winionMovement.SetTargetPosition(this.winion02_Pos01.position, true);
		this.Fix.winionBehaviour.arriveAction = delegate
		{
			this.FixArriveAction = true;
			this.Fix.winionBehaviour.moveRandomPos = false;
			this.Fix.winionBehaviour.arriveAction = null;
			this.Fix.winionMovement.SetActiveMovement(false, true, false);
			this.Fix.winionLookAt.LookAtTarget(this.Bo.gameObject);
		};
		yield return new WaitUntil(() => this.FixArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SoundManager.Instance.bgmPlayer.volume = 0f;
		SoundManager.Instance.Play_SfxSound(SoundManager.SfxSound.ScreenAdjustmentSound, true, 0.2f, 0.4f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.FightingSound_short, false, 1f, 1f);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		yield return new WaitForSeconds(0.3f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.1f, 0.7f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Response(0.1f, 0.4f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.1f, 0.7f, 0.5f, true);
		yield return new WaitUntil(() => !SingletoneBehaviour<GlitchManager>.Instance.usePixelationVol);
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(1f, 0f, 0.7f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.7f, 1f, 1f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(1f, 1f, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Response(1f, 0f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(false);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.ScreenAdjustmentSound, 5f);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle_WatchWinion_GetDamage", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.Bo.winionMovement.SetMoveSpeed(MoveSpeed.SuperFast, false);
		this.BoArriveAction = false;
		this.Bo.winionMovement.SetActiveMovement(true, true, false);
		this.Bo.winionMovement.SetTargetPosition(this.watchWinion_Pos02.position, true);
		this.Bo.winionBehaviour.arriveAction = delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionBehaviour.moveRandomPos = false;
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
		};
		yield return new WaitForSeconds(0.4f);
		yield return new WaitUntil(() => this.BoArriveAction);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.WatchWinionFacePos00.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BodyExploding_01, false, 1f, 1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound, false, 0.4f, 1f);
		this.Fix.winionAnimator.SetAnimationCanChange(true);
		this.Fix.winionAnimator.PlayAnimation("Death_Winion02", false);
		this.Fix.winionAnimator.SetAnimationCanChange(false);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		this.Fix.winionAnimator.EndFrameAction = delegate
		{
			this.Fix.winionAnimator.SetAnimationCanChange(true);
			this.Fix.winionAnimator.PlayAnimation("Death02_Winion02", false);
			this.blood_Winion00.SetActive(true);
			this.Fix.winionAnimator.SetAnimationCanChange(false);
			this.Fix.winionAnimator.EndFrameAction = null;
		};
		yield return new WaitForSeconds(1.5f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BodyExploding_01, false, 1f, 1f);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.BoomSound, false, 0.4f, 1.5f);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		DummyWinionAnimator.playDieAnimation = true;
		yield return new WaitForSeconds(1f);
		this.blood_Winion01.SetActive(true);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		int num;
		for (int i = 0; i < this.BubbleList.Count; i = num + 1)
		{
			this.BubbleList[i].SetActive(true);
			yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
			num = i;
		}
		yield return new WaitUntil(() => this.endDialogue);
		this.Bo.winionAnimator.PlayAnimation("FrontIdle_WatchWinion_GetDamage", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		for (int i = 0; i < this.BubbleList.Count / 2; i = num + 1)
		{
			if (!this.BubbleList[i].activeSelf)
			{
				this.BubbleList[i].SetActive(true);
			}
			yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
			num = i;
		}
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.DeathSound, false, 0.6f, 1f);
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x000B5B64 File Offset: 0x000B3D64
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		if (eventDetailNum == 17 && dialogueNum == 1)
		{
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("FrontIdle_WatchWinion_Smile", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		}
	}

	// Token: 0x04001554 RID: 5460
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x04001555 RID: 5461
	public Transform winion01_Pos00;

	// Token: 0x04001556 RID: 5462
	public Transform winion02_Pos00;

	// Token: 0x04001557 RID: 5463
	public Transform winion02_Pos01;

	// Token: 0x04001558 RID: 5464
	public Transform watchWinion_Pos00;

	// Token: 0x04001559 RID: 5465
	public Transform watchWinion_Pos01;

	// Token: 0x0400155A RID: 5466
	public Transform watchWinion_Pos02;

	// Token: 0x0400155B RID: 5467
	public GameObject light;

	// Token: 0x0400155C RID: 5468
	public GameObject black;

	// Token: 0x0400155D RID: 5469
	public CanvasGroup black_canvasGroup;

	// Token: 0x0400155E RID: 5470
	public GameObject Winions01;

	// Token: 0x0400155F RID: 5471
	public GameObject Winions02;

	// Token: 0x04001560 RID: 5472
	private Camera _camera;

	// Token: 0x04001561 RID: 5473
	public GameObject blood_Winion00;

	// Token: 0x04001562 RID: 5474
	public GameObject blood_Winion01;

	// Token: 0x04001563 RID: 5475
	public List<GameObject> BubbleList;

	// Token: 0x04001564 RID: 5476
	[Space]
	[Header("챕터 3의 이벤트 8번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;

	// Token: 0x04001565 RID: 5477
	public GameObject WatchWinionFacePos00;

	// Token: 0x04001566 RID: 5478
	public Transform BizitPos01;

	// Token: 0x04001567 RID: 5479
	public Transform winion01_Pos01;
}
