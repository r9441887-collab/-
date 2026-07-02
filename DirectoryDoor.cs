using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class DirectoryDoor : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x0000F03F File Offset: 0x0000D23F
	public void GreenSet()
	{
		if (!HorrorSceneManager.GreenDoorOpen)
		{
			SingletoneBehaviour<DoorSound>.Instance.LockDoorSound.Play();
			return;
		}
		if (this.myDoor.isOpen)
		{
			this.myDoor.Closing(false, false);
			return;
		}
		base.StartCoroutine("GreendaySetter");
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000F07F File Offset: 0x0000D27F
	private IEnumerator GreendaySetter()
	{
		this.CloseOtherDoors();
		this.TrashTerrain.SetActive(false);
		this.GreenTerrain.SetActive(true);
		this.LightCube.SetActive(true);
		SingletoneBehaviour<SkyBoxManager>.Instance.RenderSkybox(this.firstViewCamera, 0);
		yield return null;
		this.myDoor.OpenDoor();
		yield break;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0002380C File Offset: 0x00021A0C
	private void CloseOtherDoors()
	{
		foreach (DoorInteraction doorInteraction in this.otherDoor)
		{
			doorInteraction.Closing(false, true);
		}
	}

	// Token: 0x0400019C RID: 412
	[SerializeField]
	private GameObject GreenTerrain;

	// Token: 0x0400019D RID: 413
	[SerializeField]
	private GameObject LightCube;

	// Token: 0x0400019E RID: 414
	[SerializeField]
	private GameObject TrashTerrain;

	// Token: 0x0400019F RID: 415
	[SerializeField]
	private Camera firstViewCamera;

	// Token: 0x040001A0 RID: 416
	[SerializeField]
	private DoorInteraction myDoor;

	// Token: 0x040001A1 RID: 417
	[SerializeField]
	private List<DoorInteraction> otherDoor;
}
