using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003B5 RID: 949
public class MouseRaycast : SingletoneBehaviour<MouseRaycast>
{
	// Token: 0x06001C19 RID: 7193 RVA: 0x0001A46F File Offset: 0x0001866F
	private void Start()
	{
		this.raycaster = base.GetComponent<GraphicRaycaster>();
		this.eventSystem = base.GetComponent<EventSystem>();
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x000D0138 File Offset: 0x000CE338
	private void Update()
	{
		if (MouseRaycast.ForDebug != this._ForDebug)
		{
			MouseRaycast.ForDebug = this._ForDebug;
		}
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < this.MouseOverUI.Count; i++)
		{
			if (this.MouseOverUI[i].isOnMouseOverUI)
			{
				flag = true;
			}
			if (this.MouseOverUI[i].isOnMouseOverTitleBar)
			{
				flag2 = true;
			}
		}
		MouseRaycast.isMouseOnUI = flag;
		this._isMouseOnUI = MouseRaycast.isMouseOnUI;
		MouseRaycast.isMouseOnTitle = flag2;
		this._isMouseOnTitle = flag2;
		if (Input.GetMouseButtonDown(0))
		{
			this.pointerEventData = new PointerEventData(this.eventSystem);
			this.pointerEventData.position = Input.mousePosition;
			List<RaycastResult> list = new List<RaycastResult>();
			this.raycaster.Raycast(this.pointerEventData, list);
			bool flag3 = false;
			foreach (RaycastResult raycastResult in list)
			{
				if (raycastResult.gameObject.transform.parent.name == "TopMostLayer")
				{
					raycastResult.gameObject.transform.SetParent(this.firstLayer);
					raycastResult.gameObject.transform.SetParent(this.topMostLayer);
					break;
				}
				if (raycastResult.gameObject.tag == "Window")
				{
					flag3 = true;
					this.targetWindow = raycastResult.gameObject;
					break;
				}
			}
			if (flag3)
			{
				this.SetFirstLayer(this.targetWindow);
			}
		}
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0001A489 File Offset: 0x00018689
	public void SetTopMostLayer(GameObject target)
	{
		target.transform.SetParent(this.topMostLayer);
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x000D02DC File Offset: 0x000CE4DC
	public void SetFirstLayer(GameObject target)
	{
		for (int i = 0; i < this.firstLayer.childCount; i++)
		{
			this.firstLayer.GetChild(i).SetParent(this.secondLayer);
		}
		target.transform.SetParent(this.firstLayer);
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x0001A49C File Offset: 0x0001869C
	public void SetSecondLayer(GameObject target)
	{
		target.transform.SetParent(this.secondLayer);
	}

	// Token: 0x04001972 RID: 6514
	public Transform topMostLayer;

	// Token: 0x04001973 RID: 6515
	public Transform firstLayer;

	// Token: 0x04001974 RID: 6516
	public Transform secondLayer;

	// Token: 0x04001975 RID: 6517
	public GameObject targetWindow;

	// Token: 0x04001976 RID: 6518
	public static bool isMouseOnUI;

	// Token: 0x04001977 RID: 6519
	public bool _isMouseOnUI;

	// Token: 0x04001978 RID: 6520
	public static bool isMouseOnTitle;

	// Token: 0x04001979 RID: 6521
	public bool _isMouseOnTitle;

	// Token: 0x0400197A RID: 6522
	public static bool ForDebug;

	// Token: 0x0400197B RID: 6523
	public bool _ForDebug;

	// Token: 0x0400197C RID: 6524
	public List<OnMouseOverUI> MouseOverUI = new List<OnMouseOverUI>();

	// Token: 0x0400197D RID: 6525
	private GraphicRaycaster raycaster;

	// Token: 0x0400197E RID: 6526
	private PointerEventData pointerEventData;

	// Token: 0x0400197F RID: 6527
	private EventSystem eventSystem;

	// Token: 0x04001980 RID: 6528
	private Vector3 previousMousePosition;
}
