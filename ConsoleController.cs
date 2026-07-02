using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class ConsoleController : SingletoneBehaviour<ConsoleController>
{
	// Token: 0x06000964 RID: 2404 RVA: 0x00014052 File Offset: 0x00012252
	private void Start()
	{
		DOVirtual.Int(0, 2, this.underBarDuration, delegate(int value)
		{
			this.underBarStatus = value <= 1;
		}).SetLoops(-1, LoopType.Yoyo);
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x000489B4 File Offset: 0x00046BB4
	private void Update()
	{
		if (!this.isWriting)
		{
			if (this.underBarStatus)
			{
				this.consoleText.text = this.beforeString + "_";
			}
			else
			{
				this.consoleText.text = this.beforeString;
			}
			if (this.ConsoleWindow.activeSelf)
			{
				this.timer += Time.deltaTime;
				if (this.timer >= this.timeLimit)
				{
					this.IsConsoleOn = false;
					this.timer = 0f;
					this.consoleText.text = this.beforeString;
					this.ConsoleWindow.GetComponent<UIWindow>().DestroyBox(false, false);
					return;
				}
			}
		}
		else
		{
			this.timer = 0f;
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x000117CB File Offset: 0x0000F9CB
	public void ClearConsole()
	{
		base.StartCoroutine("ConsoleClearRoutine");
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00014075 File Offset: 0x00012275
	private IEnumerator ConsoleClearRoutine()
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
		this.AddText("");
		yield break;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00014084 File Offset: 0x00012284
	public void SetPosition(Vector2 position_)
	{
		this.position = position_;
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0001408D File Offset: 0x0001228D
	public void SetConsoleSize(float scale)
	{
		this.ConsoleWindow.transform.localScale = Vector3.one * scale;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x000140AA File Offset: 0x000122AA
	public void ShowConsole(string text)
	{
		this.timer = 0f;
		this.TurnOnConsole();
		this.textQueue.Enqueue(text);
		if (this.AddTextRoutine == null)
		{
			this.AddTextRoutine = base.StartCoroutine("YieldText");
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x000140E2 File Offset: 0x000122E2
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

	// Token: 0x0600096C RID: 2412 RVA: 0x00048A70 File Offset: 0x00046C70
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

	// Token: 0x0600096D RID: 2413 RVA: 0x000140F1 File Offset: 0x000122F1
	public void StopTexting()
	{
		if (this.isWriting && this.tween != null && this.tween.IsPlaying())
		{
			this.tween.Kill(false);
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0001411C File Offset: 0x0001231C
	private void OnDisable()
	{
		this.isWriting = false;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00014125 File Offset: 0x00012325
	public void TurnOnConsole()
	{
		this.IsConsoleOn = true;
		this.ConsoleWindow.SetActive(true);
		this.ConsoleWindow.GetComponent<RectTransform>().localPosition = this.position;
		SingletoneBehaviour<MouseRaycast>.Instance.SetFirstLayer(this.ConsoleWindow);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00048B0C File Offset: 0x00046D0C
	public string randomHex(int returnNum = 5)
	{
		ConsoleController.<>c__DisplayClass29_0 CS$<>8__locals1;
		CS$<>8__locals1.num = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		CS$<>8__locals1.hex = new string[] { "A", "B", "C", "D", "E", "F" };
		string text = "";
		for (int i = 0; i < returnNum; i++)
		{
			text = text + ConsoleController.<randomHex>g__GetText|29_0(ref CS$<>8__locals1) + ConsoleController.<randomHex>g__GetText|29_0(ref CS$<>8__locals1) + " ";
		}
		return text;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00048BE4 File Offset: 0x00046DE4
	public void BlackFade(float duration = 1f, bool fadeOut = true, float targetValue = 1f)
	{
		if (this.LastTween != null)
		{
			this.LastTween.Kill(false);
		}
		if (fadeOut)
		{
			this.blackBackGround.SetActive(true);
			this.blackBackGround_CanvasGroup.alpha = 0f;
			this.LastTween = this.blackBackGround_CanvasGroup.DOFade(targetValue, duration);
			return;
		}
		this.blackBackGround.SetActive(true);
		this.LastTween = this.blackBackGround_CanvasGroup.DOFade(0f, duration).OnComplete(delegate
		{
			this.blackBackGround.SetActive(false);
		});
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00048CC4 File Offset: 0x00046EC4
	[CompilerGenerated]
	internal static string <randomHex>g__GetText|29_0(ref ConsoleController.<>c__DisplayClass29_0 A_0)
	{
		int num = Random.Range(0, 16);
		if (num >= 10)
		{
			num -= 10;
			return A_0.hex[num];
		}
		return A_0.num[num];
	}

	// Token: 0x04000A4F RID: 2639
	public GameObject ConsoleWindow;

	// Token: 0x04000A50 RID: 2640
	public TextMeshProUGUI consoleText;

	// Token: 0x04000A51 RID: 2641
	public string targetString;

	// Token: 0x04000A52 RID: 2642
	public bool isWriting;

	// Token: 0x04000A53 RID: 2643
	public string beforeString = "";

	// Token: 0x04000A54 RID: 2644
	private bool underBarStatus;

	// Token: 0x04000A55 RID: 2645
	public float underBarDuration = 0.5f;

	// Token: 0x04000A56 RID: 2646
	private Tween tween;

	// Token: 0x04000A57 RID: 2647
	[Header("타이핑 속도")]
	public float speed = 35f;

	// Token: 0x04000A58 RID: 2648
	[Header("timeLimit 만큼 대기 후 창이 종료됩니다.")]
	public float timeLimit = 5f;

	// Token: 0x04000A59 RID: 2649
	[SerializeField]
	private float timer;

	// Token: 0x04000A5A RID: 2650
	public bool IsConsoleOn;

	// Token: 0x04000A5B RID: 2651
	public Vector2 position = Vector2.one;

	// Token: 0x04000A5C RID: 2652
	public GameObject blackBackGround;

	// Token: 0x04000A5D RID: 2653
	public CanvasGroup blackBackGround_CanvasGroup;

	// Token: 0x04000A5E RID: 2654
	private Coroutine AddTextRoutine;

	// Token: 0x04000A5F RID: 2655
	private Queue<string> textQueue = new Queue<string>();

	// Token: 0x04000A60 RID: 2656
	public Tween LastTween;
}
