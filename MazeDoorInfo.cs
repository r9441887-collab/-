using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class MazeDoorInfo : MonoBehaviour
{
	// Token: 0x060002EB RID: 747 RVA: 0x00028C94 File Offset: 0x00026E94
	public void OpenDoorAndCreateHole()
	{
		if (this.door.isLocked)
		{
			SingletoneBehaviour<DoorSound>.Instance.LockDoorSound.Play();
			return;
		}
		if (this.nextHole == null)
		{
			Vector3 zero = Vector3.zero;
			Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			if (this.doorInfo == DoorInfo.Left || this.doorInfo == DoorInfo.Front)
			{
				quaternion = Quaternion.Euler(new Vector3(0f, -90f, 0f));
			}
			this.nextHole = SingletoneBehaviour<MazeController>.Instance.GenerateMaze(zero, quaternion, base.transform);
			this.nextHole.GetComponent<LongHoleController>().SetDefault(false);
			this.nextHole.transform.SetParent(base.transform);
			this.nextHole.transform.localPosition = zero;
			this.nextHole.transform.localRotation = quaternion;
			this.nextHole.transform.SetParent(SingletoneBehaviour<MazeController>.Instance.transform);
			int[] passwords = SingletoneBehaviour<MazeController>.Instance.passwords;
			if (passwords.Length > SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex)
			{
				if (passwords[SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex] == this.roomIndex)
				{
					Debug.Log("+++++정답입니다.");
					this.nextHole.GetComponent<LongHoleController>().SetCorrectSetting();
					this.nextHole.GetComponent<LongHoleController>().SetHorrorSetting();
				}
				else
				{
					Debug.Log("-----오답입니다.");
					this.nextHole.GetComponent<LongHoleController>().SetWrongSetting();
				}
				this.previousHole.GetComponent<LongHoleController>().OtherHoles.Add(this.nextHole);
			}
		}
		this.door.OpenDoor();
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00028E30 File Offset: 0x00027030
	public void OpenDoorEndRoom()
	{
		if (this.door.isLocked)
		{
			SingletoneBehaviour<DoorSound>.Instance.LockDoorSound.Play();
			return;
		}
		if (this.nextHole == null)
		{
			Vector3 zero = Vector3.zero;
			Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			if (this.doorInfo == DoorInfo.Left || this.doorInfo == DoorInfo.Front)
			{
				quaternion = Quaternion.Euler(new Vector3(0f, -90f, 0f));
			}
			this.nextHole = SingletoneBehaviour<MazeController>.Instance.GenerateMaze(zero, quaternion, base.transform);
			LongHoleController component = this.nextHole.GetComponent<LongHoleController>();
			component.randomTexting = true;
			if (this.isCorrectDoor)
			{
				component.SetCorrectSetting();
				component.SetHorrorSetting();
				component.ArrowObject.SetActive(true);
				component.EnemyDoorSnapper.SetActive(true);
			}
			else
			{
				component.SetEndRoom();
				component.GetComponent<MazeHoleLight>().SetLight(new Color32(byte.MaxValue, 0, 30, 0), true);
			}
			this.previousHole.GetComponent<LongHoleController>().OtherHoles.Add(this.nextHole);
		}
		this.door.OpenDoor();
	}

	// Token: 0x04000326 RID: 806
	public int roomIndex;

	// Token: 0x04000327 RID: 807
	public DoorInfo doorInfo;

	// Token: 0x04000328 RID: 808
	public DoorInteraction door;

	// Token: 0x04000329 RID: 809
	public GameObject previousHole;

	// Token: 0x0400032A RID: 810
	public GameObject nextHole;

	// Token: 0x0400032B RID: 811
	public bool isCorrectDoor;
}
