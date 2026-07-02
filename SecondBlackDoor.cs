using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class SecondBlackDoor : MonoBehaviour
{
	// Token: 0x06000365 RID: 869 RVA: 0x000102F9 File Offset: 0x0000E4F9
	private void Start()
	{
		this.StartPosition = base.transform.position;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001030C File Offset: 0x0000E50C
	public void OpenFirstDoor()
	{
		this.firstDoor.OpenDoor();
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00010319 File Offset: 0x0000E519
	private IEnumerator DarkSetting()
	{
		List<Material> light = SingletoneBehaviour<MazeController>.Instance.LightMode.floorMaterials;
		List<Material> dark = SingletoneBehaviour<MazeController>.Instance.DarkMode.floorMaterials;
		this.Floor.SetMaterials(dark);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(6, 6, 6, 0));
		yield return new WaitForSeconds(0.4f);
		this.Floor.SetMaterials(light);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(50, 50, 50, 0));
		yield return new WaitForSeconds(0.4f);
		this.Floor.SetMaterials(dark);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(6, 6, 6, 0));
		yield return new WaitForSeconds(0.4f);
		this.Floor.SetMaterials(light);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(50, 50, 50, 0));
		yield return new WaitForSeconds(0.4f);
		yield break;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0002B1B4 File Offset: 0x000293B4
	public void OpenSecondBlackDoor()
	{
		if (this.secondDoor.isLocked)
		{
			SingletoneBehaviour<DoorSound>.Instance.LockDoorSound.Play();
			return;
		}
		if (this.isOpen)
		{
			return;
		}
		this.isOpen = true;
		this.longHole.SetActive(true);
		this.secondDoor.OpenDoor();
		this.secondDoor.isLocked = true;
		this.Player.transform.SetParent(this.AirAreaParent);
		this.longHole.transform.SetParent(this.AirAreaParent);
		base.transform.SetParent(this.AirAreaParent);
		this.AirAreaParent.transform.position += new Vector3(0f, 30f, 0f);
		this.Player.transform.SetParent(null);
		this.longHole.transform.SetParent(null);
		base.transform.SetParent(null);
		this.AirAreaParent.transform.position -= new Vector3(0f, 30f, 0f);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0002B2DC File Offset: 0x000294DC
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && !this.BigToSmallSize)
		{
			float num = Vector3.Distance(this.secondPosition.transform.position, this.Player.transform.position);
			float num2 = this.secondPosition.transform.position.x - this.firstPosition.transform.position.x;
			this.size = Mathf.Clamp(num, 1f, num2);
			this.size = Mathf.Min(this.size, this.minScale);
			this.minScale = Mathf.Min(this.size, this.minScale);
			this.Room.transform.localScale = Vector3.one * this.size;
			if (Mathf.Approximately(this.size, 1f))
			{
				this.SetSizeOne();
				this.firstDoor.Closing(false, false);
				this.firstDoor.isLocked = true;
				this.secondDoor.isLocked = false;
			}
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00010328 File Offset: 0x0000E528
	private void SetSizeOne()
	{
		this.BigToSmallSize = true;
		this.Room.transform.localScale = Vector3.one;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0002B3F8 File Offset: 0x000295F8
	public void Reset()
	{
		base.transform.localScale = Vector3.one * 4f;
		base.transform.position = this.StartPosition;
		this.isOpen = false;
		this.BigToSmallSize = false;
		this.size = 0f;
		this.minScale = 10f;
		this.firstDoor.poolDoor = true;
		this.firstDoor.isLocked = false;
		this.firstDoor.Opening(true, true);
		this.firstDoor.poolDoor = false;
		this.longHole.transform.position -= new Vector3(0f, 30f, 0f);
		this.secondDoor.isLocked = true;
	}

	// Token: 0x0400039E RID: 926
	public GameObject Player;

	// Token: 0x0400039F RID: 927
	public GameObject Room;

	// Token: 0x040003A0 RID: 928
	public float size;

	// Token: 0x040003A1 RID: 929
	public GameObject firstPosition;

	// Token: 0x040003A2 RID: 930
	public GameObject secondPosition;

	// Token: 0x040003A3 RID: 931
	private bool BigToSmallSize;

	// Token: 0x040003A4 RID: 932
	public DoorInteraction firstDoor;

	// Token: 0x040003A5 RID: 933
	public DoorInteraction secondDoor;

	// Token: 0x040003A6 RID: 934
	public float minScale = 10f;

	// Token: 0x040003A7 RID: 935
	public GameObject longHole;

	// Token: 0x040003A8 RID: 936
	public Transform AirAreaParent;

	// Token: 0x040003A9 RID: 937
	public MeshRenderer Floor;

	// Token: 0x040003AA RID: 938
	[Header("Reset을 위한 데이터")]
	public Vector3 StartPosition;

	// Token: 0x040003AB RID: 939
	public bool isOpen;
}
