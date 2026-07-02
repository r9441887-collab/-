using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class WinionSpriteSort : MonoBehaviour
{
	// Token: 0x0600070A RID: 1802 RVA: 0x000129E4 File Offset: 0x00010BE4
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
		if (this.sprite == null)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0003DB54 File Offset: 0x0003BD54
	private void Update()
	{
		int num = (int)(base.transform.parent.position.y * 100f);
		if (this.isDesc)
		{
			num = -num;
		}
		this.sprite.sortingOrder = num;
	}

	// Token: 0x040007F2 RID: 2034
	private SpriteRenderer sprite;

	// Token: 0x040007F3 RID: 2035
	[Header("정렬방식이 내림차순 인가요?")]
	public bool isDesc;
}
