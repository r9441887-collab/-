using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public class ChoiceBox_UI : MonoBehaviour
{
	// Token: 0x060009BC RID: 2492 RVA: 0x000142D0 File Offset: 0x000124D0
	private void Start()
	{
		this._localScale = base.transform.localScale;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0004AD98 File Offset: 0x00048F98
	private void OnEnable()
	{
		if (base.transform.parent != null)
		{
			Mask component = base.transform.parent.GetComponent<Mask>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0004ADDC File Offset: 0x00048FDC
	public void ButtonSetting(string questionContent, string sentence01, string sentence02, bool onlyOneChoice = false)
	{
		sentence01 = DBManager.instance.chatController.ReplaceSentence(sentence01);
		sentence02 = DBManager.instance.chatController.ReplaceSentence(sentence02);
		base.GetComponent<RectTransform>().pivot = new Vector2(0.501f, 0.5f);
		DOVirtual.Int(0, 1, 0.05f, delegate(int value)
		{
		}).OnComplete(delegate
		{
			base.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			base.GetComponent<RectTransform>().localPosition = Vector3.zero;
			base.transform.parent.GetComponent<Mask>().enabled = false;
		});
		if (this.choice02_Btn != null)
		{
			if (!onlyOneChoice)
			{
				this.choice02_Btn.gameObject.SetActive(true);
			}
			else
			{
				this.choice02_Btn.gameObject.SetActive(false);
			}
		}
		DBManager.instance.dialogueData.selecting_PlayerOptions = true;
		DBManager.instance.dialogueData.selectOption01 = false;
		if (!onlyOneChoice)
		{
			DBManager.instance.dialogueData.selectOption02 = false;
		}
		this.questionContent_text.text = questionContent;
		this.choice01_text.text = sentence01;
		if (!onlyOneChoice)
		{
			this.choice02_text.text = sentence02;
		}
		if (!this.ListnerAdded_1)
		{
			this.choice01_Btn.onClick.AddListener(delegate
			{
				if (this.canClick)
				{
					this.canClick = false;
					DBManager.instance.dialogueData.selectOption01 = true;
					DBManager.instance.dialogueData.selectOption02 = false;
					if (DBManager.instance.dialogueData.selecting_PlayerOptions)
					{
						DBManager.instance.dialogueData.selecting_PlayerOptions = false;
					}
				}
			});
			EventTrigger eventTrigger = this.choice01_Btn.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = 2;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.ClickUI_Light, false, 0.5f, 1f);
			});
			eventTrigger.triggers.Add(entry);
			this.ListnerAdded_1 = true;
		}
		if (!this.ListnerAdded_2 && !onlyOneChoice)
		{
			this.choice02_Btn.onClick.AddListener(delegate
			{
				if (this.canClick)
				{
					this.canClick = false;
					DBManager.instance.dialogueData.selectOption01 = false;
					DBManager.instance.dialogueData.selectOption02 = true;
					if (DBManager.instance.dialogueData.selecting_PlayerOptions)
					{
						DBManager.instance.dialogueData.selecting_PlayerOptions = false;
					}
				}
			});
			EventTrigger eventTrigger2 = this.choice02_Btn.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = 2;
			entry2.callback.AddListener(delegate(BaseEventData eventData)
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.ClickUI_Light, false, 0.5f, 1f);
			});
			eventTrigger2.triggers.Add(entry2);
			this.ListnerAdded_2 = true;
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x000142E3 File Offset: 0x000124E3
	public void CloseSetting()
	{
		if (this.choice02_Btn != null)
		{
			this.choice02_Btn.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000AAC RID: 2732
	public bool canClick;

	// Token: 0x04000AAD RID: 2733
	public TMP_Text questionContent_text;

	// Token: 0x04000AAE RID: 2734
	public Button choice01_Btn;

	// Token: 0x04000AAF RID: 2735
	public TMP_Text choice01_text;

	// Token: 0x04000AB0 RID: 2736
	public Button choice02_Btn;

	// Token: 0x04000AB1 RID: 2737
	public TMP_Text choice02_text;

	// Token: 0x04000AB2 RID: 2738
	public Vector3 _localScale;

	// Token: 0x04000AB3 RID: 2739
	public bool ListnerAdded_1;

	// Token: 0x04000AB4 RID: 2740
	public bool ListnerAdded_2;
}
