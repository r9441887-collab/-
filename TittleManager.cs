using System;
using System.Collections;
using System.Text.RegularExpressions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000DD RID: 221
public class TittleManager : SingletoneBehaviour<TittleManager>
{
	// Token: 0x0600055B RID: 1371 RVA: 0x00011589 File Offset: 0x0000F789
	public void TryOpenStoryInstaller()
	{
		StoryInstaller.Installed = PlayerPrefs.GetInt("StoryInstalled", 0) == 1;
		if (StoryInstaller.Installed)
		{
			this.StoryAleadyInstalled.SetActive(true);
			return;
		}
		this.StoryInstallerWindow.SetActive(true);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00034B5C File Offset: 0x00032D5C
	private void SetDefault()
	{
		this.PlayerName.text = PlayerPrefs.GetString("PlayerName", "Administrator");
		StoryInstaller.Installed = PlayerPrefs.GetInt("StoryInstalled", 0) == 1;
		this.StoryModeIcon.SetActive(StoryInstaller.Installed);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000115C2 File Offset: 0x0000F7C2
	private void GetVersion()
	{
		string @string = PlayerPrefs.GetString("Version", TittleManager.LastVersion.ToString());
		long.Parse(@string);
		long.Parse(@string);
		long lastVersion = TittleManager.LastVersion;
		PlayerPrefs.SetString("Version", TittleManager.LastVersion.ToString());
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00034BAC File Offset: 0x00032DAC
	private void Start()
	{
		this.GetVersion();
		this.SetDefault();
		this.NameSetting();
		if (this.StartLogoFade)
		{
			this.TitleObject.SetActive(true);
			this.TitleGroup.alpha = 1f;
			this.LoginObject.transform.localScale = Vector3.one * 1.4f;
			this.LoginGroup.alpha = 0f;
			this.LoginObject.SetActive(false);
			if (this.StartLogoFade)
			{
				this.EndLogoFade = false;
				base.StartCoroutine("PlayingLogo");
			}
			else
			{
				this.EndLogoFade = true;
			}
		}
		this.CanOpenDesktopMode = PlayerPrefs.GetInt("CanOpenDesktopMode", 0) == 1;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000115FF File Offset: 0x0000F7FF
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.ClickUI_Light, false, 1f, 1f);
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00034C64 File Offset: 0x00032E64
	public void DoTransition()
	{
		this.TitleSoundWindow.DestroyBox(false, false);
		this.LoginSoundWindow.DestroyBox(false, false);
		this.CanTransition = false;
		DOVirtual.Float(0f, 1f, 1f, delegate(float value)
		{
		}).OnComplete(delegate
		{
			this.CanTransition = true;
		});
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00034CD8 File Offset: 0x00032ED8
	public void FirstDepth()
	{
		if (!this.CanTransition)
		{
			return;
		}
		this.DoTransition();
		if (!this.EndLogoFade)
		{
			return;
		}
		if (this.UIDepth != 0)
		{
			return;
		}
		this.UIDepth = 1;
		this.TitleGroup.DOFade(0f, 0.4f).onComplete = delegate
		{
			this.TitleGroup.gameObject.SetActive(false);
		};
		this.LoginGroup.gameObject.SetActive(true);
		this.LoginGroup.DOFade(1f, 0.4f);
		ShortcutExtensions.DOScale(this.TitleObject.transform, 2.5f, 1f).SetEase(Ease.OutQuad);
		ShortcutExtensions.DOScale(this.LoginObject.transform.GetComponent<RectTransform>(), 1f, 1f).SetEase(Ease.InOutFlash);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.OSLoginSound, false, 1f, 1f);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00034DBC File Offset: 0x00032FBC
	public void SecondDepth()
	{
		if (!this.CanTransition)
		{
			return;
		}
		this.DoTransition();
		if (this.UIDepth != 1)
		{
			return;
		}
		this.TitleGroup.gameObject.SetActive(true);
		this.TitleGroup.DOFade(1f, 0.4f);
		this.LoginGroup.DOFade(0f, 0.4f).onComplete = delegate
		{
			this.LoginGroup.gameObject.SetActive(false);
		};
		this.UIDepth = 0;
		ShortcutExtensions.DOScale(this.TitleObject.transform, 1f, 1f).SetEase(Ease.OutQuad);
		ShortcutExtensions.DOScale(this.LoginObject.transform.GetComponent<RectTransform>(), 1.4f, 1f).SetEase(Ease.InOutFlash);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00011621 File Offset: 0x0000F821
	private IEnumerator PlayingLogo()
	{
		this.LogoGroup.alpha = 0f;
		SingletoneBehaviour<FadeInAndOut>.Instance.canvasGroup.alpha = 1f;
		yield return new WaitForSeconds(1f);
		yield return TweenExtensions.WaitForCompletion(this.LogoGroup.DOFade(1f, 1f));
		yield return new WaitForSeconds(1f);
		yield return TweenExtensions.WaitForCompletion(this.LogoGroup.DOFade(0f, 1f));
		yield return new WaitForSeconds(1f);
		yield return TweenExtensions.WaitForCompletion(SingletoneBehaviour<FadeInAndOut>.Instance.canvasGroup.DOFade(0f, 1f));
		this.EndLogoFade = true;
		yield break;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00011630 File Offset: 0x0000F830
	public void EnterScale(GameObject target)
	{
		ShortcutExtensions.DOScale(target.transform, Vector3.one * 1.2f, 1f);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00011652 File Offset: 0x0000F852
	public void ExitScale(GameObject target)
	{
		ShortcutExtensions.DOScale(target.transform, Vector3.one, 1f);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001166A File Offset: 0x0000F86A
	public void LoadScene(string sceneName, bool _startBlackScreen = false)
	{
		SceneLoader.LoadScene(sceneName, _startBlackScreen, false);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00034E80 File Offset: 0x00033080
	public void TryStartStoryMode()
	{
		this.settingName();
		this.PlayerHasNickName = PlayerPrefs.GetString("PlayerName", "Administrator") != "Administrator";
		if (!this.PlayerHasNickName)
		{
			this.PlayerNameInput.SetActive(true);
			return;
		}
		if (this.StartGame)
		{
			base.StartCoroutine("LoadNextScene");
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00011674 File Offset: 0x0000F874
	public void TryStartDesktopMode()
	{
		base.StartCoroutine("LoadDesktopScene");
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00011682 File Offset: 0x0000F882
	public void SetStartGame(bool value)
	{
		this.StartGame = value;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00034EDC File Offset: 0x000330DC
	public void SetPlayerNickName()
	{
		PlayerPrefs.SetString("PlayerName", this.PlayerNameInputField.text);
		DataManager._PlayerName = this.PlayerNameInputField.text;
		this.PlayerName.text = this.PlayerNameInputField.text;
		this.PlayerHasNickName = true;
		this.PlayerNameInput.SetActive(false);
		if (this.StartGame)
		{
			base.StartCoroutine("LoadNextScene");
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001168B File Offset: 0x0000F88B
	private IEnumerator LoadNextScene()
	{
		SoundManager.instance.Stop_BGM(1f);
		yield return TweenExtensions.WaitForCompletion(SingletoneBehaviour<FadeInAndOut>.Instance.canvasGroup.DOFade(1f, 1f));
		yield return new WaitForSeconds(0.5f);
		Events.StartAutoEvent = true;
		Events.AutoChapterIndex = DataManager._LastChapter;
		Events.AutoEventIndex = DataManager._LastEvent;
		this.LoadScene("Chapter 02_EunBin", false);
		yield break;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001169A File Offset: 0x0000F89A
	private IEnumerator LoadDesktopScene()
	{
		SoundManager.instance.Stop_BGM(1f);
		yield return TweenExtensions.WaitForCompletion(SingletoneBehaviour<FadeInAndOut>.Instance.canvasGroup.DOFade(1f, 1f));
		yield return new WaitForSeconds(0.5f);
		this.LoadScene("DesktopMode", false);
		yield break;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000116A9 File Offset: 0x0000F8A9
	public void NameChanged(TMP_InputField field)
	{
		this.ConfirmButton.interactable = field.text.Length > 0;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000116C4 File Offset: 0x0000F8C4
	public void ExitGame()
	{
		Application.Quit();
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00034F4C File Offset: 0x0003314C
	public void ClearData(bool force = false)
	{
		PlayerPrefs.DeleteAll();
		this.SetDefault();
		DataManager.LoadPrefs();
		this.CanOpenDesktopMode = PlayerPrefs.GetInt("CanOpenDesktopMode", 0) == 1;
		this.PlayerName.text = PlayerPrefs.GetString("PlayerName", "Administrator");
		StoryInstaller.Installed = PlayerPrefs.GetInt("StoryInstalled", 0) == 1;
		this.StoryModeIcon.SetActive(StoryInstaller.Installed);
		this.DesktopModeIcon.SetActive(false);
		this.PlayerNameInput.SetActive(false);
		this.PlayerHasNickName = false;
		this.StartGame = false;
		this.PlayerNameInputField.text = "";
		DBManager.instance.DialogueSetting();
		PlayerPrefs.SetInt("Language", 0);
		LanguageController.CurrentLanguage = PlayerPrefs.GetInt("Language", 0);
		this.LanguageSetting.SelectLanguage(LanguageController.CurrentLanguage, force);
		if (SingletoneBehaviour<DemoScript>.Instance != null && SingletoneBehaviour<DemoScript>.Instance._IsDemo)
		{
			SingletoneBehaviour<DemoScript>.Instance.SetDemoChapter();
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x000116CB File Offset: 0x0000F8CB
	private IEnumerator WaitDBManager()
	{
		yield return new WaitUntil(() => DBManager.instance != null);
		LanguageController.CurrentLanguage = PlayerPrefs.GetInt("Language", 0);
		this.LanguageSetting.SelectLanguage(LanguageController.CurrentLanguage, false);
		yield break;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00035050 File Offset: 0x00033250
	private void Awake()
	{
		if (PlayerPrefs.GetInt("FirstDemo", 1) == 0)
		{
			this.ClearDataObject.SetActive(true);
		}
		else
		{
			this.ClearDataObject.SetActive(false);
		}
		base.StartCoroutine("WaitDBManager");
		this.PlayerHasNickName = PlayerPrefs.GetString("PlayerName", "Administrator") != "Administrator";
		SingletoneBehaviour<IconManager>.Instance = this.iconManager;
		EndingCredit.CreditEnd = false;
		WinionHangingMouse.activeAlready = false;
		GameManager.ReadyGetter = false;
		GameManager.RunningFixMemory = false;
		GameManager.HeaderOpenForce = false;
		GameManager.SystemBroken = false;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x000116DA File Offset: 0x0000F8DA
	public void NameSetting()
	{
		if (this.PlayerNameInputField != null)
		{
			this.PlayerNameInputField.onValueChanged.AddListener(new UnityAction<string>(this.ValidateInput));
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000350E0 File Offset: 0x000332E0
	private void ValidateInput(string input)
	{
		string text = "[가-힣ㄱ-ㅎㅏ-ㅣ]";
		int @int = PlayerPrefs.GetInt("Language", 0);
		if (@int == 0)
		{
			text = "[a-zA-Z]";
		}
		else if (@int == 1)
		{
			text = "[가-힣ㄱ-ㅎㅏ-ㅣ]";
		}
		string text2 = "";
		foreach (char c in input)
		{
			if (Regex.IsMatch(c.ToString(), text))
			{
				text2 += c.ToString();
			}
		}
		this.PlayerNameInputField.text = text2;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00035164 File Offset: 0x00033364
	public void settingName()
	{
		this.NameChanged(this.PlayerNameInputField);
		this.PlayerHasNickName = PlayerPrefs.GetString("PlayerName", "Administrator") != "Administrator";
		if (!this.PlayerHasNickName)
		{
			this.PlayerNameInputField.text = "";
			return;
		}
		string @string = PlayerPrefs.GetString("PlayerName", "Administrator");
		this.PlayerNameInputField.text = @string;
	}

	// Token: 0x040005E0 RID: 1504
	public GameObject WarningText;

	// Token: 0x040005E1 RID: 1505
	public GameObject WarningText_Black;

	// Token: 0x040005E2 RID: 1506
	public GameObject KoreanWarning;

	// Token: 0x040005E3 RID: 1507
	public CanvasGroup WarningText_BlackCanvaManager;

	// Token: 0x040005E4 RID: 1508
	public IconManager iconManager;

	// Token: 0x040005E5 RID: 1509
	public bool StartLogoFade;

	// Token: 0x040005E6 RID: 1510
	public bool EndLogoFade = true;

	// Token: 0x040005E7 RID: 1511
	public bool CanTransition = true;

	// Token: 0x040005E8 RID: 1512
	public CanvasGroup LogoGroup;

	// Token: 0x040005E9 RID: 1513
	public CanvasGroup TitleGroup;

	// Token: 0x040005EA RID: 1514
	public CanvasGroup LoginGroup;

	// Token: 0x040005EB RID: 1515
	public int UIDepth;

	// Token: 0x040005EC RID: 1516
	public GameObject TitleObject;

	// Token: 0x040005ED RID: 1517
	public GameObject LoginObject;

	// Token: 0x040005EE RID: 1518
	public UIWindow TitleSoundWindow;

	// Token: 0x040005EF RID: 1519
	public UIWindow LoginSoundWindow;

	// Token: 0x040005F0 RID: 1520
	[Space(10f)]
	public GameObject StoryModeIcon;

	// Token: 0x040005F1 RID: 1521
	public GameObject StoryInstallerWindow;

	// Token: 0x040005F2 RID: 1522
	public GameObject StoryAleadyInstalled;

	// Token: 0x040005F3 RID: 1523
	public GameObject DesktopModeIcon;

	// Token: 0x040005F4 RID: 1524
	[Space(10f)]
	[Header("Login Page Player Name")]
	public TextMeshProUGUI PlayerName;

	// Token: 0x040005F5 RID: 1525
	[Header("Player Name Input")]
	public TMP_InputField PlayerNameInputField;

	// Token: 0x040005F6 RID: 1526
	[Space(10f)]
	public bool PlayerHasNickName;

	// Token: 0x040005F7 RID: 1527
	public GameObject PlayerNameInput;

	// Token: 0x040005F8 RID: 1528
	public bool StartGame;

	// Token: 0x040005F9 RID: 1529
	public bool CanOpenDesktopMode;

	// Token: 0x040005FA RID: 1530
	public GameObject ClearDataObject;

	// Token: 0x040005FB RID: 1531
	public Button ConfirmButton;

	// Token: 0x040005FC RID: 1532
	public static long LastVersion = 20250115001L;

	// Token: 0x040005FD RID: 1533
	public GameObject LoginButton;

	// Token: 0x040005FE RID: 1534
	public GameObject CantLogin;

	// Token: 0x040005FF RID: 1535
	public LanguageController LanguageSetting;
}
