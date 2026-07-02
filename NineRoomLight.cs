using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class NineRoomLight : MonoBehaviour
{
	// Token: 0x06000332 RID: 818 RVA: 0x00010103 File Offset: 0x0000E303
	private void OnEnable()
	{
		this.SetDefault(false);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00029E1C File Offset: 0x0002801C
	private void OnTriggerEnter(Collider other)
	{
		if (this.firstEnter && other.tag == "Player")
		{
			this.firstEnter = false;
			if (this.CorrectLastDoor != null)
			{
				this.CorrectLastDoor.CloseEndAction = delegate
				{
					this.doors[3].transform.parent.parent.gameObject.SetActive(true);
					if (this.PreviousMaze != null)
					{
						this.PreviousMaze.GetComponent<NineRoomLight>().PoolingSelf();
					}
					this.CorrectLastDoor.CloseEndAction = null;
				};
				this.CorrectLastDoor.Closing(false, false);
				this.CorrectLastDoor = null;
			}
			int num = this.nextRoomWay;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0001010C File Offset: 0x0000E30C
	public void PoolingSelf()
	{
		this.SetDefault(true);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00029E8C File Offset: 0x0002808C
	public void SetDefault(bool value)
	{
		this.firstEnter = true;
		this.isPooling = value;
		if (this.CorrectLastDoor != null)
		{
			this.CorrectLastDoor.CloseEndAction = null;
		}
		if (value)
		{
			this.CorrectLastDoor = null;
			this.PreviousMaze = null;
		}
		this.doors[3].transform.parent.parent.gameObject.SetActive(false);
		this.doors[3].Closing(true, true);
		this.doors[3].isLocked = true;
		this.roomType = Random.Range(0, 9);
		for (int i = 0; i < this.doors.Count; i++)
		{
			this.doors[i].CloseEndAction = null;
			this.doors[i].Closing(true, true);
		}
		bool[] array = new bool[9];
		switch (this.roomType)
		{
		case 0:
			this.nextRoomWay = 0;
			array[1] = true;
			array[4] = true;
			array[7] = true;
			break;
		case 1:
			this.nextRoomWay = 1;
			array[3] = true;
			array[4] = true;
			array[7] = true;
			break;
		case 2:
			this.nextRoomWay = 2;
			array[5] = true;
			array[4] = true;
			array[7] = true;
			break;
		case 3:
			this.nextRoomWay = 0;
			array[0] = true;
			array[1] = true;
			array[3] = true;
			array[6] = true;
			array[7] = true;
			break;
		case 4:
			this.nextRoomWay = 1;
			array[3] = true;
			array[6] = true;
			array[7] = true;
			break;
		case 5:
			this.nextRoomWay = 2;
			array[5] = true;
			array[7] = true;
			array[8] = true;
			break;
		case 6:
			this.nextRoomWay = 0;
			array[1] = true;
			array[2] = true;
			array[5] = true;
			array[7] = true;
			array[8] = true;
			break;
		case 7:
			this.nextRoomWay = 1;
			array[0] = true;
			array[1] = true;
			array[2] = true;
			array[3] = true;
			array[5] = true;
			array[7] = true;
			array[8] = true;
			break;
		case 8:
			this.nextRoomWay = 2;
			array[0] = true;
			array[1] = true;
			array[2] = true;
			array[3] = true;
			array[5] = true;
			array[6] = true;
			array[7] = true;
			break;
		}
		this.CloseDoorWall();
		this.SetBlockingWall();
		for (int j = 0; j < array.Length; j++)
		{
			Color color = Color.white;
			float num;
			float num2;
			if (array[j])
			{
				color = Color.green;
				num = 1f;
				num2 = 5f;
				this.rooms[j].GetComponent<MeshRenderer>().SetMaterials(SingletoneBehaviour<SystemWinionRoomManager>.Instance.greenMaterials);
			}
			else
			{
				color = Color.red;
				num = 0.5f;
				num2 = 10f;
				this.rooms[j].GetComponent<MeshRenderer>().SetMaterials(SingletoneBehaviour<SystemWinionRoomManager>.Instance.redMaterials);
			}
			for (int k = 0; k < 4; k++)
			{
				this.roomLights[j * 4 + k].color = color;
				this.roomLights[j * 4 + k].intensity = num;
				this.roomLights[j * 4 + k].range = num2;
			}
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0002A19C File Offset: 0x0002839C
	public void CloseDoorWall()
	{
		switch (this.nextRoomWay)
		{
		case 0:
			this.walls[0].SetActive(false);
			this.walls[1].SetActive(true);
			this.walls[2].SetActive(true);
			this.doors[0].transform.parent.parent.gameObject.SetActive(true);
			this.doors[1].transform.parent.parent.gameObject.SetActive(false);
			this.doors[2].transform.parent.parent.gameObject.SetActive(false);
			return;
		case 1:
			this.walls[0].SetActive(true);
			this.walls[1].SetActive(false);
			this.walls[2].SetActive(true);
			this.doors[0].transform.parent.parent.gameObject.SetActive(false);
			this.doors[1].transform.parent.parent.gameObject.SetActive(true);
			this.doors[2].transform.parent.parent.gameObject.SetActive(false);
			return;
		case 2:
			this.walls[0].SetActive(true);
			this.walls[1].SetActive(true);
			this.walls[2].SetActive(false);
			this.doors[0].transform.parent.parent.gameObject.SetActive(false);
			this.doors[1].transform.parent.parent.gameObject.SetActive(false);
			this.doors[2].transform.parent.parent.gameObject.SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0002A3C0 File Offset: 0x000285C0
	public void SetBlockingWall()
	{
		bool[] array = new bool[15];
		switch (this.roomType)
		{
		case 0:
			array[3] = true;
			array[4] = true;
			array[5] = true;
			array[6] = true;
			array[7] = true;
			array[8] = true;
			array[10] = true;
			array[11] = true;
			break;
		case 1:
			array[4] = true;
			array[5] = true;
			array[7] = true;
			array[8] = true;
			array[9] = true;
			array[10] = true;
			array[11] = true;
			array[12] = true;
			break;
		case 2:
			array[6] = true;
			array[7] = true;
			array[9] = true;
			array[11] = true;
			array[12] = true;
			array[13] = true;
			array[14] = true;
			break;
		case 3:
			array[3] = true;
			array[5] = true;
			array[6] = true;
			array[7] = true;
			array[8] = true;
			array[9] = true;
			array[11] = true;
			array[13] = true;
			break;
		case 4:
			array[5] = true;
			array[6] = true;
			array[7] = true;
			array[8] = true;
			array[9] = true;
			array[11] = true;
			array[12] = true;
			array[13] = true;
			array[14] = true;
			break;
		case 5:
			array[4] = true;
			array[6] = true;
			array[7] = true;
			array[8] = true;
			array[9] = true;
			array[10] = true;
			array[12] = true;
			array[14] = true;
			break;
		case 6:
			array[3] = true;
			array[5] = true;
			array[6] = true;
			array[8] = true;
			array[10] = true;
			array[12] = true;
			array[14] = true;
			break;
		case 7:
			array[3] = true;
			array[5] = true;
			array[7] = true;
			array[8] = true;
			array[9] = true;
			array[11] = true;
			array[12] = true;
			array[14] = true;
			break;
		case 8:
			array[3] = true;
			array[5] = true;
			array[6] = true;
			array[7] = true;
			array[9] = true;
			array[11] = true;
			array[12] = true;
			array[14] = true;
			break;
		}
		for (int i = 3; i < array.Length; i++)
		{
			if (array[i])
			{
				this.walls[i].SetActive(false);
			}
			else
			{
				this.walls[i].SetActive(true);
			}
		}
	}

	// Token: 0x04000367 RID: 871
	public bool isPooling;

	// Token: 0x04000368 RID: 872
	public bool firstEnter = true;

	// Token: 0x04000369 RID: 873
	public int roomType;

	// Token: 0x0400036A RID: 874
	private int nextRoomWay;

	// Token: 0x0400036B RID: 875
	public List<GameObject> walls = new List<GameObject>();

	// Token: 0x0400036C RID: 876
	public List<GameObject> rooms = new List<GameObject>();

	// Token: 0x0400036D RID: 877
	public List<Light> roomLights = new List<Light>();

	// Token: 0x0400036E RID: 878
	public List<DoorInteraction> doors = new List<DoorInteraction>();

	// Token: 0x0400036F RID: 879
	public DoorInteraction CorrectLastDoor;

	// Token: 0x04000370 RID: 880
	public GameObject PreviousMaze;
}
