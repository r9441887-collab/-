using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class MazeController : SingletoneBehaviour<MazeController>
{
	// Token: 0x060002D5 RID: 725 RVA: 0x0000FCE9 File Offset: 0x0000DEE9
	private void Awake()
	{
		this.firstView = true;
		this.passwords = new int[] { 4, 3, 2, 7, 4, 8, 6, -1, 8 };
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x000287B0 File Offset: 0x000269B0
	public void Reset()
	{
		this.CurrentRoomIndex = -1;
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject.tag == "Hole")
			{
				LongHoleController component = gameObject.GetComponent<LongHoleController>();
				if (!component.isPooling && !component.isLastRoom)
				{
					SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.AddGameObjectPool("LongHolePrefab", gameObject, SingletoneBehaviour<MazeController>.Instance.transform);
				}
				component.SetDefault(true);
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00028840 File Offset: 0x00026A40
	public void GetOtherHoles(GameObject findHole)
	{
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject != findHole)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00028888 File Offset: 0x00026A88
	public GameObject GenerateMaze(Vector3 _pos, Quaternion _rota, Transform _parent)
	{
		GameObject gameObjectPrefab = SingletoneBehaviour<HorrorSceneManager>.Instance.objectPoolingSystem.GetGameObjectPrefab("LongHolePrefab", "SystemPrefabs/", _parent, false);
		gameObjectPrefab.transform.SetParent(_parent);
		gameObjectPrefab.transform.localPosition = _pos;
		gameObjectPrefab.transform.localRotation = _rota;
		gameObjectPrefab.transform.SetParent(base.transform);
		gameObjectPrefab.SetActive(true);
		return gameObjectPrefab;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0000FD0A File Offset: 0x0000DF0A
	public void RemovObject(GameObject target, string objectKey)
	{
		GameManager.instance.objectPoolingSystem.AddGameObjectPool(objectKey, target, SingletoneBehaviour<HorrorSceneManager>.Instance.transform);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x000288EC File Offset: 0x00026AEC
	private void OnEnable()
	{
		GameObject gameObject = this.GenerateMaze(new Vector3(-4f, 0f, 0f), Quaternion.identity, base.transform);
		gameObject.GetComponent<LongHoleController>().OtherHoles.Add(this.StartRoom);
		this.FirstHole = gameObject;
		base.StartCoroutine("DarkSetting");
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000FD27 File Offset: 0x0000DF27
	private IEnumerator DarkSetting()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		if (this.firstView)
		{
			yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
			HorrorSceneManager.dialogueNum = 6;
			this.firstView = false;
			DBManager.instance.dialogueController.StartNextDialogue_3D();
		}
		MazeHoleLight hole = this.FirstHole.GetComponent<MazeHoleLight>();
		hole.SetDarkMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(6, 6, 6, 0));
		yield return new WaitForSeconds(0.5f);
		hole.SetLightMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(50, 50, 50, 0));
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.SwitchSound, false, 1f, 2f);
		yield return new WaitForSeconds(0.2f);
		hole.SetDarkMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(6, 6, 6, 0));
		yield return new WaitForSeconds(0.2f);
		hole.SetLightMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.SwitchSound, false, 1f, 2f);
		yield return new WaitForSeconds(0.35f);
		hole.SetDarkMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLighting(new Color32(6, 6, 6, 0));
		yield return new WaitForSeconds(0.35f);
		hole.SetLightMode();
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(1f);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.SwitchSound, false, 1f, 2f);
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		yield return new WaitForSeconds(0.5f);
		SingletoneBehaviour<MyPcWindowResolution>.Instance.NoSignal.SetActive(true);
		yield break;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00028948 File Offset: 0x00026B48
	private void Update()
	{
		if (this.Center != null)
		{
			float num = Vector3.Distance(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position, this.Center.transform.position);
			num = Mathf.Clamp(num / 8f, 0f, 1f);
			SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(num);
		}
	}

	// Token: 0x04000309 RID: 777
	public List<GameObject> doorSnapper;

	// Token: 0x0400030A RID: 778
	public GameObject MazeLastBug;

	// Token: 0x0400030B RID: 779
	public Material EmissionRed;

	// Token: 0x0400030C RID: 780
	public Material EmissionWhite;

	// Token: 0x0400030D RID: 781
	[SerializeField]
	public MaterialMode LightMode;

	// Token: 0x0400030E RID: 782
	[SerializeField]
	public MaterialMode DarkMode;

	// Token: 0x0400030F RID: 783
	[SerializeField]
	public MaterialMode TransparentMode;

	// Token: 0x04000310 RID: 784
	[SerializeField]
	public List<MaterialMode> WallPaper;

	// Token: 0x04000311 RID: 785
	public GameObject LongHolePrefab;

	// Token: 0x04000312 RID: 786
	public GameObject StartRoom;

	// Token: 0x04000313 RID: 787
	public GameObject FirstHole;

	// Token: 0x04000314 RID: 788
	public int CurrentRoomIndex;

	// Token: 0x04000315 RID: 789
	public bool IsLightTurnOn = true;

	// Token: 0x04000316 RID: 790
	public int[] passwords = new int[] { 4, 3, 2, 7, 4, 8, 6, -1, 8 };

	// Token: 0x04000317 RID: 791
	public bool firstView = true;

	// Token: 0x04000318 RID: 792
	public GameObject Center;
}
