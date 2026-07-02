using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class LastRoomEnter : MonoBehaviour
{
	// Token: 0x0600024A RID: 586 RVA: 0x0002604C File Offset: 0x0002424C
	private void OnEnable()
	{
		this.isEnter = false;
		if (this.EnterRoutine != null)
		{
			base.StopCoroutine(this.EnterRoutine);
		}
		if (this.FirstRoutine != null)
		{
			base.StopCoroutine(this.FirstRoutine);
		}
		if (this.SecondRoutine != null)
		{
			base.StopCoroutine(this.SecondRoutine);
		}
		if (this.KnockRoutine != null)
		{
			base.StopCoroutine(this.KnockRoutine);
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x000260B0 File Offset: 0x000242B0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (this.isEnter)
			{
				return;
			}
			this.isEnter = true;
			if (this.LastHoleEnterCollider)
			{
				LastRoomEnter.firstEnter = false;
				this.EnterRoutine = base.StartCoroutine("EnterColliderEnter");
			}
			if (this.FirstCollider)
			{
				this.FirstRoutine = base.StartCoroutine("FirstColliderEnter");
			}
			if (this.SecondCollider)
			{
				this.SecondRoutine = base.StartCoroutine("SecondColliderEnter");
			}
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000F935 File Offset: 0x0000DB35
	private IEnumerator TurnOnErrorBox()
	{
		int count = SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.GetChild(0).childCount;
		int num;
		for (int i = 0; i < count; i = num + 1)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.01f);
			num = i;
		}
		yield break;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000F93D File Offset: 0x0000DB3D
	private IEnumerator TurnOffErrorBox()
	{
		int count = SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.GetChild(0).childCount;
		int num;
		for (int i = 0; i < count; i = num + 1)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
			yield return new WaitForSeconds(0.01f);
			num = i;
		}
		yield break;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000F945 File Offset: 0x0000DB45
	private IEnumerator EnterColliderEnter()
	{
		MazeBossBug LastBug = SingletoneBehaviour<MazeController>.Instance.MazeLastBug.GetComponent<MazeBossBug>();
		LastBug.tailRigging.weight = 0f;
		LastBug.CanMove = false;
		LastBug.transform.position = this.EnemyWaitingPosition.transform.position;
		LastBug.moveSpeed = 0f;
		LastBug.followIndex = SingletoneBehaviour<MazeController>.Instance.doorSnapper.Count;
		yield return null;
		LastBug.tailRigging.weight = 1f;
		this.KnockRoutine = base.StartCoroutine("DoorKnocking");
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 9;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => LastRoomEnter.firstEnter);
		yield return new WaitForSeconds(1f);
		WhatIsLastDoor.LastDoor.isLocked = false;
		WhatIsLastDoor.LastDoor.Opening(false, false);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DoorImpact, false, 1f, 1f);
		base.StopCoroutine(this.KnockRoutine);
		LastBug.CanMove = true;
		yield break;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000F954 File Offset: 0x0000DB54
	private IEnumerator DoorKnocking()
	{
		yield return new WaitForSeconds(1f);
		MazeBossBug LastBug = SingletoneBehaviour<MazeController>.Instance.MazeLastBug.GetComponent<MazeBossBug>();
		int[] index = new int[] { 34, 35 };
		while (!LastBug.CanMove)
		{
			SoundManager.Instance.Play_SfxSound_2((SoundManager.SfxSound_2)index[Random.Range(0, index.Length)], false, 1f, 1f);
			yield return new WaitForSeconds(Random.Range(0.2f, 1f));
		}
		yield break;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000F95C File Offset: 0x0000DB5C
	private IEnumerator FirstColliderEnter()
	{
		LastRoomEnter.firstEnter = true;
		base.StartCoroutine("TurnOnErrorBox");
		this.parentHole.GetComponent<LongHoleController>().isLastRoom = true;
		SingletoneBehaviour<SkyBoxManager>.Instance.RenderSkybox(SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera, 1);
		if (SingletoneBehaviour<HorrorSceneManager>.Instance.GreenTerrain.activeSelf)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.GreenTerrain.SetActive(false);
		}
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrain.transform.SetParent(this.frontDoor.transform.parent);
		Vector3 vector;
		vector..ctor(100f, 0f, 100f);
		float y = this.parentHole.transform.localRotation.eulerAngles.y;
		if (y <= 0f)
		{
			if (y <= -180f)
			{
				if (y == -270f)
				{
					goto IL_016D;
				}
				if (y != -180f)
				{
					goto IL_01D1;
				}
				goto IL_018F;
			}
			else
			{
				if (y == -90f)
				{
					goto IL_01B1;
				}
				if (y != 0f)
				{
					goto IL_01D1;
				}
			}
		}
		else if (y <= 180f)
		{
			if (y == 90f)
			{
				goto IL_016D;
			}
			if (y != 180f)
			{
				goto IL_01D1;
			}
			goto IL_018F;
		}
		else
		{
			if (y == 270f)
			{
				goto IL_01B1;
			}
			if (y != 360f)
			{
				goto IL_01D1;
			}
		}
		vector..ctor(100f, 0f, -100f);
		Debug.Log("Case : 1");
		goto IL_01D1;
		IL_016D:
		vector..ctor(100f, 0f, 100f);
		Debug.Log("Case : 2");
		goto IL_01D1;
		IL_018F:
		vector..ctor(-100f, 0f, 100f);
		Debug.Log("Case : 3");
		goto IL_01D1;
		IL_01B1:
		vector..ctor(-100f, 0f, -100f);
		Debug.Log("Case : 4");
		IL_01D1:
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrain.transform.localPosition = vector;
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.SetParent(this.frontDoor.transform.parent);
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.localPosition = Vector3.zero;
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.transform.localRotation = Quaternion.identity;
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrain.SetActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.BlueScreenTerrainCenter.SetActive(true);
		this.frontDoor.isLocked = false;
		this.frontDoor.OpenDoor();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 10;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return null;
		yield break;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000F96B File Offset: 0x0000DB6B
	private IEnumerator SecondColliderEnter()
	{
		Debug.Log("Second 문 닫습니다.");
		LastRoomEnter.lastEnter = false;
		SingletoneBehaviour<HorrorSceneManager>.Instance.ActiveReverbFilter(true);
		this.secondDoor.isLocked = false;
		this.secondDoor.Closing(false, false);
		MazeBossBug LastBug = SingletoneBehaviour<MazeController>.Instance.MazeLastBug.GetComponent<MazeBossBug>();
		LastBug.tailRigging.weight = 0f;
		DOVirtual.Float(5f, 0f, 0.5f, delegate(float value)
		{
			LastBug.moveSpeed = value;
		}).OnComplete(delegate
		{
			LastBug.CanMove = false;
			LastBug.moveSpeed = 0f;
			LastBug.transform.position = this.EnemyWaitingPosition.transform.position;
		});
		LastBug.followIndex = SingletoneBehaviour<MazeController>.Instance.doorSnapper.Count;
		LastBug.tailRigging.weight = 1f;
		this.KnockRoutine = base.StartCoroutine("DoorKnocking");
		yield return new WaitUntil(() => LastRoomEnter.lastEnter);
		this.secondDoor.isLocked = false;
		this.secondDoor.Opening(false, false);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DoorImpact, false, 1f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.HitStress, false, 1f, 1f);
		LastBug.CanMove = true;
		LastBug.DestroySelf();
		if (this.KnockRoutine != null)
		{
			base.StopCoroutine(this.KnockRoutine);
		}
		yield break;
	}

	// Token: 0x04000273 RID: 627
	public GameObject parentHole;

	// Token: 0x04000274 RID: 628
	public bool LastHoleEnterCollider;

	// Token: 0x04000275 RID: 629
	public bool FirstCollider;

	// Token: 0x04000276 RID: 630
	public bool SecondCollider;

	// Token: 0x04000277 RID: 631
	public bool secondEnter;

	// Token: 0x04000278 RID: 632
	public GameObject lookTarget;

	// Token: 0x04000279 RID: 633
	public DoorInteraction frontDoor;

	// Token: 0x0400027A RID: 634
	public DoorInteraction secondDoor;

	// Token: 0x0400027B RID: 635
	public List<GameObject> disableObjects;

	// Token: 0x0400027C RID: 636
	public GameObject forParent;

	// Token: 0x0400027D RID: 637
	public GameObject EnemyWaitingPosition;

	// Token: 0x0400027E RID: 638
	public static bool firstEnter;

	// Token: 0x0400027F RID: 639
	public static bool lastEnter;

	// Token: 0x04000280 RID: 640
	private Coroutine EnterRoutine;

	// Token: 0x04000281 RID: 641
	private Coroutine FirstRoutine;

	// Token: 0x04000282 RID: 642
	private Coroutine SecondRoutine;

	// Token: 0x04000283 RID: 643
	private Coroutine KnockRoutine;

	// Token: 0x04000284 RID: 644
	public bool isEnter;
}
