using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000430 RID: 1072
public class WinionRoomMark : MonoBehaviour
{
	// Token: 0x06001EA1 RID: 7841 RVA: 0x0001BC8F File Offset: 0x00019E8F
	private void Start()
	{
		this.markImg = base.gameObject.GetComponent<Image>();
		this.defalutAlpha = this.markImg.color.a;
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x000DC0C8 File Offset: 0x000DA2C8
	private void Update()
	{
		if (SingletoneBehaviour<IconManager>.Instance.IsWindowActive(this.winionFolderType) && !this.changeAlpha)
		{
			this.changeAlpha = true;
			Color color = this.markImg.color;
			color.a = 0f;
			this.markImg.color = color;
			return;
		}
		if (!SingletoneBehaviour<IconManager>.Instance.IsWindowActive(this.winionFolderType) && this.changeAlpha)
		{
			this.changeAlpha = false;
			Color color2 = this.markImg.color;
			color2.a = this.defalutAlpha;
			this.markImg.color = color2;
		}
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x000DC164 File Offset: 0x000DA364
	public void SetActiveFalse()
	{
		this.changeAlpha = false;
		Color color = this.markImg.color;
		color.a = this.defalutAlpha;
		this.markImg.color = color;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001CCD RID: 7373
	public Image markImg;

	// Token: 0x04001CCE RID: 7374
	public float defalutAlpha;

	// Token: 0x04001CCF RID: 7375
	public Icon winionFolderType;

	// Token: 0x04001CD0 RID: 7376
	private bool changeAlpha;
}
