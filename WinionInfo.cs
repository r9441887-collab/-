using System;
using UnityEngine;

// Token: 0x0200011E RID: 286
[Serializable]
public class WinionInfo : ISerializationCallbackReceiver
{
	// Token: 0x060006D7 RID: 1751 RVA: 0x0003CBE4 File Offset: 0x0003ADE4
	public void OnAfterDeserialize()
	{
		this.winionAppearance.id = (int)this.winionType;
		switch (this.winionType)
		{
		case Winion.Ion:
			this.winionName = "아이온";
			return;
		case Winion.Bo:
			this.winionName = "보";
			return;
		case Winion.Grid:
			this.winionName = "그리드";
			return;
		case Winion.Fix:
			this.winionName = "픽스";
			return;
		case Winion.Debug:
			this.winionName = "디버그";
			return;
		default:
			return;
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x040007A5 RID: 1957
	[Header("위니언 종류")]
	public Winion winionType;

	// Token: 0x040007A6 RID: 1958
	[Header("위니언 이름")]
	public string winionName;

	// Token: 0x040007A7 RID: 1959
	[Header("위니언 외형")]
	public WinionAppearance winionAppearance;

	// Token: 0x040007A8 RID: 1960
	[Header("위니언 성격")]
	public WinionPersonality presonality;

	// Token: 0x040007A9 RID: 1961
	[Header("좋아하는 음식")]
	public WinionFood favoriteFood;

	// Token: 0x040007AA RID: 1962
	[Header("현재 바이러스 여부")]
	public bool hasVirus;

	// Token: 0x040007AB RID: 1963
	[Header("위니언 상태 관리")]
	public int battery;

	// Token: 0x040007AC RID: 1964
	public bool isDischarged;

	// Token: 0x040007AD RID: 1965
	public int memory;

	// Token: 0x040007AE RID: 1966
	public int hungry;

	// Token: 0x040007AF RID: 1967
	[Space]
	[Space]
	public int moodType;

	// Token: 0x040007B0 RID: 1968
	public int moodLevel;

	// Token: 0x040007B1 RID: 1969
	public float friendship;

	// Token: 0x040007B2 RID: 1970
	public int winionLevel;

	// Token: 0x040007B3 RID: 1971
	public int tempWinionLevel;

	// Token: 0x040007B4 RID: 1972
	public bool canIndependent;

	// Token: 0x040007B5 RID: 1973
	public bool isIndependent;

	// Token: 0x040007B6 RID: 1974
	public bool isEnable;

	// Token: 0x040007B7 RID: 1975
	public bool isDeath;
}
