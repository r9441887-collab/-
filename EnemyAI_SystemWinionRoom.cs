using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class EnemyAI_SystemWinionRoom : SingletoneBehaviour<EnemyAI_SystemWinionRoom>
{
	// Token: 0x0600014A RID: 330 RVA: 0x000238EC File Offset: 0x00021AEC
	private void Start()
	{
		EnemyAI_SystemWinionRoom.deadPointChange = false;
		EnemyAI_SystemWinionRoom.Player = SingletoneBehaviour<SystemWinionRoomManager>.Instance.Player;
		base.StartCoroutine("PlayEnemySound");
		base.StartCoroutine("PlayPlayerWarning");
		this.MissionText.text = "";
		this.TargetServerCount = this.ServerList.Count;
		this.TurnOffServerCount = 0;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
	private void Update()
	{
		this.CalcDistance();
		if (!this.startMission)
		{
			return;
		}
		this.CheckServerCount();
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000F10C File Offset: 0x0000D30C
	private IEnumerator PlayEnemySound()
	{
		for (;;)
		{
			yield return new WaitUntil(() => this.CanPlay);
			if (this.CanPlay)
			{
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyHoulingSound + Random.Range(0, 4), false, 1f, Random.Range(0.85f, 1.15f));
				yield return new WaitForSeconds(60f);
			}
		}
		yield break;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000F11B File Offset: 0x0000D31B
	private IEnumerator PlayPlayerWarning()
	{
		for (;;)
		{
			yield return new WaitUntil(() => this.warningFar);
			if (this.warningFar)
			{
				SingletoneBehaviour<PopUpMessage>.Instance.PopFarAway();
				yield return new WaitForSeconds(2f);
				SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
				yield return new WaitForSeconds(Random.Range(1f, 3f));
			}
		}
		yield break;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00023950 File Offset: 0x00021B50
	private void CalcDistance()
	{
		float num = float.MaxValue;
		for (int i = 0; i < this.EnemyList.Count; i++)
		{
			float num2 = Vector3.Distance(EnemyAI_SystemWinionRoom.Player.transform.position, this.EnemyList[i].transform.position);
			if (num > num2)
			{
				num = num2;
				this.minEnemy = this.EnemyList[i].name;
			}
		}
		this.minDistance = num;
		if (this.minDistance >= 60f)
		{
			this.CanPlay = true;
		}
		else
		{
			this.CanPlay = false;
		}
		if (Vector3.Distance(EnemyAI_SystemWinionRoom.Player.transform.position, this.FarAwayCheck.position) >= 130f)
		{
			this.warningFar = true;
			return;
		}
		this.warningFar = false;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000F12A File Offset: 0x0000D32A
	private void CheckServerCount()
	{
		this.MissionText.text = string.Format("({0} / {1})", this.TurnOffServerCount, this.TargetServerCount);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00023A1C File Offset: 0x00021C1C
	public void StartMission()
	{
		this.startMission = true;
		this.NoneTargetServer.SetActive(false);
		for (int i = 0; i < this.ServerList.Count; i++)
		{
			this.ServerList[i].transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00023A74 File Offset: 0x00021C74
	public void ResetMission()
	{
		this.startMission = false;
		this.NoneTargetServer.SetActive(true);
		for (int i = 0; i < this.ServerList.Count; i++)
		{
			this.ServerList[i].transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00023ACC File Offset: 0x00021CCC
	public void TurnOffServer()
	{
		this.TurnOffServerCount++;
		if (this.TurnOffServerCount % 5 == 0)
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.SystemWinionFear2, false, 1f, Random.Range(0.8f, 1.2f));
			this.EnemyList[2].ReadyFollowPlayer();
		}
		int turnOffServerCount = this.TurnOffServerCount;
		int num = this.TargetServerCount - 1;
		if (this.TurnOffServerCount == this.TargetServerCount)
		{
			base.StartCoroutine("EndingChapter3Routine");
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000F157 File Offset: 0x0000D357
	private IEnumerator EndingChapter3Routine()
	{
		EnemyAI_SystemWinionRoom.deadPointChange = true;
		yield return new WaitForSeconds(2f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.ErrorPopUpMessage, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.ShakeCamera(1f);
		for (int i = 0; i < this.otherObjects.Count; i++)
		{
			this.otherObjects[i].SetActive(false);
		}
		for (int j = 0; j < this.EnemyList.Count - 1; j++)
		{
			this.EnemyList[j].gameObject.SetActive(false);
		}
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 7;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		this.EnemyList[2].SetMustKill(true);
		this.EnemyList[2].ReadyFollowPlayer();
		this.ExitObject.SetActive(true);
		this.EnterDoor.poolDoor = true;
		this.EnterDoor.isLocked = false;
		this.EnterDoor.Opening(false, false);
		this.EnterDoor.isLocked = true;
		yield break;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00023B50 File Offset: 0x00021D50
	public void GameEnd()
	{
		this.SecondDoor.isLocked = true;
		this.EnemyList[2].gameObject.SetActive(false);
		this.EnterDoor.isLocked = false;
		this.EnterDoor.Closing(false, false);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		DOVirtual.Float(0f, 1f, 3f, delegate(float f)
		{
		}).OnComplete(delegate
		{
			this.Black.DOFade(1f, 1f).OnComplete(delegate
			{
				Debug.Log("Chapter 3 Clear!");
				Events.StartAutoEvent = true;
				Events.AutoChapterIndex = 3;
				Events.AutoEventIndex = 1;
				SceneLoader.LoadScene("Chapter 02_EunBin", false, false);
			});
		});
	}

	// Token: 0x040001A6 RID: 422
	public int TargetServerCount;

	// Token: 0x040001A7 RID: 423
	public int TurnOffServerCount;

	// Token: 0x040001A8 RID: 424
	public TextMeshPro MissionText;

	// Token: 0x040001A9 RID: 425
	public List<GameObject> ServerList;

	// Token: 0x040001AA RID: 426
	public GameObject NoneTargetServer;

	// Token: 0x040001AB RID: 427
	public List<BallCreature_AI> EnemyList;

	// Token: 0x040001AC RID: 428
	public bool startMission;

	// Token: 0x040001AD RID: 429
	private static GameObject Player;

	// Token: 0x040001AE RID: 430
	public float minDistance;

	// Token: 0x040001AF RID: 431
	public string minEnemy;

	// Token: 0x040001B0 RID: 432
	public Transform FarAwayCheck;

	// Token: 0x040001B1 RID: 433
	public bool warningFar;

	// Token: 0x040001B2 RID: 434
	public static bool deadPointChange;

	// Token: 0x040001B3 RID: 435
	public bool CanPlay;

	// Token: 0x040001B4 RID: 436
	public List<GameObject> otherObjects;

	// Token: 0x040001B5 RID: 437
	public GameObject ExitObject;

	// Token: 0x040001B6 RID: 438
	public DoorInteraction EnterDoor;

	// Token: 0x040001B7 RID: 439
	public DoorInteraction SecondDoor;

	// Token: 0x040001B8 RID: 440
	public CanvasGroup Black;
}
