using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200041D RID: 1053
public class TrashCanObject : MonoBehaviour
{
	// Token: 0x06001E39 RID: 7737 RVA: 0x0001B90F File Offset: 0x00019B0F
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x000D9A88 File Offset: 0x000D7C88
	private void Update()
	{
		this.xPos = this.rectTransform.anchoredPosition.x;
		if (this.xPos > 700f)
		{
			this.rectTransform.anchoredPosition = new Vector2(-700f, -180f);
		}
		if (this.xPos < 0f)
		{
			this.image.fillOrigin = 1;
			this.image.fillAmount = (this.xPos + 650f) / 100f;
		}
		else if (this.xPos > 0f)
		{
			this.image.fillOrigin = 0;
			this.image.fillAmount = (650f - this.xPos) / 100f;
		}
		base.transform.Translate(Vector3.right * Time.deltaTime * this.speed);
	}

	// Token: 0x04001C5A RID: 7258
	public float speed = 10f;

	// Token: 0x04001C5B RID: 7259
	private float xPos;

	// Token: 0x04001C5C RID: 7260
	private RectTransform rectTransform;

	// Token: 0x04001C5D RID: 7261
	private Image image;
}
