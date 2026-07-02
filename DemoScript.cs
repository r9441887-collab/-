using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class DemoScript : SingletoneBehaviour<DemoScript>
{
	// Token: 0x0600052A RID: 1322 RVA: 0x00034304 File Offset: 0x00032504
	private void Awake()
	{
		if (!this._IsDemo)
		{
			Object.Destroy(this);
			return;
		}
		if (this.passAwake)
		{
			return;
		}
		if (!this.isTitle)
		{
			ChapterSetter component = base.GetComponent<ChapterSetter>();
			int chapter = component.GetChapter();
			Debug.Log(chapter);
			Object.Destroy(component);
			if (chapter == this.EndChapter)
			{
				this.blackPanel.alpha = 1f;
				this.blackPanel.gameObject.SetActive(true);
				this.EndDemoWindow.SetActive(true);
				base.StartCoroutine("TempFadeIn");
				Debug.Log("데모 끝입니다.");
			}
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x000113DD File Offset: 0x0000F5DD
	private IEnumerator TempFadeIn()
	{
		yield return new WaitUntil(() => SingletoneBehaviour<FadeInAndOut>.Instance != null);
		yield return new WaitUntil(() => SingletoneBehaviour<FadeInAndOut>.Instance.isBlack);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(3f, 0f, null, null);
		yield break;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0003439C File Offset: 0x0003259C
	private void Start()
	{
		if (PlayerPrefs.GetInt("FirstDemo", 1) == 1)
		{
			this.SetDemoChapter();
		}
		if (this.isTitle && this._IsDemo)
		{
			if (this.demoReadMe_windowInfo != null)
			{
				this.demoReadMe_windowInfo.SetActive(true);
			}
			if (this.demoReadMe != null)
			{
				this.demoReadMe.SetActive(true);
			}
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00034404 File Offset: 0x00032604
	public void SetDemoChapter()
	{
		if (this.isTitle)
		{
			PlayerPrefs.SetInt("FirstDemo", 0);
			int num = this.StartChapter / 100;
			int num2 = this.StartChapter % 100;
			PlayerPrefs.SetInt("LastChapter", num);
			PlayerPrefs.SetInt("LastEvent", num2);
			DataManager._LastChapter = PlayerPrefs.GetInt("LastChapter", num);
			DataManager._LastEvent = PlayerPrefs.GetInt("LastEvent", num2);
			Events.AutoChapterIndex = num;
			Events.AutoEventIndex = num2;
			Debug.Log("데모 버전 입니다. : " + Events.AutoChapterIndex.ToString() + " - " + Events.AutoEventIndex.ToString());
		}
	}

	// Token: 0x040005B5 RID: 1461
	[Header("데모 빌드입니까?")]
	public bool _IsDemo;

	// Token: 0x040005B6 RID: 1462
	public bool passAwake;

	// Token: 0x040005B7 RID: 1463
	public bool isTitle;

	// Token: 0x040005B8 RID: 1464
	public int StartChapter = 201;

	// Token: 0x040005B9 RID: 1465
	public int EndChapter = 207;

	// Token: 0x040005BA RID: 1466
	public CanvasGroup blackPanel;

	// Token: 0x040005BB RID: 1467
	public GameObject EndDemoWindow;

	// Token: 0x040005BC RID: 1468
	public GameObject demoReadMe_windowInfo;

	// Token: 0x040005BD RID: 1469
	public GameObject demoReadMe;
}
