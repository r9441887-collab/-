using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class OpenGallery : IconDoubleClick
{
	// Token: 0x06001D95 RID: 7573 RVA: 0x000D7014 File Offset: 0x000D5214
	public override void OpenIcon()
	{
		if (this.OpenRealFolder)
		{
			SingletoneBehaviour<GalleryManager>.Instance.CheckImages();
			string persistentDataPath = Application.persistentDataPath;
			if (Directory.Exists(persistentDataPath))
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = persistentDataPath,
					UseShellExecute = true
				});
				return;
			}
		}
		else
		{
			GameObject targetWindow = this.targetWindow;
			if (targetWindow == null)
			{
				return;
			}
			targetWindow.SetActive(true);
		}
	}

	// Token: 0x04001B93 RID: 7059
	public bool OpenRealFolder;
}
