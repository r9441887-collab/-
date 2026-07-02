using System;
using UnityEngine;

// Token: 0x02000410 RID: 1040
[Serializable]
public class MailData
{
	// Token: 0x04001C26 RID: 7206
	public int id;

	// Token: 0x04001C27 RID: 7207
	public float scale;

	// Token: 0x04001C28 RID: 7208
	public float posY;

	// Token: 0x04001C29 RID: 7209
	public int animationIndex;

	// Token: 0x04001C2A RID: 7210
	public Sprite profileImage;

	// Token: 0x04001C2B RID: 7211
	public string name;

	// Token: 0x04001C2C RID: 7212
	public string title;

	// Token: 0x04001C2D RID: 7213
	public string date;

	// Token: 0x04001C2E RID: 7214
	[TextArea(10, 10)]
	public string content;

	// Token: 0x04001C2F RID: 7215
	public string fileName;
}
