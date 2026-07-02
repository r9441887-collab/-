using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class MiniGameWindow : MonoBehaviour
{
	// Token: 0x0600083A RID: 2106 RVA: 0x0001365A File Offset: 0x0001185A
	public void SetActive(bool active)
	{
		this.worldObject.SetActive(active);
		this.uiObject.SetActive(active);
		SingletoneBehaviour<MiniGameCenter>.Instance.UpdatePlayingMiniGame();
	}

	// Token: 0x0400090E RID: 2318
	[Header("미니게임 이름")]
	public string GameName;

	// Token: 0x0400090F RID: 2319
	public GameObject worldObject;

	// Token: 0x04000910 RID: 2320
	public GameObject uiObject;
}
