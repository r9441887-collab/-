using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class KeyTest : MonoBehaviour
{
	// Token: 0x06000720 RID: 1824 RVA: 0x00012B4A File Offset: 0x00010D4A
	private void Start()
	{
		DummyWinionAnimator.minDistance = this._minDistance;
		DummyWinionAnimator.maxDistance = this._maxDistance;
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x0400080D RID: 2061
	public float _minDistance = 2f;

	// Token: 0x0400080E RID: 2062
	public float _maxDistance = 8f;

	// Token: 0x0400080F RID: 2063
	public bool forDebug;
}
