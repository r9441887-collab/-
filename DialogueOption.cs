using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000385 RID: 901
public class DialogueOption : SingletoneBehaviour<DialogueOption>
{
	// Token: 0x06001AC9 RID: 6857 RVA: 0x00011486 File Offset: 0x0000F686
	private void OnEnable()
	{
		base.StartCoroutine("WaitDBManager");
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x000193EE File Offset: 0x000175EE
	private void OnDisable()
	{
		DBManager.instance.dialogueController.StopDialogue();
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x000193FF File Offset: 0x000175FF
	public void CloseWindow()
	{
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
		this.TargetWindow.DestroyBox(false, true);
		DBManager.instance.dialogueController.StopDialogue();
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x00019435 File Offset: 0x00017635
	private IEnumerator WaitDBManager()
	{
		yield return new WaitUntil(() => DBManager.instance != null);
		if (!this.isTitle)
		{
			this.SettingION();
		}
		else
		{
			this.SettingION_Title();
		}
		yield break;
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x000CA34C File Offset: 0x000C854C
	public void SetActive_GameObject()
	{
		if (!this.isTitle)
		{
			if (DBManager.instance.dialogueData.smallTalk_ing)
			{
				SystemBox.Instance.Show(new MessageConfig("", DBManager.instance.GetSettingString("메세지박스", 0, 9, 0)), SystemBox.MessageType.Default, true, 4f, false, true);
				return;
			}
			if (GameManager.instance.gameData.Bo.blockDialogue)
			{
				SystemBox.Instance.Show(new MessageConfig("", DBManager.instance.GetSettingString("메세지박스", 0, 8, 0)), SystemBox.MessageType.Default, true, 4f, false, true);
				return;
			}
		}
		this.Setting();
		if (!this.isTitle)
		{
			this.SettingION();
		}
		else
		{
			this.SettingION_Title();
		}
		this.DialogueWindow.gameObject.SetActive(true);
		SingletoneBehaviour<MouseRaycast>.Instance.SetTopMostLayer(this.DialogueWindow.gameObject);
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x000CA430 File Offset: 0x000C8630
	public void Setting()
	{
		this.showAllConversation.isOn = DBManager.instance.showAllConversation;
		this.ShowAllConversation_Setting();
		if (this.showAllConversation.isOn)
		{
			this.showTextByWord.isOn = false;
			this.showTextByChar.isOn = false;
			return;
		}
		this.showTextByWord.isOn = DBManager.instance.showTextByWord;
		this.showTextByChar.isOn = !DBManager.instance.showTextByWord;
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x00019444 File Offset: 0x00017644
	public void ReSetting()
	{
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
		this.showAllConversation.isOn = false;
		this.ShowAllConversation_Setting();
		this.TextSpeedSetting();
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x000CA4AC File Offset: 0x000C86AC
	public void ShowAllConversation_Setting()
	{
		if (this.showAllConversation.isOn)
		{
			this.showTextByChar.isOn = false;
			this.showTextByWord.isOn = false;
			this.option02_Block.SetActive(true);
			this.ShowWordSetting();
			return;
		}
		this.showTextByWord.isOn = true;
		this.showTextByChar.isOn = false;
		this.option02_Block.SetActive(false);
		this.ShowWordSetting();
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x00019476 File Offset: 0x00017676
	public void ShowWordSetting()
	{
		if (!this.showAllConversation.isOn)
		{
			if (this.showTextByWord.isOn)
			{
				this.showTextByChar.isOn = false;
			}
			else
			{
				this.showTextByChar.isOn = true;
			}
			this.TextSpeedSetting();
		}
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x000194B2 File Offset: 0x000176B2
	public void ShowCharSetting()
	{
		if (!this.showAllConversation.isOn)
		{
			if (this.showTextByChar.isOn)
			{
				this.showTextByWord.isOn = false;
			}
			else
			{
				this.showTextByWord.isOn = true;
			}
			this.TextSpeedSetting();
		}
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x000CA51C File Offset: 0x000C871C
	public void TextSpeedSetting()
	{
		if (this.showTextByWord.isOn)
		{
			this.defaultSpeed = 0.05f;
			this.curSpeed = this.defaultSpeed;
		}
		if (this.showTextByChar.isOn)
		{
			this.defaultSpeed = 0.03f;
			this.curSpeed = this.defaultSpeed;
		}
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x000CA574 File Offset: 0x000C8774
	public void Apply()
	{
		this.CloseWindow();
		PlayerPrefs.SetInt("ShowAllConversation", this.showAllConversation.isOn ? 1 : 0);
		PlayerPrefs.SetInt("ShowTextByWord", this.showTextByWord.isOn ? 1 : 0);
		DBManager.instance.showAllConversation = this.showAllConversation.isOn;
		DBManager.instance.showTextByWord = this.showTextByWord.isOn;
		this.DialogueWindow.DestroyBox(false, false);
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x000CA5F4 File Offset: 0x000C87F4
	public void TestDialouge()
	{
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
		int @int = PlayerPrefs.GetInt("Language", 0);
		if (this.isRidio)
		{
			if (@int == 0)
			{
				DBManager.instance.dialogueController.TextDialouge(this.dialogueIndex, this.speechBubble_EN_Ridio, this.SpeechBubbleText_EN_Ridio);
				return;
			}
			if (@int == 1)
			{
				DBManager.instance.dialogueController.TextDialouge(this.dialogueIndex, this.speechBubble_KR_Ridio, this.SpeechBubbleText_KR_Ridio);
				return;
			}
		}
		else
		{
			if (@int == 0)
			{
				DBManager.instance.dialogueController.TextDialouge(this.dialogueIndex, this.speechBubble_EN, this.SpeechBubbleText_EN);
				return;
			}
			DBManager.instance.dialogueController.TextDialouge(this.dialogueIndex, this.speechBubble_KR, this.SpeechBubbleText_KR);
		}
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x000CA6C0 File Offset: 0x000C88C0
	public void SettingION_Title()
	{
		int @int = PlayerPrefs.GetInt("LastChapter", 0);
		this.radio.SetActive(true);
		switch (@int)
		{
		case 0:
			this.Chapter00Setting();
			return;
		case 1:
			this.Chapter01Setting();
			return;
		case 2:
			this.Chapter02Setting();
			return;
		case 3:
			this.Chapter03Setting();
			return;
		default:
			return;
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x000CA718 File Offset: 0x000C8918
	private void Chapter00Setting()
	{
		int num;
		if (this.isTitle)
		{
			num = PlayerPrefs.GetInt("LastEvent", 0);
			num--;
		}
		else
		{
			num = DBManager.instance.dialogueData.curEventNum;
		}
		this.isRidio = false;
		this.ION_IMG.gameObject.SetActive(true);
		switch (num)
		{
		case -1:
		case 0:
			this.roomImg.sprite = this.RoomSprite[0];
			this.ION_IMG.gameObject.SetActive(false);
			this.isRidio = true;
			this.testBtn.interactable = true;
			this.roomBlack.SetActive(false);
			this.dialogueIndex = 16;
			return;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
			this.roomImg.sprite = this.RoomSprite[0];
			this.ION_IMG.gameObject.SetActive(true);
			this.testBtn.interactable = true;
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 0;
			return;
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 1;
			return;
		case 12:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 2;
			return;
		case 13:
			if (this.isTitle)
			{
				this.roomImg.sprite = this.RoomSprite[1];
				this.PlayAnimation("FrontIdle");
				this.dialogueIndex = 1;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[1];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 3;
			return;
		case 14:
		case 15:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 1;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x000CA928 File Offset: 0x000C8B28
	private void Chapter01Setting()
	{
		int num;
		if (this.isTitle)
		{
			num = PlayerPrefs.GetInt("LastEvent", 0);
			num--;
		}
		else
		{
			num = DBManager.instance.dialogueData.curEventNum;
		}
		this.isRidio = false;
		this.ION_IMG.gameObject.SetActive(true);
		switch (num)
		{
		case -1:
		case 0:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 1;
			return;
		case 1:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 4;
			return;
		case 2:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 1;
			return;
		case 3:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 5;
			return;
		case 4:
		case 5:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 1;
			return;
		case 6:
		case 7:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 6;
			return;
		case 8:
		case 9:
		case 10:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 7;
			return;
		case 11:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 8;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x000CAB08 File Offset: 0x000C8D08
	private void Chapter02Setting()
	{
		int num;
		if (this.isTitle)
		{
			num = PlayerPrefs.GetInt("LastEvent", 0);
			num--;
		}
		else
		{
			num = DBManager.instance.dialogueData.curEventNum;
		}
		this.isRidio = false;
		this.radio.SetActive(true);
		this.ION_IMG.gameObject.SetActive(true);
		switch (num)
		{
		case -1:
		case 0:
			this.roomImg.sprite = this.RoomSprite[0];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 8;
			return;
		case 1:
			if (this.isTitle)
			{
				this.roomImg.sprite = this.RoomSprite[0];
				this.SettingFlip(true);
				this.PlayAnimation("BeHit02_Front");
				this.dialogueIndex = 9;
				return;
			}
			if (DBManager.instance.dialogueData.curEventDetailNum == 1)
			{
				this.roomImg.sprite = this.RoomSprite[0];
				this.PlayAnimation("FrontIdle");
				this.dialogueIndex = 8;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[0];
			this.SettingFlip(true);
			this.PlayAnimation("BeHit02_Front");
			this.dialogueIndex = 9;
			return;
		case 2:
			this.roomImg.sprite = this.RoomSprite[1];
			this.SettingFlip(false);
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 8;
			return;
		case 3:
			if (this.isTitle)
			{
				this.roomImg.sprite = this.RoomSprite[3];
				this.roomBlack.SetActive(true);
				this.radio.SetActive(false);
				this.PlayAnimation("Fear_Blood");
				this.dialogueIndex = 10;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[2];
			this.PlayAnimation("FrontIdle");
			this.dialogueIndex = 8;
			return;
		case 4:
			this.roomImg.sprite = this.RoomSprite[3];
			this.roomBlack.SetActive(true);
			this.radio.SetActive(false);
			this.PlayAnimation("Fear_Blood");
			this.dialogueIndex = 10;
			return;
		case 5:
			if (DBManager.instance.dialogueData.curEventDetailNum < 11)
			{
				this.roomImg.sprite = this.RoomSprite[3];
				this.roomBlack.SetActive(true);
				this.radio.SetActive(false);
				this.PlayAnimation("Fear_Blood");
				this.dialogueIndex = 10;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[2];
			this.roomBlack.SetActive(false);
			this.radio.SetActive(true);
			this.PlayAnimation("FrontIdle_Emptiness");
			this.dialogueIndex = 11;
			return;
		case 6:
			if (this.isTitle)
			{
				this.roomImg.sprite = this.RoomSprite[2];
				this.roomBlack.SetActive(false);
				this.radio.SetActive(true);
				this.PlayAnimation("FrontIdle_Emptiness");
				this.dialogueIndex = 13;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[2];
			this.roomBlack.SetActive(true);
			this.PlayAnimation("Sleeping");
			this.dialogueIndex = 12;
			return;
		case 7:
			this.roomImg.sprite = this.RoomSprite[2];
			this.roomBlack.SetActive(false);
			this.PlayAnimation("FrontIdle_Emptiness");
			this.dialogueIndex = 13;
			return;
		case 8:
			this.roomImg.sprite = this.RoomSprite[4];
			this.PlayAnimation("FrontIdle_Emptiness");
			this.dialogueIndex = 13;
			return;
		case 9:
			if (this.isTitle)
			{
				this.roomImg.sprite = this.RoomSprite[4];
				this.ION_IMG.gameObject.SetActive(false);
				this.isRidio = true;
				this.roomBlack.SetActive(true);
				this.dialogueIndex = 18;
				return;
			}
			if (DBManager.instance.dialogueData.curEventDetailNum == 0 || DBManager.instance.dialogueData.curEventDetailNum == 4)
			{
				this.roomImg.sprite = this.RoomSprite[5];
				this.radio.SetActive(false);
				this.PlayAnimation("BackIdle_TrashCan");
				this.dialogueIndex = 14;
				return;
			}
			this.roomImg.sprite = this.RoomSprite[5];
			this.radio.SetActive(false);
			this.PlayAnimation("FrontIdle_TrashCan");
			this.dialogueIndex = 7;
			return;
		case 10:
		case 11:
		case 12:
			this.roomImg.sprite = this.RoomSprite[4];
			this.roomBlack.SetActive(true);
			this.radio.SetActive(true);
			this.PlayAnimation("Tatter");
			this.dialogueIndex = 15;
			return;
		default:
			this.roomImg.sprite = this.RoomSprite[4];
			this.roomBlack.SetActive(true);
			this.radio.SetActive(true);
			this.PlayAnimation("Tatter");
			this.isRidio = true;
			this.dialogueIndex = 16;
			return;
		}
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x000CB04C File Offset: 0x000C924C
	private void Chapter03Setting()
	{
		int num;
		if (this.isTitle)
		{
			num = PlayerPrefs.GetInt("LastEvent", 0);
			if (num < 12)
			{
				num--;
			}
		}
		else
		{
			num = DBManager.instance.dialogueData.curEventNum;
		}
		this.isRidio = false;
		this.ION_IMG.gameObject.SetActive(true);
		if (num >= 12)
		{
			this.roomImg.sprite = this.RoomSprite[4];
			this.roomBlack.SetActive(true);
			this.ION_IMG.gameObject.SetActive(false);
			this.flower.gameObject.SetActive(true);
			this.isRidio = true;
			this.dialogueIndex = 19;
			return;
		}
		if (num == -1)
		{
			this.roomImg.sprite = this.RoomSprite[4];
			this.roomBlack.SetActive(true);
			this.PlayAnimation("Tatter");
			this.dialogueIndex = 16;
			this.isRidio = true;
			return;
		}
		if (this.isTitle && num == 11)
		{
			this.roomImg.sprite = this.RoomSprite[4];
			this.roomBlack.SetActive(true);
			this.ION_IMG.gameObject.SetActive(false);
			this.flower.gameObject.SetActive(true);
			this.isRidio = true;
			this.dialogueIndex = 19;
		}
		this.roomImg.sprite = this.RoomSprite[4];
		this.roomBlack.SetActive(true);
		this.PlayAnimation("Tatter");
		this.dialogueIndex = 17;
		this.isRidio = true;
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x000CB1DC File Offset: 0x000C93DC
	public void SettingION()
	{
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00)
		{
			this.Chapter00Setting();
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
		{
			this.Chapter01Setting();
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02)
		{
			this.Chapter02Setting();
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03)
		{
			this.Chapter03Setting();
		}
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x000194EE File Offset: 0x000176EE
	public void PlayAnimation(string animationName)
	{
		this.PreviewION.PlayAnimation(animationName, false);
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x000CB24C File Offset: 0x000C944C
	public void SettingFlip(bool right = false)
	{
		float x = this.PreviewION.gameObject.transform.localScale.x;
		if (right)
		{
			if (x > 0f)
			{
				Vector3 localScale = this.PreviewION.gameObject.transform.localScale;
				localScale.x *= -1f;
				this.PreviewION.gameObject.transform.localScale = localScale;
				return;
			}
		}
		else if (x < 0f)
		{
			Vector3 localScale2 = this.PreviewION.gameObject.transform.localScale;
			localScale2.x *= -1f;
			this.PreviewION.gameObject.transform.localScale = localScale2;
		}
	}

	// Token: 0x040017D5 RID: 6101
	public Toggle showAllConversation;

	// Token: 0x040017D6 RID: 6102
	public Toggle showTextByChar;

	// Token: 0x040017D7 RID: 6103
	public Toggle showTextByWord;

	// Token: 0x040017D8 RID: 6104
	public GameObject option02_Block;

	// Token: 0x040017D9 RID: 6105
	public TMP_Text SpeedText;

	// Token: 0x040017DA RID: 6106
	private float defaultSpeed = 0.05f;

	// Token: 0x040017DB RID: 6107
	public float curSpeed = 0.05f;

	// Token: 0x040017DC RID: 6108
	public Image roomImg;

	// Token: 0x040017DD RID: 6109
	public Image ION_IMG;

	// Token: 0x040017DE RID: 6110
	public List<Sprite> RoomSprite;

	// Token: 0x040017DF RID: 6111
	public GameObject speechBubble_KR;

	// Token: 0x040017E0 RID: 6112
	public TMP_Text SpeechBubbleText_KR;

	// Token: 0x040017E1 RID: 6113
	public GameObject speechBubble_KR_Ridio;

	// Token: 0x040017E2 RID: 6114
	public TMP_Text SpeechBubbleText_KR_Ridio;

	// Token: 0x040017E3 RID: 6115
	public GameObject speechBubble_EN;

	// Token: 0x040017E4 RID: 6116
	public TMP_Text SpeechBubbleText_EN;

	// Token: 0x040017E5 RID: 6117
	public GameObject speechBubble_EN_Ridio;

	// Token: 0x040017E6 RID: 6118
	public TMP_Text SpeechBubbleText_EN_Ridio;

	// Token: 0x040017E7 RID: 6119
	public bool isRidio;

	// Token: 0x040017E8 RID: 6120
	public GameObject speechBubble;

	// Token: 0x040017E9 RID: 6121
	public TMP_Text SpeechBubbleText;

	// Token: 0x040017EA RID: 6122
	public Button testBtn;

	// Token: 0x040017EB RID: 6123
	public CustomAnimator PreviewION;

	// Token: 0x040017EC RID: 6124
	public int dialogueIndex;

	// Token: 0x040017ED RID: 6125
	public GameObject roomBlack;

	// Token: 0x040017EE RID: 6126
	public UIWindow DialogueWindow;

	// Token: 0x040017EF RID: 6127
	public RectTransform radioBubblePos;

	// Token: 0x040017F0 RID: 6128
	public RectTransform IonBubblePos;

	// Token: 0x040017F1 RID: 6129
	public bool isTitle;

	// Token: 0x040017F2 RID: 6130
	public UIWindow TargetWindow;

	// Token: 0x040017F3 RID: 6131
	public GameObject radio;

	// Token: 0x040017F4 RID: 6132
	public GameObject flower;
}
