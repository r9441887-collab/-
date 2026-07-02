using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class WhatIsLastDoor : MonoBehaviour
{
	// Token: 0x06000408 RID: 1032 RVA: 0x0001090F File Offset: 0x0000EB0F
	private void Awake()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Obstacle");
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0002CD50 File Offset: 0x0002AF50
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("!!! Last Door Forced Reveal !!!");
			if (base.transform.childCount > 0)
			{
				GameObject gameObject = base.transform.GetChild(0).gameObject;
				gameObject.SetActive(true);
				DoorInteraction component = gameObject.GetComponent<DoorInteraction>();
				if (component != null)
				{
					WhatIsLastDoor.LastDoor = component;
					this.LastDoorObject = gameObject;
					component.isLocked = false;
					component.Opening(true, true);
				}
				Renderer[] array = gameObject.GetComponentsInChildren<Renderer>(true);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = true;
				}
				array = base.GetComponentsInChildren<Renderer>(true);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = true;
				}
				return;
			}
			Debug.Log("Door child not found!");
		}
	}

	// Token: 0x04000438 RID: 1080
	public GameObject LastDoorObject;

	// Token: 0x04000439 RID: 1081
	public static DoorInteraction LastDoor;
}
