using System;
using UnityEngine;

// Token: 0x02000400 RID: 1024
[Serializable]
public class ItemRow
{
	// Token: 0x06001DB0 RID: 7600 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
	public void SetRowObject(Transform r)
	{
		this.rowObject = r;
		for (int i = 0; i < 2; i++)
		{
			this.items[i] = this.rowObject.GetChild(i).GetComponentInChildren<ItemElement>();
			this.items[i].row = this.row;
			this.items[i].column = i;
		}
	}

	// Token: 0x04001BC4 RID: 7108
	[SerializeField]
	public ItemElement[] items = new ItemElement[4];

	// Token: 0x04001BC5 RID: 7109
	public int row;

	// Token: 0x04001BC6 RID: 7110
	private Transform rowObject;
}
