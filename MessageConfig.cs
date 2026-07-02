using System;

// Token: 0x020003D5 RID: 981
public class MessageConfig
{
	// Token: 0x06001CBC RID: 7356 RVA: 0x000D3440 File Offset: 0x000D1640
	public void AutoWindowPosition()
	{
		int num = SystemBox.windowCount_1d++;
		if (SystemBox.windowCount_1d == 10)
		{
			SystemBox.windowCount_1d = 0;
			num = 0;
			SystemBox.windowCount_2d++;
		}
		if (SystemBox.windowCount_2d == 10)
		{
			SystemBox.windowCount_2d = 0;
		}
		int windowCount_2d = SystemBox.windowCount_2d;
		this.x += 15 * num + 50 * windowCount_2d;
		this.y -= 15 * num + 5 * windowCount_2d;
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x000D34B8 File Offset: 0x000D16B8
	public MessageConfig()
	{
		this.AutoWindowPosition();
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x000D3514 File Offset: 0x000D1714
	public MessageConfig(string context)
	{
		this.Context = context;
		this.AutoWindowPosition();
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x000D3578 File Offset: 0x000D1778
	public MessageConfig(string title, string context)
	{
		this.Title = title;
		this.Context = context;
		this.AutoWindowPosition();
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x000D35E4 File Offset: 0x000D17E4
	public MessageConfig(string context, int width, int height)
	{
		this.width = width;
		this.height = height;
		this.Context = context;
		this.AutoWindowPosition();
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x000D3654 File Offset: 0x000D1854
	public MessageConfig(string title, string context, int width, int height)
	{
		this.width = width;
		this.height = height;
		this.Title = title;
		this.Context = context;
		this.AutoWindowPosition();
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x000D36CC File Offset: 0x000D18CC
	public MessageConfig(int width, int height)
	{
		this.width = width;
		this.height = height;
		this.AutoWindowPosition();
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x000D3738 File Offset: 0x000D1938
	public MessageConfig(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x000D37AC File Offset: 0x000D19AC
	public MessageConfig(string title, string context, int x, int y, int width, int height)
	{
		this.Title = title;
		this.Context = context;
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x04001AD1 RID: 6865
	public string Title = "";

	// Token: 0x04001AD2 RID: 6866
	public string Context = "";

	// Token: 0x04001AD3 RID: 6867
	public int x = -260;

	// Token: 0x04001AD4 RID: 6868
	public int y = 130;

	// Token: 0x04001AD5 RID: 6869
	public int width = 500;

	// Token: 0x04001AD6 RID: 6870
	public int height = 300;
}
