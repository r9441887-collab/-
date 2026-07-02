using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class DBManager : MonoBehaviour
{
	// Token: 0x060009D6 RID: 2518 RVA: 0x0004D054 File Offset: 0x0004B254
	private void Awake()
	{
		if (DBManager.instance == null)
		{
			DBManager.instance = this;
		}
		ChapterSetter.CompleteTranslateIngame = false;
		this.eventDialogueController = base.GetComponent<EventDialogueController>();
		this.dialogueController = base.GetComponent<DialogueController>();
		this.chatController = base.GetComponent<ChatController>();
		this.dataParser.Init();
		this.winionCommonInfo = this.dataParser.WinionCommonInfoParser(this.csvFileName.winionInfoFileName);
		DayInfo.SetLanguage();
		if (!DBManager.GotoIngame && this.istitle)
		{
			SingletoneBehaviour<TittleManager>.Instance.WarningText_Black.SetActive(true);
			SingletoneBehaviour<TittleManager>.Instance.WarningText_BlackCanvaManager.DOFade(1f, 0.5f);
			SingletoneBehaviour<TittleManager>.Instance.WarningText.SetActive(true);
			SingletoneBehaviour<TittleManager>.Instance.WarningText.GetComponent<MessageBox>().DisableAction = delegate
			{
				SingletoneBehaviour<TittleManager>.Instance.WarningText_BlackCanvaManager.DOFade(0f, 0.5f).OnComplete(delegate
				{
					SingletoneBehaviour<TittleManager>.Instance.WarningText_Black.SetActive(false);
				});
				SingletoneBehaviour<TittleManager>.Instance.WarningText.GetComponent<MessageBox>().DisableAction = null;
			};
			return;
		}
		if (this.istitle)
		{
			SingletoneBehaviour<TittleManager>.Instance.WarningText_Black.SetActive(false);
			SingletoneBehaviour<TittleManager>.Instance.WarningText.SetActive(false);
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0004D170 File Offset: 0x0004B370
	public void PopBackLog()
	{
		if (DisplayMode.IsDisplayModeActive)
		{
			return;
		}
		if (this.dialogueData.NoBacklogOpen)
		{
			return;
		}
		if (!this.dialogueData.OpenBackLog && !this.istitle)
		{
			this.dialogueData.NoBacklogOpen = true;
			this.dialogueData.OpenBackLog = true;
			this.backLog_Obj.SetActive(true);
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0004D1CC File Offset: 0x0004B3CC
	public void Update()
	{
		if (this.is3DScene)
		{
			if (this._backLog_Events.Count != DBManager.backLog_Events.Count)
			{
				this._backLog_Events = DBManager.backLog_Events;
			}
			if (Input.GetKeyDown(108))
			{
				this.PopBackLog();
			}
		}
		if (GameManager.instance != null)
		{
			if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
			{
				if (this._backLog_Events.Count != DBManager.backLog_Events.Count)
				{
					this._backLog_Events = DBManager.backLog_Events;
				}
				if (Input.GetKeyDown(108))
				{
					this.PopBackLog();
					return;
				}
			}
		}
		else if (this.is3DScene)
		{
			if (this._backLog_Events.Count != DBManager.backLog_Events.Count)
			{
				this._backLog_Events = DBManager.backLog_Events;
			}
			if (Input.GetKeyDown(108))
			{
				this.PopBackLog();
			}
		}
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0004D29C File Offset: 0x0004B49C
	public void IsCutSceneSetting(bool isCutScene)
	{
		if (isCutScene)
		{
			this.curBackLog = this.dialogueData.OpenBackLog;
			this.dialogueData.OpenBackLog = true;
			for (int i = 0; i < this.dialogueController.autoDialogue_Text.Count; i++)
			{
				this.dialogueController.autoDialogue_Text[i].gameObject.SetActive(false);
			}
			for (int j = 0; j < this.LastDialogue.Count; j++)
			{
				this.LastDialogue[j].gameObject.SetActive(false);
			}
		}
		if (!isCutScene)
		{
			this.dialogueData.OpenBackLog = this.curBackLog;
			for (int k = 0; k < this.dialogueController.autoDialogue_Text.Count; k++)
			{
				this.dialogueController.autoDialogue_Text[k].gameObject.SetActive(true);
			}
			for (int l = 0; l < this.LastDialogue.Count; l++)
			{
				this.LastDialogue[l].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0001432A File Offset: 0x0001252A
	public void NoBacklogOpen_False()
	{
		this.dialogueData.NoBacklogOpen = false;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00014338 File Offset: 0x00012538
	public void Start3DParser()
	{
		this.myPC_3DGame();
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0004D3A8 File Offset: 0x0004B5A8
	public void StartParser()
	{
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
		{
			this.ChangeDialogueScript(GameManager.instance.gameData.curChapter);
			this.ConversationsSetting();
			this.myPC_3DGame();
			return;
		}
		SoundManager.instance.bgmPlayer.volume = 0f;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00014340 File Offset: 0x00012540
	private void Start()
	{
		this.DialogueSetting();
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0004D400 File Offset: 0x0004B600
	public void DialogueSetting()
	{
		if (PlayerPrefs.HasKey("ShowAllConversation"))
		{
			this.showAllConversation = PlayerPrefs.GetInt("ShowAllConversation", 0) == 1;
			this.showTextByWord = PlayerPrefs.GetInt("ShowTextByWord", 0) == 1;
			return;
		}
		this.showAllConversation = false;
		this.showTextByWord = true;
		if (this.showTextByWord)
		{
			this.showSpeed = this.defalutSpeed_word;
			return;
		}
		this.showSpeed = this.defalutSpeed_char;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0004D474 File Offset: 0x0004B674
	public void ChangeDialogueScript(GameManager.Chapter chapter)
	{
		if (chapter != this.curChapter_EventDialogue)
		{
			this.curChapter_EventDialogue = chapter;
			int @int = PlayerPrefs.GetInt("Language", 0);
			string text = this.csvFileName.chapter01_KR_EventDialogueFileName;
			string text2 = this.csvFileName.winion_Dialogue_byEventChapter01_KR_FileName;
			if (this.curChapter_EventDialogue == GameManager.Chapter.Tutorial)
			{
				text = this.csvFileName.chapter00_KR_EventDialogueFileName;
				text2 = this.csvFileName.winion_Dialogue_byEventChapter00_KR_FileName;
			}
			if (this.curChapter_EventDialogue == GameManager.Chapter.chapter00)
			{
				if (@int == 0)
				{
					text = this.csvFileName.chapter00_EN_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter00_EN_FileName;
				}
				else if (@int == 1)
				{
					text = this.csvFileName.chapter00_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter00_KR_FileName;
				}
				else
				{
					text = this.csvFileName.chapter00_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter00_KR_FileName;
				}
			}
			if (this.curChapter_EventDialogue == GameManager.Chapter.chapter01)
			{
				if (@int == 0)
				{
					text = this.csvFileName.chapter01_EN_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter01_EN_FileName;
				}
				else if (@int == 1)
				{
					text = this.csvFileName.chapter01_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter01_KR_FileName;
				}
				else
				{
					text = this.csvFileName.chapter01_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter01_KR_FileName;
				}
			}
			if (this.curChapter_EventDialogue == GameManager.Chapter.chapter02)
			{
				if (@int == 0)
				{
					text = this.csvFileName.chapter02_EN_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter02_EN_FileName;
				}
				else if (@int == 1)
				{
					text = this.csvFileName.chapter02_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter02_KR_FileName;
				}
				else
				{
					text = this.csvFileName.chapter02_KR_EventDialogueFileName;
					text2 = this.csvFileName.winion_Dialogue_byEventChapter02_KR_FileName;
				}
			}
			if (this.curChapter_EventDialogue == GameManager.Chapter.chapter03)
			{
				if (@int == 0)
				{
					text = this.csvFileName.chapter03_EN_EventDialogueFileName;
					text2 = "";
				}
				else if (@int == 1)
				{
					text = this.csvFileName.chapter03_KR_EventDialogueFileName;
					text2 = "";
				}
				else
				{
					text = this.csvFileName.chapter03_KR_EventDialogueFileName;
					text2 = "";
				}
			}
			this.EventDialogueSetting(text, this.Chapter_EventDialogue);
			if (text2 != "")
			{
				this.Dialogue_byEvent(text2, this.WinionDialogue_ByEventChapter);
				return;
			}
			this.WinionDialogue_ByEventChapter.Clear();
		}
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0004D66C File Offset: 0x0004B86C
	public void EventDialogueSetting(string csvFileName, Dictionary<int, EventDialogue> dictionary)
	{
		dictionary.Clear();
		List<EventDialogue> list = this.dataParser.EventDialogueParser(csvFileName);
		for (int i = 0; i < list.Count; i++)
		{
			int eventNum = list[i].eventNum;
			try
			{
				dictionary.Add(eventNum, list[i]);
			}
			catch
			{
			}
		}
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
	public void Dialogue_byEvent(string csvFileName, Dictionary<int, WinionDialogue_ByEvent> dictionary)
	{
		dictionary.Clear();
		foreach (DialogueByEvent dialogueByEvent in this.dataParser.DialogueParser_byEvent(csvFileName))
		{
			if (dialogueByEvent.haveDetailNumRange)
			{
				for (int i = dialogueByEvent.detailNum; i <= dialogueByEvent.detailNum02; i++)
				{
					int num = this.Get_Dialogue_byEvent_Id(dialogueByEvent.eventNum, i, dialogueByEvent.winionIndex);
					if (dictionary.ContainsKey(num))
					{
						if (dialogueByEvent.repeat)
						{
							dictionary[num].repeat = true;
						}
						dictionary[num].event_Dialogues.Add(dialogueByEvent.dialogue);
					}
					else
					{
						WinionDialogue_ByEvent winionDialogue_ByEvent = new WinionDialogue_ByEvent();
						if (dialogueByEvent.repeat)
						{
							winionDialogue_ByEvent.repeat = true;
						}
						winionDialogue_ByEvent.event_Dialogues = new List<Dialogue>();
						winionDialogue_ByEvent.event_Dialogues.Add(dialogueByEvent.dialogue);
						dictionary.Add(num, winionDialogue_ByEvent);
					}
				}
			}
			else
			{
				int num = this.Get_Dialogue_byEvent_Id(dialogueByEvent.eventNum, dialogueByEvent.detailNum, dialogueByEvent.winionIndex);
				if (dictionary.ContainsKey(num))
				{
					if (dialogueByEvent.repeat)
					{
						dictionary[num].repeat = true;
					}
					dictionary[num].event_Dialogues.Add(dialogueByEvent.dialogue);
				}
				else
				{
					WinionDialogue_ByEvent winionDialogue_ByEvent2 = new WinionDialogue_ByEvent();
					if (dialogueByEvent.repeat)
					{
						winionDialogue_ByEvent2.repeat = true;
					}
					winionDialogue_ByEvent2.event_Dialogues = new List<Dialogue>();
					winionDialogue_ByEvent2.event_Dialogues.Add(dialogueByEvent.dialogue);
					dictionary.Add(num, winionDialogue_ByEvent2);
				}
			}
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0004D880 File Offset: 0x0004BA80
	public int Get_Dialogue_byEvent_Id(int eventId, int eventDetailId, int WinionId)
	{
		string text = "";
		if (eventId.ToString().Length == 1)
		{
			text = text + "0" + eventId.ToString();
		}
		else
		{
			text += eventId.ToString();
		}
		if (eventDetailId == -1)
		{
			text += "99";
		}
		else if (eventDetailId.ToString().Length == 1)
		{
			text = text + "0" + eventDetailId.ToString();
		}
		else
		{
			text += eventDetailId.ToString();
		}
		if (WinionId.ToString().Length == 1)
		{
			text = text + "0" + WinionId.ToString();
		}
		else
		{
			text += WinionId.ToString();
		}
		text == "05-1010";
		return int.Parse(text);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0004D950 File Offset: 0x0004BB50
	public void Dialogue_byBehavior(string csvFileName, Dictionary<int, List<LineByBehavior>> dictionary)
	{
		foreach (DialogueByBehavior dialogueByBehavior in this.dataParser.DialogueParser_byBehavior(csvFileName))
		{
			int num = this.Get_Dialogue_byBehavior_Id(dialogueByBehavior.behaviorNum, dialogueByBehavior.winionIndex);
			dictionary.Add(num, dialogueByBehavior.dialogue);
		}
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0004D9C4 File Offset: 0x0004BBC4
	public int Get_Dialogue_byBehavior_Id(int behaviorNum, int WinionId)
	{
		string text = "";
		if (behaviorNum.ToString().Length == 1)
		{
			text = text + "0" + behaviorNum.ToString();
		}
		else
		{
			text += behaviorNum.ToString();
		}
		if (WinionId.ToString().Length == 1)
		{
			text = text + "0" + WinionId.ToString();
		}
		else
		{
			text += WinionId.ToString();
		}
		return int.Parse(text);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0004DA44 File Offset: 0x0004BC44
	public void myPC_3DGame()
	{
		if (this.csvFileName.myPC_3DGame_FileName01_KR != "")
		{
			int @int = PlayerPrefs.GetInt("Language", 0);
			string text;
			if (@int == 0)
			{
				text = this.csvFileName.myPC_3DGame_FileName01_EN;
			}
			else if (@int == 1)
			{
				text = this.csvFileName.myPC_3DGame_FileName01_KR;
			}
			else
			{
				text = this.csvFileName.myPC_3DGame_FileName01_KR;
			}
			Dialogue_3D dialogue_3D = this.dataParser.DialogueParser_3D(text);
			this.Dialogue_3D.Add(dialogue_3D);
		}
		if (this.csvFileName.myPC_3DGame_FileName02_KR != "")
		{
			int int2 = PlayerPrefs.GetInt("Language", 0);
			string text;
			if (int2 == 0)
			{
				text = this.csvFileName.myPC_3DGame_FileName02_EN;
			}
			else if (int2 == 1)
			{
				text = this.csvFileName.myPC_3DGame_FileName02_KR;
			}
			else
			{
				text = this.csvFileName.myPC_3DGame_FileName02_KR;
			}
			Dialogue_3D dialogue_3D2 = this.dataParser.DialogueParser_3D(text);
			this.Dialogue_3D.Add(dialogue_3D2);
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0004DB30 File Offset: 0x0004BD30
	public void SettingContent()
	{
		this.setting_Content = true;
		int @int = PlayerPrefs.GetInt("Language", 0);
		if (@int == 0)
		{
			this.MailContent(DBManager.instance.csvFileName.mailContent_EN_FileName);
			this.GameContent_UI(DBManager.instance.csvFileName.ContentUI_EN_FileName);
			this.GameContent_Event(DBManager.instance.csvFileName.ContentEvent_EN_FileName);
		}
		else if (@int == 1)
		{
			this.MailContent(DBManager.instance.csvFileName.mailContent_KR_FileName);
			this.GameContent_UI(DBManager.instance.csvFileName.ContentUI_KR_FileName);
			this.GameContent_Event(DBManager.instance.csvFileName.ContentEvent_KR_FileName);
		}
		else
		{
			this.MailContent(DBManager.instance.csvFileName.mailContent_KR_FileName);
			this.GameContent_UI(DBManager.instance.csvFileName.ContentUI_KR_FileName);
			this.GameContent_Event(DBManager.instance.csvFileName.ContentEvent_KR_FileName);
		}
		DBManager.instance.SetTranslateLastDialogue();
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0004DC28 File Offset: 0x0004BE28
	public void MailContent(string csvFileName)
	{
		this.mailContents.Clear();
		Contents contents = this.dataParser.DialogueParser_Content(csvFileName);
		this.mailContents = contents.Content_List;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0004DC5C File Offset: 0x0004BE5C
	public void GameContent_UI(string csvFileName)
	{
		this.Contents_UI.Clear();
		Contents contents = this.dataParser.DialogueParser_Content(csvFileName);
		for (int i = 0; i < contents.Content_List.Count; i++)
		{
			this.Contents_UI.Add(contents.Content_List[i].ID, contents.Content_List[i]);
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
	public void GameContent_Event(string csvFileName)
	{
		this.Contents_Event.Clear();
		Contents contents = this.dataParser.DialogueParser_Content(csvFileName);
		for (int i = 0; i < contents.Content_List.Count; i++)
		{
			this.Contents_Event.Add(contents.Content_List[i].ID, contents.Content_List[i]);
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x00014348 File Offset: 0x00012548
	public Content GetContent(string id = "", int contentsNum = 0)
	{
		if (!this.setting_Content)
		{
			this.SettingContent();
		}
		if (contentsNum != 0)
		{
			return this.Contents_Event[id];
		}
		return this.Contents_UI[id];
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0004DD24 File Offset: 0x0004BF24
	public string GetSettingString(string ID = "", int mailID = 0, int index = 0, int contentsNum = 0)
	{
		if (contentsNum == 2)
		{
			return this.chatController.SettingSentence(this.mailContents[mailID].Line_List[index], false, -1, false).Replace("\r", "");
		}
		return this.chatController.SettingSentence(this.GetContent(ID, contentsNum).Line_List[index], false, -1, false).Replace("\r", "");
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0004DD9C File Offset: 0x0004BF9C
	public void ConversationsSetting()
	{
		int @int = PlayerPrefs.GetInt("Language", 0);
		string text;
		if (@int == 0)
		{
			text = this.csvFileName.Setting_Conversations_EN_FileName;
		}
		else if (@int == 1)
		{
			text = this.csvFileName.Setting_Conversations_KR_FileName;
		}
		else
		{
			text = this.csvFileName.Setting_Conversations_KR_FileName;
		}
		this.Setting_Conversations = this.dataParser.DialogueParser_3D(text);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0004DDFC File Offset: 0x0004BFFC
	public void Dialogue_LoveLevel(string _csvFileName)
	{
		List<List<Dialogue>> list = this.dataParser.DialogueParser_withLoveLevel(_csvFileName);
		if (_csvFileName == this.csvFileName.ION_ChatWithLoveFileName)
		{
			for (int i = 0; i < list.Count; i++)
			{
				RandomDialogue randomDialogue = this.ION_Dialogue_LoveLevel01;
				randomDialogue.randomDialogue = list[i];
				randomDialogue.weight = new List<float>();
				for (int j = 0; j < list[i].Count; j++)
				{
					randomDialogue.weight.Add(1f);
				}
			}
		}
		if (_csvFileName == this.csvFileName.Bo_ChatWithLoveFileName)
		{
			for (int k = 0; k < list.Count; k++)
			{
				RandomDialogue randomDialogue = this.Bo_Dialogue_LoveLevel01;
				randomDialogue.randomDialogue = list[k];
				randomDialogue.weight = new List<float>();
				for (int l = 0; l < list[k].Count; l++)
				{
					randomDialogue.weight.Add(1f);
				}
			}
		}
		if (_csvFileName == this.csvFileName.Grid_ChatWithLoveFileName)
		{
			for (int m = 0; m < list.Count; m++)
			{
				RandomDialogue randomDialogue = this.Grid_Dialogue_LoveLevel01;
				randomDialogue.randomDialogue = list[m];
				randomDialogue.weight = new List<float>();
				for (int n = 0; n < list[m].Count; n++)
				{
					randomDialogue.weight.Add(1f);
				}
			}
		}
		if (_csvFileName == this.csvFileName.Fix_ChatWithLoveFileName)
		{
			for (int num = 0; num < list.Count; num++)
			{
				RandomDialogue randomDialogue = this.Fix_Dialogue_LoveLevel01;
				randomDialogue.randomDialogue = list[num];
				randomDialogue.weight = new List<float>();
				for (int num2 = 0; num2 < list[num].Count; num2++)
				{
					randomDialogue.weight.Add(1f);
				}
			}
		}
		if (_csvFileName == this.csvFileName.Debug_ChatWithLoveFileName)
		{
			for (int num3 = 0; num3 < list.Count; num3++)
			{
				RandomDialogue randomDialogue = this.Debug_Dialogue_LoveLevel01;
				randomDialogue.randomDialogue = list[num3];
				randomDialogue.weight = new List<float>();
				for (int num4 = 0; num4 < list[num3].Count; num4++)
				{
					randomDialogue.weight.Add(1f);
				}
			}
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0004E054 File Offset: 0x0004C254
	public DBManager.EmotionSpeechBubble ReplaceIdToEmotionBubble(string id)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(id);
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
							if (id == "{우히히}")
							{
								return DBManager.EmotionSpeechBubble.Hehehe;
							}
						}
					}
					else if (id == "{나이뻐}")
					{
						return DBManager.EmotionSpeechBubble.AmIPretty;
					}
				}
				else if (num != 390279577U)
				{
					if (num == 815280957U)
					{
						if (id == "{반짝반짝}")
						{
							return DBManager.EmotionSpeechBubble.Twinkle;
						}
					}
				}
				else if (id == "{눈물}")
				{
					return DBManager.EmotionSpeechBubble.Sad;
				}
			}
			else if (num <= 1792803225U)
			{
				if (num != 1094323117U)
				{
					if (num == 1792803225U)
					{
						if (id == "{느낌표}")
						{
							return DBManager.EmotionSpeechBubble.ExclamationMark;
						}
					}
				}
				else if (id == "{기분 나빠}")
				{
					return DBManager.EmotionSpeechBubble.Angry;
				}
			}
			else if (num != 1819490413U)
			{
				if (num == 1892855261U)
				{
					if (id == "{편지}")
					{
						return DBManager.EmotionSpeechBubble.Letter;
					}
				}
			}
			else if (id == "{애정 업}")
			{
				return DBManager.EmotionSpeechBubble.GrowAffection;
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
						if (id == "{좌절}")
						{
							return DBManager.EmotionSpeechBubble.Despair;
						}
					}
				}
				else if (id == "{애정 다운}")
				{
					return DBManager.EmotionSpeechBubble.LoseAffection;
				}
			}
			else if (num != 2010738123U)
			{
				if (num == 2073664585U)
				{
					if (id == "{나 할말있어}")
					{
						return DBManager.EmotionSpeechBubble.HaveSomethingToSay;
					}
				}
			}
			else if (id == "{행복}")
			{
				return DBManager.EmotionSpeechBubble.Happy;
			}
		}
		else if (num <= 2605631725U)
		{
			if (num != 2286645616U)
			{
				if (num == 2605631725U)
				{
					if (id == "{부끄러움}")
					{
						return DBManager.EmotionSpeechBubble.Shame;
					}
				}
			}
			else if (id == "{눈껌뻑}")
			{
				return DBManager.EmotionSpeechBubble.OhWhat;
			}
		}
		else if (num != 2643319536U)
		{
			if (num != 3475316093U)
			{
				if (num == 3740787401U)
				{
					if (id == "{잠}")
					{
						return DBManager.EmotionSpeechBubble.Sleep;
					}
				}
			}
			else if (id == "{우울}")
			{
				return DBManager.EmotionSpeechBubble.Depresstion;
			}
		}
		else if (id == "{똥}")
		{
			return DBManager.EmotionSpeechBubble.poop;
		}
		return DBManager.EmotionSpeechBubble.None;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00014374 File Offset: 0x00012574
	public void SettingsDictionary(DBManager.EmotionSpeechBubble emotion, string prefabName)
	{
		this.emotionSpeechBubblePrefabsName_Dic.Add(emotion, prefabName);
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0004E324 File Offset: 0x0004C524
	public GameObject Get_Emotion_SpeechBubble(DBManager.EmotionSpeechBubble emotion, WinionHandler curwinion = null)
	{
		string text = this.emotionSpeechBubblePrefabsName_Dic[emotion];
		text = text.Trim();
		Transform transform = this.chatController.emotionBubbleParent;
		Transform transform2 = curwinion.winionStatus.speechBubblePos;
		if (!curwinion.worldWinionEnabled && curwinion.UIWinionEnabled)
		{
			int whichFolder = (int)curwinion.whichFolder;
			transform = SingletoneBehaviour<WinionFolderManager>.Instance.windows[whichFolder].GetComponent<WinionFileSelector>().winionEmotionBubblePos;
			transform2 = curwinion.uiWinionSpeechBubblePos;
		}
		GameObject gameObjectPrefab = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab(text, "EmotionSpeechBubblePrefabs/", transform, false);
		if (curwinion != null)
		{
			gameObjectPrefab.transform.position = transform2.position;
		}
		SpeechBubbleInfo component = gameObjectPrefab.GetComponent<SpeechBubbleInfo>();
		RectTransform component2 = gameObjectPrefab.GetComponent<RectTransform>();
		if (component2 != null)
		{
			if (component.changeSize)
			{
				component2.localScale = new Vector3(component.size, component.size, component.size);
			}
			else
			{
				component2.localScale = Vector3.one;
			}
		}
		return gameObjectPrefab;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0004E420 File Offset: 0x0004C620
	public void Return_Emotion_SpeechBubble(GameObject emotion_speechBubble, DBManager.EmotionSpeechBubble emotion)
	{
		string text = this.emotionSpeechBubblePrefabsName_Dic[emotion];
		text = text.Trim();
		GameManager.instance.objectPoolingSystem.AddGameObjectPool(text, emotion_speechBubble, this.chatController.emotionBubbleParent);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0004E460 File Offset: 0x0004C660
	public void SetTranslateLastDialogue()
	{
		for (int i = 0; i < this.LastDialogue.Count; i++)
		{
			this.LastDialogue[i].text = this.GetSettingString("백로그", 0, 1, 0);
		}
		if (this.backLog_Title_Text != null)
		{
			this.backLog_Title_Text.text = DBManager.instance.GetSettingString("백로그", 0, 0, 0);
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0004E4D0 File Offset: 0x0004C6D0
	public void ExTextActive(bool active = false)
	{
		if (active)
		{
			for (int i = 0; i < this.LastDialogue.Count; i++)
			{
				this.LastDialogue[i].gameObject.SetActive(true);
			}
			for (int j = 0; j < this.dialogueController.autoDialogue_Text.Count; j++)
			{
				this.dialogueController.autoDialogue_Text[j].gameObject.SetActive(true);
			}
			return;
		}
		for (int k = 0; k < this.LastDialogue.Count; k++)
		{
			this.LastDialogue[k].gameObject.SetActive(false);
		}
		for (int l = 0; l < this.dialogueController.autoDialogue_Text.Count; l++)
		{
			this.dialogueController.autoDialogue_Text[l].gameObject.SetActive(false);
		}
	}

	// Token: 0x04000ADA RID: 2778
	public static DBManager instance;

	// Token: 0x04000ADB RID: 2779
	public DialogueData dialogueData;

	// Token: 0x04000ADC RID: 2780
	public WinionFaceInfo winionFaceInfo;

	// Token: 0x04000ADD RID: 2781
	public EventDialogueController eventDialogueController;

	// Token: 0x04000ADE RID: 2782
	public DialogueController dialogueController;

	// Token: 0x04000ADF RID: 2783
	public ChatController chatController;

	// Token: 0x04000AE0 RID: 2784
	public CsvFileName csvFileName;

	// Token: 0x04000AE1 RID: 2785
	public DataParser dataParser;

	// Token: 0x04000AE2 RID: 2786
	public WinionCommonInfo winionCommonInfo;

	// Token: 0x04000AE3 RID: 2787
	public Dictionary<DBManager.EmotionSpeechBubble, string> emotionSpeechBubblePrefabsName_Dic = new Dictionary<DBManager.EmotionSpeechBubble, string>();

	// Token: 0x04000AE4 RID: 2788
	public RandomDialogue ION_Dialogue_LoveLevel01;

	// Token: 0x04000AE5 RID: 2789
	public RandomDialogue Bo_Dialogue_LoveLevel01;

	// Token: 0x04000AE6 RID: 2790
	public RandomDialogue Grid_Dialogue_LoveLevel01;

	// Token: 0x04000AE7 RID: 2791
	public RandomDialogue Fix_Dialogue_LoveLevel01;

	// Token: 0x04000AE8 RID: 2792
	public RandomDialogue Debug_Dialogue_LoveLevel01;

	// Token: 0x04000AE9 RID: 2793
	public GameManager.Chapter curChapter_EventDialogue = GameManager.Chapter.chapter00;

	// Token: 0x04000AEA RID: 2794
	public Dictionary<int, EventDialogue> Chapter_EventDialogue = new Dictionary<int, EventDialogue>();

	// Token: 0x04000AEB RID: 2795
	public Dictionary<int, WinionDialogue_ByEvent> WinionDialogue_ByEventChapter = new Dictionary<int, WinionDialogue_ByEvent>();

	// Token: 0x04000AEC RID: 2796
	public Dictionary<int, List<LineByBehavior>> WinionDialogue_ByBehavior = new Dictionary<int, List<LineByBehavior>>();

	// Token: 0x04000AED RID: 2797
	public List<Dialogue_3D> Dialogue_3D = new List<Dialogue_3D>();

	// Token: 0x04000AEE RID: 2798
	public Dialogue_3D Setting_Conversations = new Dialogue_3D();

	// Token: 0x04000AEF RID: 2799
	public bool is3DScene;

	// Token: 0x04000AF0 RID: 2800
	public bool istitle;

	// Token: 0x04000AF1 RID: 2801
	public List<Content> mailContents = new List<Content>();

	// Token: 0x04000AF2 RID: 2802
	public Dictionary<string, Content> Contents_UI = new Dictionary<string, Content>();

	// Token: 0x04000AF3 RID: 2803
	public Dictionary<string, Content> Contents_Event = new Dictionary<string, Content>();

	// Token: 0x04000AF4 RID: 2804
	public bool showAllConversation;

	// Token: 0x04000AF5 RID: 2805
	public bool showTextByWord;

	// Token: 0x04000AF6 RID: 2806
	public float showSpeed = 0.02f;

	// Token: 0x04000AF7 RID: 2807
	public float defalutSpeed_char = 0.02f;

	// Token: 0x04000AF8 RID: 2808
	public float defalutSpeed_word = 0.04f;

	// Token: 0x04000AF9 RID: 2809
	public Ingame_Language ingame_Language;

	// Token: 0x04000AFA RID: 2810
	public TitleTextSetting title_Language;

	// Token: 0x04000AFB RID: 2811
	public static List<BackLog_Event> backLog_Events = new List<BackLog_Event>();

	// Token: 0x04000AFC RID: 2812
	public List<BackLog_Event> _backLog_Events = new List<BackLog_Event>();

	// Token: 0x04000AFD RID: 2813
	public GameObject backLog_Obj;

	// Token: 0x04000AFE RID: 2814
	public SettingUI settingUI;

	// Token: 0x04000AFF RID: 2815
	public static bool GotoIngame = false;

	// Token: 0x04000B00 RID: 2816
	public bool NextDay;

	// Token: 0x04000B01 RID: 2817
	private bool curBackLog;

	// Token: 0x04000B02 RID: 2818
	private bool setting_Content;

	// Token: 0x04000B03 RID: 2819
	public List<TextMeshProUGUI> LastDialogue;

	// Token: 0x04000B04 RID: 2820
	public TMP_Text backLog_Title_Text;

	// Token: 0x020001AC RID: 428
	public enum EmotionSpeechBubble
	{
		// Token: 0x04000B06 RID: 2822
		None,
		// Token: 0x04000B07 RID: 2823
		GrowAffection,
		// Token: 0x04000B08 RID: 2824
		LoseAffection,
		// Token: 0x04000B09 RID: 2825
		Happy,
		// Token: 0x04000B0A RID: 2826
		Hehehe,
		// Token: 0x04000B0B RID: 2827
		Sad,
		// Token: 0x04000B0C RID: 2828
		Angry,
		// Token: 0x04000B0D RID: 2829
		Depresstion,
		// Token: 0x04000B0E RID: 2830
		Despair,
		// Token: 0x04000B0F RID: 2831
		Shame,
		// Token: 0x04000B10 RID: 2832
		Sleep,
		// Token: 0x04000B11 RID: 2833
		Twinkle,
		// Token: 0x04000B12 RID: 2834
		ExclamationMark,
		// Token: 0x04000B13 RID: 2835
		HaveSomethingToSay,
		// Token: 0x04000B14 RID: 2836
		AmIPretty,
		// Token: 0x04000B15 RID: 2837
		OhWhat,
		// Token: 0x04000B16 RID: 2838
		Letter,
		// Token: 0x04000B17 RID: 2839
		poop
	}
}
