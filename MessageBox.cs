using System;
using TMPro;
using UnityEngine;

// Token: 0x020003AD RID: 941
public class MessageBox : UIWindow
{
	// Token: 0x06001BF0 RID: 7152 RVA: 0x0001A25E File Offset: 0x0001845E
	private void Update()
	{
		if (this.CloseAuto)
		{
			this.timer += Time.deltaTime;
			if (this.timer >= this.removeTime)
			{
				base.DestroyBox(false, false);
			}
		}
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x0001A290 File Offset: 0x00018490
	public void SetWindowData(string str1, string str2)
	{
		this.titleTMP.text = str1;
		this.contentTMP.text = str2;
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x0001A2AA File Offset: 0x000184AA
	public void SetTimer(bool _closeAuto = false, float _removeTime = 4f)
	{
		this.CloseAuto = _closeAuto;
		this.removeTime = _removeTime;
		this.timer = 0f;
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x0001A2C5 File Offset: 0x000184C5
	public override void CallbackEnableEnd()
	{
		base.CallbackEnableEnd();
		this.contentTMP.enabled = true;
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x0001A2D9 File Offset: 0x000184D9
	public override void CallbackDisableEnd()
	{
		if (this.RemoveObject)
		{
			SystemBox.Instance.RemoveWindow();
			this.setDisable = false;
		}
		base.CallbackDisableEnd();
	}

	// Token: 0x0400194D RID: 6477
	public bool RemoveObject = true;

	// Token: 0x0400194E RID: 6478
	public bool CloseAuto;

	// Token: 0x0400194F RID: 6479
	public float removeTime = 3f;

	// Token: 0x04001950 RID: 6480
	public float timer;

	// Token: 0x04001951 RID: 6481
	public TextMeshProUGUI titleTMP;

	// Token: 0x04001952 RID: 6482
	public TextMeshProUGUI contentTMP;
}
