using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200045E RID: 1118
[CreateAssetMenu(fileName = "New Mail Database", menuName = "Mail System/Database")]
public class MailDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
	// Token: 0x06001F4D RID: 8013 RVA: 0x000DEAA8 File Offset: 0x000DCCA8
	public void OnAfterDeserialize()
	{
		this.GetId = new Dictionary<MailSO, int>();
		this.GetItem = new Dictionary<int, MailSO>();
		for (int i = 0; i < this.Items.Length; i++)
		{
			this.GetId.Add(this.Items[i], i);
			this.GetItem.Add(i, this.Items[i]);
		}
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x000DEB08 File Offset: 0x000DCD08
	public void ChangeLanguage()
	{
		PlayerPrefs.GetInt("Language", 0);
		int num = 0;
		while (num < this.Items.Length && num != DBManager.instance.mailContents.Count)
		{
			this.Items[num].data.title = DBManager.instance.GetSettingString("", num, 0, 2);
			this.Items[num].data.name = DBManager.instance.GetSettingString("", num, 1, 2);
			this.Items[num].data.content = DBManager.instance.GetSettingString("", num, 2, 2);
			if (DBManager.instance.mailContents[num].Line_List.Count == 4)
			{
				this.Items[num].data.fileName = DBManager.instance.GetSettingString("", num, 3, 2);
			}
			num++;
		}
	}

	// Token: 0x04001DBB RID: 7611
	public MailSO[] Items;

	// Token: 0x04001DBC RID: 7612
	public Dictionary<MailSO, int> GetId = new Dictionary<MailSO, int>();

	// Token: 0x04001DBD RID: 7613
	public Dictionary<int, MailSO> GetItem = new Dictionary<int, MailSO>();
}
