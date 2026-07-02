using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class Events : SingletoneBehaviour<Events>
{
	// Token: 0x06001A92 RID: 6802 RVA: 0x000C39C8 File Offset: 0x000C1BC8
	public void Start()
	{
		this.DebugNextDay();
		if (Events.StartAutoEvent)
		{
			switch (Events.AutoChapterIndex)
			{
			case 0:
				this.event_chapter00[Events.AutoEventIndex].gameObject.SetActive(true);
				break;
			case 1:
				this.event_chapter01[Events.AutoEventIndex].gameObject.SetActive(true);
				break;
			case 2:
				this.event_chapter02[Events.AutoEventIndex].gameObject.SetActive(true);
				break;
			case 3:
				this.event_chapter03[Events.AutoEventIndex].gameObject.SetActive(true);
				break;
			}
			Events.StartAutoEvent = false;
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x00019267 File Offset: 0x00017467
	public void DebugNextDay()
	{
		SingletoneBehaviour<WinionCalender>.Instance.fadeAction = delegate
		{
			this.StartEvent(this.DebugEventIndex);
			SingletoneBehaviour<WinionCalender>.Instance.fadeActionEnd = true;
			SingletoneBehaviour<WinionCalender>.Instance.fadeAction = null;
		};
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000C3A7C File Offset: 0x000C1C7C
	public void StartEvent(int eventIndex)
	{
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00)
		{
			if (eventIndex >= this.event_chapter00.Count)
			{
				for (int i = 0; i < this.event_chapter00.Count; i++)
				{
					if (this.event_chapter00[i].gameObject.activeSelf)
					{
						this.event_chapter00[i].gameObject.SetActive(false);
					}
				}
				for (int j = 0; j < this.event_chapter01.Count; j++)
				{
					if (j == 0)
					{
						this.event_chapter01[0].gameObject.SetActive(true);
						this.event_chapter01[0].awake = false;
						this.event_chapter01[0].Init();
					}
					else if (this.event_chapter01[j].gameObject.activeSelf)
					{
						this.event_chapter01[j].gameObject.SetActive(false);
					}
				}
				return;
			}
			for (int k = 0; k < this.event_chapter00.Count; k++)
			{
				if (k == eventIndex)
				{
					this.event_chapter00[eventIndex].gameObject.SetActive(true);
					this.event_chapter00[eventIndex].awake = false;
					this.event_chapter00[eventIndex].Init();
				}
				else if (this.event_chapter00[k].gameObject.activeSelf)
				{
					this.event_chapter00[k].gameObject.SetActive(false);
				}
			}
			return;
		}
		else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
		{
			if (eventIndex >= this.event_chapter01.Count)
			{
				for (int l = 0; l < this.event_chapter01.Count; l++)
				{
					if (this.event_chapter01[l].gameObject.activeSelf)
					{
						this.event_chapter01[l].gameObject.SetActive(false);
					}
				}
				for (int m = 0; m < this.event_chapter02.Count; m++)
				{
					if (m == 0)
					{
						this.event_chapter02[0].gameObject.SetActive(true);
						this.event_chapter02[0].awake = false;
						this.event_chapter02[0].Init();
					}
					else if (this.event_chapter02[m].gameObject.activeSelf)
					{
						this.event_chapter02[m].gameObject.SetActive(false);
					}
				}
				return;
			}
			for (int n = 0; n < this.event_chapter01.Count; n++)
			{
				if (n == eventIndex)
				{
					this.event_chapter01[eventIndex].gameObject.SetActive(true);
					this.event_chapter01[eventIndex].awake = false;
					this.event_chapter01[eventIndex].Init();
				}
				else if (this.event_chapter01[n].gameObject.activeSelf)
				{
					this.event_chapter01[n].gameObject.SetActive(false);
				}
			}
			return;
		}
		else
		{
			if (GameManager.instance.gameData.curChapter != GameManager.Chapter.chapter02)
			{
				if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03)
				{
					for (int num = 0; num < this.event_chapter03.Count; num++)
					{
						if (num == eventIndex)
						{
							this.event_chapter03[eventIndex].gameObject.SetActive(true);
							this.event_chapter03[eventIndex].awake = false;
							this.event_chapter03[eventIndex].Init();
						}
						else if (this.event_chapter03[num].gameObject.activeSelf)
						{
							this.event_chapter03[num].gameObject.SetActive(false);
						}
					}
				}
				return;
			}
			if (eventIndex >= this.event_chapter02.Count)
			{
				for (int num2 = 0; num2 < this.event_chapter02.Count; num2++)
				{
					if (this.event_chapter02[num2].gameObject.activeSelf)
					{
						this.event_chapter02[num2].gameObject.SetActive(false);
					}
				}
				for (int num3 = 0; num3 < this.event_chapter03.Count; num3++)
				{
					if (num3 == 0)
					{
						this.event_chapter03[0].gameObject.SetActive(true);
						this.event_chapter03[0].awake = false;
						this.event_chapter03[0].Init();
					}
					else if (this.event_chapter03[num3].gameObject.activeSelf)
					{
						this.event_chapter03[num3].gameObject.SetActive(false);
					}
				}
				return;
			}
			for (int num4 = 0; num4 < this.event_chapter02.Count; num4++)
			{
				if (num4 == eventIndex)
				{
					this.event_chapter02[eventIndex].gameObject.SetActive(true);
					this.event_chapter02[eventIndex].awake = false;
					this.event_chapter02[eventIndex].Init();
				}
				else if (this.event_chapter02[num4].gameObject.activeSelf)
				{
					this.event_chapter02[num4].gameObject.SetActive(false);
				}
			}
			return;
		}
	}

	// Token: 0x040016C6 RID: 5830
	[Header("챕터 0")]
	public List<EventBase> event_chapter00;

	// Token: 0x040016C7 RID: 5831
	[Space]
	[Header("챕터 1")]
	public List<EventBase> event_chapter01;

	// Token: 0x040016C8 RID: 5832
	[Space]
	[Header("챕터 2")]
	public List<EventBase> event_chapter02;

	// Token: 0x040016C9 RID: 5833
	[Space]
	[Header("챕터 3")]
	public List<EventBase> event_chapter03;

	// Token: 0x040016CA RID: 5834
	public int DebugEventIndex;

	// Token: 0x040016CB RID: 5835
	public static bool StartAutoEvent;

	// Token: 0x040016CC RID: 5836
	public static int AutoChapterIndex;

	// Token: 0x040016CD RID: 5837
	public static int AutoEventIndex;
}
