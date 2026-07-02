using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class MiniGameCenter : SingletoneBehaviour<MiniGameCenter>
{
	// Token: 0x06000838 RID: 2104 RVA: 0x000429A8 File Offset: 0x00040BA8
	public void UpdatePlayingMiniGame()
	{
		bool flag = false;
		for (int i = 0; i < this.Objects.Count; i++)
		{
			flag = flag || this.Objects[i].activeSelf;
			if (flag)
			{
				break;
			}
		}
		this.isPlaying = flag;
	}

	// Token: 0x0400090C RID: 2316
	public List<GameObject> Objects;

	// Token: 0x0400090D RID: 2317
	public bool isPlaying;
}
