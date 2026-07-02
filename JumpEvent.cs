using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A8 RID: 936
public class JumpEvent : MonoBehaviour
{
	// Token: 0x06001BD1 RID: 7121 RVA: 0x000CF6EC File Offset: 0x000CD8EC
	public static void SetCurrentChapter()
	{
		int currEventStatic = ChapterSetter.CurrEventStatic;
		int num = currEventStatic / 100;
		int num2 = currEventStatic % 100;
		JumpEvent.JumpButton[num][num2].GetComponent<Image>().color = Color.red;
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000CF728 File Offset: 0x000CD928
	public void Awake()
	{
		JumpEvent.JumpButton = new List<List<GameObject>>();
		for (int i = 0; i < 4; i++)
		{
			JumpEvent.JumpButton.Add(new List<GameObject>());
			for (int j = 0; j < this.eventCount[i]; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ButtonPrefab, this.buttonParent[i]);
				int chapter = i;
				int _event = j;
				JumpEvent.JumpButton[i].Add(gameObject);
				string text = string.Format("{0} - {1}", i, j);
				gameObject.name = text;
				gameObject.GetComponent<Button>().onClick.AddListener(delegate
				{
					this.JumpToEvent(chapter, _event);
				});
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
			}
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x0001A159 File Offset: 0x00018359
	private void OnEnable()
	{
		JumpEvent.SetCurrentChapter();
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000CF80C File Offset: 0x000CDA0C
	public void JumpToEvent(int c, int e)
	{
		if (!this.isTitle && (DBManager.instance.dialogueData.smallTalk_ing || GameManager.instance.gameData.Bo.blockDialogue))
		{
			SystemBox.Instance.Show(new MessageConfig("", DBManager.instance.GetSettingString("메세지박스", 0, 9, 0)), SystemBox.MessageType.Default, true, 4f, false, true);
			return;
		}
		DBManager.instance._backLog_Events.Clear();
		DBManager.instance._backLog_Events.Clear();
		Events.StartAutoEvent = true;
		Events.AutoChapterIndex = c;
		Events.AutoEventIndex = e;
		SceneLoader.LoadScene("Chapter 02_EunBin", false, false);
	}

	// Token: 0x0400192D RID: 6445
	public List<int> eventCount = new List<int>();

	// Token: 0x0400192E RID: 6446
	public List<Transform> buttonParent = new List<Transform>();

	// Token: 0x0400192F RID: 6447
	public GameObject ButtonPrefab;

	// Token: 0x04001930 RID: 6448
	public static List<List<GameObject>> JumpButton;

	// Token: 0x04001931 RID: 6449
	public bool isTitle;
}
