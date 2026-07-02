using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D8 RID: 216
public class LanguageController : MonoBehaviour
{
	// Token: 0x0600053E RID: 1342 RVA: 0x000346A0 File Offset: 0x000328A0
	private void SetDisableAll()
	{
		for (int i = 0; i < this.Languages.Count; i++)
		{
			this.Languages[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000346DC File Offset: 0x000328DC
	public void SelectLanguage(int index, bool force = false)
	{
		DBManager.GotoIngame = false;
		LanguageController.CurrentLanguage = index;
		if (LanguageController.CurrentLanguage == 1)
		{
			SingletoneBehaviour<TittleManager>.Instance.KoreanWarning.SetActive(true);
			SingletoneBehaviour<TittleManager>.Instance.DesktopModeIcon.SetActive(true);
		}
		else
		{
			SingletoneBehaviour<TittleManager>.Instance.KoreanWarning.SetActive(false);
			SingletoneBehaviour<TittleManager>.Instance.DesktopModeIcon.SetActive(false);
		}
		if (PlayerPrefs.GetInt("Language", -1) != LanguageController.CurrentLanguage || force)
		{
			PlayerPrefs.SetInt("Language", LanguageController.CurrentLanguage);
			DBManager.instance.settingUI.Setting();
			DBManager.instance.SettingContent();
			DBManager.instance.title_Language.Title_Setting();
			PlayerPrefs.DeleteKey("PlayerName");
			DBManager.instance.ConversationsSetting();
			ChapterSetter.CompleteTranslateIngame = false;
		}
		this.SetDisableAll();
		if (this.Languages != null && index < this.Languages.Count)
		{
			this.Languages[index].gameObject.SetActive(true);
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x000347DC File Offset: 0x000329DC
	private void Awake()
	{
		this.Languages = new List<Transform>();
		for (int i = 0; i < base.transform.GetChild(0).childCount; i++)
		{
			int index = i;
			this.Languages.Add(base.transform.GetChild(0).GetChild(i).GetChild(0)
				.GetChild(1));
			base.transform.GetChild(0).GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				if (!DBManager.GotoIngame)
				{
					this.WarningText();
				}
				SingletoneBehaviour<TittleManager>.Instance.SecondDepth();
				this.SelectLanguage(index, false);
				DOVirtual.Float(0f, 1f, 0.2f, delegate(float f)
				{
				}).OnComplete(delegate
				{
					base.gameObject.GetComponent<UIWindow>().DestroyBox(false, false);
				});
			});
		}
		LanguageController.CurrentLanguage = PlayerPrefs.GetInt("Language", 0);
		base.StartCoroutine("WaitDBManager");
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x000348A0 File Offset: 0x00032AA0
	public void WarningText()
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
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00011477 File Offset: 0x0000F677
	private IEnumerator WaitDBManager()
	{
		yield return new WaitUntil(() => DBManager.instance != null);
		this.SelectLanguage(LanguageController.CurrentLanguage, false);
		yield break;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00011486 File Offset: 0x0000F686
	private void OnEnable()
	{
		base.StartCoroutine("WaitDBManager");
	}

	// Token: 0x040005CC RID: 1484
	public List<Transform> Languages;

	// Token: 0x040005CD RID: 1485
	public static int CurrentLanguage;
}
