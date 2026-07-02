using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class ChatController : MonoBehaviour
{
	// Token: 0x06000933 RID: 2355 RVA: 0x00045C70 File Offset: 0x00043E70
	private void Start()
	{
		this.eventDialogueController = DBManager.instance.eventDialogueController;
		this.dialogueController = DBManager.instance.dialogueController;
		this.mainCamera = Camera.main;
		if (this.playerSelectionWindow == null)
		{
			this.LoadChoiceWindow();
			this.playerSelectionWindow.SetActive(false);
			this.playerSelectionWindow_one.SetActive(false);
		}
		this.language = PlayerPrefs.GetInt("Language", 0);
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00013EDC File Offset: 0x000120DC
	private void Update()
	{
		if (this.startChat && !this._isAuto && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)))
		{
			this.stopChat = true;
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00045CE8 File Offset: 0x00043EE8
	public ChatController.BubblePos GetBubblePos(Vector3 pos)
	{
		Vector3 vector = this.mainCamera.WorldToScreenPoint(pos);
		float num = (float)(Screen.width / 2);
		ChatController.BubblePos bubblePos;
		if (vector.x < num)
		{
			bubblePos = ChatController.BubblePos.rightBubble;
		}
		else
		{
			bubblePos = ChatController.BubblePos.leftBubble;
		}
		return bubblePos;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00045D1C File Offset: 0x00043F1C
	public GameObject GetSpeechBubble(WinionHandler winionHandler, ChatController.BubbleType bubbleType = ChatController.BubbleType.NormalBubble)
	{
		Transform transform = null;
		GameObject gameObject = null;
		bool flag = false;
		Transform transform2;
		ChatController.BubblePos bubblePos;
		if (!winionHandler.worldWinionEnabled && winionHandler.UIWinionEnabled)
		{
			int whichFolder = (int)winionHandler.whichFolder;
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[whichFolder].GetComponent<WinionFileSelector>();
			if (winionHandler.whichFolder == Winion.System)
			{
				flag = true;
			}
			transform2 = component.winionSpeechBubblePos;
			bubblePos = this.GetBubblePos(winionHandler.uiWinion.transform.position);
			Icon icon = Icon.None;
			switch (winionHandler.whichFolder)
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
		else
		{
			if (winionHandler.winionDialogue_upUI)
			{
				transform2 = this.ChocieBoxParent;
			}
			else
			{
				transform2 = this.speechBubbleParent;
			}
			bubblePos = this.GetBubblePos(winionHandler.gameObject.transform.position);
			if (GameManager.instance != null && GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01 && DBManager.instance.dialogueData.curEventNum == 9 && DBManager.instance.dialogueData.curEventDetailNum < 18)
			{
				if (winionHandler.winionStatus.winionInfo.winionType == Winion.Bo)
				{
					bubblePos = ChatController.BubblePos.leftBubble;
				}
				if (winionHandler.winionStatus.winionInfo.winionType == Winion.Fix)
				{
					bubblePos = ChatController.BubblePos.rightBubble;
				}
			}
		}
		switch (bubblePos)
		{
		case ChatController.BubblePos.Bubble:
			if (!winionHandler.worldWinionEnabled && winionHandler.UIWinionEnabled)
			{
				transform = winionHandler.uiWinionSpeechBubblePos_Left;
			}
			else
			{
				transform = winionHandler.winionStatus.speechBubbleLeftPos;
			}
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.SilentBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Silent", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.ShoutBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Shout", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.AngryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Angry", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.imaginaryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Imaginary", "SystemPrefabs/", transform2, false);
				break;
			}
			break;
		case ChatController.BubblePos.rightBubble:
			if (!winionHandler.worldWinionEnabled && winionHandler.UIWinionEnabled)
			{
				transform = winionHandler.uiWinionSpeechBubblePos_Right;
			}
			else
			{
				transform = winionHandler.winionStatus.speechBubbleRightPos;
				if (GameManager.instance != null && GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && DBManager.instance.dialogueData.curEventNum == 8 && winionHandler.winionStatus.isWatchWinion)
				{
					transform = winionHandler.BowatchWinionBubblePos;
				}
			}
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.SilentBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Silent", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.ShoutBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Shout", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.AngryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Angry", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.imaginaryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Imaginary", "SystemPrefabs/", transform2, false);
				break;
			}
			break;
		case ChatController.BubblePos.leftBubble:
			if (!winionHandler.worldWinionEnabled && winionHandler.UIWinionEnabled)
			{
				transform = winionHandler.uiWinionSpeechBubblePos_Left;
			}
			else
			{
				transform = winionHandler.winionStatus.speechBubbleLeftPos;
			}
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.SilentBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Silent", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.ShoutBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Shout", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.AngryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Angry", "SystemPrefabs/", transform2, false);
				break;
			case ChatController.BubbleType.imaginaryBubble:
				gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Imaginary", "SystemPrefabs/", transform2, false);
				break;
			}
			break;
		}
		if (transform == null)
		{
			return null;
		}
		gameObject.transform.position = transform.position;
		SpeechBubbleInfo component2 = gameObject.GetComponent<SpeechBubbleInfo>();
		component2.BrightBubble(flag);
		component2.StartSpeechBubble(transform);
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x000461DC File Offset: 0x000443DC
	public GameObject GetSpeechBubble_chapter02Event10_Fix(ChatController.BubbleType bubbleType = ChatController.BubbleType.NormalBubble)
	{
		GameObject gameObject = null;
		int num = 1;
		Transform winionSpeechBubblePos = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>().winionSpeechBubblePos;
		this.GetBubblePos(ScreenCanvas.Instance.boFolderInteraction.fixWindowSpeechPos.transform.position);
		Transform fixWindowSpeechPos = ScreenCanvas.Instance.boFolderInteraction.fixWindowSpeechPos;
		switch (bubbleType)
		{
		case ChatController.BubbleType.NormalBubble:
			gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right", "SystemPrefabs/", winionSpeechBubblePos, false);
			break;
		case ChatController.BubbleType.SilentBubble:
			gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Silent", "SystemPrefabs/", winionSpeechBubblePos, false);
			break;
		case ChatController.BubbleType.ShoutBubble:
			gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Shout", "SystemPrefabs/", winionSpeechBubblePos, false);
			break;
		case ChatController.BubbleType.AngryBubble:
			gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Angry", "SystemPrefabs/", winionSpeechBubblePos, false);
			break;
		case ChatController.BubbleType.imaginaryBubble:
			gameObject = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Right_Imaginary", "SystemPrefabs/", winionSpeechBubblePos, false);
			break;
		}
		if (fixWindowSpeechPos == null)
		{
			return null;
		}
		gameObject.transform.position = fixWindowSpeechPos.position;
		gameObject.GetComponent<SpeechBubbleInfo>().StartSpeechBubble(fixWindowSpeechPos);
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00046320 File Offset: 0x00044520
	public GameObject GetSpeechBubble_MyPC_3DGame(ChatController.BubbleType bubbleType = ChatController.BubbleType.NormalBubble)
	{
		GameObject gameObject = null;
		Transform bubbleParent = SingletoneBehaviour<MyPcWindowResolution>.Instance.bubbleParent;
		Transform faceBubblePos = SingletoneBehaviour<MyPcWindowResolution>.Instance.faceBubblePos;
		switch (bubbleType)
		{
		case ChatController.BubbleType.NormalBubble:
			gameObject = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left", "SystemPrefabs/", bubbleParent, false);
			break;
		case ChatController.BubbleType.SilentBubble:
			gameObject = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Silent", "SystemPrefabs/", bubbleParent, false);
			break;
		case ChatController.BubbleType.ShoutBubble:
			gameObject = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Shout", "SystemPrefabs/", bubbleParent, false);
			break;
		case ChatController.BubbleType.AngryBubble:
			gameObject = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Angry", "SystemPrefabs/", bubbleParent, false);
			break;
		case ChatController.BubbleType.imaginaryBubble:
			gameObject = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("SpeechBubble_Left_Imaginary", "SystemPrefabs/", bubbleParent, false);
			break;
		}
		if (faceBubblePos == null)
		{
			return null;
		}
		gameObject.transform.position = faceBubblePos.position;
		gameObject.GetComponent<SpeechBubbleInfo>().StartSpeechBubble(faceBubblePos);
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0004642C File Offset: 0x0004462C
	public void ReturnSpeechBubble(GameObject speechBubble)
	{
		string text = "";
		SpeechBubbleInfo component = speechBubble.GetComponent<SpeechBubbleInfo>();
		ChatController.BubblePos bubblePos = component.bubblePos;
		ChatController.BubbleType bubbleType = component.bubbleType;
		component.StopSpeechBubble();
		switch (bubblePos)
		{
		case ChatController.BubblePos.Bubble:
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				text = "SpeechBubble_Left";
				break;
			case ChatController.BubbleType.SilentBubble:
				text = "SpeechBubble_Left_Silent";
				break;
			case ChatController.BubbleType.ShoutBubble:
				text = "SpeechBubble_Left_Shout";
				break;
			case ChatController.BubbleType.AngryBubble:
				text = "SpeechBubble_Left_Angry";
				break;
			case ChatController.BubbleType.imaginaryBubble:
				text = "SpeechBubble_Left_Imaginary";
				break;
			}
			break;
		case ChatController.BubblePos.rightBubble:
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				text = "SpeechBubble_Right";
				break;
			case ChatController.BubbleType.SilentBubble:
				text = "SpeechBubble_Right_Silent";
				break;
			case ChatController.BubbleType.ShoutBubble:
				text = "SpeechBubble_Right_Shout";
				break;
			case ChatController.BubbleType.AngryBubble:
				text = "SpeechBubble_Right_Angry";
				break;
			case ChatController.BubbleType.imaginaryBubble:
				text = "SpeechBubble_Right_Imaginary";
				break;
			}
			break;
		case ChatController.BubblePos.leftBubble:
			switch (bubbleType)
			{
			case ChatController.BubbleType.NormalBubble:
				text = "SpeechBubble_Left";
				break;
			case ChatController.BubbleType.SilentBubble:
				text = "SpeechBubble_Left_Silent";
				break;
			case ChatController.BubbleType.ShoutBubble:
				text = "SpeechBubble_Left_Shout";
				break;
			case ChatController.BubbleType.AngryBubble:
				text = "SpeechBubble_Left_Angry";
				break;
			case ChatController.BubbleType.imaginaryBubble:
				text = "SpeechBubble_Left_Imaginary";
				break;
			}
			break;
		}
		if (GameManager.instance != null)
		{
			GameManager.instance.objectPoolingSystem.AddGameObjectPool(text, speechBubble, this.speechBubbleParent);
			return;
		}
		SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.AddGameObjectPool(text, speechBubble, this.speechBubbleParent);
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00046588 File Offset: 0x00044788
	public void LoadChoiceWindow()
	{
		string text = "ChoiceWindow";
		GameObject gameObject = Resources.Load<GameObject>("SystemPrefabs/" + text);
		this.playerSelectionWindow = Object.Instantiate<GameObject>(gameObject, this.ChocieBoxParent);
		this.playerSelectionWindow == null;
		text = "ChoiceWindow_oneChoice";
		gameObject = Resources.Load<GameObject>("SystemPrefabs/" + text);
		this.playerSelectionWindow_one = Object.Instantiate<GameObject>(gameObject, this.ChocieBoxParent);
		this.playerSelectionWindow == null;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00013F06 File Offset: 0x00012106
	public GameObject GetChoiceWindow(bool oneChoice = false)
	{
		if (oneChoice)
		{
			return this.playerSelectionWindow_one;
		}
		return this.playerSelectionWindow;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00013F18 File Offset: 0x00012118
	public void Chat_Obect(GameObject speechBubble, string sentence, bool useChatAnimation = true, float ChatAnimationDuration = 0.02f, bool ifTest = false, bool isAuto = false)
	{
		this.startChat = true;
		base.StartCoroutine(this.ObjectChat(speechBubble, sentence, useChatAnimation, ChatAnimationDuration, ifTest, isAuto));
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00013F37 File Offset: 0x00012137
	private IEnumerator ObjectChat(GameObject speechBubble, string sentence, bool useChatAnimation, float ChatAnimationDuration = 0.02f, bool ifTest = false, bool isAuto = false)
	{
		this.language = PlayerPrefs.GetInt("Language", 0);
		int sentence_Length_withoutReachText = this.GetSentence_Length_withoutReachText(sentence);
		if (this.language == 1)
		{
			int num;
			if (sentence_Length_withoutReachText <= 30)
			{
				num = 16;
			}
			else if (sentence_Length_withoutReachText > 30 && sentence_Length_withoutReachText <= 40)
			{
				num = this.GetSentence_Length_withoutReachText(sentence) / 2;
			}
			else
			{
				num = 15;
			}
			num = Mathf.Clamp(num, 15, 40);
			sentence = this.SettingSentence(sentence, true, num, false);
		}
		else if (this.language == 0)
		{
			int num;
			if (sentence_Length_withoutReachText <= 24)
			{
				num = 24;
			}
			else
			{
				num = this.GetSentence_Length_withoutReachText(sentence) / 2;
			}
			num = Mathf.Clamp(num, 16, 24);
			sentence = this.SettingSentence(sentence, true, num, false);
		}
		string[] sentenceArray = this.DivideSentence(sentence);
		TMP_Text objectText = speechBubble.GetComponent<SpeechBubbleInfo>().bubbleText;
		string writerText = "";
		bool flag = false;
		if (isAuto)
		{
			this._isAuto = true;
			flag = true;
			ChatAnimationDuration = 0.022f;
		}
		else
		{
			this._isAuto = false;
			if (ifTest)
			{
				useChatAnimation = !SingletoneBehaviour<DialogueOption>.Instance.showAllConversation.isOn;
			}
			else
			{
				useChatAnimation = !DBManager.instance.showAllConversation;
				ChatAnimationDuration = DBManager.instance.showSpeed;
			}
		}
		if (!useChatAnimation)
		{
			writerText = sentence;
			objectText.text = writerText;
		}
		else
		{
			bool showWord = false;
			bool showChar = false;
			if (ifTest)
			{
				if (SingletoneBehaviour<DialogueOption>.Instance.showTextByWord.isOn)
				{
					showWord = true;
					showChar = false;
				}
				else
				{
					showWord = false;
					showChar = true;
				}
			}
			else if (flag)
			{
				showWord = true;
				showChar = false;
			}
			else if (DBManager.instance.showTextByWord)
			{
				showWord = true;
				showChar = false;
				if (ChatAnimationDuration != DBManager.instance.defalutSpeed_word)
				{
					DBManager.instance.showSpeed = DBManager.instance.defalutSpeed_word;
					ChatAnimationDuration = DBManager.instance.defalutSpeed_word;
				}
			}
			else
			{
				showWord = false;
				showChar = true;
				if (ChatAnimationDuration != DBManager.instance.defalutSpeed_char)
				{
					DBManager.instance.showSpeed = DBManager.instance.defalutSpeed_char;
					ChatAnimationDuration = DBManager.instance.defalutSpeed_char;
				}
			}
			int num2;
			for (int i = 0; i < sentenceArray.Length; i = num2 + 1)
			{
				if (this.stopChat)
				{
					writerText = sentence;
					objectText.text = writerText;
					break;
				}
				if (showWord)
				{
					writerText += sentenceArray[i];
					objectText.text = writerText;
					yield return new WaitForSeconds(ChatAnimationDuration);
				}
				else if (showChar)
				{
					if (this.ContainsRichText(sentenceArray[i]))
					{
						List<string> separatedTexts = ChatController.SeparateText(sentenceArray[i]);
						for (int p = 0; p < separatedTexts.Count; p = num2 + 1)
						{
							if (this.ContainsRichText(separatedTexts[p]))
							{
								writerText += separatedTexts[p];
								objectText.text = writerText;
								yield return new WaitForSeconds(ChatAnimationDuration);
							}
							else
							{
								for (int j = 0; j < separatedTexts[p].Length; j = num2 + 1)
								{
									writerText += separatedTexts[p][j].ToString();
									objectText.text = writerText;
									yield return new WaitForSeconds(ChatAnimationDuration);
									num2 = j;
								}
							}
							num2 = p;
						}
						separatedTexts = null;
					}
					else
					{
						for (int p = 0; p < sentenceArray[i].Length; p = num2 + 1)
						{
							writerText += sentenceArray[i][p].ToString();
							objectText.text = writerText;
							yield return new WaitForSeconds(ChatAnimationDuration);
							num2 = p;
						}
					}
				}
				num2 = i;
			}
		}
		this.dialogueController.endChat = true;
		this.startChat = false;
		this.stopChat = false;
		yield break;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00046604 File Offset: 0x00044804
	public string CutSentence_Choice(string sentence)
	{
		this.language = PlayerPrefs.GetInt("Language", 0);
		int sentence_Length_withoutReachText = this.GetSentence_Length_withoutReachText(sentence);
		if (this.language == 1)
		{
			int num;
			if (sentence_Length_withoutReachText <= 30)
			{
				num = 16;
			}
			else if (sentence_Length_withoutReachText > 30 && sentence_Length_withoutReachText <= 40)
			{
				num = this.GetSentence_Length_withoutReachText(sentence) / 2;
			}
			else
			{
				num = 15;
			}
			num = Mathf.Clamp(num, 15, 40);
			sentence = this.SettingSentence(sentence, true, num, false);
		}
		else if (this.language == 0)
		{
			int num;
			if (sentence_Length_withoutReachText <= 24)
			{
				num = 24;
			}
			else
			{
				num = this.GetSentence_Length_withoutReachText(sentence) / 2;
			}
			num = Mathf.Clamp(num, 16, 24);
			sentence = this.SettingSentence(sentence, true, num, false);
		}
		return sentence;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00013F73 File Offset: 0x00012173
	public void Chat_Obect_TmpText(TMP_Text tmp_Text, string sentence, bool isCutting = true, bool useChatAnimation = true, bool isAuto = false)
	{
		this.startChat = true;
		base.StartCoroutine(this.ObjectChat_TmpText(tmp_Text, sentence, isCutting, useChatAnimation, isAuto));
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00013F90 File Offset: 0x00012190
	private IEnumerator ObjectChat_TmpText(TMP_Text tmp_Text, string sentence, bool isCutting = true, bool useChatAnimation = true, bool isAuto = false)
	{
		int num = 30;
		int sentence_Length_withoutReachText = this.GetSentence_Length_withoutReachText(sentence);
		this.language = PlayerPrefs.GetInt("Language", 0);
		if (this.language == 1)
		{
			if (sentence_Length_withoutReachText <= 30)
			{
				num = 30;
			}
			else if (sentence_Length_withoutReachText > 30)
			{
				num = Mathf.Clamp(this.GetSentence_Length_withoutReachText(sentence) / 2, 15, 40);
			}
			sentence = this.SettingSentence(sentence, isCutting, num, false);
		}
		else if (this.language == 0)
		{
			if (sentence_Length_withoutReachText <= 50)
			{
				num = 50;
			}
			else
			{
				num = this.GetSentence_Length_withoutReachText(sentence) / 2;
			}
			num = Mathf.Clamp(num, 24, 30);
			sentence = this.SettingSentence(sentence, isCutting, num, false);
		}
		string[] sentenceArray = this.DivideSentence(sentence);
		string writerText = "";
		float ChatAnimationDuration = 0f;
		bool flag = false;
		if (isAuto)
		{
			this._isAuto = true;
			flag = true;
			ChatAnimationDuration = 0.025f;
		}
		else
		{
			this._isAuto = false;
			useChatAnimation = !DBManager.instance.showAllConversation;
			ChatAnimationDuration = DBManager.instance.showSpeed;
		}
		if (!useChatAnimation)
		{
			writerText = sentence;
			tmp_Text.text = writerText;
		}
		else
		{
			bool showWord = false;
			bool showChar = false;
			if (flag)
			{
				showWord = true;
				showChar = false;
			}
			else if (DBManager.instance.showTextByWord)
			{
				showWord = true;
				showChar = false;
				if (ChatAnimationDuration != DBManager.instance.defalutSpeed_word)
				{
					DBManager.instance.showSpeed = DBManager.instance.defalutSpeed_word;
					ChatAnimationDuration = DBManager.instance.defalutSpeed_word;
				}
			}
			else
			{
				showWord = false;
				showChar = true;
				if (ChatAnimationDuration != DBManager.instance.defalutSpeed_char)
				{
					DBManager.instance.showSpeed = DBManager.instance.defalutSpeed_char;
					ChatAnimationDuration = DBManager.instance.defalutSpeed_char;
				}
			}
			int num2;
			for (int i = 0; i < sentenceArray.Length; i = num2 + 1)
			{
				if (this.stopChat)
				{
					writerText = sentence;
					tmp_Text.text = writerText;
					break;
				}
				if (showWord)
				{
					writerText += sentenceArray[i];
					tmp_Text.text = writerText;
					yield return new WaitForSeconds(ChatAnimationDuration);
				}
				else if (showChar)
				{
					if (this.ContainsRichText(sentenceArray[i]))
					{
						List<string> separatedTexts = ChatController.SeparateText(sentenceArray[i]);
						for (int p = 0; p < separatedTexts.Count; p = num2 + 1)
						{
							if (this.ContainsRichText(separatedTexts[p]))
							{
								writerText += separatedTexts[p];
								tmp_Text.text = writerText;
							}
							else
							{
								for (int j = 0; j < separatedTexts[p].Length; j = num2 + 1)
								{
									writerText += separatedTexts[p][j].ToString();
									tmp_Text.text = writerText;
									yield return new WaitForSeconds(ChatAnimationDuration);
									num2 = j;
								}
							}
							num2 = p;
						}
						separatedTexts = null;
					}
					else
					{
						for (int p = 0; p < sentenceArray[i].Length; p = num2 + 1)
						{
							writerText += sentenceArray[i][p].ToString();
							tmp_Text.text = writerText;
							yield return new WaitForSeconds(ChatAnimationDuration);
							num2 = p;
						}
					}
				}
				num2 = i;
			}
		}
		this.dialogueController.endChat = true;
		this.startChat = false;
		this.stopChat = false;
		yield break;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x000466A8 File Offset: 0x000448A8
	public static string SplitString(string input)
	{
		int num = 30;
		string text = "";
		int i = input.Length;
		Debug.Log("입력 : " + input);
		while (i > 0)
		{
			int num2 = Math.Min(num, i);
			if (text.Length > 0 && i - num2 > num2)
			{
				num2 = (i + 1) / 2;
			}
			text = text + input.Substring(input.Length - i, num2) + "\n";
			i -= num2;
		}
		Debug.Log("출력 : " + text);
		return text.TrimEnd('\n');
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00046730 File Offset: 0x00044930
	public string SettingSentence(string sentence, bool isCutting = true, int _cutLineNum = -1, bool isBackLog = false)
	{
		if (sentence.Contains("\\n"))
		{
			if (isBackLog)
			{
				sentence = sentence.Replace("\\n", " ");
			}
			else
			{
				isCutting = false;
			}
		}
		string text = this.ReplaceSentence(sentence);
		if (isCutting)
		{
			text = this.CutSentence(text, _cutLineNum);
		}
		return text;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00046780 File Offset: 0x00044980
	public string ReplaceSentence(string sentence)
	{
		this.language = PlayerPrefs.GetInt("Language", 0);
		string text = sentence;
		if (text.Contains("<c=red>"))
		{
			text = text.Replace("<c=red>", this.redStringTag);
		}
		if (text.Contains("<c=D_red>"))
		{
			text = text.Replace("<c=D_red>", this.deepRedStringTag);
		}
		if (text.Contains("<c=yellow>"))
		{
			text = text.Replace("<c=yellow>", this.yellowStringTag);
		}
		if (text.Contains("<c=green>"))
		{
			text = text.Replace("<c=green>", this.greenStringTag);
		}
		if (text.Contains("<c=D_green>"))
		{
			text = text.Replace("<c=D_green>", this.deepGreenStringTag);
		}
		if (text.Contains("<c=pink>"))
		{
			text = text.Replace("<c=pink>", this.pinkStringTag);
		}
		if (text.Contains("<c=blue>"))
		{
			text = text.Replace("<c=blue>", this.blueStringTag);
		}
		if (text.Contains("<c=navy>"))
		{
			text = text.Replace("<c=navy>", this.navyStringTag);
		}
		if (text.Contains("<c=purple>"))
		{
			text = text.Replace("<c=purple>", this.purpleStringTag);
		}
		if (text.Contains("<c=D_pink>"))
		{
			text = text.Replace("<c=D_pink>", this.deePinkStringTag);
		}
		if (text.Contains("{name}"))
		{
			if (this.language == 1)
			{
				text = this.KoreanNameReplace(text);
			}
			else
			{
				if (DataManager._PlayerName == "")
				{
					DataManager._PlayerName = PlayerPrefs.GetString("PlayerName", "Administrator");
				}
				text = text.Replace("{name}", DataManager._PlayerName);
			}
		}
		if (text.Contains("`"))
		{
			text = text.Replace("`", ",");
		}
		if (text.Contains("\\n"))
		{
			text = text.Replace("\\n", "\n");
		}
		if (text.Contains("^"))
		{
			text = text.Replace("^", "\"");
		}
		if (text.Contains("\\t"))
		{
			text = text.Replace("\\t", "\t");
		}
		return text;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x000469A8 File Offset: 0x00044BA8
	private string KoreanNameReplace(string sentence)
	{
		string text = sentence;
		if (DataManager._PlayerName == "")
		{
			DataManager._PlayerName = PlayerPrefs.GetString("PlayerName", "Administrator");
		}
		if (text.Contains("{name}이다"))
		{
			string text2 = DataManager._PlayerName + "이";
			text2 = this.ReturnJongSung(text2, "이", "이", "가");
			if (text2 == "이")
			{
				text2 = DataManager._PlayerName + "이다";
			}
			if (text2 == "가")
			{
				text2 = DataManager._PlayerName + "다";
			}
			else
			{
				text2 = DataManager._PlayerName + "이다";
			}
			text = text.Replace("{name}이다", text2);
		}
		if (text.Contains("{name}이네"))
		{
			string text3 = DataManager._PlayerName + "이";
			text3 = this.ReturnJongSung(text3, "이", "이", "가");
			if (text3 == "이")
			{
				text3 = DataManager._PlayerName + "이네";
			}
			if (text3 == "가")
			{
				text3 = DataManager._PlayerName + "네";
			}
			else
			{
				text3 = DataManager._PlayerName + "이네";
			}
			text = text.Replace("{name}이네", text3);
		}
		if (text.Contains("{name}이랑"))
		{
			string text4 = DataManager._PlayerName + "이";
			text4 = this.ReturnJongSung(text4, "이", "이", "가");
			if (text4 == "이")
			{
				text4 = DataManager._PlayerName + "이랑";
			}
			if (text4 == "가")
			{
				text4 = DataManager._PlayerName + "랑";
			}
			else
			{
				text4 = DataManager._PlayerName + "이랑";
			}
			text = text.Replace("{name}이랑", text4);
		}
		if (text.Contains("{name}이었"))
		{
			string text5 = DataManager._PlayerName + "이";
			text5 = this.ReturnJongSung(text5, "이", "이", "가");
			if (text5 == "이")
			{
				text5 = DataManager._PlayerName + "이었";
			}
			if (text5 == "가")
			{
				text5 = DataManager._PlayerName + "였";
			}
			else
			{
				text5 = DataManager._PlayerName + "이었";
			}
			text = text.Replace("{name}이었", text5);
		}
		if (text.Contains("{name}이"))
		{
			string text6 = DataManager._PlayerName + "이";
			text6 = this.ReturnJongSung(text6, "이", "이", "가");
			if (text6 == "이")
			{
				text6 = DataManager._PlayerName + "이";
			}
			else if (text6 == "가")
			{
				text6 = DataManager._PlayerName + "가";
			}
			else
			{
				text6 = DataManager._PlayerName + "이";
			}
			text = text.Replace("{name}이", text6);
		}
		if (text.Contains("{name}은"))
		{
			string text7 = DataManager._PlayerName + "은";
			text7 = this.ReturnJongSung(text7, "은", "은", "는");
			text7 = DataManager._PlayerName + text7;
			text = text.Replace("{name}은", text7);
		}
		if (text.Contains("{name}과"))
		{
			string text8 = DataManager._PlayerName + "과";
			text8 = this.ReturnJongSung(text8, "과", "과", "와");
			text8 = DataManager._PlayerName + text8;
			text = text.Replace("{name}과", text8);
		}
		if (text.Contains("{name}을"))
		{
			string text9 = DataManager._PlayerName + "을";
			text9 = this.ReturnJongSung(text9, "을", "을", "를");
			text9 = DataManager._PlayerName + text9;
			text = text.Replace("{name}을", text9);
		}
		return text.Replace("{name}", DataManager._PlayerName);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00046DCC File Offset: 0x00044FCC
	public string ConvertKoreaStringJongSung(string koreaString, string check, string first, string second)
	{
		string text = string.Empty;
		string[] array = koreaString.Split(new string[] { check }, StringSplitOptions.None);
		if (array.Length >= 2)
		{
			for (int i = 0; i < array.Length - 1; i++)
			{
				int num = 1;
				if (array[i].Length > 0)
				{
					char[] array2 = array[i].ToCharArray(0, array[i].Length);
					if (array2.Length != 0 && array2[array2.Length - 1] == ' ')
					{
						num = 2;
					}
					char[] array3 = array[i].ToCharArray(array[i].Length - num, 1);
					if (array3.Length != 0)
					{
						if (array3[0] >= '가' && array3[0] <= '힣')
						{
							string text2 = (((array3[0] - '가') % '\u001c' > '\0') ? first : second);
							array[i] += text2;
						}
						else if (array3[0] == '0' || array3[0] == '1' || array3[0] == '3' || array3[0] == '6' || array3[0] == '7' || array3[0] == '8')
						{
							array[i] += first;
						}
						else if (array3[0] == '2' || array3[0] == '4' || array3[0] == '5' || array3[0] == '9')
						{
							array[i] += second;
						}
					}
				}
				text += array[i];
			}
			text += array[array.Length - 1];
			koreaString = text;
		}
		return koreaString;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00046F2C File Offset: 0x0004512C
	public string ReturnJongSung(string koreaString, string check, string first, string second)
	{
		string text = string.Empty;
		string[] array = koreaString.Split(new string[] { check }, StringSplitOptions.None);
		if (array.Length >= 2)
		{
			for (int i = 0; i < array.Length - 1; i++)
			{
				int num = 1;
				if (array[i].Length > 0)
				{
					char[] array2 = array[i].ToCharArray(0, array[i].Length);
					if (array2.Length != 0 && array2[array2.Length - 1] == ' ')
					{
						num = 2;
					}
					char[] array3 = array[i].ToCharArray(array[i].Length - num, 1);
					if (array3.Length != 0)
					{
						if (array3[0] >= '가' && array3[0] <= '힣')
						{
							string text2 = (((array3[0] - '가') % '\u001c' > '\0') ? first : second);
							return text2.ToString();
						}
						if (array3[0] == '0' || array3[0] == '1' || array3[0] == '3' || array3[0] == '6' || array3[0] == '7' || array3[0] == '8')
						{
							array[i] += first;
						}
						else
						{
							if (array3[0] != '2' && array3[0] != '4' && array3[0] != '5' && array3[0] != '9')
							{
								return first;
							}
							array[i] += second;
						}
					}
				}
				text += array[i];
			}
			text += array[array.Length - 1];
			koreaString = text;
		}
		return koreaString;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00047088 File Offset: 0x00045288
	private int GetSentence_Length_withoutReachText(string word)
	{
		string text = word;
		if (text.Contains(this.redStringTag))
		{
			text = text.Replace(this.redStringTag, "");
		}
		if (text.Contains(this.deepRedStringTag))
		{
			text = text.Replace(this.deepRedStringTag, "");
		}
		if (text.Contains("</color>"))
		{
			text = text.Replace("</color>", "");
		}
		if (text.Contains(this.yellowStringTag))
		{
			text = text.Replace(this.yellowStringTag, "");
		}
		if (text.Contains("</color>"))
		{
			text = text.Replace("</color>", "");
		}
		if (text.Contains(this.greenStringTag))
		{
			text = text.Replace(this.greenStringTag, "");
		}
		if (text.Contains(this.deepGreenStringTag))
		{
			text = text.Replace(this.deepGreenStringTag, "");
		}
		if (text.Contains(this.pinkStringTag))
		{
			text = text.Replace(this.pinkStringTag, "");
		}
		if (text.Contains(this.blueStringTag))
		{
			text = text.Replace(this.blueStringTag, "");
		}
		if (text.Contains(this.navyStringTag))
		{
			text = text.Replace(this.navyStringTag, "");
		}
		if (text.Contains(this.purpleStringTag))
		{
			text = text.Replace(this.purpleStringTag, "");
		}
		if (text.Contains(this.deePinkStringTag))
		{
			text = text.Replace(this.deePinkStringTag, "");
		}
		if (text.Contains("</color>"))
		{
			text = text.Replace("</color>", "");
		}
		if (text.Contains("<b>"))
		{
			text = text.Replace("<b>", "");
		}
		if (text.Contains("</b>"))
		{
			text = text.Replace("</b>", "");
		}
		if (text.Contains("{name}"))
		{
			text = text.Replace("{name}", "");
		}
		if (text.Contains("`"))
		{
			text = text.Replace("`", "");
		}
		return text.Length;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000472B0 File Offset: 0x000454B0
	public string CutSentence(string sentence, int _cutLineNum = -1)
	{
		int num = this.cutLineNum;
		if (_cutLineNum != -1)
		{
			num = _cutLineNum;
		}
		string[] array = sentence.Split(' ', StringSplitOptions.None);
		string text = array[0];
		int num2 = this.GetSentence_Length_withoutReachText(array[0]);
		for (int i = 1; i < array.Length; i++)
		{
			int num3 = this.GetSentence_Length_withoutReachText(array[i]) + 1;
			if (i == array.Length - 1)
			{
				num2 = this.GetSentence_Length_withoutReachText(array[i]);
				if (num2 <= _cutLineNum / 2)
				{
					text = text + " " + array[i];
				}
				else if (num3 <= _cutLineNum)
				{
					text = text + " " + array[i];
				}
				else
				{
					text = text + "\n" + array[i];
				}
			}
			else if (num2 + num3 < num)
			{
				num2 += num3;
				text = text + " " + array[i];
			}
			else
			{
				num2 = this.GetSentence_Length_withoutReachText(array[i]);
				text = text + "\n" + array[i];
			}
		}
		return text;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000473A0 File Offset: 0x000455A0
	private string[] DivideSentence(string sentence)
	{
		string text = "<b>";
		string text2 = "</b>";
		string text3 = this.redStringTag;
		string text4 = this.deepRedStringTag;
		string text5 = this.yellowStringTag;
		string text6 = this.greenStringTag;
		string text7 = this.deepGreenStringTag;
		string text8 = this.pinkStringTag;
		string text9 = this.blueStringTag;
		string text10 = this.navyStringTag;
		string text11 = this.purpleStringTag;
		string text12 = this.deePinkStringTag;
		string text13 = "</color>";
		string text14 = "";
		bool flag = false;
		if (sentence.Contains(text3))
		{
			text14 = text3;
		}
		if (sentence.Contains(text4))
		{
			text14 = text4;
		}
		else if (sentence.Contains(text5))
		{
			text14 = text5;
		}
		else if (sentence.Contains(text6))
		{
			text14 = text6;
		}
		else if (sentence.Contains(text7))
		{
			text14 = text7;
		}
		else if (sentence.Contains(text8))
		{
			text14 = text8;
		}
		else if (sentence.Contains(text9))
		{
			text14 = text9;
		}
		else if (sentence.Contains(text10))
		{
			text14 = text10;
		}
		else if (sentence.Contains(text11))
		{
			text14 = text11;
		}
		else if (sentence.Contains(text12))
		{
			text14 = text12;
		}
		else
		{
			flag = true;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		string[] array = sentence.Split(' ', StringSplitOptions.None);
		bool flag2 = false;
		int num = 0;
		bool flag3 = false;
		int num2 = 0;
		bool flag4 = false;
		int num3 = 0;
		bool flag5 = false;
		int num4 = 0;
		this.WordPriview = new List<string>();
		this.WordPriview.Clear();
		this.combineWord = "";
		this.WordPriview_2 = new List<string>();
		this.WordPriview_2.Clear();
		this.combineWord_2 = "";
		foreach (string text15 in array)
		{
			string text16 = text15;
			bool flag6 = false;
			List<string> list3 = new List<string>();
			List<int> list4 = new List<int>();
			if (text15.Contains(text))
			{
				flag2 = true;
				num = text15.IndexOf(text);
				list4.Add(num);
			}
			if (text15.Contains(text2))
			{
				flag3 = true;
				num2 = text15.IndexOf(text2);
				list4.Add(num2);
			}
			if (text15.Contains(text14))
			{
				flag4 = true;
				num3 = text15.IndexOf(text14);
				list4.Add(num3);
			}
			if (text15.Contains(text13))
			{
				flag5 = true;
				num4 = text15.IndexOf(text13);
				list4.Add(num4);
			}
			if (text16.Contains(text))
			{
				text16.Replace(text, "$B");
			}
			if (text16.Contains(text2))
			{
				text16.Replace(text2, "$V");
			}
			if (!flag && text16.Contains(text14))
			{
				text16.Replace(text14, "$C");
			}
			if (text16.Contains(text13))
			{
				text16.Replace(text13, "$E");
			}
			this.WordPriview.Add(text16 + " ");
			list4.Sort();
			foreach (int num5 in list4)
			{
				if (flag2 && num == num5)
				{
					list3.Add(text);
				}
				if (flag3 && num2 == num5)
				{
					list3.Add(text2);
				}
				if (flag4 && num3 == num5)
				{
					list3.Add(text14);
				}
				if (flag5 && num4 == num5)
				{
					list3.Add(text13);
				}
			}
			for (int j = 0; j < text15.Length; j++)
			{
				while (list3.Count != 0)
				{
					if (list3[0] == text)
					{
						if (num != j)
						{
							break;
						}
						j += text.Length;
						list2.Add(text);
						flag2 = false;
						list3.RemoveAt(0);
						if (list3.Count == 0)
						{
							break;
						}
					}
					if (list3[0] == text2)
					{
						if (num2 != j)
						{
							break;
						}
						j += text2.Length;
						list2.Remove(text);
						flag3 = false;
						list3.RemoveAt(0);
						if (list3.Count == 0)
						{
							break;
						}
					}
					if (list3[0] == text14)
					{
						if (num3 != j)
						{
							break;
						}
						j += text14.Length;
						list2.Add(text14);
						flag4 = false;
						list3.RemoveAt(0);
						if (list3.Count == 0)
						{
							break;
						}
					}
					if (list3[0] == text13)
					{
						if (num4 != j)
						{
							break;
						}
						j += text13.Length;
						list2.Remove(text14);
						flag5 = false;
						list3.RemoveAt(0);
						if (list3.Count == 0)
						{
							break;
						}
					}
				}
				if (j >= text15.Length)
				{
					break;
				}
				string text17 = "";
				string text18 = "";
				if (list2.Count > 0)
				{
					for (int k = 0; k < list2.Count; k++)
					{
						text17 += list2[k];
					}
					for (int l = list2.Count - 1; l >= 0; l--)
					{
						if (list2[l] == text)
						{
							text18 += text2;
						}
						else if (!flag && list2[l] == text14)
						{
							text18 += text13;
						}
					}
				}
				text17 += text15[j].ToString();
				if (text18 != "")
				{
					text17 += text18;
				}
				list.Add(text17);
				if (text17 == "\n")
				{
					flag6 = true;
				}
			}
			if (!flag6)
			{
				list.Add(" ");
			}
		}
		for (int m = 0; m < this.WordPriview.Count; m++)
		{
			if (this.WordPriview[m].Contains("$B"))
			{
				this.WordPriview[m].Replace("$B", text);
			}
			if (this.WordPriview[m].Contains("$V"))
			{
				this.WordPriview[m].Replace("$V", text2);
			}
			if (this.WordPriview[m].Contains("$C"))
			{
				this.WordPriview[m].Replace("$C", text14);
			}
			if (this.WordPriview[m].Contains("$E"))
			{
				this.WordPriview[m].Replace("$E", text13);
			}
			this.combineWord = this.combineWord + this.WordPriview[m] + " ";
		}
		for (int n = 0; n < list.Count; n++)
		{
			this.WordPriview_2.Add(list[n]);
			this.combineWord_2 = this.combineWord_2 + list[n] + " ";
		}
		return this.WordPriview.ToArray();
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00047A84 File Offset: 0x00045C84
	private bool ContainsRichText(string text)
	{
		string text2 = "<.*?>";
		return Regex.IsMatch(text, text2);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00047AA0 File Offset: 0x00045CA0
	public static List<string> SeparateText(string input)
	{
		List<string> list = new List<string>();
		string text = "(<[^>]+>)|([^<]+)";
		foreach (object obj in Regex.Matches(input, text))
		{
			Match match = (Match)obj;
			list.Add(match.Value);
		}
		return list;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00013FC4 File Offset: 0x000121C4
	public void Chat_Obect_TmpText_ChangeSpeed(TMP_Text tmp_Text, string sentence, bool isCutting = true, bool isAuto = false, float ChangeSpeed = 0.05f)
	{
		this.startChat = true;
		base.StartCoroutine(this.ObjectChat_TmpText_ChangeSpeed_co(tmp_Text, sentence, isCutting, isAuto, ChangeSpeed));
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00013FE1 File Offset: 0x000121E1
	private IEnumerator ObjectChat_TmpText_ChangeSpeed_co(TMP_Text tmp_Text, string sentence, bool isCutting = true, bool isAuto = false, float ChangeSpeed = 0.05f)
	{
		this.language = PlayerPrefs.GetInt("Language", 0);
		this.dialogueController.endChat = false;
		int num = 30;
		int num2 = 40;
		if (this.language == 0)
		{
			num = 40;
			num2 = 50;
		}
		else if (this.language == 1)
		{
			num = 30;
			num2 = 40;
		}
		if (sentence.Length > num)
		{
			num = Mathf.Clamp(this.GetSentence_Length_withoutReachText(sentence) / 2, num / 2, num2);
		}
		sentence = this.SettingSentence(sentence, isCutting, num, false);
		string[] sentenceArray = this.DivideSentence(sentence);
		string writerText = "";
		int num3;
		for (int i = 0; i < sentenceArray.Length; i = num3 + 1)
		{
			if (this.ContainsRichText(sentenceArray[i]))
			{
				List<string> separatedTexts = ChatController.SeparateText(sentenceArray[i]);
				for (int p = 0; p < separatedTexts.Count; p = num3 + 1)
				{
					if (this.ContainsRichText(separatedTexts[p]))
					{
						writerText += separatedTexts[p];
						tmp_Text.text = writerText;
					}
					else
					{
						for (int j = 0; j < separatedTexts[p].Length; j = num3 + 1)
						{
							writerText += separatedTexts[p][j].ToString();
							tmp_Text.text = writerText;
							yield return new WaitForSeconds(ChangeSpeed);
							num3 = j;
						}
					}
					num3 = p;
				}
				separatedTexts = null;
			}
			else
			{
				for (int p = 0; p < sentenceArray[i].Length; p = num3 + 1)
				{
					writerText += sentenceArray[i][p].ToString();
					tmp_Text.text = writerText;
					yield return new WaitForSeconds(ChangeSpeed);
					num3 = p;
				}
			}
			num3 = i;
		}
		this.dialogueController.endChat = true;
		this.startChat = false;
		this.stopChat = false;
		yield break;
	}

	// Token: 0x040009F9 RID: 2553
	public Camera mainCamera;

	// Token: 0x040009FA RID: 2554
	private EventDialogueController eventDialogueController;

	// Token: 0x040009FB RID: 2555
	private DialogueController dialogueController;

	// Token: 0x040009FC RID: 2556
	public Transform speechBubbleParent;

	// Token: 0x040009FD RID: 2557
	public Transform emotionBubbleParent;

	// Token: 0x040009FE RID: 2558
	public Transform ChocieBoxParent;

	// Token: 0x040009FF RID: 2559
	public int cutLineNum = 22;

	// Token: 0x04000A00 RID: 2560
	private bool startChat;

	// Token: 0x04000A01 RID: 2561
	public bool stopChat;

	// Token: 0x04000A02 RID: 2562
	public GameObject playerSelectionWindow;

	// Token: 0x04000A03 RID: 2563
	public GameObject playerSelectionWindow_one;

	// Token: 0x04000A04 RID: 2564
	public string redStringTag = "<#FF6467>";

	// Token: 0x04000A05 RID: 2565
	public string deepRedStringTag = "<#A90D0D>";

	// Token: 0x04000A06 RID: 2566
	public string yellowStringTag = "<#FFC800>";

	// Token: 0x04000A07 RID: 2567
	public string greenStringTag = "<#00FF98>";

	// Token: 0x04000A08 RID: 2568
	public string deepGreenStringTag = "<#3FA74A>";

	// Token: 0x04000A09 RID: 2569
	public string pinkStringTag = "<#FF8EE9>";

	// Token: 0x04000A0A RID: 2570
	public string blueStringTag = "<#64DDFF>";

	// Token: 0x04000A0B RID: 2571
	public string navyStringTag = "<#4E7FFF>";

	// Token: 0x04000A0C RID: 2572
	public string purpleStringTag = "<#B770FF>";

	// Token: 0x04000A0D RID: 2573
	public string deePinkStringTag = "<#EB4FEB>";

	// Token: 0x04000A0E RID: 2574
	private int language;

	// Token: 0x04000A0F RID: 2575
	private bool _isAuto;

	// Token: 0x04000A10 RID: 2576
	public List<string> WordPriview;

	// Token: 0x04000A11 RID: 2577
	[TextArea(5, 5)]
	public string combineWord = "";

	// Token: 0x04000A12 RID: 2578
	public List<string> WordPriview_2;

	// Token: 0x04000A13 RID: 2579
	[TextArea(5, 5)]
	public string combineWord_2 = "";

	// Token: 0x02000192 RID: 402
	public enum BubblePos
	{
		// Token: 0x04000A15 RID: 2581
		Bubble,
		// Token: 0x04000A16 RID: 2582
		rightBubble,
		// Token: 0x04000A17 RID: 2583
		leftBubble,
		// Token: 0x04000A18 RID: 2584
		emotionBubble
	}

	// Token: 0x02000193 RID: 403
	public enum BubbleType
	{
		// Token: 0x04000A1A RID: 2586
		NormalBubble,
		// Token: 0x04000A1B RID: 2587
		SilentBubble,
		// Token: 0x04000A1C RID: 2588
		ShoutBubble,
		// Token: 0x04000A1D RID: 2589
		AngryBubble,
		// Token: 0x04000A1E RID: 2590
		imaginaryBubble
	}
}
