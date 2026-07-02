using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class CirclePurse : PurseByBeat
{
	// Token: 0x06000828 RID: 2088 RVA: 0x00042850 File Offset: 0x00040A50
	public override void Pulse()
	{
		if (this.removeNextPurse)
		{
			base.RemoveSelf(false);
			return;
		}
		if (this.tween != null && this.tween.IsActive() && !this.tween.IsComplete())
		{
			this.tween.Kill(false);
			this.tween = null;
			base.transform.localScale = this._startSize;
		}
		this.tween = ShortcutExtensions.DOScale(base.transform, 3f, 1.5f).SetEase(Ease.Linear).OnComplete(delegate
		{
			base.transform.localScale = this._startSize;
		});
	}
}
