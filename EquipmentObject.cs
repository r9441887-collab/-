using System;
using UnityEngine;

// Token: 0x0200045A RID: 1114
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
	// Token: 0x06001F48 RID: 8008 RVA: 0x0001C325 File Offset: 0x0001A525
	public void Awake()
	{
		this.type = ItemType.Equipment;
		this.equipmentType = EquipmentType.None;
	}

	// Token: 0x04001DAE RID: 7598
	public EquipmentType equipmentType;
}
