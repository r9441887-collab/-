using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class Chapter03_Event05 : EventBase
{
	// Token: 0x06001878 RID: 6264 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x000B295C File Offset: 0x000B0B5C
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		this.eventNum = 5;
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

	// Token: 0x0600187A RID: 6266 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x000B2A0C File Offset: 0x000B0C0C
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
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[7])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.RecollectionOfMemory, true, 1f);
				}
				if (SoundManager.instance.bgmPlayer.volume != 1f)
				{
					SoundManager.instance.BGM_ChangeVolume_Tween(3f, 1f, false);
				}
				SoundManager.instance.bgmPlayer.pitch = 1f;
			}
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0f, 0.75f, false);
			this.ION.SetActiveWorldWinion(true);
			this.ION.SetActiveUIWinion(false);
			this.ION.winionStatus.winionInfo.isDeath = false;
			this.ION.winionStatus.isBizit = true;
			this.ION.winionMovement.SettingPos_SetTargetPos(this.BizitPos00);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
			this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
			this.ION.winionAnimator.SetAnimationCanChange(false);
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			this.Grid.SetActiveWorldWinion(false);
			this.Grid.SetActiveUIWinion(false);
			this.Bo.SetActiveWorldWinion(false);
			this.Bo.SetActiveUIWinion(false);
			SystemWinion.instance.SystemWinion_Empty(false);
			SystemWinion.instance.systemWinionAnimator.SetIdleEye();
			SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_CenterSwitch(true, 0f);
			this.startEvent = true;
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x000187F1 File Offset: 0x000169F1
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

	// Token: 0x0600187D RID: 6269 RVA: 0x000B0F1C File Offset: 0x000AF11C
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

	// Token: 0x0600187E RID: 6270 RVA: 0x0001882B File Offset: 0x00016A2B
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.EventDetailNum_00_co());
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x00018841 File Offset: 0x00016A41
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
		DBManager.instance.dialogueController.StopSpeedDialogue();
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Ion);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Ion, this.BizitFacePos00.GetComponent<RectTransform>().localPosition);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(2f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.blackCanvasGroup.alpha = 1f;
		this.light.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		this.black.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HeartSound_Nervous, true, 0.7f, 1f);
		SoundManager.instance.BGM_ChangePitch(2f, -0.7f);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitForSeconds(1.5f);
		SoundManager.instance.BGM_ChangePitch(3f, 0.7f);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.winionAnimator.SetAnimationCanChange(true);
		this.ION.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Shock02", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		this.ION.SetAutoChackIdle_Personal = false;
		this.IONArriveAction = false;
		this.ION.winionMovement.SetActiveMovement(true, true, false);
		this.ION.winionMovement.SetTargetPosition(this.BizitPos01.position, true);
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.IONArriveAction = true;
			this.ION.winionBehaviour.moveRandomPos = false;
			this.ION.winionBehaviour.arriveAction = null;
			this.ION.winionMovement.SetActiveMovement(false, true, false);
			this.ION.winionAnimator.SetAnimationCanChange(true);
			this.ION.winionAnimator.PlayAnimation("LeftIdle", false);
		};
		yield return new WaitUntil(() => this.IONArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.ION.SetAutoChackIdle_Personal = true;
		this.ION.winionAnimator.PlayAnimation("LeftIdle_Bizit_Angry", false);
		this.ION.winionAnimator.SetAnimationCanChange(false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Ion, false);
		SoundManager.instance.BGM_ChangeVolume_Tween(6.5f, 0f, false);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HeartSound_Nervous, 5f);
		this.black.SetActive(true);
		this.blackCanvasGroup.alpha = 0f;
		bool finish_fadeOut = false;
		Action action = delegate
		{
			finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, action, this.blackCanvasGroup, 1f);
		yield return new WaitUntil(() => finish_fadeOut);
		yield return new WaitUntil(() => SoundManager.instance.bgmPlayer.volume == 0f);
		DBManager.instance.dialogueController.PermissionSpeedDialogue();
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000B2CC8 File Offset: 0x000B0EC8
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		if (eventDetailNum == 1)
		{
			if (dialogueNum == 0)
			{
				SystemWinion.instance.systemWinionAnimator.PlayAnimation("Smile", true);
			}
			if (dialogueNum == 1)
			{
				SystemWinion.instance.systemWinionAnimator.SetIdleEye();
				SystemWinion.instance.SetActiveLookAtTarget(true, this.ION.gameObject.transform);
			}
		}
	}

	// Token: 0x0400150D RID: 5389
	[Space]
	[Space]
	[Header("챕터 3의 이벤트 0번의 필요 변수들")]
	public Transform BizitPos00;

	// Token: 0x0400150E RID: 5390
	public Transform BizitPos01;

	// Token: 0x0400150F RID: 5391
	public GameObject light;

	// Token: 0x04001510 RID: 5392
	public GameObject black;

	// Token: 0x04001511 RID: 5393
	public CanvasGroup blackCanvasGroup;

	// Token: 0x04001512 RID: 5394
	[Space]
	[Header("챕터 3의 이벤트 4번 얼굴윈도우 위치")]
	public GameObject BizitFacePos00;
}
