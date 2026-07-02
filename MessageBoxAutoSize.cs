using System;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class MessageBoxAutoSize : MonoBehaviour
{
	// Token: 0x06001BF6 RID: 7158 RVA: 0x000CFCA4 File Offset: 0x000CDEA4
	public void ChangedText()
	{
		if (this.textRect != null)
		{
			this.width = (float)((int)this.textRect.rect.width);
			this.height = (float)((int)this.textRect.rect.height);
			this.mainWidth = this.width + (float)this.widthDiff;
			this.mainWidth = Mathf.Max(500f, this.mainWidth);
			this.mainHeight = this.height + (float)this.heightDiff;
			this.mainHeight = Mathf.Max(300f, this.mainHeight);
			Vector2 sizeDelta = this.mainRect.sizeDelta;
			sizeDelta.x = this.mainWidth;
			sizeDelta.y = this.mainHeight;
			this.mainRect.sizeDelta = sizeDelta;
		}
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x0001A314 File Offset: 0x00018514
	private void Update()
	{
		this.ChangedText();
	}

	// Token: 0x04001953 RID: 6483
	public RectTransform textRect;

	// Token: 0x04001954 RID: 6484
	public RectTransform mainRect;

	// Token: 0x04001955 RID: 6485
	private float width;

	// Token: 0x04001956 RID: 6486
	private float height;

	// Token: 0x04001957 RID: 6487
	private float mainWidth;

	// Token: 0x04001958 RID: 6488
	private float mainHeight;

	// Token: 0x04001959 RID: 6489
	public int widthDiff = 100;

	// Token: 0x0400195A RID: 6490
	public int heightDiff = 160;
}
