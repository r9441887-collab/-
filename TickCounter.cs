using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class TickCounter : MonoBehaviour
{
	// Token: 0x0600085A RID: 2138 RVA: 0x00013793 File Offset: 0x00011993
	private void Awake()
	{
		this.defaultTick = this.tickCount;
		this.defaultGroup = this.groupCount;
		this.defaultLine = this.lineCount;
		this.defaultLPT = this.linePerTick;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x000137C5 File Offset: 0x000119C5
	public void ResetCounter()
	{
		this.tickCount = this.defaultTick;
		this.groupCount = this.defaultGroup;
		this.lineCount = this.defaultLine;
		this.linePerTick = this.defaultLPT;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x000430A8 File Offset: 0x000412A8
	public void CalculateTick()
	{
		this.tickCount++;
		if (this.tickCount == 1)
		{
			this.firstTick.Play();
			return;
		}
		if (this.tickCount == this.linePerTick)
		{
			this.laskTick.Play();
			this.tickCount = 0;
			this.groupCount++;
			if (this.groupCount == 4)
			{
				this.lineCount++;
				this.groupCount = 0;
				return;
			}
		}
		else
		{
			this.mainTick.Play();
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000137F7 File Offset: 0x000119F7
	private void OnDisable()
	{
		this.ResetCounter();
	}

	// Token: 0x0400093F RID: 2367
	[SerializeField]
	private AudioSource firstTick;

	// Token: 0x04000940 RID: 2368
	[SerializeField]
	private AudioSource mainTick;

	// Token: 0x04000941 RID: 2369
	[SerializeField]
	private AudioSource laskTick;

	// Token: 0x04000942 RID: 2370
	[SerializeField]
	private int tickCount;

	// Token: 0x04000943 RID: 2371
	[SerializeField]
	private int groupCount;

	// Token: 0x04000944 RID: 2372
	[SerializeField]
	private int lineCount;

	// Token: 0x04000945 RID: 2373
	[SerializeField]
	private int linePerTick = 12;

	// Token: 0x04000946 RID: 2374
	private int defaultTick;

	// Token: 0x04000947 RID: 2375
	private int defaultGroup;

	// Token: 0x04000948 RID: 2376
	private int defaultLine;

	// Token: 0x04000949 RID: 2377
	private int defaultLPT;
}
