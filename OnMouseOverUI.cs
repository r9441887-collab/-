using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003B8 RID: 952
public class OnMouseOverUI : MonoBehaviour
{
	// Token: 0x06001C25 RID: 7205 RVA: 0x0001A51F File Offset: 0x0001871F
	private void OnDisable()
	{
		this.isOnMouseOverUI = false;
		this.isOnMouseOverTitleBar = false;
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x0001A52F File Offset: 0x0001872F
	private void Start()
	{
		this.raycaster = base.GetComponent<GraphicRaycaster>();
		this.eventSystem = base.GetComponent<EventSystem>();
		SingletoneBehaviour<MouseRaycast>.Instance.MouseOverUI.Add(this);
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x000D05E4 File Offset: 0x000CE7E4
	private void Update()
	{
		if (this.previousMousePosition != Input.mousePosition)
		{
			this.previousMousePosition = Input.mousePosition;
			this.pointerEventData = new PointerEventData(this.eventSystem);
			this.pointerEventData.position = Input.mousePosition;
			List<RaycastResult> list = new List<RaycastResult>();
			this.raycaster.Raycast(this.pointerEventData, list);
			bool flag = false;
			bool flag2 = false;
			foreach (RaycastResult raycastResult in list)
			{
				if (raycastResult.gameObject.GetComponent<RectTransform>() != null)
				{
					if (raycastResult.gameObject.GetComponent<CantNextDialogue>() != null)
					{
						flag2 = true;
						this.findName = raycastResult.gameObject.name;
						flag = true;
						break;
					}
					if (!(raycastResult.gameObject.GetComponent<PassRaycast>() != null))
					{
						if (raycastResult.gameObject.tag == "TitleBar")
						{
							flag2 = true;
						}
						this.findName = raycastResult.gameObject.name;
						flag = true;
						break;
					}
				}
			}
			this.isOnMouseOverTitleBar = flag2;
			this.isOnMouseOverUI = flag;
		}
	}

	// Token: 0x04001988 RID: 6536
	private Vector3 previousMousePosition;

	// Token: 0x04001989 RID: 6537
	public bool isOnMouseOverUI;

	// Token: 0x0400198A RID: 6538
	public bool isOnMouseOverTitleBar;

	// Token: 0x0400198B RID: 6539
	public string findName = "";

	// Token: 0x0400198C RID: 6540
	private GraphicRaycaster raycaster;

	// Token: 0x0400198D RID: 6541
	private PointerEventData pointerEventData;

	// Token: 0x0400198E RID: 6542
	private EventSystem eventSystem;
}
