using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class EventDialogueController : MonoBehaviour
{
	// Token: 0x06001A5C RID: 6748 RVA: 0x000C0BD0 File Offset: 0x000BEDD0
	public void StartEvent(int curEventNum)
	{
		if (DBManager.instance.curChapter_EventDialogue != GameManager.instance.gameData.curChapter)
		{
			DBManager.instance.ChangeDialogueScript(GameManager.instance.gameData.curChapter);
		}
		DBManager.instance.dialogueData.curEventNum = curEventNum;
		if (!DBManager.instance.dialogueData.isDebug)
		{
			if (EventDialogueController.CurEventDetailNumIs3D)
			{
				DBManager.instance.dialogueData.curEventDetailNum = 5;
				EventDialogueController.CurEventDetailNumIs3D = false;
			}
			else
			{
				DBManager.instance.dialogueData.curEventDetailNum = 0;
			}
		}
		else if (EventDialogueController.CurEventDetailNumIs3D)
		{
			DBManager.instance.dialogueData.curEventDetailNum = 5;
			EventDialogueController.CurEventDetailNumIs3D = false;
		}
		else
		{
			DBManager.instance.dialogueData.isDebug = false;
		}
		this.nextEventNum = DBManager.instance.dialogueData.curEventNum;
		this.language = PlayerPrefs.GetInt("Language", 0);
		ScreenCanvas.Instance.fixFolderInteraction.WinionFixRoomSetting();
		ScreenCanvas.Instance.ionFolderInteraction.WinionIONRoomSetting();
		ScreenCanvas.Instance.boFolderInteraction.WinionBoRoomSetting();
		ScreenCanvas.Instance.debugFolderInteraction.WinionDebugRoomSetting();
		ScreenCanvas.Instance.gridFolderInteraction.WinionGridRoomSetting();
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial)
		{
			SystemWinion.instance.isVirus = false;
		}
		else
		{
			SystemWinion.instance.isVirus = true;
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01 && curEventNum >= 7)
		{
			GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus = true;
		}
		else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && curEventNum < 13)
		{
			GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus = true;
		}
		else
		{
			GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus = false;
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && curEventNum > 3 && curEventNum < 11)
		{
			GameManager.instance.gameData.ION.winionAnimator.winionEmptiness = true;
			GameManager.instance.gameData.ION.winionAnimator.PlayAnimation("FrontIdle", false);
		}
		else
		{
			GameManager.instance.gameData.ION.winionAnimator.winionEmptiness = false;
			GameManager.instance.gameData.ION.winionAnimator.PlayAnimation("FrontIdle", false);
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 && curEventNum < 11)
		{
			GameManager.instance.gameData.Debug.winionAnimator.debug_bright = true;
			GameManager.instance.gameData.Debug.winionAnimator.PlayAnimation("FrontIdle", false);
		}
		else
		{
			GameManager.instance.gameData.Debug.winionAnimator.debug_bright = false;
			GameManager.instance.gameData.Debug.winionAnimator.PlayAnimation("FrontIdle", false);
		}
		if (!DBManager.GotoIngame)
		{
			DBManager.GotoIngame = true;
		}
		BackLog_Event backLog_Event = new BackLog_Event();
		backLog_Event.chapter = GameManager.instance.gameData.curChapter;
		backLog_Event.eventID = curEventNum;
		backLog_Event.BackLog_Bundle_List = new List<BackLog_Bundle>();
		bool flag = false;
		if (DBManager.backLog_Events.Count != 0 && curEventNum == DBManager.backLog_Events[DBManager.backLog_Events.Count - 1].eventID && GameManager.instance.gameData.curChapter == DBManager.backLog_Events[DBManager.backLog_Events.Count - 1].chapter)
		{
			DBManager.backLog_Events[DBManager.backLog_Events.Count - 1] = backLog_Event;
			flag = true;
		}
		if (!flag)
		{
			DBManager.backLog_Events.Add(backLog_Event);
		}
		DBManager.instance.dialogueData.runNextEvent = true;
		this.curDialogue = this.GetEventDialogue();
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x000C0FBC File Offset: 0x000BF1BC
	public void FinishEvent()
	{
		if (DBManager.instance.dialogueData.finishEvent)
		{
			DBManager.instance.dialogueData.curEventDetailNum = 0;
			this.nextEventNum = DBManager.instance.dialogueData.curEventNum;
			DBManager.instance.dialogueData.runNextEvent = false;
			DBManager.instance.dialogueData.finishEvent = false;
		}
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000C1020 File Offset: 0x000BF220
	public Dialogue GetEventDialogue()
	{
		Dialogue dialogue = new Dialogue();
		if (DBManager.instance.dialogueData.runNextEvent)
		{
			int curEventNum = DBManager.instance.dialogueData.curEventNum;
			EventDialogue eventDialogue = null;
			switch (GameManager.instance.gameData.curChapter)
			{
			case GameManager.Chapter.chapter00:
				eventDialogue = DBManager.instance.Chapter_EventDialogue[curEventNum];
				break;
			case GameManager.Chapter.chapter01:
				eventDialogue = DBManager.instance.Chapter_EventDialogue[curEventNum];
				break;
			case GameManager.Chapter.chapter02:
				eventDialogue = DBManager.instance.Chapter_EventDialogue[curEventNum];
				break;
			case GameManager.Chapter.chapter03:
				eventDialogue = DBManager.instance.Chapter_EventDialogue[curEventNum];
				break;
			}
			if (eventDialogue != null)
			{
				int curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
				dialogue = eventDialogue.event_Dialogues[curEventDetailNum];
			}
		}
		return dialogue;
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x000C10F0 File Offset: 0x000BF2F0
	public int GetEventDialogueNum()
	{
		int num = 0;
		if (DBManager.instance.dialogueData.runNextEvent)
		{
			int curEventNum = DBManager.instance.dialogueData.curEventNum;
			EventDialogue eventDialogue = DBManager.instance.Chapter_EventDialogue[curEventNum];
			if (eventDialogue != null)
			{
				num = eventDialogue.event_Dialogues.Count;
			}
		}
		return num;
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000190B4 File Offset: 0x000172B4
	public List<DetailLine> GetNextDetailLine()
	{
		this.curDialogue = this.GetEventDialogue();
		return this.curDialogue.d_lines;
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x000C1144 File Offset: 0x000BF344
	public void StartNextDialogue(bool isAuto = false, float autoSpeed = 1.5f, bool useChatAnimation = true)
	{
		DBManager.instance.dialogueData.curEvent.endDialogue = false;
		List<DetailLine> nextDetailLine = this.GetNextDetailLine();
		base.StartCoroutine(this.EventDialogue(nextDetailLine, isAuto, autoSpeed, useChatAnimation));
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000190CD File Offset: 0x000172CD
	private IEnumerator EventDialogue(List<DetailLine> d_lines, bool isAuto = false, float autoSpeed = 1.5f, bool useChatAnimation = true)
	{
		int backLog_EventsCount = 0;
		int BackLog_Bundle_ListCount = 0;
		if (this.makeBackLog_Bundle)
		{
			this.makeBackLog_Bundle = false;
			BackLog_Bundle backLog_Bundle = new BackLog_Bundle();
			backLog_Bundle.backLog_Lines = new List<BackLog_Line>();
			backLog_EventsCount = DBManager.backLog_Events.Count;
			DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Add(backLog_Bundle);
		}
		bool originAuto = isAuto;
		float originSpeed = autoSpeed;
		bool originChatAnim = useChatAnimation;
		bool curFast = false;
		bool useSystemWinionBubble = false;
		ChatController chatController = DBManager.instance.chatController;
		DBManager.instance.dialogueData.curDialogue_ing = true;
		SingletoneBehaviour<WinionFolderManager>.Instance.CanMoveFolder = false;
		bool changeIndex_now = false;
		bool useChoice_Option = false;
		Choice curDialogueChoice = null;
		bool isSystem = false;
		bool isLine = false;
		if (DBManager.instance.dialogueData.stopDialogue)
		{
			yield return new WaitUntil(() => !DBManager.instance.dialogueData.stopDialogue);
		}
		BackLog_Line backLog_Line = new BackLog_Line();
		int i = 0;
		while (i < d_lines.Count)
		{
			List<Line> lines = d_lines[i].lines;
			int j = 0;
			while (j < lines.Count)
			{
				GameObject speechBubble = null;
				isSystem = false;
				if (lines[j].nameID == 10)
				{
					this.curTalkingWinion = null;
					if (!SystemWinion.instance.openSystemWinonWindow)
					{
						if (SystemWinion.instance.inSystemWinionRoom)
						{
							SystemWinion.instance.systemWinionBubble_inRoom.SetActive(true);
							SystemWinion.instance.systemWinionBubble_inRoom_text.text = "";
							useSystemWinionBubble = true;
						}
						else if (SystemWinion.instance.systemWinionLastLine)
						{
							SystemWinion.instance.systemWinionBubble_LastLine.SetActive(true);
							SystemWinion.instance.systemWinionBubble_LastLine_text.text = "";
							useSystemWinionBubble = true;
						}
						else
						{
							SystemWinion.instance.systemWinionBubble.SetActive(true);
							SystemWinion.instance.systemWinionBubble_text.text = "";
							useSystemWinionBubble = true;
						}
					}
				}
				else if (lines[j].nameID == 30)
				{
					this.curTalkingWinion = null;
					isSystem = true;
				}
				else if (lines[j].nameID == 20)
				{
					this.curTalkingWinion = null;
					this.line_Text.gameObject.SetActive(true);
					this.line_Text.text = "";
					isLine = true;
				}
				else
				{
					this.curTalkingWinion = null;
					this.curTalkingWinion = DBManager.instance.dialogueController.GetWinionHandler(lines[j].nameID);
					switch (lines[j].nameID)
					{
					case 0:
						DBManager.instance.dialogueData.curDialogue_Winion = Winion.Ion;
						break;
					case 1:
						DBManager.instance.dialogueData.curDialogue_Winion = Winion.Bo;
						break;
					case 2:
						DBManager.instance.dialogueData.curDialogue_Winion = Winion.Grid;
						break;
					case 3:
						DBManager.instance.dialogueData.curDialogue_Winion = Winion.Fix;
						break;
					case 4:
						DBManager.instance.dialogueData.curDialogue_Winion = Winion.Debug;
						break;
					}
					if (this.curTalkingWinion.specialBubblePos)
					{
						if (lines[j].nameID == 3 && DBManager.instance.dialogueData.curEventNum == 10)
						{
							speechBubble = chatController.GetSpeechBubble_chapter02Event10_Fix(ChatController.BubbleType.NormalBubble);
						}
					}
					else
					{
						speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
					}
					speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
				backLog_Line = new BackLog_Line();
				backLog_Line.winionName = this.GetWinionName(lines[j].nameID);
				backLog_Line.dialogue = new List<string>();
				backLog_EventsCount = DBManager.backLog_Events.Count;
				BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
				DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
				int emotionSB_Num = 0;
				int bubbleType_Num = 0;
				int changeFace_Num = 0;
				int num;
				for (int k = 0; k < lines[j].context.Length; k = num + 1)
				{
					if (speechBubble == null && lines[j].nameID != 10)
					{
						if ((lines[j].nameID != 10 || !SystemWinion.instance.openSystemWinonWindow) && !isSystem && !isLine)
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
									if (lines[j].nameID != 10)
									{
										speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, bubbleType2);
									}
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
									chatController.ReturnSpeechBubble(speechBubble);
									if (lines[j].nameID != 10)
									{
										speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
									}
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
								if (lines[j].nameID != 10)
								{
									speechBubble = chatController.GetSpeechBubble(this.curTalkingWinion, ChatController.BubbleType.NormalBubble);
								}
								speechBubbleInfo = speechBubble.GetComponent<SpeechBubbleInfo>();
								speechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
							}
						}
					}
					if (!curFast && DBManager.instance.dialogueController.DebugSpeedDialogue)
					{
						curFast = true;
						if (!originAuto)
						{
							isAuto = true;
							autoSpeed = DBManager.instance.dialogueController.DebugAutoSpeed;
						}
					}
					if (curFast && !DBManager.instance.dialogueController.DebugSpeedDialogue)
					{
						curFast = false;
						if (!originAuto)
						{
							isAuto = false;
							autoSpeed = originSpeed;
							useChatAnimation = originChatAnim;
						}
					}
					if (lines[j].changeFace && changeFace_Num < lines[j].changeExpression_Index.Count && lines[j].changeExpression_Index[changeFace_Num] == k)
					{
						for (int l = 0; l < lines[j].changeExpression[changeFace_Num].Count; l++)
						{
							DBManager.instance.winionFaceInfo.SettingFaceWindow(lines[j].changeExpression[changeFace_Num][l]);
						}
						num = changeFace_Num;
						changeFace_Num = num + 1;
					}
					DBManager.instance.dialogueController.endChat = false;
					if (lines[j].nameID == 10 && !SystemWinion.instance.openSystemWinonWindow)
					{
						if (SystemWinion.instance.inSystemWinionRoom)
						{
							DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_inRoom_text, lines[j].context[k], true, useChatAnimation, isAuto);
						}
						else if (SystemWinion.instance.systemWinionLastLine)
						{
							DBManager.instance.chatController.Chat_Obect_TmpText_ChangeSpeed(SystemWinion.instance.systemWinionBubble_LastLine_text, lines[j].context[k], false, false, 0.075f);
						}
						else
						{
							SingletoneBehaviour<SystemWinionConsole>.Instance.isRunning = true;
							DBManager.instance.chatController.Chat_Obect_TmpText(SystemWinion.instance.systemWinionBubble_text, lines[j].context[k], true, useChatAnimation, isAuto);
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
					else if (isLine)
					{
						DBManager.instance.chatController.Chat_Obect_TmpText_ChangeSpeed(this.line_Text, lines[j].context[k], false, false, 0.05f);
					}
					else
					{
						chatController.Chat_Obect(speechBubble, lines[j].context[k], useChatAnimation, 0.02f, false, isAuto);
					}
					backLog_Line.dialogue.Add(lines[j].context[k]);
					int count = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Count;
					DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines[count - 1] = backLog_Line;
					if (this.curTalkingWinion != null && !this.curTalkingWinion.worldWinionEnabled && this.curTalkingWinion.UIWinionEnabled)
					{
						Icon icon = Icon.None;
						switch (this.curTalkingWinion.whichFolder)
						{
						case Winion.Ion:
							icon = Icon.Folder_Ion;
							break;
						case Winion.Bo:
							icon = Icon.Folder_Bo;
							break;
						case Winion.Grid:
							icon = Icon.Folder_Grid;
							break;
						case Winion.Fix:
							icon = Icon.Folder_Fix;
							break;
						case Winion.Debug:
							icon = Icon.Folder_Debug;
							break;
						}
						if (icon != Icon.None && !SingletoneBehaviour<IconManager>.Instance.IsWindowActive(icon))
						{
							SingletoneBehaviour<IconManager>.Instance.OpenFolder(icon);
						}
					}
					yield return new WaitUntil(() => DBManager.instance.dialogueController.endChat);
					if (!isAuto && isSystem)
					{
						yield return new WaitUntil(() => (!MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))) || this.closeSystemBox);
						this.closeSystemBox = false;
					}
					else if (!isAuto)
					{
						yield return new WaitForSeconds(0.3f);
						yield return new WaitUntil(() => !MouseRaycast.isMouseOnTitle && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)));
					}
					else if (!isLine)
					{
						if (!originAuto)
						{
							string text2 = chatController.ReplaceSentence(lines[j].context[k]);
							autoSpeed = DBManager.instance.dialogueController.DebugAutoSpeed + (float)text2.Length * DBManager.instance.dialogueController.OneLetter;
							autoSpeed = Mathf.Clamp(autoSpeed, DBManager.instance.dialogueController.MinSpeed, DBManager.instance.dialogueController.MaxSpeed);
						}
						yield return new WaitForSeconds(autoSpeed);
					}
					if (lines[j].haveEmotionSpeechBubble && !isSystem && emotionSB_Num < lines[j].emotionSpeechBubble_contextIndex.Count && lines[j].emotionSpeechBubble_contextIndex[emotionSB_Num] == k)
					{
						chatController.ReturnSpeechBubble(speechBubble);
						speechBubble = null;
						string text3 = lines[j].emotionSpeechBubble_ID[emotionSB_Num];
						bool flag = DBManager.instance.dialogueController.Animation_ByEmotion(text3, this.curTalkingWinion);
						num = emotionSB_Num;
						emotionSB_Num = num + 1;
						if (flag)
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
					else if (SystemWinion.instance.systemWinionLastLine)
					{
						SystemWinion.instance.systemWinionBubble_LastLine.SetActive(false);
					}
					else
					{
						SystemWinion.instance.systemWinionBubble.SetActive(false);
					}
					useSystemWinionBubble = false;
				}
				else if (isLine)
				{
					this.line_Text.text = "";
					this.line_Text.gameObject.SetActive(false);
				}
				if (lines[j].haveChoice)
				{
					GameObject choiceObj = DBManager.instance.chatController.GetChoiceWindow(false);
					ChoiceBox_UI choiceBox_UI = choiceObj.GetComponent<ChoiceBox_UI>();
					int num2 = lines[j].context.Length - 1;
					string text4 = DBManager.instance.chatController.CutSentence_Choice(lines[j].context[num2]);
					string text5 = DBManager.instance.chatController.CutSentence_Choice(lines[j].choice.firstOption);
					string text6 = "";
					if (!lines[j].choice.onlyOneChoice)
					{
						text6 = DBManager.instance.chatController.CutSentence_Choice(lines[j].choice.secondOption);
					}
					choiceObj.SetActive(true);
					choiceBox_UI.ButtonSetting(text4, text5, text6, lines[j].choice.onlyOneChoice);
					yield return new WaitForSeconds(0.6f);
					choiceBox_UI.canClick = true;
					yield return new WaitUntil(() => !DBManager.instance.dialogueData.selecting_PlayerOptions);
					choiceObj.SetActive(false);
					if (DBManager.instance.dialogueData.selectOption01)
					{
						if (lines[j].choice.firstOptDialogNum != lines[j].choice.secondOptDialogNum)
						{
							curDialogueChoice = lines[j].choice;
							useChoice_Option = true;
						}
						changeIndex_now = true;
						i = lines[j].choice.firstOptDialogNum;
						backLog_Line = new BackLog_Line();
						backLog_Line.winionName = "{name}";
						backLog_Line.dialogue = new List<string>();
						backLog_Line.dialogue.Add(lines[j].choice.firstOption);
						backLog_EventsCount = DBManager.backLog_Events.Count;
						BackLog_Bundle_ListCount = DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List.Count;
						DBManager.backLog_Events[backLog_EventsCount - 1].BackLog_Bundle_List[BackLog_Bundle_ListCount - 1].backLog_Lines.Add(backLog_Line);
						break;
					}
					if (DBManager.instance.dialogueData.selectOption02)
					{
						changeIndex_now = true;
						i = lines[j].choice.secondOptDialogNum;
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
				else
				{
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
			if (curDialogueChoice != null && useChoice_Option && !curDialogueChoice.onlyOneChoice && i == curDialogueChoice.secondOptDialogNum)
			{
				if (curDialogueChoice.haveCommon)
				{
					i = curDialogueChoice.commonDialogNum;
					curDialogueChoice = null;
					useChoice_Option = false;
				}
				else
				{
					i = this.curDialogue.d_lines.Count;
					curDialogueChoice = null;
					useChoice_Option = false;
				}
			}
			this.curDialogueNum = i;
			int curEventDetailNum = DBManager.instance.dialogueData.curEventDetailNum;
			int num3 = this.curDialogueNum;
			DBManager.instance.dialogueData.curEvent.SettingEvent(curEventDetailNum, num3);
			lines = null;
		}
		DBManager.instance.dialogueData.curEvent.EndEvent();
		DBManager.instance.dialogueData.curDialogue_ing = false;
		DBManager.instance.dialogueData.curDialogue_Winion = Winion.None;
		SingletoneBehaviour<WinionFolderManager>.Instance.CanMoveFolder = true;
		yield return null;
		yield break;
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000C1180 File Offset: 0x000BF380
	public string GetWinionName(int id)
	{
		string text = "";
		switch (id)
		{
		case 0:
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 13 && DBManager.instance.dialogueData.curEventNum <= 25)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 7, 0);
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 17, 0);
			}
			break;
		case 1:
			if (!DBManager.instance.is3DScene)
			{
				if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum == 13)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 8, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 15 && DBManager.instance.dialogueData.curEventNum <= 17)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 8, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 19 && DBManager.instance.dialogueData.curEventNum <= 22)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 8, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 24 && DBManager.instance.dialogueData.curEventNum <= 25)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 8, 0);
				}
				else
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 18, 0);
				}
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 18, 0);
			}
			break;
		case 2:
			if (!DBManager.instance.is3DScene)
			{
				if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum == 13)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 9, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 15 && DBManager.instance.dialogueData.curEventNum <= 17)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 9, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 19 && DBManager.instance.dialogueData.curEventNum <= 22)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 9, 0);
				}
				else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 24 && DBManager.instance.dialogueData.curEventNum <= 25)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 9, 0);
				}
				else
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 19, 0);
				}
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 19, 0);
			}
			break;
		case 3:
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 13 && DBManager.instance.dialogueData.curEventNum <= 25)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 10, 0);
			}
			else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 22, 0);
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 20, 0);
			}
			break;
		case 4:
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum == 13)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 11, 0);
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 15 && DBManager.instance.dialogueData.curEventNum <= 17)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 11, 0);
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 19 && DBManager.instance.dialogueData.curEventNum <= 22)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 11, 0);
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 24 && DBManager.instance.dialogueData.curEventNum <= 25)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 11, 0);
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 21, 0);
			}
			break;
		case 5:
			text = DBManager.instance.GetSettingString("위니언", 0, 21, 0);
			break;
		case 10:
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && DBManager.instance.dialogueData.curEventNum >= 2 && DBManager.instance.dialogueData.curEventNum <= 7)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 2, 0);
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && DBManager.instance.dialogueData.curEventNum == 9)
			{
				if (DBManager.instance.dialogueData.curEventDetailNum == 3 || DBManager.instance.dialogueData.curEventDetailNum == 4)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 2, 0);
				}
				else
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 16, 0);
				}
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && DBManager.instance.dialogueData.curEventNum == 11)
			{
				if (DBManager.instance.dialogueData.curEventDetailNum >= 5)
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 16, 0);
				}
				else
				{
					text = DBManager.instance.GetSettingString("위니언", 0, 13, 0);
				}
			}
			else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 13 && DBManager.instance.dialogueData.curEventNum <= 25)
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 12, 0);
			}
			else
			{
				text = DBManager.instance.GetSettingString("위니언", 0, 13, 0);
			}
			break;
		case 11:
			text = DBManager.instance.GetSettingString("위니언", 0, 2, 0);
			break;
		case 12:
			text = DBManager.instance.GetSettingString("위니언", 0, 16, 0);
			break;
		case 13:
			text = DBManager.instance.GetSettingString("위니언", 0, 3, 0);
			break;
		case 14:
			text = DBManager.instance.GetSettingString("위니언", 0, 4, 0);
			break;
		case 15:
			text = DBManager.instance.GetSettingString("위니언", 0, 5, 0);
			break;
		case 16:
			text = DBManager.instance.GetSettingString("위니언", 0, 6, 0);
			break;
		case 17:
			text = DBManager.instance.GetSettingString("위니언", 0, 0, 0);
			break;
		case 18:
			text = DBManager.instance.GetSettingString("위니언", 0, 1, 0);
			break;
		case 20:
			text = DBManager.instance.GetSettingString("위니언", 0, 14, 0);
			break;
		case 30:
			text = DBManager.instance.GetSettingString("위니언", 0, 15, 0);
			break;
		}
		return text;
	}

	// Token: 0x0400166F RID: 5743
	private int nextEventNum;

	// Token: 0x04001670 RID: 5744
	public Dialogue curDialogue;

	// Token: 0x04001671 RID: 5745
	public int curDialogueNum;

	// Token: 0x04001672 RID: 5746
	private bool useChoice_Option01;

	// Token: 0x04001673 RID: 5747
	private Choice curDialogueChoice;

	// Token: 0x04001674 RID: 5748
	public WinionHandler curTalkingWinion;

	// Token: 0x04001675 RID: 5749
	public static bool CurEventDetailNumIs3D;

	// Token: 0x04001676 RID: 5750
	public int language;

	// Token: 0x04001677 RID: 5751
	public TMP_Text line_Text;

	// Token: 0x04001678 RID: 5752
	public bool makeBackLog_Bundle;

	// Token: 0x04001679 RID: 5753
	private bool closeSystemBox;
}
