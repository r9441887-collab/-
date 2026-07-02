using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000444 RID: 1092
public class TitleBarSetter : MonoBehaviour
{
	// Token: 0x06001F01 RID: 7937 RVA: 0x000DD958 File Offset: 0x000DBB58
	private void Start()
	{
		List<Sprite> sprites = SingletoneBehaviour<ImageGetter>.Instance.TitleSprrites[(int)this.titleBarColor].Sprites;
		this.left.sprite = sprites[0];
		this.leftDot.sprite = sprites[1];
		this.center.sprite = sprites[2];
		this.rightDot.sprite = sprites[3];
		this.right.sprite = sprites[4];
	}

	// Token: 0x04001D5E RID: 7518
	public TitleBarColor titleBarColor;

	// Token: 0x04001D5F RID: 7519
	public Image left;

	// Token: 0x04001D60 RID: 7520
	public Image leftDot;

	// Token: 0x04001D61 RID: 7521
	public Image center;

	// Token: 0x04001D62 RID: 7522
	public Image right;

	// Token: 0x04001D63 RID: 7523
	public Image rightDot;

	// Token: 0x04001D64 RID: 7524
	public TextMeshProUGUI titleText;
}
