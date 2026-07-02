using System;

// Token: 0x02000460 RID: 1120
[Serializable]
public class MailSlot
{
	// Token: 0x06001F5C RID: 8028 RVA: 0x0001C39D File Offset: 0x0001A59D
	public MailSlot(int _id, MailSO _item)
	{
		this.ID = _id;
		this.item = _item;
		this.item.data.id = _id;
		this.isRecv = true;
		this.isRead = false;
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x0001C3D2 File Offset: 0x0001A5D2
	public void SetDefault()
	{
		this.ID = 0;
		this.item = null;
		this.isRecv = false;
		this.isRead = false;
	}

	// Token: 0x04001DC1 RID: 7617
	public int ID;

	// Token: 0x04001DC2 RID: 7618
	public MailSO item;

	// Token: 0x04001DC3 RID: 7619
	public bool isRecv;

	// Token: 0x04001DC4 RID: 7620
	public bool isRead;
}
