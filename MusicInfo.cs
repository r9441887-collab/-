using System;
using TMPro;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class MusicInfo : MonoBehaviour
{
	// Token: 0x0600083C RID: 2108 RVA: 0x000429F0 File Offset: 0x00040BF0
	private void Awake()
	{
		if (this.audioClip != null)
		{
			this.songLength = (int)this.audioClip.length;
			this.songMinute = this.songLength / 60;
			this.songSecond = this.songLength % 60;
			if (this.titleText == null)
			{
				this.titleText = base.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			}
			if (this.lengthText == null)
			{
				this.lengthText = base.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
			}
			this.titleText.text = this.artist + " - " + this.songName + "\t";
			this.lengthText.text = this.songMinute.ToString() + ":" + this.songSecond.ToString("00");
		}
	}

	// Token: 0x04000911 RID: 2321
	public string artist;

	// Token: 0x04000912 RID: 2322
	public string songName;

	// Token: 0x04000913 RID: 2323
	public int songLength;

	// Token: 0x04000914 RID: 2324
	public int songMinute;

	// Token: 0x04000915 RID: 2325
	public int songSecond;

	// Token: 0x04000916 RID: 2326
	public AudioClip audioClip;

	// Token: 0x04000917 RID: 2327
	public TextMeshProUGUI titleText;

	// Token: 0x04000918 RID: 2328
	public TextMeshProUGUI lengthText;
}
