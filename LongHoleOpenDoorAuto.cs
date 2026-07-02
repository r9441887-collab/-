using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class LongHoleOpenDoorAuto : MonoBehaviour
{
	// Token: 0x060002A9 RID: 681 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
	private void OnEnable()
	{
		this.firstEnter = false;
		if (this.OpenDoorAwake)
		{
			this.OpenAllDoor();
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0002797C File Offset: 0x00025B7C
	public void OpenAllDoor()
	{
		if (this.firstEnter)
		{
			return;
		}
		this.firstEnter = true;
		foreach (DoorInteraction doorInteraction in this.doorInteractions)
		{
			MazeDoorInfo component = doorInteraction.transform.parent.GetComponent<MazeDoorInfo>();
			if (component.isCorrectDoor)
			{
				this.Controller.AutoOpenObject.SetActive(false);
				this.Controller.EnemyDoorSnapper.SetActive(false);
				doorInteraction.isLocked = false;
				component.OpenDoorEndRoom();
				doorInteraction.isLocked = true;
				SingletoneBehaviour<MazeController>.Instance.MazeLastBug.GetComponent<MazeBossBug>().moveSpeed += 0.1f;
				return;
			}
		}
		DoorInteraction doorInteraction2 = this.doorInteractions[Random.Range(0, this.doorInteractions.Count)];
		doorInteraction2.isLocked = false;
		doorInteraction2.poolDoor = true;
		doorInteraction2.transform.parent.GetComponent<MazeDoorInfo>().OpenDoorEndRoom();
		doorInteraction2.isLocked = true;
		doorInteraction2.poolDoor = false;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000FBB7 File Offset: 0x0000DDB7
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.OpenAllDoor();
		}
	}

	// Token: 0x040002D6 RID: 726
	public List<DoorInteraction> doorInteractions;

	// Token: 0x040002D7 RID: 727
	public LongHoleController Controller;

	// Token: 0x040002D8 RID: 728
	public bool OpenDoorAwake;

	// Token: 0x040002D9 RID: 729
	public bool firstEnter;
}
