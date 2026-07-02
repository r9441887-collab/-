using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200019D RID: 413
[Serializable]
public class DataManager : SingletoneBehaviour<DataManager>
{
	// Token: 0x06000984 RID: 2436 RVA: 0x00048E6C File Offset: 0x0004706C
	private void Start()
	{
		if (!this.StartLoadData)
		{
			DataManager.LoadPrefs();
			return;
		}
		base.StartCoroutine("LoadData");
		if (!SceneManager.GetActiveScene().name.Contains("Title"))
		{
			this.EnsureWinionInfosLength();
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x000141CA File Offset: 0x000123CA
	private IEnumerator LoadData()
	{
		this.Load();
		yield return new WaitUntil(() => this._dataLoadEnd);
		if (SceneManager.GetActiveScene().name.Contains("Title"))
		{
			this.SetDataForTitle();
		}
		else
		{
			this.SetData();
			GameManager.instance.SettingGameData();
		}
		DataManager.LoadPrefs();
		yield break;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00048EB4 File Offset: 0x000470B4
	public void Save()
	{
		if (SceneManager.GetActiveScene().name.Contains("Title"))
		{
			return;
		}
		this.GetData();
		string text = JsonUtility.ToJson(this, true);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(Application.persistentDataPath + this.savePath);
		binaryFormatter.Serialize(fileStream, text);
		fileStream.Close();
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00048F14 File Offset: 0x00047114
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + this.savePath))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(Application.persistentDataPath + this.savePath, FileMode.Open);
			JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream).ToString(), this);
			this._dataLoadEnd = true;
			fileStream.Close();
			return;
		}
		this.SetDefaultValues();
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00048F7C File Offset: 0x0004717C
	private void GetData()
	{
		this._day = SingletoneBehaviour<WinionCalender>.Instance.currentDay;
		this._curChapter = GameManager.instance.gameData.curChapter;
		for (int i = 0; i < 5; i++)
		{
			this._winionInfos[i] = GameManager.instance.gameData.winions[i].winionStatus.winionInfo;
		}
		this._BoolData.ChargerOpen = SingletoneBehaviour<WinionBatteryCenter>.Instance.ChargerOpen;
		this._BoolData.VaccineOpen = OpenVaccine.installVaccine;
		for (int j = 0; j < this._BoolData.CapturedImage.Count; j++)
		{
			if (SingletoneBehaviour<GalleryManager>.Instance.galleryItems[j].activeSelf)
			{
				this._BoolData.CapturedImage[j] = true;
			}
			else
			{
				this._BoolData.CapturedImage[j] = false;
			}
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00049064 File Offset: 0x00047264
	private void SetDefaultValues()
	{
		this._day = 0;
		List<WinionHandler> winionHandlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < 5; i++)
		{
			this._winionInfos[i] = new WinionInfo();
			this._winionInfos[i] = winionHandlers[i].winionStatus.winionInfo;
		}
		this._BoolData.ChargerOpen = SingletoneBehaviour<WinionBatteryCenter>.Instance.ChargerOpen;
		OpenVaccine.installVaccine = this._BoolData.VaccineOpen;
		this._BoolData.CapturedImage.Clear();
		for (int j = 0; j < SingletoneBehaviour<GalleryManager>.Instance.galleryItems.Count; j++)
		{
			this._BoolData.CapturedImage.Add(false);
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00049120 File Offset: 0x00047320
	private void SetData()
	{
		SingletoneBehaviour<WinionCalender>.Instance.currentDay = this._day;
		for (int i = 0; i < 5; i++)
		{
			GameManager.instance.gameData.winions[i].SetIdleByWinionStatus();
		}
		SingletoneBehaviour<WinionBatteryCenter>.Instance.SetBatteryCenter(this._BoolData.ChargerOpen);
		SingletoneBehaviour<VaccineManager>.Instance.SetVaccine(this._BoolData.VaccineOpen);
		for (int j = 0; j < this._BoolData.CapturedImage.Count; j++)
		{
			if (this._BoolData.CapturedImage[j])
			{
				SingletoneBehaviour<GalleryManager>.Instance.galleryItems[j].SetActive(true);
			}
			else
			{
				SingletoneBehaviour<GalleryManager>.Instance.galleryItems[j].SetActive(false);
			}
		}
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x000491EC File Offset: 0x000473EC
	private void SetDataForTitle()
	{
		for (int i = 0; i < this._BoolData.CapturedImage.Count; i++)
		{
			if (this._BoolData.CapturedImage[i])
			{
				SingletoneBehaviour<GalleryManager>.Instance.galleryItems[i].SetActive(true);
			}
			else
			{
				SingletoneBehaviour<GalleryManager>.Instance.galleryItems[i].SetActive(false);
			}
		}
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00049258 File Offset: 0x00047458
	public static void LoadPrefs()
	{
		DataManager._PlayerName = PlayerPrefs.GetString("PlayerName", "Administrator");
		DataManager._Language = PlayerPrefs.GetInt("Language", 0);
		DataManager._LastChapter = PlayerPrefs.GetInt("LastChapter", 0);
		DataManager._LastEvent = PlayerPrefs.GetInt("LastEvent", 0);
		DataManager._3DClear = PlayerPrefs.GetInt("3DClear", 0) == 1;
		DayInfo.SetLanguage();
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x000492C4 File Offset: 0x000474C4
	private void EnsureWinionInfosLength()
	{
		while (this._winionInfos.Count < 5)
		{
			this._winionInfos.Add(new WinionInfo());
		}
		while (this._winionInfos.Count > 5)
		{
			this._winionInfos.RemoveAt(this._winionInfos.Count - 1);
		}
	}

	// Token: 0x04000A6C RID: 2668
	[Header("데이터 로드를 할까요?")]
	[SerializeField]
	private bool StartLoadData = true;

	// Token: 0x04000A6D RID: 2669
	[Header("저장 경로")]
	public string savePath = "MainData.Save.json";

	// Token: 0x04000A6E RID: 2670
	[Header("진행 날짜")]
	[SerializeField]
	private int _day;

	// Token: 0x04000A6F RID: 2671
	[SerializeField]
	private List<WinionInfo> _winionInfos;

	// Token: 0x04000A70 RID: 2672
	[SerializeField]
	private GameManager.Chapter _curChapter;

	// Token: 0x04000A71 RID: 2673
	[Header("Bool 데이터")]
	[SerializeField]
	public BoolData _BoolData;

	// Token: 0x04000A72 RID: 2674
	[Header("Player 데이터")]
	public static string _PlayerName = "";

	// Token: 0x04000A73 RID: 2675
	[Header("Player 언어")]
	public static int _Language;

	// Token: 0x04000A74 RID: 2676
	public static int _LastChapter;

	// Token: 0x04000A75 RID: 2677
	public static int _LastEvent;

	// Token: 0x04000A76 RID: 2678
	public static bool _3DClear;

	// Token: 0x04000A77 RID: 2679
	private bool _dataLoadEnd;
}
