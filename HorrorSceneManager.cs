using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Token: 0x02000044 RID: 68
public class HorrorSceneManager : SingletoneBehaviour<HorrorSceneManager>
{
	// Token: 0x060001A6 RID: 422 RVA: 0x000246C8 File Offset: 0x000228C8
	public void DestroyCube()
	{
		ParticleSystem.MainModule mainModule = this.firstParticle.main;
		DOVirtual.Float(2f, 0.1f, 0.5f, delegate(float value)
		{
			mainModule.startSize = new ParticleSystem.MinMaxCurve(value);
			this.firstParticle.transform.parent.localScale = Vector3.one * value;
		}).SetEase(Ease.InOutQuad).OnComplete(delegate
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Explosion, false, 1f, 1f);
			this.secondParticle.Play();
		});
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000F3FD File Offset: 0x0000D5FD
	public void ActiveReverbFilter(bool value)
	{
		this.caveFilter.enabled = value;
		this.caveFilterRun.enabled = value;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000F417 File Offset: 0x0000D617
	public void ActiveBlackReverbFilter(bool value)
	{
		this.blackRoomFilter.enabled = value;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000F425 File Offset: 0x0000D625
	private void Start()
	{
		DataManager.LoadPrefs();
		this.PlayerCamera = this.FirstCamera.GetComponent<Camera>();
		this.SettingsVolum(this.volume);
		this.objectPoolingSystem.InitPooling();
		this.firstObjectDrop = true;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0002472C File Offset: 0x0002292C
	public void SetChapter(HorrorChapter chapter)
	{
		SingletoneBehaviour<CommandLineController>.Instance.ClearConsole();
		SingletoneBehaviour<MyPcWindowResolution>.Instance.NoSignal.SetActive(false);
		HorrorSceneManager.GreenDoorOpen = true;
		HorrorSceneManager.SystemDoorOpen = false;
		switch (chapter)
		{
		case HorrorChapter.Chapter0:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(5f);
			base.StartCoroutine("SetZeroChapter");
			return;
		case HorrorChapter.Chapter1:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(5f);
			base.StartCoroutine("SetFirstChapter");
			return;
		case HorrorChapter.Chapter2:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(5f);
			base.StartCoroutine("SetSecondChapter");
			return;
		case HorrorChapter.Chapter3:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(6.5f);
			base.StartCoroutine("SetThirdChapter");
			return;
		case HorrorChapter.Chapter4:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(6.5f);
			base.StartCoroutine("SetFourChapter");
			return;
		case HorrorChapter.Chapter5:
			this.Player.GetComponent<FirstPersonMovement>().SetSpeed(6.5f);
			return;
		case HorrorChapter.Chapter2_First:
			base.StartCoroutine("SetSecondChapter_Start");
			return;
		default:
			return;
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000F45B File Offset: 0x0000D65B
	public void PlayerCameraShake(float power = 5f, float duration = 1f)
	{
		ShortcutExtensions.DOShakeRotation(this.FirstCamera.transform.parent, duration, Vector3.one * power, 50, 90f, true, ShakeRandomnessMode.Harmonic);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00024850 File Offset: 0x00022A50
	public void PlayerCameraActive(bool value, bool SetZero = true)
	{
		if (!this.volumeProfile)
		{
			throw new NullReferenceException("VolumeProfile");
		}
		if (!this.volumeProfile.TryGet<MotionBlur>(ref this._motionBlur))
		{
			throw new NullReferenceException("_motionBlur");
		}
		this._motionBlur.active = value;
		if (value)
		{
			this.Player.GetComponent<FirstPersonMovement>().enabled = true;
			this.FirstCamera.FixRotation();
			this.FirstCamera.enabled = true;
			this.FirstCamera.sensitivity = 2f;
			return;
		}
		this.FirstCamera.enabled = false;
		this.FirstCamera.sensitivity = 0f;
		if (SetZero)
		{
			ShortcutExtensions.DOLocalRotate(this.FirstCamera.transform, Vector3.zero, 1f, RotateMode.Fast);
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000F488 File Offset: 0x0000D688
	public void PlayerMovementActive(bool value)
	{
		FirstPersonMovement component = this.Player.GetComponent<FirstPersonMovement>();
		component.enabled = value;
		component.ClearVelocity();
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000F4A1 File Offset: 0x0000D6A1
	private IEnumerator SetInitializec()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000F4A9 File Offset: 0x0000D6A9
	private IEnumerator SetZeroChapter()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter0;
		yield return null;
		this.firstObjectDrop = false;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
		this.Player.GetComponent<FirstPersonMovement>().SetStartPoint();
		yield return base.StartCoroutine("SetFalseFirstChapter");
		yield return base.StartCoroutine("SetFalseSecondChapter");
		yield return base.StartCoroutine("SetFalseThirdChapter");
		yield return base.StartCoroutine("SetFalseFourChapter");
		this.GreenTerrain.SetActive(true);
		this.Fish.SetActive(false);
		this.Map.SetActive(true);
		this.SecondBlackDoor.secondDoor.isLocked = true;
		RenderSettings.fogDensity = 0f;
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 0;
		yield return new WaitUntil(() => MyPcWindowResolution.VolumeTweenEnd);
		MyPcWindowResolution.VolumeTweenEnd = false;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		yield return null;
		yield break;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
	private IEnumerator SetFirstChapter()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter1;
		HorrorSceneManager.TrashDoorOpen = true;
		SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.BlackSkyMusic, 0.1f);
		yield return new WaitForSeconds(2f);
		SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.EnemyCrying, 0.3f);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		this.Fish.SetActive(false);
		SingletoneBehaviour<SparrowSound>.Instance.Stop_SfxSound();
		this.Enemy.GetComponent<WindowBugMovement>().Reset();
		this.PlayerCamera.fieldOfView = 80f;
		this.PlayerOutlineCamera.fieldOfView = 80f;
		this.Player.transform.position = new Vector3(380f, 119f, 150f);
		this.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		this.PlayerCameraActive(true, true);
		SingletoneBehaviour<SkyBoxManager>.Instance.RenderSkybox(this.PlayerCamera, 0);
		this.SecondBlackDoor.gameObject.SetActive(false);
		this.CloseOtherDoors();
		this.Eyes.SetActive(true);
		EyesOnMe component = this.Eyes.GetComponent<EyesOnMe>();
		component.CloseEye(true);
		component.canEyeOpen = false;
		this.BlackDoor.GetComponent<BlackRoomOpen>().Reset();
		yield break;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000F4C7 File Offset: 0x0000D6C7
	private IEnumerator SetSecondChapter()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter2;
		HorrorSceneManager.TrashDoorOpen = true;
		this.ActiveReverbFilter(false);
		this.Player.GetComponent<FirstPersonMovement>().enabled = false;
		this.PlayerCamera.fieldOfView = 80f;
		this.PlayerOutlineCamera.fieldOfView = 80f;
		this.WindowBugFace.SetActive(true);
		yield return new WaitForSeconds(2f);
		this.GreenTerrain.SetActive(true);
		this.BlueScreenTerrain.SetActive(false);
		SingletoneBehaviour<SparrowSound>.Instance.Stop_SfxSound();
		this.SecondBlackDoor.Reset();
		this.SecondBlackDoor.gameObject.SetActive(true);
		SingletoneBehaviour<MazeController>.Instance.Reset();
		SingletoneBehaviour<MazeController>.Instance.gameObject.SetActive(false);
		SingletoneBehaviour<MazeController>.Instance.MazeLastBug.GetComponent<MazeBossBug>().Reset();
		SingletoneBehaviour<MazeController>.Instance.MazeLastBug.SetActive(false);
		this.PlayerCamera.fieldOfView = 80f;
		this.PlayerOutlineCamera.fieldOfView = 80f;
		this.Player.transform.position = new Vector3(980f, 108f, 78f);
		this.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		this.WindowBugFace.SetActive(false);
		this.PlayerCameraActive(true, true);
		SingletoneBehaviour<SkyBoxManager>.Instance.DarkSky();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		this.GreenTerrain.transform.GetChild(0).gameObject.SetActive(false);
		this.Eyes.SetActive(false);
		yield break;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000F4D6 File Offset: 0x0000D6D6
	private IEnumerator SetSecondChapter_Start()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter2;
		RenderSettings.fogDensity = 0f;
		HorrorSceneManager.TrashDoorOpen = true;
		this.ActiveReverbFilter(false);
		this.Player.GetComponent<FirstPersonMovement>().enabled = false;
		this.PlayerCamera.fieldOfView = 80f;
		this.PlayerOutlineCamera.fieldOfView = 80f;
		this.WindowBugFace.SetActive(true);
		yield return new WaitForSeconds(2f);
		this.GreenTerrain.SetActive(true);
		this.BlueScreenTerrain.SetActive(false);
		SingletoneBehaviour<SparrowSound>.Instance.Stop_SfxSound();
		this.SecondBlackDoor.gameObject.SetActive(true);
		this.PlayerCamera.fieldOfView = 80f;
		this.PlayerOutlineCamera.fieldOfView = 80f;
		this.Player.transform.position = new Vector3(980f, 108f, 78f);
		this.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		this.WindowBugFace.SetActive(false);
		this.PlayerCameraActive(true, true);
		SingletoneBehaviour<SkyBoxManager>.Instance.DarkSky();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		this.GreenTerrain.transform.GetChild(0).gameObject.SetActive(false);
		this.Eyes.SetActive(false);
		yield break;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000F4E5 File Offset: 0x0000D6E5
	private IEnumerator SetThirdChapter()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter3;
		this.PlayerMovementActive(false);
		this.PlayerCameraActive(false, true);
		HorrorSceneManager.TrashDoorOpen = false;
		SingletoneBehaviour<SkyBoxManager>.Instance.DarkSky();
		this.Map.SetActive(true);
		HorrorSceneManager.GreenDoorOpen = false;
		HorrorSceneManager.SystemDoorOpen = true;
		this.trashDoor.myDoor.isLocked = true;
		this.GreenTerrain.SetActive(false);
		this.TrashTerrain.SetActive(false);
		this.BlackDoor.SetActive(false);
		this.SecondBlackDoor.gameObject.SetActive(false);
		this.SaturationAdjust.SetActive(false);
		this.BlackTerrain.SetActive(true);
		this.BlackTerrainForMinimap.SetActive(true);
		this.BlackObjects.SetActive(true);
		SingletoneBehaviour<HorrorVolumeManager>.Instance.SetColor(-20f);
		RenderSettings.fogDensity = 0.18f;
		SingletoneBehaviour<SystemWinionRoomManager>.Instance.SystemWinionRoom.SetZeroStage();
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 0;
		yield return new WaitUntil(() => MyPcWindowResolution.VolumeTweenEnd);
		MyPcWindowResolution.VolumeTweenEnd = false;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		this.PlayerMovementActive(true);
		this.PlayerCameraActive(true, true);
		yield break;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00024918 File Offset: 0x00022B18
	public void CloseOtherDoors()
	{
		foreach (DoorInteraction doorInteraction in this.otherDoor)
		{
			doorInteraction.Closing(true, true);
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0002496C File Offset: 0x00022B6C
	public void FixedUpdate()
	{
		if (this.gridText.bounds.size.x != 0f)
		{
			this.gridBubble.localScale = this.gridText.bounds.size + new Vector3(2f, 2f, 1f);
		}
		else if (this.gridText.bounds.size.x == 0f)
		{
			this.gridBubble.localScale = Vector3.zero;
		}
		if (this.boText.bounds.size.x != 0f)
		{
			this.boBubble.localScale = this.boText.bounds.size + new Vector3(2f, 2f, 1f);
			return;
		}
		if (this.boText.bounds.size.x == 0f)
		{
			this.boBubble.localScale = Vector3.zero;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00024A88 File Offset: 0x00022C88
	private void Update()
	{
		if (HorrorSceneManager.CanOpenOption && Input.GetKeyDown(27) && !this.OptionAlreadyOpen)
		{
			this.OptionAlreadyOpen = true;
			this.HorrorScene_option.SetActive(true);
		}
		if (MyPcWindowResolution.chapter < HorrorChapter.Chapter2)
		{
			if (!this.firstObjectDrop && this.Player.transform.position.x > 200f)
			{
				this.firstObjectDrop = true;
				this.chairs = base.GetComponent<HorrorItemDrop>().DropObject();
			}
			if (!this.firstBugAppear && this.Player.transform.position.x > 320f)
			{
				this.firstBugAppear = true;
				base.StartCoroutine("FirstEnemyAppear");
				this.Enemy.SetActive(true);
				this.Enemy.GetComponent<WindowBugMovement>().StartMove();
				this.Enemy.GetComponent<WindowBugMovement>().ResetArrive = true;
			}
		}
		if (this.Player.transform.position.y < 30f)
		{
			this.Player.transform.position = new Vector3(380f, 119f, 150f);
			this.SetChapter(HorrorChapter.Chapter1);
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
	private IEnumerator SetFourChapter()
	{
		this.PlayerMovementActive(false);
		this.PlayerCameraActive(false, true);
		HorrorSceneManager.TrashDoorOpen = false;
		this.Map.SetActive(false);
		this.SaturationAdjust.SetActive(false);
		this.Directory.SetActive(false);
		this.BlackDoor.SetActive(false);
		this.BlackTerrain.SetActive(true);
		this.BlackTerrainForMinimap.SetActive(true);
		SingletoneBehaviour<SystemWinionRoomManager>.Instance.SystemWinionRoom.BigRoom.SetActive(true);
		SingletoneBehaviour<SystemWinionRoomManager>.Instance.SystemWinionRoomObject.SetActive(false);
		SingletoneBehaviour<SystemWinionRoomManager>.Instance.SystemWinionRoom.ZeroStage.SetActive(false);
		this.GreenTerrain.SetActive(false);
		RenderSettings.fogDensity = 0.18f;
		SingletoneBehaviour<Last3D_Manager>.Instance.SetDefault();
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 8;
		yield return new WaitUntil(() => MyPcWindowResolution.VolumeTweenEnd);
		MyPcWindowResolution.VolumeTweenEnd = false;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		this.PlayerMovementActive(true);
		this.PlayerCameraActive(true, true);
		this.serverOpen = false;
		yield break;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000F503 File Offset: 0x0000D703
	private IEnumerator FirstEnemyAppear()
	{
		MyPcWindowResolution.chapter = HorrorChapter.Chapter4;
		yield return null;
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		HorrorSceneManager.dialogueNum = 4;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		yield return new WaitForSeconds(1f);
		HorrorSceneManager.dialogueNum = 5;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield break;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000F50B File Offset: 0x0000D70B
	public void SettingsVolum(Volume _volume)
	{
		if (this.volume != null)
		{
			this.volume = _volume;
		}
		this.volumeProfile = this.volume.profile;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000F533 File Offset: 0x0000D733
	public void ShakeCamera(float duration = 1f)
	{
		ShortcutExtensions.DOPunchPosition(this.PlayerCamera.transform, new Vector3(0.2f, 0.2f, 0.2f), duration, 50, 0f, false);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000F563 File Offset: 0x0000D763
	public void BlackRoomOpen()
	{
		if (this.BlackObjects.activeSelf)
		{
			this.BlackObjects.GetComponent<SystemWinionRoomOpen>().BlackRoomOpen();
			return;
		}
		this.BlackRoomDoor.isLocked = true;
		this.BlackRoomDoor.OpenDoor();
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000F59A File Offset: 0x0000D79A
	private IEnumerator SetFalseFirstChapter()
	{
		this.GreenTerrain.SetActive(false);
		this.Fish.SetActive(false);
		this.TrashTerrain.SetActive(false);
		yield return null;
		yield break;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000F5A9 File Offset: 0x0000D7A9
	private IEnumerator SetFalseSecondChapter()
	{
		this.BlueScreenTerrain.SetActive(false);
		this.BlueScreenTerrainCenter.SetActive(false);
		yield return null;
		yield break;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
	private IEnumerator SetFalseThirdChapter()
	{
		this.BlackTerrain.SetActive(false);
		this.BlackTerrainForMinimap.SetActive(false);
		this.BlackObjects.SetActive(false);
		this.BlackRoomDoor.isLocked = false;
		this.BlackRoomDoor.Closing(false, false);
		this.BlackRoomDoor.isLocked = true;
		yield return null;
		yield break;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
	private IEnumerator SetFalseFourChapter()
	{
		SingletoneBehaviour<Last3D_Manager>.Instance.transform.GetChild(0).gameObject.SetActive(false);
		yield return null;
		yield break;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00024BB4 File Offset: 0x00022DB4
	public void FadeOutBlack(float duration = 1f, Action action = null)
	{
		this.BlackPanel.DOFade(1f, duration).OnComplete(delegate
		{
			Action action2 = action;
			if (action2 == null)
			{
				return;
			}
			action2();
		});
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00024BF4 File Offset: 0x00022DF4
	public void FadeInWhite(float duration = 1f, Action action = null)
	{
		this.BlackPanel.DOFade(0f, duration).OnComplete(delegate
		{
			Action action2 = action;
			if (action2 == null)
			{
				return;
			}
			action2();
		});
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000F5CF File Offset: 0x0000D7CF
	public void First3DGameEnd()
	{
		base.StartCoroutine("First3DGameEndRoutine");
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000F5DD File Offset: 0x0000D7DD
	private IEnumerator First3DGameEndRoutine()
	{
		yield return new WaitForSeconds(2f);
		this.FadeOutBlack(1f, null);
		yield return new WaitForSeconds(2f);
		EventDialogueController.CurEventDetailNumIs3D = true;
		DBManager.instance.dialogueData.curEventDetailNum = 5;
		Events.AutoChapterIndex = 2;
		Events.StartAutoEvent = true;
		PlayerPrefs.SetInt("3DClear", 1);
		Events.AutoEventIndex = 6;
		SceneLoader.LoadScene("Chapter 02_EunBin", true, false);
		yield break;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000F5EC File Offset: 0x0000D7EC
	public void OpenServerDoor()
	{
		if (this.serverOpen)
		{
			return;
		}
		this.serverOpen = false;
		base.StartCoroutine("ServerOpenDialogue");
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000F60A File Offset: 0x0000D80A
	private IEnumerator ServerOpenDialogue()
	{
		this.Player.GetComponent<Rigidbody>().constraints = 126;
		this.PlayerMovementActive(false);
		HorrorSceneManager.GameNum = 1;
		HorrorSceneManager.dialogueNum = 9;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		int num;
		for (int i = 0; i < 15; i = num + 1)
		{
			SingletoneBehaviour<CommandLineController>.Instance.ShowConsole(StairsEnter.randomHex(6));
			yield return new WaitUntil(() => !SingletoneBehaviour<CommandLineController>.Instance.isWriting);
			yield return new WaitForSeconds(0.1f);
			num = i;
		}
		yield return new WaitForSeconds(0.5f);
		HorrorSceneManager.dialogueNum = 10;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		this.PlayerMovementActive(true);
		this.Player.GetComponent<Rigidbody>().constraints = 80;
		yield break;
	}

	// Token: 0x040001F5 RID: 501
	public static HorrorChapter Chapter = HorrorChapter.Chapter1;

	// Token: 0x040001F6 RID: 502
	public ObjectPoolingSystem objectPoolingSystem;

	// Token: 0x040001F7 RID: 503
	public Volume volume;

	// Token: 0x040001F8 RID: 504
	private VolumeProfile volumeProfile;

	// Token: 0x040001F9 RID: 505
	public CanvasGroup BlackPanel;

	// Token: 0x040001FA RID: 506
	public GameObject GreenTerrain;

	// Token: 0x040001FB RID: 507
	public GameObject TrashTerrain;

	// Token: 0x040001FC RID: 508
	public GameObject Player;

	// Token: 0x040001FD RID: 509
	[HideInInspector]
	public Camera PlayerCamera;

	// Token: 0x040001FE RID: 510
	public Camera PlayerOutlineCamera;

	// Token: 0x040001FF RID: 511
	public static bool GreenDoorOpen;

	// Token: 0x04000200 RID: 512
	public static bool TrashDoorOpen;

	// Token: 0x04000201 RID: 513
	public static bool SystemDoorOpen;

	// Token: 0x04000202 RID: 514
	[Header("GreenDay Instance")]
	public GameObject Enemy;

	// Token: 0x04000203 RID: 515
	public GameObject EnemyLegSound;

	// Token: 0x04000204 RID: 516
	public GameObject EnemyVoiceSound;

	// Token: 0x04000205 RID: 517
	public GameObject EnemyPatrollPositions;

	// Token: 0x04000206 RID: 518
	public GameObject Eyes;

	// Token: 0x04000207 RID: 519
	public GameObject Map;

	// Token: 0x04000208 RID: 520
	public GameObject Directory;

	// Token: 0x04000209 RID: 521
	public GameObject BlackDoor;

	// Token: 0x0400020A RID: 522
	public SecondBlackDoor SecondBlackDoor;

	// Token: 0x0400020B RID: 523
	public GameObject LightingController;

	// Token: 0x0400020C RID: 524
	public GameObject SaturationAdjust;

	// Token: 0x0400020D RID: 525
	public GameObject Fish;

	// Token: 0x0400020E RID: 526
	public TrashCanDoor trashDoor;

	// Token: 0x0400020F RID: 527
	public GameObject BlueScreenTerrain;

	// Token: 0x04000210 RID: 528
	public GameObject BlueScreenTerrainCenter;

	// Token: 0x04000211 RID: 529
	[Header("System Winion Room Instance")]
	public GameObject BlackTerrain;

	// Token: 0x04000212 RID: 530
	public GameObject BlackTerrainForMinimap;

	// Token: 0x04000213 RID: 531
	public GameObject BlackObjects;

	// Token: 0x04000214 RID: 532
	public GameObject MinimapObject;

	// Token: 0x04000215 RID: 533
	public DoorInteraction BlackRoomDoor;

	// Token: 0x04000216 RID: 534
	public GameObject WindowBugFace;

	// Token: 0x04000217 RID: 535
	public AudioReverbFilter caveFilter;

	// Token: 0x04000218 RID: 536
	public AudioReverbFilter caveFilterRun;

	// Token: 0x04000219 RID: 537
	public AudioReverbFilter blackRoomFilter;

	// Token: 0x0400021A RID: 538
	public ParticleSystem firstParticle;

	// Token: 0x0400021B RID: 539
	public ParticleSystem secondParticle;

	// Token: 0x0400021C RID: 540
	public FirstPersonLook FirstCamera;

	// Token: 0x0400021D RID: 541
	public FirstPersonLook SecondCamera;

	// Token: 0x0400021E RID: 542
	public static int GameNum = 0;

	// Token: 0x0400021F RID: 543
	public static int dialogueNum = 0;

	// Token: 0x04000220 RID: 544
	[SerializeField]
	private List<DoorInteraction> otherDoor;

	// Token: 0x04000221 RID: 545
	private MotionBlur _motionBlur;

	// Token: 0x04000222 RID: 546
	public bool firstObjectDrop;

	// Token: 0x04000223 RID: 547
	public bool firstBugAppear;

	// Token: 0x04000224 RID: 548
	public List<GameObject> chairs = new List<GameObject>();

	// Token: 0x04000225 RID: 549
	public TMP_Text gridText;

	// Token: 0x04000226 RID: 550
	public TMP_Text boText;

	// Token: 0x04000227 RID: 551
	public Transform gridBubble;

	// Token: 0x04000228 RID: 552
	public Transform boBubble;

	// Token: 0x04000229 RID: 553
	public bool OptionAlreadyOpen;

	// Token: 0x0400022A RID: 554
	public GameObject HorrorScene_option;

	// Token: 0x0400022B RID: 555
	public static bool CanOpenOption = true;

	// Token: 0x0400022C RID: 556
	public DoorInteraction ServerDoor;

	// Token: 0x0400022D RID: 557
	public bool serverOpen;
}
