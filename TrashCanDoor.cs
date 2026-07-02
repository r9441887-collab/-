using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class TrashCanDoor : MonoBehaviour
{
	// Token: 0x060003FE RID: 1022 RVA: 0x0002CC20 File Offset: 0x0002AE20
	public void TrashCanOpen()
	{
		if (!HorrorSceneManager.TrashDoorOpen && this.myDoor.isLocked)
		{
			SingletoneBehaviour<DoorSound>.Instance.LockDoorSound.Play();
			return;
		}
		if (this.myDoor.isOpen)
		{
			this.myDoor.Closing(false, false);
			return;
		}
		base.StartCoroutine("TrashCanSetter");
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000108E9 File Offset: 0x0000EAE9
	private IEnumerator TrashCanSetter()
	{
		this.CloseOtherDoors();
		this.GreenTerrain.SetActive(false);
		this.LightCube.SetActive(false);
		this.TrashTerrain.SetActive(true);
		SingletoneBehaviour<SkyBoxManager>.Instance.DarkSky();
		yield return null;
		this.myDoor.OpenDoor();
		yield break;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0002CC78 File Offset: 0x0002AE78
	private void CloseOtherDoors()
	{
		foreach (DoorInteraction doorInteraction in this.otherDoor)
		{
			doorInteraction.Closing(false, true);
		}
	}

	// Token: 0x0400042F RID: 1071
	[SerializeField]
	private GameObject GreenTerrain;

	// Token: 0x04000430 RID: 1072
	[SerializeField]
	private GameObject LightCube;

	// Token: 0x04000431 RID: 1073
	[SerializeField]
	private GameObject TrashTerrain;

	// Token: 0x04000432 RID: 1074
	[SerializeField]
	private Camera firstViewCamera;

	// Token: 0x04000433 RID: 1075
	public DoorInteraction myDoor;

	// Token: 0x04000434 RID: 1076
	[SerializeField]
	private List<DoorInteraction> otherDoor;
}
