using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class UserManager : MonoBehaviour
{
	// Token: 0x060008DB RID: 2267 RVA: 0x00013BDA File Offset: 0x00011DDA
	private void Awake()
	{
		if (UserManager.LocalPlayer == null)
		{
			UserManager.LocalPlayer = this;
		}
	}

	// Token: 0x040009AE RID: 2478
	public static UserManager LocalPlayer;
}
