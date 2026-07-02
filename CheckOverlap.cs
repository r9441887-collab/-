using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class CheckOverlap : SingletoneBehaviour<CheckOverlap>
{
	// Token: 0x06000623 RID: 1571 RVA: 0x00036F98 File Offset: 0x00035198
	public bool IsOverlap(Vector3 position)
	{
		bool flag = false;
		foreach (GameObject gameObject in this.obstacle)
		{
			if (flag)
			{
				break;
			}
			BoxCollider2D component = gameObject.GetComponent<BoxCollider2D>();
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			if (component != null)
			{
				flag = component.bounds.Contains(position);
			}
			if (component2 != null)
			{
				Camera.main.WorldToScreenPoint(position);
				Rect rect = component2.rect;
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(component2, position, null, ref vector);
				flag = rect.Contains(vector);
			}
		}
		return flag;
	}

	// Token: 0x040006B0 RID: 1712
	public List<GameObject> obstacle = new List<GameObject>();
}
