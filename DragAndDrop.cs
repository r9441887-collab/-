using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class DragAndDrop : MonoBehaviour
{
	// Token: 0x060008B1 RID: 2225 RVA: 0x00013A42 File Offset: 0x00011C42
	public Vector3 GetMousePos()
	{
		return Camera.main.WorldToScreenPoint(base.transform.position);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00013A59 File Offset: 0x00011C59
	public virtual void OnMouseDown()
	{
		if (MouseRaycast.isMouseOnUI)
		{
			return;
		}
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		this.mousePosition = Input.mousePosition - this.GetMousePos();
		DragAndDrop.MouseClick = true;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000441BC File Offset: 0x000423BC
	public virtual void OnMouseDrag()
	{
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		if (!DragAndDrop.MouseClick)
		{
			return;
		}
		this.PickUpTimer += Time.deltaTime;
		if (this.PickUpTimer >= this.pickDistance)
		{
			if (!this.isPickUp)
			{
				this.OnMouseDragStart();
			}
			this.isPickUp = true;
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition - this.mousePosition);
			base.transform.position = vector;
			if (this.targetObject != null)
			{
				this.targetObject.transform.localPosition = this.Y_Vector;
			}
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00044260 File Offset: 0x00042460
	public virtual void OnMouseUp()
	{
		if (SingletoneBehaviour<MiniGameCenter>.Instance.isPlaying)
		{
			return;
		}
		if (this.isPickUp)
		{
			this.OnMouseDragEnd();
		}
		else if (!this.isPickUp)
		{
			this.OnMouseClickUp();
		}
		this.isPickUp = false;
		this.PickUpTimer = 0f;
		DragAndDrop.MouseClick = false;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00013A8C File Offset: 0x00011C8C
	public virtual void OnMouseDragStart()
	{
		this.Y_Vector = new Vector3(0f, this.Y_Float, 0f);
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00013AA9 File Offset: 0x00011CA9
	public virtual void OnMouseDragEnd()
	{
		if (this.targetObject != null)
		{
			this.targetObject.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0000E32C File Offset: 0x0000C52C
	public virtual void OnMouseClickUp()
	{
	}

	// Token: 0x04000992 RID: 2450
	public bool debug;

	// Token: 0x04000993 RID: 2451
	public GameObject targetObject;

	// Token: 0x04000994 RID: 2452
	public bool isPickUp;

	// Token: 0x04000995 RID: 2453
	public float PickUpTimer;

	// Token: 0x04000996 RID: 2454
	public Vector3 mousePosition;

	// Token: 0x04000997 RID: 2455
	public int ClickCount;

	// Token: 0x04000998 RID: 2456
	public double lastClickTime;

	// Token: 0x04000999 RID: 2457
	public Vector3 Y_Vector = new Vector3(0f, 0.07f, 0f);

	// Token: 0x0400099A RID: 2458
	public float Y_Float = 0.03f;

	// Token: 0x0400099B RID: 2459
	public static bool MouseClick;

	// Token: 0x0400099C RID: 2460
	public float pickDistance = 0.15f;
}
