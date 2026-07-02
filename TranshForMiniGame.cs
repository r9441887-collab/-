using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class TranshForMiniGame : MonoBehaviour
{
	// Token: 0x0600085F RID: 2143 RVA: 0x00043130 File Offset: 0x00041330
	private void Start()
	{
		int num = 0;
		while (SingletoneBehaviour<TrashCanForMiniGame>.Instance.lastParent == base.transform.parent)
		{
			base.transform.SetParent(SingletoneBehaviour<SpawnNewWindow>.Instance.GetRandomTransform());
			base.GetComponent<RectTransform>().localPosition = Vector3.zero;
			if (num++ >= 10)
			{
				break;
			}
		}
		base.GetComponent<RectTransform>().localPosition = Vector3.zero;
		base.transform.SetParent(SingletoneBehaviour<TrashCanForMiniGame>.Instance.DragableParent);
		SingletoneBehaviour<TrashCanForMiniGame>.Instance.AddTrash(base.gameObject);
	}
}
