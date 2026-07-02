using System;

// Token: 0x0200015B RID: 347
public class ClosePurse : PurseByBeat
{
	// Token: 0x0600082B RID: 2091 RVA: 0x00013544 File Offset: 0x00011744
	public override void Pulse()
	{
		base.Pulse();
		if (this.removeNextPurse)
		{
			return;
		}
		this.HP--;
		if (this.HP <= 0)
		{
			this.RemoveNextPurse(false);
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000428E8 File Offset: 0x00040AE8
	public override void Init()
	{
		base.Init();
		switch (this.windowType)
		{
		default:
			return;
		}
	}

	// Token: 0x04000903 RID: 2307
	public WindowType windowType;
}
