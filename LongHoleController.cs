using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class LongHoleController : MonoBehaviour
{
	// Token: 0x06000280 RID: 640 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void OnDestroy()
	{
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000FA2C File Offset: 0x0000DC2C
	public IEnumerator SetMiniBug()
	{
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.ShortRising, false, 1f, 1f);
		int index = Random.Range(4, 6);
		MazeDoorInfo targetDoor = this.childDoors[index].transform.parent.GetComponent<MazeDoorInfo>();
		targetDoor.door.isLocked = true;
		Transform child = this.SpawnPosition.GetChild(index);
		this.MiniBug.transform.SetParent(child);
		this.MiniBug.transform.localPosition = Vector3.zero;
		this.MiniBug.transform.localRotation = Quaternion.identity;
		this.MiniBug.SetActive(true);
		yield return new WaitForSeconds(2f);
		targetDoor.door.isLocked = false;
		if (index % 2 == 0)
		{
			targetDoor = this.childDoors[index + 1].transform.parent.GetComponent<MazeDoorInfo>();
		}
		else
		{
			targetDoor = this.childDoors[index - 1].transform.parent.GetComponent<MazeDoorInfo>();
		}
		targetDoor.door.isLocked = true;
		yield return new WaitForSeconds(3f);
		this.MiniBug.SetActive(false);
		targetDoor.door.isLocked = false;
		yield break;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00026AB0 File Offset: 0x00024CB0
	private void Update()
	{
		if (!this.randomTexting)
		{
			this.frontText.text = this.endString;
			this.backText.text = this.endString;
			return;
		}
		this.timer += Time.deltaTime;
		if (this.timer >= 0.1f)
		{
			this.frontText.text = this.GetRandomText();
			this.backText.text = this.frontText.text;
			this.timer = 0f;
		}
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00026B3C File Offset: 0x00024D3C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (this.firstEnter)
			{
				return;
			}
			this.firstEnter = true;
			if (LongHoleController.SirenSound != null && (this.wrongRoomEnter || SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex != 4))
			{
				LongHoleController.SirenSound.DOFade(0f, 0.5f);
			}
			if (this.ActiveMiniBug)
			{
				base.StartCoroutine("SetMiniBug");
				this.ActiveMiniBug = false;
			}
			if (this.doorLocking)
			{
				this.DoorActive(false);
			}
			else
			{
				this.DoorActive(true);
			}
			if (this.changeFootStep)
			{
				FirstPersonAudio.stepSoundChange = true;
				this.changeFootStep = false;
			}
			else
			{
				FirstPersonAudio.stepSoundChange = false;
			}
			if (this.doorOpening)
			{
				this.doorOpening = false;
				base.StopCoroutine(this.doorwing);
				this.SetDoorSpeed(false);
				this.CloseDoor();
			}
			int currentRoomIndex = SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex;
			SingletoneBehaviour<HorrorSceneManager>.Instance.ActiveReverbFilter(false);
			if (currentRoomIndex == 2)
			{
				SingletoneBehaviour<HorrorSceneManager>.Instance.ActiveReverbFilter(true);
			}
			if (this.wrongRoomEnter)
			{
				SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex = 0;
				this.frontText.text = "3-0";
				this.backText.text = "3-0";
			}
			else
			{
				SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex++;
				Debug.Log("Room Index Plus : " + SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex.ToString());
			}
			if (WhatIsLastDoor.LastDoor != null)
			{
				base.StartCoroutine("PoolingNextHole");
			}
			if (SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex == 9)
			{
				Debug.Log("Clear All!");
			}
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00026CD8 File Offset: 0x00024ED8
	public string GetRandomText()
	{
		string text = "abcdefghijklmnopqrstuvwxyz0123456789-~!@#$%^&*()_-+=;':./>?<";
		string text2 = "";
		for (int i = 0; i < 4; i++)
		{
			text2 += text[Random.Range(0, text.Length)].ToString();
		}
		return text2;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000FA3B File Offset: 0x0000DC3B
	private IEnumerator PoolingNextHole()
	{
		DoorInteraction lastDoor = WhatIsLastDoor.LastDoor;
		lastDoor.isLocked = false;
		lastDoor.Closing(false, false);
		lastDoor.isLocked = true;
		Transform transform = lastDoor.transform;
		int num = 0;
		while (transform.tag != "Hole" && transform.parent != null && num++ < 10)
		{
			transform = transform.parent;
		}
		LongHoleController Controller = transform.GetComponent<LongHoleController>();
		if (Controller != null)
		{
			this.OtherHoles.Add(Controller.gameObject);
			this.previousHole = Controller.gameObject;
			yield return TweenExtensions.WaitForCompletion(lastDoor.tween);
			Controller.CloseDoor();
			Controller.GetComponent<MazeHoleLight>().SetLightMode();
			lastDoor.isLocked = true;
			foreach (GameObject gameObject in Controller.OtherHoles)
			{
				if (gameObject.gameObject != base.gameObject)
				{
					if (gameObject.tag == "Hole")
					{
						LongHoleController component = gameObject.GetComponent<LongHoleController>();
						component.gameObject.SetActive(false);
						if (!component.isPooling)
						{
							SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.AddGameObjectPool("LongHolePrefab", gameObject, SingletoneBehaviour<MazeController>.Instance.transform);
						}
						component.SetDefault(true);
					}
					else
					{
						gameObject.SetActive(false);
					}
				}
			}
		}
		this.randomTexting = false;
		yield break;
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00026D20 File Offset: 0x00024F20
	public void TranslateMessage()
	{
		this.texts[0].text = DBManager.instance.GetSettingString("3D_2", 0, 5, 1);
		this.texts[1].text = DBManager.instance.GetSettingString("3D_2", 0, 6, 1);
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000FA4A File Offset: 0x0000DC4A
	private void Start()
	{
		this.TranslateMessage();
		base.transform.tag = "Hole";
		this.childDoors = base.GetComponentsInChildren<DoorInteraction>().ToList<DoorInteraction>();
		this.childInfos = base.GetComponentsInChildren<MazeDoorInfo>().ToList<MazeDoorInfo>();
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000FA84 File Offset: 0x0000DC84
	private void OnDisable()
	{
		this.CloseDoor();
		this.SetDefault(false);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00026D74 File Offset: 0x00024F74
	public void CloseDoor()
	{
		if (this.childDoors.Count <= 0)
		{
			this.childDoors = base.GetComponentsInChildren<DoorInteraction>().ToList<DoorInteraction>();
		}
		foreach (DoorInteraction doorInteraction in this.childDoors)
		{
			doorInteraction.poolDoor = false;
			doorInteraction.isLocked = false;
			doorInteraction.Closing(true, true);
		}
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00026DF4 File Offset: 0x00024FF4
	public void DoorActive(bool active)
	{
		this.childDoors = base.GetComponentsInChildren<DoorInteraction>().ToList<DoorInteraction>();
		foreach (DoorInteraction doorInteraction in this.childDoors)
		{
			doorInteraction.isLocked = !active;
		}
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00026E5C File Offset: 0x0002505C
	public void SetDefault(bool value = false)
	{
		this.isPooling = value;
		this.isLastRoom = false;
		this.Monitor8.SetActive(false);
		this.Monitor9.SetActive(false);
		this.SetNormalRoom();
		this.OtherHoles.Clear();
		this.previousHole = null;
		this.childDoors = base.GetComponentsInChildren<DoorInteraction>().ToList<DoorInteraction>();
		this.childInfos = base.GetComponentsInChildren<MazeDoorInfo>().ToList<MazeDoorInfo>();
		this.firstEnter = false;
		this.wrongRoomEnter = false;
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		this.randomTexting = true;
		base.GetComponent<MazeHoleLight>().ActiveNeonSign(false);
		base.GetComponent<MazeHoleLight>().SetLight(Color.white, false);
		this.holeGhost.Clear();
		this.Japanese.transform.GetChild(0).gameObject.SetActive(false);
		this.Japanese.transform.GetChild(1).gameObject.SetActive(false);
		this.Japanese.transform.GetChild(2).gameObject.SetActive(false);
		this.Japanese.transform.GetChild(3).gameObject.SetActive(false);
		this.LastActionObject.GetComponent<MazeLastAction>().firstEnter = true;
		this.LastActionObject.SetActive(false);
		this.LastDoor.SetActive(false);
		this.SetDoorSpeed(false);
		this.AutoOpenObject.SetActive(false);
		this.WarningBox.SetActive(false);
		this.ActiveMiniBug = false;
		this.changeFootStep = false;
		this.doorLocking = false;
		this.DoorActive(false);
		this.CloseDoor();
		foreach (MazeDoorInfo mazeDoorInfo in this.childInfos)
		{
			mazeDoorInfo.isCorrectDoor = false;
			mazeDoorInfo.nextHole = null;
		}
		this.ArrowObject.SetActive(false);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00027058 File Offset: 0x00025258
	public void SetCorrectSetting()
	{
		this.endString = "3-" + (SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex + 1).ToString();
		this.wrongRoomEnter = false;
		this.doorLocking = false;
		this.DoorActive(false);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000FA93 File Offset: 0x0000DC93
	public void SetWrongSetting()
	{
		this.endString = "3-0";
		this.wrongRoomEnter = true;
		this.doorLocking = false;
		this.DoorActive(false);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000FAB5 File Offset: 0x0000DCB5
	private IEnumerator DoorWing()
	{
		this.doorOpening = true;
		this.SetDoorSpeed(true);
		for (;;)
		{
			yield return new WaitForSeconds(this.doorSpeed);
			for (int i = 0; i < this.childDoors.Count - 1; i++)
			{
				if (Random.Range(0, 10) <= 5)
				{
					this.childDoors[i].poolDoor = true;
					this.childDoors[i].isLocked = false;
					this.childDoors[i].OpenDoor();
				}
			}
		}
		yield break;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000270A0 File Offset: 0x000252A0
	public void SetHorrorSetting()
	{
		int currentRoomIndex = SingletoneBehaviour<MazeController>.Instance.CurrentRoomIndex;
		Debug.Log(currentRoomIndex.ToString() + " : Setting");
		switch (currentRoomIndex)
		{
		case 0:
			this.Monitor8.SetActive(true);
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.ShortCue, false, 1f, 1f);
			base.GetComponent<MazeHoleLight>().SetDarkMode();
			SingletoneBehaviour<MazeController>.Instance.IsLightTurnOn = false;
			SingletoneBehaviour<MazeController>.Instance.Center = this.Center;
			return;
		case 1:
			this.CorrectDoorSound.Play();
			base.GetComponent<MazeHoleLight>().SetWallPapers(1);
			SingletoneBehaviour<MazeController>.Instance.Center = null;
			SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
			this.WarningBox.SetActive(true);
			return;
		case 2:
			base.GetComponent<MazeHoleLight>().SetTransparentMode();
			SingletoneBehaviour<SkyBoxManager>.Instance.RenderSkybox(SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera, 1);
			SingletoneBehaviour<HorrorSceneManager>.Instance.GreenTerrain.SetActive(false);
			return;
		case 3:
			base.GetComponent<MazeHoleLight>().SetWallPapers(0);
			base.GetComponent<MazeHoleLight>().SetLight(new Color32(100, 100, 100, 0), true);
			this.Japanese.transform.GetChild(0).gameObject.SetActive(true);
			this.changeFootStep = true;
			return;
		case 4:
			if (!SingletoneBehaviour<MazeController>.Instance.IsLightTurnOn)
			{
				base.GetComponent<MazeHoleLight>().SetDarkMode();
			}
			this.ActiveMiniBug = true;
			base.GetComponent<MazeHoleLight>().SetLight(new Color32(byte.MaxValue, 0, 30, 0), true);
			base.GetComponent<MazeHoleLight>().LightBlickInfinity();
			LongHoleController.SirenSound = SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.SirenSound, true, 0.3f, 1f);
			LongHoleController.SirenSound.DOFade(0.6f, 0.5f);
			return;
		case 5:
			this.Monitor9.SetActive(true);
			this.doorwing = base.StartCoroutine("DoorWing");
			this.Japanese.transform.GetChild(0).gameObject.SetActive(true);
			this.Japanese.transform.GetChild(1).gameObject.SetActive(true);
			this.Japanese.transform.GetChild(2).gameObject.SetActive(true);
			this.Japanese.transform.GetChild(3).gameObject.SetActive(true);
			base.GetComponent<MazeHoleLight>().LightBlink();
			return;
		case 6:
			base.GetComponent<MazeHoleLight>().ActiveNeonSign(true);
			this.doorLocking = true;
			this.DoorActive(false);
			this.holeGhost.SetSecond(this);
			return;
		case 7:
			this.LastActionObject.SetActive(true);
			this.doorLocking = true;
			this.DoorActive(false);
			return;
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
			this.AutoOpenObject.SetActive(true);
			this.SetNextCorrectDoor();
			this.SetNormalRoom();
			this.doorLocking = true;
			this.DoorActive(false);
			return;
		case 17:
			Debug.Log("Arrive End Room");
			this.NormalRoom.SetActive(false);
			this.EndRoom.SetActive(false);
			this.AutoOpenObject.SetActive(false);
			this.EnemyDoorSnapper.SetActive(false);
			this.LastRoom.SetActive(true);
			this.isLastRoom = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000273F0 File Offset: 0x000255F0
	public void SetDoorSpeed(bool value)
	{
		if (this.childDoors.Count <= 0)
		{
			this.childDoors = base.GetComponentsInChildren<DoorInteraction>().ToList<DoorInteraction>();
		}
		if (value)
		{
			for (int i = 0; i < this.childDoors.Count; i++)
			{
				this.childDoors[i].openDuration = 200f;
				this.childDoors[i].closeDuration = 300f;
			}
			return;
		}
		for (int j = 0; j < this.childDoors.Count; j++)
		{
			this.childDoors[j].openDuration = 50f;
			this.childDoors[j].closeDuration = 150f;
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x000274A4 File Offset: 0x000256A4
	public void SetNextCorrectDoor()
	{
		if (this.childInfos.Count <= 0)
		{
			this.childInfos = base.GetComponentsInChildren<MazeDoorInfo>().ToList<MazeDoorInfo>();
		}
		int num = Random.Range(2, this.childInfos.Count);
		Debug.Log("Answer : " + num.ToString());
		this.childInfos[num].isCorrectDoor = true;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
	public void SetNormalRoom()
	{
		this.NormalRoom.SetActive(true);
		this.EndRoom.SetActive(false);
		this.LastRoom.SetActive(false);
		this.EnemyDoorSnapper.SetActive(false);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
	public void SetEndRoom()
	{
		this.NormalRoom.SetActive(false);
		this.EndRoom.SetActive(true);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000FB10 File Offset: 0x0000DD10
	public void PopUp()
	{
		SingletoneBehaviour<PopUpMessage>.Instance.PopUp();
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000E953 File Offset: 0x0000CB53
	public void PopDown()
	{
		SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
	}

	// Token: 0x040002A3 RID: 675
	public GameObject NormalRoom;

	// Token: 0x040002A4 RID: 676
	public GameObject EndRoom;

	// Token: 0x040002A5 RID: 677
	public GameObject LastRoom;

	// Token: 0x040002A6 RID: 678
	public GameObject AutoOpenObject;

	// Token: 0x040002A7 RID: 679
	public GameObject EnemyDoorSnapper;

	// Token: 0x040002A8 RID: 680
	public List<GameObject> OtherHoles = new List<GameObject>();

	// Token: 0x040002A9 RID: 681
	public GameObject previousHole;

	// Token: 0x040002AA RID: 682
	public List<DoorInteraction> childDoors = new List<DoorInteraction>();

	// Token: 0x040002AB RID: 683
	public List<MazeDoorInfo> childInfos = new List<MazeDoorInfo>();

	// Token: 0x040002AC RID: 684
	public bool firstEnter;

	// Token: 0x040002AD RID: 685
	public TextMeshPro frontText;

	// Token: 0x040002AE RID: 686
	public TextMeshPro backText;

	// Token: 0x040002AF RID: 687
	public bool wrongRoomEnter;

	// Token: 0x040002B0 RID: 688
	public string endString = "3-0";

	// Token: 0x040002B1 RID: 689
	public bool randomTexting;

	// Token: 0x040002B2 RID: 690
	public bool ActiveMiniBug;

	// Token: 0x040002B3 RID: 691
	public bool doorLocking;

	// Token: 0x040002B4 RID: 692
	public bool changeFootStep;

	// Token: 0x040002B5 RID: 693
	private float timer;

	// Token: 0x040002B6 RID: 694
	public GameObject WarningBox;

	// Token: 0x040002B7 RID: 695
	public GameObject MiniBug;

	// Token: 0x040002B8 RID: 696
	public Transform SpawnPosition;

	// Token: 0x040002B9 RID: 697
	public MazeHoleGhost holeGhost;

	// Token: 0x040002BA RID: 698
	public GameObject Japanese;

	// Token: 0x040002BB RID: 699
	public GameObject Center;

	// Token: 0x040002BC RID: 700
	public GameObject LastActionObject;

	// Token: 0x040002BD RID: 701
	public GameObject LastDoor;

	// Token: 0x040002BE RID: 702
	public AudioSource CorrectDoorSound;

	// Token: 0x040002BF RID: 703
	public GameObject ArrowObject;

	// Token: 0x040002C0 RID: 704
	public GameObject Monitor8;

	// Token: 0x040002C1 RID: 705
	public GameObject Monitor9;

	// Token: 0x040002C2 RID: 706
	public List<TextMeshPro> texts;

	// Token: 0x040002C3 RID: 707
	public static AudioSource SirenSound;

	// Token: 0x040002C4 RID: 708
	public bool isPooling;

	// Token: 0x040002C5 RID: 709
	private Coroutine doorwing;

	// Token: 0x040002C6 RID: 710
	public bool doorOpening;

	// Token: 0x040002C7 RID: 711
	public float doorSpeed = 0.1f;

	// Token: 0x040002C8 RID: 712
	public bool isLastRoom;
}
