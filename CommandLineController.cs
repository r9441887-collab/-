using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class CommandLineController : SingletoneBehaviour<CommandLineController>
{
	// Token: 0x06000596 RID: 1430 RVA: 0x000117C3 File Offset: 0x0000F9C3
	private void Start()
	{
		Cursor.lockState = 1;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0003552C File Offset: 0x0003372C
	private void Update()
	{
		if (this.consoleText.textInfo.lineCount <= 1 && this.isFull)
		{
			this.isFull = false;
		}
		if (this.canExitByESC && !HorrorSetting.isPlayingVideo && !DBManager.instance.dialogueData.NoBacklogOpen && Input.GetKeyDown(27))
		{
			if (HorrorSetting.canPlayVideo)
			{
				HorrorSetting.canPlayVideo = false;
				SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
				return;
			}
			if (SingletoneBehaviour<PopUpMessage>.Instance.LastMessage != null)
			{
				SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
				return;
			}
			if (OpenMyPC.CanClose)
			{
				this.ClosePC();
				OpenMyPC.CanClose = false;
			}
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000355CC File Offset: 0x000337CC
	public void ClosePC()
	{
		Action closePC = DBManager.instance.dialogueData.curEvent.closePC;
		if (closePC != null)
		{
			closePC();
		}
		SoundManager.instance.BGM_ChangeVolume_Tween(1f, OpenMyPC.originVolume, false);
		SoundManager.instance.bgmPlayer.pitch = OpenMyPC.originPitch;
		this.screenListener.enabled = true;
		this.worldListener.enabled = false;
		Cursor.lockState = 0;
		base.GetComponent<UIWindow>().DestroyBox(false, false);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000117CB File Offset: 0x0000F9CB
	public void ClearConsole()
	{
		base.StartCoroutine("ConsoleClearRoutine");
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x000117D9 File Offset: 0x0000F9D9
	public IEnumerator ConsoleClearRoutine()
	{
		while (this.isWriting)
		{
			yield return null;
		}
		if (this.tween != null && this.tween.IsPlaying())
		{
			this.tween.Kill(false);
		}
		Tween tween = this.AddText("Clear \n>");
		yield return TweenExtensions.WaitForCompletion(tween);
		this.consoleText.text = "";
		this.beforeString = "";
		this.ShowConsole("");
		yield break;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000117E8 File Offset: 0x0000F9E8
	private void OnEnable()
	{
		this.FirstViewCamera.SetActive(true);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000117F6 File Offset: 0x0000F9F6
	public void ShowConsole(string text)
	{
		this.textQueue.Enqueue(text);
		if (this.AddTextRoutine == null)
		{
			this.AddTextRoutine = base.StartCoroutine("YieldText");
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001181D File Offset: 0x0000FA1D
	private IEnumerator YieldText()
	{
		while (this.textQueue.Any<string>())
		{
			while (this.isWriting)
			{
				yield return null;
			}
			if (this.textQueue.Any<string>())
			{
				string text = this.textQueue.Dequeue();
				Tween tween = this.AddText(text);
				yield return TweenExtensions.WaitForCompletion(tween);
			}
			this.AddTextRoutine = null;
		}
		yield break;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0003564C File Offset: 0x0003384C
	public Tween AddText(string text)
	{
		if (this.isWriting && this.tween != null && this.tween.IsPlaying())
		{
			this.tween.Kill(false);
		}
		this.consoleText.text = this.beforeString;
		this.isWriting = true;
		text = "\n> " + text;
		TweenerCore<string, string, StringOptions> tweenerCore = this.consoleText.DOText(text, this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>().SetEase(Ease.Linear)
			.SetRelative(true);
		tweenerCore.onComplete = delegate
		{
			this.isWriting = false;
			this.beforeString = this.consoleText.text;
		};
		this.tween = tweenerCore;
		return tweenerCore;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001182C File Offset: 0x0000FA2C
	public void StopTexting()
	{
		if (this.isWriting && this.tween != null && this.tween.IsPlaying())
		{
			this.tween.Kill(false);
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00011857 File Offset: 0x0000FA57
	private void OnDisable()
	{
		this.isWriting = false;
		if (this.FirstViewCamera != null && this.FirstViewCamera.activeSelf)
		{
			this.FirstViewCamera.SetActive(false);
		}
	}

	// Token: 0x0400060F RID: 1551
	public bool canExitByESC;

	// Token: 0x04000610 RID: 1552
	public AudioListener screenListener;

	// Token: 0x04000611 RID: 1553
	public AudioListener worldListener;

	// Token: 0x04000612 RID: 1554
	public GameObject FirstViewCamera;

	// Token: 0x04000613 RID: 1555
	public TextMeshProUGUI consoleText;

	// Token: 0x04000614 RID: 1556
	public float speed = 1f;

	// Token: 0x04000615 RID: 1557
	public string targetString;

	// Token: 0x04000616 RID: 1558
	public bool isWriting;

	// Token: 0x04000617 RID: 1559
	public string beforeString = "";

	// Token: 0x04000618 RID: 1560
	public bool underBarStatus;

	// Token: 0x04000619 RID: 1561
	public float underBarDuration = 0.5f;

	// Token: 0x0400061A RID: 1562
	private Tween tween;

	// Token: 0x0400061B RID: 1563
	public bool isFull;

	// Token: 0x0400061C RID: 1564
	private Coroutine AddTextRoutine;

	// Token: 0x0400061D RID: 1565
	private Queue<string> textQueue = new Queue<string>();
}
