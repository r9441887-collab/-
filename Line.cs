using System;
using System.Collections.Generic;

// Token: 0x020001B7 RID: 439
[Serializable]
public class Line
{
	// Token: 0x04000B7B RID: 2939
	public int nameID;

	// Token: 0x04000B7C RID: 2940
	public string[] context;

	// Token: 0x04000B7D RID: 2941
	public bool haveSpecialBubble;

	// Token: 0x04000B7E RID: 2942
	public List<int> bubbleTypeIndex;

	// Token: 0x04000B7F RID: 2943
	public List<ChatController.BubbleType> bubbleTypeList;

	// Token: 0x04000B80 RID: 2944
	public bool haveEmotionSpeechBubble;

	// Token: 0x04000B81 RID: 2945
	public List<int> emotionSpeechBubble_contextIndex;

	// Token: 0x04000B82 RID: 2946
	public List<string> emotionSpeechBubble_ID;

	// Token: 0x04000B83 RID: 2947
	public bool changeFace;

	// Token: 0x04000B84 RID: 2948
	public List<List<string>> changeExpression;

	// Token: 0x04000B85 RID: 2949
	public List<int> changeExpression_Index;

	// Token: 0x04000B86 RID: 2950
	public bool haveChoice;

	// Token: 0x04000B87 RID: 2951
	public Choice choice;

	// Token: 0x04000B88 RID: 2952
	public bool getItem;

	// Token: 0x04000B89 RID: 2953
	public int ItemId;

	// Token: 0x04000B8A RID: 2954
	public bool changeEvnetID;

	// Token: 0x04000B8B RID: 2955
	public int evnetIDToBeChange;
}
