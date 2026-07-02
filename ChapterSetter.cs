using System;
using System.Collections;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class ChapterSetter : MonoBehaviour
{
	// Token: 0x06001A12 RID: 6674 RVA: 0x00018EF9 File Offset: 0x000170F9
	private IEnumerator SettingChapter()
	{
		ChapterSetter.ChapterSetEnd = -1;
		yield return new WaitUntil(() => SingletoneBehaviour<WinionCalender>.Instance != null);
		if (!SingletoneBehaviour<WinionCalender>.Instance.tweenActive)
		{
			yield return new WaitUntil(() => SingletoneBehaviour<FadeInAndOut>.Instance != null);
			SingletoneBehaviour<FadeInAndOut>.Instance.canvasGroup.alpha = 1f;
		}
		if (!ChapterSetter.CompleteTranslateIngame)
		{
			yield return new WaitUntil(() => DBManager.instance != null);
			DBManager.instance.SettingContent();
			ChapterSetter.CompleteTranslateIngame = true;
			yield return new WaitUntil(() => SingletoneBehaviour<MailManager>.Instance != null);
			SingletoneBehaviour<MailManager>.Instance.mailList.ChangeLanguage();
			DBManager.instance.ingame_Language.Setting();
		}
		yield return new WaitUntil(() => GameManager.ReadyGetter);
		yield return new WaitUntil(() => !UnityObjectUtility.IsUnityNull(GameManager.instance));
		GameManager.instance.gameData.curChapter = this.ThisChapter;
		if (this.ThisChapter >= GameManager.Chapter.chapter00)
		{
			for (int i = 0; i < GameManager.instance.GetWinionHandlers().Count; i++)
			{
				GameManager.instance.GetWinionHandlers()[i].winionStatus.winionInfo.battery = 100;
				GameManager.instance.GetWinionHandlers()[i].winionDragAndDrop.canDragAndDrop = false;
				for (int j = 0; j < 5; j++)
				{
					SingletoneBehaviour<WinionFolderManager>.Instance.SetExitLockWinionFolder((Winion)i, (Winion)j, false);
				}
			}
		}
		this.GetChapter();
		this.SetVaccine();
		this.SetFixIsDead();
		this.SetFixMemory();
		this.SetSystemBroken();
		this.Set3DHorrorSetting();
		this.SetDefault();
		ChapterSetter.CurrEventStatic = this.CurrEvent;
		PlayerPrefs.SetInt("LastChapter", this.CurrEvent / 100);
		PlayerPrefs.SetInt("LastEvent", this.CurrEvent % 100);
		yield return base.StartCoroutine("SettingMail");
		ChapterSetter.ChapterSetEnd = 0;
		yield return new WaitUntil(() => !SingletoneBehaviour<WinionCalender>.Instance.tweenActive);
		yield return new WaitUntil(() => ChapterSetter.ChapterSetEnd == 1);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(2f, 0f, null, null);
		this.SettingRoutine = null;
		yield break;
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x00018F08 File Offset: 0x00017108
	private void OnEnable()
	{
		if (this.SettingRoutine == null)
		{
			this.SettingRoutine = base.StartCoroutine(this.SettingChapter());
		}
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000BF3CC File Offset: 0x000BD5CC
	public int GetChapter()
	{
		this.MyName = base.gameObject.name;
		this.CurrEvent = this.ExtractNumber(this.MyName);
		switch (this.ThisChapter)
		{
		case GameManager.Chapter.chapter01:
			this.CurrEvent += 100;
			break;
		case GameManager.Chapter.chapter02:
			this.CurrEvent += 200;
			break;
		case GameManager.Chapter.chapter03:
			this.CurrEvent += 300;
			break;
		}
		return this.CurrEvent;
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x00018F24 File Offset: 0x00017124
	private int ExtractNumber(string input)
	{
		return int.Parse(Regex.Match(input, "\\d+").Value);
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x00018F3B File Offset: 0x0001713B
	private void Set3DHorrorSetting()
	{
		SingletoneBehaviour<HorrorSetting>.Instance.SetChapterHorrorSetting(this.CurrEvent);
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000BF464 File Offset: 0x000BD664
	private void SetDefault()
	{
		int currEvent = this.CurrEvent;
		if (currEvent <= 206)
		{
			if (currEvent == 205)
			{
				PlayerPrefs.SetInt("3DClear", 0);
				return;
			}
			if (currEvent != 206)
			{
				return;
			}
			Cursor.lockState = 0;
			return;
		}
		else
		{
			if (currEvent == 301)
			{
				Cursor.lockState = 0;
				SoundManager.instance.Stop_BGM(0.2f);
				return;
			}
			if (currEvent != 311)
			{
				return;
			}
			Cursor.lockState = 0;
			return;
		}
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x00018F4D File Offset: 0x0001714D
	private IEnumerator SettingMail()
	{
		yield return new WaitUntil(() => SingletoneBehaviour<MailManager>.Instance != null);
		yield return new WaitUntil(() => SingletoneBehaviour<GalleryManager>.Instance != null);
		SystemAlram.MuteSound = true;
		if (this.CurrEvent > 0)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(0, true);
		}
		if (this.CurrEvent > 1)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(1, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_2", 1);
		}
		if (this.CurrEvent > 2)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(2, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_3", 1);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_4", 1);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_5", 1);
		}
		if (this.CurrEvent > 4)
		{
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_6", 1);
		}
		if (this.CurrEvent > 5)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(3, true);
		}
		if (this.CurrEvent > 6)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(4, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_10", 1);
		}
		if (this.CurrEvent > 8)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(5, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_11", 1);
		}
		if (this.CurrEvent > 10)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(6, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(7, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_12", 1);
		}
		if (this.CurrEvent > 11)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(8, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_13", 1);
		}
		if (this.CurrEvent > 12)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(9, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(10, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(11, true);
		}
		if (this.CurrEvent > 13)
		{
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_7", 1);
		}
		if (this.CurrEvent > 14)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(12, true);
		}
		if (this.CurrEvent > 15)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(13, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(14, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_14", 1);
		}
		if (this.CurrEvent > 100)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(15, true);
		}
		if (this.CurrEvent > 101)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(17, true);
		}
		if (this.CurrEvent > 205)
		{
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_0", 1);
		}
		if (this.CurrEvent > 207)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(19, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(20, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(21, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(22, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(23, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(24, true);
		}
		if (this.CurrEvent > 208)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(25, true);
		}
		if (this.CurrEvent > 209)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(26, true);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_1", 1);
		}
		if (this.CurrEvent > 211)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(27, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(28, true);
		}
		if (this.CurrEvent > 221)
		{
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_8", 1);
		}
		if (this.CurrEvent > 312)
		{
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_9", 1);
			SingletoneBehaviour<GalleryManager>.Instance.AddImage("Image_15", 1);
		}
		if (this.CurrEvent > 313)
		{
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(29, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(30, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(31, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(32, true);
			SingletoneBehaviour<MailManager>.Instance.AddNewMail(33, true);
		}
		SystemAlram.MuteSound = false;
		yield break;
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
	private void SetFixIsDead()
	{
		int currEvent = this.CurrEvent;
		if (currEvent == 226)
		{
			PlayerPrefs.SetInt("FixIsDead", 0);
			return;
		}
		if (currEvent != 227)
		{
			return;
		}
		PlayerPrefs.SetInt("FixIsDead", 1);
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x000BF510 File Offset: 0x000BD710
	private void SetSystemBroken()
	{
		int currEvent = this.CurrEvent;
		if (currEvent - 103 > 8 && currEvent - 200 > 5)
		{
			if (currEvent != 206)
			{
				GameManager.SystemBroken = false;
			}
			else
			{
				GameManager.SystemBroken = true;
			}
		}
		else
		{
			GameManager.SystemBroken = true;
		}
		SingletoneBehaviour<MailManager>.Instance.SetMailBrokenStatus(GameManager.SystemBroken);
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x000BF564 File Offset: 0x000BD764
	private void SetFixMemory()
	{
		int currEvent = this.CurrEvent;
		switch (currEvent)
		{
		case 213:
		case 215:
		case 216:
		case 217:
		case 219:
		case 220:
		case 221:
		case 222:
		case 224:
		case 225:
			break;
		case 214:
		case 218:
		case 223:
			goto IL_0058;
		default:
			if (currEvent - 302 > 7)
			{
				goto IL_0058;
			}
			break;
		}
		GameManager.RunningFixMemory = true;
		return;
		IL_0058:
		GameManager.RunningFixMemory = false;
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x000BF5D0 File Offset: 0x000BD7D0
	private void SetVaccine()
	{
		if (this.CurrEvent >= 208)
		{
			int currEvent = this.CurrEvent;
			switch (currEvent)
			{
			case 213:
			case 215:
			case 216:
			case 217:
			case 219:
			case 220:
			case 221:
			case 222:
			case 224:
			case 225:
				break;
			case 214:
			case 218:
			case 223:
				goto IL_0070;
			default:
				if (currEvent - 302 > 7)
				{
					goto IL_0070;
				}
				break;
			}
			SingletoneBehaviour<VaccineManager>.Instance.SetVaccine(false);
			VaccineInstaller.Installed = false;
			return;
			IL_0070:
			SingletoneBehaviour<VaccineManager>.Instance.SetVaccine(true);
			VaccineInstaller.Installed = true;
			return;
		}
		SingletoneBehaviour<VaccineManager>.Instance.SetVaccine(false);
		VaccineInstaller.Installed = false;
	}

	// Token: 0x0400161D RID: 5661
	public GameManager.Chapter ThisChapter;

	// Token: 0x0400161E RID: 5662
	public string MyName;

	// Token: 0x0400161F RID: 5663
	public int CurrEvent;

	// Token: 0x04001620 RID: 5664
	public static int CurrEventStatic = 0;

	// Token: 0x04001621 RID: 5665
	public static int ChapterSetEnd = -1;

	// Token: 0x04001622 RID: 5666
	public static bool CompleteTranslateIngame = false;

	// Token: 0x04001623 RID: 5667
	public Coroutine SettingRoutine;
}
