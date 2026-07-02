using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000161 RID: 353
public class MusicSelector : MonoBehaviour
{
	// Token: 0x0600083E RID: 2110 RVA: 0x00042AE0 File Offset: 0x00040CE0
	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			MusicInfo musicInfo = child.GetComponent<MusicInfo>();
			if (!(musicInfo == null))
			{
				string text = string.Concat(new string[]
				{
					" ",
					(i + 1).ToString(),
					". ",
					musicInfo.titleText.text,
					"\t"
				});
				musicInfo.titleText.text = text;
				Button component = musicInfo.GetComponent<Button>();
				Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
				buttonClickedEvent.AddListener(delegate
				{
					this.SelectMusic(musicInfo);
				});
				component.onClick = buttonClickedEvent;
			}
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00042BC4 File Offset: 0x00040DC4
	private void SelectMusic(MusicInfo musicInfo)
	{
		SingletoneBehaviour<MusicPlayerNameFlow>.Instance.ResetTween();
		if (this.selectedMusicInfo != null)
		{
			this.selectedMusicInfo.GetComponent<Image>().color = this._unSelectedColor;
		}
		this.selectedMusicInfo = musicInfo;
		this.selectedMusicInfo.GetComponent<Image>().color = this._SelectedColor;
		this.SelectedText.text = this.selectedMusicInfo.titleText.text;
	}

	// Token: 0x04000919 RID: 2329
	public List<MusicInfo> MusicInfos = new List<MusicInfo>();

	// Token: 0x0400091A RID: 2330
	[SerializeField]
	private TextMeshProUGUI SelectedText;

	// Token: 0x0400091B RID: 2331
	[SerializeField]
	private MusicInfo selectedMusicInfo;

	// Token: 0x0400091C RID: 2332
	[SerializeField]
	private Color _SelectedColor;

	// Token: 0x0400091D RID: 2333
	[SerializeField]
	private Color _unSelectedColor;
}
