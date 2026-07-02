using System;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class MoveCamera : MonoBehaviour
{
	// Token: 0x06001F9A RID: 8090 RVA: 0x000DF564 File Offset: 0x000DD764
	private void Update()
	{
		Vector2 vector;
		vector..ctor(Input.GetAxis("Horizontal") * this.spd, Input.GetAxis("Vertical") * this.spd);
		base.transform.Translate(vector * Time.deltaTime);
	}

	// Token: 0x04001DE9 RID: 7657
	private float spd = 3f;
}
