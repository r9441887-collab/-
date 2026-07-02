using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
[CreateAssetMenu(fileName = "New Winion Animation Pack", menuName = "Winion Animation/Animaion Pack")]
public class WinionAnimations : CustomAnimations
{
	// Token: 0x04001D72 RID: 7538
	public int Type;

	// Token: 0x04001D73 RID: 7539
	public int Level;

	// Token: 0x04001D74 RID: 7540
	[TextArea(10, 10)]
	public string Description = "";
}
