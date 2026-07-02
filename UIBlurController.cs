using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AA RID: 170
public class UIBlurController : MonoBehaviour
{
	// Token: 0x06000440 RID: 1088 RVA: 0x00010BBB File Offset: 0x0000EDBB
	private void Start()
	{
		this.SetBlurAmount();
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
	public void SetBlurAmount()
	{
		if (this._uiImage == null)
		{
			this._uiImage = base.GetComponent<Image>();
		}
		float aspect = Camera.main.aspect;
		float num = this.BlurAmount;
		float num2 = this.BlurAmount;
		if (aspect > 1f)
		{
			num /= aspect;
		}
		else
		{
			num2 *= aspect;
		}
		this._uiImage.material.SetFloat("_yBlur", num2);
		this._uiImage.material.SetFloat("_xBlur", num);
	}

	// Token: 0x04000487 RID: 1159
	[HideInInspector]
	[Range(0.001f, 0.015f)]
	public float BlurAmount = 0.005f;

	// Token: 0x04000488 RID: 1160
	private Image _uiImage;
}
