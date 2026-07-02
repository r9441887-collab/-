using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class Last3D_Manager : SingletoneBehaviour<Last3D_Manager>
{
	// Token: 0x060004EB RID: 1259 RVA: 0x000111EA File Offset: 0x0000F3EA
	private void Start()
	{
		this.originPositions = new List<Vector3>();
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000111F7 File Offset: 0x0000F3F7
	public void EndLastChase()
	{
		base.StartCoroutine("AppearSkyEnemy");
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00011205 File Offset: 0x0000F405
	private IEnumerator AppearSkyEnemy()
	{
		LastCreature_AI.EnemyDie = false;
		this.FirstMaze.SetActive(false);
		this.SecondMaze.SetActive(true);
		this.originPositions.Clear();
		for (int i = 0; i < this.DebrisPiece.transform.childCount; i++)
		{
			this.originPositions.Add(this.DebrisPiece.transform.GetChild(i).position);
		}
		this.SkyEnemy.transform.localPosition = new Vector3(0f, 9f, -29f);
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 11;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 126;
		yield return new WaitForSeconds(1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, Random.Range(0.85f, 1.15f));
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, Random.Range(0.85f, 1.15f));
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, Random.Range(0.85f, 1.15f));
		yield return new WaitForSeconds(2f);
		Vector3 camPosition = SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.gameObject.transform.localPosition;
		SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.enabled = false;
		SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.gameObject.transform.position = this.EnemyAppearCamPosition.position;
		SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.gameObject.transform.rotation = this.EnemyAppearCamPosition.rotation;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, Random.Range(0.85f, 1.15f));
		yield return new WaitForSeconds(2f);
		this.DebrisParticle.Play();
		yield return new WaitForSeconds(1f);
		this.DebrisObject.SetActive(false);
		this.DebrisPiece.SetActive(true);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 1f, 1f);
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLocalMove(this.SkyEnemy.transform, new Vector3(0f, 2.7f, -21.5f), 0.3f, false).SetEase(Ease.Linear));
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		HorrorSceneManager.dialogueNum = 12;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitForSeconds(2f);
		base.StartCoroutine("BGMSound");
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLocalMove(this.SkyEnemy.transform, new Vector3(0f, 2f, -20.5f), 2f, false).SetEase(Ease.Linear));
		SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.gameObject.transform.localPosition = camPosition;
		SingletoneBehaviour<HorrorSceneManager>.Instance.FirstCamera.enabled = true;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 80;
		this.DebrisObject.SetActive(true);
		this.DebrisPiece.SetActive(false);
		for (int j = 0; j < this.DebrisPiece.transform.childCount; j++)
		{
			this.DebrisPiece.transform.GetChild(j).position = this.originPositions[j];
		}
		this.SkyEnemy.transform.localPosition = new Vector3(0f, 9f, -29f);
		HorrorSceneManager.CanOpenOption = true;
		yield break;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00011214 File Offset: 0x0000F414
	private IEnumerator BGMSound()
	{
		this.BGMStop = false;
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.4f, 1f);
		yield return new WaitForSeconds(0.4f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.6f, 1f);
		yield return new WaitForSeconds(0.4f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.8f, 1f);
		yield return new WaitForSeconds(0.4f);
		do
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 1f, 1f);
			yield return new WaitForSeconds(0.4f);
		}
		while (!this.BGMStop);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.8f, 1f);
		yield return new WaitForSeconds(0.4f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.6f, 1f);
		yield return new WaitForSeconds(0.4f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.4f, 1f);
		yield return new WaitForSeconds(0.4f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.LastBGMSound, false, 0.2f, 1f);
		yield return new WaitForSeconds(0.4f);
		yield break;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00011223 File Offset: 0x0000F423
	public void OpenSecondMaze()
	{
		this.SecondMazeDoor.OpenDoor();
		DoorWingAuto.StartWing = true;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00011236 File Offset: 0x0000F436
	public void PlyaerScale()
	{
		this.ScaleTween = DOVirtual.Float(0f, 1f, 10f, delegate(float f)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.localScale = Vector3.one * (0.6f * f + 1f);
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera.fieldOfView = Mathf.Lerp(80f, 60f, f);
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerOutlineCamera.fieldOfView = Mathf.Lerp(80f, 60f, f);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<CapsuleCollider>().radius = 0.5f * f + 0.5f;
		});
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00011271 File Offset: 0x0000F471
	public void FirstEnemyAppear()
	{
		base.StartCoroutine("FirstEnemyAppearRoutine");
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001127F File Offset: 0x0000F47F
	private IEnumerator FirstEnemyAppearRoutine()
	{
		this.FirstEnemyPosition = this.FirstEnemy.transform.position;
		this.FirstEnemy.SetActive(true);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 0.5f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 126;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLookAt(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform, this.FirstEnemy.transform.position, 0.5f, AxisConstraint.None, null));
		yield return new WaitForSeconds(2f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		yield return new WaitForSeconds(1f);
		this.FirstEnemy.GetComponent<LastCreature_AI>().StartMovement();
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 80;
		yield break;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001128E File Offset: 0x0000F48E
	public void SecondEnemyAppear()
	{
		base.StartCoroutine("SecondEnemyAppearRoutine");
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001129C File Offset: 0x0000F49C
	private IEnumerator SecondEnemyAppearRoutine()
	{
		this.WrongSpeedPad.SetActive(false);
		this.CorrectSpeedPad.SetActive(true);
		this.SecondEnemy.SetActive(true);
		this.SecondEnemy.transform.localPosition = new Vector3(125f, 0f, -45f);
		this.SecondEnemy.GetComponent<LastCreature_AI>().StartMovement();
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 0.5f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		yield break;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x000331BC File Offset: 0x000313BC
	public void FirstEnemyDead()
	{
		this.FirstEnemy.GetComponent<LastCreature_AI>().StopMovement();
		this.FirstCloseDoor.isLocked = false;
		this.FirstCloseDoor.Closing(false, false);
		this.FirstCloseDoor.CloseEndAction = delegate
		{
			this.FirstEnemy.SetActive(false);
			this.FirstEnemy.transform.position = this.FirstEnemyPosition;
			this.FirstCloseDoor.isLocked = true;
			this.FirstCloseDoor.CloseEndAction = null;
		};
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0003320C File Offset: 0x0003140C
	public void ThirdEnemyAppear()
	{
		this.ThirdEnemy.transform.localPosition = new Vector3(176f, 0f, -54f);
		this.ThirdEnemy.SetActive(true);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 0.5f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000112AB File Offset: 0x0000F4AB
	public void SecondServerOff()
	{
		base.StartCoroutine("GameOverRoutine");
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000112B9 File Offset: 0x0000F4B9
	private IEnumerator GameOverRoutine()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		LastCreature_AI.EnemyDie = true;
		LastCreature_AI.EnemyKilledPlayer = true;
		this.AttackStop = true;
		DoorWingAuto.StartWing = false;
		yield return new WaitForSeconds(10f);
		this.BGMStop = true;
		yield return new WaitForSeconds(2f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.FadeOutBlack(1.5f, null);
		yield return new WaitForSeconds(2.5f);
		Events.StartAutoEvent = true;
		Events.AutoChapterIndex = 3;
		Events.AutoEventIndex = 11;
		SceneLoader.LoadScene("Chapter 02_EunBin", true, false);
		yield break;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00033290 File Offset: 0x00031490
	public void SetDefault()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.MinimapObject.SetActive(false);
		this.FirstMaze.SetActive(true);
		this.SecondMaze.SetActive(false);
		this.SkyLine.SetActive(true);
		base.transform.GetChild(0).gameObject.SetActive(true);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x000332E8 File Offset: 0x000314E8
	public void Reset()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.MinimapObject.SetActive(false);
		this.ScaleTween.Kill(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.localScale = Vector3.one;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera.fieldOfView = 80f;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerOutlineCamera.fieldOfView = 80f;
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<CapsuleCollider>().radius = 0.5f;
		this.FirstEnemy.SetActive(false);
		this.SecondEnemy.SetActive(false);
		this.ThirdEnemy.SetActive(false);
		this.LastEnemy.SetActive(false);
		this.doors[0].isLocked = false;
		this.doors[0].Closing(true, true);
		this.doors[0].isLocked = true;
		this.doors[1].isLocked = false;
		this.doors[1].Opening(true, true);
		this.doors[1].isLocked = true;
		this.FirstEnemy.transform.localPosition = new Vector3(31f, 1f, -32f);
		this.SecondEnemy.transform.localPosition = new Vector3(125f, 0f, -45f);
		for (int i = 0; i < this.triggers.Count; i++)
		{
			this.triggers[i].firstEnter = true;
		}
		this.WrongSpeedPad.SetActive(true);
		this.CorrectSpeedPad.SetActive(false);
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00033494 File Offset: 0x00031694
	public void LastEnemyAppear()
	{
		this.AttackStop = false;
		SingletoneBehaviour<HorrorSceneManager>.Instance.MinimapObject.SetActive(true);
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 13;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		base.StartCoroutine("LastEnemyRoutine");
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x000112C8 File Offset: 0x0000F4C8
	private IEnumerator LastEnemyRoutine()
	{
		while (!this.AttackStop)
		{
			LastCreature_AI.ReadyAttack = false;
			this.LastEnemy.SetActive(true);
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
			yield return new WaitUntil(() => LastCreature_AI.ReadyAttack);
			this.LastEnemy.SetActive(false);
			yield return new WaitForSeconds(Random.Range(1f, 3f));
		}
		this.LastEnemy.SetActive(false);
		yield break;
	}

	// Token: 0x04000582 RID: 1410
	public GameObject FirstMaze;

	// Token: 0x04000583 RID: 1411
	public GameObject SecondMaze;

	// Token: 0x04000584 RID: 1412
	public GameObject SkyLine;

	// Token: 0x04000585 RID: 1413
	public GameObject SkyEnemy;

	// Token: 0x04000586 RID: 1414
	public Transform EnemyAppearCamPosition;

	// Token: 0x04000587 RID: 1415
	public Transform SkyEnemyAppearPosition;

	// Token: 0x04000588 RID: 1416
	public GameObject DebrisObject;

	// Token: 0x04000589 RID: 1417
	public GameObject DebrisPiece;

	// Token: 0x0400058A RID: 1418
	private List<Vector3> originPositions;

	// Token: 0x0400058B RID: 1419
	public ParticleSystem DebrisParticle;

	// Token: 0x0400058C RID: 1420
	public GameObject LookTargetObject;

	// Token: 0x0400058D RID: 1421
	public bool BGMStop;

	// Token: 0x0400058E RID: 1422
	public bool AttackStop;

	// Token: 0x0400058F RID: 1423
	public DoorInteraction SecondMazeDoor;

	// Token: 0x04000590 RID: 1424
	public Tween ScaleTween;

	// Token: 0x04000591 RID: 1425
	private Vector3 FirstEnemyPosition;

	// Token: 0x04000592 RID: 1426
	public GameObject FirstEnemy;

	// Token: 0x04000593 RID: 1427
	public GameObject SecondEnemy;

	// Token: 0x04000594 RID: 1428
	public GameObject WrongSpeedPad;

	// Token: 0x04000595 RID: 1429
	public GameObject CorrectSpeedPad;

	// Token: 0x04000596 RID: 1430
	public DoorInteraction FirstCloseDoor;

	// Token: 0x04000597 RID: 1431
	public GameObject ThirdEnemy;

	// Token: 0x04000598 RID: 1432
	public List<Last3D_Trigger> triggers = new List<Last3D_Trigger>();

	// Token: 0x04000599 RID: 1433
	public List<DoorInteraction> doors = new List<DoorInteraction>();

	// Token: 0x0400059A RID: 1434
	public GameObject LastEnemy;
}
