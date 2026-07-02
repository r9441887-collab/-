using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034B RID: 843
public class Chapter03_Event10 : EventBase
{
	// Token: 0x06001950 RID: 6480 RVA: 0x00018B49 File Offset: 0x00016D49
	public override void useStart()
	{
		this.Init();
		this._camera = Camera.main;
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x00018B5C File Offset: 0x00016D5C
	public IEnumerator CrazyText()
	{
		this.bigitText.enabled = true;
		Tweener sizeTween = DOVirtual.Float(-1f, 1f, 0.5f, delegate(float value)
		{
			this.bigitText.fontSize = 27.9f + value + Random.Range(-0.5f, 0.5f);
		}).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		string[] lines = this.virusScanText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		int num;
		for (int i = 0; i < 15; i = num + 1)
		{
			foreach (string text in lines)
			{
				TweenerCore<string, string, StringOptions> tweenerCore = this.bigitText.DOText(text + "\n", 0.05f, true, ScrambleMode.None, null).SetEase(Ease.Linear).SetRelative(true);
				yield return TweenExtensions.WaitForCompletion(tweenerCore);
			}
			string[] array = null;
			num = i;
		}
		yield return new WaitForSeconds(1f);
		sizeTween.Kill(false);
		yield break;
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x000B89E8 File Offset: 0x000B6BE8
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter03)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		}
		this.eventNum = 10;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x000B8A94 File Offset: 0x000B6C94
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
			foreach (GameObject gameObject in this.console_SystemWinionRoom)
			{
				gameObject.SetActive(true);
			}
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
			this.ION.winionStatus.isBizit = false;
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this.Fix.winionStatus.isFriend01 = false;
			this.Fix.winionStatus.isWinion02 = false;
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			this._Debug.winionStatus.isFriend02 = false;
			this._Debug.winionStatus.isWinion01 = false;
			this.Bo.winionStatus.isWatchWinion = false;
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SettingWinionRoom(true);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos00_systemWinionPos);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.BoPos00_systemWinionPos);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, true);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, true);
			int num = 7;
			SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, true, false);
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			component.SystemWinionRoomColor(true, this.Grid);
			component.SystemWinionRoomColor(true, this.Bo);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
			this.Grid.winionLookAt.SetActiveLookAt(false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			this.Grid.winionAnimator.PlayAnimation("Sleeping", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this.bigitText.enabled = false;
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x00018B6B File Offset: 0x00016D6B
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

	// Token: 0x06001956 RID: 6486 RVA: 0x000B8E7C File Offset: 0x000B707C
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 10)
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

	// Token: 0x06001957 RID: 6487 RVA: 0x00018BA5 File Offset: 0x00016DA5
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x00018BBB File Offset: 0x00016DBB
	private IEnumerator curEventDetailNum_00_co()
	{
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0.5f, 1f).pitch = 1f;
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		DBManager.instance.dialogueController.StopSpeedDialogue();
		yield return new WaitUntil(() => !DBManager.instance.dialogueData.curDialogue_ing);
		base.BlockDialogue(true);
		yield return new WaitForSeconds(1f);
		SystemWinion.instance.inSystemWinionRoom = true;
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0.5f, 1f).pitch = 0.15f;
		foreach (GameObject gameObject in this.console_SystemWinionRoom)
		{
			gameObject.SetActive(false);
			yield return new WaitForSeconds(Random.Range(0.3f, 0.4f));
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(0.7f);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Grid.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		yield return new WaitForSeconds(0.4f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("BackIdle", false);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<GlitchManager>.Instance.CameraShake(1.3f, 13f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, false);
		DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		Chapter03_Event10.<>c__DisplayClass24_1 CS$<>8__locals2 = new Chapter03_Event10.<>c__DisplayClass24_1();
		this.black.SetActive(true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0f, false);
		this.black.GetComponent<Image>().color = Color.white;
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals2.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals2.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 2f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 2f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, 0f, false);
		this.light.SetActive(false);
		this.light02.SetActive(true);
		this.background.SetActive(true);
		ScreenCanvas.Instance.RemoveUI(true);
		this.ION.SetActiveWorldWinion(true);
		this.ION.SetActiveUIWinion(false);
		this.ION.winionStatus.isBizit = true;
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
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(5f, 0.2f, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.SystemWinionRoom, false);
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(5f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals2.finish_fadeOut);
		CS$<>8__locals2.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals2 = null;
		FadeOutAction = null;
		SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 0f);
		SoundManager.instance.BGM_ChangeVolume_Tween(15f, 1f, false);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.PlayAnimation("BackIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2.5f);
		SoundManager.instance.BGM_ChangeVolume_Tween(10f, 0.4f, false);
		SoundManager.instance.BGM_ChangePitch(10f, 0.4f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		SoundManager.instance.BGM_ChangeVolume_Tween(2f, 0f, false);
		yield return new WaitForSeconds(3f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(4f);
		this.black.GetComponent<Image>().color = Color.black;
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Red");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 0.75f, false);
		AudioSource audioSource_horror = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase01, true, 1f, 1f);
		audioSource_horror.volume = 0.2f;
		SoundManager.instance.SfxSoundTween(audioSource_horror, 1f, 5f, true, false);
		SoundManager.instance.SfxSoundTween(audioSource_horror, 2f, 5f, false, true);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		yield return new WaitForSeconds(1.5f);
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.red;
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.6f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		audioSource_horror = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase02, true, 1f, 1f);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		DBManager.instance.dialogueData.NoBacklogOpen = true;
		yield return null;
		Coroutine crazy_Co = base.StartCoroutine("CrazyText");
		SoundManager.instance.SfxSoundTween(audioSource_horror, 2f, 5f, false, true);
		SingletoneBehaviour<GlitchManager>.Instance.TweenLensDistortionVol_Intensity(6f, 0f, 0.7f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(3f, 0.4f);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		this.eventDialogueController.StartNextDialogue(true, 0.5f, true);
		yield return new WaitForSeconds(1f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(false);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(2f);
		this.black.SetActive(true);
		this.black.GetComponent<Image>().color = Color.red;
		this.black_canvasGroup.alpha = 1f;
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		ShortcutExtensions.DOShakePosition(this._camera, 0.15f, Vector3.one * 0.1f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
		yield return new WaitForSeconds(1f);
		AudioSource hitSound = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Hit_metallic, false, 1f, 1f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HorrorChase02, 3f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HorrorChase01, 2f);
		base.StopCoroutine(crazy_Co);
		DBManager.instance.dialogueController.PermissionSpeedDialogue();
		yield return new WaitForSeconds(2f);
		yield return TweenExtensions.WaitForCompletion(this.black.GetComponent<Image>().DOColor(Color.black, 1f));
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, 0f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenLensDistortionVol_Intensity(0.5f, 0.7f, 0f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0f);
		yield return new WaitUntil(() => !hitSound.isPlaying);
		yield return new WaitForSeconds(2f);
		MyPcWindowResolution.chapter = HorrorChapter.Chapter4;
		Cursor.lockState = 1;
		SceneLoader.LoadScene("HorrorSceneForLoading", true, false);
		DBManager.instance.NoBacklogOpen_False();
		SingletoneBehaviour<WinionCalender>.Instance.NextDay("", "");
		base.BlockDialogue(false);
		yield break;
		yield break;
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x00015987 File Offset: 0x00013B87
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
	}

	// Token: 0x0400158F RID: 5519
	[Space]
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public RectTransform GridPos00_systemWinionPos;

	// Token: 0x04001590 RID: 5520
	public RectTransform BoPos00_systemWinionPos;

	// Token: 0x04001591 RID: 5521
	public Transform BizitPos00;

	// Token: 0x04001592 RID: 5522
	public Transform SystemWinionPos00;

	// Token: 0x04001593 RID: 5523
	public GameObject light;

	// Token: 0x04001594 RID: 5524
	public GameObject light02;

	// Token: 0x04001595 RID: 5525
	public GameObject black;

	// Token: 0x04001596 RID: 5526
	public CanvasGroup black_canvasGroup;

	// Token: 0x04001597 RID: 5527
	public List<GameObject> console_SystemWinionRoom;

	// Token: 0x04001598 RID: 5528
	public GameObject background;

	// Token: 0x04001599 RID: 5529
	private Camera _camera;

	// Token: 0x0400159A RID: 5530
	[Space]
	[Header("챕터 3의 이벤트 2번 얼굴윈도우 위치")]
	public GameObject BoFacePos00;

	// Token: 0x0400159B RID: 5531
	public GameObject GridFacePos00;

	// Token: 0x0400159C RID: 5532
	public GameObject BizitFacePos00;

	// Token: 0x0400159D RID: 5533
	public TextMeshProUGUI bigitText;

	// Token: 0x0400159E RID: 5534
	[TextArea(10, 10)]
	public string virusScanText;
}
