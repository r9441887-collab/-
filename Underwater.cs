using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
[AddComponentMenu("#NVJOB/Tools/Underwater")]
public class Underwater : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x0000E494 File Offset: 0x0000C694
	private void Awake()
	{
		this.thisTransform = base.transform;
		this.underwater.SetActive(false);
		this.horizenDown.material = this.horizenDownMat1;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0001EDAC File Offset: 0x0001CFAC
	private void LateUpdate()
	{
		if (this.thisTransform.position.y < this.waterLevel)
		{
			if (!this.underwater.activeSelf)
			{
				this.underwater.SetActive(true);
				this.horizenDown.material = this.horizenDownMat2;
				return;
			}
		}
		else if (this.underwater.activeSelf)
		{
			this.underwater.SetActive(false);
			this.horizenDown.material = this.horizenDownMat1;
		}
	}

	// Token: 0x0400005C RID: 92
	public float waterLevel = -27f;

	// Token: 0x0400005D RID: 93
	public GameObject underwater;

	// Token: 0x0400005E RID: 94
	public Renderer horizenDown;

	// Token: 0x0400005F RID: 95
	public Material horizenDownMat1;

	// Token: 0x04000060 RID: 96
	public Material horizenDownMat2;

	// Token: 0x04000061 RID: 97
	private Transform thisTransform;
}
