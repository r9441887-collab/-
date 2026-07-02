using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class MazeHoleGhost : MonoBehaviour
{
	// Token: 0x060002EE RID: 750 RVA: 0x0000FDDB File Offset: 0x0000DFDB
	public void SetFirst(LongHoleController controller)
	{
		base.gameObject.SetActive(true);
		this.FirstGhost.SetActive(true);
		this.lookAtPlayer = true;
		this.Controller = controller;
		this.firstGhost = true;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000FE0A File Offset: 0x0000E00A
	public void SetSecond(LongHoleController controller)
	{
		base.gameObject.SetActive(true);
		this.Controller = controller;
		this.secondGhost = true;
		this.isSmile = false;
		this.SecondGhostAnimator.PlayAnimation("HorrorFaceIdle", false);
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00028F5C File Offset: 0x0002715C
	public void Clear()
	{
		this.FirstGhost.SetActive(false);
		this.SecondGhost.SetActive(false);
		this.lookAtPlayer = false;
		this.firstGhost = false;
		this.secondGhost = false;
		this.isSmile = false;
		this.Controller = null;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000FE3E File Offset: 0x0000E03E
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Entering");
			if (this.secondGhost)
			{
				this.SecondGhost.SetActive(true);
			}
			this.lookAtPlayer = true;
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00028FB0 File Offset: 0x000271B0
	private void Update()
	{
		if (this.lookAtPlayer)
		{
			if (this.firstGhost)
			{
				this.LookAtPlayer(this.FirstGhost);
			}
			if (this.secondGhost)
			{
				this.LookAtPlayer(this.SecondGhost);
				if (!this.isSmile && Vector3.Distance(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position, this.SecondGhost.transform.position) > 3.5f)
				{
					this.isSmile = true;
					this.SecondGhostAnimator.PlayAnimation("HorrorFaceSmile", false);
					this.SecondGhostAnimator.EndFrameAction = delegate
					{
						this.SecondGhostAnimator.PlayAnimation("HorrorFaceSmileStop", false);
						this.LastDoor.SetActive(true);
						this.lastDoorInfo.door.isLocked = false;
						this.lastDoorInfo.OpenDoorAndCreateHole();
						this.Controller.previousHole.SetActive(false);
						this.SecondGhostAnimator.EndFrameAction = null;
					};
				}
			}
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00029058 File Offset: 0x00027258
	private void LookAtPlayer(GameObject Ghost)
	{
		Ghost.transform.LookAt(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform);
		Vector3 localEulerAngles = Ghost.transform.localEulerAngles;
		localEulerAngles.x = 0f;
		localEulerAngles.z = 0f;
		Ghost.transform.localRotation = Quaternion.Euler(localEulerAngles);
	}

	// Token: 0x0400032C RID: 812
	public GameObject FirstGhost;

	// Token: 0x0400032D RID: 813
	public GameObject SecondGhost;

	// Token: 0x0400032E RID: 814
	public CustomAnimator SecondGhostAnimator;

	// Token: 0x0400032F RID: 815
	public bool lookAtPlayer;

	// Token: 0x04000330 RID: 816
	public LongHoleController Controller;

	// Token: 0x04000331 RID: 817
	public bool firstGhost;

	// Token: 0x04000332 RID: 818
	public bool secondGhost;

	// Token: 0x04000333 RID: 819
	public GameObject LastDoor;

	// Token: 0x04000334 RID: 820
	public MazeDoorInfo lastDoorInfo;

	// Token: 0x04000335 RID: 821
	public bool isSmile;
}
