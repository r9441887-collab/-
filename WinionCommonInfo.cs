using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[Serializable]
public class WinionCommonInfo
{
	// Token: 0x04000AA0 RID: 2720
	[Header("위니언 외형 종류")]
	public List<WinionAppearance> winionAppearancesList;

	// Token: 0x04000AA1 RID: 2721
	[Header("위니언 좋아하는 음식 종류")]
	public List<WinionFood> winionFoodsList;

	// Token: 0x04000AA2 RID: 2722
	[Header("위니언 성격 종류")]
	public List<WinionPersonality> winionPersonalitiesList;

	// Token: 0x04000AA3 RID: 2723
	[Header("위니언 감정 종류")]
	public List<WinionEmotion> winionEmotionsList;
}
