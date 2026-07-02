using System;
using UnityEngine;

// Token: 0x0200044C RID: 1100
[CreateAssetMenu(fileName = "New Winion Animation Tree", menuName = "Winion Animation/Animaion Tree")]
public class WinionAnimationTree : ScriptableObject
{
	// Token: 0x06001F19 RID: 7961 RVA: 0x000DE158 File Offset: 0x000DC358
	public WinionAnimations GetAnimationPack(int type, int level)
	{
		int num = 0;
		for (int i = 0; i < this.winionAnimations.Length; i++)
		{
			if (this.winionAnimations[i].Level == level && this.winionAnimations[i].Type == type)
			{
				return this.winionAnimations[i];
			}
		}
		return this.winionAnimations[num];
	}

	// Token: 0x04001D75 RID: 7541
	public WinionAnimations[] winionAnimations;
}
