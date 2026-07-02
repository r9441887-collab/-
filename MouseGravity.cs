using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200013F RID: 319
public class MouseGravity : MonoBehaviour
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x0001304F File Offset: 0x0001124F
	private void Start()
	{
		base.StartCoroutine("MouseGravityRoutine");
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void LateUpdate()
	{
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0001305D File Offset: 0x0001125D
	private IEnumerator MouseGravityRoutine()
	{
		int a = 0;
		for (;;)
		{
			a ^= 1;
			if (a == 0)
			{
				Vector3 mousePosition = Input.mousePosition;
				Vector3 vector;
				vector..ctor(0f, 1f, 0f);
				Vector3 vector2 = mousePosition - vector;
				Mouse.current.WarpCursorPosition(vector2);
			}
			yield return new WaitForSeconds(this.waitTime);
		}
		yield break;
	}

	// Token: 0x04000897 RID: 2199
	public float waitTime = 0.1f;
}
