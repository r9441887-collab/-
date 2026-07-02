using System;
using System.Collections.Generic;

// Token: 0x020001BA RID: 442
[Serializable]
public class LineByBehavior
{
	// Token: 0x04000B8F RID: 2959
	public int nameID;

	// Token: 0x04000B90 RID: 2960
	public string[] context;

	// Token: 0x04000B91 RID: 2961
	public bool haveSpecialBubble;

	// Token: 0x04000B92 RID: 2962
	public List<int> bubbleTypeIndex;

	// Token: 0x04000B93 RID: 2963
	public List<ChatController.BubbleType> bubbleTypeList;

	// Token: 0x04000B94 RID: 2964
	public bool haveEmotionSpeechBubble;

	// Token: 0x04000B95 RID: 2965
	public List<int> emotionSpeechBubble_contextIndex;

	// Token: 0x04000B96 RID: 2966
	public List<string> emotionSpeechBubble_ID;
}
