using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class Clock : MonoBehaviour
{
	// Token: 0x0600042B RID: 1067 RVA: 0x00010AC4 File Offset: 0x0000ECC4
	private void Start()
	{
		if (this.randomTime)
		{
			this.hour = (float)Random.Range(0, 12);
			this.minutes = (float)Random.Range(0, 60);
			this.seconds = (float)Random.Range(0, 60);
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0002DC7C File Offset: 0x0002BE7C
	private void Update()
	{
		this.seconds += Time.deltaTime * this.clockSpeed;
		if (this.seconds >= 60f)
		{
			this.seconds = 0f;
			this.minutes += Time.deltaTime * this.clockSpeed;
			if (this.minutes > 60f)
			{
				this.minutes = 0f;
				this.hour += Time.deltaTime * this.clockSpeed;
				this.hour += 1f;
				if (this.hour >= 24f)
				{
					this.hour = 0f;
				}
			}
		}
		float num = 6f * this.seconds;
		float num2 = 6f * this.minutes;
		float num3 = 30f * this.hour + 0.5f * this.minutes;
		this.pointerSeconds.transform.localEulerAngles = new Vector3(0f, 0f, num);
		this.pointerMinutes.transform.localEulerAngles = new Vector3(0f, 0f, num2);
		this.pointerHours.transform.localEulerAngles = new Vector3(0f, 0f, num3);
	}

	// Token: 0x0400046F RID: 1135
	public bool randomTime = true;

	// Token: 0x04000470 RID: 1136
	public float minutes;

	// Token: 0x04000471 RID: 1137
	public float hour;

	// Token: 0x04000472 RID: 1138
	public float seconds;

	// Token: 0x04000473 RID: 1139
	public GameObject pointerSeconds;

	// Token: 0x04000474 RID: 1140
	public GameObject pointerMinutes;

	// Token: 0x04000475 RID: 1141
	public GameObject pointerHours;

	// Token: 0x04000476 RID: 1142
	public float clockSpeed = 60f;
}
