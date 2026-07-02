using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class MailActions : MonoBehaviour
{
	// Token: 0x06001DC3 RID: 7619 RVA: 0x000D8044 File Offset: 0x000D6244
	private void Start()
	{
		this.AddAction("Hello Winion.win", delegate
		{
		});
		this.AddAction("Tutorial.win", delegate
		{
		});
		this.AddAction("VaccineDownlader.win", delegate
		{
		});
		this.AddAction("VaccineDownlader_2.win", delegate
		{
		});
		this.AddAction("Anti-Virus for Winion.win", delegate
		{
			if (!SingletoneBehaviour<IconManager>.Instance.Objects[20].activeSelf)
			{
				if (this.GetConditionByKey["Anti-Virus Can Install"])
				{
					base.StartCoroutine("DelayActiveAntiVirus");
					return;
				}
			}
			else
			{
				SystemBox.Instance.Show(new MessageConfig(DBManager.instance.GetSettingString("메세지박스", 0, 7, 0)), SystemBox.MessageType.Default, true, 2f, false, true);
			}
		});
		this.GetConditionByKey["Anti-Virus Can Install"] = true;
		this.AddAction("Delicious_Lunch.win", delegate
		{
			this.objectsForEvents[2].SetActive(true);
			this.objectsForEvents[2].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[2].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("eb8298ec9d9820eabf88.win", delegate
		{
			this.objectsForEvents[3].SetActive(true);
			this.objectsForEvents[3].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[3].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("Dancing Fix.win", delegate
		{
			this.objectsForEvents[4].SetActive(true);
			this.objectsForEvents[4].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[4].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("My Selfie.win", delegate
		{
			this.objectsForEvents[5].SetActive(true);
			this.objectsForEvents[5].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[5].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("A happy nap.win", delegate
		{
			this.objectsForEvents[6].SetActive(true);
			this.objectsForEvents[6].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[6].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("I-ON and Grid.win", delegate
		{
			this.objectsForEvents[7].SetActive(true);
			this.objectsForEvents[7].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[7].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("Precious friends.win", delegate
		{
			this.objectsForEvents[8].SetActive(true);
			this.objectsForEvents[8].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[8].transform.SetParent(SystemBox.Instance.transform);
		});
		this.AddAction("My dear friends.win", delegate
		{
			this.objectsForEvents[9].SetActive(true);
			this.objectsForEvents[9].GetComponent<RectTransform>().position = Vector3.zero;
			this.objectsForEvents[9].transform.SetParent(SystemBox.Instance.transform);
		});
		for (int i = 0; i < this.Events.Count; i++)
		{
			this.GetActionIndexByKey[this.Events[i]._actionName] = i;
		}
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x0001B581 File Offset: 0x00019781
	private IEnumerator DelayActiveAntiVirus()
	{
		this.GetConditionByKey["Anti-Virus Can Install"] = false;
		yield return new WaitForSeconds(0.5f);
		int num = 0;
		int num2 = 1;
		this.objectsForEvents[num].SetActive(true);
		this.objectsForEvents[num].transform.SetParent(this.objectsForEvents[num2].transform);
		yield return new WaitForSeconds(0.5f);
		this.GetConditionByKey["Anti-Virus Can Install"] = true;
		yield break;
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x0001B590 File Offset: 0x00019790
	public void AddAction(string FileName, Action action)
	{
		this.Events.Add(new ActionData(FileName, action));
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x000D820C File Offset: 0x000D640C
	public void GetAction(TextMeshProUGUI actionNameText)
	{
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			SystemBox.Instance.Show(new MessageConfig(DBManager.instance.GetSettingString("메세지박스", 0, 0, 0), DBManager.instance.GetSettingString("메세지박스", 0, 9, 0), 650, 300), SystemBox.MessageType.Error, false, 4f, false, true);
			return;
		}
		string text = actionNameText.text;
		string text2 = actionNameText.text;
		string text3 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[25].Line_List[3]);
		string text4 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[28].Line_List[3]);
		string text5 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[4].Line_List[3]);
		string text6 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[5].Line_List[3]);
		string text7 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[6].Line_List[3]);
		string text8 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[8].Line_List[3]);
		string text9 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[14].Line_List[3]);
		string text10 = MailActions.RemoveWhitespace(DBManager.instance.mailContents[33].Line_List[3]);
		MailActions.RemoveWhitespace(actionNameText.text).Equals(MailActions.RemoveWhitespace(text6));
		text = MailActions.RemoveWhitespace(text);
		if (text.Equals(text3))
		{
			text2 = "Delicious_Lunch.win";
		}
		else if (text.Equals(text4))
		{
			text2 = "eb8298ec9d9820eabf88.win";
		}
		else if (text.Equals(text5))
		{
			text2 = "Dancing Fix.win";
		}
		else if (text.Equals(text6))
		{
			text2 = "My Selfie.win";
		}
		else if (text.Equals(text7))
		{
			text2 = "A happy nap.win";
		}
		else if (text.Equals(text8))
		{
			text2 = "I-ON and Grid.win";
		}
		else if (text.Equals(text9))
		{
			text2 = "Precious friends.win";
		}
		else if (text.Equals(text10))
		{
			text2 = "My dear friends.win";
		}
		int num;
		if (this.GetActionIndexByKey.TryGetValue(text2, out num))
		{
			Action action = this.Events[num]._action;
			if (action == null)
			{
				return;
			}
			action();
		}
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x0001B5A4 File Offset: 0x000197A4
	private static string RemoveWhitespace(string input)
	{
		return string.Concat<char>(input.Where((char c) => !char.IsWhiteSpace(c)));
	}

	// Token: 0x04001BE9 RID: 7145
	public List<ActionData> Events = new List<ActionData>();

	// Token: 0x04001BEA RID: 7146
	public List<GameObject> objectsForEvents = new List<GameObject>();

	// Token: 0x04001BEB RID: 7147
	private Dictionary<string, int> GetActionIndexByKey = new Dictionary<string, int>();

	// Token: 0x04001BEC RID: 7148
	private Dictionary<string, bool> GetConditionByKey = new Dictionary<string, bool>();
}
