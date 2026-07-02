using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000163 RID: 355
public class MusicPlayerNameFlow : SingletoneBehaviour<MusicPlayerNameFlow>
{
	// Token: 0x06000843 RID: 2115 RVA: 0x00042C38 File Offset: 0x00040E38
	private void Awake()
	{
		if (this.tween != null)
		{
			this.tween.Kill(false);
		}
		if (this._scrollRect == null)
		{
			base.GetComponent<ScrollRect>();
		}
		Sequence sequence = DOTween.Sequence();
		sequence.Append(DOVirtual.Float(0f, 1f, this._speed, delegate(float f)
		{
			this._value = f;
			this._scrollRect.horizontalNormalizedPosition = this._value;
		}));
		sequence.SetEase(this.ease);
		sequence.SetDelay(this._delay);
		sequence.SetLoops(-1);
		sequence.Play<Sequence>();
		this.tween = sequence;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x000136A4 File Offset: 0x000118A4
	public void ResetTween()
	{
		this.tween.Restart(true, -1f);
	}

	// Token: 0x04000920 RID: 2336
	[SerializeField]
	private float _speed = 1f;

	// Token: 0x04000921 RID: 2337
	[SerializeField]
	private float _delay = 0.5f;

	// Token: 0x04000922 RID: 2338
	[Range(0f, 1f)]
	[SerializeField]
	private float _value;

	// Token: 0x04000923 RID: 2339
	[SerializeField]
	private ScrollRect _scrollRect;

	// Token: 0x04000924 RID: 2340
	private Tween tween;

	// Token: 0x04000925 RID: 2341
	[SerializeField]
	private Ease ease;
}
