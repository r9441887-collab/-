using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x0200041F RID: 1055
public class VaccineManager : SingletoneBehaviour<VaccineManager>
{
	// Token: 0x06001E3E RID: 7742 RVA: 0x0001B954 File Offset: 0x00019B54
	public void SetVaccine(bool value)
	{
		this.VaccineIcon.SetActive(value);
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x0001B962 File Offset: 0x00019B62
	public void ClearConsole()
	{
		this.antiVirusText.text = this.clearText;
		this.resultText.text = "";
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0001B985 File Offset: 0x00019B85
	public void DeepScanToggle(Toggle toggle)
	{
		this.isDeepScan = toggle.isOn;
		if (this.isDeepScan)
		{
			this.InputField.interactable = true;
			return;
		}
		this.InputField.interactable = false;
		this.InputField.text = "";
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x000D9B68 File Offset: 0x000D7D68
	public void ScanMyPC()
	{
		if (DBManager.instance.dialogueData.curDialogue_ing)
		{
			return;
		}
		this.scanButton.interactable = false;
		if (this.isDeepScan)
		{
			base.StartCoroutine("DeepScanRoutine");
			return;
		}
		base.StartCoroutine("ScanMyPCRoutine");
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0001B9C4 File Offset: 0x00019BC4
	private IEnumerator ReadyScan()
	{
		this.resultText.text = "Scanning...";
		this.antiVirusText.DOText("\n\n\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		string[] array = this.virusScanText[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		string[] array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0001B9D3 File Offset: 0x00019BD3
	private IEnumerator DeepScanRoutine()
	{
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = true;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = false;
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.TranshCan);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.MailBox);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.MyPC);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.BatteryCenter);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.Folder_Ion);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.Folder_Bo);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.Folder_Grid);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.Folder_Fix);
		VaccineManager.<DeepScanRoutine>g__LockIcon|22_0(Icon.Folder_Debug);
		int num = 3;
		WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
		if (component.WinionIDText.text != this.FixPassword)
		{
			this.FixPassword = component.WinionIDText.text;
		}
		bool isCorrect = false;
		this.CheckFixIsVirus = false;
		this.CheckDebugIsVirus = false;
		if (string.Compare(this.FixPassword, this.InputField.text) == 0)
		{
			isCorrect = true;
			this.CheckFixIsVirus = true;
		}
		else if (this.PassDebug || string.Compare(this.DebugPassword, this.InputField.text) == 0)
		{
			isCorrect = true;
			this.CheckDebugIsVirus = true;
			this.KillDebug = true;
		}
		else
		{
			isCorrect = false;
			this.CheckFixIsVirus = false;
			this.CheckDebugIsVirus = false;
		}
		string errorMessage = "<color=#AC0000><size=30>Password does not match.</color></size>";
		int FixIsDead = PlayerPrefs.GetInt("FixIsDead", 0);
		Debug.Log(FixIsDead.ToString() + " : " + this.CheckFixIsVirus.ToString());
		if (FixIsDead == 1 && this.CheckFixIsVirus)
		{
			isCorrect = false;
			errorMessage = "<color=#AC0000><size=30>Something is wrong...</color></size>";
		}
		yield return base.StartCoroutine("ReadyScan");
		if (isCorrect)
		{
			if (this.CheckFixIsVirus)
			{
				SoundManager.instance.BGM_ChangeVolume_Tween(5f, 0f, false);
				AudioSource audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase01, false, 1f, 1f);
				SoundManager.instance.SfxSoundTween(audioSource, 2f, 5f, false, true);
				PlayerPrefs.SetInt("FixIsDead", 1);
			}
			string[] array = this.virusScanText[5].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
					.SetRelative(true);
				yield return TweenExtensions.WaitForCompletion(tweenerCore);
				yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
			}
			string[] array2 = null;
			this.antiVirusText.text = this.antiVirusText.text + "</size>\n\n";
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine("ScanTextRoutine");
			if (this.CheckFixIsVirus)
			{
				yield return base.StartCoroutine("FindVirus");
			}
			else if (this.CheckDebugIsVirus)
			{
				yield return base.StartCoroutine("FindWarningObject");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
				SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
			}
		}
		else
		{
			string[] array = this.virusScanText[4].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if (FixIsDead == 1 && this.CheckFixIsVirus)
			{
				this.CheckFixIsVirus = false;
				this.CheckDebugIsVirus = false;
				array = errorMessage.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			}
			foreach (string text2 in array)
			{
				TweenerCore<string, string, StringOptions> tweenerCore2 = this.antiVirusText.DOText(text2 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
					.SetRelative(true);
				yield return TweenExtensions.WaitForCompletion(tweenerCore2);
				yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
			}
			string[] array2 = null;
			this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.TranshCan, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.MailBox, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.MyPC, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.BatteryCenter, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Ion, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Bo, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Grid, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Fix, true);
			SingletoneBehaviour<IconManager>.Instance.SetIconLock(Icon.Folder_Debug, true);
			yield return new WaitForSeconds(1f);
			this.resultText.text = errorMessage;
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
			SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		}
		yield return null;
		this.antiVirusText.fontSize = 30f;
		this.scanButton.interactable = true;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		yield break;
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x0001B9E2 File Offset: 0x00019BE2
	private IEnumerator FindVirus()
	{
		SoundManager.Instance.Stop_BGM(1f);
		yield return new WaitForSeconds(1f);
		VaccineManager.<FindVirus>g__CloseFolder|23_0(Icon.Folder_Ion);
		VaccineManager.<FindVirus>g__CloseFolder|23_0(Icon.Folder_Bo);
		VaccineManager.<FindVirus>g__CloseFolder|23_0(Icon.Folder_Grid);
		VaccineManager.<FindVirus>g__CloseFolder|23_0(Icon.Folder_Fix);
		VaccineManager.<FindVirus>g__CloseFolder|23_0(Icon.Folder_Debug);
		AudioSource audioSource = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.HorrorChase02, false, 1f, 1f);
		SoundManager.instance.SfxSoundTween(audioSource, 1.5f, 2f, false, true);
		Tweener sizeTween = DOVirtual.Float(-1f, 1f, 0.5f, delegate(float value)
		{
			this.antiVirusText.fontSize = 30f + value + Random.Range(-1f, 1f);
		}).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		this.antiVirusText.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		this.antiVirusText.color = Color.green;
		yield return new WaitForSeconds(0.2f);
		this.antiVirusText.color = Color.red;
		string[] array = this.virusScanText[6].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		string[] array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(1f);
		int num;
		for (int i = 0; i < 5; i = num + 1)
		{
			array = this.virusScanText[7].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text2 in array)
			{
				TweenerCore<string, string, StringOptions> tweenerCore2 = this.antiVirusText.DOText(text2 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
					.SetRelative(true);
				yield return TweenExtensions.WaitForCompletion(tweenerCore2);
				yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
			}
			array2 = null;
			TextMeshProUGUI textMeshProUGUI = this.antiVirusText;
			textMeshProUGUI.text += "</size>\n\n";
			num = i;
		}
		Action action = delegate
		{
			SingletoneBehaviour<MouseRaycast>.Instance.SetTopMostLayer(this.FixKillVideo.gameObject);
			DBManager.instance.dialogueData.NoBacklogOpen = true;
			this.FixKillVideo.gameObject.SetActive(true);
			SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.1f, false);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.1f);
			this.InputField.text = "";
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1f, 0f, action, null, 1f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HorrorChase01, 1f);
		SoundManager.instance.Stop_SfxSound(SoundManager.SfxSound.HorrorChase02, 1f);
		bool waitPlay = true;
		Action action2 = delegate
		{
			waitPlay = false;
			this.FixKillVideo.Play();
			sizeTween.Kill(false);
			this.antiVirusText.color = Color.green;
			this.ClearConsole();
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1f, 2f, action2, null);
		yield return new WaitUntil(() => !this.FixKillVideo.isPlaying && !waitPlay);
		DBManager.instance.NoBacklogOpen_False();
		DBManager.instance.dialogueData.curEvent.startEvent = true;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		yield break;
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x0001B9F1 File Offset: 0x00019BF1
	private IEnumerator FindWarningObject()
	{
		yield return new WaitForSeconds(0.3f);
		string[] array = this.virusScanText[8].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		string[] array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(0.3f);
		array = this.virusScanText[9].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text2 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore2 = this.antiVirusText.DOText(text2 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore2);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		TextMeshProUGUI textMeshProUGUI = this.antiVirusText;
		textMeshProUGUI.text += "</size>\n\n";
		yield return new WaitForSeconds(1f);
		array = this.virusScanText[10].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text3 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore3 = this.antiVirusText.DOText(text3 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore3);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		array2 = null;
		yield return new WaitForSeconds(1f);
		array = this.virusScanText[11].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text4 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore4 = this.antiVirusText.DOText(text4 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore4);
			yield return new WaitForSeconds(2f);
		}
		array2 = null;
		array = this.virusScanText[12].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text5 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore5 = this.antiVirusText.DOText(text5 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore5);
			yield return new WaitForSeconds(0.2f);
		}
		array2 = null;
		array = this.virusScanText[13].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text6 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore6 = this.antiVirusText.DOText(text6 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore6);
			yield return new WaitForSeconds(1f);
		}
		array2 = null;
		TextMeshProUGUI textMeshProUGUI2 = this.antiVirusText;
		textMeshProUGUI2.text += "\n\n";
		array = this.virusScanText[14].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text7 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore7 = this.antiVirusText.DOText(text7 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore7);
			yield return new WaitForSeconds(1f);
		}
		array2 = null;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		yield break;
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x0001BA00 File Offset: 0x00019C00
	private IEnumerator ScanMyPCRoutine()
	{
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = true;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = false;
		yield return base.StartCoroutine("ReadyScan");
		yield return base.StartCoroutine("ScanTextRoutine");
		TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText("\n\nThe virus has been detected. \nBut it is not critical, maybe.\n\n\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return TweenExtensions.WaitForCompletion(tweenerCore);
		this.resultText.text = "The virus has been detected\nBut it is not critical, maybe.\n";
		this.scanButton.interactable = true;
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Vaccine).GetComponent<UIWindow>().CantClose = false;
		SingletoneBehaviour<IconManager>.Instance.WindowActiveList[20].canOpen = true;
		yield break;
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x0001BA0F File Offset: 0x00019C0F
	private IEnumerator ScanTextRoutine()
	{
		string[] array = this.virusScanText[1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore = this.antiVirusText.DOText(text + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		string[] array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(1f);
		array = this.virusScanText[2].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text2 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore2 = this.antiVirusText.DOText(text2 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore2);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		array2 = null;
		this.antiVirusText.DOText("\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(1f);
		int count = 20;
		int num;
		for (int i = 0; i < count; i = num + 1)
		{
			string text3 = "Scanning file: " + Guid.NewGuid().ToString() + ".tmp";
			TweenerCore<string, string, StringOptions> tweenerCore3 = this.antiVirusText.DOText(text3 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore3);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
			num = i;
		}
		array = this.virusScanText[3].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text4 in array)
		{
			TweenerCore<string, string, StringOptions> tweenerCore4 = this.antiVirusText.DOText(text4 + "\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
				.SetRelative(true);
			yield return TweenExtensions.WaitForCompletion(tweenerCore4);
			yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
		}
		array2 = null;
		this.antiVirusText.DOText("\n\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		string previusText = this.antiVirusText.text;
		List<string> tempString = new List<string>();
		tempString.Add("Progress: [######              ] 30%");
		tempString.Add("Progress: [#######             ] 35%");
		tempString.Add("Progress: [########            ] 40%");
		tempString.Add("Progress: [#########           ] 45%");
		tempString.Add("Progress: [##########          ] 50%");
		tempString.Add("Progress: [###########         ] 55%");
		tempString.Add("Progress: [############        ] 60%");
		tempString.Add("Progress: [#############       ] 65%");
		tempString.Add("Progress: [##############      ] 70%");
		tempString.Add("Progress: [###############     ] 75%");
		tempString.Add("Progress: [################    ] 80%");
		tempString.Add("Progress: [#################   ] 85%");
		tempString.Add("Progress: [##################  ] 90%");
		tempString.Add("Progress: [################### ] 95%");
		tempString.Add("Progress: [####################] 100%");
		for (int i = 0; i < tempString.Count; i = num + 1)
		{
			this.antiVirusText.text = previusText + "\n" + tempString[i];
			yield return new WaitForSeconds(0.4f);
			num = i;
		}
		this.antiVirusText.DOText("\n\n", this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x000D9BB4 File Offset: 0x000D7DB4
	public void SettingDebug()
	{
		this.PassDebug = true;
		this.InputField.text = this.DebugPassword;
		if (this.DebugPassword.Length < 3)
		{
			this.InputField.text = "****";
		}
		this.toggle.isOn = true;
		this.transparentPanel.SetActive(true);
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x0001BA1E File Offset: 0x00019C1E
	public void ResetDebugSetting()
	{
		this.PassDebug = false;
		this.InputField.text = "";
		this.toggle.isOn = false;
		this.transparentPanel.SetActive(false);
		base.StopAllCoroutines();
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0001BA73 File Offset: 0x00019C73
	[CompilerGenerated]
	internal static void <DeepScanRoutine>g__LockIcon|22_0(Icon icon)
	{
		SingletoneBehaviour<IconManager>.Instance.ForceRemoveWindowInfo(icon, false);
		SingletoneBehaviour<IconManager>.Instance.SetIconLock(icon, false);
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0001BA8D File Offset: 0x00019C8D
	[CompilerGenerated]
	internal static void <FindVirus>g__CloseFolder|23_0(Icon icon)
	{
		SingletoneBehaviour<IconManager>.Instance.ForceRemoveWindowInfo(icon, false);
		SingletoneBehaviour<IconManager>.Instance.CloseFolder(icon, false);
	}

	// Token: 0x04001C5F RID: 7263
	public string FixPassword;

	// Token: 0x04001C60 RID: 7264
	[HideInInspector]
	public bool CheckFixIsVirus;

	// Token: 0x04001C61 RID: 7265
	public string DebugPassword;

	// Token: 0x04001C62 RID: 7266
	[HideInInspector]
	public bool CheckDebugIsVirus;

	// Token: 0x04001C63 RID: 7267
	public GameObject VaccineIcon;

	// Token: 0x04001C64 RID: 7268
	public TMP_InputField InputField;

	// Token: 0x04001C65 RID: 7269
	public TextMeshProUGUI resultText;

	// Token: 0x04001C66 RID: 7270
	public bool isDeepScan;

	// Token: 0x04001C67 RID: 7271
	public TextMeshProUGUI antiVirusText;

	// Token: 0x04001C68 RID: 7272
	public float speed = 300f;

	// Token: 0x04001C69 RID: 7273
	public Button scanButton;

	// Token: 0x04001C6A RID: 7274
	[TextArea(10, 10)]
	public string clearText;

	// Token: 0x04001C6B RID: 7275
	[TextArea(10, 10)]
	public List<string> virusScanText = new List<string>();

	// Token: 0x04001C6C RID: 7276
	public VideoPlayer FixKillVideo;

	// Token: 0x04001C6D RID: 7277
	public Toggle toggle;

	// Token: 0x04001C6E RID: 7278
	public GameObject transparentPanel;

	// Token: 0x04001C6F RID: 7279
	public bool KillDebug;

	// Token: 0x04001C70 RID: 7280
	public bool EventDialogue;

	// Token: 0x04001C71 RID: 7281
	public bool PassDebug;
}
