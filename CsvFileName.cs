using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
[Serializable]
public class CsvFileName
{
	// Token: 0x04000AB9 RID: 2745
	public string winionInfoFileName;

	// Token: 0x04000ABA RID: 2746
	[Header("챕터별 이벤트 대화 파일 이름")]
	public string chapter00_KR_EventDialogueFileName;

	// Token: 0x04000ABB RID: 2747
	public string chapter00_EN_EventDialogueFileName;

	// Token: 0x04000ABC RID: 2748
	public string chapter01_KR_EventDialogueFileName;

	// Token: 0x04000ABD RID: 2749
	public string chapter01_EN_EventDialogueFileName;

	// Token: 0x04000ABE RID: 2750
	public string chapter02_KR_EventDialogueFileName;

	// Token: 0x04000ABF RID: 2751
	public string chapter02_EN_EventDialogueFileName;

	// Token: 0x04000AC0 RID: 2752
	public string chapter03_KR_EventDialogueFileName;

	// Token: 0x04000AC1 RID: 2753
	public string chapter03_EN_EventDialogueFileName;

	// Token: 0x04000AC2 RID: 2754
	[Space]
	[Header("이벤트 별 잡담")]
	public string winion_Dialogue_byEventChapter00_KR_FileName;

	// Token: 0x04000AC3 RID: 2755
	public string winion_Dialogue_byEventChapter00_EN_FileName;

	// Token: 0x04000AC4 RID: 2756
	public string winion_Dialogue_byEventChapter01_KR_FileName;

	// Token: 0x04000AC5 RID: 2757
	public string winion_Dialogue_byEventChapter01_EN_FileName;

	// Token: 0x04000AC6 RID: 2758
	public string winion_Dialogue_byEventChapter02_KR_FileName;

	// Token: 0x04000AC7 RID: 2759
	public string winion_Dialogue_byEventChapter02_EN_FileName;

	// Token: 0x04000AC8 RID: 2760
	[Header("행위 별 잡담")]
	public string winion_Dialogue_byBehavior_FileName;

	// Token: 0x04000AC9 RID: 2761
	[Space]
	[Header("애정도 별 대화")]
	public string ION_ChatWithLoveFileName;

	// Token: 0x04000ACA RID: 2762
	public string Bo_ChatWithLoveFileName;

	// Token: 0x04000ACB RID: 2763
	public string Grid_ChatWithLoveFileName;

	// Token: 0x04000ACC RID: 2764
	public string Fix_ChatWithLoveFileName;

	// Token: 0x04000ACD RID: 2765
	public string Debug_ChatWithLoveFileName;

	// Token: 0x04000ACE RID: 2766
	[Space]
	public string myPC_3DGame_FileName01_KR;

	// Token: 0x04000ACF RID: 2767
	public string myPC_3DGame_FileName01_EN;

	// Token: 0x04000AD0 RID: 2768
	public string myPC_3DGame_FileName02_KR;

	// Token: 0x04000AD1 RID: 2769
	public string myPC_3DGame_FileName02_EN;

	// Token: 0x04000AD2 RID: 2770
	[Space]
	public string Setting_Conversations_KR_FileName;

	// Token: 0x04000AD3 RID: 2771
	public string Setting_Conversations_EN_FileName;

	// Token: 0x04000AD4 RID: 2772
	[Space]
	public string mailContent_KR_FileName;

	// Token: 0x04000AD5 RID: 2773
	public string mailContent_EN_FileName;

	// Token: 0x04000AD6 RID: 2774
	[Space]
	public string ContentUI_KR_FileName;

	// Token: 0x04000AD7 RID: 2775
	public string ContentUI_EN_FileName;

	// Token: 0x04000AD8 RID: 2776
	public string ContentEvent_KR_FileName;

	// Token: 0x04000AD9 RID: 2777
	public string ContentEvent_EN_FileName;
}
