using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class EventBase : MonoBehaviour
{
	// Token: 0x06001A38 RID: 6712 RVA: 0x00019007 File Offset: 0x00017207
	private void Start()
	{
		base.StartCoroutine(this.Start_co());
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x00019016 File Offset: 0x00017216
	private IEnumerator Start_co()
	{
		this.playStart = false;
		yield return new WaitUntil(() => ChapterSetter.ChapterSetEnd == -1);
		yield return new WaitUntil(() => ChapterSetter.ChapterSetEnd == 0);
		ChapterSetter.ChapterSetEnd = 1;
		this.startEvent = false;
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		this.SettingWinion();
		this.useStart();
		this.Change_Language();
		SingletoneBehaviour<BottomLineManager>.Instance.SettingTime_Event();
		this.playStart = true;
		yield break;
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000BFE68 File Offset: 0x000BE068
	public void SettingWinion()
	{
		if (!this.setting_Complete)
		{
			this.setting_Complete = true;
			this.systemWinion = GameManager.instance.gameData.systemWinion;
			this.ION = GameManager.instance.gameData.ION;
			this.Bo = GameManager.instance.gameData.Bo;
			this.Grid = GameManager.instance.gameData.Grid;
			this.Fix = GameManager.instance.gameData.Fix;
			this._Debug = GameManager.instance.gameData.Debug;
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x00014622 File Offset: 0x00012822
	public virtual void useStart()
	{
		this.Init();
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void Init()
	{
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x00019025 File Offset: 0x00017225
	private void Update()
	{
		if (this.playStart)
		{
			this.useUpdate();
		}
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void useUpdate()
	{
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x00019035 File Offset: 0x00017235
	public void StartEvent()
	{
		this.startEvent = true;
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x0001903E File Offset: 0x0001723E
	public virtual void SettingCondition(int curEventDetailNum)
	{
		switch (curEventDetailNum)
		{
		case 0:
			this.systemWinion.haveEvent = true;
			break;
		case 1:
		case 2:
		case 3:
		case 4:
			break;
		default:
			return;
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x000BFF08 File Offset: 0x000BE108
	public virtual void CheckEventDetailStartCondition()
	{
		switch (this.curEventDetailNum)
		{
		default:
			return;
		}
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x000BFF34 File Offset: 0x000BE134
	public virtual void EndEvent()
	{
		this.endDialogue = true;
		this.isSetting = false;
		this.checkCondition = true;
		DBManager.instance.dialogueData.curEventDetailNum++;
		this.curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
		if (this.eventDialogueNum <= this.curEventDetailNum)
		{
			this.eventDialogueController.FinishEvent();
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void Change_Language()
	{
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x00069D18 File Offset: 0x00067F18
	public virtual void SettingEvent(int eventDetailNum = 0, int dialogueNum = 0)
	{
		switch (eventDetailNum)
		{
		case 0:
			switch (dialogueNum)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				break;
			default:
				return;
			}
			break;
		case 1:
			switch (dialogueNum)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				break;
			default:
				return;
			}
			break;
		case 2:
			switch (dialogueNum)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				break;
			default:
				return;
			}
			break;
		case 3:
			switch (dialogueNum)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				break;
			default:
				return;
			}
			break;
		case 4:
			switch (dialogueNum)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				break;
			default:
				return;
			}
			break;
		case 5:
			switch (dialogueNum)
			{
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x000BFF9C File Offset: 0x000BE19C
	public void BlockDialogue(bool block = true)
	{
		if (!this.Grid.blockDialogue)
		{
			if (block)
			{
				DBManager.instance.eventDialogueController.makeBackLog_Bundle = true;
			}
		}
		else if (!block)
		{
			DBManager.instance.eventDialogueController.makeBackLog_Bundle = false;
		}
		EventBase.blockDialogue = block;
		if (this.ION.winionStatus.winionInfo.isDeath)
		{
			this.ION.blockDialogue = true;
		}
		else if (this.ION.doubleLock && !block)
		{
			this.ION.blockDialogue = true;
		}
		else
		{
			this.ION.blockDialogue = block;
		}
		if (this.Bo.doubleLock && !block)
		{
			this.Bo.blockDialogue = true;
		}
		else
		{
			this.Bo.blockDialogue = block;
		}
		if (this.Grid.doubleLock && !block)
		{
			this.Grid.blockDialogue = true;
		}
		else
		{
			this.Grid.blockDialogue = block;
		}
		if (this.Fix.winionStatus.winionInfo.isDeath)
		{
			this.Fix.blockDialogue = true;
		}
		else if (this.Fix.doubleLock && !block)
		{
			this.Fix.blockDialogue = true;
		}
		else
		{
			this.Fix.blockDialogue = block;
		}
		if (this._Debug.doubleLock && !block)
		{
			this._Debug.blockDialogue = true;
		}
		else
		{
			this._Debug.blockDialogue = block;
		}
		if (SystemWinion.instance.doubleLock && !block)
		{
			SystemWinion.instance.blockDialogue = true;
		}
		else
		{
			SystemWinion.instance.blockDialogue = block;
		}
		if (block)
		{
			SingletoneBehaviour<SystemWinionConsole>.Instance.ClearConsole();
		}
		if (block)
		{
			this.SettingEvent(false);
			return;
		}
		this.SettingEvent(true);
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x000C0148 File Offset: 0x000BE348
	public void Check_HaveSmallTalk_AboutEvent()
	{
		WinionHandler winionHandler = this.ION;
		bool flag;
		int num;
		int num2;
		int num3;
		int num4;
		for (int i = 0; i < 5; i++)
		{
			flag = false;
			switch (i)
			{
			case 0:
				winionHandler = this.ION;
				break;
			case 1:
				winionHandler = this.Bo;
				break;
			case 2:
				winionHandler = this.Grid;
				break;
			case 3:
				winionHandler = this.Fix;
				break;
			case 4:
				winionHandler = this._Debug;
				break;
			}
			num = DBManager.instance.dialogueData.curEventNum;
			num2 = DBManager.instance.dialogueData.curEventDetailNum;
			num3 = winionHandler.winionStatus.winionInfo.presonality.id;
			num4 = DBManager.instance.Get_Dialogue_byEvent_Id(num, -1, num3);
			if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num4))
			{
				WinionDialogue_ByEvent winionDialogue_ByEvent = DBManager.instance.WinionDialogue_ByEventChapter[num4];
				if (winionDialogue_ByEvent.NextDialogueNum < winionDialogue_ByEvent.event_Dialogues.Count)
				{
					flag = true;
					Dialogue dialogue = winionDialogue_ByEvent.event_Dialogues[winionDialogue_ByEvent.NextDialogueNum];
				}
			}
			if (!flag)
			{
				num4 = DBManager.instance.Get_Dialogue_byEvent_Id(num, num2, num3);
				if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num4))
				{
					WinionDialogue_ByEvent winionDialogue_ByEvent2 = DBManager.instance.WinionDialogue_ByEventChapter[num4];
					if (winionDialogue_ByEvent2.NextDialogueNum < winionDialogue_ByEvent2.event_Dialogues.Count)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				winionHandler.have_smallTalk_Events = true;
			}
			else
			{
				winionHandler.have_smallTalk_Events = false;
			}
		}
		flag = false;
		num = DBManager.instance.dialogueData.curEventNum;
		num2 = DBManager.instance.dialogueData.curEventDetailNum;
		num3 = SystemWinion.instance.systemWinionID;
		num4 = DBManager.instance.Get_Dialogue_byEvent_Id(num, -1, num3);
		if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num4))
		{
			WinionDialogue_ByEvent winionDialogue_ByEvent3 = DBManager.instance.WinionDialogue_ByEventChapter[num4];
			if (winionDialogue_ByEvent3.NextDialogueNum < winionDialogue_ByEvent3.event_Dialogues.Count)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			num4 = DBManager.instance.Get_Dialogue_byEvent_Id(num, num2, num3);
			if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num4))
			{
				WinionDialogue_ByEvent winionDialogue_ByEvent4 = DBManager.instance.WinionDialogue_ByEventChapter[num4];
				if (winionDialogue_ByEvent4.NextDialogueNum < winionDialogue_ByEvent4.event_Dialogues.Count)
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			SystemWinion.instance.have_smallTalk_Events = true;
			return;
		}
		SystemWinion.instance.have_smallTalk_Events = false;
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x000C03A4 File Offset: 0x000BE5A4
	public void SettingEvent(bool show)
	{
		if (show)
		{
			this.Check_HaveSmallTalk_AboutEvent();
		}
		WinionHandler winionHandler = this.ION;
		for (int i = 0; i < 5; i++)
		{
			switch (i)
			{
			case 0:
				winionHandler = this.ION;
				break;
			case 1:
				winionHandler = this.Bo;
				break;
			case 2:
				winionHandler = this.Grid;
				break;
			case 3:
				winionHandler = this.Fix;
				break;
			case 4:
				winionHandler = this._Debug;
				break;
			}
			if (!winionHandler.haveEvent)
			{
				if (winionHandler.have_smallTalk_Events)
				{
					if (show)
					{
						if (this.CheckWinion_Dialogue(winionHandler))
						{
							winionHandler.HaveDialogueEmotion(true);
						}
					}
					else
					{
						winionHandler.ReturnDialogueEmotion(true);
					}
				}
				else if (winionHandler.haveSmallTalkEmotion)
				{
					winionHandler.ReturnDialogueEmotion(true);
				}
			}
			else if (!winionHandler.haveEventEmotion)
			{
				if (show)
				{
					winionHandler.HaveDialogueEmotion(false);
				}
			}
			else if (!show)
			{
				winionHandler.ReturnDialogueEmotion(false);
			}
		}
		if (!SystemWinion.instance.haveEvent)
		{
			if (SystemWinion.instance.have_smallTalk_Events)
			{
				if (!show)
				{
					SystemWinion.instance.RetrunDialogueEmotion();
					return;
				}
				if (this.CheckWinion_Dialogue_SystemWinion())
				{
					SystemWinion.instance.HaveDialogueEmotion(true);
					return;
				}
			}
			else if (SystemWinion.instance.haveSmallTalkEmotion)
			{
				SystemWinion.instance.RetrunDialogueEmotion();
				return;
			}
		}
		else if (!SystemWinion.instance.haveEventEmotion)
		{
			if (show && this.CheckWinion_Dialogue_SystemWinion())
			{
				SystemWinion.instance.HaveDialogueEmotion(false);
				return;
			}
		}
		else if (!show)
		{
			winionHandler.ReturnDialogueEmotion(false);
		}
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x000C04F8 File Offset: 0x000BE6F8
	public void SettingHaveEventWinion(bool _haveEvent, WinionHandler winionHandler = null)
	{
		if (winionHandler != null)
		{
			if (_haveEvent)
			{
				winionHandler.haveEvent = true;
				winionHandler.HaveDialogueEmotion(false);
				return;
			}
			winionHandler.haveEvent = false;
			winionHandler.ReturnDialogueEmotion(false);
			return;
		}
		else
		{
			if (_haveEvent)
			{
				this.systemWinion.haveEvent = true;
				SystemWinion.instance.HaveDialogueEmotion(false);
				return;
			}
			this.systemWinion.haveEvent = false;
			SystemWinion.instance.RetrunDialogueEmotion();
			return;
		}
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x000C0560 File Offset: 0x000BE760
	public void ResetAllEmotion()
	{
		WinionHandler winionHandler = this.ION;
		for (int i = 0; i < 5; i++)
		{
			switch (i)
			{
			case 0:
				winionHandler = this.ION;
				break;
			case 1:
				winionHandler = this.Bo;
				break;
			case 2:
				winionHandler = this.Grid;
				break;
			case 3:
				winionHandler = this.Fix;
				break;
			case 4:
				winionHandler = this._Debug;
				break;
			}
			if (winionHandler.haveEventEmotion)
			{
				winionHandler.ReturnDialogueEmotion(false);
			}
			else if (winionHandler.have_smallTalk_Events)
			{
				winionHandler.ReturnDialogueEmotion(true);
			}
		}
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x000C05E8 File Offset: 0x000BE7E8
	public void Capture()
	{
		GameObject gameObjectPrefab = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("PopUpWindow_Capture", "SystemPrefabs/", ScreenCanvas.Instance.popupPos, false);
		gameObjectPrefab.SetActive(true);
		PopUpWindow component = gameObjectPrefab.GetComponent<PopUpWindow>();
		gameObjectPrefab.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		component.Init(PopUpWindow.WhereToPopUp.Capture);
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000C064C File Offset: 0x000BE84C
	public bool CheckWinion_Dialogue(WinionHandler winion)
	{
		if (!winion.worldWinionEnabled && !winion.UIWinionEnabled)
		{
			return false;
		}
		bool flag = false;
		int curEventNum = DBManager.instance.dialogueData.curEventNum;
		int num = DBManager.instance.dialogueData.curEventDetailNum;
		int id = winion.winionStatus.winionInfo.presonality.id;
		int num2 = DBManager.instance.Get_Dialogue_byEvent_Id(curEventNum, num, id);
		if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num2))
		{
			WinionDialogue_ByEvent winionDialogue_ByEvent = DBManager.instance.WinionDialogue_ByEventChapter[num2];
			if (winionDialogue_ByEvent.NextDialogueNum < winionDialogue_ByEvent.event_Dialogues.Count)
			{
				return true;
			}
			if (winionDialogue_ByEvent.repeat)
			{
				return false;
			}
		}
		if (!flag)
		{
			num2 = DBManager.instance.Get_Dialogue_byEvent_Id(curEventNum, -1, id);
			if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num2))
			{
				WinionDialogue_ByEvent winionDialogue_ByEvent2 = DBManager.instance.WinionDialogue_ByEventChapter[num2];
				if (winionDialogue_ByEvent2.NextDialogueNum < winionDialogue_ByEvent2.event_Dialogues.Count)
				{
					return true;
				}
				bool repeat = winionDialogue_ByEvent2.repeat;
				return false;
			}
		}
		return false;
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000C0758 File Offset: 0x000BE958
	public bool CheckWinion_Dialogue_SystemWinion()
	{
		bool flag = false;
		int curEventNum = DBManager.instance.dialogueData.curEventNum;
		int num = DBManager.instance.dialogueData.curEventDetailNum;
		int systemWinionID = SystemWinion.instance.systemWinionID;
		int num2 = DBManager.instance.Get_Dialogue_byEvent_Id(curEventNum, num, systemWinionID);
		if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num2))
		{
			WinionDialogue_ByEvent winionDialogue_ByEvent = DBManager.instance.WinionDialogue_ByEventChapter[num2];
			if (winionDialogue_ByEvent.NextDialogueNum < winionDialogue_ByEvent.event_Dialogues.Count)
			{
				return true;
			}
			if (winionDialogue_ByEvent.repeat)
			{
				return false;
			}
		}
		if (!flag)
		{
			num2 = DBManager.instance.Get_Dialogue_byEvent_Id(curEventNum, -1, systemWinionID);
			if (DBManager.instance.WinionDialogue_ByEventChapter.ContainsKey(num2))
			{
				WinionDialogue_ByEvent winionDialogue_ByEvent2 = DBManager.instance.WinionDialogue_ByEventChapter[num2];
				if (winionDialogue_ByEvent2.NextDialogueNum < winionDialogue_ByEvent2.event_Dialogues.Count)
				{
					return true;
				}
				bool repeat = winionDialogue_ByEvent2.repeat;
				return false;
			}
		}
		return false;
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void Chapter06_go3D()
	{
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void Chapter02_Event22_Debug02(string animationName)
	{
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000C0848 File Offset: 0x000BEA48
	public void Tutorial_CutScene_GlitchON(float film = 0.75f, float chromaticA = 0.75f, float vignette = 0.45f, float pixelation = 0.9f, float LenIntensity = 0.4f, float LenX = 1f, float LenY = 1f, float LenScale = 1.2f, float vhs = 0.5f)
	{
		SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
		this._originFilmIntensity = SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.GetVignette();
		this._originVignetteIntensity = SingletoneBehaviour<GlitchManager>.Instance.vignette.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, film, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetChromaticAberration();
		this._originChromaticAberration = SingletoneBehaviour<GlitchManager>.Instance.chromaticAberration.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 0f, chromaticA, false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, vignette, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetPixelationVol();
		this._originPixelation = SingletoneBehaviour<GlitchManager>.Instance.pixelationVol.m_Scale.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.5f, this._originPixelation, pixelation, false);
		SingletoneBehaviour<GlitchManager>.Instance.LensDistortion_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.intensity.value = LenIntensity;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.xMultiplier.value = LenX;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.yMultiplier.value = LenY;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.scale.value = LenScale;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, vhs);
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000C09CC File Offset: 0x000BEBCC
	public void Tutorial_CutScene_GlitchOFF()
	{
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.1f, this._originFilmIntensity, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.1f, this._originVignetteIntensity, false);
		float num = SingletoneBehaviour<GlitchManager>.Instance.pixelationVol.m_Scale.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.1f, num, this._originPixelation, false);
		num = SingletoneBehaviour<GlitchManager>.Instance.chromaticAberration.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, num, this._originChromaticAberration, false);
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.intensity.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.xMultiplier.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.yMultiplier.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.scale.value = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.1f, 0f);
	}

	// Token: 0x04001646 RID: 5702
	protected EventDialogueController eventDialogueController;

	// Token: 0x04001647 RID: 5703
	protected int curEventDetailNum;

	// Token: 0x04001648 RID: 5704
	protected int eventDialogueNum;

	// Token: 0x04001649 RID: 5705
	[Header("위니언들")]
	public SystemWinion systemWinion;

	// Token: 0x0400164A RID: 5706
	public WinionHandler ION;

	// Token: 0x0400164B RID: 5707
	public WinionHandler Bo;

	// Token: 0x0400164C RID: 5708
	public WinionHandler Grid;

	// Token: 0x0400164D RID: 5709
	public WinionHandler Fix;

	// Token: 0x0400164E RID: 5710
	public WinionHandler _Debug;

	// Token: 0x0400164F RID: 5711
	[Space]
	public bool IONArriveAction;

	// Token: 0x04001650 RID: 5712
	public bool BoArriveAction;

	// Token: 0x04001651 RID: 5713
	public bool GridArriveAction;

	// Token: 0x04001652 RID: 5714
	public bool FixArriveAction;

	// Token: 0x04001653 RID: 5715
	public bool DebugArriveAction;

	// Token: 0x04001654 RID: 5716
	[Space]
	[Space]
	public bool startEvent;

	// Token: 0x04001655 RID: 5717
	public bool isSetting;

	// Token: 0x04001656 RID: 5718
	public bool checkCondition = true;

	// Token: 0x04001657 RID: 5719
	public bool endDialogue;

	// Token: 0x04001658 RID: 5720
	public int eventNum;

	// Token: 0x04001659 RID: 5721
	public bool Init_Complete;

	// Token: 0x0400165A RID: 5722
	public bool setting_Complete;

	// Token: 0x0400165B RID: 5723
	public bool pressIllustrationBtn;

	// Token: 0x0400165C RID: 5724
	public bool getIllustration;

	// Token: 0x0400165D RID: 5725
	public EventIllustration illustration;

	// Token: 0x0400165E RID: 5726
	public bool winionDialogue_upUI;

	// Token: 0x0400165F RID: 5727
	public bool finish3D_Game;

	// Token: 0x04001660 RID: 5728
	public bool awake = true;

	// Token: 0x04001661 RID: 5729
	public bool playStart;

	// Token: 0x04001662 RID: 5730
	public Action openPC;

	// Token: 0x04001663 RID: 5731
	public Action closePC;

	// Token: 0x04001664 RID: 5732
	public static bool blockDialogue;

	// Token: 0x04001665 RID: 5733
	public float _originFilmIntensity;

	// Token: 0x04001666 RID: 5734
	public float _originVignetteIntensity;

	// Token: 0x04001667 RID: 5735
	public float _originChromaticAberration;

	// Token: 0x04001668 RID: 5736
	public float _originPixelation;
}
