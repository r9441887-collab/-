using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001AA RID: 426
[Serializable]
public class DataParser
{
	// Token: 0x060009CA RID: 2506 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void Init()
	{
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0004B104 File Offset: 0x00049304
	public WinionCommonInfo WinionCommonInfoParser(string csvFileName)
	{
		WinionCommonInfo winionCommonInfo = new WinionCommonInfo();
		List<WinionAppearance> list = new List<WinionAppearance>();
		List<WinionFood> list2 = new List<WinionFood>();
		List<WinionPersonality> list3 = new List<WinionPersonality>();
		List<WinionEmotion> list4 = new List<WinionEmotion>();
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int num4 = 3;
		int num5 = 4;
		int num6 = 5;
		int num7 = 6;
		int num8 = 7;
		int num9 = 8;
		int num10 = 9;
		string[] array = Resources.Load<TextAsset>("CsvFiles/" + csvFileName).text.Split(new char[] { '\n' });
		for (int i = 1; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			WinionAppearance winionAppearance = new WinionAppearance();
			WinionFood winionFood = new WinionFood();
			WinionPersonality winionPersonality = new WinionPersonality();
			WinionEmotion winionEmotion = new WinionEmotion();
			if (array2[num].ToString() != "")
			{
				int num11 = 0;
				if (int.TryParse(array2[num].ToString(), out num11))
				{
					winionAppearance.id = num11;
				}
				winionAppearance.prefabsName = array2[num2].ToString();
				list.Add(winionAppearance);
			}
			if (array2[num3].ToString() != "")
			{
				int num12 = 0;
				if (int.TryParse(array2[num3].ToString(), out num12))
				{
					winionFood.id = num12;
				}
				winionFood.foodName = array2[num4].ToString();
				list2.Add(winionFood);
			}
			if (array2[num5].ToString() != "")
			{
				int num13 = 0;
				if (int.TryParse(array2[num5].ToString(), out num13))
				{
					winionPersonality.id = num13;
				}
				winionPersonality.presonalityName = array2[num6].ToString();
				list3.Add(winionPersonality);
			}
			if (array2[num7].ToString() != "")
			{
				int num14 = 0;
				if (int.TryParse(array2[num7].ToString(), out num14))
				{
					winionEmotion.id = num14;
				}
				winionEmotion.EmotionName = array2[num8].ToString();
				list4.Add(winionEmotion);
			}
			if (array2[num9].ToString() != "")
			{
				string text = array2[num9].ToString();
				DBManager.EmotionSpeechBubble emotionSpeechBubble = DBManager.instance.ReplaceIdToEmotionBubble(text);
				if (emotionSpeechBubble != DBManager.EmotionSpeechBubble.None)
				{
					DBManager.instance.SettingsDictionary(emotionSpeechBubble, array2[num10].ToString());
				}
			}
		}
		winionCommonInfo.winionAppearancesList = list;
		winionCommonInfo.winionFoodsList = list2;
		winionCommonInfo.winionPersonalitiesList = list3;
		winionCommonInfo.winionEmotionsList = list4;
		return winionCommonInfo;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0004B364 File Offset: 0x00049564
	public List<EventDialogue> EventDialogueParser(string csvFileName)
	{
		bool flag = false;
		List<EventDialogue> list = new List<EventDialogue>();
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int num4 = 3;
		int num5 = 4;
		int num6 = 5;
		int num7 = 6;
		int num8 = 7;
		int num9 = 8;
		int @int = PlayerPrefs.GetInt("Language", 0);
		TextAsset textAsset;
		if (@int == 0)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/EN/" + csvFileName);
		}
		else if (@int == 1)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		else
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		string[] array = textAsset.text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			EventDialogue eventDialogue = new EventDialogue();
			eventDialogue.event_Dialogues = new List<Dialogue>();
			int num10 = 0;
			if (int.TryParse(array2[num].ToString(), out num10))
			{
				eventDialogue.eventNum = num10;
			}
			do
			{
				Dialogue dialogue = new Dialogue();
				List<DetailLine> list2 = new List<DetailLine>();
				int num11 = 0;
				if (int.TryParse(array2[num2].ToString(), out num11))
				{
					dialogue.eventDetailNum = num11;
				}
				do
				{
					DetailLine detailLine = new DetailLine();
					List<Line> list3 = new List<Line>();
					for (;;)
					{
						Line line = new Line();
						List<int> list4 = new List<int>();
						List<ChatController.BubbleType> list5 = new List<ChatController.BubbleType>();
						List<int> list6 = new List<int>();
						List<string> list7 = new List<string>();
						List<List<string>> list8 = new List<List<string>>();
						List<int> list9 = new List<int>();
						Choice choice = new Choice();
						List<string> list10 = new List<string>();
						int num12 = 0;
						string text = array2[num4].ToString().Trim();
						int num13 = 0;
						if (int.TryParse(text, out num13))
						{
							line.nameID = num13;
						}
						else
						{
							List<WinionPersonality> winionPersonalitiesList = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
							for (int j = 0; j < winionPersonalitiesList.Count; j++)
							{
								if (text == winionPersonalitiesList[j].presonalityName)
								{
									line.nameID = winionPersonalitiesList[j].id;
									break;
								}
							}
						}
						do
						{
							string text2 = array2[num5].ToString();
							ChatController.BubbleType bubbleType = this.FindBubbleType(text2);
							if (bubbleType != ChatController.BubbleType.NormalBubble)
							{
								line.haveSpecialBubble = true;
								list4.Add(num12);
								list5.Add(bubbleType);
								string text3 = this.FindBubbleTypeString(bubbleType);
								text2 = text2.Replace(text3, "");
							}
							string text4 = this.FindEmotionBubble(text2);
							if (text4 != "")
							{
								line.haveEmotionSpeechBubble = true;
								list6.Add(num12);
								list7.Add(text4);
								text2 = text2.Replace(text4, "");
							}
							if (array2[num9].ToString().Trim() != "")
							{
								line.changeFace = true;
								List<string> list11 = DBManager.instance.winionFaceInfo.DivideSentence(array2[num9].ToString().Trim());
								list8.Add(list11);
								list9.Add(num12);
							}
							list10.Add(text2);
							num12++;
							if (array2[num6].ToString() != "")
							{
								int num14 = 0;
								if (int.TryParse(array2[num6].ToString(), out num14) && num14 == 1)
								{
									line.haveChoice = true;
									int num15 = 0;
									int num16 = i;
									choice.onlyOneChoice = false;
									for (int k = 0; k < 2; k++)
									{
										num16++;
										if (string.IsNullOrWhiteSpace(array[num16].Split(new char[] { ',' })[num7].ToString()))
										{
											choice.onlyOneChoice = true;
											break;
										}
									}
									int num17 = 2;
									if (choice.onlyOneChoice)
									{
										num17 = 1;
									}
									for (int l = 0; l < num17; l++)
									{
										if (++i < array.Length)
										{
											array2 = array[i].Split(new char[] { ',' });
										}
										if (l == 0)
										{
											choice.firstOption = array2[num7].ToString();
											List<string> list12 = new List<string>();
											if (array2[num8].ToString().Contains("_"))
											{
												list12 = array2[num8].ToString().Split('_', StringSplitOptions.None).ToList<string>();
											}
											else
											{
												list12.Add(array2[num8].ToString());
											}
											if (int.TryParse(list12[0], out num15))
											{
												choice.firstOptDialogNum = num15;
												if (list12.Count > 1)
												{
													choice.haveCommon = true;
													if (int.TryParse(list12[1], out num15))
													{
														choice.commonDialogNum = num15;
													}
												}
											}
										}
										else if (l == 1)
										{
											choice.secondOption = array2[num7].ToString();
											List<string> list13 = new List<string>();
											if (array2[num8].ToString().Contains("_"))
											{
												list13 = array2[num8].ToString().Split('_', StringSplitOptions.None).ToList<string>();
											}
											else
											{
												list13.Add(array2[num8].ToString());
											}
											if (int.TryParse(list13[0], out num15))
											{
												choice.secondOptDialogNum = num15;
												if (list13.Count > 1)
												{
													choice.haveCommon = true;
													if (int.TryParse(list13[1], out num15))
													{
														choice.commonDialogNum = num15;
													}
												}
											}
										}
									}
								}
							}
							if (++i >= array.Length)
							{
								goto IL_0534;
							}
							array2 = array[i].Split(new char[] { ',' });
						}
						while (array2[num4].ToString() == "");
						IL_0551:
						if (line.haveChoice)
						{
							line.choice = choice;
						}
						line.context = list10.ToArray();
						if (line.haveSpecialBubble)
						{
							line.bubbleTypeIndex = list4;
							line.bubbleTypeList = list5;
						}
						if (line.haveEmotionSpeechBubble)
						{
							line.emotionSpeechBubble_contextIndex = list6;
							line.emotionSpeechBubble_ID = list7;
						}
						if (line.changeFace)
						{
							line.changeExpression = list8;
							line.changeExpression_Index = list9;
						}
						list3.Add(line);
						if (flag || !(array2[num3].ToString() == ""))
						{
							break;
						}
						continue;
						IL_0534:
						flag = true;
						goto IL_0551;
					}
					detailLine.lines = list3;
					list2.Add(detailLine);
				}
				while (!flag && array2[num2].ToString() == "");
				dialogue.d_lines = list2;
				eventDialogue.event_Dialogues.Add(dialogue);
			}
			while (!flag && array2[num].ToString() == "");
			list.Add(eventDialogue);
		}
		return list;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0004B9CC File Offset: 0x00049BCC
	public List<DialogueByEvent> DialogueParser_byEvent(string csvFileName)
	{
		bool flag = false;
		List<DialogueByEvent> list = new List<DialogueByEvent>();
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int num4 = 3;
		int num5 = 4;
		int num6 = 5;
		int num7 = 6;
		int num8 = 7;
		int num9 = 8;
		int @int = PlayerPrefs.GetInt("Language", 0);
		TextAsset textAsset;
		if (@int == 0)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/EN/" + csvFileName);
		}
		else if (@int == 1)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		else
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		string[] array = textAsset.text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			DialogueByEvent dialogueByEvent = new DialogueByEvent();
			int num10 = 0;
			if (int.TryParse(array2[num].ToString(), out num10))
			{
				dialogueByEvent.eventNum = num10;
			}
			new List<string>();
			string text = array2[num2].ToString();
			if (text.Contains("_[반복]"))
			{
				text = text.Replace("_[반복]", "");
				dialogueByEvent.repeat = true;
			}
			else
			{
				dialogueByEvent.repeat = false;
			}
			int num11 = 0;
			if (int.TryParse(text, out num11))
			{
				dialogueByEvent.detailNum = num11;
			}
			string text2 = array2[num3].ToString().Trim();
			int num12 = 0;
			if (int.TryParse(text2, out num12))
			{
				dialogueByEvent.winionIndex = num12;
			}
			else
			{
				List<WinionPersonality> winionPersonalitiesList = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
				for (int j = 0; j < winionPersonalitiesList.Count; j++)
				{
					if (text2 == winionPersonalitiesList[j].presonalityName)
					{
						dialogueByEvent.winionIndex = winionPersonalitiesList[j].id;
						break;
					}
				}
			}
			Dialogue dialogue = new Dialogue();
			List<DetailLine> list2 = new List<DetailLine>();
			do
			{
				DetailLine detailLine = new DetailLine();
				List<Line> list3 = new List<Line>();
				for (;;)
				{
					Line line = new Line();
					List<int> list4 = new List<int>();
					List<ChatController.BubbleType> list5 = new List<ChatController.BubbleType>();
					List<int> list6 = new List<int>();
					List<string> list7 = new List<string>();
					Choice choice = new Choice();
					List<string> list8 = new List<string>();
					int num13 = 0;
					string text3 = array2[num5].ToString().Trim();
					int num14 = 0;
					if (int.TryParse(text3, out num14))
					{
						line.nameID = num14;
					}
					else
					{
						List<WinionPersonality> winionPersonalitiesList2 = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
						for (int k = 0; k < winionPersonalitiesList2.Count; k++)
						{
							if (text3 == winionPersonalitiesList2[k].presonalityName)
							{
								line.nameID = winionPersonalitiesList2[k].id;
								break;
							}
						}
					}
					do
					{
						string text4 = array2[num6].ToString();
						ChatController.BubbleType bubbleType = this.FindBubbleType(text4);
						if (bubbleType != ChatController.BubbleType.NormalBubble)
						{
							line.haveSpecialBubble = true;
							list4.Add(num13);
							list5.Add(bubbleType);
							string text5 = this.FindBubbleTypeString(bubbleType);
							text4 = text4.Replace(text5, "");
						}
						string text6 = this.FindEmotionBubble(text4);
						if (text6 != "")
						{
							line.haveEmotionSpeechBubble = true;
							list6.Add(num13);
							list7.Add(text6);
							text4 = text4.Replace(text6, "");
						}
						list8.Add(text4);
						num13++;
						if (array2[num7].ToString() != "")
						{
							int num15 = 0;
							if (int.TryParse(array2[num7].ToString(), out num15) && num15 == 1)
							{
								line.haveChoice = true;
								int num16 = 0;
								int num17 = i;
								choice.onlyOneChoice = false;
								for (int l = 0; l < 2; l++)
								{
									num17++;
									if (string.IsNullOrWhiteSpace(array[num17].Split(new char[] { ',' })[num8].ToString()))
									{
										choice.onlyOneChoice = true;
										break;
									}
								}
								int num18 = 2;
								if (choice.onlyOneChoice)
								{
									num18 = 1;
								}
								for (int m = 0; m < num18; m++)
								{
									if (++i < array.Length)
									{
										array2 = array[i].Split(new char[] { ',' });
									}
									if (m == 0)
									{
										choice.firstOption = array2[num8].ToString();
										List<string> list9 = new List<string>();
										if (array2[num9].ToString().Contains("_"))
										{
											list9 = array2[num9].ToString().Split('_', StringSplitOptions.None).ToList<string>();
										}
										else
										{
											list9.Add(array2[num9].ToString());
										}
										if (int.TryParse(list9[0], out num16))
										{
											choice.firstOptDialogNum = num16;
											if (list9.Count > 1)
											{
												choice.haveCommon = true;
												if (int.TryParse(list9[1], out num16))
												{
													choice.commonDialogNum = num16;
												}
											}
										}
									}
									else if (m == 1)
									{
										choice.secondOption = array2[num8].ToString();
										List<string> list10 = new List<string>();
										if (array2[num9].ToString().Contains("_"))
										{
											list10 = array2[num9].ToString().Split('_', StringSplitOptions.None).ToList<string>();
										}
										else
										{
											list10.Add(array2[num9].ToString());
										}
										if (int.TryParse(list10[0], out num16))
										{
											choice.secondOptDialogNum = num16;
											if (list10.Count > 1)
											{
												choice.haveCommon = true;
												if (int.TryParse(list10[1], out num16))
												{
													choice.commonDialogNum = num16;
												}
											}
										}
									}
								}
							}
						}
						if (++i >= array.Length)
						{
							goto IL_058A;
						}
						array2 = array[i].Split(new char[] { ',' });
					}
					while (array2[num5].ToString() == "");
					IL_05A7:
					if (line.haveChoice)
					{
						line.choice = choice;
					}
					line.context = list8.ToArray();
					if (line.haveSpecialBubble)
					{
						line.bubbleTypeIndex = list4;
						line.bubbleTypeList = list5;
					}
					if (line.haveEmotionSpeechBubble)
					{
						line.emotionSpeechBubble_contextIndex = list6;
						line.emotionSpeechBubble_ID = list7;
					}
					list3.Add(line);
					if (flag || !(array2[num4].ToString() == ""))
					{
						break;
					}
					continue;
					IL_058A:
					flag = true;
					goto IL_05A7;
				}
				detailLine.lines = list3;
				list2.Add(detailLine);
			}
			while (!flag && array2[num3].ToString() == "");
			dialogue.d_lines = list2;
			dialogueByEvent.dialogue = dialogue;
			list.Add(dialogueByEvent);
		}
		return list;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0004C050 File Offset: 0x0004A250
	public List<DialogueByBehavior> DialogueParser_byBehavior(string csvFileName)
	{
		bool flag = false;
		List<DialogueByBehavior> list = new List<DialogueByBehavior>();
		int num = 0;
		int num2 = 1;
		int num3 = 4;
		int num4 = 5;
		string[] array = Resources.Load<TextAsset>("CsvFiles/" + csvFileName).text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			DialogueByBehavior dialogueByBehavior = new DialogueByBehavior();
			int num5 = 0;
			if (int.TryParse(array2[num].ToString(), out num5))
			{
				dialogueByBehavior.behaviorNum = num5;
			}
			string text = array2[num2].ToString().Trim();
			int num6 = 0;
			if (int.TryParse(text, out num6))
			{
				dialogueByBehavior.winionIndex = num6;
			}
			else
			{
				List<WinionPersonality> winionPersonalitiesList = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
				for (int j = 0; j < winionPersonalitiesList.Count; j++)
				{
					if (text == winionPersonalitiesList[j].presonalityName)
					{
						dialogueByBehavior.winionIndex = winionPersonalitiesList[j].id;
						break;
					}
				}
			}
			List<LineByBehavior> list2 = new List<LineByBehavior>();
			for (;;)
			{
				LineByBehavior lineByBehavior = new LineByBehavior();
				List<int> list3 = new List<int>();
				List<ChatController.BubbleType> list4 = new List<ChatController.BubbleType>();
				List<int> list5 = new List<int>();
				List<string> list6 = new List<string>();
				List<string> list7 = new List<string>();
				int num7 = 0;
				string text2 = array2[num3].ToString().Trim();
				int num8 = 0;
				if (int.TryParse(text2, out num8))
				{
					lineByBehavior.nameID = num8;
				}
				else
				{
					List<WinionPersonality> winionPersonalitiesList2 = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
					for (int k = 0; k < winionPersonalitiesList2.Count; k++)
					{
						if (text2 == winionPersonalitiesList2[k].presonalityName)
						{
							lineByBehavior.nameID = winionPersonalitiesList2[k].id;
							break;
						}
					}
				}
				do
				{
					string text3 = array2[num4].ToString();
					ChatController.BubbleType bubbleType = this.FindBubbleType(text3);
					if (bubbleType != ChatController.BubbleType.NormalBubble)
					{
						lineByBehavior.haveSpecialBubble = true;
						list3.Add(num7);
						list4.Add(bubbleType);
						string text4 = this.FindBubbleTypeString(bubbleType);
						text3 = text3.Replace(text4, "");
					}
					string text5 = this.FindEmotionBubble(text3);
					if (text5 != "")
					{
						lineByBehavior.haveEmotionSpeechBubble = true;
						list5.Add(num7);
						list6.Add(text5);
						text3 = text3.Replace(text5, "");
					}
					list7.Add(text3);
					num7++;
					if (++i >= array.Length)
					{
						goto IL_0285;
					}
					array2 = array[i].Split(new char[] { ',' });
				}
				while (array2[num3].ToString() == "");
				IL_02A2:
				lineByBehavior.context = list7.ToArray();
				if (lineByBehavior.haveSpecialBubble)
				{
					lineByBehavior.bubbleTypeIndex = list3;
					lineByBehavior.bubbleTypeList = list4;
				}
				if (lineByBehavior.haveEmotionSpeechBubble)
				{
					lineByBehavior.emotionSpeechBubble_contextIndex = list5;
					lineByBehavior.emotionSpeechBubble_ID = list6;
				}
				list2.Add(lineByBehavior);
				if (flag || !(array2[num].ToString() == ""))
				{
					break;
				}
				continue;
				IL_0285:
				flag = true;
				goto IL_02A2;
			}
			dialogueByBehavior.dialogue = list2;
			list.Add(dialogueByBehavior);
			if (flag)
			{
				break;
			}
		}
		return list;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0004C388 File Offset: 0x0004A588
	public List<List<Dialogue>> DialogueParser_withLoveLevel(string csvFileName)
	{
		bool flag = false;
		List<List<Dialogue>> list = new List<List<Dialogue>>();
		List<Dialogue> list2 = new List<Dialogue>();
		List<Dialogue> list3 = new List<Dialogue>();
		List<Dialogue> list4 = new List<Dialogue>();
		List<Dialogue> list5 = new List<Dialogue>();
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int num4 = 3;
		int num5 = 4;
		int num6 = 5;
		int num7 = 6;
		int num8 = 7;
		int num9 = 9;
		int num10 = 10;
		string[] array = Resources.Load<TextAsset>("CsvFiles/" + csvFileName).text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			Dialogue dialogue = new Dialogue();
			int num11 = 0;
			int num12 = 0;
			if (int.TryParse(array2[num].ToString(), out num11))
			{
				num12 = num11;
			}
			int num13 = 0;
			if (int.TryParse(array2[num2].ToString(), out num13))
			{
				dialogue.eventDetailNum = num13;
			}
			List<DetailLine> list6 = new List<DetailLine>();
			do
			{
				DetailLine detailLine = new DetailLine();
				List<Line> list7 = new List<Line>();
				for (;;)
				{
					Line line = new Line();
					List<int> list8 = new List<int>();
					List<ChatController.BubbleType> list9 = new List<ChatController.BubbleType>();
					List<int> list10 = new List<int>();
					List<string> list11 = new List<string>();
					Choice choice = new Choice();
					List<string> list12 = new List<string>();
					int num14 = 0;
					string text = array2[num4].ToString().Trim();
					int num15 = 0;
					if (int.TryParse(text, out num15))
					{
						line.nameID = num15;
					}
					else
					{
						List<WinionPersonality> winionPersonalitiesList = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
						for (int j = 0; j < winionPersonalitiesList.Count; j++)
						{
							if (text == winionPersonalitiesList[j].presonalityName)
							{
								line.nameID = winionPersonalitiesList[j].id;
								break;
							}
						}
					}
					do
					{
						string text2 = array2[num5].ToString();
						ChatController.BubbleType bubbleType = this.FindBubbleType(text2);
						if (bubbleType != ChatController.BubbleType.NormalBubble)
						{
							line.haveSpecialBubble = true;
							list8.Add(num14);
							list9.Add(bubbleType);
							string text3 = this.FindBubbleTypeString(bubbleType);
							text2 = text2.Replace(text3, "");
						}
						string text4 = this.FindEmotionBubble(text2);
						if (text4 != "")
						{
							line.haveEmotionSpeechBubble = true;
							list10.Add(num14);
							list11.Add(text4);
							text2 = text2.Replace(text4, "");
						}
						list12.Add(text2);
						num14++;
						if (array2[num10].ToString() != "")
						{
							line.getItem = true;
							int num16 = 0;
							if (int.TryParse(array2[num10].ToString(), out num16))
							{
								line.ItemId = num16;
							}
						}
						if (array2[num6].ToString() != "")
						{
							int num17 = 0;
							if (int.TryParse(array2[num6].ToString(), out num17) && num17 == 1)
							{
								line.haveChoice = true;
								int num18 = 0;
								int num19 = i;
								choice.onlyOneChoice = false;
								for (int k = 0; k < 2; k++)
								{
									num19++;
									if (string.IsNullOrWhiteSpace(array[num19].Split(new char[] { ',' })[num7].ToString()))
									{
										choice.onlyOneChoice = true;
										break;
									}
								}
								int num20 = 2;
								if (choice.onlyOneChoice)
								{
									num20 = 1;
								}
								for (int l = 0; l < num20; l++)
								{
									if (++i < array.Length)
									{
										array2 = array[i].Split(new char[] { ',' });
									}
									if (l == 0)
									{
										choice.firstOption = array2[num7].ToString();
										List<string> list13 = new List<string>();
										if (array2[num8].ToString().Contains("_"))
										{
											list13 = array2[num8].ToString().Split('_', StringSplitOptions.None).ToList<string>();
										}
										else
										{
											list13.Add(array2[num8].ToString());
										}
										if (int.TryParse(list13[0], out num18))
										{
											choice.firstOptDialogNum = num18;
											if (list13.Count > 1)
											{
												choice.haveCommon = true;
												if (int.TryParse(list13[1], out num18))
												{
													choice.commonDialogNum = num18;
												}
											}
										}
										int num21 = 0;
										if (array2[num9].ToString() != "" && int.TryParse(array2[num9].ToString(), out num21))
										{
											choice.firstOption_changeCurWinionLove = true;
											choice.firstOption_AddloveNum = num21;
										}
									}
									else if (l == 1)
									{
										choice.secondOption = array2[num7].ToString();
										List<string> list14 = new List<string>();
										if (array2[num8].ToString().Contains("_"))
										{
											list14 = array2[num8].ToString().Split('_', StringSplitOptions.None).ToList<string>();
										}
										else
										{
											list14.Add(array2[num8].ToString());
										}
										if (int.TryParse(list14[0], out num18))
										{
											choice.secondOptDialogNum = num18;
											if (list14.Count > 1)
											{
												choice.haveCommon = true;
												if (int.TryParse(list14[1], out num18))
												{
													choice.commonDialogNum = num18;
												}
											}
										}
										int num22 = 0;
										if (array2[num9].ToString() != "" && int.TryParse(array2[num9].ToString(), out num22))
										{
											choice.secondOption_changeCurWinionLove = true;
											choice.secondOption_AddloveNum = num22;
										}
									}
								}
							}
						}
						if (++i >= array.Length)
						{
							goto IL_0551;
						}
						array2 = array[i].Split(new char[] { ',' });
					}
					while (array2[num4].ToString() == "");
					IL_056E:
					if (line.haveChoice)
					{
						line.choice = choice;
					}
					line.context = list12.ToArray();
					if (line.haveSpecialBubble)
					{
						line.bubbleTypeIndex = list8;
						line.bubbleTypeList = list9;
					}
					if (line.haveEmotionSpeechBubble)
					{
						line.emotionSpeechBubble_contextIndex = list10;
						line.emotionSpeechBubble_ID = list11;
					}
					list7.Add(line);
					if (flag || !(array2[num3].ToString() == ""))
					{
						break;
					}
					continue;
					IL_0551:
					flag = true;
					goto IL_056E;
				}
				detailLine.lines = list7;
				list6.Add(detailLine);
			}
			while (!flag && array2[num2].ToString() == "");
			dialogue.d_lines = list6;
			if (num12 == 1)
			{
				list2.Add(dialogue);
			}
			else if (num12 == 2)
			{
				list3.Add(dialogue);
			}
			else if (num12 == 3)
			{
				list4.Add(dialogue);
			}
			else if (num12 == 4)
			{
				list4.Add(dialogue);
			}
			else
			{
				list5.Add(dialogue);
			}
		}
		if (list2.Count != 0)
		{
			list.Add(list2);
		}
		if (list3.Count != 0)
		{
			list.Add(list3);
		}
		if (list4.Count != 0)
		{
			list.Add(list4);
		}
		if (list5.Count != 0)
		{
			list.Add(list5);
		}
		return list;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0004CA48 File Offset: 0x0004AC48
	public string FindEmotionBubble(string sentence)
	{
		string text = "";
		if (sentence.Contains("{행복}"))
		{
			text = "{행복}";
		}
		if (sentence.Contains("{애정 업}"))
		{
			text = "{애정 업}";
		}
		if (sentence.Contains("{애정 다운}"))
		{
			text = "{애정 다운}";
		}
		if (sentence.Contains("{기분 나빠}"))
		{
			text = "{기분 나빠}";
		}
		if (sentence.Contains("{반짝반짝}"))
		{
			text = "{반짝반짝}";
		}
		if (sentence.Contains("{부끄러움}"))
		{
			text = "{부끄러움}";
		}
		if (sentence.Contains("{잠}"))
		{
			text = "{잠}";
		}
		if (sentence.Contains("{우울}"))
		{
			text = "{우울}";
		}
		if (sentence.Contains("{좌절}"))
		{
			text = "{좌절}";
		}
		if (sentence.Contains("{잠}"))
		{
			text = "{잠}";
		}
		if (sentence.Contains("{나이뻐}"))
		{
			text = "{나이뻐}";
		}
		if (sentence.Contains("{우히히}"))
		{
			text = "{우히히}";
		}
		if (sentence.Contains("{느낌표}"))
		{
			text = "{느낌표}";
		}
		if (sentence.Contains("{눈물}"))
		{
			text = "{눈물}";
		}
		if (sentence.Contains("{똥}"))
		{
			text = "{똥}";
		}
		if (sentence.Contains("{편지}"))
		{
			text = "{편지}";
		}
		if (sentence.Contains("{나 할말있어}"))
		{
			text = "{나 할말있어}";
		}
		if (sentence.Contains("{눈껌뻑}"))
		{
			text = "{눈껌뻑}";
		}
		return text;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0004CBB4 File Offset: 0x0004ADB4
	public Dialogue_3D DialogueParser_3D(string csvFileName)
	{
		bool flag = false;
		Dialogue_3D dialogue_3D = new Dialogue_3D();
		dialogue_3D.detailLine_3D = new List<DetailLine_3D>();
		int num = 0;
		int num2 = 2;
		int num3 = 3;
		int num4 = 4;
		int @int = PlayerPrefs.GetInt("Language", 0);
		TextAsset textAsset;
		if (@int == 0)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/EN/" + csvFileName);
		}
		else if (@int == 1)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		else
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		string[] array = textAsset.text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			DetailLine_3D detailLine_3D = new DetailLine_3D();
			detailLine_3D.lines_3D = new List<Line_3D>();
			int num5 = 0;
			if (int.TryParse(array2[num].ToString(), out num5))
			{
				detailLine_3D.dialogueNum = num5;
			}
			for (;;)
			{
				Line_3D line_3D = new Line_3D();
				List<List<string>> list = new List<List<string>>();
				List<int> list2 = new List<int>();
				List<string> list3 = new List<string>();
				int num6 = 0;
				string text = array2[num2].ToString().Trim();
				int num7 = 0;
				if (int.TryParse(text, out num7))
				{
					line_3D.winionNum = num7;
				}
				else
				{
					List<WinionPersonality> winionPersonalitiesList = DBManager.instance.winionCommonInfo.winionPersonalitiesList;
					for (int j = 0; j < winionPersonalitiesList.Count; j++)
					{
						if (text == winionPersonalitiesList[j].presonalityName)
						{
							line_3D.winionNum = winionPersonalitiesList[j].id;
							break;
						}
					}
				}
				do
				{
					string text2 = array2[num3].ToString();
					if (array2[num4].ToString().Trim() != "")
					{
						line_3D.changeFace = true;
						List<string> list4 = DBManager.instance.winionFaceInfo.DivideSentence(array2[num4].ToString().Trim());
						list.Add(list4);
						list2.Add(num6);
					}
					list3.Add(text2);
					num6++;
					if (++i >= array.Length)
					{
						goto IL_0212;
					}
					array2 = array[i].Split(new char[] { ',' });
				}
				while (array2[num2].ToString() == "");
				IL_022E:
				line_3D.dialogue_List = list3;
				line_3D.changeExpression = list;
				line_3D.changeExpression_Index = list2;
				detailLine_3D.lines_3D.Add(line_3D);
				if (flag || !(array2[num].ToString() == ""))
				{
					break;
				}
				continue;
				IL_0212:
				flag = true;
				goto IL_022E;
			}
			dialogue_3D.detailLine_3D.Add(detailLine_3D);
			if (flag)
			{
				break;
			}
		}
		return dialogue_3D;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0004CE50 File Offset: 0x0004B050
	public ChatController.BubbleType FindBubbleType(string sentence)
	{
		ChatController.BubbleType bubbleType = ChatController.BubbleType.NormalBubble;
		if (sentence.Contains("(조용)"))
		{
			bubbleType = ChatController.BubbleType.SilentBubble;
		}
		if (sentence.Contains("(소리침)"))
		{
			bubbleType = ChatController.BubbleType.ShoutBubble;
		}
		if (sentence.Contains("(화남)"))
		{
			bubbleType = ChatController.BubbleType.AngryBubble;
		}
		if (sentence.Contains("(상상)"))
		{
			bubbleType = ChatController.BubbleType.imaginaryBubble;
		}
		return bubbleType;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0004CE9C File Offset: 0x0004B09C
	public string FindBubbleTypeString(ChatController.BubbleType bubbleType)
	{
		string text = "";
		switch (bubbleType)
		{
		case ChatController.BubbleType.SilentBubble:
			text = "(조용)";
			break;
		case ChatController.BubbleType.ShoutBubble:
			text = "(소리침)";
			break;
		case ChatController.BubbleType.AngryBubble:
			text = "(화남)";
			break;
		case ChatController.BubbleType.imaginaryBubble:
			text = "(상상)";
			break;
		}
		return text;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0004CEE8 File Offset: 0x0004B0E8
	public Contents DialogueParser_Content(string csvFileName)
	{
		bool flag = false;
		Contents contents = new Contents();
		contents.Content_List = new List<Content>();
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int @int = PlayerPrefs.GetInt("Language", 0);
		TextAsset textAsset;
		if (@int == 0)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/EN/" + csvFileName);
		}
		else if (@int == 1)
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		else
		{
			textAsset = Resources.Load<TextAsset>("CsvFiles/KR/" + csvFileName);
		}
		string[] array = textAsset.text.Split(new char[] { '\n' });
		int i = 1;
		while (i < array.Length)
		{
			string[] array2 = array[i].Split(new char[] { ',' });
			Content content = new Content();
			content.Line_List = new List<string>();
			content.ID = array2[num].ToString();
			List<string> list = new List<string>();
			do
			{
				string text = array2[num3].ToString();
				list.Add(text);
				if (++i < array.Length)
				{
					array2 = array[i].Split(new char[] { ',' });
					if (array2[num2].ToString() == "")
					{
						continue;
					}
				}
				else
				{
					flag = true;
				}
			}
			while (!flag && array2[num].ToString() == "");
			content.Line_List = list;
			contents.Content_List.Add(content);
			if (flag)
			{
				break;
			}
		}
		return contents;
	}
}
