using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class DoorWingAuto : MonoBehaviour
{
	// Token: 0x06000077 RID: 119 RVA: 0x0000E805 File Offset: 0x0000CA05
	private void Start()
	{
		this.MyDoor.openDuration = 200f;
		this.MyDoor.closeDuration = 300f;
		this.doorSpeed = 1f;
		DoorWingAuto.StartWing = false;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0001FD88 File Offset: 0x0001DF88
	private void Update()
	{
		if (DoorWingAuto.StartWing)
		{
			if (!this._startWing)
			{
				this._startWing = true;
				base.StartCoroutine("DoorWing");
				return;
			}
		}
		else if (this._startWing)
		{
			this._startWing = false;
			base.StopCoroutine("DoorWing");
			this.MyDoor.isLocked = true;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000E838 File Offset: 0x0000CA38
	private IEnumerator DoorWing()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.doorSpeed);
			yield return new WaitUntil(() => this.waitWing);
			if (Vector3.Distance(base.transform.position, SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position) >= 10f)
			{
				if (Random.Range(0, 10) <= 5)
				{
					this.MyDoor.poolDoor = true;
					this.MyDoor.isLocked = false;
					this.MyDoor.OpenDoor();
				}
				if (!DoorWingAuto.StartWing)
				{
					break;
				}
			}
		}
		yield break;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000E847 File Offset: 0x0000CA47
	public void CloseDoor()
	{
		this.waitWing = false;
		this.MyDoor.isLocked = false;
		this.MyDoor.Closing(false, false);
		this.MyDoor.isLocked = true;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000E875 File Offset: 0x0000CA75
	public void CloseDoor_Exit()
	{
		this.waitWing = true;
	}

	// Token: 0x040000B3 RID: 179
	public static bool StartWing;

	// Token: 0x040000B4 RID: 180
	public bool _startWing;

	// Token: 0x040000B5 RID: 181
	public DoorInteraction MyDoor;

	// Token: 0x040000B6 RID: 182
	public GameObject FrontObject;

	// Token: 0x040000B7 RID: 183
	public GameObject BackObject;

	// Token: 0x040000B8 RID: 184
	public float doorSpeed = 0.1f;

	// Token: 0x040000B9 RID: 185
	public bool waitWing = true;
}
