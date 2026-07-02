using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class TrashCanLayer : MonoBehaviour
{
	// Token: 0x06001E36 RID: 7734 RVA: 0x0001B8E3 File Offset: 0x00019AE3
	private void Start()
	{
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x000D9998 File Offset: 0x000D7B98
	private void Update()
	{
		this.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		for (int i = 0; i < this.layers.Count; i++)
		{
			Vector3 vector = this.layers[i].anchoredPosition;
			vector.x = this.mousePosition.x * this.weights[i];
			vector.y = this.mousePosition.y * this.weights[this.layers.Count - i - 1];
			vector.x = Mathf.Clamp(vector.x, -100f, 100f);
			vector.y = Mathf.Clamp(vector.y, -100f, 100f);
			this.layers[i].anchoredPosition = vector;
		}
	}

	// Token: 0x04001C56 RID: 7254
	public List<RectTransform> layers = new List<RectTransform>();

	// Token: 0x04001C57 RID: 7255
	public List<float> weights = new List<float>();

	// Token: 0x04001C58 RID: 7256
	public Vector3 mousePosition;

	// Token: 0x04001C59 RID: 7257
	private RectTransform rect;
}
