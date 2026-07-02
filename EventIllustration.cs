using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036E RID: 878
public class EventIllustration : MonoBehaviour
{
	// Token: 0x06001A74 RID: 6772 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x0001915D File Offset: 0x0001735D
	public void Setting()
	{
		this.illustrationImage.sprite = this.curIllustrationSprite;
		this.StartEnableRoutine();
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000C3280 File Offset: 0x000C1480
	public void StartEnableRoutine()
	{
		this.isDestroy = false;
		Vector3 lastScale = base.transform.localScale;
		TweenCallback<float> <>9__2;
		TweenCallback <>9__3;
		DOVirtual.Float(lastScale.x, lastScale.x * 1.05f, 0.15f, delegate(float value)
		{
			this.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).OnComplete(delegate
		{
			float num = lastScale.x * 1.05f;
			float x = lastScale.x;
			float num2 = 0.1f;
			TweenCallback<float> tweenCallback;
			if ((tweenCallback = <>9__2) == null)
			{
				tweenCallback = (<>9__2 = delegate(float value)
				{
					this.transform.localScale = Vector3.one * value;
				});
			}
			Tweener tweener = DOVirtual.Float(num, x, num2, tweenCallback).SetEase(Ease.Linear);
			TweenCallback tweenCallback2;
			if ((tweenCallback2 = <>9__3) == null)
			{
				tweenCallback2 = (<>9__3 = delegate
				{
					this.transform.localScale = lastScale;
				});
			}
			tweener.OnComplete(tweenCallback2);
		});
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x000C3300 File Offset: 0x000C1500
	public void DestroyBox()
	{
		if (this.isDestroy)
		{
			return;
		}
		this.isDestroy = true;
		Vector3 _localScale = base.transform.localScale;
		TweenCallback<float> <>9__2;
		TweenCallback <>9__3;
		DOVirtual.Float(_localScale.x, _localScale.x * 1.03f, 0.15f, delegate(float value)
		{
			this.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo)
			.OnComplete(delegate
			{
				float num = _localScale.x * 1.03f;
				float num2 = 0f;
				float num3 = 0.1f;
				TweenCallback<float> tweenCallback;
				if ((tweenCallback = <>9__2) == null)
				{
					tweenCallback = (<>9__2 = delegate(float value)
					{
						this.transform.localScale = Vector3.one * value;
					});
				}
				Tweener tweener = DOVirtual.Float(num, num2, num3, tweenCallback).SetEase(Ease.Linear);
				TweenCallback tweenCallback2;
				if ((tweenCallback2 = <>9__3) == null)
				{
					tweenCallback2 = (<>9__3 = delegate
					{
						this.transform.localScale = Vector3.one;
						this.gameObject.SetActive(false);
						DBManager.instance.dialogueData.curEvent.pressIllustrationBtn = false;
					});
				}
				tweener.OnComplete(tweenCallback2);
			});
	}

	// Token: 0x040016A0 RID: 5792
	private bool isDestroy;

	// Token: 0x040016A1 RID: 5793
	public TMP_Text tittleText;

	// Token: 0x040016A2 RID: 5794
	public Image illustrationImage;

	// Token: 0x040016A3 RID: 5795
	public Sprite curIllustrationSprite;
}
