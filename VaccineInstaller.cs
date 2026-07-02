using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class VaccineInstaller : MonoBehaviour
{
	// Token: 0x06001D36 RID: 7478 RVA: 0x000D5720 File Offset: 0x000D3920
	private void OnEnable()
	{
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, 0f, false);
		this.index = 0;
		this.MainContent.SetActive(true);
		this.installInfo[0].SetActive(true);
		for (int i = 1; i < this.installInfo.Count; i++)
		{
			this.installInfo[i].SetActive(false);
		}
		this.downloadTMP.text = "";
		this.loadingEndButton.SetActive(false);
		int count = this.loadingBars.Count;
		for (int j = 0; j < count; j++)
		{
			this.loadingBars[j].SetActive(false);
		}
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x000D57D8 File Offset: 0x000D39D8
	private void OnDisable()
	{
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, 1f, false);
		this.index = 0;
		this.MainContent.SetActive(false);
		this.installInfo[0].SetActive(true);
		for (int i = 1; i < this.installInfo.Count; i++)
		{
			this.installInfo[i].SetActive(false);
		}
		this.downloadTMP.text = "";
		this.loadingEndButton.SetActive(false);
		int count = this.loadingBars.Count;
		for (int j = 0; j < count; j++)
		{
			this.loadingBars[j].SetActive(false);
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x000D5890 File Offset: 0x000D3A90
	public void NextButtonClick()
	{
		switch (this.index)
		{
		case 0:
			this.installInfo[0].SetActive(false);
			this.downloadTMP.text = "";
			this.installInfo[1].SetActive(true);
			base.StartCoroutine("InstallTexting");
			break;
		case 1:
			this.installInfo[1].SetActive(false);
			this.installInfo[2].SetActive(true);
			break;
		case 2:
			this.installInfo[2].SetActive(false);
			this.installInfo[0].SetActive(true);
			base.gameObject.SetActive(false);
			DBManager.instance.dialogueData.curEvent.startEvent = true;
			VaccineInstaller.Installed = true;
			break;
		}
		this.index++;
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0001B031 File Offset: 0x00019231
	private IEnumerator InstallTexting()
	{
		yield return new WaitForSeconds(1f);
		int index = 0;
		string[] array = this.downloadText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		int num;
		foreach (string text in array)
		{
			if (index >= this.loadingBars.Count)
			{
				index = this.loadingBars.Count - 1;
			}
			TweenerCore<string, string, StringOptions> tweenerCore = this.downloadTMP.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore);
			if (Random.Range(0f, 1f) <= 0.65f)
			{
				List<GameObject> list = this.loadingBars;
				num = index;
				index = num + 1;
				list[num].SetActive(true);
			}
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		string[] array2 = null;
		this.downloadTMP.DOText("Success: All packages have been downloaded and configured properly.\n\n\nInstalling...\n\n\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		for (int i = index; i < this.loadingBars.Count; i = num + 1)
		{
			this.loadingBars[i].SetActive(true);
			yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
			num = i;
		}
		TweenerCore<string, string, StringOptions> tweenerCore2 = this.downloadTMP.DOText("'Anti Virus for Winion' install completed with no errors.\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return TweenExtensions.WaitForCompletion(tweenerCore2);
		yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		this.loadingEndButton.SetActive(true);
		OpenVaccine.installVaccine = true;
		SingletoneBehaviour<DataManager>.Instance._BoolData.VaccineOpen = true;
		SingletoneBehaviour<VaccineManager>.Instance.SetVaccine(true);
		yield break;
	}

	// Token: 0x04001B3B RID: 6971
	public static bool Installed;

	// Token: 0x04001B3C RID: 6972
	public GameObject MainContent;

	// Token: 0x04001B3D RID: 6973
	public List<GameObject> installInfo = new List<GameObject>();

	// Token: 0x04001B3E RID: 6974
	public List<GameObject> loadingBars = new List<GameObject>();

	// Token: 0x04001B3F RID: 6975
	public GameObject loadingEndButton;

	// Token: 0x04001B40 RID: 6976
	public int index;

	// Token: 0x04001B41 RID: 6977
	[TextArea(10, 10)]
	public string downloadText;

	// Token: 0x04001B42 RID: 6978
	public TextMeshProUGUI downloadTMP;

	// Token: 0x04001B43 RID: 6979
	public float speed = 5f;
}
