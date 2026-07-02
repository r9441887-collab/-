using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[Serializable]
public class WinionFolderData
{
	// Token: 0x060004BC RID: 1212 RVA: 0x000110B8 File Offset: 0x0000F2B8
	public void AddInToFolder(GameObject target)
	{
		this.winions.Add(target);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x000318C8 File Offset: 0x0002FAC8
	public void RemoveFromFolder(GameObject Winion)
	{
		Winion winionType = Winion.GetComponent<UIWinion>().winionHandler.winionStatus.winionInfo.winionType;
		foreach (GameObject gameObject in this.winions)
		{
			Winion winionType2 = gameObject.GetComponent<WinionStatus>().winionInfo.winionType;
			if (winionType == winionType2)
			{
				this.winions.Remove(gameObject);
				break;
			}
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00031954 File Offset: 0x0002FB54
	public UIWinion GetUIWinion(Winion targetWinion)
	{
		UIWinion uiwinion = null;
		for (int i = 0; i < this.winions.Count; i++)
		{
			UIWinion component = this.winions[i].GetComponent<UIWinion>();
			if (component.winionHandler.winionStatus.winionInfo.winionType == targetWinion)
			{
				uiwinion = component;
			}
		}
		return uiwinion ?? null;
	}

	// Token: 0x04000552 RID: 1362
	[Header("폴더")]
	public GameObject folder;

	// Token: 0x04000553 RID: 1363
	[HideInInspector]
	public List<GameObject> winions = new List<GameObject>();

	// Token: 0x04000554 RID: 1364
	[Header("폴더 안에 들어갈 수 있는지")]
	public List<bool> canEnter = new List<bool>();

	// Token: 0x04000555 RID: 1365
	[Header("폴더에서 내보낼 수 있는지")]
	public List<bool> canExit = new List<bool>();
}
