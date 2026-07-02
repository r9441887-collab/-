using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
[Serializable]
public class WindowActiveData
{
	// Token: 0x04001BB4 RID: 7092
	public string WindowTitle;

	// Token: 0x04001BB5 RID: 7093
	public bool isActive;

	// Token: 0x04001BB6 RID: 7094
	public bool canOpen = true;

	// Token: 0x04001BB7 RID: 7095
	public Icon iconType = Icon.None;

	// Token: 0x04001BB8 RID: 7096
	public GameObject targetWindow;

	// Token: 0x04001BB9 RID: 7097
	public GameObject windowInfo;
}
