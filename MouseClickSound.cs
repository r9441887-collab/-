using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003B4 RID: 948
public class MouseClickSound : MonoBehaviour
{
	// Token: 0x06001C16 RID: 7190 RVA: 0x0001A455 File Offset: 0x00018655
	private void Start()
	{
		this.raycaster = base.GetComponent<GraphicRaycaster>();
		this.eventSystem = base.GetComponent<EventSystem>();
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000D0078 File Offset: 0x000CE278
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.pointerEventData = new PointerEventData(this.eventSystem);
			this.pointerEventData.position = Input.mousePosition;
			List<RaycastResult> list = new List<RaycastResult>();
			this.raycaster.Raycast(this.pointerEventData, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					this.TopName = list[i].gameObject.name;
					if (!(list[i].gameObject.GetComponent<PlayMouseSound>() != null))
					{
						break;
					}
					SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
				}
			}
		}
	}

	// Token: 0x0400196E RID: 6510
	private GraphicRaycaster raycaster;

	// Token: 0x0400196F RID: 6511
	private PointerEventData pointerEventData;

	// Token: 0x04001970 RID: 6512
	private EventSystem eventSystem;

	// Token: 0x04001971 RID: 6513
	public string TopName;
}
