using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000BE RID: 190
public class WinionFileSelector : MonoBehaviour
{
	// Token: 0x060004A9 RID: 1193 RVA: 0x0003112C File Offset: 0x0002F32C
	private void Awake()
	{
		for (int i = 0; i < 8; i++)
		{
			this.text += "0123456789"[Random.Range(0, "0123456789".Length)].ToString();
			this.text2 += "0123456789"[Random.Range(0, "0123456789".Length)].ToString();
			this.debugPassword += "0123456789"[Random.Range(0, "0123456789".Length)].ToString();
		}
		this.WinionPassword = this.text;
		this.WinionIdNumber = this.text2;
		if (this.folder == Winion.Fix)
		{
			SingletoneBehaviour<VaccineManager>.Instance.FixPassword = this.text2;
		}
		if (this.folder == Winion.Debug)
		{
			SingletoneBehaviour<VaccineManager>.Instance.DebugPassword = this.debugPassword;
		}
		if (this.WinionIDText != null)
		{
			this.WinionIDText.text = this.text2;
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00031250 File Offset: 0x0002F450
	public void SetRemoveTarget(GameObject target)
	{
		if (target != null)
		{
			this.targetWinion = target;
			this.FileName.text = this.GetText(target.GetComponent<WinionFile>().winion);
			this.RemoveButton.interactable = true;
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = 2;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.Remove(false);
			});
			this.Trigger.triggers.Clear();
			this.Trigger.triggers.Add(entry);
			return;
		}
		this.targetWinion = null;
		this.FileName.text = "";
		this.RemoveButton.interactable = false;
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = 2;
		entry2.callback.AddListener(delegate(BaseEventData data)
		{
		});
		this.Trigger.triggers.Clear();
		this.Trigger.triggers.Add(entry2);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00031358 File Offset: 0x0002F558
	public void Remove(bool ManualExit = false)
	{
		if (this.targetWinion == null)
		{
			return;
		}
		Winion winion = this.targetWinion.GetComponent<WinionFile>().winion;
		if (!WinionFileSelector.ForceExit && ManualExit && !SingletoneBehaviour<WinionFolderManager>.Instance.folders[(int)this.folder].canExit[(int)winion])
		{
			return;
		}
		SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(winion, this.folder);
		SingletoneBehaviour<WinionFolderManager>.Instance.RemoveWinion_OutOfFolder(this.folder, winion);
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			SingletoneBehaviour<DesktopController>.Instance.winionRoomSettings.OutOf_RoomSetting(this.folder, winion);
		}
		Object.Destroy(this.targetWinion);
		this.targetWinion = null;
		this.FileName.text = "";
		this.RemoveButton.interactable = false;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0003142C File Offset: 0x0002F62C
	public string GetText(Winion winion)
	{
		switch (winion)
		{
		case Winion.Ion:
			return "I-ON";
		case Winion.Bo:
			return "BO";
		case Winion.Grid:
			return "GRID";
		case Winion.Fix:
			return "FIX";
		case Winion.Debug:
			return "DEBUG";
		default:
			return "";
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00011030 File Offset: 0x0000F230
	public void SwitchLightOff(bool _switch, WinionHandler winion)
	{
		this.light.SetActive(_switch);
		if (winion != null)
		{
			this.UIWinionColorLightOff(_switch, winion, "#6B6B6B", false);
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00031478 File Offset: 0x0002F678
	public void UIWinionColorLightOff(bool _switch, WinionHandler winion, string color = "#6B6B6B", bool fixColor = false)
	{
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		UIWinion component = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType].GetComponent<UIWinion>();
		if (_switch)
		{
			if (fixColor)
			{
				if (color == "")
				{
					color = "#6B6B6B";
				}
				this.colorFix_String = color;
				color = this.colorFix_String;
				component.ChangedColor = false;
			}
			if (!component.ChangedColor)
			{
				if (this.colorFix_String != "")
				{
					color = this.colorFix_String;
				}
				Color color2;
				if (ColorUtility.TryParseHtmlString(color, ref color2))
				{
					winion.uiwinionColorChange = true;
					component.ChangedColor = true;
					component.winionImg.color = color2;
					if (winion.haveEventEmotion)
					{
						winion.emotionHaveEvent_obj.GetComponent<Image>().color = color2;
					}
					if (winion.haveSmallTalkEmotion)
					{
						winion.emotionSmallTalk_obj.GetComponent<Image>().color = color2;
						return;
					}
				}
			}
		}
		else if (component.ChangedColor)
		{
			winion.uiwinionColorChange = false;
			component.winionImg.color = Color.white;
			component.ChangedColor = false;
			if (winion.haveEventEmotion)
			{
				winion.emotionHaveEvent_obj.GetComponent<Image>().color = Color.white;
			}
			if (winion.haveSmallTalkEmotion)
			{
				winion.emotionSmallTalk_obj.GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x000315C0 File Offset: 0x0002F7C0
	public void UIWinionColor_Gradient(WinionHandler winion, string color1, string color2, string bubbleColor = "#6B6B6B")
	{
		winion.uiwinionColorChange = true;
		winion.changeUIGradient = true;
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		UIWinion component = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType].GetComponent<UIWinion>();
		Color color3;
		if (ColorUtility.TryParseHtmlString(bubbleColor, ref color3))
		{
			if (winion.haveEventEmotion)
			{
				winion.emotionHaveEvent_obj.GetComponent<Image>().color = color3;
			}
			if (winion.haveSmallTalkEmotion)
			{
				winion.emotionSmallTalk_obj.GetComponent<Image>().color = color3;
			}
		}
		component.winionImg.color = Color.white;
		Color color4;
		if (ColorUtility.TryParseHtmlString(color1, ref color4))
		{
			component.winionGradient.color1 = color4;
		}
		if (ColorUtility.TryParseHtmlString(color2, ref color4))
		{
			component.winionGradient.color2 = color4;
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0003167C File Offset: 0x0002F87C
	public void resetGradient(WinionHandler winion)
	{
		winion.uiwinionColorChange = false;
		winion.changeUIGradient = false;
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		UIWinion component = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType].GetComponent<UIWinion>();
		if (winion.haveEventEmotion)
		{
			winion.emotionHaveEvent_obj.GetComponent<Image>().color = Color.white;
		}
		if (winion.haveSmallTalkEmotion)
		{
			winion.emotionSmallTalk_obj.GetComponent<Image>().color = Color.white;
		}
		component.winionGradient.color1 = Color.white;
		component.winionGradient.color2 = Color.white;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00011055 File Offset: 0x0000F255
	public void AboutWinionButtonClick()
	{
		this.AboutWinion.SetActive(true);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00031718 File Offset: 0x0002F918
	private void OnDisable()
	{
		if (this.AboutWinion != null)
		{
			GameObject aboutWinion = this.AboutWinion;
			if (aboutWinion != null)
			{
				aboutWinion.SetActive(false);
			}
		}
		if (this.WinionMetaData != null)
		{
			GameObject winionMetaData = this.WinionMetaData;
			if (winionMetaData == null)
			{
				return;
			}
			winionMetaData.SetActive(false);
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00031764 File Offset: 0x0002F964
	public void CheckPassword(TMP_InputField InputField)
	{
		if (string.Compare(this.WinionPassword, InputField.text) == 0 && WinionFileSelector.CanEnterPass)
		{
			this.WinionMetaData.SetActive(true);
			this.WinionMetaData.transform.SetParent(SystemBox.Instance.transform);
			return;
		}
		SystemBox.Instance.Show(new MessageConfig(DBManager.instance.GetSettingString("메세지박스", 0, 0, 0), DBManager.instance.GetSettingString("메세지박스", 0, 1, 0), 650, 300), SystemBox.MessageType.Error, true, 4f, false, true);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00031800 File Offset: 0x0002FA00
	public void SystemWinionRoomColor(bool _switch, WinionHandler winion)
	{
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		UIWinion component = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[winionType].GetComponent<UIWinion>();
		if (_switch)
		{
			Color color;
			if (ColorUtility.TryParseHtmlString("#E29B9B", ref color))
			{
				component.ChangedColor = true;
				component.winionImg.color = color;
				return;
			}
		}
		else
		{
			component.winionImg.color = Color.white;
			component.ChangedColor = false;
		}
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00011063 File Offset: 0x0000F263
	public void TryOpenWinionList(bool active)
	{
		if (active && !GameManager.HeaderOpenForce && GameManager.RunningFixMemory)
		{
			return;
		}
		this.WinionList.SetActive(active);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00011083 File Offset: 0x0000F283
	public void TryOpenWinionInfo(bool active)
	{
		if (active && !GameManager.HeaderOpenForce && GameManager.RunningFixMemory)
		{
			return;
		}
		this.WinionInfo.SetActive(active);
	}

	// Token: 0x04000536 RID: 1334
	[Header("위니언 비밀번호 (정보용)")]
	public string WinionPassword = "";

	// Token: 0x04000537 RID: 1335
	[Header("위니언 고유번호 (백신용)")]
	public string WinionIdNumber = "";

	// Token: 0x04000538 RID: 1336
	public Winion folder;

	// Token: 0x04000539 RID: 1337
	public TextMeshProUGUI FileName;

	// Token: 0x0400053A RID: 1338
	public Button RemoveButton;

	// Token: 0x0400053B RID: 1339
	public EventTrigger Trigger;

	// Token: 0x0400053C RID: 1340
	public GameObject targetWinion;

	// Token: 0x0400053D RID: 1341
	public Transform winionEmotionBubblePos;

	// Token: 0x0400053E RID: 1342
	public Transform winionSpeechBubblePos;

	// Token: 0x0400053F RID: 1343
	[Header("애정도 별 스티커")]
	public Transform friendShipStar;

	// Token: 0x04000540 RID: 1344
	[Header("배터리 양")]
	public TextMeshProUGUI batteryText;

	// Token: 0x04000541 RID: 1345
	[Header("현재 기분")]
	public TextMeshProUGUI moodText;

	// Token: 0x04000542 RID: 1346
	public GameObject light;

	// Token: 0x04000543 RID: 1347
	public List<Sprite> roomSprite;

	// Token: 0x04000544 RID: 1348
	public Image roomImage;

	// Token: 0x04000545 RID: 1349
	public string text = "";

	// Token: 0x04000546 RID: 1350
	public string text2 = "";

	// Token: 0x04000547 RID: 1351
	public string debugPassword = "";

	// Token: 0x04000548 RID: 1352
	public static bool ForceExit;

	// Token: 0x04000549 RID: 1353
	public string colorFix_String = "";

	// Token: 0x0400054A RID: 1354
	public GameObject AboutWinion;

	// Token: 0x0400054B RID: 1355
	public GameObject WinionMetaData;

	// Token: 0x0400054C RID: 1356
	public TextMeshProUGUI WinionIDText;

	// Token: 0x0400054D RID: 1357
	public static bool CanEnterPass;

	// Token: 0x0400054E RID: 1358
	public GameObject WinionList;

	// Token: 0x0400054F RID: 1359
	public GameObject WinionInfo;
}
