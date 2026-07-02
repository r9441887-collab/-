using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class DoorSnapperEnter : MonoBehaviour
{
	// Token: 0x06000145 RID: 325 RVA: 0x0000F0A5 File Offset: 0x0000D2A5
	private void OnEnable()
	{
		this.firstEnter = true;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000F0AE File Offset: 0x0000D2AE
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && this.firstEnter)
		{
			SingletoneBehaviour<MazeController>.Instance.doorSnapper.Add(base.gameObject);
			this.firstEnter = false;
		}
	}

	// Token: 0x040001A5 RID: 421
	public bool firstEnter = true;
}
