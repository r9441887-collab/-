using System;
using System.Collections.Generic;

// Token: 0x02000149 RID: 329
[Serializable]
public class DistanceData
{
	// Token: 0x060007DF RID: 2015 RVA: 0x00040E70 File Offset: 0x0003F070
	public DistanceData(string name)
	{
		this.Name = name;
		this.canShake = true;
		this.Distance.Add(0f);
		this.Distance.Add(0f);
		this.Distance.Add(0f);
		this.Distance.Add(0f);
		this.Distance.Add(0f);
		this.NearBy.Add(false);
		this.NearBy.Add(false);
		this.NearBy.Add(false);
		this.NearBy.Add(false);
		this.NearBy.Add(false);
	}

	// Token: 0x040008C4 RID: 2244
	public string Name;

	// Token: 0x040008C5 RID: 2245
	public List<float> Distance = new List<float>();

	// Token: 0x040008C6 RID: 2246
	public List<bool> NearBy = new List<bool>();

	// Token: 0x040008C7 RID: 2247
	public long lastShakeTime;

	// Token: 0x040008C8 RID: 2248
	public bool canShake = true;
}
