using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class DropTo : UIPositionMove
{
	// Token: 0x0600082E RID: 2094 RVA: 0x00013573 File Offset: 0x00011773
	private void OnEnable()
	{
		if (this._rectTransform == null)
		{
			this._rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0001358F File Offset: 0x0001178F
	private void OnDestroy()
	{
		SingletoneBehaviour<TrashCanForMiniGame>.Instance.RemoveFile(null, true);
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0001359D File Offset: 0x0001179D
	public override void GetMouseDownPosition()
	{
		if (this.firstClick)
		{
			this.startPosition = this._rectTransform.localPosition;
			this.firstClick = false;
		}
		base.GetMouseDownPosition();
		SingletoneBehaviour<TrashCanForMiniGame>.Instance.targetRect = this._rectTransform;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x000135D5 File Offset: 0x000117D5
	public override void GetMouseUpPosition()
	{
		base.GetMouseUpPosition();
		SingletoneBehaviour<TrashCanForMiniGame>.Instance.CheckRemove();
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x000135E7 File Offset: 0x000117E7
	public void SetOriginPosition()
	{
		this._rectTransform.localPosition = this.startPosition;
	}

	// Token: 0x04000904 RID: 2308
	public Vector3 startPosition;

	// Token: 0x04000905 RID: 2309
	private RectTransform _rectTransform;

	// Token: 0x04000906 RID: 2310
	private bool firstClick = true;
}
