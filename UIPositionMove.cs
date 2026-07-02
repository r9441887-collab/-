using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class UIPositionMove : MonoBehaviour
{
	// Token: 0x06001CEB RID: 7403 RVA: 0x000D3E48 File Offset: 0x000D2048
	public virtual void GetMouseDownPosition()
	{
		this.isClick = true;
		RectTransform component = base.GetComponent<RectTransform>();
		Vector2 vector = Input.mousePosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(component, vector, Camera.main, ref this.diffPosition);
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0001AD8A File Offset: 0x00018F8A
	public virtual void GetMouseUpPosition()
	{
		this.isClick = false;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x000D3E80 File Offset: 0x000D2080
	public virtual void MouseMove()
	{
		if (this.isClick && this.canMove)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			Camera main = Camera.main;
			Vector3 mousePosition = Input.mousePosition;
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(SystemBox.SystemCanvas, mousePosition, main, ref vector);
			Vector2 vector2 = vector - this.diffPosition * component.localScale;
			component.localPosition = vector2;
			float num = 1280f;
			float num2 = component.sizeDelta.x / 2f;
			Vector3 localScale = component.localScale;
			float num3 = 720f;
			float num4 = component.sizeDelta.y / 2f * component.localScale.y;
			Vector2 anchoredPosition = component.anchoredPosition;
			float num5 = anchoredPosition.x;
			num5 = Mathf.Clamp(num5, -num, num);
			anchoredPosition.x = num5;
			float num6 = anchoredPosition.y;
			num6 = Mathf.Clamp(num6, -num3, num3 - num4);
			anchoredPosition.y = num6;
			component.anchoredPosition = anchoredPosition;
		}
	}

	// Token: 0x04001AF8 RID: 6904
	public bool isClick;

	// Token: 0x04001AF9 RID: 6905
	public Vector2 diffPosition;

	// Token: 0x04001AFA RID: 6906
	public bool canMove = true;
}
