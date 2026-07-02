using System;
using UnityEngine;

// Token: 0x020003DD RID: 989
public class Korean_English_ChangeKey : MonoBehaviour
{
	// Token: 0x06001CEF RID: 7407 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x000D3F90 File Offset: 0x000D2190
	public void Change_Language()
	{
		if (PlayerPrefs.GetInt("Language", 0) == 0)
		{
			PlayerPrefs.SetInt("Language", 1);
		}
		else
		{
			PlayerPrefs.SetInt("Language", 0);
		}
		Debug.Log("curLanguage  " + PlayerPrefs.GetInt("Language", -1).ToString());
		DBManager.instance.StartParser();
		DBManager.instance.SettingContent();
		DBManager.instance.ingame_Language.Setting();
	}
}
