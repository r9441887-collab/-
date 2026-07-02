using System;

// Token: 0x020003FB RID: 1019
public class OpenImage : IconDoubleClick
{
	// Token: 0x06001D97 RID: 7575 RVA: 0x000D706C File Offset: 0x000D526C
	public override void OpenIcon()
	{
		if (!this.canOpen)
		{
			SystemBox.Instance.Show(new MessageConfig(DBManager.instance.GetSettingString("메세지박스", 0, 0, 0), DBManager.instance.GetSettingString("메세지박스", 0, 1, 0), 650, 300), SystemBox.MessageType.Error, false, 4f, false, true);
			return;
		}
		this.data = base.GetComponent<ImageData>();
		SingletoneBehaviour<GalleryManager>.Instance.SetImageIndex(this.data.index);
		this.targetWindow.SetActive(true);
		SingletoneBehaviour<MouseRaycast>.Instance.SetFirstLayer(this.targetWindow);
	}

	// Token: 0x04001B94 RID: 7060
	public bool canOpen = true;

	// Token: 0x04001B95 RID: 7061
	private ImageData data;
}
