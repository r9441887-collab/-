using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
[Serializable]
public abstract class CustomAnimation : ScriptableObject
{
	// Token: 0x04001D6E RID: 7534
	[SerializeField]
	public string Key = "AnimationName";

	// Token: 0x04001D6F RID: 7535
	[SerializeField]
	public float frameSpeed = 1f;

	// Token: 0x04001D70 RID: 7536
	[SerializeField]
	public Sprite[] sprites;
}
