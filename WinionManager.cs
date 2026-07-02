using System;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class WinionManager : MonoBehaviour
{
	// Token: 0x06001E9E RID: 7838 RVA: 0x0001BC7A File Offset: 0x00019E7A
	private void Awake()
	{
		if (WinionManager.Instance == null)
		{
			WinionManager.Instance = this;
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x000DC088 File Offset: 0x000DA288
	public GameObject GetRandomWinion()
	{
		int num = Random.Range(0, GameManager.instance.gameData.WinionCount);
		return GameManager.instance.gameData.winions[num].gameObject;
	}

	// Token: 0x04001CCC RID: 7372
	public static WinionManager Instance;
}
