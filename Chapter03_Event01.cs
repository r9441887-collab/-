using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class Chapter03_Event01 : EventBase
{
	// Token: 0x060017E5 RID: 6117 RVA: 0x00014622 File Offset: 0x00012822
	public override void useStart()
	{
		this.Init();
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x000AF5C4 File Offset: 0x000AD7C4
	public override void Init()
	{
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter03)
		{
			GameManager.instance.gameData.curChapter = GameManager.Chapter.chapter03;
		}
		this.eventNum = 1;
		DBManager.instance.dialogueData.curEvent = this;
		DBManager.instance.dialogueData.curEventNum = this.eventNum;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		this.eventDialogueController.StartEvent(this.eventNum);
		this.eventDialogueNum = this.eventDialogueController.GetEventDialogueNum();
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x00014648 File Offset: 0x00012848
	public override void useUpdate()
	{
		if (this.checkCondition)
		{
			this.CheckEventDetailStartCondition();
		}
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000AF670 File Offset: 0x000AD870
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
				SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.8f;
			}
			else
			{
				if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmClip[1])
				{
					SoundManager.instance.Play_BGM(SoundManager.BGM.Ingame_Normal, true, 1f);
				}
				SoundManager.instance.bgmPlayer.volume = 0f;
				SoundManager.instance.bgmPlayer.pitch = 0.8f;
			}
			SingletoneBehaviour<WinionCalender>.Instance.powerBtn.setting_NextDay(false, false, 0);
			this.ION.SetActiveWorldWinion(false);
			this.ION.SetActiveUIWinion(false);
			this.Fix.SetActiveWorldWinion(false);
			this.Fix.SetActiveUIWinion(false);
			this._Debug.SetActiveWorldWinion(false);
			this._Debug.SetActiveUIWinion(false);
			this.Grid.winionAnimator.Grid_emptyness02 = false;
			this.Bo.winionAnimator.Bo_emptyness02 = true;
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().BlackGroup.alpha = 1f;
			SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.SystemWinionRoom);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SettingWinionRoom(true);
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(false);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos00_systemWinionPos);
			SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.GridPos00_systemWinionPos);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Bo, true);
			DBManager.instance.winionFaceInfo.InSystemWinionRoom_FaceSetting(Winion.Grid, true);
			int num = 7;
			SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, true, false);
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			component.SystemWinionRoomColor(true, this.Grid);
			component.SystemWinionRoomColor(true, this.Bo);
			SystemWinion.instance.SystemWinion_Empty(true);
			this.startEvent = true;
		}
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x0001854C File Offset: 0x0001674C
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

	// Token: 0x060017EA RID: 6122 RVA: 0x00099534 File Offset: 0x00097734
	public override void EndEvent()
	{
		this.endDialogue = true;
		if (this.curEventDetailNum == 7)
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

	// Token: 0x060017EB RID: 6123 RVA: 0x00018586 File Offset: 0x00016786
	private void curEventDetailNum_00()
	{
		this.startEvent = false;
		base.StartCoroutine(this.curEventDetailNum_00_co());
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x0001859C File Offset: 0x0001679C
	private IEnumerator curEventDetailNum_00_co()
	{
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().BlackGroupFadeIn();
		DBManager.instance.dialogueController.PermissionSpeedDialogue();
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Misc_Noise_01, true, 0.5f, 1f).pitch = 1f;
		if (SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitForSeconds(3f);
		}
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
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
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos00.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos00.GetComponent<RectTransform>().localPosition);
		this.Bo.winionAnimator.Bo_emptyness02 = false;
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
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
		Chapter03_Event01.<>c__DisplayClass21_0 CS$<>8__locals1 = new Chapter03_Event01.<>c__DisplayClass21_0();
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		CS$<>8__locals1.finish_fadeOut = false;
		Action FadeOutAction = delegate
		{
			CS$<>8__locals1.finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, FadeOutAction, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinionRoom).GetComponent<SystemWinionRoom>().SetActiveSystemWinion(true);
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Grid, Winion.System, this.GridPos00_systemWinionPos);
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionIntoSpecialRoom(Winion.Bo, Winion.System, this.GridPos00_systemWinionPos);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		CS$<>8__locals1.finish_fadeOut = false;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, FadeOutAction, this.black_canvasGroup);
		yield return new WaitUntil(() => CS$<>8__locals1.finish_fadeOut);
		CS$<>8__locals1.finish_fadeOut = false;
		this.black.SetActive(false);
		CS$<>8__locals1 = null;
		FadeOutAction = null;
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.GridArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Grid, Vector3.zero, this.GridPos02_systemWinionPos, delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
		});
		yield return new WaitForSeconds(2.5f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.BoArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Bo, Vector3.zero, this.BoPos02_systemWinionPos, delegate
		{
			this.BoArriveAction = true;
			this.Bo.winionMovement.SetActiveMovement(false, true, false);
			this.Bo.winionBehaviour.arriveAction = null;
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		});
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.GridArriveAction = false;
		SingletoneBehaviour<WinionFolderManager>.Instance.WinionMoveInSpecialRoom(Winion.Grid, Vector3.zero, this.GridPos01_systemWinionPos, delegate
		{
			this.GridArriveAction = true;
			this.Grid.winionMovement.SetActiveMovement(false, true, false);
			this.Grid.winionBehaviour.arriveAction = null;
			this.Grid.winionAnimator.PlayAnimation("BackIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
		});
		yield return new WaitForSeconds(2.5f);
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
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		});
		yield return new WaitUntil(() => this.BoArriveAction);
		yield return new WaitUntil(() => this.GridArriveAction);
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.SR_Strange_room, true, 0.5f, 1f).pitch = 0.15f;
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Grid);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Grid, this.GridFacePos01.GetComponent<RectTransform>().localPosition);
		SingletoneBehaviour<IconManager>.Instance.OpenFolder(Icon.Face_Bo);
		SingletoneBehaviour<IconManager>.Instance.SetWindowPosition(Icon.Face_Bo, this.BoFacePos01.GetComponent<RectTransform>().localPosition);
		yield return new WaitUntil(() => this.endDialogue);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		yield return new WaitForSeconds(1f);
		foreach (GameObject gameObject in this.console_SystemWinionRoom)
		{
			gameObject.SetActive(true);
			yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		yield return new WaitForSeconds(3f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		this.Bo.winionLookAt.LookAtTarget(this.Grid.gameObject);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionLookAt.LookAtTarget(this.Bo.gameObject);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		yield return new WaitForSeconds(1.5f);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Grid, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(Icon.Face_Bo, false);
		this.Grid.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		this.Grid.winionLookAt.SetActiveLookAt(false);
		this.Grid.winionAnimator.SetAnimationCanChange(true);
		this.Grid.winionAnimator.PlayAnimation("Sleeping", false);
		this.Grid.winionAnimator.SetAnimationCanChange(false);
		yield return new WaitForSeconds(1f);
		this.Bo.winionLookAt.SetActiveLookAt(false);
		this.Bo.winionAnimator.SetAnimationCanChange(true);
		this.Bo.winionAnimator.PlayAnimation("LeftIdle", false);
		this.Bo.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
		this.Bo.winionAnimator.SetAnimationCanChange(false);
		this.eventDialogueController.StartNextDialogue(false, 1.5f, true);
		yield return new WaitUntil(() => this.endDialogue);
		this.black.SetActive(true);
		this.black_canvasGroup.alpha = 0f;
		bool finish_fadeOut = false;
		Action action = delegate
		{
			finish_fadeOut = true;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(2f, 0f, action, this.black_canvasGroup, 1f);
		yield return new WaitUntil(() => finish_fadeOut);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.SR_Strange_room, 5f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.Misc_Noise_01, 5f);
		SingletoneBehaviour<GlitchManager>.Instance.SwitchOn_LiftGammaGain(false, false, false);
		SystemWinion.instance.inSystemWinionRoom = false;
		yield return new WaitForSeconds(3f);
		SingletoneBehaviour<WinionCalender>.Instance.NextDay(DayInfo.MemoryRecovering, "");
		base.BlockDialogue(false);
		yield break;
		yield break;
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
	public override void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		if (eventDetailNum == 0 && dialogueNum == 5)
		{
			this.Grid.winionLookAt.SetActiveLookAt(false);
			this.Grid.winionAnimator.SetAnimationCanChange(true);
			this.Grid.winionAnimator.PlayAnimation("FrontIdle", false);
			this.Grid.winionAnimator.SetAnimationCanChange(false);
			this.Bo.winionLookAt.SetActiveLookAt(false);
			this.Bo.winionAnimator.SetAnimationCanChange(true);
			this.Bo.winionAnimator.PlayAnimation("FrontIdle", false);
			this.Bo.winionAnimator.SetAnimationCanChange(false);
		}
	}

	// Token: 0x040014C5 RID: 5317
	[Space]
	[Header("챕터 2의 이벤트 25번의 필요 변수들")]
	public RectTransform GridPos00_systemWinionPos;

	// Token: 0x040014C6 RID: 5318
	public RectTransform GridPos01_systemWinionPos;

	// Token: 0x040014C7 RID: 5319
	public RectTransform GridPos02_systemWinionPos;

	// Token: 0x040014C8 RID: 5320
	public RectTransform BoPos00_systemWinionPos;

	// Token: 0x040014C9 RID: 5321
	public RectTransform BoPos01_systemWinionPos;

	// Token: 0x040014CA RID: 5322
	public RectTransform BoPos02_systemWinionPos;

	// Token: 0x040014CB RID: 5323
	[Space]
	[Header("챕터 2의 이벤트 25번 얼굴윈도우 위치")]
	public GameObject GridFacePos00;

	// Token: 0x040014CC RID: 5324
	public GameObject GridFacePos01;

	// Token: 0x040014CD RID: 5325
	public GameObject BoFacePos00;

	// Token: 0x040014CE RID: 5326
	public GameObject BoFacePos01;

	// Token: 0x040014CF RID: 5327
	public GameObject light;

	// Token: 0x040014D0 RID: 5328
	public GameObject black;

	// Token: 0x040014D1 RID: 5329
	public CanvasGroup black_canvasGroup;

	// Token: 0x040014D2 RID: 5330
	public List<GameObject> console_SystemWinionRoom;
}
