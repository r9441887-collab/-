using System;
using UnityEngine;

// Token: 0x02000458 RID: 1112
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
	// Token: 0x06001F46 RID: 8006 RVA: 0x0001C314 File Offset: 0x0001A514
	public void Awake()
	{
		this.type = ItemType.Default;
	}
}
