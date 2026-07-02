using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class DialogueController : MonoBehaviour
{
	// Token: 0x060009FC RID: 2556 RVA: 0x0004E6B8 File Offset: 0x0004C8B8
	private void Update()
	{
		if (this.autoDialogue_Text.Count > 0 && !this.StopDialogue_Speed && Input.GetMouseButtonDown(1))
		{
			if (this.DebugSpeedDialogue)
			{
				if (!this.StopDialogue_Speed)
				{
					for (int i = 0; i < this.autoDialogue_Text.Count; i++)
					{
						this.autoDialogue_Text[i].text = DBManager.instance.GetSettingString("화면설정", 0, 3, 0);
					}
				}
				this.DebugSpeedDialogue = false;
				return;
			}
			if (!this.DebugSpeedDialogue)
			{
				if (!this.StopDialogue_Speed)
				{
					for (int j = 0; j < this.autoDialogue_Text.Count; j++)
					{
						this.autoDialogue_Text[j].text = DBManager.instance.GetSettingString("화면설정", 0, 4, 0);
					}
				}
				this.DebugSpeedDialogue = true;
			}
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0004E790 File Offset: 0x0004C990
	public void StopSpeedDialogue()
	{
		this.StopDialogue_Speed = true;
		this.DebugSpeedDialogue = false;
		for (int i = 0; i < this.autoDialogue_Text.Count; i++)
		{
			this.autoDialogue_Text[i].text = DBManager.instance.GetSettingString("화면설정", 0, 5, 0);
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
	public void PermissionSpeedDialogue()
	{
		this.StopDialogue_Speed = false;
		this.DebugSpeedDialogue = false;
		if (!this.StopDialogue_Speed)
		{
			for (int i = 0; i < this.autoDialogue_Text.Count; i++)
			{
				this.autoDialogue_Text[i].text = DBManager.instance.GetSettingString("화면설정", 0, 3, 0);
			}
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
	public void ResetSpeedDialogue()
	{
		this.StopDialogue_Speed = false;
		this.DebugSpeedDialogue = false;
		if (!this.StopDialogue_Speed)
		{
			for (int i = 0; i < this.autoDialogue_Text.Count; i++)
			{
				this.autoDialogue_Text[i].text = DBManager.instance.GetSettingString("화면설정", 0, 3, 0);
			}
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0004E840 File Offset: 0x0004CA40
	public Dialogue GetDialogue(WinionHandler winionHandler, bool isSystemWinion = false)
	{
		Dialogue dialogue = null;
		List<float> list = new List<float>();
		if (!isSystemWinion)
		{
			switch (winionHandler.winionStatus.winionInfo.winionType)
			{
			case Winion.Ion:
			{
				List<Dialogue> list2 = DBManager.instance.ION_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.ION_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			case Winion.Bo:
			{
				List<Dialogue> list2 = DBManager.instance.Bo_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.Bo_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			case Winion.Grid:
			{
				List<Dialogue> list2 = DBManager.instance.Grid_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.Grid_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			case Winion.Fix:
			{
				List<Dialogue> list2 = DBManager.instance.Fix_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.Fix_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			case Winion.Debug:
			{
				List<Dialogue> list2 = DBManager.instance.Debug_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.Debug_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			default:
			{
				List<Dialogue> list2 = DBManager.instance.Bo_Dialogue_LoveLevel01.randomDialogue;
				list = DBManager.instance.Bo_Dialogue_LoveLevel01.weight;
				dialogue = this.PickDialogue(list2, list);
				break;
			}
			}
		}
		return dialogue;
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0004E994 File Offset: 0x0004CB94
	private Dialogue PickDialogue(List<Dialogue> dialogues, List<float> dialogueWeights)
	{
		float num = dialogueWeights.Sum();
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		for (int i = 0; i < dialogues.Count; i++)
		{
			num3 += dialogueWeights[i];
			if (num2 < num3)
			{
				int num4 = i;
				dialogueWeights[num4] *= 0.3f;
				return dialogues[i];
			}
		}
		return null;
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0004EA00 File Offset: 0x0004CC00
	public void StartDialogue(WinionHandler winionHandler, bool isAuto = false)
	{
		Dialogue dialogue = this.GetDialogue(winionHandler, false);
		base.StartCoroutine(this.Dialogue(dialogue.d_lines, isAuto, 1.5f, true, null, false));
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0004EA34 File Offset: 0x0004CC34
	public void StartSystemWinionDialogue(bool isAuto = false)
	{
		Dialogue dialogue = this.GetDialogue(null, true);
		base.StartCoroutine(this.Dialogue(dialogue.d_lines, isAuto, 1.5f, true, null, false));
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000143A9 File Offset: 0x000125A9
	public void StartDialogueByEvent(Dialogue dialogue, bool isAuto = false, float autoSpeed = 1.5f, bool useChatAnimation = true, WinionHandler targetHandler = null, bool isRepeat = false)
	{
		base.StartCoroutine(this.Dialogue(dialogue.d_lines, isAuto, autoSpeed, useChatAnimation, targetHandler, isRepeat));
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x000143C6 File Offset: 0x000125C6
	private IEnumerator Dialogue(List<DetailLine> d_lines, bool isAuto = false, float autoSpeed = 1.5f, bool useChatAnimation = true, WinionHandler targetHandler = null, bool isRepeat = false)
	{
		Action action = this.start_Action;
		if (action != null)
		{
			action();
		}
		bool flag = false;
		int backLog_EventsCount = 0;
		int BackLog_Bundle_ListCount = 0;
		if (targetHandler != null)
		{
			DBManager.instance.dialogueData.curSmallTalkWinion = targetHandler.winionStatus.winionInfo.winionType;
			if (targetHandler.whichFolder == Winion.TrashCan)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.TranshCan).GetComponent<UIWindow>().XButton.gameObject.SetActive(false);
			}
		}
		BackLog_Bundle backLog_Bundle = new BackLog_Bundle();
		if (!isRepeat)
		{
			backLog_EventsCount = DBManager.backLog_Events.Count;
			BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
			if (DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count > 0 && DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].isSmallTalk)
			{
				if (this.targetWinion == null)
				{
					if (DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].smallTalk_Winion == SystemWinion.instance.systemWinionID)
					{
						flag = true;
					}
				}
				else if (DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].smallTalk_Winion == this.GetWinionID(this.targetWinion.winionStatus.winionInfo.winionType))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				backLog_Bundle.backLog_Lines = new List<BackLog_Line>();
				backLog_Bundle.isSmallTalk = true;
				if (this.targetWinion == null)
				{
					backLog_Bundle.smallTalk_Winion = SystemWinion.instance.systemWinionID;
				}
				else
				{
					backLog_Bundle.smallTalk_Winion = this.GetWinionID(this.targetWinion.winionStatus.winionInfo.winionType);
				}
				DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Add(backLog_Bundle);
			}
		}
		bool originAuto = isAuto;
		float originSpeed = autoSpeed;
		bool originChatAnim = useChatAnimation;
		bool curFast = false;
		bool useSystemWinionBubble = false;
		ChatController chatController = DBManager.instance.chatController;
		DBManager.instance.dialogueData.curDialogue_ing = true;
		this.curTalkingWinion = null;
		Choice curChoice = null;
		bool useChoice_Option = false;
		bool changeIndex_now = false;
		bool isSystem = false;
		bool isSystemWinion = false;
		DBManager.instance.dialogueData.curEvent.BlockDialogue(true);
		BackLog_Line backLog_Line = new BackLog_Line();
		int i = 0;
		while (i < d_lines.Count)
		{
			DetailLine detailLine = d_lines[i];
			List<Line> lines = this.GetNextLine(detailLine);
			int j = 0;
			while (j < lines.Count)
			{
				GameObject speechBubble = null;
				isSystem = false;
				if (lines[j].nameID == 10)
				{
					if (!SystemWinion.instance.openSystemWinonWindow)
					{
						if (SystemWinion.instance.inSystemWinionRoom)
						{
							SystemWinion.instance.systemWinionBubble_inRoom.SetActive(true);
							SystemWinion.instance.systemWinionBubble_inRoom_text.text = "";
							useSystemWinionBubble = true;
						}
						else
						{
							isSystemWinion = true;
							SystemWinion.instance.systemWinionBubble.SetActive(true);
							SystemWinion.instance.systemWinionBubble_text.text = "";
							useSystemWinionBubble = true;
						}
					}
				}
				else if (lines[j].nameID == 20)
				{
					this.curTalkingWinion = null;
					this.curTalkingWinion = GameManager.instance.gameData.winions[2];
					speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
					speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
				else if (lines[j].nameID == 30)
				{
					this.curTalkingWinion = null;
					isSystem = true;
				}
				else
				{
					this.curTalkingWinion = null;
					this.curTalkingWinion = DBManager.instance.dialogueController.GetWinionHandler(lines[j].nameID);
					speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
					speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
				if (!isRepeat)
				{
					backLog_Line = new BackLog_Line();
					backLog_Line.winionName = DBManager.instance.eventDialogueController.GetWinionName(lines[j].nameID);
					backLog_Line.dialogue = new List<string>();
					backLog_EventsCount = DBManager.backLog_Events.Count;
					BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
					DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
				}
				if (!useSystemWinionBubble && this.curTalkingWinion != null && !this.curTalkingWinion.UIWinionEnabled && this.curTalkingWinion.winionMovement.haveDestination)
				{
					this.curTalkingWinion.winionMovement.SetActiveMovement(false, false, false);
				}
				if (this.curTalkingWinion != null)
				{
					this.curTalkingWinion.dialogue_ing = true;
				}
				int emotionSB_Num = 0;
				int bubbleType_Num = 0;
				int num;
				for (int k = 0; k < lines[j].context.Length; k = num + 1)
				{
					if (speechBubble == null && lines[j].nameID != 10)
					{
						if (!isSystem)
						{
							if (lines[j].haveSpecialBubble)
							{
								if (bubbleType_Num < lines[j].bubbleTypeIndex.Count)
								{
									if (lines[j].bubbleTypeIndex[bubbleType_Num] == k)
									{
										ChatController.BubbleType bubbleType = lines[j].bubbleTypeList[bubbleType_Num];
										speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, bubbleType);
										SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
										speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
										num = bubbleType_Num;
										bubbleType_Num = num + 1;
									}
									else
									{
										speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
										SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
										speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
									}
								}
								else
								{
									speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
									SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
									speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
								}
							}
							else
							{
								speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
								SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
							}
						}
					}
					else if (lines[j].haveSpecialBubble)
					{
						if (bubbleType_Num < lines[j].bubbleTypeIndex.Count)
						{
							if (lines[j].bubbleTypeIndex[bubbleType_Num] == k)
							{
								ChatController.BubbleType bubbleType2 = lines[j].bubbleTypeList[bubbleType_Num];
								SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								if (bubbleType2 != speechBubbleInfo.bubbleType)
								{
									chatController.ReturnSpeechBubble(speechBubble);
									speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, bubbleType2);
									speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
									speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
								}
								num = bubbleType_Num;
								bubbleType_Num = num + 1;
							}
							else
							{
								SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								if (speechBubbleInfo.bubbleType != ChatController.BubbleType.NormalBubble)
								{
									speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
									speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
									speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
								}
							}
						}
						else
						{
							SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
							if (speechBubbleInfo.bubbleType != ChatController.BubbleType.NormalBubble)
							{
								chatController.ReturnSpeechBubble(speechBubble);
								speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
								speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
							}
						}
					}
					if (!curFast && this.DebugSpeedDialogue)
					{
						curFast = true;
						if (!originAuto)
						{
							isAuto = true;
							autoSpeed = this.DebugAutoSpeed;
						}
						else
						{
							useChatAnimation = false;
						}
					}
					if (curFast && !this.DebugSpeedDialogue)
					{
						curFast = false;
						if (!originAuto)
						{
							isAuto = false;
							autoSpeed = originSpeed;
							useChatAnimation = originChatAnim;
						}
					}
					this.endChat = false;
					if (lines[j].nameID == 10 && !SystemWinion.instance.openSystemWinonWindow)
					{
						if (SystemWinion.instance.inSystemWinionRoom)
						{
							SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = true;
							DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_inRoom_text, lines[j].context[k], useChatAnimation, true, isAuto);
						}
						else
						{
							SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = true;
							DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_text, lines[j].context[k], useChatAnimation, true, isAuto);
						}
					}
					else if (lines[j].nameID == 10 && SystemWinion.instance.openSystemWinonWindow)
					{
						if (SystemWinion.instance.systemWinionWindow == null)
						{
							GameObject window = SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinion);
							SystemWinion.instance.systemWinionWindow = window.GetComponent<SystemWinionWindow>();
						}
						SystemWinion.instance.systemWinionWindow.SetConsole(lines[j].context[k], useChatAnimation, isAuto);
					}
					else if (isSystem)
					{
						this.closeSystemBox = false;
						string text = chatController.SettingSentence(lines[j].context[k], false, -1, false).Replace("\r", "");
						GameObject systemBox = SystemBox.Instance.Show(new MessageConfig(text, 500, 250), SystemBox.MessageType.Default, true, 4f, false, true);
						systemBox.GetComponent<DestroyAction>().destroyAction = delegate
						{
							this.closeSystemBox = true;
							systemBox.GetComponent<DestroyAction>().destroyAction = null;
						};
						DBManager.instance.dialogueController.endChat = true;
					}
					else
					{
						chatController.Chat_Obect(speechBubble, lines[j].context[k], useChatAnimation, 0.02f, false, isAuto);
					}
					if (!isRepeat)
					{
						backLog_Line.dialogue.Add(lines[j].context[k]);
						int count = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Count;
						DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines[count - 1] = backLog_Line;
					}
					yield return new WaitUntil(() => this.endChat);
					if (!isAuto && isSystem)
					{
						yield return new WaitUntil(() => (!MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))) || this.closeSystemBox);
						this.closeSystemBox = false;
					}
					if (!isAuto)
					{
						yield return new WaitForSeconds(0.3f);
						yield return new WaitUntil(() => !MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)));
					}
					else
					{
						string text2 = chatController.ReplaceSentence(lines[j].context[k]);
						autoSpeed = this.DebugAutoSpeed + (float)text2.Length * this.OneLetter;
						autoSpeed = Mathf.Clamp(autoSpeed, this.MinSpeed, this.MaxSpeed);
						yield return new WaitForSeconds(autoSpeed);
					}
					if (lines[j].haveEmotionSpeechBubble && !isSystem && emotionSB_Num < lines[j].emotionSpeechBubble_contextIndex.Count && lines[j].emotionSpeechBubble_contextIndex[emotionSB_Num] == k)
					{
						chatController.ReturnSpeechBubble(speechBubble);
						speechBubble = null;
						string text3 = lines[j].emotionSpeechBubble_ID[emotionSB_Num];
						bool flag2 = this.Animation_ByEmotion(text3, this.curTalkingWinion);
						num = emotionSB_Num;
						emotionSB_Num = num + 1;
						if (flag2)
						{
							yield return new WaitForSeconds(2.5f);
						}
					}
					num = k;
				}
				if (SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning)
				{
					SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = false;
					SingletoneBehaviour<SystemWinionConsole>.Instance.ClearConsole();
				}
				if (speechBubble != null)
				{
					chatController.ReturnSpeechBubble(speechBubble);
				}
				else if (speechBubble == null && useSystemWinionBubble)
				{
					if (SystemWinion.instance.inSystemWinionRoom)
					{
						SystemWinion.instance.systemWinionBubble_inRoom.SetActive(false);
					}
					else
					{
						SystemWinion.instance.systemWinionBubble.SetActive(false);
					}
					useSystemWinionBubble = false;
				}
				if (lines[j].haveChoice)
				{
					GameObject choiceObj = DBManager.instance.chatController.GetChoiceWindow(lines[j].choice.onlyOneChoice);
					ChoiceBox_UI choiceBox_UI = choiceObj.GetComponent<ChoiceBox_UI>();
					int num2 = lines[j].context.Length - 1;
					int num3 = 25;
					if (DBManager.instance.eventDialogueController.language == 0)
					{
						num3 = 35;
					}
					string text4 = DBManager.instance.chatController.CutSentence(lines[j].context[num2], num3);
					string text5 = DBManager.instance.chatController.CutSentence_Choice(lines[j].choice.firstOption);
					string text6 = "";
					if (!lines[j].choice.onlyOneChoice)
					{
						text6 = DBManager.instance.chatController.CutSentence_Choice(lines[j].choice.secondOption);
					}
					choiceBox_UI.ButtonSetting(text4, text5, text6, lines[j].choice.onlyOneChoice);
					choiceObj.SetActive(true);
					yield return new WaitForSeconds(0.6f);
					choiceBox_UI.canClick = true;
					yield return new WaitUntil(() => !DBManager.instance.dialogueData.selecting_PlayerOptions);
					choiceBox_UI.CloseSetting();
					choiceObj.SetActive(false);
					if (DBManager.instance.dialogueData.selectOption01)
					{
						if (lines[j].choice.firstOptDialogNum != lines[j].choice.secondOptDialogNum)
						{
							curChoice = lines[j].choice;
							useChoice_Option = true;
						}
						changeIndex_now = true;
						i = lines[j].choice.firstOptDialogNum;
						if (!isRepeat)
						{
							backLog_Line = new BackLog_Line();
							backLog_Line.winionName = "{name}";
							backLog_Line.dialogue = new List<string>();
							backLog_Line.dialogue.Add(lines[j].choice.firstOption);
							backLog_EventsCount = DBManager.backLog_Events.Count;
							BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
							DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
							break;
						}
						break;
					}
					else
					{
						if (!DBManager.instance.dialogueData.selectOption02)
						{
							break;
						}
						changeIndex_now = true;
						i = lines[j].choice.secondOptDialogNum;
						if (!isRepeat)
						{
							backLog_Line = new BackLog_Line();
							backLog_Line.winionName = "{name}";
							backLog_Line.dialogue = new List<string>();
							backLog_Line.dialogue.Add(lines[j].choice.secondOption);
							backLog_EventsCount = DBManager.backLog_Events.Count;
							BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
							DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
							break;
						}
						break;
					}
				}
				else
				{
					if (this.curTalkingWinion != null)
					{
						this.curTalkingWinion.dialogue_ing = false;
						if (!this.curTalkingWinion.UIWinionEnabled && this.curTalkingWinion.winionMovement.haveDestination)
						{
							this.curTalkingWinion.winionMovement.SetActiveMovement(true, false, false);
						}
					}
					speechBubble = null;
					num = j;
					j = num + 1;
				}
			}
			if (changeIndex_now)
			{
				changeIndex_now = false;
			}
			else
			{
				int num = i;
				i = num + 1;
			}
			if (curChoice != null && useChoice_Option && !curChoice.onlyOneChoice && i == curChoice.secondOptDialogNum)
			{
				if (!curChoice.haveCommon)
				{
					break;
				}
				i = curChoice.commonDialogNum;
				curChoice = null;
				useChoice_Option = false;
			}
			lines = null;
		}
		Action action2 = this.finish_Action;
		if (action2 != null)
		{
			action2();
		}
		if (this.settingTalkEmotion)
		{
			if (isSystemWinion)
			{
				isSystemWinion = false;
				this.settingTalkEmotion = false;
				SystemWinion.instance.HaveDialogueEmotion(true);
			}
			else if (this.targetWinion != null)
			{
				this.settingTalkEmotion = false;
				this.targetWinion.HaveDialogueEmotion(true);
				Action settingTalkEmotion_Action = this.SettingTalkEmotion_Action;
				if (settingTalkEmotion_Action != null)
				{
					settingTalkEmotion_Action();
				}
				this.targetWinion = null;
			}
		}
		else if (DBManager.instance.dialogueController.SettingTalkEmotion_Action != null)
		{
			DBManager.instance.dialogueController.SettingTalkEmotion_Action = null;
		}
		if (SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning)
		{
			SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = false;
			SingletoneBehaviour<SystemWinionConsole>.Instance.ClearConsole();
		}
		if (DBManager.instance.dialogueData.smallTalk_ing)
		{
			DBManager.instance.dialogueData.smallTalk_ing = false;
		}
		DBManager.instance.dialogueData.curDialogue_ing = false;
		DBManager.instance.dialogueData.curEvent.BlockDialogue(false);
		if (targetHandler != null)
		{
			DBManager.instance.dialogueData.curSmallTalkWinion = Winion.None;
			if (targetHandler.whichFolder == Winion.TrashCan)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.TranshCan).GetComponent<UIWindow>().XButton.gameObject.SetActive(true);
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00014402 File Offset: 0x00012602
	public List<Line> GetNextLine(DetailLine curDetailLine)
	{
		new List<Line>();
		this.personalityDialogue_ing = false;
		return curDetailLine.lines;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0004EA68 File Offset: 0x0004CC68
	public WinionHandler GetWinionHandler(int id)
	{
		WinionHandler winionHandler = null;
		switch (id)
		{
		case 0:
			winionHandler = GameManager.instance.gameData.ION;
			break;
		case 1:
			winionHandler = GameManager.instance.gameData.Bo;
			break;
		case 2:
			winionHandler = GameManager.instance.gameData.Grid;
			break;
		case 3:
			winionHandler = GameManager.instance.gameData.Fix;
			break;
		case 4:
			winionHandler = GameManager.instance.gameData.Debug;
			break;
		case 5:
			winionHandler = GameManager.instance.gameData.Debug_0;
			break;
		case 11:
			winionHandler = GameManager.instance.gameData.Fix;
			break;
		case 12:
			winionHandler = GameManager.instance.gameData.ION;
			break;
		case 13:
			winionHandler = GameManager.instance.gameData.Fix;
			break;
		case 14:
			winionHandler = GameManager.instance.gameData.Debug;
			break;
		case 15:
			winionHandler = GameManager.instance.gameData.Debug;
			break;
		case 16:
			winionHandler = GameManager.instance.gameData.Fix;
			break;
		case 17:
			winionHandler = GameManager.instance.gameData.Bo;
			break;
		case 18:
			winionHandler = GameManager.instance.gameData.ION;
			break;
		}
		return winionHandler;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0004EBDC File Offset: 0x0004CDDC
	public WinionHandler GetWinion_byCondition(int condition)
	{
		WinionHandler winionHandler = null;
		switch (condition)
		{
		case 1:
			winionHandler = GameManager.instance.gameData.winions[1];
			break;
		case 2:
			winionHandler = GameManager.instance.gameData.winions[1];
			break;
		case 3:
			winionHandler = GameManager.instance.gameData.winions[1];
			break;
		case 4:
			winionHandler = GameManager.instance.gameData.winions[1];
			break;
		}
		winionHandler == null;
		return winionHandler;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0004EC70 File Offset: 0x0004CE70
	public bool Animation_ByEmotion(string emotion, WinionHandler curWinion)
	{
		bool flag = false;
		if (curWinion.winionAnimator.canChangeAnimation)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(emotion);
			if (num <= 1892855261U)
			{
				if (num <= 815280957U)
				{
					if (num <= 299880153U)
					{
						if (num != 201270621U)
						{
							if (num == 299880153U)
							{
								if (!(emotion == "{우히히}"))
								{
								}
							}
						}
						else if (!(emotion == "{나이뻐}"))
						{
						}
					}
					else if (num != 390279577U)
					{
						if (num == 815280957U)
						{
							if (!(emotion == "{반짝반짝}"))
							{
							}
						}
					}
					else if (emotion == "{눈물}")
					{
						flag = true;
						curWinion.winionAnimator.PlayAnimation("Cry", false);
					}
				}
				else if (num <= 1792803225U)
				{
					if (num != 1094323117U)
					{
						if (num == 1792803225U)
						{
							if (!(emotion == "{느낌표}"))
							{
							}
						}
					}
					else if (!(emotion == "{기분 나빠}"))
					{
					}
				}
				else if (num != 1819490413U)
				{
					if (num == 1892855261U)
					{
						if (!(emotion == "{편지}"))
						{
						}
					}
				}
				else if (!(emotion == "{애정 업}"))
				{
				}
			}
			else if (num <= 2073664585U)
			{
				if (num <= 1952932825U)
				{
					if (num != 1911330474U)
					{
						if (num == 1952932825U)
						{
							if (!(emotion == "{좌절}"))
							{
							}
						}
					}
					else if (!(emotion == "{애정 다운}"))
					{
					}
				}
				else if (num != 2010738123U)
				{
					if (num == 2073664585U)
					{
						if (!(emotion == "{나 할말있어}"))
						{
						}
					}
				}
				else if (!(emotion == "{행복}"))
				{
				}
			}
			else if (num <= 2605631725U)
			{
				if (num != 2286645616U)
				{
					if (num == 2605631725U)
					{
						if (!(emotion == "{부끄러움}"))
						{
						}
					}
				}
				else if (!(emotion == "{눈껌뻑}"))
				{
				}
			}
			else if (num != 2643319536U)
			{
				if (num != 3475316093U)
				{
					if (num == 3740787401U)
					{
						if (!(emotion == "{잠}"))
						{
						}
					}
				}
				else if (!(emotion == "{우울}"))
				{
				}
			}
			else if (!(emotion == "{똥}"))
			{
			}
		}
		return flag;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00014417 File Offset: 0x00012617
	public void StartDialoguelineByBehaviors(LineByBehavior lineByBehaviors)
	{
		base.StartCoroutine(this.Dialogue_lineByBehaviors(lineByBehaviors, true));
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00014428 File Offset: 0x00012628
	private IEnumerator Dialogue_lineByBehaviors(LineByBehavior lineByBehavior, bool isAuto = true)
	{
		ChatController chatController = DBManager.instance.chatController;
		DBManager.instance.dialogueData.curDialogue_ing = true;
		bool useSystemWinionBubble = false;
		this.curTalkingWinion = null;
		GameObject speechBubble = null;
		bool isSystem = false;
		if (lineByBehavior.nameID == 10)
		{
			this.curTalkingWinion = null;
			if (!SystemWinion.instance.openSystemWinonWindow)
			{
				if (SystemWinion.instance.inSystemWinionRoom)
				{
					SystemWinion.instance.systemWinionBubble_inRoom.SetActive(true);
					SystemWinion.instance.systemWinionBubble_inRoom_text.text = "";
				}
				else
				{
					SystemWinion.instance.systemWinionBubble.SetActive(true);
					SystemWinion.instance.systemWinionBubble_text.text = "";
				}
			}
		}
		else if (lineByBehavior.nameID == 20)
		{
			this.curTalkingWinion = null;
			this.curTalkingWinion = GameManager.instance.gameData.winions[2];
			speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
			speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		}
		else if (lineByBehavior.nameID == 30)
		{
			this.curTalkingWinion = null;
			isSystem = true;
		}
		else
		{
			this.curTalkingWinion = null;
			this.curTalkingWinion = DBManager.instance.dialogueController.GetWinionHandler(lineByBehavior.nameID);
			speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
			speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		}
		if (!isSystem && !this.curTalkingWinion.UIWinionEnabled && this.curTalkingWinion.winionMovement.haveDestination)
		{
			this.curTalkingWinion.winionMovement.SetActiveMovement(false, false, false);
		}
		int emotionSB_Num = 0;
		int bubbleType_Num = 0;
		int num;
		for (int i = 0; i < lineByBehavior.context.Length; i = num + 1)
		{
			if (speechBubble == null && lineByBehavior.nameID != 10)
			{
				if (!isSystem)
				{
					if (lineByBehavior.haveSpecialBubble)
					{
						if (bubbleType_Num < lineByBehavior.bubbleTypeIndex.Count)
						{
							if (lineByBehavior.bubbleTypeIndex[bubbleType_Num] == i)
							{
								ChatController.BubbleType bubbleType = lineByBehavior.bubbleTypeList[bubbleType_Num];
								speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, bubbleType);
								SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
							}
							num = bubbleType_Num;
							bubbleType_Num = num + 1;
						}
						else
						{
							speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
							SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
							speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
						}
					}
					else
					{
						speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
						SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
						speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
					}
				}
			}
			else if (lineByBehavior.haveSpecialBubble)
			{
				if (bubbleType_Num < lineByBehavior.bubbleTypeIndex.Count)
				{
					if (lineByBehavior.bubbleTypeIndex[bubbleType_Num] == i)
					{
						ChatController.BubbleType bubbleType2 = lineByBehavior.bubbleTypeList[bubbleType_Num];
						SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
						if (bubbleType2 != speechBubbleInfo.bubbleType)
						{
							chatController.ReturnSpeechBubble(speechBubble);
							speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, bubbleType2);
							speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
							speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
						}
						num = bubbleType_Num;
						bubbleType_Num = num + 1;
					}
				}
				else
				{
					SpeechBubbleInfo speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
					if (speechBubbleInfo.bubbleType != ChatController.BubbleType.NormalBubble)
					{
						chatController.ReturnSpeechBubble(speechBubble);
						speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
						speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
						speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
					}
				}
			}
			this.endChat = false;
			if (lineByBehavior.nameID == 10 && !SystemWinion.instance.openSystemWinonWindow)
			{
				if (SystemWinion.instance.inSystemWinionRoom)
				{
					DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_inRoom_text, lineByBehavior.context[i], true, true, isAuto);
				}
				else
				{
					DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_text, lineByBehavior.context[i], true, true, isAuto);
				}
			}
			else if (lineByBehavior.nameID == 10 && SystemWinion.instance.openSystemWinonWindow)
			{
				if (SystemWinion.instance.systemWinionWindow == null)
				{
					GameObject window = SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.SystemWinion);
					SystemWinion.instance.systemWinionWindow = window.GetComponent<SystemWinionWindow>();
				}
				SystemWinion.instance.systemWinionWindow.SetConsole(lineByBehavior.context[i], true, false);
			}
			else if (isSystem)
			{
				SystemBox.Instance.Show(new MessageConfig(lineByBehavior.context[i]), SystemBox.MessageType.Default, false, 4f, false, true);
				DBManager.instance.dialogueController.endChat = true;
			}
			else
			{
				chatController.Chat_Obect(speechBubble, lineByBehavior.context[i], true, 0.02f, false, false);
			}
			yield return new WaitUntil(() => this.endChat);
			if (!isAuto)
			{
				yield return new WaitForSeconds(0.3f);
				yield return new WaitUntil(() => !MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)));
			}
			else if (!isSystem)
			{
				yield return new WaitForSeconds(1.5f);
			}
			if (lineByBehavior.haveEmotionSpeechBubble && !isSystem && emotionSB_Num < lineByBehavior.emotionSpeechBubble_contextIndex.Count && lineByBehavior.emotionSpeechBubble_contextIndex[emotionSB_Num] == i)
			{
				chatController.ReturnSpeechBubble(speechBubble);
				speechBubble = null;
				string text = lineByBehavior.emotionSpeechBubble_ID[emotionSB_Num];
				bool flag = this.Animation_ByEmotion(text, this.curTalkingWinion);
				num = emotionSB_Num;
				emotionSB_Num = num + 1;
				if (flag)
				{
					yield return new WaitForSeconds(2.5f);
				}
			}
			num = i;
		}
		if (SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning)
		{
			SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = false;
			SingletoneBehaviour<SystemWinionConsole>.Instance.ClearConsole();
		}
		if (speechBubble != null)
		{
			chatController.ReturnSpeechBubble(speechBubble);
		}
		else if (speechBubble == null && useSystemWinionBubble)
		{
			if (SystemWinion.instance.inSystemWinionRoom)
			{
				SystemWinion.instance.systemWinionBubble_inRoom.SetActive(false);
			}
			else
			{
				SystemWinion.instance.systemWinionBubble.SetActive(false);
			}
			useSystemWinionBubble = false;
		}
		if (this.curTalkingWinion != null)
		{
			this.curTalkingWinion.dialogue_ing = false;
			if (!this.curTalkingWinion.UIWinionEnabled && this.curTalkingWinion.winionMovement.haveDestination)
			{
				this.curTalkingWinion.winionMovement.SetActiveMovement(true, false, false);
			}
		}
		DBManager.instance.dialogueData.curDialogue_ing = false;
		if (this.curTalkingWinion != null)
		{
			this.curTalkingWinion.dialogue_ing = false;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00014445 File Offset: 0x00012645
	public bool CanPlayingDialogue_3D()
	{
		return this.startNextDialogue_3D_co == null;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0004EEFC File Offset: 0x0004D0FC
	public void StartNextDialogue_3D()
	{
		if (this.startNextDialogue_3D_co == null)
		{
			DetailLine_3D detailLine_3D = new DetailLine_3D();
			if (HorrorSceneManager.GameNum == 0)
			{
				detailLine_3D = DBManager.instance.Dialogue_3D[0].detailLine_3D[HorrorSceneManager.dialogueNum];
				HorrorSceneManager.dialogueNum++;
			}
			else if (HorrorSceneManager.GameNum == 1)
			{
				detailLine_3D = DBManager.instance.Dialogue_3D[1].detailLine_3D[HorrorSceneManager.dialogueNum];
				HorrorSceneManager.dialogueNum++;
			}
			this.startNextDialogue_3D_co = base.StartCoroutine(this.StartNextDialogue_3D_co(detailLine_3D));
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00014452 File Offset: 0x00012652
	private IEnumerator StartNextDialogue_3D_co(DetailLine_3D detailLine)
	{
		ChatController chatController = DBManager.instance.chatController;
		GameObject speechBubble = null;
		speechBubble = chatController.GetSpeechBubble_MyPC_3DGame(ChatController.BubbleType.NormalBubble);
		speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		DialogueController.Is3DChat = true;
		int changeFace_Num = 0;
		int backLog_EventsCount = 0;
		int BackLog_Bundle_ListCount = 0;
		BackLog_Bundle backLog_Bundle = new BackLog_Bundle();
		backLog_Bundle.backLog_Lines = new List<BackLog_Line>();
		backLog_EventsCount = DBManager.backLog_Events.Count;
		DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Add(backLog_Bundle);
		BackLog_Line backLog_Line = new BackLog_Line();
		foreach (Line_3D line in detailLine.lines_3D)
		{
			if (speechBubble != null)
			{
				chatController.ReturnSpeechBubble(speechBubble);
			}
			backLog_Line = new BackLog_Line();
			int num;
			if (line.winionNum == 1)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
			backLog_Line.winionName = DBManager.instance.eventDialogueController.GetWinionName(num);
			backLog_Line.dialogue = new List<string>();
			backLog_EventsCount = DBManager.backLog_Events.Count;
			BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
			DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
			int num3;
			for (int i = 0; i < line.dialogue_List.Count; i = num3 + 1)
			{
				if (speechBubble == null)
				{
					speechBubble = chatController.GetSpeechBubble_MyPC_3DGame(ChatController.BubbleType.NormalBubble);
					speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
				this.endChat = false;
				if (line.changeFace && changeFace_Num < line.changeExpression_Index.Count && line.changeExpression_Index[changeFace_Num] == i)
				{
					for (int j = 0; j < line.changeExpression[changeFace_Num].Count; j++)
					{
						DBManager.instance.winionFaceInfo.SettingFaceWindow_3DGame(SingletoneBehaviour<MyPcWindowResolution>.Instance.FaceImage, line.changeExpression[changeFace_Num][j]);
					}
				}
				if (line.winionNum == 1)
				{
					DBManager.instance.chatController.Chat_Obect_TmpText(SingletoneBehaviour<HorrorSceneManager>.Instance.gridText, line.dialogue_List[i], true, true, false);
				}
				else
				{
					DBManager.instance.chatController.Chat_Obect_TmpText(SingletoneBehaviour<HorrorSceneManager>.Instance.boText, line.dialogue_List[i], true, true, false);
				}
				backLog_Line.dialogue.Add(line.dialogue_List[i]);
				int count = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Count;
				DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines[count - 1] = backLog_Line;
				yield return new WaitUntil(() => this.endChat);
				float num2 = (float)SingletoneBehaviour<HorrorSceneManager>.Instance.gridText.text.Length * 0.03f;
				yield return new WaitForSeconds(1.5f + num2);
				if (line.winionNum == 1)
				{
					SingletoneBehaviour<HorrorSceneManager>.Instance.gridText.text = "";
				}
				else
				{
					SingletoneBehaviour<HorrorSceneManager>.Instance.boText.text = "";
				}
				num3 = i;
			}
			line = null;
		}
		List<Line_3D>.Enumerator enumerator = default(List<Line_3D>.Enumerator);
		DialogueController.Is3DChat = false;
		if (speechBubble != null)
		{
			chatController.ReturnSpeechBubble(speechBubble);
		}
		this.startNextDialogue_3D_co = null;
		yield return null;
		yield break;
		yield break;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00014468 File Offset: 0x00012668
	public void StopDialogue()
	{
		if (this.TestDialogue != null)
		{
			this.stopTestDialogue = true;
			if (!this.endChat)
			{
				DBManager.instance.chatController.stopChat = true;
			}
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0004EF98 File Offset: 0x0004D198
	public void TextDialouge(int index = 0, GameObject speechBubble = null, TMP_Text speechBubble_Text = null)
	{
		if (DBManager.instance.Setting_Conversations.detailLine_3D.Count == 0)
		{
			DBManager.instance.ConversationsSetting();
		}
		if (this.TestDialogue == null)
		{
			this.TestDialogue = base.StartCoroutine(this.StartNextDialogue_Setting_co(DBManager.instance.Setting_Conversations.detailLine_3D[index], speechBubble, speechBubble_Text));
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00014491 File Offset: 0x00012691
	private IEnumerator StartNextDialogue_Setting_co(DetailLine_3D detailLine, GameObject speechBubble, TMP_Text speechBubble_Text)
	{
		ChatController chatController = DBManager.instance.chatController;
		speechBubble.SetActive(true);
		speechBubble_Text.text = "";
		foreach (Line_3D line in detailLine.lines_3D)
		{
			int i = 0;
			while (i < line.dialogue_List.Count && !this.stopTestDialogue)
			{
				this.endChat = false;
				DBManager.instance.chatController.Chat_Obect(speechBubble, line.dialogue_List[i], !SingletoneBehaviour<DialogueOption>.Instance.showAllConversation.isOn, SingletoneBehaviour<DialogueOption>.Instance.curSpeed, true, false);
				yield return new WaitUntil(() => this.endChat);
				yield return new WaitForSeconds(0.3f);
				yield return new WaitUntil(() => !MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32) || this.stopTestDialogue));
				speechBubble_Text.text = "";
				if (this.stopTestDialogue)
				{
					break;
				}
				int num = i;
				i = num + 1;
			}
			if (this.stopTestDialogue)
			{
				break;
			}
			line = null;
		}
		List<Line_3D>.Enumerator enumerator = default(List<Line_3D>.Enumerator);
		DialogueController.Is3DChat = false;
		speechBubble.SetActive(false);
		this.TestDialogue = null;
		this.stopTestDialogue = false;
		yield return null;
		yield break;
		yield break;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00042594 File Offset: 0x00040794
	public int GetWinionID(Winion winion)
	{
		int num = 0;
		switch (winion)
		{
		case Winion.Ion:
			num = 0;
			break;
		case Winion.Bo:
			num = 1;
			break;
		case Winion.Grid:
			num = 2;
			break;
		case Winion.Fix:
			num = 3;
			break;
		case Winion.Debug:
			num = 4;
			break;
		}
		return num;
	}

	// Token: 0x04000B1D RID: 2845
	public bool endChat;

	// Token: 0x04000B1E RID: 2846
	public bool personalityDialogue_ing;

	// Token: 0x04000B1F RID: 2847
	public int personalityDialogueNum;

	// Token: 0x04000B20 RID: 2848
	private WinionHandler curTalkingWinion;

	// Token: 0x04000B21 RID: 2849
	public bool DebugSpeedDialogue;

	// Token: 0x04000B22 RID: 2850
	public float DebugAutoSpeed = 0.5f;

	// Token: 0x04000B23 RID: 2851
	public float OneLetter = 0.02f;

	// Token: 0x04000B24 RID: 2852
	public float MinSpeed = 0.5f;

	// Token: 0x04000B25 RID: 2853
	public float MaxSpeed = 0.85f;

	// Token: 0x04000B26 RID: 2854
	public GameObject speedText;

	// Token: 0x04000B27 RID: 2855
	public bool StopDialogue_Speed;

	// Token: 0x04000B28 RID: 2856
	public List<TMP_Text> autoDialogue_Text;

	// Token: 0x04000B29 RID: 2857
	public bool settingTalkEmotion;

	// Token: 0x04000B2A RID: 2858
	public WinionHandler targetWinion;

	// Token: 0x04000B2B RID: 2859
	public Action SettingTalkEmotion_Action;

	// Token: 0x04000B2C RID: 2860
	public Action start_Action;

	// Token: 0x04000B2D RID: 2861
	public Action finish_Action;

	// Token: 0x04000B2E RID: 2862
	private bool closeSystemBox;

	// Token: 0x04000B2F RID: 2863
	private Coroutine startNextDialogue_3D_co;

	// Token: 0x04000B30 RID: 2864
	public static bool Is3DChat;

	// Token: 0x04000B31 RID: 2865
	private Coroutine TestDialogue;

	// Token: 0x04000B32 RID: 2866
	private bool stopTestDialogue;
}
