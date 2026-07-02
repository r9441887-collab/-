using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class MemoryRecovery : SingletoneBehaviour<MemoryRecovery>
{
	// Token: 0x06001D6C RID: 7532 RVA: 0x0001B27D File Offset: 0x0001947D
	private void StartMemoryRecover()
	{
		if (this.recoverTextRoutine != null)
		{
			base.StopCoroutine(this.recoverTextRoutine);
		}
		this.recoverTextRoutine = base.StartCoroutine("RecoverTexting");
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x0001B2A4 File Offset: 0x000194A4
	private IEnumerator RecoverTexting()
	{
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.RecoverStart, false, 0.5f, 0.3f);
		this.RecoverWindow.SetActive(true);
		yield return new WaitForSeconds(2f);
		AudioSource machineSound = SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.RecoveringSound, false, 0f, 1f);
		TweenCallback<float> <>9__4;
		TweenCallback <>9__3;
		DOVirtual.Float(0f, 1f, 0.5f, delegate(float f)
		{
			machineSound.volume = f;
		}).OnComplete(delegate
		{
			Tweener tweener = DOVirtual.Float(0f, 1f, 2f, delegate(float w)
			{
			});
			TweenCallback tweenCallback;
			if ((tweenCallback = <>9__3) == null)
			{
				tweenCallback = (<>9__3 = delegate
				{
					float num = 1f;
					float num2 = 0f;
					float num3 = 0.5f;
					TweenCallback<float> tweenCallback2;
					if ((tweenCallback2 = <>9__4) == null)
					{
						tweenCallback2 = (<>9__4 = delegate(float g)
						{
							machineSound.volume = g;
						});
					}
					DOVirtual.Float(num, num2, num3, tweenCallback2);
				});
			}
			tweener.OnComplete(tweenCallback);
		});
		SingletoneBehaviour<LoadingBar>.Instance.SetPosition(this.LoadingPosition.localPosition);
		SingletoneBehaviour<LoadingBar>.Instance.PlayLoadingWithInterrupt(0.3f);
		for (;;)
		{
			this.RecoverText.text = "Recovering.";
			yield return new WaitForSeconds(0.5f);
			this.RecoverText.text = "Recovering..";
			yield return new WaitForSeconds(0.5f);
			this.RecoverText.text = "Recovering...";
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x0001B2B3 File Offset: 0x000194B3
	public void SetPastTime()
	{
		base.StartCoroutine("RecoveryRoutine");
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x0001B2C1 File Offset: 0x000194C1
	private IEnumerator RecoveryRoutine()
	{
		this.StartMemoryRecover();
		SingletoneBehaviour<LoadingBar>.Instance.interruptAction = delegate
		{
			Action action = delegate
			{
				this.PlayingMemory = true;
				this.RecoverWindow.SetActive(false);
				SingletoneBehaviour<LoadingBar>.Instance.LoadingWindow.SetActive(false);
				SingletoneBehaviour<LoadingBar>.Instance.loadingSpeed = 10f;
				Action memoryRecoveryStartAction = this.MemoryRecoveryStartAction;
				if (memoryRecoveryStartAction != null)
				{
					memoryRecoveryStartAction();
				}
				Action action2 = delegate
				{
				};
				SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1f, 2f, action2, null);
				Action memoryPlayAction = this.MemoryPlayAction;
				if (memoryPlayAction == null)
				{
					return;
				}
				memoryPlayAction();
			};
			SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1f, 0f, action, null, 1f);
		};
		yield return null;
		yield break;
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x0001B2D0 File Offset: 0x000194D0
	public void SetPresentTime(float delay = 0f)
	{
		base.StartCoroutine(this.FadeOutToPresent(delay));
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x0001B2E0 File Offset: 0x000194E0
	private IEnumerator FadeOutToPresent(float delay)
	{
		if (!SingletoneBehaviour<FadeInAndOut>.Instance.isBlack)
		{
			SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(1f, 0f, null, null, 1f);
			yield return new WaitUntil(() => SingletoneBehaviour<FadeInAndOut>.Instance.isBlack);
		}
		this.StartMemoryRecover();
		SingletoneBehaviour<LoadingBar>.Instance.PlayLoadingWithInterrupt(10f);
		SingletoneBehaviour<LoadingBar>.Instance.EndMemoryRoutine();
		yield return new WaitForSeconds(delay);
		Action memoryRecoveryEndAction = this.MemoryRecoveryEndAction;
		if (memoryRecoveryEndAction != null)
		{
			memoryRecoveryEndAction();
		}
		yield return new WaitForSeconds(1f);
		yield return new WaitUntil(() => SingletoneBehaviour<LoadingBar>.Instance.ReadyLoadingEnd);
		Action action = delegate
		{
			SingletoneBehaviour<LoadingBar>.Instance.interrupt = false;
		};
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(1f, 1f, action, null);
		yield return new WaitUntil(() => !SingletoneBehaviour<FadeInAndOut>.Instance.isBlack);
		SingletoneBehaviour<LoadingBar>.Instance.loadingSpeed = 1f;
		SingletoneBehaviour<LoadingBar>.Instance.LoadingActionEnd = true;
		yield return new WaitUntil(() => SingletoneBehaviour<LoadingBar>.Instance.GetLoadingEnd());
		Action memoryPlayAction = this.MemoryPlayAction;
		if (memoryPlayAction != null)
		{
			memoryPlayAction();
		}
		this.RecoverWindow.SetActive(false);
		this.MemoryRecoveryEnd = true;
		yield break;
	}

	// Token: 0x04001B74 RID: 7028
	public TextMeshProUGUI RecoverText;

	// Token: 0x04001B75 RID: 7029
	private Coroutine recoverTextRoutine;

	// Token: 0x04001B76 RID: 7030
	public RectTransform LoadingPosition;

	// Token: 0x04001B77 RID: 7031
	public GameObject RecoverWindow;

	// Token: 0x04001B78 RID: 7032
	public bool PlayingMemory;

	// Token: 0x04001B79 RID: 7033
	public Action MemoryRecoveryStartAction;

	// Token: 0x04001B7A RID: 7034
	public Action MemoryRecoveryEndAction;

	// Token: 0x04001B7B RID: 7035
	public Action MemoryPlayAction;

	// Token: 0x04001B7C RID: 7036
	public bool MemoryRecoveryEnd;
}
