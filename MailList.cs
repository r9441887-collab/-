using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200045F RID: 1119
[CreateAssetMenu(fileName = "New MailList", menuName = "Mail System/MailList")]
public class MailList : ScriptableObject, ISerializationCallbackReceiver
{
	// Token: 0x06001F51 RID: 8017 RVA: 0x0001C35C File Offset: 0x0001A55C
	private void OnEnable()
	{
		this.database = Resources.Load<MailDatabaseObject>("Mail Database");
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x0001C36E File Offset: 0x0001A56E
	public void ChangeLanguage()
	{
		this.database.ChangeLanguage();
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x0001C37B File Offset: 0x0001A57B
	public int GetMailCount()
	{
		return this.database.Items.Length;
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x000DEBFC File Offset: 0x000DCDFC
	public MailSlot AddItem(int id = -1)
	{
		if (id == -1)
		{
			id = Random.Range(0, this.database.Items.Length);
		}
		for (int i = 0; i < this.Container.Count; i++)
		{
			if (this.Container[i].ID == id)
			{
				return null;
			}
		}
		MailSlot mailSlot = new MailSlot(id, this.database.GetItem[id]);
		this.Container.Add(mailSlot);
		if (!SystemAlram.MuteSound)
		{
			int @int = PlayerPrefs.GetInt("Language", 0);
			int num = 0;
			if (SingletoneBehaviour<MailManager>.Instance.isReservationMail)
			{
				if (@int == 0)
				{
					num = 3;
				}
				else if (@int == 1)
				{
					num = 1;
				}
			}
			else if (@int == 0)
			{
				num = 2;
			}
			else if (@int == 1)
			{
				num = 0;
			}
			SingletoneBehaviour<SystemAlram>.Instance.OnMessageOpen(num);
		}
		return mailSlot;
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x000DECBC File Offset: 0x000DCEBC
	public void SetReadMail(int id)
	{
		for (int i = 0; i < this.Container.Count; i++)
		{
			if (this.Container[i].ID == id)
			{
				this.Container[i].isRead = true;
			}
		}
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x000DED08 File Offset: 0x000DCF08
	public void Save()
	{
		string text = JsonUtility.ToJson(this, true);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(Application.persistentDataPath + this.savePath);
		binaryFormatter.Serialize(fileStream, text);
		fileStream.Close();
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x000DED48 File Offset: 0x000DCF48
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + this.savePath))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(Application.persistentDataPath + this.savePath, FileMode.Open);
			JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream).ToString(), this);
			fileStream.Close();
		}
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x000DEDA0 File Offset: 0x000DCFA0
	public void Clear()
	{
		for (int i = 0; i < this.Container.Count; i++)
		{
			this.Container[i].SetDefault();
		}
		this.Container.Clear();
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x000DEDE0 File Offset: 0x000DCFE0
	public void OnAfterDeserialize()
	{
		if (this.database == null)
		{
			return;
		}
		for (int i = 0; i < this.Container.Count; i++)
		{
			try
			{
				this.Container[i].item = this.database.GetItem[this.Container[i].ID];
			}
			catch (KeyNotFoundException)
			{
			}
		}
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x04001DBE RID: 7614
	public string savePath;

	// Token: 0x04001DBF RID: 7615
	private MailDatabaseObject database;

	// Token: 0x04001DC0 RID: 7616
	public List<MailSlot> Container = new List<MailSlot>();
}
