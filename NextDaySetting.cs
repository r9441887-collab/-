using System;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class NextDaySetting : MonoBehaviour
{
	// Token: 0x06001A9A RID: 6810 RVA: 0x000C3FC0 File Offset: 0x000C21C0
	private void Start()
	{
		int @int = PlayerPrefs.GetInt("Language", 0);
		if (@int == 0)
		{
			this.title00.SetActive(true);
			this.title01.SetActive(true);
			return;
		}
		if (@int == 1)
		{
			this.title00.SetActive(false);
			this.title01.SetActive(false);
		}
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x040016CE RID: 5838
	public GameObject title00;

	// Token: 0x040016CF RID: 5839
	public GameObject title01;
}
