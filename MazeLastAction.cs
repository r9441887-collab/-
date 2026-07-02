using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class MazeLastAction : MonoBehaviour
{
	// Token: 0x0600031B RID: 795 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!this.firstEnter)
			{
				return;
			}
			this.firstEnter = false;
			base.StartCoroutine("LastActionRoutine");
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00010008 File Offset: 0x0000E208
	private void OnEnable()
	{
		this.FrontPad.SetActive(false);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00010008 File Offset: 0x0000E208
	private void OnDisable()
	{
		this.FrontPad.SetActive(false);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00010016 File Offset: 0x0000E216
	private IEnumerator LastActionRoutine()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.ClickUI_Dark, false, 1f, 1f);
		SingletoneBehaviour<MyPcWindowResolution>.Instance.NoSignal.SetActive(false);
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 7;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		DOVirtual.Float(0f, 1f, 5f, null).OnComplete(delegate
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 0.5f, 1f);
		});
		DOVirtual.Float(0f, 1f, 7f, null).OnComplete(delegate
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 0.7f, 1f);
		});
		DOVirtual.Float(0f, 1f, 10f, null).OnComplete(delegate
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, 1f);
		});
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.KnockWallSound, false, 1f, 1f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.DebrisSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 126;
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLookAt(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform, this.firstLookTarget.transform.position, 1f, AxisConstraint.None, null));
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 8;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.AsphaltBombSound, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
		SingletoneBehaviour<MazeController>.Instance.MazeLastBug.SetActive(true);
		SingletoneBehaviour<MazeController>.Instance.MazeLastBug.transform.position = this.firstLookTarget.transform.position;
		yield return TweenExtensions.WaitForCompletion(DOVirtual.Int(0, 1, 2.2f, delegate(int value)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.LookAt(this.firstLookTarget.transform);
		}));
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 80;
		this.FrontPad.SetActive(true);
		this.FrontDoor.door.isLocked = false;
		this.FrontDoor.OpenDoorAndCreateHole();
		this.FrontDoor.nextHole.GetComponent<LongHoleController>().AutoOpenObject.SetActive(true);
		this.FrontDoor.nextHole.GetComponent<LongHoleController>().SetNextCorrectDoor();
		this.FrontDoor.nextHole.GetComponent<LongHoleController>().DoorActive(false);
		yield break;
	}

	// Token: 0x0400034D RID: 845
	public GameObject firstLookTarget;

	// Token: 0x0400034E RID: 846
	public GameObject Enemy;

	// Token: 0x0400034F RID: 847
	public MazeDoorInfo FrontDoor;

	// Token: 0x04000350 RID: 848
	public GameObject FrontPad;

	// Token: 0x04000351 RID: 849
	public bool firstEnter = true;
}
